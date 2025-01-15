using System;
using System.Collections.Generic;
using System.Linq;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x02000049 RID: 73
	public class Selector : Node
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000DC8C File Offset: 0x0000BE8C
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000DC94 File Offset: 0x0000BE94
		public NodeList<Element> Elements { get; set; }

		// Token: 0x060002F3 RID: 755 RVA: 0x0000DC9D File Offset: 0x0000BE9D
		public Selector(IEnumerable<Element> elements)
		{
			if (elements is NodeList<Element>)
			{
				this.Elements = elements as NodeList<Element>;
				return;
			}
			this.Elements = new NodeList<Element>(elements);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000DCC8 File Offset: 0x0000BEC8
		public bool Match(Selector other)
		{
			return other.Elements.Count <= this.Elements.Count && this.Elements[0].Value == other.Elements[0].Value;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000DD16 File Offset: 0x0000BF16
		private Parser Parser
		{
			get
			{
				if (Selector.parser == null)
				{
					Selector.parser = new Parser();
				}
				return Selector.parser;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000DD2E File Offset: 0x0000BF2E
		private Parsers Parsers
		{
			get
			{
				if (Selector.parsers == null)
				{
					Selector.parsers = new Parsers(this.Parser.NodeProvider);
				}
				return Selector.parsers;
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000DD54 File Offset: 0x0000BF54
		public override Node Evaluate(Env env)
		{
			NodeList<Element> nodeList = new NodeList<Element>();
			foreach (Element element in this.Elements)
			{
				if (element.NodeValue is Extend)
				{
					if (env.MediaPath.Any<Media>())
					{
						env.MediaPath.Peek().AddExtension(this, (Extend)((Extend)element.NodeValue).Evaluate(env), env);
					}
					else
					{
						env.AddExtension(this, (Extend)((Extend)element.NodeValue).Evaluate(env), env);
					}
				}
				else
				{
					nodeList.Add(element.Evaluate(env) as Element);
				}
			}
			Selector selector = new Selector(nodeList).ReducedFrom<Selector>(new Node[] { this });
			if (selector.Elements.All((Element e) => e.NodeValue == null))
			{
				return selector;
			}
			this.Parser.Tokenizer.SetupInput(selector.ToCSS(env), "");
			NodeList<Selector> nodeList2 = new NodeList<Selector>();
			Selector selector2;
			while (selector2 = this.Parsers.Selector(this.Parser))
			{
				selector2.IsReference = base.IsReference;
				nodeList2.Add(selector2.Evaluate(env) as Selector);
				if (!this.Parser.Tokenizer.Match(','))
				{
					break;
				}
			}
			return nodeList2;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000DEE0 File Offset: 0x0000C0E0
		protected override Node CloneCore()
		{
			return new Selector(this.Elements.Select((Element e) => e.Clone()));
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000DF14 File Offset: 0x0000C114
		public override void AppendCSS(Env env)
		{
			env.Output.Push();
			if (this.Elements[0].Combinator.Value == "")
			{
				env.Output.Append(new char?(' '));
			}
			env.Output.Append(this.Elements);
			env.Output.Append(env.Output.Pop().ToString());
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000DF90 File Offset: 0x0000C190
		public override void Accept(IVisitor visitor)
		{
			this.Elements = base.VisitAndReplace<NodeList<Element>>(this.Elements, visitor);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000DFA5 File Offset: 0x0000C1A5
		public override string ToString()
		{
			return this.ToCSS(new Env(null));
		}

		// Token: 0x040000AF RID: 175
		[ThreadStatic]
		private static Parser parser;

		// Token: 0x040000B0 RID: 176
		[ThreadStatic]
		private static Parsers parsers;
	}
}
