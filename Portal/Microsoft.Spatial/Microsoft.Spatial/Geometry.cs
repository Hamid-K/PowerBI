using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200002C RID: 44
	public abstract class Geometry : ISpatial
	{
		// Token: 0x0600016E RID: 366 RVA: 0x000042F0 File Offset: 0x000024F0
		protected Geometry(CoordinateSystem coordinateSystem, SpatialImplementation creator)
		{
			Util.CheckArgumentNull(coordinateSystem, "coordinateSystem");
			Util.CheckArgumentNull(creator, "creator");
			this.coordinateSystem = coordinateSystem;
			this.creator = creator;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000431C File Offset: 0x0000251C
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00004324 File Offset: 0x00002524
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

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000171 RID: 369
		public abstract bool IsEmpty { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000432D File Offset: 0x0000252D
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00004335 File Offset: 0x00002535
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

		// Token: 0x06000174 RID: 372 RVA: 0x0000433E File Offset: 0x0000253E
		public virtual void SendTo(GeometryPipeline chain)
		{
			Util.CheckArgumentNull(chain, "chain");
			chain.SetCoordinateSystem(this.coordinateSystem);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00004358 File Offset: 0x00002558
		internal bool? BaseEquals(Geometry other)
		{
			if (other == null)
			{
				return new bool?(false);
			}
			if (this == other)
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
			return null;
		}

		// Token: 0x0400001E RID: 30
		private SpatialImplementation creator;

		// Token: 0x0400001F RID: 31
		private CoordinateSystem coordinateSystem;
	}
}
