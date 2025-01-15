using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A23 RID: 6691
	public struct Object : IProgramNodeBuilder, IEquatable<Object>
	{
		// Token: 0x170024CB RID: 9419
		// (get) Token: 0x0600DBC8 RID: 56264 RVA: 0x002EDFBE File Offset: 0x002EC1BE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DBC9 RID: 56265 RVA: 0x002EDFC6 File Offset: 0x002EC1C6
		private Object(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DBCA RID: 56266 RVA: 0x002EDFCF File Offset: 0x002EC1CF
		public static Object CreateUnsafe(ProgramNode node)
		{
			return new Object(node);
		}

		// Token: 0x0600DBCB RID: 56267 RVA: 0x002EDFD8 File Offset: 0x002EC1D8
		public static Object? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Object)
			{
				return null;
			}
			return new Object?(Object.CreateUnsafe(node));
		}

		// Token: 0x0600DBCC RID: 56268 RVA: 0x002EE00D File Offset: 0x002EC20D
		public Object(GrammarBuilders g, property value0)
		{
			this._node = g.Rule.Object.BuildASTNode(value0.Node);
		}

		// Token: 0x0600DBCD RID: 56269 RVA: 0x002EE02C File Offset: 0x002EC22C
		public static implicit operator @object(Object arg)
		{
			return @object.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024CC RID: 9420
		// (get) Token: 0x0600DBCE RID: 56270 RVA: 0x002EE03A File Offset: 0x002EC23A
		public property property
		{
			get
			{
				return property.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600DBCF RID: 56271 RVA: 0x002EE04E File Offset: 0x002EC24E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DBD0 RID: 56272 RVA: 0x002EE064 File Offset: 0x002EC264
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DBD1 RID: 56273 RVA: 0x002EE08E File Offset: 0x002EC28E
		public bool Equals(Object other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005414 RID: 21524
		private ProgramNode _node;
	}
}
