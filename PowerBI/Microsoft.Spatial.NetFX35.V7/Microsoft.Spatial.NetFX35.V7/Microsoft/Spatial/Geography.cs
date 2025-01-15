using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000019 RID: 25
	public abstract class Geography : ISpatial
	{
		// Token: 0x060000BD RID: 189 RVA: 0x0000322C File Offset: 0x0000142C
		protected Geography(CoordinateSystem coordinateSystem, SpatialImplementation creator)
		{
			Util.CheckArgumentNull(coordinateSystem, "coordinateSystem");
			Util.CheckArgumentNull(creator, "creator");
			this.coordinateSystem = coordinateSystem;
			this.creator = creator;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003258 File Offset: 0x00001458
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003260 File Offset: 0x00001460
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000C0 RID: 192
		public abstract bool IsEmpty { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003269 File Offset: 0x00001469
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00003271 File Offset: 0x00001471
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

		// Token: 0x060000C3 RID: 195 RVA: 0x0000327A File Offset: 0x0000147A
		public virtual void SendTo(GeographyPipeline chain)
		{
			Util.CheckArgumentNull(chain, "chain");
			chain.SetCoordinateSystem(this.coordinateSystem);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003293 File Offset: 0x00001493
		internal static int ComputeHashCodeFor<T>(CoordinateSystem coords, IEnumerable<T> fields)
		{
			return Enumerable.Aggregate<T, int>(fields, coords.GetHashCode(), (int current, T field) => (current * 397) ^ field.GetHashCode());
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000032C0 File Offset: 0x000014C0
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
			return default(bool?);
		}

		// Token: 0x04000017 RID: 23
		private SpatialImplementation creator;

		// Token: 0x04000018 RID: 24
		private CoordinateSystem coordinateSystem;
	}
}
