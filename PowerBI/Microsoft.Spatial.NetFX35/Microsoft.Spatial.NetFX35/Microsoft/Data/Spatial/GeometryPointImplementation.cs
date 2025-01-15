using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000060 RID: 96
	internal class GeometryPointImplementation : GeometryPoint
	{
		// Token: 0x06000274 RID: 628 RVA: 0x00006A6A File Offset: 0x00004C6A
		internal GeometryPointImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
			this.x = double.NaN;
			this.y = double.NaN;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00006A94 File Offset: 0x00004C94
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "zvalue and mvalue are spelled correctly")]
		internal GeometryPointImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, double x, double y, double? z, double? m)
			: base(coordinateSystem, creator)
		{
			if (double.IsNaN(x) || double.IsInfinity(x))
			{
				throw new ArgumentException(Strings.InvalidPointCoordinate(x, "x"));
			}
			if (double.IsNaN(y) || double.IsInfinity(y))
			{
				throw new ArgumentException(Strings.InvalidPointCoordinate(y, "y"));
			}
			this.x = x;
			this.y = y;
			this.z = z;
			this.m = m;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00006B17 File Offset: 0x00004D17
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "X is meaningful")]
		public override double X
		{
			get
			{
				if (this.IsEmpty)
				{
					throw new NotSupportedException(Strings.Point_AccessCoordinateWhenEmpty);
				}
				return this.x;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00006B32 File Offset: 0x00004D32
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Y is meaningful")]
		public override double Y
		{
			get
			{
				if (this.IsEmpty)
				{
					throw new NotSupportedException(Strings.Point_AccessCoordinateWhenEmpty);
				}
				return this.y;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00006B4D File Offset: 0x00004D4D
		public override bool IsEmpty
		{
			get
			{
				return double.IsNaN(this.x);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00006B5A File Offset: 0x00004D5A
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		public override double? Z
		{
			get
			{
				return this.z;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00006B62 File Offset: 0x00004D62
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		public override double? M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00006B6C File Offset: 0x00004D6C
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeometryPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeometry(SpatialType.Point);
			if (!this.IsEmpty)
			{
				pipeline.BeginFigure(new GeometryPosition(this.x, this.y, this.z, this.m));
				pipeline.EndFigure();
			}
			pipeline.EndGeometry();
		}

		// Token: 0x04000075 RID: 117
		private double x;

		// Token: 0x04000076 RID: 118
		private double y;

		// Token: 0x04000077 RID: 119
		private double? z;

		// Token: 0x04000078 RID: 120
		private double? m;
	}
}
