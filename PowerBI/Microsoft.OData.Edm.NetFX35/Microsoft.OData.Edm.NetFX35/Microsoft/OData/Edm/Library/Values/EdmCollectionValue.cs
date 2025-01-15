using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library.Values
{
	// Token: 0x020001DA RID: 474
	public class EdmCollectionValue : EdmValue, IEdmCollectionValue, IEdmValue, IEdmElement
	{
		// Token: 0x060009E4 RID: 2532 RVA: 0x00019C00 File Offset: 0x00017E00
		public EdmCollectionValue(IEdmCollectionTypeReference type, IEnumerable<IEdmDelayedValue> elements)
			: base(type)
		{
			this.elements = elements;
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00019C10 File Offset: 0x00017E10
		public IEnumerable<IEdmDelayedValue> Elements
		{
			get
			{
				return this.elements;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00019C18 File Offset: 0x00017E18
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Collection;
			}
		}

		// Token: 0x040004CA RID: 1226
		private readonly IEnumerable<IEdmDelayedValue> elements;
	}
}
