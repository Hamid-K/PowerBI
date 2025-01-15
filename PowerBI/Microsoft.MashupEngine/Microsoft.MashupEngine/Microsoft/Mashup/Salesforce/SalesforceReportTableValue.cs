using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x02000208 RID: 520
	internal class SalesforceReportTableValue : TableValue
	{
		// Token: 0x06000A8D RID: 2701 RVA: 0x00017BC0 File Offset: 0x00015DC0
		public SalesforceReportTableValue(IEngineHost host, SalesforceDataLoader dataLoader, string id)
		{
			this.host = host;
			this.dataLoader = dataLoader;
			this.id = id;
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00017BDD File Offset: 0x00015DDD
		public override TypeValue Type
		{
			get
			{
				if (this.rowType == null)
				{
					this.ComputeMetadata();
				}
				return TableTypeValue.New(this.rowType);
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00017BF8 File Offset: 0x00015DF8
		public override Keys Columns
		{
			get
			{
				if (this.columns == null)
				{
					this.ComputeMetadata();
				}
				return this.columns;
			}
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00017C0E File Offset: 0x00015E0E
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			Func<Value, Value>[] converters = new Func<Value, Value>[this.Columns.Length];
			for (int i = 0; i < this.Columns.Length; i++)
			{
				converters[i] = SalesforceTypes.MakeConverter(this.rowType.Fields[i]["Type"].AsType);
			}
			Dictionary<string, Value> groupingsAcross = new Dictionary<string, Value>();
			this.ParseGrouping(this.Data["groupingsAcross"].AsRecord, groupingsAcross);
			Dictionary<string, Value> groupingsDown = new Dictionary<string, Value>();
			this.ParseGrouping(this.Data["groupingsDown"].AsRecord, groupingsDown);
			RecordValue asRecord = this.Data["factMap"].AsRecord;
			foreach (NamedValue namedValue in asRecord.GetFields())
			{
				ListValue asList = namedValue.Value.AsRecord["rows"].AsList;
				if (asList.Count > 0)
				{
					string[] array = namedValue.Key.Split(new char[] { '!' });
					int groupingCount = this.downLevels + this.acrossLevels;
					Value[] groupings = new Value[groupingCount];
					this.GetGroupingParts(array[0], this.downLevels, groupingsDown, groupings, 0);
					this.GetGroupingParts(array[1], this.acrossLevels, groupingsAcross, groupings, this.downLevels);
					for (int j = 0; j < groupingCount; j++)
					{
						string text = (this.useLabels[j] ? "label" : "value");
						groupings[j] = converters[j](groupings[j].AsRecord[text]);
					}
					foreach (IValueReference valueReference in asList)
					{
						ListValue asList2 = valueReference.Value.AsRecord["dataCells"].AsList;
						IValueReference[] array2 = new IValueReference[asList2.Count + groupingCount];
						for (int k = 0; k < array2.Length; k++)
						{
							if (k < groupingCount)
							{
								array2[k] = groupings[k];
							}
							else
							{
								string text2 = (this.useLabels[k] ? "label" : "value");
								array2[k] = converters[k](asList2[k - groupingCount].AsRecord[text2]);
							}
						}
						yield return RecordValue.New(this.Columns, array2);
					}
					IEnumerator<IValueReference> enumerator = null;
					groupings = null;
				}
			}
			RecordFieldEnumerator recordFieldEnumerator = default(RecordFieldEnumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00017C20 File Offset: 0x00015E20
		private void ParseGrouping(RecordValue grouping, IDictionary<string, Value> result)
		{
			if (grouping.Keys.Contains("key"))
			{
				result[grouping["key"].AsText.String] = grouping;
			}
			foreach (IValueReference valueReference in grouping["groupings"].AsList)
			{
				this.ParseGrouping(valueReference.Value.AsRecord, result);
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00017CB0 File Offset: 0x00015EB0
		private void GetGroupingParts(string key, int levels, IDictionary<string, Value> map, Value[] result, int offset)
		{
			if (key == "T")
			{
				return;
			}
			int[] array = new int[levels];
			int i = key.IndexOf('_');
			int j = 0;
			while (i >= 0)
			{
				if (j >= levels)
				{
					break;
				}
				array[j++] = i;
				i = key.IndexOf('_', i + 1);
			}
			while (j < levels)
			{
				array[j++] = key.Length;
			}
			for (int k = 0; k < levels; k++)
			{
				string text = key.Substring(0, array[k]);
				if (!map.TryGetValue(text, out result[offset + k]))
				{
					result[offset + k] = Value.Null;
				}
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x00017D48 File Offset: 0x00015F48
		private RecordValue Data
		{
			get
			{
				if (this.data == null)
				{
					string text = string.Concat(new string[]
					{
						this.dataLoader.ReportsPath,
						"/",
						this.id,
						"?",
						"includeDetails=true"
					});
					string text2 = this.dataLoader.CreateCacheKey(new string[] { "Reports", this.id });
					this.data = this.dataLoader.LoadJsonValue(text, text2).AsRecord;
				}
				return this.data;
			}
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00017DDC File Offset: 0x00015FDC
		private void ComputeMetadata()
		{
			RecordValue asRecord = this.Data["reportMetadata"].AsRecord;
			ListValue asList = asRecord["detailColumns"].AsList;
			ListValue asList2 = asRecord["groupingsAcross"].AsList;
			IEnumerable<IValueReference> asList3 = asRecord["groupingsDown"].AsList;
			RecordValue asRecord2 = this.Data["reportExtendedMetadata"].AsRecord;
			RecordValue detailColumnInfo = asRecord2["detailColumnInfo"].AsRecord;
			RecordValue groupingColumnInfo = asRecord2["groupingColumnInfo"].AsRecord;
			IEnumerable<string> enumerable = asList2.Select((IValueReference g) => g.Value.AsRecord["name"].AsText.String);
			IEnumerable<string> enumerable2 = asList3.Select((IValueReference g) => g.Value.AsRecord["name"].AsText.String);
			IEnumerable<string> enumerable3 = asList.Select((IValueReference k) => k.Value.AsText.String);
			IEnumerable<RecordValue> enumerable4 = from g in enumerable2.Concat(enumerable)
				select groupingColumnInfo[g].AsRecord;
			IEnumerable<RecordValue> enumerable5 = enumerable3.Select((string k) => detailColumnInfo[k].AsRecord);
			string[] array = (from r in enumerable4.Concat(enumerable5)
				select r["label"].AsText.String).ToArray<string>();
			string[] array2 = (from r in enumerable4.Concat(enumerable5)
				select r["dataType"].AsText.String).ToArray<string>();
			this.columns = ColumnLabelGenerator.GenerateKeys(array, array.Length);
			this.acrossLevels = enumerable.Count<string>();
			this.downLevels = enumerable2.Count<string>();
			this.useLabels = new bool[array.Length];
			Value[] array3 = new Value[array.Length];
			for (int i = 0; i < this.columns.Length; i++)
			{
				TypeValue typeValue = SalesforceTypes.TranslateType(array2[i], true);
				array3[i] = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					typeValue,
					LogicalValue.True
				});
				this.useLabels[i] = typeValue.TypeKind == ValueKind.Text || typeValue.TypeKind == ValueKind.Any;
			}
			this.rowType = RecordTypeValue.New(RecordValue.New(this.columns, array3));
		}

		// Token: 0x0400063F RID: 1599
		private readonly IEngineHost host;

		// Token: 0x04000640 RID: 1600
		private readonly SalesforceDataLoader dataLoader;

		// Token: 0x04000641 RID: 1601
		private readonly string id;

		// Token: 0x04000642 RID: 1602
		private Keys columns;

		// Token: 0x04000643 RID: 1603
		private bool[] useLabels;

		// Token: 0x04000644 RID: 1604
		private RecordTypeValue rowType;

		// Token: 0x04000645 RID: 1605
		private RecordValue data;

		// Token: 0x04000646 RID: 1606
		private int acrossLevels;

		// Token: 0x04000647 RID: 1607
		private int downLevels;
	}
}
