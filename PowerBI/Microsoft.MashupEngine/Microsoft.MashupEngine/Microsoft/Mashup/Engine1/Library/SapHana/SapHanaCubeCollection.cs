using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000423 RID: 1059
	internal sealed class SapHanaCubeCollection : SapHanaCubeCollectionBase, IEnumerable<SapHanaCubeBase>, IEnumerable
	{
		// Token: 0x06002415 RID: 9237 RVA: 0x00065D8F File Offset: 0x00063F8F
		public SapHanaCubeCollection(SapHanaOdbcDataSource dataSource, string cubesTableName, SapHanaCatalog catalog, bool useHierarchies)
			: base(dataSource, cubesTableName, catalog, useHierarchies)
		{
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06002416 RID: 9238 RVA: 0x00065D9C File Offset: 0x00063F9C
		protected override string CubesQuery
		{
			get
			{
				return "select _CUBE.CUBE_NAME as CUBE_NAME,\r\n         VIEW_TYPE,\r\n        _PARAMETERS.COUNT as PARAMETER_COUNT,\r\n        _CUBE.SCHEMA_NAME as SCHEMA_NAME\r\nfrom (\r\n    select distinct CATALOG_NAME, CUBE_NAME, SCHEMA_NAME, (CATALOG_NAME || '/' || CUBE_NAME) as VIEW_NAME\r\n    from _SYS_BI.{0}\r\n    where CATALOG_NAME = ?) as _CUBE\r\ninner join (\r\n    select VIEW_NAME, VIEW_TYPE\r\n    from SYS.VIEWS) as _VIEW\r\non _CUBE.VIEW_NAME = _VIEW.VIEW_NAME\r\nleft outer join (\r\n    select CATALOG_NAME, CUBE_NAME, count(*) as COUNT\r\n    from  _SYS_BI.BIMC_VARIABLE_VIEW\r\n    GROUP BY CATALOG_NAME, CUBE_NAME\r\n) as _PARAMETERS\r\non _PARAMETERS.CATALOG_NAME = _CUBE.CATALOG_NAME and _PARAMETERS.CUBE_NAME = _CUBE.CUBE_NAME";
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06002417 RID: 9239 RVA: 0x00065DA3 File Offset: 0x00063FA3
		protected override string[] CubesQueryColumns
		{
			get
			{
				return new string[] { "CUBE_NAME", "VIEW_TYPE", "PARAMETER_COUNT", "SCHEMA_NAME" };
			}
		}
	}
}
