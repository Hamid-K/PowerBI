using System;
using System.Data;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library;

namespace Microsoft.ReportingServices.Portal.Services.Extensions
{
	// Token: 0x02000064 RID: 100
	internal static class RsServiceExtensions
	{
		// Token: 0x0600030C RID: 780 RVA: 0x00014A30 File Offset: 0x00012C30
		internal static void ExecuteStorageAction(this RSService rsService, Action action)
		{
			rsService.ExecuteStorageAction(action, ConnectionManager.DefaultTransactionType, ConnectionManager.DefaultIsolationLevel);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00014A43 File Offset: 0x00012C43
		internal static void ExecuteStorageAction(this RSService rsService, Action action, ConnectionTransactionType transactionType)
		{
			rsService.ExecuteStorageAction(action, transactionType, ConnectionManager.DefaultIsolationLevel);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00014A54 File Offset: 0x00012C54
		internal static void ExecuteStorageAction(this RSService rsService, Action action, ConnectionTransactionType transactionType, IsolationLevel isolationLevel)
		{
			try
			{
				rsService.WillDisconnectStorage();
				rsService.SetDatabaseConnectionSettings(transactionType, isolationLevel);
				action();
			}
			catch (ResourceFileFormatNotAllowedException)
			{
				rsService.AbortTransaction();
				throw;
			}
			catch (ResourceMimeTypeNotAllowedException)
			{
				rsService.AbortTransaction();
				throw;
			}
			catch (ExcelFileExtensionChangeNotAllowedException)
			{
				rsService.AbortTransaction();
				throw;
			}
			catch (RSException ex)
			{
				rsService.AbortTransaction();
				if (ex is ReportServerStorageException && RSTrace.CatalogTrace.TraceError)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Error, ex.ToString());
				}
				throw;
			}
			catch (Exception ex2)
			{
				rsService.AbortTransaction();
				throw new InternalCatalogException(ex2, null);
			}
			finally
			{
				rsService.DisconnectStorage();
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00014B20 File Offset: 0x00012D20
		internal static void ResolveCatalogItem(this RSService rsService, Guid id, string path, out Guid actualId, out string actualPath)
		{
			if (!Guid.Empty.Equals(id))
			{
				CatalogItemPath pathById = rsService.Storage.GetPathById(id);
				if (pathById != null)
				{
					actualId = id;
					actualPath = pathById.Value;
					return;
				}
			}
			ExternalItemPath externalItemPath = rsService.CatalogToExternal(path);
			ItemType itemType;
			Guid guid;
			byte[] array;
			if (rsService.Storage.ObjectExists(externalItemPath, out itemType, out guid, out array))
			{
				actualId = guid;
				actualPath = externalItemPath.Value;
				return;
			}
			throw new ItemNotFoundException(path ?? id.ToString());
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00014BA8 File Offset: 0x00012DA8
		internal static void ResolveCatalogItem(this RSService rsService, Guid id, string path, bool throwIfNotExists, out Guid actualId, out string actualPath, out ItemType actualType)
		{
			if (!Guid.Empty.Equals(id))
			{
				CatalogItemPath pathById = rsService.Storage.GetPathById(id);
				if (pathById != null)
				{
					path = pathById.Value;
				}
			}
			ExternalItemPath externalItemPath = rsService.CatalogToExternal(path);
			ItemType itemType;
			Guid guid;
			byte[] array;
			bool flag = rsService.Storage.ObjectExists(externalItemPath, out itemType, out guid, out array);
			actualId = guid;
			actualPath = externalItemPath.Value;
			actualType = itemType;
			if (throwIfNotExists && !flag)
			{
				throw new ItemNotFoundException(path ?? id.ToString());
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00014C34 File Offset: 0x00012E34
		internal static void ResolveCatalogItem(this RSService rsService, Guid id, string path, ItemType expectedType, bool throwIfNotExists, out Guid actualId, out string actualPath)
		{
			Guid guid;
			string text;
			ItemType itemType;
			rsService.ResolveCatalogItem(id, path, throwIfNotExists, out guid, out text, out itemType);
			if (throwIfNotExists && itemType != expectedType)
			{
				throw new WrongItemTypeException(text);
			}
			actualId = guid;
			actualPath = text;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00014C6C File Offset: 0x00012E6C
		internal static void EnsureCatalogItemCanBeCreated(this RSService rsService, string parentPath, ItemType parentType, string itemName, ItemType itemType)
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(rsService, parentPath, "parent");
			CatalogItem catalogItem = rsService.CatalogItemFactory.GetCatalogItem(catalogItemContext);
			CatalogItem catalogItem2 = catalogItem;
			ItemType[] array = new ItemType[]
			{
				ItemType.Unknown,
				ItemType.Report,
				ItemType.LinkedReport,
				ItemType.MobileReport,
				ItemType.PowerBIReport,
				ItemType.Kpi,
				ItemType.ExcelWorkbook
			};
			array[0] = parentType;
			catalogItem2.ThrowIfWrongItemType(array);
			string text = CatalogItemNameUtility.BuildChildPath(catalogItemContext.ItemPath.Value, CatalogItemNameUtility.ValidateAndTrimItemName(itemName, "name"));
			CatalogItemContext catalogItemContext2 = new CatalogItemContext(rsService, text, "ItemPath");
			rsService.EnsureAllowedAsSubitem(catalogItem.ThisItemType, itemType, catalogItem.SecurityDescriptor, catalogItem.ItemContext.ItemPath, catalogItemContext2.ItemName);
			ItemType itemType2;
			if (rsService.Storage.ObjectExists(catalogItemContext2.ItemPath, out itemType2))
			{
				throw new ItemAlreadyExistsException(text);
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00014D18 File Offset: 0x00012F18
		internal static void EnsureCatalogItemCanBeEdited(this RSService rsService, string origParentPath, string origItemName, ItemType newItemType, string newParentPath, string newItemName)
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(rsService, origParentPath, "parent");
			rsService.CatalogItemFactory.GetCatalogItem(catalogItemContext);
			string text = CatalogItemNameUtility.BuildChildPath(catalogItemContext.ItemPath.Value, CatalogItemNameUtility.ValidateAndTrimItemName(origItemName, "name"));
			CatalogItemContext catalogItemContext2 = new CatalogItemContext(rsService, text, "ItemPath");
			CatalogItem catalogItem = rsService.CatalogItemFactory.GetCatalogItem(catalogItemContext2);
			string text2 = CatalogItemNameUtility.BuildChildPath(new CatalogItemContext(rsService, newParentPath, "parent").ItemPath.Value, CatalogItemNameUtility.ValidateAndTrimItemName(newItemName, "name"));
			if (catalogItem.ThisItemType != newItemType)
			{
				throw new WrongItemTypeException(text);
			}
			CatalogItemContext catalogItemContext3 = new CatalogItemContext(rsService, text2, "ItemPath");
			ItemType itemType;
			bool flag = rsService.Storage.ObjectExists(catalogItemContext3.ItemPath, out itemType);
			if (string.Compare(text, text2, StringComparison.OrdinalIgnoreCase) != 0 && flag)
			{
				throw new ItemAlreadyExistsException(text2);
			}
			rsService.EnsureAllowedToEditItem(newItemType, catalogItem.SecurityDescriptor, catalogItem.ItemContext.ItemPath, catalogItemContext2.ItemName);
		}
	}
}
