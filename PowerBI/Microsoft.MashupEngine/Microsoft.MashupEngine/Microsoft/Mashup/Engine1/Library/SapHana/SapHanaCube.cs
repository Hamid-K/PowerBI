using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Odbc;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000421 RID: 1057
	internal sealed class SapHanaCube : SapHanaCubeBase, ICube
	{
		// Token: 0x060023FC RID: 9212 RVA: 0x000655D4 File Offset: 0x000637D4
		public SapHanaCube(IResource resource, SapHanaOdbcDataSource dataSource, OdbcDataSourceInfo dataSourceInfo, string packageName, string name, bool hasParameters, bool useHierarchies, SapHanaViewType viewType)
			: base(resource, dataSource, dataSourceInfo, packageName, "_SYS_BIC", name, hasParameters, useHierarchies, viewType)
		{
			this.viewName = this.catalogName + "/" + name;
			this.columns = dataSource.GetOrCreateTableInfo(new OdbcIdentifier(null, "_SYS_BIC", this.viewName), null).Columns;
		}

		// Token: 0x04000E81 RID: 3713
		private const string sysBic = "_SYS_BIC";
	}
}
