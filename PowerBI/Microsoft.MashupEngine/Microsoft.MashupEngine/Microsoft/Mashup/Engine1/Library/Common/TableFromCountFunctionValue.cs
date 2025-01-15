using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001141 RID: 4417
	internal sealed class TableFromCountFunctionValue : NativeFunctionValue1<TableValue, NumberValue>
	{
		// Token: 0x060073BD RID: 29629 RVA: 0x0018E228 File Offset: 0x0018C428
		private TableFromCountFunctionValue()
			: base(TypeValue.Table, "count", TypeValue.Int64)
		{
		}

		// Token: 0x060073BE RID: 29630 RVA: 0x0018E23F File Offset: 0x0018C43F
		public override TableValue TypedInvoke(NumberValue count)
		{
			return new CountTableValue(count.AsInteger64);
		}

		// Token: 0x04003FBB RID: 16315
		public static readonly FunctionValue Instance = new TableFromCountFunctionValue();
	}
}
