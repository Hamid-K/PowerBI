using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001540 RID: 5440
	public struct parseSubject_fromStr : IProgramNodeBuilder, IEquatable<parseSubject_fromStr>
	{
		// Token: 0x17001EC6 RID: 7878
		// (get) Token: 0x0600B177 RID: 45431 RVA: 0x0027086A File Offset: 0x0026EA6A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B178 RID: 45432 RVA: 0x00270872 File Offset: 0x0026EA72
		private parseSubject_fromStr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B179 RID: 45433 RVA: 0x0027087B File Offset: 0x0026EA7B
		public static parseSubject_fromStr CreateUnsafe(ProgramNode node)
		{
			return new parseSubject_fromStr(node);
		}

		// Token: 0x0600B17A RID: 45434 RVA: 0x00270884 File Offset: 0x0026EA84
		public static parseSubject_fromStr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.parseSubject_fromStr)
			{
				return null;
			}
			return new parseSubject_fromStr?(parseSubject_fromStr.CreateUnsafe(node));
		}

		// Token: 0x0600B17B RID: 45435 RVA: 0x002708B9 File Offset: 0x0026EAB9
		public parseSubject_fromStr(GrammarBuilders g, fromStr value0)
		{
			this._node = g.UnnamedConversion.parseSubject_fromStr.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B17C RID: 45436 RVA: 0x002708D8 File Offset: 0x0026EAD8
		public static implicit operator parseSubject(parseSubject_fromStr arg)
		{
			return parseSubject.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EC7 RID: 7879
		// (get) Token: 0x0600B17D RID: 45437 RVA: 0x002708E6 File Offset: 0x0026EAE6
		public fromStr fromStr
		{
			get
			{
				return fromStr.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B17E RID: 45438 RVA: 0x002708FA File Offset: 0x0026EAFA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B17F RID: 45439 RVA: 0x00270910 File Offset: 0x0026EB10
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B180 RID: 45440 RVA: 0x0027093A File Offset: 0x0026EB3A
		public bool Equals(parseSubject_fromStr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045EE RID: 17902
		private ProgramNode _node;
	}
}
