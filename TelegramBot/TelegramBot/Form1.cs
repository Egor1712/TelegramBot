using System;
using System.Drawing;
using System.Net.Mime;
using System.Windows.Forms;
using Services;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramBot
{
    public partial class Form1 : Form
    {
        private User user;
        private TelegramBotClient botClient;
        private readonly TextBox login = new TextBox();
        private readonly TextBox password = new TextBox();
        private readonly TextBox token = new TextBox();

        public Form1()
        {
            InitializeComponent();
            var button = new Button();
            login.Location = new Point(100, 100);
            login.Height = 50;
            login.PlaceholderText = "login";
            password.Location = new Point(100, 150);
            password.Height = 50;
            password.PlaceholderText = "password";
            token.Location = new Point(100, 200);
            token.Height = 50;
            token.PlaceholderText = "token";
            button.Location = new Point(100, 250);
            button.Height = 50;
            button.Text = "Click me!";
            button.Click += ButtonOnClick;
            Controls.Add(button);
            Controls.Add(login);
            Controls.Add(password);
            Controls.Add(token);
        }

        private async void ButtonOnClick(object? sender, EventArgs e)
        {
            user = new User(login.Text, password.Text);
            botClient = new TelegramBotClient(token.Text);
            botClient.OnMessage += HandleMessage;
            await user.Initialize();
            botClient.StartReceiving();
        }

        private async void HandleMessage(object? sender, MessageEventArgs eventArgs)
        {
            if (eventArgs.Message.Text == "/start")
            {
                foreach (var subject in user.Subjects)
                {
                    await botClient.SendTextMessageAsync(eventArgs.Message.Chat.Id,
                                                         subject.ToString());
                }
            }
        }
    }
}