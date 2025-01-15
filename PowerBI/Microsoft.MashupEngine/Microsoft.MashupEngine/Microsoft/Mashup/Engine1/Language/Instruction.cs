using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x0200172E RID: 5934
	internal abstract class Instruction
	{
		// Token: 0x060096E0 RID: 38624
		public abstract Value Execute(Value frame);

		// Token: 0x060096E1 RID: 38625
		public abstract Value Execute(ref MembersFrame0 frame);

		// Token: 0x060096E2 RID: 38626
		public abstract Value Execute(ref MembersFrame1 frame);

		// Token: 0x060096E3 RID: 38627
		public abstract Value Execute(ref MembersFrame2 frame);

		// Token: 0x060096E4 RID: 38628
		public abstract Value Execute(ref MembersFrameN frame);

		// Token: 0x060096E5 RID: 38629 RVA: 0x001F4898 File Offset: 0x001F2A98
		public virtual bool ExecuteCondition(Value frame)
		{
			return this.Execute(frame).AsBoolean;
		}

		// Token: 0x060096E6 RID: 38630 RVA: 0x001F48A6 File Offset: 0x001F2AA6
		public virtual bool ExecuteCondition(ref MembersFrame0 frame)
		{
			return this.Execute(ref frame).AsBoolean;
		}

		// Token: 0x060096E7 RID: 38631 RVA: 0x001F48B4 File Offset: 0x001F2AB4
		public virtual bool ExecuteCondition(ref MembersFrame1 frame)
		{
			return this.Execute(ref frame).AsBoolean;
		}

		// Token: 0x060096E8 RID: 38632 RVA: 0x001F48C2 File Offset: 0x001F2AC2
		public virtual bool ExecuteCondition(ref MembersFrame2 frame)
		{
			return this.Execute(ref frame).AsBoolean;
		}

		// Token: 0x060096E9 RID: 38633 RVA: 0x001F48D0 File Offset: 0x001F2AD0
		public virtual bool ExecuteCondition(ref MembersFrameN frame)
		{
			return this.Execute(ref frame).AsBoolean;
		}

		// Token: 0x060096EA RID: 38634 RVA: 0x001F48DE File Offset: 0x001F2ADE
		public virtual bool TryGetConstant(out Value value)
		{
			value = Value.Null;
			return false;
		}

		// Token: 0x060096EB RID: 38635 RVA: 0x001F48E8 File Offset: 0x001F2AE8
		public static Value[] Execute(Value frame, Instruction[] instructions)
		{
			Value[] array = new Value[instructions.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = instructions[i].Execute(frame);
			}
			return array;
		}

		// Token: 0x060096EC RID: 38636 RVA: 0x001F491C File Offset: 0x001F2B1C
		public static Value[] Execute(ref MembersFrame0 frame, Instruction[] instructions)
		{
			Value[] array = new Value[instructions.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = instructions[i].Execute(ref frame);
			}
			return array;
		}

		// Token: 0x060096ED RID: 38637 RVA: 0x001F4950 File Offset: 0x001F2B50
		public static Value[] Execute(ref MembersFrame1 frame, Instruction[] instructions)
		{
			Value[] array = new Value[instructions.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = instructions[i].Execute(ref frame);
			}
			return array;
		}

		// Token: 0x060096EE RID: 38638 RVA: 0x001F4984 File Offset: 0x001F2B84
		public static Value[] Execute(ref MembersFrame2 frame, Instruction[] instructions)
		{
			Value[] array = new Value[instructions.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = instructions[i].Execute(ref frame);
			}
			return array;
		}

		// Token: 0x060096EF RID: 38639 RVA: 0x001F49B8 File Offset: 0x001F2BB8
		public static Value[] Execute(ref MembersFrameN frame, Instruction[] instructions)
		{
			Value[] array = new Value[instructions.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = instructions[i].Execute(ref frame);
			}
			return array;
		}
	}
}
