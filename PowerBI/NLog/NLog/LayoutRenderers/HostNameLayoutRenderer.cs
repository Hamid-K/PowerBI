using System;
using System.Net;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000C9 RID: 201
	[LayoutRenderer("hostname")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public class HostNameLayoutRenderer : LayoutRenderer
	{
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0001FDA0 File Offset: 0x0001DFA0
		// (set) Token: 0x06000C68 RID: 3176 RVA: 0x0001FDA8 File Offset: 0x0001DFA8
		internal string HostName { get; private set; }

		// Token: 0x06000C69 RID: 3177 RVA: 0x0001FDB4 File Offset: 0x0001DFB4
		protected override void InitializeLayoutRenderer()
		{
			base.InitializeLayoutRenderer();
			try
			{
				this.HostName = HostNameLayoutRenderer.GetHostName();
				if (string.IsNullOrEmpty(this.HostName))
				{
					InternalLogger.Info("HostName is not available.");
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Error getting host name.");
				if (ex.MustBeRethrown())
				{
					throw;
				}
				this.HostName = string.Empty;
			}
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0001FE1C File Offset: 0x0001E01C
		private static string GetHostName()
		{
			string text;
			if ((text = HostNameLayoutRenderer.TryLookupValue(() => Environment.GetEnvironmentVariable("HOSTNAME"), "HOSTNAME")) == null)
			{
				if ((text = HostNameLayoutRenderer.TryLookupValue(() => Dns.GetHostName(), "DnsHostName")) == null)
				{
					text = HostNameLayoutRenderer.TryLookupValue(() => EnvironmentHelper.GetMachineName(), "MachineName");
				}
			}
			return text;
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0001FEAC File Offset: 0x0001E0AC
		private static string TryLookupValue(Func<string> lookupFunc, string lookupType)
		{
			string text3;
			try
			{
				string text = lookupFunc();
				string text2 = ((text != null) ? text.Trim() : null);
				text3 = (string.IsNullOrEmpty(text2) ? null : text2);
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "Failed to lookup {0}", new object[] { lookupType });
				text3 = null;
			}
			return text3;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0001FF04 File Offset: 0x0001E104
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(this.HostName);
		}
	}
}
