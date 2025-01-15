using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000795 RID: 1941
	public class ManagerElement : TypeElement
	{
		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06003E99 RID: 16025 RVA: 0x000D1D66 File Offset: 0x000CFF66
		// (set) Token: 0x06003E9A RID: 16026 RVA: 0x000D1D6E File Offset: 0x000CFF6E
		public ManagerCodePoint Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06003E9B RID: 16027 RVA: 0x000D1D77 File Offset: 0x000CFF77
		// (set) Token: 0x06003E9C RID: 16028 RVA: 0x000D1D7F File Offset: 0x000CFF7F
		public List<TypeElement> Filters
		{
			get
			{
				return this._filters;
			}
			set
			{
				this._filters = value;
			}
		}

		// Token: 0x04002527 RID: 9511
		private List<TypeElement> _filters = new List<TypeElement>();

		// Token: 0x04002528 RID: 9512
		private ManagerCodePoint _id;
	}
}
