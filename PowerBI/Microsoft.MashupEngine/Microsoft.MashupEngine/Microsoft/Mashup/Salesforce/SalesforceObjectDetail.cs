using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001D6 RID: 470
	internal class SalesforceObjectDetail : SalesforceObjectHeader
	{
		// Token: 0x06000946 RID: 2374 RVA: 0x000127E8 File Offset: 0x000109E8
		public SalesforceObjectDetail(BinaryReader reader)
			: base(reader)
		{
			int num = reader.ReadInt32();
			this.fields.Capacity = num;
			for (int i = 0; i < num; i++)
			{
				this.fields.Add(new SalesforceObjectField(reader));
			}
			num = reader.ReadInt32();
			this.relationships.Capacity = num;
			for (int j = 0; j < num; j++)
			{
				this.relationships.Add(new SalesforceObjectRelationship(reader));
			}
			this.keyColumns = this.GetTableKeys();
			this.detail = this;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00012885 File Offset: 0x00010A85
		public SalesforceObjectDetail(JsonTokenizer json)
			: base(json)
		{
			this.keyColumns = this.GetTableKeys();
			this.detail = this;
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x000128B7 File Offset: 0x00010AB7
		public int[] KeyColumns
		{
			get
			{
				return this.keyColumns;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x000128BF File Offset: 0x00010ABF
		public IList<SalesforceObjectField> Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x000128C7 File Offset: 0x00010AC7
		public Keys KeyColumnNames
		{
			get
			{
				return Keys.New(this.KeyColumns.Select((int idx) => this.fields[idx].Name).ToArray<string>());
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x000128EA File Offset: 0x00010AEA
		public RecordTypeValue Type
		{
			get
			{
				if (this.type == null)
				{
					this.type = this.CreateBaseObjectType();
				}
				return this.type;
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00012908 File Offset: 0x00010B08
		public new void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			writer.Write(this.fields.Count);
			foreach (SalesforceObjectField salesforceObjectField in this.fields)
			{
				salesforceObjectField.Serialize(writer);
			}
			writer.Write(this.relationships.Count);
			foreach (SalesforceObjectRelationship salesforceObjectRelationship in this.relationships)
			{
				salesforceObjectRelationship.Serialize(writer);
			}
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x000129C4 File Offset: 0x00010BC4
		protected override void LoadFields(JsonTokenizer json)
		{
			json.ReadListValues(this.fields, (JsonTokenizer t) => new SalesforceObjectField(t));
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x000129F1 File Offset: 0x00010BF1
		protected override void LoadRelationships(JsonTokenizer json)
		{
			json.ReadListValues(this.relationships, (JsonTokenizer t) => new SalesforceObjectRelationship(t));
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00012A20 File Offset: 0x00010C20
		private RecordTypeValue CreateBaseObjectType()
		{
			string[] array = new string[this.fields.Count];
			Value[] array2 = new Value[this.fields.Count];
			for (int i = 0; i < this.fields.Count; i++)
			{
				TypeValue typeValue = SalesforceTypes.TranslateType(this.fields[i].ColumnType, false);
				if (this.fields[i].Nullable)
				{
					typeValue = typeValue.Nullable;
				}
				array[i] = this.fields[i].Name;
				array2[i] = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					typeValue,
					LogicalValue.False
				});
			}
			return RecordTypeValue.New(RecordValue.New(Keys.New(array), array2));
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00012ADC File Offset: 0x00010CDC
		public TableValue AddNavigationProperties(IEngineHost host, SoqlQuery query, SalesforceCatalog catalog, SalesforceDataLoader dataLoader, Func<SalesforceObjectHeader, TableValue> tableLoader)
		{
			List<SalesforceLinkInfo> list = new List<SalesforceLinkInfo>();
			foreach (SalesforceObjectRelationship salesforceObjectRelationship in this.relationships)
			{
				SalesforceObjectHeader salesforceObjectHeader = catalog[salesforceObjectRelationship.Child];
				if (salesforceObjectRelationship.RelationshipName != null && salesforceObjectHeader != null)
				{
					List<SalesforceLinkInfo> list2 = list;
					string relationshipName = salesforceObjectRelationship.RelationshipName;
					int[] array = this.keyColumns;
					list2.Add(new SalesforceLinkInfo(relationshipName, this.KeyColumnNames, array, salesforceObjectHeader, Keys.New(salesforceObjectRelationship.Field), false, tableLoader));
				}
			}
			for (int i = 0; i < this.fields.Count; i++)
			{
				SalesforceObjectField salesforceObjectField = this.fields[i];
				if (salesforceObjectField.RelationshipName != null && salesforceObjectField.ReferenceTo != null && salesforceObjectField.ReferenceTo.Length != 0)
				{
					string text = salesforceObjectField.ReferenceTo[0];
					SalesforceObjectHeader salesforceObjectHeader2 = catalog[text];
					if (salesforceObjectHeader2 != null)
					{
						Keys keyColumnNames = salesforceObjectHeader2.LoadDetail(host, dataLoader).KeyColumnNames;
						List<SalesforceLinkInfo> list3 = list;
						string relationshipName2 = salesforceObjectField.RelationshipName;
						int[] array = new int[] { i };
						list3.Add(new SalesforceLinkInfo(relationshipName2, Keys.New(salesforceObjectField.Name), array, salesforceObjectHeader2, keyColumnNames, true, tableLoader));
					}
				}
			}
			list.Sort(new Comparison<SalesforceLinkInfo>(SalesforceLinkInfo.Comparison));
			HashSet<string> hashSet = new HashSet<string>();
			foreach (SalesforceLinkInfo salesforceLinkInfo in list)
			{
				salesforceLinkInfo.Name = NavigationPropertiesHelper.EnsureUniqueName(salesforceLinkInfo.Name, query.Columns, hashSet);
			}
			KeysBuilder keysBuilder = default(KeysBuilder);
			keysBuilder.Union(query.Columns);
			TableValue tableValue = new QueryTableValue(new OptimizableQuery(query));
			foreach (SalesforceLinkInfo salesforceLinkInfo2 in list)
			{
				keysBuilder.Add(salesforceLinkInfo2.Name);
				tableValue = tableValue.NestedJoin(salesforceLinkInfo2.SourceColumns, salesforceLinkInfo2.LinkTable, salesforceLinkInfo2.TargetKeys, TableTypeAlgebra.JoinKind.LeftOuter, salesforceLinkInfo2.Name, keysBuilder.ToKeys(), null);
				if (salesforceLinkInfo2.SingleTarget)
				{
					tableValue = tableValue.ExpandListColumn(tableValue.Columns.Length - 1, true);
				}
			}
			return tableValue;
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00012D40 File Offset: 0x00010F40
		private int[] GetTableKeys()
		{
			for (int i = 0; i < this.fields.Count; i++)
			{
				if (this.fields[i].ColumnType == "id")
				{
					return new int[] { i };
				}
			}
			return new int[0];
		}

		// Token: 0x0400054F RID: 1359
		private readonly List<SalesforceObjectField> fields = new List<SalesforceObjectField>();

		// Token: 0x04000550 RID: 1360
		private readonly List<SalesforceObjectRelationship> relationships = new List<SalesforceObjectRelationship>();

		// Token: 0x04000551 RID: 1361
		private readonly int[] keyColumns;

		// Token: 0x04000552 RID: 1362
		private RecordTypeValue type;
	}
}
