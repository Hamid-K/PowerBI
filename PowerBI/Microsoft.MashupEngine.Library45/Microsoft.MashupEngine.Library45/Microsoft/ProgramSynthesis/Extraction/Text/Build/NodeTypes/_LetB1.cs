using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F41 RID: 3905
	public struct _LetB1 : IProgramNodeBuilder, IEquatable<_LetB1>
	{
		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x06006CAD RID: 27821 RVA: 0x0016395E File Offset: 0x00161B5E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006CAE RID: 27822 RVA: 0x00163966 File Offset: 0x00161B66
		private _LetB1(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006CAF RID: 27823 RVA: 0x0016396F File Offset: 0x00161B6F
		public static _LetB1 CreateUnsafe(ProgramNode node)
		{
			return new _LetB1(node);
		}

		// Token: 0x06006CB0 RID: 27824 RVA: 0x00163978 File Offset: 0x00161B78
		public static _LetB1? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB1)
			{
				return null;
			}
			return new _LetB1?(_LetB1.CreateUnsafe(node));
		}

		// Token: 0x06006CB1 RID: 27825 RVA: 0x001639B2 File Offset: 0x00161BB2
		public static _LetB1 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB1(new Hole(g.Symbol._LetB1, holeId));
		}

		// Token: 0x06006CB2 RID: 27826 RVA: 0x001639CA File Offset: 0x00161BCA
		public Prepend Cast_Prepend()
		{
			return Prepend.CreateUnsafe(this.Node);
		}

		// Token: 0x06006CB3 RID: 27827 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Prepend(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06006CB4 RID: 27828 RVA: 0x001639D7 File Offset: 0x00161BD7
		public bool Is_Prepend(GrammarBuilders g, out Prepend value)
		{
			value = Prepend.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06006CB5 RID: 27829 RVA: 0x001639EB File Offset: 0x00161BEB
		public Prepend? As_Prepend(GrammarBuilders g)
		{
			return new Prepend?(Prepend.CreateUnsafe(this.Node));
		}

		// Token: 0x06006CB6 RID: 27830 RVA: 0x001639FD File Offset: 0x00161BFD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006CB7 RID: 27831 RVA: 0x00163A10 File Offset: 0x00161C10
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006CB8 RID: 27832 RVA: 0x00163A3A File Offset: 0x00161C3A
		public bool Equals(_LetB1 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F2C RID: 12076
		private ProgramNode _node;
	}
}
