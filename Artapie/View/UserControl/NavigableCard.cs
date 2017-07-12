namespace View.UserControl
{
    using System.Windows;

    using ViewModel.Shared;

    public class NavigableCard : MaterialDesignThemes.Wpf.Card
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(NavigableCard),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.None));
        
        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }

            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public static readonly DependencyProperty CloseCommandProperty = DependencyProperty.Register(
            nameof(CloseCommand),
            typeof(IDelegateCommand),
            typeof(NavigableCard),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));

        public IDelegateCommand CloseCommand
        {
            get
            {
                return (IDelegateCommand)GetValue(CloseCommandProperty);
            }

            set
            {
                SetValue(TitleProperty, value);
            }
        }
    }
}
