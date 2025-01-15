using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200003A RID: 58
	internal class BadProperty : BadElement, IEdmStructuralProperty, IEdmProperty, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x0600028D RID: 653 RVA: 0x000094EE File Offset: 0x000076EE
		public BadProperty(IEdmStructuredType declaringType, string name, IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.name = name ?? string.Empty;
			this.declaringType = declaringType;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00009519 File Offset: 0x00007719
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00009521 File Offset: 0x00007721
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00009529 File Offset: 0x00007729
		public IEdmTypeReference Type
		{
			get
			{
				return this.type.GetValue(this, BadProperty.ComputeTypeFunc, null);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00008D69 File Offset: 0x00006F69
		public string DefaultValueString
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00008EC3 File Offset: 0x000070C3
		public EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.None;
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00009540 File Offset: 0x00007740
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(base.Errors);
			return edmError.ErrorCode + ":" + this.ToTraceString();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00008EE2 File Offset: 0x000070E2
		private IEdmTypeReference ComputeType()
		{
			return new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x04000060 RID: 96
		private readonly string name;

		// Token: 0x04000061 RID: 97
		private readonly IEdmStructuredType declaringType;

		// Token: 0x04000062 RID: 98
		private readonly Cache<BadProperty, IEdmTypeReference> type = new Cache<BadProperty, IEdmTypeReference>();

		// Token: 0x04000063 RID: 99
		private static readonly Func<BadProperty, IEdmTypeReference> ComputeTypeFunc = (BadProperty me) => me.ComputeType();
	}
}
