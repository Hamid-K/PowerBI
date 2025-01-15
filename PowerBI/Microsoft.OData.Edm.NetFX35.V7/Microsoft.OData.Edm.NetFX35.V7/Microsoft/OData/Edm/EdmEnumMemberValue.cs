using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200005C RID: 92
	public class EdmEnumMemberValue : IEdmEnumMemberValue, IEdmElement
	{
		// Token: 0x0600035F RID: 863 RVA: 0x0000ADA7 File Offset: 0x00008FA7
		public EdmEnumMemberValue(long value)
		{
			this.value = value;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000ADB6 File Offset: 0x00008FB6
		public long Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040000BC RID: 188
		private readonly long value;
	}
}
