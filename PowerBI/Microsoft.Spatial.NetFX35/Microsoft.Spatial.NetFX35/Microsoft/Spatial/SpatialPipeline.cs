using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000034 RID: 52
	public class SpatialPipeline
	{
		// Token: 0x06000162 RID: 354 RVA: 0x000043C5 File Offset: 0x000025C5
		public SpatialPipeline()
		{
			this.startingLink = this;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000043D4 File Offset: 0x000025D4
		public SpatialPipeline(GeographyPipeline geographyPipeline, GeometryPipeline geometryPipeline)
		{
			this.geographyPipeline = geographyPipeline;
			this.geometryPipeline = geometryPipeline;
			this.startingLink = this;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000043F1 File Offset: 0x000025F1
		public virtual GeographyPipeline GeographyPipeline
		{
			get
			{
				return this.geographyPipeline;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000043F9 File Offset: 0x000025F9
		public virtual GeometryPipeline GeometryPipeline
		{
			get
			{
				return this.geometryPipeline;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00004401 File Offset: 0x00002601
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00004409 File Offset: 0x00002609
		public SpatialPipeline StartingLink
		{
			get
			{
				return this.startingLink;
			}
			set
			{
				this.startingLink = value;
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00004412 File Offset: 0x00002612
		[SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "we have DrawGeometry and DrawGeography properties that can be used instead of the implicit cast")]
		public static implicit operator GeographyPipeline(SpatialPipeline spatialPipeline)
		{
			if (spatialPipeline != null)
			{
				return spatialPipeline.GeographyPipeline;
			}
			return null;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000441F File Offset: 0x0000261F
		[SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "we have DrawGeometry and DrawGeography properties that can be used instead of the implicit cast")]
		public static implicit operator GeometryPipeline(SpatialPipeline spatialPipeline)
		{
			if (spatialPipeline != null)
			{
				return spatialPipeline.GeometryPipeline;
			}
			return null;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000442C File Offset: 0x0000262C
		public virtual SpatialPipeline ChainTo(SpatialPipeline destination)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400001D RID: 29
		private GeographyPipeline geographyPipeline;

		// Token: 0x0400001E RID: 30
		private GeometryPipeline geometryPipeline;

		// Token: 0x0400001F RID: 31
		private SpatialPipeline startingLink;
	}
}
