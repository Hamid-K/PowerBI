using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.BIServer.Telemetry.Services;
using Microsoft.PowerBI.ReportServer.PbixLib.Parsing;

namespace Microsoft.PowerBI.ReportServer.WebApi.Pbix
{
	// Token: 0x02000014 RID: 20
	internal class PbixTelemetryLogger : IPbixTelemetryLogger
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002F4F File Offset: 0x0000114F
		public static IPbixTelemetryLogger GetInstance()
		{
			return new PbixTelemetryLogger();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002C9D File Offset: 0x00000E9D
		protected PbixTelemetryLogger()
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002F58 File Offset: 0x00001158
		public void LogPbixTelemetry(PbixReportElements pbixReportElements)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("ismobileoptimized", pbixReportElements.IsMobileOptimized().ToString());
			string hashedAnalysisServerConnectionString = pbixReportElements.GetHashedAnalysisServerConnectionString();
			dictionary.Add("asconnectionstring", hashedAnalysisServerConnectionString);
			dictionary.Add("customvisualscount", ((pbixReportElements.CustomVisuals != null) ? pbixReportElements.CustomVisuals.Count : 0).ToString());
			dictionary.Add("datamodelsize", ((pbixReportElements.DataModel != null) ? pbixReportElements.DataModel.Length : 0).ToString());
			dictionary.Add("staticresourcecount", ((pbixReportElements.StaticResources != null) ? pbixReportElements.StaticResources.Count<KeyValuePair<string, byte[]>>() : 0).ToString());
			if (pbixReportElements.ModelVersion != null)
			{
				string modelVersion = pbixReportElements.ModelVersion;
				dictionary.Add("modelversion", modelVersion);
			}
			TelemetryService.Current.TrackEvent("RS.PBI.Shred", dictionary, null);
		}

		// Token: 0x0400003C RID: 60
		private const string PbixParseEventName = "RS.PBI.Shred";

		// Token: 0x0400003D RID: 61
		private const string IsMobileOptimizedKey = "ismobileoptimized";

		// Token: 0x0400003E RID: 62
		private const string AsConnectionStringKey = "asconnectionstring";

		// Token: 0x0400003F RID: 63
		private const string CustomVisualsCountKey = "customvisualscount";

		// Token: 0x04000040 RID: 64
		private const string DataModelSizeInBytesKey = "datamodelsize";

		// Token: 0x04000041 RID: 65
		private const string StaticResourceCountKey = "staticresourcecount";

		// Token: 0x04000042 RID: 66
		private const string ModelVersionKey = "modelversion";
	}
}
