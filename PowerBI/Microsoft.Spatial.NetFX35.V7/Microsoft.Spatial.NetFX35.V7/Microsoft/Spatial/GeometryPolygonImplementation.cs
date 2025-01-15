using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000058 RID: 88
	internal class GeometryPolygonImplementation : GeometryPolygon
	{
		// Token: 0x06000223 RID: 547 RVA: 0x00005862 File Offset: 0x00003A62
		internal GeometryPolygonImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeometryLineString[] rings)
			: base(coordinateSystem, creator)
		{
			this.rings = rings ?? new GeometryLineString[0];
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000587D File Offset: 0x00003A7D
		internal GeometryPolygonImplementation(SpatialImplementation creator, params GeometryLineString[] rings)
			: this(CoordinateSystem.DefaultGeometry, creator, rings)
		{
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000588C File Offset: 0x00003A8C
		public override bool IsEmpty
		{
			get
			{
				return this.rings.Length == 0;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00005898 File Offset: 0x00003A98
		public override ReadOnlyCollection<GeometryLineString> Rings
		{
			get
			{
				return new ReadOnlyCollection<GeometryLineString>(this.rings);
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000058A8 File Offset: 0x00003AA8
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

		// Token: 0x04000063 RID: 99
		private GeometryLineString[] rings;
	}
}
