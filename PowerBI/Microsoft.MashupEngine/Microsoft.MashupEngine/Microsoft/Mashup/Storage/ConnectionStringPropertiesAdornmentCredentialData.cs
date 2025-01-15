using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200205E RID: 8286
	[XmlType("ConnectionStringProperties")]
	public class ConnectionStringPropertiesAdornmentCredentialData : CredentialData
	{
		// Token: 0x0600CABD RID: 51901 RVA: 0x00287642 File Offset: 0x00285842
		public ConnectionStringPropertiesAdornmentCredentialData()
		{
		}

		// Token: 0x0600CABE RID: 51902 RVA: 0x00287BFE File Offset: 0x00285DFE
		public ConnectionStringPropertiesAdornmentCredentialData(IDictionary<string, string> properties)
		{
			this.properties = new SerializableDictionary<string, string>(properties);
		}

		// Token: 0x170030C8 RID: 12488
		// (get) Token: 0x0600CABF RID: 51903 RVA: 0x00002139 File Offset: 0x00000339
		public override CredentialDataKind Kind
		{
			get
			{
				return CredentialDataKind.Adornment;
			}
		}

		// Token: 0x170030C9 RID: 12489
		// (get) Token: 0x0600CAC0 RID: 51904 RVA: 0x001AA8D9 File Offset: 0x001A8AD9
		public override CredentialDataType Type
		{
			get
			{
				return CredentialDataType.ConnectionStringProperties;
			}
		}

		// Token: 0x170030CA RID: 12490
		// (get) Token: 0x0600CAC1 RID: 51905 RVA: 0x00287C12 File Offset: 0x00285E12
		// (set) Token: 0x0600CAC2 RID: 51906 RVA: 0x00287C1A File Offset: 0x00285E1A
		public SerializableDictionary<string, string> Properties
		{
			get
			{
				return this.properties;
			}
			set
			{
				this.properties = value;
			}
		}

		// Token: 0x0600CAC3 RID: 51907 RVA: 0x00287C23 File Offset: 0x00285E23
		public override IResourceCredential ToResourceCredential(IdentityContext context = null)
		{
			return new ConnectionStringPropertiesAdornment(this.properties);
		}

		// Token: 0x0600CAC4 RID: 51908 RVA: 0x00287C30 File Offset: 0x00285E30
		public override bool TryMergeWith(CredentialData credentialData)
		{
			ConnectionStringPropertiesAdornmentCredentialData connectionStringPropertiesAdornmentCredentialData = credentialData as ConnectionStringPropertiesAdornmentCredentialData;
			if (connectionStringPropertiesAdornmentCredentialData != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in connectionStringPropertiesAdornmentCredentialData.Properties)
				{
					this.properties[keyValuePair.Key] = keyValuePair.Value;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600CAC5 RID: 51909 RVA: 0x0000336E File Offset: 0x0000156E
		public override void Validate()
		{
		}

		// Token: 0x04006706 RID: 26374
		private SerializableDictionary<string, string> properties;
	}
}
