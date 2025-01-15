using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200011C RID: 284
	public class EdmEnumValue : EdmValue, IEdmEnumValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x0600076E RID: 1902 RVA: 0x00013D4E File Offset: 0x00011F4E
		public EdmEnumValue(IEdmEnumTypeReference type, IEdmEnumMember member)
			: this(type, member.Value)
		{
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00013D5D File Offset: 0x00011F5D
		public EdmEnumValue(IEdmEnumTypeReference type, IEdmEnumMemberValue value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x00013D6D File Offset: 0x00011F6D
		public IEdmEnumMemberValue Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x000092ED File Offset: 0x000074ED
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Enum;
			}
		}

		// Token: 0x0400042A RID: 1066
		private readonly IEdmEnumMemberValue value;
	}
}
