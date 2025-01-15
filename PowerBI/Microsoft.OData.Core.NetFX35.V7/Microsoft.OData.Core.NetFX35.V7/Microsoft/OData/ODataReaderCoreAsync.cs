using System;

namespace Microsoft.OData
{
	// Token: 0x02000093 RID: 147
	internal abstract class ODataReaderCoreAsync : ODataReaderCore
	{
		// Token: 0x060005C4 RID: 1476 RVA: 0x0000FD9A File Offset: 0x0000DF9A
		protected ODataReaderCoreAsync(ODataInputContext inputContext, bool readingResourceSet, bool readingDelta, IODataReaderWriterListener listener)
			: base(inputContext, readingResourceSet, readingDelta, listener)
		{
		}
	}
}
