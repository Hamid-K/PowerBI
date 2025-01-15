using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F43 RID: 3907
	public struct _LetB3 : IProgramNodeBuilder, IEquatable<_LetB3>
	{
		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x06006CC5 RID: 27845 RVA: 0x00163B3E File Offset: 0x00161D3E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006CC6 RID: 27846 RVA: 0x00163B46 File Offset: 0x00161D46
		private _LetB3(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006CC7 RID: 27847 RVA: 0x00163B4F File Offset: 0x00161D4F
		public static _LetB3 CreateUnsafe(ProgramNode node)
		{
			return new _LetB3(node);
		}

		// Token: 0x06006CC8 RID: 27848 RVA: 0x00163B58 File Offset: 0x00161D58
		public static _LetB3? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB3)
			{
				return null;
			}
			return new _LetB3?(_LetB3.CreateUnsafe(node));
		}

		// Token: 0x06006CC9 RID: 27849 RVA: 0x00163B92 File Offset: 0x00161D92
		public static _LetB3 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB3(new Hole(g.Symbol._LetB3, holeId));
		}

		// Token: 0x06006CCA RID: 27850 RVA: 0x00163BAA File Offset: 0x00161DAA
		public First Cast_First()
		{
			return First.CreateUnsafe(this.Node);
		}

		// Token: 0x06006CCB RID: 27851 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_First(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06006CCC RID: 27852 RVA: 0x00163BB7 File Offset: 0x00161DB7
		public bool Is_First(GrammarBuilders g, out First value)
		{
			value = First.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06006CCD RID: 27853 RVA: 0x00163BCB File Offset: 0x00161DCB
		public First? As_First(GrammarBuilders g)
		{
			return new First?(First.CreateUnsafe(this.Node));
		}

		// Token: 0x06006CCE RID: 27854 RVA: 0x00163BDD File Offset: 0x00161DDD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006CCF RID: 27855 RVA: 0x00163BF0 File Offset: 0x00161DF0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006CD0 RID: 27856 RVA: 0x00163C1A File Offset: 0x00161E1A
		public bool Equals(_LetB3 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F2E RID: 12078
		private ProgramNode _node;
	}
}
