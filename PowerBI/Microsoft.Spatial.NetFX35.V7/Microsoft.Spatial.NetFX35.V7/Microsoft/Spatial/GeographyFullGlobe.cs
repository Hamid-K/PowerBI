using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200000F RID: 15
	public abstract class GeographyFullGlobe : GeographySurface
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00002DE4 File Offset: 0x00000FE4
		protected GeographyFullGlobe(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002DF0 File Offset: 0x00000FF0
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		[SuppressMessage("Microsoft.Design", "CA1011", Justification = "Method should not be declared on base")]
		public bool Equals(GeographyFullGlobe other)
		{
			return base.BaseEquals(other) ?? true;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002E17 File Offset: 0x00001017
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyFullGlobe);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002E25 File Offset: 0x00001025
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<int>(base.CoordinateSystem, new int[1]);
		}
	}
}
