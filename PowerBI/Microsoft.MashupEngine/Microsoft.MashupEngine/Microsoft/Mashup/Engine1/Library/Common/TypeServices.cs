using System;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200115C RID: 4444
	internal static class TypeServices
	{
		// Token: 0x0600745C RID: 29788 RVA: 0x0018F868 File Offset: 0x0018DA68
		public static RecordTypeValue ConvertToMoreColumns(RecordTypeValue recordType, string moreColumnsName)
		{
			RecordValue recordValue = RecordValue.New(TypeServices.MoreColumnsKeys, new Value[] { TextValue.New(moreColumnsName) });
			return BinaryOperator.AddMeta.Invoke(recordType, recordValue).AsType.AsRecordType;
		}

		// Token: 0x0600745D RID: 29789 RVA: 0x0018F8A5 File Offset: 0x0018DAA5
		public static TypeValue SetDomain(TypeValue type, ListValue values)
		{
			return BinaryOperator.AddMeta.Invoke(type, RecordValue.New(TypeServices.DomainKeys, new Value[] { values })).AsType;
		}

		// Token: 0x0600745E RID: 29790 RVA: 0x0018F8CC File Offset: 0x0018DACC
		public static Value GetDomain(TypeValue type)
		{
			Value value;
			if (type.TryGetMetaField("DomainValues", out value) && value.IsList)
			{
				return value;
			}
			return Value.Null;
		}

		// Token: 0x0600745F RID: 29791 RVA: 0x0018F8F8 File Offset: 0x0018DAF8
		public static TypeValue ClearDomain(TypeValue type)
		{
			RecordValue recordValue = type.MetaValue;
			recordValue = Library.Record.RemoveFields.Invoke(recordValue, TypeServices.DomainTextValue, Library.MissingField.Ignore).AsRecord;
			return type.NewMeta(recordValue).AsType;
		}

		// Token: 0x06007460 RID: 29792 RVA: 0x0018F933 File Offset: 0x0018DB33
		public static bool IsSerializedText(TypeValue type)
		{
			return type.NonNullable.Equals(TypeValue.SerializedText);
		}

		// Token: 0x06007461 RID: 29793 RVA: 0x0018F945 File Offset: 0x0018DB45
		public static bool IsGuid(TypeValue type)
		{
			return type.NonNullable.Equals(TypeValue.Guid);
		}

		// Token: 0x06007462 RID: 29794 RVA: 0x0018F958 File Offset: 0x0018DB58
		public static bool TryGetSerializationFormat(TypeValue type, out string format)
		{
			format = null;
			if (type.TypeKind != ValueKind.Text || !type.MetaValue.Keys.Contains("Serialization.Format"))
			{
				return false;
			}
			Value value = type.MetaValue["Serialization.Format"];
			if (!value.IsText)
			{
				return false;
			}
			format = value.AsString;
			return true;
		}

		// Token: 0x06007463 RID: 29795 RVA: 0x0018F9B0 File Offset: 0x0018DBB0
		public static TableTypeValue ConvertToFolder(TableTypeValue type, TextValue navigationColumn, TextValue navigationName, TextValue navigationData)
		{
			RecordValue metaValue = type.MetaValue;
			RecordValue recordValue = RecordValue.New(TypeServices.FolderNavigationKeys, new Value[]
			{
				LogicalValue.True,
				navigationColumn,
				navigationName,
				navigationData
			});
			RecordValue asRecord = metaValue.Concatenate(recordValue).AsRecord;
			return type.NewMeta(asRecord).AsType.AsTableType;
		}

		// Token: 0x06007464 RID: 29796 RVA: 0x0018FA07 File Offset: 0x0018DC07
		public static TableTypeValue ConvertToFolder(TableTypeValue type, TextValue navigationColumn)
		{
			return BinaryOperator.AddMeta.Invoke(type, RecordValue.New(TypeServices.FolderOnlyNavigationKeys, new Value[]
			{
				LogicalValue.True,
				navigationColumn
			})).AsType.AsTableType;
		}

		// Token: 0x06007465 RID: 29797 RVA: 0x0018FA3A File Offset: 0x0018DC3A
		public static TypeValue ConvertToInline(TypeValue type)
		{
			return BinaryOperator.AddMeta.Invoke(type, RecordValue.New(Keys.New("Inline"), new Value[] { LogicalValue.True })).AsType;
		}

		// Token: 0x06007466 RID: 29798 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public static Value ConvertToLimitedPreview(Value type)
		{
			return type;
		}

		// Token: 0x06007467 RID: 29799 RVA: 0x0018FA69 File Offset: 0x0018DC69
		public static bool IsCompatibleType(TypeValue left, TypeValue right)
		{
			left = TypeServices.StripNullableAndMetadata(left);
			right = TypeServices.StripNullableAndMetadata(right);
			return left.IsCompatibleWith(right) || right.IsCompatibleWith(left) || left.TypeKind == right.TypeKind;
		}

		// Token: 0x06007468 RID: 29800 RVA: 0x0018FAA0 File Offset: 0x0018DCA0
		public static bool IsScalar(TypeValue type)
		{
			ValueKind typeKind = type.TypeKind;
			return typeKind <= ValueKind.Binary;
		}

		// Token: 0x06007469 RID: 29801 RVA: 0x0018FABC File Offset: 0x0018DCBC
		public static bool IsValid(TypeValue type)
		{
			if (type == null)
			{
				return false;
			}
			if (type.TypeKind == ValueKind.List)
			{
				return TypeServices.IsValid(type.AsListType.ItemType);
			}
			return type.TypeKind != ValueKind.None;
		}

		// Token: 0x0600746A RID: 29802 RVA: 0x0018FAEB File Offset: 0x0018DCEB
		public static TypeValue StripNullableAndMetadata(TypeValue type)
		{
			return TypeAlgebra.StripNullable(type);
		}

		// Token: 0x0600746B RID: 29803 RVA: 0x0018FAF4 File Offset: 0x0018DCF4
		public static TypeValue GetTypeValueForKind(ValueKind kind)
		{
			switch (kind)
			{
			case ValueKind.Time:
				return TypeValue.Time;
			case ValueKind.Date:
				return TypeValue.Date;
			case ValueKind.DateTime:
				return TypeValue.DateTime;
			case ValueKind.DateTimeZone:
				return TypeValue.DateTimeZone;
			case ValueKind.Duration:
				return TypeValue.Duration;
			case ValueKind.Number:
				return TypeValue.Number;
			case ValueKind.Logical:
				return TypeValue.Logical;
			case ValueKind.Text:
				return TypeValue.Text;
			case ValueKind.Binary:
				return TypeValue.Binary;
			case ValueKind.List:
				return TypeValue.List;
			case ValueKind.Record:
				return TypeValue.Record;
			case ValueKind.Table:
				return TypeValue.Table;
			case ValueKind.Function:
				return TypeValue.Function;
			case ValueKind.Type:
				return TypeValue._Type;
			default:
				return TypeValue.Any;
			}
		}

		// Token: 0x04003FFF RID: 16383
		private const string TableFolder = "Table.Folder";

		// Token: 0x04004000 RID: 16384
		private const string TableNavigationColumn = "Table.NavigationColumn";

		// Token: 0x04004001 RID: 16385
		private static readonly Keys FolderNavigationKeys = Keys.New("Table.Folder", "Table.NavigationColumn", "NavigationTable.NameColumn", "NavigationTable.DataColumn");

		// Token: 0x04004002 RID: 16386
		private static readonly Keys FolderOnlyNavigationKeys = Keys.New("Table.Folder", "Table.NavigationColumn");

		// Token: 0x04004003 RID: 16387
		private const string LimitedPreviewName = "LimitedPreview";

		// Token: 0x04004004 RID: 16388
		private static readonly Keys LimitedPreviewKey = Keys.New("LimitedPreview");

		// Token: 0x04004005 RID: 16389
		private const string InlineKeyName = "Inline";

		// Token: 0x04004006 RID: 16390
		public const string WindowsNTFileTimeFormat = "WindowsNTFileTimeFormat";

		// Token: 0x04004007 RID: 16391
		private static readonly Keys WindowsNTFileTimeFormatKey = Keys.New("WindowsNTFileTimeFormat");

		// Token: 0x04004008 RID: 16392
		public static readonly RecordValue WindowsNTFileTimeFormatMetadataRecord = RecordValue.New(TypeServices.WindowsNTFileTimeFormatKey, new Value[] { LogicalValue.True });

		// Token: 0x04004009 RID: 16393
		private const string DomainKeyName = "DomainValues";

		// Token: 0x0400400A RID: 16394
		private static readonly TextValue DomainTextValue = TextValue.New("DomainValues");

		// Token: 0x0400400B RID: 16395
		private static readonly Keys DomainKeys = Keys.New("DomainValues");

		// Token: 0x0400400C RID: 16396
		public const string MoreColumnsName = "MoreColumns";

		// Token: 0x0400400D RID: 16397
		private static readonly Keys MoreColumnsKeys = Keys.New("MoreColumns");
	}
}
