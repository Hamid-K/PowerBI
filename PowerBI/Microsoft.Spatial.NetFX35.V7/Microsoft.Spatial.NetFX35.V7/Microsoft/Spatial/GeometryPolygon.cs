using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000026 RID: 38
	public abstract class GeometryPolygon : GeometrySurface
	{
		// Token: 0x060000FC RID: 252 RVA: 0x0000377C File Offset: 0x0000197C
		protected GeometryPolygon(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000FD RID: 253
		public abstract ReadOnlyCollection<GeometryLineString> Rings { get; }

		// Token: 0x060000FE RID: 254 RVA: 0x00003788 File Offset: 0x00001988
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeometryPolygon other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return Enumerable.SequenceEqual<GeometryLineString>(this.Rings, other.Rings);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000037BF File Offset: 0x000019BF
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryPolygon);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000037CD File Offset: 0x000019CD
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeometryLineString>(base.CoordinateSystem, this.Rings);
		}
	}
}
