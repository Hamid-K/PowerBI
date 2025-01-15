using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x0200001E RID: 30
	public abstract class GeographyMultiLineString : GeographyMultiCurve
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00003A76 File Offset: 0x00001C76
		protected GeographyMultiLineString(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000102 RID: 258
		public abstract ReadOnlyCollection<GeographyLineString> LineStrings { get; }

		// Token: 0x06000103 RID: 259 RVA: 0x00003A80 File Offset: 0x00001C80
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeographyMultiLineString other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return Enumerable.SequenceEqual<GeographyLineString>(this.LineStrings, other.LineStrings);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00003AB7 File Offset: 0x00001CB7
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyMultiLineString);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00003AC5 File Offset: 0x00001CC5
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeographyLineString>(base.CoordinateSystem, this.LineStrings);
		}
	}
}
