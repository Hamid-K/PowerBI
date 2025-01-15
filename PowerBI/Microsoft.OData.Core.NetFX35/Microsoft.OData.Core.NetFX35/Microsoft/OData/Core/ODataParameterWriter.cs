using System;

namespace Microsoft.OData.Core
{
	// Token: 0x020000ED RID: 237
	public abstract class ODataParameterWriter
	{
		// Token: 0x060008F9 RID: 2297
		public abstract void WriteStart();

		// Token: 0x060008FA RID: 2298
		public abstract void WriteValue(string parameterName, object parameterValue);

		// Token: 0x060008FB RID: 2299
		public abstract ODataCollectionWriter CreateCollectionWriter(string parameterName);

		// Token: 0x060008FC RID: 2300
		public abstract ODataWriter CreateEntryWriter(string parameterName);

		// Token: 0x060008FD RID: 2301
		public abstract ODataWriter CreateFeedWriter(string parameterName);

		// Token: 0x060008FE RID: 2302
		public abstract void WriteEnd();

		// Token: 0x060008FF RID: 2303
		public abstract void Flush();
	}
}
