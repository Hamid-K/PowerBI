using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Visitors
{
	// Token: 0x02001605 RID: 5637
	public class ProgramNodeDetail : IEquatable<ProgramNodeDetail>
	{
		// Token: 0x0600BB6F RID: 47983 RVA: 0x00285231 File Offset: 0x00283431
		public ProgramNodeDetail(IReadOnlyList<ProgramNodeDetail> nodes, IReadOnlyList<ProgramNodeDetail> ancestors)
		{
			this._nodes = nodes;
			this.Ancestors = ancestors;
		}

		// Token: 0x17002050 RID: 8272
		// (get) Token: 0x0600BB70 RID: 47984 RVA: 0x00285247 File Offset: 0x00283447
		public IReadOnlyList<ProgramNodeDetail> Ancestors { get; }

		// Token: 0x17002051 RID: 8273
		// (get) Token: 0x0600BB71 RID: 47985 RVA: 0x00285250 File Offset: 0x00283450
		public IReadOnlyList<ProgramNodeDetail> Children
		{
			get
			{
				IReadOnlyList<ProgramNodeDetail> readOnlyList;
				if ((readOnlyList = this._children) == null)
				{
					readOnlyList = (this._children = this.LoadChildren());
				}
				return readOnlyList;
			}
		}

		// Token: 0x17002052 RID: 8274
		// (get) Token: 0x0600BB72 RID: 47986 RVA: 0x00285276 File Offset: 0x00283476
		// (set) Token: 0x0600BB73 RID: 47987 RVA: 0x0028527E File Offset: 0x0028347E
		public int Depth { get; set; }

		// Token: 0x17002053 RID: 8275
		// (get) Token: 0x0600BB74 RID: 47988 RVA: 0x00285288 File Offset: 0x00283488
		public IReadOnlyList<ProgramNodeDetail> Descendents
		{
			get
			{
				IReadOnlyList<ProgramNodeDetail> readOnlyList;
				if ((readOnlyList = this._descendents) == null)
				{
					readOnlyList = (this._descendents = this.LoadDescendents());
				}
				return readOnlyList;
			}
		}

		// Token: 0x17002054 RID: 8276
		// (get) Token: 0x0600BB75 RID: 47989 RVA: 0x002852B0 File Offset: 0x002834B0
		public IReadOnlyList<ProgramNodeDetail> SelfAndDescendents
		{
			get
			{
				IReadOnlyList<ProgramNodeDetail> readOnlyList;
				if ((readOnlyList = this._selfAndDescendents) == null)
				{
					readOnlyList = (this._selfAndDescendents = this.Yield<ProgramNodeDetail>().Concat(this.Descendents).ToList<ProgramNodeDetail>());
				}
				return readOnlyList;
			}
		}

		// Token: 0x17002055 RID: 8277
		// (get) Token: 0x0600BB76 RID: 47990 RVA: 0x002852E6 File Offset: 0x002834E6
		// (set) Token: 0x0600BB77 RID: 47991 RVA: 0x002852EE File Offset: 0x002834EE
		public ProgramNode Node { get; set; }

		// Token: 0x17002056 RID: 8278
		// (get) Token: 0x0600BB78 RID: 47992 RVA: 0x002852F7 File Offset: 0x002834F7
		// (set) Token: 0x0600BB79 RID: 47993 RVA: 0x002852FF File Offset: 0x002834FF
		public int Order { get; set; }

		// Token: 0x17002057 RID: 8279
		// (get) Token: 0x0600BB7A RID: 47994 RVA: 0x00285308 File Offset: 0x00283508
		// (set) Token: 0x0600BB7B RID: 47995 RVA: 0x00285310 File Offset: 0x00283510
		public ProgramNodeDetail Parent { get; set; }

		// Token: 0x0600BB7C RID: 47996 RVA: 0x00285319 File Offset: 0x00283519
		public bool Equals(ProgramNodeDetail other)
		{
			return other != null && this.Node == other.Node;
		}

		// Token: 0x0600BB7D RID: 47997 RVA: 0x00285337 File Offset: 0x00283537
		public override bool Equals(object other)
		{
			return this.Equals(other as ProgramNodeDetail);
		}

		// Token: 0x0600BB7E RID: 47998 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600BB7F RID: 47999 RVA: 0x00285345 File Offset: 0x00283545
		public static bool operator ==(ProgramNodeDetail left, ProgramNodeDetail right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600BB80 RID: 48000 RVA: 0x0028535B File Offset: 0x0028355B
		public static bool operator !=(ProgramNodeDetail left, ProgramNodeDetail right)
		{
			return !(left == right);
		}

		// Token: 0x0600BB81 RID: 48001 RVA: 0x00285367 File Offset: 0x00283567
		public override string ToString()
		{
			ProgramNode node = this.Node;
			return ((node != null) ? node.ToString() : null) ?? string.Empty;
		}

		// Token: 0x0600BB82 RID: 48002 RVA: 0x00285384 File Offset: 0x00283584
		private IReadOnlyList<ProgramNodeDetail> LoadChildren()
		{
			return this._nodes.Where((ProgramNodeDetail nodeDetail) => nodeDetail.Parent == this).ToReadOnlyList<ProgramNodeDetail>();
		}

		// Token: 0x0600BB83 RID: 48003 RVA: 0x002853A2 File Offset: 0x002835A2
		private IReadOnlyList<ProgramNodeDetail> LoadDescendents()
		{
			return this._nodes.Where((ProgramNodeDetail nodeDetail) => nodeDetail.Ancestors.Any((ProgramNodeDetail ancestor) => ancestor.Equals(this))).ToReadOnlyList<ProgramNodeDetail>();
		}

		// Token: 0x040046E9 RID: 18153
		private IReadOnlyList<ProgramNodeDetail> _children;

		// Token: 0x040046EA RID: 18154
		private IReadOnlyList<ProgramNodeDetail> _descendents;

		// Token: 0x040046EB RID: 18155
		private IReadOnlyList<ProgramNodeDetail> _selfAndDescendents;

		// Token: 0x040046EC RID: 18156
		private readonly IReadOnlyList<ProgramNodeDetail> _nodes;
	}
}
