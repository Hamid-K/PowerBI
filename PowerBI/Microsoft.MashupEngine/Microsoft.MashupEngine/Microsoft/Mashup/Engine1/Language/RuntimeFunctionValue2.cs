using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001754 RID: 5972
	internal sealed class RuntimeFunctionValue2 : RuntimeFunctionValue
	{
		// Token: 0x060097CC RID: 38860 RVA: 0x001F63FB File Offset: 0x001F45FB
		public RuntimeFunctionValue2(Instruction instruction, FunctionTypeValue type, IExpression ast)
			: base(instruction, type, ast)
		{
		}

		// Token: 0x060097CD RID: 38861 RVA: 0x001F645A File Offset: 0x001F465A
		public override Value Invoke()
		{
			if (base.Min > 0)
			{
				throw ValueException.InvalidArguments(this, Array.Empty<Value>());
			}
			return this.Invoke(Value.Null, Value.Null);
		}

		// Token: 0x060097CE RID: 38862 RVA: 0x001F6481 File Offset: 0x001F4681
		public override Value Invoke(Value arg0)
		{
			if (base.Min > 1)
			{
				throw ValueException.InvalidArguments(this, new Value[] { arg0 });
			}
			return this.Invoke(arg0, Value.Null);
		}

		// Token: 0x060097CF RID: 38863 RVA: 0x001F64AC File Offset: 0x001F46AC
		public override Value Invoke(Value arg0, Value arg1)
		{
			MembersFrame2 membersFrame = new MembersFrame2(null, arg0, arg1);
			return this.instruction.Execute(ref membersFrame);
		}
	}
}
