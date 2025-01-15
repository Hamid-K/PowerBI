using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015CE RID: 5582
	public struct constDate : IProgramNodeBuilder, IEquatable<constDate>
	{
		// Token: 0x17001FF4 RID: 8180
		// (get) Token: 0x0600B92E RID: 47406 RVA: 0x00280BCA File Offset: 0x0027EDCA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B92F RID: 47407 RVA: 0x00280BD2 File Offset: 0x0027EDD2
		private constDate(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B930 RID: 47408 RVA: 0x00280BDB File Offset: 0x0027EDDB
		public static constDate CreateUnsafe(ProgramNode node)
		{
			return new constDate(node);
		}

		// Token: 0x0600B931 RID: 47409 RVA: 0x00280BE4 File Offset: 0x0027EDE4
		public static constDate? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.constDate)
			{
				return null;
			}
			return new constDate?(constDate.CreateUnsafe(node));
		}

		// Token: 0x0600B932 RID: 47410 RVA: 0x00280C1E File Offset: 0x0027EE1E
		public static constDate CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new constDate(new Hole(g.Symbol.constDate, holeId));
		}

		// Token: 0x0600B933 RID: 47411 RVA: 0x00280C36 File Offset: 0x0027EE36
		public Date Cast_Date()
		{
			return Date.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B934 RID: 47412 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Date(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B935 RID: 47413 RVA: 0x00280C43 File Offset: 0x0027EE43
		public bool Is_Date(GrammarBuilders g, out Date value)
		{
			value = Date.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B936 RID: 47414 RVA: 0x00280C57 File Offset: 0x0027EE57
		public Date? As_Date(GrammarBuilders g)
		{
			return new Date?(Date.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B937 RID: 47415 RVA: 0x00280C69 File Offset: 0x0027EE69
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B938 RID: 47416 RVA: 0x00280C7C File Offset: 0x0027EE7C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B939 RID: 47417 RVA: 0x00280CA6 File Offset: 0x0027EEA6
		public bool Equals(constDate other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400467C RID: 18044
		private ProgramNode _node;
	}
}
