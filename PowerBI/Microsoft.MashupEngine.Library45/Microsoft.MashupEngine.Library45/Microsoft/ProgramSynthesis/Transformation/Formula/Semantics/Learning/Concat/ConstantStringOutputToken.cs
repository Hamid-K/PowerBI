using System;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Concat
{
	// Token: 0x0200176F RID: 5999
	public class ConstantStringOutputToken : OutputToken, IEquatable<ConstantStringOutputToken>
	{
		// Token: 0x170021CD RID: 8653
		// (get) Token: 0x0600C6E9 RID: 50921 RVA: 0x002AC224 File Offset: 0x002AA424
		public bool AllDelimiters
		{
			get
			{
				bool flag = this._allDelimiters.GetValueOrDefault();
				if (this._allDelimiters == null)
				{
					flag = base.Output.All((char c) => c.IsDelimiter());
					this._allDelimiters = new bool?(flag);
					return flag;
				}
				return flag;
			}
		}

		// Token: 0x170021CE RID: 8654
		// (get) Token: 0x0600C6EA RID: 50922 RVA: 0x002AC284 File Offset: 0x002AA484
		// (set) Token: 0x0600C6EB RID: 50923 RVA: 0x002AC28C File Offset: 0x002AA48C
		public DynamicOutputToken NextToken { get; set; }

		// Token: 0x0600C6EC RID: 50924 RVA: 0x002AC295 File Offset: 0x002AA495
		public override bool Equals(object other)
		{
			return this.Equals(other as ConstantStringOutputToken);
		}

		// Token: 0x0600C6ED RID: 50925 RVA: 0x002AC2A3 File Offset: 0x002AA4A3
		public bool Equals(ConstantStringOutputToken other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600C6EE RID: 50926 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600C6EF RID: 50927 RVA: 0x002AC2C4 File Offset: 0x002AA4C4
		public override bool IsCompatible(OutputToken other)
		{
			ConstantStringOutputToken constantStringOutputToken = other as ConstantStringOutputToken;
			return constantStringOutputToken != null && base.Output == constantStringOutputToken.Output;
		}

		// Token: 0x0600C6F0 RID: 50928 RVA: 0x002AC2EE File Offset: 0x002AA4EE
		public static bool operator ==(ConstantStringOutputToken left, ConstantStringOutputToken right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600C6F1 RID: 50929 RVA: 0x002AC304 File Offset: 0x002AA504
		public static bool operator !=(ConstantStringOutputToken left, ConstantStringOutputToken right)
		{
			return !(left == right);
		}

		// Token: 0x0600C6F2 RID: 50930 RVA: 0x002AC310 File Offset: 0x002AA510
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = base.ToString() + ": " + base.Output.ToCSharpPseudoLiteral());
			}
			return text;
		}

		// Token: 0x04004E2B RID: 20011
		private bool? _allDelimiters;

		// Token: 0x04004E2C RID: 20012
		private string _toString;
	}
}
