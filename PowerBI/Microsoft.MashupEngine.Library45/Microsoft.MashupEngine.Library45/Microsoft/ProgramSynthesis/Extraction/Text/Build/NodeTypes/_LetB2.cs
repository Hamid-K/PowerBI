using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F42 RID: 3906
	public struct _LetB2 : IProgramNodeBuilder, IEquatable<_LetB2>
	{
		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x06006CB9 RID: 27833 RVA: 0x00163A4E File Offset: 0x00161C4E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006CBA RID: 27834 RVA: 0x00163A56 File Offset: 0x00161C56
		private _LetB2(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006CBB RID: 27835 RVA: 0x00163A5F File Offset: 0x00161C5F
		public static _LetB2 CreateUnsafe(ProgramNode node)
		{
			return new _LetB2(node);
		}

		// Token: 0x06006CBC RID: 27836 RVA: 0x00163A68 File Offset: 0x00161C68
		public static _LetB2? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB2)
			{
				return null;
			}
			return new _LetB2?(_LetB2.CreateUnsafe(node));
		}

		// Token: 0x06006CBD RID: 27837 RVA: 0x00163AA2 File Offset: 0x00161CA2
		public static _LetB2 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB2(new Hole(g.Symbol._LetB2, holeId));
		}

		// Token: 0x06006CBE RID: 27838 RVA: 0x00163ABA File Offset: 0x00161CBA
		public LetPrepend Cast_LetPrepend()
		{
			return LetPrepend.CreateUnsafe(this.Node);
		}

		// Token: 0x06006CBF RID: 27839 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LetPrepend(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06006CC0 RID: 27840 RVA: 0x00163AC7 File Offset: 0x00161CC7
		public bool Is_LetPrepend(GrammarBuilders g, out LetPrepend value)
		{
			value = LetPrepend.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06006CC1 RID: 27841 RVA: 0x00163ADB File Offset: 0x00161CDB
		public LetPrepend? As_LetPrepend(GrammarBuilders g)
		{
			return new LetPrepend?(LetPrepend.CreateUnsafe(this.Node));
		}

		// Token: 0x06006CC2 RID: 27842 RVA: 0x00163AED File Offset: 0x00161CED
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006CC3 RID: 27843 RVA: 0x00163B00 File Offset: 0x00161D00
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006CC4 RID: 27844 RVA: 0x00163B2A File Offset: 0x00161D2A
		public bool Equals(_LetB2 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F2D RID: 12077
		private ProgramNode _node;
	}
}
