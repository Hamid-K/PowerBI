using System;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning
{
	// Token: 0x02000126 RID: 294
	internal static class CommonDataSetPlanningUtils
	{
		// Token: 0x06000B1E RID: 2846 RVA: 0x0002B78C File Offset: 0x0002998C
		internal static DataTransformTable GetDataTransformInputTable(DataShape dataShape)
		{
			if (dataShape.Transforms.IsNullOrEmpty<DataTransform>())
			{
				return null;
			}
			return dataShape.Transforms[0].Input.Table;
		}
	}
}
