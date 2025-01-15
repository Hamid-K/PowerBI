using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A25 RID: 6693
	public struct SelectObject : IProgramNodeBuilder, IEquatable<SelectObject>
	{
		// Token: 0x170024D0 RID: 9424
		// (get) Token: 0x0600DBDD RID: 56285 RVA: 0x002EE19E File Offset: 0x002EC39E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DBDE RID: 56286 RVA: 0x002EE1A6 File Offset: 0x002EC3A6
		private SelectObject(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DBDF RID: 56287 RVA: 0x002EE1AF File Offset: 0x002EC3AF
		public static SelectObject CreateUnsafe(ProgramNode node)
		{
			return new SelectObject(node);
		}

		// Token: 0x0600DBE0 RID: 56288 RVA: 0x002EE1B8 File Offset: 0x002EC3B8
		public static SelectObject? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SelectObject)
			{
				return null;
			}
			return new SelectObject?(SelectObject.CreateUnsafe(node));
		}

		// Token: 0x0600DBE1 RID: 56289 RVA: 0x002EE1ED File Offset: 0x002EC3ED
		public SelectObject(GrammarBuilders g, x value0, path value1)
		{
			this._node = g.Rule.SelectObject.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600DBE2 RID: 56290 RVA: 0x002EE213 File Offset: 0x002EC413
		public static implicit operator @object(SelectObject arg)
		{
			return @object.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024D1 RID: 9425
		// (get) Token: 0x0600DBE3 RID: 56291 RVA: 0x002EE221 File Offset: 0x002EC421
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170024D2 RID: 9426
		// (get) Token: 0x0600DBE4 RID: 56292 RVA: 0x002EE235 File Offset: 0x002EC435
		public path path
		{
			get
			{
				return path.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600DBE5 RID: 56293 RVA: 0x002EE249 File Offset: 0x002EC449
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DBE6 RID: 56294 RVA: 0x002EE25C File Offset: 0x002EC45C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DBE7 RID: 56295 RVA: 0x002EE286 File Offset: 0x002EC486
		public bool Equals(SelectObject other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005416 RID: 21526
		private ProgramNode _node;
	}
}
