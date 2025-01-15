using System;

namespace System.Web.Http.Validation
{
	// Token: 0x02000095 RID: 149
	public class ModelValidationResult
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000AD1E File Offset: 0x00008F1E
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x0000AD2F File Offset: 0x00008F2F
		public string MemberName
		{
			get
			{
				return this._memberName ?? string.Empty;
			}
			set
			{
				this._memberName = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000AD38 File Offset: 0x00008F38
		// (set) Token: 0x060003AB RID: 939 RVA: 0x0000AD49 File Offset: 0x00008F49
		public string Message
		{
			get
			{
				return this._message ?? string.Empty;
			}
			set
			{
				this._message = value;
			}
		}

		// Token: 0x040000D9 RID: 217
		private string _memberName;

		// Token: 0x040000DA RID: 218
		private string _message;
	}
}
