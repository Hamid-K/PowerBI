using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002DF RID: 735
	public class SequencerTrace : TraceSourceBase<SequencerTrace>
	{
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x00044B6F File Offset: 0x00042D6F
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("P.Sequencer");
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x000034D8 File Offset: 0x000016D8
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Info;
			}
		}

		// Token: 0x0400076A RID: 1898
		public const string IdentifierString = "P.Sequencer";
	}
}
