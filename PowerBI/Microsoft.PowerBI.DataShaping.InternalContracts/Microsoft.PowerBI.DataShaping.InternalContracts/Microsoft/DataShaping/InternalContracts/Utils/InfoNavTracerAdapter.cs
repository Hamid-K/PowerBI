using System;
using System.Diagnostics;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;

namespace Microsoft.DataShaping.InternalContracts.Utils
{
	// Token: 0x02000021 RID: 33
	internal class InfoNavTracerAdapter : Microsoft.InfoNav.ITracer
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x000034A3 File Offset: 0x000016A3
		public InfoNavTracerAdapter(Microsoft.DataShaping.ServiceContracts.ITracer dseTracer)
		{
			this._dseTracer = dseTracer;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000034B2 File Offset: 0x000016B2
		public bool ShouldTrace(TraceLevel level)
		{
			return true;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000034B5 File Offset: 0x000016B5
		public void TraceFatal(string message)
		{
			this._dseTracer.Trace(TraceLevel.Error, message);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000034C4 File Offset: 0x000016C4
		public void TraceFatal(string format, object arg0)
		{
			this._dseTracer.Trace(TraceLevel.Error, format, arg0);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000034D4 File Offset: 0x000016D4
		public void TraceFatal(string format, object arg0, object arg1)
		{
			this._dseTracer.Trace(TraceLevel.Error, format, arg0, arg1);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000034E5 File Offset: 0x000016E5
		public void TraceFatal(string format, object arg0, object arg1, object arg2)
		{
			this._dseTracer.Trace(TraceLevel.Error, format, arg0, arg1, arg2);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000034F8 File Offset: 0x000016F8
		public void TraceFatal(string format, object arg0, object arg1, object arg2, object arg3)
		{
			this._dseTracer.Trace(TraceLevel.Error, format, arg0, arg1, arg2, arg3);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000350D File Offset: 0x0000170D
		public void TraceError(string message)
		{
			this._dseTracer.Trace(TraceLevel.Error, message);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000351C File Offset: 0x0000171C
		public void TraceError(string format, object arg0)
		{
			this._dseTracer.Trace(TraceLevel.Error, format, arg0);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000352C File Offset: 0x0000172C
		public void TraceError(string format, object arg0, object arg1)
		{
			this._dseTracer.Trace(TraceLevel.Error, format, arg0, arg1);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000353D File Offset: 0x0000173D
		public void TraceError(string format, object arg0, object arg1, object arg2)
		{
			this._dseTracer.Trace(TraceLevel.Error, format, arg0, arg1, arg2);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003550 File Offset: 0x00001750
		public void TraceError(string format, object arg0, object arg1, object arg2, object arg3)
		{
			this._dseTracer.Trace(TraceLevel.Error, format, arg0, arg1, arg2, arg3);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003565 File Offset: 0x00001765
		public void TraceWarning(string message)
		{
			this._dseTracer.Trace(TraceLevel.Error, message);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003574 File Offset: 0x00001774
		public void TraceWarning(string format, object arg0)
		{
			this._dseTracer.Trace(TraceLevel.Warning, format, arg0);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003584 File Offset: 0x00001784
		public void TraceWarning(string format, object arg0, object arg1)
		{
			this._dseTracer.Trace(TraceLevel.Warning, format, arg0);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003594 File Offset: 0x00001794
		public void TraceWarning(string format, object arg0, object arg1, object arg2)
		{
			this._dseTracer.Trace(TraceLevel.Warning, format, arg0, arg1, arg2);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000035A7 File Offset: 0x000017A7
		public void TraceWarning(string format, object arg0, object arg1, object arg2, object arg3)
		{
			this._dseTracer.Trace(TraceLevel.Warning, format, arg0, arg1, arg2, arg3);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000035BC File Offset: 0x000017BC
		public void TraceInformation(string message)
		{
			this._dseTracer.Trace(TraceLevel.Info, message);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000035CB File Offset: 0x000017CB
		public void TraceInformation(string format, object arg0)
		{
			this._dseTracer.Trace(TraceLevel.Info, format, arg0);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000035DB File Offset: 0x000017DB
		public void TraceInformation(string format, object arg0, object arg1)
		{
			this._dseTracer.Trace(TraceLevel.Info, format, arg0, arg1);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000035EC File Offset: 0x000017EC
		public void TraceInformation(string format, object arg0, object arg1, object arg2)
		{
			this._dseTracer.Trace(TraceLevel.Info, format, arg0, arg1, arg2);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000035FF File Offset: 0x000017FF
		public void TraceInformation(string format, object arg0, object arg1, object arg2, object arg3)
		{
			this._dseTracer.Trace(TraceLevel.Info, format, arg0, arg1, arg2, arg3);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003614 File Offset: 0x00001814
		public void TraceVerbose(string message)
		{
			this._dseTracer.Trace(TraceLevel.Verbose, message);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003623 File Offset: 0x00001823
		public void TraceVerbose(string format, object arg0)
		{
			this._dseTracer.Trace(TraceLevel.Verbose, format, arg0);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003633 File Offset: 0x00001833
		public void TraceVerbose(string format, object arg0, object arg1)
		{
			this._dseTracer.Trace(TraceLevel.Verbose, format, arg0, arg1);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003644 File Offset: 0x00001844
		public void TraceVerbose(string format, object arg0, object arg1, object arg2)
		{
			this._dseTracer.Trace(TraceLevel.Verbose, format, arg0, arg1, arg2);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003657 File Offset: 0x00001857
		public void TraceVerbose(string format, object arg0, object arg1, object arg2, object arg3)
		{
			this._dseTracer.Trace(TraceLevel.Verbose, format, arg0, arg1, arg2, arg3);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000366C File Offset: 0x0000186C
		public void SanitizedTrace(TraceLevel level, string format, params string[] args)
		{
			this._dseTracer.SanitizedTrace(level, format, args);
		}

		// Token: 0x0400005F RID: 95
		private readonly Microsoft.DataShaping.ServiceContracts.ITracer _dseTracer;
	}
}
