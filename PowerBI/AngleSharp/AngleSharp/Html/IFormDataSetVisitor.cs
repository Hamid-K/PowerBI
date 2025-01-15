using System;
using AngleSharp.Dom.Io;

namespace AngleSharp.Html
{
	// Token: 0x020000B9 RID: 185
	public interface IFormDataSetVisitor
	{
		// Token: 0x0600059B RID: 1435
		void Text(FormDataSetEntry entry, string value);

		// Token: 0x0600059C RID: 1436
		void File(FormDataSetEntry entry, string fileName, string contentType, IFile content);
	}
}
