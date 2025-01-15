using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x02000197 RID: 407
	[DomName("ProcessingInstruction")]
	public interface IProcessingInstruction : ICharacterData, INode, IEventTarget, IMarkupFormattable, IChildNode, INonDocumentTypeChildNode
	{
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000E8F RID: 3727
		[DomName("target")]
		string Target { get; }
	}
}
