
using System;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
namespace CameraXamarin.service
{
  public class CameraService
  {
    PermissionStatus cameraStatus;
    PermissionStatus storageStatus;

    public async Task Init()
    {
      await CrossMedia.Current.Initialize();
      cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>();
      storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<StoragePermission>();

      if(cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
      {
        cameraStatus = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();
        storageStatus = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
      }

    }

    public async Task<MediaFile> TakeFoto()
    {
      if(cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted
        &&  CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
      {
        var options = new StoreCameraMediaOptions
        {
          DefaultCamera = CameraDevice.Front,
          AllowCropping = true,
          PhotoSize = PhotoSize.Medium,
          CompressionQuality = 90,
          Directory = "CameraDemo",
          Name = $"{Guid.NewGuid()}.jgp",
          SaveToAlbum = true


        };
        return await CrossMedia.Current.TakePhotoAsync(options);
      }
      return null;
    }

    public async Task<MediaFile> ChoosePhoto()
    {
      if (CrossMedia.Current.IsPickPhotoSupported)
      {
        return await CrossMedia.Current.PickPhotoAsync();
      }
      return null;
    }

  }
}
