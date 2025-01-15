using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x02000156 RID: 342
	public static class CsdlConstants
	{
		// Token: 0x04000501 RID: 1281
		public static readonly Version EdmxVersion4 = EdmConstants.EdmVersion4;

		// Token: 0x04000502 RID: 1282
		public static readonly Version EdmxVersion401 = EdmConstants.EdmVersion401;

		// Token: 0x04000503 RID: 1283
		public static readonly Version EdmxVersionLatest = CsdlConstants.EdmxVersion401;

		// Token: 0x04000504 RID: 1284
		internal const string CsdlFileExtension = ".csdl";

		// Token: 0x04000505 RID: 1285
		internal const string EdmOasisNamespace = "http://docs.oasis-open.org/odata/ns/edm";

		// Token: 0x04000506 RID: 1286
		internal const string SchemaNamespaceAnnotation = "SchemaNamespace";

		// Token: 0x04000507 RID: 1287
		internal const string AnnotationSerializationLocationAnnotation = "AnnotationSerializationLocation";

		// Token: 0x04000508 RID: 1288
		internal const string NamespacePrefixAnnotation = "NamespacePrefix";

		// Token: 0x04000509 RID: 1289
		internal const string IsEnumMemberValueExplicitAnnotation = "IsEnumMemberValueExplicit";

		// Token: 0x0400050A RID: 1290
		internal const string IsSerializedAsElementAnnotation = "IsSerializedAsElement";

		// Token: 0x0400050B RID: 1291
		internal const string NamespaceAliasAnnotation = "NamespaceAlias";

		// Token: 0x0400050C RID: 1292
		internal const string UsedNamespacesAnnotation = "UsedNamespaces";

		// Token: 0x0400050D RID: 1293
		internal const string ReferencesAnnotation = "References";

		// Token: 0x0400050E RID: 1294
		internal const string PrimitiveValueConverterMapAnnotation = "PrimitiveValueConverterMap";

		// Token: 0x0400050F RID: 1295
		internal const string Attribute_Abstract = "Abstract";

		// Token: 0x04000510 RID: 1296
		internal const string Attribute_Action = "Action";

		// Token: 0x04000511 RID: 1297
		internal const string Attribute_Alias = "Alias";

		// Token: 0x04000512 RID: 1298
		internal const string Attribute_AnnotationPath = "AnnotationPath";

		// Token: 0x04000513 RID: 1299
		internal const string Attribute_AppliesTo = "AppliesTo";

		// Token: 0x04000514 RID: 1300
		internal const string Attribute_BaseType = "BaseType";

		// Token: 0x04000515 RID: 1301
		internal const string Attribute_Binary = "Binary";

		// Token: 0x04000516 RID: 1302
		internal const string Attribute_Bool = "Bool";

		// Token: 0x04000517 RID: 1303
		internal const string Attribute_ContainsTarget = "ContainsTarget";

		// Token: 0x04000518 RID: 1304
		internal const string Attribute_Date = "Date";

		// Token: 0x04000519 RID: 1305
		internal const string Attribute_DateTimeOffset = "DateTimeOffset";

		// Token: 0x0400051A RID: 1306
		internal const string Attribute_Decimal = "Decimal";

		// Token: 0x0400051B RID: 1307
		internal const string Attribute_DefaultValue = "DefaultValue";

		// Token: 0x0400051C RID: 1308
		internal const string Attribute_ElementType = "ElementType";

		// Token: 0x0400051D RID: 1309
		internal const string Attribute_Extends = "Extends";

		// Token: 0x0400051E RID: 1310
		internal const string Attribute_EntityType = "EntityType";

		// Token: 0x0400051F RID: 1311
		internal const string Attribute_EntitySet = "EntitySet";

		// Token: 0x04000520 RID: 1312
		internal const string Attribute_EntitySetPath = "EntitySetPath";

		// Token: 0x04000521 RID: 1313
		internal const string Attribute_EnumMember = "EnumMember";

		// Token: 0x04000522 RID: 1314
		internal const string Attribute_Float = "Float";

		// Token: 0x04000523 RID: 1315
		internal const string Attribute_Function = "Function";

		// Token: 0x04000524 RID: 1316
		internal const string Attribute_Guid = "Guid";

		// Token: 0x04000525 RID: 1317
		internal const string Attribute_HasStream = "HasStream";

		// Token: 0x04000526 RID: 1318
		internal const string Attribute_Int = "Int";

		// Token: 0x04000527 RID: 1319
		internal const string Attribute_IncludeInServiceDocument = "IncludeInServiceDocument";

		// Token: 0x04000528 RID: 1320
		internal const string Attribute_IsBound = "IsBound";

		// Token: 0x04000529 RID: 1321
		internal const string Attribute_IsComposable = "IsComposable";

		// Token: 0x0400052A RID: 1322
		internal const string Attribute_IsFlags = "IsFlags";

		// Token: 0x0400052B RID: 1323
		internal const string Attribute_MaxLength = "MaxLength";

		// Token: 0x0400052C RID: 1324
		internal const string Attribute_Name = "Name";

		// Token: 0x0400052D RID: 1325
		internal const string Attribute_Namespace = "Namespace";

		// Token: 0x0400052E RID: 1326
		internal const string Attribute_NavigationPropertyPath = "NavigationPropertyPath";

		// Token: 0x0400052F RID: 1327
		internal const string Attribute_Nullable = "Nullable";

		// Token: 0x04000530 RID: 1328
		internal const string Attribute_OpenType = "OpenType";

		// Token: 0x04000531 RID: 1329
		internal const string Attribute_Partner = "Partner";

		// Token: 0x04000532 RID: 1330
		internal const string Attribute_Path = "Path";

		// Token: 0x04000533 RID: 1331
		internal const string Attribute_Precision = "Precision";

		// Token: 0x04000534 RID: 1332
		internal const string Attribute_Property = "Property";

		// Token: 0x04000535 RID: 1333
		internal const string Attribute_PropertyPath = "PropertyPath";

		// Token: 0x04000536 RID: 1334
		internal const string Attribute_ReferencedProperty = "ReferencedProperty";

		// Token: 0x04000537 RID: 1335
		internal const string Attribute_Qualifier = "Qualifier";

		// Token: 0x04000538 RID: 1336
		internal const string Attribute_Scale = "Scale";

		// Token: 0x04000539 RID: 1337
		internal const string Attribute_Srid = "SRID";

		// Token: 0x0400053A RID: 1338
		internal const string Attribute_String = "String";

		// Token: 0x0400053B RID: 1339
		internal const string Attribute_Target = "Target";

		// Token: 0x0400053C RID: 1340
		internal const string Attribute_Term = "Term";

		// Token: 0x0400053D RID: 1341
		internal const string Attribute_Duration = "Duration";

		// Token: 0x0400053E RID: 1342
		internal const string Attribute_TimeOfDay = "TimeOfDay";

		// Token: 0x0400053F RID: 1343
		internal const string Attribute_Type = "Type";

		// Token: 0x04000540 RID: 1344
		internal const string Attribute_UnderlyingType = "UnderlyingType";

		// Token: 0x04000541 RID: 1345
		internal const string Attribute_Unicode = "Unicode";

		// Token: 0x04000542 RID: 1346
		internal const string Attribute_Value = "Value";

		// Token: 0x04000543 RID: 1347
		internal const string Element_Action = "Action";

		// Token: 0x04000544 RID: 1348
		internal const string Element_ActionImport = "ActionImport";

		// Token: 0x04000545 RID: 1349
		internal const string Element_Annotation = "Annotation";

		// Token: 0x04000546 RID: 1350
		internal const string Element_Annotations = "Annotations";

		// Token: 0x04000547 RID: 1351
		internal const string Element_Apply = "Apply";

		// Token: 0x04000548 RID: 1352
		internal const string Element_Binary = "Binary";

		// Token: 0x04000549 RID: 1353
		internal const string Element_Bool = "Bool";

		// Token: 0x0400054A RID: 1354
		internal const string Element_Cast = "Cast";

		// Token: 0x0400054B RID: 1355
		internal const string Element_Collection = "Collection";

		// Token: 0x0400054C RID: 1356
		internal const string Element_CollectionType = "CollectionType";

		// Token: 0x0400054D RID: 1357
		internal const string Element_ComplexType = "ComplexType";

		// Token: 0x0400054E RID: 1358
		internal const string Element_Date = "Date";

		// Token: 0x0400054F RID: 1359
		internal const string Element_DateTimeOffset = "DateTimeOffset";

		// Token: 0x04000550 RID: 1360
		internal const string Element_Decimal = "Decimal";

		// Token: 0x04000551 RID: 1361
		internal const string Element_EntityContainer = "EntityContainer";

		// Token: 0x04000552 RID: 1362
		internal const string Element_EntitySet = "EntitySet";

		// Token: 0x04000553 RID: 1363
		internal const string Element_EntitySetReference = "EntitySetReference";

		// Token: 0x04000554 RID: 1364
		internal const string Element_EntityType = "EntityType";

		// Token: 0x04000555 RID: 1365
		internal const string Element_EnumMember = "EnumMember";

		// Token: 0x04000556 RID: 1366
		internal const string Element_EnumType = "EnumType";

		// Token: 0x04000557 RID: 1367
		internal const string Element_Float = "Float";

		// Token: 0x04000558 RID: 1368
		internal const string Element_Guid = "Guid";

		// Token: 0x04000559 RID: 1369
		internal const string Element_Function = "Function";

		// Token: 0x0400055A RID: 1370
		internal const string Element_FunctionImport = "FunctionImport";

		// Token: 0x0400055B RID: 1371
		internal const string Element_FunctionReference = "FunctionReference";

		// Token: 0x0400055C RID: 1372
		internal const string Element_If = "If";

		// Token: 0x0400055D RID: 1373
		internal const string Element_IsType = "IsType";

		// Token: 0x0400055E RID: 1374
		internal const string Element_Int = "Int";

		// Token: 0x0400055F RID: 1375
		internal const string Element_Key = "Key";

		// Token: 0x04000560 RID: 1376
		internal const string Element_LabeledElement = "LabeledElement";

		// Token: 0x04000561 RID: 1377
		internal const string Element_LabeledElementReference = "LabeledElementReference";

		// Token: 0x04000562 RID: 1378
		internal const string Element_LongDescription = "LongDescription";

		// Token: 0x04000563 RID: 1379
		internal const string Element_Member = "Member";

		// Token: 0x04000564 RID: 1380
		internal const string Element_NavigationProperty = "NavigationProperty";

		// Token: 0x04000565 RID: 1381
		internal const string Element_NavigationPropertyBinding = "NavigationPropertyBinding";

		// Token: 0x04000566 RID: 1382
		internal const string Element_NavigationPropertyPath = "NavigationPropertyPath";

		// Token: 0x04000567 RID: 1383
		internal const string Element_Null = "Null";

		// Token: 0x04000568 RID: 1384
		internal const string Element_OnDelete = "OnDelete";

		// Token: 0x04000569 RID: 1385
		internal const string Element_Parameter = "Parameter";

		// Token: 0x0400056A RID: 1386
		internal const string Element_ParameterReference = "ParameterReference";

		// Token: 0x0400056B RID: 1387
		internal const string Element_Path = "Path";

		// Token: 0x0400056C RID: 1388
		internal const string Element_Property = "Property";

		// Token: 0x0400056D RID: 1389
		internal const string Element_PropertyPath = "PropertyPath";

		// Token: 0x0400056E RID: 1390
		internal const string Element_PropertyRef = "PropertyRef";

		// Token: 0x0400056F RID: 1391
		internal const string Element_PropertyReference = "PropertyReference";

		// Token: 0x04000570 RID: 1392
		internal const string Element_PropertyValue = "PropertyValue";

		// Token: 0x04000571 RID: 1393
		internal const string Element_Record = "Record";

		// Token: 0x04000572 RID: 1394
		internal const string Element_ReferenceType = "ReferenceType";

		// Token: 0x04000573 RID: 1395
		internal const string Element_ReferentialConstraint = "ReferentialConstraint";

		// Token: 0x04000574 RID: 1396
		internal const string Element_ReturnType = "ReturnType";

		// Token: 0x04000575 RID: 1397
		internal const string Element_Singleton = "Singleton";

		// Token: 0x04000576 RID: 1398
		internal const string Element_Schema = "Schema";

		// Token: 0x04000577 RID: 1399
		internal const string Element_String = "String";

		// Token: 0x04000578 RID: 1400
		internal const string Element_Summary = "Summary";

		// Token: 0x04000579 RID: 1401
		internal const string Element_Duration = "Duration";

		// Token: 0x0400057A RID: 1402
		internal const string Element_Term = "Term";

		// Token: 0x0400057B RID: 1403
		internal const string Element_TimeOfDay = "TimeOfDay";

		// Token: 0x0400057C RID: 1404
		internal const string Element_TypeDefinition = "TypeDefinition";

		// Token: 0x0400057D RID: 1405
		internal const string Element_TypeRef = "TypeRef";

		// Token: 0x0400057E RID: 1406
		internal const string Value_Cascade = "Cascade";

		// Token: 0x0400057F RID: 1407
		internal const string Value_Collection = "Collection";

		// Token: 0x04000580 RID: 1408
		internal const string Value_EndMany = "*";

		// Token: 0x04000581 RID: 1409
		internal const string Value_EndOptional = "0..1";

		// Token: 0x04000582 RID: 1410
		internal const string Value_EndRequired = "1";

		// Token: 0x04000583 RID: 1411
		internal const string Value_Max = "max";

		// Token: 0x04000584 RID: 1412
		internal const string Value_None = "None";

		// Token: 0x04000585 RID: 1413
		internal const string Value_Ref = "Ref";

		// Token: 0x04000586 RID: 1414
		internal const string Value_SridVariable = "Variable";

		// Token: 0x04000587 RID: 1415
		internal const string Value_ScaleVariable = "Variable";

		// Token: 0x04000588 RID: 1416
		internal const string TypeName_Untyped = "Edm.Untyped";

		// Token: 0x04000589 RID: 1417
		internal const string TypeName_Untyped_Short = "Untyped";

		// Token: 0x0400058A RID: 1418
		internal const string TypeName_Entity = "Edm.EntityType";

		// Token: 0x0400058B RID: 1419
		internal const string TypeName_Entity_Short = "EntityType";

		// Token: 0x0400058C RID: 1420
		internal const string TypeName_Complex = "Edm.ComplexType";

		// Token: 0x0400058D RID: 1421
		internal const string TypeName_Complex_Short = "ComplexType";

		// Token: 0x0400058E RID: 1422
		internal const bool Default_Abstract = false;

		// Token: 0x0400058F RID: 1423
		internal const bool Default_ContainsTarget = false;

		// Token: 0x04000590 RID: 1424
		internal const bool Default_HasStream = false;

		// Token: 0x04000591 RID: 1425
		internal const bool Default_IncludeInServiceDocument = false;

		// Token: 0x04000592 RID: 1426
		internal const bool Default_IsAtomic = false;

		// Token: 0x04000593 RID: 1427
		internal const bool Default_IsBound = false;

		// Token: 0x04000594 RID: 1428
		internal const bool Default_IsComposable = false;

		// Token: 0x04000595 RID: 1429
		internal const bool Default_IsFlags = false;

		// Token: 0x04000596 RID: 1430
		internal const bool Default_OpenType = false;

		// Token: 0x04000597 RID: 1431
		internal const bool Default_Nullable = true;

		// Token: 0x04000598 RID: 1432
		internal const bool Default_IsUnicode = true;

		// Token: 0x04000599 RID: 1433
		internal const int Default_TemporalPrecision = 0;

		// Token: 0x0400059A RID: 1434
		internal const int Default_SpatialGeographySrid = 4326;

		// Token: 0x0400059B RID: 1435
		internal const int Default_SpatialGeometrySrid = 0;

		// Token: 0x0400059C RID: 1436
		internal const int Default_UnspecifiedSrid = -2147483648;

		// Token: 0x0400059D RID: 1437
		internal const int Default_Scale = 0;

		// Token: 0x0400059E RID: 1438
		internal const int Max_NameLength = 480;

		// Token: 0x0400059F RID: 1439
		internal const int Max_NamespaceLength = 512;

		// Token: 0x040005A0 RID: 1440
		internal const string EdmxFileExtension = ".edmx";

		// Token: 0x040005A1 RID: 1441
		internal const string EdmxOasisNamespace = "http://docs.oasis-open.org/odata/ns/edmx";

		// Token: 0x040005A2 RID: 1442
		internal const string ODataMetadataNamespace = "http://docs.oasis-open.org/odata/ns/metadata";

		// Token: 0x040005A3 RID: 1443
		internal const string EdmxVersionAnnotation = "EdmxVersion";

		// Token: 0x040005A4 RID: 1444
		internal const string Prefix_Edmx = "edmx";

		// Token: 0x040005A5 RID: 1445
		internal const string Prefix_ODataMetadata = "m";

		// Token: 0x040005A6 RID: 1446
		internal const string Attribute_TargetNamespace = "TargetNamespace";

		// Token: 0x040005A7 RID: 1447
		internal const string Attribute_TermNamespace = "TermNamespace";

		// Token: 0x040005A8 RID: 1448
		internal const string Attribute_Version = "Version";

		// Token: 0x040005A9 RID: 1449
		internal const string Attribute_Uri = "Uri";

		// Token: 0x040005AA RID: 1450
		internal const string Element_ConceptualModels = "ConceptualModels";

		// Token: 0x040005AB RID: 1451
		internal const string Element_Edmx = "Edmx";

		// Token: 0x040005AC RID: 1452
		internal const string Element_Runtime = "Runtime";

		// Token: 0x040005AD RID: 1453
		internal const string Element_Reference = "Reference";

		// Token: 0x040005AE RID: 1454
		internal const string Element_Include = "Include";

		// Token: 0x040005AF RID: 1455
		internal const string Element_IncludeAnnotations = "IncludeAnnotations";

		// Token: 0x040005B0 RID: 1456
		internal const string Element_DataServices = "DataServices";

		// Token: 0x040005B1 RID: 1457
		internal const string OperationReturnExternalTarget = "$ReturnType";

		// Token: 0x040005B2 RID: 1458
		internal static Dictionary<Version, string[]> SupportedVersions = new Dictionary<Version, string[]>
		{
			{
				EdmConstants.EdmVersion4,
				new string[] { "http://docs.oasis-open.org/odata/ns/edm" }
			},
			{
				EdmConstants.EdmVersion401,
				new string[] { "http://docs.oasis-open.org/odata/ns/edm" }
			}
		};

		// Token: 0x040005B3 RID: 1459
		internal static Dictionary<Version, string> SupportedEdmxVersions = new Dictionary<Version, string>
		{
			{
				CsdlConstants.EdmxVersion4,
				"http://docs.oasis-open.org/odata/ns/edmx"
			},
			{
				CsdlConstants.EdmxVersion401,
				"http://docs.oasis-open.org/odata/ns/edmx"
			}
		};

		// Token: 0x040005B4 RID: 1460
		internal static Dictionary<string, Version> SupportedEdmxNamespaces = new Dictionary<string, Version> { 
		{
			"http://docs.oasis-open.org/odata/ns/edmx",
			CsdlConstants.EdmxVersion4
		} };

		// Token: 0x040005B5 RID: 1461
		internal static Dictionary<Version, Version> EdmToEdmxVersions = new Dictionary<Version, Version>
		{
			{
				EdmConstants.EdmVersion4,
				CsdlConstants.EdmxVersion4
			},
			{
				EdmConstants.EdmVersion401,
				CsdlConstants.EdmxVersion401
			}
		};

		// Token: 0x040005B6 RID: 1462
		internal static Dictionary<Version, Version> EdmxToEdmVersions = CsdlConstants.EdmToEdmxVersions.ToDictionary((KeyValuePair<Version, Version> v) => v.Value, (KeyValuePair<Version, Version> v) => v.Key);
	}
}
