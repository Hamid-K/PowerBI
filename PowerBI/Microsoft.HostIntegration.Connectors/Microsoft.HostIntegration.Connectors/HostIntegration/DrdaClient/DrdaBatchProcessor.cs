using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009A4 RID: 2468
	internal sealed class DrdaBatchProcessor
	{
		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x06004C78 RID: 19576 RVA: 0x00132129 File Offset: 0x00130329
		public int RecordsAffectedByLastBatch
		{
			get
			{
				return this.recordsAffectedByLastBatch;
			}
		}

		// Token: 0x06004C79 RID: 19577 RVA: 0x00132131 File Offset: 0x00130331
		public void InitializeBatch()
		{
			if (this.currentBatch == null)
			{
				this.currentBatch = new List<DrdaCommand>();
			}
		}

		// Token: 0x06004C7A RID: 19578 RVA: 0x00132148 File Offset: 0x00130348
		public int AddToBatch(DrdaCommand command)
		{
			DrdaCommand drdaCommand = (DrdaCommand)command.Clone();
			this.currentBatch.Add(drdaCommand);
			return 0;
		}

		// Token: 0x06004C7B RID: 19579 RVA: 0x00132170 File Offset: 0x00130370
		public int ExecuteBatch()
		{
			Trace.MessageTrace("Executing batch. Batch Count {0}", this.currentBatch.Count);
			this.recordsAffectedByLastBatch = 0;
			if (this.currentBatch.Count == 0)
			{
				return 0;
			}
			try
			{
				this.ExecuteBatchInner(this.currentBatch);
			}
			catch (DrdaException ex)
			{
				Trace.MessageTrace("Exception executing batch {0}", ex.ToString());
				throw DrdaException.BatchFailed(ex.SqlCode);
			}
			Trace.MessageTrace("Done Insert batch. Records Affected {0}", this.recordsAffectedByLastBatch);
			return this.recordsAffectedByLastBatch;
		}

		// Token: 0x06004C7C RID: 19580 RVA: 0x00132208 File Offset: 0x00130408
		public void ClearBatch()
		{
			Trace.MessageVerboseTrace("Clear Batch");
			if (this.currentBatch != null)
			{
				this.currentBatch.ForEach(delegate(DrdaCommand command)
				{
					command.Dispose();
				});
				this.currentBatch.Clear();
			}
		}

		// Token: 0x06004C7D RID: 19581 RVA: 0x0013225C File Offset: 0x0013045C
		public bool GetBatchedRecordsAffected(int commandIdentifier, out int recordsAffected, out Exception error)
		{
			recordsAffected = this.recordsAffectedByLastBatch;
			error = null;
			return true;
		}

		// Token: 0x06004C7E RID: 19582 RVA: 0x0013226C File Offset: 0x0013046C
		private int ExecuteBatchInner(List<DrdaCommand> batch)
		{
			int num = 0;
			if (batch.Count == 0)
			{
				return num;
			}
			SortedDictionary<string, DrdaParameter> sortedDictionary = new SortedDictionary<string, DrdaParameter>();
			IRequester requester = null;
			string text = null;
			foreach (DrdaCommand drdaCommand in batch)
			{
				try
				{
					if (requester != drdaCommand.Requester || string.Compare(text, drdaCommand.CommandText, true) != 0)
					{
						if (drdaCommand.IsNeedPrepareParameters())
						{
							drdaCommand.Prepare();
							try
							{
								drdaCommand.StartTimeout();
								drdaCommand.PrepareParametersAsync(false, CancellationToken.None).GetAwaiter().GetResult();
							}
							finally
							{
								drdaCommand.StopTimeout();
							}
							sortedDictionary.Clear();
							foreach (object obj in drdaCommand.Parameters)
							{
								DrdaParameter drdaParameter = (DrdaParameter)obj;
								sortedDictionary.Add(drdaParameter.SourceColumn, drdaParameter);
							}
							requester = drdaCommand.Requester;
							text = drdaCommand.CommandText;
						}
					}
					else
					{
						foreach (object obj2 in drdaCommand.Parameters)
						{
							DrdaParameter drdaParameter2 = (DrdaParameter)obj2;
							DrdaParameter drdaParameter3 = sortedDictionary[drdaParameter2.SourceColumn];
							drdaParameter2.Direction = drdaParameter3.Direction;
							drdaParameter2.Scale = drdaParameter3.Scale;
							drdaParameter2.Precision = drdaParameter3.Precision;
							drdaParameter2.Size = drdaParameter3.Size;
							drdaParameter2.IsNullable = drdaParameter3.IsNullable;
							drdaParameter2.DrdaType = drdaParameter3.DrdaType;
						}
					}
					this.recordsAffectedByLastBatch += drdaCommand.ExecuteNonQuery();
					drdaCommand.Dispose();
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			batch.Clear();
			return num;
		}

		// Token: 0x04003C7D RID: 15485
		private List<DrdaCommand> currentBatch;

		// Token: 0x04003C7E RID: 15486
		private int recordsAffectedByLastBatch;

		// Token: 0x020009A5 RID: 2469
		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		internal struct COLUMNDESCRIPTOR
		{
			// Token: 0x04003C7F RID: 15487
			private byte[] dataBuffer;

			// Token: 0x04003C80 RID: 15488
			private int indicator;
		}
	}
}
