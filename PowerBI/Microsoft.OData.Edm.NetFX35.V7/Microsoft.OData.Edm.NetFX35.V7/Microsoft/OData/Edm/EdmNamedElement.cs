using System;
using System.Diagnostics;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000065 RID: 101
	[DebuggerDisplay("Name:{Name}")]
	public abstract class EdmNamedElement : EdmElement, IEdmNamedElement, IEdmElement
	{
		// Token: 0x0600039E RID: 926 RVA: 0x0000B434 File Offset: 0x00009634
		protected EdmNamedElement(string name)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.name = name;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000B44F File Offset: 0x0000964F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040000D5 RID: 213
		private readonly string name;
	}
}
