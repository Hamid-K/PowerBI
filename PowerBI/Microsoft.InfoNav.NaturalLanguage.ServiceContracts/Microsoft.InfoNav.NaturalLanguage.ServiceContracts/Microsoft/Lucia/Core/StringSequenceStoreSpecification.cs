using System;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000E4 RID: 228
	[ImmutableObject(true)]
	public sealed class StringSequenceStoreSpecification
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x0000868D File Offset: 0x0000688D
		public StringSequenceStoreSpecification(string stringSequenceStoreFilePath, string stringSequenceStorePointersFilePath)
		{
			Contract.CheckNonEmpty(stringSequenceStoreFilePath, "stringSequenceStoreFilePath");
			Contract.CheckNonEmpty(stringSequenceStorePointersFilePath, "stringSequenceStorePointersFilePath");
			this._stringSequenceStoreFilePath = stringSequenceStoreFilePath;
			this._stringSequenceStorePointersFilePath = stringSequenceStorePointersFilePath;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x000086B9 File Offset: 0x000068B9
		public string StringSequenceStoreFilePath
		{
			get
			{
				return this._stringSequenceStoreFilePath;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x000086C1 File Offset: 0x000068C1
		public string StringSequenceStorePointersFilePath
		{
			get
			{
				return this._stringSequenceStorePointersFilePath;
			}
		}

		// Token: 0x040004FD RID: 1277
		private readonly string _stringSequenceStoreFilePath;

		// Token: 0x040004FE RID: 1278
		private readonly string _stringSequenceStorePointersFilePath;
	}
}
