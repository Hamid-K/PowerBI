using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200004C RID: 76
	internal class GeographyMultiPointImplementation : GeographyMultiPoint
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x0000509A File Offset: 0x0000329A
		internal GeographyMultiPointImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeographyPoint[] points)
			: base(coordinateSystem, creator)
		{
			this.points = points ?? new GeographyPoint[0];
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000050B5 File Offset: 0x000032B5
		internal GeographyMultiPointImplementation(SpatialImplementation creator, params GeographyPoint[] points)
			: this(CoordinateSystem.DefaultGeography, creator, points)
		{
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x000050C4 File Offset: 0x000032C4
		public override bool IsEmpty
		{
			get
			{
				return this.points.Length == 0;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000050D0 File Offset: 0x000032D0
		public override ReadOnlyCollection<Geography> Geographies
		{
			get
			{
				return new ReadOnlyCollection<Geography>(this.points);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000050DD File Offset: 0x000032DD
		public override ReadOnlyCollection<GeographyPoint> Points
		{
			get
			{
				return new ReadOnlyCollection<GeographyPoint>(this.points);
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000050EC File Offset: 0x000032EC
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeographyPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeography(SpatialType.MultiPoint);
			for (int i = 0; i < this.points.Length; i++)
			{
				this.points[i].SendTo(pipeline);
			}
			pipeline.EndGeography();
		}

		// Token: 0x04000053 RID: 83
		private GeographyPoint[] points;
	}
}
