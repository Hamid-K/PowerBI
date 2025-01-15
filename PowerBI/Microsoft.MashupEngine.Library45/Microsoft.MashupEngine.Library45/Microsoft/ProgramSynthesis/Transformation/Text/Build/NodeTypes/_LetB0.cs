using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C56 RID: 7254
	public struct _LetB0 : IProgramNodeBuilder, IEquatable<_LetB0>
	{
		// Token: 0x170028EC RID: 10476
		// (get) Token: 0x0600F584 RID: 62852 RVA: 0x00347F42 File Offset: 0x00346142
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F585 RID: 62853 RVA: 0x00347F4A File Offset: 0x0034614A
		private _LetB0(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F586 RID: 62854 RVA: 0x00347F53 File Offset: 0x00346153
		public static _LetB0 CreateUnsafe(ProgramNode node)
		{
			return new _LetB0(node);
		}

		// Token: 0x0600F587 RID: 62855 RVA: 0x00347F5C File Offset: 0x0034615C
		public static _LetB0? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB0)
			{
				return null;
			}
			return new _LetB0?(_LetB0.CreateUnsafe(node));
		}

		// Token: 0x0600F588 RID: 62856 RVA: 0x00347F96 File Offset: 0x00346196
		public static _LetB0 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB0(new Hole(g.Symbol._LetB0, holeId));
		}

		// Token: 0x0600F589 RID: 62857 RVA: 0x00347FAE File Offset: 0x003461AE
		public LetSharedNumberFormat Cast_LetSharedNumberFormat()
		{
			return LetSharedNumberFormat.CreateUnsafe(this.Node);
		}

		// Token: 0x0600F58A RID: 62858 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LetSharedNumberFormat(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600F58B RID: 62859 RVA: 0x00347FBB File Offset: 0x003461BB
		public bool Is_LetSharedNumberFormat(GrammarBuilders g, out LetSharedNumberFormat value)
		{
			value = LetSharedNumberFormat.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600F58C RID: 62860 RVA: 0x00347FCF File Offset: 0x003461CF
		public LetSharedNumberFormat? As_LetSharedNumberFormat(GrammarBuilders g)
		{
			return new LetSharedNumberFormat?(LetSharedNumberFormat.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F58D RID: 62861 RVA: 0x00347FE1 File Offset: 0x003461E1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F58E RID: 62862 RVA: 0x00347FF4 File Offset: 0x003461F4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F58F RID: 62863 RVA: 0x0034801E File Offset: 0x0034621E
		public bool Equals(_LetB0 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B45 RID: 23365
		private ProgramNode _node;
	}
}
