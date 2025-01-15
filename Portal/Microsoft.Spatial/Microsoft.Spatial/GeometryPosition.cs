using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Microsoft.Spatial
{
	// Token: 0x0200003E RID: 62
	public class GeometryPosition : IEquatable<GeometryPosition>
	{
		// Token: 0x060001CF RID: 463 RVA: 0x00004DEA File Offset: 0x00002FEA
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public GeometryPosition(double x, double y, double? z, double? m)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.m = m;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00004E10 File Offset: 0x00003010
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y make sense in context")]
		public GeometryPosition(double x, double y)
			: this(x, y, null, null)
		{
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00004E37 File Offset: 0x00003037
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public double? M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00004E3F File Offset: 0x0000303F
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public double X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00004E47 File Offset: 0x00003047
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public double Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00004E4F File Offset: 0x0000304F
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public double? Z
		{
			get
			{
				return this.z;
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00004E57 File Offset: 0x00003057
		public static bool operator ==(GeometryPosition left, GeometryPosition right)
		{
			if (left == null)
			{
				return right == null;
			}
			return right != null && left.Equals(right);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00004E6D File Offset: 0x0000306D
		public static bool operator !=(GeometryPosition left, GeometryPosition right)
		{
			return !(left == right);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00004E79 File Offset: 0x00003079
		public override bool Equals(object obj)
		{
			return obj != null && obj.GetType() == typeof(GeometryPosition) && this.Equals((GeometryPosition)obj);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00004EA0 File Offset: 0x000030A0
		public bool Equals(GeometryPosition other)
		{
			return other != null && other.x.Equals(this.x) && other.y.Equals(this.y) && other.z.Equals(this.z) && other.m.Equals(this.m);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00004F24 File Offset: 0x00003124
		public override int GetHashCode()
		{
			int num = this.x.GetHashCode();
			num = (num * 397) ^ this.y.GetHashCode();
			num = (num * 397) ^ ((this.z != null) ? this.z.Value.GetHashCode() : 0);
			return (num * 397) ^ ((this.m != null) ? this.m.Value.GetHashCode() : 0);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00004FBC File Offset: 0x000031BC
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "GeometryPosition({0}, {1}, {2}, {3})", new object[]
			{
				this.x,
				this.y,
				(this.z != null) ? this.z.ToString() : "null",
				(this.m != null) ? this.m.ToString() : "null"
			});
		}

		// Token: 0x04000042 RID: 66
		private readonly double? m;

		// Token: 0x04000043 RID: 67
		private readonly double x;

		// Token: 0x04000044 RID: 68
		private readonly double y;

		// Token: 0x04000045 RID: 69
		private readonly double? z;
	}
}
