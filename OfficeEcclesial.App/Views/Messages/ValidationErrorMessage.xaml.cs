namespace OfficeEcclesial.App.Views.Messages
{
    public partial class ValidationErrorMessage
    {
        public ValidationErrorMessage()
        {
            InitializeComponent();
        }

        public string Message
        {
            set => MessageTextBlock.Text = value;
        }

    }
}
