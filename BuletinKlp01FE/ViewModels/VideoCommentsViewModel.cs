using BuletinKlp01FE.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BuletinKlp01FE.ViewModels
{

    public class VideoCommentsViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Comment> comments;
        public ObservableCollection<Comment> Comments
        {
            get { return comments; }
            set
            {

                comments = value;
            }
        }


        public VideoCommentsViewModel()
        {
            Comments = new ObservableCollection<Comment>();
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                ((INotifyPropertyChanged)comments).PropertyChanged += value;
            }

            remove
            {
                ((INotifyPropertyChanged)comments).PropertyChanged -= value;
            }
        }
    }
}