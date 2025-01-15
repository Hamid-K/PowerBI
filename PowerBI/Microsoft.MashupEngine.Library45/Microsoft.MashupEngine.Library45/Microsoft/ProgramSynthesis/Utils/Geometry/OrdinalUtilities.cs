using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005E5 RID: 1509
	public static class OrdinalUtilities
	{
		// Token: 0x06002091 RID: 8337 RVA: 0x0005CABD File Offset: 0x0005ACBD
		public static Direction Horizontal(this Ordinal ordinal)
		{
			switch (ordinal)
			{
			case Ordinal.TopLeft:
			case Ordinal.BottomLeft:
				return Direction.Left;
			case Ordinal.TopRight:
			case Ordinal.BottomRight:
				return Direction.Right;
			default:
				throw new ArgumentException("ordinal", string.Format("Invalid ordinal: {0}", ordinal));
			}
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x0005CAF5 File Offset: 0x0005ACF5
		public static Direction Vertical(this Ordinal ordinal)
		{
			if (ordinal <= Ordinal.TopRight)
			{
				return Direction.Up;
			}
			if (ordinal - Ordinal.BottomRight > 1)
			{
				throw new ArgumentException("ordinal", string.Format("Invalid ordinal: {0}", ordinal));
			}
			return Direction.Down;
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x0005C5D3 File Offset: 0x0005A7D3
		public static Direction Relative(this Ordinal ordinal, Direction relativeDirection)
		{
			return (Direction)((ordinal + (int)relativeDirection) % (Ordinal)4);
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x0005C5D3 File Offset: 0x0005A7D3
		public static Ordinal Relative(this Ordinal ordinal, Ordinal relativeOrdinal)
		{
			return (ordinal + (int)relativeOrdinal) % (Ordinal)4;
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x00004FAE File Offset: 0x000031AE
		public static Ordinal TopLeft(this Ordinal ordinal)
		{
			return ordinal;
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x0005CB21 File Offset: 0x0005AD21
		public static Ordinal TopRight(this Ordinal ordinal)
		{
			return ordinal.Relative(Ordinal.TopRight);
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x0005CB2A File Offset: 0x0005AD2A
		public static Ordinal BottomRight(this Ordinal ordinal)
		{
			return ordinal.Relative(Ordinal.BottomRight);
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x0005CB33 File Offset: 0x0005AD33
		public static Ordinal BottomLeft(this Ordinal ordinal)
		{
			return ordinal.Relative(Ordinal.BottomLeft);
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x0005CB3C File Offset: 0x0005AD3C
		public static Direction Up(this Ordinal ordinal)
		{
			return ordinal.Relative(Direction.Up);
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x0005CB45 File Offset: 0x0005AD45
		public static Direction Right(this Ordinal ordinal)
		{
			return ordinal.Relative(Direction.Right);
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x0005CB4E File Offset: 0x0005AD4E
		public static Direction Down(this Ordinal ordinal)
		{
			return ordinal.Relative(Direction.Down);
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x0005CB57 File Offset: 0x0005AD57
		public static Direction Left(this Ordinal ordinal)
		{
			return ordinal.Relative(Direction.Left);
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x0005CB3C File Offset: 0x0005AD3C
		public static Direction ClockwiseDirection(this Ordinal ordinal)
		{
			return ordinal.Relative(Direction.Up);
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x0005CB57 File Offset: 0x0005AD57
		public static Direction CounterClockwiseDirection(this Ordinal ordinal)
		{
			return ordinal.Relative(Direction.Left);
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x0005CB21 File Offset: 0x0005AD21
		public static Ordinal ClockwiseOrdinal(this Ordinal ordinal)
		{
			return ordinal.Relative(Ordinal.TopRight);
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x0005CB33 File Offset: 0x0005AD33
		public static Ordinal CounterClockwiseOrdinal(this Ordinal ordinal)
		{
			return ordinal.Relative(Ordinal.BottomLeft);
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x0005CB2A File Offset: 0x0005AD2A
		public static Ordinal Opposite(this Ordinal ordinal)
		{
			return ordinal.Relative(Ordinal.BottomRight);
		}

		// Token: 0x04000F9B RID: 3995
		public static readonly IReadOnlyCollection<Ordinal> Ordinals = new Ordinal[]
		{
			Ordinal.TopLeft,
			Ordinal.TopRight,
			Ordinal.BottomRight,
			Ordinal.BottomLeft
		};
	}
}
