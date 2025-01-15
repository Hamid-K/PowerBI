using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200005E RID: 94
	internal class GeometryCollectionImplementation : GeometryCollection
	{
		// Token: 0x0600029E RID: 670 RVA: 0x000065B2 File Offset: 0x000047B2
		internal GeometryCollectionImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params Geometry[] geometry)
			: base(coordinateSystem, creator)
		{
			this.geometryArray = geometry ?? new Geometry[0];
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000065CD File Offset: 0x000047CD
		internal GeometryCollectionImplementation(SpatialImplementation creator, params Geometry[] geometry)
			: this(CoordinateSystem.DefaultGeometry, creator, geometry)
		{
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x000065DC File Offset: 0x000047DC
		public override bool IsEmpty
		{
			get
			{
				return this.geometryArray.Length == 0;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x000065E8 File Offset: 0x000047E8
		public override ReadOnlyCollection<Geometry> Geometries
		{
			get
			{
				return new ReadOnlyCollection<Geometry>(this.geometryArray);
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000065F8 File Offset: 0x000047F8
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

		// Token: 0x04000071 RID: 113
		private Geometry[] geometryArray;
	}
}
