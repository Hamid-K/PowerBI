using System;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200002B RID: 43
	internal static class MashupDiscoveryExtensions
	{
		// Token: 0x0600026A RID: 618 RVA: 0x0000A478 File Offset: 0x00008678
		internal static MashupDiscovery AddQuery(this MashupDiscovery discovery, string query)
		{
			if (query == null)
			{
				throw new ArgumentException("query");
			}
			if (discovery.Kind != MashupDiscoveryKind.DataSource && discovery.Kind != MashupDiscoveryKind.UnknownNativeQuery)
			{
				throw new InvalidOperationException();
			}
			if (MashupDiscoveryExtensions.IsKusto(discovery.DataSourceReference))
			{
				return discovery;
			}
			return new MashupDiscovery(MashupDiscoveryKind.DataSource, discovery.FunctionName, discovery.DataSourceReference.AddQuery(query), discovery.Coordinate, discovery.Options, discovery.HasUnknownOptions, null);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A4E4 File Offset: 0x000086E4
		internal static MashupDiscovery SetUnknownQuery(this MashupDiscovery discovery)
		{
			if (discovery.Kind == MashupDiscoveryKind.DataSource && discovery.DataSourceReference.Query == null)
			{
				bool? supportsNativeQueryChallenge = discovery.DataSourceReference.ResourceKindInfo.SupportsNativeQueryChallenge;
				bool flag = false;
				if (!((supportsNativeQueryChallenge.GetValueOrDefault() == flag) & (supportsNativeQueryChallenge != null)) && !MashupDiscoveryExtensions.IsKusto(discovery.DataSourceReference))
				{
					return new MashupDiscovery(MashupDiscoveryKind.UnknownNativeQuery, discovery.FunctionName, discovery.DataSourceReference, discovery.Coordinate, discovery.Options, discovery.HasUnknownOptions, null);
				}
			}
			return discovery;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000A564 File Offset: 0x00008764
		internal static MashupDiscovery RemoveQuery(this MashupDiscovery discovery)
		{
			if (discovery.DataSourceReference == null || discovery.DataSourceReference.Query == null)
			{
				return discovery;
			}
			return new MashupDiscovery(MashupDiscoveryKind.DataSource, discovery.FunctionName, discovery.DataSourceReference.RemoveQuery(), discovery.Coordinate, discovery.Options, discovery.HasUnknownOptions, null);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000A5B4 File Offset: 0x000087B4
		internal static MashupDiscovery AddMetadata(this MashupDiscovery discovery, string metaJson)
		{
			if (discovery.DataSourceReference == null || discovery.Metadata == metaJson)
			{
				return discovery;
			}
			return new MashupDiscovery(MashupDiscoveryKind.DataSource, discovery.FunctionName, discovery.DataSourceReference.RemoveQuery(), discovery.Coordinate, discovery.Options, discovery.HasUnknownOptions, metaJson);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000A604 File Offset: 0x00008804
		private static bool IsKusto(DataSourceReference dataSourceReference)
		{
			string kind = dataSourceReference.DataSource.Kind;
			return kind == "Kusto" || kind == "AzureDataExplorer";
		}
	}
}
