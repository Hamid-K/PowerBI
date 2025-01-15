using System;

namespace Microsoft.Mashup.Engine1.Library.SqlExpression
{
	// Token: 0x020003DF RID: 991
	internal class SqlExpressionModuleSource
	{
		// Token: 0x04000D54 RID: 3412
		public const string Source = "// Copyright (c) Microsoft Corporation.  All rights reserved.\r\n\r\nsection SqlExpression;\r\n\r\nshared SqlExpression.SchemaFrom = (schema) =>\r\nlet\r\n    NullNativeExpressions = Table.TransformColumns(schema, {{\"NativeDefaultExpression\", each null}, {\"NativeExpression\", each null}}),\r\n    RemoveNativeType = Table.RemoveColumns(NullNativeExpressions, {\"NativeTypeName\"}),\r\n    AddNativeType = Table.AddColumn(RemoveNativeType, \"NativeTypeName\", each\r\n        if ([Kind] = \"text\") then\r\n            if ([IsVariableLength] = false) then \"nchar\"\r\n            else \"nvarchar\"\r\n        else if ([Kind] = \"number\") then\r\n            if ([TypeName] = \"Decimal.Type\") then \"decimal\"\r\n            else if ([TypeName] = \"Currency.Type\") then \"money\"\r\n            else if ([TypeName] = \"Int64.Type\") then \"bigint\"\r\n            else if ([TypeName] = \"Int32.Type\") then \"int\"\r\n            else if ([TypeName] = \"Int16.Type\") then \"smallint\"\r\n            else if ([TypeName] = \"Int8.Type\") then \"tinyint\"\r\n            else \"double\"\r\n        else if ([Kind] = \"date\") then \"date\"\r\n        else if ([Kind] = \"datetime\") then \"datetime2\"\r\n        else if ([Kind] = \"datetimezone\") then \"datetimeoffset\"\r\n        else if ([Kind] = \"time\") then \"time\"\r\n        else if ([Kind] = \"logical\") then \"bit\"\r\n        else if ([Kind] = \"binary\") then\r\n            if ([IsVariableLength] = false) then \"binary\"\r\n            else \"varbinary\"\r\n        else null),\r\n    Reorder = Table.ReorderColumns(AddNativeType, Table.ColumnNames(schema))\r\nin\r\n    Reorder;\r\n\r\n    ";
	}
}
