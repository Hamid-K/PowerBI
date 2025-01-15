using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000295 RID: 661
	internal class InterceptionContextMutableData
	{
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060020F1 RID: 8433 RVA: 0x0005C9F0 File Offset: 0x0005ABF0
		// (set) Token: 0x060020F2 RID: 8434 RVA: 0x0005C9F8 File Offset: 0x0005ABF8
		public bool HasExecuted { get; set; }

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x0005CA01 File Offset: 0x0005AC01
		// (set) Token: 0x060020F4 RID: 8436 RVA: 0x0005CA09 File Offset: 0x0005AC09
		public Exception OriginalException { get; set; }

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060020F5 RID: 8437 RVA: 0x0005CA12 File Offset: 0x0005AC12
		// (set) Token: 0x060020F6 RID: 8438 RVA: 0x0005CA1A File Offset: 0x0005AC1A
		public TaskStatus TaskStatus { get; set; }

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x0005CA23 File Offset: 0x0005AC23
		private IDictionary<string, object> UserStateMap
		{
			get
			{
				if (this._userStateMap == null)
				{
					this._userStateMap = new Dictionary<string, object>(StringComparer.Ordinal);
				}
				return this._userStateMap;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060020F8 RID: 8440 RVA: 0x0005CA43 File Offset: 0x0005AC43
		// (set) Token: 0x060020F9 RID: 8441 RVA: 0x0005CA50 File Offset: 0x0005AC50
		[Obsolete("Not safe when multiple interceptors are in use. Use SetUserState and FindUserState instead.")]
		public object UserState
		{
			get
			{
				return this.FindUserState("__LegacyUserState__");
			}
			set
			{
				this.SetUserState("__LegacyUserState__", value);
			}
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x0005CA60 File Offset: 0x0005AC60
		public object FindUserState(string key)
		{
			object obj;
			if (this._userStateMap == null || !this.UserStateMap.TryGetValue(key, out obj))
			{
				return null;
			}
			return obj;
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x0005CA88 File Offset: 0x0005AC88
		public void SetUserState(string key, object value)
		{
			this.UserStateMap[key] = value;
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060020FC RID: 8444 RVA: 0x0005CA97 File Offset: 0x0005AC97
		public bool IsExecutionSuppressed
		{
			get
			{
				return this._isSuppressed;
			}
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x0005CA9F File Offset: 0x0005AC9F
		public void SuppressExecution()
		{
			if (!this._isSuppressed && this.HasExecuted)
			{
				throw new InvalidOperationException(Strings.SuppressionAfterExecution);
			}
			this._isSuppressed = true;
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x0005CAC3 File Offset: 0x0005ACC3
		// (set) Token: 0x060020FF RID: 8447 RVA: 0x0005CACB File Offset: 0x0005ACCB
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
			set
			{
				if (!this.HasExecuted)
				{
					this.SuppressExecution();
				}
				this._exception = value;
			}
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x0005CAE2 File Offset: 0x0005ACE2
		public void SetExceptionThrown(Exception exception)
		{
			this.HasExecuted = true;
			this.OriginalException = exception;
			this.Exception = exception;
		}

		// Token: 0x04000B8A RID: 2954
		private const string LegacyUserState = "__LegacyUserState__";

		// Token: 0x04000B8B RID: 2955
		private Exception _exception;

		// Token: 0x04000B8C RID: 2956
		private bool _isSuppressed;

		// Token: 0x04000B8D RID: 2957
		private IDictionary<string, object> _userStateMap;
	}
}
