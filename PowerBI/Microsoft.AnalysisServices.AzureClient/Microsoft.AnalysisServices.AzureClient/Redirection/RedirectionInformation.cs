using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;

namespace Microsoft.AnalysisServices.AzureClient.Redirection
{
	// Token: 0x02000014 RID: 20
	internal sealed class RedirectionInformation
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00002AB9 File Offset: 0x00000CB9
		public RedirectionInformation(RedirectionInformation info)
		{
			this.PbiEndpoint = info.PbiEndpoint;
			this.PbiResourceId = info.PbiResourceId;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002AD9 File Offset: 0x00000CD9
		private RedirectionInformation(RedirectionInformation.RedirectionInformationRecord record)
		{
			this.PbiEndpoint = record.PbiEndpoint;
			this.PbiResourceId = record.PbiResourceId;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002AF9 File Offset: 0x00000CF9
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002B01 File Offset: 0x00000D01
		public string PbiEndpoint { get; internal set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002B0A File Offset: 0x00000D0A
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002B12 File Offset: 0x00000D12
		public string PbiResourceId { get; internal set; }

		// Token: 0x06000073 RID: 115 RVA: 0x00002B1C File Offset: 0x00000D1C
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

		// Token: 0x06000074 RID: 116 RVA: 0x00002B58 File Offset: 0x00000D58
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

		// Token: 0x06000075 RID: 117 RVA: 0x00002BB8 File Offset: 0x00000DB8
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

		// Token: 0x06000076 RID: 118 RVA: 0x00002CAC File Offset: 0x00000EAC
		private static RedirectionInformation.RedirectionInformationRecord[] DeserializeRedirectionInformation(Stream info)
		{
			RedirectionInformation.RedirectionInformationRecord[] array;
			using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(info, new XmlDictionaryReaderQuotas()))
			{
				array = (RedirectionInformation.RedirectionInformationRecord[])new DataContractSerializer(typeof(RedirectionInformation.RedirectionInformationRecord[]), "RedirectionInformations", string.Empty).ReadObject(xmlDictionaryReader, true);
			}
			return array;
		}

		// Token: 0x0400002F RID: 47
		private const string EmbeddedSecurityConfigResourceName = "RedirectionConfig.xml";

		// Token: 0x04000030 RID: 48
		private const string RemoteSecurityConfigUrl = "https://global.asazure.windows.net/RedirectionConfig.xml";

		// Token: 0x04000031 RID: 49
		private static RedirectionInformation.RedirectionInformationRecord[] embeddedRedirectionConfig;

		// Token: 0x04000032 RID: 50
		private static RedirectionInformation.RedirectionInformationRecord[] remoteRedirectionConfig;

		// Token: 0x02000048 RID: 72
		[DataContract(Name = "RedirectionInformation", Namespace = "")]
		private sealed class RedirectionInformationRecord
		{
			// Token: 0x17000057 RID: 87
			// (get) Token: 0x06000220 RID: 544 RVA: 0x0000AD06 File Offset: 0x00008F06
			// (set) Token: 0x06000221 RID: 545 RVA: 0x0000AD0E File Offset: 0x00008F0E
			[DataMember(Name = "DomainPostfix", Order = 0)]
			public string DomainPostfix { get; private set; }

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x06000222 RID: 546 RVA: 0x0000AD17 File Offset: 0x00008F17
			// (set) Token: 0x06000223 RID: 547 RVA: 0x0000AD1F File Offset: 0x00008F1F
			[DataMember(Name = "PbiEndpoint", Order = 10)]
			public string PbiEndpoint { get; private set; }

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000224 RID: 548 RVA: 0x0000AD28 File Offset: 0x00008F28
			// (set) Token: 0x06000225 RID: 549 RVA: 0x0000AD30 File Offset: 0x00008F30
			[DataMember(Name = "PbiResourceId", Order = 20)]
			public string PbiResourceId { get; private set; }
		}
	}
}
