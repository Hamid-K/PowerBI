using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000040 RID: 64
	internal abstract class ODataCollectionReaderCoreAsync : ODataCollectionReaderCore
	{
		// Token: 0x06000217 RID: 535 RVA: 0x00008929 File Offset: 0x00006B29
		protected ODataCollectionReaderCoreAsync(ODataInputContext inputContext, IEdmTypeReference expectedItemTypeReference, IODataReaderWriterListener listener)
			: base(inputContext, expectedItemTypeReference, listener)
		{
		}
	}
}
