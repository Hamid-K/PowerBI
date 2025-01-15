using System;
using System.IO;
using System.Text;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000029 RID: 41
	// (Invoke) Token: 0x06000055 RID: 85
	public delegate Stream CreateAndRegisterStream(string name, string extension, Encoding encoding, string mimeType, bool willSeek, StreamOper operation);
}
