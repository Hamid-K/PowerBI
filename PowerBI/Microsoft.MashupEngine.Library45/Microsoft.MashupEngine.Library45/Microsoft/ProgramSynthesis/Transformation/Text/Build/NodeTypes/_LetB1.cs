using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C57 RID: 7255
	public struct _LetB1 : IProgramNodeBuilder, IEquatable<_LetB1>
	{
		// Token: 0x170028ED RID: 10477
		// (get) Token: 0x0600F590 RID: 62864 RVA: 0x00348032 File Offset: 0x00346232
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F591 RID: 62865 RVA: 0x0034803A File Offset: 0x0034623A
		private _LetB1(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F592 RID: 62866 RVA: 0x00348043 File Offset: 0x00346243
		public static _LetB1 CreateUnsafe(ProgramNode node)
		{
			return new _LetB1(node);
		}

		// Token: 0x0600F593 RID: 62867 RVA: 0x0034804C File Offset: 0x0034624C
		public static _LetB1? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB1)
			{
				return null;
			}
			return new _LetB1?(_LetB1.CreateUnsafe(node));
		}

		// Token: 0x0600F594 RID: 62868 RVA: 0x00348086 File Offset: 0x00346286
		public static _LetB1 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB1(new Hole(g.Symbol._LetB1, holeId));
		}

		// Token: 0x0600F595 RID: 62869 RVA: 0x0034809E File Offset: 0x0034629E
		public LetSharedDateTimeFormat Cast_LetSharedDateTimeFormat()
		{
			return LetSharedDateTimeFormat.CreateUnsafe(this.Node);
		}

		// Token: 0x0600F596 RID: 62870 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LetSharedDateTimeFormat(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600F597 RID: 62871 RVA: 0x003480AB File Offset: 0x003462AB
		public bool Is_LetSharedDateTimeFormat(GrammarBuilders g, out LetSharedDateTimeFormat value)
		{
			value = LetSharedDateTimeFormat.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600F598 RID: 62872 RVA: 0x003480BF File Offset: 0x003462BF
		public LetSharedDateTimeFormat? As_LetSharedDateTimeFormat(GrammarBuilders g)
		{
			return new LetSharedDateTimeFormat?(LetSharedDateTimeFormat.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F599 RID: 62873 RVA: 0x003480D1 File Offset: 0x003462D1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F59A RID: 62874 RVA: 0x003480E4 File Offset: 0x003462E4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F59B RID: 62875 RVA: 0x0034810E File Offset: 0x0034630E
		public bool Equals(_LetB1 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B46 RID: 23366
		private ProgramNode _node;
	}
}
