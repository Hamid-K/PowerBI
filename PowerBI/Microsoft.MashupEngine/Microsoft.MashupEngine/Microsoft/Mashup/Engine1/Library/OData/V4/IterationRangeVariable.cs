using System;
using System.Globalization;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000856 RID: 2134
	internal sealed class IterationRangeVariable
	{
		// Token: 0x06003D70 RID: 15728 RVA: 0x000C7AC2 File Offset: 0x000C5CC2
		public IterationRangeVariable()
		{
			this.count = -1;
		}

		// Token: 0x06003D71 RID: 15729 RVA: 0x000C7AD1 File Offset: 0x000C5CD1
		public EntityRangeVariable New(Microsoft.OData.Edm.IEdmEntityTypeReference typeReference, CollectionNavigationNode navigationNode)
		{
			this.count++;
			return new EntityRangeVariable("e" + this.count.ToString(CultureInfo.InvariantCulture), typeReference, navigationNode);
		}

		// Token: 0x04002029 RID: 8233
		private int count;
	}
}
