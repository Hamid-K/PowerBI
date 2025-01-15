using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000117 RID: 279
	public class EdmCollectionValue : EdmValue, IEdmCollectionValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000757 RID: 1879 RVA: 0x00013C9F File Offset: 0x00011E9F
		public EdmCollectionValue(IEdmCollectionTypeReference type, IEnumerable<IEdmDelayedValue> elements)
			: base(type)
		{
			this.elements = elements;
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x00013CAF File Offset: 0x00011EAF
		public IEnumerable<IEdmDelayedValue> Elements
		{
			get
			{
				return this.elements;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x00009097 File Offset: 0x00007297
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Collection;
			}
		}

		// Token: 0x04000425 RID: 1061
		private readonly IEnumerable<IEdmDelayedValue> elements;
	}
}
