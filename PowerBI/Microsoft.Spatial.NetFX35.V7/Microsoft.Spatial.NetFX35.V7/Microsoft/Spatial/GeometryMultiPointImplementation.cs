using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000055 RID: 85
	internal class GeometryMultiPointImplementation : GeometryMultiPoint
	{
		// Token: 0x0600020F RID: 527 RVA: 0x000055F2 File Offset: 0x000037F2
		internal GeometryMultiPointImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeometryPoint[] points)
			: base(coordinateSystem, creator)
		{
			this.points = points ?? new GeometryPoint[0];
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000560D File Offset: 0x0000380D
		internal GeometryMultiPointImplementation(SpatialImplementation creator, params GeometryPoint[] points)
			: this(CoordinateSystem.DefaultGeometry, creator, points)
		{
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000561C File Offset: 0x0000381C
		public override bool IsEmpty
		{
			get
			{
				return this.points.Length == 0;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00005628 File Offset: 0x00003828
		public override ReadOnlyCollection<Geometry> Geometries
		{
			get
			{
				return new ReadOnlyCollection<Geometry>(this.points);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00005635 File Offset: 0x00003835
		public override ReadOnlyCollection<GeometryPoint> Points
		{
			get
			{
				return new ReadOnlyCollection<GeometryPoint>(this.points);
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00005644 File Offset: 0x00003844
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

		// Token: 0x0400005D RID: 93
		private GeometryPoint[] points;
	}
}
