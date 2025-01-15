using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000BC RID: 188
	[Serializable]
	internal class CASConfigElement : ConfigurationElement, ISerializable
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x00015607 File Offset: 0x00013807
		internal CASConfigElement()
		{
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00015CBA File Offset: 0x00013EBA
		internal CASConfigElement(bool leadHostManagement)
		{
			this.LeadHostManagement = leadHostManagement;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00015CC9 File Offset: 0x00013EC9
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x00015CDB File Offset: 0x00013EDB
		[ConfigurationProperty("leadHostManagement", IsRequired = false, DefaultValue = true)]
		internal bool LeadHostManagement
		{
			get
			{
				return (bool)base["leadHostManagement"];
			}
			set
			{
				base["leadHostManagement"] = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00015CEE File Offset: 0x00013EEE
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x00015D0F File Offset: 0x00013F0F
		internal bool InMemory
		{
			get
			{
				if (this.inMemory == null)
				{
					return this.LeadHostManagement;
				}
				return this.inMemory.Value;
			}
			set
			{
				this.inMemory = new bool?(value);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00015D1D File Offset: 0x00013F1D
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x00015D2F File Offset: 0x00013F2F
		[ConfigurationProperty("overrideCasConfig", IsRequired = false, DefaultValue = true)]
		internal bool OverrideCasConfig
		{
			get
			{
				return (bool)base["overrideCasConfig"];
			}
			set
			{
				base["overrideCasConfig"] = value;
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00015D44 File Offset: 0x00013F44
		protected CASConfigElement(SerializationInfo info, StreamingContext context)
		{
			this.LeadHostManagement = info.GetBoolean("leadHostManagement");
			try
			{
				this.OverrideCasConfig = info.GetBoolean("overrideCasConfig");
			}
			catch (SerializationException)
			{
				this.OverrideCasConfig = true;
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00015D98 File Offset: 0x00013F98
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("leadHostManagement", this.LeadHostManagement);
			info.AddValue("overrideCasConfig", this.OverrideCasConfig);
		}

		// Token: 0x04000361 RID: 865
		internal const string LEAD_HOST_MANAGEMENT = "leadHostManagement";

		// Token: 0x04000362 RID: 866
		internal const string OVERRIDE_CAS_CONFIG = "overrideCasConfig";

		// Token: 0x04000363 RID: 867
		private bool? inMemory;
	}
}
