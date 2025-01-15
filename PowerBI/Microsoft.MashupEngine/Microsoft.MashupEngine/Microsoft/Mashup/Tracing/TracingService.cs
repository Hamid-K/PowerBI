using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Tracing
{
	// Token: 0x020020BC RID: 8380
	public sealed class TracingService : ITracingService, ITracingOptions
	{
		// Token: 0x0600CD3D RID: 52541 RVA: 0x0028D138 File Offset: 0x0028B338
		private TracingService(params string[] options)
		{
			this.options = new HashSet<string>(options);
		}

		// Token: 0x0600CD3E RID: 52542 RVA: 0x0028D15C File Offset: 0x0028B35C
		public static ITracingService New(string[] options)
		{
			if (options.Length != 0)
			{
				return new TracingService(options);
			}
			return TracingService.Instance;
		}

		// Token: 0x17003156 RID: 12630
		// (get) Token: 0x0600CD3F RID: 52543 RVA: 0x000020FA File Offset: 0x000002FA
		public string TracePath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17003157 RID: 12631
		// (get) Token: 0x0600CD40 RID: 52544 RVA: 0x0028D17C File Offset: 0x0028B37C
		public SourceLevels Levels
		{
			get
			{
				if (TracingService.EtwEnabled && this.traceSource.Switch.Level != SourceLevels.All && this.traceSource.Switch.Level < SourceLevels.Information)
				{
					return SourceLevels.Information;
				}
				return this.traceSource.Switch.Level;
			}
		}

		// Token: 0x17003158 RID: 12632
		// (get) Token: 0x0600CD41 RID: 52545 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public ITracingOptions Options
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600CD42 RID: 52546 RVA: 0x0028D1CA File Offset: 0x0028B3CA
		public IHostTrace CreateTrace(Guid? activityId, string correlationId, string entryName, TraceEventType severityLevel, bool forPerformance, IResource resource)
		{
			return this.CreateTrace(activityId, correlationId, entryName, severityLevel, false, forPerformance, resource);
		}

		// Token: 0x0600CD43 RID: 52547 RVA: 0x0028D1DC File Offset: 0x0028B3DC
		public IHostTrace CreateUserTrace(Guid? activityId, string correlationId, string entryName, TraceEventType severityLevel)
		{
			return this.CreateTrace(activityId, correlationId, entryName, severityLevel, true, false, null);
		}

		// Token: 0x0600CD44 RID: 52548 RVA: 0x0028D1EC File Offset: 0x0028B3EC
		public IHostTrace CreateTrace(Guid? activityId, string correlationId, string entryName, TraceEventType severityLevel, bool isUserTrace = false, bool forPerformance = false, IResource resource = null)
		{
			Trace trace = new Trace(TracingService.EtwEnabled || forPerformance, this.traceSource, entryName, activityId, correlationId, severityLevel, isUserTrace);
			IHostTrace hostTrace = new TracingService.HostTrace(this.Levels, trace);
			if (resource != null && trace.IsEnabled)
			{
				hostTrace.Add("ResourceKind", resource.Kind, false);
				hostTrace.Add("ResourcePath", resource.Path, true);
			}
			return hostTrace;
		}

		// Token: 0x17003159 RID: 12633
		// (get) Token: 0x0600CD45 RID: 52549 RVA: 0x0028D254 File Offset: 0x0028B454
		public TraceSource TraceSource
		{
			get
			{
				return this.traceSource;
			}
		}

		// Token: 0x0600CD46 RID: 52550 RVA: 0x0028D25C File Offset: 0x0028B45C
		public void Flush()
		{
			this.traceSource.Flush();
		}

		// Token: 0x0600CD47 RID: 52551 RVA: 0x0028D269 File Offset: 0x0028B469
		public void Close()
		{
			this.traceSource.Close();
		}

		// Token: 0x1700315A RID: 12634
		// (get) Token: 0x0600CD48 RID: 52552 RVA: 0x0028D276 File Offset: 0x0028B476
		public IEnumerable<string> Keys
		{
			get
			{
				return this.options.ToArray<string>();
			}
		}

		// Token: 0x0600CD49 RID: 52553 RVA: 0x0028D283 File Offset: 0x0028B483
		public bool IsEnabled(string key)
		{
			return this.options.Contains(key);
		}

		// Token: 0x1700315B RID: 12635
		// (get) Token: 0x0600CD4A RID: 52554 RVA: 0x0028D291 File Offset: 0x0028B491
		private static bool EtwEnabled
		{
			get
			{
				return EtwEventSource.Log.IsEnabled();
			}
		}

		// Token: 0x040067D7 RID: 26583
		public static ITracingService Instance = new TracingService(Array.Empty<string>());

		// Token: 0x040067D8 RID: 26584
		private readonly TraceSource traceSource = new TraceSource("DataMashup.Trace");

		// Token: 0x040067D9 RID: 26585
		private readonly HashSet<string> options;

		// Token: 0x020020BD RID: 8381
		private class HostTrace : IHostTrace, IDisposable
		{
			// Token: 0x0600CD4C RID: 52556 RVA: 0x0028D2AE File Offset: 0x0028B4AE
			public HostTrace(SourceLevels levels, Trace trace)
			{
				this.levels = levels;
				this.trace = trace;
			}

			// Token: 0x1700315C RID: 12636
			// (get) Token: 0x0600CD4D RID: 52557 RVA: 0x0028D2C4 File Offset: 0x0028B4C4
			public SourceLevels Levels
			{
				get
				{
					if (!this.trace.IsEnabled)
					{
						return SourceLevels.Off;
					}
					return this.levels;
				}
			}

			// Token: 0x0600CD4E RID: 52558 RVA: 0x0028D2DB File Offset: 0x0028B4DB
			public void Suspend()
			{
				this.trace.Suspend();
			}

			// Token: 0x0600CD4F RID: 52559 RVA: 0x0028D2E8 File Offset: 0x0028B4E8
			public void Resume()
			{
				this.trace.Resume();
			}

			// Token: 0x0600CD50 RID: 52560 RVA: 0x0028D2F5 File Offset: 0x0028B4F5
			public IHostTraceValue Begin(string name, bool isPii)
			{
				if (!this.trace.IsEnabled)
				{
					return TracingService.HostTrace.NoOpHostTraceValue.Instance;
				}
				return new TracingService.HostTrace.HostTraceValue(this.trace, name, isPii);
			}

			// Token: 0x0600CD51 RID: 52561 RVA: 0x0028D318 File Offset: 0x0028B518
			public void Add(string name, object value, bool isPii)
			{
				if (this.trace.IsEnabled)
				{
					using (IHostTraceValue hostTraceValue = this.Begin(name, isPii))
					{
						hostTraceValue.Add(value);
					}
				}
			}

			// Token: 0x0600CD52 RID: 52562 RVA: 0x0028D360 File Offset: 0x0028B560
			public void Add(Exception e, bool hasPii = true)
			{
				this.trace.AddException(e, TraceEventType.Error, hasPii);
			}

			// Token: 0x0600CD53 RID: 52563 RVA: 0x0028D370 File Offset: 0x0028B570
			public void Add(Exception e, TraceEventType type, bool hasPii = true)
			{
				this.trace.AddException(e, type, hasPii);
			}

			// Token: 0x0600CD54 RID: 52564 RVA: 0x0028D380 File Offset: 0x0028B580
			public void Dispose()
			{
				this.trace.Dispose();
			}

			// Token: 0x040067DA RID: 26586
			private readonly SourceLevels levels;

			// Token: 0x040067DB RID: 26587
			private readonly Trace trace;

			// Token: 0x020020BE RID: 8382
			private class HostTraceValue : IHostTraceValue, IDisposable
			{
				// Token: 0x0600CD55 RID: 52565 RVA: 0x0028D38D File Offset: 0x0028B58D
				public HostTraceValue(Trace trace, string name, bool isPii)
				{
					this.isPii = isPii;
					this.trace = trace;
					this.traceValue = new TraceValue2(trace, name);
					this.valueOffset = this.trace.Writer.Length;
				}

				// Token: 0x0600CD56 RID: 52566 RVA: 0x0028D3C6 File Offset: 0x0028B5C6
				public void Add(object value)
				{
					if (value == null)
					{
						this.traceValue.AddNull();
						return;
					}
					this.traceValue.Add(value.ToString());
				}

				// Token: 0x0600CD57 RID: 52567 RVA: 0x0028D3E8 File Offset: 0x0028B5E8
				public void Begin()
				{
					this.traceValue.Begin();
				}

				// Token: 0x0600CD58 RID: 52568 RVA: 0x0028D3F5 File Offset: 0x0028B5F5
				public void End()
				{
					this.traceValue.End();
				}

				// Token: 0x0600CD59 RID: 52569 RVA: 0x0028D404 File Offset: 0x0028B604
				public void Dispose()
				{
					if (this.isPii)
					{
						this.trace.MarkPii(this.valueOffset, this.trace.Writer.Length - this.valueOffset, "\"[Hidden]\"");
					}
					this.traceValue.Dispose();
				}

				// Token: 0x040067DC RID: 26588
				private const string HiddenString = "\"[Hidden]\"";

				// Token: 0x040067DD RID: 26589
				private readonly bool isPii;

				// Token: 0x040067DE RID: 26590
				private readonly int valueOffset;

				// Token: 0x040067DF RID: 26591
				private readonly Trace trace;

				// Token: 0x040067E0 RID: 26592
				private TraceValue2 traceValue;
			}

			// Token: 0x020020BF RID: 8383
			private class NoOpHostTraceValue : IHostTraceValue, IDisposable
			{
				// Token: 0x0600CD5A RID: 52570 RVA: 0x000020FD File Offset: 0x000002FD
				private NoOpHostTraceValue()
				{
				}

				// Token: 0x0600CD5B RID: 52571 RVA: 0x0000336E File Offset: 0x0000156E
				public void Add(object value)
				{
				}

				// Token: 0x0600CD5C RID: 52572 RVA: 0x0000336E File Offset: 0x0000156E
				public void Begin()
				{
				}

				// Token: 0x0600CD5D RID: 52573 RVA: 0x0000336E File Offset: 0x0000156E
				public void End()
				{
				}

				// Token: 0x0600CD5E RID: 52574 RVA: 0x0000336E File Offset: 0x0000156E
				public void Dispose()
				{
				}

				// Token: 0x040067E1 RID: 26593
				public static readonly IHostTraceValue Instance = new TracingService.HostTrace.NoOpHostTraceValue();
			}
		}
	}
}
