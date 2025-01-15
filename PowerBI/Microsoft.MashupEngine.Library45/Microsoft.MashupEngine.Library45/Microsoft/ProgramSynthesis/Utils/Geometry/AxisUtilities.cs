using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x02000593 RID: 1427
	public static class AxisUtilities
	{
		// Token: 0x06001F28 RID: 7976 RVA: 0x00059BE6 File Offset: 0x00057DE6
		public static IReadOnlyCollection<Direction> AlignedDirections(this Axis axis)
		{
			if (axis == Axis.Horizontal)
			{
				return AxisUtilities.HorizontalDirections;
			}
			if (axis == Axis.Vertical)
			{
				return AxisUtilities.VerticalDirections;
			}
			throw new ArgumentException("axis", string.Format("Invalid axis: {0}", axis));
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x00059C15 File Offset: 0x00057E15
		public static Axis Perpendicular(this Axis axis)
		{
			if (axis == Axis.Horizontal)
			{
				return Axis.Vertical;
			}
			if (axis == Axis.Vertical)
			{
				return Axis.Horizontal;
			}
			throw new ArgumentException("axis", string.Format("Invalid axis: {0}", axis));
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x00059C3C File Offset: 0x00057E3C
		public static Direction IncreasingDirection(this Axis axis)
		{
			if (axis == Axis.Horizontal)
			{
				return Direction.Right;
			}
			if (axis == Axis.Vertical)
			{
				return Direction.Down;
			}
			throw new ArgumentException("axis", string.Format("Invalid axis: {0}", axis));
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x00059C63 File Offset: 0x00057E63
		public static Direction DecreasingDirection(this Axis axis)
		{
			if (axis == Axis.Horizontal)
			{
				return Direction.Left;
			}
			if (axis == Axis.Vertical)
			{
				return Direction.Up;
			}
			throw new ArgumentException("axis", string.Format("Invalid axis: {0}", axis));
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x00059C8A File Offset: 0x00057E8A
		// Note: this type is marked as 'beforefieldinit'.
		static AxisUtilities()
		{
			Axis[] array = new Axis[2];
			array[0] = Axis.Vertical;
			AxisUtilities.Axes = array;
		}

		// Token: 0x04000F03 RID: 3843
		private static readonly IReadOnlyCollection<Direction> VerticalDirections = new Direction[]
		{
			Direction.Up,
			Direction.Down
		};

		// Token: 0x04000F04 RID: 3844
		private static readonly IReadOnlyCollection<Direction> HorizontalDirections = new Direction[]
		{
			Direction.Left,
			Direction.Right
		};

		// Token: 0x04000F05 RID: 3845
		public static readonly IReadOnlyCollection<Axis> Axes;
	}
}
