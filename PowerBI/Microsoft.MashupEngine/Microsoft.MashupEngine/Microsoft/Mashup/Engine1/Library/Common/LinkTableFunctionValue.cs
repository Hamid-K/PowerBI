using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010DC RID: 4316
	internal sealed class LinkTableFunctionValue : NativeFunctionValue0<TableValue>
	{
		// Token: 0x060070F1 RID: 28913 RVA: 0x001839F1 File Offset: 0x00181BF1
		public LinkTableFunctionValue(Func<TableValue> loader)
			: base(TypeValue.Table)
		{
			this.loader = loader;
		}

		// Token: 0x060070F2 RID: 28914 RVA: 0x00183A05 File Offset: 0x00181C05
		public override TableValue TypedInvoke()
		{
			return this.loader();
		}

		// Token: 0x04003E32 RID: 15922
		private readonly Func<TableValue> loader;
	}
}
