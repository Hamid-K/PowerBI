using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils.Text;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x02001607 RID: 5639
	public class LearnDebugTrace
	{
		// Token: 0x17002058 RID: 8280
		// (get) Token: 0x0600BBB5 RID: 48053 RVA: 0x00285AD1 File Offset: 0x00283CD1
		public List<CacheEvent> CacheEvents { get; } = new List<CacheEvent>();

		// Token: 0x17002059 RID: 8281
		// (get) Token: 0x0600BBB6 RID: 48054 RVA: 0x00285AD9 File Offset: 0x00283CD9
		public List<CountEvent> CountEvents { get; } = new List<CountEvent>();

		// Token: 0x1700205A RID: 8282
		// (get) Token: 0x0600BBB7 RID: 48055 RVA: 0x00285AE1 File Offset: 0x00283CE1
		// (set) Token: 0x0600BBB8 RID: 48056 RVA: 0x00285AE9 File Offset: 0x00283CE9
		public bool EnableConditional { get; set; }

		// Token: 0x1700205B RID: 8283
		// (get) Token: 0x0600BBB9 RID: 48057 RVA: 0x00285AF2 File Offset: 0x00283CF2
		// (set) Token: 0x0600BBBA RID: 48058 RVA: 0x00285AFA File Offset: 0x00283CFA
		public bool EnableOutput { get; set; }

		// Token: 0x1700205C RID: 8284
		// (get) Token: 0x0600BBBB RID: 48059 RVA: 0x00285B03 File Offset: 0x00283D03
		// (set) Token: 0x0600BBBC RID: 48060 RVA: 0x00285B0B File Offset: 0x00283D0B
		public bool EnableStatistics { get; set; }

		// Token: 0x1700205D RID: 8285
		// (get) Token: 0x0600BBBD RID: 48061 RVA: 0x00285B14 File Offset: 0x00283D14
		// (set) Token: 0x0600BBBE RID: 48062 RVA: 0x00285B1C File Offset: 0x00283D1C
		public bool EnableWitness { get; set; }

		// Token: 0x1700205E RID: 8286
		// (get) Token: 0x0600BBBF RID: 48063 RVA: 0x00285B25 File Offset: 0x00283D25
		// (set) Token: 0x0600BBC0 RID: 48064 RVA: 0x00285B2D File Offset: 0x00283D2D
		public WitnessDebugGrouping DefaultWitnessGrouping { get; set; } = WitnessDebugGrouping.ByExampleAndWitness;

		// Token: 0x1700205F RID: 8287
		// (get) Token: 0x0600BBC1 RID: 48065 RVA: 0x00285B36 File Offset: 0x00283D36
		// (set) Token: 0x0600BBC2 RID: 48066 RVA: 0x00285B3E File Offset: 0x00283D3E
		public Func<WitnessEvent, bool> DefaultWitnessGroupingFilter { get; set; } = (WitnessEvent _) => true;

		// Token: 0x17002060 RID: 8288
		// (get) Token: 0x0600BBC3 RID: 48067 RVA: 0x00285B47 File Offset: 0x00283D47
		// (set) Token: 0x0600BBC4 RID: 48068 RVA: 0x00285B4F File Offset: 0x00283D4F
		public IDictionary<IRow, string> ExampleLookup { get; set; }

		// Token: 0x17002061 RID: 8289
		// (get) Token: 0x0600BBC5 RID: 48069 RVA: 0x00285B58 File Offset: 0x00283D58
		// (set) Token: 0x0600BBC6 RID: 48070 RVA: 0x00285B60 File Offset: 0x00283D60
		public Func<string> RenderConditional { get; set; }

		// Token: 0x17002062 RID: 8290
		// (get) Token: 0x0600BBC7 RID: 48071 RVA: 0x00285B69 File Offset: 0x00283D69
		public List<TimedEvent> TimedEvents { get; } = new List<TimedEvent>();

		// Token: 0x17002063 RID: 8291
		// (get) Token: 0x0600BBC8 RID: 48072 RVA: 0x00285B71 File Offset: 0x00283D71
		public List<WitnessEvent> WitnessEvents { get; } = new List<WitnessEvent>();

		// Token: 0x0600BBC9 RID: 48073 RVA: 0x00285B79 File Offset: 0x00283D79
		public override string ToString()
		{
			return this.Render();
		}

		// Token: 0x0600BBCA RID: 48074 RVA: 0x00285B84 File Offset: 0x00283D84
		private string FormatExample(WitnessEvent item)
		{
			if (this.ExampleLookup == null || item.InputRow == null)
			{
				return string.Empty;
			}
			string text;
			if (!this.ExampleLookup.TryGetValue(item.InputRow, out text))
			{
				return item.InputRow.ToString();
			}
			return text;
		}

		// Token: 0x0600BBCB RID: 48075 RVA: 0x00285BCC File Offset: 0x00283DCC
		private void PopulateItemTier()
		{
			List<WitnessEvent> witnessEvents = this.WitnessEvents;
			lock (witnessEvents)
			{
				foreach (WitnessEvent witnessEvent in this.WitnessEvents)
				{
					LearnDebugTrace.TierInfo tierInfo;
					if (LearnDebugTrace._tierLookup.TryGetValue(witnessEvent.WitnessName, out tierInfo))
					{
						witnessEvent.Tier = new int?(tierInfo.Tier);
						witnessEvent.MethodOrder = tierInfo.MethodOrder;
					}
				}
			}
		}

		// Token: 0x0600BBCC RID: 48076 RVA: 0x00285C74 File Offset: 0x00283E74
		public void HitEvent(string category, string name = null, int count = 1)
		{
			CacheEvent cacheEvent = this.CacheEvents.FirstOrDefault((CacheEvent e) => e.Category == category && e.Name == name);
			if (cacheEvent == null)
			{
				this.CacheEvents.Add(new CacheEvent
				{
					Category = category,
					Name = name,
					HitCount = count
				});
				return;
			}
			cacheEvent.HitCount += count;
		}

		// Token: 0x0600BBCD RID: 48077 RVA: 0x00285CF0 File Offset: 0x00283EF0
		public void MissEvent(string category, string name = null, int count = 1)
		{
			CacheEvent cacheEvent = this.CacheEvents.FirstOrDefault((CacheEvent e) => e.Category == category && e.Name == name);
			if (cacheEvent == null)
			{
				this.CacheEvents.Add(new CacheEvent
				{
					Category = category,
					Name = name,
					MissCount = count
				});
				return;
			}
			cacheEvent.MissCount += count;
		}

		// Token: 0x0600BBCE RID: 48078 RVA: 0x00285D6A File Offset: 0x00283F6A
		public void CountEvent(string category, string name, int count = 1)
		{
			this.CountEvents.Add(new CountEvent
			{
				Category = category,
				Name = name,
				Count = count
			});
		}

		// Token: 0x0600BBCF RID: 48079 RVA: 0x00002188 File Offset: 0x00000388
		public TimedEvent StartTimedEvent(string category, string name, bool focus = false, bool allowGroup = true)
		{
			return null;
		}

		// Token: 0x0600BBD0 RID: 48080 RVA: 0x00285D91 File Offset: 0x00283F91
		public string Render()
		{
			return this.Render(this.DefaultWitnessGrouping, this.DefaultWitnessGroupingFilter);
		}

		// Token: 0x0600BBD1 RID: 48081 RVA: 0x00285DA8 File Offset: 0x00283FA8
		public string Render(WitnessDebugGrouping grouping, Func<WitnessEvent, bool> filter)
		{
			if (!this.EnableOutput)
			{
				return string.Empty;
			}
			TextBuilder textBuilder = TextBuilder.Create(4);
			if (this.EnableStatistics)
			{
				this.RenderStatistics(textBuilder);
			}
			if (this.EnableConditional && this.RenderConditional != null)
			{
				TextBuilder textBuilder2 = textBuilder;
				string text = this.RenderConditional();
				textBuilder2.AddLine((text != null) ? text.TrimEnd(Array.Empty<char>()) : null).AddBlankLine();
			}
			if (this.EnableWitness)
			{
				if (filter == null)
				{
					filter = (WitnessEvent _) => true;
				}
				IReadOnlyList<WitnessEvent> readOnlyList = this.WitnessEvents.Where(filter).ToReadOnlyList<WitnessEvent>();
				string text2;
				switch (grouping)
				{
				case WitnessDebugGrouping.ByNatural:
					text2 = this.RenderByNatural(readOnlyList);
					break;
				case WitnessDebugGrouping.ByWitness:
					text2 = this.RenderByWitness(readOnlyList);
					break;
				case WitnessDebugGrouping.ByWitnessAndExample:
					text2 = this.RenderByWitnessAndExample(readOnlyList);
					break;
				case WitnessDebugGrouping.ByExample:
					text2 = this.RenderByExample(readOnlyList);
					break;
				case WitnessDebugGrouping.ByExampleAndWitness:
					text2 = this.RenderByExampleAndWitness(readOnlyList);
					break;
				default:
					throw new ArgumentOutOfRangeException("grouping", grouping, null);
				}
				string text3 = text2;
				if (text3.Any<char>())
				{
					textBuilder.AddSection(string.Format("Witness Functions ({0:N0})", this.WitnessEvents.Count), text3.Trim(), 2);
				}
			}
			return textBuilder.Render();
		}

		// Token: 0x0600BBD2 RID: 48082 RVA: 0x00285EEC File Offset: 0x002840EC
		public TextBuilder RenderStatistics(TextBuilder builder)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			int? num;
			if (this.TimedEvents.Any<TimedEvent>())
			{
				TextTableBorder textTableBorder = TextTableBorder.None;
				num = null;
				int? num2 = num;
				num = null;
				TextTableBuilder textTableBuilder = TextTableBuilder.Create(textTableBorder, num2, num);
				string text4 = "";
				int num3 = 0;
				num = null;
				int? num4 = num;
				bool flag = false;
				string text5 = null;
				string text6 = null;
				num = null;
				int? num5 = num;
				num = null;
				TextTableBuilder textTableBuilder2 = textTableBuilder.AddColumn(text4, num3, num4, flag, text5, text6, num5, num).AddBorderColumn();
				string text7 = "Calls";
				string text8 = "N0";
				int num6 = 0;
				string text9 = "--";
				num = null;
				int? num7 = num;
				num = null;
				TextTableBuilder textTableBuilder3 = textTableBuilder2.AddNumberColumn(text7, text8, num6, text9, num7, num).AddBorderColumn();
				string text10 = "Total (ms)";
				string text11 = "N2";
				int num8 = 0;
				string text12 = "--";
				num = null;
				int? num9 = num;
				num = null;
				ITextRowBuilder textRowBuilder = textTableBuilder3.AddNumberColumn(text10, text11, num8, text12, num9, num).AddHeadingRow();
				int k;
				object[][] array = (from item in this.TimedEvents
					where item.Focus
					group item by item.Category).OrderBy(delegate(IGrouping<string, TimedEvent> g)
				{
					if (g.Key.StartsWith("Learner"))
					{
						return 10;
					}
					if (g.Key.StartsWith("Witness"))
					{
						return 20;
					}
					if (!g.Key.StartsWith("Recognition"))
					{
						return 100;
					}
					if (!g.Key.Contains("/"))
					{
						return 30;
					}
					return 35;
				}).Select(delegate(IGrouping<string, TimedEvent> g)
				{
					object[] array6 = new object[3];
					array6[0] = g.Key;
					array6[1] = g.Count<TimedEvent>();
					int num31 = 2;
					object obj;
					if (!(g.Key == "Witness"))
					{
						obj = string.Empty;
					}
					else
					{
						obj = g.Sum((TimedEvent i) => k.Elapsed);
					}
					array6[num31] = obj;
					return array6;
				}).ToArray<object[]>();
				object[][] array2 = array;
				for (k = 0; k < array2.Length; k++)
				{
					object[] array3 = array2[k];
					string category2 = array3[0] as string;
					ITextRowBuilder textRowBuilder2 = textRowBuilder.AddBorderRow().AddDataRow(new object[]
					{
						array3[0],
						array3[1],
						array3[2]
					});
					IReadOnlyList<IReadOnlyList<object>> readOnlyList = (from item in this.TimedEvents
						where item.Focus
						where item.Category == category2 && item.AllowGroup
						group item by item.Name into g
						orderby g.Key
						select g).Select(delegate(IGrouping<string, TimedEvent> g)
					{
						object[] array7 = new object[3];
						array7[0] = "  " + g.Key;
						array7[1] = g.Count<TimedEvent>();
						array7[2] = g.Sum((TimedEvent i) => k.Elapsed);
						return array7;
					}).ToArray<object[]>();
					num = null;
					textRowBuilder2.AddDataRows(readOnlyList, num);
					int m = 1;
					ITextRowBuilder textRowBuilder3 = textRowBuilder;
					IReadOnlyList<IReadOnlyList<object>> readOnlyList2 = this.TimedEvents.Where((TimedEvent item) => item.Category == category2 && !item.AllowGroup).Select(delegate(TimedEvent item)
					{
						object[] array8 = new object[3];
						int num32 = 0;
						string text34 = "  {0}:{1}";
						object name = item.Name;
						int k = m;
						m = k + 1;
						array8[num32] = string.Format(text34, name, k);
						array8[1] = 1;
						array8[2] = item.Elapsed;
						return array8;
					}).ToArray<object[]>();
					num = null;
					textRowBuilder3.AddDataRows(readOnlyList2, num);
				}
				object[][] array4 = (from <>h__TransparentIdentifier1 in (from item in this.TimedEvents
						group item by item.Category into g
						let count = g.Count<TimedEvent>()
						select new
						{
							<>h__TransparentIdentifier0 = <>h__TransparentIdentifier0,
							sum = g.Sum((TimedEvent i) => m.Elapsed)
						}).OrderBy(delegate(<>h__TransparentIdentifier1)
					{
						if (<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.g.Key.StartsWith("Learner"))
						{
							return 10;
						}
						if (<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.g.Key.StartsWith("Witness"))
						{
							return 20;
						}
						if (!<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.g.Key.StartsWith("Recognition"))
						{
							return 100;
						}
						if (!<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.g.Key.Contains("/"))
						{
							return 30;
						}
						return 35;
					})
					select new object[]
					{
						<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.g.Key,
						<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.count,
						(<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.g.Key == "Witness") ? <>h__TransparentIdentifier1.sum : string.Empty
					}).ToArray<object[]>();
				if (array.Any<object[]>())
				{
					textRowBuilder.AddDoubleBorderRow();
				}
				array2 = array4;
				for (k = 0; k < array2.Length; k++)
				{
					object[] array5 = array2[k];
					string category = array5[0] as string;
					textRowBuilder.AddDataRow(array5);
					ITextRowBuilder textRowBuilder4 = textRowBuilder;
					IReadOnlyList<IReadOnlyList<object>> readOnlyList3 = (from item in this.TimedEvents
						where item.Category == category && item.AllowGroup
						group item by item.Name into g
						orderby g.Key
						select g).Select(delegate(IGrouping<string, TimedEvent> g)
					{
						object[] array9 = new object[3];
						array9[0] = "  " + g.Key;
						array9[1] = g.Count<TimedEvent>();
						array9[2] = g.Sum((TimedEvent i) => m.Elapsed);
						return array9;
					}).ToArray<object[]>();
					num = null;
					textRowBuilder4.AddDataRows(readOnlyList3, num);
					int i = 1;
					ITextRowBuilder textRowBuilder5 = textRowBuilder;
					IReadOnlyList<IReadOnlyList<object>> readOnlyList4 = this.TimedEvents.Where((TimedEvent item) => item.Category == category && !item.AllowGroup).Select(delegate(TimedEvent item)
					{
						object[] array10 = new object[3];
						int num33 = 0;
						string text35 = "  {0}:{1}";
						object name2 = item.Name;
						int m = i;
						i = m + 1;
						array10[num33] = string.Format(text35, name2, m);
						array10[1] = 1;
						array10[2] = item.Elapsed;
						return array10;
					}).ToArray<object[]>();
					num = null;
					textRowBuilder5.AddDataRows(readOnlyList4, num);
					textRowBuilder.AddBorderRow();
				}
				text = textRowBuilder.Render();
			}
			if (this.CacheEvents.Any<CacheEvent>())
			{
				TextTableBorder textTableBorder2 = TextTableBorder.None;
				num = null;
				int? num10 = num;
				num = null;
				TextTableBuilder textTableBuilder4 = TextTableBuilder.Create(textTableBorder2, num10, num);
				string text13 = "";
				int num11 = 0;
				num = null;
				int? num12 = num;
				bool flag2 = false;
				string text14 = null;
				string text15 = null;
				num = null;
				int? num13 = num;
				num = null;
				TextTableBuilder textTableBuilder5 = textTableBuilder4.AddColumn(text13, num11, num12, flag2, text14, text15, num13, num).AddBorderColumn();
				string text16 = "Miss";
				string text17 = "N0";
				int num14 = 0;
				string text18 = "--";
				num = null;
				int? num15 = num;
				num = null;
				TextTableBuilder textTableBuilder6 = textTableBuilder5.AddNumberColumn(text16, text17, num14, text18, num15, num).AddBorderColumn();
				string text19 = "Hit";
				string text20 = "N0";
				int num16 = 0;
				string text21 = "--";
				num = null;
				int? num17 = num;
				num = null;
				TextTableBuilder textTableBuilder7 = textTableBuilder6.AddNumberColumn(text19, text20, num16, text21, num17, num);
				string empty = string.Empty;
				string text22 = "P0";
				int num18 = 0;
				string text23 = "--";
				num = null;
				int? num19 = num;
				num = null;
				ITextRowBuilder textRowBuilder6 = textTableBuilder7.AddNumberColumn(empty, text22, num18, text23, num19, num).AddHeadingRow().AddBorderRow();
				object[][] array2 = (from item in this.CacheEvents
					group item by item.Category into g
					let hitCount = g.Sum((CacheEvent i) => i.HitCount)
					let missCount = g.Sum((CacheEvent i) => i.MissCount)
					let totalCount = hitCount + missCount
					let hitRate = (totalCount == 0) ? null : new double?((double)hitCount / (double)totalCount)
					orderby g.Key
					select new object[] { g.Key, missCount, hitCount, hitRate }).ToArray<object[]>();
				for (int k = 0; k < array2.Length; k++)
				{
					object[] categoryRow2 = array2[k];
					textRowBuilder6.AddDataRow(categoryRow2);
					ITextRowBuilder textRowBuilder7 = textRowBuilder6;
					IReadOnlyList<IReadOnlyList<object>> readOnlyList5 = (from item in this.CacheEvents
						where item.Category == categoryRow2[0] as string
						group item by item.Name into g
						let hitCount = g.Sum((CacheEvent i) => i.HitCount)
						let missCount = g.Sum((CacheEvent i) => i.MissCount)
						let totalCount = hitCount + missCount
						let hitRate = (totalCount == 0) ? null : new double?((double)hitCount / (double)totalCount)
						orderby g.Key
						select new object[]
						{
							"  " + g.Key,
							missCount,
							hitCount,
							hitRate
						}).ToArray<object[]>();
					num = null;
					textRowBuilder7.AddDataRows(readOnlyList5, num);
					textRowBuilder6.AddBorderRow();
				}
				text2 = textRowBuilder6.Render();
			}
			if (this.CountEvents.Any<CountEvent>())
			{
				TextTableBorder textTableBorder3 = TextTableBorder.None;
				num = null;
				int? num20 = num;
				num = null;
				TextTableBuilder textTableBuilder8 = TextTableBuilder.Create(textTableBorder3, num20, num);
				string text24 = "";
				int num21 = 0;
				num = null;
				int? num22 = num;
				bool flag3 = false;
				string text25 = null;
				string text26 = null;
				num = null;
				int? num23 = num;
				num = null;
				TextTableBuilder textTableBuilder9 = textTableBuilder8.AddColumn(text24, num21, num22, flag3, text25, text26, num23, num).AddBorderColumn();
				string text27 = "Count";
				string text28 = "N0";
				int num24 = 0;
				string text29 = "--";
				num = null;
				int? num25 = num;
				num = null;
				ITextRowBuilder textRowBuilder8 = textTableBuilder9.AddNumberColumn(text27, text28, num24, text29, num25, num).AddHeadingRow().AddBorderRow();
				object[][] array2 = (from item in this.CountEvents
					group item by item.Category into g
					let count = g.Sum((CountEvent i) => i.Count)
					orderby g.Key
					select new object[] { g.Key, count }).ToArray<object[]>();
				for (int k = 0; k < array2.Length; k++)
				{
					object[] categoryRow = array2[k];
					textRowBuilder8.AddDataRow(categoryRow);
					ITextRowBuilder textRowBuilder9 = textRowBuilder8;
					IReadOnlyList<IReadOnlyList<object>> readOnlyList6 = (from item in this.CountEvents
						where item.Category == categoryRow[0] as string
						group item by item.Name into g
						let count = g.Sum((CountEvent i) => i.Count)
						orderby g.Key
						select new object[]
						{
							"  " + g.Key,
							count
						}).ToArray<object[]>();
					num = null;
					textRowBuilder9.AddDataRows(readOnlyList6, num);
					textRowBuilder8.AddBorderRow();
				}
				text3 = textRowBuilder8.Render();
			}
			TextTableBuilder textTableBuilder10 = TextTableBuilder.Create(TextTableBorder.None, null, null);
			string text30 = "";
			int num26 = 0;
			int? num27 = new int?(2);
			TextTableBuilder textTableBuilder11 = textTableBuilder10.AddColumn(text30, num26, null, false, null, null, null, num27);
			string text31 = "";
			int num28 = 0;
			num = new int?(2);
			num27 = null;
			int? num29 = num27;
			bool flag4 = false;
			string text32 = null;
			string text33 = null;
			int? num30 = num;
			num27 = null;
			ITextRowBuilder textRowBuilder10 = textTableBuilder11.AddColumn(text31, num28, num29, flag4, text32, text33, num30, num27).AddDataRow(new object[]
			{
				text,
				text2 + Environment.NewLine + text3
			});
			return builder.AddSection("Statistics", textRowBuilder10.Render(), 2);
		}

		// Token: 0x0600BBD3 RID: 48083 RVA: 0x00286A04 File Offset: 0x00284C04
		public string RenderByExample(IReadOnlyList<WitnessEvent> items)
		{
			this.PopulateItemTier();
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1;
			foreach (IGrouping<IRow, WitnessEvent> grouping in from item in items
				group item by item.InputRow into eg
				orderby eg.Min((WitnessEvent i) => i.Order)
				select eg)
			{
				stringBuilder.AppendLine(string.Format("{0,3}: {1}", num++, this.FormatExample(grouping.First<WitnessEvent>())));
				stringBuilder.AppendLine();
				int num2 = 1;
				foreach (WitnessEvent witnessEvent in grouping.OrderBy((WitnessEvent i) => i.Order))
				{
					stringBuilder.AppendLine(string.Format("   T{0}.{1,3}: {2}", witnessEvent.Tier.GetValueOrDefault(), num2++, witnessEvent.WitnessName));
					stringBuilder.AppendLine(string.Format("           {0}", witnessEvent));
				}
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600BBD4 RID: 48084 RVA: 0x00286B88 File Offset: 0x00284D88
		public string RenderByExampleAndWitness(IReadOnlyList<WitnessEvent> items)
		{
			this.PopulateItemTier();
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1;
			foreach (IGrouping<IRow, WitnessEvent> grouping in from item in items
				group item by item.InputRow into eg
				orderby eg.Min((WitnessEvent i) => i.Order)
				select eg)
			{
				stringBuilder.AppendLine(string.Format("{0,3}: {1}", num++, this.FormatExample(grouping.First<WitnessEvent>())));
				stringBuilder.AppendLine();
				int num2 = 1;
				int num3 = 1;
				foreach (IGrouping<string, WitnessEvent> grouping2 in from ig in grouping
					group ig by ig.WitnessName into wg
					orderby wg.Min((WitnessEvent i) => i.Tier), wg.Min((WitnessEvent i) => i.MethodOrder)
					select wg)
				{
					stringBuilder.AppendLine(string.Format("      T{0}.{1,3}: {2}", grouping2.Min((WitnessEvent i) => i.Tier).GetValueOrDefault(), num3++, grouping2.Key));
					foreach (WitnessEvent witnessEvent in grouping2.OrderBy((WitnessEvent i) => i.Order))
					{
						stringBuilder.AppendLine(string.Format("            {0,4}|{1,4}: {2}", num2++, witnessEvent.Order, witnessEvent));
					}
				}
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600BBD5 RID: 48085 RVA: 0x00286E10 File Offset: 0x00285010
		public string RenderByNatural(IReadOnlyList<WitnessEvent> items)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (WitnessEvent witnessEvent in items)
			{
				stringBuilder.AppendLine(string.Format("{0,3}: {1}", witnessEvent.Order, witnessEvent.WitnessName));
				stringBuilder.AppendLine(string.Format("     {0}", witnessEvent));
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600BBD6 RID: 48086 RVA: 0x00286E98 File Offset: 0x00285098
		public string RenderByWitness(IReadOnlyList<WitnessEvent> items)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (IGrouping<string, WitnessEvent> grouping in from item in items
				group item by item.WitnessName into wg
				orderby wg.Min((WitnessEvent i) => i.Tier)
				select wg)
			{
				stringBuilder.AppendLine(grouping.Key);
				foreach (WitnessEvent witnessEvent in grouping.OrderBy((WitnessEvent i) => i.Order))
				{
					stringBuilder.AppendLine(string.Format("{0,3}: {1}", witnessEvent.Order, this.FormatExample(witnessEvent)));
					stringBuilder.AppendLine(string.Format("     {0}", witnessEvent));
				}
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600BBD7 RID: 48087 RVA: 0x00286FD4 File Offset: 0x002851D4
		public string RenderByWitnessAndExample(IReadOnlyList<WitnessEvent> items)
		{
			this.PopulateItemTier();
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1;
			foreach (IGrouping<string, WitnessEvent> grouping in from item in items
				group item by item.WitnessName into eg
				orderby eg.Min((WitnessEvent i) => i.Tier)
				select eg)
			{
				int valueOrDefault = grouping.Min((WitnessEvent i) => i.Tier).GetValueOrDefault();
				stringBuilder.AppendLine(string.Format("T{0}.{1,3}: {2}", valueOrDefault, num++, grouping.Key));
				stringBuilder.AppendLine();
				int num2 = 1;
				int num3 = 1;
				foreach (IGrouping<IRow, WitnessEvent> grouping2 in from eg in grouping
					group eg by eg.InputRow into eg
					orderby eg.Min((WitnessEvent i) => i.Order), eg.Min((WitnessEvent i) => i.MethodOrder)
					select eg)
				{
					stringBuilder.AppendLine(string.Format("{0,3}: {1}", num3++, this.FormatExample(grouping2.First<WitnessEvent>())));
					foreach (WitnessEvent witnessEvent in grouping2.OrderBy((WitnessEvent i) => i.Order))
					{
						stringBuilder.AppendLine(string.Format("      {0,4}|{1,4}: {2}", num2++, witnessEvent.Order, witnessEvent));
					}
				}
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040046F3 RID: 18163
		private static readonly IDictionary<string, LearnDebugTrace.TierInfo> _tierLookup = new Dictionary<string, LearnDebugTrace.TierInfo>
		{
			{
				"ConcatPrefix",
				new LearnDebugTrace.TierInfo(1, 101)
			},
			{
				"ConcatSuffix",
				new LearnDebugTrace.TierInfo(1, 102)
			},
			{
				"LowerCase",
				new LearnDebugTrace.TierInfo(2, 101)
			},
			{
				"ProperCase",
				new LearnDebugTrace.TierInfo(2, 102)
			},
			{
				"UpperCase",
				new LearnDebugTrace.TierInfo(2, 103)
			},
			{
				"LetX",
				new LearnDebugTrace.TierInfo(3, 301)
			},
			{
				"InputColumnName",
				new LearnDebugTrace.TierInfo(3, 302)
			},
			{
				"SplitSubject",
				new LearnDebugTrace.TierInfo(4, 401)
			},
			{
				"SplitDelimiter",
				new LearnDebugTrace.TierInfo(4, 402)
			},
			{
				"SplitInstance",
				new LearnDebugTrace.TierInfo(4, 403)
			},
			{
				"RoundNumberSubject",
				new LearnDebugTrace.TierInfo(4, 404)
			},
			{
				"RoundNumberFormat",
				new LearnDebugTrace.TierInfo(4, 405)
			},
			{
				"AsNumberColumnName",
				new LearnDebugTrace.TierInfo(4, 406)
			},
			{
				"ParseNumberSubject",
				new LearnDebugTrace.TierInfo(4, 407)
			},
			{
				"ParseNumber",
				new LearnDebugTrace.TierInfo(4, 408)
			},
			{
				"ParseNumberLocale",
				new LearnDebugTrace.TierInfo(4, 409)
			},
			{
				"FormatNumberSubject",
				new LearnDebugTrace.TierInfo(4, 410)
			},
			{
				"FormatNumberFormat",
				new LearnDebugTrace.TierInfo(4, 411)
			},
			{
				"AsDateTimeColumnName",
				new LearnDebugTrace.TierInfo(4, 412)
			},
			{
				"ParseDateTimeSubject",
				new LearnDebugTrace.TierInfo(4, 413)
			},
			{
				"ParseDateTimeSubjectLocale",
				new LearnDebugTrace.TierInfo(4, 414)
			},
			{
				"FormatDateTimeSubject",
				new LearnDebugTrace.TierInfo(4, 415)
			},
			{
				"FormatDateTimeFormat",
				new LearnDebugTrace.TierInfo(4, 416)
			},
			{
				"Trim",
				new LearnDebugTrace.TierInfo(5, 501)
			},
			{
				"SliceSubject",
				new LearnDebugTrace.TierInfo(5, 502)
			},
			{
				"SlicePos1",
				new LearnDebugTrace.TierInfo(5, 503)
			},
			{
				"SlicePos2",
				new LearnDebugTrace.TierInfo(5, 504)
			},
			{
				"Str",
				new LearnDebugTrace.TierInfo(6, 601)
			},
			{
				"AbsK",
				new LearnDebugTrace.TierInfo(6, 602)
			},
			{
				"End",
				new LearnDebugTrace.TierInfo(6, 603)
			},
			{
				"OffsetPosition",
				new LearnDebugTrace.TierInfo(6, 604)
			},
			{
				"OffsetK",
				new LearnDebugTrace.TierInfo(6, 605)
			},
			{
				"FindSubject",
				new LearnDebugTrace.TierInfo(7, 701)
			},
			{
				"FindDelimiter",
				new LearnDebugTrace.TierInfo(7, 702)
			},
			{
				"FindInstance",
				new LearnDebugTrace.TierInfo(7, 703)
			},
			{
				"FindOffset",
				new LearnDebugTrace.TierInfo(7, 704)
			},
			{
				"FindDigitsSubject",
				new LearnDebugTrace.TierInfo(7, 705)
			},
			{
				"FindDigitsInstance",
				new LearnDebugTrace.TierInfo(7, 706)
			},
			{
				"MatchSubject",
				new LearnDebugTrace.TierInfo(7, 707)
			},
			{
				"MatchPattern",
				new LearnDebugTrace.TierInfo(7, 708)
			},
			{
				"MatchInstance",
				new LearnDebugTrace.TierInfo(7, 709)
			},
			{
				"MatchEndSubject",
				new LearnDebugTrace.TierInfo(7, 710)
			},
			{
				"MatchEndPattern",
				new LearnDebugTrace.TierInfo(7, 711)
			},
			{
				"MatchEndInstance",
				new LearnDebugTrace.TierInfo(7, 712)
			},
			{
				"MatchFullSubject",
				new LearnDebugTrace.TierInfo(7, 713)
			},
			{
				"MatchFullPattern",
				new LearnDebugTrace.TierInfo(7, 714)
			}
		};

		// Token: 0x02001608 RID: 5640
		private class TierInfo
		{
			// Token: 0x0600BBDA RID: 48090 RVA: 0x002876C8 File Offset: 0x002858C8
			public TierInfo(int tier, int methodOrder)
			{
				this.Tier = tier;
				this.MethodOrder = methodOrder;
			}

			// Token: 0x17002064 RID: 8292
			// (get) Token: 0x0600BBDB RID: 48091 RVA: 0x002876DE File Offset: 0x002858DE
			public int MethodOrder { get; }

			// Token: 0x17002065 RID: 8293
			// (get) Token: 0x0600BBDC RID: 48092 RVA: 0x002876E6 File Offset: 0x002858E6
			public int Tier { get; }
		}
	}
}
