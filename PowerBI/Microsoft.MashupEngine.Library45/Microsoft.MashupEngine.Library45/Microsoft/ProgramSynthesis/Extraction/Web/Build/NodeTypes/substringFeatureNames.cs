using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001097 RID: 4247
	public struct substringFeatureNames : IProgramNodeBuilder, IEquatable<substringFeatureNames>
	{
		// Token: 0x1700168B RID: 5771
		// (get) Token: 0x06007FED RID: 32749 RVA: 0x001ACB76 File Offset: 0x001AAD76
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007FEE RID: 32750 RVA: 0x001ACB7E File Offset: 0x001AAD7E
		private substringFeatureNames(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007FEF RID: 32751 RVA: 0x001ACB87 File Offset: 0x001AAD87
		public static substringFeatureNames CreateUnsafe(ProgramNode node)
		{
			return new substringFeatureNames(node);
		}

		// Token: 0x06007FF0 RID: 32752 RVA: 0x001ACB90 File Offset: 0x001AAD90
		public static substringFeatureNames? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.substringFeatureNames)
			{
				return null;
			}
			return new substringFeatureNames?(substringFeatureNames.CreateUnsafe(node));
		}

		// Token: 0x06007FF1 RID: 32753 RVA: 0x001ACBCA File Offset: 0x001AADCA
		public static substringFeatureNames CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new substringFeatureNames(new Hole(g.Symbol.substringFeatureNames, holeId));
		}

		// Token: 0x06007FF2 RID: 32754 RVA: 0x001ACBE2 File Offset: 0x001AADE2
		public substringFeatureNames(GrammarBuilders g, string[] value)
		{
			this = new substringFeatureNames(new LiteralNode(g.Symbol.substringFeatureNames, value));
		}

		// Token: 0x1700168C RID: 5772
		// (get) Token: 0x06007FF3 RID: 32755 RVA: 0x001ACBFB File Offset: 0x001AADFB
		public string[] Value
		{
			get
			{
				return (string[])((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06007FF4 RID: 32756 RVA: 0x001ACC12 File Offset: 0x001AAE12
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007FF5 RID: 32757 RVA: 0x001ACC28 File Offset: 0x001AAE28
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007FF6 RID: 32758 RVA: 0x001ACC52 File Offset: 0x001AAE52
		public bool Equals(substringFeatureNames other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033B0 RID: 13232
		private ProgramNode _node;
	}
}
