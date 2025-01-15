using System;

namespace Microsoft.OData.Core
{
	// Token: 0x020000F2 RID: 242
	internal abstract class ODataReaderCoreAsync : ODataReaderCore
	{
		// Token: 0x06000941 RID: 2369 RVA: 0x00021960 File Offset: 0x0001FB60
		protected ODataReaderCoreAsync(ODataInputContext inputContext, bool readingFeed, bool readingDelta, IODataReaderWriterListener listener)
			: base(inputContext, readingFeed, readingDelta, listener)
		{
		}
	}
}
