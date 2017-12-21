using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SQLiteDemo.Data;
using SQLiteDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SQLiteDemo.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        INavigationService _navigationService;
        //
        public ICommand OnSaveClicked { get; set; }
        public ICommand OnDeleteClicked { get; set; }
       
        public DelegateCommand<Note> ItemTappedCommand { get; set; }


        //----------------------
        private string _enotes;
        public string ENotes
        {
            get { return _enotes; }
            set { SetProperty(ref _enotes, value); }
        }

        private string _ename;
        public string EName
        {
            get { return _ename; }
            set { SetProperty(ref _ename, value); }
        }

        private List<Note> _lvSource;
        public List<Note> LvSource
        {
            get { return _lvSource; }
            set { SetProperty(ref _lvSource, value); }
        }



        //------------------
        static DB database;
        public static DB Database
        {
            get
            {
                if (database == null)
                {
                    database = new DB(DependencyService.Get<IFileHelper>().GetLocalFilePath("NoteSQLite.db3"));
                }
                return database;
            }
        }

        public int ResumeAtTodoId { get; set; }

        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            _navigationService = navigationService;
            //
            Title = "Main Page";
            OnSaveClicked = new DelegateCommand(onSaveClicked);
            OnDeleteClicked = new DelegateCommand(onDeleteClicked);

            //
          
            ItemTappedCommand = new DelegateCommand<Note>(ItemTapped);

            //load DB in to list view
            LoadData();
        }

        public void ItemTapped(Note note)
        {
            
            EName = note.Name;
            ENotes = note.Notes;
            //
            noteDelete = new Note { ID = note.ID, Notes = note.Notes, Name = note.Name };

        }

        private Note noteDelete = null;
       
        async void onDeleteClicked()
        {
            //Note note = new Note {ID = id, Notes = ENotes, Name = EName };
            if (noteDelete != null)
            {
                await Database.DeleteItemAsync(noteDelete);
                noteDelete = null;
                LoadData();
            }
            
        }

        async void onSaveClicked()
        {
            Note note = new Note { Notes = ENotes, Name = EName};
            await Database.SaveItemAsync(note);


            //load listview
            LoadData();
        }

        async void LoadData()
        {
           
            //load listview
            LvSource = await Database.GetItemsAsync();
        }



    }
}
