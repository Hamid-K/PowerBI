using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200063D RID: 1597
	internal static class ReportProcessingCompatibilityVersion
	{
		// Token: 0x0600576F RID: 22383 RVA: 0x0016F7ED File Offset: 0x0016D9ED
		public static int GetCompatibilityVersion(IConfiguration configuration)
		{
			if (configuration == null)
			{
				return ReportProcessingCompatibilityVersion.CurrentVersion;
			}
			return ReportProcessingCompatibilityVersion.TranslateToVersionNumber(configuration.UpgradeState);
		}

		// Token: 0x06005770 RID: 22384 RVA: 0x0016F803 File Offset: 0x0016DA03
		public static int TranslateToVersionNumber(ProcessingUpgradeState upgradeState)
		{
			if (upgradeState == ProcessingUpgradeState.CurrentVersion)
			{
				return ReportProcessingCompatibilityVersion.CurrentVersion;
			}
			if (upgradeState != ProcessingUpgradeState.PreviousVersionCompat)
			{
				Global.Tracer.Assert(false, "Invalid ProcessingUpgradeState");
				throw new InvalidOperationException("Invalid ProcessingUpgradeState");
			}
			return ReportProcessingCompatibilityVersion.CompatVersion;
		}

		// Token: 0x06005771 RID: 22385 RVA: 0x0016F834 File Offset: 0x0016DA34
		public static void TraceCompatibilityVersion(IConfiguration configuration)
		{
			if (Global.Tracer.TraceVerbose)
			{
				int compatibilityVersion = ReportProcessingCompatibilityVersion.GetCompatibilityVersion(configuration);
				Global.Tracer.Trace(TraceLevel.Verbose, "Processing compatibility version information: ProcessingUpgradeState: {0} Active Compat Version Number: {1}", new object[]
				{
					(configuration == null) ? "<null>" : configuration.UpgradeState.ToString(),
					compatibilityVersion
				});
			}
		}

		// Token: 0x04002E3C RID: 11836
		public const int SQL11CTP3 = 2;

		// Token: 0x04002E3D RID: 11837
		public const int SQL11RC0 = 100;

		// Token: 0x04002E3E RID: 11838
		public const int O15CTP3 = 200;

		// Token: 0x04002E3F RID: 11839
		public const int SQL16CTP3 = 300;

		// Token: 0x04002E40 RID: 11840
		public const int SQL16RC1 = 400;

		// Token: 0x04002E41 RID: 11841
		public const int Service202208 = 500;

		// Token: 0x04002E42 RID: 11842
		public const int UndefinedVersion = 0;

		// Token: 0x04002E43 RID: 11843
		public static readonly int CompatVersion = 400;

		// Token: 0x04002E44 RID: 11844
		public static readonly int CurrentVersion = 500;
	}
}
