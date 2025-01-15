using System;
using Microsoft.InfoNav.DataShapeQueryGeneration.Resolution;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000C8 RID: 200
	internal sealed class ResolvedSemanticQueryDataShapeCommand
	{
		// Token: 0x06000740 RID: 1856 RVA: 0x0001BB78 File Offset: 0x00019D78
		internal ResolvedSemanticQueryDataShapeCommand(ResolvedSemanticQueryDataShape queryDataShape, QueryExtensionSchemaContext extension, string dataSourceVariables)
		{
			this.QueryDataShape = queryDataShape;
			this.Extension = extension;
			this.DataSourceVariables = dataSourceVariables;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x0001BB95 File Offset: 0x00019D95
		internal ResolvedSemanticQueryDataShape QueryDataShape { get; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001BB9D File Offset: 0x00019D9D
		internal QueryExtensionSchemaContext Extension { get; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x0001BBA5 File Offset: 0x00019DA5
		internal string DataSourceVariables { get; }

		// Token: 0x06000744 RID: 1860 RVA: 0x0001BBAD File Offset: 0x00019DAD
		internal ResolvedSemanticQueryDataShapeCommand Clone(ResolvedSemanticQueryDataShape newQueryDataShape = null, QueryExtensionSchemaContext newExtensionSchema = null)
		{
			return new ResolvedSemanticQueryDataShapeCommand(newQueryDataShape ?? this.QueryDataShape, newExtensionSchema ?? this.Extension, this.DataSourceVariables);
		}
	}
}
