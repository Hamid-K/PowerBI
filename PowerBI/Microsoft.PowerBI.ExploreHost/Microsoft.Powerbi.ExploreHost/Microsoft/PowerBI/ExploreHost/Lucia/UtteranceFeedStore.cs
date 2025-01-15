using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Lucia.Hosting.Analytics;
using Microsoft.PowerBI.Lucia.Hosting;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000071 RID: 113
	internal sealed class UtteranceFeedStore
	{
		// Token: 0x06000326 RID: 806 RVA: 0x0000A2BB File Offset: 0x000084BB
		public UtteranceFeedStore(IStreamBasedStorage storage)
		{
			this.m_storage = storage;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000A2CC File Offset: 0x000084CC
		public async Task StoreUtteranceFeedAsync(string datasetId, Stream stream, CancellationToken cancellationToken)
		{
			string storageKey = UtteranceFeedStore.GetStorageKey(datasetId);
			using (Stream outputStream = this.m_storage.CreateNewEntry(storageKey, true))
			{
				await stream.CopyToAsync(outputStream, 81920, cancellationToken);
			}
			Stream outputStream = null;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000A328 File Offset: 0x00008528
		public string GetUtteranceHistory(string datasetId, CancellationToken cancellationToken)
		{
			string storageKey = UtteranceFeedStore.GetStorageKey(datasetId);
			if (!this.m_storage.ContainsEntry(storageKey))
			{
				return null;
			}
			cancellationToken.ThrowIfCancellationRequested();
			string text;
			using (UtteranceFeedReader utteranceFeedReader = UtteranceFeedReader.Create(this.m_storage.GetExistingEntry(storageKey), false))
			{
				if (!utteranceFeedReader.TryReadMetadata())
				{
					text = null;
				}
				else
				{
					text = JsonConvert.SerializeObject(new UtteranceHistoryResult(UtteranceFeedStore.GetUtteranceViewItems(utteranceFeedReader, cancellationToken), new DateTime?(utteranceFeedReader.Metadata.Timestamp), UtteranceFeedExtendedMetadata.DatasetName(utteranceFeedReader.Metadata), UtteranceFeedExtendedMetadata.WorkspaceName(utteranceFeedReader.Metadata)));
				}
			}
			return text;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000A3C8 File Offset: 0x000085C8
		private static IEnumerable<UtteranceViewItem> GetUtteranceViewItems(UtteranceFeedReader reader, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			foreach (UtteranceViewItem utteranceViewItem in UtteranceViewService.GetUtterances(reader.ReadAll()))
			{
				cancellationToken.ThrowIfCancellationRequested();
				yield return utteranceViewItem;
			}
			IEnumerator<UtteranceViewItem> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000A3DF File Offset: 0x000085DF
		private static string GetStorageKey(string datasetId)
		{
			return "UtteranceFeed-" + datasetId;
		}

		// Token: 0x0400016C RID: 364
		private readonly IStreamBasedStorage m_storage;
	}
}
