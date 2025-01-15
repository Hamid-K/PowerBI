using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200031B RID: 795
	[DebuggerDisplay("DataModel={DataModel}")]
	internal class SchemaManager
	{
		// Token: 0x060025CF RID: 9679 RVA: 0x0006BE68 File Offset: 0x0006A068
		private SchemaManager(SchemaDataModelOption dataModel, AttributeValueNotification providerNotification, AttributeValueNotification providerManifestTokenNotification, ProviderManifestNeeded providerManifestNeeded)
		{
			this._dataModel = dataModel;
			this._providerNotification = providerNotification;
			this._providerManifestTokenNotification = providerManifestTokenNotification;
			this._providerManifestNeeded = providerManifestNeeded;
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x0006BEA8 File Offset: 0x0006A0A8
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

		// Token: 0x060025D1 RID: 9681 RVA: 0x0006BEFA File Offset: 0x0006A0FA
		public static void NoOpAttributeValueNotification(string attributeValue, Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
		{
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x0006BEFC File Offset: 0x0006A0FC
		public static IList<EdmSchemaError> ParseAndValidate(IEnumerable<XmlReader> xmlReaders, IEnumerable<string> sourceFilePaths, SchemaDataModelOption dataModel, DbProviderManifest providerManifest, out IList<Schema> schemaCollection)
		{
			return SchemaManager.ParseAndValidate(xmlReaders, sourceFilePaths, dataModel, new AttributeValueNotification(SchemaManager.NoOpAttributeValueNotification), new AttributeValueNotification(SchemaManager.NoOpAttributeValueNotification), (Action<string, ErrorCode, EdmSchemaErrorSeverity> error) => providerManifest ?? MetadataItem.EdmProviderManifest, out schemaCollection);
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x0006BF44 File Offset: 0x0006A144
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

		// Token: 0x060025D4 RID: 9684 RVA: 0x0006C0DC File Offset: 0x0006A2DC
		internal static bool TryGetSchemaVersion(XmlReader reader, out double version, out DataSpace dataSpace)
		{
			if (!reader.EOF && reader.NodeType != XmlNodeType.Element)
			{
				while (reader.Read() && reader.NodeType != XmlNodeType.Element)
				{
				}
			}
			if (!reader.EOF && (reader.LocalName == "Schema" || reader.LocalName == "Mapping"))
			{
				return SchemaManager.TryGetSchemaVersion(reader.NamespaceURI, out version, out dataSpace);
			}
			version = 0.0;
			dataSpace = DataSpace.OSpace;
			return false;
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x0006C154 File Offset: 0x0006A354
		internal static bool TryGetSchemaVersion(string xmlNamespaceName, out double version, out DataSpace dataSpace)
		{
			if (xmlNamespaceName != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(xmlNamespaceName);
				if (num <= 2737002321U)
				{
					if (num <= 276889131U)
					{
						if (num != 54480152U)
						{
							if (num == 276889131U)
							{
								if (xmlNamespaceName == "http://schemas.microsoft.com/ado/2006/04/edm/ssdl")
								{
									version = 1.0;
									dataSpace = DataSpace.SSpace;
									return true;
								}
							}
						}
						else if (xmlNamespaceName == "http://schemas.microsoft.com/ado/2009/11/edm/ssdl")
						{
							version = 3.0;
							dataSpace = DataSpace.SSpace;
							return true;
						}
					}
					else if (num != 334451066U)
					{
						if (num != 1932155917U)
						{
							if (num == 2737002321U)
							{
								if (xmlNamespaceName == "http://schemas.microsoft.com/ado/2009/11/edm")
								{
									version = 3.0;
									dataSpace = DataSpace.CSpace;
									return true;
								}
							}
						}
						else if (xmlNamespaceName == "urn:schemas-microsoft-com:windows:storage:mapping:CS")
						{
							version = 1.0;
							dataSpace = DataSpace.CSSpace;
							return true;
						}
					}
					else if (xmlNamespaceName == "http://schemas.microsoft.com/ado/2008/09/mapping/cs")
					{
						version = 2.0;
						dataSpace = DataSpace.CSSpace;
						return true;
					}
				}
				else if (num <= 2886803144U)
				{
					if (num != 2826911950U)
					{
						if (num == 2886803144U)
						{
							if (xmlNamespaceName == "http://schemas.microsoft.com/ado/2006/04/edm")
							{
								version = 1.0;
								dataSpace = DataSpace.CSpace;
								return true;
							}
						}
					}
					else if (xmlNamespaceName == "http://schemas.microsoft.com/ado/2007/05/edm")
					{
						version = 1.1;
						dataSpace = DataSpace.CSpace;
						return true;
					}
				}
				else if (num != 3075483009U)
				{
					if (num != 3250650152U)
					{
						if (num == 4049421514U)
						{
							if (xmlNamespaceName == "http://schemas.microsoft.com/ado/2009/11/mapping/cs")
							{
								version = 3.0;
								dataSpace = DataSpace.CSSpace;
								return true;
							}
						}
					}
					else if (xmlNamespaceName == "http://schemas.microsoft.com/ado/2009/02/edm/ssdl")
					{
						version = 2.0;
						dataSpace = DataSpace.SSpace;
						return true;
					}
				}
				else if (xmlNamespaceName == "http://schemas.microsoft.com/ado/2008/09/edm")
				{
					version = 2.0;
					dataSpace = DataSpace.CSpace;
					return true;
				}
			}
			version = 0.0;
			dataSpace = DataSpace.OSpace;
			return false;
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x0006C380 File Offset: 0x0006A580
		private static bool CheckIsSameVersion(Schema schemaToBeAdded, IEnumerable<Schema> schemaCollection, List<EdmSchemaError> errorCollection)
		{
			if (schemaToBeAdded.SchemaVersion != 0.0 && schemaCollection.Count<Schema>() > 0 && schemaCollection.Any((Schema s) => s.SchemaVersion != 0.0 && s.SchemaVersion != schemaToBeAdded.SchemaVersion))
			{
				errorCollection.Add(new EdmSchemaError(Strings.CannotLoadDifferentVersionOfSchemaInTheSameItemCollection, 194, EdmSchemaErrorSeverity.Error));
			}
			return true;
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060025D7 RID: 9687 RVA: 0x0006C3E4 File Offset: 0x0006A5E4
		public double SchemaVersion
		{
			get
			{
				return this.effectiveSchemaVersion;
			}
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x0006C3EC File Offset: 0x0006A5EC
		public void AddSchema(Schema schema)
		{
			if (this._namespaceLookUpTable.Count == 0 && schema.DataModel != SchemaDataModelOption.ProviderManifestModel && this.PrimitiveSchema.Namespace != null)
			{
				this._namespaceLookUpTable.Add(this.PrimitiveSchema.Namespace);
			}
			this._namespaceLookUpTable.Add(schema.Namespace);
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x0006C448 File Offset: 0x0006A648
		public bool TryResolveType(string namespaceName, string typeName, out SchemaType schemaType)
		{
			string text = (string.IsNullOrEmpty(namespaceName) ? typeName : (namespaceName + "." + typeName));
			schemaType = this.SchemaTypes.LookUpEquivalentKey(text);
			return schemaType != null;
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x0006C482 File Offset: 0x0006A682
		public bool IsValidNamespaceName(string namespaceName)
		{
			return this._namespaceLookUpTable.Contains(namespaceName);
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x0006C490 File Offset: 0x0006A690
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

		// Token: 0x060025DC RID: 9692 RVA: 0x0006C4DC File Offset: 0x0006A6DC
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

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x060025DD RID: 9693 RVA: 0x0006C534 File Offset: 0x0006A734
		internal SchemaElementLookUpTable<SchemaType> SchemaTypes
		{
			get
			{
				return this._schemaTypes;
			}
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x0006C53C File Offset: 0x0006A73C
		internal DbProviderManifest GetProviderManifest(Action<string, ErrorCode, EdmSchemaErrorSeverity> addError)
		{
			if (this._providerManifest == null)
			{
				this._providerManifest = this._providerManifestNeeded(addError);
			}
			return this._providerManifest;
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060025DF RID: 9695 RVA: 0x0006C55E File Offset: 0x0006A75E
		internal SchemaDataModelOption DataModel
		{
			get
			{
				return this._dataModel;
			}
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x0006C566 File Offset: 0x0006A766
		internal void EnsurePrimitiveSchemaIsLoaded(double forSchemaVersion)
		{
			if (this._primitiveSchema == null)
			{
				this.effectiveSchemaVersion = forSchemaVersion;
				this._primitiveSchema = new PrimitiveSchema(this);
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060025E1 RID: 9697 RVA: 0x0006C583 File Offset: 0x0006A783
		internal PrimitiveSchema PrimitiveSchema
		{
			get
			{
				return this._primitiveSchema;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060025E2 RID: 9698 RVA: 0x0006C58B File Offset: 0x0006A78B
		internal AttributeValueNotification ProviderNotification
		{
			get
			{
				return this._providerNotification;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060025E3 RID: 9699 RVA: 0x0006C593 File Offset: 0x0006A793
		internal AttributeValueNotification ProviderManifestTokenNotification
		{
			get
			{
				return this._providerManifestTokenNotification;
			}
		}

		// Token: 0x04000D49 RID: 3401
		private readonly HashSet<string> _namespaceLookUpTable = new HashSet<string>(StringComparer.Ordinal);

		// Token: 0x04000D4A RID: 3402
		private readonly SchemaElementLookUpTable<SchemaType> _schemaTypes = new SchemaElementLookUpTable<SchemaType>();

		// Token: 0x04000D4B RID: 3403
		private const int MaxErrorCount = 100;

		// Token: 0x04000D4C RID: 3404
		private DbProviderManifest _providerManifest;

		// Token: 0x04000D4D RID: 3405
		private PrimitiveSchema _primitiveSchema;

		// Token: 0x04000D4E RID: 3406
		private double effectiveSchemaVersion;

		// Token: 0x04000D4F RID: 3407
		private readonly SchemaDataModelOption _dataModel;

		// Token: 0x04000D50 RID: 3408
		private readonly ProviderManifestNeeded _providerManifestNeeded;

		// Token: 0x04000D51 RID: 3409
		private readonly AttributeValueNotification _providerNotification;

		// Token: 0x04000D52 RID: 3410
		private readonly AttributeValueNotification _providerManifestTokenNotification;
	}
}
