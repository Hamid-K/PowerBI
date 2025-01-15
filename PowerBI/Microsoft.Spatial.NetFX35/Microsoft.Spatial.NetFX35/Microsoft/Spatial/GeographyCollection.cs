using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x0200001C RID: 28
	public abstract class GeographyCollection : Geography
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00003A08 File Offset: 0x00001C08
		protected GeographyCollection(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000FC RID: 252
		public abstract ReadOnlyCollection<Geography> Geographies { get; }

		// Token: 0x060000FD RID: 253 RVA: 0x00003A14 File Offset: 0x00001C14
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

		// Token: 0x060000FE RID: 254 RVA: 0x00003A4B File Offset: 0x00001C4B
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyCollection);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003A59 File Offset: 0x00001C59
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<Geography>(base.CoordinateSystem, this.Geographies);
		}
	}
}
