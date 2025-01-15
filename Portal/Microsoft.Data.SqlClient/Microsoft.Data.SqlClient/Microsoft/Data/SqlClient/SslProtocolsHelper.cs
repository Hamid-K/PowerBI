using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000115 RID: 277
	internal static class SslProtocolsHelper
	{
		// Token: 0x060015CB RID: 5579 RVA: 0x0005EF48 File Offset: 0x0005D148
		private static string ToFriendlyName(this SslProtocolsHelper.NativeProtocols protocol)
		{
			string text;
			if (protocol.HasFlag(SslProtocolsHelper.NativeProtocols.SP_PROT_TLS1_3_CLIENT) || protocol.HasFlag(SslProtocolsHelper.NativeProtocols.SP_PROT_TLS1_3_SERVER))
			{
				text = "TLS 1.3";
			}
			else if (protocol.HasFlag(SslProtocolsHelper.NativeProtocols.SP_PROT_TLS1_2_CLIENT) || protocol.HasFlag(SslProtocolsHelper.NativeProtocols.SP_PROT_TLS1_2_SERVER))
			{
				text = "TLS 1.2";
			}
			else if (protocol.HasFlag(SslProtocolsHelper.NativeProtocols.SP_PROT_TLS1_1_CLIENT) || protocol.HasFlag(SslProtocolsHelper.NativeProtocols.SP_PROT_TLS1_1_SERVER))
			{
				text = "TLS 1.1";
			}
			else if (protocol.HasFlag(SslProtocolsHelper.NativeProtocols.SP_PROT_TLS1_0_CLIENT) || protocol.HasFlag(SslProtocolsHelper.NativeProtocols.SP_PROT_TLS1_0_SERVER))
			{
				text = "TLS 1.0";
			}
			else if (protocol.HasFlag(SslProtocolsHelper.NativeProtocols.SP_PROT_SSL3_CLIENT) || protocol.HasFlag(SslProtocolsHelper.NativeProtocols.SP_PROT_SSL3_SERVER))
			{
				text = "SSL 3.0";
			}
			else if (protocol.HasFlag(SslProtocolsHelper.NativeProtocols.SP_PROT_SSL2_CLIENT) || protocol.HasFlag(SslProtocolsHelper.NativeProtocols.SP_PROT_SSL2_SERVER))
			{
				text = "SSL 2.0";
			}
			else
			{
				if (!protocol.HasFlag(SslProtocolsHelper.NativeProtocols.SP_PROT_NONE))
				{
					throw new ArgumentException(StringsHelper.GetString(Strings.net_invalid_enum, new object[] { "NativeProtocols" }), "NativeProtocols");
				}
				text = "None";
			}
			return text;
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x0005F0D4 File Offset: 0x0005D2D4
		public static string GetProtocolWarning(uint protocol)
		{
			string text = string.Empty;
			if ((protocol & 828U) != 0U)
			{
				text = StringsHelper.GetString(Strings.SEC_ProtocolWarning, new object[] { ((SslProtocolsHelper.NativeProtocols)protocol).ToFriendlyName() });
			}
			return text;
		}

		// Token: 0x02000266 RID: 614
		[Flags]
		private enum NativeProtocols
		{
			// Token: 0x040016F8 RID: 5880
			SP_PROT_SSL2_SERVER = 4,
			// Token: 0x040016F9 RID: 5881
			SP_PROT_SSL2_CLIENT = 8,
			// Token: 0x040016FA RID: 5882
			SP_PROT_SSL3_SERVER = 16,
			// Token: 0x040016FB RID: 5883
			SP_PROT_SSL3_CLIENT = 32,
			// Token: 0x040016FC RID: 5884
			SP_PROT_TLS1_0_SERVER = 64,
			// Token: 0x040016FD RID: 5885
			SP_PROT_TLS1_0_CLIENT = 128,
			// Token: 0x040016FE RID: 5886
			SP_PROT_TLS1_1_SERVER = 256,
			// Token: 0x040016FF RID: 5887
			SP_PROT_TLS1_1_CLIENT = 512,
			// Token: 0x04001700 RID: 5888
			SP_PROT_TLS1_2_SERVER = 1024,
			// Token: 0x04001701 RID: 5889
			SP_PROT_TLS1_2_CLIENT = 2048,
			// Token: 0x04001702 RID: 5890
			SP_PROT_TLS1_3_SERVER = 4096,
			// Token: 0x04001703 RID: 5891
			SP_PROT_TLS1_3_CLIENT = 8192,
			// Token: 0x04001704 RID: 5892
			SP_PROT_SSL2 = 12,
			// Token: 0x04001705 RID: 5893
			SP_PROT_SSL3 = 48,
			// Token: 0x04001706 RID: 5894
			SP_PROT_TLS1_0 = 192,
			// Token: 0x04001707 RID: 5895
			SP_PROT_TLS1_1 = 768,
			// Token: 0x04001708 RID: 5896
			SP_PROT_TLS1_2 = 3072,
			// Token: 0x04001709 RID: 5897
			SP_PROT_TLS1_3 = 12288,
			// Token: 0x0400170A RID: 5898
			SP_PROT_NONE = 0
		}
	}
}
