using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200015C RID: 348
	internal abstract class ODataReaderCoreAsync : ODataReaderCore
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x0001CF9D File Offset: 0x0001B19D
		protected ODataReaderCoreAsync(ODataInputContext inputContext, bool readingFeed, IODataReaderWriterListener listener)
			: base(inputContext, readingFeed, listener)
		{
		}
	}
}
