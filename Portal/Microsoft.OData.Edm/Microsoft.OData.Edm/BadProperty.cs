using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000063 RID: 99
	internal class BadProperty : BadElement, IEdmStructuralProperty, IEdmProperty, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000204 RID: 516 RVA: 0x0000522B File Offset: 0x0000342B
		public BadProperty(IEdmStructuredType declaringType, string name, IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.name = name ?? string.Empty;
			this.declaringType = declaringType;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00005256 File Offset: 0x00003456
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000525E File Offset: 0x0000345E
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00005266 File Offset: 0x00003466
		public IEdmTypeReference Type
		{
			get
			{
				return this.type.GetValue(this, BadProperty.ComputeTypeFunc, null);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000026B0 File Offset: 0x000008B0
		public string DefaultValueString
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000026A6 File Offset: 0x000008A6
		public EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.None;
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000527C File Offset: 0x0000347C
		public override string ToString()
		{
			EdmError edmError = base.Errors.FirstOrDefault<EdmError>();
			return edmError.ErrorCode + ":" + this.ToTraceString();
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00004C9B File Offset: 0x00002E9B
		private IEdmTypeReference ComputeType()
		{
			return new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x040000BA RID: 186
		private readonly string name;

		// Token: 0x040000BB RID: 187
		private readonly IEdmStructuredType declaringType;

		// Token: 0x040000BC RID: 188
		private readonly Cache<BadProperty, IEdmTypeReference> type = new Cache<BadProperty, IEdmTypeReference>();

		// Token: 0x040000BD RID: 189
		private static readonly Func<BadProperty, IEdmTypeReference> ComputeTypeFunc = (BadProperty me) => me.ComputeType();
	}
}
