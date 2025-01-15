using System;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000057 RID: 87
	public class NoOpModelCache<T> : IModelCache<T>, IDisposable
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x00005813 File Offset: 0x00003A13
		public bool Add(string connectionString, string modelMetadataVersion, TranslationsBehavior translationsBehavior, T value, ConnectionType connectionType)
		{
			return false;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00005816 File Offset: 0x00003A16
		public void Clear(string connectionString)
		{
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00005818 File Offset: 0x00003A18
		public void ClearAll()
		{
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000581A File Offset: 0x00003A1A
		public void Dispose()
		{
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000581C File Offset: 0x00003A1C
		public bool TryGetValue(string connectionString, string modelMetadataVersion, TranslationsBehavior translationsBehavior, out T value)
		{
			value = default(T);
			return false;
		}
	}
}
