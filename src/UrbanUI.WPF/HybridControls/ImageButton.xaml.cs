using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace UrbanUI.WPF.HybridControls
{
   /// <summary>
   /// Thanks to Victor Macasaet for creating this amazing WPF Control
   /// </summary>
   public partial class ImageButton : Button
    {
        #region Events
        public event RoutedEventHandler LongPress;
        public event EventHandler<PressHoldEventArgs> PressHold;
        public new event EventHandler<MouseButtonEventArgs> PreviewMouseLeftButtonDown;
        public new event EventHandler<TouchEventArgs> PreviewTouchDown;
        #endregion

        #region Properties
        public new Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public static readonly new DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(ImageButton), new PropertyMetadata(SystemColors.ControlBrush));

        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.Register("PressedBackground", typeof(Brush), typeof(ImageButton), new PropertyMetadata(null));

        public Brush DisabledBackground
        {
            get { return (Brush)GetValue(DisabledBackgroundProperty); }
            set { SetValue(DisabledBackgroundProperty, value); }
        }

        public static readonly DependencyProperty DisabledBackgroundProperty =
            DependencyProperty.Register("DisabledBackground", typeof(Brush), typeof(ImageButton), new PropertyMetadata(null));
        
        public Brush PressedBorderBrush
        {
            get { return (Brush)GetValue(PressedBorderBrushProperty); }
            set { SetValue(PressedBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PressedBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressedBorderBrushProperty =
            DependencyProperty.Register("PressedBorderBrush", typeof(Brush), typeof(ImageButton), new PropertyMetadata(null));
        
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public new Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }
        public static readonly new DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(ImageButton), new PropertyMetadata(Brushes.White));

        public Brush PressedForeground
        {
            get { return (Brush)GetValue(PressedForegroundProperty); }
            set { SetValue(PressedForegroundProperty, value); }
        }

        public static readonly DependencyProperty PressedForegroundProperty =
            DependencyProperty.Register("PressedForeground", typeof(Brush), typeof(ImageButton), new PropertyMetadata(null));

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ImageButton), new PropertyMetadata(new CornerRadius(0)));

        public new Thickness BorderThickness
        {
            get { return (Thickness)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }
        public static readonly new DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(ImageButton), new PropertyMetadata(null));

        public Thickness? PressedBorderThickness
        {
            get { return (Thickness?)GetValue(PressedBorderThicknessProperty); }
            set { SetValue(PressedBorderThicknessProperty, value); }
        }

        public static readonly DependencyProperty PressedBorderThicknessProperty =
            DependencyProperty.Register("PressedBorderThickness", typeof(Thickness?), typeof(ImageButton), new PropertyMetadata(null));

        public new FontFamily FontFamily
        {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }
        public static readonly new DependencyProperty FontFamilyProperty =
            DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(ImageButton), new PropertyMetadata(new FontFamily("Segoe UI")));

        public new FontWeight FontWeight
        {
            get { return (FontWeight)GetValue(FontWeightProperty); }
            set { SetValue(FontWeightProperty, value); }
        }
        public static readonly new DependencyProperty FontWeightProperty =
            DependencyProperty.Register("FontWeight", typeof(FontWeight), typeof(ImageButton), new PropertyMetadata(FontWeights.Bold));

        public new double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
        public static readonly new DependencyProperty FontSizeProperty =
            DependencyProperty.Register("FontSize", typeof(double), typeof(ImageButton), new PropertyMetadata(15.0));


        public bool Checkable
        {
            get { return (bool)GetValue(CheckableProperty); }
            set { SetValue(CheckableProperty, value); }
        }
        public static readonly DependencyProperty CheckableProperty =
            DependencyProperty.Register("Checkable", typeof(bool), typeof(ImageButton), new PropertyMetadata(false,
                (o, e) =>
                {
                    var @this = o as ImageButton;
                    @this.IsChecked = false;
                }));

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(ImageButton), new PropertyMetadata(false));

        public new HorizontalAlignment HorizontalContentAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalContentAlignmentProperty); }
            set { SetValue(HorizontalContentAlignmentProperty, value); }
        }

        public static readonly new DependencyProperty HorizontalContentAlignmentProperty =
            DependencyProperty.Register("HorizontalContentAlignment", typeof(HorizontalAlignment), typeof(ImageButton), new PropertyMetadata(HorizontalAlignment.Center));


        public new VerticalAlignment VerticalContentAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalContentAlignmentProperty); }
            set { SetValue(VerticalContentAlignmentProperty, value); }
        }

        public static readonly new DependencyProperty VerticalContentAlignmentProperty =
            DependencyProperty.Register("VerticalContentAlignment", typeof(VerticalAlignment), typeof(ImageButton), new PropertyMetadata(VerticalAlignment.Center));

        public HorizontalAlignment HorizontalIconAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalIconAlignmentProperty); }
            set { SetValue(HorizontalIconAlignmentProperty, value); }
        }

        public static readonly DependencyProperty HorizontalIconAlignmentProperty =
            DependencyProperty.Register("HorizontalIconAlignment", typeof(HorizontalAlignment), typeof(ImageButton), new PropertyMetadata(HorizontalAlignment.Center));


        public VerticalAlignment VerticalIconAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalIconAlignmentProperty); }
            set { SetValue(VerticalIconAlignmentProperty, value); }
        }

        public static readonly DependencyProperty VerticalIconAlignmentProperty =
            DependencyProperty.Register("VerticalIconAlignment", typeof(VerticalAlignment), typeof(ImageButton), new PropertyMetadata(VerticalAlignment.Center));


        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(ImageButton), new PropertyMetadata(double.NaN));

        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(double), typeof(ImageButton), new PropertyMetadata(double.NaN));

        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));

        public ImageSource PressedIcon
        {
            get { return (ImageSource)GetValue(PressedIconProperty); }
            set { SetValue(PressedIconProperty, value); }
        }

        public static readonly DependencyProperty PressedIconProperty =
            DependencyProperty.Register("PressedIcon", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));

        public ImageSource DisabledIcon
        {
            get { return (ImageSource)GetValue(DisabledIconProperty); }
            set { SetValue(DisabledIconProperty, value); }
        }

        public static readonly DependencyProperty DisabledIconProperty =
            DependencyProperty.Register("DisabledIcon", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));

        public Stretch IconStretch
        {
            get { return (Stretch)GetValue(IconStretchProperty); }
            set { SetValue(IconStretchProperty, value); }
        }

        public static readonly DependencyProperty IconStretchProperty =
            DependencyProperty.Register("IconStretch", typeof(Stretch), typeof(ImageButton), new PropertyMetadata(Stretch.Uniform));

        public TimeSpan LongPressDelay
        {
            get { return (TimeSpan)GetValue(LongPressDelayProperty); }
            set { SetValue(LongPressDelayProperty, value); }
        }

        public static readonly DependencyProperty LongPressDelayProperty =
            DependencyProperty.Register("LongPressDelay", typeof(TimeSpan), typeof(ImageButton), new PropertyMetadata(TimeSpan.FromSeconds(3)));


        public bool LongPressEnabled
        {
            get { return (bool)GetValue(LongPressEnabledProperty); }
            set { SetValue(LongPressEnabledProperty, value); }
        }

        public static readonly DependencyProperty LongPressEnabledProperty =
            DependencyProperty.Register("LongPressEnabled", typeof(bool), typeof(ImageButton), new PropertyMetadata(false));

        public Thickness ContentMargin
        {
            get { return (Thickness)GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }

        public static readonly DependencyProperty ContentMarginProperty =
            DependencyProperty.Register("ContentMargin", typeof(Thickness), typeof(ImageButton), new PropertyMetadata(new Thickness(5)));

        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }

        public static readonly DependencyProperty IconMarginProperty =
            DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(ImageButton), new PropertyMetadata(new Thickness(0)));

        public Effect IconEffect
        {
            get { return (Effect)GetValue(IconEffectProperty); }
            set { SetValue(IconEffectProperty, value); }
        }

        public static readonly DependencyProperty IconEffectProperty =
            DependencyProperty.Register("IconEffect", typeof(Effect), typeof(ImageButton), new PropertyMetadata(null));

        public Effect PressedIconEffect
        {
            get { return (Effect)GetValue(PressedIconEffectProperty); }
            set { SetValue(PressedIconEffectProperty, value); }
        }

        public static readonly DependencyProperty PressedIconEffectProperty =
            DependencyProperty.Register("PressedIconEffect", typeof(Effect), typeof(ImageButton), new PropertyMetadata(null));

        public Effect CheckedIconEffect
        {
            get { return (Effect)GetValue(CheckedIconEffectProperty); }
            set { SetValue(CheckedIconEffectProperty, value); }
        }

        public static readonly DependencyProperty CheckedIconEffectProperty =
            DependencyProperty.Register("CheckedIconEffect", typeof(Effect), typeof(ImageButton), new PropertyMetadata(null));

        public Effect DisabledIconEffect
        {
            get { return (Effect)GetValue(DisabledIconEffectProperty); }
            set { SetValue(DisabledIconEffectProperty, value); }
        }

        public static readonly DependencyProperty DisabledIconEffectProperty =
            DependencyProperty.Register("DisabledIconEffect", typeof(Effect), typeof(ImageButton), new PropertyMetadata(null));
        
        public Brush CheckedForeground
        {
            get { return (Brush)GetValue(CheckedForegroundProperty); }
            set { SetValue(CheckedForegroundProperty, value); }
        }

        public static readonly DependencyProperty CheckedForegroundProperty =
            DependencyProperty.Register("CheckedForeground", typeof(Brush), typeof(ImageButton), new PropertyMetadata(null));

        public Brush CheckedBackground
        {
            get { return (Brush)GetValue(CheckedBackgroundProperty); }
            set { SetValue(CheckedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty CheckedBackgroundProperty =
            DependencyProperty.Register("CheckedBackground", typeof(Brush), typeof(ImageButton), new PropertyMetadata(null));

        public ImageSource CheckedIcon
        {
            get { return (ImageSource)GetValue(CheckedIconProperty); }
            set { SetValue(CheckedIconProperty, value); }
        }

        public static readonly DependencyProperty CheckedIconProperty =
            DependencyProperty.Register("CheckedIcon", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));

        public bool Flipped
        {
            get { return (bool)GetValue(FlippedProperty); }
            set { SetValue(FlippedProperty, value); }
        }

        public static readonly DependencyProperty FlippedProperty =
            DependencyProperty.Register("Flipped", typeof(bool), typeof(ImageButton), new PropertyMetadata(false));


        #endregion

        static int firstMouseDownHash = -1;

        public ImageButton()
        {
            InitializeComponent();
            DataContext = this;

            base.HorizontalContentAlignment = HorizontalAlignment.Center;
            base.VerticalContentAlignment = VerticalAlignment.Center;

            if (PressedBackground == null)
                BindingOperations.SetBinding(this, PressedBackgroundProperty, new Binding("Background"));

            if (CheckedBackground == null)
                BindingOperations.SetBinding(this, CheckedBackgroundProperty, new Binding("PressedBackground"));

            if (DisabledBackground == null)
                BindingOperations.SetBinding(this, DisabledBackgroundProperty, new Binding("Background"));

            if (PressedForeground == null)
                BindingOperations.SetBinding(this, PressedForegroundProperty, new Binding("Foreground"));

            if (CheckedForeground == null)
                BindingOperations.SetBinding(this, CheckedForegroundProperty, new Binding("PressedForeground"));

            if (PressedBorderBrush == null)
                BindingOperations.SetBinding(this, PressedBorderBrushProperty, new Binding("BorderBrush"));

            if (PressedBorderThickness == null)
                BindingOperations.SetBinding(this, PressedBorderThicknessProperty, new Binding("BorderThickness"));

            if (PressedIcon == null)
                BindingOperations.SetBinding(this, PressedIconProperty, new Binding("Icon"));

            if (CheckedIcon == null)
                BindingOperations.SetBinding(this, CheckedIconProperty, new Binding("PressedIcon"));

            if (DisabledIcon == null)
                BindingOperations.SetBinding(this, DisabledIconProperty, new Binding("Icon"));

            if (DisabledIconEffect == null)
                BindingOperations.SetBinding(this, DisabledIconEffectProperty, new Binding("IconEffect"));

            if (PressedIconEffect == null)
                BindingOperations.SetBinding(this, PressedIconEffectProperty, new Binding("IconEffect"));

            if (CheckedIconEffect == null)
                BindingOperations.SetBinding(this, CheckedIconEffectProperty, new Binding("PressedIconEffect"));

            if (IconHeight == double.NaN)
                BindingOperations.SetBinding(this, IconHeightProperty, new Binding("ActualHeight"));


            base.PreviewMouseLeftButtonDown += OnPreviewPress;
            PreviewMouseLeftButtonUp += OnPreviewRelease;
            PreviewMouseMove += OnPreviewTouchMove;
            MouseLeave += OnLeave;

            base.PreviewTouchDown += OnPreviewPress;
            PreviewTouchUp += OnPreviewRelease;
            PreviewTouchMove += OnPreviewTouchMove;
            TouchLeave += OnLeave;
        }

        private void OnLeave(object sender, RoutedEventArgs e)
        {
            if (firstMouseDownHash == GetHashCode())
            {
                PressHold?.Invoke(this, new PressHoldEventArgs(PressHoldEventArgs.EventType.Release));
                firstMouseDownHash = -1;
                IsPressed = false;
            }
        }

        private async void OnPreviewPress(object sender, RoutedEventArgs e)
        {
            if (firstMouseDownHash == -1)
            {
                firstMouseDownHash = GetHashCode();
                IsPressed = true;
                if (e is MouseButtonEventArgs)
                    PreviewMouseLeftButtonDown?.Invoke(sender, e as MouseButtonEventArgs);
                else if (e is TouchEventArgs)
                    PreviewTouchDown?.Invoke(sender, e as TouchEventArgs);
            }

            e.Handled = true;

            PressHold?.Invoke(this, new PressHoldEventArgs(PressHoldEventArgs.EventType.Press));
            if (LongPressEnabled)
            {
                var longPressMSec = LongPressDelay.TotalMilliseconds;
                var longPressStep = 10;
                var longPressStepDelay = longPressMSec / longPressStep;

                while (longPressMSec > 0)
                {
                    if (firstMouseDownHash == -1)
                        break;
                    await Task.Delay(TimeSpan.FromMilliseconds(longPressStepDelay));
                    longPressMSec -= longPressStepDelay;
                }

                if (firstMouseDownHash == GetHashCode())
                {
                    firstMouseDownHash = -1;
                    IsPressed = false;
                    e.Handled = false;
                    LongPress?.Invoke(this, e);
                    e.Handled = true;
                }
            }
        }

        private void OnPreviewRelease(object sender, RoutedEventArgs e)
        {
            var exec = IsPressed && firstMouseDownHash == GetHashCode();

            if (firstMouseDownHash == GetHashCode())
                firstMouseDownHash = -1;

            PressHold?.Invoke(this, new PressHoldEventArgs(PressHoldEventArgs.EventType.Release));

            if (exec)
            {
                OnClick();
            }

            IsPressed = false;
        }

        private void OnPreviewTouchMove(object sender, RoutedEventArgs e)
        {
            if (firstMouseDownHash == GetHashCode())
            {
                var cursorLocation = new Point();

                if (e is TouchEventArgs)
                    cursorLocation = (e as TouchEventArgs).GetTouchPoint(this).Position;
                else if (e is MouseEventArgs)
                    cursorLocation = (e as MouseEventArgs).GetPosition(this);

                var x = cursorLocation.X;
                var y = cursorLocation.Y;

                if (x < 0 || x > ActualWidth || y < 0 || y > ActualHeight)
                {
                    PressHold?.Invoke(this, new PressHoldEventArgs(PressHoldEventArgs.EventType.Release));
                    firstMouseDownHash = -1;
                    IsPressed = false;
                }
            }
        }

        protected override void OnClick()
        {
            if (Checkable)
                IsChecked = !IsChecked;
            base.OnClick();
        }
    }

    public class PressHoldEventArgs : RoutedEventArgs
    {
        public enum EventType
        {
            Press,
            Release
        }

        public EventType Event { get; private set; }

        public PressHoldEventArgs(EventType Event)
        {
            this.Event = Event;
        }
    }
}
