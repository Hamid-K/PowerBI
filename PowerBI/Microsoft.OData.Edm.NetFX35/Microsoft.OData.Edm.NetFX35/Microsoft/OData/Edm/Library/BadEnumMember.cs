using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200013A RID: 314
	internal class BadEnumMember : BadElement, IEdmEnumMember, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x0000E492 File Offset: 0x0000C692
		public BadEnumMember(IEdmEnumType declaringType, string name, IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.name = name ?? string.Empty;
			this.declaringType = declaringType;
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0000E4B2 File Offset: 0x0000C6B2
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0000E4BA File Offset: 0x0000C6BA
		public IEdmEnumType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0000E4C2 File Offset: 0x0000C6C2
		public IEdmPrimitiveValue Value
		{
			get
			{
				return new BadPrimitiveValue(new EdmPrimitiveTypeReference(this.declaringType.UnderlyingType, false), base.Errors);
			}
		}

		// Token: 0x0400023C RID: 572
		private readonly string name;

		// Token: 0x0400023D RID: 573
		private readonly IEdmEnumType declaringType;
	}
}
