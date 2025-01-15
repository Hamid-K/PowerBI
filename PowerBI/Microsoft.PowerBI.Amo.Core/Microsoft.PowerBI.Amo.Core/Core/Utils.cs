using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Xml;

namespace Microsoft.AnalysisServices.Core
{
	// Token: 0x020000ED RID: 237
	[Guid("8E07D707-BC6D-4CFD-B1CA-6DC220AB795F")]
	internal static class Utils
	{
		// Token: 0x06000F1C RID: 3868 RVA: 0x00033CFC File Offset: 0x00031EFC
		static Utils()
		{
			byte[] publicKey = Assembly.GetExecutingAssembly().GetName().GetPublicKey();
			if ("0024000004800000940000000602000000240000525341310004000001000100272736AD6E5F9586BAC2D531EABC3ACC666C2F8EC879FA94F8F7B0327D2FF2ED523448F83C3D5C5DD2DFC7BC99C5286B2C125117BF5CBE242B9D41750732B2BDFFE649C6EFB8E5526D526FDD130095ECDB7BF210809C6CDAD8824FAA9AC0310AC3CBA2AA0523567B2DFA7FE250B30FACBD62D4EC99B94AC47C7D3B28F1F6E4C8" != new StrongNamePublicKeyBlob(publicKey).ToString())
			{
				throw new Exception("Internal failure: the AMO public key constant is different than the real public key !");
			}
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00033E7F File Offset: 0x0003207F
		internal static bool AreIDsEqual(string id1, string id2)
		{
			id1 = Utils.Trim(id1);
			id2 = Utils.Trim(id2);
			return id1 != null && id2 != null && string.Compare(id1, id2, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00033EAC File Offset: 0x000320AC
		internal static StringCollection Copy(StringCollection source, StringCollection dest)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			dest.Clear();
			int i = 0;
			int count = source.Count;
			while (i < count)
			{
				dest.Add(source[i]);
				i++;
			}
			return dest;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00033EEF File Offset: 0x000320EF
		internal static string Trim(string str)
		{
			if (str != null)
			{
				str = str.Trim();
				if (str.Length == 0)
				{
					str = null;
				}
			}
			return str;
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x00033F08 File Offset: 0x00032108
		internal static bool IsSyntacticallyValidName(string name, Type type, out string error)
		{
			return Utils.IsSyntacticallyValidName(name, type, ModelType.Multidimensional, 1050, out error);
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x00033F18 File Offset: 0x00032118
		internal static bool IsSyntacticallyValidName(string name, Type type, ModelType modelType, int compatibilityLevel, out string error)
		{
			int num = 100;
			if (Utils.IsRelaxedNameValidation(modelType, compatibilityLevel))
			{
				num = int.MaxValue;
			}
			return Utils.IsSyntacticallyValid(name, "Name", type, modelType, compatibilityLevel, num, out error);
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x00033F48 File Offset: 0x00032148
		internal static bool IsSyntacticallyValidID(string id, Type type, out string error)
		{
			return Utils.IsSyntacticallyValid(id, "ID", type, ModelType.Multidimensional, 1050, 100, out error);
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00033F5F File Offset: 0x0003215F
		internal static bool IsRelaxedNameValidation(ModelType modelType, int compatibilityLevel)
		{
			return modelType == ModelType.Default || compatibilityLevel == 0 || (modelType == ModelType.Tabular && compatibilityLevel >= 1103);
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x00033F7C File Offset: 0x0003217C
		private static bool IsSyntacticallyValid(string str, string propertyName, Type type, ModelType modelType, int compatibilityLevel, int maxLength, out string error)
		{
			if (null == type)
			{
				throw new ArgumentNullException("type");
			}
			if (propertyName == null)
			{
				throw new ArgumentNullException("propertyName");
			}
			str = Utils.Trim(str);
			if (str == null)
			{
				error = SR.ValueIsRequired(propertyName);
				return false;
			}
			if (str.Length > maxLength)
			{
				error = SR.ValueIsTooLong(propertyName, str.Length, maxLength);
				return false;
			}
			char[] array = Utils.GetInvalidChars(type, propertyName, modelType, compatibilityLevel);
			if (str.IndexOfAny(array) != -1)
			{
				error = SR.ValueHasInvalidCharacters(propertyName, Utils.GetInvalidCharsAsUserFriendlyString(array));
				return false;
			}
			error = null;
			return true;
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x00034009 File Offset: 0x00032209
		internal static bool IsValidName(string name, Type type, ModelType modelType, int compatibilityLevel, NamedComponentCollection col, out string error)
		{
			if (col == null)
			{
				return Utils.IsSyntacticallyValidName(name, type, modelType, compatibilityLevel, out error);
			}
			return col.IsValidName(name, type, modelType, compatibilityLevel, out error);
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x00034028 File Offset: 0x00032228
		internal static bool IsValidNameCharsForDatabase(Server server, string databaseNameToValidate, out string error)
		{
			ConnectionInfo connectionInfo = ((server != null) ? server.ConnectionInfo : null);
			bool flag = server != null && connectionInfo != null;
			bool flag2 = false;
			if (!flag || connectionInfo.IsPbiPremiumXmlaEp || flag2)
			{
				error = null;
				return true;
			}
			return Utils.IsValidNameCharsForDatabaseWithoutPbiPublicXmla(databaseNameToValidate, out error);
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x00034069 File Offset: 0x00032269
		internal static bool IsValidNameCharsForDatabaseWithoutPbiPublicXmla(string databaseNameToValidate, out string error)
		{
			if (databaseNameToValidate.IndexOfAny(Utils.invalidChars) != -1)
			{
				error = SR.ValueHasInvalidCharacters("Name", Utils.GetInvalidCharsAsUserFriendlyString(Utils.invalidChars));
				return false;
			}
			error = null;
			return true;
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00034095 File Offset: 0x00032295
		internal static string GetSyntacticallyValidName(string baseName, Type type, ModelType modelType, int compatibilityLevel)
		{
			return Utils.GetSyntacticallyValid(baseName, type, "Name", modelType, compatibilityLevel, 100);
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x000340A8 File Offset: 0x000322A8
		internal static string GetNewID(string idPrefix, Type itemsType)
		{
			if (idPrefix == null)
			{
				idPrefix = itemsType.Name;
			}
			else
			{
				idPrefix = Utils.GetSyntacticallyValidID(idPrefix, itemsType);
			}
			string text;
			if (!Utils.IsSyntacticallyValidID(idPrefix, itemsType, out text))
			{
				StringGenerator stringGenerator = new StringGenerator(idPrefix, 100);
				for (string text2 = stringGenerator.Next; text2 != null; text2 = stringGenerator.Next)
				{
					if (Utils.IsSyntacticallyValidID(text2, itemsType, out text))
					{
						return text2;
					}
				}
				throw new Exception();
			}
			return idPrefix;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00034106 File Offset: 0x00032306
		internal static string GetSyntacticallyValidID(string baseID, Type type)
		{
			return Utils.GetSyntacticallyValid(baseID, type, "ID", ModelType.Multidimensional, 1050, 100);
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0003411C File Offset: 0x0003231C
		private static string GetSyntacticallyValid(string str, Type type, string propertyName, ModelType modelType, int compatibilityLevel, int maxLength)
		{
			if (null == type)
			{
				throw new ArgumentNullException("type");
			}
			str = Utils.Trim(str);
			if (str == null)
			{
				return "A";
			}
			int i = 0;
			int length = str.Length;
			while (i < length)
			{
				char c = str[i];
				if (char.IsControl(c))
				{
					str = str.Replace(c, ' ');
				}
				i++;
			}
			char[] array = Utils.GetInvalidChars(type, propertyName, modelType, compatibilityLevel);
			int j = 0;
			int num = array.Length;
			while (j < num)
			{
				str = str.Replace(array[j], ' ');
				j++;
			}
			str = Utils.Trim(str);
			if (str == null)
			{
				return "A";
			}
			if (str.Length > maxLength)
			{
				str = str.Substring(0, maxLength).TrimEnd(Array.Empty<char>());
			}
			return str;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x000341DC File Offset: 0x000323DC
		private static char[] GetInvalidChars(Type forType, string propertyName, ModelType modelType, int compatibilityLevel)
		{
			if (forType == typeof(Server) || forType.IsSubclassOf(typeof(Server)))
			{
				return Utils.invalidCharsForServer;
			}
			if (forType == typeof(Database) || forType.IsSubclassOf(typeof(Database)))
			{
				return Utils.invalidCharsForDatabase;
			}
			return Utils.invalidChars;
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00034244 File Offset: 0x00032444
		private static string GetInvalidCharsAsUserFriendlyString(char[] chars)
		{
			if (chars == Utils.invalidChars)
			{
				return ". , ; ' ` : / \\ * | ? \" & % $ ! + = ( ) [ ] { } < >";
			}
			if (chars == Utils.invalidCharsForDataSource)
			{
				return ": / \\ * | ? \" ( ) [ ] { } < >";
			}
			if (chars == Utils.invalidCharsForDimensionAndHierarchy)
			{
				return ". , ; ' ` : / \\ * | ? \" & % $ ! + = ( ) [ ] { } < >";
			}
			if (chars == Utils.invalidCharsForLevelAndAttribute)
			{
				return ". , ; ' ` : / \\ * | ? \" & % $ ! + = [ ] { } < >";
			}
			if (chars == Utils.invalidCharsForServer)
			{
				return "";
			}
			if (chars == Utils.invalidCharsForDatabase)
			{
				return "";
			}
			return null;
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x000342A8 File Offset: 0x000324A8
		internal static XmlException CreateSerializationException(XmlReader xmlReader)
		{
			if (xmlReader is IXmlLineInfo)
			{
				IXmlLineInfo xmlLineInfo = (IXmlLineInfo)xmlReader;
				return new XmlException(SR.Serialization_UnexpectedElement(xmlReader.Name, xmlReader.NamespaceURI), null, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
			}
			return new XmlException(SR.Serialization_UnexpectedElement(xmlReader.Name, xmlReader.NamespaceURI));
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x000342FE File Offset: 0x000324FE
		internal static AmoException CreateItemNotFoundException(object keyValue, string keyPropertyName, string itemClassName)
		{
			return new AmoException((keyValue == null) ? SR.Collections_KeyIsNull(keyPropertyName, itemClassName) : SR.Collections_KeyNotFound(keyValue.ToString(), keyPropertyName, itemClassName));
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0003431E File Offset: 0x0003251E
		internal static AmoException CreateParentMissingException(ModelComponent obj, Type parentType)
		{
			return new AmoException(SR.Parent_DirectOrIndirectIsMissing(obj.FriendlyName, parentType.Name));
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x00034336 File Offset: 0x00032536
		internal static void CheckValidPath(string path)
		{
			if (path != null && -1 != path.IndexOfAny(Path.GetInvalidPathChars()))
			{
				throw new ArgumentException(SR.InvalidPath(path));
			}
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x00034358 File Offset: 0x00032558
		internal static bool AreSamePaths(string path1, string path2)
		{
			if (string.IsNullOrEmpty(path1))
			{
				return string.IsNullOrEmpty(path2);
			}
			if (string.IsNullOrEmpty(path2))
			{
				return false;
			}
			if (string.Compare(path1, path2, StringComparison.CurrentCultureIgnoreCase) == 0)
			{
				return true;
			}
			path1 = Path.GetFullPath(path1).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
			path2 = Path.GetFullPath(path2).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
			return string.Compare(path1, path2, StringComparison.CurrentCultureIgnoreCase) == 0;
		}

		// Token: 0x040007F2 RID: 2034
		internal const string LocalDataSourceID = ".";

		// Token: 0x040007F3 RID: 2035
		internal const int MaxMajorObjectsDepth = 10;

		// Token: 0x040007F4 RID: 2036
		private static readonly string[] invalidStrings = new string[]
		{
			"AUX", "CLOCK$", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8",
			"COM9", "CON", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8",
			"LPT9", "NUL", "PRN"
		};

		// Token: 0x040007F5 RID: 2037
		private static readonly char[] invalidCharsForServer = new char[0];

		// Token: 0x040007F6 RID: 2038
		private const string invalidCharsForServer_UserFriendly = "";

		// Token: 0x040007F7 RID: 2039
		private static readonly char[] invalidCharsForDatabase = new char[0];

		// Token: 0x040007F8 RID: 2040
		private const string invalidCharsForDatabase_UserFriendly = "";

		// Token: 0x040007F9 RID: 2041
		private static readonly char[] invalidCharsForDataSource = new char[]
		{
			':', '/', '\\', '*', '|', '?', '"', '(', ')', '[',
			']', '{', '}', '<', '>'
		};

		// Token: 0x040007FA RID: 2042
		private const string invalidCharsForDataSource_UserFriendly = ": / \\ * | ? \" ( ) [ ] { } < >";

		// Token: 0x040007FB RID: 2043
		private static readonly char[] invalidCharsForLevelAndAttribute = new char[]
		{
			'.', ',', ';', '\'', '`', ':', '/', '\\', '*', '|',
			'?', '"', '&', '%', '$', '!', '+', '=', '[', ']',
			'{', '}', '<', '>'
		};

		// Token: 0x040007FC RID: 2044
		private const string invalidCharsForLevelAndAttribute_UserFriendly = ". , ; ' ` : / \\ * | ? \" & % $ ! + = [ ] { } < >";

		// Token: 0x040007FD RID: 2045
		private static readonly char[] invalidCharsForDimensionAndHierarchy = new char[]
		{
			'.', ',', ';', '\'', '`', ':', '/', '\\', '*', '|',
			'?', '"', '&', '%', '$', '!', '+', '=', '(', ')',
			'[', ']', '{', '}', '<', '>'
		};

		// Token: 0x040007FE RID: 2046
		private const string invalidCharsForDimensionAndHierarchy_UserFriendly = ". , ; ' ` : / \\ * | ? \" & % $ ! + = ( ) [ ] { } < >";

		// Token: 0x040007FF RID: 2047
		private static readonly char[] invalidChars = new char[]
		{
			'.', ',', ';', '\'', '`', ':', '/', '\\', '*', '|',
			'?', '"', '&', '%', '$', '!', '+', '=', '(', ')',
			'[', ']', '{', '}', '<', '>'
		};

		// Token: 0x04000800 RID: 2048
		private const string invalidChars_UserFriendly = ". , ; ' ` : / \\ * | ? \" & % $ ! + = ( ) [ ] { } < >";

		// Token: 0x04000801 RID: 2049
		internal const string PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736AD6E5F9586BAC2D531EABC3ACC666C2F8EC879FA94F8F7B0327D2FF2ED523448F83C3D5C5DD2DFC7BC99C5286B2C125117BF5CBE242B9D41750732B2BDFFE649C6EFB8E5526D526FDD130095ECDB7BF210809C6CDAD8824FAA9AC0310AC3CBA2AA0523567B2DFA7FE250B30FACBD62D4EC99B94AC47C7D3B28F1F6E4C8";

		// Token: 0x04000802 RID: 2050
		internal const string DefaultStringValue = null;

		// Token: 0x04000803 RID: 2051
		internal const int DefaultIntegerValue = 0;

		// Token: 0x04000804 RID: 2052
		internal const double DefaultDoubleValue = 0.0;

		// Token: 0x04000805 RID: 2053
		internal const long DefaultLongValue = 0L;

		// Token: 0x04000806 RID: 2054
		internal const bool DefaultBoolValue = false;

		// Token: 0x04000807 RID: 2055
		private const string DefaultNameOrID = "A";

		// Token: 0x04000808 RID: 2056
		internal const string MeasuresDimensionID = "Measures";

		// Token: 0x04000809 RID: 2057
		internal const long LargePartitionWithNoAggsThreshold = 500000L;

		// Token: 0x0400080A RID: 2058
		internal const int DimensionsWithSingleAttributeThreshold = 10;

		// Token: 0x0400080B RID: 2059
		internal const int MeasureGroupsThreshold = 15;

		// Token: 0x0400080C RID: 2060
		internal const long LargeAttributeThreshold = 500000L;

		// Token: 0x0400080D RID: 2061
		internal const long LargePartitionEstimatedRowsThreshold = 20000000L;

		// Token: 0x0400080E RID: 2062
		internal const long LargePartitionEstimatedSizeThreshold = 262144000L;

		// Token: 0x0400080F RID: 2063
		internal const long SmallPartitionEstimatedRowsThreshold = 2000000L;

		// Token: 0x04000810 RID: 2064
		internal const long SmallPartitionEstimatedSizeThreshold = 52428800L;

		// Token: 0x04000811 RID: 2065
		internal const int SmallPartitionMaxCount = 5;

		// Token: 0x04000812 RID: 2066
		internal const int AggregationsPerPartitionThreshold = 500;

		// Token: 0x04000813 RID: 2067
		internal const int NonKeyLargeAttributeMembers = 1000000;

		// Token: 0x04000814 RID: 2068
		internal const int NonKeyLargeAttributePercent = 95;

		// Token: 0x04000815 RID: 2069
		internal const long ParentChildKeyMembersThreshold = 500000L;

		// Token: 0x04000816 RID: 2070
		internal const int ParentChildDimsWithOutlineCalcsThreshold = 3;
	}
}
