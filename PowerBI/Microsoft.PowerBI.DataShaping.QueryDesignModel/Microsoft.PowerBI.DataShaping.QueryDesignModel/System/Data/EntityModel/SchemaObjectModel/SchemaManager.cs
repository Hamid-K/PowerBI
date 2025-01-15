using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using Microsoft.Data.Common;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000051 RID: 81
	[DebuggerDisplay("DataModel={DataModel}")]
	internal class SchemaManager
	{
		// Token: 0x06000845 RID: 2117 RVA: 0x0001147C File Offset: 0x0000F67C
		private SchemaManager(SchemaDataModelOption dataModel, AttributeValueNotification providerNotification, AttributeValueNotification providerManifestTokenNotification, ProviderManifestNeeded providerManifestNeeded)
		{
			this._dataModel = dataModel;
			this._providerNotification = providerNotification;
			this._providerManifestTokenNotification = providerManifestTokenNotification;
			this._providerManifestNeeded = providerManifestNeeded;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000114BC File Offset: 0x0000F6BC
		public static IList<EdmSchemaError> LoadProviderManifest(XmlReader xmlReader, string location, bool checkForSystemNamespace, out Schema schema)
		{
			IList<Schema> list = new List<Schema>(1);
			DbProviderManifest dbProviderManifest = (checkForSystemNamespace ? EdmProviderManifest.Instance : null);
			IList<EdmSchemaError> list2 = SchemaManager.ParseAndValidate(new XmlReader[] { xmlReader }, new string[] { location }, SchemaDataModelOption.ProviderManifestModel, dbProviderManifest, out list);
			if (list.Count != 0)
			{
				schema = list[0];
				return list2;
			}
			schema = null;
			return list2;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001150E File Offset: 0x0000F70E
		public static void NoOpAttributeValueNotification(string attributeValue, Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
		{
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00011510 File Offset: 0x0000F710
		public static IList<EdmSchemaError> ParseAndValidate(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> sourceFilePaths, SchemaDataModelOption dataModel, DbProviderManifest providerManifest, out IList<Schema> schemaCollection)
		{
			AttributeValueNotification attributeValueNotification;
			if ((attributeValueNotification = SchemaManager.<>O.<0>__NoOpAttributeValueNotification) == null)
			{
				attributeValueNotification = (SchemaManager.<>O.<0>__NoOpAttributeValueNotification = new AttributeValueNotification(SchemaManager.NoOpAttributeValueNotification));
			}
			AttributeValueNotification attributeValueNotification2;
			if ((attributeValueNotification2 = SchemaManager.<>O.<0>__NoOpAttributeValueNotification) == null)
			{
				attributeValueNotification2 = (SchemaManager.<>O.<0>__NoOpAttributeValueNotification = new AttributeValueNotification(SchemaManager.NoOpAttributeValueNotification));
			}
			return SchemaManager.ParseAndValidate(xmlReaders, sourceFilePaths, dataModel, attributeValueNotification, attributeValueNotification2, delegate(Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
			{
				if (providerManifest != null)
				{
					return providerManifest;
				}
				return MetadataItem.EdmProviderManifest;
			}, out schemaCollection);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00011578 File Offset: 0x0000F778
		public static IList<EdmSchemaError> ParseAndValidate(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> sourceFilePaths, SchemaDataModelOption dataModel, AttributeValueNotification providerNotification, AttributeValueNotification providerManifestTokenNotification, ProviderManifestNeeded providerManifestNeeded, out IList<Schema> schemaCollection)
		{
			SchemaManager schemaManager = new SchemaManager(dataModel, providerNotification, providerManifestTokenNotification, providerManifestNeeded);
			List<EdmSchemaError> list = new List<EdmSchemaError>();
			schemaCollection = new List<Schema>();
			bool flag = false;
			List<string> list2;
			if (sourceFilePaths != null)
			{
				list2 = new List<string>(sourceFilePaths);
			}
			else
			{
				list2 = new List<string>();
			}
			int num = 0;
			foreach (XmlReader xmlReader in xmlReaders)
			{
				string text = null;
				if (list2.Count <= num)
				{
					SchemaManager.TryGetBaseUri(xmlReader, out text);
				}
				else
				{
					text = list2[num];
				}
				Schema schema = new Schema(schemaManager);
				IList<EdmSchemaError> list3 = schema.Parse(xmlReader, text);
				SchemaManager.CheckIsSameVersion(schema, schemaCollection, list);
				if (SchemaManager.UpdateErrorCollectionAndCheckForMaxErrors(list, list3, ref flag))
				{
					return list;
				}
				if (!flag)
				{
					schemaCollection.Add(schema);
					schemaManager.AddSchema(schema);
					double schemaVersion = schema.SchemaVersion;
				}
				num++;
			}
			if (!flag)
			{
				foreach (Schema schema2 in schemaCollection)
				{
					if (SchemaManager.UpdateErrorCollectionAndCheckForMaxErrors(list, schema2.Resolve(), ref flag))
					{
						return list;
					}
				}
				if (!flag)
				{
					foreach (Schema schema3 in schemaCollection)
					{
						if (SchemaManager.UpdateErrorCollectionAndCheckForMaxErrors(list, schema3.ValidateSchema(), ref flag))
						{
							return list;
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001171C File Offset: 0x0000F91C
		private static bool CheckIsSameVersion(Schema schemaToBeAdded, IEnumerable<Schema> schemaCollection, List<EdmSchemaError> errorCollection)
		{
			if (schemaToBeAdded.SchemaVersion != 0.0 && schemaCollection.Count<Schema>() > 0 && schemaCollection.Any((Schema s) => s.SchemaVersion != 0.0 && s.SchemaVersion != schemaToBeAdded.SchemaVersion))
			{
				errorCollection.Add(new EdmSchemaError(Strings.CannotLoadDifferentVersionOfSchemaInTheSameItemCollection, 194, EdmSchemaErrorSeverity.Error));
			}
			return true;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00011780 File Offset: 0x0000F980
		public void AddSchema(Schema schema)
		{
			if (this._namespaceLookUpTable.Count == 0 && schema.DataModel != SchemaDataModelOption.ProviderManifestModel && this.PrimitiveSchema.Namespace != null)
			{
				this._namespaceLookUpTable.Add(this.PrimitiveSchema.Namespace);
			}
			this._namespaceLookUpTable.Add(schema.Namespace);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x000117DC File Offset: 0x0000F9DC
		public bool TryResolveType(string namespaceName, string typeName, out SchemaType schemaType)
		{
			string text = (string.IsNullOrEmpty(namespaceName) ? typeName : (namespaceName + "." + typeName));
			schemaType = this.SchemaTypes.LookUpEquivalentKey(text);
			return schemaType != null;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00011816 File Offset: 0x0000FA16
		public bool IsValidNamespaceName(string namespaceName)
		{
			return this._namespaceLookUpTable.Contains(namespaceName);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00011824 File Offset: 0x0000FA24
		internal static bool TryGetBaseUri(XmlReader xmlReader, out string location)
		{
			string baseURI = xmlReader.BaseURI;
			Uri uri = null;
			if (!string.IsNullOrEmpty(baseURI) && Uri.TryCreate(baseURI, UriKind.Absolute, out uri) && uri.Scheme == "file")
			{
				location = Helper.GetFileNameFromUri(uri);
				return true;
			}
			location = null;
			return false;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00011870 File Offset: 0x0000FA70
		private static bool UpdateErrorCollectionAndCheckForMaxErrors(List<EdmSchemaError> errorCollection, IList<EdmSchemaError> newErrors, ref bool errorEncountered)
		{
			if (!errorEncountered && !MetadataHelper.CheckIfAllErrorsAreWarnings(newErrors))
			{
				errorEncountered = true;
			}
			errorCollection.AddRange(newErrors);
			if (errorEncountered)
			{
				if (errorCollection.Where((EdmSchemaError e) => e.Severity == EdmSchemaErrorSeverity.Error).Count<EdmSchemaError>() > 100)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x000118C8 File Offset: 0x0000FAC8
		internal SchemaElementLookUpTable<SchemaType> SchemaTypes
		{
			get
			{
				return this._schemaTypes;
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000118D0 File Offset: 0x0000FAD0
		internal DbProviderManifest GetProviderManifest(Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
		{
			if (this._providerManifest == null)
			{
				this._providerManifest = this._providerManifestNeeded(addError);
			}
			return this._providerManifest;
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x000118F2 File Offset: 0x0000FAF2
		internal SchemaDataModelOption DataModel
		{
			get
			{
				return this._dataModel;
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000118FA File Offset: 0x0000FAFA
		internal void EnsurePrimitiveSchemaIsLoaded()
		{
			if (this._primitiveSchema == null)
			{
				this._primitiveSchema = new PrimitiveSchema(this);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x00011910 File Offset: 0x0000FB10
		internal PrimitiveSchema PrimitiveSchema
		{
			get
			{
				return this._primitiveSchema;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00011918 File Offset: 0x0000FB18
		internal AttributeValueNotification ProviderNotification
		{
			get
			{
				return this._providerNotification;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00011920 File Offset: 0x0000FB20
		internal AttributeValueNotification ProviderManifestTokenNotification
		{
			get
			{
				return this._providerManifestTokenNotification;
			}
		}

		// Token: 0x040006BD RID: 1725
		private readonly HashSet<string> _namespaceLookUpTable = new HashSet<string>(StringComparer.Ordinal);

		// Token: 0x040006BE RID: 1726
		private readonly SchemaElementLookUpTable<SchemaType> _schemaTypes = new SchemaElementLookUpTable<SchemaType>();

		// Token: 0x040006BF RID: 1727
		private const int MaxErrorCount = 100;

		// Token: 0x040006C0 RID: 1728
		private DbProviderManifest _providerManifest;

		// Token: 0x040006C1 RID: 1729
		private PrimitiveSchema _primitiveSchema;

		// Token: 0x040006C2 RID: 1730
		private readonly SchemaDataModelOption _dataModel;

		// Token: 0x040006C3 RID: 1731
		private readonly ProviderManifestNeeded _providerManifestNeeded;

		// Token: 0x040006C4 RID: 1732
		private readonly AttributeValueNotification _providerNotification;

		// Token: 0x040006C5 RID: 1733
		private readonly AttributeValueNotification _providerManifestTokenNotification;

		// Token: 0x020002A3 RID: 675
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000F7B RID: 3963
			public static AttributeValueNotification <0>__NoOpAttributeValueNotification;
		}
	}
}
