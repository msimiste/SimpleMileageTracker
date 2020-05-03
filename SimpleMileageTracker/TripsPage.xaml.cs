using SimpleMileageTracker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleMileageTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TripsPage : ContentPage
    {
        public TripsPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            var notes = new List<Trip>();

            var files = Directory.EnumerateFiles(App.FolderPath, "*.notes.txt");
            foreach (var filename in files)
            {
                notes.Add(new Trip
                {
                    Filename = filename,
                    Text = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename)
                });
            }

            listView.ItemsSource = notes
                .OrderBy(d => d.Date)
                .ToList();
        }

        async void OnNoteAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TrackerEntryPage
            {
                BindingContext = new Trip()
            });
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new TrackerEntryPage
                {
                    BindingContext = e.SelectedItem as Trip
                });
            }
        }

    }
}