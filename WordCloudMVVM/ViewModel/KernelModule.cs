using System;
using System.IO;
using Ninject.Modules;
using WordCloudMVVM.Model;
using WordCloudMVVM.Model.Cloud.Build.Intersection;
using WordCloudMVVM.Model.CloudPaint;
using WordCloudMVVM.Model.Read;
using WordCloudMVVM.Model.WordInspector;
using WordCloudMVVM.Model.WordParse;
using WordCloudMVVM.Model.WordParse.Clean;

namespace WordCloudMVVM.ViewModel
{
    public class KernelModule : NinjectModule
    {
        public override void Load()
        {
            string pathDicitonaryBadWord = Path.Combine(Environment.CurrentDirectory, "InspectorDictionary", "InspectorDictionary.txt");
            string pathAffHunspell = Path.Combine(Environment.CurrentDirectory, "HunspellDictionary", "ru_RU.aff");
            string pathDicionaryHunspell = Path.Combine(Environment.CurrentDirectory, "HunspellDictionary", "ru_RU.dic");

            Bind<ITextReader>().To<TXTReader>();
            Bind<NHunspell.Hunspell>()
                .ToSelf()
                .WithConstructorArgument("affFile", pathAffHunspell)
                .WithConstructorArgument("dictFile", pathDicionaryHunspell);
            Bind<IWordInspector>()
                .To<BadWordInspector>()
                .WithConstructorArgument("path", pathDicitonaryBadWord);
            Bind<ICleaner>().To<TextCleaner>();
            Bind<ITokenizer>().To<StemTokenizer>();
            Bind<IWordWeightParser>().To<WordCountParser>();
            Bind<ICloudBuilder>().To<LineCloudBuilder>();
            Bind<ICloudPainter>().To<CloudPainter>();
            Bind<IIntersectionChecker>().To<IntersectionChecker>();
        }
    }
}
