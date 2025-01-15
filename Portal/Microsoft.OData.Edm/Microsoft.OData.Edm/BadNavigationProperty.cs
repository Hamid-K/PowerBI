using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000064 RID: 100
	internal class BadNavigationProperty : BadElement, IEdmNavigationProperty, IEdmProperty, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x0600020D RID: 525 RVA: 0x000052C7 File Offset: 0x000034C7
		public BadNavigationProperty(IEdmStructuredType declaringType, string name, IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.name = name ?? string.Empty;
			this.declaringType = declaringType;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600020E RID: 526 RVA: 0x000052F2 File Offset: 0x000034F2
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600020F RID: 527 RVA: 0x000052FA File Offset: 0x000034FA
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00005302 File Offset: 0x00003502
		public IEdmTypeReference Type
		{
			get
			{
				return this.type.GetValue(this, BadNavigationProperty.ComputeTypeFunc, null);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000211 RID: 529 RVA: 0x000026A6 File Offset: 0x000008A6
		public EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.None;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmNavigationProperty Partner
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000026A6 File Offset: 0x000008A6
		public EdmOnDeleteAction OnDelete
		{
			get
			{
				return EdmOnDeleteAction.None;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmReferentialConstraint ReferentialConstraint
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000026A6 File Offset: 0x000008A6
		public bool ContainsTarget
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00005318 File Offset: 0x00003518
		public override string ToString()
		{
			EdmError edmError = base.Errors.FirstOrDefault<EdmError>();
			return edmError.ErrorCode + ":" + this.ToTraceString();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00004C9B File Offset: 0x00002E9B
		private IEdmTypeReference ComputeType()
		{
			return new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x040000BE RID: 190
		private readonly string name;

		// Token: 0x040000BF RID: 191
		private readonly IEdmStructuredType declaringType;

		// Token: 0x040000C0 RID: 192
		private readonly Cache<BadNavigationProperty, IEdmTypeReference> type = new Cache<BadNavigationProperty, IEdmTypeReference>();

		// Token: 0x040000C1 RID: 193
		private static readonly Func<BadNavigationProperty, IEdmTypeReference> ComputeTypeFunc = (BadNavigationProperty me) => me.ComputeType();
	}
}
