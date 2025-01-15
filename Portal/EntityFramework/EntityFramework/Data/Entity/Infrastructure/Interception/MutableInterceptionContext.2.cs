using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000299 RID: 665
	public abstract class MutableInterceptionContext<TResult> : DbInterceptionContext, IDbMutableInterceptionContext<TResult>, IDbMutableInterceptionContext
	{
		// Token: 0x06002127 RID: 8487 RVA: 0x0005D3F4 File Offset: 0x0005B5F4
		protected MutableInterceptionContext()
		{
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x0005D407 File Offset: 0x0005B607
		protected MutableInterceptionContext(DbInterceptionContext copyFrom)
			: base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06002129 RID: 8489 RVA: 0x0005D427 File Offset: 0x0005B627
		InterceptionContextMutableData<TResult> IDbMutableInterceptionContext<TResult>.MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x0600212A RID: 8490 RVA: 0x0005D42F File Offset: 0x0005B62F
		InterceptionContextMutableData IDbMutableInterceptionContext.MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x0600212B RID: 8491 RVA: 0x0005D437 File Offset: 0x0005B637
		public TResult OriginalResult
		{
			get
			{
				return this._mutableData.OriginalResult;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x0600212C RID: 8492 RVA: 0x0005D444 File Offset: 0x0005B644
		// (set) Token: 0x0600212D RID: 8493 RVA: 0x0005D451 File Offset: 0x0005B651
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

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x0600212E RID: 8494 RVA: 0x0005D45F File Offset: 0x0005B65F
		public bool IsExecutionSuppressed
		{
			get
			{
				return this._mutableData.IsExecutionSuppressed;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x0005D46C File Offset: 0x0005B66C
		// (set) Token: 0x06002130 RID: 8496 RVA: 0x0005D479 File Offset: 0x0005B679
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

		// Token: 0x06002131 RID: 8497 RVA: 0x0005D487 File Offset: 0x0005B687
		public object FindUserState(string key)
		{
			Check.NotNull<string>(key, "key");
			return this._mutableData.FindUserState(key);
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x0005D4A1 File Offset: 0x0005B6A1
		public void SetUserState(string key, object value)
		{
			Check.NotNull<string>(key, "key");
			this._mutableData.SetUserState(key, value);
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x0005D4BC File Offset: 0x0005B6BC
		public void SuppressExecution()
		{
			this._mutableData.SuppressExecution();
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06002134 RID: 8500 RVA: 0x0005D4C9 File Offset: 0x0005B6C9
		public Exception OriginalException
		{
			get
			{
				return this._mutableData.OriginalException;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06002135 RID: 8501 RVA: 0x0005D4D6 File Offset: 0x0005B6D6
		// (set) Token: 0x06002136 RID: 8502 RVA: 0x0005D4E3 File Offset: 0x0005B6E3
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

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06002137 RID: 8503 RVA: 0x0005D4F1 File Offset: 0x0005B6F1
		public TaskStatus TaskStatus
		{
			get
			{
				return this._mutableData.TaskStatus;
			}
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x0005D4FE File Offset: 0x0005B6FE
		public new MutableInterceptionContext<TResult> AsAsync()
		{
			return (MutableInterceptionContext<TResult>)base.AsAsync();
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x0005D50B File Offset: 0x0005B70B
		public new MutableInterceptionContext<TResult> WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (MutableInterceptionContext<TResult>)base.WithDbContext(context);
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x0005D525 File Offset: 0x0005B725
		public new MutableInterceptionContext<TResult> WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (MutableInterceptionContext<TResult>)base.WithObjectContext(context);
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x0005D53F File Offset: 0x0005B73F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x0005D547 File Offset: 0x0005B747
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x0005D550 File Offset: 0x0005B750
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x0005D558 File Offset: 0x0005B758
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B96 RID: 2966
		private readonly InterceptionContextMutableData<TResult> _mutableData = new InterceptionContextMutableData<TResult>();
	}
}
