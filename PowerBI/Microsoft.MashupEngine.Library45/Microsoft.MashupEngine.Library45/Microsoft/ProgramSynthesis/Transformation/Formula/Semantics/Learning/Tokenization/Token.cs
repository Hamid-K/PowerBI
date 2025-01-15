using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Tokenization
{
	// Token: 0x020016BF RID: 5823
	public class Token : IEquatable<Token>
	{
		// Token: 0x170020EF RID: 8431
		// (get) Token: 0x0600C22D RID: 49709 RVA: 0x0029D95C File Offset: 0x0029BB5C
		// (set) Token: 0x0600C22E RID: 49710 RVA: 0x0029D964 File Offset: 0x0029BB64
		public bool Covered
		{
			get
			{
				return this._covered;
			}
			set
			{
				this._toString = null;
				this._covered = value;
			}
		}

		// Token: 0x170020F0 RID: 8432
		// (get) Token: 0x0600C22F RID: 49711 RVA: 0x0029D974 File Offset: 0x0029BB74
		public int EndIndex
		{
			get
			{
				int num = this._endIndex.GetValueOrDefault();
				if (this._endIndex == null)
				{
					num = this.StartIndex + this.Value.Length - 1;
					this._endIndex = new int?(num);
					return num;
				}
				return num;
			}
		}

		// Token: 0x170020F1 RID: 8433
		// (get) Token: 0x0600C230 RID: 49712 RVA: 0x0029D9BE File Offset: 0x0029BBBE
		// (set) Token: 0x0600C231 RID: 49713 RVA: 0x0029D9C6 File Offset: 0x0029BBC6
		public TokenKind Kind
		{
			get
			{
				return this._kind;
			}
			set
			{
				this._toString = null;
				this._kind = value;
			}
		}

		// Token: 0x170020F2 RID: 8434
		// (get) Token: 0x0600C232 RID: 49714 RVA: 0x0029D9D6 File Offset: 0x0029BBD6
		// (set) Token: 0x0600C233 RID: 49715 RVA: 0x0029D9DE File Offset: 0x0029BBDE
		public int StartIndex { get; set; }

		// Token: 0x170020F3 RID: 8435
		// (get) Token: 0x0600C234 RID: 49716 RVA: 0x0029D9E7 File Offset: 0x0029BBE7
		// (set) Token: 0x0600C235 RID: 49717 RVA: 0x0029D9EF File Offset: 0x0029BBEF
		public string Value { get; set; }

		// Token: 0x0600C236 RID: 49718 RVA: 0x0029D9F8 File Offset: 0x0029BBF8
		public bool Equals(Token other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600C237 RID: 49719 RVA: 0x0029DA16 File Offset: 0x0029BC16
		public override bool Equals(object other)
		{
			return this.Equals(other as Token);
		}

		// Token: 0x0600C238 RID: 49720 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600C239 RID: 49721 RVA: 0x0029DA24 File Offset: 0x0029BC24
		public static bool operator ==(Token left, Token right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C23A RID: 49722 RVA: 0x0029DA3A File Offset: 0x0029BC3A
		public static bool operator !=(Token left, Token right)
		{
			return !(left == right);
		}

		// Token: 0x0600C23B RID: 49723 RVA: 0x0029DA48 File Offset: 0x0029BC48
		public override string ToString()
		{
			string text = (this.Covered ? "✓" : "  ");
			string text2 = ("\"" + this.Value + "\"").PadRight(10);
			string text3;
			if ((text3 = this._toString) == null)
			{
				text3 = (this._toString = string.Format("[{0,2}..{1,2}] {2} {3,-10}: {4}", new object[] { this.StartIndex, this.EndIndex, text, this.Kind, text2 }));
			}
			return text3;
		}

		// Token: 0x04004B49 RID: 19273
		private bool _covered;

		// Token: 0x04004B4A RID: 19274
		private int? _endIndex;

		// Token: 0x04004B4B RID: 19275
		private TokenKind _kind;

		// Token: 0x04004B4C RID: 19276
		private string _toString;
	}
}
