using System;
using System.IO;

namespace AngleSharp.Dom
{
	// Token: 0x0200015F RID: 351
	internal sealed class ProcessingInstruction : CharacterData, IProcessingInstruction, ICharacterData, INode, IEventTarget, IMarkupFormattable, IChildNode, INonDocumentTypeChildNode
	{
		// Token: 0x06000C5C RID: 3164 RVA: 0x0004573E File Offset: 0x0004393E
		internal ProcessingInstruction(Document owner, string name)
			: base(owner, name, NodeType.ProcessingInstruction)
		{
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x00042B73 File Offset: 0x00040D73
		public string Target
		{
			get
			{
				return base.NodeName;
			}
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0004574C File Offset: 0x0004394C
		public override INode Clone(bool deep = true)
		{
			ProcessingInstruction processingInstruction = new ProcessingInstruction(base.Owner, this.Target);
			base.CloneNode(processingInstruction, deep);
			return processingInstruction;
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00045774 File Offset: 0x00043974
		public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
		{
			writer.Write(formatter.Processing(this));
		}
	}
}
