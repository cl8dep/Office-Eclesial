using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using OfficeEcclesial.App.Annotations;
using OfficeEcclesial.App.Database.Entities;

namespace OfficeEcclesial.Database.Entities
{
    public class CaritasProject : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Person Representant { get; set; }
        public IEnumerable<Person> Beneficiaries { get; set; }

        private bool _isSelected;
        [NotMapped]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value; 
                OnPropertyChanged(nameof(IsSelected));
            }
        }



        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void Assign(CaritasProject project)
        {
            Name = project.Name;
            Representant.Assign(project.Representant);
            Beneficiaries = project.Beneficiaries;
        }
    }
}
