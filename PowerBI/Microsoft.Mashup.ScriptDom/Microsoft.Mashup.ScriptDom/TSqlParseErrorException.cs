using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200017F RID: 383
	[Serializable]
	internal sealed class TSqlParseErrorException : Exception
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06002147 RID: 8519 RVA: 0x0015D050 File Offset: 0x0015B250
		public bool DoNotLog
		{
			get
			{
				return this._doNotLog;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06002148 RID: 8520 RVA: 0x0015D058 File Offset: 0x0015B258
		public ParseError ParseError
		{
			get
			{
				return this._parseError;
			}
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x0015D060 File Offset: 0x0015B260
		public TSqlParseErrorException(ParseError error, bool doNotLog)
		{
			this._parseError = error;
			this._doNotLog = doNotLog;
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x0015D076 File Offset: 0x0015B276
		public TSqlParseErrorException(ParseError error)
			: this(error, false)
		{
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x0015D080 File Offset: 0x0015B280
		private TSqlParseErrorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0400196D RID: 6509
		private ParseError _parseError;

		// Token: 0x0400196E RID: 6510
		private bool _doNotLog;
	}
}
