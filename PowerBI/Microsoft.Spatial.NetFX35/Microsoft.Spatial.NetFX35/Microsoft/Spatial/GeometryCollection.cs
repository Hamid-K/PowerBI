using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000025 RID: 37
	public abstract class GeometryCollection : Geometry
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00003F05 File Offset: 0x00002105
		protected GeometryCollection(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600012B RID: 299
		public abstract ReadOnlyCollection<Geometry> Geometries { get; }

		// Token: 0x0600012C RID: 300 RVA: 0x00003F10 File Offset: 0x00002110
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

		// Token: 0x0600012D RID: 301 RVA: 0x00003F47 File Offset: 0x00002147
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryCollection);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00003F55 File Offset: 0x00002155
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<Geometry>(base.CoordinateSystem, this.Geometries);
		}
	}
}
