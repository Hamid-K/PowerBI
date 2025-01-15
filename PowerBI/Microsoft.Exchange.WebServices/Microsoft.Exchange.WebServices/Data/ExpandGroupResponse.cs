using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200015A RID: 346
	internal sealed class ExpandGroupResponse : ServiceResponse
	{
		// Token: 0x06001063 RID: 4195 RVA: 0x0002FE71 File Offset: 0x0002EE71
		internal ExpandGroupResponse()
		{
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0002FE84 File Offset: 0x0002EE84
		public ExpandGroupResults Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0002FE8C File Offset: 0x0002EE8C
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			this.Members.LoadFromXml(reader);
		}

		// Token: 0x040009A0 RID: 2464
		private ExpandGroupResults members = new ExpandGroupResults();
	}
}
