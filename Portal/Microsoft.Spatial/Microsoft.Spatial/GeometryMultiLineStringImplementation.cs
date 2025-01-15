using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000059 RID: 89
	internal class GeometryMultiLineStringImplementation : GeometryMultiLineString
	{
		// Token: 0x0600027F RID: 639 RVA: 0x00006228 File Offset: 0x00004428
		internal GeometryMultiLineStringImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeometryLineString[] lineStrings)
			: base(coordinateSystem, creator)
		{
			this.lineStrings = lineStrings ?? new GeometryLineString[0];
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00006243 File Offset: 0x00004443
		internal GeometryMultiLineStringImplementation(SpatialImplementation creator, params GeometryLineString[] lineStrings)
			: this(CoordinateSystem.DefaultGeometry, creator, lineStrings)
		{
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00006252 File Offset: 0x00004452
		public override bool IsEmpty
		{
			get
			{
				return this.lineStrings.Length == 0;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000625E File Offset: 0x0000445E
		public override ReadOnlyCollection<Geometry> Geometries
		{
			get
			{
				return new ReadOnlyCollection<Geometry>(this.lineStrings);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000626B File Offset: 0x0000446B
		public override ReadOnlyCollection<GeometryLineString> LineStrings
		{
			get
			{
				return new ReadOnlyCollection<GeometryLineString>(this.lineStrings);
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00006278 File Offset: 0x00004478
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

		// Token: 0x04000069 RID: 105
		private GeometryLineString[] lineStrings;
	}
}
