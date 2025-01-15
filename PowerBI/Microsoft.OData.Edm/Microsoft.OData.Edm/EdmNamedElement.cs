using System;
using System.Diagnostics;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000C2 RID: 194
	[DebuggerDisplay("Name:{Name}")]
	public abstract class EdmNamedElement : EdmElement, IEdmNamedElement, IEdmElement
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x0000BDC1 File Offset: 0x00009FC1
		protected EdmNamedElement(string name)
		{
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.name = name;
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000BDDC File Offset: 0x00009FDC
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000171 RID: 369
		private readonly string name;
	}
}
