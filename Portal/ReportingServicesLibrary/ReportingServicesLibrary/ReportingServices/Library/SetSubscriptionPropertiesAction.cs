using System;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000210 RID: 528
	internal sealed class SetSubscriptionPropertiesAction : RSSoapAction<SetSubscriptionPropertiesActionParameters>
	{
		// Token: 0x060012B9 RID: 4793 RVA: 0x00042C38 File Offset: 0x00040E38
		public SetSubscriptionPropertiesAction(RSService service)
			: base("SetSubscriptionPropertiesAction", service)
		{
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00042C48 File Offset: 0x00040E48
		protected override void AddActionToBatch()
		{
			string subscriptionBatchXmlBlob = base.Service.GetSubscriptionBatchXmlBlob(base.ActionParameters.SubscriptionID, base.ActionParameters.EventType, base.ActionParameters.MatchData, base.ActionParameters.Description, ParameterValueOrFieldReference.ThisArrayToXml(base.ActionParameters.Parameters), ExtensionSettings.ThisToXml(base.ActionParameters.ExtensionSettings), DataRetrievalPlan.ThisToXml(base.ActionParameters.DataSettings), base.ActionParameters.IsDataDriven.ToString());
			base.Service.Storage.ConnectionManager.VerifyConnection(true);
			byte[] array = CatalogEncryption.Instance.Encrypt(subscriptionBatchXmlBlob);
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetSubscriptionProperties, base.ActionParameters.SubscriptionID, "subscriptionID", null, null, null, null, false, array, null);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00042D2C File Offset: 0x00040F2C
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			string text = CatalogEncryption.Instance.DecryptToString(parameters.Content, null);
			XmlDocument xmlDocument = new XmlDocument();
			XmlUtil.SafeOpenXmlDocumentString(xmlDocument, text);
			base.ActionParameters.SubscriptionID = parameters.Item;
			XmlNode xmlNode = xmlDocument.DocumentElement.SelectSingleNode("ExtensionSettings");
			base.ActionParameters.ExtensionSettings = ExtensionSettings.XmlToThis(xmlNode.InnerText);
			base.ActionParameters.Description = xmlDocument.DocumentElement.SelectSingleNode("Description").InnerText;
			base.ActionParameters.EventType = xmlDocument.DocumentElement.SelectSingleNode("EventType").InnerText;
			base.ActionParameters.MatchData = xmlDocument.DocumentElement.SelectSingleNode("SubscriptionData").InnerText;
			XmlNode xmlNode2 = xmlDocument.DocumentElement.SelectSingleNode("Parameters");
			string text2 = ((xmlNode2.InnerText == string.Empty) ? null : xmlNode2.InnerText);
			base.ActionParameters.Parameters = ParameterValueOrFieldReference.XmlToThisArray(text2, false);
			XmlNode xmlNode3 = xmlDocument.DocumentElement.SelectSingleNode("DataSettings");
			string text3 = ((xmlNode3.InnerText == string.Empty) ? null : xmlNode3.InnerText);
			base.ActionParameters.DataSettings = DataRetrievalPlan.XmlToThis(text3);
			XmlNode xmlNode4 = xmlDocument.DocumentElement.SelectSingleNode("IsDataDriven");
			if (xmlNode4 != null && !string.IsNullOrEmpty(xmlNode4.InnerText))
			{
				base.ActionParameters.IsDataDriven = bool.Parse(xmlNode4.InnerText);
			}
			else
			{
				base.ActionParameters.IsDataDriven = base.ActionParameters.DataSettings != null;
			}
			base.BatchTraceInput();
			this.PerformActionNow();
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00042ED8 File Offset: 0x000410D8
		internal override void PerformActionNow()
		{
			base.Service.SubscriptionManager.SetSubscriptionProperties(Globals.ParseGuidParameter(base.ActionParameters.SubscriptionID, "subscriptionID"), base.ActionParameters.EventType, base.ActionParameters.MatchData, base.ActionParameters.ExtensionSettings, base.ActionParameters.Description, base.ActionParameters.Parameters, base.ActionParameters.DataSettings, base.ActionParameters.IsDataDriven);
		}
	}
}
