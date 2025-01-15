using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000148 RID: 328
	internal sealed class UpdateInboxRulesRequest : SimpleServiceRequestBase
	{
		// Token: 0x06000FEA RID: 4074 RVA: 0x0002EB27 File Offset: 0x0002DB27
		internal UpdateInboxRulesRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0002EB30 File Offset: 0x0002DB30
		internal override string GetXmlElementName()
		{
			return "UpdateInboxRules";
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0002EB38 File Offset: 0x0002DB38
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (!string.IsNullOrEmpty(this.mailboxSmtpAddress))
			{
				writer.WriteElementValue(XmlNamespace.Messages, "MailboxSmtpAddress", this.mailboxSmtpAddress);
			}
			writer.WriteElementValue(XmlNamespace.Messages, "RemoveOutlookRuleBlob", this.RemoveOutlookRuleBlob);
			writer.WriteStartElement(XmlNamespace.Messages, "Operations");
			foreach (RuleOperation ruleOperation in this.inboxRuleOperations)
			{
				ruleOperation.WriteToXml(writer, ruleOperation.XmlElementName);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0002EBD4 File Offset: 0x0002DBD4
		internal override string GetResponseXmlElementName()
		{
			return "UpdateInboxRulesResponse";
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0002EBDC File Offset: 0x0002DBDC
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			UpdateInboxRulesResponse updateInboxRulesResponse = new UpdateInboxRulesResponse();
			updateInboxRulesResponse.LoadFromXml(reader, "UpdateInboxRulesResponse");
			return updateInboxRulesResponse;
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0002EBFC File Offset: 0x0002DBFC
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2010_SP1;
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0002EC00 File Offset: 0x0002DC00
		internal override void Validate()
		{
			if (this.inboxRuleOperations == null)
			{
				throw new ArgumentException("RuleOperations cannot be null.", "Operations");
			}
			int num = 0;
			foreach (RuleOperation ruleOperation in this.inboxRuleOperations)
			{
				EwsUtilities.ValidateParam(ruleOperation, "RuleOperation");
				num++;
			}
			if (num == 0)
			{
				throw new ArgumentException("RuleOperations cannot be empty.", "Operations");
			}
			base.Service.Validate();
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0002EC90 File Offset: 0x0002DC90
		internal UpdateInboxRulesResponse Execute()
		{
			UpdateInboxRulesResponse updateInboxRulesResponse = (UpdateInboxRulesResponse)base.InternalExecute();
			if (updateInboxRulesResponse.Result == ServiceResult.Error)
			{
				throw new UpdateInboxRulesException(updateInboxRulesResponse, this.inboxRuleOperations.GetEnumerator());
			}
			return updateInboxRulesResponse;
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x0002ECC5 File Offset: 0x0002DCC5
		// (set) Token: 0x06000FF3 RID: 4083 RVA: 0x0002ECCD File Offset: 0x0002DCCD
		internal string MailboxSmtpAddress
		{
			get
			{
				return this.mailboxSmtpAddress;
			}
			set
			{
				this.mailboxSmtpAddress = value;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x0002ECD6 File Offset: 0x0002DCD6
		// (set) Token: 0x06000FF5 RID: 4085 RVA: 0x0002ECDE File Offset: 0x0002DCDE
		internal bool RemoveOutlookRuleBlob
		{
			get
			{
				return this.removeOutlookRuleBlob;
			}
			set
			{
				this.removeOutlookRuleBlob = value;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x0002ECE7 File Offset: 0x0002DCE7
		// (set) Token: 0x06000FF7 RID: 4087 RVA: 0x0002ECEF File Offset: 0x0002DCEF
		internal IEnumerable<RuleOperation> InboxRuleOperations
		{
			get
			{
				return this.inboxRuleOperations;
			}
			set
			{
				this.inboxRuleOperations = value;
			}
		}

		// Token: 0x0400097F RID: 2431
		private string mailboxSmtpAddress;

		// Token: 0x04000980 RID: 2432
		private bool removeOutlookRuleBlob;

		// Token: 0x04000981 RID: 2433
		private IEnumerable<RuleOperation> inboxRuleOperations;
	}
}
