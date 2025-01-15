using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x0200174E RID: 5966
	internal static class MemberAccessInstruction
	{
		// Token: 0x060097AF RID: 38831 RVA: 0x001F6063 File Offset: 0x001F4263
		public static Instruction Member(int index)
		{
			switch (index)
			{
			case 0:
				return MemberAccessInstruction.member0;
			case 1:
				return MemberAccessInstruction.member1;
			case 2:
				return MemberAccessInstruction.member2;
			default:
				return new MemberAccessInstruction.MemberAccessInstructionN(index);
			}
		}

		// Token: 0x04005061 RID: 20577
		private static readonly Instruction member0 = new MemberAccessInstruction.MemberAccessInstructionN(0);

		// Token: 0x04005062 RID: 20578
		private static readonly Instruction member1 = new MemberAccessInstruction.MemberAccessInstructionN(1);

		// Token: 0x04005063 RID: 20579
		private static readonly Instruction member2 = new MemberAccessInstruction.MemberAccessInstructionN(2);

		// Token: 0x0200174F RID: 5967
		private sealed class MemberAccessInstructionN : Instruction
		{
			// Token: 0x060097B1 RID: 38833 RVA: 0x001F60B4 File Offset: 0x001F42B4
			public MemberAccessInstructionN(int index)
			{
				this.index = index;
			}

			// Token: 0x060097B2 RID: 38834 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override Value Execute(Value frame)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060097B3 RID: 38835 RVA: 0x001F60C3 File Offset: 0x001F42C3
			public override Value Execute(ref MembersFrame0 frame)
			{
				return frame.Member(this.index);
			}

			// Token: 0x060097B4 RID: 38836 RVA: 0x001F60D1 File Offset: 0x001F42D1
			public override Value Execute(ref MembersFrame1 frame)
			{
				return frame.Member(this.index);
			}

			// Token: 0x060097B5 RID: 38837 RVA: 0x001F60DF File Offset: 0x001F42DF
			public override Value Execute(ref MembersFrame2 frame)
			{
				return frame.Member(this.index);
			}

			// Token: 0x060097B6 RID: 38838 RVA: 0x001F60ED File Offset: 0x001F42ED
			public override Value Execute(ref MembersFrameN frame)
			{
				return frame.Member(this.index);
			}

			// Token: 0x04005064 RID: 20580
			private readonly int index;
		}
	}
}
