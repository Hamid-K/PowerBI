using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000155 RID: 341
	public sealed class DelegateUserResponse : ServiceResponse
	{
		// Token: 0x06001054 RID: 4180 RVA: 0x0002FC47 File Offset: 0x0002EC47
		internal DelegateUserResponse(bool readDelegateUser, DelegateUser delegateUser)
		{
			this.readDelegateUser = readDelegateUser;
			this.delegateUser = delegateUser;
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0002FC5D File Offset: 0x0002EC5D
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			if (this.readDelegateUser)
			{
				if (this.delegateUser == null)
				{
					this.delegateUser = new DelegateUser();
				}
				reader.ReadStartElement(XmlNamespace.Messages, "DelegateUser");
				this.delegateUser.LoadFromXml(reader, XmlNamespace.Messages, reader.LocalName);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0002FC99 File Offset: 0x0002EC99
		public DelegateUser DelegateUser
		{
			get
			{
				return this.delegateUser;
			}
		}

		// Token: 0x0400099B RID: 2459
		private bool readDelegateUser;

		// Token: 0x0400099C RID: 2460
		private DelegateUser delegateUser;
	}
}
