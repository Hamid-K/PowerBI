using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.HtmlTable;
using Microsoft.Mashup.HtmlUtils;

namespace Microsoft.Mashup.Engine1.Library.HtmlTable
{
	// Token: 0x0200002F RID: 47
	public sealed class HtmlTableModule : Module45
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00006462 File Offset: 0x00004662
		public override ResourceManager DocumentationResources
		{
			get
			{
				return Resources.ResourceManager;
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006469 File Offset: 0x00004669
		protected override RecordValue GetModuleExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return HtmlTableModule.Table;
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00006495 File Offset: 0x00004695
		public override string Name
		{
			get
			{
				return "HtmlTable";
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000649C File Offset: 0x0000469C
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "Html.Table";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x040000BA RID: 186
		private static readonly FunctionValue Table = new HtmlTableModule.TableFunctionValue();

		// Token: 0x040000BB RID: 187
		public const string HtmlTableName = "Html.Table";

		// Token: 0x040000BC RID: 188
		private Keys exportKeys;

		// Token: 0x02000030 RID: 48
		private class TableFunctionValue : NativeFunctionValue3<TableValue, Value, ListValue, Value>
		{
			// Token: 0x06000113 RID: 275 RVA: 0x000064E4 File Offset: 0x000046E4
			public TableFunctionValue()
				: base(TypeValue.Table, 2, "html", TypeValue.Any, "columnNameSelectorPairs", TypeValue.List, "options", HtmlTableModule.TableFunctionValue.optionType)
			{
			}

			// Token: 0x06000114 RID: 276 RVA: 0x0000651C File Offset: 0x0000471C
			public override TableValue TypedInvoke(Value html, ListValue columnNameSelectorPairs, Value options)
			{
				if (html.IsBinary)
				{
					html = Library.Text.FromBinary.Invoke(html);
				}
				else
				{
					html = html.AsText;
				}
				HtmlTableOptions htmlTableOptions = new HtmlTableOptions(HtmlTableModule.TableFunctionValue.optionRecord.CreateOptions("Html.Table", options));
				string[] array = columnNameSelectorPairs.Select((IValueReference p) => p.Value.AsList[0].AsString).ToArray<string>();
				string[] array2 = columnNameSelectorPairs.Select((IValueReference p) => p.Value.AsList[1].AsString).ToArray<string>();
				FunctionValue[] columnExtractors = columnNameSelectorPairs.Select(delegate(IValueReference p)
				{
					if (p.Value.AsList.Count <= 2)
					{
						return null;
					}
					return p.Value.AsList[2].AsFunction;
				}).ToArray<FunctionValue>();
				TableValue tableValue2;
				using (IHtmlDocument htmlDocument = <9d499489-7606-41ac-b77b-5f31a68d089b>AngleSharpUtils.ParseHtmlAndNormalizeStyles(html.AsString))
				{
					TableTypeValue tableTypeValue = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(Keys.New(array), (int columnIndex) => HtmlTableModule.TableFunctionValue.GetColumnType(columnExtractors[columnIndex]))));
					TableValue tableValue;
					if (htmlTableOptions.RowSelector != null)
					{
						tableValue = HtmlTableModule.TableFunctionValue.GetTableFromHtml(htmlDocument, htmlTableOptions.RowSelector, array2, columnExtractors, tableTypeValue);
					}
					else
					{
						if (array2.Length > 1)
						{
							throw ValueException.NewExpressionError<Message0>(Resources.RowSelectorRequired, null, null);
						}
						tableValue = HtmlTableModule.TableFunctionValue.GetTableFromHtml(htmlDocument, array2, columnExtractors, tableTypeValue);
					}
					tableValue2 = ValueServices.AddFirstRowMayContainHeadersMeta(ValueServices.AddShouldInferTableTypeMeta(tableValue));
				}
				return tableValue2;
			}

			// Token: 0x06000115 RID: 277 RVA: 0x00006690 File Offset: 0x00004890
			private static RecordValue GetColumnType(FunctionValue columnExtractor)
			{
				if (columnExtractor == null)
				{
					return RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
					{
						NullableTypeValue.Text,
						LogicalValue.False
					});
				}
				return RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					columnExtractor.Type.AsFunctionType.ReturnType,
					LogicalValue.False
				});
			}

			// Token: 0x06000116 RID: 278 RVA: 0x000066EC File Offset: 0x000048EC
			private static Dictionary<INode, int> GetDomOrder(IHtmlDocument document)
			{
				int num = 0;
				Dictionary<INode, int> dictionary = new Dictionary<INode, int>();
				HtmlTableModule.TableFunctionValue.GetDomOrder(document, dictionary, ref num);
				return dictionary;
			}

			// Token: 0x06000117 RID: 279 RVA: 0x0000670C File Offset: 0x0000490C
			private static void GetDomOrder(INode node, Dictionary<INode, int> domOrder, ref int nodeIndex)
			{
				nodeIndex++;
				domOrder[node] = nodeIndex;
				foreach (INode node2 in node.ChildNodes)
				{
					HtmlTableModule.TableFunctionValue.GetDomOrder(node2, domOrder, ref nodeIndex);
				}
			}

			// Token: 0x06000118 RID: 280 RVA: 0x00006768 File Offset: 0x00004968
			private static TableValue GetTableFromHtml(IHtmlDocument document, string[] columnSelectors, FunctionValue[] columnExtractors, TableTypeValue tableType)
			{
				Value[] array = new Value[columnSelectors.Length];
				for (int i = 0; i < columnSelectors.Length; i++)
				{
					string columnSelector = columnSelectors[i];
					FunctionValue columnExtractor = columnExtractors[i];
					ListValue listValue = ListValue.New((from element in HtmlTableModule.TableFunctionValue.GuardAgainstDomExceptions<IHtmlCollection<IElement>>(() => document.QuerySelectorAll(columnSelector), columnSelector)
						select HtmlTableModule.TableFunctionValue.GetElementContent(element, columnExtractor)).ToArray<Value>());
					array[i] = listValue;
				}
				return TableModule.Table.FromColumns.Invoke(ListValue.New(array), tableType).AsTable;
			}

			// Token: 0x06000119 RID: 281 RVA: 0x00006808 File Offset: 0x00004A08
			private static TableValue GetTableFromHtml(IHtmlDocument document, string rowSelector, string[] columnSelectors, FunctionValue[] columnExtractors, TableTypeValue tableType)
			{
				Dictionary<INode, int> domOrder = HtmlTableModule.TableFunctionValue.GetDomOrder(document);
				IHtmlCollection<IElement> htmlCollection = HtmlTableModule.TableFunctionValue.GuardAgainstDomExceptions<IHtmlCollection<IElement>>(() => document.QuerySelectorAll(rowSelector), rowSelector);
				Value[][] array = (from i in Enumerable.Range(0, htmlCollection.Length)
					select new Value[columnSelectors.Length]).ToArray<Value[]>();
				for (int k = 0; k < columnSelectors.Length; k++)
				{
					string columnSelector = columnSelectors[k];
					FunctionValue functionValue = columnExtractors[k];
					IHtmlCollection<IElement> htmlCollection2 = HtmlTableModule.TableFunctionValue.GuardAgainstDomExceptions<IHtmlCollection<IElement>>(() => document.QuerySelectorAll(columnSelector), columnSelector);
					int num = 0;
					for (int j = 0; j < htmlCollection.Length; j++)
					{
						Value[] array2 = array[j];
						int num2 = domOrder[htmlCollection[j]];
						int num3 = ((j < htmlCollection.Length - 1) ? domOrder[htmlCollection[j + 1]] : int.MaxValue);
						while (num < htmlCollection2.Count<IElement>() && domOrder[htmlCollection2[num]] < num2)
						{
							num++;
						}
						IElement element = null;
						if (num < htmlCollection2.Count<IElement>() && domOrder[htmlCollection2[num]] >= num2 && domOrder[htmlCollection2[num]] < num3)
						{
							element = htmlCollection2[num];
						}
						array2[k] = HtmlTableModule.TableFunctionValue.GetElementContent(element, functionValue);
					}
				}
				return TableModule.Table.FromRows.Invoke(ListValue.New(array.Select((Value[] r) => ListValue.New(r))), tableType).AsTable;
			}

			// Token: 0x0600011A RID: 282 RVA: 0x000069E4 File Offset: 0x00004BE4
			private static Value GetElementContent(IElement element, FunctionValue contentExtractor)
			{
				if (element == null || contentExtractor == null)
				{
					return TextValue.NewOrNull((element != null) ? <9d499489-7606-41ac-b77b-5f31a68d089b>AngleSharpUtils.GetInnerText(element).Trim() : null);
				}
				NamedValue[] array = new NamedValue[3];
				array[0] = new NamedValue("TagName", TextValue.New(element.TagName));
				array[1] = new NamedValue("TextContent", TextValue.NewOrNull(element.TextContent));
				array[2] = new NamedValue("Attributes", RecordValue.New(element.Attributes.Select((IAttr a) => new NamedValue(a.Name, TextValue.NewOrNull(a.Value))).ToArray<NamedValue>()));
				RecordValue recordValue = RecordValue.New(array);
				return contentExtractor.Invoke(recordValue);
			}

			// Token: 0x0600011B RID: 283 RVA: 0x00006AA0 File Offset: 0x00004CA0
			private static T GuardAgainstDomExceptions<T>(Func<T> action, string selector)
			{
				T t;
				try
				{
					t = action();
				}
				catch (DomException ex)
				{
					throw ValueException.NewExpressionError(ex.Message, TextValue.NewOrNull(selector), ex);
				}
				return t;
			}

			// Token: 0x040000BD RID: 189
			private static readonly OptionRecordDefinition optionRecord = new OptionRecordDefinition(new OptionItem[]
			{
				new OptionItem("RowSelector", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, null)
			});

			// Token: 0x040000BE RID: 190
			private static readonly TypeValue optionType = HtmlTableModule.TableFunctionValue.optionRecord.CreateRecordType().Nullable;
		}

		// Token: 0x02000037 RID: 55
		private enum Exports
		{
			// Token: 0x040000D0 RID: 208
			Html_Table,
			// Token: 0x040000D1 RID: 209
			Count
		}
	}
}
