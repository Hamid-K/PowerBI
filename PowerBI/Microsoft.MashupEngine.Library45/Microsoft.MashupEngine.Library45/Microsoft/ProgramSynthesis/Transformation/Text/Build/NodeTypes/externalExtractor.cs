using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.CustomExtraction;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C5F RID: 7263
	public struct externalExtractor : IProgramNodeBuilder, IEquatable<externalExtractor>
	{
		// Token: 0x170028F6 RID: 10486
		// (get) Token: 0x0600F5EE RID: 62958 RVA: 0x003487B6 File Offset: 0x003469B6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F5EF RID: 62959 RVA: 0x003487BE File Offset: 0x003469BE
		private externalExtractor(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F5F0 RID: 62960 RVA: 0x003487C7 File Offset: 0x003469C7
		public static externalExtractor CreateUnsafe(ProgramNode node)
		{
			return new externalExtractor(node);
		}

		// Token: 0x0600F5F1 RID: 62961 RVA: 0x003487D0 File Offset: 0x003469D0
		public static externalExtractor? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.externalExtractor)
			{
				return null;
			}
			return new externalExtractor?(externalExtractor.CreateUnsafe(node));
		}

		// Token: 0x0600F5F2 RID: 62962 RVA: 0x0034880A File Offset: 0x00346A0A
		public static externalExtractor CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new externalExtractor(new Hole(g.Symbol.externalExtractor, holeId));
		}

		// Token: 0x0600F5F3 RID: 62963 RVA: 0x00348822 File Offset: 0x00346A22
		public externalExtractor(GrammarBuilders g, CustomExtractor value)
		{
			this = new externalExtractor(new LiteralNode(g.Symbol.externalExtractor, value));
		}

		// Token: 0x170028F7 RID: 10487
		// (get) Token: 0x0600F5F4 RID: 62964 RVA: 0x0034883B File Offset: 0x00346A3B
		public CustomExtractor Value
		{
			get
			{
				return (CustomExtractor)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600F5F5 RID: 62965 RVA: 0x00348852 File Offset: 0x00346A52
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F5F6 RID: 62966 RVA: 0x00348868 File Offset: 0x00346A68
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F5F7 RID: 62967 RVA: 0x00348892 File Offset: 0x00346A92
		public bool Equals(externalExtractor other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B4E RID: 23374
		private ProgramNode _node;
	}
}
