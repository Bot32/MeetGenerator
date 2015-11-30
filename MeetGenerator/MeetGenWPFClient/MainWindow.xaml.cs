using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebApiClientLibrary.RequestHadlers;

namespace MeetGenWPFClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region User Actions

        private async void CreateUser_Button_Click(object sender, RoutedEventArgs e)
        {
            UserRequestHandler userHandler = new UserRequestHandler(HostAddress_TextBox.Text);

            User user = new User
            {
                FirstName = UserFirstName_TextBox.Text,
                LastName = UserLastName_TextBox.Text,
                Email = UserEmail_TextBox.Text
            };

            ReadUserHttpResponseMessage(await userHandler.Create(user));
        }
        private void GetUserByID_Button_Click(object sender, RoutedEventArgs e)
        {
            GetUser(UserID_TextBox.Text);
        }
        private void GetUserByEmail_Button_Click(object sender, RoutedEventArgs e)
        {
            GetUser(UserEmail_TextBox.Text);
        }
        async void GetUser(String userIdentificator)
        {
            UserRequestHandler userHandler = new UserRequestHandler(HostAddress_TextBox.Text);
            ReadUserHttpResponseMessage(await userHandler.Get(userIdentificator));
        }
        private async void UpdateUser_Button_Click(object sender, RoutedEventArgs e)
        {
            UserRequestHandler userHandler = new UserRequestHandler(HostAddress_TextBox.Text);

            User user = new User
            {
                Id = Guid.Parse(UserID_TextBox.Text),
                FirstName = UserFirstName_TextBox.Text,
                LastName = UserLastName_TextBox.Text,
                Email = UserEmail_TextBox.Text
            };

            ReadUserHttpResponseMessage(await userHandler.Update(user));
        }
        private async void DeleteUser_Button_Click(object sender, RoutedEventArgs e)
        {
            UserRequestHandler userHandler = new UserRequestHandler(HostAddress_TextBox.Text);
            HttpResponseMessage response = await userHandler.Delete(Guid.Parse(UserID_TextBox.Text));

            Console.Text += response.StatusCode.ToString() + "\n";
            Console.Text += await response.Content.ReadAsStringAsync() + "\n";
        }


        private void ResetUserInfo_Button_Click(object sender, RoutedEventArgs e)
        {
            ResetUserInfo();
        }
        private void SendUserIdToMeetingTab_Button_Click(object sender, RoutedEventArgs e)
        {
            MeetingOwnerID_TextBox.Text = UserID_TextBox.Text;
        }


        void PrintUser(User user)
        {
            Console.Text += "User:" + "\n";
            Console.Text += "ID = " + user.Id.ToString() + "\n";
            Console.Text += "Email = " + user.Email + "\n";
            Console.Text += "First name = " + user.FirstName + "\n";
            Console.Text += "Last name = " + user.LastName + "\n";
        }
        void ResetUserInfo()
        {
            UserID_TextBox.Text = "";
            UserEmail_TextBox.Text = "";
            UserFirstName_TextBox.Text = "";
            UserLastName_TextBox.Text = "";
        }
        async void ReadUserHttpResponseMessage(HttpResponseMessage response)
        {
            User resultUser;

            Console.Text += response.StatusCode.ToString() + "\n";
            if ((response.IsSuccessStatusCode))
            {
                resultUser = await ReadUserFromHttpResponse(response);
                PrintUser(resultUser);
            }
            else Console.Text += await response.Content.ReadAsStringAsync() + "\n";
        }
        async Task<User> ReadUserFromHttpResponse(HttpResponseMessage response)
        {
            User resultUser = await response.Content.ReadAsAsync<User>();

            UserID_TextBox.Text = resultUser.Id.ToString();
            UserEmail_TextBox.Text = resultUser.Email;
            UserFirstName_TextBox.Text = resultUser.FirstName;
            UserLastName_TextBox.Text = resultUser.LastName;

            return resultUser;
        }

        #endregion


        #region Place Actions

        private async void CreatePlace_Button_Click(object sender, RoutedEventArgs e)
        {
            PlaceRequestHandler placeHandler = new PlaceRequestHandler(HostAddress_TextBox.Text);

            Place place = new Place
            {
                Address = PlaceAddress_TextBox.Text,
                Description = PlaceDescription_TextBox.Text
            };

            ReadPlaceHttpResponseMessage(await placeHandler.Create(place));
        }
        private async void GetPlace_Button_Click(object sender, RoutedEventArgs e)
        {
            PlaceRequestHandler placeHandler = new PlaceRequestHandler(HostAddress_TextBox.Text);
            ReadPlaceHttpResponseMessage(await placeHandler.Get(Guid.Parse(PlaceID_TextBox.Text)));
        }
        private async void UpdatePlace_Button_Click(object sender, RoutedEventArgs e)
        {
            PlaceRequestHandler placeHandler = new PlaceRequestHandler(HostAddress_TextBox.Text);

            Place place = new Place
            {
                Id = Guid.Parse(PlaceID_TextBox.Text),
                Address = PlaceAddress_TextBox.Text,
                Description = PlaceDescription_TextBox.Text,
            };

            ReadPlaceHttpResponseMessage(await placeHandler.Update(place));
        }
        private async void DeletePlace_Button_Click(object sender, RoutedEventArgs e)
        {
            PlaceRequestHandler placeHandler = new PlaceRequestHandler(HostAddress_TextBox.Text);
            HttpResponseMessage response = await placeHandler.Delete(Guid.Parse(PlaceID_TextBox.Text));

            Console.Text += response.StatusCode.ToString() + "\n";
            Console.Text += await response.Content.ReadAsStringAsync() + "\n";
        }


        private void ResetPlaceInfo_Button_Click(object sender, RoutedEventArgs e)
        {
            ResetPlaceInfo();
        }
        private void SendPlaceIdToMeetingTab_Button_Click(object sender, RoutedEventArgs e)
        {
            MeetingPlaceID_TextBox.Text = PlaceID_TextBox.Text;
        }


        void PrintPlace(Place place)
        {
            Console.Text += "Place:" + "\n";
            Console.Text += "ID = " + place.Id.ToString() + "\n";
            Console.Text += "Address = " + place.Address + "\n";
            Console.Text += "Desciption = " + place.Description + "\n";
        }
        void ResetPlaceInfo()
        {
            PlaceID_TextBox.Text = "";
            PlaceAddress_TextBox.Text = "";
            PlaceDescription_TextBox.Text = "";
        }
        async void ReadPlaceHttpResponseMessage(HttpResponseMessage response)
        {
            Place resultPlace;

            Console.Text += response.StatusCode.ToString() + "\n";
            if ((response.IsSuccessStatusCode))
            {
                resultPlace = await ReadPlaceFromHttpResponse(response);
                PrintPlace(resultPlace);
            }
            else Console.Text += await response.Content.ReadAsStringAsync() + "\n";
        }
        async Task<Place> ReadPlaceFromHttpResponse(HttpResponseMessage response)
        {
            Place resultPlace = await response.Content.ReadAsAsync<Place>();

            PlaceID_TextBox.Text = resultPlace.Id.ToString();
            PlaceAddress_TextBox.Text = resultPlace.Address;
            PlaceDescription_TextBox.Text = resultPlace.Description;

            return resultPlace;
        }

        #endregion


        #region Meeting Actions

        private async void CreateMeeting_Button_Click(object sender, RoutedEventArgs e)
        {
            MeetingRequestHandler meetingHandler = new MeetingRequestHandler(HostAddress_TextBox.Text);

            Meeting meet = new Meeting
            {
                Title = MeetingTitle_TextBox.Text,
                Description = MeetingDescription_TextBox.Text,
                Date = MeetingDate_DatePicker.DisplayDate,
                InvitedPeople = new Dictionary<Guid, User>(),
                Place = new Place
                {
                    Id = Guid.Parse(MeetingPlaceID_TextBox.Text)
                },
                Owner = new User
                {
                    Id = Guid.Parse(MeetingOwnerID_TextBox.Text),
                    FirstName = "hophop"
                }
            };

            ReadMeetingHttpResponseMessage(await meetingHandler.Create(meet));
        }
        private async void GetMeeting_Button_Click(object sender, RoutedEventArgs e)
        {
            MeetingRequestHandler meetHandler = new MeetingRequestHandler(HostAddress_TextBox.Text);
            ReadMeetingHttpResponseMessage(await meetHandler.Get(Guid.Parse(MeetingID_TextBox.Text)));
        }
        private async void UpdateMeeting_Button_Click(object sender, RoutedEventArgs e)
        {
            MeetingRequestHandler meetingHandler = new MeetingRequestHandler(HostAddress_TextBox.Text);

            Meeting meet = new Meeting
            {
                Id = Guid.Parse(MeetingID_TextBox.Text),
                Title = MeetingTitle_TextBox.Text,
                Description = MeetingDescription_TextBox.Text,
                Date = MeetingDate_DatePicker.DisplayDate,
                InvitedPeople = new Dictionary<Guid, User>(),
                Place = new Place
                {
                    Id = Guid.Parse(MeetingPlaceID_TextBox.Text)
                },
                Owner = new User
                {
                    Id = Guid.Parse(MeetingOwnerID_TextBox.Text),
                }
            };

            ReadMeetingHttpResponseMessage(await meetingHandler.Update(meet));
        }
        private async void DeleteMeeting_Button_Click(object sender, RoutedEventArgs e)
        {
            MeetingRequestHandler meetingHandler = new MeetingRequestHandler(HostAddress_TextBox.Text);
            HttpResponseMessage response = await meetingHandler.Delete(Guid.Parse(MeetingID_TextBox.Text));

            Console.Text += response.StatusCode.ToString() + "\n";
            Console.Text += await response.Content.ReadAsStringAsync() + "\n";
        }


        private void ResetMeetingInfo_Button_Click(object sender, RoutedEventArgs e)
        {
            ResetMeetingnfo();
        }


        void PrintMeeting(Meeting meet)
        {
            Console.Text += "Meeting: \n";
            Console.Text += "ID = " + meet.Id.ToString() + "\n";
            Console.Text += "Date = " + meet.Date + "\n";
            Console.Text += "Title = " + meet.Title + "\n";
            Console.Text += "Desciption = " + meet.Description + "\n";
            Console.Text += "Meeting owner: \n";
            PrintUser(meet.Owner);
            Console.Text += "Meeting place: \n";
            PrintPlace(meet.Place);
        }
        void ResetMeetingnfo()
        {
            MeetingID_TextBox.Text = "";
            MeetingTitle_TextBox.Text = "";
            MeetingDescription_TextBox.Text = "";
            MeetingOwnerID_TextBox.Text = "";
            MeetingPlaceID_TextBox.Text = "";
        }
        async void ReadMeetingHttpResponseMessage(HttpResponseMessage response)
        {
            Meeting resultMeet;

            Console.Text += response.StatusCode.ToString() + "\n";
            if ((response.IsSuccessStatusCode))
            {
                resultMeet = await ReadMeetingFromHttpResponse(response);
                PrintMeeting(resultMeet);
            }
            else Console.Text += await response.Content.ReadAsStringAsync() + "\n";
        }
        async Task<Meeting> ReadMeetingFromHttpResponse(HttpResponseMessage response)
        {
            Meeting resultmeeting = await response.Content.ReadAsAsync<Meeting>();

            MeetingID_TextBox.Text = resultmeeting.Id.ToString();
            MeetingTitle_TextBox.Text = resultmeeting.Title;
            MeetingDescription_TextBox.Text = resultmeeting.Description;
            MeetingDate_DatePicker.DisplayDate = resultmeeting.Date;
            MeetingOwnerID_TextBox.Text = resultmeeting.Owner.Id.ToString();
            MeetingPlaceID_TextBox.Text = resultmeeting.Place.Id.ToString();

            return resultmeeting;
        }

        #endregion




    }
}
