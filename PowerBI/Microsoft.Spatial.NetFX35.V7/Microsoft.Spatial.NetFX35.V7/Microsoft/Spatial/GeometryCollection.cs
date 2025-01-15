using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x0200001D RID: 29
	public abstract class GeometryCollection : Geometry
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00003384 File Offset: 0x00001584
		protected GeometryCollection(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000D5 RID: 213
		public abstract ReadOnlyCollection<Geometry> Geometries { get; }

		// Token: 0x060000D6 RID: 214 RVA: 0x00003390 File Offset: 0x00001590
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeometryCollection other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return Enumerable.SequenceEqual<Geometry>(this.Geometries, other.Geometries);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000033C7 File Offset: 0x000015C7
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryCollection);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000033D5 File Offset: 0x000015D5
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<Geometry>(base.CoordinateSystem, this.Geometries);
		}
	}
}
