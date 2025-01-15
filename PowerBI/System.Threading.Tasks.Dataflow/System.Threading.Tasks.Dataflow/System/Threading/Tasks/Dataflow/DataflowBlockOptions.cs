using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x0200001B RID: 27
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("TaskScheduler = {TaskScheduler}, MaxMessagesPerTask = {MaxMessagesPerTask}, BoundedCapacity = {BoundedCapacity}")]
	public class DataflowBlockOptions
	{
		// Token: 0x0600007B RID: 123 RVA: 0x000034C8 File Offset: 0x000016C8
		internal DataflowBlockOptions DefaultOrClone()
		{
			if (this != DataflowBlockOptions.Default)
			{
				return new DataflowBlockOptions
				{
					TaskScheduler = this.TaskScheduler,
					CancellationToken = this.CancellationToken,
					MaxMessagesPerTask = this.MaxMessagesPerTask,
					BoundedCapacity = this.BoundedCapacity,
					NameFormat = this.NameFormat,
					EnsureOrdered = this.EnsureOrdered
				};
			}
			return this;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000356A File Offset: 0x0000176A
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003572 File Offset: 0x00001772
		public TaskScheduler TaskScheduler
		{
			get
			{
				return this._taskScheduler;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._taskScheduler = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003589 File Offset: 0x00001789
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003591 File Offset: 0x00001791
		public CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
			set
			{
				this._cancellationToken = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000359A File Offset: 0x0000179A
		// (set) Token: 0x06000082 RID: 130 RVA: 0x000035A2 File Offset: 0x000017A2
		public int MaxMessagesPerTask
		{
			get
			{
				return this._maxMessagesPerTask;
			}
			set
			{
				if (value < 1 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._maxMessagesPerTask = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000035BE File Offset: 0x000017BE
		internal int ActualMaxMessagesPerTask
		{
			get
			{
				if (this._maxMessagesPerTask != -1)
				{
					return this._maxMessagesPerTask;
				}
				return int.MaxValue;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000035D5 File Offset: 0x000017D5
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000035DD File Offset: 0x000017DD
		public int BoundedCapacity
		{
			get
			{
				return this._boundedCapacity;
			}
			set
			{
				if (value < 1 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._boundedCapacity = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000035F9 File Offset: 0x000017F9
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00003601 File Offset: 0x00001801
		public string NameFormat
		{
			get
			{
				return this._nameFormat;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._nameFormat = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003618 File Offset: 0x00001818
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003620 File Offset: 0x00001820
		public bool EnsureOrdered
		{
			get
			{
				return this._ensureOrdered;
			}
			set
			{
				this._ensureOrdered = value;
			}
		}

		// Token: 0x0400001A RID: 26
		public const int Unbounded = -1;

		// Token: 0x0400001B RID: 27
		private TaskScheduler _taskScheduler = TaskScheduler.Default;

		// Token: 0x0400001C RID: 28
		private CancellationToken _cancellationToken = CancellationToken.None;

		// Token: 0x0400001D RID: 29
		private int _maxMessagesPerTask = -1;

		// Token: 0x0400001E RID: 30
		private int _boundedCapacity = -1;

		// Token: 0x0400001F RID: 31
		private string _nameFormat = "{0} Id={1}";

		// Token: 0x04000020 RID: 32
		private bool _ensureOrdered = true;

		// Token: 0x04000021 RID: 33
		internal static readonly DataflowBlockOptions Default = new DataflowBlockOptions();
	}
}
