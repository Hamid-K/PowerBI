using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x0200172F RID: 5935
	internal sealed class ConstantInstruction : Instruction
	{
		// Token: 0x060096F1 RID: 38641 RVA: 0x001F49E9 File Offset: 0x001F2BE9
		public ConstantInstruction(Value value)
		{
			this.value = value;
		}

		// Token: 0x060096F2 RID: 38642 RVA: 0x001F49F8 File Offset: 0x001F2BF8
		public override Value Execute(Value frame)
		{
			return this.value;
		}

		// Token: 0x060096F3 RID: 38643 RVA: 0x001F49F8 File Offset: 0x001F2BF8
		public override Value Execute(ref MembersFrame0 frame)
		{
			return this.value;
		}

		// Token: 0x060096F4 RID: 38644 RVA: 0x001F49F8 File Offset: 0x001F2BF8
		public override Value Execute(ref MembersFrame1 frame)
		{
			return this.value;
		}

		// Token: 0x060096F5 RID: 38645 RVA: 0x001F49F8 File Offset: 0x001F2BF8
		public override Value Execute(ref MembersFrame2 frame)
		{
			return this.value;
		}

		// Token: 0x060096F6 RID: 38646 RVA: 0x001F49F8 File Offset: 0x001F2BF8
		public override Value Execute(ref MembersFrameN frame)
		{
			return this.value;
		}

		// Token: 0x060096F7 RID: 38647 RVA: 0x001F4A00 File Offset: 0x001F2C00
		public override bool TryGetConstant(out Value value)
		{
			value = this.value;
			return true;
		}

		// Token: 0x04005029 RID: 20521
		private readonly Value value;
	}
}
