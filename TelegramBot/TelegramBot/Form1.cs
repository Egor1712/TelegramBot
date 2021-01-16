
using System;
using System.Windows.Forms;
using Services;

namespace TelegramBot
{
    public partial class Form1 : Form
    {
        private User user;
        public Form1()
        {
            InitializeComponent();
            var button = new Button();
            button.Text = "Click me!";
            button.Click += ButtonOnClick;
            Controls.Add(button);
            user =  new User("chusoveg17@gmail.com", "6E2tjFtp");
        }

        private async void ButtonOnClick(object? sender, EventArgs e)
        {
            await user.Initialize();
            foreach (var subject in user.Subjects)
            {
                var label = new Label();
                label.Text = subject.ToString();
                label.AutoSize = true;
                Controls.Add(label);
            }
        }
    }
}