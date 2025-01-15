using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser.ReportObjectModel
{
	// Token: 0x020002C5 RID: 709
	internal abstract class User
	{
		// Token: 0x170006F2 RID: 1778
		internal abstract object this[string key] { get; }

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060015F3 RID: 5619
		internal abstract string UserID { get; }

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060015F4 RID: 5620
		internal abstract string Language { get; }
	}
}
