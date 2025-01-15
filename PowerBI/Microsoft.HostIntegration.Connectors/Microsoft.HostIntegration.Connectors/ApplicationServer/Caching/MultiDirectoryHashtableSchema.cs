using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000247 RID: 583
	internal class MultiDirectoryHashtableSchema : IStoreSchema
	{
		// Token: 0x0600139F RID: 5023 RVA: 0x0003D94D File Offset: 0x0003BB4D
		internal MultiDirectoryHashtableSchema()
		{
			this._rootSize = 4;
			this._subDirectorySize = 4;
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x0003D963 File Offset: 0x0003BB63
		public int RootBitMaskSize
		{
			get
			{
				return this._rootSize;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x0003D96B File Offset: 0x0003BB6B
		public int SubDirectoryBitMaskSize
		{
			get
			{
				return this._subDirectorySize;
			}
		}

		// Token: 0x04000BC8 RID: 3016
		private int _rootSize;

		// Token: 0x04000BC9 RID: 3017
		private int _subDirectorySize;
	}
}
