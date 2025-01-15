using System;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x02000495 RID: 1173
	[AttributeUsage(AttributeTargets.Field)]
	public class TGUIAttribute : Attribute
	{
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x0008B7D3 File Offset: 0x000899D3
		// (set) Token: 0x06001872 RID: 6258 RVA: 0x0008B7DB File Offset: 0x000899DB
		public string Label { get; set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x0008B7E4 File Offset: 0x000899E4
		// (set) Token: 0x06001874 RID: 6260 RVA: 0x0008B7EC File Offset: 0x000899EC
		public string Description { get; set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x0008B7F5 File Offset: 0x000899F5
		// (set) Token: 0x06001876 RID: 6262 RVA: 0x0008B7FD File Offset: 0x000899FD
		public bool IsSaveFileName { get; set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x0008B806 File Offset: 0x00089A06
		// (set) Token: 0x06001878 RID: 6264 RVA: 0x0008B80E File Offset: 0x00089A0E
		public bool IsFolder { get; set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x0008B817 File Offset: 0x00089A17
		// (set) Token: 0x0600187A RID: 6266 RVA: 0x0008B81F File Offset: 0x00089A1F
		public bool ShowPreviewIcon { get; set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x0008B828 File Offset: 0x00089A28
		// (set) Token: 0x0600187C RID: 6268 RVA: 0x0008B830 File Offset: 0x00089A30
		public string OutputFilenameTemplate { get; set; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x0008B839 File Offset: 0x00089A39
		// (set) Token: 0x0600187E RID: 6270 RVA: 0x0008B841 File Offset: 0x00089A41
		public string SuggestedSweeps { get; set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x0008B84A File Offset: 0x00089A4A
		// (set) Token: 0x06001880 RID: 6272 RVA: 0x0008B852 File Offset: 0x00089A52
		public bool RegistryBacked { get; set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x0008B85B File Offset: 0x00089A5B
		// (set) Token: 0x06001882 RID: 6274 RVA: 0x0008B863 File Offset: 0x00089A63
		public bool NotGUI { get; set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x0008B86C File Offset: 0x00089A6C
		// (set) Token: 0x06001884 RID: 6276 RVA: 0x0008B874 File Offset: 0x00089A74
		public bool NoSweep { get; set; }
	}
}
