using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001747 RID: 5959
	internal sealed class ThrowInstruction : Instruction
	{
		// Token: 0x06009789 RID: 38793 RVA: 0x001F5D94 File Offset: 0x001F3F94
		public ThrowInstruction(Instruction instruction)
		{
			this.instruction = instruction;
		}

		// Token: 0x0600978A RID: 38794 RVA: 0x001F5DA3 File Offset: 0x001F3FA3
		public override Value Execute(Value frame)
		{
			throw ValueException.New(this.instruction.Execute(frame).AsRecord, null);
		}

		// Token: 0x0600978B RID: 38795 RVA: 0x001F5DBC File Offset: 0x001F3FBC
		public override Value Execute(ref MembersFrame0 frame)
		{
			throw ValueException.New(this.instruction.Execute(ref frame).AsRecord, null);
		}

		// Token: 0x0600978C RID: 38796 RVA: 0x001F5DD5 File Offset: 0x001F3FD5
		public override Value Execute(ref MembersFrame1 frame)
		{
			throw ValueException.New(this.instruction.Execute(ref frame).AsRecord, null);
		}

		// Token: 0x0600978D RID: 38797 RVA: 0x001F5DEE File Offset: 0x001F3FEE
		public override Value Execute(ref MembersFrame2 frame)
		{
			throw ValueException.New(this.instruction.Execute(ref frame).AsRecord, null);
		}

		// Token: 0x0600978E RID: 38798 RVA: 0x001F5E07 File Offset: 0x001F4007
		public override Value Execute(ref MembersFrameN frame)
		{
			throw ValueException.New(this.instruction.Execute(ref frame).AsRecord, null);
		}

		// Token: 0x0400505A RID: 20570
		private readonly Instruction instruction;
	}
}
