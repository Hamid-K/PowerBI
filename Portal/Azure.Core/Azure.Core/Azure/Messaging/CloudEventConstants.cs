using System;
using System.Runtime.CompilerServices;

namespace Azure.Messaging
{
	// Token: 0x02000036 RID: 54
	[NullableContext(1)]
	[Nullable(0)]
	internal class CloudEventConstants
	{
		// Token: 0x04000071 RID: 113
		public const string SpecVersion = "specversion";

		// Token: 0x04000072 RID: 114
		public const string Id = "id";

		// Token: 0x04000073 RID: 115
		public const string Source = "source";

		// Token: 0x04000074 RID: 116
		public const string Type = "type";

		// Token: 0x04000075 RID: 117
		public const string DataContentType = "datacontenttype";

		// Token: 0x04000076 RID: 118
		public const string DataSchema = "dataschema";

		// Token: 0x04000077 RID: 119
		public const string Subject = "subject";

		// Token: 0x04000078 RID: 120
		public const string Time = "time";

		// Token: 0x04000079 RID: 121
		public const string Data = "data";

		// Token: 0x0400007A RID: 122
		public const string DataBase64 = "data_base64";

		// Token: 0x0400007B RID: 123
		public const string ErrorSkipValidationSuggestion = "The `skipValidation` parameter can be set to 'true' in the CloudEvent.Parse or CloudEvent.ParseEvents method to skip this validation.";
	}
}
