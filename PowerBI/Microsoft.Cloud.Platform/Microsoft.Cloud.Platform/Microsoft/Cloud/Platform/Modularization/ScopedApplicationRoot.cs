using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000CF RID: 207
	public class ScopedApplicationRoot : ApplicationRoot, IDisposable
	{
		// Token: 0x060005DB RID: 1499 RVA: 0x00014B91 File Offset: 0x00012D91
		public static ScopedApplicationRoot Create(IEnumerable<IBlock> blocksToAdd, string[] args, string name)
		{
			ScopedApplicationRoot scopedApplicationRoot = new ScopedApplicationRoot(blocksToAdd, name);
			ScopedApplicationRoot.InitializeAndStartScopedApplicationRoot(scopedApplicationRoot, args);
			return scopedApplicationRoot;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00014BA4 File Offset: 0x00012DA4
		public T GetService<T>() where T : class
		{
			RequestedBlockService requestedBlockService = new RequestedBlockService(null, typeof(T));
			BlockServiceTicket service = this.GetService(requestedBlockService);
			this.m_tickets.Add(service);
			return service.GetService() as T;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00014BE6 File Offset: 0x00012DE6
		protected static void InitializeAndStartScopedApplicationRoot(ApplicationRoot root, string[] args)
		{
			root.Initialize(new ScopedApplicationRootHost(), args, ApplicationSwitchesTypes.CommandLine);
			root.Start();
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00014BFB File Offset: 0x00012DFB
		protected override void OnInitialize()
		{
			base.OnInitialize();
			if (this.m_blocksToAdd != null)
			{
				base.AddBlocks(this.m_blocksToAdd);
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00014C17 File Offset: 0x00012E17
		protected override void OnStateChangeFailed(BlockState stateBefore, BlockState stateDesired, string blockName, Exception exception)
		{
			throw exception;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00014C1C File Offset: 0x00012E1C
		protected override ActivityFactory GetActivityFactoryInstance()
		{
			ActivityFactory activityFactory = null;
			using (IEnumerator<IBlock> enumerator = this.m_blocksToAdd.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((activityFactory = enumerator.Current as ActivityFactory) != null)
					{
						break;
					}
				}
			}
			return activityFactory ?? base.GetActivityFactoryInstance();
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected override void ValidateRuntimeRequirements()
		{
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00014C7C File Offset: 0x00012E7C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00014C88 File Offset: 0x00012E88
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(typeof(ScopedApplicationRoot).Name);
				}
				foreach (BlockServiceTicket blockServiceTicket in this.m_tickets)
				{
					blockServiceTicket.Dispose();
				}
				base.Stop();
				base.WaitForStopToComplete();
				base.Shutdown();
				this.m_disposed = true;
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00014D14 File Offset: 0x00012F14
		protected ScopedApplicationRoot([NotNull] IEnumerable<IBlock> blocks, [NotNull] string name)
			: base(name)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<IBlock>>(blocks, "blocks");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			this.m_blocksToAdd = blocks;
			this.m_tickets = new List<BlockServiceTicket>();
			this.m_disposed = false;
		}

		// Token: 0x04000202 RID: 514
		private readonly IEnumerable<IBlock> m_blocksToAdd;

		// Token: 0x04000203 RID: 515
		private readonly List<BlockServiceTicket> m_tickets;

		// Token: 0x04000204 RID: 516
		private bool m_disposed;
	}
}
