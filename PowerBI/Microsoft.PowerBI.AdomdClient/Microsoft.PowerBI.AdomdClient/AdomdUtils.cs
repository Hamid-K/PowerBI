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
		// Token: 0x060006FC RID: 1788 RVA: 0x00023818 File Offset: 0x00021A18
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

		// Token: 0x060006FD RID: 1789 RVA: 0x000238B0 File Offset: 0x00021AB0
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

		// Token: 0x060006FE RID: 1790 RVA: 0x000238E8 File Offset: 0x00021AE8
		internal static DataRowCollection GetRows(AdomdConnection connection, string requestType, ListDictionary restrictions)
		{
			return connection.IDiscoverProvider.Discover(requestType, restrictions).MainRowsetTable.Rows;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00023904 File Offset: 0x00021B04
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

		// Token: 0x06000700 RID: 1792 RVA: 0x00023984 File Offset: 0x00021B84
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

		// Token: 0x06000701 RID: 1793 RVA: 0x000239F4 File Offset: 0x00021BF4
		internal static void AddCubeSourceRestrictionIfApplicable(AdomdConnection connection, ListDictionary restrictions)
		{
			if (connection.IsPostYukonProvider())
			{
				restrictions.Add(AdomdUtils.cubeSourceRestrictionName, AdomdUtils.cubeSourceAll);
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00023A13 File Offset: 0x00021C13
		internal static void AddMemberBinaryRestrictionIfApplicable(AdomdConnection connection, ListDictionary restrictions)
		{
			if (connection.IsPostYukonProvider())
			{
				restrictions.Add("TREE_OP", 72);
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00023A30 File Offset: 0x00021C30
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

		// Token: 0x06000704 RID: 1796 RVA: 0x00023AC0 File Offset: 0x00021CC0
		internal static bool ShouldAddObjectVisibilityRestriction(AdomdConnection connection)
		{
			return connection.ShowHiddenObjects && connection.IsPostYukonProvider();
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00023AD4 File Offset: 0x00021CD4
		internal static void CopyRestrictions(ListDictionary sourceRestrictions, ListDictionary destinationRestrictions)
		{
			foreach (object obj in sourceRestrictions.Keys)
			{
				destinationRestrictions.Add(obj, sourceRestrictions[obj]);
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00023B30 File Offset: 0x00021D30
		internal static void FillNamesHashTable(DataTable table, Hashtable hash)
		{
			AdomdUtils.FillNamesHashTable(table, hash, 0);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00023B3C File Offset: 0x00021D3C
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

		// Token: 0x06000708 RID: 1800 RVA: 0x00023B94 File Offset: 0x00021D94
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

		// Token: 0x06000709 RID: 1801 RVA: 0x00023C2C File Offset: 0x00021E2C
		internal static string UnQualifyPropertyName(string name)
		{
			Match match = AdomdUtils.namePropertyRegex.Match(name);
			if (match.Success)
			{
				return match.Groups["propertyName"].Value;
			}
			return name;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00023C64 File Offset: 0x00021E64
		internal static bool Equals(IMetadataObject object1, IMetadataObject object2)
		{
			return object1 == object2 || (object1 != null && object2 != null && !(object1.UniqueName != object2.UniqueName) && !(object1.CubeName != object2.CubeName) && !(object1.Type != object2.Type) && object1.Connection == object2.Connection && (object1.Connection == null || (object1.SessionId == object2.SessionId && object1.Catalog == object2.Catalog)));
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00023CFC File Offset: 0x00021EFC
		internal static bool Equals(ISubordinateObject object1, ISubordinateObject object2)
		{
			return object1 == object2 || (object1 != null && object2 != null && object1.Ordinal == object2.Ordinal && !(object1.Type != object2.Type) && object.Equals(object1.Parent, object2.Parent));
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00023D50 File Offset: 0x00021F50
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

		// Token: 0x0600070D RID: 1805 RVA: 0x00023DC9 File Offset: 0x00021FC9
		internal static int GetHashCode(ISubordinateObject propertyObject)
		{
			return propertyObject.Ordinal;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00023DD4 File Offset: 0x00021FD4
		internal static string GetDataTableFilter(string columnName, string columnValue)
		{
			if (columnValue == null)
			{
				return string.Format(CultureInfo.InvariantCulture, "( {0} is NULL )", columnName);
			}
			string text = AdomdUtils.Enquote(columnValue, "'", "'");
			return string.Format(CultureInfo.InvariantCulture, "( {0} = {1} )", columnName, text);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00023E18 File Offset: 0x00022018
		internal static string Enquote(string stringValue, string openQuote, string closeQuote)
		{
			if (openQuote == null || closeQuote == null)
			{
				return stringValue;
			}
			string text = stringValue.Replace(closeQuote, closeQuote + closeQuote);
			return openQuote + text + closeQuote;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00023E44 File Offset: 0x00022044
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

		// Token: 0x06000711 RID: 1809 RVA: 0x00023E7D File Offset: 0x0002207D
		internal static void EnsureCacheNotAbandoned(MetadataCacheState state)
		{
			if (state == MetadataCacheState.Abandoned)
			{
				throw new InvalidOperationException(SR.MetadataCache_Abandoned);
			}
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00023E8E File Offset: 0x0002208E
		internal static void EnsureCacheNotInvalid(MetadataCacheState state, AdomdUtils.GetInvalidatedMessageDelegate msgDelegate)
		{
			if (state == MetadataCacheState.Invalid)
			{
				throw new AdomdCacheExpiredException(msgDelegate());
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00023EA0 File Offset: 0x000220A0
		internal static bool IsPostYukonVersion(Version serverVersion)
		{
			return serverVersion != null && serverVersion.Major >= 9;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00023EBC File Offset: 0x000220BC
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

		// Token: 0x040004E9 RID: 1257
		private static string cubeSourceRestrictionName = "CUBE_SOURCE";

		// Token: 0x040004EA RID: 1258
		private static int cubeSourceCube = 1;

		// Token: 0x040004EB RID: 1259
		private static int cubeSourceDimension = 2;

		// Token: 0x040004EC RID: 1260
		private static int cubeSourceAll = AdomdUtils.cubeSourceCube | AdomdUtils.cubeSourceDimension;

		// Token: 0x040004ED RID: 1261
		private const string treeOpRestrictionName = "TREE_OP";

		// Token: 0x040004EE RID: 1262
		private const int MDTREEOP_FETCH_BLOB_PROPERTIES = 64;

		// Token: 0x040004EF RID: 1263
		private const int MDTREEOP_SELF = 8;

		// Token: 0x040004F0 RID: 1264
		private static string MetadataHashCodeTemplate = "{0}#{1}#{2}#{3}#{4}#{5}";

		// Token: 0x040004F1 RID: 1265
		internal const string UnqualifiedPropertyName = "MemberPropertyUnqualifiedName";

		// Token: 0x040004F2 RID: 1266
		internal const string NamesHashtablePropertyName = "MemberPropertiesNamesHash";

		// Token: 0x040004F3 RID: 1267
		private const string propertyNameGroupName = "propertyName";

		// Token: 0x040004F4 RID: 1268
		private const string IdentifierRegExpr = "(((?<propertyName>(\\w+)))|(\\[(?<propertyName>((\\s*)(([^\\s\\]])|(\\]\\]))((([^\\]])|(\\]\\])))*))\\]))";

		// Token: 0x040004F5 RID: 1269
		private static string namePropertyFormat = "^((.*)(((?<propertyName>(\\w+)))|(\\[(?<propertyName>((\\s*)(([^\\s\\]])|(\\]\\]))((([^\\]])|(\\]\\])))*))\\])))$";

		// Token: 0x040004F6 RID: 1270
		private static Regex namePropertyRegex = new Regex(AdomdUtils.namePropertyFormat, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.RightToLeft | RegexOptions.CultureInvariant);

		// Token: 0x040004F7 RID: 1271
		internal static readonly Version UnknownVersion = new Version("0.0.0.0");

		// Token: 0x020001A9 RID: 425
		// (Invoke) Token: 0x060012F7 RID: 4855
		internal delegate string GetInvalidatedMessageDelegate();
	}
}
