using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000007 RID: 7
	public class AppInsights : IAppInsightsWrapper
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002068 File Offset: 0x00000268
		public AppInsights(string appInsightsId, string connectionString, bool createFromDefaultConfiguration = false)
		{
			TelemetryConfiguration telemetryConfiguration = null;
			try
			{
				if (createFromDefaultConfiguration)
				{
					telemetryConfiguration = TelemetryConfiguration.CreateDefault();
				}
				else
				{
					telemetryConfiguration = TelemetryConfiguration.Active;
				}
			}
			catch (XmlException ex)
			{
				this.AddErrorDetails(ex);
				throw;
			}
			if (string.IsNullOrEmpty(connectionString))
			{
				telemetryConfiguration.ConnectionString = string.Format(CultureInfo.InvariantCulture, "InstrumentationKey={0}", new object[] { appInsightsId });
			}
			else
			{
				telemetryConfiguration.ConnectionString = connectionString;
			}
			this.appInsights = new TelemetryClient(telemetryConfiguration);
			this.appInsights.Context.Cloud.RoleInstance = "_";
			TelemetryContextExtensions.GetInternalContext(this.appInsights.Context).NodeName = "_";
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000211C File Offset: 0x0000031C
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002133 File Offset: 0x00000333
		public string UserId
		{
			get
			{
				return this.appInsights.Context.User.Id;
			}
			set
			{
				this.appInsights.Context.User.Id = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000214B File Offset: 0x0000034B
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002162 File Offset: 0x00000362
		public string SessionId
		{
			get
			{
				return this.appInsights.Context.Session.Id;
			}
			set
			{
				this.appInsights.Context.Session.Id = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000217C File Offset: 0x0000037C
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000021C7 File Offset: 0x000003C7
		public bool IsReturningUser
		{
			get
			{
				return this.appInsights.Context.Session.IsFirst == null || this.appInsights.Context.Session.IsFirst.Value;
			}
			set
			{
				this.appInsights.Context.Session.IsFirst = new bool?(!value);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000021E7 File Offset: 0x000003E7
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000021FE File Offset: 0x000003FE
		public string DeviceId
		{
			get
			{
				return this.appInsights.Context.Device.Id;
			}
			set
			{
				this.appInsights.Context.Device.Id = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002216 File Offset: 0x00000416
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000222D File Offset: 0x0000042D
		public string ApplicationVersion
		{
			get
			{
				return this.appInsights.Context.Component.Version;
			}
			set
			{
				this.appInsights.Context.Component.Version = value;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002248 File Offset: 0x00000448
		public void TrackEvent(string eventName, DateTime timestamp, Dictionary<string, string> propertiesToAdd)
		{
			EventTelemetry eventTelemetry = new EventTelemetry();
			eventTelemetry.Name = eventName;
			eventTelemetry.Timestamp = this.GetValidDateTimeOffset(timestamp, eventName);
			foreach (string text in propertiesToAdd.Keys)
			{
				eventTelemetry.Properties[text] = propertiesToAdd[text];
			}
			this.appInsights.TrackEvent(eventTelemetry);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022D0 File Offset: 0x000004D0
		public void TrackTrace(TraceType traceType, string message, Dictionary<string, string> propertiesToAdd)
		{
			SeverityLevel severityLevel;
			switch (traceType)
			{
			case TraceType.Verbose:
				severityLevel = 0;
				break;
			case TraceType.Warning:
				severityLevel = 2;
				break;
			case TraceType.Error:
			case TraceType.ExpectedError:
			case TraceType.UnexpectedError:
			case TraceType.Fatal:
				severityLevel = 3;
				break;
			default:
				severityLevel = 1;
				break;
			}
			TraceTelemetry traceTelemetry = new TraceTelemetry
			{
				Timestamp = DateTime.UtcNow,
				Message = (message ?? string.Empty),
				SeverityLevel = new SeverityLevel?(severityLevel)
			};
			foreach (string text in propertiesToAdd.Keys)
			{
				traceTelemetry.Properties[text] = propertiesToAdd[text].ScrubAndOrObfuscateTaggedInfo();
			}
			this.appInsights.TrackTrace(traceTelemetry);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023A4 File Offset: 0x000005A4
		public void Flush()
		{
			this.appInsights.Flush();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023B4 File Offset: 0x000005B4
		private void AddErrorDetails(XmlException ex)
		{
			IDictionary data = ex.Data;
			data["InitializationErrorType"] = ex.GetType().FullName;
			data["InitializationErrorHResult"] = ex.GetHResult().ToString("X", CultureInfo.InvariantCulture);
			string text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationInsights.config");
			if (File.Exists(text))
			{
				byte[] array = null;
				try
				{
					array = File.ReadAllBytes(text);
				}
				catch (Exception ex2)
				{
					data["ConfigFileAccessErrorType"] = ex2.GetType().FullName;
					data["ConfigFileAccessErrorHResult"] = ex2.GetHResult().ToString("X", CultureInfo.InvariantCulture);
					if (ex2 is IOException)
					{
						IOException ex3 = ex2 as IOException;
						string text2 = null;
						if (ex3.IsDeviceNotReadyException())
						{
							text2 = "Device not ready";
						}
						else if (ex3.IsFileInUseException())
						{
							text2 = "File in use";
						}
						else if (ex3.IsInvalidPathException())
						{
							text2 = "Invalid path";
						}
						if (!string.IsNullOrEmpty(text2))
						{
							data["ConfigFileAccessErrorDetails"] = text2;
						}
					}
					return;
				}
				if (array != null)
				{
					data["ConfigFileContentHex"] = BitConverter.ToString(array);
					return;
				}
				data["ConfigFileContentHex"] = "(null)";
				return;
			}
			else
			{
				data["ConfigFileAccessErrorDetails"] = "File not found";
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002514 File Offset: 0x00000714
		private DateTimeOffset GetValidDateTimeOffset(DateTime timestamp, string eventName)
		{
			DateTimeOffset dateTimeOffset;
			try
			{
				if (timestamp.Kind != DateTimeKind.Utc)
				{
					timestamp = timestamp.ToUniversalTime();
				}
				dateTimeOffset = new DateTimeOffset(timestamp);
				if (dateTimeOffset < DateTimeOffset.MinValue)
				{
					dateTimeOffset = DateTimeOffset.MinValue;
				}
				else if (dateTimeOffset > DateTimeOffset.MaxValue)
				{
					dateTimeOffset = DateTimeOffset.MaxValue;
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				dateTimeOffset = new DateTimeOffset(DateTime.UtcNow);
			}
			return dateTimeOffset;
		}

		// Token: 0x0400002E RID: 46
		private TelemetryClient appInsights;
	}
}
