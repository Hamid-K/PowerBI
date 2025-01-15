using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016E9 RID: 5865
	public class SubstringFindProgramDescriptor : IEquatable<SubstringFindProgramDescriptor>
	{
		// Token: 0x17002155 RID: 8533
		// (get) Token: 0x0600C392 RID: 50066 RVA: 0x002A1801 File Offset: 0x0029FA01
		// (set) Token: 0x0600C393 RID: 50067 RVA: 0x002A1809 File Offset: 0x0029FA09
		public char Delimiter { get; set; }

		// Token: 0x17002156 RID: 8534
		// (get) Token: 0x0600C394 RID: 50068 RVA: 0x002A1812 File Offset: 0x0029FA12
		// (set) Token: 0x0600C395 RID: 50069 RVA: 0x002A181A File Offset: 0x0029FA1A
		public int DelimiterIndex { get; set; }

		// Token: 0x17002157 RID: 8535
		// (get) Token: 0x0600C396 RID: 50070 RVA: 0x002A1823 File Offset: 0x0029FA23
		// (set) Token: 0x0600C397 RID: 50071 RVA: 0x002A182B File Offset: 0x0029FA2B
		public int Instance { get; set; }

		// Token: 0x17002158 RID: 8536
		// (get) Token: 0x0600C398 RID: 50072 RVA: 0x002A1834 File Offset: 0x0029FA34
		// (set) Token: 0x0600C399 RID: 50073 RVA: 0x002A183C File Offset: 0x0029FA3C
		public int NegativeInstance { get; set; }

		// Token: 0x17002159 RID: 8537
		// (get) Token: 0x0600C39A RID: 50074 RVA: 0x002A1845 File Offset: 0x0029FA45
		// (set) Token: 0x0600C39B RID: 50075 RVA: 0x002A184D File Offset: 0x0029FA4D
		public int Offset { get; set; }

		// Token: 0x0600C39C RID: 50076 RVA: 0x002A1856 File Offset: 0x0029FA56
		public bool Equals(SubstringFindProgramDescriptor other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600C39D RID: 50077 RVA: 0x002A1874 File Offset: 0x0029FA74
		public override bool Equals(object other)
		{
			return this.Equals(other as SubstringFindProgramDescriptor);
		}

		// Token: 0x0600C39E RID: 50078 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600C39F RID: 50079 RVA: 0x002A1882 File Offset: 0x0029FA82
		public static bool operator ==(SubstringFindProgramDescriptor left, SubstringFindProgramDescriptor right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C3A0 RID: 50080 RVA: 0x002A1898 File Offset: 0x0029FA98
		public static bool operator !=(SubstringFindProgramDescriptor left, SubstringFindProgramDescriptor right)
		{
			return !(left == right);
		}

		// Token: 0x0600C3A1 RID: 50081 RVA: 0x002A18A4 File Offset: 0x0029FAA4
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = string.Format("({0}, {{{1},{2}}}, {3})", new object[]
				{
					this.Delimiter.ToCSharpPseudoLiteral(),
					this.Instance,
					this.NegativeInstance,
					this.Offset
				}));
			}
			return text;
		}

		// Token: 0x04004C30 RID: 19504
		private string _toString;
	}
}
