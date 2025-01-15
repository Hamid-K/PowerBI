using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200151F RID: 5407
	public struct outStr1_constString : IProgramNodeBuilder, IEquatable<outStr1_constString>
	{
		// Token: 0x17001E84 RID: 7812
		// (get) Token: 0x0600B02D RID: 45101 RVA: 0x0026EB06 File Offset: 0x0026CD06
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B02E RID: 45102 RVA: 0x0026EB0E File Offset: 0x0026CD0E
		private outStr1_constString(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B02F RID: 45103 RVA: 0x0026EB17 File Offset: 0x0026CD17
		public static outStr1_constString CreateUnsafe(ProgramNode node)
		{
			return new outStr1_constString(node);
		}

		// Token: 0x0600B030 RID: 45104 RVA: 0x0026EB20 File Offset: 0x0026CD20
		public static outStr1_constString? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.outStr1_constString)
			{
				return null;
			}
			return new outStr1_constString?(outStr1_constString.CreateUnsafe(node));
		}

		// Token: 0x0600B031 RID: 45105 RVA: 0x0026EB55 File Offset: 0x0026CD55
		public outStr1_constString(GrammarBuilders g, constString value0)
		{
			this._node = g.UnnamedConversion.outStr1_constString.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B032 RID: 45106 RVA: 0x0026EB74 File Offset: 0x0026CD74
		public static implicit operator outStr1(outStr1_constString arg)
		{
			return outStr1.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E85 RID: 7813
		// (get) Token: 0x0600B033 RID: 45107 RVA: 0x0026EB82 File Offset: 0x0026CD82
		public constString constString
		{
			get
			{
				return constString.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B034 RID: 45108 RVA: 0x0026EB96 File Offset: 0x0026CD96
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B035 RID: 45109 RVA: 0x0026EBAC File Offset: 0x0026CDAC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B036 RID: 45110 RVA: 0x0026EBD6 File Offset: 0x0026CDD6
		public bool Equals(outStr1_constString other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045CD RID: 17869
		private ProgramNode _node;
	}
}
