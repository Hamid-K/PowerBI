using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200105D RID: 4189
	public struct subNodeSequence : IProgramNodeBuilder, IEquatable<subNodeSequence>
	{
		// Token: 0x17001646 RID: 5702
		// (get) Token: 0x06007C85 RID: 31877 RVA: 0x001A534E File Offset: 0x001A354E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007C86 RID: 31878 RVA: 0x001A5356 File Offset: 0x001A3556
		private subNodeSequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007C87 RID: 31879 RVA: 0x001A535F File Offset: 0x001A355F
		public static subNodeSequence CreateUnsafe(ProgramNode node)
		{
			return new subNodeSequence(node);
		}

		// Token: 0x06007C88 RID: 31880 RVA: 0x001A5368 File Offset: 0x001A3568
		public static subNodeSequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.subNodeSequence)
			{
				return null;
			}
			return new subNodeSequence?(subNodeSequence.CreateUnsafe(node));
		}

		// Token: 0x06007C89 RID: 31881 RVA: 0x001A53A2 File Offset: 0x001A35A2
		public static subNodeSequence CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new subNodeSequence(new Hole(g.Symbol.subNodeSequence, holeId));
		}

		// Token: 0x06007C8A RID: 31882 RVA: 0x001A53BA File Offset: 0x001A35BA
		public MapToWebRegion Cast_MapToWebRegion()
		{
			return MapToWebRegion.CreateUnsafe(this.Node);
		}

		// Token: 0x06007C8B RID: 31883 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_MapToWebRegion(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007C8C RID: 31884 RVA: 0x001A53C7 File Offset: 0x001A35C7
		public bool Is_MapToWebRegion(GrammarBuilders g, out MapToWebRegion value)
		{
			value = MapToWebRegion.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007C8D RID: 31885 RVA: 0x001A53DB File Offset: 0x001A35DB
		public MapToWebRegion? As_MapToWebRegion(GrammarBuilders g)
		{
			return new MapToWebRegion?(MapToWebRegion.CreateUnsafe(this.Node));
		}

		// Token: 0x06007C8E RID: 31886 RVA: 0x001A53ED File Offset: 0x001A35ED
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007C8F RID: 31887 RVA: 0x001A5400 File Offset: 0x001A3600
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007C90 RID: 31888 RVA: 0x001A542A File Offset: 0x001A362A
		public bool Equals(subNodeSequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003376 RID: 13174
		private ProgramNode _node;
	}
}
