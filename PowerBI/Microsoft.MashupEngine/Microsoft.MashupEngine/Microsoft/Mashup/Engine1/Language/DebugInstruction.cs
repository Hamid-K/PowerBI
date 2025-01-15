using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001750 RID: 5968
	internal sealed class DebugInstruction : Instruction
	{
		// Token: 0x060097B7 RID: 38839 RVA: 0x001F60FB File Offset: 0x001F42FB
		public DebugInstruction(Instruction instruction, SourceLocation location)
		{
			this.instruction = instruction;
			this.location = location;
		}

		// Token: 0x060097B8 RID: 38840 RVA: 0x001F6114 File Offset: 0x001F4314
		public override Value Execute(Value frame)
		{
			Value value;
			try
			{
				value = this.instruction.Execute(frame);
			}
			catch (ValueException ex)
			{
				ex.AddFrame(this.location);
				throw;
			}
			return value;
		}

		// Token: 0x060097B9 RID: 38841 RVA: 0x001F6150 File Offset: 0x001F4350
		public override Value Execute(ref MembersFrame0 frame)
		{
			Value value;
			try
			{
				value = this.instruction.Execute(ref frame);
			}
			catch (ValueException ex)
			{
				ex.AddFrame(this.location);
				throw;
			}
			return value;
		}

		// Token: 0x060097BA RID: 38842 RVA: 0x001F618C File Offset: 0x001F438C
		public override Value Execute(ref MembersFrame1 frame)
		{
			Value value;
			try
			{
				value = this.instruction.Execute(ref frame);
			}
			catch (ValueException ex)
			{
				ex.AddFrame(this.location);
				throw;
			}
			return value;
		}

		// Token: 0x060097BB RID: 38843 RVA: 0x001F61C8 File Offset: 0x001F43C8
		public override Value Execute(ref MembersFrame2 frame)
		{
			Value value;
			try
			{
				value = this.instruction.Execute(ref frame);
			}
			catch (ValueException ex)
			{
				ex.AddFrame(this.location);
				throw;
			}
			return value;
		}

		// Token: 0x060097BC RID: 38844 RVA: 0x001F6204 File Offset: 0x001F4404
		public override Value Execute(ref MembersFrameN frame)
		{
			Value value;
			try
			{
				value = this.instruction.Execute(ref frame);
			}
			catch (ValueException ex)
			{
				ex.AddFrame(this.location);
				throw;
			}
			return value;
		}

		// Token: 0x060097BD RID: 38845 RVA: 0x001F6240 File Offset: 0x001F4440
		public override bool ExecuteCondition(Value frame)
		{
			bool flag;
			try
			{
				flag = this.instruction.ExecuteCondition(frame);
			}
			catch (ValueException ex)
			{
				ex.AddFrame(this.location);
				throw;
			}
			return flag;
		}

		// Token: 0x060097BE RID: 38846 RVA: 0x001F627C File Offset: 0x001F447C
		public override bool ExecuteCondition(ref MembersFrame0 frame)
		{
			bool flag;
			try
			{
				flag = this.instruction.ExecuteCondition(ref frame);
			}
			catch (ValueException ex)
			{
				ex.AddFrame(this.location);
				throw;
			}
			return flag;
		}

		// Token: 0x060097BF RID: 38847 RVA: 0x001F62B8 File Offset: 0x001F44B8
		public override bool ExecuteCondition(ref MembersFrame1 frame)
		{
			bool flag;
			try
			{
				flag = this.instruction.ExecuteCondition(ref frame);
			}
			catch (ValueException ex)
			{
				ex.AddFrame(this.location);
				throw;
			}
			return flag;
		}

		// Token: 0x060097C0 RID: 38848 RVA: 0x001F62F4 File Offset: 0x001F44F4
		public override bool ExecuteCondition(ref MembersFrame2 frame)
		{
			bool flag;
			try
			{
				flag = this.instruction.ExecuteCondition(ref frame);
			}
			catch (ValueException ex)
			{
				ex.AddFrame(this.location);
				throw;
			}
			return flag;
		}

		// Token: 0x060097C1 RID: 38849 RVA: 0x001F6330 File Offset: 0x001F4530
		public override bool ExecuteCondition(ref MembersFrameN frame)
		{
			bool flag;
			try
			{
				flag = this.instruction.ExecuteCondition(ref frame);
			}
			catch (ValueException ex)
			{
				ex.AddFrame(this.location);
				throw;
			}
			return flag;
		}

		// Token: 0x04005065 RID: 20581
		private readonly Instruction instruction;

		// Token: 0x04005066 RID: 20582
		private readonly SourceLocation location;
	}
}
