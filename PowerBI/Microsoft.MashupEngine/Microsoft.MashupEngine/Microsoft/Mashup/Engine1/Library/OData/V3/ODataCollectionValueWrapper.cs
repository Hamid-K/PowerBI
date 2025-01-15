using System;
using System.Collections;
using Microsoft.Data.OData;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008CC RID: 2252
	internal class ODataCollectionValueWrapper : IODataCollectionValueWrapper
	{
		// Token: 0x0600405F RID: 16479 RVA: 0x000D6E38 File Offset: 0x000D5038
		public ODataCollectionValueWrapper(ODataCollectionValue collectionValue)
		{
			this.collectionValue = collectionValue;
		}

		// Token: 0x170014BD RID: 5309
		// (get) Token: 0x06004060 RID: 16480 RVA: 0x000D6E47 File Offset: 0x000D5047
		public string TypeName
		{
			get
			{
				return this.collectionValue.TypeName;
			}
		}

		// Token: 0x170014BE RID: 5310
		// (get) Token: 0x06004061 RID: 16481 RVA: 0x000D6E54 File Offset: 0x000D5054
		public IEnumerable Items
		{
			get
			{
				return this.GetItems();
			}
		}

		// Token: 0x06004062 RID: 16482 RVA: 0x000D6E5C File Offset: 0x000D505C
		private IEnumerable GetItems()
		{
			foreach (object obj in this.collectionValue.Items)
			{
				yield return ODataWrapperHelper.WrapValueIfNecessary(obj);
			}
			yield break;
		}

		// Token: 0x040021D0 RID: 8656
		private readonly ODataCollectionValue collectionValue;
	}
}
