using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000250 RID: 592
	internal sealed class SystemResourceManager : ISystemResourceManager
	{
		// Token: 0x060015AA RID: 5546 RVA: 0x0005550C File Offset: 0x0005370C
		internal static void CheckMissingContentItems(IEnumerable<string> metadataContentItemPaths, IEnumerable<string> payloadContentItemPaths)
		{
			metadataContentItemPaths = metadataContentItemPaths ?? new string[0];
			payloadContentItemPaths = payloadContentItemPaths ?? new string[0];
			IEnumerable<string> enumerable = metadataContentItemPaths.Select((string x) => x.ToLowerInvariant().TrimStart(new char[] { '/' }).Trim());
			IEnumerable<string> enumerable2 = payloadContentItemPaths.Select((string x) => x.ToLowerInvariant().TrimStart(new char[] { '/' }).Trim());
			IEnumerable<string> enumerable3 = enumerable.Except(enumerable2);
			if (enumerable3.Any<string>())
			{
				throw new SystemResourcePackageReferencedItemMissingException(enumerable3.First<string>());
			}
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0005559C File Offset: 0x0005379C
		internal static SystemResource CreateFromSoapProperties(Property[] properties)
		{
			if (properties == null)
			{
				throw new ArgumentNullException(ErrorStringsWrapper.rsMissingParameter("properties"), "properties");
			}
			SystemResource systemResource = new SystemResource();
			Property property = properties.SingleOrDefault((Property x) => x.Name == "Resource.Name");
			if (property == null || string.IsNullOrEmpty(property.Value))
			{
				throw new ArgumentException(ErrorStringsWrapper.rsMissingRequiredPropertyForItemType("Resource.Name"), "Resource.Name");
			}
			systemResource.Name = property.Value;
			Property property2 = properties.SingleOrDefault((Property x) => x.Name == "Resource.Type");
			if (property2 == null || string.IsNullOrEmpty(property2.Value))
			{
				throw new ArgumentException(ErrorStringsWrapper.rsMissingRequiredPropertyForItemType("Resource.Type"), "Resource.Type");
			}
			systemResource.TypeName = property2.Value;
			Property property3 = properties.SingleOrDefault((Property x) => x.Name == "Resource.Version");
			if (property3 == null || string.IsNullOrEmpty(property3.Value))
			{
				throw new ArgumentException(ErrorStringsWrapper.rsMissingRequiredPropertyForItemType("Resource.Version"), "Resource.Version");
			}
			systemResource.Version = property3.Value;
			Property property4 = properties.SingleOrDefault((Property x) => x.Name == "Resource.PackageId");
			if (property4 == null || string.IsNullOrEmpty(property4.Value))
			{
				throw new ArgumentException(ErrorStringsWrapper.rsMissingRequiredPropertyForItemType("Resource.PackageId"), "Resource.PackageId");
			}
			systemResource.PackageId = new Guid(property4.Value);
			IEnumerable<Property> enumerable = properties.Where((Property x) => x.Name.StartsWith("Item."));
			if (!enumerable.Any<Property>())
			{
				throw new ArgumentException(ErrorStringsWrapper.rsMissingRequiredPropertyForItemType("Item."), "Item.");
			}
			foreach (Property property5 in enumerable)
			{
				if (string.IsNullOrEmpty(property5.Value))
				{
					throw new ArgumentException(ErrorStringsWrapper.rsMissingRequiredPropertyForItemType(property5.Name), property5.Name);
				}
				systemResource.Items.Add(property5.Name.Substring("Item.".Length), property5.Value);
			}
			return systemResource;
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x000557F8 File Offset: 0x000539F8
		internal static XDocument DeserializeMetadata(Func<Stream> metadataStream)
		{
			if (metadataStream == null)
			{
				throw new ArgumentNullException(ErrorStringsWrapper.rsMissingParameter("metadataStream"), "metadataStream");
			}
			Stream stream = null;
			XDocument xdocument;
			try
			{
				stream = metadataStream();
				using (XmlReader xmlReader = XmlReader.Create(stream))
				{
					xdocument = XDocument.Load(xmlReader);
				}
			}
			finally
			{
				if (stream != null)
				{
					stream.Dispose();
				}
			}
			return xdocument;
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x00055868 File Offset: 0x00053A68
		internal static XmlSchemaSet GetPackageMetatadataSchemaSet()
		{
			Type typeFromHandle = typeof(SystemResource);
			Assembly assembly = typeFromHandle.Assembly;
			XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
			using (Stream manifestResourceStream = assembly.GetManifestResourceStream(typeFromHandle.Namespace + ".SystemResource.SystemResourcePackageMetadata.xsd"))
			{
				using (XmlReader xmlReader = XmlReader.Create(manifestResourceStream))
				{
					xmlSchemaSet.Add("http://schemas.microsoft.com/sqlserver/reporting/2016/01/systemresourcepackagemetadata", xmlReader);
				}
			}
			return xmlSchemaSet;
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x000558EC File Offset: 0x00053AEC
		internal static string GetPackagePath(string typeName)
		{
			return string.Join("/", new string[] { "/68f0607b-9378-4bbb-9e70-4da3d7d66838", typeName });
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0005590C File Offset: 0x00053B0C
		internal static bool IsSystemResourcePath(string path)
		{
			return !string.IsNullOrEmpty(path) && path.StartsWith("/68f0607b-9378-4bbb-9e70-4da3d7d66838", StringComparison.OrdinalIgnoreCase) && (path.Length == "/68f0607b-9378-4bbb-9e70-4da3d7d66838".Length || path["/68f0607b-9378-4bbb-9e70-4da3d7d66838".Length] == '/');
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x0005595C File Offset: 0x00053B5C
		internal static void ParseMetadata(XDocument metadata, out string typeName, out string version, out string name, out IDictionary<string, SystemResourceContentItem> contents)
		{
			if (metadata == null)
			{
				throw new ArgumentNullException(ErrorStringsWrapper.rsMissingParameter("metadata"), "metadata");
			}
			XNamespace.Get("http://schemas.microsoft.com/sqlserver/reporting/2016/01/systemresourcepackagemetadata");
			XElement root = metadata.Root;
			typeName = root.Attribute(XName.Get("type")).Value;
			version = root.Attribute(XName.Get("version")).Value;
			name = root.Attribute(XName.Get("name")).Value;
			IEnumerable<XElement> enumerable = root.Element(XName.Get("Contents", "http://schemas.microsoft.com/sqlserver/reporting/2016/01/systemresourcepackagemetadata")).Elements(XName.Get("Item", "http://schemas.microsoft.com/sqlserver/reporting/2016/01/systemresourcepackagemetadata"));
			contents = enumerable.Select(delegate(XElement x)
			{
				string value = x.Attribute(XName.Get("key")).Value;
				string value2 = x.Attribute(XName.Get("path")).Value;
				string text = ((x.Attributes(XName.Get("contentType")).Count<XAttribute>() == 1) ? x.Attribute(XName.Get("contentType")).Value : null);
				return new SystemResourceContentItem
				{
					Key = value,
					Path = value2,
					ContentType = text
				};
			}).ToDictionary((SystemResourceContentItem x) => x.Key, (SystemResourceContentItem x) => x);
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x00055A70 File Offset: 0x00053C70
		internal static void ValidateMetadata(XDocument metadata)
		{
			if (metadata == null)
			{
				throw new ArgumentNullException(ErrorStringsWrapper.rsMissingParameter("metadata"), "metadata");
			}
			XmlSchemaSet packageMetatadataSchemaSet = SystemResourceManager.GetPackageMetatadataSchemaSet();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			xmlReaderSettings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
			xmlReaderSettings.Schemas = packageMetatadataSchemaSet;
			xmlReaderSettings.ValidationEventHandler += delegate(object o, ValidationEventArgs e)
			{
				throw new SystemResourcePackageMetadataInvalidException(e.Exception);
			};
			using (XmlReader xmlReader = metadata.CreateReader())
			{
				using (XmlReader xmlReader2 = XmlReader.Create(xmlReader, xmlReaderSettings))
				{
					while (xmlReader2.Read())
					{
					}
				}
			}
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00055B2C File Offset: 0x00053D2C
		public SystemResourceManager(RSService rsService)
		{
			if (rsService == null)
			{
				throw new ArgumentNullException(ErrorStringsWrapper.rsMissingParameter("rsService"), "rsService");
			}
			this._rsService = rsService;
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x00055B54 File Offset: 0x00053D54
		public SystemResource Install(Func<Stream> metadataStream, IDictionary<string, Func<Stream>> contentStreams, byte[] packageBytes, string packageName, string typeName, Func<string, ISystemResourcePackageContentValidator> validator, Func<string, string> contentTypeMapper)
		{
			if (metadataStream == null)
			{
				throw new ArgumentNullException(ErrorStringsWrapper.rsMissingParameter("metadataStream"), "metadataStream");
			}
			if (contentStreams == null)
			{
				throw new ArgumentNullException(ErrorStringsWrapper.rsMissingParameter("contentStreams"), "contentStreams");
			}
			if (contentStreams.Any((KeyValuePair<string, Func<Stream>> x) => x.Value == null))
			{
				throw new ArgumentException(ErrorStringsWrapper.rsInvalidParameter("contentStreams"), "contentStreams");
			}
			if (packageBytes == null)
			{
				throw new ArgumentNullException(ErrorStringsWrapper.rsMissingParameter("packageBytes"), "packageBytes");
			}
			if (packageBytes.Length == 0)
			{
				throw new ArgumentException(ErrorStringsWrapper.rsInvalidParameter("packageBytes"), "packageBytes");
			}
			if (string.IsNullOrEmpty(packageName))
			{
				throw new ArgumentNullException(ErrorStringsWrapper.rsMissingParameter("packageName"), "packageName");
			}
			if (contentTypeMapper == null)
			{
				throw new ArgumentNullException(ErrorStringsWrapper.rsMissingParameter("contentTypeMapper"), "contentTypeMapper");
			}
			if (string.IsNullOrEmpty(typeName))
			{
				throw new ArgumentNullException(ErrorStringsWrapper.rsMissingParameter("typeName"), "typeName");
			}
			XDocument xdocument = SystemResourceManager.DeserializeMetadata(metadataStream);
			SystemResourceManager.ValidateMetadata(xdocument);
			string text;
			string text2;
			string text3;
			IDictionary<string, SystemResourceContentItem> dictionary;
			try
			{
				SystemResourceManager.ParseMetadata(xdocument, out text, out text2, out text3, out dictionary);
			}
			catch (Exception ex)
			{
				throw new SystemResourcePackageMetadataInvalidException(ex);
			}
			if (!string.Equals(typeName, text, StringComparison.OrdinalIgnoreCase))
			{
				throw new SystemResourcePackageWrongTypeException(typeName);
			}
			SystemResourceManager.CheckMissingContentItems(dictionary.Select((KeyValuePair<string, SystemResourceContentItem> x) => x.Value.Path), contentStreams.Keys);
			if (validator != null)
			{
				ISystemResourcePackageContentValidator systemResourcePackageContentValidator = validator(text);
				if (systemResourcePackageContentValidator != null && !systemResourcePackageContentValidator.Validate(dictionary.Values))
				{
					throw new SystemResourcePackageValidationFailedException();
				}
			}
			List<Property> list = new List<Property>
			{
				new Property
				{
					Name = "Resource.Type",
					Value = text
				},
				new Property
				{
					Name = "Resource.Name",
					Value = text3
				},
				new Property
				{
					Name = "Resource.Version",
					Value = text2
				}
			};
			SystemResource systemResource = new SystemResource
			{
				Name = text3,
				Version = text2,
				TypeName = text
			};
			using (RSServiceStorageAccess rsserviceStorageAccess = new RSServiceStorageAccess(this._rsService))
			{
				this.TryDelete(text, false);
				CatalogItem catalogItem = this.CreateFolder("/68f0607b-9378-4bbb-9e70-4da3d7d66838", text);
				string itemPathAsString = catalogItem.ItemContext.ItemPathAsString;
				systemResource.Id = catalogItem.ItemID;
				Guid guid;
				using (MemoryStream memoryStream = new MemoryStream(packageBytes))
				{
					string text4;
					this.CreateResource(itemPathAsString, packageName, memoryStream, "application/octet-stream", out guid, out text4);
				}
				list.Add(new Property
				{
					Name = "Resource.PackageId",
					Value = guid.ToString()
				});
				systemResource.PackageId = guid;
				CatalogItem catalogItem2 = this.CreateFolder(itemPathAsString, "fbac82c8-9bad-4dba-929f-c04e7ca4111f");
				foreach (KeyValuePair<string, SystemResourceContentItem> keyValuePair in dictionary)
				{
					string text5 = keyValuePair.Value.ContentType ?? contentTypeMapper(keyValuePair.Value.Path);
					Guid guid2;
					Property property = this.AddContentItem(contentStreams[keyValuePair.Value.Path], keyValuePair.Key, text5, catalogItem2.ItemContext.ItemPathAsString, out guid2);
					list.Add(property);
					systemResource.Items.Add(keyValuePair.Key, guid2.ToString());
				}
				this._rsService.SetPropertiesAction.SetProperties(catalogItem, new ItemProperties(list.ToArray(), ItemType.Folder));
				rsserviceStorageAccess.Commit();
			}
			return systemResource;
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00055F54 File Offset: 0x00054154
		public IEnumerable<SystemResource> LoadAll()
		{
			List<SystemResource> list = new List<SystemResource>();
			ListChildrenAction listChildrenAction = this._rsService.ListChildrenAction;
			listChildrenAction.ActionParameters.ItemPath = "/68f0607b-9378-4bbb-9e70-4da3d7d66838";
			listChildrenAction.Execute();
			foreach (CatalogItemDescriptor catalogItemDescriptor in listChildrenAction.ActionParameters.Children)
			{
				SystemResource systemResource;
				if (this.TryLoadByTypeName(catalogItemDescriptor.Name, out systemResource))
				{
					list.Add(systemResource);
				}
			}
			return list;
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00055FE4 File Offset: 0x000541E4
		public bool TryDelete(string typeName)
		{
			return this.TryDelete(typeName, true);
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00055FF0 File Offset: 0x000541F0
		public bool TryLoadByTypeName(string typeName, out SystemResource systemResource)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				throw new ArgumentNullException(ErrorStringsWrapper.rsMissingParameter("typeName"), "typeName");
			}
			string packagePath = SystemResourceManager.GetPackagePath(typeName);
			bool flag;
			try
			{
				Guid guid;
				using (new RSServiceStorageAccess(this._rsService))
				{
					ItemType itemType;
					byte[] array;
					if (!this._rsService.Storage.ObjectExists(new ExternalItemPath(packagePath), out itemType, out guid, out array))
					{
						systemResource = null;
						return false;
					}
				}
				GetPropertiesAction getPropertiesAction = this._rsService.GetPropertiesAction;
				getPropertiesAction.ActionParameters.ItemPath = packagePath;
				getPropertiesAction.Execute();
				systemResource = SystemResourceManager.CreateFromSoapProperties(getPropertiesAction.ActionParameters.PropertyValues);
				systemResource.Id = guid;
				flag = true;
			}
			catch
			{
				systemResource = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x000560CC File Offset: 0x000542CC
		private Property AddContentItem(Func<Stream> contentStream, string key, string contentType, string parentPath, out Guid id)
		{
			Stream stream = null;
			Guid guid;
			try
			{
				stream = contentStream();
				string text;
				this.CreateResource(parentPath, key, stream, contentType, out guid, out text);
			}
			finally
			{
				if (stream != null)
				{
					stream.Dispose();
				}
			}
			id = guid;
			return new Property
			{
				Name = "Item." + key,
				Value = guid.ToString()
			};
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00056140 File Offset: 0x00054340
		private CatalogItem CreateFolder(string parentPath, string itemName)
		{
			CreateSystemResourceFolderAction createSystemResourceFolderAction = new CreateSystemResourceFolderAction(this._rsService);
			createSystemResourceFolderAction.ActionParameters.ParentPath = parentPath;
			createSystemResourceFolderAction.ActionParameters.ItemName = itemName;
			createSystemResourceFolderAction.PerformActionNow();
			return createSystemResourceFolderAction.ActionParameters.ItemInfo;
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00056178 File Offset: 0x00054378
		private void CreateResource(string parentPath, string itemName, Stream stream, string contentType, out Guid itemId, out string itemPath)
		{
			CreateSystemResourceAction createSystemResourceAction = new CreateSystemResourceAction(this._rsService);
			createSystemResourceAction.ActionParameters.ParentPath = parentPath;
			createSystemResourceAction.ActionParameters.ItemName = itemName;
			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
					{
						byte[] array = new byte[4096];
						int num;
						while ((num = binaryReader.Read(array, 0, array.Length)) > 0)
						{
							binaryWriter.Write(array, 0, num);
						}
						createSystemResourceAction.ActionParameters.Content = memoryStream.ToArray();
					}
				}
			}
			createSystemResourceAction.ActionParameters.MimeType = contentType;
			createSystemResourceAction.PerformActionNow();
			itemPath = createSystemResourceAction.ActionParameters.ItemInfo.ItemContext.ItemPathAsString;
			itemId = createSystemResourceAction.ActionParameters.ItemInfo.ItemID;
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00056288 File Offset: 0x00054488
		private bool TryDelete(string typeName, bool execute)
		{
			DeleteSystemResourceFolderAction deleteSystemResourceFolderAction = new DeleteSystemResourceFolderAction(this._rsService);
			deleteSystemResourceFolderAction.ActionParameters.ItemPath = SystemResourceManager.GetPackagePath(typeName);
			bool flag;
			try
			{
				if (execute)
				{
					deleteSystemResourceFolderAction.Execute();
				}
				else
				{
					deleteSystemResourceFolderAction.PerformActionNow();
				}
				flag = true;
			}
			catch (ItemNotFoundException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x000562E0 File Offset: 0x000544E0
		public bool TryLoadContentItem(string typeName, string key, out byte[] bytes)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				throw new ArgumentNullException(ErrorStringsWrapper.rsMissingParameter("typeName"), "typeName");
			}
			bytes = null;
			SystemResource systemResource;
			if (!this.TryLoadByTypeName(typeName, out systemResource))
			{
				return false;
			}
			Guid packageId;
			if (string.IsNullOrEmpty(key))
			{
				packageId = systemResource.PackageId;
			}
			else
			{
				if (!systemResource.Items.Keys.Contains(key.ToLowerInvariant()))
				{
					return false;
				}
				packageId = new Guid(systemResource.Items[key.ToLowerInvariant()]);
			}
			using (new RSServiceStorageAccess(this._rsService))
			{
				CatalogItemPath pathById = this._rsService.Storage.GetPathById(packageId);
				if (pathById != null)
				{
					GetResourceContentsAction getResourceContentsAction = this._rsService.GetResourceContentsAction;
					getResourceContentsAction.ActionParameters.ItemPath = pathById.Value;
					getResourceContentsAction.PerformActionNow();
					bytes = getResourceContentsAction.ActionParameters.Content;
					return true;
				}
			}
			return false;
		}

		// Token: 0x040007F5 RID: 2037
		private readonly RSService _rsService;
	}
}
