using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002E2 RID: 738
	public class RequestBlockerTrace : TraceSourceBase<RequestBlockerTrace>
	{
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x00044BAB File Offset: 0x00042DAB
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("P.RequestBlocker");
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x000034D8 File Offset: 0x000016D8
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Info;
			}
		}

		// Token: 0x0400076D RID: 1901
		public const string IdentifierString = "P.RequestBlocker";
	}
}
