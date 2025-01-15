using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000121 RID: 289
	public struct TextPosition
	{
		// Token: 0x060004F3 RID: 1267 RVA: 0x00007818 File Offset: 0x00005A18
		public TextPosition(int row, int column)
		{
			this.row = row;
			this.column = column;
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00007828 File Offset: 0x00005A28
		public int Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00007830 File Offset: 0x00005A30
		public int Row
		{
			get
			{
				return this.row;
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00007838 File Offset: 0x00005A38
		public override bool Equals(object obj)
		{
			return obj is TextPosition && this == (TextPosition)obj;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00007855 File Offset: 0x00005A55
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00007867 File Offset: 0x00005A67
		public static bool operator ==(TextPosition left, TextPosition right)
		{
			return left.column == right.column && left.row == right.row;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00007887 File Offset: 0x00005A87
		public static bool operator !=(TextPosition left, TextPosition right)
		{
			return !(left == right);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00007893 File Offset: 0x00005A93
		public static bool operator <(TextPosition left, TextPosition right)
		{
			return left.row < right.row || (left.row == right.row && left.column < right.column);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000078C3 File Offset: 0x00005AC3
		public static bool operator <=(TextPosition left, TextPosition right)
		{
			return left.row < right.row || (left.row == right.row && left.column <= right.column);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000078F6 File Offset: 0x00005AF6
		public static bool operator >(TextPosition left, TextPosition right)
		{
			return left.row > right.row || (left.row == right.row && left.column > right.column);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00007926 File Offset: 0x00005B26
		public static bool operator >=(TextPosition left, TextPosition right)
		{
			return left.row > right.row || (left.row == right.row && left.column >= right.column);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00007959 File Offset: 0x00005B59
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "({0}, {1})", this.row + 1, this.column + 1);
		}

		// Token: 0x040002CD RID: 717
		private readonly int row;

		// Token: 0x040002CE RID: 718
		private readonly int column;

		// Token: 0x02000122 RID: 290
		public sealed class Comparer : IComparer<TextPosition>
		{
			// Token: 0x060004FF RID: 1279 RVA: 0x000020FD File Offset: 0x000002FD
			private Comparer()
			{
			}

			// Token: 0x06000500 RID: 1280 RVA: 0x00007984 File Offset: 0x00005B84
			public int Compare(TextPosition x, TextPosition y)
			{
				if (x < y)
				{
					return -1;
				}
				if (x > y)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x040002CF RID: 719
			public static readonly IComparer<TextPosition> Instance = new TextPosition.Comparer();
		}
	}
}
