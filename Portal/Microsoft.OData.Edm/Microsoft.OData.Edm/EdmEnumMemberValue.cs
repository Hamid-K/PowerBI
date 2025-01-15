using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000040 RID: 64
	public class EdmEnumMemberValue : IEdmEnumMemberValue, IEdmElement
	{
		// Token: 0x06000147 RID: 327 RVA: 0x00004631 File Offset: 0x00002831
		public EdmEnumMemberValue(long value)
		{
			this.value = value;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00004640 File Offset: 0x00002840
		public long Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000078 RID: 120
		private readonly long value;
	}
}
