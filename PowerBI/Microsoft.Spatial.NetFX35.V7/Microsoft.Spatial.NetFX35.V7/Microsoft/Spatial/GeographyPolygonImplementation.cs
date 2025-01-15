using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200004F RID: 79
	internal class GeographyPolygonImplementation : GeographyPolygon
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x0000530A File Offset: 0x0000350A
		internal GeographyPolygonImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeographyLineString[] rings)
			: base(coordinateSystem, creator)
		{
			this.rings = rings ?? new GeographyLineString[0];
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00005325 File Offset: 0x00003525
		internal GeographyPolygonImplementation(SpatialImplementation creator, params GeographyLineString[] rings)
			: this(CoordinateSystem.DefaultGeography, creator, rings)
		{
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00005334 File Offset: 0x00003534
		public override bool IsEmpty
		{
			get
			{
				return this.rings.Length == 0;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00005340 File Offset: 0x00003540
		public override ReadOnlyCollection<GeographyLineString> Rings
		{
			get
			{
				return new ReadOnlyCollection<GeographyLineString>(this.rings);
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00005350 File Offset: 0x00003550
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

		// Token: 0x04000059 RID: 89
		private GeographyLineString[] rings;
	}
}
