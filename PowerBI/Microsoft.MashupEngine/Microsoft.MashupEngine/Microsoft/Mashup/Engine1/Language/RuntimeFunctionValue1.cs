using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001753 RID: 5971
	internal sealed class RuntimeFunctionValue1 : RuntimeFunctionValue
	{
		// Token: 0x060097C9 RID: 38857 RVA: 0x001F63FB File Offset: 0x001F45FB
		public RuntimeFunctionValue1(Instruction instruction, FunctionTypeValue type, IExpression ast)
			: base(instruction, type, ast)
		{
		}

		// Token: 0x060097CA RID: 38858 RVA: 0x001F642A File Offset: 0x001F462A
		public override Value Invoke()
		{
			if (base.Min > 0)
			{
				throw ValueException.InvalidArguments(this, Array.Empty<Value>());
			}
			return this.Invoke(Value.Null);
		}

		// Token: 0x060097CB RID: 38859 RVA: 0x001F644C File Offset: 0x001F464C
		public override Value Invoke(Value arg0)
		{
			return this.instruction.Execute(arg0);
		}
	}
}
