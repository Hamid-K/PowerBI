using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200007A RID: 122
	public class EdmEnumMember : EdmNamedElement, IEdmEnumMember, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000261 RID: 609 RVA: 0x00005F5B File Offset: 0x0000415B
		public EdmEnumMember(IEdmEnumType declaringType, string name, IEdmEnumMemberValue value)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmEnumType>(declaringType, "declaringType");
			EdmUtil.CheckArgumentNull<IEdmEnumMemberValue>(value, "value");
			this.declaringType = declaringType;
			this.value = value;
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00005F8A File Offset: 0x0000418A
		public IEdmEnumType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00005F92 File Offset: 0x00004192
		public IEdmEnumMemberValue Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040000D2 RID: 210
		private readonly IEdmEnumType declaringType;

		// Token: 0x040000D3 RID: 211
		private IEdmEnumMemberValue value;
	}
}
