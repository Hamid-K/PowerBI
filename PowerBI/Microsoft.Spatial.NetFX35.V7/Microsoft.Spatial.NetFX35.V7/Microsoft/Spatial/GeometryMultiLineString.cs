using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000021 RID: 33
	public abstract class GeometryMultiLineString : GeometryMultiCurve
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00003456 File Offset: 0x00001656
		protected GeometryMultiLineString(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000E1 RID: 225
		public abstract ReadOnlyCollection<GeometryLineString> LineStrings { get; }

		// Token: 0x060000E2 RID: 226 RVA: 0x00003460 File Offset: 0x00001660
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

		// Token: 0x060000E3 RID: 227 RVA: 0x00003497 File Offset: 0x00001697
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryMultiLineString);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000034A5 File Offset: 0x000016A5
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeometryLineString>(base.CoordinateSystem, this.LineStrings);
		}
	}
}
