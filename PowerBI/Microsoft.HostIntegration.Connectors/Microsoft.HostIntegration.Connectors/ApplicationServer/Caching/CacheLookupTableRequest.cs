using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020003AA RID: 938
	[DataContract(Name = "VasCacheLookupTableRequest", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class CacheLookupTableRequest
	{
		// Token: 0x06002152 RID: 8530 RVA: 0x00066E09 File Offset: 0x00065009
		internal CacheLookupTableRequest(VersionRanges ranges, string cacheName, GenerationNumber generationNumber)
		{
			this.Ranges = ranges;
			this.CacheName = cacheName;
			this.GenerationNumber = generationNumber;
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x00066E26 File Offset: 0x00065026
		internal CacheLookupTableRequest(VersionRanges ranges, HashSet<string> cacheNames, GenerationNumber generationNumber)
		{
			this.Ranges = ranges;
			this.CacheNames = cacheNames;
			this.GenerationNumber = generationNumber;
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06002154 RID: 8532 RVA: 0x00066E43 File Offset: 0x00065043
		// (set) Token: 0x06002155 RID: 8533 RVA: 0x00066E4B File Offset: 0x0006504B
		[DataMember]
		internal VersionRanges Ranges
		{
			get
			{
				return this.m_ranges;
			}
			private set
			{
				this.m_ranges = value;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06002156 RID: 8534 RVA: 0x00066E54 File Offset: 0x00065054
		// (set) Token: 0x06002157 RID: 8535 RVA: 0x00066E5C File Offset: 0x0006505C
		[DataMember]
		internal string CacheName
		{
			get
			{
				return this.m_cacheName;
			}
			private set
			{
				this.m_cacheName = value;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x00066E65 File Offset: 0x00065065
		// (set) Token: 0x06002159 RID: 8537 RVA: 0x00066E6D File Offset: 0x0006506D
		[DataMember(Order = 2, IsRequired = false)]
		internal HashSet<string> CacheNames
		{
			get
			{
				return this.m_cacheNames;
			}
			set
			{
				this.m_cacheNames = value;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x0600215A RID: 8538 RVA: 0x00066E76 File Offset: 0x00065076
		// (set) Token: 0x0600215B RID: 8539 RVA: 0x00066E7E File Offset: 0x0006507E
		[DataMember]
		internal GenerationNumber GenerationNumber
		{
			get
			{
				return this.m_generationNumber;
			}
			private set
			{
				this.m_generationNumber = value;
			}
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x00066E88 File Offset: 0x00065088
		public override string ToString()
		{
			string text = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[] { this.m_ranges, this.m_generationNumber });
			if (!string.IsNullOrEmpty(this.m_cacheName))
			{
				text += this.m_cacheName;
			}
			return text;
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x00066EDC File Offset: 0x000650DC
		public LookupTableRequest GetCasLookupTableRequest()
		{
			HashSet<string> hashSet;
			if (this.m_cacheName != null)
			{
				string[] array = new string[] { this.m_cacheName };
				hashSet = new HashSet<string>(array);
			}
			else
			{
				hashSet = this.m_cacheNames;
			}
			return new LookupTableRequest(this.m_ranges, hashSet, this.m_generationNumber);
		}

		// Token: 0x0400153C RID: 5436
		private VersionRanges m_ranges;

		// Token: 0x0400153D RID: 5437
		private string m_cacheName;

		// Token: 0x0400153E RID: 5438
		private GenerationNumber m_generationNumber;

		// Token: 0x0400153F RID: 5439
		private HashSet<string> m_cacheNames;
	}
}
