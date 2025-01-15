using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001569 RID: 5481
	public struct TimePart : IProgramNodeBuilder, IEquatable<TimePart>
	{
		// Token: 0x17001F37 RID: 7991
		// (get) Token: 0x0600B330 RID: 45872 RVA: 0x00273046 File Offset: 0x00271246
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B331 RID: 45873 RVA: 0x0027304E File Offset: 0x0027124E
		private TimePart(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B332 RID: 45874 RVA: 0x00273057 File Offset: 0x00271257
		public static TimePart CreateUnsafe(ProgramNode node)
		{
			return new TimePart(node);
		}

		// Token: 0x0600B333 RID: 45875 RVA: 0x00273060 File Offset: 0x00271260
		public static TimePart? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TimePart)
			{
				return null;
			}
			return new TimePart?(TimePart.CreateUnsafe(node));
		}

		// Token: 0x0600B334 RID: 45876 RVA: 0x00273095 File Offset: 0x00271295
		public TimePart(GrammarBuilders g, itime value0, timePartKind value1)
		{
			this._node = g.Rule.TimePart.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B335 RID: 45877 RVA: 0x002730BB File Offset: 0x002712BB
		public static implicit operator number1(TimePart arg)
		{
			return number1.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F38 RID: 7992
		// (get) Token: 0x0600B336 RID: 45878 RVA: 0x002730C9 File Offset: 0x002712C9
		public itime itime
		{
			get
			{
				return itime.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F39 RID: 7993
		// (get) Token: 0x0600B337 RID: 45879 RVA: 0x002730DD File Offset: 0x002712DD
		public timePartKind timePartKind
		{
			get
			{
				return timePartKind.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B338 RID: 45880 RVA: 0x002730F1 File Offset: 0x002712F1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B339 RID: 45881 RVA: 0x00273104 File Offset: 0x00271304
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B33A RID: 45882 RVA: 0x0027312E File Offset: 0x0027132E
		public bool Equals(TimePart other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004617 RID: 17943
		private ProgramNode _node;
	}
}
