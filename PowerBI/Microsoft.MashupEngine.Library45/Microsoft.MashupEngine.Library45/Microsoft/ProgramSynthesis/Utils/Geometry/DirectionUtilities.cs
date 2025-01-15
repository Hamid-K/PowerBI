using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005D7 RID: 1495
	public static class DirectionUtilities
	{
		// Token: 0x06002045 RID: 8261 RVA: 0x0005C54D File Offset: 0x0005A74D
		public static Derivative Derivative(this Direction dir)
		{
			switch (dir)
			{
			case Direction.Up:
			case Direction.Left:
				return Microsoft.ProgramSynthesis.Utils.Geometry.Derivative.Decreasing;
			case Direction.Right:
			case Direction.Down:
				return Microsoft.ProgramSynthesis.Utils.Geometry.Derivative.Increasing;
			default:
				throw new ArgumentException("dir", string.Format("Invalid direction: {0}", dir));
			}
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x0005C585 File Offset: 0x0005A785
		public static Axis AlignedAxis(this Direction dir)
		{
			switch (dir)
			{
			case Direction.Up:
			case Direction.Down:
				return Axis.Vertical;
			case Direction.Right:
			case Direction.Left:
				return Axis.Horizontal;
			default:
				throw new ArgumentException("dir", string.Format("Invalid direction: {0}", dir));
			}
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x0005C5BD File Offset: 0x0005A7BD
		public static bool IsHorizontal(this Direction dir)
		{
			return dir.AlignedAxis() == Axis.Horizontal;
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x0005C5C8 File Offset: 0x0005A7C8
		public static bool IsVertical(this Direction dir)
		{
			return dir.AlignedAxis() == Axis.Vertical;
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x00004FAE File Offset: 0x000031AE
		public static int Value(this Derivative derivative)
		{
			return (int)derivative;
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x0005C5D3 File Offset: 0x0005A7D3
		public static Direction Relative(this Direction direction, Direction relativeDirection)
		{
			return (direction + (int)relativeDirection) % (Direction)4;
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x0005C5D3 File Offset: 0x0005A7D3
		public static Ordinal Relative(this Direction direction, Ordinal relativeOrdinal)
		{
			return (Ordinal)((direction + (int)relativeOrdinal) % (Direction)4);
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x00004FAE File Offset: 0x000031AE
		public static Direction Up(this Direction direction)
		{
			return direction;
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x0005C5DA File Offset: 0x0005A7DA
		public static Direction Right(this Direction direction)
		{
			return direction.Relative(Direction.Right);
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x0005C5E3 File Offset: 0x0005A7E3
		public static Direction Down(this Direction direction)
		{
			return direction.Relative(Direction.Down);
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x0005C5EC File Offset: 0x0005A7EC
		public static Direction Left(this Direction direction)
		{
			return direction.Relative(Direction.Left);
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x0005C5F5 File Offset: 0x0005A7F5
		public static Ordinal TopLeft(this Direction direction)
		{
			return direction.Relative(Ordinal.TopLeft);
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x0005C5FE File Offset: 0x0005A7FE
		public static Ordinal TopRight(this Direction direction)
		{
			return direction.Relative(Ordinal.TopRight);
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x0005C607 File Offset: 0x0005A807
		public static Ordinal BottomRight(this Direction direction)
		{
			return direction.Relative(Ordinal.BottomRight);
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x0005C610 File Offset: 0x0005A810
		public static Ordinal BottomLeft(this Direction direction)
		{
			return direction.Relative(Ordinal.BottomLeft);
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x0005C5FE File Offset: 0x0005A7FE
		public static Ordinal ClockwiseOrdinal(this Direction dir)
		{
			return dir.Relative(Ordinal.TopRight);
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x0005C5F5 File Offset: 0x0005A7F5
		public static Ordinal CounterClockwiseOrdinal(this Direction dir)
		{
			return dir.Relative(Ordinal.TopLeft);
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x0005C5DA File Offset: 0x0005A7DA
		public static Direction ClockwiseDirection(this Direction dir)
		{
			return dir.Relative(Direction.Right);
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x0005C5EC File Offset: 0x0005A7EC
		public static Direction CounterClockwiseDirection(this Direction dir)
		{
			return dir.Relative(Direction.Left);
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x0005C5E3 File Offset: 0x0005A7E3
		public static Direction Opposite(this Direction dir)
		{
			return dir.Relative(Direction.Down);
		}

		// Token: 0x04000F8E RID: 3982
		public static readonly IReadOnlyCollection<Direction> Directions = new Direction[]
		{
			Direction.Up,
			Direction.Right,
			Direction.Down,
			Direction.Left
		};
	}
}
