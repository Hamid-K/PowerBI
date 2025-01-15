using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200016E RID: 366
	internal class CsdlSemanticsEnumMember : CsdlSemanticsElement, IEdmEnumMember, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x0600099E RID: 2462 RVA: 0x0001A812 File Offset: 0x00018A12
		public CsdlSemanticsEnumMember(CsdlSemanticsEnumTypeDefinition declaringType, CsdlEnumMember member)
			: base(member)
		{
			this.member = member;
			this.declaringType = declaringType;
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0001A834 File Offset: 0x00018A34
		public string Name
		{
			get
			{
				return this.member.Name;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0001A841 File Offset: 0x00018A41
		public IEdmEnumType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0001A849 File Offset: 0x00018A49
		public IEdmEnumMemberValue Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsEnumMember.ComputeValueFunc, null);
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x0001A85D File Offset: 0x00018A5D
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.declaringType.Model;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0001A86A File Offset: 0x00018A6A
		public override CsdlElement Element
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0001A872 File Offset: 0x00018A72
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.declaringType.Context);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0001A88C File Offset: 0x00018A8C
		private IEdmEnumMemberValue ComputeValue()
		{
			if (this.member.Value == null)
			{
				return new BadEdmEnumMemberValue(new EdmError[]
				{
					new EdmError(this.member.Location ?? base.Location, EdmErrorCode.EnumMemberMustHaveValue, Strings.CsdlSemantics_EnumMemberMustHaveValue)
				});
			}
			return new EdmEnumMemberValue(this.member.Value.Value);
		}

		// Token: 0x040005C5 RID: 1477
		private readonly CsdlEnumMember member;

		// Token: 0x040005C6 RID: 1478
		private readonly CsdlSemanticsEnumTypeDefinition declaringType;

		// Token: 0x040005C7 RID: 1479
		private readonly Cache<CsdlSemanticsEnumMember, IEdmEnumMemberValue> valueCache = new Cache<CsdlSemanticsEnumMember, IEdmEnumMemberValue>();

		// Token: 0x040005C8 RID: 1480
		private static readonly Func<CsdlSemanticsEnumMember, IEdmEnumMemberValue> ComputeValueFunc = (CsdlSemanticsEnumMember me) => me.ComputeValue();
	}
}
