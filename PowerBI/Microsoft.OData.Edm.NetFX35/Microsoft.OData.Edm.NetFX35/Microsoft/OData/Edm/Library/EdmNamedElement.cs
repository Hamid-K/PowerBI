using System;
using System.Diagnostics;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000100 RID: 256
	[DebuggerDisplay("Name:{Name}")]
	public abstract class EdmNamedElement : EdmElement, IEdmNamedElement, IEdmElement
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x0000D2A6 File Offset: 0x0000B4A6
		protected EdmNamedElement(string name)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.name = name;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0000D2C1 File Offset: 0x0000B4C1
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040001E3 RID: 483
		private readonly string name;
	}
}
