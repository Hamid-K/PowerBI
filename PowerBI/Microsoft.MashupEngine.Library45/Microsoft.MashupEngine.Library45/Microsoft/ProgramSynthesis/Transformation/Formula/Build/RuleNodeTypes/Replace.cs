using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200154E RID: 5454
	public struct Replace : IProgramNodeBuilder, IEquatable<Replace>
	{
		// Token: 0x17001EE4 RID: 7908
		// (get) Token: 0x0600B205 RID: 45573 RVA: 0x00271516 File Offset: 0x0026F716
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B206 RID: 45574 RVA: 0x0027151E File Offset: 0x0026F71E
		private Replace(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B207 RID: 45575 RVA: 0x00271527 File Offset: 0x0026F727
		public static Replace CreateUnsafe(ProgramNode node)
		{
			return new Replace(node);
		}

		// Token: 0x0600B208 RID: 45576 RVA: 0x00271530 File Offset: 0x0026F730
		public static Replace? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Replace)
			{
				return null;
			}
			return new Replace?(Replace.CreateUnsafe(node));
		}

		// Token: 0x0600B209 RID: 45577 RVA: 0x00271565 File Offset: 0x0026F765
		public Replace(GrammarBuilders g, fromStr value0, replaceFindText value1, replaceText value2)
		{
			this._node = g.Rule.Replace.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600B20A RID: 45578 RVA: 0x00271592 File Offset: 0x0026F792
		public static implicit operator outStr(Replace arg)
		{
			return outStr.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EE5 RID: 7909
		// (get) Token: 0x0600B20B RID: 45579 RVA: 0x002715A0 File Offset: 0x0026F7A0
		public fromStr fromStr
		{
			get
			{
				return fromStr.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001EE6 RID: 7910
		// (get) Token: 0x0600B20C RID: 45580 RVA: 0x002715B4 File Offset: 0x0026F7B4
		public replaceFindText replaceFindText
		{
			get
			{
				return replaceFindText.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001EE7 RID: 7911
		// (get) Token: 0x0600B20D RID: 45581 RVA: 0x002715C8 File Offset: 0x0026F7C8
		public replaceText replaceText
		{
			get
			{
				return replaceText.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600B20E RID: 45582 RVA: 0x002715DC File Offset: 0x0026F7DC
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B20F RID: 45583 RVA: 0x002715F0 File Offset: 0x0026F7F0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B210 RID: 45584 RVA: 0x0027161A File Offset: 0x0026F81A
		public bool Equals(Replace other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045FC RID: 17916
		private ProgramNode _node;
	}
}
