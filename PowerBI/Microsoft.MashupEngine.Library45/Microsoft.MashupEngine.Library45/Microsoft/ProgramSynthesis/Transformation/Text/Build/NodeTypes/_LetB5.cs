using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C5B RID: 7259
	public struct _LetB5 : IProgramNodeBuilder, IEquatable<_LetB5>
	{
		// Token: 0x170028F1 RID: 10481
		// (get) Token: 0x0600F5C0 RID: 62912 RVA: 0x003483F2 File Offset: 0x003465F2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F5C1 RID: 62913 RVA: 0x003483FA File Offset: 0x003465FA
		private _LetB5(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F5C2 RID: 62914 RVA: 0x00348403 File Offset: 0x00346603
		public static _LetB5 CreateUnsafe(ProgramNode node)
		{
			return new _LetB5(node);
		}

		// Token: 0x0600F5C3 RID: 62915 RVA: 0x0034840C File Offset: 0x0034660C
		public static _LetB5? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB5)
			{
				return null;
			}
			return new _LetB5?(_LetB5.CreateUnsafe(node));
		}

		// Token: 0x0600F5C4 RID: 62916 RVA: 0x00348446 File Offset: 0x00346646
		public static _LetB5 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB5(new Hole(g.Symbol._LetB5, holeId));
		}

		// Token: 0x0600F5C5 RID: 62917 RVA: 0x0034845E File Offset: 0x0034665E
		public RSubStr Cast_RSubStr()
		{
			return RSubStr.CreateUnsafe(this.Node);
		}

		// Token: 0x0600F5C6 RID: 62918 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_RSubStr(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600F5C7 RID: 62919 RVA: 0x0034846B File Offset: 0x0034666B
		public bool Is_RSubStr(GrammarBuilders g, out RSubStr value)
		{
			value = RSubStr.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600F5C8 RID: 62920 RVA: 0x0034847F File Offset: 0x0034667F
		public RSubStr? As_RSubStr(GrammarBuilders g)
		{
			return new RSubStr?(RSubStr.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F5C9 RID: 62921 RVA: 0x00348491 File Offset: 0x00346691
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F5CA RID: 62922 RVA: 0x003484A4 File Offset: 0x003466A4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F5CB RID: 62923 RVA: 0x003484CE File Offset: 0x003466CE
		public bool Equals(_LetB5 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B4A RID: 23370
		private ProgramNode _node;
	}
}
