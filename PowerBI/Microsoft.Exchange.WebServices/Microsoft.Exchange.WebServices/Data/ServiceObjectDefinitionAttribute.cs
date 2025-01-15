using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(4, AllowMultiple = false, Inherited = false)]
	internal sealed class ServiceObjectDefinitionAttribute : Attribute
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000210E File Offset: 0x0000110E
		internal ServiceObjectDefinitionAttribute(string xmlElementName)
		{
			this.xmlElementName = xmlElementName;
			this.returnedByServer = true;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002124 File Offset: 0x00001124
		internal string XmlElementName
		{
			get
			{
				return this.xmlElementName;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000212C File Offset: 0x0000112C
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002134 File Offset: 0x00001134
		public bool ReturnedByServer
		{
			get
			{
				return this.returnedByServer;
			}
			set
			{
				this.returnedByServer = value;
			}
		}

		// Token: 0x04000003 RID: 3
		private string xmlElementName;

		// Token: 0x04000004 RID: 4
		private bool returnedByServer;
	}
}
