using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x02000379 RID: 889
	internal class TransfromValue : NativeFunctionValue1<Value, Value>
	{
		// Token: 0x06001F6A RID: 8042 RVA: 0x00051662 File Offset: 0x0004F862
		public TransfromValue(TypeValue type, Func<TypeValue, Value, Value> transformer)
			: base(type, "Value", TypeValue.Any)
		{
			this.type = type;
			this.transformer = transformer;
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x00051683 File Offset: 0x0004F883
		public override Value TypedInvoke(Value value)
		{
			return this.transformer(this.type, value);
		}

		// Token: 0x04000B6A RID: 2922
		private TypeValue type;

		// Token: 0x04000B6B RID: 2923
		private Func<TypeValue, Value, Value> transformer;
	}
}
