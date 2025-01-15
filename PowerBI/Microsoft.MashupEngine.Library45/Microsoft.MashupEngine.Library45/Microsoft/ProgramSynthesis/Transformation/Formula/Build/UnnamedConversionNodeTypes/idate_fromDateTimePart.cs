using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200153E RID: 5438
	public struct idate_fromDateTimePart : IProgramNodeBuilder, IEquatable<idate_fromDateTimePart>
	{
		// Token: 0x17001EC2 RID: 7874
		// (get) Token: 0x0600B163 RID: 45411 RVA: 0x002706A2 File Offset: 0x0026E8A2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B164 RID: 45412 RVA: 0x002706AA File Offset: 0x0026E8AA
		private idate_fromDateTimePart(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B165 RID: 45413 RVA: 0x002706B3 File Offset: 0x0026E8B3
		public static idate_fromDateTimePart CreateUnsafe(ProgramNode node)
		{
			return new idate_fromDateTimePart(node);
		}

		// Token: 0x0600B166 RID: 45414 RVA: 0x002706BC File Offset: 0x0026E8BC
		public static idate_fromDateTimePart? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.idate_fromDateTimePart)
			{
				return null;
			}
			return new idate_fromDateTimePart?(idate_fromDateTimePart.CreateUnsafe(node));
		}

		// Token: 0x0600B167 RID: 45415 RVA: 0x002706F1 File Offset: 0x0026E8F1
		public idate_fromDateTimePart(GrammarBuilders g, fromDateTimePart value0)
		{
			this._node = g.UnnamedConversion.idate_fromDateTimePart.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B168 RID: 45416 RVA: 0x00270710 File Offset: 0x0026E910
		public static implicit operator idate(idate_fromDateTimePart arg)
		{
			return idate.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EC3 RID: 7875
		// (get) Token: 0x0600B169 RID: 45417 RVA: 0x0027071E File Offset: 0x0026E91E
		public fromDateTimePart fromDateTimePart
		{
			get
			{
				return fromDateTimePart.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B16A RID: 45418 RVA: 0x00270732 File Offset: 0x0026E932
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B16B RID: 45419 RVA: 0x00270748 File Offset: 0x0026E948
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B16C RID: 45420 RVA: 0x00270772 File Offset: 0x0026E972
		public bool Equals(idate_fromDateTimePart other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045EC RID: 17900
		private ProgramNode _node;
	}
}
