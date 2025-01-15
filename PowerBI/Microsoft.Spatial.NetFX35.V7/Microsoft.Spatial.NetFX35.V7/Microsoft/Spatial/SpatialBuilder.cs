using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200002F RID: 47
	public class SpatialBuilder : SpatialPipeline, IShapeProvider, IGeographyProvider, IGeometryProvider
	{
		// Token: 0x0600011B RID: 283 RVA: 0x000038E5 File Offset: 0x00001AE5
		public SpatialBuilder(GeographyPipeline geographyInput, GeometryPipeline geometryInput, IGeographyProvider geographyOutput, IGeometryProvider geometryOutput)
			: base(geographyInput, geometryInput)
		{
			this.geographyOutput = geographyOutput;
			this.geometryOutput = geometryOutput;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600011C RID: 284 RVA: 0x000038FE File Offset: 0x00001AFE
		// (remove) Token: 0x0600011D RID: 285 RVA: 0x0000390C File Offset: 0x00001B0C
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
		// (add) Token: 0x0600011E RID: 286 RVA: 0x0000391A File Offset: 0x00001B1A
		// (remove) Token: 0x0600011F RID: 287 RVA: 0x00003928 File Offset: 0x00001B28
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

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00003936 File Offset: 0x00001B36
		public Geography ConstructedGeography
		{
			get
			{
				return this.geographyOutput.ConstructedGeography;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00003943 File Offset: 0x00001B43
		public Geometry ConstructedGeometry
		{
			get
			{
				return this.geometryOutput.ConstructedGeometry;
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003950 File Offset: 0x00001B50
		public static SpatialBuilder Create()
		{
			return SpatialImplementation.CurrentImplementation.CreateBuilder();
		}

		// Token: 0x0400001B RID: 27
		private readonly IGeographyProvider geographyOutput;

		// Token: 0x0400001C RID: 28
		private readonly IGeometryProvider geometryOutput;
	}
}
