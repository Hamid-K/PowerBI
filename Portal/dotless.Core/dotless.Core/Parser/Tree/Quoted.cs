using System;
using System.Text;
using System.Text.RegularExpressions;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x02000044 RID: 68
	public class Quoted : TextNode
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000C756 File Offset: 0x0000A956
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000C75E File Offset: 0x0000A95E
		public char? Quote { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000C767 File Offset: 0x0000A967
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000C76F File Offset: 0x0000A96F
		public bool Escaped { get; set; }

		// Token: 0x060002A3 RID: 675 RVA: 0x0000C778 File Offset: 0x0000A978
		public Quoted(string value, char? quote)
			: base(value)
		{
			this.Quote = quote;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000C798 File Offset: 0x0000A998
		public Quoted(string value, char? quote, bool escaped)
			: base(value)
		{
			this.Escaped = escaped;
			this.Quote = quote;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000C7BF File Offset: 0x0000A9BF
		public Quoted(string value, string contents, bool escaped)
			: base(contents)
		{
			this.Escaped = escaped;
			this.Quote = new char?(value[0]);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000C7F4 File Offset: 0x0000A9F4
		public Quoted(string value, bool escaped)
			: base(value)
		{
			this.Escaped = escaped;
			this.Quote = null;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000C82E File Offset: 0x0000AA2E
		protected override Node CloneCore()
		{
			return new Quoted(base.Value, this.Quote, this.Escaped);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000C847 File Offset: 0x0000AA47
		public override void AppendCSS(Env env)
		{
			env.Output.Append(this.RenderString());
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000C85C File Offset: 0x0000AA5C
		public StringBuilder RenderString()
		{
			if (this.Escaped)
			{
				return new StringBuilder(this.UnescapeContents());
			}
			return new StringBuilder().Append(this.Quote).Append(base.Value).Append(this.Quote);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000C8AD File Offset: 0x0000AAAD
		public override string ToString()
		{
			return this.RenderString().ToString();
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000C8BC File Offset: 0x0000AABC
		public override Node Evaluate(Env env)
		{
			return new Quoted(Regex.Replace(base.Value, "@\\{([\\w-]+)\\}", delegate(Match m)
			{
				Node node = new Variable("@" + m.Groups[1].Value)
				{
					Location = new NodeLocation(this.Location.Index + m.Index, this.Location.Source, this.Location.FileName)
				}.Evaluate(env);
				if (!(node is TextNode))
				{
					return node.ToCSS(env);
				}
				return (node as TextNode).Value;
			}), this.Quote, this.Escaped).ReducedFrom<Quoted>(new Node[] { this });
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000C919 File Offset: 0x0000AB19
		public string UnescapeContents()
		{
			return this._unescape.Replace(base.Value, "$1$2");
		}

		// Token: 0x0400009D RID: 157
		private readonly Regex _unescape = new Regex("(^|[^\\\\])\\\\(['\"])");
	}
}
