using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000037 RID: 55
	internal class BadNavigationProperty : BadElement, IEdmNavigationProperty, IEdmProperty, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000278 RID: 632 RVA: 0x0000939D File Offset: 0x0000759D
		public BadNavigationProperty(IEdmStructuredType declaringType, string name, IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.name = name ?? string.Empty;
			this.declaringType = declaringType;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000093C8 File Offset: 0x000075C8
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600027A RID: 634 RVA: 0x000093D0 File Offset: 0x000075D0
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600027B RID: 635 RVA: 0x000093D8 File Offset: 0x000075D8
		public IEdmTypeReference Type
		{
			get
			{
				return this.type.GetValue(this, BadNavigationProperty.ComputeTypeFunc, null);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00008EC3 File Offset: 0x000070C3
		public EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.None;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmNavigationProperty Partner
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00008EC3 File Offset: 0x000070C3
		public EdmOnDeleteAction OnDelete
		{
			get
			{
				return EdmOnDeleteAction.None;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmReferentialConstraint ReferentialConstraint
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00008EC3 File Offset: 0x000070C3
		public bool ContainsTarget
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000093EC File Offset: 0x000075EC
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(base.Errors);
			return edmError.ErrorCode + ":" + this.ToTraceString();
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00008EE2 File Offset: 0x000070E2
		private IEdmTypeReference ComputeType()
		{
			return new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x04000058 RID: 88
		private readonly string name;

		// Token: 0x04000059 RID: 89
		private readonly IEdmStructuredType declaringType;

		// Token: 0x0400005A RID: 90
		private readonly Cache<BadNavigationProperty, IEdmTypeReference> type = new Cache<BadNavigationProperty, IEdmTypeReference>();

		// Token: 0x0400005B RID: 91
		private static readonly Func<BadNavigationProperty, IEdmTypeReference> ComputeTypeFunc = (BadNavigationProperty me) => me.ComputeType();
	}
}
