using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Microsoft.ProgramSynthesis.Detection.FileType.Detectors
{
	// Token: 0x02000AC8 RID: 2760
	internal class JsonLinesDetector : TextualFormatDetector
	{
		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06004542 RID: 17730 RVA: 0x0000FA11 File Offset: 0x0000DC11
		internal override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06004543 RID: 17731 RVA: 0x000D8D81 File Offset: 0x000D6F81
		internal override IEnumerable<FileType> SupportedFileTypes
		{
			get
			{
				return Seq.Of<FileType>(new FileType[] { FileType.JsonLines });
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06004544 RID: 17732 RVA: 0x000D8D92 File Offset: 0x000D6F92
		internal override IEnumerable<string> SupportedExtensions
		{
			get
			{
				return Seq.Of<string>(new string[] { "json", "jsonl" });
			}
		}

		// Token: 0x06004545 RID: 17733 RVA: 0x000D8DB0 File Offset: 0x000D6FB0
		private static string RemoveNoise(string jsonFragment)
		{
			int num = jsonFragment.Length - 1;
			while (num >= 0 && jsonFragment[num] != '}' && jsonFragment[num] != ']')
			{
				num--;
			}
			if (num < 0)
			{
				return string.Empty;
			}
			int num2 = 0;
			while (num2 < jsonFragment.Length && jsonFragment[num2] != '{' && jsonFragment[num2] != '[')
			{
				num2++;
			}
			if (num2 == jsonFragment.Length || num <= num2)
			{
				return string.Empty;
			}
			return jsonFragment.Substring(num2, num - num2 + 1);
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x000D8E38 File Offset: 0x000D7038
		internal override FileType MatchFormat(FileTypeIdentifier caller, string header, string footer)
		{
			List<string> list = header.Split(new char[] { '\n' }).Take(16).ToList<string>();
			if (list.Count < 2)
			{
				return FileType.Unknown;
			}
			bool flag = true;
			foreach (string text in list)
			{
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text))
				{
					string text2 = JsonLinesDetector.RemoveNoise(text);
					if (string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(text))
					{
						return FileType.Unknown;
					}
					flag = false;
					JsonLinesDetector.JsonSerializerErrorHandler.Instance.ClearErrors();
					JsonConvert.DeserializeObject(text2, JsonLinesDetector.JsonSerializerErrorHandler.Instance);
					if (JsonLinesDetector.JsonSerializerErrorHandler.Instance.ErrorEncountered)
					{
						return FileType.Unknown;
					}
				}
			}
			if (!flag)
			{
				return FileType.JsonLines;
			}
			return FileType.Unknown;
		}

		// Token: 0x04001FA8 RID: 8104
		private const int MaxObjectsToInspect = 16;

		// Token: 0x02000AC9 RID: 2761
		private class JsonSerializerErrorHandler : JsonSerializerSettings
		{
			// Token: 0x17000C67 RID: 3175
			// (get) Token: 0x06004548 RID: 17736 RVA: 0x000D8F24 File Offset: 0x000D7124
			// (set) Token: 0x06004549 RID: 17737 RVA: 0x000D8F2C File Offset: 0x000D712C
			public bool ErrorEncountered { get; private set; }

			// Token: 0x0600454A RID: 17738 RVA: 0x000D8F35 File Offset: 0x000D7135
			public void ClearErrors()
			{
				this.ErrorEncountered = false;
			}

			// Token: 0x0600454B RID: 17739 RVA: 0x000D8F3E File Offset: 0x000D713E
			private JsonSerializerErrorHandler()
			{
				base.DateParseHandling = DateParseHandling.None;
				base.Error = new EventHandler<ErrorEventArgs>(this.HandleErrors);
			}

			// Token: 0x0600454C RID: 17740 RVA: 0x000D8F5F File Offset: 0x000D715F
			private void HandleErrors(object sender, ErrorEventArgs args)
			{
				this.ErrorEncountered = true;
				args.ErrorContext.Handled = true;
			}

			// Token: 0x17000C68 RID: 3176
			// (get) Token: 0x0600454D RID: 17741 RVA: 0x000D8F74 File Offset: 0x000D7174
			public static JsonLinesDetector.JsonSerializerErrorHandler Instance { get; } = new JsonLinesDetector.JsonSerializerErrorHandler();
		}
	}
}
