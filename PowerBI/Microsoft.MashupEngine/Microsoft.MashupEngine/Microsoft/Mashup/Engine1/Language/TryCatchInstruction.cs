using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001748 RID: 5960
	internal sealed class TryCatchInstruction : Instruction
	{
		// Token: 0x0600978F RID: 38799 RVA: 0x001F5E20 File Offset: 0x001F4020
		public TryCatchInstruction(Instruction tryInstruction, Instruction catchInstruction)
		{
			this.tryInstruction = tryInstruction;
			this.catchInstruction = catchInstruction;
		}

		// Token: 0x06009790 RID: 38800 RVA: 0x001F5E38 File Offset: 0x001F4038
		public override Value Execute(Value frame)
		{
			Value value;
			try
			{
				value = this.tryInstruction.Execute(frame);
			}
			catch (ValueException ex)
			{
				value = this.catchInstruction.Execute(frame).AsFunction.Invoke(ex.Value);
			}
			return value;
		}

		// Token: 0x06009791 RID: 38801 RVA: 0x001F5E88 File Offset: 0x001F4088
		public override Value Execute(ref MembersFrame0 frame)
		{
			Value value;
			try
			{
				value = this.tryInstruction.Execute(ref frame);
			}
			catch (ValueException ex)
			{
				value = this.catchInstruction.Execute(ref frame).AsFunction.Invoke(ex.Value);
			}
			return value;
		}

		// Token: 0x06009792 RID: 38802 RVA: 0x001F5ED8 File Offset: 0x001F40D8
		public override Value Execute(ref MembersFrame1 frame)
		{
			Value value;
			try
			{
				value = this.tryInstruction.Execute(ref frame);
			}
			catch (ValueException ex)
			{
				value = this.catchInstruction.Execute(ref frame).AsFunction.Invoke(ex.Value);
			}
			return value;
		}

		// Token: 0x06009793 RID: 38803 RVA: 0x001F5F28 File Offset: 0x001F4128
		public override Value Execute(ref MembersFrame2 frame)
		{
			Value value;
			try
			{
				value = this.tryInstruction.Execute(ref frame);
			}
			catch (ValueException ex)
			{
				value = this.catchInstruction.Execute(ref frame).AsFunction.Invoke(ex.Value);
			}
			return value;
		}

		// Token: 0x06009794 RID: 38804 RVA: 0x001F5F78 File Offset: 0x001F4178
		public override Value Execute(ref MembersFrameN frame)
		{
			Value value;
			try
			{
				value = this.tryInstruction.Execute(ref frame);
			}
			catch (ValueException ex)
			{
				value = this.catchInstruction.Execute(ref frame).AsFunction.Invoke(ex.Value);
			}
			return value;
		}

		// Token: 0x0400505B RID: 20571
		private readonly Instruction tryInstruction;

		// Token: 0x0400505C RID: 20572
		private readonly Instruction catchInstruction;
	}
}
