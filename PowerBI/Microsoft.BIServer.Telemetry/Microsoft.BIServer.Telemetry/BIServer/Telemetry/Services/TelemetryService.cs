using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.BIServer.Telemetry.Helpers;
using Microsoft.BIServer.Telemetry.Interfaces;
using Microsoft.Owin;

namespace Microsoft.BIServer.Telemetry.Services
{
	// Token: 0x02000003 RID: 3
	public sealed class TelemetryService : ITelemetryService
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public TelemetryService(ITelemetryServiceConfiguration telemetryServiceConfiguration)
		{
			this._telemetryServiceConfiguration = telemetryServiceConfiguration;
			TelemetryConfiguration.Active.TelemetryChannel.EndpointAddress = "https://vortex.data.microsoft.com/collect/v1";
			TelemetryClient telemetryClient = new TelemetryClient
			{
				InstrumentationKey = this._telemetryServiceConfiguration.InstrumentationKey
			};
			this.SetContextProperties(telemetryClient, this._telemetryServiceConfiguration);
			this._client = telemetryClient;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020A9 File Offset: 0x000002A9
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020B0 File Offset: 0x000002B0
		public static ITelemetryService Current
		{
			get
			{
				return TelemetryService._current;
			}
			set
			{
				TelemetryService._current = value;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020B8 File Offset: 0x000002B8
		private void SetContextProperties(TelemetryClient client, ITelemetryServiceConfiguration configuration)
		{
			foreach (string text in configuration.AdditionalProperties.Keys)
			{
				client.Context.Properties.Add(text, configuration.AdditionalProperties[text]);
			}
			client.Context.Component.Version = configuration.Build;
			client.Context.Properties.Add("Build", configuration.Build);
			client.Context.Properties.Add("Product", configuration.Product);
			client.Context.Properties.Add("IsExternalUser", (!TelemetryUtils.IsInternal()).ToString());
			client.Context.Properties.Add("MachineID", TelemetryUtils.GetMachineId());
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021B4 File Offset: 0x000003B4
		public async Task TrackOwinRequestAsync(OwinMiddleware next, IOwinContext context)
		{
			if (this._telemetryServiceConfiguration.IsTelemetryEnabled)
			{
				Stopwatch stopWatch = new Stopwatch();
				DateTime startTime = DateTime.Now;
				stopWatch.Start();
				await next.Invoke(context);
				stopWatch.Stop();
				try
				{
					string text = "UnAuthenticaded";
					if (context.Authentication.User != null)
					{
						text = context.Authentication.User.Identity.Name;
					}
					RequestTelemetry requestTelemetry = new RequestTelemetry(TelemetryUtils.NormalizeName(context.Request), startTime, stopWatch.Elapsed, context.Response.StatusCode.ToString(), context.Response.StatusCode == 200);
					requestTelemetry.HttpMethod = context.Request.Method;
					requestTelemetry.Properties.Add("UserID", TelemetryUtils.GetSHA256Hash(text));
					this._client.TrackRequest(requestTelemetry);
				}
				catch (Exception)
				{
				}
				stopWatch = null;
			}
			else
			{
				await next.Invoke(context);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002209 File Offset: 0x00000409
		public void FlushRequests()
		{
			if (this.IsEnabled())
			{
				this._client.Flush();
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000221E File Offset: 0x0000041E
		public bool IsEnabled()
		{
			return this._telemetryServiceConfiguration.IsTelemetryEnabled;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000222B File Offset: 0x0000042B
		public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
		{
			if (this.IsEnabled())
			{
				this._client.TrackEvent(eventName, properties, metrics);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002243 File Offset: 0x00000443
		public void TrackTrace(string trace)
		{
			if (this.IsEnabled())
			{
				this._client.TrackTrace(trace);
			}
		}

		// Token: 0x04000028 RID: 40
		private static ITelemetryService _current = TelemetrySimulator.Instance;

		// Token: 0x04000029 RID: 41
		private const string EndpointAddress = "https://vortex.data.microsoft.com/collect/v1";

		// Token: 0x0400002A RID: 42
		private readonly TelemetryClient _client;

		// Token: 0x0400002B RID: 43
		private readonly ITelemetryServiceConfiguration _telemetryServiceConfiguration;
	}
}
