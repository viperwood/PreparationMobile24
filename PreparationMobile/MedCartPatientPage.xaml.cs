using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PreparationMobile.Models;

namespace PreparationMobile;

public partial class MedCartPatientPage : ContentPage
{
    public MedCartPatientPage()
    {
        InitializeComponent();
        DiagnosisBox();
    }

    private List<Patients> patients;
    private List<string> _preparatList = new();
    private List<DiagnosisList> diagnosList;
    private List<ReseptModel> _resept = new();
    private List<TipeEventList> tipeEventLists;

    private async Task DiagnosisBox()
    {
        HttpResponseMessage httpResponseMessage = await HttpClientHelper.Client.GetAsync("http://10.0.2.2:5263/api/Patient/GetPatient");
        string cont = await httpResponseMessage.Content.ReadAsStringAsync();
        patients = JsonConvert.DeserializeObject<List<Patients>>(cont)!.ToList();
        PatientPicker.ItemsSource = patients.Select(x => new
        {
            FioPatient = x.Fio
        }).ToList();
        
        HttpResponseMessage responseMessage = await HttpClientHelper.Client.GetAsync("http://10.0.2.2:5263/api/Diagnosis/GetDiagnosis");
        string content = await responseMessage.Content.ReadAsStringAsync();
        diagnosList = JsonConvert.DeserializeObject<List<DiagnosisList>>(content)!.ToList();
        DiagnosPicker.ItemsSource = diagnosList.Select(x => new
        {
            DiagnosName = x.Diagnosisname
        }).ToList();
        
        HttpResponseMessage responseMes = await HttpClientHelper.Client.GetAsync("http://10.0.2.2:5263/api/Priparation/GetPriparation");
        string contents = await responseMes.Content.ReadAsStringAsync();
        _preparatList = JsonConvert.DeserializeObject<List<string>>(contents)!.ToList();
        PreparationPicker.ItemsSource = _preparatList.Select(x => new
        {
            PreparationName = x
        }).ToList();

        HttpResponseMessage message = await HttpClientHelper.Client.GetAsync("http://10.0.2.2:5263/api/TipeEvent/GetTipeEvent");
        string contantTipe = await message.Content.ReadAsStringAsync();
        tipeEventLists = JsonConvert.DeserializeObject<List<TipeEventList>>(contantTipe)!.ToList();
        TipeDirectionPicker.ItemsSource = tipeEventLists.Select(x => new
        {
            NameTipe = x.Eventname
        }).ToList();
    }

    private void AddListPreparat(object? sender, EventArgs eventArgs)
    {
        if (_resept.Count < 10)
        {
            ReseptModel reseptModel = new ReseptModel();
            reseptModel.Dosa = DosaEntry.Text;
            reseptModel.Format = FormEntry.Text;
            reseptModel.Recomendation = RecomindationEntry.Text;
            reseptModel.NamePreparation = _preparatList[PreparationPicker.SelectedIndex];
            _resept.Add(reseptModel);
            ReseptList.ItemsSource = _resept.Select(x => new
            {
                Preparation = x.NamePreparation,
                DosaPrep = x.Dosa,
                FormatPriema = x.Format
            }).ToList();   
        }
    }

    private async void Next(object? sender, EventArgs eventArgs)
    {
        AddPostDirection addPostDirection = new AddPostDirection();
        addPostDirection.Fio = patients[PatientPicker.SelectedIndex].Userid;
        addPostDirection.Diagnos = diagnosList[DiagnosPicker.SelectedIndex].Diagnosisid;
        addPostDirection.Anamnis = AnamnisEntry.Text;
        addPostDirection.Symptomatic = SimpomaticEntry.Text;
        addPostDirection.NameDiagnostic = DiagnosticNameEntry.Text;
        addPostDirection.DoctorId = UserLog.UserLogSistem[0];
        addPostDirection.Description = DescriptioneEntry.Text;
        addPostDirection.TipeEvent = tipeEventLists[TipeDirectionPicker.SelectedIndex].Eventid;
        ReseptModel reseptModel = new ReseptModel();
        reseptModel.Recomendation = RecomindationEntry.Text;
        HttpResponseMessage message = await HttpClientHelper.Client.PostAsJsonAsync("http://10.0.2.2:5263/api/AddDirection/PostDirection",addPostDirection);
        HttpResponseMessage resp = await HttpClientHelper.Client.PostAsJsonAsync("http://10.0.2.2:5263/api/AddListPreparation/PostListPriporation",_resept);
    }

    private async void BackButton(object? sender, EventArgs e)
    {
        MenuDoc menuDoc = new MenuDoc();
        await Navigation.PushAsync(menuDoc);
    }
}

class Patients
{
    public string? Fio { get; set; }
    public int Userid { get; set; }
}

class DiagnosisList
{
    public string? Diagnosisname { get; set; }
    public int Diagnosisid { get; set; }
}

class TipeEventList
{
    public string? Eventname { get; set; }
    public int Eventid { get; set; }
}

class AddPostDirection
{
    public int? Fio { get; set; }
    public int? Diagnos { get; set; }
    public string? Anamnis { get; set; }
    public string? Symptomatic { get; set; }
    public string? NameDiagnostic { get; set; }
    public string? Description { get; set; }
    public int TipeEvent { get; set; } 
    public int DoctorId { get; set; } 
}