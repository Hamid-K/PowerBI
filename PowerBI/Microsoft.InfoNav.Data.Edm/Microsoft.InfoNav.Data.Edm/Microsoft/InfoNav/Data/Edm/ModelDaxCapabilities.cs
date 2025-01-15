using System;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x0200002B RID: 43
	public sealed class ModelDaxCapabilities
	{
		// Token: 0x06000194 RID: 404 RVA: 0x00008EEA File Offset: 0x000070EA
		public ModelDaxCapabilities(bool supportsVariables = false, bool supportsInOperator = false, bool supportsTableConstructor = false, bool supportsVirtualColumns = false)
		{
			this._supportsVariables = supportsVariables;
			this._supportsInOperator = supportsInOperator;
			this._supportsTableConstructor = supportsTableConstructor;
			this._supportsVirtualColumns = supportsVirtualColumns;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00008F0F File Offset: 0x0000710F
		public bool SupportsVariables
		{
			get
			{
				return this._supportsVariables;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00008F17 File Offset: 0x00007117
		public bool SupportsInOperator
		{
			get
			{
				return this._supportsInOperator;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00008F1F File Offset: 0x0000711F
		public bool SupportsTableConstructor
		{
			get
			{
				return this._supportsTableConstructor;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00008F27 File Offset: 0x00007127
		public bool SupportsVirtualColumns
		{
			get
			{
				return this._supportsVirtualColumns;
			}
		}

		// Token: 0x04000192 RID: 402
		private readonly bool _supportsVariables;

		// Token: 0x04000193 RID: 403
		private readonly bool _supportsInOperator;

		// Token: 0x04000194 RID: 404
		private readonly bool _supportsTableConstructor;

		// Token: 0x04000195 RID: 405
		private readonly bool _supportsVirtualColumns;
	}
}
