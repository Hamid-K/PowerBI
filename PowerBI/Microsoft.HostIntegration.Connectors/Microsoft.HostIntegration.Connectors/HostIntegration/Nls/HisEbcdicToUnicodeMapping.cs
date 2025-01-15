using System;
using Microsoft.HostIntegration.ConfigurationSectionHandlers.Encoding;
using Microsoft.HostIntegration.StrictResources.Nls;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x02000625 RID: 1573
	internal class HisEbcdicToUnicodeMapping
	{
		// Token: 0x060034FB RID: 13563 RVA: 0x000B1258 File Offset: 0x000AF458
		internal HisEbcdicToUnicodeMapping(EbcdicToUnicodeConversion etuc)
		{
			if (!string.IsNullOrWhiteSpace(etuc.From))
			{
				ushort num = Convert.ToUInt16(etuc.From, 16);
				if (num > 255)
				{
					string text = SR.InvalidEBCDICMappings("from", etuc.From);
					throw new NotSupportedException(SR.InvalidConfigurationFile, new CustomEncodingsMappingDoubleEbcdicBytesNotAllowedException(text));
				}
				this.EbcdicByte = (byte)num;
			}
			if (!string.IsNullOrWhiteSpace(etuc.To))
			{
				this.UnicodeChar = (char)Convert.ToUInt16(etuc.To, 16);
			}
			this.Reversible = etuc.Reversible;
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x060034FC RID: 13564 RVA: 0x000B12E4 File Offset: 0x000AF4E4
		// (set) Token: 0x060034FD RID: 13565 RVA: 0x000B12EC File Offset: 0x000AF4EC
		internal char UnicodeChar { get; private set; }

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x060034FE RID: 13566 RVA: 0x000B12F5 File Offset: 0x000AF4F5
		// (set) Token: 0x060034FF RID: 13567 RVA: 0x000B12FD File Offset: 0x000AF4FD
		internal byte EbcdicByte { get; private set; }

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06003500 RID: 13568 RVA: 0x000B1306 File Offset: 0x000AF506
		// (set) Token: 0x06003501 RID: 13569 RVA: 0x000B130E File Offset: 0x000AF50E
		internal bool Reversible { get; private set; }
	}
}
