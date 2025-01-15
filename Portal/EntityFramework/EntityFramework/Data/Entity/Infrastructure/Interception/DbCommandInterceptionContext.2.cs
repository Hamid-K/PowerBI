using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200027A RID: 634
	public class DbCommandInterceptionContext<TResult> : DbCommandInterceptionContext, IDbMutableInterceptionContext<TResult>, IDbMutableInterceptionContext
	{
		// Token: 0x06001FFB RID: 8187 RVA: 0x0005B274 File Offset: 0x00059474
		public DbCommandInterceptionContext()
		{
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x0005B287 File Offset: 0x00059487
		public DbCommandInterceptionContext(DbInterceptionContext copyFrom)
			: base(copyFrom)
		{
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x0005B29B File Offset: 0x0005949B
		InterceptionContextMutableData IDbMutableInterceptionContext.MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001FFE RID: 8190 RVA: 0x0005B2A3 File Offset: 0x000594A3
		InterceptionContextMutableData<TResult> IDbMutableInterceptionContext<TResult>.MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001FFF RID: 8191 RVA: 0x0005B2AB File Offset: 0x000594AB
		internal InterceptionContextMutableData<TResult> MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06002000 RID: 8192 RVA: 0x0005B2B3 File Offset: 0x000594B3
		public TResult OriginalResult
		{
			get
			{
				return this._mutableData.OriginalResult;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x0005B2C0 File Offset: 0x000594C0
		// (set) Token: 0x06002002 RID: 8194 RVA: 0x0005B2CD File Offset: 0x000594CD
		public TResult Result
		{
			get
			{
				return this._mutableData.Result;
			}
			set
			{
				this._mutableData.Result = value;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06002003 RID: 8195 RVA: 0x0005B2DB File Offset: 0x000594DB
		public bool IsExecutionSuppressed
		{
			get
			{
				return this._mutableData.IsExecutionSuppressed;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06002004 RID: 8196 RVA: 0x0005B2E8 File Offset: 0x000594E8
		// (set) Token: 0x06002005 RID: 8197 RVA: 0x0005B2F5 File Offset: 0x000594F5
		[Obsolete("Not safe when multiple interceptors are in use. Use SetUserState and FindUserState instead.")]
		public object UserState
		{
			get
			{
				return this._mutableData.UserState;
			}
			set
			{
				this._mutableData.UserState = value;
			}
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x0005B303 File Offset: 0x00059503
		public object FindUserState(string key)
		{
			Check.NotNull<string>(key, "key");
			return this._mutableData.FindUserState(key);
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x0005B31D File Offset: 0x0005951D
		public void SetUserState(string key, object value)
		{
			Check.NotNull<string>(key, "key");
			this._mutableData.SetUserState(key, value);
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x0005B338 File Offset: 0x00059538
		public void SuppressExecution()
		{
			this._mutableData.SuppressExecution();
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x0005B345 File Offset: 0x00059545
		public Exception OriginalException
		{
			get
			{
				return this._mutableData.OriginalException;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x0600200A RID: 8202 RVA: 0x0005B352 File Offset: 0x00059552
		// (set) Token: 0x0600200B RID: 8203 RVA: 0x0005B35F File Offset: 0x0005955F
		public Exception Exception
		{
			get
			{
				return this._mutableData.Exception;
			}
			set
			{
				this._mutableData.Exception = value;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x0600200C RID: 8204 RVA: 0x0005B36D File Offset: 0x0005956D
		public TaskStatus TaskStatus
		{
			get
			{
				return this._mutableData.TaskStatus;
			}
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x0005B37A File Offset: 0x0005957A
		public new DbCommandInterceptionContext<TResult> AsAsync()
		{
			return (DbCommandInterceptionContext<TResult>)base.AsAsync();
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x0005B387 File Offset: 0x00059587
		public new DbCommandInterceptionContext<TResult> WithCommandBehavior(CommandBehavior commandBehavior)
		{
			return (DbCommandInterceptionContext<TResult>)base.WithCommandBehavior(commandBehavior);
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x0005B395 File Offset: 0x00059595
		protected override DbInterceptionContext Clone()
		{
			return new DbCommandInterceptionContext<TResult>(this);
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x0005B39D File Offset: 0x0005959D
		public new DbCommandInterceptionContext<TResult> WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (DbCommandInterceptionContext<TResult>)base.WithDbContext(context);
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x0005B3B7 File Offset: 0x000595B7
		public new DbCommandInterceptionContext<TResult> WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (DbCommandInterceptionContext<TResult>)base.WithObjectContext(context);
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x0005B3D1 File Offset: 0x000595D1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x0005B3D9 File Offset: 0x000595D9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x0005B3E2 File Offset: 0x000595E2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x0005B3EA File Offset: 0x000595EA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B77 RID: 2935
		private readonly InterceptionContextMutableData<TResult> _mutableData = new InterceptionContextMutableData<TResult>();
	}
}
