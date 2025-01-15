using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000051 RID: 81
	internal class GeographyMultiPointImplementation : GeographyMultiPoint
	{
		// Token: 0x06000257 RID: 599 RVA: 0x00005D62 File Offset: 0x00003F62
		internal GeographyMultiPointImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeographyPoint[] points)
			: base(coordinateSystem, creator)
		{
			this.points = points ?? new GeographyPoint[0];
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00005D7D File Offset: 0x00003F7D
		internal GeographyMultiPointImplementation(SpatialImplementation creator, params GeographyPoint[] points)
			: this(CoordinateSystem.DefaultGeography, creator, points)
		{
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00005D8C File Offset: 0x00003F8C
		public override bool IsEmpty
		{
			get
			{
				return this.points.Length == 0;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00005D98 File Offset: 0x00003F98
		public override ReadOnlyCollection<Geography> Geographies
		{
			get
			{
				return new ReadOnlyCollection<Geography>(this.points);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00005DA5 File Offset: 0x00003FA5
		public override ReadOnlyCollection<GeographyPoint> Points
		{
			get
			{
				return new ReadOnlyCollection<GeographyPoint>(this.points);
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00005DB4 File Offset: 0x00003FB4
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

		// Token: 0x04000060 RID: 96
		private GeographyPoint[] points;
	}
}
