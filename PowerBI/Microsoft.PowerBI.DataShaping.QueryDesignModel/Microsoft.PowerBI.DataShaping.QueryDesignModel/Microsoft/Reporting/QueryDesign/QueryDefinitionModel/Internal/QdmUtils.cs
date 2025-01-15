using System;
using System.Linq;
using Microsoft.InfoNav;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x02000104 RID: 260
	public static class QdmUtils
	{
		// Token: 0x06000EE8 RID: 3816 RVA: 0x0002877B File Offset: 0x0002697B
		public static bool SupportsTreatAs(this IConceptualColumn column)
		{
			return column.Grouping.Identity == GroupingIdentity.Value || column.Grouping.IdentityColumns.Contains(column);
		}
	}
}
