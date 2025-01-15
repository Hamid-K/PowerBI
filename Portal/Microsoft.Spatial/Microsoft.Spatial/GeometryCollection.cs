using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x0200001F RID: 31
	public abstract class GeometryCollection : Geometry
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00003B2C File Offset: 0x00001D2C
		protected GeometryCollection(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000114 RID: 276
		public abstract ReadOnlyCollection<Geometry> Geometries { get; }

		// Token: 0x06000115 RID: 277 RVA: 0x00003B38 File Offset: 0x00001D38
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeometryCollection other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return this.Geometries.SequenceEqual(other.Geometries);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003B6F File Offset: 0x00001D6F
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryCollection);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00003B7D File Offset: 0x00001D7D
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<Geometry>(base.CoordinateSystem, this.Geometries);
		}
	}
}
