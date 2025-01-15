using System;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.Tabular.Serialization;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001D5 RID: 469
	internal static class MetadataReaderExtensions
	{
		// Token: 0x06001C06 RID: 7174 RVA: 0x000C3BC8 File Offset: 0x000C1DC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsOnProperty(this IMetadataReader reader)
		{
			return !string.IsNullOrEmpty(reader.PropertyName);
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x000C3BD8 File Offset: 0x000C1DD8
		public static bool TryMoveToProperty(this IMetadataReader reader, string propertyName)
		{
			while (reader.IsOnProperty() && string.Compare(reader.PropertyName, propertyName, StringComparison.InvariantCulture) != 0)
			{
				reader.Skip();
			}
			return string.Compare(reader.PropertyName, propertyName, StringComparison.InvariantCulture) == 0;
		}
	}
}
