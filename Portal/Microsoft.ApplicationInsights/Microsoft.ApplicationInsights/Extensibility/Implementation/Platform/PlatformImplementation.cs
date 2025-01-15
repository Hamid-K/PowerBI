using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Security;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Platform
{
	// Token: 0x020000AE RID: 174
	internal class PlatformImplementation : IPlatform
	{
		// Token: 0x06000543 RID: 1347 RVA: 0x00015B40 File Offset: 0x00013D40
		public PlatformImplementation()
		{
			try
			{
				this.environmentVariables = Environment.GetEnvironmentVariables();
			}
			catch (SecurityException ex)
			{
				CoreEventSource.Log.FailedToLoadEnvironmentVariables(ex.ToString(), "Incorrect");
			}
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00015B88 File Offset: 0x00013D88
		public string ReadConfigurationXml()
		{
			string text;
			try
			{
				text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApplicationInsights.config");
			}
			catch (SecurityException)
			{
				CoreEventSource.Log.ApplicationInsightsConfigNotAccessibleWarning("Incorrect");
				return string.Empty;
			}
			try
			{
				if (File.Exists(text))
				{
					return File.ReadAllText(text);
				}
			}
			catch (FileNotFoundException)
			{
				CoreEventSource.Log.ApplicationInsightsConfigNotFoundWarning(text, "Incorrect");
			}
			catch (DirectoryNotFoundException)
			{
				CoreEventSource.Log.ApplicationInsightsConfigNotFoundWarning(text, "Incorrect");
			}
			catch (IOException)
			{
				CoreEventSource.Log.ApplicationInsightsConfigNotFoundWarning(text, "Incorrect");
			}
			catch (UnauthorizedAccessException)
			{
				CoreEventSource.Log.ApplicationInsightsConfigNotFoundWarning(text, "Incorrect");
			}
			catch (SecurityException)
			{
				CoreEventSource.Log.ApplicationInsightsConfigNotFoundWarning(text, "Incorrect");
			}
			return string.Empty;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00015C8C File Offset: 0x00013E8C
		public IDebugOutput GetDebugOutput()
		{
			IDebugOutput debugOutput;
			if ((debugOutput = this.debugOutput) == null)
			{
				debugOutput = (this.debugOutput = new TelemetryDebugWriter());
			}
			return debugOutput;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00015CB4 File Offset: 0x00013EB4
		public string GetEnvironmentVariable(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentNullException("name");
			}
			IDictionary dictionary = this.environmentVariables;
			object obj = ((dictionary != null) ? dictionary[name] : null);
			if (obj == null)
			{
				return null;
			}
			return obj.ToString();
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00015CF4 File Offset: 0x00013EF4
		public string GetMachineName()
		{
			string text;
			if ((text = this.hostName) == null)
			{
				text = (this.hostName = PlatformImplementation.GetHostName());
			}
			return text;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00015D1C File Offset: 0x00013F1C
		private static string GetHostName()
		{
			try
			{
				string domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
				string text = Dns.GetHostName();
				if (!text.EndsWith(domainName, StringComparison.OrdinalIgnoreCase))
				{
					text = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[] { text, domainName });
				}
				return text;
			}
			catch (Exception ex)
			{
				CoreEventSource.Log.FailedToGetMachineName(ex.Message, "Incorrect");
			}
			return string.Empty;
		}

		// Token: 0x04000218 RID: 536
		private readonly IDictionary environmentVariables;

		// Token: 0x04000219 RID: 537
		private IDebugOutput debugOutput;

		// Token: 0x0400021A RID: 538
		private string hostName;
	}
}
