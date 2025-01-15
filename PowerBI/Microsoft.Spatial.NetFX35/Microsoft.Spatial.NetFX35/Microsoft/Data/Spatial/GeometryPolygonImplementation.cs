using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000061 RID: 97
	internal class GeometryPolygonImplementation : GeometryPolygon
	{
		// Token: 0x0600027C RID: 636 RVA: 0x00006BBE File Offset: 0x00004DBE
		internal GeometryPolygonImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeometryLineString[] rings)
			: base(coordinateSystem, creator)
		{
			this.rings = rings ?? new GeometryLineString[0];
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00006BD9 File Offset: 0x00004DD9
		internal GeometryPolygonImplementation(SpatialImplementation creator, params GeometryLineString[] rings)
			: this(CoordinateSystem.DefaultGeometry, creator, rings)
		{
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00006BE8 File Offset: 0x00004DE8
		public override bool IsEmpty
		{
			get
			{
				return this.rings.Length == 0;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00006BF5 File Offset: 0x00004DF5
		public override ReadOnlyCollection<GeometryLineString> Rings
		{
			get
			{
				return new ReadOnlyCollection<GeometryLineString>(this.rings);
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00006C04 File Offset: 0x00004E04
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeometryPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeometry(SpatialType.Polygon);
			for (int i = 0; i < this.rings.Length; i++)
			{
				this.rings[i].SendFigure(pipeline);
			}
			pipeline.EndGeometry();
		}

		// Token: 0x04000079 RID: 121
		private GeometryLineString[] rings;
	}
}
