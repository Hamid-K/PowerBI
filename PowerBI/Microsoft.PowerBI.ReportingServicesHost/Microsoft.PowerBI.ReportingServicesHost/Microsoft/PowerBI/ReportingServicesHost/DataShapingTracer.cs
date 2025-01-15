using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.DataExtension.Contracts.Hosting;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200003B RID: 59
	internal sealed class DataShapingTracer : Microsoft.DataShaping.ServiceContracts.ITracer, Microsoft.PowerBI.DataExtension.Contracts.Hosting.ITracer
	{
		// Token: 0x0600013B RID: 315 RVA: 0x000049DF File Offset: 0x00002BDF
		private DataShapingTracer()
		{
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000049E7 File Offset: 0x00002BE7
		public void Trace(TraceLevel level, string message)
		{
			this.InternalTrace(level, message);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000049F1 File Offset: 0x00002BF1
		public void Trace(TraceLevel level, string format, object arg0)
		{
			this.InternalTrace(level, string.Format(CultureInfo.InvariantCulture, format, arg0));
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004A06 File Offset: 0x00002C06
		public void Trace(TraceLevel level, string format, object arg0, object arg1)
		{
			this.InternalTrace(level, string.Format(CultureInfo.InvariantCulture, format, arg0, arg1));
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004A1D File Offset: 0x00002C1D
		public void Trace(TraceLevel level, string format, object arg0, object arg1, object arg2)
		{
			this.InternalTrace(level, string.Format(CultureInfo.InvariantCulture, format, arg0, arg1, arg2));
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004A36 File Offset: 0x00002C36
		public void Trace(TraceLevel level, string format, object arg0, object arg1, object arg2, object arg3)
		{
			this.InternalTrace(level, string.Format(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1, arg2, arg3 }));
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004A64 File Offset: 0x00002C64
		public void Trace(TraceLevel level, string format, params string[] args)
		{
			this.InternalTrace(level, string.Format(CultureInfo.InvariantCulture, format, args));
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004A86 File Offset: 0x00002C86
		public void SanitizedTrace(TraceLevel level, string message)
		{
			this.InternalSanitizedTrace(level, message);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004A90 File Offset: 0x00002C90
		public void SanitizedTrace(TraceLevel level, string format, object arg0)
		{
			this.InternalSanitizedTrace(level, string.Format(CultureInfo.InvariantCulture, format, arg0));
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004AA5 File Offset: 0x00002CA5
		public void SanitizedTrace(TraceLevel level, string format, object arg0, object arg1)
		{
			this.InternalSanitizedTrace(level, string.Format(CultureInfo.InvariantCulture, format, arg0, arg1));
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004ABC File Offset: 0x00002CBC
		public void SanitizedTrace(TraceLevel level, string format, object arg0, object arg1, object arg2)
		{
			this.InternalSanitizedTrace(level, string.Format(CultureInfo.InvariantCulture, format, arg0, arg1, arg2));
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004AD5 File Offset: 0x00002CD5
		public void SanitizedTrace(TraceLevel level, string format, object arg0, object arg1, object arg2, object arg3)
		{
			this.InternalSanitizedTrace(level, string.Format(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1, arg2, arg3 }));
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004B04 File Offset: 0x00002D04
		public void SanitizedTrace(TraceLevel level, string format, params string[] args)
		{
			this.InternalSanitizedTrace(level, string.Format(CultureInfo.InvariantCulture, format, args));
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004B28 File Offset: 0x00002D28
		private void InternalTrace(TraceLevel level, string message)
		{
			switch (level)
			{
			case TraceLevel.Error:
				TelemetryService.Instance.TraceError(message);
				return;
			case TraceLevel.Warning:
				TelemetryService.Instance.TraceWarning(message);
				return;
			case TraceLevel.Info:
				TelemetryService.Instance.TraceInfo(message);
				return;
			case TraceLevel.Verbose:
				TelemetryService.Instance.TraceVerbose(message);
				return;
			default:
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unsupported TraceLevel type '{0}'", level.ToString()));
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004BA0 File Offset: 0x00002DA0
		private void InternalSanitizedTrace(TraceLevel level, string message)
		{
			switch (level)
			{
			case TraceLevel.Error:
				TelemetryService.Instance.TraceError(message);
				return;
			case TraceLevel.Warning:
				TelemetryService.Instance.TraceWarning(message);
				return;
			case TraceLevel.Info:
				TelemetryService.Instance.TraceInfo(message);
				return;
			case TraceLevel.Verbose:
				TelemetryService.Instance.TraceVerbose(message);
				return;
			default:
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unsupported TraceLevel type '{0}'", level.ToString()));
			}
		}

		// Token: 0x040000F5 RID: 245
		public static readonly DataShapingTracer Instance = new DataShapingTracer();
	}
}
