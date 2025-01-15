using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200006E RID: 110
	internal sealed class ProcessingConfiguration : IConfiguration
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00012A6C File Offset: 0x00010C6C
		public bool ShowSubreportErrorDetails
		{
			get
			{
				return Globals.Configuration.EnableRemoteErrors;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00012A78 File Offset: 0x00010C78
		public IRdlSandboxConfig RdlSandboxing
		{
			get
			{
				return Globals.Configuration.RdlSandboxing;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00012A84 File Offset: 0x00010C84
		public IMapTileServerConfiguration MapTileServerConfiguration
		{
			get
			{
				return Globals.Configuration.MapTileServerConfiguration;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00005BEF File Offset: 0x00003DEF
		public ProcessingUpgradeState UpgradeState
		{
			get
			{
				return ProcessingUpgradeState.CurrentVersion;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00005BEF File Offset: 0x00003DEF
		public bool ProhibitSerializableValues
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00005BEF File Offset: 0x00003DEF
		public bool UseSafeExpressionsRuntime
		{
			get
			{
				return false;
			}
		}
	}
}
