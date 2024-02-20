using Newtonsoft.Json;
using PreparationMobile.Models;

namespace PreparationMobile;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
        UserLog.UserLogSistem.Clear();
        /*TestListApi();*/
        _login = "";
        Title = "Login:";
        BindingContext = this;
    }

    /*public List<TestApi> TestList = new();*/
    /*private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }*/

    
    

    /*public async Task TestListApi()
    {
        using (var client = new HttpClient())
        {
                var responseMessage = await client.GetAsync("http://10.0.2.2:5263/api/Diagnosis/GetDiagnosis");
                string content = await responseMessage.Content.ReadAsStringAsync();
                TestList = JsonConvert.DeserializeObject<List<TestApi>>(content)!.ToList();
                ListView.ItemsSource = TestList.Select(x => new
                {
                    asd = x.Diagnosisname
                }).ToList();

                
                PickerTest.ItemsSource = TestList.Select(x => new
                {
                    asdx = x.Diagnosisname
                }).ToList();
                

                /*Picker picker = new Picker {Title = "Select"};
                picker.SetBinding(Picker.ItemsSourceProperty, "TestList");
                picker.ItemDisplayBinding = new Binding("Diagnosisname");#1#
        }
    }*/

    /*private string _text;
    private void CompletedTest(object? sender, EventArgs e)
    {
        TextCell.Text = _text;
    }

    private void TestText(object? sender, TextChangedEventArgs e)
    {
        _text = e.NewTextValue;
    }

    private void PickerTest_OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        TextPicker.Text = TestList[PickerTest.SelectedIndex].Diagnosisid.ToString();
    }*/

    private string _login;
    private async void LoginButton(object? sender, EventArgs e)
    {
        using (var client = new HttpClient())
        {
            HttpResponseMessage responseMessage = await client.GetAsync($"http://10.0.2.2:5263/api/Login/GetLogin?loginUser={_login}");
            string content = await responseMessage.Content.ReadAsStringAsync();
            List<LoginModel> userInfo = JsonConvert.DeserializeObject<List<LoginModel>>(content)!.ToList();
            if (userInfo.Count == 1)
            {
                UserLog.UserLogSistem.Add(userInfo[0].Userid);
                MenuDoc menuDoc = new MenuDoc();
                await Navigation.PushAsync(menuDoc);
            }
        }
    }

    private async void CompletedText(object? sender, EventArgs e)
    {
        _login = LoginTextName.Text;
        using (var client = new HttpClient())
        {
            HttpResponseMessage responseMessage = await client.GetAsync($"http://10.0.2.2:5263/api/Login/GetLogin?loginUser={_login}");
            string content = await responseMessage.Content.ReadAsStringAsync();
            List<LoginModel> userInfo = JsonConvert.DeserializeObject<List<LoginModel>>(content)!.ToList();
            if (userInfo.Count == 1)
            {
                UserLog.UserLogSistem.Add(userInfo[0].Userid);
                MenuDoc menuDoc = new MenuDoc();
                await Navigation.PushAsync(menuDoc);
            }
        }
    }

    private void LoginText(object? sender, TextChangedEventArgs e)
    {
        _login = LoginTextName.Text;
    }
}

/*public class TestApi
{
    public int Diagnosisid { get; set; }

    public string Diagnosisname { get; set; }
}*/