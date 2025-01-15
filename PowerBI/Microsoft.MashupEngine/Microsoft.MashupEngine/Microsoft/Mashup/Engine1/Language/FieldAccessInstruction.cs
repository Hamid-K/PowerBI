using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x0200173F RID: 5951
	internal sealed class FieldAccessInstruction : Instruction
	{
		// Token: 0x0600975F RID: 38751 RVA: 0x001F5844 File Offset: 0x001F3A44
		public FieldAccessInstruction(Instruction record, string field)
		{
			this.record = record;
			this.field = field;
		}

		// Token: 0x06009760 RID: 38752 RVA: 0x001F585A File Offset: 0x001F3A5A
		public override Value Execute(Value frame)
		{
			return this.record.Execute(frame)[this.field];
		}

		// Token: 0x06009761 RID: 38753 RVA: 0x001F5873 File Offset: 0x001F3A73
		public override Value Execute(ref MembersFrame0 frame)
		{
			return this.record.Execute(ref frame)[this.field];
		}

		// Token: 0x06009762 RID: 38754 RVA: 0x001F588C File Offset: 0x001F3A8C
		public override Value Execute(ref MembersFrame1 frame)
		{
			return this.record.Execute(ref frame)[this.field];
		}

		// Token: 0x06009763 RID: 38755 RVA: 0x001F58A5 File Offset: 0x001F3AA5
		public override Value Execute(ref MembersFrame2 frame)
		{
			return this.record.Execute(ref frame)[this.field];
		}

		// Token: 0x06009764 RID: 38756 RVA: 0x001F58BE File Offset: 0x001F3ABE
		public override Value Execute(ref MembersFrameN frame)
		{
			return this.record.Execute(ref frame)[this.field];
		}

		// Token: 0x04005046 RID: 20550
		private readonly Instruction record;

		// Token: 0x04005047 RID: 20551
		private readonly string field;
	}
}
