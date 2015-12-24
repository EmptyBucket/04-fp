using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Text Files (*.txt)|*.txt";
            openFileDialog.DefaultExt = ".txt";
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
            Button button = (Button)sender;
            IndeterminateOpen = true;
            button.IsEnabled = false;
            await Task.Run(() =>
            {
                InspectWords inspectWords = Parse(mPathTextFile);
                mGoodWord = inspectWords.GoodWords;
                mBadWord = inspectWords.BadWords;
                GoodWordCollection = new ObservableCollection<WordModelView>(
                    WordWeightToWordStyleConverter.Convert(mGoodWord, MaxFontSize)
                    .Select(word => new WordModelView(word.Say, word.FontSize, word.Color, true)));
                BadWordCollection = new ObservableCollection<WordModelView>(
                    WordWeightToWordStyleConverter.Convert(mBadWord, MaxFontSize)
                    .Select(word => new WordModelView(word.Say, word.FontSize, word.Color, false)));
            });
            button.IsEnabled = true;
            IndeterminateOpen = false;
        }

        public async void CreateImageAsync(object sender)
        {
            Button button = (Button)sender;
            IndeterminateCreate = true;
            button.IsEnabled = false;
            await Task.Run(() =>
            {
                var styleWords = GoodWordCollection
                    .Concat(BadWordCollection)
                    .Where(wordIsActive => wordIsActive.Active)
                    .Select(word => new WordStyle(word.Say, word.FontSize, word.Color));
                DrawingImage drawImage = DrawGeometryWords(styleWords, SizeWidth, SizeHeight, MaxFontSize);
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
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".png";
            saveFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.jpe, *.tif, *.png) | *.jpg; *.jpeg; *.jpe; *.tif; *.png";

            if (saveFileDialog.ShowDialog() == true)
            {
                var drawingImage = new Image { Source = BitmapImage };
                var widthDraw = BitmapImage.Drawing.Bounds.Width;
                var heightDraw = BitmapImage.Drawing.Bounds.Height;
                drawingImage.Arrange(new Rect(0, 0, widthDraw, heightDraw));

                var dpiX = 1000;
                var dpiY = 1000;

                var width = (int)Math.Floor(widthDraw * dpiX / 96);
                var height = (int)Math.Floor(heightDraw * dpiY / 96);

                var bitmap = new RenderTargetBitmap(width, height, dpiX, dpiY, PixelFormats.Pbgra32);
                bitmap.Render(drawingImage);

                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));

                using (var stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    encoder.Save(stream);
            }
        }

        public void UpdateMaxFont()
        {
            GoodWordCollection = new List<WordModelView>(
                WordWeightToWordStyleConverter.Convert(mGoodWord, MaxFontSize)
                .Select(word => new WordModelView(word.Say, word.FontSize, word.Color, true)));
            BadWordCollection = new List<WordModelView>(
                WordWeightToWordStyleConverter.Convert(mBadWord, MaxFontSize)
                .Select(word => new WordModelView(word.Say, word.FontSize, word.Color, false)));
        }

        private DrawingImage mBitmapImage;
        public DrawingImage BitmapImage
        {
            get
            {
                return mBitmapImage;
            }
            private set
            {
                Set("BitmapImage", ref mBitmapImage, value);
            }
        }
        private IEnumerable<WordModelView> mGoodWordCollection = new List<WordModelView>();
        public IEnumerable<WordModelView> GoodWordCollection
        {
            get
            {
                return mGoodWordCollection;
            }
            set
            {
                Set("GoodWordCollection", ref mGoodWordCollection, value);
            }
        }
        private IEnumerable<WordModelView> mBadWordCollection = new List<WordModelView>();
        public IEnumerable<WordModelView> BadWordCollection
        {
            get
            {
                return mBadWordCollection;
            }
            set
            {
                Set("BadWordCollection", ref mBadWordCollection, value);
            }
        }

	    private RelayCommand overviewTextFileCommand;
        public RelayCommand OverviewTextFileCommand { get { return overviewTextFileCommand; } }

	    private RelayCommand<object> openTextFileCommand;
        public RelayCommand<object> OpenTextFileCommand { get { return openTextFileCommand; } }

	    private RelayCommand<object> createImageCommand;
        public RelayCommand<object> CreateImageCommand { get { return createImageCommand; } }

	    private RelayCommand saveImageCommand;
        public RelayCommand SaveImageCommand { get { return saveImageCommand; } }

	    private RelayCommand updateMaxFontCommand;
        public RelayCommand UpdateMaxFontCommand { get { return updateMaxFontCommand; } }

        private string mPathTextFile;
        public string PathTextFile
        {
            get
            {
                return mPathTextFile;
            }
            set
            {
                Set("PathTextFile", ref mPathTextFile, value);
            }
        }
        private int maxFontSize = 20;
        public int MaxFontSize { get { return maxFontSize; } set { maxFontSize = value; } }

        private int sizeWidth = 100;
        public int SizeWidth { get { return sizeWidth; } set { sizeWidth = value; } }

        private int sizeHeight = 100;
        public int SizeHeight { get { return sizeHeight; } set { sizeHeight = value; } }

        private readonly ParseDelegate Parse;
        private readonly DrawGeometryWordsDelegate DrawGeometryWords;
        private IEnumerable<WordWeight> mGoodWord = new List<WordWeight>();
        private IEnumerable<WordWeight> mBadWord = new List<WordWeight>();

        private bool mIndeterminateOpen = false;
        public bool IndeterminateOpen
        {
            get
            {
                return mIndeterminateOpen;
            }
            set
            {
                Set("IndeterminateOpen", ref mIndeterminateOpen, value);
            }
        }
        private bool mIndeterminateCreate = false;
        public bool IndeterminateCreate
        {
            get
            {
                return mIndeterminateCreate;
            }
            set
            {
                Set("IndeterminateCreate", ref mIndeterminateCreate, value);
            }
        }

        public MainViewModel(DrawGeometryWordsDelegate drawGeometryWords, ParseDelegate parse)
        {
            Parse = parse;
            DrawGeometryWords = drawGeometryWords;
            overviewTextFileCommand = new RelayCommand(OverviewTextFile);
            openTextFileCommand = new RelayCommand<object>(OpenTextFileAsync);
            createImageCommand = new RelayCommand<object>(CreateImageAsync);
            saveImageCommand = new RelayCommand(SaveImage);
            updateMaxFontCommand = new RelayCommand(UpdateMaxFont);
        }
    }
}