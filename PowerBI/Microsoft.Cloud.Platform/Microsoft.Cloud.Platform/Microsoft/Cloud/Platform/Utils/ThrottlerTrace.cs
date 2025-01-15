using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002E0 RID: 736
	public class ThrottlerTrace : TraceSourceBase<ThrottlerTrace>
	{
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x00044B83 File Offset: 0x00042D83
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("P.Throttler");
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x000034D8 File Offset: 0x000016D8
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Info;
			}
		}

		// Token: 0x0400076B RID: 1899
		public const string IdentifierString = "P.Throttler";
	}
}
