using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Library.Values;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000095 RID: 149
	internal class CsdlSemanticsEnumMember : CsdlSemanticsElement, IEdmEnumMember, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x0600027F RID: 639 RVA: 0x0000646B File Offset: 0x0000466B
		public CsdlSemanticsEnumMember(CsdlSemanticsEnumTypeDefinition declaringType, CsdlEnumMember member)
			: base(member)
		{
			this.member = member;
			this.declaringType = declaringType;
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000648D File Offset: 0x0000468D
		public string Name
		{
			get
			{
				return this.member.Name;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000649A File Offset: 0x0000469A
		public IEdmEnumType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000282 RID: 642 RVA: 0x000064A2 File Offset: 0x000046A2
		public IEdmPrimitiveValue Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsEnumMember.ComputeValueFunc, null);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000283 RID: 643 RVA: 0x000064B6 File Offset: 0x000046B6
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.declaringType.Model;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000284 RID: 644 RVA: 0x000064C3 File Offset: 0x000046C3
		public override CsdlElement Element
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000064CB File Offset: 0x000046CB
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.declaringType.Context);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000064E4 File Offset: 0x000046E4
		private IEdmPrimitiveValue ComputeValue()
		{
			if (this.member.Value == null)
			{
				return new BadPrimitiveValue(new EdmPrimitiveTypeReference(this.DeclaringType.UnderlyingType, false), new EdmError[]
				{
					new EdmError(this.member.Location ?? base.Location, EdmErrorCode.EnumMemberValueOutOfRange, Strings.CsdlSemantics_EnumMemberValueOutOfRange)
				});
			}
			return new EdmIntegerConstant(new EdmPrimitiveTypeReference(this.DeclaringType.UnderlyingType, false), this.member.Value.Value);
		}

		// Token: 0x040000FE RID: 254
		private readonly CsdlEnumMember member;

		// Token: 0x040000FF RID: 255
		private readonly CsdlSemanticsEnumTypeDefinition declaringType;

		// Token: 0x04000100 RID: 256
		private readonly Cache<CsdlSemanticsEnumMember, IEdmPrimitiveValue> valueCache = new Cache<CsdlSemanticsEnumMember, IEdmPrimitiveValue>();

		// Token: 0x04000101 RID: 257
		private static readonly Func<CsdlSemanticsEnumMember, IEdmPrimitiveValue> ComputeValueFunc = (CsdlSemanticsEnumMember me) => me.ComputeValue();
	}
}
