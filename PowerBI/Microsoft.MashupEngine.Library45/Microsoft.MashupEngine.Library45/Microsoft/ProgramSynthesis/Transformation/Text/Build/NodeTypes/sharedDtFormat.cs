using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C78 RID: 7288
	public struct sharedDtFormat : IProgramNodeBuilder, IEquatable<sharedDtFormat>
	{
		// Token: 0x17002928 RID: 10536
		// (get) Token: 0x0600F6E8 RID: 63208 RVA: 0x00349EFA File Offset: 0x003480FA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F6E9 RID: 63209 RVA: 0x00349F02 File Offset: 0x00348102
		private sharedDtFormat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F6EA RID: 63210 RVA: 0x00349F0B File Offset: 0x0034810B
		public static sharedDtFormat CreateUnsafe(ProgramNode node)
		{
			return new sharedDtFormat(node);
		}

		// Token: 0x0600F6EB RID: 63211 RVA: 0x00349F14 File Offset: 0x00348114
		public static sharedDtFormat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.sharedDtFormat)
			{
				return null;
			}
			return new sharedDtFormat?(sharedDtFormat.CreateUnsafe(node));
		}

		// Token: 0x0600F6EC RID: 63212 RVA: 0x00349F4E File Offset: 0x0034814E
		public static sharedDtFormat CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new sharedDtFormat(new Hole(g.Symbol.sharedDtFormat, holeId));
		}

		// Token: 0x0600F6ED RID: 63213 RVA: 0x00349F66 File Offset: 0x00348166
		public sharedDtFormat(GrammarBuilders g)
		{
			this = new sharedDtFormat(new VariableNode(g.Symbol.sharedDtFormat));
		}

		// Token: 0x17002929 RID: 10537
		// (get) Token: 0x0600F6EE RID: 63214 RVA: 0x00349F7E File Offset: 0x0034817E
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600F6EF RID: 63215 RVA: 0x00349F8B File Offset: 0x0034818B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F6F0 RID: 63216 RVA: 0x00349FA0 File Offset: 0x003481A0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F6F1 RID: 63217 RVA: 0x00349FCA File Offset: 0x003481CA
		public bool Equals(sharedDtFormat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B67 RID: 23399
		private ProgramNode _node;
	}
}
