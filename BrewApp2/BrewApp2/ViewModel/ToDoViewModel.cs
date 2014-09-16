using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

// Directive for the data model.
using LocalDatabaseSample.Model;


namespace LocalDatabaseSample.ViewModel
{
    public class ToDoViewModel : INotifyPropertyChanged
    {
       
        // Remove a to-do task item from the database and collections.
        public void DeleteToDoItem(ToDoItem toDoForDelete)
        {

            // Remove the to-do item from the "all" observable collection.
            AllToDoItems.Remove(toDoForDelete);

           

            // Remove the to-do item from the appropriate category.   
            switch (toDoForDelete.Category.Name)
            {
                case "Pilsner":
                   PilsnerToDoItems.Remove(toDoForDelete);
                    break;
                case "Work":
                    WorkToDoItems.Remove(toDoForDelete);
                    break;
                case "Hobbies":
                    HobbiesToDoItems.Remove(toDoForDelete);
                    break;
                case "Porter":
                    PorterToDoItems.Remove(toDoForDelete);
                    break;
                default:
                    break;
            }

            // Save changes to the database.
            toDoDB.SubmitChanges();
        }

      
        // Add a to-do item to the database and collections.
        public void AddToDoItem(ToDoItem newToDoItem)
        {
           
            // Save changes to the database.
            toDoDB.SubmitChanges();

            // Add a to-do item to the "all" observable collection.
            AllToDoItems.Add(newToDoItem);

            // Add a to-do item to the appropriate filtered collection.
            switch (newToDoItem.Category.Name)
            {
                case "Pilsner":
                    PilsnerToDoItems.Add(newToDoItem);
                    break;
                case "Work":
                    WorkToDoItems.Add(newToDoItem);
                    break;
                case "Hobbies":
                    HobbiesToDoItems.Add(newToDoItem);
                    break;
                case "Porter":
                    PorterToDoItems.Add(newToDoItem);
                    break;
                default:
                    break;
            }
        }
        
        // Query database and load the collections and list used by the pivot pages.
        public void LoadCollectionsFromDatabase()
        {

           
            // Specify the query for all to-do items in the database.
            var toDoItemsInDB = from ToDoItem todo in toDoDB._beerItems
                                select todo;

            // Query the database and load all to-do items.
            AllToDoItems = new ObservableCollection<ToDoItem>(toDoItemsInDB);

            // Specify the query for all categories in the database.
            var toDoCategoriesInDB = from ToDoCategory category in toDoDB.Categories
                                     select category;


            // Query the database and load all associated items to their respective collections.
            foreach (ToDoCategory category in toDoCategoriesInDB)
            {
                switch (category.Name)
                {
                    case "Pilsner":
                        PilsnerToDoItems = new ObservableCollection<ToDoItem>(category.ToDos);
                        break;
                    case "Work":
                        WorkToDoItems = new ObservableCollection<ToDoItem>(category.ToDos);
                        break;
                    case "Hobbies":
                        HobbiesToDoItems = new ObservableCollection<ToDoItem>(category.ToDos);
                        break;
                    case "Porter":
                        PorterToDoItems = new ObservableCollection<ToDoItem>(category.ToDos);
                        break;
                    default:
                        break;
                }
            }

            // Load a list of all categories.
            CategoriesList = toDoDB.Categories.ToList();

        }
        // All to-do items.
        private ObservableCollection<ToDoItem> _allToDoItems;
        public ObservableCollection<ToDoItem> AllToDoItems
        {
            get { return _allToDoItems; }
            set
            {
                _allToDoItems = value;
                NotifyPropertyChanged("AllToDoItems");
            }
        }

        // To-do items associated with the Pilsner category.
        private ObservableCollection<ToDoItem> _pilsnerToDoItems;
        public ObservableCollection<ToDoItem> PilsnerToDoItems
        {
            get { return _pilsnerToDoItems; }
            set
            {
                _pilsnerToDoItems = value;
                NotifyPropertyChanged("PilsnerToDoItems");
            }
        }

        // To-do items associated with the work category.
        private ObservableCollection<ToDoItem> _workToDoItems;
        public ObservableCollection<ToDoItem> WorkToDoItems
        {
            get { return _workToDoItems; }
            set
            {
                _workToDoItems = value;
                NotifyPropertyChanged("WorkToDoItems");
            }
        }

        // To-do items associated with the hobbies category.
        private ObservableCollection<ToDoItem> _hobbiesToDoItems;
        public ObservableCollection<ToDoItem> HobbiesToDoItems
        {
            get { return _hobbiesToDoItems; }
            set
            {
                _hobbiesToDoItems = value;
                NotifyPropertyChanged("HobbiesToDoItems");
            }
        }

        // To-do items associated with the porter category.
        private ObservableCollection<ToDoItem> _porterToDoItems;
        public ObservableCollection<ToDoItem> PorterToDoItems
        {
            get { return _porterToDoItems; }
            set
            {
                _porterToDoItems = value;
                NotifyPropertyChanged("PorterToDoItems");
            }
        }
        // A list of all categories, used by the add task page.
        private List<ToDoCategory> _categoriesList;
        public List<ToDoCategory> CategoriesList
        {
            get { return _categoriesList; }
            set
            {
                _categoriesList = value;
                NotifyPropertyChanged("CategoriesList");
            }
            }

        
        
        // LINQ to SQL data context for the local database.
        private ToDoDataContext toDoDB;

        // Class constructor, create the data context object.
        public ToDoViewModel(string toDoDBConnectionString)
        {
            toDoDB = new ToDoDataContext(toDoDBConnectionString);
        }

        //
        // TODO: Add collections, list, and methods here.
        //

        // Write changes in the data context to the database.
        public void SaveChangesToDB()
        {
            toDoDB.SubmitChanges();
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the app that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
       
        
        
        
        }

        #endregion
    }
}
