using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000430 RID: 1072
	[CompilerGenerated]
	internal class RDLValidatingReaderStrings
	{
		// Token: 0x060022C8 RID: 8904 RVA: 0x000025F4 File Offset: 0x000007F4
		protected RDLValidatingReaderStrings()
		{
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x060022C9 RID: 8905 RVA: 0x000829C0 File Offset: 0x00080BC0
		// (set) Token: 0x060022CA RID: 8906 RVA: 0x000829C7 File Offset: 0x00080BC7
		public static CultureInfo Culture
		{
			get
			{
				return RDLValidatingReaderStrings.Keys.Culture;
			}
			set
			{
				RDLValidatingReaderStrings.Keys.Culture = value;
			}
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x000829CF File Offset: 0x00080BCF
		public static string rdlValidationMissingChildElement(string parentType, string childType, string linenumber, string position)
		{
			return RDLValidatingReaderStrings.Keys.GetString("rdlValidationMissingChildElement", parentType, childType, linenumber, position);
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x000829DF File Offset: 0x00080BDF
		public static string rdlValidationInvalidElement(string parentType, string childType, string linenumber, string position)
		{
			return RDLValidatingReaderStrings.Keys.GetString("rdlValidationInvalidElement", parentType, childType, linenumber, position);
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x000829EF File Offset: 0x00080BEF
		public static string rdlValidationInvalidParent(string childType, string childNs, string parentType, string linenumber, string position)
		{
			return RDLValidatingReaderStrings.Keys.GetString("rdlValidationInvalidParent", childType, childNs, parentType, linenumber, position);
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x00082A01 File Offset: 0x00080C01
		public static string rdlValidationNoElementDecl(string elementExpandedName, string element, string elementNs, string linenumber)
		{
			return RDLValidatingReaderStrings.Keys.GetString("rdlValidationNoElementDecl", elementExpandedName, element, elementNs, linenumber);
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x00082A11 File Offset: 0x00080C11
		public static string rdlValidationInvalidNamespaceElement(string elementExpandedName, string nameSpace, string linenumber, string position)
		{
			return RDLValidatingReaderStrings.Keys.GetString("rdlValidationInvalidNamespaceElement", elementExpandedName, nameSpace, linenumber, position);
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x00082A21 File Offset: 0x00080C21
		public static string rdlValidationInvalidNamespaceAttribute(string attributeExpandedName, string nameSpace, string linenumber, string position)
		{
			return RDLValidatingReaderStrings.Keys.GetString("rdlValidationInvalidNamespaceAttribute", attributeExpandedName, nameSpace, linenumber, position);
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x00082A31 File Offset: 0x00080C31
		public static string rdlValidationInvalidMicroVersionedElement(string elementName, string parentName, string linenumber, string position)
		{
			return RDLValidatingReaderStrings.Keys.GetString("rdlValidationInvalidMicroVersionedElement", elementName, parentName, linenumber, position);
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x00082A41 File Offset: 0x00080C41
		public static string rdlValidationInvalidMicroVersionedAttribute(string attributeName, string parentName, string linenumber, string position)
		{
			return RDLValidatingReaderStrings.Keys.GetString("rdlValidationInvalidMicroVersionedAttribute", attributeName, parentName, linenumber, position);
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x00082A51 File Offset: 0x00080C51
		public static string rdlValidationUnsupportedSchema(string objectType, string objectName, string linenumber, string position)
		{
			return RDLValidatingReaderStrings.Keys.GetString("rdlValidationUnsupportedSchema", objectType, objectName, linenumber, position);
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x00082A61 File Offset: 0x00080C61
		public static string rdlValidationUndefinedSchemaNamespace(string objectType, string objectName, string linenumber, string position)
		{
			return RDLValidatingReaderStrings.Keys.GetString("rdlValidationUndefinedSchemaNamespace", objectType, objectName, linenumber, position);
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x00082A71 File Offset: 0x00080C71
		public static string rdlValidationMultipleUndefinedSchemaNamespaces(string objectType, string objectName, string linenumber, string position)
		{
			return RDLValidatingReaderStrings.Keys.GetString("rdlValidationMultipleUndefinedSchemaNamespaces", objectType, objectName, linenumber, position);
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x00082A81 File Offset: 0x00080C81
		public static string rdlValidationUnknownRequiredNamespaces(string xmlns, string prefix, string sqlServerVersionName, string linenumber, string position)
		{
			return RDLValidatingReaderStrings.Keys.GetString("rdlValidationUnknownRequiredNamespaces", xmlns, prefix, sqlServerVersionName, linenumber, position);
		}

		// Token: 0x02000531 RID: 1329
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x0600254E RID: 9550 RVA: 0x000025F4 File Offset: 0x000007F4
			private Keys()
			{
			}

			// Token: 0x17000ABF RID: 2751
			// (get) Token: 0x0600254F RID: 9551 RVA: 0x00087F41 File Offset: 0x00086141
			// (set) Token: 0x06002550 RID: 9552 RVA: 0x00087F48 File Offset: 0x00086148
			public static CultureInfo Culture
			{
				get
				{
					return RDLValidatingReaderStrings.Keys._culture;
				}
				set
				{
					RDLValidatingReaderStrings.Keys._culture = value;
				}
			}

			// Token: 0x06002551 RID: 9553 RVA: 0x00087F50 File Offset: 0x00086150
			public static string GetString(string key)
			{
				return RDLValidatingReaderStrings.Keys.resourceManager.GetString(key, RDLValidatingReaderStrings.Keys._culture);
			}

			// Token: 0x06002552 RID: 9554 RVA: 0x00087F62 File Offset: 0x00086162
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3)
			{
				return string.Format(CultureInfo.CurrentCulture, RDLValidatingReaderStrings.Keys.resourceManager.GetString(key, RDLValidatingReaderStrings.Keys._culture), new object[] { arg0, arg1, arg2, arg3 });
			}

			// Token: 0x06002553 RID: 9555 RVA: 0x00087F95 File Offset: 0x00086195
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3, object arg4)
			{
				return string.Format(CultureInfo.CurrentCulture, RDLValidatingReaderStrings.Keys.resourceManager.GetString(key, RDLValidatingReaderStrings.Keys._culture), new object[] { arg0, arg1, arg2, arg3, arg4 });
			}

			// Token: 0x04001369 RID: 4969
			private static ResourceManager resourceManager = new ResourceManager(typeof(RDLValidatingReaderStrings).FullName, typeof(RDLValidatingReaderStrings).Module.Assembly);

			// Token: 0x0400136A RID: 4970
			private static CultureInfo _culture = null;

			// Token: 0x0400136B RID: 4971
			public const string rdlValidationMissingChildElement = "rdlValidationMissingChildElement";

			// Token: 0x0400136C RID: 4972
			public const string rdlValidationInvalidElement = "rdlValidationInvalidElement";

			// Token: 0x0400136D RID: 4973
			public const string rdlValidationInvalidParent = "rdlValidationInvalidParent";

			// Token: 0x0400136E RID: 4974
			public const string rdlValidationNoElementDecl = "rdlValidationNoElementDecl";

			// Token: 0x0400136F RID: 4975
			public const string rdlValidationInvalidNamespaceElement = "rdlValidationInvalidNamespaceElement";

			// Token: 0x04001370 RID: 4976
			public const string rdlValidationInvalidNamespaceAttribute = "rdlValidationInvalidNamespaceAttribute";

			// Token: 0x04001371 RID: 4977
			public const string rdlValidationInvalidMicroVersionedElement = "rdlValidationInvalidMicroVersionedElement";

			// Token: 0x04001372 RID: 4978
			public const string rdlValidationInvalidMicroVersionedAttribute = "rdlValidationInvalidMicroVersionedAttribute";

			// Token: 0x04001373 RID: 4979
			public const string rdlValidationUnsupportedSchema = "rdlValidationUnsupportedSchema";

			// Token: 0x04001374 RID: 4980
			public const string rdlValidationUndefinedSchemaNamespace = "rdlValidationUndefinedSchemaNamespace";

			// Token: 0x04001375 RID: 4981
			public const string rdlValidationMultipleUndefinedSchemaNamespaces = "rdlValidationMultipleUndefinedSchemaNamespaces";

			// Token: 0x04001376 RID: 4982
			public const string rdlValidationUnknownRequiredNamespaces = "rdlValidationUnknownRequiredNamespaces";
		}
	}
}
