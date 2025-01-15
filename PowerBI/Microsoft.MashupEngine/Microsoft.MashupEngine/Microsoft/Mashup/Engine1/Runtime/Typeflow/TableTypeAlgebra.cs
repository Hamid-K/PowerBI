using System;

namespace Microsoft.Mashup.Engine1.Runtime.Typeflow
{
	// Token: 0x020016E5 RID: 5861
	public static class TableTypeAlgebra
	{
		// Token: 0x060094ED RID: 38125 RVA: 0x001EBC74 File Offset: 0x001E9E74
		public static TableTypeAlgebra.JoinKind GetJoinKind(Value joinKind)
		{
			if (joinKind.Equals(Library.JoinKind.Inner))
			{
				return TableTypeAlgebra.JoinKind.Inner;
			}
			if (joinKind.Equals(Library.JoinKind.LeftOuter))
			{
				return TableTypeAlgebra.JoinKind.LeftOuter;
			}
			if (joinKind.Equals(Library.JoinKind.RightOuter))
			{
				return TableTypeAlgebra.JoinKind.RightOuter;
			}
			if (joinKind.Equals(Library.JoinKind.FullOuter))
			{
				return TableTypeAlgebra.JoinKind.FullOuter;
			}
			if (joinKind.Equals(Library.JoinKind.LeftAnti))
			{
				return TableTypeAlgebra.JoinKind.LeftAnti;
			}
			if (joinKind.Equals(Library.JoinKind.RightAnti))
			{
				return TableTypeAlgebra.JoinKind.RightAnti;
			}
			if (joinKind.Equals(Library.JoinKind.LeftSemi))
			{
				return TableTypeAlgebra.JoinKind.LeftSemi;
			}
			if (joinKind.Equals(Library.JoinKind.RightSemi))
			{
				return TableTypeAlgebra.JoinKind.RightSemi;
			}
			throw ValueException.NewExpressionError<Message0>(Strings.InvalidJoinKind, joinKind, null);
		}

		// Token: 0x060094EE RID: 38126 RVA: 0x001EBD08 File Offset: 0x001E9F08
		public static Value GetValue(TableTypeAlgebra.JoinKind joinKind)
		{
			switch (joinKind)
			{
			case TableTypeAlgebra.JoinKind.Inner:
				return Library.JoinKind.Inner;
			case TableTypeAlgebra.JoinKind.LeftOuter:
				return Library.JoinKind.LeftOuter;
			case TableTypeAlgebra.JoinKind.FullOuter:
				return Library.JoinKind.FullOuter;
			case TableTypeAlgebra.JoinKind.RightOuter:
				return Library.JoinKind.RightOuter;
			case TableTypeAlgebra.JoinKind.LeftAnti:
				return Library.JoinKind.LeftAnti;
			case TableTypeAlgebra.JoinKind.RightAnti:
				return Library.JoinKind.RightAnti;
			case TableTypeAlgebra.JoinKind.LeftSemi:
				return Library.JoinKind.LeftSemi;
			case TableTypeAlgebra.JoinKind.RightSemi:
				return Library.JoinKind.RightSemi;
			default:
				throw ValueException.NewExpressionError<Message0>(Strings.InvalidJoinKind, null, null);
			}
		}

		// Token: 0x020016E6 RID: 5862
		public enum JoinKind
		{
			// Token: 0x04004F34 RID: 20276
			Inner,
			// Token: 0x04004F35 RID: 20277
			LeftOuter,
			// Token: 0x04004F36 RID: 20278
			FullOuter,
			// Token: 0x04004F37 RID: 20279
			RightOuter,
			// Token: 0x04004F38 RID: 20280
			LeftAnti,
			// Token: 0x04004F39 RID: 20281
			RightAnti,
			// Token: 0x04004F3A RID: 20282
			LeftSemi,
			// Token: 0x04004F3B RID: 20283
			RightSemi
		}
	}
}
