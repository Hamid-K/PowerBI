using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Html
{
	// Token: 0x02000AC7 RID: 2759
	internal static class HtmlModuleHelper
	{
		// Token: 0x06004D07 RID: 19719 RVA: 0x000FE09C File Offset: 0x000FC29C
		private static Value AddWebMetadata(Value value, string[] selectors, string elementTagName, string elementClass, string elementId)
		{
			RecordValue recordValue = RecordValue.New(HtmlModuleHelper.webMetadataKeys, new Value[]
			{
				ListValue.New(selectors),
				TextValue.New(elementTagName),
				TextValue.NewOrNull(elementClass),
				TextValue.NewOrNull(elementId)
			});
			return BinaryOperator.AddMeta.Invoke(value, recordValue);
		}

		// Token: 0x06004D08 RID: 19720 RVA: 0x000FE0EC File Offset: 0x000FC2EC
		private static RecordValue CreateServiceRecord(DomNode document, TextValue caption, Func<DomNode, ListValue> translator, TableTypeValue outputType)
		{
			return RecordValue.New(HtmlModuleHelper.TableKeys, delegate(int i)
			{
				switch (i)
				{
				case 0:
					return caption;
				case 1:
					return HtmlModuleHelper.ServiceSource;
				case 2:
				case 3:
					return Value.Null;
				case 4:
					return HtmlModuleHelper.AddWebMetadata(new HtmlModuleHelper.HtmlServiceListValue(document, translator).ToTable(outputType), new string[] { "html" }, "HTML", null, null);
				default:
					throw new InvalidOperationException();
				}
			});
		}

		// Token: 0x06004D09 RID: 19721 RVA: 0x000FE134 File Offset: 0x000FC334
		public static TableValue Contents(DomNode document)
		{
			bool flag;
			List<Value> list = HtmlModuleHelper.HtmlStructureValueCreator.CreateStructuredTableValues(HtmlModuleHelper.DomNodeInfo.CreateRootNodeInfo(document), out flag);
			list.Add(HtmlModuleHelper.CreateServiceRecord(document, HtmlModuleHelper.DocumentRowCaption, new Func<DomNode, ListValue>(HtmlDomValueCreator.CreateListValue), HtmlDocumentResult.Type));
			NavigationTableTypeValueBuilder navigationTableTypeValueBuilder = new NavigationTableTypeValueBuilder(HtmlModuleHelper.TableFieldType, 3);
			navigationTableTypeValueBuilder.AddNameColumnName(HtmlModuleHelper.NameColumnName);
			navigationTableTypeValueBuilder.AddDataColumnName(HtmlModuleHelper.DataColumnName);
			navigationTableTypeValueBuilder.AddRowConfigurationColumnName(HtmlModuleHelper.DataColumnName);
			TableTypeValue tableTypeValue = navigationTableTypeValueBuilder.ToTypeValue();
			TableValue tableValue = ListValue.New(list.ToArray()).ToTable(tableTypeValue);
			if (flag)
			{
				return ValueServices.AddFirstRowMayContainHeadersMeta(tableValue);
			}
			return tableValue;
		}

		// Token: 0x040028FF RID: 10495
		private const string ColumnHeaderNameForFullSpanData = "Header";

		// Token: 0x04002900 RID: 10496
		private const string HtmlSelector = "html";

		// Token: 0x04002901 RID: 10497
		private const string HtmlTagName = "HTML";

		// Token: 0x04002902 RID: 10498
		private static readonly Regex ValidCssIdentifierRegex = new Regex("^-?[_a-zA-Z]+[_a-zA-Z0-9-]*$", RegexOptions.Compiled);

		// Token: 0x04002903 RID: 10499
		private static readonly Keys TableKeys = Keys.New(new string[] { "Caption", "Source", "ClassName", "Id", "Data" });

		// Token: 0x04002904 RID: 10500
		private static readonly Keys webMetadataKeys = Keys.New("Web.Selectors", "Web.ElementTagName", "Web.ElementClass", "Web.ElementId");

		// Token: 0x04002905 RID: 10501
		private static readonly TextValue TableSource = TextValue.New("Table");

		// Token: 0x04002906 RID: 10502
		private static readonly TextValue ServiceSource = TextValue.New("Service");

		// Token: 0x04002907 RID: 10503
		private static readonly TextValue DocumentRowCaption = TextValue.New("Document");

		// Token: 0x04002908 RID: 10504
		private static readonly string NameColumnName = "Caption";

		// Token: 0x04002909 RID: 10505
		private static readonly string DataColumnName = "Data";

		// Token: 0x0400290A RID: 10506
		private static readonly TableTypeValue TableFieldType = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(HtmlModuleHelper.TableKeys, new Value[]
		{
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				NullableTypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				NullableTypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				NullableTypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				NullableTypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Table,
				LogicalValue.False
			})
		}), false));

		// Token: 0x02000AC8 RID: 2760
		private sealed class HtmlServiceListValue : ListValue
		{
			// Token: 0x06004D0B RID: 19723 RVA: 0x000FE34B File Offset: 0x000FC54B
			public HtmlServiceListValue(DomNode document, Func<DomNode, ListValue> function)
			{
				this.document = document;
				this.function = function;
			}

			// Token: 0x17001833 RID: 6195
			// (get) Token: 0x06004D0C RID: 19724 RVA: 0x00002139 File Offset: 0x00000339
			public override bool IsBuffered
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06004D0D RID: 19725 RVA: 0x000FE361 File Offset: 0x000FC561
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				return this.BufferedValue.GetEnumerator();
			}

			// Token: 0x06004D0E RID: 19726 RVA: 0x000FE36E File Offset: 0x000FC56E
			public override IValueReference GetReference(int index)
			{
				return this.BufferedValue.GetReference(index);
			}

			// Token: 0x17001834 RID: 6196
			// (get) Token: 0x06004D0F RID: 19727 RVA: 0x000FE37C File Offset: 0x000FC57C
			private ListValue BufferedValue
			{
				get
				{
					if (this.bufferedValue == null)
					{
						this.bufferedValue = this.function(this.document).AsList;
					}
					return this.bufferedValue;
				}
			}

			// Token: 0x17001835 RID: 6197
			// (get) Token: 0x06004D10 RID: 19728 RVA: 0x000FE3A8 File Offset: 0x000FC5A8
			public override int Count
			{
				get
				{
					return this.BufferedValue.Count;
				}
			}

			// Token: 0x17001836 RID: 6198
			// (get) Token: 0x06004D11 RID: 19729 RVA: 0x000FE3B5 File Offset: 0x000FC5B5
			public override long LargeCount
			{
				get
				{
					return this.BufferedValue.LargeCount;
				}
			}

			// Token: 0x0400290B RID: 10507
			private ListValue bufferedValue;

			// Token: 0x0400290C RID: 10508
			private readonly DomNode document;

			// Token: 0x0400290D RID: 10509
			private readonly Func<DomNode, ListValue> function;
		}

		// Token: 0x02000AC9 RID: 2761
		private sealed class HtmlStructureValueCreator
		{
			// Token: 0x06004D12 RID: 19730 RVA: 0x000FE3C2 File Offset: 0x000FC5C2
			private HtmlStructureValueCreator(HashSet<string> searchTerms)
			{
				this.searchTerms = searchTerms;
			}

			// Token: 0x06004D13 RID: 19731 RVA: 0x000FE400 File Offset: 0x000FC600
			private RecordValue CreateFromDefinitionList(HtmlModuleHelper.DomNodeInfo nodeInfo)
			{
				List<string> list = new List<string>();
				HashSet<string> hashSet = new HashSet<string>();
				List<Value> list2 = new List<Value>();
				List<string> list3 = new List<string>();
				List<Value> list4 = new List<Value>();
				bool flag = false;
				foreach (HtmlModuleHelper.DomNodeInfo domNodeInfo in nodeInfo.Children)
				{
					DomNode node = domNodeInfo.Node;
					if (HtmlModuleHelper.HtmlStructureValueCreator.IsVisibleElement(node, this.visitedNodes))
					{
						string name = node.Name;
						if (!(name == "DT"))
						{
							if (name == "DD")
							{
								if (!flag)
								{
									flag = true;
								}
								list4.Add(this.CreateFromEntry(domNodeInfo));
							}
						}
						else
						{
							if (flag)
							{
								string text = string.Join(", ", list3.ToArray());
								list.Add(HtmlModuleHelper.HtmlStructureValueCreator.MakeUniqueKey(text, hashSet));
								list2.Add(HtmlModuleHelper.HtmlStructureValueCreator.SimplifyItemContents(list4));
								list3.Clear();
								list4.Clear();
								flag = false;
							}
							list3.Add(this.CreateFromDefinitionListTerm(node).Trim());
						}
					}
				}
				if (list3.Count > 0)
				{
					string text2 = string.Join(", ", list3.ToArray());
					list.Add(HtmlModuleHelper.HtmlStructureValueCreator.MakeUniqueKey(text2, hashSet));
					list2.Add(HtmlModuleHelper.HtmlStructureValueCreator.SimplifyItemContents(list4));
				}
				if (list.Count != 0)
				{
					return RecordValue.New(Keys.New(list.ToArray()), list2.ToArray());
				}
				return RecordValue.Empty;
			}

			// Token: 0x06004D14 RID: 19732 RVA: 0x000FE57C File Offset: 0x000FC77C
			private string CreateFromDefinitionListTerm(DomNode node)
			{
				List<Value> list = new List<Value>();
				foreach (DomNode domNode in node.Children)
				{
					if (this.visitedNodes.Add(domNode))
					{
						list.Add(HtmlModuleHelper.HtmlTextValueCreator.CreateValue(domNode));
					}
				}
				if (list.Count == 0)
				{
					return string.Empty;
				}
				return HtmlModuleHelper.HtmlStructureValueCreator.SimplifyItemContents(list).AsString;
			}

			// Token: 0x06004D15 RID: 19733 RVA: 0x000FE604 File Offset: 0x000FC804
			private ListValue CreateFromOrdinaryList(HtmlModuleHelper.DomNodeInfo nodeInfo)
			{
				List<Value> list = new List<Value>();
				foreach (HtmlModuleHelper.DomNodeInfo domNodeInfo in nodeInfo.Children)
				{
					DomNode node = domNodeInfo.Node;
					if (this.visitedNodes.Add(node) && node.Kind == DomNodeKind.Element && node.Name == "LI")
					{
						list.Add(this.CreateFromEntry(domNodeInfo));
					}
				}
				if (!list.All((Value o) => HtmlModuleHelper.HtmlStructureValueCreator.IsEmptyItem(o)))
				{
					return ListValue.New(list.ToArray());
				}
				return ListValue.Empty;
			}

			// Token: 0x06004D16 RID: 19734 RVA: 0x000FE6C8 File Offset: 0x000FC8C8
			private Value CreateTableValueFromTableNode(HtmlModuleHelper.DomNodeInfo nodeInfo, DomNode parent, int childPosition, out bool isColumnsGenerated)
			{
				DomNode node = nodeInfo.Node;
				this.foundNodeInfos.Add(nodeInfo);
				TextValue textValue = null;
				TextValue tableSource = HtmlModuleHelper.TableSource;
				bool flag;
				TableValue tableValue = this.CreateFromTable(nodeInfo, out flag, out textValue, out isColumnsGenerated);
				if (textValue == null)
				{
					textValue = this.FindCaption(parent, childPosition);
				}
				int num = 0;
				bool flag2 = false;
				int num2 = 0;
				foreach (IValueReference valueReference in tableValue)
				{
					RecordValue asRecord = valueReference.Value.AsRecord;
					for (int i = 0; i < asRecord.Count; i++)
					{
						Value value = asRecord[i];
						ValueKind kind = value.Kind;
						if (kind != ValueKind.Text)
						{
							if (kind - ValueKind.List <= 1)
							{
								num2++;
							}
						}
						else if (value.AsString.Length > 0)
						{
							flag2 = true;
						}
					}
					num++;
				}
				Value value2;
				if (num == 0)
				{
					value2 = Value.Null;
				}
				else
				{
					if (!flag2 && num2 <= 1)
					{
						value2 = Value.Null;
					}
					if (flag || (num > 1 && tableValue.Item0.AsRecord.Count > 1))
					{
						value2 = tableValue;
					}
					else
					{
						value2 = HtmlModuleHelper.HtmlTextValueCreator.CreateValue(node);
					}
				}
				this.foundValues.Add(nodeInfo, value2);
				this.foundDetails.Add(nodeInfo, RecordValue.New(HtmlModuleHelper.TableKeys, new Value[]
				{
					textValue ?? Value.Null,
					tableSource,
					TextValue.NewOrNull(node.Class),
					TextValue.NewOrNull(node.Id),
					HtmlModuleHelper.AddWebMetadata(value2, nodeInfo.Selectors.ToArray<string>(), nodeInfo.Node.Name, node.Class, node.Id)
				}));
				return value2;
			}

			// Token: 0x06004D17 RID: 19735 RVA: 0x000FE878 File Offset: 0x000FCA78
			private TableValue CreateFromTable(HtmlModuleHelper.DomNodeInfo nodeInfo, out bool foundHeaders, out TextValue caption, out bool isColumnsGenerated)
			{
				foundHeaders = false;
				caption = null;
				IList<HtmlModuleHelper.DomNodeInfo> list = null;
				List<IList<HtmlModuleHelper.DomNodeInfo>> list2 = new List<IList<HtmlModuleHelper.DomNodeInfo>>();
				this.FindAllTHeadsAndTBodies(nodeInfo, list2, out list, out caption);
				bool flag = list != null;
				if (!flag && list2.Count > 0)
				{
					list = list2.First<IList<HtmlModuleHelper.DomNodeInfo>>();
				}
				bool flag2 = list != null && list.Count > 0;
				int num = 0;
				List<string> list3 = new List<string>();
				TextValue textValue = null;
				bool flag3 = false;
				bool flag4;
				if (flag2)
				{
					flag4 = (flag3 = this.TryCreateTableHeader(list.Select((HtmlModuleHelper.DomNodeInfo n) => n.Node).ToList<DomNode>(), flag, out list3, out num, out textValue));
				}
				else
				{
					flag4 = false;
				}
				foundHeaders = flag4;
				isColumnsGenerated = !flag3 || (flag3 && list3.Count == 0);
				if (flag)
				{
					num = 0;
				}
				List<List<Value>> list4 = new List<List<Value>>();
				List<string> list5 = new List<string>();
				List<string> list6 = new List<string>();
				int num2 = 0;
				SortedDictionary<int, HtmlModuleHelper.HtmlStructureValueCreator.CellValueRowSpan> sortedDictionary = new SortedDictionary<int, HtmlModuleHelper.HtmlStructureValueCreator.CellValueRowSpan>();
				foreach (IList<HtmlModuleHelper.DomNodeInfo> list7 in list2)
				{
					foreach (HtmlModuleHelper.DomNodeInfo domNodeInfo in ((List<HtmlModuleHelper.DomNodeInfo>)list7))
					{
						DomNode node = domNodeInfo.Node;
						if (node.Name == "TR" || HtmlModuleHelper.HtmlStructureValueCreator.IsVisibleElement(node, this.visitedNodes))
						{
							bool flag5 = sortedDictionary.Count == 0;
							List<Value> list8 = this.CreateFromTableRow(domNodeInfo, sortedDictionary);
							bool flag6 = sortedDictionary.Count == 0;
							if (!list8.All((Value o) => HtmlModuleHelper.HtmlStructureValueCreator.IsEmptyItem(o)))
							{
								if (textValue != null && flag5 && flag6 && list8[0].IsText)
								{
									string testValue = list8[0].AsString;
									if (list8.All((Value cell) => cell.IsText && string.Equals(cell.AsString, testValue, StringComparison.Ordinal)))
									{
										textValue = list8[0].AsText;
										continue;
									}
								}
								if (foundHeaders && list8.Count == list3.Count)
								{
									if (list8.Zip(list3, (Value c, string t) => (c.IsText && string.Equals(c.AsString, t, StringComparison.Ordinal)) || (c.IsNull && string.IsNullOrEmpty(t))).All((bool o) => o))
									{
										continue;
									}
								}
								if (textValue != null)
								{
									list8.Insert(0, textValue);
								}
								num2 = Math.Max(num2, list8.Count);
								list4.Add(list8);
								list5.Add(node.Class);
								list6.Add(node.GetAttributeValue("bgColor", null));
							}
						}
					}
					num = 0;
				}
				if (list4.Count == 0)
				{
					return TableValue.Empty;
				}
				if (textValue != null)
				{
					list3.Insert(0, HtmlModuleHelper.HtmlStructureValueCreator.MakeUniqueKey("Header", new HashSet<string>(list3)));
				}
				if (!foundHeaders)
				{
					list3 = this.NormalizeTableWithoutMarkedHeaders(list4, list5, list6);
				}
				Keys keys = ColumnLabelGenerator.GenerateKeys(list3.Select(delegate(string s)
				{
					if (s.Length <= 4000)
					{
						return s;
					}
					return s.Substring(0, 4000);
				}).ToArray<string>(), num2);
				RecordValue[] array = new RecordValue[list4.Count];
				for (int j = 0; j < list4.Count; j++)
				{
					List<Value> list9 = list4[j];
					HtmlModuleHelper.HtmlStructureValueCreator.Tabularize(num2, list9);
					array[j] = RecordValue.New(keys, list9.ToArray());
				}
				RecordValue columnDescriptor = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					DataSource.NullableSerializedTextType,
					LogicalValue.False
				});
				TableTypeValue tableTypeValue = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(keys, (int i) => columnDescriptor)));
				Value[] array2 = array;
				TableValue tableValue = ListValue.New(array2).ToTable(tableTypeValue);
				if (isColumnsGenerated)
				{
					return ValueServices.AddFirstRowMayContainHeadersMeta(tableValue);
				}
				return tableValue;
			}

			// Token: 0x06004D18 RID: 19736 RVA: 0x000FECB0 File Offset: 0x000FCEB0
			private List<Value> CreateFromTableRow(HtmlModuleHelper.DomNodeInfo nodeInfo, IDictionary<int, HtmlModuleHelper.HtmlStructureValueCreator.CellValueRowSpan> rowSpanTracker)
			{
				List<Value> list = new List<Value>();
				int num = 0;
				foreach (HtmlModuleHelper.DomNodeInfo domNodeInfo in nodeInfo.Children)
				{
					DomNode node = domNodeInfo.Node;
					if ((node.Name == "TD" || node.Name == "TH") && HtmlModuleHelper.HtmlStructureValueCreator.IsVisibleElement(node, this.visitedNodes))
					{
						int attributeValue = node.GetAttributeValue("colSpan", 1);
						int attributeValue2 = node.GetAttributeValue("rowSpan", 1);
						Value value = this.CreateFromEntry(domNodeInfo);
						for (int i = 0; i < attributeValue; i++)
						{
							while (rowSpanTracker.ContainsKey(num))
							{
								num++;
							}
							rowSpanTracker.Add(num, new HtmlModuleHelper.HtmlStructureValueCreator.CellValueRowSpan(value, attributeValue2));
							num++;
						}
					}
				}
				List<int> list2 = new List<int>();
				foreach (KeyValuePair<int, HtmlModuleHelper.HtmlStructureValueCreator.CellValueRowSpan> keyValuePair in rowSpanTracker)
				{
					list.Add(keyValuePair.Value.Value);
					if (!keyValuePair.Value.TryDecrement())
					{
						list2.Add(keyValuePair.Key);
					}
				}
				foreach (int num2 in list2)
				{
					rowSpanTracker.Remove(num2);
				}
				return list;
			}

			// Token: 0x06004D19 RID: 19737 RVA: 0x000FEE48 File Offset: 0x000FD048
			private static bool IsVisibleElement(DomNode node, HashSet<DomNode> visitedNodes)
			{
				return node.Kind == DomNodeKind.Element && !node.GetAttributeValue("hidden", false) && visitedNodes.Add(node);
			}

			// Token: 0x06004D1A RID: 19738 RVA: 0x000FEE6C File Offset: 0x000FD06C
			private List<string> NormalizeTableWithoutMarkedHeaders(List<List<Value>> rows, List<string> rowClasses, List<string> rowBackgrounds)
			{
				if (rows.Count <= 2)
				{
					return new List<string>();
				}
				List<Value> list = rows[0];
				if (list.Any((Value v) => !v.IsText) || list.Distinct(_ValueComparer.LaxDefault).Count<Value>() <= 1)
				{
					return new List<string>();
				}
				string text = rowClasses[0];
				string text2 = rowBackgrounds[0];
				if (text == null && text2 == null)
				{
					return new List<string>();
				}
				bool flag = true;
				for (int i = 1; i < rowClasses.Count; i++)
				{
					if ((text != null && text == rowClasses[i]) || (text2 != null && text2 == rowBackgrounds[i]))
					{
						List<Value> list2 = rows[i];
						if (list2.Distinct(_ValueComparer.LaxDefault).Count<Value>() != 1)
						{
							if (list2.Count == list.Count)
							{
								if (!list2.Zip(list, (Value c, Value t) => !_ValueComparer.LaxDefault.Equals(c, t)).Any((bool o) => o))
								{
									goto IL_0124;
								}
							}
							flag = false;
							break;
						}
					}
					IL_0124:;
				}
				if (!flag)
				{
					return new List<string>();
				}
				List<int> list3 = new List<int>();
				for (int j = 0; j < rows.Count; j++)
				{
					List<Value> list4 = rows[j];
					if (list4.Count == list.Count)
					{
						if (list4.Zip(list, (Value c, Value t) => _ValueComparer.LaxDefault.Equals(c, t)).All((bool o) => o))
						{
							list3.Insert(0, j);
						}
					}
				}
				foreach (int num in list3)
				{
					rows.RemoveAt(num);
					rowClasses.RemoveAt(num);
					rowBackgrounds.RemoveAt(num);
				}
				return list.Select((Value v) => v.AsString).ToList<string>();
			}

			// Token: 0x06004D1B RID: 19739 RVA: 0x000FF0C4 File Offset: 0x000FD2C4
			private Value CreateFromEntry(HtmlModuleHelper.DomNodeInfo nodeInfo)
			{
				DomNode node = nodeInfo.Node;
				List<Value> list = new List<Value>();
				List<Value> list2 = new List<Value>();
				List<Value> list3 = new List<Value>();
				int num = -1;
				foreach (HtmlModuleHelper.DomNodeInfo domNodeInfo in nodeInfo.Children)
				{
					DomNode node2 = domNodeInfo.Node;
					num++;
					if (this.visitedNodes.Add(node2))
					{
						string name = node2.Name;
						if (!(name == "IMG"))
						{
							if (!(name == "SUP"))
							{
								list.Add(this.CreateStructure(domNodeInfo, node, num));
							}
							else
							{
								list2.Add(this.CreateStructure(domNodeInfo, node, num));
							}
						}
						else
						{
							list3.Add(TextValue.New(node2.GetAttributeValue("alt", string.Empty)));
						}
					}
				}
				Value value = HtmlModuleHelper.HtmlStructureValueCreator.SimplifyItemContents(list);
				if (value.IsNull)
				{
					value = HtmlModuleHelper.HtmlStructureValueCreator.SimplifyItemContents(list2);
				}
				if (value.IsText)
				{
					value = TextValue.New(value.AsString.Trim());
				}
				if (HtmlModuleHelper.HtmlStructureValueCreator.IsEmptyItem(value) && list3.Count == 1)
				{
					value = list3[0];
				}
				return value;
			}

			// Token: 0x06004D1C RID: 19740 RVA: 0x000FF20C File Offset: 0x000FD40C
			private bool TryCreateTableHeader(List<DomNode> nodes, bool separateHeaderNode, out List<string> headers, out int startingBodyRowIndex, out TextValue inlineDataHeader)
			{
				inlineDataHeader = null;
				headers = new List<string>();
				SortedDictionary<int, HtmlModuleHelper.HtmlStructureValueCreator.CellValueRowSpan> sortedDictionary = new SortedDictionary<int, HtmlModuleHelper.HtmlStructureValueCreator.CellValueRowSpan>();
				startingBodyRowIndex = 0;
				HashSet<DomNode> hashSet = new HashSet<DomNode>();
				foreach (DomNode domNode in nodes)
				{
					if (HtmlModuleHelper.HtmlStructureValueCreator.IsVisibleElement(domNode, hashSet))
					{
						List<string> list;
						if (!this.TryCreateTableHeaderFromTableRow(domNode, sortedDictionary, hashSet, separateHeaderNode, out list))
						{
							break;
						}
						startingBodyRowIndex++;
						this.visitedNodes.Add(domNode);
						if (list.Count > 1 && list.Distinct<string>().Count<string>() == 1)
						{
							inlineDataHeader = TextValue.New(list[0]);
						}
						else
						{
							int num = 0;
							foreach (string text in list)
							{
								if (num < headers.Count)
								{
									if (text.Length > 0)
									{
										headers[num] = ((headers[num].Length > 0) ? (headers[num] + " " + text) : text);
									}
								}
								else
								{
									headers.Add(text);
								}
								num++;
							}
						}
					}
				}
				return headers.Count != 0;
			}

			// Token: 0x06004D1D RID: 19741 RVA: 0x000FF390 File Offset: 0x000FD590
			private bool TryCreateTableHeaderFromTableRow(DomNode node, IDictionary<int, HtmlModuleHelper.HtmlStructureValueCreator.CellValueRowSpan> rowSpanTracker, HashSet<DomNode> localVisitedNodes, bool separateHeaderNode, out List<string> headerRow)
			{
				headerRow = new List<string>();
				bool flag = true;
				int num = 0;
				foreach (DomNode domNode in node.Children)
				{
					if (HtmlModuleHelper.HtmlStructureValueCreator.IsVisibleElement(domNode, localVisitedNodes))
					{
						if ((!separateHeaderNode || !(domNode.Name == "TD")) && !(domNode.Name == "TH"))
						{
							flag = false;
							break;
						}
						int attributeValue = domNode.GetAttributeValue("colSpan", 1);
						int attributeValue2 = domNode.GetAttributeValue("rowSpan", 1);
						TextValue textValue = this.NormalizeTextValueForHeader(HtmlModuleHelper.HtmlTextValueCreator.CreateValue(domNode));
						for (int i = 0; i < attributeValue; i++)
						{
							while (rowSpanTracker.ContainsKey(num))
							{
								num++;
							}
							rowSpanTracker.Add(num, new HtmlModuleHelper.HtmlStructureValueCreator.CellValueRowSpan(textValue, attributeValue2));
							num++;
						}
					}
				}
				List<int> list = new List<int>();
				foreach (KeyValuePair<int, HtmlModuleHelper.HtmlStructureValueCreator.CellValueRowSpan> keyValuePair in rowSpanTracker)
				{
					if (!keyValuePair.Value.TryDecrement())
					{
						headerRow.Add(keyValuePair.Value.Value.AsString);
						list.Add(keyValuePair.Key);
					}
					else
					{
						headerRow.Add(string.Empty);
					}
				}
				foreach (int num2 in list)
				{
					rowSpanTracker.Remove(num2);
				}
				return flag;
			}

			// Token: 0x06004D1E RID: 19742 RVA: 0x000FF54C File Offset: 0x000FD74C
			private TextValue NormalizeTextValueForHeader(TextValue value)
			{
				return TextValue.New(value.String.Replace(Environment.NewLine, " "));
			}

			// Token: 0x06004D1F RID: 19743 RVA: 0x000FF568 File Offset: 0x000FD768
			private void CreateStructureForRoot(HtmlModuleHelper.DomNodeInfo nodeInfo, out bool isColumnsGenerated)
			{
				this.CreateStructureForRoot(nodeInfo, null, int.MinValue, out isColumnsGenerated);
			}

			// Token: 0x06004D20 RID: 19744 RVA: 0x000FF578 File Offset: 0x000FD778
			private void CreateStructureForRoot(HtmlModuleHelper.DomNodeInfo nodeInfo, DomNode parent, int childPosition, out bool isColumnsGenerated)
			{
				DomNode node = nodeInfo.Node;
				isColumnsGenerated = false;
				if (node.Kind == DomNodeKind.Element && !node.GetAttributeValue("hidden", false))
				{
					if (node.Name == "TABLE")
					{
						this.CreateTableValueFromTableNode(nodeInfo, parent, childPosition, out isColumnsGenerated);
						return;
					}
					int num = -1;
					foreach (HtmlModuleHelper.DomNodeInfo domNodeInfo in nodeInfo.Children)
					{
						DomNode node2 = domNodeInfo.Node;
						num++;
						if (this.visitedNodes.Add(node2))
						{
							this.CreateStructureForRoot(domNodeInfo, node, num, out isColumnsGenerated);
						}
					}
				}
			}

			// Token: 0x06004D21 RID: 19745 RVA: 0x000FF628 File Offset: 0x000FD828
			private Value CreateStructure(HtmlModuleHelper.DomNodeInfo nodeInfo, DomNode parent, int childPosition)
			{
				DomNode node = nodeInfo.Node;
				DomNodeKind kind = node.Kind;
				if (kind != DomNodeKind.Element)
				{
					if (kind != DomNodeKind.Text)
					{
						return Value.Null;
					}
					return TextValue.New(node.Text);
				}
				else
				{
					if (node.GetAttributeValue("hidden", false))
					{
						return Value.Null;
					}
					string name = node.Name;
					if (name != null)
					{
						switch (name.Length)
						{
						case 2:
						{
							char c = name[0];
							if (c <= 'D')
							{
								if (c != 'B')
								{
									if (c != 'D')
									{
										goto IL_0160;
									}
									if (!(name == "DL"))
									{
										goto IL_0160;
									}
									RecordValue recordValue = this.CreateFromDefinitionList(nodeInfo);
									if (!recordValue.IsEmpty)
									{
										return recordValue;
									}
									return Value.Null;
								}
								else
								{
									if (!(name == "BR"))
									{
										goto IL_0160;
									}
									return TextValue.New(Environment.NewLine);
								}
							}
							else
							{
								if (c != 'O')
								{
									if (c != 'U')
									{
										goto IL_0160;
									}
									if (!(name == "UL"))
									{
										goto IL_0160;
									}
								}
								else if (!(name == "OL"))
								{
									goto IL_0160;
								}
								ListValue listValue = this.CreateFromOrdinaryList(nodeInfo);
								if (!listValue.IsEmpty)
								{
									return listValue;
								}
								return Value.Null;
							}
							break;
						}
						case 3:
							goto IL_0160;
						case 4:
							if (!(name == "HEAD"))
							{
								goto IL_0160;
							}
							break;
						case 5:
						{
							if (!(name == "TABLE"))
							{
								goto IL_0160;
							}
							bool flag;
							return this.CreateTableValueFromTableNode(nodeInfo, parent, childPosition, out flag);
						}
						case 6:
							if (!(name == "SCRIPT"))
							{
								goto IL_0160;
							}
							break;
						default:
							goto IL_0160;
						}
						return Value.Null;
					}
					IL_0160:
					int num = -1;
					foreach (HtmlModuleHelper.DomNodeInfo domNodeInfo in nodeInfo.Children)
					{
						DomNode node2 = domNodeInfo.Node;
						num++;
						if (this.visitedNodes.Add(node2))
						{
							this.CreateStructure(domNodeInfo, node, num);
						}
					}
					return HtmlModuleHelper.HtmlTextValueCreator.CreateValue(node);
				}
			}

			// Token: 0x06004D22 RID: 19746 RVA: 0x000FF81C File Offset: 0x000FDA1C
			private TextValue FindCaption(DomNode parent, int childPosition)
			{
				if (parent != null && childPosition > 0)
				{
					int num = 0;
					int num2 = 1;
					while (num < 5 && childPosition - num2 >= 0)
					{
						DomNode domNode = parent.Children[childPosition - num2];
						if (domNode.Kind == DomNodeKind.Element)
						{
							num++;
							if (domNode.Name.StartsWith("h", StringComparison.OrdinalIgnoreCase))
							{
								return HtmlModuleHelper.HtmlTextValueCreator.CreateValue(domNode);
							}
						}
						num2++;
					}
				}
				return null;
			}

			// Token: 0x06004D23 RID: 19747 RVA: 0x000FF87C File Offset: 0x000FDA7C
			public static List<Value> CreateStructuredTableValues(HtmlModuleHelper.DomNodeInfo nodeInfo, out bool isColumnsGenerated)
			{
				List<Value> list = new List<Value>();
				HtmlModuleHelper.HtmlStructureValueCreator htmlStructureValueCreator = new HtmlModuleHelper.HtmlStructureValueCreator(HtmlModuleHelper.HtmlStructureValueCreator.StructureContext);
				htmlStructureValueCreator.CreateStructureForRoot(nodeInfo, out isColumnsGenerated);
				foreach (HtmlModuleHelper.DomNodeInfo domNodeInfo in htmlStructureValueCreator.foundNodeInfos)
				{
					if (htmlStructureValueCreator.foundValues[domNodeInfo].IsTable)
					{
						list.Add(htmlStructureValueCreator.foundDetails[domNodeInfo]);
					}
				}
				return list;
			}

			// Token: 0x06004D24 RID: 19748 RVA: 0x000FF908 File Offset: 0x000FDB08
			private void FindAllTHeadsAndTBodies(HtmlModuleHelper.DomNodeInfo nodeInfo, List<IList<HtmlModuleHelper.DomNodeInfo>> bodies, out IList<HtmlModuleHelper.DomNodeInfo> head, out TextValue caption)
			{
				head = null;
				caption = null;
				foreach (HtmlModuleHelper.DomNodeInfo domNodeInfo in nodeInfo.Children)
				{
					DomNode node = domNodeInfo.Node;
					if (node.Kind == DomNodeKind.Element && this.visitedNodes.Add(node))
					{
						string text = node.Name ?? string.Empty;
						if (!(text == "THEAD"))
						{
							if (!(text == "TBODY"))
							{
								if (!(text == "CAPTION"))
								{
									IList<HtmlModuleHelper.DomNodeInfo> list;
									TextValue textValue;
									this.FindAllTHeadsAndTBodies(domNodeInfo, bodies, out list, out textValue);
									if (head == null)
									{
										head = list;
									}
									if (caption == null)
									{
										caption = textValue;
									}
								}
								else
								{
									caption = HtmlModuleHelper.HtmlTextValueCreator.CreateValue(domNodeInfo.Children.Select((HtmlModuleHelper.DomNodeInfo i) => i.Node).ToList<DomNode>());
								}
							}
							else
							{
								IList<HtmlModuleHelper.DomNodeInfo> children = domNodeInfo.Children;
								if (children.Count > 0)
								{
									bodies.Add(children);
								}
							}
						}
						else if (head == null)
						{
							head = domNodeInfo.Children;
						}
					}
				}
			}

			// Token: 0x06004D25 RID: 19749 RVA: 0x000FFA3C File Offset: 0x000FDC3C
			private static bool IsEmptyItem(Value value)
			{
				return value.IsNull || (value.IsText && value.AsText.String.Trim().Length == 0);
			}

			// Token: 0x06004D26 RID: 19750 RVA: 0x000FFA6C File Offset: 0x000FDC6C
			private static string MakeUniqueKey(string currentTerm, HashSet<string> termSet)
			{
				int num = 1;
				string text = currentTerm;
				while (!termSet.Add(text))
				{
					text = currentTerm + num++.ToString();
				}
				return text;
			}

			// Token: 0x06004D27 RID: 19751 RVA: 0x000FFAA0 File Offset: 0x000FDCA0
			private static Value SimplifyItemContents(List<Value> itemContents)
			{
				if (itemContents.Count == 0)
				{
					return Value.Null;
				}
				if (itemContents.Count == 1)
				{
					return itemContents[0];
				}
				if (itemContents.All((Value o) => o.IsNull || o.IsText))
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (Value value in itemContents)
					{
						if (value.IsText)
						{
							stringBuilder.Append(value.AsText.String);
						}
					}
					return TextValue.New(stringBuilder.ToString().Trim());
				}
				return ListValue.New(itemContents.ToArray());
			}

			// Token: 0x06004D28 RID: 19752 RVA: 0x000FFB6C File Offset: 0x000FDD6C
			private static void Tabularize(int maxFieldCount, List<Value> currentRow)
			{
				for (int i = currentRow.Count; i < maxFieldCount; i++)
				{
					currentRow.Add(Value.Null);
				}
			}

			// Token: 0x0400290E RID: 10510
			private const string DefinitionListSeparator = ", ";

			// Token: 0x0400290F RID: 10511
			private const int MaxColumnHeaderLength = 4000;

			// Token: 0x04002910 RID: 10512
			private static readonly HashSet<string> StructureContext = new HashSet<string>(new string[] { "TABLE" });

			// Token: 0x04002911 RID: 10513
			private readonly IEnumerable<string> searchTerms;

			// Token: 0x04002912 RID: 10514
			private readonly List<HtmlModuleHelper.DomNodeInfo> foundNodeInfos = new List<HtmlModuleHelper.DomNodeInfo>();

			// Token: 0x04002913 RID: 10515
			private readonly HashSet<DomNode> visitedNodes = new HashSet<DomNode>();

			// Token: 0x04002914 RID: 10516
			private readonly Dictionary<HtmlModuleHelper.DomNodeInfo, Value> foundValues = new Dictionary<HtmlModuleHelper.DomNodeInfo, Value>();

			// Token: 0x04002915 RID: 10517
			private readonly Dictionary<HtmlModuleHelper.DomNodeInfo, RecordValue> foundDetails = new Dictionary<HtmlModuleHelper.DomNodeInfo, RecordValue>();

			// Token: 0x02000ACA RID: 2762
			private sealed class CellValueRowSpan
			{
				// Token: 0x06004D2A RID: 19754 RVA: 0x000FFBAF File Offset: 0x000FDDAF
				public CellValueRowSpan(Value value, int remainingSpan)
				{
					this.value = value;
					this.remainingSpan = remainingSpan;
				}

				// Token: 0x17001837 RID: 6199
				// (get) Token: 0x06004D2B RID: 19755 RVA: 0x000FFBC5 File Offset: 0x000FDDC5
				public Value Value
				{
					get
					{
						return this.value;
					}
				}

				// Token: 0x06004D2C RID: 19756 RVA: 0x000FFBCD File Offset: 0x000FDDCD
				public bool TryDecrement()
				{
					this.remainingSpan--;
					return this.remainingSpan > 0;
				}

				// Token: 0x04002916 RID: 10518
				private int remainingSpan;

				// Token: 0x04002917 RID: 10519
				private readonly Value value;
			}
		}

		// Token: 0x02000ACE RID: 2766
		private sealed class HtmlTextValueCreator
		{
			// Token: 0x06004D41 RID: 19777 RVA: 0x000FFCB4 File Offset: 0x000FDEB4
			private void AddNewlineIfBlock(string nodeName)
			{
				if (nodeName != null)
				{
					switch (nodeName.Length)
					{
					case 1:
						if (!(nodeName == "P"))
						{
							return;
						}
						break;
					case 2:
					{
						char c = nodeName[1];
						switch (c)
						{
						case '1':
							if (!(nodeName == "H1"))
							{
								return;
							}
							break;
						case '2':
							if (!(nodeName == "H2"))
							{
								return;
							}
							break;
						case '3':
							if (!(nodeName == "H3"))
							{
								return;
							}
							break;
						case '4':
							if (!(nodeName == "H4"))
							{
								return;
							}
							break;
						case '5':
							if (!(nodeName == "H5"))
							{
								return;
							}
							break;
						case '6':
							if (!(nodeName == "H6"))
							{
								return;
							}
							break;
						default:
							if (c != 'R')
							{
								return;
							}
							if (!(nodeName == "BR"))
							{
								return;
							}
							break;
						}
						break;
					}
					case 3:
						if (!(nodeName == "DIV"))
						{
							return;
						}
						break;
					default:
						return;
					}
					if (this.canAppendNewLine)
					{
						this.builder.AppendLine();
						this.canAppendNewLine = false;
					}
				}
			}

			// Token: 0x06004D42 RID: 19778 RVA: 0x000FFDAC File Offset: 0x000FDFAC
			private void CollectTextFromNode(DomNode node)
			{
				if (!this.visitedNodes.Add(node))
				{
					return;
				}
				DomNodeKind kind = node.Kind;
				if (kind != DomNodeKind.Element)
				{
					if (kind != DomNodeKind.Text)
					{
						return;
					}
					this.builder.Append(node.Text);
					this.canAppendNewLine = true;
					return;
				}
				else
				{
					if (node.GetAttributeValue("hidden", false))
					{
						return;
					}
					if (node.Name == "SELECT")
					{
						return;
					}
					this.AddNewlineIfBlock(node.Name);
					if (node.Children.Count > 0)
					{
						this.CollectTextFromNodes(node.Children);
					}
					this.AddNewlineIfBlock(node.Name);
					return;
				}
			}

			// Token: 0x06004D43 RID: 19779 RVA: 0x000FFE48 File Offset: 0x000FE048
			private void CollectTextFromNodes(List<DomNode> nodes)
			{
				foreach (DomNode domNode in nodes)
				{
					this.CollectTextFromNode(domNode);
				}
			}

			// Token: 0x06004D44 RID: 19780 RVA: 0x000FFE98 File Offset: 0x000FE098
			public static TextValue CreateValue(DomNode node)
			{
				HtmlModuleHelper.HtmlTextValueCreator htmlTextValueCreator = new HtmlModuleHelper.HtmlTextValueCreator();
				htmlTextValueCreator.CollectTextFromNode(node);
				return TextValue.New(htmlTextValueCreator.builder.ToString().Trim());
			}

			// Token: 0x06004D45 RID: 19781 RVA: 0x000FFEBA File Offset: 0x000FE0BA
			public static TextValue CreateValue(List<DomNode> nodes)
			{
				HtmlModuleHelper.HtmlTextValueCreator htmlTextValueCreator = new HtmlModuleHelper.HtmlTextValueCreator();
				htmlTextValueCreator.CollectTextFromNodes(nodes);
				return TextValue.New(htmlTextValueCreator.builder.ToString().Trim());
			}

			// Token: 0x04002929 RID: 10537
			private bool canAppendNewLine;

			// Token: 0x0400292A RID: 10538
			private StringBuilder builder = new StringBuilder();

			// Token: 0x0400292B RID: 10539
			private HashSet<DomNode> visitedNodes = new HashSet<DomNode>();
		}

		// Token: 0x02000ACF RID: 2767
		private class DomNodeInfo
		{
			// Token: 0x06004D47 RID: 19783 RVA: 0x000FFEFC File Offset: 0x000FE0FC
			private DomNodeInfo(DomNode node, HtmlModuleHelper.DomNodeInfo parent, int childIndex, int ofTypeIndex)
			{
				this.node = node;
				this.parent = parent;
				this.childIndex = childIndex;
				this.ofTypeIndex = ofTypeIndex;
				this.children = Lazy.New<IList<HtmlModuleHelper.DomNodeInfo>>(new Func<IList<HtmlModuleHelper.DomNodeInfo>>(this.GetChildren));
				this.localSelectors = new LazyDictionary<HtmlModuleHelper.LocalSelectorParameters, string>(new Func<HtmlModuleHelper.LocalSelectorParameters, string>(this.GetLocalSelector));
				this.fullSelectors = new LazyDictionary<HtmlModuleHelper.FullSelectorParameters, string>(new Func<HtmlModuleHelper.FullSelectorParameters, string>(this.GetFullSelector));
			}

			// Token: 0x17001838 RID: 6200
			// (get) Token: 0x06004D48 RID: 19784 RVA: 0x000FFF71 File Offset: 0x000FE171
			public IList<HtmlModuleHelper.DomNodeInfo> Children
			{
				get
				{
					return this.children.Value;
				}
			}

			// Token: 0x17001839 RID: 6201
			// (get) Token: 0x06004D49 RID: 19785 RVA: 0x000FFF7E File Offset: 0x000FE17E
			public DomNode Node
			{
				get
				{
					return this.node;
				}
			}

			// Token: 0x1700183A RID: 6202
			// (get) Token: 0x06004D4A RID: 19786 RVA: 0x000FFF86 File Offset: 0x000FE186
			public IEnumerable<string> Selectors
			{
				get
				{
					return HtmlModuleHelper.DomNodeInfo.selectorParameters.Select((HtmlModuleHelper.FullSelectorParameters selectorParams) => this.fullSelectors[selectorParams]).Distinct<string>();
				}
			}

			// Token: 0x06004D4B RID: 19787 RVA: 0x000FFFA3 File Offset: 0x000FE1A3
			public static HtmlModuleHelper.DomNodeInfo CreateRootNodeInfo(DomNode node)
			{
				return new HtmlModuleHelper.DomNodeInfo(node, null, 0, 0);
			}

			// Token: 0x06004D4C RID: 19788 RVA: 0x000FFFAE File Offset: 0x000FE1AE
			private static string GetRelationshipOperator(HtmlModuleHelper.NodeRelationship nodeRelationship)
			{
				if (nodeRelationship == HtmlModuleHelper.NodeRelationship.Children)
				{
					return " > ";
				}
				if (nodeRelationship != HtmlModuleHelper.NodeRelationship.Descendants)
				{
					throw new ArgumentException("Unexpected node relationship: " + nodeRelationship.ToString());
				}
				return " ";
			}

			// Token: 0x06004D4D RID: 19789 RVA: 0x000FFFE4 File Offset: 0x000FE1E4
			private IList<HtmlModuleHelper.DomNodeInfo> GetChildren()
			{
				if (!this.node.Children.Any<DomNode>())
				{
					return EmptyArray<HtmlModuleHelper.DomNodeInfo>.Instance;
				}
				List<HtmlModuleHelper.DomNodeInfo> list = new List<HtmlModuleHelper.DomNodeInfo>(this.node.Children.Count);
				int num = 0;
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				foreach (DomNode domNode in this.node.Children)
				{
					int num2 = -1;
					int num3 = -1;
					if (domNode.Kind == DomNodeKind.Element)
					{
						num2 = num;
						num++;
						string name = domNode.Name;
						int num4;
						if (!dictionary.TryGetValue(name, out num4))
						{
							num4 = 0;
						}
						num3 = num4;
						dictionary[name] = num4 + 1;
					}
					list.Add(new HtmlModuleHelper.DomNodeInfo(domNode, this, num2, num3));
				}
				return list;
			}

			// Token: 0x06004D4E RID: 19790 RVA: 0x001000C0 File Offset: 0x000FE2C0
			private string GetLocalSelector(HtmlModuleHelper.LocalSelectorParameters parameters)
			{
				string text = string.Join(string.Empty, (from @class in this.node.Classes
					where HtmlModuleHelper.ValidCssIdentifierRegex.IsMatch(@class)
					select string.Format(CultureInfo.InvariantCulture, ".{0}", @class)).ToArray<string>());
				string text2 = ((!string.IsNullOrEmpty(this.node.Id) && HtmlModuleHelper.ValidCssIdentifierRegex.IsMatch(this.node.Id)) ? string.Format(CultureInfo.InvariantCulture, "#{0}", this.node.Id) : string.Empty);
				string text3 = (parameters.HasNthOfType ? string.Format(CultureInfo.InvariantCulture, ":nth-of-type({0})", this.ofTypeIndex + 1) : string.Empty);
				return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}", new object[]
				{
					this.node.Name,
					text,
					text2,
					text3
				});
			}

			// Token: 0x06004D4F RID: 19791 RVA: 0x001001D8 File Offset: 0x000FE3D8
			private string GetFullSelector(HtmlModuleHelper.FullSelectorParameters parameters)
			{
				if (this.parent == null)
				{
					return "html";
				}
				HtmlModuleHelper.FullSelectorParameters fullSelectorParameters = new HtmlModuleHelper.FullSelectorParameters((parameters.NthOfTypePresence == HtmlModuleHelper.SelectorPartPresence.OnAllNodes) ? HtmlModuleHelper.SelectorPartPresence.OnAllNodes : HtmlModuleHelper.SelectorPartPresence.OnNoNodes, parameters.NodeRelationship);
				HtmlModuleHelper.LocalSelectorParameters localSelectorParameters = new HtmlModuleHelper.LocalSelectorParameters(parameters.NthOfTypePresence == HtmlModuleHelper.SelectorPartPresence.OnAllNodes || parameters.NthOfTypePresence == HtmlModuleHelper.SelectorPartPresence.OnTargetNodeOnly);
				string text = this.parent.fullSelectors[fullSelectorParameters];
				string relationshipOperator = HtmlModuleHelper.DomNodeInfo.GetRelationshipOperator(parameters.NodeRelationship);
				string text2 = this.localSelectors[localSelectorParameters];
				return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", text, relationshipOperator, text2);
			}

			// Token: 0x0400292C RID: 10540
			private static readonly HtmlModuleHelper.FullSelectorParameters[] selectorParameters = new HtmlModuleHelper.FullSelectorParameters[]
			{
				new HtmlModuleHelper.FullSelectorParameters(HtmlModuleHelper.SelectorPartPresence.OnAllNodes, HtmlModuleHelper.NodeRelationship.Children),
				new HtmlModuleHelper.FullSelectorParameters(HtmlModuleHelper.SelectorPartPresence.OnTargetNodeOnly, HtmlModuleHelper.NodeRelationship.Children),
				new HtmlModuleHelper.FullSelectorParameters(HtmlModuleHelper.SelectorPartPresence.OnNoNodes, HtmlModuleHelper.NodeRelationship.Children),
				new HtmlModuleHelper.FullSelectorParameters(HtmlModuleHelper.SelectorPartPresence.OnNoNodes, HtmlModuleHelper.NodeRelationship.Descendants)
			};

			// Token: 0x0400292D RID: 10541
			private readonly DomNode node;

			// Token: 0x0400292E RID: 10542
			private readonly HtmlModuleHelper.DomNodeInfo parent;

			// Token: 0x0400292F RID: 10543
			private readonly int childIndex;

			// Token: 0x04002930 RID: 10544
			private readonly int ofTypeIndex;

			// Token: 0x04002931 RID: 10545
			private readonly Lazy<IList<HtmlModuleHelper.DomNodeInfo>> children;

			// Token: 0x04002932 RID: 10546
			private readonly LazyDictionary<HtmlModuleHelper.LocalSelectorParameters, string> localSelectors;

			// Token: 0x04002933 RID: 10547
			private readonly LazyDictionary<HtmlModuleHelper.FullSelectorParameters, string> fullSelectors;
		}

		// Token: 0x02000AD1 RID: 2769
		private enum SelectorPartPresence
		{
			// Token: 0x04002938 RID: 10552
			OnNoNodes,
			// Token: 0x04002939 RID: 10553
			OnAllNodes,
			// Token: 0x0400293A RID: 10554
			OnTargetNodeOnly
		}

		// Token: 0x02000AD2 RID: 2770
		private enum NodeRelationship
		{
			// Token: 0x0400293C RID: 10556
			Children,
			// Token: 0x0400293D RID: 10557
			Descendants
		}

		// Token: 0x02000AD3 RID: 2771
		private class LocalSelectorParameters
		{
			// Token: 0x06004D56 RID: 19798 RVA: 0x001002EB File Offset: 0x000FE4EB
			public LocalSelectorParameters(bool hasNthOfType)
			{
				this.HasNthOfType = hasNthOfType;
			}

			// Token: 0x0400293E RID: 10558
			public readonly bool HasNthOfType;
		}

		// Token: 0x02000AD4 RID: 2772
		private class FullSelectorParameters
		{
			// Token: 0x06004D57 RID: 19799 RVA: 0x001002FA File Offset: 0x000FE4FA
			public FullSelectorParameters(HtmlModuleHelper.SelectorPartPresence nthOfTypePresence, HtmlModuleHelper.NodeRelationship nodeRelationship)
			{
				this.NthOfTypePresence = nthOfTypePresence;
				this.NodeRelationship = nodeRelationship;
			}

			// Token: 0x0400293F RID: 10559
			public readonly HtmlModuleHelper.SelectorPartPresence NthOfTypePresence;

			// Token: 0x04002940 RID: 10560
			public readonly HtmlModuleHelper.NodeRelationship NodeRelationship;
		}
	}
}
