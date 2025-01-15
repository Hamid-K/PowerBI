using System;
using System.Collections.Generic;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library.Values;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Core.Evaluation
{
	// Token: 0x02000084 RID: 132
	internal sealed class ODataEdmCollectionValue : EdmValue, IEdmCollectionValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000556 RID: 1366 RVA: 0x0001360D File Offset: 0x0001180D
		internal ODataEdmCollectionValue(ODataCollectionValue collectionValue)
			: base(collectionValue.GetEdmType())
		{
			this.collectionValue = collectionValue;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00013818 File Offset: 0x00011A18
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

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00013835 File Offset: 0x00011A35
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Collection;
			}
		}

		// Token: 0x0400023F RID: 575
		private readonly ODataCollectionValue collectionValue;
	}
}
