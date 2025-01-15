using System;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002063 RID: 8291
	[XmlType("SharedKeyAuth")]
	public class SharedKeyAuthCredentialData : FeedKeyCredentialData
	{
		// Token: 0x0600CAF5 RID: 51957 RVA: 0x00287FEC File Offset: 0x002861EC
		public SharedKeyAuthCredentialData()
		{
		}

		// Token: 0x0600CAF6 RID: 51958 RVA: 0x00287FF4 File Offset: 0x002861F4
		public SharedKeyAuthCredentialData(string sharedKey)
		{
			this.sharedKey = sharedKey;
		}

		// Token: 0x170030DB RID: 12507
		// (get) Token: 0x0600CAF7 RID: 51959 RVA: 0x00288003 File Offset: 0x00286203
		// (set) Token: 0x0600CAF8 RID: 51960 RVA: 0x0028800B File Offset: 0x0028620B
		public new string Key
		{
			get
			{
				return this.sharedKey;
			}
			set
			{
				this.sharedKey = value;
			}
		}

		// Token: 0x0600CAF9 RID: 51961 RVA: 0x00288014 File Offset: 0x00286214
		public override IResourceCredential ToResourceCredential(IdentityContext context = null)
		{
			return new SharedKeyAuthCredential(this.sharedKey);
		}

		// Token: 0x0600CAFA RID: 51962 RVA: 0x00288024 File Offset: 0x00286224
		public override bool TryMergeWith(CredentialData credentialData)
		{
			SharedKeyAuthCredentialData sharedKeyAuthCredentialData = credentialData as SharedKeyAuthCredentialData;
			if (sharedKeyAuthCredentialData != null)
			{
				if (sharedKeyAuthCredentialData.sharedKey != null)
				{
					this.sharedKey = sharedKeyAuthCredentialData.sharedKey;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600CAFB RID: 51963 RVA: 0x00288052 File Offset: 0x00286252
		public override void Validate()
		{
			if (string.IsNullOrEmpty(this.sharedKey))
			{
				throw StorageExceptions.CredentialValidationException(Strings.Feed_Key_Not_Specified, null);
			}
		}

		// Token: 0x0400670F RID: 26383
		private string sharedKey;
	}
}
