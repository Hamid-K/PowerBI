using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000013 RID: 19
	public abstract class GeographyMultiLineString : GeographyMultiCurve
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00002EAE File Offset: 0x000010AE
		protected GeographyMultiLineString(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600009D RID: 157
		public abstract ReadOnlyCollection<GeographyLineString> LineStrings { get; }

		// Token: 0x0600009E RID: 158 RVA: 0x00002EB8 File Offset: 0x000010B8
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

		// Token: 0x0600009F RID: 159 RVA: 0x00002EEF File Offset: 0x000010EF
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyMultiLineString);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00002EFD File Offset: 0x000010FD
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeographyLineString>(base.CoordinateSystem, this.LineStrings);
		}
	}
}
