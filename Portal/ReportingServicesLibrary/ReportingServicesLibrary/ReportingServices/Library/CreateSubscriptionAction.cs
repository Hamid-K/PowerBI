using System;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000208 RID: 520
	internal sealed class CreateSubscriptionAction : RSSoapAction<CreateSubscriptionActionParameters>
	{
		// Token: 0x06001282 RID: 4738 RVA: 0x00042068 File Offset: 0x00040268
		public CreateSubscriptionAction(RSService service)
			: base("CreateSubscriptionAction", service)
		{
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00042078 File Offset: 0x00040278
		protected override void AddActionToBatch()
		{
			Guid guid = Guid.NewGuid();
			string subscriptionBatchXmlBlob = base.Service.GetSubscriptionBatchXmlBlob(guid.ToString(), base.ActionParameters.EventType, base.ActionParameters.MatchData, base.ActionParameters.Description, ParameterValueOrFieldReference.ThisArrayToXml(base.ActionParameters.Parameters), ExtensionSettings.ThisToXml(base.ActionParameters.ExtensionSettings), DataRetrievalPlan.ThisToXml(base.ActionParameters.DataSettings), base.ActionParameters.IsDataDriven.ToString());
			base.Service.Storage.ConnectionManager.VerifyConnection(true);
			byte[] array = CatalogEncryption.Instance.Encrypt(subscriptionBatchXmlBlob, null);
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.CreateSubscription, base.ActionParameters.Report, "report", null, null, null, null, false, array, null);
			base.ActionParameters.SubscriptionID = guid.ToString();
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00042180 File Offset: 0x00040380
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			string text = CatalogEncryption.Instance.DecryptToString(parameters.Content, null);
			XmlDocument xmlDocument = new XmlDocument();
			XmlUtil.SafeOpenXmlDocumentString(xmlDocument, text);
			base.ActionParameters.SubscriptionID = xmlDocument.DocumentElement.SelectSingleNode("id").InnerText;
			base.ActionParameters.Report = parameters.Item;
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

		// Token: 0x06001285 RID: 4741 RVA: 0x0004234C File Offset: 0x0004054C
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.Report, "report");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			catalogItem.LoadProperties();
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Report,
				ItemType.LinkedReport
			});
			BaseReportCatalogItem baseReportCatalogItem = (BaseReportCatalogItem)catalogItem;
			baseReportCatalogItem.ThrowIfNotSubscribableByProperties(base.ActionParameters.IsDataDriven);
			baseReportCatalogItem.ThrowIfNotGoodForUnattended(false);
			baseReportCatalogItem.ThrowIfNoAccess(ReportOperation.ExecuteAndView);
			if (base.ActionParameters.IsDataDriven)
			{
				baseReportCatalogItem.ThrowIfNoAccess(ReportOperation.CreateAnySubscription);
			}
			else
			{
				baseReportCatalogItem.ThrowIfNoRights(ReportOperation.CreateAnySubscription, ReportOperation.CreateSubscription);
			}
			base.ActionParameters.Parameters = base.Service.SubscriptionManager.ValidateSubscriptionParameters(catalogItemContext.ItemPath, base.ActionParameters.Parameters, JobType.UserJobType);
			Guid guid = base.Service.SubscriptionManager.CreateSubscription(new Guid(base.ActionParameters.SubscriptionID), catalogItemContext.ItemPath, baseReportCatalogItem.ItemID, base.ActionParameters.EventType, base.ActionParameters.MatchData, base.ActionParameters.ExtensionSettings, base.ActionParameters.Description, base.ActionParameters.Parameters, base.ActionParameters.DataSettings, baseReportCatalogItem.SecurityDescriptor);
			base.ActionParameters.SubscriptionID = guid.ToString();
		}
	}
}
