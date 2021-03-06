﻿using ListViewSample.Model;
using LLM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Demo.Pages
{
    public sealed partial class NormalFixPage : Page
    {
        ObservableCollection<Contact> _contacts = new ObservableCollection<Contact>();

        public ObservableCollection<Contact> Contacts
        {
            get { return _contacts; }
            set { _contacts = value; }
        }

        Dictionary<Contact, bool> ContackStarDict = new Dictionary<Contact, bool>();

        public NormalFixPage()
        {
            this.InitializeComponent();
            DataContext = this;
            Contacts = Contact.GetContacts(140);
        }

        private void SwipeLeftFirstItem(object sender, RoutedEventArgs e)
        {
            if (Contacts.Count == 0)
                return;

            Contacts[0].IsSwipedLeft = !Contacts[0].IsSwipedLeft;
        }

        private void SwipeRightFirstItem(object sender, RoutedEventArgs e)
        {
            if (Contacts.Count == 0)
                return;

            Contacts[0].IsSwipedRight = !Contacts[0].IsSwipedRight;
        }
        
        private void EnableOrDisableSwipeLeftFirstItem(object sender, RoutedEventArgs e)
        {
            if (Contacts.Count == 0)
                return;

            Contacts[0].EnableSwipeLeft = !Contacts[0].EnableSwipeLeft;
        }

        private void EnableOrDisableSwipeRightFirstItem(object sender, RoutedEventArgs e)
        {
            if (Contacts.Count == 0)
                return;

            Contacts[0].EnableSwipeRight = !Contacts[0].EnableSwipeRight;
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            var item = Utils.FindVisualParent<LLMListViewItem>(sender as AppBarButton);
            var itemData = (Contact) item.Content;
            var dlg = new MessageDialog("Edit " + itemData.Name);
            dlg.Commands.Add(new UICommand("OK", command => { itemData.IsSwipedRight = false; }));
            dlg.Commands.Add(new UICommand("Cancel"));
            await dlg.ShowAsync();
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            var item = Utils.FindVisualParent<LLMListViewItem>(sender as AppBarButton);
            var itemData = item.Content as Contact;
            var dlg = new MessageDialog("Comfire Delete " + itemData.Name + "?");
            dlg.Commands.Add(new UICommand("OK", new UICommandInvokedHandler(param =>
            {
                _contacts.Remove(itemData);
            })));
            dlg.Commands.Add(new UICommand("Cancel"));
            await dlg.ShowAsync();
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            var item = Utils.FindVisualParent<LLMListViewItem>(sender as AppBarButton);
            var itemData = (Contact)item.Content;
            var dlg = new MessageDialog("you click Add button");
            dlg.Commands.Add(new UICommand("OK", command => { itemData.IsSwipedRight = false; }));
            dlg.Commands.Add(new UICommand("Cancel"));
            await dlg.ShowAsync();
        }
    }
}
