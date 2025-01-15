using System;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000E5 RID: 229
	[ImmutableObject(true)]
	public sealed class StringStoreSpecification
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x000086C9 File Offset: 0x000068C9
		public StringStoreSpecification(string stringStoreFilePath, string stringStorePointersFilePath)
		{
			Contract.CheckNonEmpty(stringStoreFilePath, "stringStoreFilePath");
			Contract.CheckNonEmpty(stringStorePointersFilePath, "stringStorePointersFilePath");
			this._stringStoreFilePath = stringStoreFilePath;
			this._stringStorePointersFilePath = stringStorePointersFilePath;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x000086F5 File Offset: 0x000068F5
		public string StringStoreFilePath
		{
			get
			{
				return this._stringStoreFilePath;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x000086FD File Offset: 0x000068FD
		public string StringStorePointersFilePath
		{
			get
			{
				return this._stringStorePointersFilePath;
			}
		}

		// Token: 0x040004FF RID: 1279
		private readonly string _stringStoreFilePath;

		// Token: 0x04000500 RID: 1280
		private readonly string _stringStorePointersFilePath;
	}
}
