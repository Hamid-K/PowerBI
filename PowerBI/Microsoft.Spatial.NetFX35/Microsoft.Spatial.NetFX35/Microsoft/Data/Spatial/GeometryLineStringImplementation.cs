using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200005C RID: 92
	internal class GeometryLineStringImplementation : GeometryLineString
	{
		// Token: 0x0600025D RID: 605 RVA: 0x00006854 File Offset: 0x00004A54
		internal GeometryLineStringImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeometryPoint[] points)
			: base(coordinateSystem, creator)
		{
			this.points = points ?? new GeometryPoint[0];
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000686F File Offset: 0x00004A6F
		internal GeometryLineStringImplementation(SpatialImplementation creator, params GeometryPoint[] points)
			: this(CoordinateSystem.DefaultGeometry, creator, points)
		{
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000687E File Offset: 0x00004A7E
		public override bool IsEmpty
		{
			get
			{
				return this.points.Length == 0;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000688B File Offset: 0x00004A8B
		public override ReadOnlyCollection<GeometryPoint> Points
		{
			get
			{
				return new ReadOnlyCollection<GeometryPoint>(this.points);
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00006898 File Offset: 0x00004A98
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeometryPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeometry(SpatialType.LineString);
			this.SendFigure(pipeline);
			pipeline.EndGeometry();
		}

		// Token: 0x04000071 RID: 113
		private GeometryPoint[] points;
	}
}
