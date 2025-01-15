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
		// Token: 0x06000E95 RID: 3733 RVA: 0x000316EE File Offset: 0x0002F8EE
		public RedirectionInformation(RedirectionInformation info)
		{
			this.PbiEndpoint = info.PbiEndpoint;
			this.PbiResourceId = info.PbiResourceId;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0003170E File Offset: 0x0002F90E
		private RedirectionInformation(RedirectionInformation.RedirectionInformationRecord record)
		{
			this.PbiEndpoint = record.PbiEndpoint;
			this.PbiResourceId = record.PbiResourceId;
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x0003172E File Offset: 0x0002F92E
		// (set) Token: 0x06000E98 RID: 3736 RVA: 0x00031736 File Offset: 0x0002F936
		public string PbiEndpoint { get; internal set; }

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x0003173F File Offset: 0x0002F93F
		// (set) Token: 0x06000E9A RID: 3738 RVA: 0x00031747 File Offset: 0x0002F947
		public string PbiResourceId { get; internal set; }

		// Token: 0x06000E9B RID: 3739 RVA: 0x00031750 File Offset: 0x0002F950
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

		// Token: 0x06000E9C RID: 3740 RVA: 0x0003178C File Offset: 0x0002F98C
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

		// Token: 0x06000E9D RID: 3741 RVA: 0x000317EC File Offset: 0x0002F9EC
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

		// Token: 0x06000E9E RID: 3742 RVA: 0x000318E0 File Offset: 0x0002FAE0
		private static RedirectionInformation.RedirectionInformationRecord[] DeserializeRedirectionInformation(Stream info)
		{
			RedirectionInformation.RedirectionInformationRecord[] array;
			using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(info, new XmlDictionaryReaderQuotas()))
			{
				array = (RedirectionInformation.RedirectionInformationRecord[])new DataContractSerializer(typeof(RedirectionInformation.RedirectionInformationRecord[]), "RedirectionInformations", string.Empty).ReadObject(xmlDictionaryReader, true);
			}
			return array;
		}

		// Token: 0x0400084E RID: 2126
		private const string EmbeddedSecurityConfigResourceName = "RedirectionConfig.xml";

		// Token: 0x0400084F RID: 2127
		private const string RemoteSecurityConfigUrl = "https://global.asazure.windows.net/RedirectionConfig.xml";

		// Token: 0x04000850 RID: 2128
		private static RedirectionInformation.RedirectionInformationRecord[] embeddedRedirectionConfig;

		// Token: 0x04000851 RID: 2129
		private static RedirectionInformation.RedirectionInformationRecord[] remoteRedirectionConfig;

		// Token: 0x020001CF RID: 463
		[DataContract(Name = "RedirectionInformation", Namespace = "")]
		private sealed class RedirectionInformationRecord
		{
			// Token: 0x170006E7 RID: 1767
			// (get) Token: 0x060013D5 RID: 5077 RVA: 0x00044F92 File Offset: 0x00043192
			// (set) Token: 0x060013D6 RID: 5078 RVA: 0x00044F9A File Offset: 0x0004319A
			[DataMember(Name = "DomainPostfix", Order = 0)]
			public string DomainPostfix { get; private set; }

			// Token: 0x170006E8 RID: 1768
			// (get) Token: 0x060013D7 RID: 5079 RVA: 0x00044FA3 File Offset: 0x000431A3
			// (set) Token: 0x060013D8 RID: 5080 RVA: 0x00044FAB File Offset: 0x000431AB
			[DataMember(Name = "PbiEndpoint", Order = 10)]
			public string PbiEndpoint { get; private set; }

			// Token: 0x170006E9 RID: 1769
			// (get) Token: 0x060013D9 RID: 5081 RVA: 0x00044FB4 File Offset: 0x000431B4
			// (set) Token: 0x060013DA RID: 5082 RVA: 0x00044FBC File Offset: 0x000431BC
			[DataMember(Name = "PbiResourceId", Order = 20)]
			public string PbiResourceId { get; private set; }
		}
	}
}
