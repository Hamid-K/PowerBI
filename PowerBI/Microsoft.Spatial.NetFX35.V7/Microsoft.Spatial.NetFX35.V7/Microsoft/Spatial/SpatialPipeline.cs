using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200003B RID: 59
	public class SpatialPipeline
	{
		// Token: 0x06000166 RID: 358 RVA: 0x0000438E File Offset: 0x0000258E
		public SpatialPipeline()
		{
			this.startingLink = this;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000439D File Offset: 0x0000259D
		public SpatialPipeline(GeographyPipeline geographyPipeline, GeometryPipeline geometryPipeline)
		{
			this.geographyPipeline = geographyPipeline;
			this.geometryPipeline = geometryPipeline;
			this.startingLink = this;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000043BA File Offset: 0x000025BA
		public virtual GeographyPipeline GeographyPipeline
		{
			get
			{
				return this.geographyPipeline;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000043C2 File Offset: 0x000025C2
		public virtual GeometryPipeline GeometryPipeline
		{
			get
			{
				return this.geometryPipeline;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000043CA File Offset: 0x000025CA
		// (set) Token: 0x0600016B RID: 363 RVA: 0x000043D2 File Offset: 0x000025D2
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

		// Token: 0x0600016C RID: 364 RVA: 0x000043DB File Offset: 0x000025DB
		[SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "we have DrawGeometry and DrawGeography properties that can be used instead of the implicit cast")]
		public static implicit operator GeographyPipeline(SpatialPipeline spatialPipeline)
		{
			if (spatialPipeline != null)
			{
				return spatialPipeline.GeographyPipeline;
			}
			return null;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000043E8 File Offset: 0x000025E8
		[SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "we have DrawGeometry and DrawGeography properties that can be used instead of the implicit cast")]
		public static implicit operator GeometryPipeline(SpatialPipeline spatialPipeline)
		{
			if (spatialPipeline != null)
			{
				return spatialPipeline.GeometryPipeline;
			}
			return null;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000043F5 File Offset: 0x000025F5
		public virtual SpatialPipeline ChainTo(SpatialPipeline destination)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000039 RID: 57
		private GeographyPipeline geographyPipeline;

		// Token: 0x0400003A RID: 58
		private GeometryPipeline geometryPipeline;

		// Token: 0x0400003B RID: 59
		private SpatialPipeline startingLink;
	}
}
