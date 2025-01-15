using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A28 RID: 6696
	public struct Property : IProgramNodeBuilder, IEquatable<Property>
	{
		// Token: 0x170024D8 RID: 9432
		// (get) Token: 0x0600DBFD RID: 56317 RVA: 0x002EE47A File Offset: 0x002EC67A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DBFE RID: 56318 RVA: 0x002EE482 File Offset: 0x002EC682
		private Property(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DBFF RID: 56319 RVA: 0x002EE48B File Offset: 0x002EC68B
		public static Property CreateUnsafe(ProgramNode node)
		{
			return new Property(node);
		}

		// Token: 0x0600DC00 RID: 56320 RVA: 0x002EE494 File Offset: 0x002EC694
		public static Property? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Property)
			{
				return null;
			}
			return new Property?(Property.CreateUnsafe(node));
		}

		// Token: 0x0600DC01 RID: 56321 RVA: 0x002EE4C9 File Offset: 0x002EC6C9
		public Property(GrammarBuilders g, key value0, value value1)
		{
			this._node = g.Rule.Property.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600DC02 RID: 56322 RVA: 0x002EE4EF File Offset: 0x002EC6EF
		public static implicit operator property(Property arg)
		{
			return property.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024D9 RID: 9433
		// (get) Token: 0x0600DC03 RID: 56323 RVA: 0x002EE4FD File Offset: 0x002EC6FD
		public key key
		{
			get
			{
				return key.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170024DA RID: 9434
		// (get) Token: 0x0600DC04 RID: 56324 RVA: 0x002EE511 File Offset: 0x002EC711
		public value value
		{
			get
			{
				return value.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600DC05 RID: 56325 RVA: 0x002EE525 File Offset: 0x002EC725
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DC06 RID: 56326 RVA: 0x002EE538 File Offset: 0x002EC738
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DC07 RID: 56327 RVA: 0x002EE562 File Offset: 0x002EC762
		public bool Equals(Property other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005419 RID: 21529
		private ProgramNode _node;
	}
}
