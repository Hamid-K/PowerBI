using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001758 RID: 5976
	internal sealed class MembersFunctionValue1 : MembersFunctionValue
	{
		// Token: 0x060097DC RID: 38876 RVA: 0x001F6641 File Offset: 0x001F4841
		public MembersFunctionValue1(Instruction instruction, FunctionTypeValue type, IExpression ast, Value[] members, Identifier[] memberNames)
			: base(instruction, type, ast, members, memberNames)
		{
		}

		// Token: 0x060097DD RID: 38877 RVA: 0x001F6672 File Offset: 0x001F4872
		public override Value Invoke()
		{
			if (base.Min > 0)
			{
				throw ValueException.InvalidArguments(this, Array.Empty<Value>());
			}
			return this.Invoke(Value.Null);
		}

		// Token: 0x060097DE RID: 38878 RVA: 0x001F6694 File Offset: 0x001F4894
		public override Value Invoke(Value arg0)
		{
			MembersFrame1 membersFrame = new MembersFrame1(this, arg0);
			return this.instruction.Execute(ref membersFrame);
		}
	}
}
