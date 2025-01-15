using System;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020001B6 RID: 438
	public class EdmEnumMember : EdmNamedElement, IEdmEnumMember, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x0600093C RID: 2364 RVA: 0x00019226 File Offset: 0x00017426
		public EdmEnumMember(IEdmEnumType declaringType, string name, IEdmPrimitiveValue value)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<IEdmEnumType>(declaringType, "declaringType");
			EdmUtil.CheckArgumentNull<IEdmPrimitiveValue>(value, "value");
			this.declaringType = declaringType;
			this.value = value;
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00019255 File Offset: 0x00017455
		public IEdmEnumType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0001925D File Offset: 0x0001745D
		public IEdmPrimitiveValue Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400048E RID: 1166
		private readonly IEdmEnumType declaringType;

		// Token: 0x0400048F RID: 1167
		private IEdmPrimitiveValue value;
	}
}
