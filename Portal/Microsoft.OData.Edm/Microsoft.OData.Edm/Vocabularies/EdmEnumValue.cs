using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000129 RID: 297
	public class EdmEnumValue : EdmValue, IEdmEnumValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x060007AE RID: 1966 RVA: 0x0001222E File Offset: 0x0001042E
		public EdmEnumValue(IEdmEnumTypeReference type, IEdmEnumMember member)
			: this(type, member.Value)
		{
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001223D File Offset: 0x0001043D
		public EdmEnumValue(IEdmEnumTypeReference type, IEdmEnumMemberValue value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0001224D File Offset: 0x0001044D
		public IEdmEnumMemberValue Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00003A59 File Offset: 0x00001C59
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Enum;
			}
		}

		// Token: 0x0400032F RID: 815
		private readonly IEdmEnumMemberValue value;
	}
}
