using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001547 RID: 5447
	public struct fromStrTrim_fromNumberStr : IProgramNodeBuilder, IEquatable<fromStrTrim_fromNumberStr>
	{
		// Token: 0x17001ED4 RID: 7892
		// (get) Token: 0x0600B1BD RID: 45501 RVA: 0x00270EA6 File Offset: 0x0026F0A6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B1BE RID: 45502 RVA: 0x00270EAE File Offset: 0x0026F0AE
		private fromStrTrim_fromNumberStr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B1BF RID: 45503 RVA: 0x00270EB7 File Offset: 0x0026F0B7
		public static fromStrTrim_fromNumberStr CreateUnsafe(ProgramNode node)
		{
			return new fromStrTrim_fromNumberStr(node);
		}

		// Token: 0x0600B1C0 RID: 45504 RVA: 0x00270EC0 File Offset: 0x0026F0C0
		public static fromStrTrim_fromNumberStr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.fromStrTrim_fromNumberStr)
			{
				return null;
			}
			return new fromStrTrim_fromNumberStr?(fromStrTrim_fromNumberStr.CreateUnsafe(node));
		}

		// Token: 0x0600B1C1 RID: 45505 RVA: 0x00270EF5 File Offset: 0x0026F0F5
		public fromStrTrim_fromNumberStr(GrammarBuilders g, fromNumberStr value0)
		{
			this._node = g.UnnamedConversion.fromStrTrim_fromNumberStr.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B1C2 RID: 45506 RVA: 0x00270F14 File Offset: 0x0026F114
		public static implicit operator fromStrTrim(fromStrTrim_fromNumberStr arg)
		{
			return fromStrTrim.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001ED5 RID: 7893
		// (get) Token: 0x0600B1C3 RID: 45507 RVA: 0x00270F22 File Offset: 0x0026F122
		public fromNumberStr fromNumberStr
		{
			get
			{
				return fromNumberStr.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B1C4 RID: 45508 RVA: 0x00270F36 File Offset: 0x0026F136
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B1C5 RID: 45509 RVA: 0x00270F4C File Offset: 0x0026F14C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B1C6 RID: 45510 RVA: 0x00270F76 File Offset: 0x0026F176
		public bool Equals(fromStrTrim_fromNumberStr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045F5 RID: 17909
		private ProgramNode _node;
	}
}
