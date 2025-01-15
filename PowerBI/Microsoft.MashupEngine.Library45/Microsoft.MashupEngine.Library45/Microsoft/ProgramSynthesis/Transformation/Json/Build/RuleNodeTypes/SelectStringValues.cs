using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A31 RID: 6705
	public struct SelectStringValues : IProgramNodeBuilder, IEquatable<SelectStringValues>
	{
		// Token: 0x170024F1 RID: 9457
		// (get) Token: 0x0600DC5E RID: 56414 RVA: 0x002EED2A File Offset: 0x002ECF2A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DC5F RID: 56415 RVA: 0x002EED32 File Offset: 0x002ECF32
		private SelectStringValues(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DC60 RID: 56416 RVA: 0x002EED3B File Offset: 0x002ECF3B
		public static SelectStringValues CreateUnsafe(ProgramNode node)
		{
			return new SelectStringValues(node);
		}

		// Token: 0x0600DC61 RID: 56417 RVA: 0x002EED44 File Offset: 0x002ECF44
		public static SelectStringValues? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SelectStringValues)
			{
				return null;
			}
			return new SelectStringValues?(SelectStringValues.CreateUnsafe(node));
		}

		// Token: 0x0600DC62 RID: 56418 RVA: 0x002EED79 File Offset: 0x002ECF79
		public SelectStringValues(GrammarBuilders g, x value0)
		{
			this._node = g.Rule.SelectStringValues.BuildASTNode(value0.Node);
		}

		// Token: 0x0600DC63 RID: 56419 RVA: 0x002EED98 File Offset: 0x002ECF98
		public static implicit operator _LetB0(SelectStringValues arg)
		{
			return _LetB0.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024F2 RID: 9458
		// (get) Token: 0x0600DC64 RID: 56420 RVA: 0x002EEDA6 File Offset: 0x002ECFA6
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600DC65 RID: 56421 RVA: 0x002EEDBA File Offset: 0x002ECFBA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DC66 RID: 56422 RVA: 0x002EEDD0 File Offset: 0x002ECFD0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DC67 RID: 56423 RVA: 0x002EEDFA File Offset: 0x002ECFFA
		public bool Equals(SelectStringValues other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005422 RID: 21538
		private ProgramNode _node;
	}
}
