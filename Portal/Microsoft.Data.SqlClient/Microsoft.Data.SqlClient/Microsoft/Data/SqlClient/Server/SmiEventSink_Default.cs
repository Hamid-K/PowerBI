using System;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200011E RID: 286
	internal class SmiEventSink_Default : SmiEventSink
	{
		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x000021D8 File Offset: 0x000003D8
		internal virtual string ServerVersion
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x000605FC File Offset: 0x0005E7FC
		internal SmiEventSink_Default()
		{
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x00060604 File Offset: 0x0005E804
		internal bool HasMessages
		{
			get
			{
				SmiEventSink_Default smiEventSink_Default = (SmiEventSink_Default)this._parent;
				if (smiEventSink_Default != null)
				{
					return smiEventSink_Default.HasMessages;
				}
				return this._errors != null || this._warnings != null;
			}
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00060640 File Offset: 0x0005E840
		protected virtual void DispatchMessages(bool ignoreNonFatalMessages)
		{
			SmiEventSink_Default smiEventSink_Default = (SmiEventSink_Default)this._parent;
			if (smiEventSink_Default != null)
			{
				smiEventSink_Default.DispatchMessages(ignoreNonFatalMessages);
				return;
			}
			SqlException ex = this.ProcessMessages(true, ignoreNonFatalMessages);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00060674 File Offset: 0x0005E874
		protected SqlException ProcessMessages(bool ignoreWarnings, bool ignoreNonFatalMessages)
		{
			SqlException ex = null;
			SqlErrorCollection sqlErrorCollection = null;
			if (this._errors != null)
			{
				if (ignoreNonFatalMessages)
				{
					sqlErrorCollection = new SqlErrorCollection();
					foreach (object obj in this._errors)
					{
						SqlError sqlError = (SqlError)obj;
						if (sqlError.Class >= 20)
						{
							sqlErrorCollection.Add(sqlError);
						}
					}
					if (sqlErrorCollection.Count <= 0)
					{
						sqlErrorCollection = null;
					}
				}
				else
				{
					if (this._warnings != null)
					{
						foreach (object obj2 in this._warnings)
						{
							SqlError sqlError2 = (SqlError)obj2;
							this._errors.Add(sqlError2);
						}
					}
					sqlErrorCollection = this._errors;
				}
				this._errors = null;
				this._warnings = null;
			}
			else
			{
				if (!ignoreWarnings)
				{
					sqlErrorCollection = this._warnings;
				}
				this._warnings = null;
			}
			if (sqlErrorCollection != null)
			{
				ex = SqlException.CreateException(sqlErrorCollection, this.ServerVersion);
			}
			return ex;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00060798 File Offset: 0x0005E998
		internal void ProcessMessagesAndThrow()
		{
			this.ProcessMessagesAndThrow(false);
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x000607A1 File Offset: 0x0005E9A1
		private SqlErrorCollection Errors
		{
			get
			{
				if (this._errors == null)
				{
					this._errors = new SqlErrorCollection();
				}
				return this._errors;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x000607BC File Offset: 0x0005E9BC
		// (set) Token: 0x0600168A RID: 5770 RVA: 0x000607C4 File Offset: 0x0005E9C4
		internal SmiEventSink Parent
		{
			get
			{
				return this._parent;
			}
			set
			{
				this._parent = value;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x000607CD File Offset: 0x0005E9CD
		private SqlErrorCollection Warnings
		{
			get
			{
				if (this._warnings == null)
				{
					this._warnings = new SqlErrorCollection();
				}
				return this._warnings;
			}
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x000607E8 File Offset: 0x0005E9E8
		internal void ProcessMessagesAndThrow(bool ignoreNonFatalMessages)
		{
			if (this.HasMessages)
			{
				this.DispatchMessages(ignoreNonFatalMessages);
			}
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x000607FC File Offset: 0x0005E9FC
		internal void CleanMessages()
		{
			SmiEventSink_Default smiEventSink_Default = (SmiEventSink_Default)this._parent;
			if (smiEventSink_Default != null)
			{
				smiEventSink_Default.CleanMessages();
				return;
			}
			this._errors = null;
			this._warnings = null;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0006082D File Offset: 0x0005EA2D
		internal SmiEventSink_Default(SmiEventSink parent)
		{
			this._parent = parent;
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0006083C File Offset: 0x0005EA3C
		internal override void BatchCompleted()
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.BatchCompleted);
			}
			this._parent.BatchCompleted();
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00060858 File Offset: 0x0005EA58
		internal override void ParametersAvailable(SmiParameterMetaData[] metaData, ITypedGettersV3 paramValues)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.ParametersAvailable);
			}
			this._parent.ParametersAvailable(metaData, paramValues);
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x00060876 File Offset: 0x0005EA76
		internal override void ParameterAvailable(SmiParameterMetaData metaData, SmiTypedGetterSetter paramValue, int ordinal)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.ParameterAvailable);
			}
			this._parent.ParameterAvailable(metaData, paramValue, ordinal);
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00060895 File Offset: 0x0005EA95
		internal override void DefaultDatabaseChanged(string databaseName)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.DefaultDatabaseChanged);
			}
			this._parent.DefaultDatabaseChanged(databaseName);
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x000608B4 File Offset: 0x0005EAB4
		internal override void MessagePosted(int number, byte state, byte errorClass, string server, string message, string procedure, int lineNumber)
		{
			if (this._parent != null)
			{
				this._parent.MessagePosted(number, state, errorClass, server, message, procedure, lineNumber);
				return;
			}
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int, byte, byte, string, string, string, int>("<sc.SmiEventSink_Default.MessagePosted|ADV> {0}, number={1} state={2} errorClass={3} server='{4}' message='{5}' procedure='{6}' linenumber={7}.", 0, number, state, errorClass, server, message, procedure, lineNumber);
			SqlError sqlError = new SqlError(number, state, errorClass, server, message, procedure, lineNumber, null);
			if (sqlError.Class < 11)
			{
				this.Warnings.Add(sqlError);
				return;
			}
			this.Errors.Add(sqlError);
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00060930 File Offset: 0x0005EB30
		internal override void MetaDataAvailable(SmiQueryMetaData[] metaData, bool nextEventIsRow)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.MetaDataAvailable);
			}
			this._parent.MetaDataAvailable(metaData, nextEventIsRow);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0006094E File Offset: 0x0005EB4E
		internal override void RowAvailable(ITypedGetters rowData)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.RowAvailable);
			}
			this._parent.RowAvailable(rowData);
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0006096B File Offset: 0x0005EB6B
		internal override void RowAvailable(ITypedGettersV3 rowData)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.RowAvailable);
			}
			this._parent.RowAvailable(rowData);
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x00060988 File Offset: 0x0005EB88
		internal override void StatementCompleted(int rowsAffected)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.StatementCompleted);
			}
			this._parent.StatementCompleted(rowsAffected);
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x000609A5 File Offset: 0x0005EBA5
		internal override void TransactionCommitted(long transactionId)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.TransactionCommitted);
			}
			this._parent.TransactionCommitted(transactionId);
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x000609C3 File Offset: 0x0005EBC3
		internal override void TransactionDefected(long transactionId)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.TransactionDefected);
			}
			this._parent.TransactionDefected(transactionId);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x000609E1 File Offset: 0x0005EBE1
		internal override void TransactionEnlisted(long transactionId)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.TransactionEnlisted);
			}
			this._parent.TransactionEnlisted(transactionId);
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x000609FF File Offset: 0x0005EBFF
		internal override void TransactionEnded(long transactionId)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.TransactionEnded);
			}
			this._parent.TransactionEnded(transactionId);
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00060A1D File Offset: 0x0005EC1D
		internal override void TransactionRolledBack(long transactionId)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.TransactionRolledBack);
			}
			this._parent.TransactionRolledBack(transactionId);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00060A3B File Offset: 0x0005EC3B
		internal override void TransactionStarted(long transactionId)
		{
			if (this._parent == null)
			{
				throw SQL.UnexpectedSmiEvent(SmiEventSink_Default.UnexpectedEventType.TransactionStarted);
			}
			this._parent.TransactionStarted(transactionId);
		}

		// Token: 0x04000902 RID: 2306
		private SqlErrorCollection _errors;

		// Token: 0x04000903 RID: 2307
		private SqlErrorCollection _warnings;

		// Token: 0x04000904 RID: 2308
		private SmiEventSink _parent;

		// Token: 0x02000267 RID: 615
		internal enum UnexpectedEventType
		{
			// Token: 0x0400170C RID: 5900
			BatchCompleted,
			// Token: 0x0400170D RID: 5901
			ColumnInfoAvailable,
			// Token: 0x0400170E RID: 5902
			DefaultDatabaseChanged,
			// Token: 0x0400170F RID: 5903
			MessagePosted,
			// Token: 0x04001710 RID: 5904
			MetaDataAvailable,
			// Token: 0x04001711 RID: 5905
			ParameterAvailable,
			// Token: 0x04001712 RID: 5906
			ParametersAvailable,
			// Token: 0x04001713 RID: 5907
			RowAvailable,
			// Token: 0x04001714 RID: 5908
			StatementCompleted,
			// Token: 0x04001715 RID: 5909
			TableNameAvailable,
			// Token: 0x04001716 RID: 5910
			TransactionCommitted,
			// Token: 0x04001717 RID: 5911
			TransactionDefected,
			// Token: 0x04001718 RID: 5912
			TransactionEnlisted,
			// Token: 0x04001719 RID: 5913
			TransactionEnded,
			// Token: 0x0400171A RID: 5914
			TransactionRolledBack,
			// Token: 0x0400171B RID: 5915
			TransactionStarted
		}
	}
}
