using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000024 RID: 36
	public abstract class Geometry : ISpatial
	{
		// Token: 0x06000122 RID: 290 RVA: 0x00003E2C File Offset: 0x0000202C
		protected Geometry(CoordinateSystem coordinateSystem, SpatialImplementation creator)
		{
			Util.CheckArgumentNull(coordinateSystem, "coordinateSystem");
			Util.CheckArgumentNull(creator, "creator");
			this.coordinateSystem = coordinateSystem;
			this.creator = creator;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00003E58 File Offset: 0x00002058
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00003E60 File Offset: 0x00002060
		public CoordinateSystem CoordinateSystem
		{
			get
			{
				return this.coordinateSystem;
			}
			internal set
			{
				this.coordinateSystem = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000125 RID: 293
		public abstract bool IsEmpty { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00003E69 File Offset: 0x00002069
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00003E71 File Offset: 0x00002071
		internal SpatialImplementation Creator
		{
			get
			{
				return this.creator;
			}
			set
			{
				this.creator = value;
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00003E7A File Offset: 0x0000207A
		public virtual void SendTo(GeometryPipeline chain)
		{
			Util.CheckArgumentNull(chain, "chain");
			chain.SetCoordinateSystem(this.coordinateSystem);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00003E94 File Offset: 0x00002094
		internal bool? BaseEquals(Geometry other)
		{
			if (other == null)
			{
				return new bool?(false);
			}
			if (object.ReferenceEquals(this, other))
			{
				return new bool?(true);
			}
			if (!this.coordinateSystem.Equals(other.coordinateSystem))
			{
				return new bool?(false);
			}
			if (this.IsEmpty || other.IsEmpty)
			{
				return new bool?(this.IsEmpty && other.IsEmpty);
			}
			return default(bool?);
		}

		// Token: 0x0400001B RID: 27
		private SpatialImplementation creator;

		// Token: 0x0400001C RID: 28
		private CoordinateSystem coordinateSystem;
	}
}
