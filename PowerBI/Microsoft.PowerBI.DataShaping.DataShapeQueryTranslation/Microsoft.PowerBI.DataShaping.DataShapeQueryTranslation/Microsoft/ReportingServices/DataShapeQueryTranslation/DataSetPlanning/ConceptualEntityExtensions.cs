using System;
using System.Linq;
using Microsoft.InfoNav;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000D5 RID: 213
	internal static class ConceptualEntityExtensions
	{
		// Token: 0x060008DE RID: 2270 RVA: 0x00022748 File Offset: 0x00020948
		internal static bool HasStableKeys(IConceptualEntity entity)
		{
			if (entity.KeyColumns.Count > 0)
			{
				return entity.KeyColumns.All((IConceptualColumn p) => p.IsStable);
			}
			return false;
		}
	}
}
