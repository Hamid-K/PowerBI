using System;
using System.Collections;
using Microsoft.OData.Core;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000874 RID: 2164
	internal class ODataCollectionValueWrapper : IODataCollectionValueWrapper
	{
		// Token: 0x06003E43 RID: 15939 RVA: 0x000CB809 File Offset: 0x000C9A09
		public ODataCollectionValueWrapper(ODataCollectionValue collectionValue)
		{
			this.collectionValue = collectionValue;
		}

		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x06003E44 RID: 15940 RVA: 0x000CB818 File Offset: 0x000C9A18
		public string TypeName
		{
			get
			{
				return this.collectionValue.TypeName;
			}
		}

		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x06003E45 RID: 15941 RVA: 0x000CB825 File Offset: 0x000C9A25
		public IEnumerable Items
		{
			get
			{
				return this.GetItems();
			}
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x000CB82D File Offset: 0x000C9A2D
		private IEnumerable GetItems()
		{
			foreach (object obj in this.collectionValue.Items)
			{
				yield return ODataWrapperHelper.WrapValueIfNecessary(obj);
			}
			yield break;
		}

		// Token: 0x040020BF RID: 8383
		private readonly ODataCollectionValue collectionValue;
	}
}
