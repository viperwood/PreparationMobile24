using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PreparationMobile;

public partial class ViewPatient : ContentPage
{
    public ViewPatient()
    {
        InitializeComponent();
    }

    private async void SerchButton(object? sender, EventArgs eventArgs)
    {
        if (string.IsNullOrEmpty(IdPatientEntry.Text) != true)
        {
            HttpResponseMessage responseMessage = await HttpClientHelper.Client.GetAsync($"http://10.0.2.2:5263/api/FullInformationMobile/GetPatientMobile?idPatienta={IdPatientEntry.Text}");
            string content = await responseMessage.Content.ReadAsStringAsync();
            List<PatientFullInfoModels> fullInfo =
                JsonConvert.DeserializeObject<List<PatientFullInfoModels>>(content)!.ToList();
            if (fullInfo[0].Email != null)
            {
                EmailBlock.Text = fullInfo[0].Email;
                PhoneBlock.Text = fullInfo[0].Phone;
                AddressBlock.Text = fullInfo[0].Address;
                GendernameBlock.Text = fullInfo[0].Gendername;
                BirthdayBlock.Text = fullInfo[0].Birthday.ToString();
                SeriespBlock.Text = fullInfo[0].Seriesp.ToString();
                NumberpBlock.Text = fullInfo[0].Numberp.ToString();
                FioBlock.Text = fullInfo[0].Fio;
                InsurancecompanynameBlock.Text = fullInfo[0].Insurancecompanyname;
                PlaceofworknameBlock.Text = fullInfo[0].Placeofworkname;
                PolicyvalidityBlock.Text = fullInfo[0].Policyvalidity.ToString();
                NumberpolisBlock.Text = fullInfo[0].Numberpolis;
                DataexpirationinsurancepolisyBlock.Text = fullInfo[0].Dataexpirationinsurancepolisy.ToString();
                DataissuemcBlock.Text = fullInfo[0].Dataissuemc.ToString();
            }
        }
        
    }
}