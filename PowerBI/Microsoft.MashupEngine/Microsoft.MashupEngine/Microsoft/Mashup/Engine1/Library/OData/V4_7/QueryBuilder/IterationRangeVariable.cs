using System;
using System.Globalization;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder
{
	// Token: 0x020007C5 RID: 1989
	internal sealed class IterationRangeVariable
	{
		// Token: 0x060039D8 RID: 14808 RVA: 0x000BAB70 File Offset: 0x000B8D70
		public IterationRangeVariable()
		{
			this.count = -1;
		}

		// Token: 0x060039D9 RID: 14809 RVA: 0x000BAB7F File Offset: 0x000B8D7F
		public ResourceRangeVariable New(Microsoft.OData.Edm.IEdmEntityTypeReference typeReference, CollectionNavigationNode navigationNode)
		{
			this.count++;
			return new ResourceRangeVariable("e" + this.count.ToString(CultureInfo.InvariantCulture), typeReference, navigationNode);
		}

		// Token: 0x04001DFA RID: 7674
		private int count;
	}
}
