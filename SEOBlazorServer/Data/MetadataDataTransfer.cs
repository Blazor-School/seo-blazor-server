using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SEOBlazorServer.Data
{
    public class MetadataDataTransfer : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private string _description;
        private readonly NavigationManager _navigationManager;
        private readonly MetadataProvider _metadataProvider;

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }

        public MetadataDataTransfer(NavigationManager navigationManager, MetadataProvider metadataProvider)
        {
            _navigationManager = navigationManager;
            _metadataProvider = metadataProvider;

            _navigationManager.LocationChanged += UpdateMetadata;
            UpdateMetadata(_navigationManager.Uri);
        }

        private void UpdateMetadata(object sender, LocationChangedEventArgs e)
        {
            UpdateMetadata(e.Location);
        }

        private void UpdateMetadata(string url)
        {
            var metadataValue = _metadataProvider.RouteDetailMapping.FirstOrDefault(vp => url.EndsWith(vp.Key)).Value;

            if (metadataValue is null)
            {
                metadataValue = new()
                {
                    Title = "Default",
                    Description = "Default"
                };
            }

            Title = metadataValue.Title;
            Description = metadataValue.Description;
        }

        public void Dispose()
        {
            _navigationManager.LocationChanged -= UpdateMetadata;
        }
    }
}
