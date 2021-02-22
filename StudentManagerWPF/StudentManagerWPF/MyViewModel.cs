using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace StudentManagerWPF
{
    class MyViewModel : ViewModelBase
    {
        private ObservableCollection<Filiere> filieres;

        public ObservableCollection<Filiere> Filieres
        {
            get
            {
                if (this.filieres == null)
                {
                    this.filieres = FiliereService.GetEmployees();
                }

                return this.filieres;
            }
        }
    }
}
