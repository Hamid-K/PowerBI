using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200133C RID: 4924
	public struct cndExtPoint_extPoint : IProgramNodeBuilder, IEquatable<cndExtPoint_extPoint>
	{
		// Token: 0x170019FD RID: 6653
		// (get) Token: 0x060097A0 RID: 38816 RVA: 0x00205962 File Offset: 0x00203B62
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060097A1 RID: 38817 RVA: 0x0020596A File Offset: 0x00203B6A
		private cndExtPoint_extPoint(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060097A2 RID: 38818 RVA: 0x00205973 File Offset: 0x00203B73
		public static cndExtPoint_extPoint CreateUnsafe(ProgramNode node)
		{
			return new cndExtPoint_extPoint(node);
		}

		// Token: 0x060097A3 RID: 38819 RVA: 0x0020597C File Offset: 0x00203B7C
		public static cndExtPoint_extPoint? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.cndExtPoint_extPoint)
			{
				return null;
			}
			return new cndExtPoint_extPoint?(cndExtPoint_extPoint.CreateUnsafe(node));
		}

		// Token: 0x060097A4 RID: 38820 RVA: 0x002059B1 File Offset: 0x00203BB1
		public cndExtPoint_extPoint(GrammarBuilders g, extPoint value0)
		{
			this._node = g.UnnamedConversion.cndExtPoint_extPoint.BuildASTNode(value0.Node);
		}

		// Token: 0x060097A5 RID: 38821 RVA: 0x002059D0 File Offset: 0x00203BD0
		public static implicit operator cndExtPoint(cndExtPoint_extPoint arg)
		{
			return cndExtPoint.CreateUnsafe(arg.Node);
		}

		// Token: 0x170019FE RID: 6654
		// (get) Token: 0x060097A6 RID: 38822 RVA: 0x002059DE File Offset: 0x00203BDE
		public extPoint extPoint
		{
			get
			{
				return extPoint.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060097A7 RID: 38823 RVA: 0x002059F2 File Offset: 0x00203BF2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060097A8 RID: 38824 RVA: 0x00205A08 File Offset: 0x00203C08
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060097A9 RID: 38825 RVA: 0x00205A32 File Offset: 0x00203C32
		public bool Equals(cndExtPoint_extPoint other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DB3 RID: 15795
		private ProgramNode _node;
	}
}
