using System;
using Microsoft.HostIntegration.ConfigurationSectionHandlers.Encoding;
using Microsoft.HostIntegration.StrictResources.Nls;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x02000624 RID: 1572
	internal class HisUnicodeToEbcdicMapping
	{
		// Token: 0x060034F4 RID: 13556 RVA: 0x000B1198 File Offset: 0x000AF398
		internal HisUnicodeToEbcdicMapping(UnicodeToEbcdicConversion utec)
		{
			if (!string.IsNullOrWhiteSpace(utec.From))
			{
				this.UnicodeChar = (char)Convert.ToUInt16(utec.From, 16);
			}
			if (!string.IsNullOrWhiteSpace(utec.To))
			{
				ushort num = Convert.ToUInt16(utec.To, 16);
				if (num > 255)
				{
					string text = SR.InvalidEBCDICMappings("to", utec.To);
					throw new NotSupportedException(SR.InvalidConfigurationFile, new CustomEncodingsMappingDoubleEbcdicBytesNotAllowedException(text));
				}
				this.EbcdicByte = (byte)num;
			}
			this.Reversible = utec.Reversible;
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x060034F5 RID: 13557 RVA: 0x000B1224 File Offset: 0x000AF424
		// (set) Token: 0x060034F6 RID: 13558 RVA: 0x000B122C File Offset: 0x000AF42C
		internal char UnicodeChar { get; private set; }

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x060034F7 RID: 13559 RVA: 0x000B1235 File Offset: 0x000AF435
		// (set) Token: 0x060034F8 RID: 13560 RVA: 0x000B123D File Offset: 0x000AF43D
		internal byte EbcdicByte { get; private set; }

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x060034F9 RID: 13561 RVA: 0x000B1246 File Offset: 0x000AF446
		// (set) Token: 0x060034FA RID: 13562 RVA: 0x000B124E File Offset: 0x000AF44E
		internal bool Reversible { get; private set; }
	}
}
