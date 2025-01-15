using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200005D RID: 93
	internal class GeometryMultiLineStringImplementation : GeometryMultiLineString
	{
		// Token: 0x06000262 RID: 610 RVA: 0x000068B5 File Offset: 0x00004AB5
		internal GeometryMultiLineStringImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeometryLineString[] lineStrings)
			: base(coordinateSystem, creator)
		{
			this.lineStrings = lineStrings ?? new GeometryLineString[0];
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000068D0 File Offset: 0x00004AD0
		internal GeometryMultiLineStringImplementation(SpatialImplementation creator, params GeometryLineString[] lineStrings)
			: this(CoordinateSystem.DefaultGeometry, creator, lineStrings)
		{
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000264 RID: 612 RVA: 0x000068DF File Offset: 0x00004ADF
		public override bool IsEmpty
		{
			get
			{
				return this.lineStrings.Length == 0;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000265 RID: 613 RVA: 0x000068EC File Offset: 0x00004AEC
		public override ReadOnlyCollection<Geometry> Geometries
		{
			get
			{
				return new ReadOnlyCollection<Geometry>(this.lineStrings);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000266 RID: 614 RVA: 0x000068F9 File Offset: 0x00004AF9
		public override ReadOnlyCollection<GeometryLineString> LineStrings
		{
			get
			{
				return new ReadOnlyCollection<GeometryLineString>(this.lineStrings);
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00006908 File Offset: 0x00004B08
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeometryPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeometry(SpatialType.MultiLineString);
			for (int i = 0; i < this.lineStrings.Length; i++)
			{
				this.lineStrings[i].SendTo(pipeline);
			}
			pipeline.EndGeometry();
		}

		// Token: 0x04000072 RID: 114
		private GeometryLineString[] lineStrings;
	}
}
