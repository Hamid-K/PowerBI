using System;

namespace Microsoft.OData.Client
{
	// Token: 0x020000D9 RID: 217
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class MimeTypePropertyAttribute : Attribute
	{
		// Token: 0x06000709 RID: 1801 RVA: 0x0001D24B File Offset: 0x0001B44B
		public MimeTypePropertyAttribute(string dataPropertyName, string mimeTypePropertyName)
		{
			this.dataPropertyName = dataPropertyName;
			this.mimeTypePropertyName = mimeTypePropertyName;
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001D261 File Offset: 0x0001B461
		public string DataPropertyName
		{
			get
			{
				return this.dataPropertyName;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0001D269 File Offset: 0x0001B469
		public string MimeTypePropertyName
		{
			get
			{
				return this.mimeTypePropertyName;
			}
		}

		// Token: 0x04000331 RID: 817
		private readonly string dataPropertyName;

		// Token: 0x04000332 RID: 818
		private readonly string mimeTypePropertyName;
	}
}
