using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000C2 RID: 194
	internal class SpatialIndexingSchemeTypeHelper : OptionsHelper<SpatialIndexingSchemeType>
	{
		// Token: 0x060002DE RID: 734 RVA: 0x0000CA6A File Offset: 0x0000AC6A
		private SpatialIndexingSchemeTypeHelper()
		{
			base.AddOptionMapping(SpatialIndexingSchemeType.GeographyGrid, "GEOGRAPHY_GRID");
			base.AddOptionMapping(SpatialIndexingSchemeType.GeometryGrid, "GEOMETRY_GRID");
			base.AddOptionMapping(SpatialIndexingSchemeType.GeographyAutoGrid, "GEOGRAPHY_AUTO_GRID", SqlVersionFlags.TSql110);
			base.AddOptionMapping(SpatialIndexingSchemeType.GeometryAutoGrid, "GEOMETRY_AUTO_GRID", SqlVersionFlags.TSql110);
		}

		// Token: 0x040005D1 RID: 1489
		internal static readonly SpatialIndexingSchemeTypeHelper Instance = new SpatialIndexingSchemeTypeHelper();
	}
}
