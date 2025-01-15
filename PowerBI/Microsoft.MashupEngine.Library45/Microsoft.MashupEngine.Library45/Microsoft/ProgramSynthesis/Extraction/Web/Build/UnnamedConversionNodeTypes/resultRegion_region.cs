using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001000 RID: 4096
	public struct resultRegion_region : IProgramNodeBuilder, IEquatable<resultRegion_region>
	{
		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x0600788F RID: 30863 RVA: 0x0019F272 File Offset: 0x0019D472
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007890 RID: 30864 RVA: 0x0019F27A File Offset: 0x0019D47A
		private resultRegion_region(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007891 RID: 30865 RVA: 0x0019F283 File Offset: 0x0019D483
		public static resultRegion_region CreateUnsafe(ProgramNode node)
		{
			return new resultRegion_region(node);
		}

		// Token: 0x06007892 RID: 30866 RVA: 0x0019F28C File Offset: 0x0019D48C
		public static resultRegion_region? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.resultRegion_region)
			{
				return null;
			}
			return new resultRegion_region?(resultRegion_region.CreateUnsafe(node));
		}

		// Token: 0x06007893 RID: 30867 RVA: 0x0019F2C1 File Offset: 0x0019D4C1
		public resultRegion_region(GrammarBuilders g, region value0)
		{
			this._node = g.UnnamedConversion.resultRegion_region.BuildASTNode(value0.Node);
		}

		// Token: 0x06007894 RID: 30868 RVA: 0x0019F2E0 File Offset: 0x0019D4E0
		public static implicit operator resultRegion(resultRegion_region arg)
		{
			return resultRegion.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001557 RID: 5463
		// (get) Token: 0x06007895 RID: 30869 RVA: 0x0019F2EE File Offset: 0x0019D4EE
		public region region
		{
			get
			{
				return region.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06007896 RID: 30870 RVA: 0x0019F302 File Offset: 0x0019D502
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007897 RID: 30871 RVA: 0x0019F318 File Offset: 0x0019D518
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007898 RID: 30872 RVA: 0x0019F342 File Offset: 0x0019D542
		public bool Equals(resultRegion_region other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003319 RID: 13081
		private ProgramNode _node;
	}
}
