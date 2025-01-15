using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200017D RID: 381
	internal class CsdlSemanticsEnumMember : CsdlSemanticsElement, IEdmEnumMember, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000A59 RID: 2649 RVA: 0x0001C91A File Offset: 0x0001AB1A
		public CsdlSemanticsEnumMember(CsdlSemanticsEnumTypeDefinition declaringType, CsdlEnumMember member)
			: base(member)
		{
			this.member = member;
			this.declaringType = declaringType;
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0001C93C File Offset: 0x0001AB3C
		public string Name
		{
			get
			{
				return this.member.Name;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0001C949 File Offset: 0x0001AB49
		public IEdmEnumType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0001C951 File Offset: 0x0001AB51
		public IEdmEnumMemberValue Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsEnumMember.ComputeValueFunc, null);
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0001C965 File Offset: 0x0001AB65
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.declaringType.Model;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0001C972 File Offset: 0x0001AB72
		public override CsdlElement Element
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0001C97A File Offset: 0x0001AB7A
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.declaringType.Context);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0001C994 File Offset: 0x0001AB94
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

		// Token: 0x04000640 RID: 1600
		private readonly CsdlEnumMember member;

		// Token: 0x04000641 RID: 1601
		private readonly CsdlSemanticsEnumTypeDefinition declaringType;

		// Token: 0x04000642 RID: 1602
		private readonly Cache<CsdlSemanticsEnumMember, IEdmEnumMemberValue> valueCache = new Cache<CsdlSemanticsEnumMember, IEdmEnumMemberValue>();

		// Token: 0x04000643 RID: 1603
		private static readonly Func<CsdlSemanticsEnumMember, IEdmEnumMemberValue> ComputeValueFunc = (CsdlSemanticsEnumMember me) => me.ComputeValue();
	}
}
