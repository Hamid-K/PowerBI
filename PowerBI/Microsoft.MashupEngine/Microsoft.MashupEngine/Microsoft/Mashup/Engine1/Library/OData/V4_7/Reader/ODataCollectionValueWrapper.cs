using System;
using System.Collections;
using Microsoft.OData;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader
{
	// Token: 0x02000799 RID: 1945
	internal class ODataCollectionValueWrapper : IODataCollectionValueWrapper
	{
		// Token: 0x06003901 RID: 14593 RVA: 0x000B79E0 File Offset: 0x000B5BE0
		public ODataCollectionValueWrapper(ODataCollectionValue collectionValue)
		{
			this.collectionValue = collectionValue;
		}

		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x06003902 RID: 14594 RVA: 0x000B79EF File Offset: 0x000B5BEF
		public string TypeName
		{
			get
			{
				return this.collectionValue.TypeName;
			}
		}

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x06003903 RID: 14595 RVA: 0x000B79FC File Offset: 0x000B5BFC
		public IEnumerable Items
		{
			get
			{
				return this.GetItems();
			}
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x000B7A04 File Offset: 0x000B5C04
		private IEnumerable GetItems()
		{
			foreach (object obj in this.collectionValue.Items)
			{
				yield return ODataWrapperHelper.WrapValueIfNecessary(obj);
			}
			yield break;
		}

		// Token: 0x04001D67 RID: 7527
		private readonly ODataCollectionValue collectionValue;
	}
}
