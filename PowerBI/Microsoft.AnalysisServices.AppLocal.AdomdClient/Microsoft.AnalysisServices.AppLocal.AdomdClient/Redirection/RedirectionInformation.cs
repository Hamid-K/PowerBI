using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;

namespace Microsoft.AnalysisServices.AdomdClient.Redirection
{
	// Token: 0x020000F9 RID: 249
	internal sealed class RedirectionInformation
	{
		// Token: 0x06000EA2 RID: 3746 RVA: 0x00031A1E File Offset: 0x0002FC1E
		public RedirectionInformation(RedirectionInformation info)
		{
			this.PbiEndpoint = info.PbiEndpoint;
			this.PbiResourceId = info.PbiResourceId;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00031A3E File Offset: 0x0002FC3E
		private RedirectionInformation(RedirectionInformation.RedirectionInformationRecord record)
		{
			this.PbiEndpoint = record.PbiEndpoint;
			this.PbiResourceId = record.PbiResourceId;
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00031A5E File Offset: 0x0002FC5E
		// (set) Token: 0x06000EA5 RID: 3749 RVA: 0x00031A66 File Offset: 0x0002FC66
		public string PbiEndpoint { get; internal set; }

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x00031A6F File Offset: 0x0002FC6F
		// (set) Token: 0x06000EA7 RID: 3751 RVA: 0x00031A77 File Offset: 0x0002FC77
		public string PbiResourceId { get; internal set; }

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00031A80 File Offset: 0x0002FC80
		public static bool TryGetRedirectionInfo(Uri resource, out RedirectionInformation redirectionInfo)
		{
			RedirectionInformation.RedirectionInformationRecord redirectionInformationRecord;
			if (RedirectionInformation.TryGetRedirectionInformation(resource, RedirectionInformation.GetAllowedRedirectionInformation(true), out redirectionInformationRecord) || RedirectionInformation.TryGetRedirectionInformation(resource, RedirectionInformation.GetAllowedRedirectionInformation(false), out redirectionInformationRecord))
			{
				redirectionInfo = new RedirectionInformation(redirectionInformationRecord);
				return true;
			}
			redirectionInfo = null;
			return false;
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00031ABC File Offset: 0x0002FCBC
		private static bool TryGetRedirectionInformation(Uri dataSource, RedirectionInformation.RedirectionInformationRecord[] knownRecords, out RedirectionInformation.RedirectionInformationRecord record)
		{
			record = null;
			for (int i = 0; i < knownRecords.Length; i++)
			{
				if (dataSource.Host.EndsWith(knownRecords[i].DomainPostfix, StringComparison.InvariantCultureIgnoreCase) && (record == null || knownRecords[i].DomainPostfix.Length > record.DomainPostfix.Length))
				{
					record = knownRecords[i];
				}
			}
			return record != null;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00031B1C File Offset: 0x0002FD1C
		private static RedirectionInformation.RedirectionInformationRecord[] GetAllowedRedirectionInformation(bool isEmbeddedInfo)
		{
			if (isEmbeddedInfo)
			{
				if (RedirectionInformation.embeddedRedirectionConfig == null)
				{
					Assembly executingAssembly = Assembly.GetExecutingAssembly();
					string text = executingAssembly.GetManifestResourceNames().FirstOrDefault((string name) => name.EndsWith("RedirectionConfig.xml"));
					using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(text))
					{
						RedirectionInformation.embeddedRedirectionConfig = RedirectionInformation.DeserializeRedirectionInformation(manifestResourceStream);
					}
				}
				return RedirectionInformation.embeddedRedirectionConfig;
			}
			if (RedirectionInformation.remoteRedirectionConfig == null)
			{
				using (WebClient webClient = new WebClient())
				{
					try
					{
						using (Stream stream = new MemoryStream(webClient.DownloadData("https://global.asazure.windows.net/RedirectionConfig.xml")))
						{
							RedirectionInformation.remoteRedirectionConfig = RedirectionInformation.DeserializeRedirectionInformation(stream);
						}
					}
					catch (WebException)
					{
						RedirectionInformation.remoteRedirectionConfig = new RedirectionInformation.RedirectionInformationRecord[0];
					}
				}
			}
			return RedirectionInformation.remoteRedirectionConfig;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00031C10 File Offset: 0x0002FE10
		private static RedirectionInformation.RedirectionInformationRecord[] DeserializeRedirectionInformation(Stream info)
		{
			RedirectionInformation.RedirectionInformationRecord[] array;
			using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(info, new XmlDictionaryReaderQuotas()))
			{
				array = (RedirectionInformation.RedirectionInformationRecord[])new DataContractSerializer(typeof(RedirectionInformation.RedirectionInformationRecord[]), "RedirectionInformations", string.Empty).ReadObject(xmlDictionaryReader, true);
			}
			return array;
		}

		// Token: 0x0400085B RID: 2139
		private const string EmbeddedSecurityConfigResourceName = "RedirectionConfig.xml";

		// Token: 0x0400085C RID: 2140
		private const string RemoteSecurityConfigUrl = "https://global.asazure.windows.net/RedirectionConfig.xml";

		// Token: 0x0400085D RID: 2141
		private static RedirectionInformation.RedirectionInformationRecord[] embeddedRedirectionConfig;

		// Token: 0x0400085E RID: 2142
		private static RedirectionInformation.RedirectionInformationRecord[] remoteRedirectionConfig;

		// Token: 0x020001CF RID: 463
		[DataContract(Name = "RedirectionInformation", Namespace = "")]
		private sealed class RedirectionInformationRecord
		{
			// Token: 0x170006ED RID: 1773
			// (get) Token: 0x060013E2 RID: 5090 RVA: 0x000454CE File Offset: 0x000436CE
			// (set) Token: 0x060013E3 RID: 5091 RVA: 0x000454D6 File Offset: 0x000436D6
			[DataMember(Name = "DomainPostfix", Order = 0)]
			public string DomainPostfix { get; private set; }

			// Token: 0x170006EE RID: 1774
			// (get) Token: 0x060013E4 RID: 5092 RVA: 0x000454DF File Offset: 0x000436DF
			// (set) Token: 0x060013E5 RID: 5093 RVA: 0x000454E7 File Offset: 0x000436E7
			[DataMember(Name = "PbiEndpoint", Order = 10)]
			public string PbiEndpoint { get; private set; }

			// Token: 0x170006EF RID: 1775
			// (get) Token: 0x060013E6 RID: 5094 RVA: 0x000454F0 File Offset: 0x000436F0
			// (set) Token: 0x060013E7 RID: 5095 RVA: 0x000454F8 File Offset: 0x000436F8
			[DataMember(Name = "PbiResourceId", Order = 20)]
			public string PbiResourceId { get; private set; }
		}
	}
}
