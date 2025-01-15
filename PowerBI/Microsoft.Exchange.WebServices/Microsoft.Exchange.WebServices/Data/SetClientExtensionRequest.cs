using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000138 RID: 312
	internal sealed class SetClientExtensionRequest : MultiResponseServiceRequest<ServiceResponse>
	{
		// Token: 0x06000F26 RID: 3878 RVA: 0x0002D7A7 File Offset: 0x0002C7A7
		internal SetClientExtensionRequest(ExchangeService service, List<SetClientExtensionAction> actions)
			: base(service, ServiceErrorHandling.ThrowOnError)
		{
			this.actions = actions;
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0002D7B8 File Offset: 0x0002C7B8
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.actions, "actions");
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0002D7D0 File Offset: 0x0002C7D0
		internal override ServiceResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new ServiceResponse();
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0002D7D7 File Offset: 0x0002C7D7
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0002D7DA File Offset: 0x0002C7DA
		internal override int GetExpectedResponseMessageCount()
		{
			return this.actions.Count;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0002D7E7 File Offset: 0x0002C7E7
		internal override string GetXmlElementName()
		{
			return "SetClientExtension";
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0002D7EE File Offset: 0x0002C7EE
		internal override string GetResponseXmlElementName()
		{
			return "SetClientExtensionResponse";
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0002D7F5 File Offset: 0x0002C7F5
		internal override string GetResponseMessageXmlElementName()
		{
			return "SetClientExtensionResponseMessage";
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0002D7FC File Offset: 0x0002C7FC
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Messages, "Actions");
			foreach (SetClientExtensionAction setClientExtensionAction in this.actions)
			{
				setClientExtensionAction.WriteToXml(writer, "Action");
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000957 RID: 2391
		private readonly List<SetClientExtensionAction> actions;
	}
}
