using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F40 RID: 3904
	public struct _LetB0 : IProgramNodeBuilder, IEquatable<_LetB0>
	{
		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x06006CA1 RID: 27809 RVA: 0x0016386E File Offset: 0x00161A6E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006CA2 RID: 27810 RVA: 0x00163876 File Offset: 0x00161A76
		private _LetB0(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006CA3 RID: 27811 RVA: 0x0016387F File Offset: 0x00161A7F
		public static _LetB0 CreateUnsafe(ProgramNode node)
		{
			return new _LetB0(node);
		}

		// Token: 0x06006CA4 RID: 27812 RVA: 0x00163888 File Offset: 0x00161A88
		public static _LetB0? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB0)
			{
				return null;
			}
			return new _LetB0?(_LetB0.CreateUnsafe(node));
		}

		// Token: 0x06006CA5 RID: 27813 RVA: 0x001638C2 File Offset: 0x00161AC2
		public static _LetB0 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB0(new Hole(g.Symbol._LetB0, holeId));
		}

		// Token: 0x06006CA6 RID: 27814 RVA: 0x001638DA File Offset: 0x00161ADA
		public Second Cast_Second()
		{
			return Second.CreateUnsafe(this.Node);
		}

		// Token: 0x06006CA7 RID: 27815 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Second(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06006CA8 RID: 27816 RVA: 0x001638E7 File Offset: 0x00161AE7
		public bool Is_Second(GrammarBuilders g, out Second value)
		{
			value = Second.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06006CA9 RID: 27817 RVA: 0x001638FB File Offset: 0x00161AFB
		public Second? As_Second(GrammarBuilders g)
		{
			return new Second?(Second.CreateUnsafe(this.Node));
		}

		// Token: 0x06006CAA RID: 27818 RVA: 0x0016390D File Offset: 0x00161B0D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006CAB RID: 27819 RVA: 0x00163920 File Offset: 0x00161B20
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006CAC RID: 27820 RVA: 0x0016394A File Offset: 0x00161B4A
		public bool Equals(_LetB0 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F2B RID: 12075
		private ProgramNode _node;
	}
}
