using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000804 RID: 2052
	public class DrdaExceptionInfo
	{
		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x060040D4 RID: 16596 RVA: 0x000DC946 File Offset: 0x000DAB46
		// (set) Token: 0x060040D5 RID: 16597 RVA: 0x000DC94E File Offset: 0x000DAB4E
		public ErrorCodePoint CodePoint
		{
			get
			{
				return this._errCodePoint;
			}
			set
			{
				this._errCodePoint = value;
			}
		}

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x060040D6 RID: 16598 RVA: 0x000DC957 File Offset: 0x000DAB57
		// (set) Token: 0x060040D7 RID: 16599 RVA: 0x000DC95F File Offset: 0x000DAB5F
		public int Svrcod
		{
			get
			{
				return this._svrcod;
			}
			set
			{
				this._svrcod = value;
			}
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x060040D8 RID: 16600 RVA: 0x000DC968 File Offset: 0x000DAB68
		// (set) Token: 0x060040D9 RID: 16601 RVA: 0x000DC970 File Offset: 0x000DAB70
		public CodePoint CodePointArgument
		{
			get
			{
				return this._codePointArgument;
			}
			set
			{
				this._codePointArgument = value;
			}
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x060040DA RID: 16602 RVA: 0x000DC979 File Offset: 0x000DAB79
		// (set) Token: 0x060040DB RID: 16603 RVA: 0x000DC981 File Offset: 0x000DAB81
		public bool SendCodePointArgument
		{
			get
			{
				return this._sendCodePointArgument;
			}
			set
			{
				this._sendCodePointArgument = value;
			}
		}

		// Token: 0x060040DC RID: 16604 RVA: 0x000DC98A File Offset: 0x000DAB8A
		internal DrdaExceptionInfo(ErrorCodePoint errCodePoint, int svrcod, CodePoint codePointArgument, bool sendCodpntArgument)
		{
			this._errCodePoint = errCodePoint;
			this._svrcod = svrcod;
			this._codePointArgument = codePointArgument;
			this._sendCodePointArgument = sendCodpntArgument;
		}

		// Token: 0x04002DA3 RID: 11683
		private ErrorCodePoint _errCodePoint;

		// Token: 0x04002DA4 RID: 11684
		private int _svrcod;

		// Token: 0x04002DA5 RID: 11685
		private CodePoint _codePointArgument;

		// Token: 0x04002DA6 RID: 11686
		private bool _sendCodePointArgument;
	}
}
