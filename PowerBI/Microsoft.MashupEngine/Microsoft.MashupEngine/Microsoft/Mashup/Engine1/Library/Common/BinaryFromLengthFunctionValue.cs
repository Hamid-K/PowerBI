using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200102E RID: 4142
	internal sealed class BinaryFromLengthFunctionValue : NativeFunctionValue1<BinaryValue, NumberValue>
	{
		// Token: 0x06006C14 RID: 27668 RVA: 0x0017472E File Offset: 0x0017292E
		private BinaryFromLengthFunctionValue()
			: base(TypeValue.Binary, "length", TypeValue.Int64)
		{
		}

		// Token: 0x06006C15 RID: 27669 RVA: 0x00174745 File Offset: 0x00172945
		public override BinaryValue TypedInvoke(NumberValue length)
		{
			return new LengthBinaryValue(length.AsInteger64);
		}

		// Token: 0x04003C3A RID: 15418
		public static readonly FunctionValue Instance = new BinaryFromLengthFunctionValue();
	}
}
