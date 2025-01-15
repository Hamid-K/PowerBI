using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes
{
	// Token: 0x02001AC2 RID: 6850
	public struct ejsonProgram : IProgramNodeBuilder, IEquatable<ejsonProgram>
	{
		// Token: 0x170025E9 RID: 9705
		// (get) Token: 0x0600E29B RID: 58011 RVA: 0x00301ECA File Offset: 0x003000CA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E29C RID: 58012 RVA: 0x00301ED2 File Offset: 0x003000D2
		private ejsonProgram(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E29D RID: 58013 RVA: 0x00301EDB File Offset: 0x003000DB
		public static ejsonProgram CreateUnsafe(ProgramNode node)
		{
			return new ejsonProgram(node);
		}

		// Token: 0x0600E29E RID: 58014 RVA: 0x00301EE4 File Offset: 0x003000E4
		public static ejsonProgram? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.ejsonProgram)
			{
				return null;
			}
			return new ejsonProgram?(ejsonProgram.CreateUnsafe(node));
		}

		// Token: 0x0600E29F RID: 58015 RVA: 0x00301F1E File Offset: 0x0030011E
		public static ejsonProgram CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new ejsonProgram(new Hole(g.Symbol.ejsonProgram, holeId));
		}

		// Token: 0x0600E2A0 RID: 58016 RVA: 0x00301F36 File Offset: 0x00300136
		public ejsonProgram(GrammarBuilders g, Program value)
		{
			this = new ejsonProgram(new LiteralNode(g.Symbol.ejsonProgram, value));
		}

		// Token: 0x170025EA RID: 9706
		// (get) Token: 0x0600E2A1 RID: 58017 RVA: 0x00301F4F File Offset: 0x0030014F
		public Program Value
		{
			get
			{
				return (Program)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600E2A2 RID: 58018 RVA: 0x00301F66 File Offset: 0x00300166
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E2A3 RID: 58019 RVA: 0x00301F7C File Offset: 0x0030017C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E2A4 RID: 58020 RVA: 0x00301FA6 File Offset: 0x003001A6
		public bool Equals(ejsonProgram other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005581 RID: 21889
		private ProgramNode _node;
	}
}
