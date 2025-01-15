using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002067 RID: 8295
	[XmlType("ParameterizedAuth")]
	public class ParameterizedAuthCredentialData : CredentialData
	{
		// Token: 0x0600CB18 RID: 51992 RVA: 0x00287642 File Offset: 0x00285842
		public ParameterizedAuthCredentialData()
		{
		}

		// Token: 0x0600CB19 RID: 51993 RVA: 0x00288222 File Offset: 0x00286422
		public ParameterizedAuthCredentialData(string credentialName, SerializableDictionary<string, string> values)
		{
			this.CredentialName = credentialName;
			this.Values = values;
		}

		// Token: 0x170030E5 RID: 12517
		// (get) Token: 0x0600CB1A RID: 51994 RVA: 0x00002105 File Offset: 0x00000305
		public override CredentialDataKind Kind
		{
			get
			{
				return CredentialDataKind.Credential;
			}
		}

		// Token: 0x170030E6 RID: 12518
		// (get) Token: 0x0600CB1B RID: 51995 RVA: 0x0006808E File Offset: 0x0006628E
		public override CredentialDataType Type
		{
			get
			{
				return CredentialDataType.ParameterizedAuth;
			}
		}

		// Token: 0x0600CB1C RID: 51996 RVA: 0x00288238 File Offset: 0x00286438
		public override IResourceCredential ToResourceCredential(IdentityContext context = null)
		{
			return new ParameterizedCredential(this.CredentialName, this.Values);
		}

		// Token: 0x170030E7 RID: 12519
		// (get) Token: 0x0600CB1D RID: 51997 RVA: 0x0028824B File Offset: 0x0028644B
		// (set) Token: 0x0600CB1E RID: 51998 RVA: 0x00288253 File Offset: 0x00286453
		public string CredentialName { get; set; }

		// Token: 0x170030E8 RID: 12520
		// (get) Token: 0x0600CB1F RID: 51999 RVA: 0x0028825C File Offset: 0x0028645C
		// (set) Token: 0x0600CB20 RID: 52000 RVA: 0x00288264 File Offset: 0x00286464
		public SerializableDictionary<string, string> Values { get; set; }

		// Token: 0x0600CB21 RID: 52001 RVA: 0x00288270 File Offset: 0x00286470
		public override bool TryMergeWith(CredentialData credentialData)
		{
			ParameterizedAuthCredentialData parameterizedAuthCredentialData = credentialData as ParameterizedAuthCredentialData;
			if (parameterizedAuthCredentialData != null && this.CredentialName == parameterizedAuthCredentialData.CredentialName)
			{
				foreach (KeyValuePair<string, string> keyValuePair in parameterizedAuthCredentialData.Values)
				{
					this.Values[keyValuePair.Key] = keyValuePair.Value;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600CB22 RID: 52002 RVA: 0x0000336E File Offset: 0x0000156E
		public override void Validate()
		{
		}
	}
}
