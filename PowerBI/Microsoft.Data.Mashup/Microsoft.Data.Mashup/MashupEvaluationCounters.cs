using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200002E RID: 46
	public class MashupEvaluationCounters : IContainerEvaluationMonitorService
	{
		// Token: 0x0600026F RID: 623 RVA: 0x0000A637 File Offset: 0x00008837
		internal MashupEvaluationCounters()
		{
			this.containerProcessData = new Dictionary<int, MashupEvaluationCounters.ProcessData>();
			this.processIdsToInstance = new Dictionary<int, string>();
			this.totalProcessorTime = TimeSpan.Zero;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000A660 File Offset: 0x00008860
		public bool Running
		{
			get
			{
				Dictionary<int, MashupEvaluationCounters.ProcessData> dictionary = this.containerProcessData;
				bool flag2;
				lock (dictionary)
				{
					flag2 = this.IsRunning();
				}
				return flag2;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000A6A4 File Offset: 0x000088A4
		public long? WorkingSet
		{
			get
			{
				return this.GetLongCounter(MashupEvaluationCounters.CounterIndex.WorkingSet);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000A6AD File Offset: 0x000088AD
		public long? Commit
		{
			get
			{
				return this.GetLongCounter(MashupEvaluationCounters.CounterIndex.Commit);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000A6B6 File Offset: 0x000088B6
		public float? PercentProcessorTime
		{
			get
			{
				return this.GetFloatCounter(MashupEvaluationCounters.CounterIndex.PercentProcessorTime);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000A6BF File Offset: 0x000088BF
		public float? IODataBytesPerSecond
		{
			get
			{
				return this.GetFloatCounter(MashupEvaluationCounters.CounterIndex.IoDataBytesPerSecond);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000A6C8 File Offset: 0x000088C8
		public TimeSpan? TotalProcessorTime
		{
			get
			{
				Dictionary<int, MashupEvaluationCounters.ProcessData> dictionary = this.containerProcessData;
				TimeSpan? timeSpan2;
				lock (dictionary)
				{
					TimeSpan timeSpan = this.totalProcessorTime;
					foreach (MashupEvaluationCounters.ProcessData processData in this.containerProcessData.Values)
					{
						timeSpan += processData.ProcessorTime;
					}
					timeSpan2 = new TimeSpan?(timeSpan);
				}
				return timeSpan2;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000A764 File Offset: 0x00008964
		private long? GetLongCounter(MashupEvaluationCounters.CounterIndex counterIndex)
		{
			float num;
			if (!this.TryGetCounter(counterIndex, out num))
			{
				return null;
			}
			return new long?((long)num);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A790 File Offset: 0x00008990
		private float? GetFloatCounter(MashupEvaluationCounters.CounterIndex counterIndex)
		{
			float num;
			if (!this.TryGetCounter(counterIndex, out num))
			{
				return null;
			}
			return new float?(num);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000A7B8 File Offset: 0x000089B8
		private bool TryGetCounter(MashupEvaluationCounters.CounterIndex counterIndex, out float result)
		{
			result = 0f;
			if (!this.IsRunning())
			{
				return true;
			}
			if (MashupEvaluationCounters.processCategory == null && !MashupEvaluationCounters.TryInitialize())
			{
				return false;
			}
			InstanceDataCollectionCollection instanceDataCollectionCollection;
			DateTime dateTime;
			if (!MashupEvaluationCounters.TryReadProcessCategory(out instanceDataCollectionCollection, out dateTime))
			{
				return false;
			}
			Dictionary<int, string> dictionary = this.processIdsToInstance;
			lock (dictionary)
			{
				if (dateTime > this.instanceLastRead)
				{
					this.SetInstanceNames(instanceDataCollectionCollection);
					this.instanceLastRead = dateTime;
				}
			}
			dictionary = this.processIdsToInstance;
			bool flag3;
			lock (dictionary)
			{
				bool flag2 = false;
				foreach (KeyValuePair<int, string> keyValuePair in this.processIdsToInstance)
				{
					Dictionary<int, MashupEvaluationCounters.ProcessData> dictionary2 = this.containerProcessData;
					MashupEvaluationCounters.ProcessData processData;
					lock (dictionary2)
					{
						if (!this.containerProcessData.TryGetValue(keyValuePair.Key, out processData))
						{
							continue;
						}
					}
					if (processData.TryReadCounter(instanceDataCollectionCollection, counterIndex, keyValuePair.Value))
					{
						float? counter = processData.GetCounter(counterIndex);
						if (counter != null)
						{
							flag2 = true;
							result += counter.Value;
						}
					}
				}
				flag3 = flag2;
			}
			return flag3;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000A928 File Offset: 0x00008B28
		private static bool TryInitialize()
		{
			object obj = MashupEvaluationCounters.syncRoot;
			bool flag2;
			lock (obj)
			{
				if (MashupEvaluationCounters.processCategory == null)
				{
					try
					{
						if (PerformanceCounterCategory.Exists("Process"))
						{
							MashupEvaluationCounters.processCategory = new PerformanceCounterCategory("Process");
							return true;
						}
					}
					catch (SEHException obj2) when (FxVersionDetector.FxVersion >= ClrVersion.Net40)
					{
					}
					catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
					{
					}
					flag2 = false;
				}
				else
				{
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000A9E4 File Offset: 0x00008BE4
		internal void Close()
		{
			Dictionary<int, MashupEvaluationCounters.ProcessData> dictionary = this.containerProcessData;
			lock (dictionary)
			{
				foreach (MashupEvaluationCounters.ProcessData processData in this.containerProcessData.Values)
				{
					this.totalProcessorTime += processData.CloseAndGetFinalProcessorTime();
				}
				this.containerProcessData.Clear();
				this.processIdsToInstance.Clear();
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000AA8C File Offset: 0x00008C8C
		private bool IsRunning()
		{
			if (this.containerProcessData.Count > 0)
			{
				return this.containerProcessData.Any((KeyValuePair<int, MashupEvaluationCounters.ProcessData> d) => d.Value.IsValid);
			}
			return false;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000AAC8 File Offset: 0x00008CC8
		IDisposable IContainerEvaluationMonitorService.BeginEvaluation(SafeHandle processHandle)
		{
			MashupEvaluationCounters.ProcessData processData = new MashupEvaluationCounters.ProcessData(processHandle);
			Dictionary<int, MashupEvaluationCounters.ProcessData> dictionary = this.containerProcessData;
			lock (dictionary)
			{
				this.containerProcessData.Add(processData.ProcessId, processData);
			}
			return new MashupEvaluationCounters.EndOfEvaluation(this, processData);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000AB24 File Offset: 0x00008D24
		private void EndEvaluation(MashupEvaluationCounters.ProcessData processData)
		{
			Dictionary<int, MashupEvaluationCounters.ProcessData> dictionary = this.containerProcessData;
			lock (dictionary)
			{
				this.totalProcessorTime += processData.CloseAndGetFinalProcessorTime();
				this.containerProcessData.Remove(processData.ProcessId);
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000AB88 File Offset: 0x00008D88
		private static bool TryReadProcessCategory(out InstanceDataCollectionCollection data, out DateTime readTime)
		{
			object obj = MashupEvaluationCounters.syncRoot;
			bool flag2;
			lock (obj)
			{
				if (DateTime.UtcNow - MashupEvaluationCounters.dataLastRead > MashupEvaluationCounters.oneSecond)
				{
					try
					{
						MashupEvaluationCounters.countersData = MashupEvaluationCounters.processCategory.ReadCategory();
						MashupEvaluationCounters.dataLastRead = DateTime.UtcNow;
					}
					catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
					{
						readTime = MashupEvaluationCounters.dataLastRead;
						data = null;
						return false;
					}
				}
				readTime = MashupEvaluationCounters.dataLastRead;
				data = MashupEvaluationCounters.countersData;
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000AC44 File Offset: 0x00008E44
		private void SetInstanceNames(InstanceDataCollectionCollection data)
		{
			InstanceDataCollection instanceDataCollection = data["ID Process"];
			if (instanceDataCollection == null)
			{
				return;
			}
			this.processIdsToInstance.Clear();
			Dictionary<int, MashupEvaluationCounters.ProcessData> dictionary = this.containerProcessData;
			HashSet<int> hashSet;
			lock (dictionary)
			{
				hashSet = new HashSet<int>(this.containerProcessData.Values.Select((MashupEvaluationCounters.ProcessData p) => p.ProcessId));
			}
			foreach (object obj in instanceDataCollection.Values)
			{
				InstanceData instanceData = (InstanceData)obj;
				if (hashSet.Count == 0)
				{
					break;
				}
				int num = (int)instanceData.RawValue;
				if (hashSet.Remove(num))
				{
					this.processIdsToInstance.Add(num, instanceData.InstanceName);
				}
			}
		}

		// Token: 0x0400014E RID: 334
		private static readonly TimeSpan oneSecond = TimeSpan.FromSeconds(1.0);

		// Token: 0x0400014F RID: 335
		private static readonly string[] counterNames = new string[] { "Working Set", "Private Bytes", "% Processor Time", "IO Data Bytes/sec" };

		// Token: 0x04000150 RID: 336
		private static object syncRoot = new object();

		// Token: 0x04000151 RID: 337
		private static PerformanceCounterCategory processCategory;

		// Token: 0x04000152 RID: 338
		private static InstanceDataCollectionCollection countersData;

		// Token: 0x04000153 RID: 339
		private static DateTime dataLastRead;

		// Token: 0x04000154 RID: 340
		private readonly Dictionary<int, MashupEvaluationCounters.ProcessData> containerProcessData;

		// Token: 0x04000155 RID: 341
		private readonly Dictionary<int, string> processIdsToInstance;

		// Token: 0x04000156 RID: 342
		private TimeSpan totalProcessorTime;

		// Token: 0x04000157 RID: 343
		private DateTime instanceLastRead;

		// Token: 0x02000078 RID: 120
		private class EndOfEvaluation : IDisposable
		{
			// Token: 0x060004C7 RID: 1223 RVA: 0x00011B0D File Offset: 0x0000FD0D
			public EndOfEvaluation(MashupEvaluationCounters counters, MashupEvaluationCounters.ProcessData processData)
			{
				this.counters = counters;
				this.processData = processData;
			}

			// Token: 0x060004C8 RID: 1224 RVA: 0x00011B23 File Offset: 0x0000FD23
			public void Dispose()
			{
				if (this.processData != null)
				{
					this.counters.EndEvaluation(this.processData);
					this.processData = null;
				}
			}

			// Token: 0x04000280 RID: 640
			private readonly MashupEvaluationCounters counters;

			// Token: 0x04000281 RID: 641
			private MashupEvaluationCounters.ProcessData processData;
		}

		// Token: 0x02000079 RID: 121
		private enum CounterIndex
		{
			// Token: 0x04000283 RID: 643
			WorkingSet,
			// Token: 0x04000284 RID: 644
			Commit,
			// Token: 0x04000285 RID: 645
			PercentProcessorTime,
			// Token: 0x04000286 RID: 646
			IoDataBytesPerSecond,
			// Token: 0x04000287 RID: 647
			Count
		}

		// Token: 0x0200007A RID: 122
		private class ProcessData
		{
			// Token: 0x060004C9 RID: 1225 RVA: 0x00011B48 File Offset: 0x0000FD48
			public ProcessData(SafeHandle processHandle)
			{
				this.syncRoot = new object();
				if (processHandle != null)
				{
					this.processHandle = ProcessHelpers.DuplicateHandle(processHandle);
					this.processId = ProcessInfo.GetProcessId(this.processHandle);
					this.startingProcessorTime = ProcessInfo.GetTotalProcessorTime(processHandle);
					this.currentProcessorTime = this.startingProcessorTime;
				}
			}

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x060004CA RID: 1226 RVA: 0x00011B9E File Offset: 0x0000FD9E
			public bool IsValid
			{
				get
				{
					return this.processHandle != null && !this.processHandle.IsClosed && !this.processHandle.IsInvalid;
				}
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x060004CB RID: 1227 RVA: 0x00011BC5 File Offset: 0x0000FDC5
			public int ProcessId
			{
				get
				{
					return this.processId;
				}
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x060004CC RID: 1228 RVA: 0x00011BD0 File Offset: 0x0000FDD0
			public TimeSpan ProcessorTime
			{
				get
				{
					if (this.processHandle != null)
					{
						TimeSpan? totalProcessorTime = ProcessInfo.GetTotalProcessorTime(this.processHandle);
						this.currentProcessorTime = ((totalProcessorTime != null) ? totalProcessorTime : this.currentProcessorTime);
					}
					if (this.currentProcessorTime == null || this.startingProcessorTime == null)
					{
						return TimeSpan.Zero;
					}
					return this.currentProcessorTime.Value - this.startingProcessorTime.Value;
				}
			}

			// Token: 0x060004CD RID: 1229 RVA: 0x00011C44 File Offset: 0x0000FE44
			public float? GetCounter(MashupEvaluationCounters.CounterIndex index)
			{
				object obj = this.syncRoot;
				float? num;
				lock (obj)
				{
					if (this.currentSamples == null)
					{
						num = null;
						num = num;
					}
					else
					{
						try
						{
							return new float?(CounterSample.Calculate(this.oldSamples[(int)index], this.currentSamples[(int)index]));
						}
						catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
						{
						}
						num = null;
					}
				}
				return num;
			}

			// Token: 0x060004CE RID: 1230 RVA: 0x00011CE8 File Offset: 0x0000FEE8
			public bool TryReadCounter(InstanceDataCollectionCollection data, MashupEvaluationCounters.CounterIndex counterIndex, string instanceName)
			{
				if (this.currentSamples == null)
				{
					this.Initialize();
				}
				object obj = this.syncRoot;
				bool flag2;
				lock (obj)
				{
					CounterSample counterSample = this.currentSamples[(int)counterIndex];
					CounterSample counterSample2;
					if (MashupEvaluationCounters.ProcessData.TryGetCounterSample(data, MashupEvaluationCounters.counterNames[(int)counterIndex], instanceName, out counterSample2))
					{
						if (counterSample != counterSample2)
						{
							this.oldSamples[(int)counterIndex] = counterSample;
							this.currentSamples[(int)counterIndex] = counterSample2;
						}
						flag2 = true;
					}
					else
					{
						flag2 = false;
					}
				}
				return flag2;
			}

			// Token: 0x060004CF RID: 1231 RVA: 0x00011D7C File Offset: 0x0000FF7C
			public TimeSpan CloseAndGetFinalProcessorTime()
			{
				TimeSpan processorTime = this.ProcessorTime;
				if (this.processHandle != null)
				{
					this.processHandle.Dispose();
					this.processHandle = null;
				}
				return processorTime;
			}

			// Token: 0x060004D0 RID: 1232 RVA: 0x00011DA0 File Offset: 0x0000FFA0
			private void Initialize()
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.currentSamples == null)
					{
						this.currentSamples = new CounterSample[4];
						this.oldSamples = new CounterSample[4];
						for (int i = 0; i < 4; i++)
						{
							this.currentSamples[i] = CounterSample.Empty;
							this.oldSamples[i] = CounterSample.Empty;
						}
					}
				}
			}

			// Token: 0x060004D1 RID: 1233 RVA: 0x00011E28 File Offset: 0x00010028
			private static bool TryGetCounterSample(InstanceDataCollectionCollection data, string counterName, string instanceName, out CounterSample sample)
			{
				InstanceDataCollection instanceDataCollection = data[counterName];
				if (instanceDataCollection != null)
				{
					InstanceData instanceData = instanceDataCollection[instanceName];
					if (instanceData != null)
					{
						sample = instanceData.Sample;
						return true;
					}
				}
				sample = CounterSample.Empty;
				return false;
			}

			// Token: 0x04000288 RID: 648
			private readonly int processId;

			// Token: 0x04000289 RID: 649
			private readonly object syncRoot;

			// Token: 0x0400028A RID: 650
			private SafeHandle processHandle;

			// Token: 0x0400028B RID: 651
			private TimeSpan? startingProcessorTime;

			// Token: 0x0400028C RID: 652
			private TimeSpan? currentProcessorTime;

			// Token: 0x0400028D RID: 653
			private CounterSample[] oldSamples;

			// Token: 0x0400028E RID: 654
			private CounterSample[] currentSamples;
		}
	}
}
