using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes
{
	// Token: 0x02000B6A RID: 2922
	public struct wrapStruct : IProgramNodeBuilder, IEquatable<wrapStruct>
	{
		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06004A13 RID: 18963 RVA: 0x000E93AE File Offset: 0x000E75AE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004A14 RID: 18964 RVA: 0x000E93B6 File Offset: 0x000E75B6
		private wrapStruct(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004A15 RID: 18965 RVA: 0x000E93BF File Offset: 0x000E75BF
		public static wrapStruct CreateUnsafe(ProgramNode node)
		{
			return new wrapStruct(node);
		}

		// Token: 0x06004A16 RID: 18966 RVA: 0x000E93C8 File Offset: 0x000E75C8
		public static wrapStruct? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.wrapStruct)
			{
				return null;
			}
			return new wrapStruct?(wrapStruct.CreateUnsafe(node));
		}

		// Token: 0x06004A17 RID: 18967 RVA: 0x000E9402 File Offset: 0x000E7602
		public static wrapStruct CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new wrapStruct(new Hole(g.Symbol.wrapStruct, holeId));
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x000E941A File Offset: 0x000E761A
		public WrapStructLet Cast_WrapStructLet()
		{
			return WrapStructLet.CreateUnsafe(this.Node);
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_WrapStructLet(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x000E9427 File Offset: 0x000E7627
		public bool Is_WrapStructLet(GrammarBuilders g, out WrapStructLet value)
		{
			value = WrapStructLet.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06004A1B RID: 18971 RVA: 0x000E943B File Offset: 0x000E763B
		public WrapStructLet? As_WrapStructLet(GrammarBuilders g)
		{
			return new WrapStructLet?(WrapStructLet.CreateUnsafe(this.Node));
		}

		// Token: 0x06004A1C RID: 18972 RVA: 0x000E944D File Offset: 0x000E764D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004A1D RID: 18973 RVA: 0x000E9460 File Offset: 0x000E7660
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004A1E RID: 18974 RVA: 0x000E948A File Offset: 0x000E768A
		public bool Equals(wrapStruct other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002165 RID: 8549
		private ProgramNode _node;
	}
}
