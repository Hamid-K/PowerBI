using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200158A RID: 5514
	public struct TrimFull : IProgramNodeBuilder, IEquatable<TrimFull>
	{
		// Token: 0x17001F97 RID: 8087
		// (get) Token: 0x0600B498 RID: 46232 RVA: 0x002750BA File Offset: 0x002732BA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B499 RID: 46233 RVA: 0x002750C2 File Offset: 0x002732C2
		private TrimFull(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B49A RID: 46234 RVA: 0x002750CB File Offset: 0x002732CB
		public static TrimFull CreateUnsafe(ProgramNode node)
		{
			return new TrimFull(node);
		}

		// Token: 0x0600B49B RID: 46235 RVA: 0x002750D4 File Offset: 0x002732D4
		public static TrimFull? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TrimFull)
			{
				return null;
			}
			return new TrimFull?(TrimFull.CreateUnsafe(node));
		}

		// Token: 0x0600B49C RID: 46236 RVA: 0x00275109 File Offset: 0x00273309
		public TrimFull(GrammarBuilders g, fromStr value0)
		{
			this._node = g.Rule.TrimFull.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B49D RID: 46237 RVA: 0x00275128 File Offset: 0x00273328
		public static implicit operator fromStrTrim(TrimFull arg)
		{
			return fromStrTrim.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F98 RID: 8088
		// (get) Token: 0x0600B49E RID: 46238 RVA: 0x00275136 File Offset: 0x00273336
		public fromStr fromStr
		{
			get
			{
				return fromStr.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B49F RID: 46239 RVA: 0x0027514A File Offset: 0x0027334A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B4A0 RID: 46240 RVA: 0x00275160 File Offset: 0x00273360
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B4A1 RID: 46241 RVA: 0x0027518A File Offset: 0x0027338A
		public bool Equals(TrimFull other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004638 RID: 17976
		private ProgramNode _node;
	}
}
