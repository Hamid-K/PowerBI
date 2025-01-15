using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200029D RID: 669
	public class SingletonTask
	{
		// Token: 0x0600121C RID: 4636 RVA: 0x0003F3C1 File Offset: 0x0003D5C1
		public SingletonTask(Func<Task> factory)
		{
			this.m_factory = factory;
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x0003F3DC File Offset: 0x0003D5DC
		public Task RunAsync()
		{
			SingletonTask.<>c__DisplayClass4_0 CS$<>8__locals1 = new SingletonTask.<>c__DisplayClass4_0();
			CS$<>8__locals1.<>4__this = this;
			Task task = null;
			CS$<>8__locals1.start = null;
			object @lock = this._lock;
			lock (@lock)
			{
				if (this._runningTask == null)
				{
					CS$<>8__locals1.start = ColdTask.Create(this.m_factory);
					Func<Task> func = delegate
					{
						SingletonTask.<>c__DisplayClass4_0.<<RunAsync>b__0>d <<RunAsync>b__0>d;
						<<RunAsync>b__0>d.<>4__this = CS$<>8__locals1;
						<<RunAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
						<<RunAsync>b__0>d.<>1__state = -1;
						AsyncTaskMethodBuilder <>t__builder = <<RunAsync>b__0>d.<>t__builder;
						<>t__builder.Start<SingletonTask.<>c__DisplayClass4_0.<<RunAsync>b__0>d>(ref <<RunAsync>b__0>d);
						return <<RunAsync>b__0>d.<>t__builder.Task;
					};
					this._runningTask = func();
				}
				task = this._runningTask;
			}
			if (CS$<>8__locals1.start != null)
			{
				CS$<>8__locals1.start.StartAsync();
			}
			return task;
		}

		// Token: 0x040006B4 RID: 1716
		private Task _runningTask;

		// Token: 0x040006B5 RID: 1717
		private readonly object _lock = new object();

		// Token: 0x040006B6 RID: 1718
		private readonly Func<Task> m_factory;
	}
}
