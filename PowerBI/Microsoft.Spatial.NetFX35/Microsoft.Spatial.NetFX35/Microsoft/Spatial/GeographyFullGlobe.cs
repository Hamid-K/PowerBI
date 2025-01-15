using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000019 RID: 25
	public abstract class GeographyFullGlobe : GeographySurface
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00003937 File Offset: 0x00001B37
		protected GeographyFullGlobe(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003944 File Offset: 0x00001B44
		[SuppressMessage("Microsoft.Design", "CA1011", Justification = "Method should not be declared on base")]
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeographyFullGlobe other)
		{
			return base.BaseEquals(other) ?? true;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000396B File Offset: 0x00001B6B
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyFullGlobe);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000397C File Offset: 0x00001B7C
		public override int GetHashCode()
		{
			CoordinateSystem coordinateSystem = base.CoordinateSystem;
			int[] array = new int[1];
			return Geography.ComputeHashCodeFor<int>(coordinateSystem, array);
		}
	}
}
