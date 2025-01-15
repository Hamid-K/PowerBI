using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x0200066B RID: 1643
	internal struct OdbcTableTypes
	{
		// Token: 0x060033CC RID: 13260 RVA: 0x000A5CD8 File Offset: 0x000A3ED8
		private OdbcTableTypes(string filterString, Dictionary<string, TableType> values)
		{
			this.filterString = filterString;
			this.values = values;
		}

		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x060033CD RID: 13261 RVA: 0x000A5CE8 File Offset: 0x000A3EE8
		public string FilterString
		{
			get
			{
				return this.filterString;
			}
		}

		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x060033CE RID: 13262 RVA: 0x000A5CF0 File Offset: 0x000A3EF0
		public IEnumerable<TableType> Values
		{
			get
			{
				return this.values.Values;
			}
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x000A5D00 File Offset: 0x000A3F00
		public static OdbcTableTypes LoadFrom(Value options)
		{
			if (options.IsNull)
			{
				return OdbcTableTypes.Default;
			}
			RecordValue asRecord = options.AsRecord;
			Dictionary<string, TableType> dictionary = new Dictionary<string, TableType>(asRecord.Count, StringComparer.OrdinalIgnoreCase);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < asRecord.Count; i++)
			{
				OptionsRecord optionsRecord = OdbcTableTypes.optionsDefinition.CreateOptions("ODBC", asRecord[i].AsRecord);
				if (i > 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(asRecord.Keys[i]);
				dictionary[asRecord.Keys[i]] = new TableType(asRecord.Keys[i], optionsRecord.GetAs<string>("Kind"), optionsRecord.GetAs<string>("LinkKind"), optionsRecord.GetAs<bool>("PrimaryKey"), optionsRecord.GetAs<bool>("ForeignKeys"));
			}
			return new OdbcTableTypes(stringBuilder.ToString(), dictionary);
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x000A5DEC File Offset: 0x000A3FEC
		public bool TryGetValue(string type, out TableType tableType)
		{
			return this.values.TryGetValue(type, out tableType);
		}

		// Token: 0x0400171B RID: 5915
		private static readonly TextValue tableKind = TextValue.New("Table");

		// Token: 0x0400171C RID: 5916
		private static readonly TextValue viewKind = TextValue.New("View");

		// Token: 0x0400171D RID: 5917
		private static readonly OptionRecordDefinition optionsDefinition = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("Kind", TypeValue.Text, OdbcTableTypes.tableKind, OptionItemOption.None, null, null),
			new OptionItem("LinkKind", TypeValue.Text, OdbcTableTypes.tableKind, OptionItemOption.None, null, null),
			new OptionItem("PrimaryKey", TypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, null),
			new OptionItem("ForeignKeys", TypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, null)
		});

		// Token: 0x0400171E RID: 5918
		public static readonly OdbcTableTypes Default = OdbcTableTypes.LoadFrom(RecordValue.New(Keys.New("TABLE", "VIEW"), new Value[]
		{
			RecordValue.Empty,
			RecordValue.New(Keys.New("Kind", "LinkKind"), new Value[]
			{
				OdbcTableTypes.viewKind,
				OdbcTableTypes.viewKind
			})
		}));

		// Token: 0x0400171F RID: 5919
		private readonly string filterString;

		// Token: 0x04001720 RID: 5920
		private readonly Dictionary<string, TableType> values;
	}
}
