using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Classifier.Web
{
    public class ImageClassifierTelemetryInitializer : ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
            if (string.IsNullOrEmpty(telemetry.Context.Cloud.RoleName))
            {
                // Set custom role name here.
                telemetry.Context.Cloud.RoleName = "ImageClassifierWeb";
                telemetry.Context.Cloud.RoleInstance = "ImageClassifier";
            }
        }
    }
}