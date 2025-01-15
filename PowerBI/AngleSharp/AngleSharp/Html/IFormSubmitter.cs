using System;
using System.IO;

namespace AngleSharp.Html
{
	// Token: 0x020000BA RID: 186
	public interface IFormSubmitter : IFormDataSetVisitor
	{
		// Token: 0x0600059D RID: 1437
		void Serialize(StreamWriter stream);
	}
}
