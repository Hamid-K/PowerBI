using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000053 RID: 83
	internal class GeometryLineStringImplementation : GeometryLineString
	{
		// Token: 0x06000204 RID: 516 RVA: 0x00005500 File Offset: 0x00003700
		internal GeometryLineStringImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeometryPoint[] points)
			: base(coordinateSystem, creator)
		{
			this.points = points ?? new GeometryPoint[0];
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000551B File Offset: 0x0000371B
		internal GeometryLineStringImplementation(SpatialImplementation creator, params GeometryPoint[] points)
			: this(CoordinateSystem.DefaultGeometry, creator, points)
		{
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000552A File Offset: 0x0000372A
		public override bool IsEmpty
		{
			get
			{
				return this.points.Length == 0;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00005536 File Offset: 0x00003736
		public override ReadOnlyCollection<GeometryPoint> Points
		{
			get
			{
				return new ReadOnlyCollection<GeometryPoint>(this.points);
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00005543 File Offset: 0x00003743
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeometryPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeometry(SpatialType.LineString);
			this.SendFigure(pipeline);
			pipeline.EndGeometry();
		}

		// Token: 0x0400005B RID: 91
		private GeometryPoint[] points;
	}
}
