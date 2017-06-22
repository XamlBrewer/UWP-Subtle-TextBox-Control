using Mvvm.Services;
using XamlBrewer.Uwp.TextBoxSliderSample;

namespace Mvvm
{
    internal class ShellViewModel : ViewModelBase
    {
        public ShellViewModel()
        {
            // Build the menus
            Menu.Add(new MenuItem() { Glyph = Icon.GetIcon("TextBoxIcon"), Text = "TextBox", NavigationDestination = typeof(MainPage) });
            Menu.Add(new MenuItem() { Glyph = Icon.GetIcon("SlidersIcon"), Text = "Slider", NavigationDestination = typeof(SliderPage) });

            SecondMenu.Add(new MenuItem() { Glyph = Icon.GetIcon("InfoIcon"), Text = "About", NavigationDestination = typeof(AboutPage) });
        }
    }
}
