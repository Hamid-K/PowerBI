using System;
using System.ComponentModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200027D RID: 637
	public class DbCommandTreeInterceptionContext : DbInterceptionContext, IDbMutableInterceptionContext<DbCommandTree>, IDbMutableInterceptionContext
	{
		// Token: 0x06002020 RID: 8224 RVA: 0x0005B454 File Offset: 0x00059654
		public DbCommandTreeInterceptionContext()
		{
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x0005B467 File Offset: 0x00059667
		public DbCommandTreeInterceptionContext(DbInterceptionContext copyFrom)
			: base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x0005B487 File Offset: 0x00059687
		internal InterceptionContextMutableData<DbCommandTree> MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x0005B48F File Offset: 0x0005968F
		InterceptionContextMutableData<DbCommandTree> IDbMutableInterceptionContext<DbCommandTree>.MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x0005B497 File Offset: 0x00059697
		InterceptionContextMutableData IDbMutableInterceptionContext.MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x0005B49F File Offset: 0x0005969F
		public DbCommandTree OriginalResult
		{
			get
			{
				return this._mutableData.OriginalResult;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06002026 RID: 8230 RVA: 0x0005B4AC File Offset: 0x000596AC
		// (set) Token: 0x06002027 RID: 8231 RVA: 0x0005B4B9 File Offset: 0x000596B9
		public DbCommandTree Result
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

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06002028 RID: 8232 RVA: 0x0005B4C7 File Offset: 0x000596C7
		// (set) Token: 0x06002029 RID: 8233 RVA: 0x0005B4D4 File Offset: 0x000596D4
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

		// Token: 0x0600202A RID: 8234 RVA: 0x0005B4E2 File Offset: 0x000596E2
		public object FindUserState(string key)
		{
			Check.NotNull<string>(key, "key");
			return this._mutableData.FindUserState(key);
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x0005B4FC File Offset: 0x000596FC
		public void SetUserState(string key, object value)
		{
			Check.NotNull<string>(key, "key");
			this._mutableData.SetUserState(key, value);
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x0005B517 File Offset: 0x00059717
		protected override DbInterceptionContext Clone()
		{
			return new DbCommandTreeInterceptionContext(this);
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x0005B51F File Offset: 0x0005971F
		public new DbCommandTreeInterceptionContext WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (DbCommandTreeInterceptionContext)base.WithDbContext(context);
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x0005B539 File Offset: 0x00059739
		public new DbCommandTreeInterceptionContext WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (DbCommandTreeInterceptionContext)base.WithObjectContext(context);
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x0005B553 File Offset: 0x00059753
		public new DbCommandTreeInterceptionContext AsAsync()
		{
			return (DbCommandTreeInterceptionContext)base.AsAsync();
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x0005B560 File Offset: 0x00059760
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x0005B568 File Offset: 0x00059768
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x0005B571 File Offset: 0x00059771
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x0005B579 File Offset: 0x00059779
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B79 RID: 2937
		private readonly InterceptionContextMutableData<DbCommandTree> _mutableData = new InterceptionContextMutableData<DbCommandTree>();
	}
}
