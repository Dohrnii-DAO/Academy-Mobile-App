using System.Windows.Input;
using Xamarin.Forms;

namespace DohrniiFoundation.Controls
{
    /// <summary>
    /// Class to inherit the entry properties
    /// </summary>
    public class NoOutlineEntry : Entry
    {
        public NoOutlineEntry()
        {
            this.TextChanged += this.OnTextChanged;
        }

        public static readonly BindableProperty TextChangedCommandProperty =
            BindableProperty.Create(nameof(TextChangedCommand), typeof(ICommand), typeof(NoOutlineEntry));

        public static readonly BindableProperty TextChangedCommandParameterProperty =
            BindableProperty.Create(nameof(TextChangedCommandParameter), typeof(object), typeof(NoOutlineEntry));

        public ICommand TextChangedCommand
        {
            get => (ICommand)this.GetValue(TextChangedCommandProperty);
            set => this.SetValue(TextChangedCommandProperty, (object)value);
        }

        public object TextChangedCommandParameter
        {
            get => this.GetValue(TextChangedCommandParameterProperty);
            set => this.SetValue(TextChangedCommandParameterProperty, value);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.TextChangedCommand == null ||
                 !this.TextChangedCommand.CanExecute(this.TextChangedCommandParameter))
                return;

            this.TextChangedCommand.Execute(this.TextChangedCommandParameter);
        }
    }
}
