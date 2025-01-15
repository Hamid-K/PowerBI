using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200003D RID: 61
	internal abstract class BadStructuredType : BadType, IEdmStructuredType, IEdmType, IEdmElement, IEdmCheckable
	{
		// Token: 0x0600029C RID: 668 RVA: 0x0000969A File Offset: 0x0000789A
		protected BadStructuredType(IEnumerable<EdmError> errors)
			: base(errors)
		{
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmStructuredType BaseType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600029E RID: 670 RVA: 0x000096A3 File Offset: 0x000078A3
		public IEnumerable<IEdmProperty> DeclaredProperties
		{
			get
			{
				return Enumerable.Empty<IEdmProperty>();
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00008EC3 File Offset: 0x000070C3
		public bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00008EC3 File Offset: 0x000070C3
		public bool IsOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmProperty FindProperty(string name)
		{
			return null;
		}
	}
}
