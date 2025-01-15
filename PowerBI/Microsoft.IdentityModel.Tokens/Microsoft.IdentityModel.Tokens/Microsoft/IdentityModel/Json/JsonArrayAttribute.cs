using System;

namespace Microsoft.IdentityModel.Json
{
	// Token: 0x02000017 RID: 23
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	internal sealed class JsonArrayAttribute : JsonContainerAttribute
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002382 File Offset: 0x00000582
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000238A File Offset: 0x0000058A
		public bool AllowNullItems
		{
			get
			{
				return this._allowNullItems;
			}
			set
			{
				this._allowNullItems = value;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002393 File Offset: 0x00000593
		public JsonArrayAttribute()
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000239B File Offset: 0x0000059B
		public JsonArrayAttribute(bool allowNullItems)
		{
			this._allowNullItems = allowNullItems;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023AA File Offset: 0x000005AA
		public JsonArrayAttribute(string id)
			: base(id)
		{
		}

		// Token: 0x04000028 RID: 40
		private bool _allowNullItems;
	}
}
