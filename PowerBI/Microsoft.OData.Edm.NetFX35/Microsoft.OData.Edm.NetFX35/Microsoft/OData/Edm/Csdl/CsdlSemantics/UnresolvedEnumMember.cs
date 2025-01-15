using System;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Library.Values;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001BD RID: 445
	internal class UnresolvedEnumMember : BadElement, IEdmEnumMember, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x0600095C RID: 2396 RVA: 0x00019440 File Offset: 0x00017640
		public UnresolvedEnumMember(string name, IEdmEnumType declaringType, EdmLocation location)
			: base(new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedEnumMember, Strings.Bad_UnresolvedEnumMember(name))
			})
		{
			this.name = name ?? string.Empty;
			this.declaringType = declaringType;
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x00019491 File Offset: 0x00017691
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00019499 File Offset: 0x00017699
		public IEdmPrimitiveValue Value
		{
			get
			{
				return this.value.GetValue(this, UnresolvedEnumMember.ComputeValueFunc, null);
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x000194AD File Offset: 0x000176AD
		public IEdmEnumType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x000194B5 File Offset: 0x000176B5
		private IEdmPrimitiveValue ComputeValue()
		{
			return new EdmIntegerConstant(0L);
		}

		// Token: 0x04000499 RID: 1177
		private readonly string name;

		// Token: 0x0400049A RID: 1178
		private readonly IEdmEnumType declaringType;

		// Token: 0x0400049B RID: 1179
		private readonly Cache<UnresolvedEnumMember, IEdmPrimitiveValue> value = new Cache<UnresolvedEnumMember, IEdmPrimitiveValue>();

		// Token: 0x0400049C RID: 1180
		private static readonly Func<UnresolvedEnumMember, IEdmPrimitiveValue> ComputeValueFunc = (UnresolvedEnumMember me) => me.ComputeValue();
	}
}
