using System;

namespace DocumentFormat.OpenXml.Validation
{
	// Token: 0x020030F9 RID: 12537
	internal interface ICancelable
	{
		// Token: 0x0601B323 RID: 111395
		void OnCancel(object sender, EventArgs eventArgs);
	}
}
