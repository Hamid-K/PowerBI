using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000B4 RID: 180
	public class ServiceTickets : IDisposable
	{
		// Token: 0x06000536 RID: 1334 RVA: 0x00013357 File Offset: 0x00011557
		public ServiceTickets([NotNull] IList<BlockServiceTicket> tickets)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IList<BlockServiceTicket>>(tickets, "tickets");
			this.m_tickets = tickets;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00013374 File Offset: 0x00011574
		public T GetService<T>() where T : class
		{
			foreach (BlockServiceTicket blockServiceTicket in this.m_tickets)
			{
				T t = blockServiceTicket.GetService() as T;
				if (t != null)
				{
					return t;
				}
			}
			throw new ServiceNotFoundException(typeof(T));
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000133E8 File Offset: 0x000115E8
		public T GetService<T>(string name) where T : class
		{
			T t2 = default(T);
			BlockServiceTicket blockServiceTicket = this.m_tickets.Where((BlockServiceTicket t) => ((Block)t.GetService()).Name.Equals(name)).FirstOrDefault<BlockServiceTicket>();
			if (blockServiceTicket != null)
			{
				t2 = blockServiceTicket.GetService() as T;
			}
			if (t2 == null)
			{
				throw new ServiceNotFoundException(typeof(T));
			}
			return t2;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00013454 File Offset: 0x00011654
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00013460 File Offset: 0x00011660
		protected virtual void Dispose(bool disposing)
		{
			if (this.m_tickets != null && disposing)
			{
				foreach (BlockServiceTicket blockServiceTicket in this.m_tickets)
				{
					blockServiceTicket.Dispose();
				}
			}
			this.m_tickets = null;
		}

		// Token: 0x040001CE RID: 462
		private IList<BlockServiceTicket> m_tickets;
	}
}
