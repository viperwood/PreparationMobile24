using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreparationMobile;

public partial class MenuDoc : ContentPage
{
    public MenuDoc()
    {
        InitializeComponent();
        Title = "MenuDocPage";
    }

    private async void BackButton(object? sender, EventArgs e)
    {
        MainPage mainPage = new MainPage();
        await Navigation.PushAsync(mainPage);
    }

    private async void PatientCartbutton(object? sender, EventArgs e)
    {
        MedCartPatientPage medCartPatientPage = new MedCartPatientPage();
        await Navigation.PushAsync(medCartPatientPage);
    }

    private async void FindPAtient(object? sender, EventArgs e)
    {
        ViewPatient viewPatient = new ViewPatient();
        await Navigation.PushAsync(viewPatient);
    }
}