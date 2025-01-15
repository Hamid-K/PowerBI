using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000011 RID: 17
	public abstract class GeographyFullGlobe : GeographySurface
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x0000358E File Offset: 0x0000178E
		protected GeographyFullGlobe(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003598 File Offset: 0x00001798
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		[SuppressMessage("Microsoft.Design", "CA1011", Justification = "Method should not be declared on base")]
		public bool Equals(GeographyFullGlobe other)
		{
			return base.BaseEquals(other) ?? true;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000035BF File Offset: 0x000017BF
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyFullGlobe);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000035CD File Offset: 0x000017CD
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<int>(base.CoordinateSystem, new int[1]);
		}
	}
}
