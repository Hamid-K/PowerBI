using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000DE RID: 222
	public interface ISectionDocument : IDocument, ISyntaxNode
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600034C RID: 844
		ISection Section { get; }
	}
}
