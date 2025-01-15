using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000CB RID: 203
	public static class EdmConstants
	{
		// Token: 0x0400017E RID: 382
		public static readonly Version EdmVersion4 = new Version(4, 0);

		// Token: 0x0400017F RID: 383
		public static readonly Version EdmVersion401 = new Version(4, 1);

		// Token: 0x04000180 RID: 384
		public static readonly Version EdmVersionLatest = EdmConstants.EdmVersion401;

		// Token: 0x04000181 RID: 385
		public static Version EdmVersionDefault = EdmConstants.EdmVersion4;

		// Token: 0x04000182 RID: 386
		internal const string EdmVersion401String = "4.01";

		// Token: 0x04000183 RID: 387
		internal const string EdmNamespace = "Edm";

		// Token: 0x04000184 RID: 388
		internal const string TransientNamespace = "Transient";

		// Token: 0x04000185 RID: 389
		internal const string XmlPrefix = "xml";

		// Token: 0x04000186 RID: 390
		internal const string XmlNamespacePrefix = "xmlns";

		// Token: 0x04000187 RID: 391
		internal const string InternalUri = "http://schemas.microsoft.com/ado/2011/04/edm/internal";

		// Token: 0x04000188 RID: 392
		internal const string EdmVersionAnnotation = "EdmVersion";

		// Token: 0x04000189 RID: 393
		internal const string FacetName_Nullable = "Nullable";

		// Token: 0x0400018A RID: 394
		internal const string FacetName_Precision = "Precision";

		// Token: 0x0400018B RID: 395
		internal const string FacetName_Scale = "Scale";

		// Token: 0x0400018C RID: 396
		internal const string FacetName_MaxLength = "MaxLength";

		// Token: 0x0400018D RID: 397
		internal const string FacetName_Unicode = "Unicode";

		// Token: 0x0400018E RID: 398
		internal const string FacetName_Collation = "Collation";

		// Token: 0x0400018F RID: 399
		internal const string FacetName_Srid = "SRID";

		// Token: 0x04000190 RID: 400
		internal const string Value_UnknownType = "UnknownType";

		// Token: 0x04000191 RID: 401
		internal const string Value_UnnamedType = "UnnamedType";

		// Token: 0x04000192 RID: 402
		internal const string Value_Max = "max";

		// Token: 0x04000193 RID: 403
		internal const string Value_SridVariable = "Variable";

		// Token: 0x04000194 RID: 404
		internal const string Value_ScaleVariable = "Variable";

		// Token: 0x04000195 RID: 405
		internal const string Type_Collection = "Collection";

		// Token: 0x04000196 RID: 406
		internal const string Type_Complex = "Complex";

		// Token: 0x04000197 RID: 407
		internal const string Type_Entity = "Entity";

		// Token: 0x04000198 RID: 408
		internal const string Type_EntityReference = "EntityReference";

		// Token: 0x04000199 RID: 409
		internal const string Type_Enum = "Enum";

		// Token: 0x0400019A RID: 410
		internal const string Type_TypeDefinition = "TypeDefinition";

		// Token: 0x0400019B RID: 411
		internal const string Type_Path = "Path";

		// Token: 0x0400019C RID: 412
		internal const string Type_Primitive = "Primitive";

		// Token: 0x0400019D RID: 413
		internal const string Type_Binary = "Binary";

		// Token: 0x0400019E RID: 414
		internal const string Type_Decimal = "Decimal";

		// Token: 0x0400019F RID: 415
		internal const string Type_String = "String";

		// Token: 0x040001A0 RID: 416
		internal const string Type_Stream = "Stream";

		// Token: 0x040001A1 RID: 417
		internal const string Type_Spatial = "Spatial";

		// Token: 0x040001A2 RID: 418
		internal const string Type_Temporal = "Temporal";

		// Token: 0x040001A3 RID: 419
		internal const string Type_Structured = "Structured";

		// Token: 0x040001A4 RID: 420
		internal const int Max_Precision = 2147483647;

		// Token: 0x040001A5 RID: 421
		internal const int Min_Precision = 0;

		// Token: 0x040001A6 RID: 422
		internal const string MimeTypeAttributeName = "MimeType";
	}
}
