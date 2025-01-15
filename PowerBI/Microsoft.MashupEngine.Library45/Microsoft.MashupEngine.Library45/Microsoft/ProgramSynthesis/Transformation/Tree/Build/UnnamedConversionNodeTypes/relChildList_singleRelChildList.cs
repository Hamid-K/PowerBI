using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001E5D RID: 7773
	public struct relChildList_singleRelChildList : IProgramNodeBuilder, IEquatable<relChildList_singleRelChildList>
	{
		// Token: 0x17002B7A RID: 11130
		// (get) Token: 0x060105EF RID: 67055 RVA: 0x00388F6A File Offset: 0x0038716A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060105F0 RID: 67056 RVA: 0x00388F72 File Offset: 0x00387172
		private relChildList_singleRelChildList(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060105F1 RID: 67057 RVA: 0x00388F7B File Offset: 0x0038717B
		public static relChildList_singleRelChildList CreateUnsafe(ProgramNode node)
		{
			return new relChildList_singleRelChildList(node);
		}

		// Token: 0x060105F2 RID: 67058 RVA: 0x00388F84 File Offset: 0x00387184
		public static relChildList_singleRelChildList? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.relChildList_singleRelChildList)
			{
				return null;
			}
			return new relChildList_singleRelChildList?(relChildList_singleRelChildList.CreateUnsafe(node));
		}

		// Token: 0x060105F3 RID: 67059 RVA: 0x00388FB9 File Offset: 0x003871B9
		public relChildList_singleRelChildList(GrammarBuilders g, singleRelChildList value0)
		{
			this._node = g.UnnamedConversion.relChildList_singleRelChildList.BuildASTNode(value0.Node);
		}

		// Token: 0x060105F4 RID: 67060 RVA: 0x00388FD8 File Offset: 0x003871D8
		public static implicit operator relChildList(relChildList_singleRelChildList arg)
		{
			return relChildList.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002B7B RID: 11131
		// (get) Token: 0x060105F5 RID: 67061 RVA: 0x00388FE6 File Offset: 0x003871E6
		public singleRelChildList singleRelChildList
		{
			get
			{
				return singleRelChildList.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060105F6 RID: 67062 RVA: 0x00388FFA File Offset: 0x003871FA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060105F7 RID: 67063 RVA: 0x00389010 File Offset: 0x00387210
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060105F8 RID: 67064 RVA: 0x0038903A File Offset: 0x0038723A
		public bool Equals(relChildList_singleRelChildList other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400629C RID: 25244
		private ProgramNode _node;
	}
}
