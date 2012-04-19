using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using ICOMI_CONTENT;

namespace ICOMI_SPClient.ViewModels.DesignTime
{
    public class BHPViewModelMock
    {
        public ObservableCollection<Rubrique> LesRubriques { get; private set; }

        public BHPViewModelMock()
        {
            LesRubriques = new ObservableCollection<Rubrique>(RubriqueFactory.GetListRubriques());
        }
    }
}
