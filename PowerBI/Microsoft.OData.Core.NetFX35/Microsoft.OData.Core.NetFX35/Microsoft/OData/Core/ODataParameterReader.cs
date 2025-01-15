using System;

namespace Microsoft.OData.Core
{
	// Token: 0x020000E7 RID: 231
	public abstract class ODataParameterReader
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060008C5 RID: 2245
		public abstract ODataParameterReaderState State { get; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060008C6 RID: 2246
		public abstract string Name { get; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060008C7 RID: 2247
		public abstract object Value { get; }

		// Token: 0x060008C8 RID: 2248
		public abstract ODataReader CreateEntryReader();

		// Token: 0x060008C9 RID: 2249
		public abstract ODataReader CreateFeedReader();

		// Token: 0x060008CA RID: 2250
		public abstract ODataCollectionReader CreateCollectionReader();

		// Token: 0x060008CB RID: 2251
		public abstract bool Read();
	}
}
