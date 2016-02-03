using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using WordCloudMVVM.Model;
using WordCloudMVVM.Model.Word;

namespace WordCloudMVVM.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public void OverviewTextFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt",
                DefaultExt = ".txt"
            };

            if (openFileDialog.ShowDialog() == true)
                PathTextFile = openFileDialog.FileName;
        }

        public async void OpenTextFileAsync(object sender)
        {
            if (PathTextFile == null)
            {
                MessageBox.Show("Specify the path to the text", "Error path file", MessageBoxButton.OK);
                return;
            }
            var button = (Button)sender;
            IndeterminateOpen = true;
            button.IsEnabled = false;
            await Task.Run(() =>
            {
                var text = _getTextFromFile(PathTextFile);
                var parsedWords = _getParsedWords(text);
                var inspectedWords = _badWordInspect(parsedWords);
                WordsStyleListBuild(inspectedWords);
            });
            button.IsEnabled = true;
            IndeterminateOpen = false;
        }

        private void WordsStyleListBuild(InspectWords inspectedWords)
        {
            _goodWord = inspectedWords.GoodWords;
            _badWord = inspectedWords.BadWords;
            GoodWordCollection = new List<WordModelView>(
                WordWeightToWordStyleConverter.Convert(_goodWord, MaxFontSize)
                .Select(word => new WordModelView(word.Say, word.FontSize, word.Color, true)));
            BadWordCollection = new List<WordModelView>(
                WordWeightToWordStyleConverter.Convert(_badWord, MaxFontSize)
                .Select(word => new WordModelView(word.Say, word.FontSize, word.Color, false)));
        }

        public async void CreateImageAsync(object sender)
        {
            var button = (Button)sender;
            IndeterminateCreate = true;
            button.IsEnabled = false;
            await Task.Run(() =>
            {
                var styleWords = GoodWordCollection
                    .Concat(BadWordCollection)
                    .Where(wordIsActive => wordIsActive.Active)
                    .Select(word => new WordStyle(word.Say, word.FontSize, word.Color))
                    .ToArray();
                var drawImage = _drawGeometryWords(styleWords, SizeWidth, SizeHeight, MaxFontSize);
                drawImage.Freeze();
                BitmapImage = drawImage;
            });
            button.IsEnabled = true;
            IndeterminateCreate = false;
        }

        public void SaveImage()
        {
            if (BitmapImage == null)
            {
                MessageBox.Show("The image is not created", "Error image", MessageBoxButton.OK);
                return;
            }
            var saveFileDialog = new SaveFileDialog
            {
                DefaultExt = ".png",
                Filter = "Image Files (*.jpg, *.jpeg, *.jpe, *.tif, *.png) | *.jpg; *.jpeg; *.jpe; *.tif; *.png"
            };

            if (saveFileDialog.ShowDialog() != true) return;
            var drawingImage = new Image { Source = BitmapImage };
            var widthDraw = BitmapImage.Drawing.Bounds.Width;
            var heightDraw = BitmapImage.Drawing.Bounds.Height;
            drawingImage.Arrange(new Rect(0, 0, widthDraw, heightDraw));

            const int dpiX = 1000;
            const int dpiY = 1000;

            var width = (int)Math.Floor(widthDraw * dpiX / 96);
            var height = (int)Math.Floor(heightDraw * dpiY / 96);

            var bitmap = new RenderTargetBitmap(width, height, dpiX, dpiY, PixelFormats.Pbgra32);
            bitmap.Render(drawingImage);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            using (var stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                encoder.Save(stream);
        }

        public void UpdateMaxFont()
        {
            GoodWordCollection = new List<WordModelView>(
                WordWeightToWordStyleConverter.Convert(_goodWord, MaxFontSize)
                .Select(word => new WordModelView(word.Say, word.FontSize, word.Color, true)));
            BadWordCollection = new List<WordModelView>(
                WordWeightToWordStyleConverter.Convert(_badWord, MaxFontSize)
                .Select(word => new WordModelView(word.Say, word.FontSize, word.Color, false)));
        }

        private DrawingImage _bitmapImage;
        public DrawingImage BitmapImage
        {
            get
            {
                return _bitmapImage;
            }
            private set
            {
                Set("BitmapImage", ref _bitmapImage, value);
            }
        }
        private List<WordModelView> _goodWordCollection = new List<WordModelView>();
        public List<WordModelView> GoodWordCollection
        {
            get
            {
                return _goodWordCollection;
            }
            set
            {
                Set("GoodWordCollection", ref _goodWordCollection, value);
            }
        }
        private List<WordModelView> _badWordCollection = new List<WordModelView>();
        public List<WordModelView> BadWordCollection
        {
            get
            {
                return _badWordCollection;
            }
            set
            {
                Set("BadWordCollection", ref _badWordCollection, value);
            }
        }

	    private readonly RelayCommand _overviewTextFileCommand;
        public RelayCommand OverviewTextFileCommand { get { return _overviewTextFileCommand; } }

	    private readonly RelayCommand<object> _openTextFileCommand;
        public RelayCommand<object> OpenTextFileCommand { get { return _openTextFileCommand; } }

	    private readonly RelayCommand<object> _createImageCommand;
        public RelayCommand<object> CreateImageCommand { get { return _createImageCommand; } }

	    private readonly RelayCommand _saveImageCommand;
        public RelayCommand SaveImageCommand { get { return _saveImageCommand; } }

	    private readonly RelayCommand _updateMaxFontCommand;
        public RelayCommand UpdateMaxFontCommand { get { return _updateMaxFontCommand; } }

        private string _pathTextFile;
        public string PathTextFile
        {
            get
            {
                return _pathTextFile;
            }
            set
            {
                Set("PathTextFile", ref _pathTextFile, value);
            }
        }
        private int _maxFontSize = 20;
        public int MaxFontSize { get { return _maxFontSize; } set { _maxFontSize = value; } }

        private int _sizeWidth = 100;
        public int SizeWidth { get { return _sizeWidth; } set { _sizeWidth = value; } }

        private int _sizeHeight = 100;
        public int SizeHeight { get { return _sizeHeight; } set { _sizeHeight = value; } }

        private IReadOnlyCollection<WordWeight> _goodWord = new List<WordWeight>();
        private IReadOnlyCollection<WordWeight> _badWord = new List<WordWeight>();

        private bool _indeterminateOpen;
        public bool IndeterminateOpen
        {
            get
            {
                return _indeterminateOpen;
            }
            set
            {
                Set("IndeterminateOpen", ref _indeterminateOpen, value);
            }
        }
        private bool _indeterminateCreate;
        private DrawGeometryWordsDelegate _drawGeometryWords;
        private GetTextFromFileDelegate _getTextFromFile;
        private GetParsedWordsDelegate _getParsedWords;
        private BadWordInspectDelegate _badWordInspect;

        public bool IndeterminateCreate
        {
            get
            {
                return _indeterminateCreate;
            }
            set
            {
                Set("IndeterminateCreate", ref _indeterminateCreate, value);
            }
        }

        public MainViewModel(DrawGeometryWordsDelegate drawGeometryWords, GetTextFromFileDelegate getTextFromFile, GetParsedWordsDelegate getParsedWords, BadWordInspectDelegate badWordInspect)
        {
            _drawGeometryWords = drawGeometryWords;
            _getTextFromFile = getTextFromFile;
            _getParsedWords = getParsedWords;
            _badWordInspect = badWordInspect;

            _overviewTextFileCommand = new RelayCommand(OverviewTextFile);
            _openTextFileCommand = new RelayCommand<object>(OpenTextFileAsync);
            _createImageCommand = new RelayCommand<object>(CreateImageAsync);
            _saveImageCommand = new RelayCommand(SaveImage);
            _updateMaxFontCommand = new RelayCommand(UpdateMaxFont);
        }
    }
}