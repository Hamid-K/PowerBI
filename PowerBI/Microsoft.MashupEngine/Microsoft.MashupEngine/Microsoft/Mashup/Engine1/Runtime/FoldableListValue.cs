using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001305 RID: 4869
	internal abstract class FoldableListValue : ListValue
	{
		// Token: 0x060080AE RID: 32942
		public abstract Value Aggregate(FunctionValue aggregate);

		// Token: 0x060080AF RID: 32943
		public abstract ListValue Select(FunctionValue selection);

		// Token: 0x060080B0 RID: 32944
		public abstract ListValue Distinct();

		// Token: 0x060080B1 RID: 32945
		public abstract ListValue Sort(bool ascending);

		// Token: 0x060080B2 RID: 32946
		public abstract ListValue Transform(FunctionValue transform);

		// Token: 0x060080B3 RID: 32947
		public abstract ListValue Skip(RowCount count);

		// Token: 0x060080B4 RID: 32948
		public abstract ListValue Take(RowCount count);
	}
}
