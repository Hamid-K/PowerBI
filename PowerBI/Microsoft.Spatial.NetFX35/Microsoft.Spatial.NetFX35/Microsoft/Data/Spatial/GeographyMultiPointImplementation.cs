using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000055 RID: 85
	internal class GeographyMultiPointImplementation : GeographyMultiPoint
	{
		// Token: 0x0600023A RID: 570 RVA: 0x000063EA File Offset: 0x000045EA
		internal GeographyMultiPointImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeographyPoint[] points)
			: base(coordinateSystem, creator)
		{
			this.points = points ?? new GeographyPoint[0];
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00006405 File Offset: 0x00004605
		internal GeographyMultiPointImplementation(SpatialImplementation creator, params GeographyPoint[] points)
			: this(CoordinateSystem.DefaultGeography, creator, points)
		{
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00006414 File Offset: 0x00004614
		public override bool IsEmpty
		{
			get
			{
				return this.points.Length == 0;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00006421 File Offset: 0x00004621
		public override ReadOnlyCollection<Geography> Geographies
		{
			get
			{
				return new ReadOnlyCollection<Geography>(this.points);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000642E File Offset: 0x0000462E
		public override ReadOnlyCollection<GeographyPoint> Points
		{
			get
			{
				return new ReadOnlyCollection<GeographyPoint>(this.points);
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000643C File Offset: 0x0000463C
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

		// Token: 0x04000069 RID: 105
		private GeographyPoint[] points;
	}
}
