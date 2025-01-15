using System;
using System.Globalization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.PowerBI.ServiceContracts
{
	// Token: 0x0200000E RID: 14
	public class TryGetResult<T>
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002D65 File Offset: 0x00000F65
		internal TryGetResult(T result, bool succeeded)
		{
			this.Succeeded = succeeded;
			this.m_result = result;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002D7B File Offset: 0x00000F7B
		internal TryGetResult(T result, bool succeeded, Exception e)
		{
			this.Succeeded = succeeded;
			this.m_result = result;
			this.m_exception = e;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002D98 File Offset: 0x00000F98
		public bool Succeeded { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002DA0 File Offset: 0x00000FA0
		public Exception Exception
		{
			get
			{
				return this.m_exception;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public bool HasException
		{
			get
			{
				return this.m_exception != null;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public T Result
		{
			get
			{
				if (this.Succeeded)
				{
					return this.m_result;
				}
				throw new TryGetOperationResultUnavailableException(string.Format(CultureInfo.InvariantCulture, "Result of an unsuccessful Async operation returning type {0} is not available - Exception: {1}", new object[]
				{
					typeof(T),
					(this.m_exception == null) ? "<<NULL>>" : this.m_exception.ToString()
				}), this.m_exception);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002E1C File Offset: 0x0000101C
		public T ResultOrDefault
		{
			get
			{
				if (!this)
				{
					return default(T);
				}
				return this.m_result;
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E41 File Offset: 0x00001041
		public static implicit operator bool(TryGetResult<T> result)
		{
			return result.Succeeded;
		}

		// Token: 0x0400003C RID: 60
		private readonly T m_result;

		// Token: 0x0400003D RID: 61
		private readonly Exception m_exception;
	}
}
