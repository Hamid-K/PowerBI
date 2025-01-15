using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x02000948 RID: 2376
	public struct TwoLineKeyValue : IProgramNodeBuilder, IEquatable<TwoLineKeyValue>
	{
		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x0600375A RID: 14170 RVA: 0x000ADBFA File Offset: 0x000ABDFA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x000ADC02 File Offset: 0x000ABE02
		private TwoLineKeyValue(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x000ADC0B File Offset: 0x000ABE0B
		public static TwoLineKeyValue CreateUnsafe(ProgramNode node)
		{
			return new TwoLineKeyValue(node);
		}

		// Token: 0x0600375D RID: 14173 RVA: 0x000ADC14 File Offset: 0x000ABE14
		public static TwoLineKeyValue? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TwoLineKeyValue)
			{
				return null;
			}
			return new TwoLineKeyValue?(TwoLineKeyValue.CreateUnsafe(node));
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x000ADC49 File Offset: 0x000ABE49
		public TwoLineKeyValue(GrammarBuilders g, key value0, sep value1, records value2)
		{
			this._node = g.Rule.TwoLineKeyValue.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600375F RID: 14175 RVA: 0x000ADC76 File Offset: 0x000ABE76
		public static implicit operator primarySelector(TwoLineKeyValue arg)
		{
			return primarySelector.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06003760 RID: 14176 RVA: 0x000ADC84 File Offset: 0x000ABE84
		public key key
		{
			get
			{
				return key.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06003761 RID: 14177 RVA: 0x000ADC98 File Offset: 0x000ABE98
		public sep sep
		{
			get
			{
				return sep.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06003762 RID: 14178 RVA: 0x000ADCAC File Offset: 0x000ABEAC
		public records records
		{
			get
			{
				return records.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x000ADCC0 File Offset: 0x000ABEC0
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003764 RID: 14180 RVA: 0x000ADCD4 File Offset: 0x000ABED4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x000ADCFE File Offset: 0x000ABEFE
		public bool Equals(TwoLineKeyValue other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A68 RID: 6760
		private ProgramNode _node;
	}
}
