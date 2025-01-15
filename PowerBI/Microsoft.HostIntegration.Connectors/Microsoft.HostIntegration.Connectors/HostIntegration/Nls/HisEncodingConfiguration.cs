using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.HostIntegration.ConfigurationSectionHandlers.Encoding;
using Microsoft.HostIntegration.StrictResources.Nls;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x02000627 RID: 1575
	internal class HisEncodingConfiguration
	{
		// Token: 0x06003511 RID: 13585 RVA: 0x000B14AC File Offset: 0x000AF6AC
		internal HisEncodingConfiguration(EncodingConfigurationSectionHandler encodingConfig)
		{
			List<HisCustomEncodingMappings> list = new List<HisCustomEncodingMappings>();
			if (encodingConfig != null)
			{
				foreach (object obj in encodingConfig.CodePages)
				{
					HisCustomEncodingMappings hisCustomEncodingMappings = new HisCustomEncodingMappings((CodePage)obj);
					using (List<HisCustomEncodingMappings>.Enumerator enumerator2 = list.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.CodePage == hisCustomEncodingMappings.CodePage)
							{
								throw new NotSupportedException(SR.InvalidConfigurationFile, new CustomEncodingsInvalidConfigurationFileException(SR.DuplicateCodePageDefination(hisCustomEncodingMappings.CodePage)));
							}
						}
					}
					list.Add(hisCustomEncodingMappings);
				}
			}
			this.CustomEncodings = list;
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06003512 RID: 13586 RVA: 0x000B1588 File Offset: 0x000AF788
		// (set) Token: 0x06003513 RID: 13587 RVA: 0x000B1590 File Offset: 0x000AF790
		internal List<HisCustomEncodingMappings> CustomEncodings { get; private set; }

		// Token: 0x06003514 RID: 13588 RVA: 0x000B159C File Offset: 0x000AF79C
		internal void AddEuroReplacementPages()
		{
			foreach (Tuple<int, int, byte> tuple in HisEncodingConfiguration.euroReplacementCodePageInformation)
			{
				int item = tuple.Item1;
				int item2 = tuple.Item2;
				byte item3 = tuple.Item3;
				bool flag = false;
				using (List<HisCustomEncodingMappings>.Enumerator enumerator2 = this.CustomEncodings.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.CodePage == item)
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					Encoding encoding = Encoding.GetEncoding(item2);
					CodePage codePage = new CodePage();
					codePage.Number = item;
					codePage.Name = "cp" + item.ToString(CultureInfo.InvariantCulture);
					codePage.Description = encoding.EncodingName + "(with Euro)";
					codePage.NlsCodePage = item2;
					UnicodeToEbcdicConversion unicodeToEbcdicConversion = new UnicodeToEbcdicConversion
					{
						From = "20AC",
						To = item3.ToString("X2"),
						Reversible = true
					};
					codePage.UnicodeToEbcdicConversions.AddUnicodeToEbcdicConversion(unicodeToEbcdicConversion);
					HisCustomEncodingMappings hisCustomEncodingMappings = new HisCustomEncodingMappings(codePage, true);
					this.CustomEncodings.Add(hisCustomEncodingMappings);
				}
			}
		}

		// Token: 0x04001E0A RID: 7690
		private static List<Tuple<int, int, byte>> euroReplacementCodePageInformation = new List<Tuple<int, int, byte>>
		{
			new Tuple<int, int, byte>(1153, 870, 159),
			new Tuple<int, int, byte>(1154, 21025, 225),
			new Tuple<int, int, byte>(1155, 1026, 159),
			new Tuple<int, int, byte>(1160, 20838, 254)
		};
	}
}
