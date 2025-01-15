using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary
{
	// Token: 0x02000810 RID: 2064
	public struct PositionMatch
	{
		// Token: 0x06002C89 RID: 11401 RVA: 0x0007D8A4 File Offset: 0x0007BAA4
		public PositionMatch(uint position, uint length)
		{
			this.Position = position;
			this.Length = length;
			this.Right = position + length;
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x0007D8C0 File Offset: 0x0007BAC0
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("PositionMatch(position={0}, length={1}, right={2})", new object[] { this.Position, this.Length, this.Right }));
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x0007D90C File Offset: 0x0007BB0C
		public static Optional<PositionMatch> From(Match match)
		{
			if (!match.Success)
			{
				return Optional<PositionMatch>.Nothing;
			}
			return new PositionMatch((uint)match.Index, (uint)match.Length).Some<PositionMatch>();
		}

		// Token: 0x04001549 RID: 5449
		public readonly uint Position;

		// Token: 0x0400154A RID: 5450
		public readonly uint Length;

		// Token: 0x0400154B RID: 5451
		public readonly uint Right;
	}
}
