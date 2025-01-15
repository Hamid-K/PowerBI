using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000445 RID: 1093
	internal sealed class SapHanaHDICapableCubeCollection : SapHanaCubeCollectionBase, IEnumerable<SapHanaCubeBase>, IEnumerable
	{
		// Token: 0x06002501 RID: 9473 RVA: 0x00065D8F File Offset: 0x00063F8F
		public SapHanaHDICapableCubeCollection(SapHanaOdbcDataSource dataSource, string cubesTableName, SapHanaCatalog catalog, bool useHierarchies)
			: base(dataSource, cubesTableName, catalog, useHierarchies)
		{
		}

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06002502 RID: 9474 RVA: 0x0006A1B6 File Offset: 0x000683B6
		protected override string CubesQuery
		{
			get
			{
				return "select _CUBE.CUBE_NAME as CUBE_NAME,\r\n         VIEW_TYPE,\r\n        _PARAMETERS.COUNT as PARAMETER_COUNT,\r\n        _CUBE.SCHEMA_NAME as SCHEMA_NAME,\r\n        _CUBE.QUALIFIED_NAME as QUALIFIED_NAME\r\nfrom (\r\n    select distinct CATALOG_NAME, CUBE_NAME, SCHEMA_NAME, (CATALOG_NAME || '/' || CUBE_NAME) as VIEW_NAME, QUALIFIED_NAME\r\n    from _SYS_BI.{0}\r\n    where CATALOG_NAME = ?) as _CUBE\r\ninner join (\r\n    select VIEW_NAME, VIEW_TYPE\r\n    from SYS.VIEWS) as _VIEW\r\non _CUBE.VIEW_NAME = _VIEW.VIEW_NAME or _CUBE.QUALIFIED_NAME = _VIEW.VIEW_NAME\r\nleft outer join (\r\n    select CATALOG_NAME, CUBE_NAME, count(*) as COUNT\r\n    from  _SYS_BI.BIMC_VARIABLE_VIEW\r\n    GROUP BY CATALOG_NAME, CUBE_NAME\r\n) as _PARAMETERS\r\non _PARAMETERS.CATALOG_NAME = _CUBE.CATALOG_NAME and _PARAMETERS.CUBE_NAME = _CUBE.CUBE_NAME";
			}
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06002503 RID: 9475 RVA: 0x0006A1BD File Offset: 0x000683BD
		protected override string[] CubesQueryColumns
		{
			get
			{
				return new string[] { "CUBE_NAME", "VIEW_TYPE", "PARAMETER_COUNT", "SCHEMA_NAME", "QUALIFIED_NAME" };
			}
		}
	}
}
