using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace XamlBrewer.Uwp.Controls
{
    /// <summary>
    /// TextBox that looks like a TextBlock when not editing.
    /// </summary>
    public class SubtleTextBox : TextBox
    {
        private const double HighOpacity = 1.0;
        private bool isInTextBlockMode = false;
        private DispatcherTimer timer = new DispatcherTimer();

        /// <summary>
        /// Registers the LowOpacity dependency property.
        /// </summary>
        public static readonly DependencyProperty LowOpacityProperty = DependencyProperty.Register(
            "LowOpacity", 
            typeof(double), 
            typeof(SubtleTextBox), 
            new PropertyMetadata(0.0));

        /// <summary>
        /// Initializes a new instance of the <see cref="SubtleTextBox"/> class.
        /// </summary>
        public SubtleTextBox()
        {
            Loaded += SubtleTextBox_Loaded; ;
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += Timer_Tick;
        }

        /// <summary>
        /// Gets or sets the lowest opacity for border and background.
        /// </summary>
        /// <value>The low opacity.</value>
        /// <remarks>This is the value used in TextBlock mode.</remarks>
        public double LowOpacity
        {
            get { return (double)GetValue(LowOpacityProperty); }
            set { SetValue(LowOpacityProperty, value); }
        }

        /// <summary>
        /// Makes the control look like a read-only TextBlock.
        /// </summary>
        public void ApplyTextBlockStyle()
        {
            if (isInTextBlockMode)
            {
                return;
            }

            isInTextBlockMode = true;
            Animate(HighOpacity, LowOpacity);
        }

        /// <summary>
        /// Makes the control look like a regular TextBox.
        /// </summary>
        public void ApplyTextBoxStyle()
        {
            if (!isInTextBlockMode)
            {
                return;
            }

            isInTextBlockMode = false;
            Animate(LowOpacity, HighOpacity);
        }

        /// <summary>
        /// Briefly makes the control look like a regular TextBox.
        /// </summary>
        public void SuggestInput()
        {
            ApplyTextBoxStyle();
            timer.Start();
        }

        private void SubtleTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            // Create a specific instance of the brushes to animate.
            // Otherwise it will affect all controls on the surface.
            BorderBrush = new SolidColorBrush((BorderBrush as SolidColorBrush).Color);
            Background = new SolidColorBrush((Background as SolidColorBrush).Color);

            ApplyTextBlockStyle();
        }

        private void Timer_Tick(object sender, object e)
        {
            timer.Stop();
            ApplyTextBlockStyle();
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            timer.Stop();
            ApplyTextBoxStyle();
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            ApplyTextBlockStyle();
            base.OnLostFocus(e);
        }

        private void Animate(double from, double to)
        {
            var storyboard = new Storyboard();

            var animation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = new Duration(TimeSpan.FromMilliseconds(500)),
                EnableDependentAnimation = true
            };
            Storyboard.SetTarget(animation, BorderBrush);
            Storyboard.SetTargetProperty(animation, nameof(BorderBrush.Opacity));
            storyboard.Children.Add(animation);

            animation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = new Duration(TimeSpan.FromMilliseconds(500)),
                EnableDependentAnimation = true
            };
            Storyboard.SetTarget(animation, Background);
            Storyboard.SetTargetProperty(animation, nameof(Background.Opacity));
            storyboard.Children.Add(animation);

            storyboard.Begin();
        }
    }
}
