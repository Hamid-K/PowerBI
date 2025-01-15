using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.Drda.DDM;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.DrdaClient;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x02000981 RID: 2433
	internal class Supervisor : Manager
	{
		// Token: 0x06004B57 RID: 19287 RVA: 0x0012B988 File Offset: 0x00129B88
		public Supervisor(Requester requester)
			: base(requester)
		{
			this._tracePoint = new SupervisorTracePoint(requester.TracePoint);
			this._managerCodepoint = ManagerCodePoint.SUPERVISOR;
			string text = this._requester.ConnectionInfo[25];
			if (!string.IsNullOrWhiteSpace(text))
			{
				this._extnam = text.Trim() + "-";
			}
			this._extnam = this._extnam + Requester.ProcessName + "-MSDRDACLIENT";
			this._srvnam = Dns.GetHostName().ToUpperInvariant();
		}

		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x06004B58 RID: 19288 RVA: 0x0012BA26 File Offset: 0x00129C26
		public string Srvclsnm
		{
			get
			{
				return this._srvclsnm;
			}
		}

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x06004B59 RID: 19289 RVA: 0x0012BA2E File Offset: 0x00129C2E
		public string Srvrlslv
		{
			get
			{
				return this._srvrlslv;
			}
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x0012BA38 File Offset: 0x00129C38
		public int GetManagerLevel(ManagerCodePoint managerCodepoint)
		{
			int num = 0;
			if (this._managerLevels != null)
			{
				this._managerLevels.TryGetValue(managerCodepoint, out num);
			}
			return num;
		}

		// Token: 0x06004B5B RID: 19291 RVA: 0x0012BA60 File Offset: 0x00129C60
		public override void Reset()
		{
			if (this._requester.TracePoint != null && this._tracePoint.TraceContainer != this._requester.TracePoint.TraceContainer)
			{
				this._tracePoint = new SupervisorTracePoint(this._requester.TracePoint);
			}
			this._srvrlslv = "V.01";
			this._srvclsnm = "MSDRDACLIENT";
			string text = this._requester.ConnectionInfo[25];
			if (!string.IsNullOrWhiteSpace(text))
			{
				this._extnam = text.Trim() + "-";
			}
			else
			{
				this._extnam = "";
			}
			this._extnam = this._extnam + Requester.ProcessName + "-MSDRDACLIENT";
			this._srvnam = Dns.GetHostName().ToUpperInvariant();
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x0012BB28 File Offset: 0x00129D28
		public async Task SubmitExcsatAsync(bool isAsync, bool checkReturn, CancellationToken cancellationToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter Supervisor::SubmitExcsatAsync");
			}
			EXCSAT excsat = new EXCSAT();
			excsat.TracePoint = this._requester.CommonTracePoint;
			excsat.Extnam = this._extnam;
			if (this._requester.IsIMSDB)
			{
				excsat.Srvclsnm = "DFS";
			}
			else
			{
				excsat.Srvclsnm = this._srvclsnm;
			}
			excsat.Srvnam = this._srvnam;
			if (!this._requester.IsDb2Gateway)
			{
				excsat.Srvrlslv = this._srvrlslv;
			}
			else
			{
				excsat.Srvrlslv = "SQL11010";
			}
			if (!this._requester.IsIMSDB)
			{
				excsat.MgrLvlls[ManagerCodePoint.AGENT] = 7;
				excsat.MgrLvlls[ManagerCodePoint.RDB] = 7;
			}
			if (this._requester.CcsidHost == null || (this._requester.CcsidHost._ccsiddbc == 1200 && this._requester.CcsidHost._ccsidsbc == 1208 && this._requester.CcsidHost._ccsidmbc == 1208) || this._requester.IsDb2Gateway)
			{
				excsat.MgrLvlls[ManagerCodePoint.UNICODEMGR] = 1208;
			}
			if (!this._requester.IsIMSDB)
			{
				excsat.MgrLvlls[this._requester.ConnectionManager.ManagerCodePoint] = this._requester.ConnectionManager.Level;
				excsat.MgrLvlls[this._requester.SecurityManager.ManagerCodePoint] = this._requester.SecurityManager.Level;
				excsat.MgrLvlls[this._requester.SqlManager.ManagerCodePoint] = this._requester.SqlManager.Level;
				if (this._requester.IsDuw)
				{
					excsat.MgrLvlls[ManagerCodePoint.XAMGR] = 7;
				}
			}
			string errorMessage = null;
			try
			{
				await this._requester.ConnectionManager.DdmWriter.FlushAsync(isAsync, cancellationToken);
				if (!checkReturn)
				{
					await excsat.WriteRequestDssAsync(this._requester.ConnectionManager.DdmWriter, 1, 64, isAsync, cancellationToken);
				}
				else
				{
					await excsat.WriteRequestDssAsync(this._requester.ConnectionManager.DdmWriter, 1, 0, isAsync, cancellationToken);
					do
					{
						CodePoint currentCP = await this._requester.ConnectionManager.DdmReader.MoveToNextDdmAsync(isAsync, cancellationToken);
						if (this._tracePoint.IsEnabled(TraceFlags.Information))
						{
							this._tracePoint.Trace(TraceFlags.Information, "Receiving codepoint: " + currentCP.ToString());
						}
						EXCSATRD excsatrd;
						if (currentCP == CodePoint.EXCSATRD)
						{
							if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
							{
								this._tracePoint.Trace(TraceFlags.Verbose, "Processing EXCSATRD");
							}
							excsatrd = new EXCSATRD();
							excsatrd.TracePoint = this._requester.CommonTracePoint;
							await excsatrd.ReadAsync(this._requester.ConnectionManager.DdmReader, isAsync, cancellationToken);
							this._srvclsnm = excsatrd.Srvclsnm;
							this._srvrlslv = excsatrd.Srvrlslv;
							this._managerLevels = excsatrd.MgrLvlls;
							this._extnam = excsatrd.Extnam;
						}
						else
						{
							if (this._tracePoint.IsEnabled(TraceFlags.Warning))
							{
								this._tracePoint.Trace(TraceFlags.Warning, "Supervisor::SubmitExcsatAsync(): Read CodePoint not supported: " + currentCP.ToString());
							}
							await this._requester.ConnectionManager.DdmReader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
							errorMessage = RequesterResource.NoExpectedCodepoint(currentCP);
						}
						excsatrd = null;
					}
					while (base.NeedReadMoreDdmCodepoint(1));
				}
			}
			catch (Exception ex)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "Supervisor::SubmitExcsatAsync(), failed to exchange server attributes: " + ex.ToString());
				}
				if (this._requester.ConnectionManager.DdmWriter.Offset > 0)
				{
					this._requester.ConnectionManager.DdmWriter.Reset();
				}
				throw this._requester.MakeException(ex.Message, "HY000", -1040, ex.HResult);
			}
			if (!string.IsNullOrEmpty(errorMessage))
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "Supervisor::SubmitExcsatAsync(), " + errorMessage);
				}
				throw this._requester.MakeException(errorMessage, "HY000", -1040);
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this._tracePoint.Trace(TraceFlags.Verbose, "Processed: EXCSATRD");
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit Supervisor::SubmitExcsatAsync");
			}
		}

		// Token: 0x04003B47 RID: 15175
		private const string ServerReleaseLevel = "V.01";

		// Token: 0x04003B48 RID: 15176
		private const string ServerClassName = "MSDRDACLIENT";

		// Token: 0x04003B49 RID: 15177
		private string _extnam;

		// Token: 0x04003B4A RID: 15178
		public string _srvclsnm = "MSDRDACLIENT";

		// Token: 0x04003B4B RID: 15179
		public string _srvnam;

		// Token: 0x04003B4C RID: 15180
		public string _srvrlslv = "V.01";

		// Token: 0x04003B4D RID: 15181
		public Dictionary<ManagerCodePoint, int> _managerLevels;
	}
}
