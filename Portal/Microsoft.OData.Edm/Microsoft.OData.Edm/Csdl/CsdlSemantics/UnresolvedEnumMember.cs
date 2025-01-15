using System;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A5 RID: 421
	internal class UnresolvedEnumMember : BadElement, IEdmEnumMember, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000BC6 RID: 3014 RVA: 0x00020DBC File Offset: 0x0001EFBC
		public UnresolvedEnumMember(string name, IEdmEnumType declaringType, EdmLocation location)
			: base(new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedEnumMember, Strings.Bad_UnresolvedEnumMember(name))
			})
		{
			this.name = name ?? string.Empty;
			this.declaringType = declaringType;
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x00020E0B File Offset: 0x0001F00B
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x00020E13 File Offset: 0x0001F013
		public IEdmEnumMemberValue Value
		{
			get
			{
				return this.value.GetValue(this, UnresolvedEnumMember.ComputeValueFunc, null);
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x00020E27 File Offset: 0x0001F027
		public IEdmEnumType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00020E2F File Offset: 0x0001F02F
		private static IEdmEnumMemberValue ComputeValue()
		{
			return new EdmEnumMemberValue(0L);
		}

		// Token: 0x040006E7 RID: 1767
		private readonly string name;

		// Token: 0x040006E8 RID: 1768
		private readonly IEdmEnumType declaringType;

		// Token: 0x040006E9 RID: 1769
		private readonly Cache<UnresolvedEnumMember, IEdmEnumMemberValue> value = new Cache<UnresolvedEnumMember, IEdmEnumMemberValue>();

		// Token: 0x040006EA RID: 1770
		private static readonly Func<UnresolvedEnumMember, IEdmEnumMemberValue> ComputeValueFunc = (UnresolvedEnumMember me) => UnresolvedEnumMember.ComputeValue();
	}
}
