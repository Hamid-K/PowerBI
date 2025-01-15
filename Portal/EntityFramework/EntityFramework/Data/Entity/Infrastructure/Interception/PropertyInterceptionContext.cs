using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200029A RID: 666
	public class PropertyInterceptionContext<TValue> : DbInterceptionContext, IDbMutableInterceptionContext
	{
		// Token: 0x0600213F RID: 8511 RVA: 0x0005D560 File Offset: 0x0005B760
		public PropertyInterceptionContext()
		{
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x0005D574 File Offset: 0x0005B774
		public PropertyInterceptionContext(DbInterceptionContext copyFrom)
			: base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
			PropertyInterceptionContext<TValue> propertyInterceptionContext = copyFrom as PropertyInterceptionContext<TValue>;
			if (propertyInterceptionContext != null)
			{
				this._value = propertyInterceptionContext._value;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x0005D5B5 File Offset: 0x0005B7B5
		InterceptionContextMutableData IDbMutableInterceptionContext.MutableData
		{
			get
			{
				return this._mutableData;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x0005D5BD File Offset: 0x0005B7BD
		public TValue Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002143 RID: 8515 RVA: 0x0005D5C5 File Offset: 0x0005B7C5
		// (set) Token: 0x06002144 RID: 8516 RVA: 0x0005D5D2 File Offset: 0x0005B7D2
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

		// Token: 0x06002145 RID: 8517 RVA: 0x0005D5E0 File Offset: 0x0005B7E0
		public object FindUserState(string key)
		{
			Check.NotNull<string>(key, "key");
			return this._mutableData.FindUserState(key);
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x0005D5FA File Offset: 0x0005B7FA
		public void SetUserState(string key, object value)
		{
			Check.NotNull<string>(key, "key");
			this._mutableData.SetUserState(key, value);
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x0005D615 File Offset: 0x0005B815
		public PropertyInterceptionContext<TValue> WithValue(TValue value)
		{
			PropertyInterceptionContext<TValue> propertyInterceptionContext = this.TypedClone();
			propertyInterceptionContext._value = value;
			return propertyInterceptionContext;
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x0005D624 File Offset: 0x0005B824
		private PropertyInterceptionContext<TValue> TypedClone()
		{
			return (PropertyInterceptionContext<TValue>)this.Clone();
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x0005D631 File Offset: 0x0005B831
		protected override DbInterceptionContext Clone()
		{
			return new PropertyInterceptionContext<TValue>(this);
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x0600214A RID: 8522 RVA: 0x0005D639 File Offset: 0x0005B839
		public bool IsExecutionSuppressed
		{
			get
			{
				return this._mutableData.IsExecutionSuppressed;
			}
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x0005D646 File Offset: 0x0005B846
		public void SuppressExecution()
		{
			this._mutableData.SuppressExecution();
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x0600214C RID: 8524 RVA: 0x0005D653 File Offset: 0x0005B853
		public Exception OriginalException
		{
			get
			{
				return this._mutableData.OriginalException;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x0600214D RID: 8525 RVA: 0x0005D660 File Offset: 0x0005B860
		// (set) Token: 0x0600214E RID: 8526 RVA: 0x0005D66D File Offset: 0x0005B86D
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

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x0005D67B File Offset: 0x0005B87B
		public TaskStatus TaskStatus
		{
			get
			{
				return this._mutableData.TaskStatus;
			}
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x0005D688 File Offset: 0x0005B888
		public new PropertyInterceptionContext<TValue> AsAsync()
		{
			return (PropertyInterceptionContext<TValue>)base.AsAsync();
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x0005D695 File Offset: 0x0005B895
		public new PropertyInterceptionContext<TValue> WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (PropertyInterceptionContext<TValue>)base.WithDbContext(context);
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x0005D6AF File Offset: 0x0005B8AF
		public new PropertyInterceptionContext<TValue> WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (PropertyInterceptionContext<TValue>)base.WithObjectContext(context);
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x0005D6C9 File Offset: 0x0005B8C9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x0005D6D1 File Offset: 0x0005B8D1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x0005D6DA File Offset: 0x0005B8DA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x0005D6E2 File Offset: 0x0005B8E2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B97 RID: 2967
		private readonly InterceptionContextMutableData _mutableData = new InterceptionContextMutableData();

		// Token: 0x04000B98 RID: 2968
		private TValue _value;
	}
}
