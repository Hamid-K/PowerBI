using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.InfoNav;
using Microsoft.PowerBI.Lucia.Hosting.SchemaAnnotations;
using Microsoft.PowerBI.Lucia.Hosting.SchemaAnnotations.Serialization;
using Microsoft.PowerBI.Telemetry;
using Microsoft.ReportingServices.Common;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x0200004B RID: 75
	internal sealed class ColumnStatisticsStore : IColumnStatisticsStore
	{
		// Token: 0x0600024E RID: 590 RVA: 0x00007539 File Offset: 0x00005739
		private ColumnStatisticsStore(IStreamBasedStorage storage, string storageKey, IEnumerable<string> alternateKeys, bool searchForPrimaryKeyOnly)
		{
			this.m_storage = storage;
			this.m_storageKey = storageKey;
			this.m_alternateKeys = alternateKeys.AsReadOnlyList<string>();
			this.m_searchForPrimaryKeyOnly = searchForPrimaryKeyOnly;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00007564 File Offset: 0x00005764
		internal static IColumnStatisticsStore CreateFor(IConceptualSchema schema, IStreamBasedStorage storage, IEnumerable<string> alternateKeys, bool searchForPrimaryKeyOnly)
		{
			string text = ColumnStatisticsStore.CreateSchemaBasedKeyComponent(schema);
			return ColumnStatisticsStore.CreateFor(storage, text, alternateKeys, searchForPrimaryKeyOnly);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00007581 File Offset: 0x00005781
		internal static IColumnStatisticsStore CreateFor(IStreamBasedStorage storage, string primaryKey, IEnumerable<string> alternateKeys, bool searchForPrimaryKeyOnly)
		{
			string primaryKey2 = ColumnStatisticsStore.GetPrimaryKey(primaryKey);
			IEnumerable<string> enumerable;
			if (alternateKeys == null)
			{
				enumerable = null;
			}
			else
			{
				enumerable = alternateKeys.Select((string e) => ColumnStatisticsStore.GetAlternateKeyLookupKey(e));
			}
			return new ColumnStatisticsStore(storage, primaryKey2, enumerable, searchForPrimaryKeyOnly);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000075BC File Offset: 0x000057BC
		public Task<ColumnStatisticsMetadata> GetColumnStatisticsMetadataAsync(CancellationToken cancellationToken)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			Task<ColumnStatisticsMetadata> task;
			try
			{
				Stream stream = this.m_storage.GetExistingEntry(this.m_storageKey);
				if (stream == null)
				{
					if (this.m_searchForPrimaryKeyOnly)
					{
						goto IL_00D3;
					}
					ExploreTracer.Instance.TraceInformation("ColumnStatisticsStore: Can't find a stream for column statistics with primary key '" + this.m_storageKey + "'. Trying to look up for alternate keys instead.");
					using (IEnumerator<string> enumerator = this.m_alternateKeys.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							cancellationToken.ThrowIfCancellationRequested();
							string text2;
							if (this.TryGetMappedPrimaryKey(text, cancellationToken, out text2))
							{
								stream = this.m_storage.GetExistingEntry(text2);
								if (stream != null)
								{
									ExploreTracer.Instance.TraceInformation("ColumnStatisticsStore: Found a stream for alternate key '" + text + "'");
									break;
								}
							}
						}
						goto IL_00D3;
					}
				}
				ExploreTracer.Instance.TraceInformation("ColumnStatisticsStore: Found a stream for column statistics with primary key '" + this.m_storageKey + "'.");
				IL_00D3:
				ColumnStatisticsMetadata columnStatisticsMetadata = null;
				if (stream != null)
				{
					using (stream)
					{
						cancellationToken.ThrowIfCancellationRequested();
						ColumnStatisticsMetadata columnStatisticsMetadata2 = ColumnStatisticsMetadata.Load(stream);
						cancellationToken.ThrowIfCancellationRequested();
						columnStatisticsMetadata = ColumnStatisticsMetadata.CreateFrom(columnStatisticsMetadata2);
						goto IL_0112;
					}
				}
				ExploreTracer.Instance.TraceInformation("ColumnStatisticsStore: No stream for column statistics found with the given keys.");
				IL_0112:
				TelemetryService.Instance.Log(new PBIWinColumnStatisticsAction("GetColumnStatisticsMetadataAsync", "Success", null, stopwatch.Elapsed.ToString()));
				task = Task.FromResult<ColumnStatisticsMetadata>(columnStatisticsMetadata);
			}
			catch (Exception ex) when (!AsynchronousExceptionDetection.IsStoppingException(ex))
			{
				TelemetryService.Instance.Log(new PBIWinColumnStatisticsAction("GetColumnStatisticsMetadataAsync", "Failed", ex.ToTraceString(), stopwatch.Elapsed.ToString()));
				task = Task.FromResult<ColumnStatisticsMetadata>(null);
			}
			return task;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000077C0 File Offset: 0x000059C0
		public Task StoreColumnStatisticsMetadataAsync(ColumnStatisticsMetadata columnStatistics, CancellationToken cancellationToken)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			Task task;
			try
			{
				cancellationToken.ThrowIfCancellationRequested();
				using (Stream stream = this.m_storage.CreateNewEntry(this.m_storageKey, true))
				{
					ColumnStatisticsMetadata columnStatisticsMetadata = new ColumnStatisticsMetadata();
					columnStatistics.WriteTo(columnStatisticsMetadata);
					columnStatisticsMetadata.Save(stream);
				}
				foreach (string text in this.m_alternateKeys)
				{
					cancellationToken.ThrowIfCancellationRequested();
					this.StoreAlternateKey(text);
				}
				TelemetryService.Instance.Log(new PBIWinColumnStatisticsAction("StoreColumnStatisticsMetadataAsync", "Success", null, stopwatch.Elapsed.ToString()));
				task = Task.FromResult<int>(0);
			}
			catch (Exception ex) when (!AsynchronousExceptionDetection.IsStoppingException(ex))
			{
				TelemetryService.Instance.Log(new PBIWinColumnStatisticsAction("StoreColumnStatisticsMetadataAsync", "Failed", ex.ToTraceString(), stopwatch.Elapsed.ToString()));
				task = Task.FromResult<int>(0);
			}
			return task;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00007908 File Offset: 0x00005B08
		private void StoreAlternateKey(string alternateKey)
		{
			try
			{
				using (Stream stream = this.m_storage.CreateNewEntry(alternateKey, true))
				{
					if (stream != null)
					{
						using (StreamWriter streamWriter = new StreamWriter(stream))
						{
							streamWriter.Write(this.m_storageKey);
						}
					}
				}
			}
			catch (Exception ex) when (!AsynchronousExceptionDetection.IsStoppingException(ex))
			{
				ExploreTracer.Instance.TraceWarning(string.Format("{0}: An exception occurred while saving an alternate key for column statistics: '{1}'.", "ColumnStatisticsStore", ex));
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000079B4 File Offset: 0x00005BB4
		private bool TryGetMappedPrimaryKey(string alternateKey, CancellationToken token, out string primaryKey)
		{
			bool flag;
			try
			{
				token.ThrowIfCancellationRequested();
				using (Stream existingEntry = this.m_storage.GetExistingEntry(alternateKey))
				{
					token.ThrowIfCancellationRequested();
					if (existingEntry == null)
					{
						primaryKey = null;
						flag = false;
					}
					else
					{
						using (StreamReader streamReader = new StreamReader(existingEntry))
						{
							primaryKey = streamReader.ReadToEnd();
							flag = true;
						}
					}
				}
			}
			catch (Exception ex) when (!AsynchronousExceptionDetection.IsStoppingException(ex))
			{
				ExploreTracer.Instance.TraceWarning(string.Format("{0}: An exception occurred while retrieving primary key from alternate key: '{1}'.", "ColumnStatisticsStore", ex));
				primaryKey = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00007A74 File Offset: 0x00005C74
		private static string GetPrimaryKey(string key)
		{
			return "ColumnStatistics_" + key;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00007A81 File Offset: 0x00005C81
		private static string GetAlternateKeyLookupKey(string key)
		{
			return "ColumnStatisticsAlternateKey_" + key;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00007A90 File Offset: 0x00005C90
		private static string CreateSchemaBasedKeyComponent(IConceptualSchema schema)
		{
			IEnumerable<string> enumerable = (from c in schema.Entities.SelectMany((IConceptualEntity e) => e.Properties).OfType<IConceptualColumn>()
				where c.ConceptualDataType != ConceptualPrimitiveType.Binary
				select c into p
				select p.GetFullName()).OrderBy(null);
			return string.Join("_", enumerable);
		}

		// Token: 0x040000E4 RID: 228
		private const string KeyPrefix = "ColumnStatistics_";

		// Token: 0x040000E5 RID: 229
		private const string AlternateLookupKeyPrefix = "ColumnStatisticsAlternateKey_";

		// Token: 0x040000E6 RID: 230
		private readonly IStreamBasedStorage m_storage;

		// Token: 0x040000E7 RID: 231
		private readonly string m_storageKey;

		// Token: 0x040000E8 RID: 232
		private readonly IReadOnlyList<string> m_alternateKeys;

		// Token: 0x040000E9 RID: 233
		private readonly bool m_searchForPrimaryKeyOnly;
	}
}
