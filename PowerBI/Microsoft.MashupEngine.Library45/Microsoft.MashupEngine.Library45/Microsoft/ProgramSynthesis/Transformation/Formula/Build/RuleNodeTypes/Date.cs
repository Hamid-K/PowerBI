using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001597 RID: 5527
	public struct Date : IProgramNodeBuilder, IEquatable<Date>
	{
		// Token: 0x17001FBA RID: 8122
		// (get) Token: 0x0600B523 RID: 46371 RVA: 0x00275D2A File Offset: 0x00273F2A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B524 RID: 46372 RVA: 0x00275D32 File Offset: 0x00273F32
		private Date(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B525 RID: 46373 RVA: 0x00275D3B File Offset: 0x00273F3B
		public static Date CreateUnsafe(ProgramNode node)
		{
			return new Date(node);
		}

		// Token: 0x0600B526 RID: 46374 RVA: 0x00275D44 File Offset: 0x00273F44
		public static Date? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Date)
			{
				return null;
			}
			return new Date?(Date.CreateUnsafe(node));
		}

		// Token: 0x0600B527 RID: 46375 RVA: 0x00275D79 File Offset: 0x00273F79
		public Date(GrammarBuilders g, constDt value0)
		{
			this._node = g.Rule.Date.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B528 RID: 46376 RVA: 0x00275D98 File Offset: 0x00273F98
		public static implicit operator constDate(Date arg)
		{
			return constDate.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001FBB RID: 8123
		// (get) Token: 0x0600B529 RID: 46377 RVA: 0x00275DA6 File Offset: 0x00273FA6
		public constDt constDt
		{
			get
			{
				return constDt.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B52A RID: 46378 RVA: 0x00275DBA File Offset: 0x00273FBA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B52B RID: 46379 RVA: 0x00275DD0 File Offset: 0x00273FD0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B52C RID: 46380 RVA: 0x00275DFA File Offset: 0x00273FFA
		public bool Equals(Date other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004645 RID: 17989
		private ProgramNode _node;
	}
}
