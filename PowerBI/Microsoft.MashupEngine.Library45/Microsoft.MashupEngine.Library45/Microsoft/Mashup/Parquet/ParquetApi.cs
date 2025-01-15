using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Parquet;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Parquet
{
	// Token: 0x02001F12 RID: 7954
	public static class ParquetApi
	{
		// Token: 0x06010BDF RID: 68575 RVA: 0x0039B240 File Offset: 0x00399440
		public static List<KeyValuePair<string, ListStatistics>> GetStatistics(IEngineHost engineHost, BinaryValue file, RecordValue options, out long rowCount)
		{
			OptionsRecord optionsRecord = ParquetApi.OptionRecord.CreateOptions("Parquet", options);
			return ParquetQuery.New(engineHost, file, optionsRecord).GetStatistics(out rowCount);
		}

		// Token: 0x0400645C RID: 25692
		internal static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("MaxDepth", NullableTypeValue.Int32, NumberValue.New(32), OptionItemOption.RequiresActions, null, null),
			new OptionItem("Compression", Library.CompressionType.Type.Nullable, Library.CompressionType.None, OptionItemOption.RequiresActions, new TryConvertOption(ParquetOptions.TryConvertCompressionOption), null),
			new OptionItem("PreserveOrder", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.RequiresActions, null, null),
			new OptionItem("LegacyColumnNameEncoding", NullableTypeValue.Logical, Value.Null, OptionItemOption.None, null, null),
			new OptionItem("TypeMapping", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, null),
			new OptionItem("UseStatistics", NullableTypeValue.Logical, Value.Null, OptionItemOption.ExcludeFromOptionType, null, null)
		});
	}
}
