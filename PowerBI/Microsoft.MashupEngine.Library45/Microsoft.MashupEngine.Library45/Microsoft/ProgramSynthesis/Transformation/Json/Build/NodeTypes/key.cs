using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A40 RID: 6720
	public struct key : IProgramNodeBuilder, IEquatable<key>
	{
		// Token: 0x1700250F RID: 9487
		// (get) Token: 0x0600DD3E RID: 56638 RVA: 0x002F0E8A File Offset: 0x002EF08A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DD3F RID: 56639 RVA: 0x002F0E92 File Offset: 0x002EF092
		private key(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DD40 RID: 56640 RVA: 0x002F0E9B File Offset: 0x002EF09B
		public static key CreateUnsafe(ProgramNode node)
		{
			return new key(node);
		}

		// Token: 0x0600DD41 RID: 56641 RVA: 0x002F0EA4 File Offset: 0x002EF0A4
		public static key? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.key)
			{
				return null;
			}
			return new key?(key.CreateUnsafe(node));
		}

		// Token: 0x0600DD42 RID: 56642 RVA: 0x002F0EDE File Offset: 0x002EF0DE
		public static key CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new key(new Hole(g.Symbol.key, holeId));
		}

		// Token: 0x0600DD43 RID: 56643 RVA: 0x002F0EF6 File Offset: 0x002EF0F6
		public bool Is_Key(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Key;
		}

		// Token: 0x0600DD44 RID: 56644 RVA: 0x002F0F10 File Offset: 0x002EF110
		public bool Is_Key(GrammarBuilders g, out Key value)
		{
			if (this.Node.GrammarRule == g.Rule.Key)
			{
				value = Key.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Key);
			return false;
		}

		// Token: 0x0600DD45 RID: 56645 RVA: 0x002F0F48 File Offset: 0x002EF148
		public Key? As_Key(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Key)
			{
				return null;
			}
			return new Key?(Key.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DD46 RID: 56646 RVA: 0x002F0F88 File Offset: 0x002EF188
		public Key Cast_Key(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Key)
			{
				return Key.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Key is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DD47 RID: 56647 RVA: 0x002F0FDD File Offset: 0x002EF1DD
		public bool Is_ConstKey(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ConstKey;
		}

		// Token: 0x0600DD48 RID: 56648 RVA: 0x002F0FF7 File Offset: 0x002EF1F7
		public bool Is_ConstKey(GrammarBuilders g, out ConstKey value)
		{
			if (this.Node.GrammarRule == g.Rule.ConstKey)
			{
				value = ConstKey.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ConstKey);
			return false;
		}

		// Token: 0x0600DD49 RID: 56649 RVA: 0x002F102C File Offset: 0x002EF22C
		public ConstKey? As_ConstKey(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ConstKey)
			{
				return null;
			}
			return new ConstKey?(ConstKey.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DD4A RID: 56650 RVA: 0x002F106C File Offset: 0x002EF26C
		public ConstKey Cast_ConstKey(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ConstKey)
			{
				return ConstKey.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ConstKey is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DD4B RID: 56651 RVA: 0x002F10C4 File Offset: 0x002EF2C4
		public T Switch<T>(GrammarBuilders g, Func<Key, T> func0, Func<ConstKey, T> func1)
		{
			Key key;
			if (this.Is_Key(g, out key))
			{
				return func0(key);
			}
			ConstKey constKey;
			if (this.Is_ConstKey(g, out constKey))
			{
				return func1(constKey);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol key");
		}

		// Token: 0x0600DD4C RID: 56652 RVA: 0x002F111C File Offset: 0x002EF31C
		public void Switch(GrammarBuilders g, Action<Key> func0, Action<ConstKey> func1)
		{
			Key key;
			if (this.Is_Key(g, out key))
			{
				func0(key);
				return;
			}
			ConstKey constKey;
			if (this.Is_ConstKey(g, out constKey))
			{
				func1(constKey);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol key");
		}

		// Token: 0x0600DD4D RID: 56653 RVA: 0x002F1173 File Offset: 0x002EF373
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DD4E RID: 56654 RVA: 0x002F1188 File Offset: 0x002EF388
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DD4F RID: 56655 RVA: 0x002F11B2 File Offset: 0x002EF3B2
		public bool Equals(key other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005431 RID: 21553
		private ProgramNode _node;
	}
}
