using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200005D RID: 93
	internal class GeometryPolygonImplementation : GeometryPolygon
	{
		// Token: 0x06000299 RID: 665 RVA: 0x0000652A File Offset: 0x0000472A
		internal GeometryPolygonImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeometryLineString[] rings)
			: base(coordinateSystem, creator)
		{
			this.rings = rings ?? new GeometryLineString[0];
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00006545 File Offset: 0x00004745
		internal GeometryPolygonImplementation(SpatialImplementation creator, params GeometryLineString[] rings)
			: this(CoordinateSystem.DefaultGeometry, creator, rings)
		{
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00006554 File Offset: 0x00004754
		public override bool IsEmpty
		{
			get
			{
				return this.rings.Length == 0;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00006560 File Offset: 0x00004760
		public override ReadOnlyCollection<GeometryLineString> Rings
		{
			get
			{
				return new ReadOnlyCollection<GeometryLineString>(this.rings);
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00006570 File Offset: 0x00004770
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeometryPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeometry(SpatialType.Polygon);
			for (int i = 0; i < this.rings.Length; i++)
			{
				this.rings[i].SendFigure(pipeline);
			}
			pipeline.EndGeometry();
		}

		// Token: 0x04000070 RID: 112
		private GeometryLineString[] rings;
	}
}
