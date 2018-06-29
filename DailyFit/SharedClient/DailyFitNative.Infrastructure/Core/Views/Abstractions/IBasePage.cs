using DailyFitNative.Infrastructure.Utilities.Navigation;

namespace DailyFitNative.Infrastructure.Core.Views.Abstractions
{
    public interface IBasePage
    {
        ViewId ViewId { get; set; }

        void OnPagePopped();

	    void Init();
	}
}
