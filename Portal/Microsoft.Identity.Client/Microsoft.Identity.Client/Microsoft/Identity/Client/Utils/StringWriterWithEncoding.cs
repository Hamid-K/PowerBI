using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.Identity.Client.Utils
{
	// Token: 0x020001D2 RID: 466
	internal class StringWriterWithEncoding : StringWriter
	{
		// Token: 0x06001469 RID: 5225 RVA: 0x0004563E File Offset: 0x0004383E
		public StringWriterWithEncoding(Encoding encoding)
			: base(CultureInfo.InvariantCulture)
		{
			this.Encoding = encoding;
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x00045652 File Offset: 0x00043852
		public override Encoding Encoding { get; }
	}
}
