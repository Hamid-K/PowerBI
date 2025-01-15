using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000E2 RID: 226
	[ImmutableObject(true)]
	public sealed class SynonymProviderConfiguration
	{
		// Token: 0x0600046C RID: 1132 RVA: 0x00008671 File Offset: 0x00006871
		public SynonymProviderConfiguration(IEnumerable<int> supportedNgramSize)
		{
			this._supportedNgramSize = supportedNgramSize.AsReadOnlyCollection<int>();
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00008685 File Offset: 0x00006885
		public ReadOnlyCollection<int> SupportedNgramSize
		{
			get
			{
				return this._supportedNgramSize;
			}
		}

		// Token: 0x040004FC RID: 1276
		private readonly ReadOnlyCollection<int> _supportedNgramSize;
	}
}
