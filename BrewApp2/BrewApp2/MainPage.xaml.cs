using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
// Directive for the ViewModel.
using LocalDatabaseSample.Model;

namespace BrewApp2
{
    public partial class MainPage : PhoneApplicationPage
    {
        private void newTaskAppBarButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewTaskPage.xaml", UriKind.Relative));
        }


        private void openTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Cast the parameter as a button.
            var Obutton = sender as Button;

            if (Obutton != null)
            {// Get a handle for the to-do item bound to the button.

                ToDoItem BeerRefrence = Obutton.DataContext as ToDoItem;
                
                NavigationService.Navigate(new Uri("/BeerTable.xaml", UriKind.Relative));
            }

        }  
        private void deleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Cast the parameter as a button.
            var button = sender as Button;

            if (button != null)
            {
                // Get a handle for the to-do item bound to the button.
                ToDoItem toDoForDelete = button.DataContext as ToDoItem;

                App.ViewModel.DeleteToDoItem(toDoForDelete);
            }

            // Put the focus back to the main page.
            this.Focus();
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Save changes to the database.
            App.ViewModel.SaveChangesToDB();
        }

        // Constructor
        public MainPage()
        {
            InitializeComponent();
      // Set the page DataContext property to the ViewModel.
            this.DataContext = App.ViewModel;
        
        }

    }
}