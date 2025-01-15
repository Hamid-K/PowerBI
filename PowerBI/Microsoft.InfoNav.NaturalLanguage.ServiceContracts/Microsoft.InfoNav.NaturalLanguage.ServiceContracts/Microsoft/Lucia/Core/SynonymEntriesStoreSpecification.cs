using System;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000E6 RID: 230
	[ImmutableObject(true)]
	public sealed class SynonymEntriesStoreSpecification
	{
		// Token: 0x06000476 RID: 1142 RVA: 0x00008705 File Offset: 0x00006905
		public SynonymEntriesStoreSpecification(string entriesFilePath, string entriesIndexFilePath)
		{
			Contract.CheckNonEmpty(entriesFilePath, "entriesFilePath");
			Contract.CheckNonEmpty(entriesIndexFilePath, "entriesIndexFilePath");
			this._entriesFilePath = entriesFilePath;
			this._entriesIndexFilePath = entriesIndexFilePath;
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00008731 File Offset: 0x00006931
		public string IndexFilePath
		{
			get
			{
				return this._entriesIndexFilePath;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00008739 File Offset: 0x00006939
		public string EntriesStoreFilePath
		{
			get
			{
				return this._entriesFilePath;
			}
		}

		// Token: 0x04000501 RID: 1281
		private readonly string _entriesFilePath;

		// Token: 0x04000502 RID: 1282
		private readonly string _entriesIndexFilePath;
	}
}
