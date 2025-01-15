using System;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200004F RID: 79
	internal interface IModelCache<T> : IDisposable
	{
		// Token: 0x060001BC RID: 444
		bool Add(string connectionString, string modelMetadataVersion, TranslationsBehavior translationsBehavior, T value, ConnectionType connectionType);

		// Token: 0x060001BD RID: 445
		void Clear(string connectionString);

		// Token: 0x060001BE RID: 446
		void ClearAll();

		// Token: 0x060001BF RID: 447
		bool TryGetValue(string connectionString, string modelMetadataVersion, TranslationsBehavior translationsBehavior, out T value);
	}
}
