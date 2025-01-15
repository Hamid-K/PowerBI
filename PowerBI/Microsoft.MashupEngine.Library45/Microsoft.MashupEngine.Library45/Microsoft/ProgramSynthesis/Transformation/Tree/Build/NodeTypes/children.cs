using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E89 RID: 7817
	public struct children : IProgramNodeBuilder, IEquatable<children>
	{
		// Token: 0x17002BE2 RID: 11234
		// (get) Token: 0x0601082C RID: 67628 RVA: 0x0038D20E File Offset: 0x0038B40E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0601082D RID: 67629 RVA: 0x0038D216 File Offset: 0x0038B416
		private children(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0601082E RID: 67630 RVA: 0x0038D21F File Offset: 0x0038B41F
		public static children CreateUnsafe(ProgramNode node)
		{
			return new children(node);
		}

		// Token: 0x0601082F RID: 67631 RVA: 0x0038D228 File Offset: 0x0038B428
		public static children? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.children)
			{
				return null;
			}
			return new children?(children.CreateUnsafe(node));
		}

		// Token: 0x06010830 RID: 67632 RVA: 0x0038D262 File Offset: 0x0038B462
		public static children CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new children(new Hole(g.Symbol.children, holeId));
		}

		// Token: 0x06010831 RID: 67633 RVA: 0x0038D27A File Offset: 0x0038B47A
		public bool Is_children_interval(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.children_interval;
		}

		// Token: 0x06010832 RID: 67634 RVA: 0x0038D294 File Offset: 0x0038B494
		public bool Is_children_interval(GrammarBuilders g, out children_interval value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.children_interval)
			{
				value = children_interval.CreateUnsafe(this.Node);
				return true;
			}
			value = default(children_interval);
			return false;
		}

		// Token: 0x06010833 RID: 67635 RVA: 0x0038D2CC File Offset: 0x0038B4CC
		public children_interval? As_children_interval(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.children_interval)
			{
				return null;
			}
			return new children_interval?(children_interval.CreateUnsafe(this.Node));
		}

		// Token: 0x06010834 RID: 67636 RVA: 0x0038D30C File Offset: 0x0038B50C
		public children_interval Cast_children_interval(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.children_interval)
			{
				return children_interval.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_children_interval is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06010835 RID: 67637 RVA: 0x0038D361 File Offset: 0x0038B561
		public bool Is_Prepend(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Prepend;
		}

		// Token: 0x06010836 RID: 67638 RVA: 0x0038D37B File Offset: 0x0038B57B
		public bool Is_Prepend(GrammarBuilders g, out Prepend value)
		{
			if (this.Node.GrammarRule == g.Rule.Prepend)
			{
				value = Prepend.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Prepend);
			return false;
		}

		// Token: 0x06010837 RID: 67639 RVA: 0x0038D3B0 File Offset: 0x0038B5B0
		public Prepend? As_Prepend(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Prepend)
			{
				return null;
			}
			return new Prepend?(Prepend.CreateUnsafe(this.Node));
		}

		// Token: 0x06010838 RID: 67640 RVA: 0x0038D3F0 File Offset: 0x0038B5F0
		public Prepend Cast_Prepend(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Prepend)
			{
				return Prepend.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Prepend is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06010839 RID: 67641 RVA: 0x0038D448 File Offset: 0x0038B648
		public T Switch<T>(GrammarBuilders g, Func<children_interval, T> func0, Func<Prepend, T> func1)
		{
			children_interval children_interval;
			if (this.Is_children_interval(g, out children_interval))
			{
				return func0(children_interval);
			}
			Prepend prepend;
			if (this.Is_Prepend(g, out prepend))
			{
				return func1(prepend);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol children");
		}

		// Token: 0x0601083A RID: 67642 RVA: 0x0038D4A0 File Offset: 0x0038B6A0
		public void Switch(GrammarBuilders g, Action<children_interval> func0, Action<Prepend> func1)
		{
			children_interval children_interval;
			if (this.Is_children_interval(g, out children_interval))
			{
				func0(children_interval);
				return;
			}
			Prepend prepend;
			if (this.Is_Prepend(g, out prepend))
			{
				func1(prepend);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol children");
		}

		// Token: 0x0601083B RID: 67643 RVA: 0x0038D4F7 File Offset: 0x0038B6F7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0601083C RID: 67644 RVA: 0x0038D50C File Offset: 0x0038B70C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0601083D RID: 67645 RVA: 0x0038D536 File Offset: 0x0038B736
		public bool Equals(children other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062C8 RID: 25288
		private ProgramNode _node;
	}
}
