using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001CC RID: 460
	internal class RDLValidatingReaderStringsWrapper
	{
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x00024674 File Offset: 0x00022874
		// (set) Token: 0x06000EF7 RID: 3831 RVA: 0x0002467B File Offset: 0x0002287B
		public static CultureInfo Culture
		{
			get
			{
				return RDLValidatingReaderStringsWrapper.Keys.Culture;
			}
			set
			{
				RDLValidatingReaderStringsWrapper.Keys.Culture = value;
			}
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00024683 File Offset: 0x00022883
		public static string rdlValidationMissingChildElement(string parentType, string childType, string linenumber, string position)
		{
			return RDLValidatingReaderStringsWrapper.Keys.GetString("rdlValidationMissingChildElement", parentType, childType, linenumber, position);
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x00024693 File Offset: 0x00022893
		public static string rdlValidationInvalidElement(string parentType, string childType, string linenumber, string position)
		{
			return RDLValidatingReaderStringsWrapper.Keys.GetString("rdlValidationInvalidElement", parentType, childType, linenumber, position);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x000246A3 File Offset: 0x000228A3
		public static string rdlValidationInvalidParent(string childType, string childNs, string parentType, string linenumber, string position)
		{
			return RDLValidatingReaderStringsWrapper.Keys.GetString("rdlValidationInvalidParent", childType, childNs, parentType, linenumber, position);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x000246B5 File Offset: 0x000228B5
		public static string rdlValidationNoElementDecl(string elementExpandedName, string element, string elementNs, string linenumber)
		{
			return RDLValidatingReaderStringsWrapper.Keys.GetString("rdlValidationNoElementDecl", elementExpandedName, element, elementNs, linenumber);
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x000246C5 File Offset: 0x000228C5
		public static string rdlValidationInvalidNamespaceElement(string elementExpandedName, string nameSpace, string linenumber, string position)
		{
			return RDLValidatingReaderStringsWrapper.Keys.GetString("rdlValidationInvalidNamespaceElement", elementExpandedName, nameSpace, linenumber, position);
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x000246D5 File Offset: 0x000228D5
		public static string rdlValidationInvalidNamespaceAttribute(string attributeExpandedName, string nameSpace, string linenumber, string position)
		{
			return RDLValidatingReaderStringsWrapper.Keys.GetString("rdlValidationInvalidNamespaceAttribute", attributeExpandedName, nameSpace, linenumber, position);
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x000246E5 File Offset: 0x000228E5
		public static string rdlValidationInvalidMicroVersionedElement(string elementName, string parentName, string linenumber, string position)
		{
			return RDLValidatingReaderStringsWrapper.Keys.GetString("rdlValidationInvalidMicroVersionedElement", elementName, parentName, linenumber, position);
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x000246F5 File Offset: 0x000228F5
		public static string rdlValidationInvalidMicroVersionedAttribute(string attributeName, string parentName, string linenumber, string position)
		{
			return RDLValidatingReaderStringsWrapper.Keys.GetString("rdlValidationInvalidMicroVersionedAttribute", attributeName, parentName, linenumber, position);
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00024705 File Offset: 0x00022905
		public static string rdlValidationUnsupportedSchema(string objectType, string objectName, string linenumber, string position)
		{
			return RDLValidatingReaderStringsWrapper.Keys.GetString("rdlValidationUnsupportedSchema", objectType, objectName, linenumber, position);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00024715 File Offset: 0x00022915
		public static string rdlValidationUndefinedSchemaNamespace(string objectType, string objectName, string linenumber, string position)
		{
			return RDLValidatingReaderStringsWrapper.Keys.GetString("rdlValidationUndefinedSchemaNamespace", objectType, objectName, linenumber, position);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00024725 File Offset: 0x00022925
		public static string rdlValidationMultipleUndefinedSchemaNamespaces(string objectType, string objectName, string linenumber, string position)
		{
			return RDLValidatingReaderStringsWrapper.Keys.GetString("rdlValidationMultipleUndefinedSchemaNamespaces", objectType, objectName, linenumber, position);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x00024735 File Offset: 0x00022935
		public static string rdlValidationUnknownRequiredNamespaces(string xmlns, string prefix, string sqlServerVersionName, string linenumber, string position)
		{
			return RDLValidatingReaderStringsWrapper.Keys.GetString("rdlValidationUnknownRequiredNamespaces", xmlns, prefix, sqlServerVersionName, linenumber, position);
		}

		// Token: 0x020003E3 RID: 995
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x0600189F RID: 6303 RVA: 0x0003BA49 File Offset: 0x00039C49
			private Keys()
			{
			}

			// Token: 0x1700074C RID: 1868
			// (get) Token: 0x060018A0 RID: 6304 RVA: 0x0003BA51 File Offset: 0x00039C51
			// (set) Token: 0x060018A1 RID: 6305 RVA: 0x0003BA58 File Offset: 0x00039C58
			public static CultureInfo Culture
			{
				get
				{
					return RDLValidatingReaderStringsWrapper.Keys._culture;
				}
				set
				{
					RDLValidatingReaderStringsWrapper.Keys._culture = value;
				}
			}

			// Token: 0x060018A2 RID: 6306 RVA: 0x0003BA60 File Offset: 0x00039C60
			public static string GetString(string key)
			{
				return RDLValidatingReaderStringsWrapper.Keys.resourceManager.GetString(key, RDLValidatingReaderStringsWrapper.Keys._culture);
			}

			// Token: 0x060018A3 RID: 6307 RVA: 0x0003BA72 File Offset: 0x00039C72
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3)
			{
				return string.Format(CultureInfo.CurrentCulture, RDLValidatingReaderStringsWrapper.Keys.resourceManager.GetString(key, RDLValidatingReaderStringsWrapper.Keys._culture), new object[] { arg0, arg1, arg2, arg3 });
			}

			// Token: 0x060018A4 RID: 6308 RVA: 0x0003BAA5 File Offset: 0x00039CA5
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3, object arg4)
			{
				return string.Format(CultureInfo.CurrentCulture, RDLValidatingReaderStringsWrapper.Keys.resourceManager.GetString(key, RDLValidatingReaderStringsWrapper.Keys._culture), new object[] { arg0, arg1, arg2, arg3, arg4 });
			}

			// Token: 0x040007A2 RID: 1954
			private static ResourceManager resourceManager = RDLValidatingReaderStrings.ResourceManager;

			// Token: 0x040007A3 RID: 1955
			private static CultureInfo _culture = null;

			// Token: 0x040007A4 RID: 1956
			public const string rdlValidationMissingChildElement = "rdlValidationMissingChildElement";

			// Token: 0x040007A5 RID: 1957
			public const string rdlValidationInvalidElement = "rdlValidationInvalidElement";

			// Token: 0x040007A6 RID: 1958
			public const string rdlValidationInvalidParent = "rdlValidationInvalidParent";

			// Token: 0x040007A7 RID: 1959
			public const string rdlValidationNoElementDecl = "rdlValidationNoElementDecl";

			// Token: 0x040007A8 RID: 1960
			public const string rdlValidationInvalidNamespaceElement = "rdlValidationInvalidNamespaceElement";

			// Token: 0x040007A9 RID: 1961
			public const string rdlValidationInvalidNamespaceAttribute = "rdlValidationInvalidNamespaceAttribute";

			// Token: 0x040007AA RID: 1962
			public const string rdlValidationInvalidMicroVersionedElement = "rdlValidationInvalidMicroVersionedElement";

			// Token: 0x040007AB RID: 1963
			public const string rdlValidationInvalidMicroVersionedAttribute = "rdlValidationInvalidMicroVersionedAttribute";

			// Token: 0x040007AC RID: 1964
			public const string rdlValidationUnsupportedSchema = "rdlValidationUnsupportedSchema";

			// Token: 0x040007AD RID: 1965
			public const string rdlValidationUndefinedSchemaNamespace = "rdlValidationUndefinedSchemaNamespace";

			// Token: 0x040007AE RID: 1966
			public const string rdlValidationMultipleUndefinedSchemaNamespaces = "rdlValidationMultipleUndefinedSchemaNamespaces";

			// Token: 0x040007AF RID: 1967
			public const string rdlValidationUnknownRequiredNamespaces = "rdlValidationUnknownRequiredNamespaces";
		}
	}
}
