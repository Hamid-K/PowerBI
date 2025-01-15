using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000029 RID: 41
	public abstract class GeometryMultiLineString : GeometryMultiCurve
	{
		// Token: 0x06000136 RID: 310 RVA: 0x00003FDE File Offset: 0x000021DE
		protected GeometryMultiLineString(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000137 RID: 311
		public abstract ReadOnlyCollection<GeometryLineString> LineStrings { get; }

		// Token: 0x06000138 RID: 312 RVA: 0x00003FE8 File Offset: 0x000021E8
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeometryMultiLineString other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return Enumerable.SequenceEqual<GeometryLineString>(this.LineStrings, other.LineStrings);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000401F File Offset: 0x0000221F
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryMultiLineString);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000402D File Offset: 0x0000222D
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeometryLineString>(base.CoordinateSystem, this.LineStrings);
		}
	}
}
