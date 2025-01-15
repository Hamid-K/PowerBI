using System;
using AngleSharp.Html;

namespace AngleSharp.Dom
{
	// Token: 0x02000154 RID: 340
	internal sealed class EntityReference : Node
	{
		// Token: 0x06000BB2 RID: 2994 RVA: 0x00043A74 File Offset: 0x00041C74
		internal EntityReference(Document owner)
			: this(owner, string.Empty)
		{
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00043A82 File Offset: 0x00041C82
		internal EntityReference(Document owner, string name)
			: base(owner, name, NodeType.EntityReference, NodeFlags.None)
		{
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00043A90 File Offset: 0x00041C90
		public override INode Clone(bool deep = true)
		{
			EntityReference entityReference = new EntityReference(base.Owner, base.NodeName);
			base.CloneNode(entityReference, deep);
			return entityReference;
		}
	}
}
