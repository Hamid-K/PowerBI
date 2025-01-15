using System;
using System.Globalization;
using System.Reflection;
using Microsoft.HostIntegration.Common;
using Microsoft.HostIntegration.EventLogging.MqClient;
using Microsoft.HostIntegration.MqClient.StrictResources.Globals;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000BCF RID: 3023
	public class Globals
	{
		// Token: 0x06005DCC RID: 24012 RVA: 0x0017FA10 File Offset: 0x0017DC10
		public static IPooling GetIPooling()
		{
			if (Microsoft.HostIntegration.MqClient.Globals.iPooling != null)
			{
				return Microsoft.HostIntegration.MqClient.Globals.iPooling;
			}
			object obj = Microsoft.HostIntegration.MqClient.Globals.lockObject;
			lock (obj)
			{
				if (Microsoft.HostIntegration.MqClient.Globals.iPooling != null)
				{
					return Microsoft.HostIntegration.MqClient.Globals.iPooling;
				}
				Microsoft.HostIntegration.MqClient.Globals.iPooling = Activator.CreateInstance(Microsoft.HostIntegration.Common.Globals.GetType("Microsoft.HostIntegration.MqClient.Automatons.Pooling, " + new AssemblyName(Assembly.GetExecutingAssembly().ToString())
				{
					Name = "Microsoft.HostIntegration.MqClient.Automatons"
				}.FullName)) as IPooling;
				Microsoft.HostIntegration.MqClient.Globals.iPooling.EventLogContainer = Microsoft.HostIntegration.MqClient.Globals.eventLogContainer;
			}
			return Microsoft.HostIntegration.MqClient.Globals.iPooling;
		}

		// Token: 0x06005DCE RID: 24014 RVA: 0x0017FAD2 File Offset: 0x0017DCD2
		public static string CheckMaximumLengthTrimmedNonNull(string text, string property, int maximumLength)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new ArgumentNullException(property);
			}
			string text2 = text.Trim();
			if (text2.Length > maximumLength)
			{
				throw new ArgumentOutOfRangeException(property, Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.MaximumLength(maximumLength));
			}
			return text2;
		}

		// Token: 0x06005DCF RID: 24015 RVA: 0x0017FB04 File Offset: 0x0017DD04
		public static string CheckMaximumLengthTrimmedNonNullTrace(string text, string property, int maximumLength, FlagBasedTracePoint tracePoint)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				ArgumentNullException ex = new ArgumentNullException(property);
				if (tracePoint.IsEnabled(TraceFlags.Error))
				{
					tracePoint.Trace(TraceFlags.Error, ex);
				}
				throw ex;
			}
			string text2 = text.Trim();
			if (text2.Length > maximumLength)
			{
				ArgumentOutOfRangeException ex2 = new ArgumentOutOfRangeException(property, Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.MaximumLength(maximumLength));
				if (tracePoint.IsEnabled(TraceFlags.Error))
				{
					tracePoint.Trace(TraceFlags.Error, ex2);
				}
				throw ex2;
			}
			return text2;
		}

		// Token: 0x06005DD0 RID: 24016 RVA: 0x0017FB68 File Offset: 0x0017DD68
		public static string CheckMaximumLengthTrimmed(string text, string property, int maximumLength)
		{
			string text2 = null;
			if (!string.IsNullOrWhiteSpace(text))
			{
				string text3 = text.Trim();
				if (text3.Length > maximumLength)
				{
					throw new ArgumentOutOfRangeException(property, Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.MaximumLength(maximumLength));
				}
				text2 = text3;
			}
			return text2;
		}

		// Token: 0x06005DD1 RID: 24017 RVA: 0x0017FBA2 File Offset: 0x0017DDA2
		public static string CheckMaximumLength(string text, string property, int maximumLength)
		{
			if (text != null && text.Length > maximumLength)
			{
				throw new ArgumentOutOfRangeException(property, Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.MaximumLength(maximumLength));
			}
			return text;
		}

		// Token: 0x06005DD2 RID: 24018 RVA: 0x0017FBC3 File Offset: 0x0017DDC3
		public static byte[] CheckExactLength(byte[] bytes, string property, int requiredLength)
		{
			if (bytes != null && bytes.Length != requiredLength)
			{
				throw new ArgumentOutOfRangeException(property, Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.ExactLength(requiredLength));
			}
			return bytes;
		}

		// Token: 0x06005DD3 RID: 24019 RVA: 0x0017FBE4 File Offset: 0x0017DDE4
		public static string CheckDateFormat(string text, string property, string format)
		{
			string text2 = null;
			if (text != null)
			{
				if (text.Length != format.Length)
				{
					throw new ArgumentOutOfRangeException(property, Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.ExactLength(format.Length));
				}
				DateTime dateTime;
				if (!DateTime.TryParseExact(text, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
				{
					throw new ArgumentOutOfRangeException(property);
				}
				text2 = text;
			}
			return text2;
		}

		// Token: 0x06005DD4 RID: 24020 RVA: 0x0017FC38 File Offset: 0x0017DE38
		public static string CheckTimeFormat(string text, string property, string format)
		{
			string text2 = null;
			if (text != null)
			{
				if (text.Length != format.Length)
				{
					throw new ArgumentOutOfRangeException(property, Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.ExactLength(format.Length));
				}
				string text3 = text;
				if (text[4] == '6')
				{
					if (text[5] != '0' && text[5] != '1')
					{
						throw new ArgumentOutOfRangeException(property);
					}
					text3 = text.Substring(0, 4) + "59" + text.Substring(6);
				}
				DateTime dateTime;
				if (!DateTime.TryParseExact(text3, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
				{
					throw new ArgumentOutOfRangeException(property);
				}
				text2 = text;
			}
			return text2;
		}

		// Token: 0x06005DD5 RID: 24021 RVA: 0x0017FCD1 File Offset: 0x0017DED1
		public static int CheckRange(int newValue, string property, int lowerBound, int upperBound)
		{
			if (newValue < lowerBound || newValue > upperBound)
			{
				throw new ArgumentOutOfRangeException(property, Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.ValueRange(lowerBound, upperBound));
			}
			return newValue;
		}

		// Token: 0x06005DD6 RID: 24022 RVA: 0x0017FCF4 File Offset: 0x0017DEF4
		public static int CheckRangeTrace(int newValue, string property, int lowerBound, int upperBound, FlagBasedTracePoint tracePoint)
		{
			if (newValue < lowerBound || newValue > upperBound)
			{
				ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(property, Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.ValueRange(lowerBound, upperBound));
				if (tracePoint.IsEnabled(TraceFlags.Error))
				{
					tracePoint.Trace(TraceFlags.Error, ex);
				}
				throw ex;
			}
			return newValue;
		}

		// Token: 0x04004FB2 RID: 20402
		private static IPooling iPooling;

		// Token: 0x04004FB3 RID: 20403
		private static object lockObject = new object();

		// Token: 0x04004FB4 RID: 20404
		private static MqClientEventLogContainer eventLogContainer = new MqClientEventLogContainer();
	}
}
