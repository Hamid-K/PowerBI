using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x020000A2 RID: 162
	internal sealed class MemoryUsageSimulation
	{
		// Token: 0x06000500 RID: 1280 RVA: 0x0000FA28 File Offset: 0x0000DC28
		public static void Create()
		{
			for (int i = 0; i < 1; i++)
			{
				new MemoryUsageSimulation().Start();
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000FA76 File Offset: 0x0000DC76
		public void Start()
		{
			this.m_thread = new Thread(new ThreadStart(this.ThreadStartFunc));
			this.m_thread.IsBackground = true;
			this.m_stop = false;
			this.m_thread.Start();
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000FAB5 File Offset: 0x0000DCB5
		public void Stop()
		{
			this.m_stop = true;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000FAC0 File Offset: 0x0000DCC0
		private void ThreadStartFunc()
		{
			Thread.Sleep(30000);
			MemoryUsageSimulation.SimpleMethod[] array = new MemoryUsageSimulation.SimpleMethod[]
			{
				new MemoryUsageSimulation.SimpleMethod(this.RunMemoryRequest)
			};
			while (!this.m_stop)
			{
				try
				{
					Random rand = this.m_rand;
					MemoryUsageSimulation.SimpleMethod simpleMethod;
					lock (rand)
					{
						simpleMethod = array[this.m_rand.Next(array.Length)];
					}
					simpleMethod();
					Thread.Sleep(100);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Simulator Enqueue Thread Hit Exception: " + ex.ToString());
				}
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000FB70 File Offset: 0x0000DD70
		private void RunMemoryRequest()
		{
			if (!this.m_workerSemaphore.Acquire(5000))
			{
				Console.WriteLine("Timeout waiting for semaphore.");
				return;
			}
			new Thread(delegate
			{
				try
				{
					new MemoryRequest().DoWork();
				}
				catch (Exception ex)
				{
					Console.WriteLine("Simulator worker thread hit exception: " + ex.ToString());
				}
				finally
				{
					this.m_workerSemaphore.Release();
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000FBAC File Offset: 0x0000DDAC
		private void AddMap()
		{
			List<TestMemoryAuditProxy> maps = this.m_maps;
			lock (maps)
			{
				this.m_maps.Add(new TestMemoryAuditProxy());
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000FBF8 File Offset: 0x0000DDF8
		private void RemoveMap()
		{
			List<TestMemoryAuditProxy> maps = this.m_maps;
			lock (maps)
			{
				if (this.m_maps.Count > 0)
				{
					TestMemoryAuditProxy testMemoryAuditProxy = this.m_maps[this.GetRandomIndex()];
					this.m_maps.Remove(testMemoryAuditProxy);
					((IDisposable)testMemoryAuditProxy).Dispose();
				}
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000FC68 File Offset: 0x0000DE68
		private void AllocateInCurrentMap()
		{
			List<TestMemoryAuditProxy> maps = this.m_maps;
			lock (maps)
			{
				if (this.m_maps.Count > 0)
				{
					this.m_maps[this.GetRandomIndex()].Allocate(85000);
				}
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000FCCC File Offset: 0x0000DECC
		private void SingleRequestManyAllocs()
		{
			TestMemoryAuditProxy map = this.GetRandomMap();
			ThreadPool.QueueUserWorkItem(delegate(object state)
			{
				for (int i = 0; i < 1000000; i++)
				{
					map.Allocate(128);
				}
			});
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		private TestMemoryAuditProxy GetRandomMap()
		{
			List<TestMemoryAuditProxy> maps = this.m_maps;
			TestMemoryAuditProxy testMemoryAuditProxy;
			lock (maps)
			{
				testMemoryAuditProxy = this.m_maps[this.GetRandomIndex()];
			}
			return testMemoryAuditProxy;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000FD40 File Offset: 0x0000DF40
		private int GetRandomIndex()
		{
			Random rand = this.m_rand;
			int num;
			lock (rand)
			{
				num = this.m_rand.Next(this.m_maps.Count);
			}
			return num;
		}

		// Token: 0x040002E8 RID: 744
		private readonly Random m_rand = new Random();

		// Token: 0x040002E9 RID: 745
		private readonly List<TestMemoryAuditProxy> m_maps = new List<TestMemoryAuditProxy>();

		// Token: 0x040002EA RID: 746
		private volatile bool m_stop;

		// Token: 0x040002EB RID: 747
		private volatile Thread m_thread;

		// Token: 0x040002EC RID: 748
		private readonly ManagedSemaphore m_workerSemaphore = new ManagedSemaphore(4, 4);

		// Token: 0x020000FD RID: 253
		// (Invoke) Token: 0x060007ED RID: 2029
		private delegate void SimpleMethod();
	}
}
