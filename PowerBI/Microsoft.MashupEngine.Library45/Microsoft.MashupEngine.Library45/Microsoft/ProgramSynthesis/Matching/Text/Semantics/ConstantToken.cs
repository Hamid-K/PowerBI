using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Microsoft.ProgramSynthesis.Matching.Text.Semantics
{
	// Token: 0x0200122D RID: 4653
	public class ConstantToken : Token
	{
		// Token: 0x06008C20 RID: 35872 RVA: 0x001D5A18 File Offset: 0x001D3C18
		public ConstantToken(string str, double? score = null)
			: base(FormattableString.Invariant(FormattableStringFactory.Create("Const[{0}]", new object[] { str })), score ?? (-1.0 / (double)(str.Length * str.Length)), null)
		{
			this.Constant = str;
		}

		// Token: 0x170017F8 RID: 6136
		// (get) Token: 0x06008C21 RID: 35873 RVA: 0x001D5A78 File Offset: 0x001D3C78
		public string Constant { get; }

		// Token: 0x170017F9 RID: 6137
		// (get) Token: 0x06008C22 RID: 35874 RVA: 0x001D5A80 File Offset: 0x001D3C80
		public int Length
		{
			get
			{
				return this.Constant.Length;
			}
		}

		// Token: 0x06008C23 RID: 35875 RVA: 0x001D5A8D File Offset: 0x001D3C8D
		public override uint PrefixMatchLength(string target)
		{
			if (target == null || !target.StartsWith(this.Constant, StringComparison.Ordinal))
			{
				return 0U;
			}
			return Convert.ToUInt32(this.Constant.Length);
		}

		// Token: 0x06008C24 RID: 35876 RVA: 0x001D5AB3 File Offset: 0x001D3CB3
		public override string TryGetRegexPattern()
		{
			return Regex.Escape(this.Constant);
		}

		// Token: 0x06008C25 RID: 35877 RVA: 0x001D5AC0 File Offset: 0x001D3CC0
		public override XElement RenderXML()
		{
			return new XElement("ConstantToken", this.Constant);
		}

		// Token: 0x06008C26 RID: 35878 RVA: 0x001D5AD8 File Offset: 0x001D3CD8
		protected internal static ConstantToken Parse(XElement node)
		{
			if (node.Name != "ConstantToken")
			{
				return null;
			}
			return new ConstantToken(node.Value, null);
		}
	}
}
