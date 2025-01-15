using System;
using AngleSharp.Html;

namespace AngleSharp.Dom
{
	// Token: 0x0200015E RID: 350
	internal sealed class Notation : Node
	{
		// Token: 0x06000C56 RID: 3158 RVA: 0x000456CE File Offset: 0x000438CE
		internal Notation(Document owner)
			: base(owner, "#notation", NodeType.Notation, NodeFlags.None)
		{
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x000456DF File Offset: 0x000438DF
		// (set) Token: 0x06000C58 RID: 3160 RVA: 0x000456E7 File Offset: 0x000438E7
		public string PublicId { get; set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x000456F0 File Offset: 0x000438F0
		// (set) Token: 0x06000C5A RID: 3162 RVA: 0x000456F8 File Offset: 0x000438F8
		public string SystemId { get; set; }

		// Token: 0x06000C5B RID: 3163 RVA: 0x00045704 File Offset: 0x00043904
		public override INode Clone(bool deep = true)
		{
			Notation notation = new Notation(base.Owner)
			{
				PublicId = this.PublicId,
				SystemId = this.SystemId
			};
			base.CloneNode(notation, deep);
			return notation;
		}
	}
}
