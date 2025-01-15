using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x0200001A RID: 26
	public abstract class GeographyCollection : Geography
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00002E38 File Offset: 0x00001038
		protected GeographyCollection(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000C7 RID: 199
		public abstract ReadOnlyCollection<Geography> Geographies { get; }

		// Token: 0x060000C8 RID: 200 RVA: 0x0000332C File Offset: 0x0000152C
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeographyCollection other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return Enumerable.SequenceEqual<Geography>(this.Geographies, other.Geographies);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003363 File Offset: 0x00001563
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyCollection);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003371 File Offset: 0x00001571
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<Geography>(base.CoordinateSystem, this.Geographies);
		}
	}
}
