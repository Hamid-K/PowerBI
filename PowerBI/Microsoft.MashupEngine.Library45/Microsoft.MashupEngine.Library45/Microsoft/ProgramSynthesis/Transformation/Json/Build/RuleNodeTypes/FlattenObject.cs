using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A26 RID: 6694
	public struct FlattenObject : IProgramNodeBuilder, IEquatable<FlattenObject>
	{
		// Token: 0x170024D3 RID: 9427
		// (get) Token: 0x0600DBE8 RID: 56296 RVA: 0x002EE29A File Offset: 0x002EC49A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DBE9 RID: 56297 RVA: 0x002EE2A2 File Offset: 0x002EC4A2
		private FlattenObject(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DBEA RID: 56298 RVA: 0x002EE2AB File Offset: 0x002EC4AB
		public static FlattenObject CreateUnsafe(ProgramNode node)
		{
			return new FlattenObject(node);
		}

		// Token: 0x0600DBEB RID: 56299 RVA: 0x002EE2B4 File Offset: 0x002EC4B4
		public static FlattenObject? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FlattenObject)
			{
				return null;
			}
			return new FlattenObject?(FlattenObject.CreateUnsafe(node));
		}

		// Token: 0x0600DBEC RID: 56300 RVA: 0x002EE2E9 File Offset: 0x002EC4E9
		public FlattenObject(GrammarBuilders g, x value0, path value1)
		{
			this._node = g.Rule.FlattenObject.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600DBED RID: 56301 RVA: 0x002EE30F File Offset: 0x002EC50F
		public static implicit operator @object(FlattenObject arg)
		{
			return @object.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024D4 RID: 9428
		// (get) Token: 0x0600DBEE RID: 56302 RVA: 0x002EE31D File Offset: 0x002EC51D
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170024D5 RID: 9429
		// (get) Token: 0x0600DBEF RID: 56303 RVA: 0x002EE331 File Offset: 0x002EC531
		public path path
		{
			get
			{
				return path.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600DBF0 RID: 56304 RVA: 0x002EE345 File Offset: 0x002EC545
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DBF1 RID: 56305 RVA: 0x002EE358 File Offset: 0x002EC558
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DBF2 RID: 56306 RVA: 0x002EE382 File Offset: 0x002EC582
		public bool Equals(FlattenObject other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005417 RID: 21527
		private ProgramNode _node;
	}
}
