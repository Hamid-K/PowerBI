using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x02000035 RID: 53
	public class Element : Node
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000A33E File Offset: 0x0000853E
		// (set) Token: 0x060001FE RID: 510 RVA: 0x0000A346 File Offset: 0x00008546
		public Combinator Combinator { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000A34F File Offset: 0x0000854F
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0000A357 File Offset: 0x00008557
		public string Value { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000A360 File Offset: 0x00008560
		// (set) Token: 0x06000202 RID: 514 RVA: 0x0000A368 File Offset: 0x00008568
		public Node NodeValue { get; set; }

		// Token: 0x06000203 RID: 515 RVA: 0x0000A371 File Offset: 0x00008571
		public Element(Combinator combinator, string textValue)
			: this(combinator)
		{
			this.Value = textValue.Trim();
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000A388 File Offset: 0x00008588
		public Element(Combinator combinator, Node value)
			: this(combinator)
		{
			TextNode textNode;
			if ((textNode = value as TextNode) != null && !(value is Quoted))
			{
				this.Value = textNode.Value.Trim();
				return;
			}
			this.NodeValue = value;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000A3C7 File Offset: 0x000085C7
		private Element(Combinator combinator)
		{
			this.Combinator = combinator ?? new Combinator("");
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000A3E4 File Offset: 0x000085E4
		public override Node Evaluate(Env env)
		{
			if (this.NodeValue != null)
			{
				Node node = this.NodeValue.Evaluate(env);
				return new Element(this.Combinator, node).ReducedFrom<Element>(new Node[] { this });
			}
			return this;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000A424 File Offset: 0x00008624
		protected override Node CloneCore()
		{
			if (this.NodeValue != null)
			{
				return new Element((Combinator)this.Combinator.Clone(), this.NodeValue.Clone());
			}
			return new Element((Combinator)this.Combinator.Clone(), this.Value);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000A478 File Offset: 0x00008678
		public override void AppendCSS(Env env)
		{
			env.Output.Append(this.Combinator).Push();
			if (this.NodeValue != null)
			{
				env.Output.Append(this.NodeValue).Trim();
			}
			else
			{
				env.Output.Append(this.Value);
			}
			env.Output.PopAndAppend();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000A4DB File Offset: 0x000086DB
		public override void Accept(IVisitor visitor)
		{
			this.Combinator = base.VisitAndReplace<Combinator>(this.Combinator, visitor);
			this.NodeValue = base.VisitAndReplace<Node>(this.NodeValue, visitor, true);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000A504 File Offset: 0x00008704
		internal new Element Clone()
		{
			return new Element(this.Combinator)
			{
				Value = this.Value,
				NodeValue = this.NodeValue
			};
		}
	}
}
