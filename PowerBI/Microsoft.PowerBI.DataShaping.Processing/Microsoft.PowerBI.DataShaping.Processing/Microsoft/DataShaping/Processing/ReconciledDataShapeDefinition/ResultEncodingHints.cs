using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x0200004B RID: 75
	internal class ResultEncodingHints
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000613A File Offset: 0x0000433A
		public IReadOnlyDictionary<string, ISet<string>> CalculationsWithSharedValues
		{
			get
			{
				return this._calculationsWithSharedValues;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00006142 File Offset: 0x00004342
		internal bool DisableDictionaryEncoding
		{
			get
			{
				return this._disableDictionaryEncoding;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000614A File Offset: 0x0000434A
		internal HashSet<string> DictionaryEncodingExcludeList
		{
			get
			{
				return this._dictEncodingExcludeList;
			}
		}

		// Token: 0x04000135 RID: 309
		protected bool _disableDictionaryEncoding;

		// Token: 0x04000136 RID: 310
		protected HashSet<string> _dictEncodingExcludeList;

		// Token: 0x04000137 RID: 311
		protected Dictionary<string, ISet<string>> _calculationsWithSharedValues;
	}
}
