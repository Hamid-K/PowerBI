using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C35 RID: 7221
	public struct LetPL1 : IProgramNodeBuilder, IEquatable<LetPL1>
	{
		// Token: 0x170028C7 RID: 10439
		// (get) Token: 0x0600F34E RID: 62286 RVA: 0x0034249E File Offset: 0x0034069E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F34F RID: 62287 RVA: 0x003424A6 File Offset: 0x003406A6
		private LetPL1(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F350 RID: 62288 RVA: 0x003424AF File Offset: 0x003406AF
		public static LetPL1 CreateUnsafe(ProgramNode node)
		{
			return new LetPL1(node);
		}

		// Token: 0x0600F351 RID: 62289 RVA: 0x003424B8 File Offset: 0x003406B8
		public static LetPL1? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetPL1)
			{
				return null;
			}
			return new LetPL1?(LetPL1.CreateUnsafe(node));
		}

		// Token: 0x0600F352 RID: 62290 RVA: 0x003424ED File Offset: 0x003406ED
		public LetPL1(GrammarBuilders g, pos value0, _LetB7 value1)
		{
			this._node = new LetNode(g.Rule.LetPL1, value0.Node, value1.Node);
		}

		// Token: 0x0600F353 RID: 62291 RVA: 0x00342513 File Offset: 0x00340713
		public static implicit operator PP(LetPL1 arg)
		{
			return PP.CreateUnsafe(arg.Node);
		}

		// Token: 0x170028C8 RID: 10440
		// (get) Token: 0x0600F354 RID: 62292 RVA: 0x00342521 File Offset: 0x00340721
		public pos pos
		{
			get
			{
				return pos.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170028C9 RID: 10441
		// (get) Token: 0x0600F355 RID: 62293 RVA: 0x00342535 File Offset: 0x00340735
		public _LetB7 _LetB7
		{
			get
			{
				return _LetB7.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F356 RID: 62294 RVA: 0x00342549 File Offset: 0x00340749
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F357 RID: 62295 RVA: 0x0034255C File Offset: 0x0034075C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F358 RID: 62296 RVA: 0x00342586 File Offset: 0x00340786
		public bool Equals(LetPL1 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B24 RID: 23332
		private ProgramNode _node;
	}
}
