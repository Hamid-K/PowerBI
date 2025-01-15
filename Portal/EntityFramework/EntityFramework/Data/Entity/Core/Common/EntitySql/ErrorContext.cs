using System;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200064C RID: 1612
	internal class ErrorContext
	{
		// Token: 0x04001C22 RID: 7202
		internal int InputPosition = -1;

		// Token: 0x04001C23 RID: 7203
		internal string ErrorContextInfo;

		// Token: 0x04001C24 RID: 7204
		internal bool UseContextInfoAsResourceIdentifier = true;

		// Token: 0x04001C25 RID: 7205
		internal string CommandText;
	}
}
