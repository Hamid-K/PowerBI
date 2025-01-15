using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x02000016 RID: 22
	[EventSource(Name = "Microsoft-Diagnostics-DiagnosticSource")]
	[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2113:ReflectionToRequiresUnreferencedCode", Justification = "In EventSource, EnsureDescriptorsInitialized's use of GetType preserves methods on Delegate and MulticastDelegate because the nested type OverrideEventProvider's base type EventProvider defines a delegate. This includes Delegate and MulticastDelegate methods which require unreferenced code, but EnsureDescriptorsInitialized does not access these members and is safe to call.")]
	[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2115:ReflectionToDynamicallyAccessedMembers", Justification = "In EventSource, EnsureDescriptorsInitialized's use of GetType preserves methods on Delegate and MulticastDelegate because the nested type OverrideEventProvider's base type EventProvider defines a delegate. This includes Delegate and MulticastDelegate methods which have dynamically accessed members requirements, but EnsureDescriptorsInitialized does not access these members and is safe to call.")]
	internal sealed class DiagnosticSourceEventSource : EventSource
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00002FB4 File Offset: 0x000011B4
		[Event(1, Keywords = (EventKeywords)1L)]
		public void Message(string Message)
		{
			base.WriteEvent(1, Message);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002FBE File Offset: 0x000011BE
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Arguments parameter is trimmer safe")]
		[Event(2, Keywords = (EventKeywords)2L)]
		private void Event(string SourceName, string EventName, IEnumerable<KeyValuePair<string, string>> Arguments)
		{
			base.WriteEvent(2, new object[] { SourceName, EventName, Arguments });
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002FD9 File Offset: 0x000011D9
		[Event(3, Keywords = (EventKeywords)2L)]
		private void EventJson(string SourceName, string EventName, string ArgmentsJson)
		{
			base.WriteEvent(3, SourceName, EventName, ArgmentsJson);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002FE5 File Offset: 0x000011E5
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Arguments parameter is trimmer safe")]
		[Event(4, Keywords = (EventKeywords)2L)]
		private void Activity1Start(string SourceName, string EventName, IEnumerable<KeyValuePair<string, string>> Arguments)
		{
			base.WriteEvent(4, new object[] { SourceName, EventName, Arguments });
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003000 File Offset: 0x00001200
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Arguments parameter is trimmer safe")]
		[Event(5, Keywords = (EventKeywords)2L)]
		private void Activity1Stop(string SourceName, string EventName, IEnumerable<KeyValuePair<string, string>> Arguments)
		{
			base.WriteEvent(5, new object[] { SourceName, EventName, Arguments });
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000301B File Offset: 0x0000121B
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Arguments parameter is trimmer safe")]
		[Event(6, Keywords = (EventKeywords)2L)]
		private void Activity2Start(string SourceName, string EventName, IEnumerable<KeyValuePair<string, string>> Arguments)
		{
			base.WriteEvent(6, new object[] { SourceName, EventName, Arguments });
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003036 File Offset: 0x00001236
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Arguments parameter is trimmer safe")]
		[Event(7, Keywords = (EventKeywords)2L)]
		private void Activity2Stop(string SourceName, string EventName, IEnumerable<KeyValuePair<string, string>> Arguments)
		{
			base.WriteEvent(7, new object[] { SourceName, EventName, Arguments });
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003051 File Offset: 0x00001251
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Arguments parameter is trimmer safe")]
		[Event(8, Keywords = (EventKeywords)2L, ActivityOptions = EventActivityOptions.Recursive)]
		private void RecursiveActivity1Start(string SourceName, string EventName, IEnumerable<KeyValuePair<string, string>> Arguments)
		{
			base.WriteEvent(8, new object[] { SourceName, EventName, Arguments });
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000306C File Offset: 0x0000126C
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Arguments parameter is trimmer safe")]
		[Event(9, Keywords = (EventKeywords)2L, ActivityOptions = EventActivityOptions.Recursive)]
		private void RecursiveActivity1Stop(string SourceName, string EventName, IEnumerable<KeyValuePair<string, string>> Arguments)
		{
			base.WriteEvent(9, new object[] { SourceName, EventName, Arguments });
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003088 File Offset: 0x00001288
		[Event(10, Keywords = (EventKeywords)2L)]
		private void NewDiagnosticListener(string SourceName)
		{
			base.WriteEvent(10, SourceName);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003093 File Offset: 0x00001293
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Arguments parameter is trimmer safe")]
		[Event(11, Keywords = (EventKeywords)2L, ActivityOptions = EventActivityOptions.Recursive)]
		private void ActivityStart(string SourceName, string ActivityName, IEnumerable<KeyValuePair<string, string>> Arguments)
		{
			base.WriteEvent(11, new object[] { SourceName, ActivityName, Arguments });
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000030AF File Offset: 0x000012AF
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Arguments parameter is trimmer safe")]
		[Event(12, Keywords = (EventKeywords)2L, ActivityOptions = EventActivityOptions.Recursive)]
		private void ActivityStop(string SourceName, string ActivityName, IEnumerable<KeyValuePair<string, string>> Arguments)
		{
			base.WriteEvent(12, new object[] { SourceName, ActivityName, Arguments });
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000030CB File Offset: 0x000012CB
		private DiagnosticSourceEventSource()
			: base(EventSourceSettings.EtwSelfDescribingEventFormat)
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000030EC File Offset: 0x000012EC
		[NonEvent]
		protected override void OnEventCommand(EventCommandEventArgs command)
		{
			this.BreakPointWithDebuggerFuncEval();
			lock (this)
			{
				if ((command.Command == EventCommand.Update || command.Command == EventCommand.Enable) && base.IsEnabled(EventLevel.Informational, (EventKeywords)2L))
				{
					string text = null;
					command.Arguments.TryGetValue("FilterAndPayloadSpecs", out text);
					if (!base.IsEnabled(EventLevel.Informational, (EventKeywords)2048L))
					{
						if (base.IsEnabled(EventLevel.Informational, (EventKeywords)4096L))
						{
							text = DiagnosticSourceEventSource.NewLineSeparate(text, this.AspNetCoreHostingKeywordValue);
						}
						if (base.IsEnabled(EventLevel.Informational, (EventKeywords)8192L))
						{
							text = DiagnosticSourceEventSource.NewLineSeparate(text, this.EntityFrameworkCoreCommandsKeywordValue);
						}
					}
					DiagnosticSourceEventSource.FilterAndTransform.CreateFilterAndTransformList(ref this._specs, text, this);
				}
				else if (command.Command == EventCommand.Update || command.Command == EventCommand.Disable)
				{
					DiagnosticSourceEventSource.FilterAndTransform.DestroyFilterAndTransformList(ref this._specs, this);
				}
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000031D0 File Offset: 0x000013D0
		private static string NewLineSeparate(string str1, string str2)
		{
			if (string.IsNullOrEmpty(str1))
			{
				return str2;
			}
			return str1 + "\n" + str2;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000031E8 File Offset: 0x000013E8
		[NonEvent]
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private void BreakPointWithDebuggerFuncEval()
		{
			new object();
			while (this._false)
			{
				this._false = false;
			}
		}

		// Token: 0x04000016 RID: 22
		public static DiagnosticSourceEventSource Log = new DiagnosticSourceEventSource();

		// Token: 0x04000017 RID: 23
		private readonly string AspNetCoreHostingKeywordValue = "Microsoft.AspNetCore/Microsoft.AspNetCore.Hosting.BeginRequest@Activity1Start:-httpContext.Request.Method;httpContext.Request.Host;httpContext.Request.Path;httpContext.Request.QueryString\nMicrosoft.AspNetCore/Microsoft.AspNetCore.Hosting.EndRequest@Activity1Stop:-httpContext.TraceIdentifier;httpContext.Response.StatusCode";

		// Token: 0x04000018 RID: 24
		private readonly string EntityFrameworkCoreCommandsKeywordValue = "Microsoft.EntityFrameworkCore/Microsoft.EntityFrameworkCore.BeforeExecuteCommand@Activity2Start:-Command.Connection.DataSource;Command.Connection.Database;Command.CommandText\nMicrosoft.EntityFrameworkCore/Microsoft.EntityFrameworkCore.AfterExecuteCommand@Activity2Stop:-";

		// Token: 0x04000019 RID: 25
		private volatile bool _false;

		// Token: 0x0400001A RID: 26
		private DiagnosticSourceEventSource.FilterAndTransform _specs;

		// Token: 0x0400001B RID: 27
		private DiagnosticSourceEventSource.FilterAndTransform _activitySourceSpecs;

		// Token: 0x0400001C RID: 28
		private ActivityListener _activityListener;

		// Token: 0x02000073 RID: 115
		public static class Keywords
		{
			// Token: 0x0400015F RID: 351
			public const EventKeywords Messages = (EventKeywords)1L;

			// Token: 0x04000160 RID: 352
			public const EventKeywords Events = (EventKeywords)2L;

			// Token: 0x04000161 RID: 353
			public const EventKeywords IgnoreShortCutKeywords = (EventKeywords)2048L;

			// Token: 0x04000162 RID: 354
			public const EventKeywords AspNetCoreHosting = (EventKeywords)4096L;

			// Token: 0x04000163 RID: 355
			public const EventKeywords EntityFrameworkCoreCommands = (EventKeywords)8192L;
		}

		// Token: 0x02000074 RID: 116
		[Flags]
		internal enum ActivityEvents
		{
			// Token: 0x04000165 RID: 357
			None = 0,
			// Token: 0x04000166 RID: 358
			ActivityStart = 1,
			// Token: 0x04000167 RID: 359
			ActivityStop = 2,
			// Token: 0x04000168 RID: 360
			All = 3
		}

		// Token: 0x02000075 RID: 117
		internal sealed class FilterAndTransform
		{
			// Token: 0x060002FB RID: 763 RVA: 0x0000B650 File Offset: 0x00009850
			public static void CreateFilterAndTransformList(ref DiagnosticSourceEventSource.FilterAndTransform specList, string filterAndPayloadSpecs, DiagnosticSourceEventSource eventSource)
			{
				DiagnosticSourceEventSource.FilterAndTransform.DestroyFilterAndTransformList(ref specList, eventSource);
				if (filterAndPayloadSpecs == null)
				{
					filterAndPayloadSpecs = "";
				}
				int num = filterAndPayloadSpecs.Length;
				for (;;)
				{
					if (0 >= num || !char.IsWhiteSpace(filterAndPayloadSpecs[num - 1]))
					{
						int num2 = filterAndPayloadSpecs.LastIndexOf('\n', num - 1, num);
						int num3 = 0;
						if (0 <= num2)
						{
							num3 = num2 + 1;
						}
						while (num3 < num && char.IsWhiteSpace(filterAndPayloadSpecs[num3]))
						{
							num3++;
						}
						if (DiagnosticSourceEventSource.FilterAndTransform.IsActivitySourceEntry(filterAndPayloadSpecs, num3, num))
						{
							DiagnosticSourceEventSource.FilterAndTransform.AddNewActivitySourceTransform(filterAndPayloadSpecs, num3, num, eventSource);
						}
						else
						{
							specList = new DiagnosticSourceEventSource.FilterAndTransform(filterAndPayloadSpecs, num3, num, eventSource, specList);
						}
						num = num2;
						if (num < 0)
						{
							break;
						}
					}
					else
					{
						num--;
					}
				}
				if (eventSource._activitySourceSpecs != null)
				{
					DiagnosticSourceEventSource.FilterAndTransform.NormalizeActivitySourceSpecsList(eventSource);
					DiagnosticSourceEventSource.FilterAndTransform.CreateActivityListener(eventSource);
				}
			}

			// Token: 0x060002FC RID: 764 RVA: 0x0000B6FC File Offset: 0x000098FC
			public static void DestroyFilterAndTransformList(ref DiagnosticSourceEventSource.FilterAndTransform specList, DiagnosticSourceEventSource eventSource)
			{
				ActivityListener activityListener = eventSource._activityListener;
				if (activityListener != null)
				{
					activityListener.Dispose();
				}
				eventSource._activityListener = null;
				eventSource._activitySourceSpecs = null;
				DiagnosticSourceEventSource.FilterAndTransform filterAndTransform = specList;
				specList = null;
				while (filterAndTransform != null)
				{
					filterAndTransform.Dispose();
					filterAndTransform = filterAndTransform.Next;
				}
			}

			// Token: 0x060002FD RID: 765 RVA: 0x0000B740 File Offset: 0x00009940
			public FilterAndTransform(string filterAndPayloadSpec, int startIdx, int endIdx, DiagnosticSourceEventSource eventSource, DiagnosticSourceEventSource.FilterAndTransform next)
			{
				DiagnosticSourceEventSource.FilterAndTransform.<>c__DisplayClass2_0 CS$<>8__locals1 = new DiagnosticSourceEventSource.FilterAndTransform.<>c__DisplayClass2_0();
				base..ctor();
				CS$<>8__locals1.<>4__this = this;
				this.Next = next;
				this._eventSource = eventSource;
				CS$<>8__locals1.listenerNameFilter = null;
				CS$<>8__locals1.eventNameFilter = null;
				string text = null;
				int num = startIdx;
				int num2 = endIdx;
				int num3 = filterAndPayloadSpec.IndexOf(':', startIdx, endIdx - startIdx);
				if (0 <= num3)
				{
					num2 = num3;
					num = num3 + 1;
				}
				int num4 = filterAndPayloadSpec.IndexOf('/', startIdx, num2 - startIdx);
				if (0 <= num4)
				{
					CS$<>8__locals1.listenerNameFilter = filterAndPayloadSpec.Substring(startIdx, num4 - startIdx);
					int num5 = filterAndPayloadSpec.IndexOf('@', num4 + 1, num2 - num4 - 1);
					if (0 <= num5)
					{
						text = filterAndPayloadSpec.Substring(num5 + 1, num2 - num5 - 1);
						CS$<>8__locals1.eventNameFilter = filterAndPayloadSpec.Substring(num4 + 1, num5 - num4 - 1);
					}
					else
					{
						CS$<>8__locals1.eventNameFilter = filterAndPayloadSpec.Substring(num4 + 1, num2 - num4 - 1);
					}
				}
				else if (startIdx < num2)
				{
					CS$<>8__locals1.listenerNameFilter = filterAndPayloadSpec.Substring(startIdx, num2 - startIdx);
				}
				this._eventSource.Message(string.Concat(new string[]
				{
					"DiagnosticSource: Enabling '",
					CS$<>8__locals1.listenerNameFilter ?? "*",
					"/",
					CS$<>8__locals1.eventNameFilter ?? "*",
					"'"
				}));
				if (num < endIdx && filterAndPayloadSpec[num] == '-')
				{
					this._eventSource.Message("DiagnosticSource: suppressing implicit transforms.");
					this._noImplicitTransforms = true;
					num++;
				}
				if (num < endIdx)
				{
					for (;;)
					{
						int num6 = num;
						int num7 = filterAndPayloadSpec.LastIndexOf(';', endIdx - 1, endIdx - num);
						if (0 <= num7)
						{
							num6 = num7 + 1;
						}
						if (num6 < endIdx)
						{
							if (this._eventSource.IsEnabled(EventLevel.Informational, (EventKeywords)1L))
							{
								this._eventSource.Message("DiagnosticSource: Parsing Explicit Transform '" + filterAndPayloadSpec.Substring(num6, endIdx - num6) + "'");
							}
							this._explicitTransforms = new DiagnosticSourceEventSource.TransformSpec(filterAndPayloadSpec, num6, endIdx, this._explicitTransforms);
						}
						if (num == num6)
						{
							break;
						}
						endIdx = num7;
					}
				}
				CS$<>8__locals1.writeEvent = null;
				if (text != null && text.Contains("Activity"))
				{
					Action<string, string, IEnumerable<KeyValuePair<string, string>>> action;
					if (!(text == "Activity1Start"))
					{
						if (!(text == "Activity1Stop"))
						{
							if (!(text == "Activity2Start"))
							{
								if (!(text == "Activity2Stop"))
								{
									if (!(text == "RecursiveActivity1Start"))
									{
										if (!(text == "RecursiveActivity1Stop"))
										{
											action = null;
										}
										else
										{
											action = new Action<string, string, IEnumerable<KeyValuePair<string, string>>>(this._eventSource.RecursiveActivity1Stop);
										}
									}
									else
									{
										action = new Action<string, string, IEnumerable<KeyValuePair<string, string>>>(this._eventSource.RecursiveActivity1Start);
									}
								}
								else
								{
									action = new Action<string, string, IEnumerable<KeyValuePair<string, string>>>(this._eventSource.Activity2Stop);
								}
							}
							else
							{
								action = new Action<string, string, IEnumerable<KeyValuePair<string, string>>>(this._eventSource.Activity2Start);
							}
						}
						else
						{
							action = new Action<string, string, IEnumerable<KeyValuePair<string, string>>>(this._eventSource.Activity1Stop);
						}
					}
					else
					{
						action = new Action<string, string, IEnumerable<KeyValuePair<string, string>>>(this._eventSource.Activity1Start);
					}
					CS$<>8__locals1.writeEvent = action;
					if (CS$<>8__locals1.writeEvent == null)
					{
						this._eventSource.Message("DiagnosticSource: Could not find Event to log Activity " + text);
					}
				}
				if (CS$<>8__locals1.writeEvent == null)
				{
					CS$<>8__locals1.writeEvent = new Action<string, string, IEnumerable<KeyValuePair<string, string>>>(this._eventSource.Event);
				}
				this._diagnosticsListenersSubscription = DiagnosticListener.AllListeners.Subscribe(new DiagnosticSourceEventSource.CallbackObserver<DiagnosticListener>(delegate(DiagnosticListener newListener)
				{
					DiagnosticSourceEventSource.FilterAndTransform.<>c__DisplayClass2_1 CS$<>8__locals2 = new DiagnosticSourceEventSource.FilterAndTransform.<>c__DisplayClass2_1();
					CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
					CS$<>8__locals2.newListener = newListener;
					if (CS$<>8__locals1.listenerNameFilter == null || CS$<>8__locals1.listenerNameFilter == CS$<>8__locals2.newListener.Name)
					{
						CS$<>8__locals1.<>4__this._eventSource.NewDiagnosticListener(CS$<>8__locals2.newListener.Name);
						Predicate<string> predicate = null;
						if (CS$<>8__locals1.eventNameFilter != null)
						{
							predicate = (string eventName) => CS$<>8__locals1.eventNameFilter == eventName;
						}
						IDisposable disposable = CS$<>8__locals2.newListener.Subscribe(new DiagnosticSourceEventSource.CallbackObserver<KeyValuePair<string, object>>(new Action<KeyValuePair<string, object>>(CS$<>8__locals2.<.ctor>g__OnEventWritten|2)), predicate);
						CS$<>8__locals1.<>4__this._liveSubscriptions = new DiagnosticSourceEventSource.Subscriptions(disposable, CS$<>8__locals1.<>4__this._liveSubscriptions);
					}
				}));
			}

			// Token: 0x060002FE RID: 766 RVA: 0x0000BA80 File Offset: 0x00009C80
			internal FilterAndTransform(string filterAndPayloadSpec, int endIdx, int colonIdx, string activitySourceName, string activityName, DiagnosticSourceEventSource.ActivityEvents events, ActivitySamplingResult samplingResult, DiagnosticSourceEventSource eventSource)
			{
				this._eventSource = eventSource;
				this.Next = this._eventSource._activitySourceSpecs;
				this._eventSource._activitySourceSpecs = this;
				this.SourceName = activitySourceName;
				this.ActivityName = activityName;
				this.Events = events;
				this.SamplingResult = samplingResult;
				if (colonIdx >= 0)
				{
					int num = colonIdx + 1;
					if (num < endIdx && filterAndPayloadSpec[num] == '-')
					{
						this._eventSource.Message("DiagnosticSource: suppressing implicit transforms.");
						this._noImplicitTransforms = true;
						num++;
					}
					if (num < endIdx)
					{
						for (;;)
						{
							int num2 = num;
							int num3 = filterAndPayloadSpec.LastIndexOf(';', endIdx - 1, endIdx - num);
							if (0 <= num3)
							{
								num2 = num3 + 1;
							}
							if (num2 < endIdx)
							{
								if (this._eventSource.IsEnabled(EventLevel.Informational, (EventKeywords)1L))
								{
									this._eventSource.Message("DiagnosticSource: Parsing Explicit Transform '" + filterAndPayloadSpec.Substring(num2, endIdx - num2) + "'");
								}
								this._explicitTransforms = new DiagnosticSourceEventSource.TransformSpec(filterAndPayloadSpec, num2, endIdx, this._explicitTransforms);
							}
							if (num == num2)
							{
								break;
							}
							endIdx = num3;
						}
					}
				}
			}

			// Token: 0x060002FF RID: 767 RVA: 0x0000BB7F File Offset: 0x00009D7F
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal static bool IsActivitySourceEntry(string filterAndPayloadSpec, int startIdx, int endIdx)
			{
				return MemoryExtensions.StartsWith(MemoryExtensions.AsSpan(filterAndPayloadSpec, startIdx, endIdx - startIdx), MemoryExtensions.AsSpan("[AS]"), StringComparison.Ordinal);
			}

			// Token: 0x06000300 RID: 768 RVA: 0x0000BB9C File Offset: 0x00009D9C
			internal static void AddNewActivitySourceTransform(string filterAndPayloadSpec, int startIdx, int endIdx, DiagnosticSourceEventSource eventSource)
			{
				DiagnosticSourceEventSource.ActivityEvents activityEvents = DiagnosticSourceEventSource.ActivityEvents.All;
				ActivitySamplingResult activitySamplingResult = ActivitySamplingResult.AllDataAndRecorded;
				int num = filterAndPayloadSpec.IndexOf(':', startIdx + "[AS]".Length, endIdx - startIdx - "[AS]".Length);
				ReadOnlySpan<char> readOnlySpan = MemoryExtensions.Trim(MemoryExtensions.AsSpan(filterAndPayloadSpec, startIdx + "[AS]".Length, ((num >= 0) ? num : endIdx) - startIdx - "[AS]".Length));
				int num2 = MemoryExtensions.IndexOf<char>(readOnlySpan, '/');
				ReadOnlySpan<char> readOnlySpan2;
				if (num2 >= 0)
				{
					readOnlySpan2 = MemoryExtensions.Trim(readOnlySpan.Slice(0, num2));
					ReadOnlySpan<char> readOnlySpan3 = MemoryExtensions.Trim(readOnlySpan.Slice(num2 + 1, readOnlySpan.Length - num2 - 1));
					int num3 = MemoryExtensions.IndexOf<char>(readOnlySpan3, '-');
					ReadOnlySpan<char> readOnlySpan4;
					if (num3 >= 0)
					{
						readOnlySpan4 = MemoryExtensions.Trim(readOnlySpan3.Slice(0, num3));
						readOnlySpan3 = MemoryExtensions.Trim(readOnlySpan3.Slice(num3 + 1, readOnlySpan3.Length - num3 - 1));
						if (readOnlySpan3.Length > 0)
						{
							if (MemoryExtensions.Equals(readOnlySpan3, MemoryExtensions.AsSpan("Propagate"), StringComparison.OrdinalIgnoreCase))
							{
								activitySamplingResult = ActivitySamplingResult.PropagationData;
							}
							else
							{
								if (!MemoryExtensions.Equals(readOnlySpan3, MemoryExtensions.AsSpan("Record"), StringComparison.OrdinalIgnoreCase))
								{
									return;
								}
								activitySamplingResult = ActivitySamplingResult.AllData;
							}
						}
					}
					else
					{
						readOnlySpan4 = readOnlySpan3;
					}
					if (readOnlySpan4.Length > 0)
					{
						if (MemoryExtensions.Equals(readOnlySpan4, MemoryExtensions.AsSpan("Start"), StringComparison.OrdinalIgnoreCase))
						{
							activityEvents = DiagnosticSourceEventSource.ActivityEvents.ActivityStart;
						}
						else
						{
							if (!MemoryExtensions.Equals(readOnlySpan4, MemoryExtensions.AsSpan("Stop"), StringComparison.OrdinalIgnoreCase))
							{
								return;
							}
							activityEvents = DiagnosticSourceEventSource.ActivityEvents.ActivityStop;
						}
					}
				}
				else
				{
					readOnlySpan2 = readOnlySpan;
				}
				string text = null;
				int num4 = MemoryExtensions.IndexOf<char>(readOnlySpan2, '+');
				if (num4 >= 0)
				{
					text = MemoryExtensions.Trim(readOnlySpan2.Slice(num4 + 1)).ToString();
					readOnlySpan2 = MemoryExtensions.Trim(readOnlySpan2.Slice(0, num4));
				}
				DiagnosticSourceEventSource.FilterAndTransform filterAndTransform = new DiagnosticSourceEventSource.FilterAndTransform(filterAndPayloadSpec, endIdx, num, readOnlySpan2.ToString(), text, activityEvents, activitySamplingResult, eventSource);
			}

			// Token: 0x06000301 RID: 769 RVA: 0x0000BD5C File Offset: 0x00009F5C
			private static ActivitySamplingResult Sample(string activitySourceName, string activityName, DiagnosticSourceEventSource eventSource)
			{
				DiagnosticSourceEventSource.FilterAndTransform filterAndTransform = eventSource._activitySourceSpecs;
				ActivitySamplingResult activitySamplingResult = ActivitySamplingResult.None;
				ActivitySamplingResult activitySamplingResult2 = ActivitySamplingResult.None;
				while (filterAndTransform != null)
				{
					if (filterAndTransform.ActivityName == null || filterAndTransform.ActivityName == activityName)
					{
						if (activitySourceName == filterAndTransform.SourceName)
						{
							if (filterAndTransform.SamplingResult > activitySamplingResult)
							{
								activitySamplingResult = filterAndTransform.SamplingResult;
							}
							if (activitySamplingResult >= ActivitySamplingResult.AllDataAndRecorded)
							{
								return activitySamplingResult;
							}
						}
						else if (filterAndTransform.SourceName == "*")
						{
							if (activitySamplingResult != ActivitySamplingResult.None)
							{
								return activitySamplingResult;
							}
							if (filterAndTransform.SamplingResult > activitySamplingResult2)
							{
								activitySamplingResult2 = filterAndTransform.SamplingResult;
							}
						}
					}
					filterAndTransform = filterAndTransform.Next;
				}
				if (activitySamplingResult == ActivitySamplingResult.None)
				{
					return activitySamplingResult2;
				}
				return activitySamplingResult;
			}

			// Token: 0x06000302 RID: 770 RVA: 0x0000BDE8 File Offset: 0x00009FE8
			internal static void CreateActivityListener(DiagnosticSourceEventSource eventSource)
			{
				eventSource._activityListener = new ActivityListener();
				eventSource._activityListener.SampleUsingParentId = delegate(ref ActivityCreationOptions<string> activityOptions)
				{
					return DiagnosticSourceEventSource.FilterAndTransform.Sample(activityOptions.Source.Name, activityOptions.Name, eventSource);
				};
				eventSource._activityListener.Sample = delegate(ref ActivityCreationOptions<ActivityContext> activityOptions)
				{
					return DiagnosticSourceEventSource.FilterAndTransform.Sample(activityOptions.Source.Name, activityOptions.Name, eventSource);
				};
				eventSource._activityListener.ShouldListenTo = delegate(ActivitySource activitySource)
				{
					for (DiagnosticSourceEventSource.FilterAndTransform filterAndTransform = eventSource._activitySourceSpecs; filterAndTransform != null; filterAndTransform = filterAndTransform.Next)
					{
						if (activitySource.Name == filterAndTransform.SourceName || filterAndTransform.SourceName == "*")
						{
							return true;
						}
					}
					return false;
				};
				eventSource._activityListener.ActivityStarted = delegate(Activity activity)
				{
					DiagnosticSourceEventSource.FilterAndTransform.OnActivityStarted(eventSource, activity);
				};
				eventSource._activityListener.ActivityStopped = delegate(Activity activity)
				{
					DiagnosticSourceEventSource.FilterAndTransform.OnActivityStopped(eventSource, activity);
				};
				ActivitySource.AddActivityListener(eventSource._activityListener);
			}

			// Token: 0x06000303 RID: 771 RVA: 0x0000BEB0 File Offset: 0x0000A0B0
			[DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(Activity))]
			[DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ActivityContext))]
			[DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ActivityEvent))]
			[DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(ActivityLink))]
			[DynamicDependency("Ticks", typeof(DateTime))]
			[DynamicDependency("Ticks", typeof(TimeSpan))]
			[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Activity's properties are being preserved with the DynamicDependencies on OnActivityStarted.")]
			private static void OnActivityStarted(DiagnosticSourceEventSource eventSource, Activity activity)
			{
				for (DiagnosticSourceEventSource.FilterAndTransform filterAndTransform = eventSource._activitySourceSpecs; filterAndTransform != null; filterAndTransform = filterAndTransform.Next)
				{
					if ((filterAndTransform.Events & DiagnosticSourceEventSource.ActivityEvents.ActivityStart) != DiagnosticSourceEventSource.ActivityEvents.None && (activity.Source.Name == filterAndTransform.SourceName || filterAndTransform.SourceName == "*") && (filterAndTransform.ActivityName == null || filterAndTransform.ActivityName == activity.OperationName))
					{
						eventSource.ActivityStart(activity.Source.Name, activity.OperationName, filterAndTransform.Morph(activity));
						return;
					}
				}
			}

			// Token: 0x06000304 RID: 772 RVA: 0x0000BF40 File Offset: 0x0000A140
			[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Activity's properties are being preserved with the DynamicDependencies on OnActivityStarted.")]
			private static void OnActivityStopped(DiagnosticSourceEventSource eventSource, Activity activity)
			{
				for (DiagnosticSourceEventSource.FilterAndTransform filterAndTransform = eventSource._activitySourceSpecs; filterAndTransform != null; filterAndTransform = filterAndTransform.Next)
				{
					if ((filterAndTransform.Events & DiagnosticSourceEventSource.ActivityEvents.ActivityStop) != DiagnosticSourceEventSource.ActivityEvents.None && (activity.Source.Name == filterAndTransform.SourceName || filterAndTransform.SourceName == "*") && (filterAndTransform.ActivityName == null || filterAndTransform.ActivityName == activity.OperationName))
					{
						eventSource.ActivityStop(activity.Source.Name, activity.OperationName, filterAndTransform.Morph(activity));
						return;
					}
				}
			}

			// Token: 0x06000305 RID: 773 RVA: 0x0000BFD0 File Offset: 0x0000A1D0
			internal static void NormalizeActivitySourceSpecsList(DiagnosticSourceEventSource eventSource)
			{
				DiagnosticSourceEventSource.FilterAndTransform filterAndTransform = eventSource._activitySourceSpecs;
				DiagnosticSourceEventSource.FilterAndTransform filterAndTransform2 = null;
				DiagnosticSourceEventSource.FilterAndTransform filterAndTransform3 = null;
				DiagnosticSourceEventSource.FilterAndTransform filterAndTransform4 = null;
				DiagnosticSourceEventSource.FilterAndTransform filterAndTransform5 = null;
				while (filterAndTransform != null)
				{
					if (filterAndTransform.SourceName == "*")
					{
						if (filterAndTransform4 == null)
						{
							filterAndTransform5 = (filterAndTransform4 = filterAndTransform);
						}
						else
						{
							filterAndTransform5.Next = filterAndTransform;
							filterAndTransform5 = filterAndTransform;
						}
					}
					else if (filterAndTransform2 == null)
					{
						filterAndTransform3 = (filterAndTransform2 = filterAndTransform);
					}
					else
					{
						filterAndTransform3.Next = filterAndTransform;
						filterAndTransform3 = filterAndTransform;
					}
					filterAndTransform = filterAndTransform.Next;
				}
				if (filterAndTransform2 == null || filterAndTransform4 == null)
				{
					return;
				}
				filterAndTransform3.Next = filterAndTransform4;
				filterAndTransform5.Next = null;
				eventSource._activitySourceSpecs = filterAndTransform2;
			}

			// Token: 0x06000306 RID: 774 RVA: 0x0000C054 File Offset: 0x0000A254
			private void Dispose()
			{
				if (this._diagnosticsListenersSubscription != null)
				{
					this._diagnosticsListenersSubscription.Dispose();
					this._diagnosticsListenersSubscription = null;
				}
				if (this._liveSubscriptions != null)
				{
					DiagnosticSourceEventSource.Subscriptions subscriptions = this._liveSubscriptions;
					this._liveSubscriptions = null;
					while (subscriptions != null)
					{
						subscriptions.Subscription.Dispose();
						subscriptions = subscriptions.Next;
					}
				}
			}

			// Token: 0x06000307 RID: 775 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
			[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2112:ReflectionToRequiresUnreferencedCode", Justification = "In EventSource, EnsureDescriptorsInitialized's use of GetType preserves this method which requires unreferenced code, but EnsureDescriptorsInitialized does not access this member and is safe to call.")]
			[RequiresUnreferencedCode("The type of object being written to DiagnosticSource cannot be discovered statically.")]
			public List<KeyValuePair<string, string>> Morph(object args)
			{
				List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
				if (args != null)
				{
					if (!this._noImplicitTransforms)
					{
						Type type2 = args.GetType();
						DiagnosticSourceEventSource.ImplicitTransformEntry firstImplicitTransformsEntry = this._firstImplicitTransformsEntry;
						DiagnosticSourceEventSource.TransformSpec transformSpec;
						if (firstImplicitTransformsEntry != null && firstImplicitTransformsEntry.Type == type2)
						{
							transformSpec = firstImplicitTransformsEntry.Transforms;
						}
						else if (firstImplicitTransformsEntry == null)
						{
							transformSpec = DiagnosticSourceEventSource.FilterAndTransform.MakeImplicitTransforms(type2);
							Interlocked.CompareExchange<DiagnosticSourceEventSource.ImplicitTransformEntry>(ref this._firstImplicitTransformsEntry, new DiagnosticSourceEventSource.ImplicitTransformEntry
							{
								Type = type2,
								Transforms = transformSpec
							}, null);
						}
						else
						{
							if (this._implicitTransformsTable == null)
							{
								Interlocked.CompareExchange<ConcurrentDictionary<Type, DiagnosticSourceEventSource.TransformSpec>>(ref this._implicitTransformsTable, new ConcurrentDictionary<Type, DiagnosticSourceEventSource.TransformSpec>(1, 8), null);
							}
							transformSpec = this._implicitTransformsTable.GetOrAdd(type2, (Type type) => DiagnosticSourceEventSource.FilterAndTransform.<Morph>g__MakeImplicitTransformsWrapper|12_1(type));
						}
						if (transformSpec != null)
						{
							for (DiagnosticSourceEventSource.TransformSpec transformSpec2 = transformSpec; transformSpec2 != null; transformSpec2 = transformSpec2.Next)
							{
								list.Add(transformSpec2.Morph(args));
							}
						}
					}
					if (this._explicitTransforms != null)
					{
						for (DiagnosticSourceEventSource.TransformSpec transformSpec3 = this._explicitTransforms; transformSpec3 != null; transformSpec3 = transformSpec3.Next)
						{
							KeyValuePair<string, string> keyValuePair = transformSpec3.Morph(args);
							if (keyValuePair.Value != null)
							{
								list.Add(keyValuePair);
							}
						}
					}
				}
				return list;
			}

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x06000308 RID: 776 RVA: 0x0000C1C6 File Offset: 0x0000A3C6
			// (set) Token: 0x06000309 RID: 777 RVA: 0x0000C1CE File Offset: 0x0000A3CE
			internal string SourceName { get; set; }

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x0600030A RID: 778 RVA: 0x0000C1D7 File Offset: 0x0000A3D7
			// (set) Token: 0x0600030B RID: 779 RVA: 0x0000C1DF File Offset: 0x0000A3DF
			internal string ActivityName { get; set; }

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x0600030C RID: 780 RVA: 0x0000C1E8 File Offset: 0x0000A3E8
			// (set) Token: 0x0600030D RID: 781 RVA: 0x0000C1F0 File Offset: 0x0000A3F0
			internal DiagnosticSourceEventSource.ActivityEvents Events { get; set; }

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x0600030E RID: 782 RVA: 0x0000C1F9 File Offset: 0x0000A3F9
			// (set) Token: 0x0600030F RID: 783 RVA: 0x0000C201 File Offset: 0x0000A401
			internal ActivitySamplingResult SamplingResult { get; set; }

			// Token: 0x06000310 RID: 784 RVA: 0x0000C20C File Offset: 0x0000A40C
			[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2112:ReflectionToRequiresUnreferencedCode", Justification = "In EventSource, EnsureDescriptorsInitialized's use of GetType preserves this method which requires unreferenced code, but EnsureDescriptorsInitialized does not access this member and is safe to call.")]
			[RequiresUnreferencedCode("The type of object being written to DiagnosticSource cannot be discovered statically.")]
			private static DiagnosticSourceEventSource.TransformSpec MakeImplicitTransforms(Type type)
			{
				DiagnosticSourceEventSource.TransformSpec transformSpec = null;
				TypeInfo typeInfo = type.GetTypeInfo();
				foreach (PropertyInfo propertyInfo in typeInfo.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (!(propertyInfo.GetMethod == null) && propertyInfo.GetMethod.GetParameters().Length == 0)
					{
						transformSpec = new DiagnosticSourceEventSource.TransformSpec(propertyInfo.Name, 0, propertyInfo.Name.Length, transformSpec);
					}
				}
				return DiagnosticSourceEventSource.FilterAndTransform.Reverse(transformSpec);
			}

			// Token: 0x06000311 RID: 785 RVA: 0x0000C280 File Offset: 0x0000A480
			private static DiagnosticSourceEventSource.TransformSpec Reverse(DiagnosticSourceEventSource.TransformSpec list)
			{
				DiagnosticSourceEventSource.TransformSpec transformSpec = null;
				while (list != null)
				{
					DiagnosticSourceEventSource.TransformSpec next = list.Next;
					list.Next = transformSpec;
					transformSpec = list;
					list = next;
				}
				return transformSpec;
			}

			// Token: 0x06000312 RID: 786 RVA: 0x0000C2A8 File Offset: 0x0000A4A8
			[CompilerGenerated]
			[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "The Morph method has RequiresUnreferencedCode, but the trimmer can't see through lamdba calls.")]
			internal static DiagnosticSourceEventSource.TransformSpec <Morph>g__MakeImplicitTransformsWrapper|12_1(Type transformType)
			{
				return DiagnosticSourceEventSource.FilterAndTransform.MakeImplicitTransforms(transformType);
			}

			// Token: 0x04000169 RID: 361
			public DiagnosticSourceEventSource.FilterAndTransform Next;

			// Token: 0x0400016A RID: 362
			internal const string c_ActivitySourcePrefix = "[AS]";

			// Token: 0x0400016F RID: 367
			private IDisposable _diagnosticsListenersSubscription;

			// Token: 0x04000170 RID: 368
			private DiagnosticSourceEventSource.Subscriptions _liveSubscriptions;

			// Token: 0x04000171 RID: 369
			private readonly bool _noImplicitTransforms;

			// Token: 0x04000172 RID: 370
			private DiagnosticSourceEventSource.ImplicitTransformEntry _firstImplicitTransformsEntry;

			// Token: 0x04000173 RID: 371
			private ConcurrentDictionary<Type, DiagnosticSourceEventSource.TransformSpec> _implicitTransformsTable;

			// Token: 0x04000174 RID: 372
			private readonly DiagnosticSourceEventSource.TransformSpec _explicitTransforms;

			// Token: 0x04000175 RID: 373
			private readonly DiagnosticSourceEventSource _eventSource;
		}

		// Token: 0x02000076 RID: 118
		internal sealed class ImplicitTransformEntry
		{
			// Token: 0x04000176 RID: 374
			public Type Type;

			// Token: 0x04000177 RID: 375
			public DiagnosticSourceEventSource.TransformSpec Transforms;
		}

		// Token: 0x02000077 RID: 119
		internal sealed class TransformSpec
		{
			// Token: 0x06000314 RID: 788 RVA: 0x0000C2B8 File Offset: 0x0000A4B8
			public TransformSpec(string transformSpec, int startIdx, int endIdx, DiagnosticSourceEventSource.TransformSpec next = null)
			{
				this.Next = next;
				int num = transformSpec.IndexOf('=', startIdx, endIdx - startIdx);
				if (0 <= num)
				{
					this._outputName = transformSpec.Substring(startIdx, num - startIdx);
					startIdx = num + 1;
				}
				while (startIdx < endIdx)
				{
					int num2 = transformSpec.LastIndexOf('.', endIdx - 1, endIdx - startIdx);
					int num3 = startIdx;
					if (0 <= num2)
					{
						num3 = num2 + 1;
					}
					string text = transformSpec.Substring(num3, endIdx - num3);
					this._fetches = new DiagnosticSourceEventSource.TransformSpec.PropertySpec(text, this._fetches);
					if (this._outputName == null)
					{
						this._outputName = text;
					}
					endIdx = num2;
				}
			}

			// Token: 0x06000315 RID: 789 RVA: 0x0000C348 File Offset: 0x0000A548
			[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2112:ReflectionToRequiresUnreferencedCode", Justification = "In EventSource, EnsureDescriptorsInitialized's use of GetType preserves this method which requires unreferenced code, but EnsureDescriptorsInitialized does not access this member and is safe to call.")]
			[RequiresUnreferencedCode("The type of object being written to DiagnosticSource cannot be discovered statically.")]
			public KeyValuePair<string, string> Morph(object obj)
			{
				for (DiagnosticSourceEventSource.TransformSpec.PropertySpec propertySpec = this._fetches; propertySpec != null; propertySpec = propertySpec.Next)
				{
					if (obj != null || propertySpec.IsStatic)
					{
						obj = propertySpec.Fetch(obj);
					}
				}
				return new KeyValuePair<string, string>(this._outputName, (obj != null) ? obj.ToString() : null);
			}

			// Token: 0x04000178 RID: 376
			public DiagnosticSourceEventSource.TransformSpec Next;

			// Token: 0x04000179 RID: 377
			private readonly string _outputName;

			// Token: 0x0400017A RID: 378
			private readonly DiagnosticSourceEventSource.TransformSpec.PropertySpec _fetches;

			// Token: 0x020000A3 RID: 163
			internal sealed class PropertySpec
			{
				// Token: 0x06000404 RID: 1028 RVA: 0x0000E24D File Offset: 0x0000C44D
				public PropertySpec(string propertyName, DiagnosticSourceEventSource.TransformSpec.PropertySpec next)
				{
					this.Next = next;
					this._propertyName = propertyName;
					if (this._propertyName == "*Activity")
					{
						this.IsStatic = true;
					}
				}

				// Token: 0x170000D2 RID: 210
				// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000E27C File Offset: 0x0000C47C
				// (set) Token: 0x06000406 RID: 1030 RVA: 0x0000E284 File Offset: 0x0000C484
				public bool IsStatic { get; private set; }

				// Token: 0x06000407 RID: 1031 RVA: 0x0000E290 File Offset: 0x0000C490
				[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2112:ReflectionToRequiresUnreferencedCode", Justification = "In EventSource, EnsureDescriptorsInitialized's use of GetType preserves this method which requires unreferenced code, but EnsureDescriptorsInitialized does not access this member and is safe to call.")]
				[RequiresUnreferencedCode("The type of object being written to DiagnosticSource cannot be discovered statically.")]
				public object Fetch(object obj)
				{
					DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch propertyFetch = this._fetchForExpectedType;
					Type type = ((obj != null) ? obj.GetType() : null);
					if (propertyFetch == null || propertyFetch.Type != type)
					{
						propertyFetch = (this._fetchForExpectedType = DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch.FetcherForProperty(type, this._propertyName));
					}
					object obj2 = null;
					try
					{
						obj2 = propertyFetch.Fetch(obj);
					}
					catch (Exception ex)
					{
						DiagnosticSourceEventSource.Log.Message(string.Format("Property {0}.{1} threw the exception {2}", type, this._propertyName, ex));
					}
					return obj2;
				}

				// Token: 0x040001EC RID: 492
				private const string CurrentActivityPropertyName = "*Activity";

				// Token: 0x040001ED RID: 493
				private const string EnumeratePropertyName = "*Enumerate";

				// Token: 0x040001EF RID: 495
				public DiagnosticSourceEventSource.TransformSpec.PropertySpec Next;

				// Token: 0x040001F0 RID: 496
				private readonly string _propertyName;

				// Token: 0x040001F1 RID: 497
				private volatile DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch _fetchForExpectedType;

				// Token: 0x020000A6 RID: 166
				private class PropertyFetch
				{
					// Token: 0x0600041C RID: 1052 RVA: 0x0000E636 File Offset: 0x0000C836
					public PropertyFetch(Type type)
					{
						this.Type = type;
					}

					// Token: 0x170000D5 RID: 213
					// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000E645 File Offset: 0x0000C845
					internal Type Type { get; }

					// Token: 0x0600041E RID: 1054 RVA: 0x0000E650 File Offset: 0x0000C850
					[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2112:ReflectionToRequiresUnreferencedCode", Justification = "In EventSource, EnsureDescriptorsInitialized's use of GetType preserves this method which requires unreferenced code, but EnsureDescriptorsInitialized does not access this member and is safe to call.")]
					[RequiresUnreferencedCode("The type of object being written to DiagnosticSource cannot be discovered statically.")]
					public static DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch FetcherForProperty(Type type, string propertyName)
					{
						if (propertyName == null)
						{
							return new DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch(type);
						}
						if (propertyName == "*Activity")
						{
							return new DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch.CurrentActivityPropertyFetch();
						}
						TypeInfo typeInfo = type.GetTypeInfo();
						if (propertyName == "*Enumerate")
						{
							foreach (Type type2 in typeInfo.GetInterfaces())
							{
								TypeInfo typeInfo2 = type2.GetTypeInfo();
								if (typeInfo2.IsGenericType && !(typeInfo2.GetGenericTypeDefinition() != typeof(IEnumerable<>)))
								{
									Type type3 = typeInfo2.GetGenericArguments()[0];
									Type type4 = typeof(DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch.EnumeratePropertyFetch<>).GetTypeInfo().MakeGenericType(new Type[] { type3 });
									return (DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch)Activator.CreateInstance(type4, new object[] { type });
								}
							}
							DiagnosticSourceEventSource.Log.Message(string.Format("*Enumerate applied to non-enumerable type {0}", type));
							return new DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch(type);
						}
						PropertyInfo propertyInfo = typeInfo.GetDeclaredProperty(propertyName);
						if (propertyInfo == null)
						{
							foreach (PropertyInfo propertyInfo2 in typeInfo.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
							{
								if (propertyInfo2.Name == propertyName)
								{
									propertyInfo = propertyInfo2;
									break;
								}
							}
						}
						if (propertyInfo == null)
						{
							DiagnosticSourceEventSource.Log.Message(string.Format("Property {0} not found on {1}. Ensure the name is spelled correctly. If you published the application with PublishTrimmed=true, ensure the property was not trimmed away.", propertyName, type));
							return new DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch(type);
						}
						MethodInfo getMethod = propertyInfo.GetMethod;
						if (getMethod == null || !getMethod.IsStatic)
						{
							MethodInfo setMethod = propertyInfo.SetMethod;
							if (setMethod == null || !setMethod.IsStatic)
							{
								Type type5 = (typeInfo.IsValueType ? typeof(DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch.ValueTypedFetchProperty<, >) : typeof(DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch.RefTypedFetchProperty<, >));
								Type type6 = type5.GetTypeInfo().MakeGenericType(new Type[] { propertyInfo.DeclaringType, propertyInfo.PropertyType });
								return (DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch)Activator.CreateInstance(type6, new object[] { type, propertyInfo });
							}
						}
						DiagnosticSourceEventSource.Log.Message("Property " + propertyName + " is static.");
						return new DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch(type);
					}

					// Token: 0x0600041F RID: 1055 RVA: 0x0000E853 File Offset: 0x0000CA53
					public virtual object Fetch(object obj)
					{
						return null;
					}

					// Token: 0x020000A7 RID: 167
					private sealed class RefTypedFetchProperty<TObject, TProperty> : DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch
					{
						// Token: 0x06000420 RID: 1056 RVA: 0x0000E856 File Offset: 0x0000CA56
						public RefTypedFetchProperty(Type type, PropertyInfo property)
							: base(type)
						{
							this._propertyFetch = (Func<TObject, TProperty>)property.GetMethod.CreateDelegate(typeof(Func<TObject, TProperty>));
						}

						// Token: 0x06000421 RID: 1057 RVA: 0x0000E87F File Offset: 0x0000CA7F
						public override object Fetch(object obj)
						{
							return this._propertyFetch((TObject)((object)obj));
						}

						// Token: 0x040001FA RID: 506
						private readonly Func<TObject, TProperty> _propertyFetch;
					}

					// Token: 0x020000A8 RID: 168
					// (Invoke) Token: 0x06000423 RID: 1059
					private delegate TProperty StructFunc<TStruct, TProperty>(ref TStruct thisArg);

					// Token: 0x020000A9 RID: 169
					private sealed class ValueTypedFetchProperty<TStruct, TProperty> : DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch
					{
						// Token: 0x06000426 RID: 1062 RVA: 0x0000E897 File Offset: 0x0000CA97
						public ValueTypedFetchProperty(Type type, PropertyInfo property)
							: base(type)
						{
							this._propertyFetch = (DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch.StructFunc<TStruct, TProperty>)property.GetMethod.CreateDelegate(typeof(DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch.StructFunc<TStruct, TProperty>));
						}

						// Token: 0x06000427 RID: 1063 RVA: 0x0000E8C0 File Offset: 0x0000CAC0
						public override object Fetch(object obj)
						{
							TStruct tstruct = (TStruct)((object)obj);
							return this._propertyFetch(ref tstruct);
						}

						// Token: 0x040001FB RID: 507
						private readonly DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch.StructFunc<TStruct, TProperty> _propertyFetch;
					}

					// Token: 0x020000AA RID: 170
					private sealed class CurrentActivityPropertyFetch : DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch
					{
						// Token: 0x06000428 RID: 1064 RVA: 0x0000E8E6 File Offset: 0x0000CAE6
						public CurrentActivityPropertyFetch()
							: base(null)
						{
						}

						// Token: 0x06000429 RID: 1065 RVA: 0x0000E8EF File Offset: 0x0000CAEF
						public override object Fetch(object obj)
						{
							return Activity.Current;
						}
					}

					// Token: 0x020000AB RID: 171
					private sealed class EnumeratePropertyFetch<ElementType> : DiagnosticSourceEventSource.TransformSpec.PropertySpec.PropertyFetch
					{
						// Token: 0x0600042A RID: 1066 RVA: 0x0000E8F6 File Offset: 0x0000CAF6
						public EnumeratePropertyFetch(Type type)
							: base(type)
						{
						}

						// Token: 0x0600042B RID: 1067 RVA: 0x0000E8FF File Offset: 0x0000CAFF
						public override object Fetch(object obj)
						{
							return string.Join<ElementType>(",", (IEnumerable<ElementType>)obj);
						}
					}
				}
			}
		}

		// Token: 0x02000078 RID: 120
		internal sealed class CallbackObserver<T> : IObserver<T>
		{
			// Token: 0x06000316 RID: 790 RVA: 0x0000C393 File Offset: 0x0000A593
			public CallbackObserver(Action<T> callback)
			{
				this._callback = callback;
			}

			// Token: 0x06000317 RID: 791 RVA: 0x0000C3A2 File Offset: 0x0000A5A2
			public void OnCompleted()
			{
			}

			// Token: 0x06000318 RID: 792 RVA: 0x0000C3A4 File Offset: 0x0000A5A4
			public void OnError(Exception error)
			{
			}

			// Token: 0x06000319 RID: 793 RVA: 0x0000C3A6 File Offset: 0x0000A5A6
			public void OnNext(T value)
			{
				this._callback(value);
			}

			// Token: 0x0400017B RID: 379
			private readonly Action<T> _callback;
		}

		// Token: 0x02000079 RID: 121
		internal sealed class Subscriptions
		{
			// Token: 0x0600031A RID: 794 RVA: 0x0000C3B4 File Offset: 0x0000A5B4
			public Subscriptions(IDisposable subscription, DiagnosticSourceEventSource.Subscriptions next)
			{
				this.Subscription = subscription;
				this.Next = next;
			}

			// Token: 0x0400017C RID: 380
			public IDisposable Subscription;

			// Token: 0x0400017D RID: 381
			public DiagnosticSourceEventSource.Subscriptions Next;
		}
	}
}
