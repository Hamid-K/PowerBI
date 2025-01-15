using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002E1 RID: 737
	internal interface IStreamResponse
	{
		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001A3D RID: 6717
		Stream OutputStream { get; }

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001A3E RID: 6718
		// (set) Token: 0x06001A3F RID: 6719
		string MimeType { get; set; }

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001A40 RID: 6720
		IList<string> ResponseFlags { get; }

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001A41 RID: 6721
		IMetadataStore ResponseMetadata { get; }

		// Token: 0x17000789 RID: 1929
		// (set) Token: 0x06001A42 RID: 6722
		Encoding ContentEncoding { set; }

		// Token: 0x06001A43 RID: 6723
		void Write(string s);

		// Token: 0x06001A44 RID: 6724
		void BinaryWrite(byte[] buffer);

		// Token: 0x06001A45 RID: 6725
		void WriteRSStream(RSStream stream);
	}
}
