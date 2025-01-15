using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes
{
	// Token: 0x02000C04 RID: 3076
	public struct _LetB0 : IProgramNodeBuilder, IEquatable<_LetB0>
	{
		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06004F67 RID: 20327 RVA: 0x000FAAFA File Offset: 0x000F8CFA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004F68 RID: 20328 RVA: 0x000FAB02 File Offset: 0x000F8D02
		private _LetB0(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x000FAB0B File Offset: 0x000F8D0B
		public static _LetB0 CreateUnsafe(ProgramNode node)
		{
			return new _LetB0(node);
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x000FAB14 File Offset: 0x000F8D14
		public static _LetB0? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB0)
			{
				return null;
			}
			return new _LetB0?(_LetB0.CreateUnsafe(node));
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x000FAB4E File Offset: 0x000F8D4E
		public static _LetB0 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB0(new Hole(g.Symbol._LetB0, holeId));
		}

		// Token: 0x06004F6C RID: 20332 RVA: 0x000FAB66 File Offset: 0x000F8D66
		public Between Cast_Between()
		{
			return Between.CreateUnsafe(this.Node);
		}

		// Token: 0x06004F6D RID: 20333 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Between(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06004F6E RID: 20334 RVA: 0x000FAB73 File Offset: 0x000F8D73
		public bool Is_Between(GrammarBuilders g, out Between value)
		{
			value = Between.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06004F6F RID: 20335 RVA: 0x000FAB87 File Offset: 0x000F8D87
		public Between? As_Between(GrammarBuilders g)
		{
			return new Between?(Between.CreateUnsafe(this.Node));
		}

		// Token: 0x06004F70 RID: 20336 RVA: 0x000FAB99 File Offset: 0x000F8D99
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004F71 RID: 20337 RVA: 0x000FABAC File Offset: 0x000F8DAC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004F72 RID: 20338 RVA: 0x000FABD6 File Offset: 0x000F8DD6
		public bool Equals(_LetB0 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400232C RID: 9004
		private ProgramNode _node;
	}
}
