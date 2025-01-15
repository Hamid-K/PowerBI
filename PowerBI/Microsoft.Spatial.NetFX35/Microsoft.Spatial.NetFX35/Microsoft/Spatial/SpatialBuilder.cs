using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000035 RID: 53
	public class SpatialBuilder : SpatialPipeline, IShapeProvider, IGeographyProvider, IGeometryProvider
	{
		// Token: 0x0600016B RID: 363 RVA: 0x00004433 File Offset: 0x00002633
		public SpatialBuilder(GeographyPipeline geographyInput, GeometryPipeline geometryInput, IGeographyProvider geographyOutput, IGeometryProvider geometryOutput)
			: base(geographyInput, geometryInput)
		{
			this.geographyOutput = geographyOutput;
			this.geometryOutput = geometryOutput;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600016C RID: 364 RVA: 0x0000444C File Offset: 0x0000264C
		// (remove) Token: 0x0600016D RID: 365 RVA: 0x0000445A File Offset: 0x0000265A
		[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Not following the event-handler pattern")]
		public event Action<Geography> ProduceGeography
		{
			add
			{
				this.geographyOutput.ProduceGeography += value;
			}
			remove
			{
				this.geographyOutput.ProduceGeography -= value;
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600016E RID: 366 RVA: 0x00004468 File Offset: 0x00002668
		// (remove) Token: 0x0600016F RID: 367 RVA: 0x00004476 File Offset: 0x00002676
		[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Not following the event-handler pattern")]
		public event Action<Geometry> ProduceGeometry
		{
			add
			{
				this.geometryOutput.ProduceGeometry += value;
			}
			remove
			{
				this.geometryOutput.ProduceGeometry -= value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00004484 File Offset: 0x00002684
		public Geography ConstructedGeography
		{
			get
			{
				return this.geographyOutput.ConstructedGeography;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00004491 File Offset: 0x00002691
		public Geometry ConstructedGeometry
		{
			get
			{
				return this.geometryOutput.ConstructedGeometry;
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000449E File Offset: 0x0000269E
		public static SpatialBuilder Create()
		{
			return SpatialImplementation.CurrentImplementation.CreateBuilder();
		}

		// Token: 0x04000020 RID: 32
		private readonly IGeographyProvider geographyOutput;

		// Token: 0x04000021 RID: 33
		private readonly IGeometryProvider geometryOutput;
	}
}
