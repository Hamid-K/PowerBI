using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000015 RID: 21
	public abstract class GeographyMultiLineString : GeographyMultiCurve
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00003656 File Offset: 0x00001856
		protected GeographyMultiLineString(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000DC RID: 220
		public abstract ReadOnlyCollection<GeographyLineString> LineStrings { get; }

		// Token: 0x060000DD RID: 221 RVA: 0x00003660 File Offset: 0x00001860
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeographyMultiLineString other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return this.LineStrings.SequenceEqual(other.LineStrings);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003697 File Offset: 0x00001897
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyMultiLineString);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000036A5 File Offset: 0x000018A5
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeographyLineString>(base.CoordinateSystem, this.LineStrings);
		}
	}
}
