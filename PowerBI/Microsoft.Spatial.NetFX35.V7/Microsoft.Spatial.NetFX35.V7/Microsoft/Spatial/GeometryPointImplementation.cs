using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000057 RID: 87
	internal class GeometryPointImplementation : GeometryPoint
	{
		// Token: 0x0600021B RID: 539 RVA: 0x0000570E File Offset: 0x0000390E
		internal GeometryPointImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
			this.x = double.NaN;
			this.y = double.NaN;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00005738 File Offset: 0x00003938
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

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600021D RID: 541 RVA: 0x000057BB File Offset: 0x000039BB
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

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600021E RID: 542 RVA: 0x000057D6 File Offset: 0x000039D6
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

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000057F1 File Offset: 0x000039F1
		public override bool IsEmpty
		{
			get
			{
				return double.IsNaN(this.x);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000057FE File Offset: 0x000039FE
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		public override double? Z
		{
			get
			{
				return this.z;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00005806 File Offset: 0x00003A06
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		public override double? M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00005810 File Offset: 0x00003A10
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

		// Token: 0x0400005F RID: 95
		private double x;

		// Token: 0x04000060 RID: 96
		private double y;

		// Token: 0x04000061 RID: 97
		private double? z;

		// Token: 0x04000062 RID: 98
		private double? m;
	}
}
