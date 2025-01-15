using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000127 RID: 295
	internal abstract class BadStructuredType : BadType, IEdmStructuredType, IEdmType, IEdmElement, IEdmCheckable
	{
		// Token: 0x060005D3 RID: 1491 RVA: 0x0000E180 File Offset: 0x0000C380
		protected BadStructuredType(IEnumerable<EdmError> errors)
			: base(errors)
		{
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0000E189 File Offset: 0x0000C389
		public IEdmStructuredType BaseType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0000E18C File Offset: 0x0000C38C
		public IEnumerable<IEdmProperty> DeclaredProperties
		{
			get
			{
				return Enumerable.Empty<IEdmProperty>();
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0000E193 File Offset: 0x0000C393
		public bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0000E196 File Offset: 0x0000C396
		public bool IsOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0000E199 File Offset: 0x0000C399
		public IEdmProperty FindProperty(string name)
		{
			return null;
		}
	}
}
