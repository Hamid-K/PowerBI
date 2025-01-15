using System;
using Microsoft.AnalysisServices.Tabular.Json.Linq;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000EE RID: 238
	[CompatibilityRequirement("1400")]
	public sealed class ConnectionAddress
	{
		// Token: 0x06000FB2 RID: 4018 RVA: 0x000774CC File Offset: 0x000756CC
		internal ConnectionAddress(ConnectionDetails owner)
		{
			this.owner = owner;
		}

		// Token: 0x170003DF RID: 991
		public object this[string key]
		{
			get
			{
				return this.owner.GetAddressNestedPropertyValue<object>(key);
			}
			set
			{
				this.owner.SetAddressNestedPropertyValue<object>(key, value);
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x000774F8 File Offset: 0x000756F8
		// (set) Token: 0x06000FB6 RID: 4022 RVA: 0x0007750A File Offset: 0x0007570A
		public string Server
		{
			get
			{
				return this.owner.GetAddressNestedPropertyValue<string>("server");
			}
			set
			{
				this.owner.SetAddressNestedPropertyValue<string>("server", value);
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x0007751D File Offset: 0x0007571D
		// (set) Token: 0x06000FB8 RID: 4024 RVA: 0x0007752F File Offset: 0x0007572F
		public string Database
		{
			get
			{
				return this.owner.GetAddressNestedPropertyValue<string>("database");
			}
			set
			{
				this.owner.SetAddressNestedPropertyValue<string>("database", value);
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x00077542 File Offset: 0x00075742
		// (set) Token: 0x06000FBA RID: 4026 RVA: 0x00077554 File Offset: 0x00075754
		public string Model
		{
			get
			{
				return this.owner.GetAddressNestedPropertyValue<string>("model");
			}
			set
			{
				this.owner.SetAddressNestedPropertyValue<string>("model", value);
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x00077567 File Offset: 0x00075767
		// (set) Token: 0x06000FBC RID: 4028 RVA: 0x00077579 File Offset: 0x00075779
		public string Schema
		{
			get
			{
				return this.owner.GetAddressNestedPropertyValue<string>("schema");
			}
			set
			{
				this.owner.SetAddressNestedPropertyValue<string>("schema", value);
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x0007758C File Offset: 0x0007578C
		// (set) Token: 0x06000FBE RID: 4030 RVA: 0x0007759E File Offset: 0x0007579E
		public string Object
		{
			get
			{
				return this.owner.GetAddressNestedPropertyValue<string>("object");
			}
			set
			{
				this.owner.SetAddressNestedPropertyValue<string>("object", value);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x000775B1 File Offset: 0x000757B1
		// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x000775C3 File Offset: 0x000757C3
		public string Url
		{
			get
			{
				return this.owner.GetAddressNestedPropertyValue<string>("url");
			}
			set
			{
				this.owner.SetAddressNestedPropertyValue<string>("url", value);
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x000775D6 File Offset: 0x000757D6
		// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x000775E8 File Offset: 0x000757E8
		public string ContentType
		{
			get
			{
				return this.owner.GetAddressNestedPropertyValue<string>("contentType");
			}
			set
			{
				this.owner.SetAddressNestedPropertyValue<string>("contentType", value);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x000775FB File Offset: 0x000757FB
		// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x0007760D File Offset: 0x0007580D
		public string Resource
		{
			get
			{
				return this.owner.GetAddressNestedPropertyValue<string>("resource");
			}
			set
			{
				this.owner.SetAddressNestedPropertyValue<string>("resource", value);
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x00077620 File Offset: 0x00075820
		// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x00077632 File Offset: 0x00075832
		public string Path
		{
			get
			{
				return this.owner.GetAddressNestedPropertyValue<string>("path");
			}
			set
			{
				this.owner.SetAddressNestedPropertyValue<string>("path", value);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x00077645 File Offset: 0x00075845
		// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x00077657 File Offset: 0x00075857
		public string Domain
		{
			get
			{
				return this.owner.GetAddressNestedPropertyValue<string>("domain");
			}
			set
			{
				this.owner.SetAddressNestedPropertyValue<string>("domain", value);
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x0007766A File Offset: 0x0007586A
		// (set) Token: 0x06000FCA RID: 4042 RVA: 0x0007767C File Offset: 0x0007587C
		public string Account
		{
			get
			{
				return this.owner.GetAddressNestedPropertyValue<string>("account");
			}
			set
			{
				this.owner.SetAddressNestedPropertyValue<string>("account", value);
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x0007768F File Offset: 0x0007588F
		// (set) Token: 0x06000FCC RID: 4044 RVA: 0x000776A1 File Offset: 0x000758A1
		public string EmailAddress
		{
			get
			{
				return this.owner.GetAddressNestedPropertyValue<string>("emailAddress");
			}
			set
			{
				this.owner.SetAddressNestedPropertyValue<string>("emailAddress", value);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x000776B4 File Offset: 0x000758B4
		// (set) Token: 0x06000FCE RID: 4046 RVA: 0x000776C6 File Offset: 0x000758C6
		public string ConnectionString
		{
			get
			{
				return this.owner.GetAddressNestedPropertyValue<string>("connectionstring");
			}
			set
			{
				this.owner.SetAddressNestedPropertyValue<string>("connectionstring", value);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x000776D9 File Offset: 0x000758D9
		// (set) Token: 0x06000FD0 RID: 4048 RVA: 0x000776EB File Offset: 0x000758EB
		public string Property
		{
			get
			{
				return this.owner.GetAddressNestedPropertyValue<string>("property");
			}
			set
			{
				this.owner.SetAddressNestedPropertyValue<string>("property", value);
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x000776FE File Offset: 0x000758FE
		// (set) Token: 0x06000FD2 RID: 4050 RVA: 0x00077710 File Offset: 0x00075910
		public string View
		{
			get
			{
				return this.owner.GetAddressNestedPropertyValue<string>("view");
			}
			set
			{
				this.owner.SetAddressNestedPropertyValue<string>("view", value);
			}
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00077723 File Offset: 0x00075923
		internal JToken ToJson()
		{
			return this.owner.GetAddressAsJsonObject();
		}

		// Token: 0x040001EB RID: 491
		private ConnectionDetails owner;
	}
}
