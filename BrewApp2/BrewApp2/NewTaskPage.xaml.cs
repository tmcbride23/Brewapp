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
// Directive for the data model.
using LocalDatabaseSample.Model;
namespace BrewApp2
{
    public partial class Page1 : PhoneApplicationPage
    {
        private void appBarOkButton_Click(object sender, EventArgs e)
        {
            // Confirm there is some text in the text box.
            if (newTaskNameTextBox.Text.Length > 0)
            {
                // Create a new to-do item.
                ToDoItem newToDoItem = new ToDoItem
                {
                    ItemName = newTaskNameTextBox.Text,
                    Category = (ToDoCategory)categoriesListPicker.SelectedItem
                };

                // Add the item to the ViewModel.
                App.ViewModel.AddToDoItem(newToDoItem);

                // Return to the main page.
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
        }

        private void appBarCancelButton_Click(object sender, EventArgs e)
        {
            // Return to the main page.
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        
        
        
        
        public Page1()
        {

            // Set the page DataContext property to the ViewModel.
            this.DataContext = App.ViewModel;  
            
            
            InitializeComponent();
        }
    }
}