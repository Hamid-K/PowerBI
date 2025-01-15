using System;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005DE RID: 1502
	public struct BoundsWrapper<TUnit> : IBounded<TUnit>, IEquatable<BoundsWrapper<TUnit>> where TUnit : BoundsUnit
	{
		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06002066 RID: 8294 RVA: 0x0005C658 File Offset: 0x0005A858
		public readonly Bounds<TUnit> Bounds { get; }

		// Token: 0x06002067 RID: 8295 RVA: 0x0005C660 File Offset: 0x0005A860
		public BoundsWrapper(Bounds<TUnit> bounds)
		{
			this.Bounds = bounds;
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x0005C669 File Offset: 0x0005A869
		public static implicit operator BoundsWrapper<TUnit>(Bounds<TUnit> bounds)
		{
			return new BoundsWrapper<TUnit>(bounds);
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x0005C674 File Offset: 0x0005A874
		public override string ToString()
		{
			return this.Bounds.ToString();
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x0005C698 File Offset: 0x0005A898
		public bool Equals(BoundsWrapper<TUnit> other)
		{
			return this.Bounds.Equals(other.Bounds);
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x0005C6BC File Offset: 0x0005A8BC
		public override bool Equals(object obj)
		{
			if (obj is BoundsWrapper<TUnit>)
			{
				BoundsWrapper<TUnit> boundsWrapper = (BoundsWrapper<TUnit>)obj;
				return this.Equals(boundsWrapper);
			}
			return false;
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x0005C6E4 File Offset: 0x0005A8E4
		public override int GetHashCode()
		{
			return this.Bounds.GetHashCode();
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x0005C705 File Offset: 0x0005A905
		public static bool operator ==(BoundsWrapper<TUnit> left, BoundsWrapper<TUnit> right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x0005C70F File Offset: 0x0005A90F
		public static bool operator !=(BoundsWrapper<TUnit> left, BoundsWrapper<TUnit> right)
		{
			return !left.Equals(right);
		}
	}
}
