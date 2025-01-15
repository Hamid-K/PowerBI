using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001759 RID: 5977
	internal sealed class MembersFunctionValue2 : MembersFunctionValue
	{
		// Token: 0x060097DF RID: 38879 RVA: 0x001F6641 File Offset: 0x001F4841
		public MembersFunctionValue2(Instruction instruction, FunctionTypeValue type, IExpression ast, Value[] members, Identifier[] memberNames)
			: base(instruction, type, ast, members, memberNames)
		{
		}

		// Token: 0x060097E0 RID: 38880 RVA: 0x001F66B7 File Offset: 0x001F48B7
		public override Value Invoke()
		{
			if (base.Min > 0)
			{
				throw ValueException.InvalidArguments(this, Array.Empty<Value>());
			}
			return this.Invoke(Value.Null, Value.Null);
		}

		// Token: 0x060097E1 RID: 38881 RVA: 0x001F66DE File Offset: 0x001F48DE
		public override Value Invoke(Value arg0)
		{
			if (base.Min > 1)
			{
				throw ValueException.InvalidArguments(this, new Value[] { arg0 });
			}
			return this.Invoke(arg0, Value.Null);
		}

		// Token: 0x060097E2 RID: 38882 RVA: 0x001F6708 File Offset: 0x001F4908
		public sealed override Value Invoke(Value arg0, Value arg1)
		{
			MembersFrame2 membersFrame = new MembersFrame2(this, arg0, arg1);
			return this.instruction.Execute(ref membersFrame);
		}
	}
}
