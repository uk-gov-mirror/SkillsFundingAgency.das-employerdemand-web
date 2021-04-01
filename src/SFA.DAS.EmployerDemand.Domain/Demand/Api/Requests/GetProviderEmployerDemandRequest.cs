using System.Web;
using SFA.DAS.EmployerDemand.Domain.Interfaces;

namespace SFA.DAS.EmployerDemand.Domain.Demand.Api.Requests
{
    public class GetProviderEmployerDemandRequest : IGetApiRequest
    {
        private readonly int _ukprn;
        private readonly int? _courseId;
        private readonly string _location;
        private readonly string _locationRadius;

        public GetProviderEmployerDemandRequest(int ukprn, int? courseId = null, string location = "", string locationRadius = "")
        {
            _ukprn = ukprn;
            _courseId = courseId;
            _location = location;
            _locationRadius = locationRadius;
        }

        public string GetUrl => $"providers/{_ukprn}/employer-demand?courseId={_courseId}&location={HttpUtility.UrlEncode(_location)}&locationRadius={_locationRadius}";
    }
}