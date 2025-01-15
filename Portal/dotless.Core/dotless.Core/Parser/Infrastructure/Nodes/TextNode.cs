using System;

namespace dotless.Core.Parser.Infrastructure.Nodes
{
	// Token: 0x02000062 RID: 98
	public class TextNode : Node, IComparable
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00015403 File Offset: 0x00013603
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x0001540B File Offset: 0x0001360B
		public string Value { get; set; }

		// Token: 0x06000444 RID: 1092 RVA: 0x00015414 File Offset: 0x00013614
		public TextNode(string contents)
		{
			this.Value = contents;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00015423 File Offset: 0x00013623
		public static TextNode operator &(TextNode n1, TextNode n2)
		{
			if (n1 == null)
			{
				return null;
			}
			return n2;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001542B File Offset: 0x0001362B
		public static TextNode operator |(TextNode n1, TextNode n2)
		{
			return n1 ?? n2;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00015433 File Offset: 0x00013633
		protected override Node CloneCore()
		{
			return new TextNode(this.Value);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00015440 File Offset: 0x00013640
		public override void AppendCSS(Env env)
		{
			env.Output.Append(env.Compress ? this.Value.Trim() : this.Value);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00015469 File Offset: 0x00013669
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00015471 File Offset: 0x00013671
		public virtual int CompareTo(object obj)
		{
			if (obj == null)
			{
				return -1;
			}
			return obj.ToString().CompareTo(this.ToString());
		}
	}
}
