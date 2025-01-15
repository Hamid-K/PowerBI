using System;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x020000DF RID: 223
	public sealed class ODataBinaryStreamValue : ODataValue
	{
		// Token: 0x06000A74 RID: 2676 RVA: 0x0001C2DF File Offset: 0x0001A4DF
		public ODataBinaryStreamValue(Stream stream)
		{
			ExceptionUtils.CheckArgumentNotNull<Stream>(stream, "stream");
			this.Stream = stream;
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x0001C2FA File Offset: 0x0001A4FA
		// (set) Token: 0x06000A76 RID: 2678 RVA: 0x0001C302 File Offset: 0x0001A502
		public Stream Stream { get; private set; }
	}
}
