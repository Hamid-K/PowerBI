using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000128 RID: 296
	public static class ProgressReporting
	{
		// Token: 0x02000129 RID: 297
		public sealed class ProgressChannel : IProgressChannel, IProgressChannelProvider, IDisposable
		{
			// Token: 0x1700007A RID: 122
			// (get) Token: 0x0600060D RID: 1549 RVA: 0x00020D3C File Offset: 0x0001EF3C
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x0600060E RID: 1550 RVA: 0x00020D44 File Offset: 0x0001EF44
			public ProgressChannel(IExceptionContext ectx, ProgressReporting.ProgressTracker tracker, string computationName)
			{
				this._ectx = ectx;
				Contracts.CheckValue<ProgressReporting.ProgressTracker>(this._ectx, tracker, "tracker");
				Contracts.CheckNonEmpty(this._ectx, computationName, "processName");
				this._name = computationName;
				this._tracker = tracker;
				this._subChannels = new ConcurrentDictionary<int, ProgressReporting.ProgressChannel.SubChannel>();
				this._maxSubId = 0;
				this._headerAndAction = Tuple.Create<ProgressHeader, Action<IProgressEntry>>(new ProgressHeader(null), null);
				this.Start();
			}

			// Token: 0x0600060F RID: 1551 RVA: 0x00020DBB File Offset: 0x0001EFBB
			public void SetHeader(ProgressHeader header, Action<IProgressEntry> fillAction)
			{
				this._headerAndAction = Tuple.Create<ProgressHeader, Action<IProgressEntry>>(header, fillAction);
			}

			// Token: 0x06000610 RID: 1552 RVA: 0x00020DCC File Offset: 0x0001EFCC
			public void Checkpoint(params double?[] values)
			{
				Contracts.Check(this._ectx, !this._isDisposed, "Can't report checkpoints after disposing");
				ProgressReporting.ProgressEntry progressEntry = new ProgressReporting.ProgressEntry(true, this._headerAndAction.Item1);
				int num = Utils.Size<double?>(values);
				int num2 = 0;
				int num3 = 0;
				while (num3 < progressEntry.Metrics.Length && num2 < num)
				{
					progressEntry.Metrics[num3++] = values[num2++];
				}
				int num4 = 0;
				while (num4 < progressEntry.Progress.Length && num2 < num)
				{
					progressEntry.Progress[num4++] = values[num2++];
				}
				int num5 = 0;
				while (num5 < progressEntry.ProgressLim.Length && num2 < num)
				{
					double? num6 = values[num2++];
					if (double.IsNaN(num6.GetValueOrDefault()))
					{
						num6 = null;
					}
					progressEntry.ProgressLim[num5++] = num6;
				}
				Contracts.Check(this._ectx, num2 == num, "Too many values provided in Checkpoint");
				this._tracker.Log(this, ProgressReporting.ProgressEvent.EventKind.Progress, progressEntry);
			}

			// Token: 0x06000611 RID: 1553 RVA: 0x00020EF9 File Offset: 0x0001F0F9
			private void Start()
			{
				this._tracker.Log(this, ProgressReporting.ProgressEvent.EventKind.Start, null);
			}

			// Token: 0x06000612 RID: 1554 RVA: 0x00020F09 File Offset: 0x0001F109
			private void Stop()
			{
				this._tracker.Log(this, ProgressReporting.ProgressEvent.EventKind.Stop, null);
			}

			// Token: 0x06000613 RID: 1555 RVA: 0x00020F19 File Offset: 0x0001F119
			public void Dispose()
			{
				if (this._isDisposed)
				{
					return;
				}
				this._isDisposed = true;
				this.Stop();
			}

			// Token: 0x06000614 RID: 1556 RVA: 0x00020F34 File Offset: 0x0001F134
			public ProgressReporting.ProgressEntry GetProgress()
			{
				Tuple<ProgressHeader, Action<IProgressEntry>> headerAndAction = this._headerAndAction;
				Action<IProgressEntry> item = headerAndAction.Item2;
				ProgressReporting.ProgressEntry progressEntry = new ProgressReporting.ProgressEntry(false, headerAndAction.Item1);
				if (item != null)
				{
					item(progressEntry);
				}
				return this.BuildJointEntry(progressEntry);
			}

			// Token: 0x06000615 RID: 1557 RVA: 0x00020F6D File Offset: 0x0001F16D
			public IProgressChannel StartProgressChannel(string name)
			{
				return this.StartProgressChannel(1);
			}

			// Token: 0x06000616 RID: 1558 RVA: 0x00020F78 File Offset: 0x0001F178
			private IProgressChannel StartProgressChannel(int level)
			{
				int num = Interlocked.Increment(ref this._maxSubId);
				return new ProgressReporting.ProgressChannel.SubChannel(this, level, num);
			}

			// Token: 0x06000617 RID: 1559 RVA: 0x00020F9C File Offset: 0x0001F19C
			private void SubChannelStopped(int id)
			{
				ProgressReporting.ProgressChannel.SubChannel subChannel;
				this._subChannels.TryRemove(id, out subChannel);
			}

			// Token: 0x06000618 RID: 1560 RVA: 0x00020FB8 File Offset: 0x0001F1B8
			private void SubChannelStarted(int id, ProgressReporting.ProgressChannel.SubChannel channel)
			{
				this._subChannels.GetOrAdd(id, channel);
			}

			// Token: 0x06000619 RID: 1561 RVA: 0x00020FD0 File Offset: 0x0001F1D0
			private ProgressReporting.ProgressEntry BuildJointEntry(ProgressReporting.ProgressEntry rootEntry)
			{
				if (this._maxSubId == 0 || this._subChannels.Count == 0)
				{
					return rootEntry;
				}
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				List<double?> list3 = new List<double?>();
				List<double?> list4 = new List<double?>();
				List<double?> list5 = new List<double?>();
				list.AddRange(rootEntry.Header.UnitNames);
				list2.AddRange(rootEntry.Header.MetricNames);
				list3.AddRange(rootEntry.Progress);
				list4.AddRange(rootEntry.ProgressLim);
				list5.AddRange(rootEntry.Metrics);
				foreach (ProgressReporting.ProgressChannel.SubChannel subChannel in from x in this._subChannels.Values.ToArray<ProgressReporting.ProgressChannel.SubChannel>()
					orderby x.Level
					select x)
				{
					ProgressReporting.ProgressEntry progress = subChannel.GetProgress();
					list.AddRange(progress.Header.UnitNames);
					list2.AddRange(progress.Header.MetricNames);
					list3.AddRange(progress.Progress);
					list4.AddRange(progress.ProgressLim);
					list5.AddRange(progress.Metrics);
				}
				ProgressReporting.ProgressEntry progressEntry = new ProgressReporting.ProgressEntry(false, new ProgressHeader(list2.ToArray(), list.ToArray()));
				list3.CopyTo(progressEntry.Progress);
				list4.CopyTo(progressEntry.ProgressLim);
				list5.CopyTo(progressEntry.Metrics);
				return progressEntry;
			}

			// Token: 0x04000303 RID: 771
			private readonly IExceptionContext _ectx;

			// Token: 0x04000304 RID: 772
			private readonly ProgressReporting.ProgressTracker _tracker;

			// Token: 0x04000305 RID: 773
			private readonly string _name;

			// Token: 0x04000306 RID: 774
			private Tuple<ProgressHeader, Action<IProgressEntry>> _headerAndAction;

			// Token: 0x04000307 RID: 775
			private readonly ConcurrentDictionary<int, ProgressReporting.ProgressChannel.SubChannel> _subChannels;

			// Token: 0x04000308 RID: 776
			private volatile int _maxSubId;

			// Token: 0x04000309 RID: 777
			private bool _isDisposed;

			// Token: 0x0200012A RID: 298
			private sealed class SubChannel : IProgressChannel, IProgressChannelProvider, IDisposable
			{
				// Token: 0x1700007B RID: 123
				// (get) Token: 0x0600061B RID: 1563 RVA: 0x00021160 File Offset: 0x0001F360
				public int Level
				{
					get
					{
						return this._level;
					}
				}

				// Token: 0x0600061C RID: 1564 RVA: 0x00021168 File Offset: 0x0001F368
				public ProgressReporting.ProgressEntry GetProgress()
				{
					Tuple<ProgressHeader, Action<IProgressEntry>> headerAndAction = this._headerAndAction;
					Action<IProgressEntry> item = headerAndAction.Item2;
					ProgressReporting.ProgressEntry progressEntry = new ProgressReporting.ProgressEntry(false, headerAndAction.Item1);
					if (item != null)
					{
						item(progressEntry);
					}
					return progressEntry;
				}

				// Token: 0x0600061D RID: 1565 RVA: 0x0002119B File Offset: 0x0001F39B
				public SubChannel(ProgressReporting.ProgressChannel root, int id, int level)
				{
					this._root = root;
					this._id = id;
					this._level = level;
					this._headerAndAction = Tuple.Create<ProgressHeader, Action<IProgressEntry>>(new ProgressHeader(null), null);
					this.Start();
				}

				// Token: 0x0600061E RID: 1566 RVA: 0x000211D0 File Offset: 0x0001F3D0
				public IProgressChannel StartProgressChannel(string name)
				{
					return this._root.StartProgressChannel(this._level + 1);
				}

				// Token: 0x0600061F RID: 1567 RVA: 0x000211E5 File Offset: 0x0001F3E5
				public void Dispose()
				{
					this.Stop();
				}

				// Token: 0x06000620 RID: 1568 RVA: 0x000211ED File Offset: 0x0001F3ED
				public void SetHeader(ProgressHeader header, Action<IProgressEntry> fillAction)
				{
					this._headerAndAction = Tuple.Create<ProgressHeader, Action<IProgressEntry>>(header, fillAction);
				}

				// Token: 0x06000621 RID: 1569 RVA: 0x000211FC File Offset: 0x0001F3FC
				private void Start()
				{
					this._root.SubChannelStarted(this._id, this);
				}

				// Token: 0x06000622 RID: 1570 RVA: 0x00021210 File Offset: 0x0001F410
				private void Stop()
				{
					this._root.SubChannelStopped(this._id);
				}

				// Token: 0x06000623 RID: 1571 RVA: 0x00021223 File Offset: 0x0001F423
				public void Checkpoint(params double?[] values)
				{
				}

				// Token: 0x0400030B RID: 779
				private readonly ProgressReporting.ProgressChannel _root;

				// Token: 0x0400030C RID: 780
				private readonly int _id;

				// Token: 0x0400030D RID: 781
				private readonly int _level;

				// Token: 0x0400030E RID: 782
				private Tuple<ProgressHeader, Action<IProgressEntry>> _headerAndAction;
			}
		}

		// Token: 0x0200012B RID: 299
		public sealed class ProgressTracker
		{
			// Token: 0x06000624 RID: 1572 RVA: 0x00021228 File Offset: 0x0001F428
			public ProgressTracker(IExceptionContext ectx)
			{
				Contracts.CheckValue<IExceptionContext>(ectx, "ectx");
				this._ectx = ectx;
				this._lock = new object();
				this._pendingEvents = new ConcurrentQueue<ProgressReporting.ProgressEvent>();
				this._infos = new List<ProgressReporting.ProgressTracker.CalculationInfo>();
				this._namesUsed = new HashSet<string>();
			}

			// Token: 0x06000625 RID: 1573 RVA: 0x00021294 File Offset: 0x0001F494
			public void Log(ProgressReporting.ProgressChannel source, ProgressReporting.ProgressEvent.EventKind kind, ProgressReporting.ProgressEntry entry)
			{
				if (kind == ProgressReporting.ProgressEvent.EventKind.Start)
				{
					lock (this._lock)
					{
						int num = 1;
						string name = source.Name;
						string text = name;
						while (!this._namesUsed.Add(text))
						{
							num++;
							text = string.Format("{0} #{1}", name, num);
						}
						ProgressReporting.ProgressTracker.CalculationInfo calculationInfo = new ProgressReporting.ProgressTracker.CalculationInfo(++this._index, text, source);
						this._infos.Add(calculationInfo);
						this._pendingEvents.Enqueue(new ProgressReporting.ProgressEvent(calculationInfo.Index, calculationInfo.Name, calculationInfo.StartTime, ProgressReporting.ProgressEvent.EventKind.Start));
						return;
					}
				}
				ProgressReporting.ProgressTracker.CalculationInfo calculationInfo2;
				lock (this._lock)
				{
					calculationInfo2 = this._infos.FirstOrDefault((ProgressReporting.ProgressTracker.CalculationInfo x) => x.Channel == source);
					if (calculationInfo2 == null)
					{
						throw Contracts.Except(this._ectx, "Event sent after the calculation lifetime expired.");
					}
				}
				if (kind == ProgressReporting.ProgressEvent.EventKind.Stop)
				{
					calculationInfo2.IsFinished = true;
					this._pendingEvents.Enqueue(new ProgressReporting.ProgressEvent(calculationInfo2.Index, calculationInfo2.Name, calculationInfo2.StartTime, ProgressReporting.ProgressEvent.EventKind.Stop));
					return;
				}
				this._pendingEvents.Enqueue(new ProgressReporting.ProgressEvent(calculationInfo2.Index, calculationInfo2.Name, calculationInfo2.StartTime, entry));
			}

			// Token: 0x06000626 RID: 1574 RVA: 0x00021460 File Offset: 0x0001F660
			public List<ProgressReporting.ProgressEvent> GetAllProgress()
			{
				List<ProgressReporting.ProgressEvent> list = new List<ProgressReporting.ProgressEvent>();
				HashSet<int> seen = new HashSet<int>();
				ProgressReporting.ProgressEvent progressEvent;
				while (this._pendingEvents.TryDequeue(out progressEvent))
				{
					seen.Add(progressEvent.Index);
					list.Add(progressEvent);
				}
				ProgressReporting.ProgressTracker.CalculationInfo[] array;
				lock (this._lock)
				{
					array = this._infos.Where((ProgressReporting.ProgressTracker.CalculationInfo x) => !seen.Contains(x.Index)).ToArray<ProgressReporting.ProgressTracker.CalculationInfo>();
					this._infos.RemoveAll((ProgressReporting.ProgressTracker.CalculationInfo x) => x.IsFinished);
				}
				foreach (ProgressReporting.ProgressTracker.CalculationInfo calculationInfo in array)
				{
					if (!calculationInfo.IsFinished)
					{
						ProgressReporting.ProgressEntry progress = calculationInfo.Channel.GetProgress();
						list.Add(new ProgressReporting.ProgressEvent(calculationInfo.Index, calculationInfo.Name, calculationInfo.StartTime, progress));
					}
				}
				return list;
			}

			// Token: 0x0400030F RID: 783
			private readonly IExceptionContext _ectx;

			// Token: 0x04000310 RID: 784
			private readonly object _lock;

			// Token: 0x04000311 RID: 785
			private readonly ConcurrentQueue<ProgressReporting.ProgressEvent> _pendingEvents;

			// Token: 0x04000312 RID: 786
			private readonly List<ProgressReporting.ProgressTracker.CalculationInfo> _infos;

			// Token: 0x04000313 RID: 787
			private int _index;

			// Token: 0x04000314 RID: 788
			private readonly HashSet<string> _namesUsed;

			// Token: 0x0200012C RID: 300
			private sealed class CalculationInfo
			{
				// Token: 0x06000628 RID: 1576 RVA: 0x00021588 File Offset: 0x0001F788
				public CalculationInfo(int index, string name, ProgressReporting.ProgressChannel channel)
				{
					this.Index = index;
					this.Name = name;
					this.PendingCheckpoints = new ConcurrentQueue<KeyValuePair<DateTime, ProgressReporting.ProgressEntry>>();
					this.StartTime = DateTime.Now;
					this.Channel = channel;
				}

				// Token: 0x04000316 RID: 790
				public readonly int Index;

				// Token: 0x04000317 RID: 791
				public readonly string Name;

				// Token: 0x04000318 RID: 792
				public readonly DateTime StartTime;

				// Token: 0x04000319 RID: 793
				public readonly ProgressReporting.ProgressChannel Channel;

				// Token: 0x0400031A RID: 794
				public readonly ConcurrentQueue<KeyValuePair<DateTime, ProgressReporting.ProgressEntry>> PendingCheckpoints;

				// Token: 0x0400031B RID: 795
				public bool IsFinished;
			}
		}

		// Token: 0x0200012D RID: 301
		public sealed class ProgressEntry : IProgressEntry
		{
			// Token: 0x06000629 RID: 1577 RVA: 0x000215BC File Offset: 0x0001F7BC
			public void SetProgress(int index, double value)
			{
				Contracts.Check(0 <= index && index < this.Progress.Length);
				this.Progress[index] = new double?(value);
				this.ProgressLim[index] = null;
			}

			// Token: 0x0600062A RID: 1578 RVA: 0x0002160C File Offset: 0x0001F80C
			public void SetProgress(int index, double value, double lim)
			{
				Contracts.Check(0 <= index && index < this.Progress.Length);
				this.Progress[index] = new double?(value);
				this.ProgressLim[index] = (double.IsNaN(lim) ? null : new double?(lim));
			}

			// Token: 0x0600062B RID: 1579 RVA: 0x00021671 File Offset: 0x0001F871
			public void SetMetric(int index, double value)
			{
				Contracts.Check(0 <= index && index < this.Metrics.Length);
				this.Metrics[index] = new double?(value);
			}

			// Token: 0x0600062C RID: 1580 RVA: 0x000216A4 File Offset: 0x0001F8A4
			public ProgressEntry(bool isCheckpoint, ProgressHeader header)
			{
				Contracts.CheckValue<ProgressHeader>(header, "header");
				this.Header = header;
				this.IsCheckpoint = isCheckpoint;
				this.Progress = new double?[header.UnitNames.Length];
				this.ProgressLim = new double?[header.UnitNames.Length];
				this.Metrics = new double?[header.MetricNames.Length];
			}

			// Token: 0x0400031C RID: 796
			public readonly ProgressHeader Header;

			// Token: 0x0400031D RID: 797
			public readonly bool IsCheckpoint;

			// Token: 0x0400031E RID: 798
			public readonly double?[] Progress;

			// Token: 0x0400031F RID: 799
			public readonly double?[] ProgressLim;

			// Token: 0x04000320 RID: 800
			public readonly double?[] Metrics;
		}

		// Token: 0x0200012E RID: 302
		public sealed class ProgressEvent
		{
			// Token: 0x0600062D RID: 1581 RVA: 0x0002170C File Offset: 0x0001F90C
			public ProgressEvent(int index, string name, DateTime startTime, ProgressReporting.ProgressEntry entry)
			{
				Contracts.CheckParam(index >= 0, "index");
				Contracts.CheckNonEmpty(name, "name");
				Contracts.CheckValue<ProgressReporting.ProgressEntry>(entry, "entry");
				this.Index = index;
				this.Name = name;
				this.StartTime = startTime;
				this.EventTime = DateTime.Now;
				this.Kind = ProgressReporting.ProgressEvent.EventKind.Progress;
				this.ProgressEntry = entry;
			}

			// Token: 0x0600062E RID: 1582 RVA: 0x00021778 File Offset: 0x0001F978
			public ProgressEvent(int index, string name, DateTime startTime, ProgressReporting.ProgressEvent.EventKind kind)
			{
				Contracts.CheckParam(index >= 0, "index");
				Contracts.CheckNonEmpty(name, "name");
				this.Index = index;
				this.Name = name;
				this.StartTime = startTime;
				this.EventTime = DateTime.Now;
				this.Kind = kind;
				this.ProgressEntry = null;
			}

			// Token: 0x04000321 RID: 801
			public readonly int Index;

			// Token: 0x04000322 RID: 802
			public readonly string Name;

			// Token: 0x04000323 RID: 803
			public readonly DateTime StartTime;

			// Token: 0x04000324 RID: 804
			public readonly DateTime EventTime;

			// Token: 0x04000325 RID: 805
			public readonly ProgressReporting.ProgressEvent.EventKind Kind;

			// Token: 0x04000326 RID: 806
			public readonly ProgressReporting.ProgressEntry ProgressEntry;

			// Token: 0x0200012F RID: 303
			public enum EventKind
			{
				// Token: 0x04000328 RID: 808
				Start,
				// Token: 0x04000329 RID: 809
				Progress,
				// Token: 0x0400032A RID: 810
				Stop
			}
		}
	}
}
