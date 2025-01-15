using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Semantics;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes
{
	// Token: 0x020011FD RID: 4605
	public struct inputSRegions : IProgramNodeBuilder, IEquatable<inputSRegions>
	{
		// Token: 0x170017CC RID: 6092
		// (get) Token: 0x06008AD2 RID: 35538 RVA: 0x001D1A06 File Offset: 0x001CFC06
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008AD3 RID: 35539 RVA: 0x001D1A0E File Offset: 0x001CFC0E
		private inputSRegions(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008AD4 RID: 35540 RVA: 0x001D1A17 File Offset: 0x001CFC17
		public static inputSRegions CreateUnsafe(ProgramNode node)
		{
			return new inputSRegions(node);
		}

		// Token: 0x06008AD5 RID: 35541 RVA: 0x001D1A20 File Offset: 0x001CFC20
		public static inputSRegions? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.inputSRegions)
			{
				return null;
			}
			return new inputSRegions?(inputSRegions.CreateUnsafe(node));
		}

		// Token: 0x06008AD6 RID: 35542 RVA: 0x001D1A5A File Offset: 0x001CFC5A
		public static inputSRegions CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new inputSRegions(new Hole(g.Symbol.inputSRegions, holeId));
		}

		// Token: 0x06008AD7 RID: 35543 RVA: 0x001D1A72 File Offset: 0x001CFC72
		public inputSRegions(GrammarBuilders g, IEnumerable<SuffixRegion> value)
		{
			this = new inputSRegions(new LiteralNode(g.Symbol.inputSRegions, value));
		}

		// Token: 0x170017CD RID: 6093
		// (get) Token: 0x06008AD8 RID: 35544 RVA: 0x001D1A8B File Offset: 0x001CFC8B
		public IEnumerable<SuffixRegion> Value
		{
			get
			{
				return (IEnumerable<SuffixRegion>)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06008AD9 RID: 35545 RVA: 0x001D1AA2 File Offset: 0x001CFCA2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008ADA RID: 35546 RVA: 0x001D1AB8 File Offset: 0x001CFCB8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008ADB RID: 35547 RVA: 0x001D1AE2 File Offset: 0x001CFCE2
		public bool Equals(inputSRegions other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038B1 RID: 14513
		private ProgramNode _node;
	}
}
