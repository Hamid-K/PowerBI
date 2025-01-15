using System;
using System.Text;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000071 RID: 113
	internal class SqlConnectionTimeoutErrorInternal
	{
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0001B2FF File Offset: 0x000194FF
		internal SqlConnectionTimeoutErrorPhase CurrentPhase
		{
			get
			{
				return this._currentPhase;
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0001B308 File Offset: 0x00019508
		public SqlConnectionTimeoutErrorInternal()
		{
			this._phaseDurations = new SqlConnectionTimeoutPhaseDuration[9];
			for (int i = 0; i < this._phaseDurations.Length; i++)
			{
				this._phaseDurations[i] = null;
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0001B344 File Offset: 0x00019544
		public void SetFailoverScenario(bool useFailoverServer)
		{
			this._isFailoverScenario = useFailoverServer;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0001B34D File Offset: 0x0001954D
		public void SetInternalSourceType(SqlConnectionInternalSourceType sourceType)
		{
			this._currentSourceType = sourceType;
			if (this._currentSourceType == SqlConnectionInternalSourceType.RoutingDestination)
			{
				this._originalPhaseDurations = this._phaseDurations;
				this._phaseDurations = new SqlConnectionTimeoutPhaseDuration[9];
				this.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.PreLoginBegin);
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0001B380 File Offset: 0x00019580
		internal void ResetAndRestartPhase()
		{
			this._currentPhase = SqlConnectionTimeoutErrorPhase.PreLoginBegin;
			for (int i = 0; i < this._phaseDurations.Length; i++)
			{
				this._phaseDurations[i] = null;
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0001B3B0 File Offset: 0x000195B0
		internal void SetAndBeginPhase(SqlConnectionTimeoutErrorPhase timeoutErrorPhase)
		{
			this._currentPhase = timeoutErrorPhase;
			if (this._phaseDurations[(int)timeoutErrorPhase] == null)
			{
				this._phaseDurations[(int)timeoutErrorPhase] = new SqlConnectionTimeoutPhaseDuration();
			}
			this._phaseDurations[(int)timeoutErrorPhase].StartCapture();
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0001B3DD File Offset: 0x000195DD
		internal void EndPhase(SqlConnectionTimeoutErrorPhase timeoutErrorPhase)
		{
			this._phaseDurations[(int)timeoutErrorPhase].StopCapture();
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0001B3EC File Offset: 0x000195EC
		internal void SetAllCompleteMarker()
		{
			this._currentPhase = SqlConnectionTimeoutErrorPhase.Complete;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0001B3F8 File Offset: 0x000195F8
		internal string GetErrorMessage()
		{
			StringBuilder stringBuilder;
			string text;
			switch (this._currentPhase)
			{
			case SqlConnectionTimeoutErrorPhase.PreLoginBegin:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PreLogin_Begin());
				text = SQLMessage.Duration_PreLogin_Begin(this._phaseDurations[1].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.InitializeConnection:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PreLogin_InitializeConnection());
				text = SQLMessage.Duration_PreLogin_Begin(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.SendPreLoginHandshake:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PreLogin_SendHandshake());
				text = SQLMessage.Duration_PreLoginHandshake(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration(), this._phaseDurations[3].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.ConsumePreLoginHandshake:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PreLogin_ConsumeHandshake());
				text = SQLMessage.Duration_PreLoginHandshake(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration(), this._phaseDurations[3].GetMilliSecondDuration() + this._phaseDurations[4].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.LoginBegin:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_Login_Begin());
				text = SQLMessage.Duration_Login_Begin(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration(), this._phaseDurations[3].GetMilliSecondDuration() + this._phaseDurations[4].GetMilliSecondDuration(), this._phaseDurations[5].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.ProcessConnectionAuth:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_Login_ProcessConnectionAuth());
				text = SQLMessage.Duration_Login_ProcessConnectionAuth(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration(), this._phaseDurations[3].GetMilliSecondDuration() + this._phaseDurations[4].GetMilliSecondDuration(), this._phaseDurations[5].GetMilliSecondDuration(), this._phaseDurations[6].GetMilliSecondDuration());
				break;
			case SqlConnectionTimeoutErrorPhase.PostLogin:
				stringBuilder = new StringBuilder(SQLMessage.Timeout_PostLogin());
				text = SQLMessage.Duration_PostLogin(this._phaseDurations[1].GetMilliSecondDuration() + this._phaseDurations[2].GetMilliSecondDuration(), this._phaseDurations[3].GetMilliSecondDuration() + this._phaseDurations[4].GetMilliSecondDuration(), this._phaseDurations[5].GetMilliSecondDuration(), this._phaseDurations[6].GetMilliSecondDuration(), this._phaseDurations[7].GetMilliSecondDuration());
				break;
			default:
				stringBuilder = new StringBuilder(SQLMessage.Timeout());
				text = null;
				break;
			}
			if (this._currentPhase != SqlConnectionTimeoutErrorPhase.Undefined && this._currentPhase != SqlConnectionTimeoutErrorPhase.Complete)
			{
				if (this._isFailoverScenario)
				{
					stringBuilder.Append("  ");
					stringBuilder.AppendFormat(null, SQLMessage.Timeout_FailoverInfo(), this._currentSourceType);
				}
				else if (this._currentSourceType == SqlConnectionInternalSourceType.RoutingDestination)
				{
					stringBuilder.Append("  ");
					stringBuilder.AppendFormat(null, SQLMessage.Timeout_RoutingDestination(), new object[]
					{
						this._originalPhaseDurations[1].GetMilliSecondDuration() + this._originalPhaseDurations[2].GetMilliSecondDuration(),
						this._originalPhaseDurations[3].GetMilliSecondDuration() + this._originalPhaseDurations[4].GetMilliSecondDuration(),
						this._originalPhaseDurations[5].GetMilliSecondDuration(),
						this._originalPhaseDurations[6].GetMilliSecondDuration(),
						this._originalPhaseDurations[7].GetMilliSecondDuration()
					});
				}
			}
			if (text != null)
			{
				stringBuilder.Append("  ");
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400020E RID: 526
		private SqlConnectionTimeoutPhaseDuration[] _phaseDurations;

		// Token: 0x0400020F RID: 527
		private SqlConnectionTimeoutPhaseDuration[] _originalPhaseDurations;

		// Token: 0x04000210 RID: 528
		private SqlConnectionTimeoutErrorPhase _currentPhase;

		// Token: 0x04000211 RID: 529
		private SqlConnectionInternalSourceType _currentSourceType;

		// Token: 0x04000212 RID: 530
		private bool _isFailoverScenario;
	}
}
