using System;
using Microsoft.Reporting.QueryDesign.BatchQueries;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000151 RID: 337
	internal class EnsureUniqueUnqualifiedNamesGenerator
	{
		// Token: 0x06000C70 RID: 3184 RVA: 0x0003394F File Offset: 0x00031B4F
		internal static GeneratedTable Generate(GeneratedTable input, bool forceRename)
		{
			return new GeneratedTable(input.QueryTable.EnsureUniqueUnqualifiedNames(forceRename), input.ColumnMap);
		}
	}
}
