using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000027 RID: 39
	public abstract class GeometryLineString : GeometryCurve
	{
		// Token: 0x06000130 RID: 304 RVA: 0x00003F72 File Offset: 0x00002172
		protected GeometryLineString(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000131 RID: 305
		public abstract ReadOnlyCollection<GeometryPoint> Points { get; }

		// Token: 0x06000132 RID: 306 RVA: 0x00003F7C File Offset: 0x0000217C
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeometryLineString other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return Enumerable.SequenceEqual<GeometryPoint>(this.Points, other.Points);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00003FB3 File Offset: 0x000021B3
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryLineString);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00003FC1 File Offset: 0x000021C1
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeometryPoint>(base.CoordinateSystem, this.Points);
		}
	}
}
