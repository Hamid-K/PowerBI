using System;
using System.Diagnostics;
using Microsoft.InfoNav;
using Microsoft.PowerBI.Telemetry;
using Microsoft.PowerBI.Telemetry.PIIUtils;

namespace Microsoft.PowerBI.ExploreHost
{
	// Token: 0x0200002A RID: 42
	internal sealed class ExploreTracer : ITracer
	{
		// Token: 0x0600012D RID: 301 RVA: 0x000043DE File Offset: 0x000025DE
		private ExploreTracer()
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000043E6 File Offset: 0x000025E6
		public bool ShouldTrace(TraceLevel level)
		{
			return true;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000043E9 File Offset: 0x000025E9
		public void TraceFatal(string message)
		{
			this.TraceInternal(TraceType.Fatal, message);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000043F3 File Offset: 0x000025F3
		public void TraceFatal(string format, object arg0)
		{
			this.TraceFatal(PrivateInformation.FormatInvariant(format, new object[] { arg0 }, true));
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000440C File Offset: 0x0000260C
		public void TraceFatal(string format, object arg0, object arg1)
		{
			this.TraceFatal(PrivateInformation.FormatInvariant(format, new object[] { arg0, arg1 }, true));
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004429 File Offset: 0x00002629
		public void TraceFatal(string format, object arg0, object arg1, object arg2)
		{
			this.TraceFatal(PrivateInformation.FormatInvariant(format, new object[] { arg0, arg1, arg2 }, true));
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000444B File Offset: 0x0000264B
		public void TraceFatal(string format, object arg0, object arg1, object arg2, object arg3)
		{
			this.TraceFatal(PrivateInformation.FormatInvariant(format, new object[] { arg0, arg1, arg2, arg3 }, true));
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004472 File Offset: 0x00002672
		public void TraceError(string message)
		{
			this.TraceInternal(TraceType.Error, message);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000447C File Offset: 0x0000267C
		public void TraceError(string format, object arg0)
		{
			this.TraceError(PrivateInformation.FormatInvariant(format, new object[] { arg0 }, true));
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004495 File Offset: 0x00002695
		public void TraceError(string format, object arg0, object arg1)
		{
			this.TraceError(PrivateInformation.FormatInvariant(format, new object[] { arg0, arg1 }, true));
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000044B2 File Offset: 0x000026B2
		public void TraceError(string format, object arg0, object arg1, object arg2)
		{
			this.TraceError(PrivateInformation.FormatInvariant(format, new object[] { arg0, arg1, arg2 }, true));
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000044D4 File Offset: 0x000026D4
		public void TraceError(string format, object arg0, object arg1, object arg2, object arg3)
		{
			this.TraceError(PrivateInformation.FormatInvariant(format, new object[] { arg0, arg1, arg2, arg3 }, true));
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000044FB File Offset: 0x000026FB
		public void TraceWarning(string message)
		{
			this.TraceInternal(TraceType.Warning, message);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004505 File Offset: 0x00002705
		public void TraceWarning(string format, object arg0)
		{
			this.TraceWarning(PrivateInformation.FormatInvariant(format, new object[] { arg0 }, true));
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000451E File Offset: 0x0000271E
		public void TraceWarning(string format, object arg0, object arg1)
		{
			this.TraceWarning(PrivateInformation.FormatInvariant(format, new object[] { arg0, arg1 }, true));
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000453B File Offset: 0x0000273B
		public void TraceWarning(string format, object arg0, object arg1, object arg2)
		{
			this.TraceWarning(PrivateInformation.FormatInvariant(format, new object[] { arg0, arg1, arg2 }, true));
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000455D File Offset: 0x0000275D
		public void TraceWarning(string format, object arg0, object arg1, object arg2, object arg3)
		{
			this.TraceWarning(PrivateInformation.FormatInvariant(format, new object[] { arg0, arg1, arg2, arg3 }, true));
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004584 File Offset: 0x00002784
		public void TraceInformation(string message)
		{
			this.TraceInternal(TraceType.Information, message);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000458E File Offset: 0x0000278E
		public void TraceInformation(string format, object arg0)
		{
			this.TraceInformation(PrivateInformation.FormatInvariant(format, new object[] { arg0 }, true));
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000045A7 File Offset: 0x000027A7
		public void TraceInformation(string format, object arg0, object arg1)
		{
			this.TraceInformation(PrivateInformation.FormatInvariant(format, new object[] { arg0, arg1 }, true));
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000045C4 File Offset: 0x000027C4
		public void TraceInformation(string format, object arg0, object arg1, object arg2)
		{
			this.TraceInformation(PrivateInformation.FormatInvariant(format, new object[] { arg0, arg1, arg2 }, true));
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000045E6 File Offset: 0x000027E6
		public void TraceInformation(string format, object arg0, object arg1, object arg2, object arg3)
		{
			this.TraceInformation(PrivateInformation.FormatInvariant(format, new object[] { arg0, arg1, arg2, arg3 }, true));
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000460D File Offset: 0x0000280D
		public void TraceVerbose(string message)
		{
			this.TraceInternal(TraceType.Verbose, message);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004617 File Offset: 0x00002817
		public void TraceVerbose(string format, object arg0)
		{
			this.TraceVerbose(PrivateInformation.FormatInvariant(format, new object[] { arg0 }, true));
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004630 File Offset: 0x00002830
		public void TraceVerbose(string format, object arg0, object arg1)
		{
			this.TraceVerbose(PrivateInformation.FormatInvariant(format, new object[] { arg0, arg1 }, true));
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000464D File Offset: 0x0000284D
		public void TraceVerbose(string format, object arg0, object arg1, object arg2)
		{
			this.TraceVerbose(PrivateInformation.FormatInvariant(format, new object[] { arg0, arg1, arg2 }, true));
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000466F File Offset: 0x0000286F
		public void TraceVerbose(string format, object arg0, object arg1, object arg2, object arg3)
		{
			this.TraceVerbose(PrivateInformation.FormatInvariant(format, new object[] { arg0, arg1, arg2, arg3 }, true));
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004698 File Offset: 0x00002898
		public void SanitizedTrace(TraceLevel level, string format, params string[] args)
		{
			string text = PrivateInformation.FormatInvariant(format, args, true);
			this.TraceInternal(this.ConvertToTraceType(level), text);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000046BE File Offset: 0x000028BE
		private void TraceInternal(TraceType tracetype, string message)
		{
			TelemetryService.Instance.Log(new PBIWinExploreHostTrace(tracetype, message));
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000046D1 File Offset: 0x000028D1
		private TraceType ConvertToTraceType(TraceLevel level)
		{
			switch (level)
			{
			case TraceLevel.Error:
				return TraceType.Error;
			case TraceLevel.Warning:
				return TraceType.Warning;
			case TraceLevel.Info:
				return TraceType.Information;
			case TraceLevel.Verbose:
				return TraceType.Verbose;
			default:
				throw new ArgumentException(string.Format("Unexpected TraceLevel {0}", level));
			}
		}

		// Token: 0x04000099 RID: 153
		internal static readonly ExploreTracer Instance = new ExploreTracer();
	}
}
