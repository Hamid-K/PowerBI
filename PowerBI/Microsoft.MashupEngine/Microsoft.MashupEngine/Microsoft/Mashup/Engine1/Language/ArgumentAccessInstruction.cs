using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001749 RID: 5961
	internal static class ArgumentAccessInstruction
	{
		// Token: 0x06009795 RID: 38805 RVA: 0x001F5FC8 File Offset: 0x001F41C8
		public static Instruction Argument(int index)
		{
			switch (index)
			{
			case 0:
				return ArgumentAccessInstruction.argument0;
			case 1:
				return ArgumentAccessInstruction.argument1;
			case 2:
				return ArgumentAccessInstruction.argument2;
			default:
				return new ArgumentAccessInstruction.ArgumentAccessInstructionN(index);
			}
		}

		// Token: 0x0400505D RID: 20573
		private static readonly Instruction argument0 = new ArgumentAccessInstruction.ArgumentAccessInstruction0();

		// Token: 0x0400505E RID: 20574
		private static readonly Instruction argument1 = new ArgumentAccessInstruction.ArgumentAccessInstruction1();

		// Token: 0x0400505F RID: 20575
		private static readonly Instruction argument2 = new ArgumentAccessInstruction.ArgumentAccessInstruction2();

		// Token: 0x0200174A RID: 5962
		private sealed class ArgumentAccessInstruction0 : Instruction
		{
			// Token: 0x06009797 RID: 38807 RVA: 0x0000A6A5 File Offset: 0x000088A5
			public override Value Execute(Value frame)
			{
				return frame;
			}

			// Token: 0x06009798 RID: 38808 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override Value Execute(ref MembersFrame0 frame)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x06009799 RID: 38809 RVA: 0x001F6016 File Offset: 0x001F4216
			public override Value Execute(ref MembersFrame1 frame)
			{
				return frame.Arg0;
			}

			// Token: 0x0600979A RID: 38810 RVA: 0x001F601E File Offset: 0x001F421E
			public override Value Execute(ref MembersFrame2 frame)
			{
				return frame.Arg0;
			}

			// Token: 0x0600979B RID: 38811 RVA: 0x001F6026 File Offset: 0x001F4226
			public override Value Execute(ref MembersFrameN frame)
			{
				return frame.Arg0;
			}
		}

		// Token: 0x0200174B RID: 5963
		private sealed class ArgumentAccessInstruction1 : Instruction
		{
			// Token: 0x0600979D RID: 38813 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override Value Execute(Value frame)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x0600979E RID: 38814 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override Value Execute(ref MembersFrame0 frame)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x0600979F RID: 38815 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override Value Execute(ref MembersFrame1 frame)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060097A0 RID: 38816 RVA: 0x001F602E File Offset: 0x001F422E
			public override Value Execute(ref MembersFrame2 frame)
			{
				return frame.Arg1;
			}

			// Token: 0x060097A1 RID: 38817 RVA: 0x001F6036 File Offset: 0x001F4236
			public override Value Execute(ref MembersFrameN frame)
			{
				return frame.Arg1;
			}
		}

		// Token: 0x0200174C RID: 5964
		private sealed class ArgumentAccessInstruction2 : Instruction
		{
			// Token: 0x060097A3 RID: 38819 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override Value Execute(Value frame)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060097A4 RID: 38820 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override Value Execute(ref MembersFrame0 frame)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060097A5 RID: 38821 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override Value Execute(ref MembersFrame1 frame)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060097A6 RID: 38822 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override Value Execute(ref MembersFrame2 frame)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060097A7 RID: 38823 RVA: 0x001F603E File Offset: 0x001F423E
			public override Value Execute(ref MembersFrameN frame)
			{
				return frame.Arg2;
			}
		}

		// Token: 0x0200174D RID: 5965
		private sealed class ArgumentAccessInstructionN : Instruction
		{
			// Token: 0x060097A9 RID: 38825 RVA: 0x001F6046 File Offset: 0x001F4246
			public ArgumentAccessInstructionN(int index)
			{
				this.index = index;
			}

			// Token: 0x060097AA RID: 38826 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override Value Execute(Value frame)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060097AB RID: 38827 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override Value Execute(ref MembersFrame0 frame)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060097AC RID: 38828 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override Value Execute(ref MembersFrame1 frame)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060097AD RID: 38829 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override Value Execute(ref MembersFrame2 frame)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060097AE RID: 38830 RVA: 0x001F6055 File Offset: 0x001F4255
			public override Value Execute(ref MembersFrameN frame)
			{
				return frame.Arg(this.index);
			}

			// Token: 0x04005060 RID: 20576
			private readonly int index;
		}
	}
}
