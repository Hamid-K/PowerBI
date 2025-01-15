using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000017 RID: 23
	public abstract class Geography : ISpatial
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00003810 File Offset: 0x00001A10
		protected Geography(CoordinateSystem coordinateSystem, SpatialImplementation creator)
		{
			Util.CheckArgumentNull(coordinateSystem, "coordinateSystem");
			Util.CheckArgumentNull(creator, "creator");
			this.coordinateSystem = coordinateSystem;
			this.creator = creator;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000383C File Offset: 0x00001A3C
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00003844 File Offset: 0x00001A44
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000E9 RID: 233
		public abstract bool IsEmpty { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000384D File Offset: 0x00001A4D
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00003855 File Offset: 0x00001A55
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

		// Token: 0x060000EC RID: 236 RVA: 0x0000385E File Offset: 0x00001A5E
		public virtual void SendTo(GeographyPipeline chain)
		{
			Util.CheckArgumentNull(chain, "chain");
			chain.SetCoordinateSystem(this.coordinateSystem);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003890 File Offset: 0x00001A90
		internal static int ComputeHashCodeFor<T>(CoordinateSystem coords, IEnumerable<T> fields)
		{
			Func<int, T, int> func = null;
			int hashCode = coords.GetHashCode();
			if (func == null)
			{
				func = (int current, T field) => (current * 397) ^ field.GetHashCode();
			}
			return Enumerable.Aggregate<T, int>(fields, hashCode, func);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000038BC File Offset: 0x00001ABC
		internal bool? BaseEquals(Geography other)
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

		// Token: 0x04000019 RID: 25
		private SpatialImplementation creator;

		// Token: 0x0400001A RID: 26
		private CoordinateSystem coordinateSystem;
	}
}
