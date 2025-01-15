using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Microsoft.Spatial
{
	// Token: 0x02000039 RID: 57
	public class GeometryPosition : IEquatable<GeometryPosition>
	{
		// Token: 0x06000159 RID: 345 RVA: 0x00004116 File Offset: 0x00002316
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public GeometryPosition(double x, double y, double? z, double? m)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.m = m;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000413C File Offset: 0x0000233C
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y make sense in context")]
		public GeometryPosition(double x, double y)
			: this(x, y, default(double?), default(double?))
		{
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00004163 File Offset: 0x00002363
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public double? M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000416B File Offset: 0x0000236B
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public double X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00004173 File Offset: 0x00002373
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public double Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000417B File Offset: 0x0000237B
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public double? Z
		{
			get
			{
				return this.z;
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00004183 File Offset: 0x00002383
		public static bool operator ==(GeometryPosition left, GeometryPosition right)
		{
			if (left == null)
			{
				return right == null;
			}
			return right != null && left.Equals(right);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00004199 File Offset: 0x00002399
		public static bool operator !=(GeometryPosition left, GeometryPosition right)
		{
			return !(left == right);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000041A5 File Offset: 0x000023A5
		public override bool Equals(object obj)
		{
			return obj != null && obj.GetType() == typeof(GeometryPosition) && this.Equals((GeometryPosition)obj);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000041CC File Offset: 0x000023CC
		public bool Equals(GeometryPosition other)
		{
			return other != null && other.x.Equals(this.x) && other.y.Equals(this.y) && other.z.Equals(this.z) && other.m.Equals(this.m);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00004250 File Offset: 0x00002450
		public override int GetHashCode()
		{
			int num = this.x.GetHashCode();
			num = (num * 397) ^ this.y.GetHashCode();
			num = (num * 397) ^ ((this.z != null) ? this.z.Value.GetHashCode() : 0);
			return (num * 397) ^ ((this.m != null) ? this.m.Value.GetHashCode() : 0);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000042E8 File Offset: 0x000024E8
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

		// Token: 0x04000035 RID: 53
		private readonly double? m;

		// Token: 0x04000036 RID: 54
		private readonly double x;

		// Token: 0x04000037 RID: 55
		private readonly double y;

		// Token: 0x04000038 RID: 56
		private readonly double? z;
	}
}
