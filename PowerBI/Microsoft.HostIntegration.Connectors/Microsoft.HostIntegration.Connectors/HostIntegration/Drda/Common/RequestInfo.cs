using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000833 RID: 2099
	public class RequestInfo
	{
		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x060042DD RID: 17117 RVA: 0x000E04CE File Offset: 0x000DE6CE
		// (set) Token: 0x060042DE RID: 17118 RVA: 0x000E04D6 File Offset: 0x000DE6D6
		public object Data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x000E04DF File Offset: 0x000DE6DF
		public RequestInfo(CodePoint cp, int correlationId)
		{
			this._codePoint = cp;
			this._correlationId = correlationId;
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x000E04F5 File Offset: 0x000DE6F5
		public RequestInfo(CodePoint cp, int correlationId, object data)
		{
			this._codePoint = cp;
			this._correlationId = correlationId;
			this._data = data;
		}

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x060042E1 RID: 17121 RVA: 0x000E0512 File Offset: 0x000DE712
		// (set) Token: 0x060042E2 RID: 17122 RVA: 0x000E051A File Offset: 0x000DE71A
		public int CorrelationId
		{
			get
			{
				return this._correlationId;
			}
			set
			{
				this._correlationId = value;
			}
		}

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x060042E3 RID: 17123 RVA: 0x000E0523 File Offset: 0x000DE723
		// (set) Token: 0x060042E4 RID: 17124 RVA: 0x000E052B File Offset: 0x000DE72B
		public CodePoint CodePoint
		{
			get
			{
				return this._codePoint;
			}
			set
			{
				this._codePoint = value;
			}
		}

		// Token: 0x04002EC8 RID: 11976
		private CodePoint _codePoint;

		// Token: 0x04002EC9 RID: 11977
		private int _correlationId;

		// Token: 0x04002ECA RID: 11978
		private object _data;
	}
}
