using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x0200001B RID: 27
	public abstract class Geography : ISpatial
	{
		// Token: 0x060000FC RID: 252 RVA: 0x000039D4 File Offset: 0x00001BD4
		protected Geography(CoordinateSystem coordinateSystem, SpatialImplementation creator)
		{
			Util.CheckArgumentNull(coordinateSystem, "coordinateSystem");
			Util.CheckArgumentNull(creator, "creator");
			this.coordinateSystem = coordinateSystem;
			this.creator = creator;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00003A00 File Offset: 0x00001C00
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00003A08 File Offset: 0x00001C08
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000FF RID: 255
		public abstract bool IsEmpty { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00003A11 File Offset: 0x00001C11
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00003A19 File Offset: 0x00001C19
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

		// Token: 0x06000102 RID: 258 RVA: 0x00003A22 File Offset: 0x00001C22
		public virtual void SendTo(GeographyPipeline chain)
		{
			Util.CheckArgumentNull(chain, "chain");
			chain.SetCoordinateSystem(this.coordinateSystem);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003A3B File Offset: 0x00001C3B
		internal static int ComputeHashCodeFor<T>(CoordinateSystem coords, IEnumerable<T> fields)
		{
			return fields.Aggregate(coords.GetHashCode(), (int current, T field) => (current * 397) ^ field.GetHashCode());
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00003A68 File Offset: 0x00001C68
		internal bool? BaseEquals(Geography other)
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

		// Token: 0x0400001A RID: 26
		private SpatialImplementation creator;

		// Token: 0x0400001B RID: 27
		private CoordinateSystem coordinateSystem;
	}
}
