using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.ConfigurationClasses.Tracing;
using Microsoft.Cloud.Platform.Tracing;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200015F RID: 351
	public abstract class TraceSourceBase<T> : ITraceSource where T : TraceSourceBase<T>, new()
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000917 RID: 2327
		public abstract TraceSourceIdentifier ID { get; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000918 RID: 2328
		public abstract TraceVerbosity DefaultVerbosity { get; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0001F71E File Offset: 0x0001D91E
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x0001F725 File Offset: 0x0001D925
		public static T Tracer { get; private set; }

		// Token: 0x0600091B RID: 2331 RVA: 0x0001F730 File Offset: 0x0001D930
		static TraceSourceBase()
		{
			if (CurrentProcess.WellKnownHost == ProcessWellKnownHost.MSTest && !TraceSourceBaseAdditionalListeners.Listeners.OfType<Microsoft.Cloud.Platform.Tracing.ConsoleTraceListener>().Any<Microsoft.Cloud.Platform.Tracing.ConsoleTraceListener>())
			{
				TraceSourceBaseAdditionalListeners.Add(new Microsoft.Cloud.Platform.Tracing.ConsoleTraceListener());
				TraceSourceBaseAdditionalListeners.Add(new TextFileListener("Trace"));
			}
			TraceSourceBase<T>.Tracer = new T();
			Tracing.TraceSourcesRegistrar.RegisterSource(TraceSourceBase<T>.Tracer.ID, new Action<TraceSourceConfig>(TraceSourceBase<T>.Tracer.ChangeConfig));
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0001F7A8 File Offset: 0x0001D9A8
		protected TraceSourceBase()
		{
			if (TraceSourceBase<T>.Tracer != null)
			{
				Ensure.Fail(base.GetType().FullName + "'s constructor must not be directly invoked. Use the Tracer property for a singleton");
			}
			TraceSourceConfig traceSourceConfig = new TraceSourceConfig
			{
				Name = this.ID.Name,
				Verbosity = this.DefaultVerbosity
			};
			this.m_config = traceSourceConfig;
			this.m_dotNetTraceSource = new TraceSource(this.ID.Name, SourceLevels.Verbose);
			this.m_dotNetTraceSource.Listeners.Clear();
			TraceListener[] array = new TraceListener[global::System.Diagnostics.Trace.Listeners.Count + 32];
			global::System.Diagnostics.Trace.Listeners.CopyTo(array, 0);
			foreach (TraceListener traceListener in array)
			{
				if (traceListener == null)
				{
					break;
				}
				if (!RuntimeTraceSourceConfiguration.IsIgnored(traceListener.Name))
				{
					this.m_dotNetTraceSource.Listeners.Add(traceListener);
				}
			}
			foreach (TraceListener traceListener2 in TraceSourceBaseAdditionalListeners.Listeners)
			{
				this.m_dotNetTraceSource.Listeners.Add(traceListener2);
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0001F8D8 File Offset: 0x0001DAD8
		internal TraceVerbosity Verbosity
		{
			get
			{
				return this.m_config.Verbosity;
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001F8E5 File Offset: 0x0001DAE5
		public void TraceFatal(string message)
		{
			if (this.ShouldTrace(TraceVerbosity.Fatal))
			{
				this.TraceInternal(TraceVerbosity.Fatal, message);
			}
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0001F8F8 File Offset: 0x0001DAF8
		public void TraceFatal(string format, params object[] args)
		{
			if (this.ShouldTrace(TraceVerbosity.Fatal))
			{
				this.TraceInternal(TraceVerbosity.Fatal, format, args);
			}
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001F90C File Offset: 0x0001DB0C
		public void TraceError(string message)
		{
			if (this.ShouldTrace(TraceVerbosity.Error))
			{
				this.TraceInternal(TraceVerbosity.Error, message);
			}
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001F91F File Offset: 0x0001DB1F
		public void TraceError(string format, params object[] args)
		{
			if (this.ShouldTrace(TraceVerbosity.Error))
			{
				this.TraceInternal(TraceVerbosity.Error, format, args);
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001F933 File Offset: 0x0001DB33
		public void TraceWarning(string message)
		{
			if (this.ShouldTrace(TraceVerbosity.Warning))
			{
				this.TraceInternal(TraceVerbosity.Warning, message);
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001F946 File Offset: 0x0001DB46
		public void TraceWarning(string format, params object[] args)
		{
			if (this.ShouldTrace(TraceVerbosity.Warning))
			{
				this.TraceInternal(TraceVerbosity.Warning, format, args);
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0001F95A File Offset: 0x0001DB5A
		public void TraceInformation(string message)
		{
			if (this.ShouldTrace(TraceVerbosity.Info))
			{
				this.TraceInternal(TraceVerbosity.Info, message);
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001F96D File Offset: 0x0001DB6D
		public void TraceInformation(string format, params object[] args)
		{
			if (this.ShouldTrace(TraceVerbosity.Info))
			{
				this.TraceInternal(TraceVerbosity.Info, format, args);
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001F981 File Offset: 0x0001DB81
		public void TraceVerbose(string message)
		{
			if (this.ShouldTrace(TraceVerbosity.Verbose))
			{
				this.TraceInternal(TraceVerbosity.Verbose, message);
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0001F994 File Offset: 0x0001DB94
		public void TraceVerbose(string format, params object[] args)
		{
			if (this.ShouldTrace(TraceVerbosity.Verbose))
			{
				this.TraceInternal(TraceVerbosity.Verbose, format, args);
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0001F9A8 File Offset: 0x0001DBA8
		public void Trace(TraceVerbosity verbosity, string message)
		{
			if (this.ShouldTrace(verbosity))
			{
				this.TraceInternal(verbosity, message);
			}
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0001F9BB File Offset: 0x0001DBBB
		public void TraceWithHeader(TraceVerbosity verbosity, string header, string message)
		{
			if (this.ShouldTrace(verbosity))
			{
				message = "{0} {1}".FormatWithInvariantCulture(new object[] { header, message });
				this.m_dotNetTraceSource.TraceData(this.ConvertTraceVerbosityToDotNetEventType(verbosity), 0, message);
			}
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0001F9F4 File Offset: 0x0001DBF4
		public void Trace(TraceEventType verbosity, string format, params object[] args)
		{
			TraceVerbosity traceVerbosity = this.ConvertDotNetEventTypeToTraceVerbosity(verbosity);
			if (this.ShouldTrace(traceVerbosity))
			{
				this.TraceInternal(traceVerbosity, format, args);
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0001FA1B File Offset: 0x0001DC1B
		[StringFormatMethod("format")]
		public void Trace(TraceVerbosity verbosity, [NotNull] string format, params object[] args)
		{
			if (this.ShouldTrace(verbosity))
			{
				this.TraceInternal(verbosity, format, args);
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0001FA2F File Offset: 0x0001DC2F
		public bool ShouldTrace(TraceVerbosity level)
		{
			return Tracing.ShouldTrace(level, this.Verbosity);
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0001FA3D File Offset: 0x0001DC3D
		public char Delimiter
		{
			get
			{
				return Tracing.Delimiter;
			}
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0001FA44 File Offset: 0x0001DC44
		internal void ChangeConfig(TraceSourceConfig config)
		{
			this.m_config = config;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001FA50 File Offset: 0x0001DC50
		public string GetTraceHeader(object timeStamp, object elementId, object activityId, object rootActivityId, object activityEventShortName, object clientActivityId, object component)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			stringBuilder.Append(timeStamp);
			stringBuilder.Append(" ");
			stringBuilder.Append(elementId);
			stringBuilder.Append(this.Delimiter);
			stringBuilder.Append(activityId);
			stringBuilder.Append(this.Delimiter);
			stringBuilder.Append(rootActivityId);
			stringBuilder.Append(this.Delimiter);
			stringBuilder.Append(activityEventShortName);
			stringBuilder.Append(this.Delimiter);
			stringBuilder.Append(clientActivityId);
			stringBuilder.Append(this.Delimiter);
			stringBuilder.Append(Hashing.ComputeKnuthHashForString(component.ToString()).ToString(CultureInfo.InvariantCulture));
			return stringBuilder.ToString();
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001FB0C File Offset: 0x0001DD0C
		protected void AddTraceListener(TraceListener traceListener)
		{
			if (this.m_dotNetTraceSource.Listeners.OfType<TraceListener>().None((TraceListener l) => l.GetType().Equals(traceListener.GetType())))
			{
				this.m_dotNetTraceSource.Listeners.Add(traceListener);
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001FB60 File Offset: 0x0001DD60
		protected TraceVerbosity ConvertDotNetEventTypeToTraceVerbosity(TraceEventType verbosity)
		{
			TraceVerbosity traceVerbosity = TraceVerbosity.Fatal;
			switch (verbosity)
			{
			case TraceEventType.Critical:
				return TraceVerbosity.Fatal;
			case TraceEventType.Error:
				return TraceVerbosity.Error;
			case (TraceEventType)3:
				break;
			case TraceEventType.Warning:
				return TraceVerbosity.Warning;
			default:
				if (verbosity == TraceEventType.Information)
				{
					return TraceVerbosity.Info;
				}
				if (verbosity == TraceEventType.Verbose)
				{
					return TraceVerbosity.Verbose;
				}
				break;
			}
			ExtendedDiagnostics.EnsureInvalidSwitchValue<TraceEventType>(verbosity);
			return traceVerbosity;
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x0001FBAD File Offset: 0x0001DDAD
		internal TraceSource UnderlyingDotNetTraceSource
		{
			get
			{
				return this.m_dotNetTraceSource;
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0001FBB8 File Offset: 0x0001DDB8
		private void TraceInternal(TraceVerbosity verbosity, string format, params object[] args)
		{
			string text = ((args.Length != 0) ? format.FormatWithInvariantCulture(args) : format);
			string text2 = Hashing.ComputeKnuthHashForString(format).ToString();
			this.TraceInternal(verbosity, text2, text);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0001FBEC File Offset: 0x0001DDEC
		private void TraceInternal(TraceVerbosity verbosity, string message)
		{
			string text = Hashing.ComputeKnuthHashForString(message).ToString();
			this.TraceInternal(verbosity, text, message);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0001FC14 File Offset: 0x0001DE14
		private void TraceInternal(TraceVerbosity verbosity, string sourceId, string message)
		{
			Activity activity = UtilsContext.Current.Activity;
			this.m_dotNetTraceSource.TraceData(this.ConvertTraceVerbosityToDotNetEventType(verbosity), 0, new object[]
			{
				DateTime.UtcNow,
				Tracing.InstanceId,
				activity.ActivityId,
				activity.RootActivityId,
				activity.ActivityType.ShortName,
				activity.ClientActivityId,
				sourceId,
				message
			});
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0001FC98 File Offset: 0x0001DE98
		private TraceEventType ConvertTraceVerbosityToDotNetEventType(TraceVerbosity verbosity)
		{
			TraceEventType traceEventType = TraceEventType.Critical;
			switch (verbosity)
			{
			case TraceVerbosity.Fatal:
				traceEventType = TraceEventType.Critical;
				break;
			case TraceVerbosity.Error:
				traceEventType = TraceEventType.Error;
				break;
			case TraceVerbosity.Warning:
				traceEventType = TraceEventType.Warning;
				break;
			case TraceVerbosity.Info:
				traceEventType = TraceEventType.Information;
				break;
			case TraceVerbosity.Verbose:
				traceEventType = TraceEventType.Verbose;
				break;
			default:
				ExtendedDiagnostics.EnsureInvalidSwitchValue<TraceVerbosity>(verbosity);
				break;
			}
			return traceEventType;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0001FCE4 File Offset: 0x0001DEE4
		private SourceLevels ConvertTraceVerbosityToDotNetSourceLevels(TraceVerbosity verbosity)
		{
			SourceLevels sourceLevels = SourceLevels.Off;
			switch (verbosity)
			{
			case TraceVerbosity.Fatal:
				sourceLevels = SourceLevels.Critical;
				break;
			case TraceVerbosity.Error:
				sourceLevels = SourceLevels.Error;
				break;
			case TraceVerbosity.Warning:
				sourceLevels = SourceLevels.Warning;
				break;
			case TraceVerbosity.Info:
				sourceLevels = SourceLevels.Information;
				break;
			case TraceVerbosity.Verbose:
				sourceLevels = SourceLevels.Verbose;
				break;
			default:
				ExtendedDiagnostics.EnsureInvalidSwitchValue<TraceVerbosity>(verbosity);
				break;
			}
			return sourceLevels;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0001FD2E File Offset: 0x0001DF2E
		public override string ToString()
		{
			return string.Format("Trace source with ID: {0}. Configuration: {1}.", this.ID, this.m_config);
		}

		// Token: 0x04000376 RID: 886
		private TraceSourceConfig m_config;

		// Token: 0x04000377 RID: 887
		private TraceSource m_dotNetTraceSource;
	}
}
