using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000054 RID: 84
	internal class GeographyPolygonImplementation : GeographyPolygon
	{
		// Token: 0x0600026B RID: 619 RVA: 0x00005FD2 File Offset: 0x000041D2
		internal GeographyPolygonImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeographyLineString[] rings)
			: base(coordinateSystem, creator)
		{
			this.rings = rings ?? new GeographyLineString[0];
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00005FED File Offset: 0x000041ED
		internal GeographyPolygonImplementation(SpatialImplementation creator, params GeographyLineString[] rings)
			: this(CoordinateSystem.DefaultGeography, creator, rings)
		{
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00005FFC File Offset: 0x000041FC
		public override bool IsEmpty
		{
			get
			{
				return this.rings.Length == 0;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00006008 File Offset: 0x00004208
		public override ReadOnlyCollection<GeographyLineString> Rings
		{
			get
			{
				return new ReadOnlyCollection<GeographyLineString>(this.rings);
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00006018 File Offset: 0x00004218
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeographyPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeography(SpatialType.Polygon);
			for (int i = 0; i < this.rings.Length; i++)
			{
				this.rings[i].SendFigure(pipeline);
			}
			pipeline.EndGeography();
		}

		// Token: 0x04000066 RID: 102
		private GeographyLineString[] rings;
	}
}
