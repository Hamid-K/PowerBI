using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A46 RID: 6726
	public struct transformString : IProgramNodeBuilder, IEquatable<transformString>
	{
		// Token: 0x17002515 RID: 9493
		// (get) Token: 0x0600DD9C RID: 56732 RVA: 0x002F1C1A File Offset: 0x002EFE1A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DD9D RID: 56733 RVA: 0x002F1C22 File Offset: 0x002EFE22
		private transformString(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DD9E RID: 56734 RVA: 0x002F1C2B File Offset: 0x002EFE2B
		public static transformString CreateUnsafe(ProgramNode node)
		{
			return new transformString(node);
		}

		// Token: 0x0600DD9F RID: 56735 RVA: 0x002F1C34 File Offset: 0x002EFE34
		public static transformString? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.transformString)
			{
				return null;
			}
			return new transformString?(transformString.CreateUnsafe(node));
		}

		// Token: 0x0600DDA0 RID: 56736 RVA: 0x002F1C6E File Offset: 0x002EFE6E
		public static transformString CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new transformString(new Hole(g.Symbol.transformString, holeId));
		}

		// Token: 0x0600DDA1 RID: 56737 RVA: 0x002F1C86 File Offset: 0x002EFE86
		public TransformString Cast_TransformString()
		{
			return TransformString.CreateUnsafe(this.Node);
		}

		// Token: 0x0600DDA2 RID: 56738 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_TransformString(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600DDA3 RID: 56739 RVA: 0x002F1C93 File Offset: 0x002EFE93
		public bool Is_TransformString(GrammarBuilders g, out TransformString value)
		{
			value = TransformString.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600DDA4 RID: 56740 RVA: 0x002F1CA7 File Offset: 0x002EFEA7
		public TransformString? As_TransformString(GrammarBuilders g)
		{
			return new TransformString?(TransformString.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DDA5 RID: 56741 RVA: 0x002F1CB9 File Offset: 0x002EFEB9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DDA6 RID: 56742 RVA: 0x002F1CCC File Offset: 0x002EFECC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DDA7 RID: 56743 RVA: 0x002F1CF6 File Offset: 0x002EFEF6
		public bool Equals(transformString other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005437 RID: 21559
		private ProgramNode _node;
	}
}
