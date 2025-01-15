using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200005E RID: 94
	internal class GeometryMultiPointImplementation : GeometryMultiPoint
	{
		// Token: 0x06000268 RID: 616 RVA: 0x0000694A File Offset: 0x00004B4A
		internal GeometryMultiPointImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeometryPoint[] points)
			: base(coordinateSystem, creator)
		{
			this.points = points ?? new GeometryPoint[0];
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00006965 File Offset: 0x00004B65
		internal GeometryMultiPointImplementation(SpatialImplementation creator, params GeometryPoint[] points)
			: this(CoordinateSystem.DefaultGeometry, creator, points)
		{
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00006974 File Offset: 0x00004B74
		public override bool IsEmpty
		{
			get
			{
				return this.points.Length == 0;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00006981 File Offset: 0x00004B81
		public override ReadOnlyCollection<Geometry> Geometries
		{
			get
			{
				return new ReadOnlyCollection<Geometry>(this.points);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000698E File Offset: 0x00004B8E
		public override ReadOnlyCollection<GeometryPoint> Points
		{
			get
			{
				return new ReadOnlyCollection<GeometryPoint>(this.points);
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000699C File Offset: 0x00004B9C
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeometryPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeometry(SpatialType.MultiPoint);
			for (int i = 0; i < this.points.Length; i++)
			{
				this.points[i].SendTo(pipeline);
			}
			pipeline.EndGeometry();
		}

		// Token: 0x04000073 RID: 115
		private GeometryPoint[] points;
	}
}
