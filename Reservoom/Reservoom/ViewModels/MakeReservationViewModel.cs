using System.Collections;
using System.ComponentModel;
using System.Windows.Input;
using Reservoom.Commamds;
using Reservoom.Services;
using Reservoom.Stores;

namespace Reservoom.ViewModels;

public class MakeReservationViewModel : ViewModelBase, INotifyDataErrorInfo
{
    public MakeReservationViewModel(HotelStore hotelStore, NavigationService reservationViewNavigationService)
    {
        SubmitCommand = new MakeReservationCommand(this, hotelStore, reservationViewNavigationService);
        CancelCommand = new NavigateCommand(reservationViewNavigationService);

        _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
    }

    private string? _username;
    public string Username
    {
        get { return _username; }
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    private int _floorNumber;
    public int FloorNumber
    {
        get { return _floorNumber; }
        set
        {
            _floorNumber = value;
            OnPropertyChanged(nameof(FloorNumber));
        }
    }

    private int _roomNumber;
    public int RoomNumber
    {
        get { return _roomNumber; }
        set
        {
            _roomNumber = value;
            OnPropertyChanged(nameof(RoomNumber));
        }
    }

    private DateTime _startDate = new DateTime(2021, 1, 1);
    public DateTime StartDate
    {
        get { return _startDate; }
        set
        {
            _startDate = value;
            OnPropertyChanged(nameof(StartDate));

            ClearErrors(nameof(StartDate));
            ClearErrors(nameof(EndDate));

            if (EndDate < StartDate)
            {
                AddError("The start date cannot be after the end date.", nameof(StartDate));
            }
        }
    }

    private DateTime _endDate = new DateTime(2021, 1, 8);

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public DateTime EndDate
    {
        get { return _endDate; }
        set
        {
            _endDate = value;
            OnPropertyChanged(nameof(EndDate));

            ClearErrors(nameof(StartDate));
            ClearErrors(nameof(EndDate));

            if (EndDate <= StartDate)
            {
                AddError("The end date cannot be before the start date.", nameof(EndDate));
            }
        }
    }

    private void AddError(string errorMessage, string propertyName)
    {
        if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
        {
            _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
        }

        _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);
        OnErrorsChanged(propertyName);
    }

    private void OnErrorsChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    private void ClearErrors(string propertyName)
    {
        _propertyNameToErrorsDictionary.Remove(propertyName);
        OnErrorsChanged(propertyName);
    }

    public ICommand? SubmitCommand { get; }
    public ICommand? CancelCommand { get; }

    private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
    public bool HasErrors => _propertyNameToErrorsDictionary.Any();

    public IEnumerable GetErrors(string? propertyName)
    {
        return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
    }
}
