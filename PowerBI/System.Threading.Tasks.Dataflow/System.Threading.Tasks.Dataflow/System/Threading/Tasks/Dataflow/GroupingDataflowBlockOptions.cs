using System;
using System.Diagnostics;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x0200001D RID: 29
	[DebuggerDisplay("TaskScheduler = {TaskScheduler}, MaxMessagesPerTask = {MaxMessagesPerTask}, BoundedCapacity = {BoundedCapacity}, Greedy = {Greedy}, MaxNumberOfGroups = {MaxNumberOfGroups}")]
	public class GroupingDataflowBlockOptions : DataflowBlockOptions
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00003734 File Offset: 0x00001934
		internal new GroupingDataflowBlockOptions DefaultOrClone()
		{
			if (this != GroupingDataflowBlockOptions.Default)
			{
				return new GroupingDataflowBlockOptions
				{
					TaskScheduler = base.TaskScheduler,
					CancellationToken = base.CancellationToken,
					MaxMessagesPerTask = base.MaxMessagesPerTask,
					BoundedCapacity = base.BoundedCapacity,
					NameFormat = base.NameFormat,
					EnsureOrdered = base.EnsureOrdered,
					Greedy = this.Greedy,
					MaxNumberOfGroups = this.MaxNumberOfGroups
				};
			}
			return this;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000037C7 File Offset: 0x000019C7
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000037CF File Offset: 0x000019CF
		public bool Greedy
		{
			get
			{
				return this._greedy;
			}
			set
			{
				this._greedy = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000037D8 File Offset: 0x000019D8
		// (set) Token: 0x06000099 RID: 153 RVA: 0x000037E0 File Offset: 0x000019E0
		public long MaxNumberOfGroups
		{
			get
			{
				return this._maxNumberOfGroups;
			}
			set
			{
				if (value <= 0L && value != -1L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._maxNumberOfGroups = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000037FE File Offset: 0x000019FE
		internal long ActualMaxNumberOfGroups
		{
			get
			{
				if (this._maxNumberOfGroups != -1L)
				{
					return this._maxNumberOfGroups;
				}
				return long.MaxValue;
			}
		}

		// Token: 0x04000025 RID: 37
		internal new static readonly GroupingDataflowBlockOptions Default = new GroupingDataflowBlockOptions();

		// Token: 0x04000026 RID: 38
		private bool _greedy = true;

		// Token: 0x04000027 RID: 39
		private long _maxNumberOfGroups = -1L;
	}
}
