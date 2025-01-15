using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Tracing
{
	// Token: 0x020020B3 RID: 8371
	internal class Trace : IDisposable
	{
		// Token: 0x0600CCE7 RID: 52455 RVA: 0x0028BCB0 File Offset: 0x00289EB0
		public Trace(bool traceEnabled, TraceSource traceSource, string action, Guid? activityId, string correlationId, TraceEventType severityLevel = TraceEventType.Information, bool isUserTrace = false)
		{
			if (traceEnabled || traceSource.Switch.Level != SourceLevels.Off)
			{
				this.elapsed = TimeSpan.Zero;
				this.lastResume = HiResDateTime.UtcNow;
				this.depth = 1;
				this.piiReplacementCapacity = 0;
				this.writer = new JsWriter();
				this.writer.WriteRecordStart();
				this.AddStart(this.lastResume);
				this.activityId = activityId;
				this.correlationId = correlationId;
				this.AddAction(action);
				this.severityLevel = severityLevel;
				this.action = action;
				this.isUserTrace = isUserTrace;
				this.traceWriter = new TraceWriter(traceSource);
			}
		}

		// Token: 0x1700314C RID: 12620
		// (get) Token: 0x0600CCE8 RID: 52456 RVA: 0x0028BD56 File Offset: 0x00289F56
		public bool IsEnabled
		{
			get
			{
				return this.writer != null;
			}
		}

		// Token: 0x1700314D RID: 12621
		// (get) Token: 0x0600CCE9 RID: 52457 RVA: 0x0028BD61 File Offset: 0x00289F61
		public JsWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x1700314E RID: 12622
		// (get) Token: 0x0600CCEA RID: 52458 RVA: 0x0028BD69 File Offset: 0x00289F69
		// (set) Token: 0x0600CCEB RID: 52459 RVA: 0x0028BD71 File Offset: 0x00289F71
		public TraceEventType SeverityLevel
		{
			get
			{
				return this.severityLevel;
			}
			set
			{
				this.severityLevel = value;
			}
		}

		// Token: 0x1700314F RID: 12623
		// (get) Token: 0x0600CCEC RID: 52460 RVA: 0x0028BD7A File Offset: 0x00289F7A
		private EventID TraceEventID
		{
			get
			{
				if (!this.isUserTrace)
				{
					return EventID.TraceInformation;
				}
				return EventID.UserTrace;
			}
		}

		// Token: 0x0600CCED RID: 52461 RVA: 0x0028BD90 File Offset: 0x00289F90
		public void Suspend()
		{
			this.depth--;
			if (this.depth == 0 && this.lastResume != default(DateTime))
			{
				this.elapsed += HiResDateTime.UtcNow - this.lastResume;
				this.lastResume = default(DateTime);
			}
		}

		// Token: 0x0600CCEE RID: 52462 RVA: 0x0028BDF6 File Offset: 0x00289FF6
		public void Resume()
		{
			this.depth++;
			if (this.depth == 1)
			{
				this.lastResume = HiResDateTime.UtcNow;
			}
		}

		// Token: 0x0600CCEF RID: 52463 RVA: 0x0028BE1C File Offset: 0x0028A01C
		public void MarkPii(int offset, int length, string replaceWith)
		{
			if (this.isUserTrace)
			{
				return;
			}
			if (this.piiSpans == null)
			{
				this.piiSpans = new SortedList<int, Trace.PiiRemoval>();
			}
			this.piiSpans.Add(offset, new Trace.PiiRemoval(length, replaceWith));
			this.piiReplacementCapacity += replaceWith.Length - length;
			int num = this.piiSpans.IndexOfKey(offset);
			if (num > 0)
			{
				int num2 = this.piiSpans.Keys[num - 1];
				int length2 = this.piiSpans.Values[num - 1].Length;
			}
			if (num + 1 < this.piiSpans.Count)
			{
				int num3 = this.piiSpans.Keys[num + 1];
			}
		}

		// Token: 0x0600CCF0 RID: 52464 RVA: 0x0028BED4 File Offset: 0x0028A0D4
		public void Dispose()
		{
			if (this.IsEnabled)
			{
				try
				{
					Guid guid = this.activityId ?? Guid.Empty;
					string text = this.correlationId ?? string.Empty;
					this.AddProductVersion();
					this.AddActivityId(guid);
					this.AddCorrelationId(text);
					this.AddProcessName();
					this.AddProcessId();
					this.AddThreadId();
					this.Suspend();
					this.AddDuration(this.elapsed);
					this.writer.WriteRecordEnd();
					string text2 = this.writer.ToString();
					this.traceWriter.TraceEvent(this.action, text2, this.severityLevel, guid, text, this.TraceEventID);
					if (!this.isUserTrace)
					{
						this.traceWriter.TraceEvent(this.action, Trace.BuildPiiFreeMessage(text2, this.piiSpans, 0), this.severityLevel, guid, text, EventID.TraceWithoutPii);
					}
				}
				catch (Exception ex)
				{
					if (!SafeExceptions.IsSafeException(ex))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x0600CCF1 RID: 52465 RVA: 0x0028BFDC File Offset: 0x0028A1DC
		private static string BuildPiiFreeMessage(string source, SortedList<int, Trace.PiiRemoval> piiSpans, int extraCapacity = 0)
		{
			if (piiSpans == null || piiSpans.Count == 0)
			{
				return source;
			}
			StringBuilder stringBuilder = new StringBuilder(source.Length + extraCapacity);
			int num = 0;
			foreach (KeyValuePair<int, Trace.PiiRemoval> keyValuePair in piiSpans)
			{
				int key = keyValuePair.Key;
				stringBuilder.Append(source, num, key - num);
				stringBuilder.Append(keyValuePair.Value.ReplaceWith);
				num = key + keyValuePair.Value.Length;
			}
			stringBuilder.Append(source, num, source.Length - num);
			return stringBuilder.ToString();
		}

		// Token: 0x040067B9 RID: 26553
		public const string HiddenString = "[Hidden]";

		// Token: 0x040067BA RID: 26554
		private readonly TraceWriter traceWriter;

		// Token: 0x040067BB RID: 26555
		private readonly JsWriter writer;

		// Token: 0x040067BC RID: 26556
		private readonly Guid? activityId;

		// Token: 0x040067BD RID: 26557
		private readonly string correlationId;

		// Token: 0x040067BE RID: 26558
		private readonly bool isUserTrace;

		// Token: 0x040067BF RID: 26559
		private TimeSpan elapsed;

		// Token: 0x040067C0 RID: 26560
		private DateTime lastResume;

		// Token: 0x040067C1 RID: 26561
		private TraceEventType severityLevel;

		// Token: 0x040067C2 RID: 26562
		private string action;

		// Token: 0x040067C3 RID: 26563
		private int depth;

		// Token: 0x040067C4 RID: 26564
		private SortedList<int, Trace.PiiRemoval> piiSpans;

		// Token: 0x040067C5 RID: 26565
		private int piiReplacementCapacity;

		// Token: 0x020020B4 RID: 8372
		private struct PiiRemoval
		{
			// Token: 0x0600CCF2 RID: 52466 RVA: 0x0028C094 File Offset: 0x0028A294
			public PiiRemoval(int length, string replaceWith)
			{
				this.length = length;
				this.replaceWith = replaceWith;
			}

			// Token: 0x17003150 RID: 12624
			// (get) Token: 0x0600CCF3 RID: 52467 RVA: 0x0028C0A4 File Offset: 0x0028A2A4
			public int Length
			{
				get
				{
					return this.length;
				}
			}

			// Token: 0x17003151 RID: 12625
			// (get) Token: 0x0600CCF4 RID: 52468 RVA: 0x0028C0AC File Offset: 0x0028A2AC
			public string ReplaceWith
			{
				get
				{
					return this.replaceWith;
				}
			}

			// Token: 0x040067C6 RID: 26566
			private readonly int length;

			// Token: 0x040067C7 RID: 26567
			private readonly string replaceWith;
		}
	}
}
