using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200004D RID: 77
	public static class EdmConstants
	{
		// Token: 0x04000074 RID: 116
		public static readonly Version EdmVersion4 = new Version(4, 0);

		// Token: 0x04000075 RID: 117
		public static readonly Version EdmVersionLatest = EdmConstants.EdmVersion4;

		// Token: 0x04000076 RID: 118
		internal const string EdmNamespace = "Edm";

		// Token: 0x04000077 RID: 119
		internal const string TransientNamespace = "Transient";

		// Token: 0x04000078 RID: 120
		internal const string XmlPrefix = "xml";

		// Token: 0x04000079 RID: 121
		internal const string XmlNamespacePrefix = "xmlns";

		// Token: 0x0400007A RID: 122
		internal const string DocumentationUri = "http://schemas.microsoft.com/ado/2011/04/edm/documentation";

		// Token: 0x0400007B RID: 123
		internal const string DocumentationAnnotation = "Documentation";

		// Token: 0x0400007C RID: 124
		internal const string InternalUri = "http://schemas.microsoft.com/ado/2011/04/edm/internal";

		// Token: 0x0400007D RID: 125
		internal const string EdmVersionAnnotation = "EdmVersion";

		// Token: 0x0400007E RID: 126
		internal const string FacetName_Nullable = "Nullable";

		// Token: 0x0400007F RID: 127
		internal const string FacetName_Precision = "Precision";

		// Token: 0x04000080 RID: 128
		internal const string FacetName_Scale = "Scale";

		// Token: 0x04000081 RID: 129
		internal const string FacetName_MaxLength = "MaxLength";

		// Token: 0x04000082 RID: 130
		internal const string FacetName_Unicode = "Unicode";

		// Token: 0x04000083 RID: 131
		internal const string FacetName_Collation = "Collation";

		// Token: 0x04000084 RID: 132
		internal const string FacetName_Srid = "SRID";

		// Token: 0x04000085 RID: 133
		internal const string Value_UnknownType = "UnknownType";

		// Token: 0x04000086 RID: 134
		internal const string Value_UnnamedType = "UnnamedType";

		// Token: 0x04000087 RID: 135
		internal const string Value_Max = "max";

		// Token: 0x04000088 RID: 136
		internal const string Value_SridVariable = "Variable";

		// Token: 0x04000089 RID: 137
		internal const string Value_ScaleVariable = "Variable";

		// Token: 0x0400008A RID: 138
		internal const string Type_Collection = "Collection";

		// Token: 0x0400008B RID: 139
		internal const string Type_Complex = "Complex";

		// Token: 0x0400008C RID: 140
		internal const string Type_Entity = "Entity";

		// Token: 0x0400008D RID: 141
		internal const string Type_EntityReference = "EntityReference";

		// Token: 0x0400008E RID: 142
		internal const string Type_Enum = "Enum";

		// Token: 0x0400008F RID: 143
		internal const string Type_TypeDefinition = "TypeDefinition";

		// Token: 0x04000090 RID: 144
		internal const string Type_Primitive = "Primitive";

		// Token: 0x04000091 RID: 145
		internal const string Type_Binary = "Binary";

		// Token: 0x04000092 RID: 146
		internal const string Type_Decimal = "Decimal";

		// Token: 0x04000093 RID: 147
		internal const string Type_String = "String";

		// Token: 0x04000094 RID: 148
		internal const string Type_Stream = "Stream";

		// Token: 0x04000095 RID: 149
		internal const string Type_Spatial = "Spatial";

		// Token: 0x04000096 RID: 150
		internal const string Type_Temporal = "Temporal";

		// Token: 0x04000097 RID: 151
		internal const string Type_Structured = "Structured";

		// Token: 0x04000098 RID: 152
		internal const int Max_Precision = 2147483647;

		// Token: 0x04000099 RID: 153
		internal const int Min_Precision = 0;

		// Token: 0x0400009A RID: 154
		internal const string MimeTypeAttributeName = "MimeType";
	}
}
