using System;
using System.Xml.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002061 RID: 8289
	[XmlType("FeedKey")]
	public class FeedKeyCredentialData : CredentialData
	{
		// Token: 0x0600CAD8 RID: 51928 RVA: 0x00287642 File Offset: 0x00285842
		public FeedKeyCredentialData()
		{
		}

		// Token: 0x0600CAD9 RID: 51929 RVA: 0x00287DA2 File Offset: 0x00285FA2
		public FeedKeyCredentialData(string key)
		{
			this.key = key;
		}

		// Token: 0x170030D1 RID: 12497
		// (get) Token: 0x0600CADA RID: 51930 RVA: 0x00287DB1 File Offset: 0x00285FB1
		// (set) Token: 0x0600CADB RID: 51931 RVA: 0x00287DB9 File Offset: 0x00285FB9
		public string Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x170030D2 RID: 12498
		// (get) Token: 0x0600CADC RID: 51932 RVA: 0x00002105 File Offset: 0x00000305
		public override CredentialDataKind Kind
		{
			get
			{
				return CredentialDataKind.Credential;
			}
		}

		// Token: 0x170030D3 RID: 12499
		// (get) Token: 0x0600CADD RID: 51933 RVA: 0x0000244F File Offset: 0x0000064F
		public override CredentialDataType Type
		{
			get
			{
				return CredentialDataType.FeedKey;
			}
		}

		// Token: 0x0600CADE RID: 51934 RVA: 0x00287DC2 File Offset: 0x00285FC2
		public override IResourceCredential ToResourceCredential(IdentityContext context = null)
		{
			return new FeedKeyCredential(this.key);
		}

		// Token: 0x0600CADF RID: 51935 RVA: 0x00287DD0 File Offset: 0x00285FD0
		public override bool TryMergeWith(CredentialData credentialData)
		{
			FeedKeyCredentialData feedKeyCredentialData = credentialData as FeedKeyCredentialData;
			if (feedKeyCredentialData != null)
			{
				if (feedKeyCredentialData.key != null)
				{
					this.key = feedKeyCredentialData.key;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600CAE0 RID: 51936 RVA: 0x00287DFE File Offset: 0x00285FFE
		public override void Validate()
		{
			if (string.IsNullOrEmpty(this.key))
			{
				throw StorageExceptions.CredentialValidationException(Strings.Feed_Key_Not_Specified, null);
			}
		}

		// Token: 0x04006709 RID: 26377
		private string key;
	}
}
