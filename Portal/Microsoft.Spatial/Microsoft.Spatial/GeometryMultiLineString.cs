using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000025 RID: 37
	public abstract class GeometryMultiLineString : GeometryMultiCurve
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00003F66 File Offset: 0x00002166
		protected GeometryMultiLineString(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600014D RID: 333
		public abstract ReadOnlyCollection<GeometryLineString> LineStrings { get; }

		// Token: 0x0600014E RID: 334 RVA: 0x00003F70 File Offset: 0x00002170
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeometryMultiLineString other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return this.LineStrings.SequenceEqual(other.LineStrings);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00003FA7 File Offset: 0x000021A7
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryMultiLineString);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00003FB5 File Offset: 0x000021B5
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeometryLineString>(base.CoordinateSystem, this.LineStrings);
		}
	}
}
