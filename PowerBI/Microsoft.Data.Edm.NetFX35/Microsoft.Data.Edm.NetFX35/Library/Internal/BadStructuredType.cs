using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Library.Internal
{
	// Token: 0x020000EB RID: 235
	internal abstract class BadStructuredType : BadType, IEdmStructuredType, IEdmType, IEdmElement, IEdmCheckable
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
		protected BadStructuredType(IEnumerable<EdmError> errors)
			: base(errors)
		{
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000C3E9 File Offset: 0x0000A5E9
		public IEdmStructuredType BaseType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000C3EC File Offset: 0x0000A5EC
		public IEnumerable<IEdmProperty> DeclaredProperties
		{
			get
			{
				return Enumerable.Empty<IEdmProperty>();
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0000C3F3 File Offset: 0x0000A5F3
		public bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000C3F6 File Offset: 0x0000A5F6
		public bool IsOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000C3F9 File Offset: 0x0000A5F9
		public IEdmProperty FindProperty(string name)
		{
			return null;
		}
	}
}
