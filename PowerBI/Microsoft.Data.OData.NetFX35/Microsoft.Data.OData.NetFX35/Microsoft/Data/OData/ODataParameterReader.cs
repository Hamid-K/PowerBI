using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000152 RID: 338
	public abstract class ODataParameterReader
	{
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060008EF RID: 2287
		public abstract ODataParameterReaderState State { get; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060008F0 RID: 2288
		public abstract string Name { get; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060008F1 RID: 2289
		public abstract object Value { get; }

		// Token: 0x060008F2 RID: 2290
		public abstract ODataCollectionReader CreateCollectionReader();

		// Token: 0x060008F3 RID: 2291
		public abstract bool Read();
	}
}
