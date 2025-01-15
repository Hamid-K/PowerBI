using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Hybrid.OAuth
{
	// Token: 0x02000012 RID: 18
	[DataContract]
	public sealed class ServiceToken
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002DCE File Offset: 0x00000FCE
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002DD6 File Offset: 0x00000FD6
		[DataMember(Name = "access_token")]
		public string AccessToken { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002DDF File Offset: 0x00000FDF
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002DE7 File Offset: 0x00000FE7
		[DataMember(Name = "token_type")]
		public string TokenType { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002DF0 File Offset: 0x00000FF0
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002DF8 File Offset: 0x00000FF8
		[DataMember(Name = "expires_in")]
		public long ExpiresIn { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002E01 File Offset: 0x00001001
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002E09 File Offset: 0x00001009
		[DataMember(Name = "expires_on")]
		public long ExpiresOn { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002E12 File Offset: 0x00001012
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002E1A File Offset: 0x0000101A
		[DataMember(Name = "resource")]
		public string Resource { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002E23 File Offset: 0x00001023
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002E2B File Offset: 0x0000102B
		[DataMember(Name = "refresh_token")]
		public string RefreshToken { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002E34 File Offset: 0x00001034
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002E3C File Offset: 0x0000103C
		[DataMember(Name = "scope")]
		public string Scope { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002E45 File Offset: 0x00001045
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002E4D File Offset: 0x0000104D
		[DataMember(Name = "id_token")]
		public string IdToken { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002E56 File Offset: 0x00001056
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002E5E File Offset: 0x0000105E
		[DataMember(Name = "error")]
		public string Error { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002E67 File Offset: 0x00001067
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002E6F File Offset: 0x0000106F
		[DataMember(Name = "error_description")]
		public string ErrorDescription { get; set; }

		// Token: 0x06000072 RID: 114 RVA: 0x00002E78 File Offset: 0x00001078
		public string ToJson()
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(ServiceToken));
			MemoryStream memoryStream = new MemoryStream();
			string text;
			try
			{
				dataContractJsonSerializer.WriteObject(memoryStream, this);
				text = Encoding.Default.GetString(memoryStream.ToArray());
			}
			catch (SerializationException ex)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Warning, "Error serializing saved json Service token. Message from Exception: " + ex.Message);
				text = null;
			}
			return text;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002EE8 File Offset: 0x000010E8
		public static ServiceToken FromJson(string json)
		{
			ServiceToken serviceToken;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
				{
					serviceToken = (ServiceToken)new DataContractJsonSerializer(typeof(ServiceToken)).ReadObject(memoryStream);
				}
			}
			catch (SerializationException ex)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Warning, "Error deserialzing json Service token. Message from Exception: " + ex.Message);
				serviceToken = null;
			}
			return serviceToken;
		}
	}
}
