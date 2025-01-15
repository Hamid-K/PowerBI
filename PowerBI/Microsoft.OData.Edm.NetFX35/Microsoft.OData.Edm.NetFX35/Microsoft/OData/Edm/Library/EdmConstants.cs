using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000213 RID: 531
	public static class EdmConstants
	{
		// Token: 0x0400059C RID: 1436
		internal const string EdmNamespace = "Edm";

		// Token: 0x0400059D RID: 1437
		internal const string TransientNamespace = "Transient";

		// Token: 0x0400059E RID: 1438
		internal const string XmlPrefix = "xml";

		// Token: 0x0400059F RID: 1439
		internal const string XmlNamespacePrefix = "xmlns";

		// Token: 0x040005A0 RID: 1440
		internal const string DocumentationUri = "http://schemas.microsoft.com/ado/2011/04/edm/documentation";

		// Token: 0x040005A1 RID: 1441
		internal const string DocumentationAnnotation = "Documentation";

		// Token: 0x040005A2 RID: 1442
		internal const string InternalUri = "http://schemas.microsoft.com/ado/2011/04/edm/internal";

		// Token: 0x040005A3 RID: 1443
		internal const string EdmVersionAnnotation = "EdmVersion";

		// Token: 0x040005A4 RID: 1444
		internal const string FacetName_Nullable = "Nullable";

		// Token: 0x040005A5 RID: 1445
		internal const string FacetName_Precision = "Precision";

		// Token: 0x040005A6 RID: 1446
		internal const string FacetName_Scale = "Scale";

		// Token: 0x040005A7 RID: 1447
		internal const string FacetName_MaxLength = "MaxLength";

		// Token: 0x040005A8 RID: 1448
		internal const string FacetName_Unicode = "Unicode";

		// Token: 0x040005A9 RID: 1449
		internal const string FacetName_Collation = "Collation";

		// Token: 0x040005AA RID: 1450
		internal const string FacetName_Srid = "SRID";

		// Token: 0x040005AB RID: 1451
		internal const string Value_UnknownType = "UnknownType";

		// Token: 0x040005AC RID: 1452
		internal const string Value_UnnamedType = "UnnamedType";

		// Token: 0x040005AD RID: 1453
		internal const string Value_Max = "max";

		// Token: 0x040005AE RID: 1454
		internal const string Value_SridVariable = "Variable";

		// Token: 0x040005AF RID: 1455
		internal const string Value_ScaleVariable = "Variable";

		// Token: 0x040005B0 RID: 1456
		internal const string Type_Collection = "Collection";

		// Token: 0x040005B1 RID: 1457
		internal const string Type_Complex = "Complex";

		// Token: 0x040005B2 RID: 1458
		internal const string Type_Entity = "Entity";

		// Token: 0x040005B3 RID: 1459
		internal const string Type_EntityReference = "EntityReference";

		// Token: 0x040005B4 RID: 1460
		internal const string Type_Enum = "Enum";

		// Token: 0x040005B5 RID: 1461
		internal const string Type_TypeDefinition = "TypeDefinition";

		// Token: 0x040005B6 RID: 1462
		internal const string Type_Primitive = "Primitive";

		// Token: 0x040005B7 RID: 1463
		internal const string Type_Binary = "Binary";

		// Token: 0x040005B8 RID: 1464
		internal const string Type_Decimal = "Decimal";

		// Token: 0x040005B9 RID: 1465
		internal const string Type_String = "String";

		// Token: 0x040005BA RID: 1466
		internal const string Type_Stream = "Stream";

		// Token: 0x040005BB RID: 1467
		internal const string Type_Spatial = "Spatial";

		// Token: 0x040005BC RID: 1468
		internal const string Type_Temporal = "Temporal";

		// Token: 0x040005BD RID: 1469
		internal const string Type_Structured = "Structured";

		// Token: 0x040005BE RID: 1470
		internal const int Max_Precision = 2147483647;

		// Token: 0x040005BF RID: 1471
		internal const int Min_Precision = 0;

		// Token: 0x040005C0 RID: 1472
		internal const string MimeTypeAttributeName = "MimeType";

		// Token: 0x040005C1 RID: 1473
		public static readonly Version EdmVersion4 = new Version(4, 0);

		// Token: 0x040005C2 RID: 1474
		public static readonly Version EdmVersionLatest = EdmConstants.EdmVersion4;
	}
}
