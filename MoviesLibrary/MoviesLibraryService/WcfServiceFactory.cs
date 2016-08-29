using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Unity.Wcf;
using MoviesLibrary.Interface;
using MoviesLibrary.Provider;
using System.Web.Configuration;

namespace MoviesLibrary.Service
{
    /// <summary>
    /// This is to be used as the factory for creating WCF services and bootstraps the Unity IoC container.
    /// </summary>
	public class WcfServiceFactory : UnityServiceHostFactory
    {
        /// <summary>
        /// This sets up all the registraton for IoC container
        /// </summary>
        /// <param name="container">A <see cref="IUnityContainer"/> that is used for type registrations.</param>
        protected override void ConfigureContainer(IUnityContainer container)
        {
            var unityConfigSection = WebConfigurationManager.GetSection("unity") as UnityConfigurationSection;
            if (unityConfigSection != null)
                unityConfigSection.Configure(container);
            container.RegisterType<IMoviesLibraryService, MoviesLibraryService>();
            container.RegisterType<IMovieLibraryProvider, MoviesLibraryDataProvider>();
        }
    }    
}