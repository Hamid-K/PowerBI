using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x020000BB RID: 187
	internal abstract class ODataCollectionReaderCoreAsync : ODataCollectionReaderCore
	{
		// Token: 0x060006BD RID: 1725 RVA: 0x000181F6 File Offset: 0x000163F6
		protected ODataCollectionReaderCoreAsync(ODataInputContext inputContext, IEdmTypeReference expectedItemTypeReference, IODataReaderWriterListener listener)
			: base(inputContext, expectedItemTypeReference, listener)
		{
		}
	}
}
