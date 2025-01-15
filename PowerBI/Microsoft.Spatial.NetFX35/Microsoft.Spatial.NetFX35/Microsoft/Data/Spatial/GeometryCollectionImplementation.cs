using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000062 RID: 98
	internal class GeometryCollectionImplementation : GeometryCollection
	{
		// Token: 0x06000281 RID: 641 RVA: 0x00006C46 File Offset: 0x00004E46
		internal GeometryCollectionImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params Geometry[] geometry)
			: base(coordinateSystem, creator)
		{
			this.geometryArray = geometry ?? new Geometry[0];
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00006C61 File Offset: 0x00004E61
		internal GeometryCollectionImplementation(SpatialImplementation creator, params Geometry[] geometry)
			: this(CoordinateSystem.DefaultGeometry, creator, geometry)
		{
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00006C70 File Offset: 0x00004E70
		public override bool IsEmpty
		{
			get
			{
				return this.geometryArray.Length == 0;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00006C7D File Offset: 0x00004E7D
		public override ReadOnlyCollection<Geometry> Geometries
		{
			get
			{
				return new ReadOnlyCollection<Geometry>(this.geometryArray);
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00006C8C File Offset: 0x00004E8C
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeometryPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeometry(SpatialType.Collection);
			for (int i = 0; i < this.geometryArray.Length; i++)
			{
				this.geometryArray[i].SendTo(pipeline);
			}
			pipeline.EndGeometry();
		}

		// Token: 0x0400007A RID: 122
		private Geometry[] geometryArray;
	}
}
