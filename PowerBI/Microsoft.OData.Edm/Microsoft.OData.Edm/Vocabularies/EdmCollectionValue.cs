using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000124 RID: 292
	public class EdmCollectionValue : EdmValue, IEdmCollectionValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000797 RID: 1943 RVA: 0x00012183 File Offset: 0x00010383
		public EdmCollectionValue(IEdmCollectionTypeReference type, IEnumerable<IEdmDelayedValue> elements)
			: base(type)
		{
			this.elements = elements;
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x00012193 File Offset: 0x00010393
		public IEnumerable<IEdmDelayedValue> Elements
		{
			get
			{
				return this.elements;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x0000268B File Offset: 0x0000088B
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Collection;
			}
		}

		// Token: 0x0400032A RID: 810
		private readonly IEnumerable<IEdmDelayedValue> elements;
	}
}
