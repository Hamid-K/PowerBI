using System;
using System.Collections.Generic;
using Microsoft.HostIntegration.ConfigurationSectionHandlers.Encoding;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x02000626 RID: 1574
	internal class HisCustomEncodingMappings
	{
		// Token: 0x06003502 RID: 13570 RVA: 0x000B1318 File Offset: 0x000AF518
		internal HisCustomEncodingMappings(CodePage cp)
		{
			this.CodePage = cp.Number;
			this.InheritedCodePage = cp.NlsCodePage;
			this.Name = cp.Name;
			this.description = cp.Description;
			List<HisEbcdicToUnicodeMapping> list = new List<HisEbcdicToUnicodeMapping>();
			foreach (object obj in cp.EbcdicToUnicodeConversions)
			{
				HisEbcdicToUnicodeMapping hisEbcdicToUnicodeMapping = new HisEbcdicToUnicodeMapping((EbcdicToUnicodeConversion)obj);
				list.Add(hisEbcdicToUnicodeMapping);
			}
			this.EbcdicToUnicodeMapping = list;
			List<HisUnicodeToEbcdicMapping> list2 = new List<HisUnicodeToEbcdicMapping>();
			foreach (object obj2 in cp.UnicodeToEbcdicConversions)
			{
				HisUnicodeToEbcdicMapping hisUnicodeToEbcdicMapping = new HisUnicodeToEbcdicMapping((UnicodeToEbcdicConversion)obj2);
				list2.Add(hisUnicodeToEbcdicMapping);
			}
			this.UnicodeToEbcDicMapping = list2;
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x000B141C File Offset: 0x000AF61C
		internal HisCustomEncodingMappings(CodePage cp, bool isEuroReplacement)
			: this(cp)
		{
			this.IsEuroReplacement = isEuroReplacement;
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06003504 RID: 13572 RVA: 0x000B142C File Offset: 0x000AF62C
		// (set) Token: 0x06003505 RID: 13573 RVA: 0x000B1434 File Offset: 0x000AF634
		internal string Name { get; private set; }

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06003506 RID: 13574 RVA: 0x000B143D File Offset: 0x000AF63D
		internal string Description
		{
			get
			{
				if (this.description != null)
				{
					return this.description;
				}
				return this.Name;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06003507 RID: 13575 RVA: 0x000B1454 File Offset: 0x000AF654
		// (set) Token: 0x06003508 RID: 13576 RVA: 0x000B145C File Offset: 0x000AF65C
		internal int InheritedCodePage { get; private set; }

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06003509 RID: 13577 RVA: 0x000B1465 File Offset: 0x000AF665
		// (set) Token: 0x0600350A RID: 13578 RVA: 0x000B146D File Offset: 0x000AF66D
		internal int CodePage { get; private set; }

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x0600350B RID: 13579 RVA: 0x000B1476 File Offset: 0x000AF676
		// (set) Token: 0x0600350C RID: 13580 RVA: 0x000B147E File Offset: 0x000AF67E
		internal List<HisUnicodeToEbcdicMapping> UnicodeToEbcDicMapping { get; private set; }

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x0600350D RID: 13581 RVA: 0x000B1487 File Offset: 0x000AF687
		// (set) Token: 0x0600350E RID: 13582 RVA: 0x000B148F File Offset: 0x000AF68F
		internal List<HisEbcdicToUnicodeMapping> EbcdicToUnicodeMapping { get; private set; }

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x0600350F RID: 13583 RVA: 0x000B1498 File Offset: 0x000AF698
		// (set) Token: 0x06003510 RID: 13584 RVA: 0x000B14A0 File Offset: 0x000AF6A0
		internal bool IsEuroReplacement { get; private set; }

		// Token: 0x04001E03 RID: 7683
		private string description;
	}
}
