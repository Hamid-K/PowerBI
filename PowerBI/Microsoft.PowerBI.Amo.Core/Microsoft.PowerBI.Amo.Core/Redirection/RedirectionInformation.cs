using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;

namespace Microsoft.AnalysisServices.Redirection
{
	// Token: 0x020000EE RID: 238
	internal sealed class RedirectionInformation
	{
		// Token: 0x06000F33 RID: 3891 RVA: 0x000343C3 File Offset: 0x000325C3
		public RedirectionInformation(RedirectionInformation info)
		{
			this.PbiEndpoint = info.PbiEndpoint;
			this.PbiResourceId = info.PbiResourceId;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x000343E3 File Offset: 0x000325E3
		private RedirectionInformation(RedirectionInformation.RedirectionInformationRecord record)
		{
			this.PbiEndpoint = record.PbiEndpoint;
			this.PbiResourceId = record.PbiResourceId;
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x00034403 File Offset: 0x00032603
		// (set) Token: 0x06000F36 RID: 3894 RVA: 0x0003440B File Offset: 0x0003260B
		public string PbiEndpoint { get; internal set; }

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x00034414 File Offset: 0x00032614
		// (set) Token: 0x06000F38 RID: 3896 RVA: 0x0003441C File Offset: 0x0003261C
		public string PbiResourceId { get; internal set; }

		// Token: 0x06000F39 RID: 3897 RVA: 0x00034428 File Offset: 0x00032628
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

		// Token: 0x06000F3A RID: 3898 RVA: 0x00034464 File Offset: 0x00032664
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

		// Token: 0x06000F3B RID: 3899 RVA: 0x000344C4 File Offset: 0x000326C4
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

		// Token: 0x06000F3C RID: 3900 RVA: 0x000345B8 File Offset: 0x000327B8
		private static RedirectionInformation.RedirectionInformationRecord[] DeserializeRedirectionInformation(Stream info)
		{
			RedirectionInformation.RedirectionInformationRecord[] array;
			using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(info, new XmlDictionaryReaderQuotas()))
			{
				array = (RedirectionInformation.RedirectionInformationRecord[])new DataContractSerializer(typeof(RedirectionInformation.RedirectionInformationRecord[]), "RedirectionInformations", string.Empty).ReadObject(xmlDictionaryReader, true);
			}
			return array;
		}

		// Token: 0x04000817 RID: 2071
		private const string EmbeddedSecurityConfigResourceName = "RedirectionConfig.xml";

		// Token: 0x04000818 RID: 2072
		private const string RemoteSecurityConfigUrl = "https://global.asazure.windows.net/RedirectionConfig.xml";

		// Token: 0x04000819 RID: 2073
		private static RedirectionInformation.RedirectionInformationRecord[] embeddedRedirectionConfig;

		// Token: 0x0400081A RID: 2074
		private static RedirectionInformation.RedirectionInformationRecord[] remoteRedirectionConfig;

		// Token: 0x020001AC RID: 428
		[DataContract(Name = "RedirectionInformation", Namespace = "")]
		private sealed class RedirectionInformationRecord
		{
			// Token: 0x17000632 RID: 1586
			// (get) Token: 0x0600133E RID: 4926 RVA: 0x0004371A File Offset: 0x0004191A
			// (set) Token: 0x0600133F RID: 4927 RVA: 0x00043722 File Offset: 0x00041922
			[DataMember(Name = "DomainPostfix", Order = 0)]
			public string DomainPostfix { get; private set; }

			// Token: 0x17000633 RID: 1587
			// (get) Token: 0x06001340 RID: 4928 RVA: 0x0004372B File Offset: 0x0004192B
			// (set) Token: 0x06001341 RID: 4929 RVA: 0x00043733 File Offset: 0x00041933
			[DataMember(Name = "PbiEndpoint", Order = 10)]
			public string PbiEndpoint { get; private set; }

			// Token: 0x17000634 RID: 1588
			// (get) Token: 0x06001342 RID: 4930 RVA: 0x0004373C File Offset: 0x0004193C
			// (set) Token: 0x06001343 RID: 4931 RVA: 0x00043744 File Offset: 0x00041944
			[DataMember(Name = "PbiResourceId", Order = 20)]
			public string PbiResourceId { get; private set; }
		}
	}
}
