using System;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Internal;
using Microsoft.Data.Edm.Library;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x0200008B RID: 139
	internal class CsdlSemanticsPropertyValueBinding : CsdlSemanticsElement, IEdmPropertyValueBinding, IEdmElement
	{
		// Token: 0x0600023F RID: 575 RVA: 0x00005EA9 File Offset: 0x000040A9
		public CsdlSemanticsPropertyValueBinding(CsdlSemanticsTypeAnnotation context, CsdlPropertyValue property)
			: base(property)
		{
			this.context = context;
			this.property = property;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00005ED6 File Offset: 0x000040D6
		public IEdmProperty BoundProperty
		{
			get
			{
				return this.boundPropertyCache.GetValue(this, CsdlSemanticsPropertyValueBinding.ComputeBoundPropertyFunc, null);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00005EEA File Offset: 0x000040EA
		public IEdmExpression Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsPropertyValueBinding.ComputeValueFunc, null);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00005EFE File Offset: 0x000040FE
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00005F0B File Offset: 0x0000410B
		public override CsdlElement Element
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00005F14 File Offset: 0x00004114
		private IEdmProperty ComputeBoundProperty()
		{
			IEdmProperty edmProperty = ((IEdmStructuredType)this.context.Term).FindProperty(this.property.Property);
			return edmProperty ?? new CsdlSemanticsPropertyValueBinding.UnresolvedBoundProperty((IEdmStructuredType)this.context.Term, this.property.Property);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00005F67 File Offset: 0x00004167
		private IEdmExpression ComputeValue()
		{
			return CsdlSemanticsModel.WrapExpression(this.property.Expression, this.context.TargetBindingContext, this.context.Schema);
		}

		// Token: 0x04000103 RID: 259
		private readonly CsdlPropertyValue property;

		// Token: 0x04000104 RID: 260
		private readonly CsdlSemanticsTypeAnnotation context;

		// Token: 0x04000105 RID: 261
		private readonly Cache<CsdlSemanticsPropertyValueBinding, IEdmExpression> valueCache = new Cache<CsdlSemanticsPropertyValueBinding, IEdmExpression>();

		// Token: 0x04000106 RID: 262
		private static readonly Func<CsdlSemanticsPropertyValueBinding, IEdmExpression> ComputeValueFunc = (CsdlSemanticsPropertyValueBinding me) => me.ComputeValue();

		// Token: 0x04000107 RID: 263
		private readonly Cache<CsdlSemanticsPropertyValueBinding, IEdmProperty> boundPropertyCache = new Cache<CsdlSemanticsPropertyValueBinding, IEdmProperty>();

		// Token: 0x04000108 RID: 264
		private static readonly Func<CsdlSemanticsPropertyValueBinding, IEdmProperty> ComputeBoundPropertyFunc = (CsdlSemanticsPropertyValueBinding me) => me.ComputeBoundProperty();

		// Token: 0x0200008E RID: 142
		private class UnresolvedBoundProperty : EdmElement, IEdmStructuralProperty, IEdmProperty, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement, IUnresolvedElement
		{
			// Token: 0x0600024E RID: 590 RVA: 0x00005FF1 File Offset: 0x000041F1
			public UnresolvedBoundProperty(IEdmStructuredType declaringType, string name)
			{
				this.declaringType = declaringType;
				this.name = name;
				this.type = new CsdlSemanticsPropertyValueBinding.UnresolvedBoundProperty.UnresolvedBoundPropertyType();
			}

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x0600024F RID: 591 RVA: 0x00006012 File Offset: 0x00004212
			public string DefaultValueString
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x06000250 RID: 592 RVA: 0x00006015 File Offset: 0x00004215
			public EdmConcurrencyMode ConcurrencyMode
			{
				get
				{
					return EdmConcurrencyMode.None;
				}
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x06000251 RID: 593 RVA: 0x00006018 File Offset: 0x00004218
			public EdmPropertyKind PropertyKind
			{
				get
				{
					return EdmPropertyKind.Structural;
				}
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x06000252 RID: 594 RVA: 0x0000601B File Offset: 0x0000421B
			public IEdmTypeReference Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x06000253 RID: 595 RVA: 0x00006023 File Offset: 0x00004223
			public IEdmStructuredType DeclaringType
			{
				get
				{
					return this.declaringType;
				}
			}

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x06000254 RID: 596 RVA: 0x0000602B File Offset: 0x0000422B
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x0400010B RID: 267
			private readonly IEdmStructuredType declaringType;

			// Token: 0x0400010C RID: 268
			private readonly string name;

			// Token: 0x0400010D RID: 269
			private readonly IEdmTypeReference type;

			// Token: 0x0200008F RID: 143
			private class UnresolvedBoundPropertyType : IEdmTypeReference, IEdmType, IEdmElement
			{
				// Token: 0x17000141 RID: 321
				// (get) Token: 0x06000255 RID: 597 RVA: 0x00006033 File Offset: 0x00004233
				public bool IsNullable
				{
					get
					{
						return true;
					}
				}

				// Token: 0x17000142 RID: 322
				// (get) Token: 0x06000256 RID: 598 RVA: 0x00006036 File Offset: 0x00004236
				public IEdmType Definition
				{
					get
					{
						return this;
					}
				}

				// Token: 0x17000143 RID: 323
				// (get) Token: 0x06000257 RID: 599 RVA: 0x00006039 File Offset: 0x00004239
				public EdmTypeKind TypeKind
				{
					get
					{
						return EdmTypeKind.None;
					}
				}
			}
		}
	}
}
