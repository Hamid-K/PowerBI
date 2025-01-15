using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Normalization;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.PageReaderConformance
{
	// Token: 0x0200054C RID: 1356
	internal class PageReaderConformanceModule : Module
	{
		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x06002B66 RID: 11110 RVA: 0x00083999 File Offset: 0x00081B99
		public override string Name
		{
			get
			{
				return "PageReaderConformance";
			}
		}

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x06002B67 RID: 11111 RVA: 0x000839A0 File Offset: 0x00081BA0
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(2, delegate(int index)
					{
						if (index == 0)
						{
							return "Table.ConformToPageReader";
						}
						if (index != 1)
						{
							throw new InvalidOperationException();
						}
						return "List.ConformToPageReader";
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x000839DB File Offset: 0x00081BDB
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return PageReaderConformanceModule.TableConformToPageReader;
				}
				if (index != 1)
				{
					throw new InvalidOperationException();
				}
				return PageReaderConformanceModule.ListConformToPageReader;
			});
		}

		// Token: 0x040012E0 RID: 4832
		private Keys exportKeys;

		// Token: 0x040012E1 RID: 4833
		private static FunctionValue TableConformToPageReader = new PageReaderConformanceModule.TableConformToPageReaderFunctionValue();

		// Token: 0x040012E2 RID: 4834
		private static FunctionValue ListConformToPageReader = new PageReaderConformanceModule.ListConformToPageReaderFunctionValue();

		// Token: 0x0200054D RID: 1357
		private enum Exports
		{
			// Token: 0x040012E4 RID: 4836
			Table_ConformToPageReader,
			// Token: 0x040012E5 RID: 4837
			List_ConformToPageReader,
			// Token: 0x040012E6 RID: 4838
			Count
		}

		// Token: 0x0200054E RID: 1358
		private class TableConformToPageReaderFunctionValue : NativeFunctionValue2<TableValue, TableValue, FunctionValue>
		{
			// Token: 0x06002B6B RID: 11115 RVA: 0x00083A1D File Offset: 0x00081C1D
			public TableConformToPageReaderFunctionValue()
				: base(TypeValue.Table, 2, "table", TypeValue.Table, "shapingFunction", TypeValue.Function)
			{
			}

			// Token: 0x06002B6C RID: 11116 RVA: 0x00083A40 File Offset: 0x00081C40
			public override TableValue TypedInvoke(TableValue table, FunctionValue shapingFunction)
			{
				PageReaderConformanceEnforcingTableValue pageReaderConformanceEnforcingTableValue = new PageReaderConformanceEnforcingTableValue(table);
				return shapingFunction.Invoke(pageReaderConformanceEnforcingTableValue).AsTable;
			}
		}

		// Token: 0x0200054F RID: 1359
		private class ListConformToPageReaderFunctionValue : NativeFunctionValue2<TableValue, ListValue, Value>
		{
			// Token: 0x06002B6D RID: 11117 RVA: 0x00083A60 File Offset: 0x00081C60
			public ListConformToPageReaderFunctionValue()
				: base(TypeValue.Table, 1, "list", TypeValue.List, "options", NullableTypeValue.Record)
			{
			}

			// Token: 0x06002B6E RID: 11118 RVA: 0x00083A82 File Offset: 0x00081C82
			public override TableValue TypedInvoke(ListValue list, Value options)
			{
				return new PageReaderConformanceModule.ListConformToPageReaderFunctionValue.MultipleResultTableValue(list, options);
			}

			// Token: 0x02000550 RID: 1360
			private sealed class MultipleResultTableValue : TableValue
			{
				// Token: 0x06002B6F RID: 11119 RVA: 0x00083A8B File Offset: 0x00081C8B
				public MultipleResultTableValue(ListValue list, Value options)
				{
					this.list = list;
					this.options = options;
				}

				// Token: 0x17001041 RID: 4161
				// (get) Token: 0x06002B70 RID: 11120 RVA: 0x00083AA1 File Offset: 0x00081CA1
				public override TypeValue Type
				{
					get
					{
						return PageReaderConformanceModule.ListConformToPageReaderFunctionValue.MultipleResultTableValue.tableType;
					}
				}

				// Token: 0x17001042 RID: 4162
				// (get) Token: 0x06002B71 RID: 11121 RVA: 0x00083AA8 File Offset: 0x00081CA8
				public override long LargeCount
				{
					get
					{
						return this.list.LargeCount;
					}
				}

				// Token: 0x06002B72 RID: 11122 RVA: 0x00083AB5 File Offset: 0x00081CB5
				public override IPageReader GetReader()
				{
					return PageReaderConformanceModule.ListConformToPageReaderFunctionValue.MoreResultsPageReader.New(this.GetEnumerator());
				}

				// Token: 0x06002B73 RID: 11123 RVA: 0x00083AC2 File Offset: 0x00081CC2
				public override IEnumerator<IValueReference> GetEnumerator()
				{
					Value lastValue = Value.Null;
					foreach (IValueReference valueReference in this.list)
					{
						Value value = valueReference.Value;
						if (value.IsFunction)
						{
							FunctionValue asFunction = value.AsFunction;
							if (value.Type.AsFunctionType.ParameterCount > 0)
							{
								value = value.AsFunction.Invoke(lastValue);
							}
							else
							{
								value = value.AsFunction.Invoke();
							}
						}
						lastValue = NormalizationModule.TableFromValue.Invoke(value, this.options);
						yield return RecordValue.New(PageReaderConformanceModule.ListConformToPageReaderFunctionValue.MultipleResultTableValue.recordType, new Value[] { lastValue });
					}
					IEnumerator<IValueReference> enumerator = null;
					yield break;
					yield break;
				}

				// Token: 0x040012E7 RID: 4839
				private static readonly RecordTypeValue recordType = RecordTypeValue.New(Keys.New("Result"));

				// Token: 0x040012E8 RID: 4840
				private static readonly TableTypeValue tableType = TableTypeValue.New(PageReaderConformanceModule.ListConformToPageReaderFunctionValue.MultipleResultTableValue.recordType);

				// Token: 0x040012E9 RID: 4841
				private readonly ListValue list;

				// Token: 0x040012EA RID: 4842
				private readonly Value options;
			}

			// Token: 0x02000552 RID: 1362
			private sealed class MoreResultsPageReader : DelegatingPageReader
			{
				// Token: 0x06002B7C RID: 11132 RVA: 0x00083CA4 File Offset: 0x00081EA4
				private MoreResultsPageReader(IEnumerator<IValueReference> values)
					: base(PageReaderConformanceModule.ListConformToPageReaderFunctionValue.MoreResultsPageReader.GetReader(values.Current.Value))
				{
					this.values = values;
				}

				// Token: 0x06002B7D RID: 11133 RVA: 0x00083CC3 File Offset: 0x00081EC3
				public static IPageReader New(IEnumerator<IValueReference> values)
				{
					if (!values.MoveNext())
					{
						return TableValue.Empty.GetReader();
					}
					return new PageReaderConformanceModule.ListConformToPageReaderFunctionValue.MoreResultsPageReader(values);
				}

				// Token: 0x06002B7E RID: 11134 RVA: 0x00083CDE File Offset: 0x00081EDE
				private static IPageReader GetReader(Value value)
				{
					return value.AsRecord[0].AsTable.GetReader();
				}

				// Token: 0x06002B7F RID: 11135 RVA: 0x00083CF6 File Offset: 0x00081EF6
				public override IPageReader NextResult()
				{
					if (this.values.MoveNext())
					{
						IEnumerator<IValueReference> enumerator = this.values;
						this.values = null;
						return new PageReaderConformanceModule.ListConformToPageReaderFunctionValue.MoreResultsPageReader(enumerator);
					}
					return null;
				}

				// Token: 0x06002B80 RID: 11136 RVA: 0x00083D19 File Offset: 0x00081F19
				public override void Dispose()
				{
					if (this.values != null)
					{
						this.values.Dispose();
						this.values = null;
					}
					base.Dispose();
				}

				// Token: 0x040012F0 RID: 4848
				private IEnumerator<IValueReference> values;
			}
		}
	}
}
