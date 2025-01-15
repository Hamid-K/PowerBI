using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000B4 RID: 180
	internal sealed class GetPropertiesAction : RSSoapAction<GetPropertiesActionParameters>
	{
		// Token: 0x0600081D RID: 2077 RVA: 0x00021380 File Offset: 0x0001F580
		public GetPropertiesAction(RSService service)
			: base("GetPropertiesAction", service)
		{
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00021390 File Offset: 0x0001F590
		internal override void PerformActionNow()
		{
			using (MonitoredScope.New("GetPropertiesAction.PerformActionNow"))
			{
				string itemPath = base.ActionParameters.ItemPath;
				CatalogItemContext catalogItemContext;
				if (base.ActionParameters.ItemNamespace == ItemNamespaceEnum.GUIDBased)
				{
					Guid guid = Globals.ParseGuidParameter(itemPath, "Item");
					CatalogItemPath pathById = base.Service.Storage.GetPathById(guid);
					if (pathById == null)
					{
						throw new ItemNotFoundException(base.ActionParameters.ItemPath);
					}
					catalogItemContext = new CatalogItemContext(base.Service, base.Service.CatalogToExternal(pathById), "Item");
				}
				else
				{
					ItemPathOptions itemPathOptions = ItemPathOptions.Default;
					if (base.ActionParameters.AllowEditSessionSyntax)
					{
						itemPathOptions |= ItemPathOptions.AllowEditSessionSyntax;
					}
					catalogItemContext = new CatalogItemContext(base.Service);
					if (!catalogItemContext.SetPath(itemPath, itemPathOptions))
					{
						throw new InvalidItemPathException(itemPath);
					}
				}
				CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
				catalogItem.ThrowIfNoAccess(CommonOperation.ReadProperties);
				catalogItem.LoadStoredAndDerivedProperties();
				ItemProperties properties = catalogItem.Properties;
				if (catalogItemContext.IsRoot)
				{
					properties.Reserved = bool.TrueString;
					properties.Name = CatalogItemNameUtility.PathSeparatorString;
					properties.Path = CatalogItemNameUtility.PathSeparatorString;
				}
				else
				{
					properties.Path = catalogItemContext.ItemPath.Value;
				}
				if (base.Service.MyReportsEnabled)
				{
					string text = base.Service.PathToExternal(catalogItemContext.ItemPath.Value);
					properties.VirtualPath = text;
					if (ItemPathBase.CatalogCompare(catalogItemContext.ItemPath, Global.AllUsersFolderPath) == 0 || Localization.CatalogCultureCompare(text, Global.VirtualMyReportsPath) == 0)
					{
						properties.Reserved = bool.TrueString;
					}
				}
				base.ActionParameters.PropertyValues = properties.FilterProperties(base.ActionParameters.RequestedProperties);
			}
		}
	}
}
