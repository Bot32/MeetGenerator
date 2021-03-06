﻿using MeetGenerator.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WebApiClientLibrary.Interfaces;
using WebApiClientLibrary.RequestHadlers;

namespace MeetGenWPFClient.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        string _hostAddress;
        TextBox box = new TextBox();

        IUserRequestHandler _userRequestHandler;
        IPlaceRequestHandler _placeRequestHandler;
        IMeetingRequestHandler _meetingRequestHandler;

        User _user;
        Place _place;
        Meeting _meeting;

        public MainViewModel()
        {
            _hostAddress = "http://meetgen.azurewebsites.net/";

            InitialaizeHandlers();
            SetStartData();
        }

        public string HostAddress
        {
            get
            {
                return _hostAddress;
            }

            set
            {
                _hostAddress = value;
                InitialaizeHandlers();
                OnPropertyChanged(new PropertyChangedEventArgs("HostAddress"));
            }
        }

        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged(new PropertyChangedEventArgs("User"));
            }
        }
        public Place Place
        {
            get
            {
                return _place;
            }
            set
            {
                _place = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Place"));
            }
        }
        public Meeting Meeting
        {
            get
            {
                return _meeting;
            }
            set
            {
                _meeting = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Meeting"));
            }
        }

        public ICommand CreateUser
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    HttpResponseMessage response = await _userRequestHandler.Create(User);
                    if (response.IsSuccessStatusCode)
                    User = await response.Content.ReadAsAsync<User>();
                    WriteToConsole(response);
                });
            }
        }
        public ICommand GetUserByID
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    HttpResponseMessage response = await _userRequestHandler.Get(User.Id.ToString());
                    if (response.IsSuccessStatusCode) User = await response.Content.ReadAsAsync<User>();
                    WriteToConsole(response);
                });
            }
        }
        public ICommand GetUserByEmail
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    HttpResponseMessage response = await _userRequestHandler.Get(User.Email);
                    if (response.IsSuccessStatusCode) User = await response.Content.ReadAsAsync<User>();
                    WriteToConsole(response);
                });
            }
        }
        public ICommand UpdateUser
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    HttpResponseMessage response = await _userRequestHandler.Update(User);
                    if (response.IsSuccessStatusCode) User = await response.Content.ReadAsAsync<User>();
                    WriteToConsole(response);
                });
            }
        }
        public ICommand DeleteUser
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    HttpResponseMessage response = await _userRequestHandler.Delete(User.Id);
                    if (response.IsSuccessStatusCode) User = new User();
                    WriteToConsole(response);
                });
            }
        }

        public ICommand CreatePlace
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    HttpResponseMessage response = await _placeRequestHandler.Create(Place);
                    if (response.IsSuccessStatusCode) Place = await response.Content.ReadAsAsync<Place>();
                    WriteToConsole(response);
                });
            }
        }
        public ICommand GetPlace
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    HttpResponseMessage response = await _placeRequestHandler.Get(Place.Id);
                    if (response.IsSuccessStatusCode) Place = await response.Content.ReadAsAsync<Place>();
                    WriteToConsole(response);
                });
            }
        }
        public ICommand UpdatePlace
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    HttpResponseMessage response = await _placeRequestHandler.Update(Place);
                    if (response.IsSuccessStatusCode) Place = await response.Content.ReadAsAsync<Place>();
                    WriteToConsole(response);
                });
            }
        }
        public ICommand DeletePlace
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    HttpResponseMessage response = await _placeRequestHandler.Delete(Place.Id);
                    if (response.IsSuccessStatusCode) Place = new Place();
                    WriteToConsole(response);
                });
            }
        }

        public ICommand CreateMeeting
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    HttpResponseMessage response = await _meetingRequestHandler.Create(Meeting);
                    if (response.IsSuccessStatusCode) Meeting = await response.Content.ReadAsAsync<Meeting>();
                    WriteToConsole(response);
                });
            }
        }
        public ICommand GetMeeting
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    HttpResponseMessage response = await _meetingRequestHandler.Get(Meeting.Id);
                    if (response.IsSuccessStatusCode) Meeting = await response.Content.ReadAsAsync<Meeting>();
                    WriteToConsole(response);
                });
            }
        }
        public ICommand UpdateMeeting
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    HttpResponseMessage response = await _meetingRequestHandler.Update(Meeting);
                    if (response.IsSuccessStatusCode) Meeting = await response.Content.ReadAsAsync<Meeting>();
                    WriteToConsole(response);
                });
            }
        }
        public ICommand DeleteMeeting
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    HttpResponseMessage response = await _meetingRequestHandler.Delete(Meeting.Id);
                    if (response.IsSuccessStatusCode) Meeting = Meeting = new Meeting
                    {
                        Owner = new User(),
                        Place = new Place()
                    };
                    WriteToConsole(response);
                });
            }
        }

        public ICommand ResetUserData
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    User = new User();
                });
            }
        }
        public ICommand ResetPlaceData
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Place = new Place();
                });
            }
        }
        public ICommand ResetMeetingData
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Meeting = new Meeting
                    {
                        Owner = new User(),
                        Place = new Place()
                    };
                });
            }
        }

        public ICommand SendUserToMeetingTab
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Meeting.Owner = (User)User.Clone();
                    OnPropertyChanged(new PropertyChangedEventArgs("Meeting"));
                });
            }
        }
        public ICommand SendPlaceToMeetingTab
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Meeting.Place = (Place)Place.Clone();
                    OnPropertyChanged(new PropertyChangedEventArgs("Meeting"));
                });
            }
        }

        public TextBox Box
        {
            get
            {
                return box;
            }

            set
            {
                box = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        void InitialaizeHandlers()
        {
            _userRequestHandler = new UserRequestHandler(_hostAddress);
            _placeRequestHandler = new PlaceRequestHandler(_hostAddress);
            _meetingRequestHandler = new MeetingRequestHandler(_hostAddress);
        }
        void SetStartData()
        {
            _user = new User
            {
                FirstName = "Vasilyi",
                LastName = "Pupkin",
                Email = "vasya@pupa.com"
            };
            _place = new Place
            {
                Address = "Puschkeen st., Kolotuschin h. #1",
                Description = "It is kolutushkin house."
            };
            _meeting = new Meeting
            {
                Title = "Party",
                Description = "party description",
                Owner = new User(),
                Date = new DateTime(2016, 1, 1),
                Place = new Place(),
                InvitedPeople = new Dictionary<Guid, User>()
            };
        }

        async void WriteToConsole(HttpResponseMessage response)
        {
            Box.Text += response.StatusCode + "\n";
            Box.Text += await response.Content.ReadAsStringAsync() + "\n";
        }
    }
}
