using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B3E RID: 6974
	[JsonConverter(typeof(StringEnumConverter))]
	public enum OperationId
	{
		// Token: 0x040056E5 RID: 22245
		AddColumnsFromJson,
		// Token: 0x040056E6 RID: 22246
		ChangeType,
		// Token: 0x040056E7 RID: 22247
		ConvertToLowerCase,
		// Token: 0x040056E8 RID: 22248
		ConvertToUpperCase,
		// Token: 0x040056E9 RID: 22249
		Drop,
		// Token: 0x040056EA RID: 22250
		DropNa,
		// Token: 0x040056EB RID: 22251
		DropDuplicates,
		// Token: 0x040056EC RID: 22252
		DropOutliers,
		// Token: 0x040056ED RID: 22253
		FillNa,
		// Token: 0x040056EE RID: 22254
		Filter,
		// Token: 0x040056EF RID: 22255
		FlashFill,
		// Token: 0x040056F0 RID: 22256
		GroupBy,
		// Token: 0x040056F1 RID: 22257
		LabelEncode,
		// Token: 0x040056F2 RID: 22258
		MultiLabelBinarizer,
		// Token: 0x040056F3 RID: 22259
		OneHotEncode,
		// Token: 0x040056F4 RID: 22260
		Replace,
		// Token: 0x040056F5 RID: 22261
		Scale,
		// Token: 0x040056F6 RID: 22262
		Split,
		// Token: 0x040056F7 RID: 22263
		Strip,
		// Token: 0x040056F8 RID: 22264
		CustomSplit
	}
}
