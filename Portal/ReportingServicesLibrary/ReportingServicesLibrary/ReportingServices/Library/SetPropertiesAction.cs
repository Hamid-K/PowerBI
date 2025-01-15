using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000B6 RID: 182
	internal sealed class SetPropertiesAction : RSSoapAction<SetPropertiesActionParameters>
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x000215CE File Offset: 0x0001F7CE
		public SetPropertiesAction(RSService service)
			: base("SetPropertiesAction", service)
		{
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x000215DC File Offset: 0x0001F7DC
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetProperties, base.ActionParameters.ItemPath, "Item", null, null, null, null, false, null, Property.ThisArrayToXml(base.ActionParameters.Properties));
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00021631 File Offset: 0x0001F831
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemPath = parameters.Item;
			base.ActionParameters.Properties = Property.XmlToThisArray(parameters.Properties);
			this.PerformActionNow();
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00021660 File Offset: 0x0001F860
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Item");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			if (!base.ActionParameters.IgnoreSecCheck)
			{
				catalogItem.ThrowIfNoAccess(CommonOperation.UpdateProperties);
			}
			ItemProperties itemProperties = new ItemProperties(base.ActionParameters.Properties, catalogItem.ThisItemType);
			this.SetProperties(catalogItem, itemProperties);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x000216D0 File Offset: 0x0001F8D0
		internal void SetProperties(CatalogItem catalogItem, ItemProperties suppliedProperties)
		{
			SetPropertiesAction.ValidateRdceProperty(catalogItem, suppliedProperties);
			suppliedProperties.EnsurePropertiesWritable();
			catalogItem.LoadProperties();
			catalogItem.CombineProperties(suppliedProperties);
			if (catalogItem.ModificationDate == DateTime.MinValue)
			{
				catalogItem.ModificationDate = DateTime.Now;
			}
			catalogItem.SaveProperties();
			base.ActionParameters.ModificationDate = catalogItem.ModificationDate;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0002172C File Offset: 0x0001F92C
		private static void ValidateRdceProperty(CatalogItem catalogItem, ItemProperties suppliedProperties)
		{
			if (string.IsNullOrEmpty(suppliedProperties.Rdce))
			{
				return;
			}
			if (!Globals.Configuration.IsRdceEnabled)
			{
				throw new RdceInvalidConfigurationException();
			}
			if (Globals.Configuration.Extensions.ReportDefinitionCustomization == null || string.IsNullOrEmpty(Globals.Configuration.Extensions.ReportDefinitionCustomization.Name))
			{
				throw new RdceInvalidConfigurationException();
			}
			if (string.Compare(suppliedProperties.Rdce, Globals.Configuration.Extensions.ReportDefinitionCustomization.Name, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new RdceMismatchException(suppliedProperties.Rdce, Globals.Configuration.Extensions.ReportDefinitionCustomization.Name);
			}
			catalogItem.ThrowIfRdceNotSupported();
		}
	}
}
