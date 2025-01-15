using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200004A RID: 74
	internal class GeographyLineStringImplementation : GeographyLineString
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x00004FB7 File Offset: 0x000031B7
		internal GeographyLineStringImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeographyPoint[] points)
			: base(coordinateSystem, creator)
		{
			this.points = points ?? new GeographyPoint[0];
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00004FD2 File Offset: 0x000031D2
		public override bool IsEmpty
		{
			get
			{
				return this.points.Length == 0;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00004FDE File Offset: 0x000031DE
		public override ReadOnlyCollection<GeographyPoint> Points
		{
			get
			{
				return new ReadOnlyCollection<GeographyPoint>(this.points);
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00004FEB File Offset: 0x000031EB
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeographyPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeography(SpatialType.LineString);
			this.SendFigure(pipeline);
			pipeline.EndGeography();
		}

		// Token: 0x04000051 RID: 81
		private GeographyPoint[] points;
	}
}
