using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000154 RID: 340
	internal class DelegateManagementResponse : ServiceResponse
	{
		// Token: 0x06001051 RID: 4177 RVA: 0x0002FB84 File Offset: 0x0002EB84
		internal DelegateManagementResponse(bool readDelegateUsers, List<DelegateUser> delegateUsers)
		{
			this.readDelegateUsers = readDelegateUsers;
			this.delegateUsers = delegateUsers;
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0002FB9C File Offset: 0x0002EB9C
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			if (base.ErrorCode == ServiceError.NoError)
			{
				this.delegateUserResponses = new Collection<DelegateUserResponse>();
				reader.Read();
				if (reader.IsStartElement(XmlNamespace.Messages, "ResponseMessages"))
				{
					int num = 0;
					do
					{
						reader.Read();
						if (reader.IsStartElement(XmlNamespace.Messages, "DelegateUserResponseMessageType"))
						{
							DelegateUser delegateUser = null;
							if (this.readDelegateUsers && this.delegateUsers != null)
							{
								delegateUser = this.delegateUsers[num];
							}
							DelegateUserResponse delegateUserResponse = new DelegateUserResponse(this.readDelegateUsers, delegateUser);
							delegateUserResponse.LoadFromXml(reader, "DelegateUserResponseMessageType");
							this.delegateUserResponses.Add(delegateUserResponse);
							num++;
						}
					}
					while (!reader.IsEndElement(XmlNamespace.Messages, "ResponseMessages"));
				}
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x0002FC3F File Offset: 0x0002EC3F
		internal Collection<DelegateUserResponse> DelegateUserResponses
		{
			get
			{
				return this.delegateUserResponses;
			}
		}

		// Token: 0x04000998 RID: 2456
		private bool readDelegateUsers;

		// Token: 0x04000999 RID: 2457
		private List<DelegateUser> delegateUsers;

		// Token: 0x0400099A RID: 2458
		private Collection<DelegateUserResponse> delegateUserResponses;
	}
}
