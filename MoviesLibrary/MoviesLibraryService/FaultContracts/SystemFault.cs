using System.Runtime.Serialization;

namespace MoviesLibrary.Service.FaultContracts
{
    /// <summary>
    /// Marker class indicating a general system fault occurred in a service call.
    /// </summary>
    [DataContract(Namespace = "")]
    public class SystemFault
    {
        
    }
}
