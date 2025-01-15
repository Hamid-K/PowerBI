using System;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x020000A2 RID: 162
	public abstract class ODataParameterReader
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060006EB RID: 1771
		public abstract ODataParameterReaderState State { get; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060006EC RID: 1772
		public abstract string Name { get; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060006ED RID: 1773
		public abstract object Value { get; }

		// Token: 0x060006EE RID: 1774
		public abstract ODataReader CreateResourceReader();

		// Token: 0x060006EF RID: 1775
		public abstract ODataReader CreateResourceSetReader();

		// Token: 0x060006F0 RID: 1776
		public abstract ODataCollectionReader CreateCollectionReader();

		// Token: 0x060006F1 RID: 1777
		public abstract bool Read();

		// Token: 0x060006F2 RID: 1778
		public abstract Task<bool> ReadAsync();
	}
}
