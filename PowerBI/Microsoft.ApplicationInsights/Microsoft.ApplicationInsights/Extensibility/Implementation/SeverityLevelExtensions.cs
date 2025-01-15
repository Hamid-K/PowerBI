using System;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x0200007B RID: 123
	internal static class SeverityLevelExtensions
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x00011970 File Offset: 0x0000FB70
		public static Microsoft.ApplicationInsights.DataContracts.SeverityLevel? TranslateSeverityLevel(this Microsoft.ApplicationInsights.Extensibility.Implementation.External.SeverityLevel? sdkSeverityLevel)
		{
			if (sdkSeverityLevel == null)
			{
				return null;
			}
			switch (sdkSeverityLevel.Value)
			{
			case Microsoft.ApplicationInsights.Extensibility.Implementation.External.SeverityLevel.Information:
				return new Microsoft.ApplicationInsights.DataContracts.SeverityLevel?(Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Information);
			case Microsoft.ApplicationInsights.Extensibility.Implementation.External.SeverityLevel.Warning:
				return new Microsoft.ApplicationInsights.DataContracts.SeverityLevel?(Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Warning);
			case Microsoft.ApplicationInsights.Extensibility.Implementation.External.SeverityLevel.Error:
				return new Microsoft.ApplicationInsights.DataContracts.SeverityLevel?(Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Error);
			case Microsoft.ApplicationInsights.Extensibility.Implementation.External.SeverityLevel.Critical:
				return new Microsoft.ApplicationInsights.DataContracts.SeverityLevel?(Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Critical);
			default:
				return new Microsoft.ApplicationInsights.DataContracts.SeverityLevel?(Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Verbose);
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000119D4 File Offset: 0x0000FBD4
		public static Microsoft.ApplicationInsights.Extensibility.Implementation.External.SeverityLevel? TranslateSeverityLevel(this Microsoft.ApplicationInsights.DataContracts.SeverityLevel? dataPlatformSeverityLevel)
		{
			if (dataPlatformSeverityLevel == null)
			{
				return null;
			}
			switch (dataPlatformSeverityLevel.Value)
			{
			case Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Information:
				return new Microsoft.ApplicationInsights.Extensibility.Implementation.External.SeverityLevel?(Microsoft.ApplicationInsights.Extensibility.Implementation.External.SeverityLevel.Information);
			case Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Warning:
				return new Microsoft.ApplicationInsights.Extensibility.Implementation.External.SeverityLevel?(Microsoft.ApplicationInsights.Extensibility.Implementation.External.SeverityLevel.Warning);
			case Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Error:
				return new Microsoft.ApplicationInsights.Extensibility.Implementation.External.SeverityLevel?(Microsoft.ApplicationInsights.Extensibility.Implementation.External.SeverityLevel.Error);
			case Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Critical:
				return new Microsoft.ApplicationInsights.Extensibility.Implementation.External.SeverityLevel?(Microsoft.ApplicationInsights.Extensibility.Implementation.External.SeverityLevel.Critical);
			default:
				return new Microsoft.ApplicationInsights.Extensibility.Implementation.External.SeverityLevel?(Microsoft.ApplicationInsights.Extensibility.Implementation.External.SeverityLevel.Verbose);
			}
		}
	}
}
