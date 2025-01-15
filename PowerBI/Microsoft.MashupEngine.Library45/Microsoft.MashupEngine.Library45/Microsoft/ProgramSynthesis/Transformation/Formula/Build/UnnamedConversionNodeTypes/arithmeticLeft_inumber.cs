using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001535 RID: 5429
	public struct arithmeticLeft_inumber : IProgramNodeBuilder, IEquatable<arithmeticLeft_inumber>
	{
		// Token: 0x17001EB0 RID: 7856
		// (get) Token: 0x0600B109 RID: 45321 RVA: 0x0026FE9E File Offset: 0x0026E09E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B10A RID: 45322 RVA: 0x0026FEA6 File Offset: 0x0026E0A6
		private arithmeticLeft_inumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B10B RID: 45323 RVA: 0x0026FEAF File Offset: 0x0026E0AF
		public static arithmeticLeft_inumber CreateUnsafe(ProgramNode node)
		{
			return new arithmeticLeft_inumber(node);
		}

		// Token: 0x0600B10C RID: 45324 RVA: 0x0026FEB8 File Offset: 0x0026E0B8
		public static arithmeticLeft_inumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.arithmeticLeft_inumber)
			{
				return null;
			}
			return new arithmeticLeft_inumber?(arithmeticLeft_inumber.CreateUnsafe(node));
		}

		// Token: 0x0600B10D RID: 45325 RVA: 0x0026FEED File Offset: 0x0026E0ED
		public arithmeticLeft_inumber(GrammarBuilders g, inumber value0)
		{
			this._node = g.UnnamedConversion.arithmeticLeft_inumber.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B10E RID: 45326 RVA: 0x0026FF0C File Offset: 0x0026E10C
		public static implicit operator arithmeticLeft(arithmeticLeft_inumber arg)
		{
			return arithmeticLeft.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EB1 RID: 7857
		// (get) Token: 0x0600B10F RID: 45327 RVA: 0x0026FF1A File Offset: 0x0026E11A
		public inumber inumber
		{
			get
			{
				return inumber.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B110 RID: 45328 RVA: 0x0026FF2E File Offset: 0x0026E12E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B111 RID: 45329 RVA: 0x0026FF44 File Offset: 0x0026E144
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B112 RID: 45330 RVA: 0x0026FF6E File Offset: 0x0026E16E
		public bool Equals(arithmeticLeft_inumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045E3 RID: 17891
		private ProgramNode _node;
	}
}
