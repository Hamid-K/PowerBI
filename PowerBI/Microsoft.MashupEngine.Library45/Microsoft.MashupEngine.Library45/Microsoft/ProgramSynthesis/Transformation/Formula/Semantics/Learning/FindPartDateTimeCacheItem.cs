using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x0200166D RID: 5741
	public class FindPartDateTimeCacheItem : IEquatable<FindPartDateTimeCacheItem>
	{
		// Token: 0x170020C7 RID: 8391
		// (get) Token: 0x0600BFF5 RID: 49141 RVA: 0x0029624A File Offset: 0x0029444A
		// (set) Token: 0x0600BFF6 RID: 49142 RVA: 0x00296252 File Offset: 0x00294452
		public string ColumnName { get; set; }

		// Token: 0x170020C8 RID: 8392
		// (get) Token: 0x0600BFF7 RID: 49143 RVA: 0x0029625B File Offset: 0x0029445B
		// (set) Token: 0x0600BFF8 RID: 49144 RVA: 0x00296263 File Offset: 0x00294463
		public DateTimePartKind Kind { get; set; }

		// Token: 0x170020C9 RID: 8393
		// (get) Token: 0x0600BFF9 RID: 49145 RVA: 0x0029626C File Offset: 0x0029446C
		// (set) Token: 0x0600BFFA RID: 49146 RVA: 0x00296274 File Offset: 0x00294474
		public DateTime Value { get; set; }

		// Token: 0x0600BFFB RID: 49147 RVA: 0x0029627D File Offset: 0x0029447D
		public bool Equals(FindPartDateTimeCacheItem other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600BFFC RID: 49148 RVA: 0x0029629B File Offset: 0x0029449B
		public override bool Equals(object other)
		{
			return this.Equals(other as FindPartDateTimeCacheItem);
		}

		// Token: 0x0600BFFD RID: 49149 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600BFFE RID: 49150 RVA: 0x002962A9 File Offset: 0x002944A9
		public static bool operator ==(FindPartDateTimeCacheItem left, FindPartDateTimeCacheItem right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600BFFF RID: 49151 RVA: 0x002962BF File Offset: 0x002944BF
		public static bool operator !=(FindPartDateTimeCacheItem left, FindPartDateTimeCacheItem right)
		{
			return !(left == right);
		}

		// Token: 0x0600C000 RID: 49152 RVA: 0x002962CC File Offset: 0x002944CC
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = string.Format("{0}|{1}|{2:s}", this.ColumnName, this.Kind, this.Value));
			}
			return text;
		}

		// Token: 0x04004A13 RID: 18963
		private string _toString;
	}
}
