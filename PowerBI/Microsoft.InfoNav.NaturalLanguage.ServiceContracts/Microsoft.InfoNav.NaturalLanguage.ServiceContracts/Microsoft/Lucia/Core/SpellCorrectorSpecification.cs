using System;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000CC RID: 204
	[ImmutableObject(true)]
	public sealed class SpellCorrectorSpecification : PoolObjectSpecification
	{
		// Token: 0x06000416 RID: 1046 RVA: 0x00007D67 File Offset: 0x00005F67
		public SpellCorrectorSpecification(int minPoolSize, int maxPoolSize, string lexiconFile, string dllFile)
			: base(minPoolSize, maxPoolSize)
		{
			Contract.CheckNonEmpty(lexiconFile, "lexiconFile");
			Contract.CheckNonEmpty(dllFile, "dllFile");
			this._lexiconFile = lexiconFile;
			this._dllPath = dllFile;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00007D97 File Offset: 0x00005F97
		public string LexiconFile
		{
			get
			{
				return this._lexiconFile;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00007D9F File Offset: 0x00005F9F
		public string DllPath
		{
			get
			{
				return this._dllPath;
			}
		}

		// Token: 0x040004D0 RID: 1232
		private readonly string _lexiconFile;

		// Token: 0x040004D1 RID: 1233
		private readonly string _dllPath;
	}
}
