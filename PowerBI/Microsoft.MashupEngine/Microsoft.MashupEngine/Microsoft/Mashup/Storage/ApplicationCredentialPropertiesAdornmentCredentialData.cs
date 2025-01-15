using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200205F RID: 8287
	[XmlType("ApplicationCredentialProperties")]
	public class ApplicationCredentialPropertiesAdornmentCredentialData : CredentialData
	{
		// Token: 0x0600CAC6 RID: 51910 RVA: 0x00287642 File Offset: 0x00285842
		public ApplicationCredentialPropertiesAdornmentCredentialData()
		{
		}

		// Token: 0x0600CAC7 RID: 51911 RVA: 0x00287CA4 File Offset: 0x00285EA4
		public ApplicationCredentialPropertiesAdornmentCredentialData(Dictionary<string, object> properties)
		{
			this.properties = new SerializableDictionary<string, object>(properties);
		}

		// Token: 0x170030CB RID: 12491
		// (get) Token: 0x0600CAC8 RID: 51912 RVA: 0x00002139 File Offset: 0x00000339
		public override CredentialDataKind Kind
		{
			get
			{
				return CredentialDataKind.Adornment;
			}
		}

		// Token: 0x170030CC RID: 12492
		// (get) Token: 0x0600CAC9 RID: 51913 RVA: 0x001AA8D9 File Offset: 0x001A8AD9
		public override CredentialDataType Type
		{
			get
			{
				return CredentialDataType.ConnectionStringProperties;
			}
		}

		// Token: 0x170030CD RID: 12493
		// (get) Token: 0x0600CACA RID: 51914 RVA: 0x00287CB8 File Offset: 0x00285EB8
		// (set) Token: 0x0600CACB RID: 51915 RVA: 0x00287CC0 File Offset: 0x00285EC0
		public SerializableDictionary<string, object> Properties
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

		// Token: 0x0600CACC RID: 51916 RVA: 0x00287CC9 File Offset: 0x00285EC9
		public override IResourceCredential ToResourceCredential(IdentityContext context = null)
		{
			return new ApplicationCredentialPropertiesAdornment(this.properties);
		}

		// Token: 0x0600CACD RID: 51917 RVA: 0x00287CD8 File Offset: 0x00285ED8
		public override bool TryMergeWith(CredentialData credentialData)
		{
			ApplicationCredentialPropertiesAdornmentCredentialData applicationCredentialPropertiesAdornmentCredentialData = credentialData as ApplicationCredentialPropertiesAdornmentCredentialData;
			if (applicationCredentialPropertiesAdornmentCredentialData != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in applicationCredentialPropertiesAdornmentCredentialData.Properties)
				{
					this.properties[keyValuePair.Key] = keyValuePair.Value;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600CACE RID: 51918 RVA: 0x0000336E File Offset: 0x0000156E
		public override void Validate()
		{
		}

		// Token: 0x04006707 RID: 26375
		private SerializableDictionary<string, object> properties;
	}
}
