using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x0200001C RID: 28
	public abstract class GeographyCollection : Geography
	{
		// Token: 0x06000105 RID: 261 RVA: 0x000035E0 File Offset: 0x000017E0
		protected GeographyCollection(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000106 RID: 262
		public abstract ReadOnlyCollection<Geography> Geographies { get; }

		// Token: 0x06000107 RID: 263 RVA: 0x00003AD4 File Offset: 0x00001CD4
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeographyCollection other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return this.Geographies.SequenceEqual(other.Geographies);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00003B0B File Offset: 0x00001D0B
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyCollection);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00003B19 File Offset: 0x00001D19
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<Geography>(base.CoordinateSystem, this.Geographies);
		}
	}
}
