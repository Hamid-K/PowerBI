using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001746 RID: 5958
	internal sealed class FunctionInstruction : Instruction
	{
		// Token: 0x06009782 RID: 38786 RVA: 0x001F5CE3 File Offset: 0x001F3EE3
		public FunctionInstruction(Instruction instruction, FunctionTypeValue type, IExpression ast, Instruction[] members, Identifier[] memberNames)
		{
			this.instruction = instruction;
			this.type = type;
			this.ast = ast;
			this.members = members;
			this.memberNames = memberNames;
		}

		// Token: 0x06009783 RID: 38787 RVA: 0x001F5D10 File Offset: 0x001F3F10
		public override Value Execute(Value frame)
		{
			return this.CreateFunction(Instruction.Execute(frame, this.members));
		}

		// Token: 0x06009784 RID: 38788 RVA: 0x001F5D24 File Offset: 0x001F3F24
		public override Value Execute(ref MembersFrame0 frame)
		{
			return this.CreateFunction(Instruction.Execute(ref frame, this.members));
		}

		// Token: 0x06009785 RID: 38789 RVA: 0x001F5D38 File Offset: 0x001F3F38
		public override Value Execute(ref MembersFrame1 frame)
		{
			return this.CreateFunction(Instruction.Execute(ref frame, this.members));
		}

		// Token: 0x06009786 RID: 38790 RVA: 0x001F5D4C File Offset: 0x001F3F4C
		public override Value Execute(ref MembersFrame2 frame)
		{
			return this.CreateFunction(Instruction.Execute(ref frame, this.members));
		}

		// Token: 0x06009787 RID: 38791 RVA: 0x001F5D60 File Offset: 0x001F3F60
		public override Value Execute(ref MembersFrameN frame)
		{
			return this.CreateFunction(Instruction.Execute(ref frame, this.members));
		}

		// Token: 0x06009788 RID: 38792 RVA: 0x001F5D74 File Offset: 0x001F3F74
		private Value CreateFunction(Value[] members)
		{
			return MembersFunctionValue.New(this.instruction, this.type, this.ast, members, this.memberNames);
		}

		// Token: 0x04005055 RID: 20565
		private readonly Instruction instruction;

		// Token: 0x04005056 RID: 20566
		private readonly FunctionTypeValue type;

		// Token: 0x04005057 RID: 20567
		private readonly IExpression ast;

		// Token: 0x04005058 RID: 20568
		private readonly Instruction[] members;

		// Token: 0x04005059 RID: 20569
		private readonly Identifier[] memberNames;
	}
}
