using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.DeltaLake
{
	// Token: 0x02001EE7 RID: 7911
	internal static class DeltaLakeOptions
	{
		// Token: 0x06010AAB RID: 68267 RVA: 0x003961A8 File Offset: 0x003943A8
		public static bool UseStatistics(RecordValue options)
		{
			Value value;
			return !options.TryGetValue("UseStatistics", out value) || !value.IsLogical || value.AsBoolean;
		}

		// Token: 0x06010AAC RID: 68268 RVA: 0x003961D4 File Offset: 0x003943D4
		public static bool UseVOrder(RecordValue options)
		{
			Value value;
			return !options.TryGetValue("PreserveOrder", out value) || !value.IsLogical || !value.AsBoolean;
		}

		// Token: 0x040063B4 RID: 25524
		private const string CompressionKey = "Compression";

		// Token: 0x040063B5 RID: 25525
		private const string MaxDepthKey = "MaxDepth";

		// Token: 0x040063B6 RID: 25526
		private const string LegacyColumnNameEncodingKey = "LegacyColumnNameEncoding";

		// Token: 0x040063B7 RID: 25527
		private const string PreserveOrderKey = "PreserveOrder";

		// Token: 0x040063B8 RID: 25528
		private const string TypeMappingKey = "TypeMapping";

		// Token: 0x040063B9 RID: 25529
		private const string UseStatisticsKey = "UseStatistics";

		// Token: 0x040063BA RID: 25530
		private const int MaxDepthDefault = 32;

		// Token: 0x040063BB RID: 25531
		private const string TypeMappingSql = "Sql";

		// Token: 0x040063BC RID: 25532
		public static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("MaxDepth", NullableTypeValue.Int32, NumberValue.New(32), OptionItemOption.RequiresActions, null, null),
			new OptionItem("Compression", Library.CompressionType.Type.Nullable, Library.CompressionType.None, OptionItemOption.RequiresActions, null, null),
			new OptionItem("PreserveOrder", NullableTypeValue.Logical, Value.Null, OptionItemOption.None, null, null),
			new OptionItem("UseStatistics", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.ExcludeFromOptionType, null, null)
		});

		// Token: 0x040063BD RID: 25533
		public static readonly RecordValue DefaultParquetOptions = RecordValue.New(Keys.New(new string[] { "LegacyColumnNameEncoding", "TypeMapping", "PreserveOrder", "UseStatistics", "Compression" }), new Value[]
		{
			LogicalValue.False,
			TextValue.New("Sql"),
			LogicalValue.False,
			LogicalValue.True,
			Library.CompressionType.LZ4.NewMeta(RecordValue.New(Keys.New("Hadoop"), new Value[] { LogicalValue.True }))
		});
	}
}
