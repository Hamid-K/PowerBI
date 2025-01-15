using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001E5E RID: 7774
	public struct children_interval : IProgramNodeBuilder, IEquatable<children_interval>
	{
		// Token: 0x17002B7C RID: 11132
		// (get) Token: 0x060105F9 RID: 67065 RVA: 0x0038904E File Offset: 0x0038724E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060105FA RID: 67066 RVA: 0x00389056 File Offset: 0x00387256
		private children_interval(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060105FB RID: 67067 RVA: 0x0038905F File Offset: 0x0038725F
		public static children_interval CreateUnsafe(ProgramNode node)
		{
			return new children_interval(node);
		}

		// Token: 0x060105FC RID: 67068 RVA: 0x00389068 File Offset: 0x00387268
		public static children_interval? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.children_interval)
			{
				return null;
			}
			return new children_interval?(children_interval.CreateUnsafe(node));
		}

		// Token: 0x060105FD RID: 67069 RVA: 0x0038909D File Offset: 0x0038729D
		public children_interval(GrammarBuilders g, interval value0)
		{
			this._node = g.UnnamedConversion.children_interval.BuildASTNode(value0.Node);
		}

		// Token: 0x060105FE RID: 67070 RVA: 0x003890BC File Offset: 0x003872BC
		public static implicit operator children(children_interval arg)
		{
			return children.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002B7D RID: 11133
		// (get) Token: 0x060105FF RID: 67071 RVA: 0x003890CA File Offset: 0x003872CA
		public interval interval
		{
			get
			{
				return interval.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06010600 RID: 67072 RVA: 0x003890DE File Offset: 0x003872DE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010601 RID: 67073 RVA: 0x003890F4 File Offset: 0x003872F4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010602 RID: 67074 RVA: 0x0038911E File Offset: 0x0038731E
		public bool Equals(children_interval other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400629D RID: 25245
		private ProgramNode _node;
	}
}
