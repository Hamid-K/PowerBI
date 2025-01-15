using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010F0 RID: 4336
	internal static class NavigationTableServices
	{
		// Token: 0x06007164 RID: 29028 RVA: 0x00185BDB File Offset: 0x00183DDB
		public static TableTypeValue GetODataNavigationTableType()
		{
			return NavigationTableServices.AddNavigationTableMetadata(NavigationTableServices.ODataNavigationTableTypeValue);
		}

		// Token: 0x06007165 RID: 29029 RVA: 0x00185BE7 File Offset: 0x00183DE7
		public static TypeValue ConvertToLink(TypeValue type)
		{
			return NavigationTableServices.ConvertToLink(type, "Value", false);
		}

		// Token: 0x06007166 RID: 29030 RVA: 0x00185BF8 File Offset: 0x00183DF8
		public static TypeValue ConvertToLink(TypeValue type, string itemKind, bool isLeaf = false)
		{
			RecordValue recordValue;
			if (isLeaf)
			{
				recordValue = RecordValue.New(Keys.New("NavigationTable.ItemKind", "Preview.Delay", "NavigationTable.IsLeaf"), new Value[]
				{
					TextValue.New(itemKind),
					TextValue.New(LinkName.getLinkNameFromLinkKind(itemKind)),
					LogicalValue.True
				});
			}
			else
			{
				recordValue = RecordValue.New(Keys.New("NavigationTable.ItemKind", "Preview.Delay"), new Value[]
				{
					TextValue.New(itemKind),
					TextValue.New(LinkName.getLinkNameFromLinkKind(itemKind))
				});
			}
			return BinaryOperator.AddMeta.Invoke(type, recordValue).AsType;
		}

		// Token: 0x06007167 RID: 29031 RVA: 0x00185C8C File Offset: 0x00183E8C
		public static TypeValue AddDataColumnIsLeafMetadata(TypeValue type)
		{
			return BinaryOperator.AddMeta.Invoke(type, NavigationTableServices.DataColumnIsLeafMetadata).AsType;
		}

		// Token: 0x06007168 RID: 29032 RVA: 0x00185CA3 File Offset: 0x00183EA3
		public static TableTypeValue AddNavigationTableMetadata(TableTypeValue tableTypeValue)
		{
			return NavigationTableServices.AddNavigationTableMetadata(tableTypeValue, NavigationTableServices.NameColumnValue, NavigationTableServices.DataColumnValue);
		}

		// Token: 0x06007169 RID: 29033 RVA: 0x00185CB8 File Offset: 0x00183EB8
		public static TableTypeValue AddNavigationTableMetadata(TableTypeValue tableTypeValue, TextValue nameColumnName, TextValue dataColumnName)
		{
			RecordValue metaValue = tableTypeValue.MetaValue;
			RecordValue recordValue = RecordValue.New(NavigationTableServices.MetadataKeys, new Value[] { nameColumnName, dataColumnName });
			RecordValue asRecord = metaValue.Concatenate(recordValue).AsRecord;
			return tableTypeValue.NewMeta(asRecord).AsType.AsTableType;
		}

		// Token: 0x0600716A RID: 29034 RVA: 0x00185D03 File Offset: 0x00183F03
		public static TableTypeValue AddNavigationTableMetadataWithKind(TableTypeValue tableTypeValue)
		{
			return NavigationTableServices.AddNavigationTableMetadataWithKind(tableTypeValue, NavigationTableServices.NameColumnValue, NavigationTableServices.DataColumnValue, NavigationTableServices.KindColumnValue);
		}

		// Token: 0x0600716B RID: 29035 RVA: 0x00185D1C File Offset: 0x00183F1C
		public static TableTypeValue AddNavigationTableMetadataWithKind(TableTypeValue tableTypeValue, TextValue nameColumnName, TextValue dataColumnName, TextValue kindColumnName)
		{
			RecordValue metaValue = tableTypeValue.MetaValue;
			RecordValue recordValue = RecordValue.New(NavigationTableServices.KindMetadataKeys, new Value[] { nameColumnName, dataColumnName, kindColumnName });
			RecordValue asRecord = metaValue.Concatenate(recordValue).AsRecord;
			return tableTypeValue.NewMeta(asRecord).AsType.AsTableType;
		}

		// Token: 0x0600716C RID: 29036 RVA: 0x00185D6B File Offset: 0x00183F6B
		public static TableTypeValue AddNavigationTableMetadataWithKindHidden(TableTypeValue tableTypeValue)
		{
			return NavigationTableServices.AddNavigationTableMetadataWithKindHidden(tableTypeValue, NavigationTableServices.NameColumnValue, NavigationTableServices.DataColumnValue, NavigationTableServices.KindColumnValue, NavigationTableServices.HiddenColumnValue);
		}

		// Token: 0x0600716D RID: 29037 RVA: 0x00185D88 File Offset: 0x00183F88
		public static TableTypeValue AddNavigationTableMetadataWithKindHidden(TableTypeValue tableTypeValue, TextValue nameColumnName, TextValue dataColumnName, TextValue kindColumnName, TextValue hiddenColumnName)
		{
			RecordValue metaValue = tableTypeValue.MetaValue;
			RecordValue recordValue = RecordValue.New(NavigationTableServices.KindHiddenMetadataKeys, new Value[] { nameColumnName, dataColumnName, kindColumnName, hiddenColumnName });
			RecordValue asRecord = metaValue.Concatenate(recordValue).AsRecord;
			return tableTypeValue.NewMeta(asRecord).AsType.AsTableType;
		}

		// Token: 0x0600716E RID: 29038 RVA: 0x00185DDC File Offset: 0x00183FDC
		public static TableTypeValue AddNavigationTableMetadataWithDescriptionKind(TableTypeValue tableTypeValue)
		{
			RecordValue metaValue = tableTypeValue.MetaValue;
			RecordValue recordValue = RecordValue.New(NavigationTableServices.DescriptionKindMetadataKeys, new Value[]
			{
				NavigationTableServices.NameColumnValue,
				NavigationTableServices.DescriptionColumnValue,
				NavigationTableServices.DataColumnValue,
				NavigationTableServices.KindColumnValue
			});
			RecordValue asRecord = metaValue.Concatenate(recordValue).AsRecord;
			return tableTypeValue.NewMeta(asRecord).AsType.AsTableType;
		}

		// Token: 0x0600716F RID: 29039 RVA: 0x00185E40 File Offset: 0x00184040
		public static bool TryGetIndexRecord(RecordTypeValue rowType, FunctionValue condition, out RecordValue keyRecord)
		{
			QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(rowType, condition);
			RecordBuilder recordBuilder = new RecordBuilder(rowType.FieldKeys.Length);
			try
			{
				if (NavigationTableServices.TryGetIndexes(queryExpression, rowType.FieldKeys, ref recordBuilder))
				{
					keyRecord = recordBuilder.ToRecord();
					return true;
				}
			}
			catch (ValueException)
			{
			}
			keyRecord = RecordValue.Empty;
			return false;
		}

		// Token: 0x06007170 RID: 29040 RVA: 0x00185EA4 File Offset: 0x001840A4
		public static Value AddCaption(Value value, string caption)
		{
			if (caption != null)
			{
				value = value.NewMeta(RecordValue.New(NavigationTableServices.captionMetadataKeys, new Value[] { TextValue.New(caption) }));
			}
			return value;
		}

		// Token: 0x06007171 RID: 29041 RVA: 0x00185ECB File Offset: 0x001840CB
		public static RecordValue NewDefaultValueMetadata(string caption, Value value)
		{
			if (string.IsNullOrEmpty(caption))
			{
				return RecordValue.Empty;
			}
			value = NavigationTableServices.AddCaption(value, caption);
			return RecordValue.New(NavigationTableServices.defaultValueMetadataKeys, new Value[] { value });
		}

		// Token: 0x06007172 RID: 29042 RVA: 0x00185EF8 File Offset: 0x001840F8
		public static RecordValue NewDefaultValuesMetadata(List<string> captions, List<Value> values)
		{
			if (!captions.Any<string>())
			{
				return RecordValue.Empty;
			}
			IValueReference[] array = new IValueReference[captions.Count];
			for (int i = 0; i < captions.Count; i++)
			{
				array[i] = NavigationTableServices.AddCaption(values[i], captions[i]);
			}
			return RecordValue.New(NavigationTableServices.defaultValueMetadataKeys, new Value[] { ListValue.New(array) });
		}

		// Token: 0x06007173 RID: 29043 RVA: 0x00185F5F File Offset: 0x0018415F
		public static RecordValue NewDefaultValueMetadata(Value value)
		{
			if (value == null)
			{
				return RecordValue.Empty;
			}
			return RecordValue.New(NavigationTableServices.defaultValueMetadataKeys, new Value[] { value });
		}

		// Token: 0x06007174 RID: 29044 RVA: 0x00185F7E File Offset: 0x0018417E
		public static RecordValue NewDefaultValuesMetadata(List<Value> values)
		{
			if (!values.Any<Value>())
			{
				return RecordValue.Empty;
			}
			return RecordValue.New(NavigationTableServices.defaultValueMetadataKeys, new Value[] { ListValue.New(values.ToArray()) });
		}

		// Token: 0x06007175 RID: 29045 RVA: 0x00185FAC File Offset: 0x001841AC
		public static RecordValue NewAllowedValuesIsOpenSetMetadata(bool exhaustive)
		{
			return RecordValue.New(NavigationTableServices.allowedValuesIsOpenSetMetadataKeys, new Value[] { LogicalValue.New(exhaustive) });
		}

		// Token: 0x06007176 RID: 29046 RVA: 0x00185FC8 File Offset: 0x001841C8
		private static bool TryGetIndex(BinaryQueryExpression equality, Keys columns, ref RecordBuilder recordBuilder)
		{
			QueryExpressionKind kind = equality.Left.Kind;
			ColumnAccessQueryExpression columnAccessQueryExpression;
			ConstantQueryExpression constantQueryExpression;
			if (kind != QueryExpressionKind.Constant)
			{
				if (kind != QueryExpressionKind.ColumnAccess)
				{
					return false;
				}
				if (equality.Right.Kind != QueryExpressionKind.Constant)
				{
					return false;
				}
				columnAccessQueryExpression = (ColumnAccessQueryExpression)equality.Left;
				constantQueryExpression = (ConstantQueryExpression)equality.Right;
			}
			else
			{
				if (equality.Right.Kind != QueryExpressionKind.ColumnAccess)
				{
					return false;
				}
				columnAccessQueryExpression = (ColumnAccessQueryExpression)equality.Right;
				constantQueryExpression = (ConstantQueryExpression)equality.Left;
			}
			recordBuilder.Add(columns[columnAccessQueryExpression.Column], constantQueryExpression.Value, constantQueryExpression.Value.Type);
			return true;
		}

		// Token: 0x06007177 RID: 29047 RVA: 0x00186064 File Offset: 0x00184264
		private static bool TryGetIndexes(QueryExpression expression, Keys columns, ref RecordBuilder recordBuilder)
		{
			if (expression != null && expression.Kind == QueryExpressionKind.Binary)
			{
				BinaryQueryExpression binaryQueryExpression = (BinaryQueryExpression)expression;
				BinaryOperator2 @operator = binaryQueryExpression.Operator;
				if (@operator == BinaryOperator2.Equals)
				{
					return NavigationTableServices.TryGetIndex(binaryQueryExpression, columns, ref recordBuilder);
				}
				if (@operator == BinaryOperator2.And)
				{
					return NavigationTableServices.TryGetIndexes(binaryQueryExpression.Left, columns, ref recordBuilder) && NavigationTableServices.TryGetIndexes(binaryQueryExpression.Right, columns, ref recordBuilder);
				}
			}
			return false;
		}

		// Token: 0x04003E74 RID: 15988
		public const string IdColumn = "Id";

		// Token: 0x04003E75 RID: 15989
		public const string NameColumn = "Name";

		// Token: 0x04003E76 RID: 15990
		public const string DisplayNameColumn = "DisplayName";

		// Token: 0x04003E77 RID: 15991
		public const string DataColumn = "Data";

		// Token: 0x04003E78 RID: 15992
		public const string DescriptionColumn = "Description";

		// Token: 0x04003E79 RID: 15993
		public const string KindColumn = "Kind";

		// Token: 0x04003E7A RID: 15994
		public const string HiddenColumn = "Hidden";

		// Token: 0x04003E7B RID: 15995
		public static readonly Keys MetadataKeys = Keys.New("NavigationTable.NameColumn", "NavigationTable.DataColumn");

		// Token: 0x04003E7C RID: 15996
		public static readonly Keys KindMetadataKeys = Keys.New("NavigationTable.NameColumn", "NavigationTable.DataColumn", "NavigationTable.KindColumn");

		// Token: 0x04003E7D RID: 15997
		private static readonly Keys KindHiddenMetadataKeys = Keys.New("NavigationTable.NameColumn", "NavigationTable.DataColumn", "NavigationTable.KindColumn", "NavigationTable.HiddenColumn");

		// Token: 0x04003E7E RID: 15998
		private static readonly Keys DescriptionKindMetadataKeys = Keys.New("NavigationTable.NameColumn", "NavigationTable.DescriptionColumn", "NavigationTable.DataColumn", "NavigationTable.KindColumn");

		// Token: 0x04003E7F RID: 15999
		public static readonly Keys MetadataValues = Keys.New("Name", "Data");

		// Token: 0x04003E80 RID: 16000
		public static readonly Keys MetadataValuesWithKind = Keys.New("Name", "Data", "Kind");

		// Token: 0x04003E81 RID: 16001
		public static readonly TextValue IdColumnValue = TextValue.New("Id");

		// Token: 0x04003E82 RID: 16002
		public static readonly TextValue NameColumnValue = TextValue.New("Name");

		// Token: 0x04003E83 RID: 16003
		public static readonly TextValue DisplayNameColumnValue = TextValue.New("DisplayName");

		// Token: 0x04003E84 RID: 16004
		public static readonly TextValue DescriptionColumnValue = TextValue.New("Description");

		// Token: 0x04003E85 RID: 16005
		public static readonly TextValue DataColumnValue = TextValue.New("Data");

		// Token: 0x04003E86 RID: 16006
		public static readonly TextValue KindColumnValue = TextValue.New("Kind");

		// Token: 0x04003E87 RID: 16007
		public static readonly TextValue HiddenColumnValue = TextValue.New("Hidden");

		// Token: 0x04003E88 RID: 16008
		private static readonly RecordValue DataColumnIsLeafMetadata = RecordValue.New(Keys.New("NavigationTable.IsLeaf"), new Value[] { LogicalValue.True });

		// Token: 0x04003E89 RID: 16009
		private static readonly Keys captionMetadataKeys = Keys.New("Documentation.Caption");

		// Token: 0x04003E8A RID: 16010
		private static readonly Keys defaultValueMetadataKeys = Keys.New("Documentation.DefaultValue");

		// Token: 0x04003E8B RID: 16011
		private static readonly Keys allowedValuesIsOpenSetMetadataKeys = Keys.New("Documentation.AllowedValuesIsOpenSet");

		// Token: 0x04003E8C RID: 16012
		private const string SignatureColumn = "Signature";

		// Token: 0x04003E8D RID: 16013
		public static readonly Keys ODataNavigationTableKeys = Keys.New("Name", "Data", "Signature");

		// Token: 0x04003E8E RID: 16014
		private static readonly TableTypeValue ODataNavigationTableTypeValue = TableTypeValue.New(RecordTypeValue.New(NavigationTableServices.ODataNavigationTableKeys), new TableKey[]
		{
			new TableKey(new int[] { 0, 2 }, true)
		});

		// Token: 0x04003E8F RID: 16015
		public static readonly TableTypeValue DefaultTypeValue = NavigationTableServices.AddNavigationTableMetadata(TableTypeValue.New(RecordTypeValue.New(NavigationTableServices.MetadataValues), new TableKey[]
		{
			new TableKey(new int[1], true)
		}));
	}
}
