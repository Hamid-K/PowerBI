using System;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x02000643 RID: 1603
	internal struct TranslatorArg
	{
		// Token: 0x06004D1C RID: 19740 RVA: 0x001107D6 File Offset: 0x0010E9D6
		internal TranslatorArg(Type requestedType)
		{
			this.RequestedType = requestedType;
		}

		// Token: 0x04001B66 RID: 7014
		internal readonly Type RequestedType;
	}
}
