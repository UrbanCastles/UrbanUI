using UrbanUI.WPF.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace UrbanUI.WPF.HybridControls
{
    /// <summary>
    /// Thanks to Victor Macasaet for creating this amazing WPF Control
    /// </summary>

    [ContentProperty("Children")]
    public partial class SwipeCanvas : Grid
    {
        public enum InputSource
        {
            Mouse,
            Touch
        }
        
        public event EventHandler<IndexChangedEventArgs> CurrentIndexChanged;


        #region Properties
        public Visibility PageButtonVisibility
        {
            get { return (Visibility)GetValue(PageButtonVisibilityProperty); }
            set { SetValue(PageButtonVisibilityProperty, value); }
        }

        public static readonly DependencyProperty PageButtonVisibilityProperty =
            DependencyProperty.Register("PageButtonVisibility", typeof(Visibility), typeof(SwipeCanvas), new PropertyMetadata(Visibility.Visible));


        public Brush PageButtonIdleBrush
        {
            get { return (Brush)GetValue(PageButtonIdleBrushProperty); }
            set { SetValue(PageButtonIdleBrushProperty, value); }
        }

        public static readonly DependencyProperty PageButtonIdleBrushProperty =
            DependencyProperty.Register("PageButtonIdleBrush", typeof(Brush), typeof(SwipeCanvas), new PropertyMetadata(Brushes.Transparent));


        public Brush PageButtonPressedBrush
        {
            get { return (Brush)GetValue(PageButtonPressedBrushProperty); }
            set { SetValue(PageButtonPressedBrushProperty, value); }
        }

        public static readonly DependencyProperty PageButtonPressedBrushProperty =
            DependencyProperty.Register("PageButtonPressedBrush", typeof(Brush), typeof(SwipeCanvas), new PropertyMetadata((SolidColorBrush)Application.Current.Resources["primaryColor"]));

        public Brush PageButtonSelectedBrush
        {
            get { return (Brush)GetValue(PageButtonSelectedBrushProperty); }
            set { SetValue(PageButtonSelectedBrushProperty, value); }
        }

        public static readonly DependencyProperty PageButtonSelectedBrushProperty =
            DependencyProperty.Register("PageButtonSelectedBrush", typeof(Brush), typeof(SwipeCanvas), new PropertyMetadata((SolidColorBrush)Application.Current.Resources["primaryColor"]));

        public Brush PageButtonBorderBrush
        {
            get { return (Brush)GetValue(PageButtonBorderBrushProperty); }
            set { SetValue(PageButtonBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty PageButtonBorderBrushProperty =
            DependencyProperty.Register("PageButtonBorderBrush", typeof(Brush), typeof(SwipeCanvas), new PropertyMetadata((SolidColorBrush)Application.Current.Resources["primaryColor"]));

        public Thickness PageButtonBorderThickness
        {
            get { return (Thickness)GetValue(PageButtonBorderThicknessProperty); }
            set { SetValue(PageButtonBorderThicknessProperty, value); }
        }

        public static readonly DependencyProperty PageButtonBorderThicknessProperty =
            DependencyProperty.Register("PageButtonBorderThickness", typeof(Thickness), typeof(SwipeCanvas), new PropertyMetadata(new Thickness(1)));

        public double PageButtonSize
        {
            get { return (double)GetValue(PageButtonSizeProperty); }
            set { SetValue(PageButtonSizeProperty, value); }
        }

        public static readonly DependencyProperty PageButtonSizeProperty =
            DependencyProperty.Register("PageButtonSize", typeof(double), typeof(SwipeCanvas), new PropertyMetadata(30.0));

        public Thickness PageButtonMargin
        {
            get { return (Thickness)GetValue(PageButtonMarginProperty); }
            set { SetValue(PageButtonMarginProperty, value); }
        }

        public static readonly DependencyProperty PageButtonMarginProperty =
            DependencyProperty.Register("PageButtonMargin", typeof(Thickness), typeof(SwipeCanvas), new PropertyMetadata(new Thickness(7)));

        public Thickness PageButtonSelectedMargin
        {
            get { return (Thickness)GetValue(PageButtonSelectedMarginProperty); }
            set { SetValue(PageButtonSelectedMarginProperty, value); }
        }

        public static readonly DependencyProperty PageButtonSelectedMarginProperty =
            DependencyProperty.Register("PageButtonSelectedMargin", typeof(Thickness), typeof(SwipeCanvas), new PropertyMetadata(new Thickness(5)));

        public CornerRadius PageButtonCornerRadius
        {
            get { return (CornerRadius)GetValue(PageButtonCornerRadiusProperty); }
            set { SetValue(PageButtonCornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty PageButtonCornerRadiusProperty =
            DependencyProperty.Register("PageButtonCornerRadius", typeof(CornerRadius), typeof(SwipeCanvas), new PropertyMetadata(new CornerRadius(30)));

        public double PageButtonOffset
        {
            get { return (double)GetValue(PageButtonOffsetProperty); }
            set { SetValue(PageButtonOffsetProperty, value); }
        }

        public static readonly DependencyProperty PageButtonOffsetProperty =
            DependencyProperty.Register("PageButtonOffset", typeof(double), typeof(SwipeCanvas), new PropertyMetadata(10.0));


        public Thickness PageMargin
        {
            get { return (Thickness)GetValue(PageMarginProperty); }
            set { SetValue(PageMarginProperty, value); }
        }

        public static readonly DependencyProperty PageMarginProperty =
            DependencyProperty.Register("PageMargin", typeof(Thickness), typeof(SwipeCanvas), new PropertyMetadata(new Thickness(0)));


        public int ColumnCount
        {
            get { return (int)GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }

        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.Register("ColumnCount", typeof(int), typeof(SwipeCanvas), new PropertyMetadata(1,
                (o, e) =>
                {
                    var @this = o as SwipeCanvas;

                    foreach (var container in @this.Containers)
                    {
                        container.ColumnDefinitions.Clear();
                        for (int i = 0; i < (int)e.NewValue; i++)
                        {
                            container.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                        }
                    }
                }), (v) =>
                {
                    return (int)v > 0;
                });

        public int RowCount
        {
            get { return (int)GetValue(RowCountProperty); }
            set { SetValue(RowCountProperty, value); }
        }

        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.Register("RowCount", typeof(int), typeof(SwipeCanvas), new PropertyMetadata(1,
                (o, e) =>
                {
                    var @this = o as SwipeCanvas;

                    foreach (var container in @this.Containers)
                    {
                        container.RowDefinitions.Clear();
                        for (int i = 0; i < (int)e.NewValue; i++)
                        {
                            container.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                        }
                    }
                }), (v) =>
                {
                    return (int)v > 0;
                });

        public int CurrentIndex
        {
            get { return (int)GetValue(CurrentIndexProperty); }
            set { SetValue(CurrentIndexProperty, value); }
        }

        public static readonly DependencyProperty CurrentIndexProperty =
            DependencyProperty.Register("CurrentIndex", typeof(int), typeof(SwipeCanvas), new PropertyMetadata(-1,
                (o, e) =>
                {
                    if (e.NewValue != e.OldValue)
                    {
                        var @this = o as SwipeCanvas;
                        if (@this.PageCount == 0)
                            @this.CurrentIndex = -1;
                        else if ((int)e.NewValue != -1)
                        {
                            @this.CurrentIndexChanged?.Invoke(@this, new IndexChangedEventArgs((int)e.NewValue, (int)e.OldValue));
                            @this.InitItemsLocation((int)e.NewValue);
                        }
                    }
                }));

        public double SwipeThreshold
        {
            get { return (double)GetValue(SwipeThresholdProperty); }
            set { SetValue(SwipeThresholdProperty, value); }
        }

        public static readonly DependencyProperty SwipeThresholdProperty =
            DependencyProperty.Register("SwipeThreshold", typeof(double), typeof(SwipeCanvas), new PropertyMetadata(10.0));

        public double PageChangeOffset
        {
            get { return (double)GetValue(PageChangeOffsetProperty); }
            set { SetValue(PageChangeOffsetProperty, value); }
        }

        public static readonly DependencyProperty PageChangeOffsetProperty =
            DependencyProperty.Register("PageChangeOffset", typeof(double), typeof(SwipeCanvas), new PropertyMetadata(100.0));

        public Thickness ItemsMargin
        {
            get { return (Thickness)GetValue(ItemsMarginProperty); }
            set { SetValue(ItemsMarginProperty, value); }
        }

        public static readonly DependencyProperty ItemsMarginProperty =
            DependencyProperty.Register("ItemsMargin", typeof(Thickness), typeof(SwipeCanvas), new PropertyMetadata(new Thickness(0)));

        public new ObservableList<FrameworkElement> Children { get; } = new ObservableList<FrameworkElement>();

        public int MaxItemsPerPage
        {
            get
            {
                return ColumnCount * RowCount;
            }
        }

        public int PageCount
        {
            get
            {
                if ((double)Children.Count / MaxItemsPerPage == Children.Count / MaxItemsPerPage)
                    return Children.Count / MaxItemsPerPage;
                return (Children.Count / MaxItemsPerPage) + 1;
            }
        }

        //public int PageCount { get; private set; } = 0;

        Grid[] Containers
        {
            get
            {
                return new[] {
                        LeftContainer,
                        CenterContainer,
                        RightContainer
                    };
            }
        }

        TranslateTransform[] Transforms
        {
            get
            {
                return new[]
                {
                    (TranslateTransform)LeftContainer.RenderTransform,
                    (TranslateTransform)CenterContainer.RenderTransform,
                    (TranslateTransform)RightContainer.RenderTransform
                };
            }
        }

        double[] TransformDefaultX
        {
            get
            {
                return new double[]
                    {
                        -ActualWidth,
                        0.0,
                        ActualWidth
                    };
            }
        }

        List<GridLocation> ItemsGridLocation { get; } = new List<GridLocation>();
        object[] TransformBindings { get; } =
            new object[]
            {
                new Binding("ActualWidth") { Converter = new DoubleNegativeConverter() },
                0.0,
                new Binding("ActualWidth")
            };
        #endregion

        double[] touchDownTransformX;
        double touchDownX;
        double touchDiff;
        bool touchDown = false;
        bool swiping = false;
        int? touchId = null;


        public SwipeCanvas()
        {
            InitializeComponent();

            DataContext = this;

            Unloaded += delegate
            {
                Children.Clear();
            };

            Children.CollectionChanged += Items_CollectionChanged;
            //#if MOUSE
            PageGrid.PreviewMouseLeftButtonDown += SliderCanvas_PreviewMouseLeftButtonDown;
            PageGrid.PreviewMouseMove += SliderCanvas_PreviewMouseMove;
            //#else
            PageGrid.PreviewTouchDown += SliderCanvas_PreviewTouchDown;
            PageGrid.PreviewTouchMove += SliderCanvas_PreviewTouchMove;
            PageGrid.PreviewTouchUp += delegate
            {
                touchId = null;
            };
            //#endif
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ItemsGridLocation.Clear();

            // saving row and column of each item
            for (int i = 0; i < Children.Count;)
            {
                for (int row = 0; row < RowCount; row++)
                {
                    for (int col = 0; col < ColumnCount && i < Children.Count; col++, i++)
                    {
                        ItemsGridLocation.Add(new GridLocation(col, row));
                    }
                }
            }
            // ==================================

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
                CurrentIndex = -1;
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add && (e.OldItems == null || e.OldItems.Count == 0))
                CurrentIndex = 0;

            RenderLayout();
        }

        private void RenderLayout()
        {
            // render page buttons
            PageButtonsContainer.Children.Clear();

            for (int i = 0; i < PageCount; i++)
            {
                var mIndex = i;
                var pageButton = new ImageButton()
                {
                    Margin = (mIndex == CurrentIndex) ? PageButtonSelectedMargin : PageButtonMargin,
                    Background = (mIndex == CurrentIndex) ? PageButtonSelectedBrush : PageButtonIdleBrush,
                    PressedBackground = PageButtonPressedBrush,
                    CornerRadius = PageButtonCornerRadius,
                    BorderBrush = PageButtonBorderBrush,
                    BorderThickness = PageButtonBorderThickness
                };

                pageButton.SetBinding(WidthProperty, new Binding("ActualHeight") { RelativeSource = new RelativeSource(RelativeSourceMode.Self) });
                pageButton.Click += delegate
                {
                    CurrentIndex = mIndex;
                };
                PageButtonsContainer.Children.Add(pageButton);
            }
            // ===============================================

            InitItemsLocation(CurrentIndex);
        }

        public void RenderCurrentIndex()
        {
            InitItemsLocation(CurrentIndex);
        }

        private void InitItemsLocation(int index)
        {
            var startIndex = (MaxItemsPerPage * index) - MaxItemsPerPage;

            var containers = Containers.Concat(new[] { VirtualContainer });

            foreach (var container in containers)
            {
                container.Children.Clear();

                for (int i = startIndex; i < startIndex + MaxItemsPerPage; i++)
                {
                    if (i >= 0 && i < Children.Count)
                    {
                        var child = Children[i];

                        if (child != null)
                        {
                            (child.Parent as Grid)?.Children.Remove(child);

                            GridLocation itemControlGridLocation = ItemsGridLocation[i];

                            Grid.SetColumn(child, itemControlGridLocation.Column);
                            Grid.SetRow(child, itemControlGridLocation.Row);
                            child.Margin = ItemsMargin;

                            child.PreviewMouseLeftButtonUp += delegate
                            {
                                touchDown = false;
                                swiping = false;
                            };

                            container.Children.Add(child);
                        }
                    }
                }

                startIndex += MaxItemsPerPage;
            }

            foreach (ImageButton pageButton in PageButtonsContainer.Children)
            {
                pageButton.Margin = PageButtonMargin;
                pageButton.Background = PageButtonIdleBrush;
            }

            if (index >= 0 && index < PageButtonsContainer.Children.Count)
            {
                var pageButton = PageButtonsContainer.Children[index] as ImageButton;
                pageButton.Margin = PageButtonSelectedMargin;
                pageButton.Background = PageButtonSelectedBrush;
            }
        }

        private void SliderCanvas_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (touchId == null)
            {
                touchId = e.TouchDevice.Id;
                OnTouchDown(e.GetTouchPoint(this).Position);
            }
        }

        private void SliderCanvas_PreviewTouchMove(object sender, TouchEventArgs e)
        {
            if (touchId != null && e.TouchDevice.Id == touchId.Value)
                OnTouchMove(InputSource.Touch, e.GetTouchPoint(this).Position, e.TouchDevice.Capture);
        }

        private void SliderCanvas_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            OnTouchUp(InputSource.Touch, e.TouchDevice.Capture);
        }

        private void SliderCanvas_PreviewMouseLeftButtonDown(object o, MouseButtonEventArgs e)
        {
            OnTouchDown(e.GetPosition(this));
        }

        private void SliderCanvas_PreviewMouseMove(object o, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                OnTouchMove(InputSource.Mouse, e.GetPosition(this), e.MouseDevice.Capture);
        }

        private void SliderCanvas_PreviewMouseLeftButtonUp(object o, MouseButtonEventArgs e)
        {
            OnTouchUp(InputSource.Mouse, e.MouseDevice.Capture);
        }


        private void OnTouchDown(Point touchLocation)
        {
            touchDownX = touchLocation.X;
            touchDownTransformX = Transforms.Select((t) => t.X).ToArray();
            touchDown = true;
        }

        private void OnTouchMove(InputSource source, Point touchLocation, Func<IInputElement, bool> capture)
        {
            if (touchDown)
            {
                touchDiff = touchDownX - touchLocation.X;
                if (swiping)
                {
                    for (int i = 0; i < Transforms.Length; i++)
                    {
                        Transforms[i].X = touchDownTransformX[i] - touchDiff;
                    }
                }
                else if (Math.Abs(touchDiff) > SwipeThreshold)
                {
                    swiping = true;
                    capture(PageGrid);

                    //touchDownX = touchLocation.X;

                    foreach (FrameworkElement item in Containers)
                        item.IsHitTestVisible = false;

                    if (source == InputSource.Mouse)
                        PageGrid.PreviewMouseLeftButtonUp += SliderCanvas_PreviewMouseLeftButtonUp;
                    else
                        PageGrid.PreviewTouchUp += SliderCanvas_PreviewTouchUp;
                }
            }
        }

        private void OnTouchUp(InputSource source, Func<IInputElement, bool> capture)
        {
            if (source == InputSource.Mouse)
                PageGrid.PreviewMouseLeftButtonUp -= SliderCanvas_PreviewMouseLeftButtonUp;
            else
                PageGrid.PreviewTouchUp -= SliderCanvas_PreviewTouchUp;

            foreach (FrameworkElement item in Containers)
                item.IsHitTestVisible = true;

            touchDown = false;
            swiping = false;

            capture(null);

            var oldIndex = CurrentIndex;
            var curIndex = CurrentIndex;


            if (Math.Abs(touchDiff) > PageChangeOffset)
            {
                if (touchDiff > 0)
                {
                    if (curIndex < PageCount - 1)
                    {
                        curIndex++;
                    }
                }
                else
                {
                    if (curIndex > 0)
                    {
                        curIndex--;
                    }
                }
            }

            CurrentIndex = curIndex;

            for (int i = 0; i < Transforms.Count(); i++)
            {
                var transform = Transforms[i];
                var container = Containers[i];
                var toX = TransformDefaultX[i]; // - ((curIndex - oldIndex) * ActualWidth);
                var fromX = transform.X + ((curIndex - oldIndex) * ActualWidth);
                var slideAnimation = new DoubleAnimation(fromX, toX, new Duration(TimeSpan.FromMilliseconds(200)));
                var binding = TransformBindings[i];
                slideAnimation.Completed += delegate
                {
                    if (binding is Binding)
                        BindingOperations.SetBinding(transform, TranslateTransform.XProperty, binding as Binding);
                    else
                        transform.X = (double)binding;
                    transform.BeginAnimation(TranslateTransform.XProperty, null);
                };
                transform.BeginAnimation(TranslateTransform.XProperty, slideAnimation);
            }
        }
    }

    public struct GridLocation
    {
        public int Column { get; set; }
        public int Row { get; set; }

        public GridLocation(int column, int row)
        {
            Column = column;
            Row = row;
        }
    }

    public class IndexChangedEventArgs : EventArgs
    {
        public int OldIndex { get; }
        public int NewIndex { get; }

        public IndexChangedEventArgs(int newIndex, int oldIndex)
        {
            OldIndex = oldIndex;
            NewIndex = newIndex;
        }
    }
}
