using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using XamlBrewer.Uwp.TextBoxSliderSample.ViewModels;

namespace XamlBrewer.Uwp.TextBoxSliderSample
{
    public sealed partial class SliderPage : Page
    {
        public SliderPage()
        {
            this.InitializeComponent();
            this.DataContext = new MainPageViewModel();
        }

        internal MainPageViewModel Model => (MainPageViewModel)DataContext;

        private void ValueSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            ValueBox.SuggestInput();
        }

        private void ValueSlider2_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            ValueBox2.SuggestInput();
        }
    }
}
