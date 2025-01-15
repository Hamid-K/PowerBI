using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000059 RID: 89
	internal class GeometryCollectionImplementation : GeometryCollection
	{
		// Token: 0x06000228 RID: 552 RVA: 0x000058EA File Offset: 0x00003AEA
		internal GeometryCollectionImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params Geometry[] geometry)
			: base(coordinateSystem, creator)
		{
			this.geometryArray = geometry ?? new Geometry[0];
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00005905 File Offset: 0x00003B05
		internal GeometryCollectionImplementation(SpatialImplementation creator, params Geometry[] geometry)
			: this(CoordinateSystem.DefaultGeometry, creator, geometry)
		{
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00005914 File Offset: 0x00003B14
		public override bool IsEmpty
		{
			get
			{
				return this.geometryArray.Length == 0;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00005920 File Offset: 0x00003B20
		public override ReadOnlyCollection<Geometry> Geometries
		{
			get
			{
				return new ReadOnlyCollection<Geometry>(this.geometryArray);
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00005930 File Offset: 0x00003B30
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

		// Token: 0x04000064 RID: 100
		private Geometry[] geometryArray;
	}
}
