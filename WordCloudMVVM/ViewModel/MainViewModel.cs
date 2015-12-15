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
            Button button = (Button)sender;
            if (PathTextFile == null)
            {
                MessageBox.Show("Specify the path to the text", "Error path file", MessageBoxButton.OK);
                return;
            }
            IndeterminateOpen = true;
            button.IsEnabled = false;
            await Task.Run(() =>
            {
                string text = string.Empty;
                using (FileStream fileStream = new FileStream(mPathTextFile, FileMode.Open))
                    text = Read(fileStream);
                WordWeight[] wordsWeight = Parse(text, Clean, Tokenize).ToArray();
                mGoodWord = wordsWeight
                    .Where(word => !IsBadWord(word.Say));
                mBadWord = wordsWeight
                    .Where(word => IsBadWord(word.Say));
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
                IEnumerable<WordStyle> wordFontSizeList = GoodWordCollection.Concat(BadWordCollection)
                    .Where(wordIsActive => wordIsActive.Active)
                    .Select(word => new WordStyle(word.Say, word.FontSize, word.Color));
                Dictionary<WordStyle, Geometry> geometryWords = BuildGeometryWord(wordFontSizeList, SizeWidth, SizeHeight, MaxFontSize, IntersectionCkeck);
                DrawingImage drawImage = DrawGeometry(geometryWords);
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
            GoodWordCollection = new ObservableCollection<WordModelView>(
                WordWeightToWordStyleConverter.Convert(mGoodWord, MaxFontSize)
                .Select(word => new WordModelView(word.Say, word.FontSize, word.Color, true)));
            BadWordCollection = new ObservableCollection<WordModelView>(
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
                Set(nameof(BitmapImage), ref mBitmapImage, value);
            }
        }
        private ObservableCollection<WordModelView> mGoodWordCollection = new ObservableCollection<WordModelView>();
        public ObservableCollection<WordModelView> GoodWordCollection
        {
            get
            {
                return mGoodWordCollection;
            }
            private set
            {
                Set(nameof(GoodWordCollection), ref mGoodWordCollection, value);
            }
        }
        private ObservableCollection<WordModelView> mBadWordCollection = new ObservableCollection<WordModelView>();
        public ObservableCollection<WordModelView> BadWordCollection
        {
            get
            {
                return mBadWordCollection;
            }
            set
            {
                Set(nameof(BadWordCollection), ref mBadWordCollection, value);
            }
        }

        public RelayCommand OverviewTextFileCommand { get; private set; }
        public RelayCommand<object> OpenTextFileCommand { get; private set; }
        public RelayCommand<object> CreateImageCommand { get; private set; }
        public RelayCommand SaveImageCommand { get; private set; }
        public RelayCommand UpdateMaxFontCommand { get; private set; }

        private string mPathTextFile;
        public string PathTextFile
        {
            get
            {
                return mPathTextFile;
            }
            set
            {
                Set(nameof(PathTextFile), ref mPathTextFile, value);
            }
        }
        public int MaxFontSize { get; set; } = 20;
        public int SizeWidth { get; set; } = 100;
        public int SizeHeight { get; set; } = 100;

        private readonly Func<string, Func<string, string>, Func<string, IEnumerable<string>>, IEnumerable<WordWeight>> Parse;
        private readonly Func<string, IEnumerable<string>> Tokenize;
        private readonly Func<string, string> Clean;
        private readonly Func<Stream, string> Read;
        private readonly Func<string, bool> IsBadWord;
        private readonly Func<Dictionary<WordStyle, Geometry>, DrawingImage> DrawGeometry;
        private readonly Func<IEnumerable<WordStyle>, int, int, int, Func<Geometry, IEnumerable<Geometry>, bool>, Dictionary<WordStyle, Geometry>> BuildGeometryWord;
        private readonly Func<Geometry, IEnumerable<Geometry>, bool> IntersectionCkeck;
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
                Set(nameof(IndeterminateOpen), ref mIndeterminateOpen, value);
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
                Set(nameof(IndeterminateCreate), ref mIndeterminateCreate, value);
            }
        }

        public MainViewModel(
            Func<Stream, string> read,
            Func<Dictionary<WordStyle, Geometry>, DrawingImage> drawGeometry,
            Func<IEnumerable<WordStyle>, int, int, int, Func<Geometry, IEnumerable<Geometry>, bool>, Dictionary<WordStyle, Geometry>> buildGeometryWord,
            Func<Geometry, IEnumerable<Geometry>, bool> intersectionCkeck,
            Func<string, Func<string, string>, Func<string, IEnumerable<string>>, IEnumerable<WordWeight>> parse,
            Func<string, IEnumerable<string>> tokenize,
            Func<string, string> clean,
            Func<string, bool> isBadWord)
        {
            Parse = parse;
            Tokenize = tokenize;
            Clean = clean;
            Read = read;
            DrawGeometry = drawGeometry;
            BuildGeometryWord = buildGeometryWord;
            IntersectionCkeck = intersectionCkeck;
            IsBadWord = isBadWord;
            OverviewTextFileCommand = new RelayCommand(OverviewTextFile);
            OpenTextFileCommand = new RelayCommand<object>(OpenTextFileAsync);
            CreateImageCommand = new RelayCommand<object>(CreateImageAsync);
            SaveImageCommand = new RelayCommand(SaveImage);
            UpdateMaxFontCommand = new RelayCommand(UpdateMaxFont);
        }
    }
}