using System;
using System.IO;

namespace Microsoft.OData.Json
{
	// Token: 0x0200020A RID: 522
	[CLSCompliant(false)]
	public interface IJsonStreamWriter : IJsonWriter
	{
		// Token: 0x060016FF RID: 5887
		Stream StartStreamValueScope();

		// Token: 0x06001700 RID: 5888
		TextWriter StartTextWriterValueScope(string contentType);

		// Token: 0x06001701 RID: 5889
		void EndStreamValueScope();

		// Token: 0x06001702 RID: 5890
		void EndTextWriterValueScope();
	}
}
