using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000053 RID: 83
	internal class GeographyLineStringImplementation : GeographyLineString
	{
		// Token: 0x06000230 RID: 560 RVA: 0x00006302 File Offset: 0x00004502
		internal GeographyLineStringImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeographyPoint[] points)
			: base(coordinateSystem, creator)
		{
			this.points = points ?? new GeographyPoint[0];
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000631D File Offset: 0x0000451D
		public override bool IsEmpty
		{
			get
			{
				return this.points.Length == 0;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000632A File Offset: 0x0000452A
		public override ReadOnlyCollection<GeographyPoint> Points
		{
			get
			{
				return new ReadOnlyCollection<GeographyPoint>(this.points);
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00006337 File Offset: 0x00004537
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeographyPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeography(SpatialType.LineString);
			this.SendFigure(pipeline);
			pipeline.EndGeography();
		}

		// Token: 0x04000067 RID: 103
		private GeographyPoint[] points;
	}
}
