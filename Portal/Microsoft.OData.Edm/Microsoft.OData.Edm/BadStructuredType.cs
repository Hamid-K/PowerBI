using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000067 RID: 103
	internal abstract class BadStructuredType : BadType, IEdmStructuredType, IEdmType, IEdmElement, IEdmCheckable
	{
		// Token: 0x0600021F RID: 543 RVA: 0x00005472 File Offset: 0x00003672
		protected BadStructuredType(IEnumerable<EdmError> errors)
			: base(errors)
		{
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmStructuredType BaseType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000026A9 File Offset: 0x000008A9
		public IEnumerable<IEdmProperty> DeclaredProperties
		{
			get
			{
				return Enumerable.Empty<IEdmProperty>();
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000026A6 File Offset: 0x000008A6
		public bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000026A6 File Offset: 0x000008A6
		public bool IsOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmProperty FindProperty(string name)
		{
			return null;
		}
	}
}
