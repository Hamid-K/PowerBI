using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000298 RID: 664
	public abstract class MutableInterceptionContext : DbInterceptionContext, IDbMutableInterceptionContext
	{
		// Token: 0x06002112 RID: 8466 RVA: 0x0005D2B0 File Offset: 0x0005B4B0
		protected MutableInterceptionContext()
		{
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x0005D2C3 File Offset: 0x0005B4C3
		protected MutableInterceptionContext(DbInterceptionContext copyFrom)
			: base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06002114 RID: 8468 RVA: 0x0005D2E3 File Offset: 0x0005B4E3
		InterceptionContextMutableData IDbMutableInterceptionContext.MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06002115 RID: 8469 RVA: 0x0005D2EB File Offset: 0x0005B4EB
		internal InterceptionContextMutableData MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06002116 RID: 8470 RVA: 0x0005D2F3 File Offset: 0x0005B4F3
		public bool IsExecutionSuppressed
		{
			get
			{
				return this._mutableData.IsExecutionSuppressed;
			}
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x0005D300 File Offset: 0x0005B500
		public void SuppressExecution()
		{
			this._mutableData.SuppressExecution();
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06002118 RID: 8472 RVA: 0x0005D30D File Offset: 0x0005B50D
		public Exception OriginalException
		{
			get
			{
				return this._mutableData.OriginalException;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06002119 RID: 8473 RVA: 0x0005D31A File Offset: 0x0005B51A
		// (set) Token: 0x0600211A RID: 8474 RVA: 0x0005D327 File Offset: 0x0005B527
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

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x0600211B RID: 8475 RVA: 0x0005D335 File Offset: 0x0005B535
		public TaskStatus TaskStatus
		{
			get
			{
				return this._mutableData.TaskStatus;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x0005D342 File Offset: 0x0005B542
		// (set) Token: 0x0600211D RID: 8477 RVA: 0x0005D34F File Offset: 0x0005B54F
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

		// Token: 0x0600211E RID: 8478 RVA: 0x0005D35D File Offset: 0x0005B55D
		public object FindUserState(string key)
		{
			Check.NotNull<string>(key, "key");
			return this._mutableData.FindUserState(key);
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x0005D377 File Offset: 0x0005B577
		public void SetUserState(string key, object value)
		{
			Check.NotNull<string>(key, "key");
			this._mutableData.SetUserState(key, value);
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x0005D392 File Offset: 0x0005B592
		public new MutableInterceptionContext AsAsync()
		{
			return (MutableInterceptionContext)base.AsAsync();
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x0005D39F File Offset: 0x0005B59F
		public new MutableInterceptionContext WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (MutableInterceptionContext)base.WithDbContext(context);
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x0005D3B9 File Offset: 0x0005B5B9
		public new MutableInterceptionContext WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (MutableInterceptionContext)base.WithObjectContext(context);
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x0005D3D3 File Offset: 0x0005B5D3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x0005D3DB File Offset: 0x0005B5DB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x0005D3E4 File Offset: 0x0005B5E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x0005D3EC File Offset: 0x0005B5EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B95 RID: 2965
		private readonly InterceptionContextMutableData _mutableData = new InterceptionContextMutableData();
	}
}
