using System;
using Xamarin.Forms;
using CameraXamarin.service;
using System.Threading.Tasks;

namespace CameraXamarin.ViewModel
{
  public class VMMainPage : BaseViewModel
  {

    CameraService cameraService;

    public Command TakePhotoCommand { get; }
    public Command ChoosePhotoCommand { get; }

    private ImageSource _photo;
    public ImageSource Photo
    {
      get { return _photo; }
      set { _photo = value; OnPropertyChanged(); }
    }

    public VMMainPage()
    {
      cameraService = new CameraService();
      Task.Run(async () => await cameraService.Init());
      TakePhotoCommand = new Command(OnTakePhotoCommand);
      ChoosePhotoCommand = new Command(OnChoosePhotoCommand);

    }

    private async void OnTakePhotoCommand(object value)
    {
      var file = await cameraService.TakeFoto();
      if (file != null)
        Photo = ImageSource.FromStream(() => file.GetStream());
    }

    private async void OnChoosePhotoCommand(object value)
    {
      var file = await cameraService.ChoosePhoto();
      if (file != null)
        Photo = ImageSource.FromStream(() => file.GetStream());
    }
  }
}
