using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001730 RID: 5936
	internal sealed class AddInstruction : Instruction
	{
		// Token: 0x060096F8 RID: 38648 RVA: 0x001F4A0B File Offset: 0x001F2C0B
		public AddInstruction(Instruction left, Instruction right)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x060096F9 RID: 38649 RVA: 0x001F4A21 File Offset: 0x001F2C21
		public override Value Execute(Value frame)
		{
			return this.left.Execute(frame).Add(this.right.Execute(frame));
		}

		// Token: 0x060096FA RID: 38650 RVA: 0x001F4A40 File Offset: 0x001F2C40
		public override Value Execute(ref MembersFrame0 frame)
		{
			return this.left.Execute(ref frame).Add(this.right.Execute(ref frame));
		}

		// Token: 0x060096FB RID: 38651 RVA: 0x001F4A5F File Offset: 0x001F2C5F
		public override Value Execute(ref MembersFrame1 frame)
		{
			return this.left.Execute(ref frame).Add(this.right.Execute(ref frame));
		}

		// Token: 0x060096FC RID: 38652 RVA: 0x001F4A7E File Offset: 0x001F2C7E
		public override Value Execute(ref MembersFrame2 frame)
		{
			return this.left.Execute(ref frame).Add(this.right.Execute(ref frame));
		}

		// Token: 0x060096FD RID: 38653 RVA: 0x001F4A9D File Offset: 0x001F2C9D
		public override Value Execute(ref MembersFrameN frame)
		{
			return this.left.Execute(ref frame).Add(this.right.Execute(ref frame));
		}

		// Token: 0x0400502A RID: 20522
		private readonly Instruction left;

		// Token: 0x0400502B RID: 20523
		private readonly Instruction right;
	}
}
