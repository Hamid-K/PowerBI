using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x0200166B RID: 5739
	public class FindDateTimeCacheItem : IEquatable<FindDateTimeCacheItem>
	{
		// Token: 0x170020C3 RID: 8387
		// (get) Token: 0x0600BFE4 RID: 49124 RVA: 0x002960FE File Offset: 0x002942FE
		// (set) Token: 0x0600BFE5 RID: 49125 RVA: 0x00296106 File Offset: 0x00294306
		public int StartIndex { get; set; }

		// Token: 0x170020C4 RID: 8388
		// (get) Token: 0x0600BFE6 RID: 49126 RVA: 0x00296110 File Offset: 0x00294310
		public int EndIndex
		{
			get
			{
				int num = this._endIndex.GetValueOrDefault();
				if (this._endIndex == null)
				{
					num = this.StartIndex + this.Substring.Length - 1;
					this._endIndex = new int?(num);
					return num;
				}
				return num;
			}
		}

		// Token: 0x170020C5 RID: 8389
		// (get) Token: 0x0600BFE7 RID: 49127 RVA: 0x0029615A File Offset: 0x0029435A
		// (set) Token: 0x0600BFE8 RID: 49128 RVA: 0x00296162 File Offset: 0x00294362
		public string Substring { get; set; }

		// Token: 0x170020C6 RID: 8390
		// (get) Token: 0x0600BFE9 RID: 49129 RVA: 0x0029616B File Offset: 0x0029436B
		// (set) Token: 0x0600BFEA RID: 49130 RVA: 0x00296173 File Offset: 0x00294373
		public IEnumerable<DateTime> Values { get; set; }

		// Token: 0x0600BFEB RID: 49131 RVA: 0x0029617C File Offset: 0x0029437C
		public bool Equals(FindDateTimeCacheItem other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600BFEC RID: 49132 RVA: 0x0029619A File Offset: 0x0029439A
		public override bool Equals(object other)
		{
			return this.Equals(other as FindDateTimeCacheItem);
		}

		// Token: 0x0600BFED RID: 49133 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600BFEE RID: 49134 RVA: 0x002961A8 File Offset: 0x002943A8
		public static bool operator ==(FindDateTimeCacheItem left, FindDateTimeCacheItem right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600BFEF RID: 49135 RVA: 0x002961BE File Offset: 0x002943BE
		public static bool operator !=(FindDateTimeCacheItem left, FindDateTimeCacheItem right)
		{
			return !(left == right);
		}

		// Token: 0x0600BFF0 RID: 49136 RVA: 0x002961CC File Offset: 0x002943CC
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = this.Substring + "|" + string.Join(",", this.Values.Select((DateTime n) => n.ToString("s"))));
			}
			return text;
		}

		// Token: 0x04004A0C RID: 18956
		private string _toString;

		// Token: 0x04004A0D RID: 18957
		private int? _endIndex;
	}
}
