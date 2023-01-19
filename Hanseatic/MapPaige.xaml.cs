﻿using Hanseatic.ViewModel;

namespace Hanseatic;

public partial class MapPage : ContentPage
{
    public MapPage(MapPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void RadioButton_EnterCity(object sender, CheckedChangedEventArgs e)
    {
        if (sender is RadioButton radioButton)
        {
            if (e.Value)
            {
                // Go To buypage
                await Shell.Current.GoToAsync($"{nameof(BuyPage)}?city_name={radioButton.Content}");
            }
        }
    }
}

