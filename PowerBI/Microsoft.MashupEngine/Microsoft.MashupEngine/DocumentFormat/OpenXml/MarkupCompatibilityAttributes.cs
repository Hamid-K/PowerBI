using System;

namespace DocumentFormat.OpenXml
{
	// Token: 0x020020E9 RID: 8425
	public class MarkupCompatibilityAttributes
	{
		// Token: 0x170031D0 RID: 12752
		// (get) Token: 0x0600CF39 RID: 53049 RVA: 0x00294153 File Offset: 0x00292353
		// (set) Token: 0x0600CF3A RID: 53050 RVA: 0x0029415B File Offset: 0x0029235B
		public StringValue Ignorable { get; set; }

		// Token: 0x170031D1 RID: 12753
		// (get) Token: 0x0600CF3B RID: 53051 RVA: 0x00294164 File Offset: 0x00292364
		// (set) Token: 0x0600CF3C RID: 53052 RVA: 0x0029416C File Offset: 0x0029236C
		public StringValue ProcessContent { get; set; }

		// Token: 0x170031D2 RID: 12754
		// (get) Token: 0x0600CF3D RID: 53053 RVA: 0x00294175 File Offset: 0x00292375
		// (set) Token: 0x0600CF3E RID: 53054 RVA: 0x0029417D File Offset: 0x0029237D
		public StringValue PreserveElements { get; set; }

		// Token: 0x170031D3 RID: 12755
		// (get) Token: 0x0600CF3F RID: 53055 RVA: 0x00294186 File Offset: 0x00292386
		// (set) Token: 0x0600CF40 RID: 53056 RVA: 0x0029418E File Offset: 0x0029238E
		public StringValue PreserveAttributes { get; set; }

		// Token: 0x170031D4 RID: 12756
		// (get) Token: 0x0600CF41 RID: 53057 RVA: 0x00294197 File Offset: 0x00292397
		// (set) Token: 0x0600CF42 RID: 53058 RVA: 0x0029419F File Offset: 0x0029239F
		public StringValue MustUnderstand { get; set; }

		// Token: 0x0400687C RID: 26748
		internal static string MCPrefix = NamespaceIdMap.GetNamespacePrefix(NamespaceIdMap.GetNamespaceId(AlternateContent.MarkupCompatibilityNamespace));
	}
}
