using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x02001673 RID: 5747
	public class FormattedNumberCacheItem : IEquatable<FormattedNumberCacheItem>
	{
		// Token: 0x170020DB RID: 8411
		// (get) Token: 0x0600C02B RID: 49195 RVA: 0x002964E7 File Offset: 0x002946E7
		// (set) Token: 0x0600C02C RID: 49196 RVA: 0x002964EF File Offset: 0x002946EF
		public int Scale { get; set; }

		// Token: 0x170020DC RID: 8412
		// (get) Token: 0x0600C02D RID: 49197 RVA: 0x002964F8 File Offset: 0x002946F8
		// (set) Token: 0x0600C02E RID: 49198 RVA: 0x00296500 File Offset: 0x00294700
		public decimal Value { get; set; }

		// Token: 0x0600C02F RID: 49199 RVA: 0x00296509 File Offset: 0x00294709
		public bool Equals(FormattedNumberCacheItem other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600C030 RID: 49200 RVA: 0x00296527 File Offset: 0x00294727
		public override bool Equals(object other)
		{
			return this.Equals(other as FormattedNumberCacheItem);
		}

		// Token: 0x0600C031 RID: 49201 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600C032 RID: 49202 RVA: 0x00296535 File Offset: 0x00294735
		public static bool operator ==(FormattedNumberCacheItem left, FormattedNumberCacheItem right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C033 RID: 49203 RVA: 0x0029654B File Offset: 0x0029474B
		public static bool operator !=(FormattedNumberCacheItem left, FormattedNumberCacheItem right)
		{
			return !(left == right);
		}

		// Token: 0x0600C034 RID: 49204 RVA: 0x00296558 File Offset: 0x00294758
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = string.Format("{0}   Scale: {1}", this.Value, this.Scale));
			}
			return text;
		}

		// Token: 0x04004A28 RID: 18984
		private string _toString;
	}
}
