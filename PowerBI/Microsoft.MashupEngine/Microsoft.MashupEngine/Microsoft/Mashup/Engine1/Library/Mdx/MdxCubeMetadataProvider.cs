using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x02000980 RID: 2432
	internal abstract class MdxCubeMetadataProvider
	{
		// Token: 0x060045A1 RID: 17825
		public abstract IEnumerable<MdxDimensionMetadata> GetDimensions();

		// Token: 0x060045A2 RID: 17826
		public abstract IEnumerable<MdxMeasureMetadata> GetMeasures();

		// Token: 0x060045A3 RID: 17827
		public abstract IEnumerable<MdxKpiMetadata> GetKPIs();

		// Token: 0x060045A4 RID: 17828
		public abstract IEnumerable<MdxHierarchyMetadata> GetHierarchies();

		// Token: 0x060045A5 RID: 17829
		public abstract IEnumerable<MdxLevelMetadata> GetLevels();

		// Token: 0x060045A6 RID: 17830
		public abstract IEnumerable<MdxMeasureGroupMetadata> GetMeasureGroups();

		// Token: 0x060045A7 RID: 17831
		public abstract IEnumerable<MdxPropertyMetadata> GetProperties(MdxDimension dimension);

		// Token: 0x060045A8 RID: 17832 RVA: 0x000E6755 File Offset: 0x000E4955
		public virtual bool TryGetDefaultMeasure(out string uniqueName)
		{
			uniqueName = null;
			return false;
		}

		// Token: 0x060045A9 RID: 17833 RVA: 0x000EAE54 File Offset: 0x000E9054
		public virtual IEnumerable<MdxCellPropertyMetadata> GetCellProperties()
		{
			yield break;
		}
	}
}
