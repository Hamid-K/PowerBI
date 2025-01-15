using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001533 RID: 5427
	public struct number1_inumber : IProgramNodeBuilder, IEquatable<number1_inumber>
	{
		// Token: 0x17001EAC RID: 7852
		// (get) Token: 0x0600B0F5 RID: 45301 RVA: 0x0026FCD6 File Offset: 0x0026DED6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B0F6 RID: 45302 RVA: 0x0026FCDE File Offset: 0x0026DEDE
		private number1_inumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B0F7 RID: 45303 RVA: 0x0026FCE7 File Offset: 0x0026DEE7
		public static number1_inumber CreateUnsafe(ProgramNode node)
		{
			return new number1_inumber(node);
		}

		// Token: 0x0600B0F8 RID: 45304 RVA: 0x0026FCF0 File Offset: 0x0026DEF0
		public static number1_inumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.number1_inumber)
			{
				return null;
			}
			return new number1_inumber?(number1_inumber.CreateUnsafe(node));
		}

		// Token: 0x0600B0F9 RID: 45305 RVA: 0x0026FD25 File Offset: 0x0026DF25
		public number1_inumber(GrammarBuilders g, inumber value0)
		{
			this._node = g.UnnamedConversion.number1_inumber.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B0FA RID: 45306 RVA: 0x0026FD44 File Offset: 0x0026DF44
		public static implicit operator number1(number1_inumber arg)
		{
			return number1.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EAD RID: 7853
		// (get) Token: 0x0600B0FB RID: 45307 RVA: 0x0026FD52 File Offset: 0x0026DF52
		public inumber inumber
		{
			get
			{
				return inumber.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B0FC RID: 45308 RVA: 0x0026FD66 File Offset: 0x0026DF66
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B0FD RID: 45309 RVA: 0x0026FD7C File Offset: 0x0026DF7C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B0FE RID: 45310 RVA: 0x0026FDA6 File Offset: 0x0026DFA6
		public bool Equals(number1_inumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045E1 RID: 17889
		private ProgramNode _node;
	}
}
