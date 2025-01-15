using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001568 RID: 5480
	public struct DateTimePart : IProgramNodeBuilder, IEquatable<DateTimePart>
	{
		// Token: 0x17001F34 RID: 7988
		// (get) Token: 0x0600B325 RID: 45861 RVA: 0x00272F4A File Offset: 0x0027114A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B326 RID: 45862 RVA: 0x00272F52 File Offset: 0x00271152
		private DateTimePart(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B327 RID: 45863 RVA: 0x00272F5B File Offset: 0x0027115B
		public static DateTimePart CreateUnsafe(ProgramNode node)
		{
			return new DateTimePart(node);
		}

		// Token: 0x0600B328 RID: 45864 RVA: 0x00272F64 File Offset: 0x00271164
		public static DateTimePart? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.DateTimePart)
			{
				return null;
			}
			return new DateTimePart?(DateTimePart.CreateUnsafe(node));
		}

		// Token: 0x0600B329 RID: 45865 RVA: 0x00272F99 File Offset: 0x00271199
		public DateTimePart(GrammarBuilders g, idate value0, dateTimePartKind value1)
		{
			this._node = g.Rule.DateTimePart.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B32A RID: 45866 RVA: 0x00272FBF File Offset: 0x002711BF
		public static implicit operator number1(DateTimePart arg)
		{
			return number1.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F35 RID: 7989
		// (get) Token: 0x0600B32B RID: 45867 RVA: 0x00272FCD File Offset: 0x002711CD
		public idate idate
		{
			get
			{
				return idate.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F36 RID: 7990
		// (get) Token: 0x0600B32C RID: 45868 RVA: 0x00272FE1 File Offset: 0x002711E1
		public dateTimePartKind dateTimePartKind
		{
			get
			{
				return dateTimePartKind.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B32D RID: 45869 RVA: 0x00272FF5 File Offset: 0x002711F5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B32E RID: 45870 RVA: 0x00273008 File Offset: 0x00271208
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B32F RID: 45871 RVA: 0x00273032 File Offset: 0x00271232
		public bool Equals(DateTimePart other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004616 RID: 17942
		private ProgramNode _node;
	}
}
