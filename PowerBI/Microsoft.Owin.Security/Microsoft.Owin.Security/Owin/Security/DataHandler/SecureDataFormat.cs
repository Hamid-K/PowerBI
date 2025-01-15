using System;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;

namespace Microsoft.Owin.Security.DataHandler
{
	// Token: 0x0200002B RID: 43
	public class SecureDataFormat<TData> : ISecureDataFormat<TData>
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00003513 File Offset: 0x00001713
		public SecureDataFormat(IDataSerializer<TData> serializer, IDataProtector protector, ITextEncoder encoder)
		{
			this._serializer = serializer;
			this._protector = protector;
			this._encoder = encoder;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003530 File Offset: 0x00001730
		public string Protect(TData data)
		{
			byte[] userData = this._serializer.Serialize(data);
			byte[] protectedData = this._protector.Protect(userData);
			return this._encoder.Encode(protectedData);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003568 File Offset: 0x00001768
		public TData Unprotect(string protectedText)
		{
			TData tdata;
			try
			{
				if (protectedText == null)
				{
					tdata = default(TData);
					tdata = tdata;
				}
				else
				{
					byte[] protectedData = this._encoder.Decode(protectedText);
					if (protectedData == null)
					{
						tdata = default(TData);
					}
					else
					{
						byte[] userData = this._protector.Unprotect(protectedData);
						if (userData == null)
						{
							tdata = default(TData);
						}
						else
						{
							TData model = this._serializer.Deserialize(userData);
							tdata = model;
						}
					}
				}
			}
			catch
			{
				tdata = default(TData);
			}
			return tdata;
		}

		// Token: 0x04000049 RID: 73
		private readonly IDataSerializer<TData> _serializer;

		// Token: 0x0400004A RID: 74
		private readonly IDataProtector _protector;

		// Token: 0x0400004B RID: 75
		private readonly ITextEncoder _encoder;
	}
}
