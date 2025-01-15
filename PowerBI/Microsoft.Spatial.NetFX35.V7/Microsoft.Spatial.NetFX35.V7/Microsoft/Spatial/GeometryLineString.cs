using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x0200001F RID: 31
	public abstract class GeometryLineString : GeometryCurve
	{
		// Token: 0x060000DA RID: 218 RVA: 0x000033E8 File Offset: 0x000015E8
		protected GeometryLineString(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000DB RID: 219
		public abstract ReadOnlyCollection<GeometryPoint> Points { get; }

		// Token: 0x060000DC RID: 220 RVA: 0x000033F4 File Offset: 0x000015F4
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

		// Token: 0x060000DD RID: 221 RVA: 0x0000342B File Offset: 0x0000162B
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryLineString);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003439 File Offset: 0x00001639
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeometryPoint>(base.CoordinateSystem, this.Points);
		}
	}
}
