using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x020001A0 RID: 416
	[DomName("Text")]
	public interface IText : ICharacterData, INode, IEventTarget, IMarkupFormattable, IChildNode, INonDocumentTypeChildNode
	{
		// Token: 0x06000EC0 RID: 3776
		[DomName("splitText")]
		IText Split(int offset);

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000EC1 RID: 3777
		[DomName("wholeText")]
		string Text { get; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000EC2 RID: 3778
		[DomName("assignedSlot")]
		IElement AssignedSlot { get; }
	}
}
