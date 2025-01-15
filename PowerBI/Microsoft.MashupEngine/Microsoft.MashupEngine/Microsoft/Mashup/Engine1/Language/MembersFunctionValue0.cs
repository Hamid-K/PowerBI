using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001757 RID: 5975
	internal sealed class MembersFunctionValue0 : MembersFunctionValue
	{
		// Token: 0x060097DA RID: 38874 RVA: 0x001F6641 File Offset: 0x001F4841
		public MembersFunctionValue0(Instruction instruction, FunctionTypeValue type, IExpression ast, Value[] members, Identifier[] memberNames)
			: base(instruction, type, ast, members, memberNames)
		{
		}

		// Token: 0x060097DB RID: 38875 RVA: 0x001F6650 File Offset: 0x001F4850
		public override Value Invoke()
		{
			MembersFrame0 membersFrame = new MembersFrame0(this);
			return this.instruction.Execute(ref membersFrame);
		}
	}
}
