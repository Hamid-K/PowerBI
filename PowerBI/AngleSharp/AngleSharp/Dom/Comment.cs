using System;
using System.IO;

namespace AngleSharp.Dom
{
	// Token: 0x0200014B RID: 331
	internal sealed class Comment : CharacterData, IComment, ICharacterData, INode, IEventTarget, IMarkupFormattable, IChildNode, INonDocumentTypeChildNode
	{
		// Token: 0x06000A22 RID: 2594 RVA: 0x00040CA3 File Offset: 0x0003EEA3
		internal Comment(Document owner)
			: this(owner, string.Empty)
		{
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00040CB1 File Offset: 0x0003EEB1
		internal Comment(Document owner, string data)
			: base(owner, "#comment", NodeType.Comment, data)
		{
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00040CC4 File Offset: 0x0003EEC4
		public override INode Clone(bool deep = true)
		{
			Comment comment = new Comment(base.Owner, base.Data);
			base.CloneNode(comment, deep);
			return comment;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00040CEC File Offset: 0x0003EEEC
		public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
		{
			writer.Write(formatter.Comment(this));
		}
	}
}
