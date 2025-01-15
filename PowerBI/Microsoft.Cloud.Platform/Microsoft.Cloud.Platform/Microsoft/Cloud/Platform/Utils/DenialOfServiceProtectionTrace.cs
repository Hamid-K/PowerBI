using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002E1 RID: 737
	public class DenialOfServiceProtectionTrace : TraceSourceBase<DenialOfServiceProtectionTrace>
	{
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x00044B97 File Offset: 0x00042D97
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("P.DoSP");
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x000034D8 File Offset: 0x000016D8
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Info;
			}
		}

		// Token: 0x0400076C RID: 1900
		public const string IdentifierString = "P.DoSP";
	}
}
