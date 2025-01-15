using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Identity.Client.TelemetryCore;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000165 RID: 357
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Telemetry is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ", true)]
	public class Telemetry : ITelemetryReceiver
	{
		// Token: 0x06001182 RID: 4482 RVA: 0x0003BE89 File Offset: 0x0003A089
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Telemetry is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ", true)]
		public static Telemetry GetInstance()
		{
			throw new NotImplementedException("Telemetry is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ");
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x0003BE95 File Offset: 0x0003A095
		// (set) Token: 0x06001184 RID: 4484 RVA: 0x0003BEA1 File Offset: 0x0003A0A1
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Telemetry is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ", true)]
		public bool TelemetryOnFailureOnly
		{
			get
			{
				throw new NotImplementedException("Telemetry is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ");
			}
			set
			{
				throw new NotImplementedException("Telemetry is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ");
			}
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0003BEAD File Offset: 0x0003A0AD
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Telemetry is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ", true)]
		public void RegisterReceiver(Telemetry.Receiver r)
		{
			throw new NotImplementedException("Telemetry is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ");
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0003BEB9 File Offset: 0x0003A0B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Telemetry is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ", true)]
		public bool HasRegisteredReceiver()
		{
			throw new NotImplementedException("Telemetry is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ");
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0003BEC5 File Offset: 0x0003A0C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Telemetry is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ", true)]
		void ITelemetryReceiver.HandleTelemetryEvents(List<Dictionary<string, string>> events)
		{
			throw new NotImplementedException("Telemetry is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ");
		}

		// Token: 0x020003DA RID: 986
		// (Invoke) Token: 0x06001E24 RID: 7716
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Telemetry is now specified per ClientApplication.  See https://aka.ms/msal-net-3-breaking-changes and https://aka.ms/msal-net-application-configuration. ", true)]
		public delegate void Receiver(List<Dictionary<string, string>> events);
	}
}
