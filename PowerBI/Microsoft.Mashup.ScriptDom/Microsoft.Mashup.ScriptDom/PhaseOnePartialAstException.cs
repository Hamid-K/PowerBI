using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000163 RID: 355
	[Serializable]
	internal sealed class PhaseOnePartialAstException : Exception
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06002113 RID: 8467 RVA: 0x0015C5CB File Offset: 0x0015A7CB
		public TSqlStatement Statement
		{
			get
			{
				return this._statement;
			}
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x0015C5D3 File Offset: 0x0015A7D3
		public PhaseOnePartialAstException(TSqlStatement statement)
		{
			this._statement = statement;
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x0015C5E2 File Offset: 0x0015A7E2
		private PhaseOnePartialAstException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x040018AC RID: 6316
		private TSqlStatement _statement;
	}
}
