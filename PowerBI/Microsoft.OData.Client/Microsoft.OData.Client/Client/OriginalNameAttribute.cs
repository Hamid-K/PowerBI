using System;

namespace Microsoft.OData.Client
{
	// Token: 0x0200001F RID: 31
	[AttributeUsage(AttributeTargets.All)]
	public sealed class OriginalNameAttribute : Attribute
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00005124 File Offset: 0x00003324
		public OriginalNameAttribute(string originalName)
		{
			this.originalName = originalName;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005133 File Offset: 0x00003333
		public string OriginalName
		{
			get
			{
				return this.originalName;
			}
		}

		// Token: 0x0400004C RID: 76
		private readonly string originalName;
	}
}
