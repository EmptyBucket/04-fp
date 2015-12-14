using Ninject;

namespace WordCloudMVVM.ViewModel
{
    public class ViewModelLocator
    {
        private static IKernel mKernel;

        static ViewModelLocator()
        {
            mKernel = new StandardKernel(new KernelModule());
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main => mKernel.Get<MainViewModel>();

        public static void Cleanup()
        {
        }
    }
}