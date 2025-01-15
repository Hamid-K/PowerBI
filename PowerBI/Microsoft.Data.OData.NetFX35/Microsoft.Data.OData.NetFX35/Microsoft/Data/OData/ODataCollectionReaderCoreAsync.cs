using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x0200014E RID: 334
	internal abstract class ODataCollectionReaderCoreAsync : ODataCollectionReaderCore
	{
		// Token: 0x060008E4 RID: 2276 RVA: 0x0001C3D4 File Offset: 0x0001A5D4
		protected ODataCollectionReaderCoreAsync(ODataInputContext inputContext, IEdmTypeReference expectedItemTypeReference, IODataReaderWriterListener listener)
			: base(inputContext, expectedItemTypeReference, listener)
		{
		}
	}
}
