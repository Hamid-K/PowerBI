using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x0200003E RID: 62
	public class Media : Ruleset
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000AEE5 File Offset: 0x000090E5
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000AEED File Offset: 0x000090ED
		public Node Features { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000AEF6 File Offset: 0x000090F6
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000AEFE File Offset: 0x000090FE
		public Ruleset Ruleset { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000AF07 File Offset: 0x00009107
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000AF0F File Offset: 0x0000910F
		public List<Extender> Extensions { get; set; }

		// Token: 0x0600024F RID: 591 RVA: 0x0000AF18 File Offset: 0x00009118
		public Media(Node features, NodeList rules)
			: this(features, new Ruleset(Media.GetEmptySelector(), rules), null)
		{
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000AF2D File Offset: 0x0000912D
		public Media(Node features, Ruleset ruleset, List<Extender> extensions)
		{
			this.Features = features;
			this.Ruleset = ruleset;
			this.Extensions = extensions ?? new List<Extender>();
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000AF53 File Offset: 0x00009153
		protected override Node CloneCore()
		{
			return new Media(this.Features.Clone(), (Ruleset)this.Ruleset.Clone(), this.Extensions);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000AF7B File Offset: 0x0000917B
		public static NodeList<Selector> GetEmptySelector()
		{
			return new NodeList<Selector>
			{
				new Selector(new NodeList<Element>
				{
					new Element(new Combinator(""), "&")
				})
			};
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000AFAC File Offset: 0x000091AC
		public override void Accept(IVisitor visitor)
		{
			this.Features = base.VisitAndReplace<Node>(this.Features, visitor);
			this.Ruleset = base.VisitAndReplace<Ruleset>(this.Ruleset, visitor);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000AFD4 File Offset: 0x000091D4
		public override Node Evaluate(Env env)
		{
			int count = env.MediaBlocks.Count;
			env.MediaBlocks.Add(this);
			env.MediaPath.Push(this);
			env.Frames.Push(this.Ruleset);
			NodeHelper.ExpandNodes<Import>(env, this.Ruleset.Rules);
			env.Frames.Pop();
			this.Features = this.Features.Evaluate(env);
			Ruleset ruleset = this.Ruleset.Evaluate(env) as Ruleset;
			Media media = new Media(this.Features, ruleset, this.Extensions).ReducedFrom<Media>(new Node[] { this });
			env.MediaPath.Pop();
			env.MediaBlocks[count] = media;
			if (env.MediaPath.Count == 0)
			{
				return media.EvalTop(env);
			}
			return media.EvalNested(env, this.Features, ruleset);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000B0B8 File Offset: 0x000092B8
		protected Node EvalTop(Env env)
		{
			Node node;
			if (env.MediaBlocks.Count > 1)
			{
				node = new Ruleset(Media.GetEmptySelector(), new NodeList(env.MediaBlocks.Cast<Node>()))
				{
					MultiMedia = true
				}.ReducedFrom<Ruleset>(new Node[] { this });
			}
			else
			{
				node = env.MediaBlocks[0];
			}
			env.MediaPath.Clear();
			env.MediaBlocks.Clear();
			return node;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000B12C File Offset: 0x0000932C
		protected Node EvalNested(Env env, Node features, Ruleset ruleset)
		{
			NodeList<Media> nodeList = new NodeList<Media>(env.MediaPath.ToList<Media>());
			nodeList.Add(this);
			NodeList<NodeList> nodeList2 = new NodeList<NodeList>();
			for (int i = 0; i < nodeList.Count; i++)
			{
				Value value = nodeList[i].Features as Value;
				Node node;
				if (value != null)
				{
					node = value.Values;
				}
				else
				{
					node = nodeList[i].Features;
				}
				NodeList<NodeList> nodeList3 = nodeList2;
				NodeList nodeList4;
				if ((nodeList4 = node as NodeList) == null)
				{
					(nodeList4 = new NodeList()).Add(node);
				}
				nodeList3.Add(nodeList4);
			}
			NodeList<NodeList> nodeList5 = new NodeList<NodeList>();
			foreach (NodeList nodeList6 in nodeList2)
			{
				nodeList5.Add(nodeList6);
				nodeList5.Add(new NodeList
				{
					new TextNode("and")
				});
			}
			nodeList5.RemoveAt(nodeList5.Count - 1);
			this.Features = new Value(this.Permute(nodeList5), null);
			return new Ruleset(new NodeList<Selector>(), new NodeList());
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B24C File Offset: 0x0000944C
		private NodeList Permute(NodeList<NodeList> arr)
		{
			if (arr.Count == 0)
			{
				return new NodeList();
			}
			if (arr.Count == 1)
			{
				return arr[0];
			}
			NodeList nodeList = new NodeList();
			NodeList<NodeList> nodeList2 = new NodeList<NodeList>(arr.Skip(1));
			NodeList nodeList3 = this.Permute(nodeList2);
			for (int i = 0; i < nodeList3.Count; i++)
			{
				NodeList nodeList4 = arr[0];
				for (int j = 0; j < nodeList4.Count; j++)
				{
					NodeList nodeList5 = new NodeList();
					nodeList5.Add(nodeList4[j]);
					NodeList nodeList6 = nodeList3[i] as NodeList;
					if (nodeList6)
					{
						nodeList5.AddRange(nodeList6);
					}
					else
					{
						nodeList5.Add(nodeList3[i]);
					}
					nodeList.Add(new Expression(nodeList5));
				}
			}
			return nodeList;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000B318 File Offset: 0x00009518
		public void BubbleSelectors(NodeList<Selector> selectors)
		{
			this.Ruleset = new Ruleset(new NodeList<Selector>(selectors), new NodeList { this.Ruleset });
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000B33C File Offset: 0x0000953C
		public override void AppendCSS(Env env, Context ctx)
		{
			if (env.Compress && !this.Ruleset.Rules.Any<Node>())
			{
				return;
			}
			env.Output.Push();
			this.Ruleset.IsRoot = ctx.Count == 0;
			env.ExtendMediaScope.Push(this);
			this.Ruleset.AppendCSS(env, ctx);
			env.ExtendMediaScope.Pop();
			if (!env.Compress)
			{
				env.Output.Trim().Indent(2);
			}
			StringBuilder stringBuilder = env.Output.Pop();
			if (base.IsReference)
			{
				if (this.Ruleset.Rules.All((Node r) => r.IsReference))
				{
					return;
				}
			}
			if (env.Compress && stringBuilder.Length == 0)
			{
				return;
			}
			env.Output.Append("@media");
			if (this.Features)
			{
				env.Output.Append(new char?(' '));
				env.Output.Append(this.Features);
			}
			if (env.Compress)
			{
				env.Output.Append(new char?('{'));
			}
			else
			{
				env.Output.Append(" {\n");
			}
			env.Output.Append(stringBuilder);
			if (env.Compress)
			{
				env.Output.Append(new char?('}'));
				return;
			}
			env.Output.Append("\n}\n");
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000B4C8 File Offset: 0x000096C8
		public void AddExtension(Selector selector, Extend extends, Env env)
		{
			using (List<Selector>.Enumerator enumerator = extends.Exact.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Selector extending2 = enumerator.Current;
					Extender extender;
					if ((extender = this.Extensions.OfType<ExactExtender>().FirstOrDefault((ExactExtender e) => e.BaseSelector.ToString().Trim() == extending2.ToString().Trim())) == null)
					{
						extender = new ExactExtender(extending2, extends);
						this.Extensions.Add(extender);
					}
					extender.AddExtension(selector, env);
				}
			}
			using (List<Selector>.Enumerator enumerator = extends.Partial.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Selector extending = enumerator.Current;
					Extender extender2;
					if ((extender2 = this.Extensions.OfType<PartialExtender>().FirstOrDefault((PartialExtender e) => e.BaseSelector.ToString().Trim() == extending.ToString().Trim())) == null)
					{
						extender2 = new PartialExtender(extending, extends);
						this.Extensions.Add(extender2);
					}
					extender2.AddExtension(selector, env);
				}
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000B5F0 File Offset: 0x000097F0
		public IEnumerable<Extender> FindUnmatchedExtensions()
		{
			return this.Extensions.Where((Extender e) => !e.IsMatched);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000B61C File Offset: 0x0000981C
		public ExactExtender FindExactExtension(string selection)
		{
			return this.Extensions.OfType<ExactExtender>().FirstOrDefault((ExactExtender e) => e.BaseSelector.ToString().Trim() == selection);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000B652 File Offset: 0x00009852
		public PartialExtender[] FindPartialExtensions(Context selection)
		{
			return this.Extensions.OfType<PartialExtender>().WhereExtenderMatches(selection).ToArray<PartialExtender>();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000B66C File Offset: 0x0000986C
		[Obsolete("This method doesn't return the correct results. Use FindPartialExtensions(Context) instead.", false)]
		public PartialExtender[] FindPartialExtensions(string selection)
		{
			return (from e in this.Extensions.OfType<PartialExtender>()
				where selection.Contains(e.BaseSelector.ToString().Trim())
				select e).ToArray<PartialExtender>();
		}
	}
}
