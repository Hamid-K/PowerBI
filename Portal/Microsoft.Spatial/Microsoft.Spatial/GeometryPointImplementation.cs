using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200005C RID: 92
	internal class GeometryPointImplementation : GeometryPoint
	{
		// Token: 0x06000291 RID: 657 RVA: 0x000063D6 File Offset: 0x000045D6
		internal GeometryPointImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
			this.x = double.NaN;
			this.y = double.NaN;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00006400 File Offset: 0x00004600
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

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00006483 File Offset: 0x00004683
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

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000649E File Offset: 0x0000469E
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

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000295 RID: 661 RVA: 0x000064B9 File Offset: 0x000046B9
		public override bool IsEmpty
		{
			get
			{
				return double.IsNaN(this.x);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000296 RID: 662 RVA: 0x000064C6 File Offset: 0x000046C6
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		public override double? Z
		{
			get
			{
				return this.z;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000297 RID: 663 RVA: 0x000064CE File Offset: 0x000046CE
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		public override double? M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000064D8 File Offset: 0x000046D8
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

		// Token: 0x0400006C RID: 108
		private double x;

		// Token: 0x0400006D RID: 109
		private double y;

		// Token: 0x0400006E RID: 110
		private double? z;

		// Token: 0x0400006F RID: 111
		private double? m;
	}
}
