using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200009C RID: 156
	public sealed class SetClientExtensionAction : ComplexProperty
	{
		// Token: 0x0600073A RID: 1850 RVA: 0x00018C9A File Offset: 0x00017C9A
		public SetClientExtensionAction(SetClientExtensionActionId setClientExtensionActionId, string extensionId, ClientExtension clientExtension)
		{
			base.Namespace = XmlNamespace.Types;
			this.setClientExtensionActionId = setClientExtensionActionId;
			this.extensionId = extensionId;
			this.clientExtension = clientExtension;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00018CBE File Offset: 0x00017CBE
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("ActionId", this.setClientExtensionActionId);
			if (!string.IsNullOrEmpty(this.extensionId))
			{
				writer.WriteAttributeValue("ExtensionId", this.extensionId);
			}
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00018CF4 File Offset: 0x00017CF4
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (this.clientExtension != null)
			{
				this.clientExtension.WriteToXml(writer, XmlNamespace.Types, "ClientExtension");
			}
		}

		// Token: 0x0400025E RID: 606
		private readonly SetClientExtensionActionId setClientExtensionActionId;

		// Token: 0x0400025F RID: 607
		private readonly string extensionId;

		// Token: 0x04000260 RID: 608
		private readonly ClientExtension clientExtension;
	}
}
