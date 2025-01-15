using System;
using System.Diagnostics;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x0200001C RID: 28
	[DebuggerDisplay("TaskScheduler = {TaskScheduler}, MaxMessagesPerTask = {MaxMessagesPerTask}, BoundedCapacity = {BoundedCapacity}, MaxDegreeOfParallelism = {MaxDegreeOfParallelism}")]
	public class ExecutionDataflowBlockOptions : DataflowBlockOptions
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00003638 File Offset: 0x00001838
		internal new ExecutionDataflowBlockOptions DefaultOrClone()
		{
			if (this != ExecutionDataflowBlockOptions.Default)
			{
				return new ExecutionDataflowBlockOptions
				{
					TaskScheduler = base.TaskScheduler,
					CancellationToken = base.CancellationToken,
					MaxMessagesPerTask = base.MaxMessagesPerTask,
					BoundedCapacity = base.BoundedCapacity,
					NameFormat = base.NameFormat,
					EnsureOrdered = base.EnsureOrdered,
					MaxDegreeOfParallelism = this.MaxDegreeOfParallelism,
					SingleProducerConstrained = this.SingleProducerConstrained
				};
			}
			return this;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000036C3 File Offset: 0x000018C3
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000036CB File Offset: 0x000018CB
		public int MaxDegreeOfParallelism
		{
			get
			{
				return this._maxDegreeOfParallelism;
			}
			set
			{
				if (value < 1 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._maxDegreeOfParallelism = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000036E7 File Offset: 0x000018E7
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000036EF File Offset: 0x000018EF
		public bool SingleProducerConstrained
		{
			get
			{
				return this._singleProducerConstrained;
			}
			set
			{
				this._singleProducerConstrained = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000036F8 File Offset: 0x000018F8
		internal int ActualMaxDegreeOfParallelism
		{
			get
			{
				if (this._maxDegreeOfParallelism != -1)
				{
					return this._maxDegreeOfParallelism;
				}
				return int.MaxValue;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000370F File Offset: 0x0000190F
		internal bool SupportsParallelExecution
		{
			get
			{
				return this._maxDegreeOfParallelism == -1 || this._maxDegreeOfParallelism > 1;
			}
		}

		// Token: 0x04000022 RID: 34
		internal new static readonly ExecutionDataflowBlockOptions Default = new ExecutionDataflowBlockOptions();

		// Token: 0x04000023 RID: 35
		private int _maxDegreeOfParallelism = 1;

		// Token: 0x04000024 RID: 36
		private bool _singleProducerConstrained;
	}
}
