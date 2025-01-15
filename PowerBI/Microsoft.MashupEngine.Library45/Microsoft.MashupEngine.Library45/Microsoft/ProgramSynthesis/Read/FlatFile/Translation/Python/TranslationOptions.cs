using System;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Translation.Python
{
	// Token: 0x020012D7 RID: 4823
	public class TranslationOptions
	{
		// Token: 0x170018F9 RID: 6393
		// (get) Token: 0x06009176 RID: 37238 RVA: 0x001EAB28 File Offset: 0x001E8D28
		// (set) Token: 0x06009177 RID: 37239 RVA: 0x001EAB30 File Offset: 0x001E8D30
		public string NamesVar { get; set; } = "names";

		// Token: 0x170018FA RID: 6394
		// (get) Token: 0x06009178 RID: 37240 RVA: 0x001EAB39 File Offset: 0x001E8D39
		// (set) Token: 0x06009179 RID: 37241 RVA: 0x001EAB41 File Offset: 0x001E8D41
		public string ResultVar { get; set; } = "df";

		// Token: 0x170018FB RID: 6395
		// (get) Token: 0x0600917A RID: 37242 RVA: 0x001EAB4A File Offset: 0x001E8D4A
		// (set) Token: 0x0600917B RID: 37243 RVA: 0x001EAB52 File Offset: 0x001E8D52
		public string ColumnsVar { get; set; } = "columns";

		// Token: 0x170018FC RID: 6396
		// (get) Token: 0x0600917C RID: 37244 RVA: 0x001EAB5B File Offset: 0x001E8D5B
		// (set) Token: 0x0600917D RID: 37245 RVA: 0x001EAB63 File Offset: 0x001E8D63
		public string ColspecsVar { get; set; } = "colspecs";

		// Token: 0x170018FD RID: 6397
		// (get) Token: 0x0600917E RID: 37246 RVA: 0x001EAB6C File Offset: 0x001E8D6C
		// (set) Token: 0x0600917F RID: 37247 RVA: 0x001EAB74 File Offset: 0x001E8D74
		public string LinesVar { get; set; } = "lines";

		// Token: 0x170018FE RID: 6398
		// (get) Token: 0x06009180 RID: 37248 RVA: 0x001EAB7D File Offset: 0x001E8D7D
		// (set) Token: 0x06009181 RID: 37249 RVA: 0x001EAB85 File Offset: 0x001E8D85
		public bool CreateNewSparkSession { get; set; } = true;

		// Token: 0x170018FF RID: 6399
		// (get) Token: 0x06009182 RID: 37250 RVA: 0x001EAB8E File Offset: 0x001E8D8E
		// (set) Token: 0x06009183 RID: 37251 RVA: 0x001EAB96 File Offset: 0x001E8D96
		public string SparkSession { get; set; } = "spark";

		// Token: 0x17001900 RID: 6400
		// (get) Token: 0x06009184 RID: 37252 RVA: 0x001EAB9F File Offset: 0x001E8D9F
		// (set) Token: 0x06009185 RID: 37253 RVA: 0x001EABA7 File Offset: 0x001E8DA7
		public string SchemaVar { get; set; } = "schema";

		// Token: 0x17001901 RID: 6401
		// (get) Token: 0x06009186 RID: 37254 RVA: 0x001EABB0 File Offset: 0x001E8DB0
		// (set) Token: 0x06009187 RID: 37255 RVA: 0x001EABB8 File Offset: 0x001E8DB8
		public string TrimFnc { get; set; } = "trim";

		// Token: 0x17001902 RID: 6402
		// (get) Token: 0x06009188 RID: 37256 RVA: 0x001EABC1 File Offset: 0x001E8DC1
		// (set) Token: 0x06009189 RID: 37257 RVA: 0x001EABC9 File Offset: 0x001E8DC9
		public string RStripCRFnc { get; set; } = "rstrip_cr";

		// Token: 0x17001903 RID: 6403
		// (get) Token: 0x0600918A RID: 37258 RVA: 0x001EABD2 File Offset: 0x001E8DD2
		// (set) Token: 0x0600918B RID: 37259 RVA: 0x001EABDA File Offset: 0x001E8DDA
		public string IsNotCommentFnc { get; set; } = "is_not_comment";

		// Token: 0x17001904 RID: 6404
		// (get) Token: 0x0600918C RID: 37260 RVA: 0x001EABE3 File Offset: 0x001E8DE3
		// (set) Token: 0x0600918D RID: 37261 RVA: 0x001EABEB File Offset: 0x001E8DEB
		public bool SuppressTypeDetection { get; set; } = true;
	}
}
