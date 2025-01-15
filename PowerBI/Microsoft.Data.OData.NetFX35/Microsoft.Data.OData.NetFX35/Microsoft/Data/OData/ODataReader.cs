using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000159 RID: 345
	public abstract class ODataReader
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000917 RID: 2327
		public abstract ODataReaderState State { get; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000918 RID: 2328
		public abstract ODataItem Item { get; }

		// Token: 0x06000919 RID: 2329
		public abstract bool Read();
	}
}
