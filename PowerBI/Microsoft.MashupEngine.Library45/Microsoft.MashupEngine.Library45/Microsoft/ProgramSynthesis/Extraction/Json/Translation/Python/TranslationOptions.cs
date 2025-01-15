using System;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Translation.Python
{
	// Token: 0x02000B98 RID: 2968
	public class TranslationOptions
	{
		// Token: 0x06004B6B RID: 19307 RVA: 0x000EDAB6 File Offset: 0x000EBCB6
		public TranslationOptions(string functionName = "read_json", string input = "file", string encoding = "utf-8")
		{
			this.FunctionName = functionName;
			this.Input = input;
			this.Encoding = encoding;
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06004B6C RID: 19308 RVA: 0x000EDAD3 File Offset: 0x000EBCD3
		public string FunctionName { get; }

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06004B6D RID: 19309 RVA: 0x000EDADB File Offset: 0x000EBCDB
		public string Input { get; }

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06004B6E RID: 19310 RVA: 0x000EDAE3 File Offset: 0x000EBCE3
		public string Encoding { get; }
	}
}
