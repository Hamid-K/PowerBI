using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000446 RID: 1094
	internal sealed class SapHanaHDICube : SapHanaCubeBase, ICube
	{
		// Token: 0x06002504 RID: 9476 RVA: 0x0006A1F0 File Offset: 0x000683F0
		public SapHanaHDICube(IResource resource, SapHanaOdbcDataSource dataSource, OdbcDataSourceInfo dataSourceInfo, string catalogName, string schemaName, string name, bool hasParameters, bool useHierarchies, string qualifiedName, SapHanaViewType viewType)
			: base(resource, dataSource, dataSourceInfo, catalogName, schemaName, name, hasParameters, useHierarchies, viewType)
		{
			if (string.IsNullOrEmpty(qualifiedName))
			{
				throw ValueException.NewParameterError<Message1>(Strings.SapHanaHDICubeError(name), Value.Null);
			}
			this.viewName = qualifiedName;
			this.columns = dataSource.GetOrCreateTableInfo(new OdbcIdentifier(null, this.schemaName, this.viewName), null).Columns;
		}
	}
}
