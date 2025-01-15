using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library.Values;
using Microsoft.Data.Edm.Values;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x0200017D RID: 381
	internal sealed class ODataEdmCollectionValue : EdmValue, IEdmCollectionValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000A76 RID: 2678 RVA: 0x000231DB File Offset: 0x000213DB
		internal ODataEdmCollectionValue(ODataCollectionValue collectionValue)
			: base(collectionValue.GetEdmType())
		{
			this.collectionValue = collectionValue;
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x000233E4 File Offset: 0x000215E4
		public IEnumerable<IEdmDelayedValue> Elements
		{
			get
			{
				if (this.collectionValue != null)
				{
					IEdmTypeReference itemType = ((base.Type == null) ? null : (base.Type.Definition as IEdmCollectionType).ElementType);
					foreach (object collectionItem in this.collectionValue.Items)
					{
						yield return ODataEdmValueUtils.ConvertValue(collectionItem, itemType);
					}
				}
				yield break;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00023401 File Offset: 0x00021601
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Collection;
			}
		}

		// Token: 0x040003FD RID: 1021
		private readonly ODataCollectionValue collectionValue;
	}
}
