using System;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;

namespace Microsoft.Owin.Security.DataHandler
{
	// Token: 0x0200002A RID: 42
	public class PropertiesDataFormat : SecureDataFormat<AuthenticationProperties>
	{
		// Token: 0x060000BF RID: 191 RVA: 0x00003500 File Offset: 0x00001700
		public PropertiesDataFormat(IDataProtector protector)
			: base(DataSerializers.Properties, protector, TextEncodings.Base64Url)
		{
		}
	}
}
