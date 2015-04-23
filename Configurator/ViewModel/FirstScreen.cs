// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Configurator.Waut.Model.PlantConfiguration.model;
using Microsoft.Practices.Prism.Commands;

namespace Configurator.Waut.Model.PlantConfiguration.ViewModels
{ 
    public class FirstScreen
    {
        public FirstScreen()
        {
            this.SubmitCommand = new DelegateCommand<object>(this.OnSubmit);

        }

        public ICommand SubmitCommand { get; private set; }

        private void OnSubmit(object obj)
        {
            Debug.WriteLine(this.BuildResultString());
        }

        private string BuildResultString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Name: ");
            builder.Append(this.QuestionnaireViewModel.Questionnaire.Name);
            return builder.ToString();
        }
    }
}
