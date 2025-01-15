using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C6F RID: 7279
	public struct lookupDictionary : IProgramNodeBuilder, IEquatable<lookupDictionary>
	{
		// Token: 0x17002916 RID: 10518
		// (get) Token: 0x0600F68E RID: 63118 RVA: 0x003496CE File Offset: 0x003478CE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F68F RID: 63119 RVA: 0x003496D6 File Offset: 0x003478D6
		private lookupDictionary(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F690 RID: 63120 RVA: 0x003496DF File Offset: 0x003478DF
		public static lookupDictionary CreateUnsafe(ProgramNode node)
		{
			return new lookupDictionary(node);
		}

		// Token: 0x0600F691 RID: 63121 RVA: 0x003496E8 File Offset: 0x003478E8
		public static lookupDictionary? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.lookupDictionary)
			{
				return null;
			}
			return new lookupDictionary?(lookupDictionary.CreateUnsafe(node));
		}

		// Token: 0x0600F692 RID: 63122 RVA: 0x00349722 File Offset: 0x00347922
		public static lookupDictionary CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new lookupDictionary(new Hole(g.Symbol.lookupDictionary, holeId));
		}

		// Token: 0x0600F693 RID: 63123 RVA: 0x0034973A File Offset: 0x0034793A
		public lookupDictionary(GrammarBuilders g, IReadOnlyDictionary<Optional<string>, string> value)
		{
			this = new lookupDictionary(new LiteralNode(g.Symbol.lookupDictionary, value));
		}

		// Token: 0x17002917 RID: 10519
		// (get) Token: 0x0600F694 RID: 63124 RVA: 0x00349753 File Offset: 0x00347953
		public IReadOnlyDictionary<Optional<string>, string> Value
		{
			get
			{
				return (IReadOnlyDictionary<Optional<string>, string>)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600F695 RID: 63125 RVA: 0x0034976A File Offset: 0x0034796A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F696 RID: 63126 RVA: 0x00349780 File Offset: 0x00347980
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F697 RID: 63127 RVA: 0x003497AA File Offset: 0x003479AA
		public bool Equals(lookupDictionary other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B5E RID: 23390
		private ProgramNode _node;
	}
}
