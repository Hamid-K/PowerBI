using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000E5 RID: 229
	internal abstract class DelegateManagementRequestBase<TResponse> : SimpleServiceRequestBase where TResponse : DelegateManagementResponse
	{
		// Token: 0x06000BBB RID: 3003 RVA: 0x00027C9F File Offset: 0x00026C9F
		internal DelegateManagementRequestBase(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00027CA8 File Offset: 0x00026CA8
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.Mailbox, "Mailbox");
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00027CC0 File Offset: 0x00026CC0
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.Mailbox.WriteToXml(writer, XmlNamespace.Messages, "Mailbox");
		}

		// Token: 0x06000BBE RID: 3006
		internal abstract TResponse CreateResponse();

		// Token: 0x06000BBF RID: 3007 RVA: 0x00027CD4 File Offset: 0x00026CD4
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			DelegateManagementResponse delegateManagementResponse = this.CreateResponse();
			delegateManagementResponse.LoadFromXml(reader, this.GetResponseXmlElementName());
			return delegateManagementResponse;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00027CFC File Offset: 0x00026CFC
		internal TResponse Execute()
		{
			TResponse tresponse = (TResponse)((object)base.InternalExecute());
			tresponse.ThrowIfNecessary();
			return tresponse;
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x00027D23 File Offset: 0x00026D23
		// (set) Token: 0x06000BC2 RID: 3010 RVA: 0x00027D2B File Offset: 0x00026D2B
		public Mailbox Mailbox
		{
			get
			{
				return this.mailbox;
			}
			set
			{
				this.mailbox = value;
			}
		}

		// Token: 0x040008AF RID: 2223
		private Mailbox mailbox;
	}
}
