using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200152F RID: 5423
	public struct condition_or : IProgramNodeBuilder, IEquatable<condition_or>
	{
		// Token: 0x17001EA4 RID: 7844
		// (get) Token: 0x0600B0CD RID: 45261 RVA: 0x0026F946 File Offset: 0x0026DB46
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B0CE RID: 45262 RVA: 0x0026F94E File Offset: 0x0026DB4E
		private condition_or(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B0CF RID: 45263 RVA: 0x0026F957 File Offset: 0x0026DB57
		public static condition_or CreateUnsafe(ProgramNode node)
		{
			return new condition_or(node);
		}

		// Token: 0x0600B0D0 RID: 45264 RVA: 0x0026F960 File Offset: 0x0026DB60
		public static condition_or? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.condition_or)
			{
				return null;
			}
			return new condition_or?(condition_or.CreateUnsafe(node));
		}

		// Token: 0x0600B0D1 RID: 45265 RVA: 0x0026F995 File Offset: 0x0026DB95
		public condition_or(GrammarBuilders g, or value0)
		{
			this._node = g.UnnamedConversion.condition_or.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B0D2 RID: 45266 RVA: 0x0026F9B4 File Offset: 0x0026DBB4
		public static implicit operator condition(condition_or arg)
		{
			return condition.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EA5 RID: 7845
		// (get) Token: 0x0600B0D3 RID: 45267 RVA: 0x0026F9C2 File Offset: 0x0026DBC2
		public or or
		{
			get
			{
				return or.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B0D4 RID: 45268 RVA: 0x0026F9D6 File Offset: 0x0026DBD6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B0D5 RID: 45269 RVA: 0x0026F9EC File Offset: 0x0026DBEC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B0D6 RID: 45270 RVA: 0x0026FA16 File Offset: 0x0026DC16
		public bool Equals(condition_or other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045DD RID: 17885
		private ProgramNode _node;
	}
}
