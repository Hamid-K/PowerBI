using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000028 RID: 40
	public abstract class Geometry : ISpatial
	{
		// Token: 0x06000102 RID: 258 RVA: 0x000037E0 File Offset: 0x000019E0
		protected Geometry(CoordinateSystem coordinateSystem, SpatialImplementation creator)
		{
			Util.CheckArgumentNull(coordinateSystem, "coordinateSystem");
			Util.CheckArgumentNull(creator, "creator");
			this.coordinateSystem = coordinateSystem;
			this.creator = creator;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000380C File Offset: 0x00001A0C
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00003814 File Offset: 0x00001A14
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000105 RID: 261
		public abstract bool IsEmpty { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000381D File Offset: 0x00001A1D
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00003825 File Offset: 0x00001A25
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

		// Token: 0x06000108 RID: 264 RVA: 0x0000382E File Offset: 0x00001A2E
		public virtual void SendTo(GeometryPipeline chain)
		{
			Util.CheckArgumentNull(chain, "chain");
			chain.SetCoordinateSystem(this.coordinateSystem);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00003848 File Offset: 0x00001A48
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
			return default(bool?);
		}

		// Token: 0x04000019 RID: 25
		private SpatialImplementation creator;

		// Token: 0x0400001A RID: 26
		private CoordinateSystem coordinateSystem;
	}
}
