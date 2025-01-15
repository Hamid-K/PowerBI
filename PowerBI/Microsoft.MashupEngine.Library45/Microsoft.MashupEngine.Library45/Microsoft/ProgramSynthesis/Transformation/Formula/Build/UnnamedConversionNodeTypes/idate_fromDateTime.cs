using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200153D RID: 5437
	public struct idate_fromDateTime : IProgramNodeBuilder, IEquatable<idate_fromDateTime>
	{
		// Token: 0x17001EC0 RID: 7872
		// (get) Token: 0x0600B159 RID: 45401 RVA: 0x002705BE File Offset: 0x0026E7BE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B15A RID: 45402 RVA: 0x002705C6 File Offset: 0x0026E7C6
		private idate_fromDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B15B RID: 45403 RVA: 0x002705CF File Offset: 0x0026E7CF
		public static idate_fromDateTime CreateUnsafe(ProgramNode node)
		{
			return new idate_fromDateTime(node);
		}

		// Token: 0x0600B15C RID: 45404 RVA: 0x002705D8 File Offset: 0x0026E7D8
		public static idate_fromDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.idate_fromDateTime)
			{
				return null;
			}
			return new idate_fromDateTime?(idate_fromDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600B15D RID: 45405 RVA: 0x0027060D File Offset: 0x0026E80D
		public idate_fromDateTime(GrammarBuilders g, fromDateTime value0)
		{
			this._node = g.UnnamedConversion.idate_fromDateTime.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B15E RID: 45406 RVA: 0x0027062C File Offset: 0x0026E82C
		public static implicit operator idate(idate_fromDateTime arg)
		{
			return idate.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EC1 RID: 7873
		// (get) Token: 0x0600B15F RID: 45407 RVA: 0x0027063A File Offset: 0x0026E83A
		public fromDateTime fromDateTime
		{
			get
			{
				return fromDateTime.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B160 RID: 45408 RVA: 0x0027064E File Offset: 0x0026E84E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B161 RID: 45409 RVA: 0x00270664 File Offset: 0x0026E864
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B162 RID: 45410 RVA: 0x0027068E File Offset: 0x0026E88E
		public bool Equals(idate_fromDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045EB RID: 17899
		private ProgramNode _node;
	}
}
