using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000022 RID: 34
	public class PBIWinScriptVisualTransform : BaseTelemetryEvent
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00003778 File Offset: 0x00001978
		public PBIWinScriptVisualTransform(long inputRowsCount, long streamSizeBytes, long parsingTimeMilliseconds, long overallInputCreationTimeMilliseconds, long overallTransformMilliseconds, string parentId, bool isError)
			: base("PBI.Win.ScriptVisualTransform", TelemetryUse.Verbose)
		{
			this.inputRowsCount = inputRowsCount;
			this.streamSizeBytes = streamSizeBytes;
			this.parsingTimeMilliseconds = parsingTimeMilliseconds;
			this.overallInputCreationTimeMilliseconds = overallInputCreationTimeMilliseconds;
			this.overallTransformMilliseconds = overallTransformMilliseconds;
			this.parentId = parentId;
			this.isError = isError;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000037C6 File Offset: 0x000019C6
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x000037CE File Offset: 0x000019CE
		public long inputRowsCount { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000037D7 File Offset: 0x000019D7
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000037DF File Offset: 0x000019DF
		public long streamSizeBytes { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000037E8 File Offset: 0x000019E8
		// (set) Token: 0x060000DD RID: 221 RVA: 0x000037F0 File Offset: 0x000019F0
		public long parsingTimeMilliseconds { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000037F9 File Offset: 0x000019F9
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00003801 File Offset: 0x00001A01
		public long overallInputCreationTimeMilliseconds { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000380A File Offset: 0x00001A0A
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00003812 File Offset: 0x00001A12
		public long overallTransformMilliseconds { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x0000381B File Offset: 0x00001A1B
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00003823 File Offset: 0x00001A23
		public string parentId { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000382C File Offset: 0x00001A2C
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00003834 File Offset: 0x00001A34
		public bool isError { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003840 File Offset: 0x00001A40
		public override Dictionary<string, string> Properties
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				long inputRowsCount = this.inputRowsCount;
				string text = ((inputRowsCount == null) ? string.Empty : inputRowsCount.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("inputRowsCount", text);
				long streamSizeBytes = this.streamSizeBytes;
				string text2 = ((streamSizeBytes == null) ? string.Empty : streamSizeBytes.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("streamSizeBytes", text2);
				long parsingTimeMilliseconds = this.parsingTimeMilliseconds;
				string text3 = ((parsingTimeMilliseconds == null) ? string.Empty : parsingTimeMilliseconds.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("parsingTimeMilliseconds", text3);
				long overallInputCreationTimeMilliseconds = this.overallInputCreationTimeMilliseconds;
				string text4 = ((overallInputCreationTimeMilliseconds == null) ? string.Empty : overallInputCreationTimeMilliseconds.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("overallInputCreationTimeMilliseconds", text4);
				long overallTransformMilliseconds = this.overallTransformMilliseconds;
				string text5 = ((overallTransformMilliseconds == null) ? string.Empty : overallTransformMilliseconds.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("overallTransformMilliseconds", text5);
				string parentId = this.parentId;
				string text6 = ((parentId == null) ? string.Empty : parentId.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("parentId", text6);
				bool isError = this.isError;
				string text7 = ((isError == null) ? string.Empty : isError.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("isError", text7);
				return dictionary;
			}
		}
	}
}
