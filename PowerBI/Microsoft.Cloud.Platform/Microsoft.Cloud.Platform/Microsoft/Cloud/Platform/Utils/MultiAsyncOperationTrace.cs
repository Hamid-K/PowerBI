using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002E4 RID: 740
	public class MultiAsyncOperationTrace : TraceSourceBase<MultiAsyncOperationTrace>
	{
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x00044BD3 File Offset: 0x00042DD3
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("P.MultiAsyncOperation");
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x000034D8 File Offset: 0x000016D8
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Info;
			}
		}

		// Token: 0x0400076F RID: 1903
		public const string IdentifierString = "P.MultiAsyncOperation";
	}
}
