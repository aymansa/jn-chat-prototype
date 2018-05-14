using chatproto;
using chatproto.Renderers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace chat_proto
{

	public partial class MainPage : ContentPage
	{
        private double _width { get; set; }
        private double _height { get; set; }
        private int _tempUserResponseCounter = 0;
        public ChatViewModel viewModel;
        private AbsoluteLayout loadingBtn { get; set; }
        public bool IsLoading { get; set; }
        private string EntryText { get; set; }
        public MainPage()
		{
			InitializeComponent();
            this.viewModel = new ChatViewModel();
            TempGetChatHistory();
            loadingBtn = typingButton();
            IsLoading = false;
        }
        #region Fake API Calls
        private void TempGetChatHistory()
        {
            for (int i = 0; i < viewModel.Interactions.Count; i++)
            {
                Interaction _interaction = viewModel.Interactions[i];
                if (!_interaction.IsUser)
                {
                    ChatBox.Children.Add(new ButtonUser()
                    {
                        Text = _interaction.SpokenWord,
                        VerticalOptions = LayoutOptions.End,
                        HorizontalOptions = LayoutOptions.Start,
                        Margin = new Thickness(10, 0, 10, 0)
                    });
                    if (i == viewModel.Interactions.Count - 1)
                    {//Most Recent Pyxus interactoin
                        if (_interaction.showMessage)
                            RequestBox.IsVisible = true;
                    }
                }
                else
                {
                    ChatBox.Children.Add(new ButtonRenderer()
                    {
                        Text = _interaction.SpokenWord,
                        VerticalOptions = LayoutOptions.End,
                        HorizontalOptions = LayoutOptions.End,
                        TextColor = Color.White,
                        Margin = new Thickness(10, 0, 10, 0)

                    });
                }
            }
        }
        #endregion
        #region Post User Response
        private async Task<List<Interaction>> PostUserResponse(string userResponse, int tempInteractionCounter)
        {
            //FAKE SENDING RESPONSE TO API
            ///.....
            ///END

            ///Handle the received interaction
            await ChatWindow.ScrollToAsync(0, ChatBox.Height, true);
            List<Interaction> interactions = new List<Interaction>();
            switch (tempInteractionCounter)
            {
                case 0:
                    interactions.Add(new Interaction() { SpokenWord = "Mine too!" });
                    interactions.Add(new Interaction() { SpokenWord = "Do you like pizza?" });
                    break;
                case 1:
                    await Task.Run(async () =>
                    {
                        System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                        client.MaxResponseContentBufferSize = 256000;
                        Stream stream = await client.GetStreamAsync("https://homepages.cae.wisc.edu/~ece533/images/boat.png");
                        byte[] testImg = GetImageStreamAsBytes(stream);
                        interactions.Add(new Interaction() { image = testImg, showMessage = true });
                    });

                    break;
                default:
                    interactions.Add(new Interaction() { SpokenWord = "Pyxus says something intelligent", showMessage = true });
                    break;
            }
            return interactions;
        }
        #endregion
        #region Send User Input
        private void SendEntryText(object sender, EventArgs e)
        {

            if (RequestBox.Text != null)
            {
                EntryText = RequestBox.Text;
                RequestBox.Text = null;
            }


            Device.BeginInvokeOnMainThread(async () => {
                if (!String.IsNullOrWhiteSpace(EntryText))
                {//Add users text input
                    ChatBox.Children.Add(new ButtonRenderer()
                    {
                        Text = EntryText,
                        TextColor = Color.White,
                        VerticalOptions = LayoutOptions.End,
                        HorizontalOptions = LayoutOptions.End
                    });
                    //Add Dancing dots
                    ChatBox.Children.Add(loadingBtn);
                    //scroll to bottom
                    IsLoading = true;
                    //Send user text & await response
                    List<Interaction> i = await PostUserResponse(EntryText, _tempUserResponseCounter);
                    await Task.Run(() =>
                    {//Fake awaiting API Response
                        Thread.Sleep(1000);
                    });
                    await DisplayPyxusResponse(i);
                    //This is just used as apart of faking API calls
                    _tempUserResponseCounter += 1;
                }
            });
        }
        #endregion
        #region Display response from Pyxus
        private async Task DisplayPyxusResponse(List<Interaction> i)
        {
            await ChatWindow.ScrollToAsync(0, ChatBox.Height, true);
            if (IsLoading)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    ChatBox.Children.Remove(loadingBtn);

                    foreach (Interaction _interaction in i)
                    {
                        if (_interaction.image != null)
                        {//Pyxus responds w/ image
                            Image image = new Image()
                            {
                                Source = ImageSource.FromStream(() => new MemoryStream(_interaction.image)),
                                VerticalOptions = LayoutOptions.End,
                                HorizontalOptions = LayoutOptions.Start,
                                Margin = new Thickness(10, 0, 10, 0)
                            };
                            ChatBox.Children.Add(image);
                        }
                        else
                        {//Pyxus responds w/ text
                            ChatBox.Children.Add(new ButtonUser()
                            {
                                Text = _interaction.SpokenWord,
                                VerticalOptions = LayoutOptions.End,
                                HorizontalOptions = LayoutOptions.Start,
                                Margin = new Thickness(10, 0, 10, 0)
                            });
                        }
                        RequestBox.IsVisible = _interaction.showMessage;
                        CustomBtns.IsVisible = !_interaction.showMessage;
                        sendBtn.IsVisible = _interaction.showMessage;
                    }
                    await ChatWindow.ScrollToAsync(0, ChatBox.Height, true);
                });
                IsLoading = false;
            }
        }
        #endregion
        #region Init Dancing Dots
        private AbsoluteLayout typingButton()
        {
            var absoluteLayout = new AbsoluteLayout();
            var innerBtn = new ButtonUser()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(10, 10, 10, 10)
            };
            var DancingDots = new Lottie.Forms.AnimationView()
            {
                Animation = "typing.json",
                AutoPlay = true,
                Loop = true,
                HeightRequest = 75,
                WidthRequest = 75,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Center
            };

            absoluteLayout.Children.Add(innerBtn, new Rectangle(0, 0, 0.35, 1), AbsoluteLayoutFlags.All);
            absoluteLayout.Children.Add(DancingDots, new Rectangle(0, 0, 0.35, 1), AbsoluteLayoutFlags.All);
            return absoluteLayout;
        }
        #endregion
        #region Overrides
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _width = this.Width;
            _height = this.Height;

        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != _width || height != _height)
            {
                _width = width;
                _height = height;
            }
        }
        #endregion
        #region Read Stream To Byte Array
        public byte[] GetImageStreamAsBytes(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        #endregion
    }
}
