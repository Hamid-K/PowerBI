using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016D9 RID: 5849
	public class OutputSuggestion : IOutputSuggestion, IEquatable<IOutputSuggestion>, IComparable<IOutputSuggestion>
	{
		// Token: 0x17002132 RID: 8498
		// (get) Token: 0x0600C318 RID: 49944 RVA: 0x002A0731 File Offset: 0x0029E931
		// (set) Token: 0x0600C319 RID: 49945 RVA: 0x002A0739 File Offset: 0x0029E939
		public Guid Id { get; set; } = Guid.NewGuid();

		// Token: 0x17002133 RID: 8499
		// (get) Token: 0x0600C31A RID: 49946 RVA: 0x002A0742 File Offset: 0x0029E942
		// (set) Token: 0x0600C31B RID: 49947 RVA: 0x002A074A File Offset: 0x0029E94A
		public double Score { get; set; }

		// Token: 0x17002134 RID: 8500
		// (get) Token: 0x0600C31C RID: 49948 RVA: 0x002A0753 File Offset: 0x0029E953
		// (set) Token: 0x0600C31D RID: 49949 RVA: 0x002A075B File Offset: 0x0029E95B
		public string Text { get; set; }

		// Token: 0x0600C31E RID: 49950 RVA: 0x002A0764 File Offset: 0x0029E964
		public int CompareTo(IOutputSuggestion other)
		{
			return string.CompareOrdinal(this.Text, other.Text);
		}

		// Token: 0x0600C31F RID: 49951 RVA: 0x002A0777 File Offset: 0x0029E977
		public bool Equals(IOutputSuggestion other)
		{
			return other != null && this.Text == other.Text;
		}

		// Token: 0x0600C320 RID: 49952 RVA: 0x002A078F File Offset: 0x0029E98F
		public override bool Equals(object other)
		{
			return this.Equals(other as OutputSuggestion);
		}

		// Token: 0x0600C321 RID: 49953 RVA: 0x002A079D File Offset: 0x0029E99D
		public override int GetHashCode()
		{
			return this.Text.GetHashCode();
		}

		// Token: 0x0600C322 RID: 49954 RVA: 0x002A07AA File Offset: 0x0029E9AA
		public static bool operator ==(OutputSuggestion left, IOutputSuggestion right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C323 RID: 49955 RVA: 0x002A07C0 File Offset: 0x0029E9C0
		public static bool operator !=(OutputSuggestion left, IOutputSuggestion right)
		{
			return !(left == right);
		}

		// Token: 0x0600C324 RID: 49956 RVA: 0x002A07CC File Offset: 0x0029E9CC
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = string.Format("[{0,7:F2}] {1}", this.Score, this.Text));
			}
			return text;
		}

		// Token: 0x04004BEF RID: 19439
		private string _toString;
	}
}
