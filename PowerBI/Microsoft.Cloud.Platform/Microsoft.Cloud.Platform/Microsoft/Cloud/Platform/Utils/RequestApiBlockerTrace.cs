using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002E3 RID: 739
	public class RequestApiBlockerTrace : TraceSourceBase<RequestApiBlockerTrace>
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x00044BBF File Offset: 0x00042DBF
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("P.RequestApiBlocker");
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x000034D8 File Offset: 0x000016D8
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Info;
			}
		}

		// Token: 0x0400076E RID: 1902
		public const string IdentifierString = "P.RequestApiBlocker";
	}
}
