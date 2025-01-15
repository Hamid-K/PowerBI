using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001026 RID: 4134
	public struct Class : IProgramNodeBuilder, IEquatable<Class>
	{
		// Token: 0x170015AB RID: 5547
		// (get) Token: 0x06007A14 RID: 31252 RVA: 0x001A1522 File Offset: 0x0019F722
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007A15 RID: 31253 RVA: 0x001A152A File Offset: 0x0019F72A
		private Class(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007A16 RID: 31254 RVA: 0x001A1533 File Offset: 0x0019F733
		public static Class CreateUnsafe(ProgramNode node)
		{
			return new Class(node);
		}

		// Token: 0x06007A17 RID: 31255 RVA: 0x001A153C File Offset: 0x0019F73C
		public static Class? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Class)
			{
				return null;
			}
			return new Class?(Class.CreateUnsafe(node));
		}

		// Token: 0x06007A18 RID: 31256 RVA: 0x001A1571 File Offset: 0x0019F771
		public Class(GrammarBuilders g, name value0, node value1)
		{
			this._node = g.Rule.Class.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007A19 RID: 31257 RVA: 0x001A1597 File Offset: 0x0019F797
		public static implicit operator atomExpr(Class arg)
		{
			return atomExpr.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015AC RID: 5548
		// (get) Token: 0x06007A1A RID: 31258 RVA: 0x001A15A5 File Offset: 0x0019F7A5
		public name name
		{
			get
			{
				return name.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015AD RID: 5549
		// (get) Token: 0x06007A1B RID: 31259 RVA: 0x001A15B9 File Offset: 0x0019F7B9
		public node node
		{
			get
			{
				return node.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007A1C RID: 31260 RVA: 0x001A15CD File Offset: 0x0019F7CD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007A1D RID: 31261 RVA: 0x001A15E0 File Offset: 0x0019F7E0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007A1E RID: 31262 RVA: 0x001A160A File Offset: 0x0019F80A
		public bool Equals(Class other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400333F RID: 13119
		private ProgramNode _node;
	}
}
