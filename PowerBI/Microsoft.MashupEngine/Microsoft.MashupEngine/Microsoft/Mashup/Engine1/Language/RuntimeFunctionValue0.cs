using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001752 RID: 5970
	internal sealed class RuntimeFunctionValue0 : RuntimeFunctionValue
	{
		// Token: 0x060097C7 RID: 38855 RVA: 0x001F63FB File Offset: 0x001F45FB
		public RuntimeFunctionValue0(Instruction instruction, FunctionTypeValue type, IExpression ast)
			: base(instruction, type, ast)
		{
		}

		// Token: 0x060097C8 RID: 38856 RVA: 0x001F6408 File Offset: 0x001F4608
		public override Value Invoke()
		{
			MembersFrame0 membersFrame = new MembersFrame0(null);
			return this.instruction.Execute(ref membersFrame);
		}
	}
}
