using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model
{
	// Token: 0x02000050 RID: 80
	public class Extension
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000319D File Offset: 0x0000139D
		// (set) Token: 0x06000201 RID: 513 RVA: 0x000031A5 File Offset: 0x000013A5
		public ExtensionType ExtensionType { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000202 RID: 514 RVA: 0x000031AE File Offset: 0x000013AE
		// (set) Token: 0x06000203 RID: 515 RVA: 0x000031B6 File Offset: 0x000013B6
		[Key]
		public string Name { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000031BF File Offset: 0x000013BF
		// (set) Token: 0x06000205 RID: 517 RVA: 0x000031C7 File Offset: 0x000013C7
		public string LocalizedName { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000031D0 File Offset: 0x000013D0
		// (set) Token: 0x06000207 RID: 519 RVA: 0x000031D8 File Offset: 0x000013D8
		public bool Visible { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000031E4 File Offset: 0x000013E4
		public IList<ExtensionParameter> Parameters
		{
			get
			{
				IList<ExtensionParameter> list;
				if ((list = this._parameters) == null)
				{
					list = (this._parameters = this.LoadExtensionParameters());
				}
				return list;
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000320A File Offset: 0x0000140A
		protected virtual IList<ExtensionParameter> LoadExtensionParameters()
		{
			return new List<ExtensionParameter>();
		}

		// Token: 0x0400019D RID: 413
		private IList<ExtensionParameter> _parameters;
	}
}
