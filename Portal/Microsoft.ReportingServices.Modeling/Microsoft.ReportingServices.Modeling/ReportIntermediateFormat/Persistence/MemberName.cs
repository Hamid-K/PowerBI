using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000015 RID: 21
	internal enum MemberName
	{
		// Token: 0x0400006D RID: 109
		IntermediateFormatVersionMajor,
		// Token: 0x0400006E RID: 110
		IntermediateFormatVersionMinor,
		// Token: 0x0400006F RID: 111
		IntermediateFormatVersionBuild,
		// Token: 0x04000070 RID: 112
		ID,
		// Token: 0x04000071 RID: 113
		Name,
		// Token: 0x04000072 RID: 114
		DataType,
		// Token: 0x04000073 RID: 115
		MaxLength,
		// Token: 0x04000074 RID: 116
		Ordinal,
		// Token: 0x04000075 RID: 117
		Table,
		// Token: 0x04000076 RID: 118
		ExtendedProperties,
		// Token: 0x04000077 RID: 119
		Flags,
		// Token: 0x04000078 RID: 120
		Dictionary,
		// Token: 0x04000079 RID: 121
		Columns,
		// Token: 0x0400007A RID: 122
		Constraints,
		// Token: 0x0400007B RID: 123
		PrimaryKey,
		// Token: 0x0400007C RID: 124
		SourceTable,
		// Token: 0x0400007D RID: 125
		TargetTable,
		// Token: 0x0400007E RID: 126
		SourceColumns,
		// Token: 0x0400007F RID: 127
		TargetColumns,
		// Token: 0x04000080 RID: 128
		SourceConstraint,
		// Token: 0x04000081 RID: 129
		TargetConstraint,
		// Token: 0x04000082 RID: 130
		Tables,
		// Token: 0x04000083 RID: 131
		Relations,
		// Token: 0x04000084 RID: 132
		CreatedTimeStamp,
		// Token: 0x04000085 RID: 133
		LastSchemaUpdate,
		// Token: 0x04000086 RID: 134
		DataSourceID,
		// Token: 0x04000087 RID: 135
		DsvSchema,
		// Token: 0x04000088 RID: 136
		Items,
		// Token: 0x04000089 RID: 137
		ModelItems,
		// Token: 0x0400008A RID: 138
		TableName,
		// Token: 0x0400008B RID: 139
		RelationEnd,
		// Token: 0x0400008C RID: 140
		Path,
		// Token: 0x0400008D RID: 141
		Node,
		// Token: 0x0400008E RID: 142
		InvalidTargetRef,
		// Token: 0x0400008F RID: 143
		SkipSecurityFilter,
		// Token: 0x04000090 RID: 144
		CompiledResultType,
		// Token: 0x04000091 RID: 145
		Compiled,
		// Token: 0x04000092 RID: 146
		CustomProperties,
		// Token: 0x04000093 RID: 147
		Cardinality,
		// Token: 0x04000094 RID: 148
		Nullable,
		// Token: 0x04000095 RID: 149
		EntityKeyTarget,
		// Token: 0x04000096 RID: 150
		Values,
		// Token: 0x04000097 RID: 151
		Arguments,
		// Token: 0x04000098 RID: 152
		CompiledFunctionInfoID,
		// Token: 0x04000099 RID: 153
		Attribute,
		// Token: 0x0400009A RID: 154
		Entity,
		// Token: 0x0400009B RID: 155
		Role,
		// Token: 0x0400009C RID: 156
		SourceEntity,
		// Token: 0x0400009D RID: 157
		TargetEntity,
		// Token: 0x0400009E RID: 158
		QName_Name,
		// Token: 0x0400009F RID: 159
		QName_Namespace,
		// Token: 0x040000A0 RID: 160
		Value,
		// Token: 0x040000A1 RID: 161
		ReadOnly,
		// Token: 0x040000A2 RID: 162
		Description,
		// Token: 0x040000A3 RID: 163
		Hidden,
		// Token: 0x040000A4 RID: 164
		Fields,
		// Token: 0x040000A5 RID: 165
		AutoName,
		// Token: 0x040000A6 RID: 166
		Variations,
		// Token: 0x040000A7 RID: 167
		SingularName,
		// Token: 0x040000A8 RID: 168
		PluralName,
		// Token: 0x040000A9 RID: 169
		RelatedRole,
		// Token: 0x040000AA RID: 170
		Optionality,
		// Token: 0x040000AB RID: 171
		ContextualName,
		// Token: 0x040000AC RID: 172
		Preferred,
		// Token: 0x040000AD RID: 173
		PromoteLookup,
		// Token: 0x040000AE RID: 174
		ExpandInline,
		// Token: 0x040000AF RID: 175
		HiddenFields,
		// Token: 0x040000B0 RID: 176
		Binding,
		// Token: 0x040000B1 RID: 177
		Expression,
		// Token: 0x040000B2 RID: 178
		Aggregate,
		// Token: 0x040000B3 RID: 179
		Filter,
		// Token: 0x040000B4 RID: 180
		OmitSecurityFilters,
		// Token: 0x040000B5 RID: 181
		SortDirection,
		// Token: 0x040000B6 RID: 182
		Width,
		// Token: 0x040000B7 RID: 183
		Alignment,
		// Token: 0x040000B8 RID: 184
		Format,
		// Token: 0x040000B9 RID: 185
		MimeType,
		// Token: 0x040000BA RID: 186
		DataCulture,
		// Token: 0x040000BB RID: 187
		DiscourageGrouping,
		// Token: 0x040000BC RID: 188
		EnableDrillthrough,
		// Token: 0x040000BD RID: 189
		DefaultAggAttr,
		// Token: 0x040000BE RID: 190
		ValueSelection,
		// Token: 0x040000BF RID: 191
		AttributeReference,
		// Token: 0x040000C0 RID: 192
		InheritsFrom,
		// Token: 0x040000C1 RID: 193
		CollectionName,
		// Token: 0x040000C2 RID: 194
		InstanceSelection,
		// Token: 0x040000C3 RID: 195
		Lookup,
		// Token: 0x040000C4 RID: 196
		Inheritance,
		// Token: 0x040000C5 RID: 197
		DisjointInheritance,
		// Token: 0x040000C6 RID: 198
		IdentifyingAttrs,
		// Token: 0x040000C7 RID: 199
		DefaultDetailAttrs,
		// Token: 0x040000C8 RID: 200
		DefaultAggregateAttrs,
		// Token: 0x040000C9 RID: 201
		SortAttrs,
		// Token: 0x040000CA RID: 202
		SecurityFilters,
		// Token: 0x040000CB RID: 203
		DefaultSecurityFilter,
		// Token: 0x040000CC RID: 204
		Entities,
		// Token: 0x040000CD RID: 205
		Version,
		// Token: 0x040000CE RID: 206
		Culture,
		// Token: 0x040000CF RID: 207
		DataSourceView,
		// Token: 0x040000D0 RID: 208
		Perspectives,
		// Token: 0x040000D1 RID: 209
		NamespacePrefixes,
		// Token: 0x040000D2 RID: 210
		Names,
		// Token: 0x040000D3 RID: 211
		Namespaces
	}
}
