using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace UrbanUI.WPF.HybridControls
{
    /// <summary>
    /// Interaction logic for ToggleButtonControl.xaml
    /// </summary>
    partial class ToggleButtonControl : UserControl
    {

        #region enums
        public enum ToggleState
        {
            ToggleOff,
            ToggleON
        }
        #endregion


        #region Getters/ Setters
        public ToggleState State
        {
            get { return (ToggleState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        public String LeftButtonText
        {
            get { return (String)GetValue(LeftButtonTextProperty); }
            set { SetValue(LeftButtonTextProperty, value); }
        }

        public String RightButtonText
        {
            get { return (String)GetValue(RightButtonTextProperty); }
            set { SetValue(RightButtonTextProperty, value); }
        }

        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public Brush ToggledBackground
        {
            get { return (Brush)GetValue(ToggledBackgroundProperty); }
            set { SetValue(ToggledBackgroundProperty, value); }
        }

        public Brush ToggledForeground
        {
            get { return (Brush)GetValue(ToggledForegroundProperty); }
            set { SetValue(ToggledForegroundProperty, value); }
        }

        public new Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public new Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public FontFamily ToggledFontFamily
        {
            get { return (FontFamily)GetValue(ToggledFontFamilyProperty); }
            set { SetValue(ToggledFontFamilyProperty, value); }
        }

        public new FontFamily FontFamily
        {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public double ToggledFontSize
        {
            get { return (double)GetValue(ToggledFontSizeProperty); }
            set { SetValue(ToggledFontSizeProperty, value); }
        }

        public new double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public FontWeight ToggledFontWeight
        {
            get { return (FontWeight)GetValue(ToggledFontWeightProperty); }
            set { SetValue(ToggledFontWeightProperty, value); }
        }

        public new FontWeight FontWeight
        {
            get { return (FontWeight)GetValue(FontWeightProperty); }
            set { SetValue(FontWeightProperty, value); }
        }

        public FontStyle ToggledFontStyle
        {
            get { return (FontStyle)GetValue(ToggledFontStyleProperty); }
            set { SetValue(ToggledFontStyleProperty, value); }
        }

        public new FontStyle FontStyle
        {
            get { return (FontStyle)GetValue(FontStyleProperty); }
            set { SetValue(FontStyleProperty, value); }
        }
        #endregion


        #region Dependency Properties
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(ToggleState), typeof(ToggleButtonControl), new PropertyMetadata(ToggleState.ToggleOff, new PropertyChangedCallback(StatePropertyChanged)));

        public static readonly DependencyProperty LeftButtonTextProperty =
            DependencyProperty.Register("LeftButtonText", typeof(String), typeof(ToggleButtonControl), new PropertyMetadata("ToggleOff", new PropertyChangedCallback(UIChanged)));

        public static readonly DependencyProperty RightButtonTextProperty =
            DependencyProperty.Register("RightButtonText", typeof(String), typeof(ToggleButtonControl), new PropertyMetadata("ToggleOn", new PropertyChangedCallback(UIChanged)));

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(double), typeof(ToggleButtonControl), new PropertyMetadata(20.0, new PropertyChangedCallback(UIChanged)));

        public static readonly DependencyProperty ToggledBackgroundProperty =
            DependencyProperty.Register("ToggledBackground", typeof(Brush), typeof(ToggleButtonControl),
                new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFromString("#F88379"), new PropertyChangedCallback(UIChanged)));

        public static readonly DependencyProperty ToggledForegroundProperty =
            DependencyProperty.Register("ToggledForeground", typeof(Brush), typeof(ToggleButtonControl),
                new PropertyMetadata(Brushes.White, new PropertyChangedCallback(UIChanged)));

        public static new readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(ToggleButtonControl),
                new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFromString("#E4E4E4"), new PropertyChangedCallback(UIChanged)));

        public static new readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(ToggleButtonControl),
                new PropertyMetadata((SolidColorBrush)new BrushConverter().ConvertFromString("#949494"), new PropertyChangedCallback(UIChanged)));

        public static readonly DependencyProperty ToggledFontFamilyProperty =
            DependencyProperty.Register("ToggledFontFamily", typeof(FontFamily), typeof(ToggleButtonControl),
                new PropertyMetadata(new FontFamily("Segoe UI"), new PropertyChangedCallback(UIChanged)));

        public static new readonly DependencyProperty FontFamilyProperty =
            DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(ToggleButtonControl),
                new PropertyMetadata(new FontFamily("Segoe UI"), new PropertyChangedCallback(UIChanged)));

        public static readonly DependencyProperty ToggledFontSizeProperty =
            DependencyProperty.Register("ToggledFontSize", typeof(double), typeof(ToggleButtonControl),
                new PropertyMetadata(17.0, new PropertyChangedCallback(UIChanged)));

        public static new readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register("FontSize", typeof(double), typeof(ToggleButtonControl),
                new PropertyMetadata(17.0, new PropertyChangedCallback(UIChanged)));

        public static readonly DependencyProperty ToggledFontWeightProperty =
            DependencyProperty.Register("ToggledFontWeight", typeof(FontWeight), typeof(ToggleButtonControl),
                new PropertyMetadata(FontWeights.Normal, new PropertyChangedCallback(UIChanged)));

        public static new readonly DependencyProperty FontWeightProperty =
            DependencyProperty.Register("FontWeight", typeof(FontWeight), typeof(ToggleButtonControl),
                new PropertyMetadata(FontWeights.Normal, new PropertyChangedCallback(UIChanged)));

        public static readonly DependencyProperty ToggledFontStyleProperty =
            DependencyProperty.Register("ToggledFontStyle", typeof(FontStyle), typeof(ToggleButtonControl),
                new PropertyMetadata(FontStyles.Normal, new PropertyChangedCallback(UIChanged)));

        public static new readonly DependencyProperty FontStyleProperty =
            DependencyProperty.Register("FontStyle", typeof(FontStyle), typeof(ToggleButtonControl),
                new PropertyMetadata(FontStyles.Normal, new PropertyChangedCallback(UIChanged)));
        #endregion


        #region Dependency Callbacks
        private static void StatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ToggleButtonControl ME = d as ToggleButtonControl;
            ME.ToggleStateChanges();
            ME.InvokeStateMethod((ToggleState)e.NewValue);
        }

        private static void UIChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ToggleButtonControl ME = d as ToggleButtonControl;
            ME.ToggleStateChanges();
        }
        #endregion

        public event EventHandler<StateEventArgs> StateChanged;

        public ToggleButtonControl()
        {
            InitializeComponent();
            ToggleStateChanges();

            buttonLeft.Click += delegate
            {
                if (State != ToggleState.ToggleOff)
                    State = ToggleState.ToggleOff;
            };
            buttonRight.Click += delegate
            {
                if (State != ToggleState.ToggleON)
                    State = ToggleState.ToggleON;
            };
        }
        

        private void InvokeStateMethod(ToggleState newVal)
        {
            StateChanged?.Invoke(this, new StateEventArgs(newVal));
        }

        private void ToggleStateChanges()
        {
            buttonLeft.Content = LeftButtonText;
            buttonRight.Content = RightButtonText;

            buttonLeft.Foreground = State == ToggleState.ToggleOff ? ToggledForeground : Foreground;
            buttonRight.Foreground = State == ToggleState.ToggleON ? ToggledForeground : Foreground;

            BorderbtLeft.Background = State == ToggleState.ToggleOff ? ToggledBackground : Background;
            BorderbtRight.Background = State == ToggleState.ToggleON ? ToggledBackground : Background;

            buttonLeft.FontFamily = State == ToggleState.ToggleOff ? ToggledFontFamily : FontFamily;
            buttonRight.FontFamily = State == ToggleState.ToggleON ? ToggledFontFamily : FontFamily;

            buttonLeft.FontSize = State == ToggleState.ToggleOff ? ToggledFontSize : FontSize;
            buttonRight.FontSize = State == ToggleState.ToggleON ? ToggledFontSize : FontSize;

            buttonLeft.FontWeight = State == ToggleState.ToggleOff ? ToggledFontWeight : FontWeight;
            buttonRight.FontWeight = State == ToggleState.ToggleON ? ToggledFontWeight : FontWeight;

            buttonLeft.FontWeight = State == ToggleState.ToggleOff ? ToggledFontWeight : FontWeight;
            buttonRight.FontWeight = State == ToggleState.ToggleON ? ToggledFontWeight : FontWeight;

            buttonLeft.FontStyle = State == ToggleState.ToggleOff ? ToggledFontStyle : FontStyle;
            buttonRight.FontStyle = State == ToggleState.ToggleON ? ToggledFontStyle : FontStyle;

            BorderbtLeft.CornerRadius = new CornerRadius(CornerRadius, 0, 0, CornerRadius);
            BorderbtRight.CornerRadius = new CornerRadius(0, CornerRadius, CornerRadius, 0);

            BorderbtLeft.Effect = State == ToggleState.ToggleOff ? getCustomDopShadow() : null;
            BorderbtRight.Effect = State == ToggleState.ToggleON ? getCustomDopShadow() : null;

            Panel.SetZIndex(BorderbtLeft, State == ToggleState.ToggleOff ? 1 : 0);
            Panel.SetZIndex(BorderbtRight, State == ToggleState.ToggleON ? 1 : 0);
        }

        private DropShadowEffect getCustomDopShadow()
        {
            DropShadowEffect dropShadowEffect = new DropShadowEffect();
            dropShadowEffect.Color = (Color)System.Windows.Media.ColorConverter.ConvertFromString("#000000");
            dropShadowEffect.Direction = 270;
            dropShadowEffect.ShadowDepth = 8;
            dropShadowEffect.Opacity = 0.16;
            dropShadowEffect.BlurRadius = 16;
            dropShadowEffect.RenderingBias = RenderingBias.Quality;

            return dropShadowEffect;
        }
    }

    public class StateEventArgs: EventArgs
    {
        public ToggleButtonControl.ToggleState State { get; }

        public StateEventArgs(ToggleButtonControl.ToggleState state)
        {
            State = state;
        }
    }
}
