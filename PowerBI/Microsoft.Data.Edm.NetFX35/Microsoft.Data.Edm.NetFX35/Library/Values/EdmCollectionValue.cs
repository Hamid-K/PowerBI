using System;
using System.Collections.Generic;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Library.Values
{
	// Token: 0x020001A9 RID: 425
	public class EdmCollectionValue : EdmValue, IEdmCollectionValue, IEdmValue, IEdmElement
	{
		// Token: 0x0600092D RID: 2349 RVA: 0x00018964 File Offset: 0x00016B64
		public EdmCollectionValue(IEdmCollectionTypeReference type, IEnumerable<IEdmDelayedValue> elements)
			: base(type)
		{
			this.elements = elements;
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00018974 File Offset: 0x00016B74
		public IEnumerable<IEdmDelayedValue> Elements
		{
			get
			{
				return this.elements;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0001897C File Offset: 0x00016B7C
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Collection;
			}
		}

		// Token: 0x04000477 RID: 1143
		private readonly IEnumerable<IEdmDelayedValue> elements;
	}
}
