using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordTran.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordTran.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TranslatorChat : ContentPage
    {
        public TranslatorChat()
        {
            InitializeComponent();
            this.BindingContext = new TranslationChatViewModel();
        }

        public void ScrollTap(object sender, System.EventArgs e)
        {
            lock (new object())
            {
                if (BindingContext != null)
                {
                    var vm = BindingContext as TranslationChatViewModel;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        while (vm.DelayedMessages.Count > 0)
                        {
                            vm.Messages.Insert(0, vm.DelayedMessages.Dequeue());
                        }
                        vm.ShowScrollTap = false;
                        vm.LastMessageVisible = true;
                        vm.PendingMessageCount = 0;
                        ChatList?.ScrollToFirst();
                    });


                }

            }
        }

        public void OnListTapped(object sender, ItemTappedEventArgs e)
        {
            chatInput.UnFocusEntry();
        }

    }
}