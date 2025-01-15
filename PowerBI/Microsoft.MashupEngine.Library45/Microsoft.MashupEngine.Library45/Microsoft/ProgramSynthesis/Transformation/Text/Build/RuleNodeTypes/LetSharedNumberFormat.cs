using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C2E RID: 7214
	public struct LetSharedNumberFormat : IProgramNodeBuilder, IEquatable<LetSharedNumberFormat>
	{
		// Token: 0x170028B2 RID: 10418
		// (get) Token: 0x0600F301 RID: 62209 RVA: 0x00341DBA File Offset: 0x0033FFBA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F302 RID: 62210 RVA: 0x00341DC2 File Offset: 0x0033FFC2
		private LetSharedNumberFormat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F303 RID: 62211 RVA: 0x00341DCB File Offset: 0x0033FFCB
		public static LetSharedNumberFormat CreateUnsafe(ProgramNode node)
		{
			return new LetSharedNumberFormat(node);
		}

		// Token: 0x0600F304 RID: 62212 RVA: 0x00341DD4 File Offset: 0x0033FFD4
		public static LetSharedNumberFormat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetSharedNumberFormat)
			{
				return null;
			}
			return new LetSharedNumberFormat?(LetSharedNumberFormat.CreateUnsafe(node));
		}

		// Token: 0x0600F305 RID: 62213 RVA: 0x00341E09 File Offset: 0x00340009
		public LetSharedNumberFormat(GrammarBuilders g, numberFormat value0, rangeString value1)
		{
			this._node = new LetNode(g.Rule.LetSharedNumberFormat, value0.Node, value1.Node);
		}

		// Token: 0x0600F306 RID: 62214 RVA: 0x00341E2F File Offset: 0x0034002F
		public static implicit operator _LetB0(LetSharedNumberFormat arg)
		{
			return _LetB0.CreateUnsafe(arg.Node);
		}

		// Token: 0x170028B3 RID: 10419
		// (get) Token: 0x0600F307 RID: 62215 RVA: 0x00341E3D File Offset: 0x0034003D
		public numberFormat numberFormat
		{
			get
			{
				return numberFormat.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170028B4 RID: 10420
		// (get) Token: 0x0600F308 RID: 62216 RVA: 0x00341E51 File Offset: 0x00340051
		public rangeString rangeString
		{
			get
			{
				return rangeString.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F309 RID: 62217 RVA: 0x00341E65 File Offset: 0x00340065
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F30A RID: 62218 RVA: 0x00341E78 File Offset: 0x00340078
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F30B RID: 62219 RVA: 0x00341EA2 File Offset: 0x003400A2
		public bool Equals(LetSharedNumberFormat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B1D RID: 23325
		private ProgramNode _node;
	}
}
