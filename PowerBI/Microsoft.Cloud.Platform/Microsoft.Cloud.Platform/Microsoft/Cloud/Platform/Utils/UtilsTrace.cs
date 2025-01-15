using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002DE RID: 734
	public class UtilsTrace : TraceSourceBase<UtilsTrace>
	{
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x00044B58 File Offset: 0x00042D58
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("P.Utils");
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x00044B64 File Offset: 0x00042D64
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Warning;
			}
		}

		// Token: 0x04000769 RID: 1897
		public const string IdentifierString = "P.Utils";
	}
}
