using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000033 RID: 51
	public class SpatialBuilder : SpatialPipeline, IShapeProvider, IGeographyProvider, IGeometryProvider
	{
		// Token: 0x06000186 RID: 390 RVA: 0x000043EB File Offset: 0x000025EB
		public SpatialBuilder(GeographyPipeline geographyInput, GeometryPipeline geometryInput, IGeographyProvider geographyOutput, IGeometryProvider geometryOutput)
			: base(geographyInput, geometryInput)
		{
			this.geographyOutput = geographyOutput;
			this.geometryOutput = geometryOutput;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000187 RID: 391 RVA: 0x00004404 File Offset: 0x00002604
		// (remove) Token: 0x06000188 RID: 392 RVA: 0x00004412 File Offset: 0x00002612
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
		// (add) Token: 0x06000189 RID: 393 RVA: 0x00004420 File Offset: 0x00002620
		// (remove) Token: 0x0600018A RID: 394 RVA: 0x0000442E File Offset: 0x0000262E
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

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000443C File Offset: 0x0000263C
		public Geography ConstructedGeography
		{
			get
			{
				return this.geographyOutput.ConstructedGeography;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00004449 File Offset: 0x00002649
		public Geometry ConstructedGeometry
		{
			get
			{
				return this.geometryOutput.ConstructedGeometry;
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00004456 File Offset: 0x00002656
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
