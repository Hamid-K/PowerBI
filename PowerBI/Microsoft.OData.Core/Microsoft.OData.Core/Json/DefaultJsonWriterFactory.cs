using System;
using System.IO;

namespace Microsoft.OData.Json
{
	// Token: 0x0200020C RID: 524
	public sealed class DefaultJsonWriterFactory : IJsonWriterFactory
	{
		// Token: 0x06001705 RID: 5893 RVA: 0x00040FFD File Offset: 0x0003F1FD
		public DefaultJsonWriterFactory()
			: this(ODataStringEscapeOption.EscapeNonAscii)
		{
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x00041006 File Offset: 0x0003F206
		public DefaultJsonWriterFactory(ODataStringEscapeOption stringEscapeOption)
		{
			this.stringEscapeOption = stringEscapeOption;
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x00041015 File Offset: 0x0003F215
		[CLSCompliant(false)]
		public IJsonWriter CreateJsonWriter(TextWriter textWriter, bool isIeee754Compatible)
		{
			return new JsonWriter(textWriter, isIeee754Compatible, this.stringEscapeOption);
		}

		// Token: 0x04000A5D RID: 2653
		private ODataStringEscapeOption stringEscapeOption;
	}
}
