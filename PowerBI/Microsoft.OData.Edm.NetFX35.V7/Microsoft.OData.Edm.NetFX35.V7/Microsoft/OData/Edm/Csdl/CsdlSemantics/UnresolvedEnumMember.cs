using System;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000195 RID: 405
	internal class UnresolvedEnumMember : BadElement, IEdmEnumMember, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000AF5 RID: 2805 RVA: 0x0001E69C File Offset: 0x0001C89C
		public UnresolvedEnumMember(string name, IEdmEnumType declaringType, EdmLocation location)
			: base(new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedEnumMember, Strings.Bad_UnresolvedEnumMember(name))
			})
		{
			this.name = name ?? string.Empty;
			this.declaringType = declaringType;
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x0001E6EB File Offset: 0x0001C8EB
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0001E6F3 File Offset: 0x0001C8F3
		public IEdmEnumMemberValue Value
		{
			get
			{
				return this.value.GetValue(this, UnresolvedEnumMember.ComputeValueFunc, null);
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x0001E707 File Offset: 0x0001C907
		public IEdmEnumType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0001E70F File Offset: 0x0001C90F
		private static IEdmEnumMemberValue ComputeValue()
		{
			return new EdmEnumMemberValue(0L);
		}

		// Token: 0x0400065F RID: 1631
		private readonly string name;

		// Token: 0x04000660 RID: 1632
		private readonly IEdmEnumType declaringType;

		// Token: 0x04000661 RID: 1633
		private readonly Cache<UnresolvedEnumMember, IEdmEnumMemberValue> value = new Cache<UnresolvedEnumMember, IEdmEnumMemberValue>();

		// Token: 0x04000662 RID: 1634
		private static readonly Func<UnresolvedEnumMember, IEdmEnumMemberValue> ComputeValueFunc = (UnresolvedEnumMember me) => UnresolvedEnumMember.ComputeValue();
	}
}
