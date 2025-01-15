using System;
using System.Collections.Generic;

namespace Microsoft.WindowsAzure.Diagnostics
{
	// Token: 0x0200046B RID: 1131
	[Obsolete("This API is deprecated.")]
	public class DirectoriesBufferConfiguration : DiagnosticDataBufferConfiguration
	{
		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06002758 RID: 10072 RVA: 0x00077A9F File Offset: 0x00075C9F
		public IList<DirectoryConfiguration> DataSources
		{
			get
			{
				return new List<DirectoryConfiguration>();
			}
		}
	}
}
