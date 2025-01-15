using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace MsolapWrapper
{
	// Token: 0x0200000E RID: 14
	internal sealed class SchemaCommandUtils
	{
		// Token: 0x06000139 RID: 313 RVA: 0x00009D08 File Offset: 0x00009108
		internal static string RewriteCsdlNamespace(string csdl)
		{
			string.IsNullOrEmpty(csdl);
			return new Regex(SchemaCommandUtils.EdmNamespacePattern).Replace(csdl, string.Empty);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000AC68 File Offset: 0x0000A068
		internal static SchemaCommandUtils.DiscoverSchemaParams GetMsolapDiscoverSchemaParams(string schemaName, IReadOnlyDictionary<string, object> restrictions, int restrictionsCount)
		{
			SchemaCommandUtils.DiscoverSchemaParams discoverSchemaParams = new SchemaCommandUtils.DiscoverSchemaParams();
			Guid schemaGuid = SchemaCommandUtils.GetSchemaGuid(schemaName);
			discoverSchemaParams.schemaGuid = schemaGuid;
			if (schemaName.Equals(SchemaCommandUtils.SchemaNames.FUNCTIONS_SCHEMANAME))
			{
				SchemaCommandUtils.BuildSchemaFunctionsRestrictions(restrictions, discoverSchemaParams, restrictionsCount);
			}
			else if (schemaName.Equals(SchemaCommandUtils.SchemaNames.KEYWORDS_SCHEMANAME))
			{
				if (restrictions != null)
				{
					int count = restrictions.Count;
				}
				discoverSchemaParams.restrictions = new object[restrictionsCount];
			}
			else if (schemaName.Equals(SchemaCommandUtils.SchemaNames.MDSCHEMA_CUBES))
			{
				SchemaCommandUtils.BuildMdSchemaCubesRestrictions(restrictions, discoverSchemaParams, restrictionsCount);
			}
			else if (schemaName.Equals(SchemaCommandUtils.SchemaNames.DISCOVER_CSDL_METADATA))
			{
				SchemaCommandUtils.BuildCsdlRestrictions(restrictions, discoverSchemaParams, restrictionsCount);
			}
			else
			{
				if (!schemaName.Equals(SchemaCommandUtils.SchemaNames.DISCOVER_XML_METADATA))
				{
					throw new NotImplementedException(WrapperContract.GetMessageInvariant("Schema command '{0}' not implemented in MsolapWrapper.", schemaName));
				}
				SchemaCommandUtils.BuildDiscoverXmlMetadataRestrictions(restrictions, discoverSchemaParams, restrictionsCount);
			}
			return discoverSchemaParams;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000A0B8 File Offset: 0x000094B8
		internal static Guid GetSchemaGuid(string schemaName)
		{
			if (schemaName.Equals(SchemaCommandUtils.SchemaNames.KEYWORDS_SCHEMANAME))
			{
				return SchemaGuid.Keywords;
			}
			if (schemaName.Equals(SchemaCommandUtils.SchemaNames.FUNCTIONS_SCHEMANAME))
			{
				return SchemaGuid.Functions;
			}
			if (schemaName.Equals(SchemaCommandUtils.SchemaNames.MDSCHEMA_CUBES))
			{
				return SchemaGuid.Cubes;
			}
			if (schemaName.Equals(SchemaCommandUtils.SchemaNames.DISCOVER_CSDL_METADATA))
			{
				return SchemaGuid.Csdl;
			}
			if (schemaName.Equals(SchemaCommandUtils.SchemaNames.DISCOVER_XML_METADATA))
			{
				return SchemaGuid.XmlMetadata;
			}
			throw new NotImplementedException(WrapperContract.GetMessageInvariant("Schema command '{0}' not implemented in MsolapWrapper.", schemaName));
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000AAA4 File Offset: 0x00009EA4
		private static void SetRestriction([MarshalAs(UnmanagedType.U1)] bool isRestrictionRequired, IReadOnlyDictionary<string, object> sourceRestrictions, string paramName, int paramIndex, object[] targetRestrictions)
		{
			object obj = null;
			int num = targetRestrictions.Length;
			if (paramIndex >= num)
			{
				if (isRestrictionRequired != null)
				{
					Utils.ThrowError(WrapperErrorSource.PowerBI, "Restriction index for '{0}' is both required and out of range (0 - {1})", paramName, num - 1);
				}
			}
			else
			{
				obj = null;
				if (sourceRestrictions.TryGetValue(paramName, out obj))
				{
					targetRestrictions[paramIndex] = obj;
				}
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000AB20 File Offset: 0x00009F20
		private static void BuildSchemaFunctionsRestrictions(IReadOnlyDictionary<string, object> restrictionsIn, SchemaCommandUtils.DiscoverSchemaParams paramsResult, int restrictionsCount)
		{
			object[] array = new object[restrictionsCount];
			paramsResult.restrictions = array;
			SchemaCommandUtils.SetRestriction(1, restrictionsIn, SchemaCommandUtils.RestrictionParamNames.ORIGIN, 3, array);
			SchemaCommandUtils.SetRestriction(0, restrictionsIn, SchemaCommandUtils.RestrictionParamNames.CATALOGNAME, 4, paramsResult.restrictions);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000AB64 File Offset: 0x00009F64
		private static void BuildMdSchemaCubesRestrictions(IReadOnlyDictionary<string, object> restrictionsIn, SchemaCommandUtils.DiscoverSchemaParams paramsResult, int restrictionsCount)
		{
			object[] array = new object[restrictionsCount];
			paramsResult.restrictions = array;
			SchemaCommandUtils.SetRestriction(1, restrictionsIn, SchemaCommandUtils.RestrictionParamNames.CATALOGNAME, 0, array);
			SchemaCommandUtils.SetRestriction(1, restrictionsIn, SchemaCommandUtils.RestrictionParamNames.CUBENAME, 1, paramsResult.restrictions);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000ABA8 File Offset: 0x00009FA8
		private static void BuildCsdlRestrictions(IReadOnlyDictionary<string, object> restrictionsIn, SchemaCommandUtils.DiscoverSchemaParams paramsResult, int restrictionsCount)
		{
			object[] array = new object[restrictionsCount];
			paramsResult.restrictions = array;
			SchemaCommandUtils.SetRestriction(1, restrictionsIn, SchemaCommandUtils.RestrictionParamNames.CATALOGNAME, 0, array);
			SchemaCommandUtils.SetRestriction(0, restrictionsIn, SchemaCommandUtils.RestrictionParamNames.PERSPECTIVENAME, 1, paramsResult.restrictions);
			SchemaCommandUtils.SetRestriction(0, restrictionsIn, SchemaCommandUtils.RestrictionParamNames.VERSION, 2, paramsResult.restrictions);
			SchemaCommandUtils.SetRestriction(0, restrictionsIn, SchemaCommandUtils.RestrictionParamNames.IGNORETRANSLATIONS, 3, paramsResult.restrictions);
			SchemaCommandUtils.SetRestriction(0, restrictionsIn, SchemaCommandUtils.RestrictionParamNames.PRINTALLTRANSLATIONS, 4, paramsResult.restrictions);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000AC24 File Offset: 0x0000A024
		private static void BuildDiscoverXmlMetadataRestrictions(IReadOnlyDictionary<string, object> restrictionsIn, SchemaCommandUtils.DiscoverSchemaParams paramsResult, int restrictionsCount)
		{
			object[] array = new object[restrictionsCount];
			paramsResult.restrictions = array;
			SchemaCommandUtils.SetRestriction(0, restrictionsIn, SchemaCommandUtils.RestrictionParamNames.OBJECTEXPANSION, 22, array);
			SchemaCommandUtils.SetRestriction(0, restrictionsIn, SchemaCommandUtils.RestrictionParamNames.DATABASEID, 0, paramsResult.restrictions);
		}

		// Token: 0x040000E9 RID: 233
		public static string EdmNamespacePattern = "xmlns:bi=[ \t\r\n]*\"http://schemas.microsoft.com/ado/2008/09/edm\"";

		// Token: 0x0200000F RID: 15
		internal class SchemaNames
		{
			// Token: 0x040000EA RID: 234
			public static string DISCOVER_CSDL_METADATA = "DISCOVER_CSDL_METADATA";

			// Token: 0x040000EB RID: 235
			public static string FUNCTIONS_SCHEMANAME = "MDSCHEMA_FUNCTIONS";

			// Token: 0x040000EC RID: 236
			public static string KEYWORDS_SCHEMANAME = "DISCOVER_KEYWORDS";

			// Token: 0x040000ED RID: 237
			public static string MDSCHEMA_CUBES = "MDSCHEMA_CUBES";

			// Token: 0x040000EE RID: 238
			public static string DISCOVER_XML_METADATA = "DISCOVER_XML_METADATA";
		}

		// Token: 0x02000010 RID: 16
		internal class RestrictionParamNames
		{
			// Token: 0x040000EF RID: 239
			public static string CATALOGNAME = "CATALOG_NAME";

			// Token: 0x040000F0 RID: 240
			public static string CUBENAME = "CUBE_NAME";

			// Token: 0x040000F1 RID: 241
			public static string ORIGIN = "ORIGIN";

			// Token: 0x040000F2 RID: 242
			public static string PERSPECTIVENAME = "PERSPECTIVE_NAME";

			// Token: 0x040000F3 RID: 243
			public static string VERSION = "VERSION";

			// Token: 0x040000F4 RID: 244
			public static string IGNORETRANSLATIONS = "IGNORE_TRANSLATIONS";

			// Token: 0x040000F5 RID: 245
			public static string PRINTALLTRANSLATIONS = "PRINT_ALL_TRANSLATIONS";

			// Token: 0x040000F6 RID: 246
			public static string OBJECTEXPANSION = "ObjectExpansion";

			// Token: 0x040000F7 RID: 247
			public static string DATABASEID = "DatabaseID";
		}

		// Token: 0x02000011 RID: 17
		internal class DiscoverSchemaParams
		{
			// Token: 0x040000F8 RID: 248
			public Guid schemaGuid;

			// Token: 0x040000F9 RID: 249
			public object[] restrictions;
		}
	}
}
