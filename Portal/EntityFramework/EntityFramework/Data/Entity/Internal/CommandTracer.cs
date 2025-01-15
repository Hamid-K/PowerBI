using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Internal
{
	// Token: 0x020000EF RID: 239
	internal sealed class CommandTracer : ICancelableDbCommandInterceptor, IDbInterceptor, IDbCommandTreeInterceptor, ICancelableEntityConnectionInterceptor, IDisposable
	{
		// Token: 0x0600120A RID: 4618 RVA: 0x0002ED23 File Offset: 0x0002CF23
		public CommandTracer(DbContext context)
			: this(context, DbInterception.Dispatch)
		{
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x0002ED31 File Offset: 0x0002CF31
		internal CommandTracer(DbContext context, DbDispatchers dispatchers)
		{
			this._context = context;
			this._dispatchers = dispatchers;
			this._dispatchers.AddInterceptor(this);
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x0002ED69 File Offset: 0x0002CF69
		public IEnumerable<DbCommand> DbCommands
		{
			get
			{
				return this._commands;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x0002ED71 File Offset: 0x0002CF71
		public IEnumerable<DbCommandTree> CommandTrees
		{
			get
			{
				return this._commandTrees;
			}
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0002ED79 File Offset: 0x0002CF79
		public bool CommandExecuting(DbCommand command, DbInterceptionContext interceptionContext)
		{
			if (interceptionContext.DbContexts.Contains(this._context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals)))
			{
				this._commands.Add(command);
				return false;
			}
			return true;
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0002EDA9 File Offset: 0x0002CFA9
		public void TreeCreated(DbCommandTreeInterceptionContext interceptionContext)
		{
			if (interceptionContext.DbContexts.Contains(this._context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals)))
			{
				this._commandTrees.Add(interceptionContext.Result);
			}
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0002EDDB File Offset: 0x0002CFDB
		public bool ConnectionOpening(EntityConnection connection, DbInterceptionContext interceptionContext)
		{
			return !interceptionContext.DbContexts.Contains(this._context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals));
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0002EDFD File Offset: 0x0002CFFD
		void IDisposable.Dispose()
		{
			this._dispatchers.RemoveInterceptor(this);
		}

		// Token: 0x040008FE RID: 2302
		private readonly List<DbCommand> _commands = new List<DbCommand>();

		// Token: 0x040008FF RID: 2303
		private readonly List<DbCommandTree> _commandTrees = new List<DbCommandTree>();

		// Token: 0x04000900 RID: 2304
		private readonly DbContext _context;

		// Token: 0x04000901 RID: 2305
		private readonly DbDispatchers _dispatchers;
	}
}
