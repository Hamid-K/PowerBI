using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x02000146 RID: 326
	public static class CsdlConstants
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x00015134 File Offset: 0x00013334
		// Note: this type is marked as 'beforefieldinit'.
		static CsdlConstants()
		{
			Dictionary<Version, string[]> dictionary = new Dictionary<Version, string[]>();
			dictionary.Add(EdmConstants.EdmVersion4, new string[] { "http://docs.oasis-open.org/odata/ns/edm" });
			CsdlConstants.SupportedVersions = dictionary;
			Dictionary<Version, string> dictionary2 = new Dictionary<Version, string>();
			dictionary2.Add(CsdlConstants.EdmxVersion4, "http://docs.oasis-open.org/odata/ns/edmx");
			CsdlConstants.SupportedEdmxVersions = dictionary2;
			CsdlConstants.SupportedEdmxNamespaces = Enumerable.ToDictionary<KeyValuePair<Version, string>, string, Version>(CsdlConstants.SupportedEdmxVersions, (KeyValuePair<Version, string> v) => v.Value, (KeyValuePair<Version, string> v) => v.Key);
			Dictionary<Version, Version> dictionary3 = new Dictionary<Version, Version>();
			dictionary3.Add(EdmConstants.EdmVersion4, CsdlConstants.EdmxVersion4);
			CsdlConstants.EdmToEdmxVersions = dictionary3;
		}

		// Token: 0x04000485 RID: 1157
		public static readonly Version EdmxVersion4 = EdmConstants.EdmVersion4;

		// Token: 0x04000486 RID: 1158
		public static readonly Version EdmxVersionLatest = CsdlConstants.EdmxVersion4;

		// Token: 0x04000487 RID: 1159
		internal const string CsdlFileExtension = ".csdl";

		// Token: 0x04000488 RID: 1160
		internal const string EdmOasisNamespace = "http://docs.oasis-open.org/odata/ns/edm";

		// Token: 0x04000489 RID: 1161
		internal const string SchemaNamespaceAnnotation = "SchemaNamespace";

		// Token: 0x0400048A RID: 1162
		internal const string AnnotationSerializationLocationAnnotation = "AnnotationSerializationLocation";

		// Token: 0x0400048B RID: 1163
		internal const string NamespacePrefixAnnotation = "NamespacePrefix";

		// Token: 0x0400048C RID: 1164
		internal const string IsEnumMemberValueExplicitAnnotation = "IsEnumMemberValueExplicit";

		// Token: 0x0400048D RID: 1165
		internal const string IsSerializedAsElementAnnotation = "IsSerializedAsElement";

		// Token: 0x0400048E RID: 1166
		internal const string NamespaceAliasAnnotation = "NamespaceAlias";

		// Token: 0x0400048F RID: 1167
		internal const string UsedNamespacesAnnotation = "UsedNamespaces";

		// Token: 0x04000490 RID: 1168
		internal const string ReferencesAnnotation = "References";

		// Token: 0x04000491 RID: 1169
		internal const string PrimitiveValueConverterMapAnnotation = "PrimitiveValueConverterMap";

		// Token: 0x04000492 RID: 1170
		internal const string Attribute_Abstract = "Abstract";

		// Token: 0x04000493 RID: 1171
		internal const string Attribute_Action = "Action";

		// Token: 0x04000494 RID: 1172
		internal const string Attribute_Alias = "Alias";

		// Token: 0x04000495 RID: 1173
		internal const string Attribute_AnnotationPath = "AnnotationPath";

		// Token: 0x04000496 RID: 1174
		internal const string Attribute_AppliesTo = "AppliesTo";

		// Token: 0x04000497 RID: 1175
		internal const string Attribute_BaseType = "BaseType";

		// Token: 0x04000498 RID: 1176
		internal const string Attribute_Binary = "Binary";

		// Token: 0x04000499 RID: 1177
		internal const string Attribute_Bool = "Bool";

		// Token: 0x0400049A RID: 1178
		internal const string Attribute_ContainsTarget = "ContainsTarget";

		// Token: 0x0400049B RID: 1179
		internal const string Attribute_Date = "Date";

		// Token: 0x0400049C RID: 1180
		internal const string Attribute_DateTimeOffset = "DateTimeOffset";

		// Token: 0x0400049D RID: 1181
		internal const string Attribute_Decimal = "Decimal";

		// Token: 0x0400049E RID: 1182
		internal const string Attribute_DefaultValue = "DefaultValue";

		// Token: 0x0400049F RID: 1183
		internal const string Attribute_ElementType = "ElementType";

		// Token: 0x040004A0 RID: 1184
		internal const string Attribute_Extends = "Extends";

		// Token: 0x040004A1 RID: 1185
		internal const string Attribute_EntityType = "EntityType";

		// Token: 0x040004A2 RID: 1186
		internal const string Attribute_EntitySet = "EntitySet";

		// Token: 0x040004A3 RID: 1187
		internal const string Attribute_EntitySetPath = "EntitySetPath";

		// Token: 0x040004A4 RID: 1188
		internal const string Attribute_EnumMember = "EnumMember";

		// Token: 0x040004A5 RID: 1189
		internal const string Attribute_Float = "Float";

		// Token: 0x040004A6 RID: 1190
		internal const string Attribute_Function = "Function";

		// Token: 0x040004A7 RID: 1191
		internal const string Attribute_Guid = "Guid";

		// Token: 0x040004A8 RID: 1192
		internal const string Attribute_HasStream = "HasStream";

		// Token: 0x040004A9 RID: 1193
		internal const string Attribute_Int = "Int";

		// Token: 0x040004AA RID: 1194
		internal const string Attribute_IncludeInServiceDocument = "IncludeInServiceDocument";

		// Token: 0x040004AB RID: 1195
		internal const string Attribute_IsBound = "IsBound";

		// Token: 0x040004AC RID: 1196
		internal const string Attribute_IsComposable = "IsComposable";

		// Token: 0x040004AD RID: 1197
		internal const string Attribute_IsFlags = "IsFlags";

		// Token: 0x040004AE RID: 1198
		internal const string Attribute_MaxLength = "MaxLength";

		// Token: 0x040004AF RID: 1199
		internal const string Attribute_Name = "Name";

		// Token: 0x040004B0 RID: 1200
		internal const string Attribute_Namespace = "Namespace";

		// Token: 0x040004B1 RID: 1201
		internal const string Attribute_NavigationPropertyPath = "NavigationPropertyPath";

		// Token: 0x040004B2 RID: 1202
		internal const string Attribute_Nullable = "Nullable";

		// Token: 0x040004B3 RID: 1203
		internal const string Attribute_OpenType = "OpenType";

		// Token: 0x040004B4 RID: 1204
		internal const string Attribute_Partner = "Partner";

		// Token: 0x040004B5 RID: 1205
		internal const string Attribute_Path = "Path";

		// Token: 0x040004B6 RID: 1206
		internal const string Attribute_Precision = "Precision";

		// Token: 0x040004B7 RID: 1207
		internal const string Attribute_Property = "Property";

		// Token: 0x040004B8 RID: 1208
		internal const string Attribute_PropertyPath = "PropertyPath";

		// Token: 0x040004B9 RID: 1209
		internal const string Attribute_ReferencedProperty = "ReferencedProperty";

		// Token: 0x040004BA RID: 1210
		internal const string Attribute_Qualifier = "Qualifier";

		// Token: 0x040004BB RID: 1211
		internal const string Attribute_Scale = "Scale";

		// Token: 0x040004BC RID: 1212
		internal const string Attribute_Srid = "SRID";

		// Token: 0x040004BD RID: 1213
		internal const string Attribute_String = "String";

		// Token: 0x040004BE RID: 1214
		internal const string Attribute_Target = "Target";

		// Token: 0x040004BF RID: 1215
		internal const string Attribute_Term = "Term";

		// Token: 0x040004C0 RID: 1216
		internal const string Attribute_Duration = "Duration";

		// Token: 0x040004C1 RID: 1217
		internal const string Attribute_TimeOfDay = "TimeOfDay";

		// Token: 0x040004C2 RID: 1218
		internal const string Attribute_Type = "Type";

		// Token: 0x040004C3 RID: 1219
		internal const string Attribute_UnderlyingType = "UnderlyingType";

		// Token: 0x040004C4 RID: 1220
		internal const string Attribute_Unicode = "Unicode";

		// Token: 0x040004C5 RID: 1221
		internal const string Attribute_Value = "Value";

		// Token: 0x040004C6 RID: 1222
		internal const string Element_Action = "Action";

		// Token: 0x040004C7 RID: 1223
		internal const string Element_ActionImport = "ActionImport";

		// Token: 0x040004C8 RID: 1224
		internal const string Element_Annotation = "Annotation";

		// Token: 0x040004C9 RID: 1225
		internal const string Element_Annotations = "Annotations";

		// Token: 0x040004CA RID: 1226
		internal const string Element_Apply = "Apply";

		// Token: 0x040004CB RID: 1227
		internal const string Element_Binary = "Binary";

		// Token: 0x040004CC RID: 1228
		internal const string Element_Bool = "Bool";

		// Token: 0x040004CD RID: 1229
		internal const string Element_Cast = "Cast";

		// Token: 0x040004CE RID: 1230
		internal const string Element_Collection = "Collection";

		// Token: 0x040004CF RID: 1231
		internal const string Element_CollectionType = "CollectionType";

		// Token: 0x040004D0 RID: 1232
		internal const string Element_ComplexType = "ComplexType";

		// Token: 0x040004D1 RID: 1233
		internal const string Element_Date = "Date";

		// Token: 0x040004D2 RID: 1234
		internal const string Element_DateTimeOffset = "DateTimeOffset";

		// Token: 0x040004D3 RID: 1235
		internal const string Element_Decimal = "Decimal";

		// Token: 0x040004D4 RID: 1236
		internal const string Element_Documentation = "Documentation";

		// Token: 0x040004D5 RID: 1237
		internal const string Element_EntityContainer = "EntityContainer";

		// Token: 0x040004D6 RID: 1238
		internal const string Element_EntitySet = "EntitySet";

		// Token: 0x040004D7 RID: 1239
		internal const string Element_EntitySetReference = "EntitySetReference";

		// Token: 0x040004D8 RID: 1240
		internal const string Element_EntityType = "EntityType";

		// Token: 0x040004D9 RID: 1241
		internal const string Element_EnumMember = "EnumMember";

		// Token: 0x040004DA RID: 1242
		internal const string Element_EnumType = "EnumType";

		// Token: 0x040004DB RID: 1243
		internal const string Element_Float = "Float";

		// Token: 0x040004DC RID: 1244
		internal const string Element_Guid = "Guid";

		// Token: 0x040004DD RID: 1245
		internal const string Element_Function = "Function";

		// Token: 0x040004DE RID: 1246
		internal const string Element_FunctionImport = "FunctionImport";

		// Token: 0x040004DF RID: 1247
		internal const string Element_FunctionReference = "FunctionReference";

		// Token: 0x040004E0 RID: 1248
		internal const string Element_If = "If";

		// Token: 0x040004E1 RID: 1249
		internal const string Element_IsType = "IsType";

		// Token: 0x040004E2 RID: 1250
		internal const string Element_Int = "Int";

		// Token: 0x040004E3 RID: 1251
		internal const string Element_Key = "Key";

		// Token: 0x040004E4 RID: 1252
		internal const string Element_LabeledElement = "LabeledElement";

		// Token: 0x040004E5 RID: 1253
		internal const string Element_LabeledElementReference = "LabeledElementReference";

		// Token: 0x040004E6 RID: 1254
		internal const string Element_LongDescription = "LongDescription";

		// Token: 0x040004E7 RID: 1255
		internal const string Element_Member = "Member";

		// Token: 0x040004E8 RID: 1256
		internal const string Element_NavigationProperty = "NavigationProperty";

		// Token: 0x040004E9 RID: 1257
		internal const string Element_NavigationPropertyBinding = "NavigationPropertyBinding";

		// Token: 0x040004EA RID: 1258
		internal const string Element_NavigationPropertyPath = "NavigationPropertyPath";

		// Token: 0x040004EB RID: 1259
		internal const string Element_Null = "Null";

		// Token: 0x040004EC RID: 1260
		internal const string Element_OnDelete = "OnDelete";

		// Token: 0x040004ED RID: 1261
		internal const string Element_Parameter = "Parameter";

		// Token: 0x040004EE RID: 1262
		internal const string Element_ParameterReference = "ParameterReference";

		// Token: 0x040004EF RID: 1263
		internal const string Element_Path = "Path";

		// Token: 0x040004F0 RID: 1264
		internal const string Element_Property = "Property";

		// Token: 0x040004F1 RID: 1265
		internal const string Element_PropertyPath = "PropertyPath";

		// Token: 0x040004F2 RID: 1266
		internal const string Element_PropertyRef = "PropertyRef";

		// Token: 0x040004F3 RID: 1267
		internal const string Element_PropertyReference = "PropertyReference";

		// Token: 0x040004F4 RID: 1268
		internal const string Element_PropertyValue = "PropertyValue";

		// Token: 0x040004F5 RID: 1269
		internal const string Element_Record = "Record";

		// Token: 0x040004F6 RID: 1270
		internal const string Element_ReferenceType = "ReferenceType";

		// Token: 0x040004F7 RID: 1271
		internal const string Element_ReferentialConstraint = "ReferentialConstraint";

		// Token: 0x040004F8 RID: 1272
		internal const string Element_ReturnType = "ReturnType";

		// Token: 0x040004F9 RID: 1273
		internal const string Element_Singleton = "Singleton";

		// Token: 0x040004FA RID: 1274
		internal const string Element_Schema = "Schema";

		// Token: 0x040004FB RID: 1275
		internal const string Element_String = "String";

		// Token: 0x040004FC RID: 1276
		internal const string Element_Summary = "Summary";

		// Token: 0x040004FD RID: 1277
		internal const string Element_Duration = "Duration";

		// Token: 0x040004FE RID: 1278
		internal const string Element_Term = "Term";

		// Token: 0x040004FF RID: 1279
		internal const string Element_TimeOfDay = "TimeOfDay";

		// Token: 0x04000500 RID: 1280
		internal const string Element_TypeDefinition = "TypeDefinition";

		// Token: 0x04000501 RID: 1281
		internal const string Element_TypeRef = "TypeRef";

		// Token: 0x04000502 RID: 1282
		internal const string Value_Cascade = "Cascade";

		// Token: 0x04000503 RID: 1283
		internal const string Value_Collection = "Collection";

		// Token: 0x04000504 RID: 1284
		internal const string Value_EndMany = "*";

		// Token: 0x04000505 RID: 1285
		internal const string Value_EndOptional = "0..1";

		// Token: 0x04000506 RID: 1286
		internal const string Value_EndRequired = "1";

		// Token: 0x04000507 RID: 1287
		internal const string Value_Max = "max";

		// Token: 0x04000508 RID: 1288
		internal const string Value_None = "None";

		// Token: 0x04000509 RID: 1289
		internal const string Value_Ref = "Ref";

		// Token: 0x0400050A RID: 1290
		internal const string Value_SridVariable = "Variable";

		// Token: 0x0400050B RID: 1291
		internal const string Value_ScaleVariable = "Variable";

		// Token: 0x0400050C RID: 1292
		internal const string TypeName_Untyped = "Edm.Untyped";

		// Token: 0x0400050D RID: 1293
		internal const string TypeName_Untyped_Short = "Untyped";

		// Token: 0x0400050E RID: 1294
		internal const bool Default_Abstract = false;

		// Token: 0x0400050F RID: 1295
		internal const bool Default_ContainsTarget = false;

		// Token: 0x04000510 RID: 1296
		internal const bool Default_HasStream = false;

		// Token: 0x04000511 RID: 1297
		internal const bool Default_IncludeInServiceDocument = false;

		// Token: 0x04000512 RID: 1298
		internal const bool Default_IsAtomic = false;

		// Token: 0x04000513 RID: 1299
		internal const bool Default_IsBound = false;

		// Token: 0x04000514 RID: 1300
		internal const bool Default_IsComposable = false;

		// Token: 0x04000515 RID: 1301
		internal const bool Default_IsFlags = false;

		// Token: 0x04000516 RID: 1302
		internal const bool Default_OpenType = false;

		// Token: 0x04000517 RID: 1303
		internal const bool Default_Nullable = true;

		// Token: 0x04000518 RID: 1304
		internal const bool Default_IsUnicode = true;

		// Token: 0x04000519 RID: 1305
		internal const int Default_TemporalPrecision = 0;

		// Token: 0x0400051A RID: 1306
		internal const int Default_SpatialGeographySrid = 4326;

		// Token: 0x0400051B RID: 1307
		internal const int Default_SpatialGeometrySrid = 0;

		// Token: 0x0400051C RID: 1308
		internal const int Default_UnspecifiedSrid = -2147483648;

		// Token: 0x0400051D RID: 1309
		internal const int Default_Scale = 0;

		// Token: 0x0400051E RID: 1310
		internal const int Max_NameLength = 480;

		// Token: 0x0400051F RID: 1311
		internal const int Max_NamespaceLength = 512;

		// Token: 0x04000520 RID: 1312
		internal const string EdmxFileExtension = ".edmx";

		// Token: 0x04000521 RID: 1313
		internal const string EdmxOasisNamespace = "http://docs.oasis-open.org/odata/ns/edmx";

		// Token: 0x04000522 RID: 1314
		internal const string ODataMetadataNamespace = "http://docs.oasis-open.org/odata/ns/metadata";

		// Token: 0x04000523 RID: 1315
		internal const string EdmxVersionAnnotation = "EdmxVersion";

		// Token: 0x04000524 RID: 1316
		internal const string Prefix_Edmx = "edmx";

		// Token: 0x04000525 RID: 1317
		internal const string Prefix_ODataMetadata = "m";

		// Token: 0x04000526 RID: 1318
		internal const string Attribute_TargetNamespace = "TargetNamespace";

		// Token: 0x04000527 RID: 1319
		internal const string Attribute_TermNamespace = "TermNamespace";

		// Token: 0x04000528 RID: 1320
		internal const string Attribute_Version = "Version";

		// Token: 0x04000529 RID: 1321
		internal const string Attribute_Uri = "Uri";

		// Token: 0x0400052A RID: 1322
		internal const string Element_ConceptualModels = "ConceptualModels";

		// Token: 0x0400052B RID: 1323
		internal const string Element_Edmx = "Edmx";

		// Token: 0x0400052C RID: 1324
		internal const string Element_Runtime = "Runtime";

		// Token: 0x0400052D RID: 1325
		internal const string Element_Reference = "Reference";

		// Token: 0x0400052E RID: 1326
		internal const string Element_Include = "Include";

		// Token: 0x0400052F RID: 1327
		internal const string Element_IncludeAnnotations = "IncludeAnnotations";

		// Token: 0x04000530 RID: 1328
		internal const string Element_DataServices = "DataServices";

		// Token: 0x04000531 RID: 1329
		internal static Dictionary<Version, string[]> SupportedVersions;

		// Token: 0x04000532 RID: 1330
		internal static Dictionary<Version, string> SupportedEdmxVersions;

		// Token: 0x04000533 RID: 1331
		internal static Dictionary<string, Version> SupportedEdmxNamespaces;

		// Token: 0x04000534 RID: 1332
		internal static Dictionary<Version, Version> EdmToEdmxVersions;
	}
}
