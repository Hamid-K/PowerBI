using System;
using Microsoft.Data.SqlClient.Server;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000B3 RID: 179
	internal class TdsParameterSetter : SmiTypedGetterSetter
	{
		// Token: 0x06000CCF RID: 3279 RVA: 0x0002732B File Offset: 0x0002552B
		internal TdsParameterSetter(TdsParserStateObject stateObj, SmiMetaData md)
		{
			this._target = new TdsRecordBufferSetter(stateObj, md);
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0001996E File Offset: 0x00017B6E
		internal override bool CanGet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00027340 File Offset: 0x00025540
		internal override SmiTypedGetterSetter GetTypedGetterSetter(SmiEventSink sink, int ordinal)
		{
			return this._target;
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00027348 File Offset: 0x00025548
		public override void SetDBNull(SmiEventSink sink, int ordinal)
		{
			this._target.EndElements(sink);
		}

		// Token: 0x04000571 RID: 1393
		private TdsRecordBufferSetter _target;
	}
}
