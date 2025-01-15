using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Etlx;
using Microsoft.Diagnostics.Tracing.Parsers;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x02000396 RID: 918
	[BlockServiceProvider(typeof(IEtwEventsReader))]
	public class EtwEventsReader : Block, IEtwEventsReader
	{
		// Token: 0x06001C43 RID: 7235 RVA: 0x0006B9C3 File Offset: 0x00069BC3
		public EtwEventsReader()
			: base(typeof(EtwEventsReader).Name)
		{
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x0006B9DC File Offset: 0x00069BDC
		public IAsyncResult BeginReadProvidersManifests(IEnumerable<string> providersManifestsFiles, AsyncCallback callback, object context)
		{
			bool flag;
			if (!providersManifestsFiles.All((string file) => Path.GetExtension(file).Equals(".etl", StringComparison.Ordinal) || Path.GetExtension(file).Equals(".etlx", StringComparison.Ordinal)))
			{
				flag = providersManifestsFiles.All((string file) => Path.GetExtension(file).Equals(".xml", StringComparison.Ordinal));
			}
			else
			{
				flag = true;
			}
			ExtendedDiagnostics.EnsureOperation(flag, "Can read manifests either from etl|etlx files or serialized xml files, not both");
			if (providersManifestsFiles.None<string>())
			{
				return new CompletedAsyncResult<EtwProvidersManifests>(callback, context, new EtwProvidersManifests(Enumerable.Empty<ProviderManifest>()));
			}
			if (Path.GetExtension(providersManifestsFiles.First<string>()).Equals(".xml", StringComparison.Ordinal))
			{
				return new CompletedAsyncResult<EtwProvidersManifests>(callback, context, this.DeserializeManifests(providersManifestsFiles));
			}
			return new CompletedAsyncResult<EtwProvidersManifests>(callback, context, this.ReadProviderManifestsFromEtlFiles(providersManifestsFiles));
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x0006BA91 File Offset: 0x00069C91
		public EtwProvidersManifests EndReadProvidersManifests(IAsyncResult asyncResult)
		{
			return ((CompletedAsyncResult<EtwProvidersManifests>)asyncResult).End();
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x0006BAA0 File Offset: 0x00069CA0
		public IAsyncResult BeginReadEtwEvents(EtwProvidersManifests providersManifests, IEnumerable<string> etlPaths, DateTime from, DateTime to, EventsQueryFilter filter, EtwEventsReaderOptions options, AsyncCallback callback, object context)
		{
			List<EtwEvent> events = new List<EtwEvent>();
			if (etlPaths == null || etlPaths.None<string>())
			{
				return new CompletedAsyncResult<IEnumerable<EtwEvent>>(callback, context, events);
			}
			IEnumerable<string> enumerable = etlPaths.Where(new Func<string, bool>(EventFilesNaming.IsCompressed));
			Dictionary<string, string> dictionary = enumerable.ToDictionary((string compressed) => compressed, (string compressed) => EventFilesNaming.GetDecompressedEventFileName(compressed));
			foreach (KeyValuePair<string, string> keyValuePair in dictionary)
			{
				Compression.DecompressFile(keyValuePair.Key, keyValuePair.Value, 65536);
			}
			List<string> list = dictionary.Values.Concat(etlPaths.Except(enumerable)).ToList<string>();
			TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Parsing a batch of {0} ETW event file(s), starting from '{1}'", new object[]
			{
				list.Count,
				list.First<string>()
			});
			using (ETWTraceEventSource etwtraceEventSource = new ETWTraceEventSource(list))
			{
				DynamicTraceEventParser dynamicTraceEventParser = new DynamicTraceEventParser(etwtraceEventSource);
				foreach (ProviderManifest providerManifest in providersManifests.ProviderManifests)
				{
					dynamicTraceEventParser.AddDynamicProvider(providerManifest, false);
				}
				dynamicTraceEventParser.All += delegate(TraceEvent traceEvent)
				{
					DateTime dateTime = traceEvent.TimeStamp.ToUniversalTime();
					if (!traceEvent.IsManifestEvent() && dateTime >= from && dateTime < to)
					{
						EtwEvent etwEvent = EtwEventsReader.TraceEventToEtwEvent(traceEvent, options);
						if (filter == null || filter.IsMatch(etwEvent))
						{
							events.Add(etwEvent);
						}
					}
				};
				etwtraceEventSource.Process();
			}
			EtwEventsReader.DeleteDecompressedEtlFiles(dictionary.Values, options);
			return new CompletedAsyncResult<IEnumerable<EtwEvent>>(callback, context, events);
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x0006BC94 File Offset: 0x00069E94
		public IAsyncResult BeginReadEtwEvents(EtwProvidersManifests providersManifests, IEnumerable<string> etlPaths, EventsQueryFilter filter, EtwEventsReaderOptions options, AsyncCallback callback, object context)
		{
			return this.BeginReadEtwEvents(providersManifests, etlPaths, DateTime.MinValue, DateTime.MaxValue, filter, options, callback, context);
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x0006BCBA File Offset: 0x00069EBA
		public IEnumerable<EtwEvent> EndReadEtwEvents(IAsyncResult asyncResult)
		{
			return CompletedAsyncResult<IEnumerable<EtwEvent>>.End(asyncResult);
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x0006BCC4 File Offset: 0x00069EC4
		private EtwProvidersManifests DeserializeManifests(IEnumerable<string> serializedManifestsFiles)
		{
			List<string> list = new List<string>();
			foreach (string text in serializedManifestsFiles)
			{
				using (FileStream fileStream = File.OpenRead(text))
				{
					SerializedProvidersManifests serializedProvidersManifests = (SerializedProvidersManifests)new DataContractSerializer(typeof(SerializedProvidersManifests)).ReadObject(fileStream);
					list.AddRange(serializedProvidersManifests.SerializedManifests);
				}
			}
			return new EtwProvidersManifests(list);
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x0006BD58 File Offset: 0x00069F58
		private EtwProvidersManifests ReadProviderManifestsFromEtlFiles(IEnumerable<string> providersManifestsFiles)
		{
			List<ProviderManifest> list = new List<ProviderManifest>();
			foreach (string text in providersManifestsFiles)
			{
				if (new FileInfo(text).Length > 65536L)
				{
					using (TraceLog traceLog = TraceLog.OpenOrConvert(text, null))
					{
						list.AddRange(traceLog.Dynamic.DynamicProviders);
					}
				}
			}
			return new EtwProvidersManifests(list);
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x0006BDEC File Offset: 0x00069FEC
		private static EtwEvent TraceEventToEtwEvent(TraceEvent traceEvent, EtwEventsReaderOptions options)
		{
			return EtwEvent.CreateFromTraceEvent(traceEvent, options);
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x0006BDF8 File Offset: 0x00069FF8
		private static void DeleteDecompressedEtlFiles(IEnumerable<string> filePaths, EtwEventsReaderOptions options)
		{
			foreach (string text in filePaths)
			{
				try
				{
					if (File.Exists(text))
					{
						File.Delete(text);
					}
				}
				catch (IOException ex)
				{
					TraceSourceBase<EventingTrace>.Tracer.TraceWarning("Failed deleting file {0}. Exception: {1}", new object[] { text, ex.Message });
					if (!options.HasFlag(EtwEventsReaderOptions.SwallowDeleteFilesErrors))
					{
						throw;
					}
				}
				catch (UnauthorizedAccessException ex2)
				{
					TraceSourceBase<EventingTrace>.Tracer.TraceWarning("Failed deleting file {0}. Exception: {1}", new object[] { text, ex2.Message });
					if (!options.HasFlag(EtwEventsReaderOptions.SwallowDeleteFilesErrors))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x04000984 RID: 2436
		private const int c_emptyEtlFileSize = 65536;
	}
}
