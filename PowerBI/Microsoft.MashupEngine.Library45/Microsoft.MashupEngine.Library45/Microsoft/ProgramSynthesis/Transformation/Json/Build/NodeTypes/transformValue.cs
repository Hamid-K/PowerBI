using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A44 RID: 6724
	public struct transformValue : IProgramNodeBuilder, IEquatable<transformValue>
	{
		// Token: 0x17002513 RID: 9491
		// (get) Token: 0x0600DD84 RID: 56708 RVA: 0x002F1A3A File Offset: 0x002EFC3A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DD85 RID: 56709 RVA: 0x002F1A42 File Offset: 0x002EFC42
		private transformValue(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DD86 RID: 56710 RVA: 0x002F1A4B File Offset: 0x002EFC4B
		public static transformValue CreateUnsafe(ProgramNode node)
		{
			return new transformValue(node);
		}

		// Token: 0x0600DD87 RID: 56711 RVA: 0x002F1A54 File Offset: 0x002EFC54
		public static transformValue? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.transformValue)
			{
				return null;
			}
			return new transformValue?(transformValue.CreateUnsafe(node));
		}

		// Token: 0x0600DD88 RID: 56712 RVA: 0x002F1A8E File Offset: 0x002EFC8E
		public static transformValue CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new transformValue(new Hole(g.Symbol.transformValue, holeId));
		}

		// Token: 0x0600DD89 RID: 56713 RVA: 0x002F1AA6 File Offset: 0x002EFCA6
		public TransformValue Cast_TransformValue()
		{
			return TransformValue.CreateUnsafe(this.Node);
		}

		// Token: 0x0600DD8A RID: 56714 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_TransformValue(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600DD8B RID: 56715 RVA: 0x002F1AB3 File Offset: 0x002EFCB3
		public bool Is_TransformValue(GrammarBuilders g, out TransformValue value)
		{
			value = TransformValue.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600DD8C RID: 56716 RVA: 0x002F1AC7 File Offset: 0x002EFCC7
		public TransformValue? As_TransformValue(GrammarBuilders g)
		{
			return new TransformValue?(TransformValue.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DD8D RID: 56717 RVA: 0x002F1AD9 File Offset: 0x002EFCD9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DD8E RID: 56718 RVA: 0x002F1AEC File Offset: 0x002EFCEC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DD8F RID: 56719 RVA: 0x002F1B16 File Offset: 0x002EFD16
		public bool Equals(transformValue other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005435 RID: 21557
		private ProgramNode _node;
	}
}
