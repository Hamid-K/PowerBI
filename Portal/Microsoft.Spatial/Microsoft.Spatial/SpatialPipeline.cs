using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000040 RID: 64
	public class SpatialPipeline
	{
		// Token: 0x060001DC RID: 476 RVA: 0x00005062 File Offset: 0x00003262
		public SpatialPipeline()
		{
			this.startingLink = this;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00005071 File Offset: 0x00003271
		public SpatialPipeline(GeographyPipeline geographyPipeline, GeometryPipeline geometryPipeline)
		{
			this.geographyPipeline = geographyPipeline;
			this.geometryPipeline = geometryPipeline;
			this.startingLink = this;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000508E File Offset: 0x0000328E
		public virtual GeographyPipeline GeographyPipeline
		{
			get
			{
				return this.geographyPipeline;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00005096 File Offset: 0x00003296
		public virtual GeometryPipeline GeometryPipeline
		{
			get
			{
				return this.geometryPipeline;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000509E File Offset: 0x0000329E
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x000050A6 File Offset: 0x000032A6
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

		// Token: 0x060001E2 RID: 482 RVA: 0x000050AF File Offset: 0x000032AF
		[SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "we have DrawGeometry and DrawGeography properties that can be used instead of the implicit cast")]
		public static implicit operator GeographyPipeline(SpatialPipeline spatialPipeline)
		{
			if (spatialPipeline != null)
			{
				return spatialPipeline.GeographyPipeline;
			}
			return null;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000050BC File Offset: 0x000032BC
		[SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "we have DrawGeometry and DrawGeography properties that can be used instead of the implicit cast")]
		public static implicit operator GeometryPipeline(SpatialPipeline spatialPipeline)
		{
			if (spatialPipeline != null)
			{
				return spatialPipeline.GeometryPipeline;
			}
			return null;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000050C9 File Offset: 0x000032C9
		public virtual SpatialPipeline ChainTo(SpatialPipeline destination)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000046 RID: 70
		private GeographyPipeline geographyPipeline;

		// Token: 0x04000047 RID: 71
		private GeometryPipeline geometryPipeline;

		// Token: 0x04000048 RID: 72
		private SpatialPipeline startingLink;
	}
}
