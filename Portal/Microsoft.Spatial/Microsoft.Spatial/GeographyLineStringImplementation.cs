using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200004F RID: 79
	internal class GeographyLineStringImplementation : GeographyLineString
	{
		// Token: 0x0600024D RID: 589 RVA: 0x00005C7F File Offset: 0x00003E7F
		internal GeographyLineStringImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeographyPoint[] points)
			: base(coordinateSystem, creator)
		{
			this.points = points ?? new GeographyPoint[0];
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00005C9A File Offset: 0x00003E9A
		public override bool IsEmpty
		{
			get
			{
				return this.points.Length == 0;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00005CA6 File Offset: 0x00003EA6
		public override ReadOnlyCollection<GeographyPoint> Points
		{
			get
			{
				return new ReadOnlyCollection<GeographyPoint>(this.points);
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00005CB3 File Offset: 0x00003EB3
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeographyPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeography(SpatialType.LineString);
			this.SendFigure(pipeline);
			pipeline.EndGeography();
		}

		// Token: 0x0400005E RID: 94
		private GeographyPoint[] points;
	}
}
