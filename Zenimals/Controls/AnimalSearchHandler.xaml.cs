﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Zenimals.Models;
using ZenMvvm;

namespace Zenimals.Controls
{
    public partial class AnimalSearchHandler : SearchHandler
    {
        public static readonly BindableProperty DataProperty = BindableProperty.Create("Data", typeof(IEnumerable<Animal>), typeof(AnimalSearchHandler));
        public IEnumerable<Animal> Data
        {
            get => GetValue(DataProperty) as IEnumerable<Animal>;
            set => SetValue(DataProperty, value);
        }

        public AnimalSearchHandler()
        {
            InitializeComponent();
            Placeholder = "Enter search term";
            ShowsResults = true;
        }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = Data
                    .Where(animal => animal.Name.ToLower().Contains(newValue.ToLower()))
                    .ToList();
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
            await Task.Delay(500);

            await DiContainer
                .Resolve<INavigationService>()
                .GoToAsync($"details", (Animal)item);
        }
    }
}
