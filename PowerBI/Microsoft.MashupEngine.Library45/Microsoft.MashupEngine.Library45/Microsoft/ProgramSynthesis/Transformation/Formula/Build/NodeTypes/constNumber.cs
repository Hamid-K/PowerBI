using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015CD RID: 5581
	public struct constNumber : IProgramNodeBuilder, IEquatable<constNumber>
	{
		// Token: 0x17001FF3 RID: 8179
		// (get) Token: 0x0600B922 RID: 47394 RVA: 0x00280ADA File Offset: 0x0027ECDA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B923 RID: 47395 RVA: 0x00280AE2 File Offset: 0x0027ECE2
		private constNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B924 RID: 47396 RVA: 0x00280AEB File Offset: 0x0027ECEB
		public static constNumber CreateUnsafe(ProgramNode node)
		{
			return new constNumber(node);
		}

		// Token: 0x0600B925 RID: 47397 RVA: 0x00280AF4 File Offset: 0x0027ECF4
		public static constNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.constNumber)
			{
				return null;
			}
			return new constNumber?(constNumber.CreateUnsafe(node));
		}

		// Token: 0x0600B926 RID: 47398 RVA: 0x00280B2E File Offset: 0x0027ED2E
		public static constNumber CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new constNumber(new Hole(g.Symbol.constNumber, holeId));
		}

		// Token: 0x0600B927 RID: 47399 RVA: 0x00280B46 File Offset: 0x0027ED46
		public Number Cast_Number()
		{
			return Number.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B928 RID: 47400 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Number(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B929 RID: 47401 RVA: 0x00280B53 File Offset: 0x0027ED53
		public bool Is_Number(GrammarBuilders g, out Number value)
		{
			value = Number.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B92A RID: 47402 RVA: 0x00280B67 File Offset: 0x0027ED67
		public Number? As_Number(GrammarBuilders g)
		{
			return new Number?(Number.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B92B RID: 47403 RVA: 0x00280B79 File Offset: 0x0027ED79
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B92C RID: 47404 RVA: 0x00280B8C File Offset: 0x0027ED8C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B92D RID: 47405 RVA: 0x00280BB6 File Offset: 0x0027EDB6
		public bool Equals(constNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400467B RID: 18043
		private ProgramNode _node;
	}
}
