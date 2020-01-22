using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Tracking.API.Services
{
    public class MyTrackingService  : TrackingService.TrackingServiceBase
    {

        private readonly ILogger<MyTrackingService> logger;

        public MyTrackingService(ILogger<MyTrackingService> logger)
        {
            this.logger = logger;
        }

        public override Task<AddLocationResponse> AddLocation(
            AddLocationRequest request,
            ServerCallContext context)
        {
            logger.LogInformation($"({request.Latitude},{request.Longitude}) {request.Speed} km");

            var response = new AddLocationResponse { IsConfirmed = true };

            return Task.FromResult(response);
        }

        public override Task<AddLocationResponse> AddLocationStream(
            IAsyncStreamReader<AddLocationRequest> requestStream, 
            ServerCallContext context)
        {
            return base.AddLocationStream(requestStream, context);
        }
    }
}
