using System;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000CE RID: 206
	[ImmutableObject(true)]
	public sealed class StemmerSpecification
	{
		// Token: 0x0600041C RID: 1052 RVA: 0x000081BB File Offset: 0x000063BB
		public StemmerSpecification(string grammarFilePath)
		{
			Contract.CheckNonEmpty(grammarFilePath, "grammarFilePath");
			this._grammarFilePath = grammarFilePath;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x000081D5 File Offset: 0x000063D5
		public string GrammarFilePath
		{
			get
			{
				return this._grammarFilePath;
			}
		}

		// Token: 0x040004D3 RID: 1235
		private readonly string _grammarFilePath;
	}
}
