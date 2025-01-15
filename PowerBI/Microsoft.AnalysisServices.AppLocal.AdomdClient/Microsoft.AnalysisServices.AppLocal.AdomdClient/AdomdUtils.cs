using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200006C RID: 108
	internal static class AdomdUtils
	{
		// Token: 0x06000709 RID: 1801 RVA: 0x00023B48 File Offset: 0x00021D48
		internal static object GetProperty(DataRow dataRow, string propertyName)
		{
			if (dataRow == null)
			{
				throw new ArgumentNullException("dataRow");
			}
			if (propertyName == null)
			{
				throw new ArgumentNullException("propertyName");
			}
			if (!dataRow.Table.Columns.Contains(propertyName))
			{
				throw new NotSupportedException(SR.Property_UnknownProperty(propertyName));
			}
			if (dataRow.Table.Columns[propertyName].AutoIncrement)
			{
				string text = dataRow.Table.TableName + propertyName;
				return dataRow.GetChildRows(text);
			}
			object obj = dataRow[propertyName];
			if (obj is XmlaError)
			{
				throw new AdomdErrorResponseException((XmlaError)obj);
			}
			return obj;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00023BE0 File Offset: 0x00021DE0
		internal static object GetProperty(DataRow dataRow, int index)
		{
			if (dataRow == null)
			{
				throw new ArgumentNullException("dataRow");
			}
			object obj = dataRow[index];
			if (obj is XmlaError)
			{
				throw new AdomdErrorResponseException((XmlaError)obj);
			}
			return obj;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00023C18 File Offset: 0x00021E18
		internal static DataRowCollection GetRows(AdomdConnection connection, string requestType, ListDictionary restrictions)
		{
			return connection.IDiscoverProvider.Discover(requestType, restrictions).MainRowsetTable.Rows;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00023C34 File Offset: 0x00021E34
		internal static void PopulateSymetry(IAdomdBaseObject iBaseObject)
		{
			if (iBaseObject.Connection == null)
			{
				throw new NotSupportedException(SR.NotSupportedWhenConnectionMissing);
			}
			AdomdUtils.CheckConnectionOpened(iBaseObject.Connection);
			if (iBaseObject.CubeName == null)
			{
				throw new NotSupportedException(SR.NotSupportedByProvider);
			}
			IAdomdBaseObject adomdBaseObject = (IAdomdBaseObject)iBaseObject.Connection.GetObjectData(iBaseObject.SchemaObjectType, iBaseObject.CubeName, iBaseObject.InternalUniqueName);
			iBaseObject.MetadataData = adomdBaseObject.MetadataData;
			iBaseObject.ParentObject = adomdBaseObject.ParentObject;
			iBaseObject.IsMetadata = true;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00023CB4 File Offset: 0x00021EB4
		internal static void CheckCopyToParameters(Array array, int index, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank > 1)
			{
				throw new ArgumentException(SR.ICollection_CannotCopyToMultidimensionalArray, "array");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (array.Length - index < count)
			{
				throw new ArgumentException(SR.ICollection_NotEnoughSpaceToCopyTo(array.Length - index, count), "index");
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00023D24 File Offset: 0x00021F24
		internal static void AddCubeSourceRestrictionIfApplicable(AdomdConnection connection, ListDictionary restrictions)
		{
			if (connection.IsPostYukonProvider())
			{
				restrictions.Add(AdomdUtils.cubeSourceRestrictionName, AdomdUtils.cubeSourceAll);
			}
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00023D43 File Offset: 0x00021F43
		internal static void AddMemberBinaryRestrictionIfApplicable(AdomdConnection connection, ListDictionary restrictions)
		{
			if (connection.IsPostYukonProvider())
			{
				restrictions.Add("TREE_OP", 72);
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00023D60 File Offset: 0x00021F60
		internal static void AddObjectVisibilityRestrictionIfApplicable(AdomdConnection connection, string requestType, ListDictionary restrictions)
		{
			if (AdomdUtils.ShouldAddObjectVisibilityRestriction(connection))
			{
				string text;
				if (requestType == DimensionCollectionInternal.schemaName)
				{
					text = "DIMENSION_VISIBILITY";
				}
				else if (requestType == HierarchyCollectionInternal.schemaName)
				{
					text = "HIERARCHY_VISIBILITY";
				}
				else if (requestType == LevelCollectionInternal.schemaName)
				{
					text = "LEVEL_VISIBILITY";
				}
				else if (requestType == MeasureCollectionInternal.schemaName)
				{
					text = "MEASURE_VISIBILITY";
				}
				else if (requestType == "MDSCHEMA_PROPERTIES")
				{
					text = "PROPERTY_VISIBILITY";
				}
				else
				{
					text = null;
				}
				if (text != null)
				{
					restrictions.Add(text, 3);
				}
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00023DF0 File Offset: 0x00021FF0
		internal static bool ShouldAddObjectVisibilityRestriction(AdomdConnection connection)
		{
			return connection.ShowHiddenObjects && connection.IsPostYukonProvider();
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00023E04 File Offset: 0x00022004
		internal static void CopyRestrictions(ListDictionary sourceRestrictions, ListDictionary destinationRestrictions)
		{
			foreach (object obj in sourceRestrictions.Keys)
			{
				destinationRestrictions.Add(obj, sourceRestrictions[obj]);
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00023E60 File Offset: 0x00022060
		internal static void FillNamesHashTable(DataTable table, Hashtable hash)
		{
			AdomdUtils.FillNamesHashTable(table, hash, 0);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00023E6C File Offset: 0x0002206C
		internal static void FillNamesHashTable(DataTable table, Hashtable hash, int startColumn)
		{
			if (table == null || hash == null)
			{
				return;
			}
			hash.Clear();
			for (int i = startColumn; i < table.Columns.Count; i++)
			{
				string caption = table.Columns[i].Caption;
				if (hash[caption] == null)
				{
					hash[caption] = i;
				}
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00023EC4 File Offset: 0x000220C4
		internal static void FillPropertiesNamesHashTable(DataTable table, Hashtable hash, int startColumn)
		{
			if (table == null || hash == null)
			{
				return;
			}
			hash.Clear();
			for (int i = startColumn; i < table.Columns.Count; i++)
			{
				DataColumn dataColumn = table.Columns[i];
				if (!dataColumn.ExtendedProperties.ContainsKey("MemberPropertyUnqualifiedName"))
				{
					dataColumn.ExtendedProperties["MemberPropertyUnqualifiedName"] = AdomdUtils.UnQualifyPropertyName(dataColumn.Caption);
				}
				string text = dataColumn.ExtendedProperties["MemberPropertyUnqualifiedName"] as string;
				if (hash[text] == null)
				{
					hash[text] = i;
				}
			}
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00023F5C File Offset: 0x0002215C
		internal static string UnQualifyPropertyName(string name)
		{
			Match match = AdomdUtils.namePropertyRegex.Match(name);
			if (match.Success)
			{
				return match.Groups["propertyName"].Value;
			}
			return name;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00023F94 File Offset: 0x00022194
		internal static bool Equals(IMetadataObject object1, IMetadataObject object2)
		{
			return object1 == object2 || (object1 != null && object2 != null && !(object1.UniqueName != object2.UniqueName) && !(object1.CubeName != object2.CubeName) && !(object1.Type != object2.Type) && object1.Connection == object2.Connection && (object1.Connection == null || (object1.SessionId == object2.SessionId && object1.Catalog == object2.Catalog)));
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0002402C File Offset: 0x0002222C
		internal static bool Equals(ISubordinateObject object1, ISubordinateObject object2)
		{
			return object1 == object2 || (object1 != null && object2 != null && object1.Ordinal == object2.Ordinal && !(object1.Type != object2.Type) && object.Equals(object1.Parent, object2.Parent));
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00024080 File Offset: 0x00022280
		internal static int GetHashCode(IMetadataObject metadataObject)
		{
			return string.Format(CultureInfo.InvariantCulture, AdomdUtils.MetadataHashCodeTemplate, new object[]
			{
				metadataObject.Connection.GetHashCode().ToString(CultureInfo.InvariantCulture),
				metadataObject.SessionId,
				metadataObject.Catalog,
				metadataObject.CubeName,
				metadataObject.Type.GetHashCode(),
				metadataObject.UniqueName
			}).GetHashCode();
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x000240F9 File Offset: 0x000222F9
		internal static int GetHashCode(ISubordinateObject propertyObject)
		{
			return propertyObject.Ordinal;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00024104 File Offset: 0x00022304
		internal static string GetDataTableFilter(string columnName, string columnValue)
		{
			if (columnValue == null)
			{
				return string.Format(CultureInfo.InvariantCulture, "( {0} is NULL )", columnName);
			}
			string text = AdomdUtils.Enquote(columnValue, "'", "'");
			return string.Format(CultureInfo.InvariantCulture, "( {0} = {1} )", columnName, text);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00024148 File Offset: 0x00022348
		internal static string Enquote(string stringValue, string openQuote, string closeQuote)
		{
			if (openQuote == null || closeQuote == null)
			{
				return stringValue;
			}
			string text = stringValue.Replace(closeQuote, closeQuote + closeQuote);
			return openQuote + text + closeQuote;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00024174 File Offset: 0x00022374
		internal static void CheckConnectionOpened(AdomdConnection connection)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (connection.State == ConnectionState.Open)
			{
				return;
			}
			if (connection.UserOpened)
			{
				throw new AdomdConnectionException(SR.Command_ConnectionIsNotOpened, null, ConnectionExceptionCause.ConnectionNotOpen);
			}
			throw new InvalidOperationException(SR.Command_ConnectionIsNotOpened);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x000241AD File Offset: 0x000223AD
		internal static void EnsureCacheNotAbandoned(MetadataCacheState state)
		{
			if (state == MetadataCacheState.Abandoned)
			{
				throw new InvalidOperationException(SR.MetadataCache_Abandoned);
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x000241BE File Offset: 0x000223BE
		internal static void EnsureCacheNotInvalid(MetadataCacheState state, AdomdUtils.GetInvalidatedMessageDelegate msgDelegate)
		{
			if (state == MetadataCacheState.Invalid)
			{
				throw new AdomdCacheExpiredException(msgDelegate());
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x000241D0 File Offset: 0x000223D0
		internal static bool IsPostYukonVersion(Version serverVersion)
		{
			return serverVersion != null && serverVersion.Major >= 9;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x000241EC File Offset: 0x000223EC
		internal static Version ConvertVersionStringToVersionObject(string versionString)
		{
			Version version = AdomdUtils.UnknownVersion;
			if (!string.IsNullOrEmpty(versionString))
			{
				try
				{
					version = new Version(versionString);
				}
				catch (ArgumentNullException)
				{
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				catch (ArgumentException)
				{
				}
				catch (FormatException)
				{
				}
				catch (OverflowException)
				{
				}
			}
			return version;
		}

		// Token: 0x040004F6 RID: 1270
		private static string cubeSourceRestrictionName = "CUBE_SOURCE";

		// Token: 0x040004F7 RID: 1271
		private static int cubeSourceCube = 1;

		// Token: 0x040004F8 RID: 1272
		private static int cubeSourceDimension = 2;

		// Token: 0x040004F9 RID: 1273
		private static int cubeSourceAll = AdomdUtils.cubeSourceCube | AdomdUtils.cubeSourceDimension;

		// Token: 0x040004FA RID: 1274
		private const string treeOpRestrictionName = "TREE_OP";

		// Token: 0x040004FB RID: 1275
		private const int MDTREEOP_FETCH_BLOB_PROPERTIES = 64;

		// Token: 0x040004FC RID: 1276
		private const int MDTREEOP_SELF = 8;

		// Token: 0x040004FD RID: 1277
		private static string MetadataHashCodeTemplate = "{0}#{1}#{2}#{3}#{4}#{5}";

		// Token: 0x040004FE RID: 1278
		internal const string UnqualifiedPropertyName = "MemberPropertyUnqualifiedName";

		// Token: 0x040004FF RID: 1279
		internal const string NamesHashtablePropertyName = "MemberPropertiesNamesHash";

		// Token: 0x04000500 RID: 1280
		private const string propertyNameGroupName = "propertyName";

		// Token: 0x04000501 RID: 1281
		private const string IdentifierRegExpr = "(((?<propertyName>(\\w+)))|(\\[(?<propertyName>((\\s*)(([^\\s\\]])|(\\]\\]))((([^\\]])|(\\]\\])))*))\\]))";

		// Token: 0x04000502 RID: 1282
		private static string namePropertyFormat = "^((.*)(((?<propertyName>(\\w+)))|(\\[(?<propertyName>((\\s*)(([^\\s\\]])|(\\]\\]))((([^\\]])|(\\]\\])))*))\\])))$";

		// Token: 0x04000503 RID: 1283
		private static Regex namePropertyRegex = new Regex(AdomdUtils.namePropertyFormat, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.RightToLeft | RegexOptions.CultureInvariant);

		// Token: 0x04000504 RID: 1284
		internal static readonly Version UnknownVersion = new Version("0.0.0.0");

		// Token: 0x020001A9 RID: 425
		// (Invoke) Token: 0x06001304 RID: 4868
		internal delegate string GetInvalidatedMessageDelegate();
	}
}
