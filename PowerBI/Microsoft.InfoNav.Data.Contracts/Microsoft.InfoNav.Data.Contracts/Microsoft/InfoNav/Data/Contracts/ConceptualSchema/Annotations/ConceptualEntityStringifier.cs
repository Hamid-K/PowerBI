using System;
using System.Globalization;
using Microsoft.InfoNav.Common;
using Microsoft.InfoNav.Utils;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x02000139 RID: 313
	public sealed class ConceptualEntityStringifier : IVertexStringifier<IConceptualEntity>
	{
		// Token: 0x06000817 RID: 2071 RVA: 0x00010C6C File Offset: 0x0000EE6C
		private ConceptualEntityStringifier()
		{
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00010C74 File Offset: 0x0000EE74
		public string VertexToString(IConceptualEntity entity)
		{
			return Convert.ToString(entity, CultureInfo.InvariantCulture).MarkAsCustomerContent();
		}

		// Token: 0x040003BC RID: 956
		public static readonly ConceptualEntityStringifier Instance = new ConceptualEntityStringifier();
	}
}
