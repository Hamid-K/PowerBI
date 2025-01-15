using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200005B RID: 91
	public class EdmEnumMember : EdmNamedElement, IEdmEnumMember, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x0600035C RID: 860 RVA: 0x0000AD68 File Offset: 0x00008F68
		public EdmEnumMember(IEdmEnumType declaringType, string name, IEdmEnumMemberValue value)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmEnumType>(declaringType, "declaringType");
			EdmUtil.CheckArgumentNull<IEdmEnumMemberValue>(value, "value");
			this.declaringType = declaringType;
			this.value = value;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000AD97 File Offset: 0x00008F97
		public IEdmEnumType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000AD9F File Offset: 0x00008F9F
		public IEdmEnumMemberValue Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040000BA RID: 186
		private readonly IEdmEnumType declaringType;

		// Token: 0x040000BB RID: 187
		private IEdmEnumMemberValue value;
	}
}
