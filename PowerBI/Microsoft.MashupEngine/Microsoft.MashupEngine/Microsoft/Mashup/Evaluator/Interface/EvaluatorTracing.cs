using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DD6 RID: 7638
	public static class EvaluatorTracing
	{
		// Token: 0x17002E8E RID: 11918
		// (get) Token: 0x0600BD3D RID: 48445 RVA: 0x00266478 File Offset: 0x00264678
		// (set) Token: 0x0600BD3E RID: 48446 RVA: 0x002664E8 File Offset: 0x002646E8
		public static ITracingService Service
		{
			get
			{
				object obj = EvaluatorTracing.syncRoot;
				ITracingService tracingService;
				lock (obj)
				{
					if (EvaluatorTracing.service == null)
					{
						if (EvaluatorTracing.serviceCtor != null)
						{
							EvaluatorTracing.service = EvaluatorTracing.serviceCtor(EvaluatorTracing.options);
						}
						else
						{
							EvaluatorTracing.service = new EvaluatorTracing.DefaultTracingService();
						}
					}
					tracingService = EvaluatorTracing.service;
				}
				return tracingService;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				object obj = EvaluatorTracing.syncRoot;
				lock (obj)
				{
					EvaluatorTracing.service = value;
					EvaluatorTracing.serviceCtor = null;
				}
			}
		}

		// Token: 0x17002E8F RID: 11919
		// (set) Token: 0x0600BD3F RID: 48447 RVA: 0x0026653C File Offset: 0x0026473C
		public static Func<string[], ITracingService> ServiceCtor
		{
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				object obj = EvaluatorTracing.syncRoot;
				lock (obj)
				{
					EvaluatorTracing.serviceCtor = value;
					EvaluatorTracing.service = null;
				}
			}
		}

		// Token: 0x17002E90 RID: 11920
		// (get) Token: 0x0600BD40 RID: 48448 RVA: 0x00266590 File Offset: 0x00264790
		// (set) Token: 0x0600BD41 RID: 48449 RVA: 0x002665D0 File Offset: 0x002647D0
		public static string[] Options
		{
			get
			{
				object obj = EvaluatorTracing.syncRoot;
				string[] array;
				lock (obj)
				{
					array = EvaluatorTracing.options;
				}
				return array;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				object obj = EvaluatorTracing.syncRoot;
				lock (obj)
				{
					if (!EvaluatorTracing.OptionsAreEqual(EvaluatorTracing.options, value))
					{
						EvaluatorTracing.options = value;
						EvaluatorTracing.service = null;
					}
				}
			}
		}

		// Token: 0x0600BD42 RID: 48450 RVA: 0x00266630 File Offset: 0x00264830
		public static IHostTrace CreateTrace(string entryName, IEngineHost engineHost, TraceEventType severityLevel = TraceEventType.Information, IResource resource = null)
		{
			return EvaluatorTracing.CreateTrace(entryName, engineHost.GetEvaluationConstants(), severityLevel, resource);
		}

		// Token: 0x0600BD43 RID: 48451 RVA: 0x00266640 File Offset: 0x00264840
		public static IHostTrace CreateTrace(string entryName, IEvaluationConstants evaluationConstants = null, TraceEventType severityLevel = TraceEventType.Information, IResource resource = null)
		{
			return EvaluatorTracing.Service.CreateTrace(evaluationConstants, entryName, severityLevel, false, resource);
		}

		// Token: 0x0600BD44 RID: 48452 RVA: 0x00266651 File Offset: 0x00264851
		public static IHostTrace CreatePerformanceTrace(string entryName, IEngineHost engineHost, TraceEventType severityLevel = TraceEventType.Information, IResource resource = null)
		{
			return EvaluatorTracing.CreatePerformanceTrace(entryName, engineHost.GetEvaluationConstants(), severityLevel, resource);
		}

		// Token: 0x0600BD45 RID: 48453 RVA: 0x00266661 File Offset: 0x00264861
		public static IHostTrace CreatePerformanceTrace(string entryName, IEvaluationConstants evaluationConstants = null, TraceEventType severityLevel = TraceEventType.Information, IResource resource = null)
		{
			return EvaluatorTracing.Service.CreateTrace(evaluationConstants, entryName, severityLevel, true, resource);
		}

		// Token: 0x0600BD46 RID: 48454 RVA: 0x00266674 File Offset: 0x00264874
		private static bool OptionsAreEqual(string[] x, string[] y)
		{
			if (x.Length != y.Length)
			{
				return false;
			}
			foreach (string text in y)
			{
				if (!x.Contains(text))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400608E RID: 24718
		private static readonly object syncRoot = new object();

		// Token: 0x0400608F RID: 24719
		private static string[] options = EmptyArray<string>.Instance;

		// Token: 0x04006090 RID: 24720
		private static ITracingService service;

		// Token: 0x04006091 RID: 24721
		private static Func<string[], ITracingService> serviceCtor;

		// Token: 0x02001DD7 RID: 7639
		private sealed class DefaultTracingService : ITracingService, ITracingOptions
		{
			// Token: 0x17002E91 RID: 11921
			// (get) Token: 0x0600BD48 RID: 48456 RVA: 0x000020FA File Offset: 0x000002FA
			public string TracePath
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002E92 RID: 11922
			// (get) Token: 0x0600BD49 RID: 48457 RVA: 0x00002105 File Offset: 0x00000305
			public SourceLevels Levels
			{
				get
				{
					return SourceLevels.Off;
				}
			}

			// Token: 0x17002E93 RID: 11923
			// (get) Token: 0x0600BD4A RID: 48458 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public ITracingOptions Options
			{
				get
				{
					return this;
				}
			}

			// Token: 0x0600BD4B RID: 48459 RVA: 0x002666C1 File Offset: 0x002648C1
			public IHostTrace CreateTrace(Guid? activityId, string correlationId, string entryName, TraceEventType severityLevel, bool forPerformance, IResource resource)
			{
				return EvaluatorTracing.DefaultTracingService.DefaultHostTrace.Instance;
			}

			// Token: 0x0600BD4C RID: 48460 RVA: 0x002666C1 File Offset: 0x002648C1
			public IHostTrace CreateUserTrace(Guid? activityId, string correlationId, string entryName, TraceEventType severityLevel)
			{
				return EvaluatorTracing.DefaultTracingService.DefaultHostTrace.Instance;
			}

			// Token: 0x0600BD4D RID: 48461 RVA: 0x0000336E File Offset: 0x0000156E
			public void Flush()
			{
			}

			// Token: 0x0600BD4E RID: 48462 RVA: 0x0000336E File Offset: 0x0000156E
			public void Close()
			{
			}

			// Token: 0x17002E94 RID: 11924
			// (get) Token: 0x0600BD4F RID: 48463 RVA: 0x00191195 File Offset: 0x0018F395
			public IEnumerable<string> Keys
			{
				get
				{
					return EmptyArray<string>.Instance;
				}
			}

			// Token: 0x0600BD50 RID: 48464 RVA: 0x00002105 File Offset: 0x00000305
			public bool IsEnabled(string key)
			{
				return false;
			}

			// Token: 0x02001DD8 RID: 7640
			private sealed class DefaultHostTrace : IHostTrace, IDisposable
			{
				// Token: 0x17002E95 RID: 11925
				// (get) Token: 0x0600BD52 RID: 48466 RVA: 0x00002105 File Offset: 0x00000305
				public SourceLevels Levels
				{
					get
					{
						return SourceLevels.Off;
					}
				}

				// Token: 0x0600BD53 RID: 48467 RVA: 0x0000336E File Offset: 0x0000156E
				public void Suspend()
				{
				}

				// Token: 0x0600BD54 RID: 48468 RVA: 0x0000336E File Offset: 0x0000156E
				public void Resume()
				{
				}

				// Token: 0x0600BD55 RID: 48469 RVA: 0x002666C8 File Offset: 0x002648C8
				public IHostTraceValue Begin(string name, bool isPii)
				{
					return new EvaluatorTracing.DefaultTracingService.DefaultHostTrace.DefaultHostTraceValue();
				}

				// Token: 0x0600BD56 RID: 48470 RVA: 0x0000336E File Offset: 0x0000156E
				public void Add(string name, object value, bool isPii)
				{
				}

				// Token: 0x0600BD57 RID: 48471 RVA: 0x0000336E File Offset: 0x0000156E
				public void Add(Exception e, bool hasPii = true)
				{
				}

				// Token: 0x0600BD58 RID: 48472 RVA: 0x0000336E File Offset: 0x0000156E
				public void Add(Exception e, TraceEventType type, bool hasPii = true)
				{
				}

				// Token: 0x0600BD59 RID: 48473 RVA: 0x0000336E File Offset: 0x0000156E
				public void Dispose()
				{
				}

				// Token: 0x04006092 RID: 24722
				public static readonly IHostTrace Instance = new EvaluatorTracing.DefaultTracingService.DefaultHostTrace();

				// Token: 0x02001DD9 RID: 7641
				private sealed class DefaultHostTraceValue : IHostTraceValue, IDisposable
				{
					// Token: 0x0600BD5C RID: 48476 RVA: 0x0000336E File Offset: 0x0000156E
					public void Add(object value)
					{
					}

					// Token: 0x0600BD5D RID: 48477 RVA: 0x0000336E File Offset: 0x0000156E
					public void Begin()
					{
					}

					// Token: 0x0600BD5E RID: 48478 RVA: 0x0000336E File Offset: 0x0000156E
					public void End()
					{
					}

					// Token: 0x0600BD5F RID: 48479 RVA: 0x0000336E File Offset: 0x0000156E
					public void Dispose()
					{
					}
				}
			}
		}
	}
}
