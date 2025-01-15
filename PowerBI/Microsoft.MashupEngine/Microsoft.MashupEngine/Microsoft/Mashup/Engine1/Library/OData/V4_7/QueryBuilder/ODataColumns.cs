using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder
{
	// Token: 0x020007C9 RID: 1993
	internal sealed class ODataColumns
	{
		// Token: 0x060039FD RID: 14845 RVA: 0x000BB672 File Offset: 0x000B9872
		private ODataColumns(RecordTypeValue recordType, IList<ODataExpandedColumn> expandedColumns, string moreColumnsColumnName)
		{
			this.recordTypeValue = recordType;
			this.expandedColumns = expandedColumns;
			this.moreColumnsColumnName = moreColumnsColumnName;
		}

		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x060039FE RID: 14846 RVA: 0x000BB68F File Offset: 0x000B988F
		public Keys Names
		{
			get
			{
				return this.recordTypeValue.Fields.Keys;
			}
		}

		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x060039FF RID: 14847 RVA: 0x000BB6A1 File Offset: 0x000B98A1
		public RecordTypeValue RecordTypeValue
		{
			get
			{
				return this.recordTypeValue;
			}
		}

		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x06003A00 RID: 14848 RVA: 0x000BB6A9 File Offset: 0x000B98A9
		public IEnumerable<ODataExpandedColumn> ExpandedColumns
		{
			get
			{
				return this.expandedColumns;
			}
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x000BB6B4 File Offset: 0x000B98B4
		public static ODataColumns New(RecordTypeValue recordTypeValue)
		{
			string text = null;
			Value value;
			if (recordTypeValue.TryGetMetaField("MoreColumns", out value))
			{
				text = value.AsString;
			}
			return new ODataColumns(recordTypeValue, EmptyArray<ODataExpandedColumn>.Instance, text);
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x000BB6E5 File Offset: 0x000B98E5
		public TypeValue GetColumnType(int index)
		{
			return this.recordTypeValue.Fields[index]["Type"].AsType;
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x000BB708 File Offset: 0x000B9908
		public ODataColumns Add(ODataColumns added)
		{
			RecordBuilder recordBuilder = new RecordBuilder(this.Names.Length + added.Names.Length);
			foreach (string text in this.Names)
			{
				recordBuilder.Add(text, this.RecordTypeValue.Fields[text], TypeValue.Any);
			}
			foreach (string text2 in added.Names)
			{
				recordBuilder.Add(text2, added.RecordTypeValue.Fields[text2], TypeValue.Any);
			}
			RecordTypeValue recordTypeValue = RecordTypeValue.New(recordBuilder.ToRecord());
			RecordValue recordValue = RecordValue.New(Keys.New("aggregate"), new Value[] { LogicalValue.New(true) });
			recordTypeValue = BinaryOperator.AddMeta.Invoke(recordTypeValue, recordValue).AsType.AsRecordType;
			return ODataColumns.New(recordTypeValue);
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x000BB838 File Offset: 0x000B9A38
		public ODataColumns SelectColumns(Keys keys)
		{
			int[] array = new int[keys.Length];
			for (int i = 0; i < keys.Length; i++)
			{
				array[i] = this.Names.IndexOfKey(keys[i]);
			}
			return this.SelectColumns(new ColumnSelection(keys, array));
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x000BB884 File Offset: 0x000B9A84
		public ODataColumns SelectColumns(ColumnSelection columnSelection)
		{
			IValueReference[] array = new IValueReference[columnSelection.Keys.Length];
			for (int i = 0; i < columnSelection.Keys.Length; i++)
			{
				int column = columnSelection.GetColumn(i);
				array[i] = this.RecordTypeValue.Fields[column];
			}
			RecordTypeValue recordTypeValue = RecordTypeValue.New(RecordValue.New(columnSelection.Keys, array));
			if (!this.recordTypeValue.MetaValue.IsEmpty)
			{
				recordTypeValue = BinaryOperator.AddMeta.Invoke(recordTypeValue, this.recordTypeValue.MetaValue).AsType.AsRecordType;
			}
			return new ODataColumns(recordTypeValue, this.expandedColumns, this.moreColumnsColumnName);
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x000BB92B File Offset: 0x000B9B2B
		public ODataColumns ExpandColumn(IList<ODataExpandedColumn> expandedColumns)
		{
			return new ODataColumns(this.recordTypeValue, expandedColumns, this.moreColumnsColumnName);
		}

		// Token: 0x04001E1A RID: 7706
		private readonly RecordTypeValue recordTypeValue;

		// Token: 0x04001E1B RID: 7707
		private readonly IList<ODataExpandedColumn> expandedColumns;

		// Token: 0x04001E1C RID: 7708
		private readonly string moreColumnsColumnName;
	}
}
