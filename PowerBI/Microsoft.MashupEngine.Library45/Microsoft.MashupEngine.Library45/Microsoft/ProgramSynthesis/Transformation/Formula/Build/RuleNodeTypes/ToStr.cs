using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200154D RID: 5453
	public struct ToStr : IProgramNodeBuilder, IEquatable<ToStr>
	{
		// Token: 0x17001EE2 RID: 7906
		// (get) Token: 0x0600B1FB RID: 45563 RVA: 0x00271432 File Offset: 0x0026F632
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B1FC RID: 45564 RVA: 0x0027143A File Offset: 0x0026F63A
		private ToStr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B1FD RID: 45565 RVA: 0x00271443 File Offset: 0x0026F643
		public static ToStr CreateUnsafe(ProgramNode node)
		{
			return new ToStr(node);
		}

		// Token: 0x0600B1FE RID: 45566 RVA: 0x0027144C File Offset: 0x0026F64C
		public static ToStr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ToStr)
			{
				return null;
			}
			return new ToStr?(ToStr.CreateUnsafe(node));
		}

		// Token: 0x0600B1FF RID: 45567 RVA: 0x00271481 File Offset: 0x0026F681
		public ToStr(GrammarBuilders g, outStr value0)
		{
			this._node = g.Rule.ToStr.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B200 RID: 45568 RVA: 0x002714A0 File Offset: 0x0026F6A0
		public static implicit operator output(ToStr arg)
		{
			return output.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EE3 RID: 7907
		// (get) Token: 0x0600B201 RID: 45569 RVA: 0x002714AE File Offset: 0x0026F6AE
		public outStr outStr
		{
			get
			{
				return outStr.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B202 RID: 45570 RVA: 0x002714C2 File Offset: 0x0026F6C2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B203 RID: 45571 RVA: 0x002714D8 File Offset: 0x0026F6D8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B204 RID: 45572 RVA: 0x00271502 File Offset: 0x0026F702
		public bool Equals(ToStr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045FB RID: 17915
		private ProgramNode _node;
	}
}
