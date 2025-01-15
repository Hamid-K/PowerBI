using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000411 RID: 1041
	public sealed class LayoutDirectionStringConverter : ExclusiveStringListConverter
	{
		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002112 RID: 8466 RVA: 0x00080381 File Offset: 0x0007E581
		internal override string[] Values
		{
			get
			{
				return LayoutDirectionStringConverter.StringValuesArray;
			}
		}

		// Token: 0x04000E7E RID: 3710
		internal static readonly string[] StringValuesArray = new string[] { "LTR", "RTL" };
	}
}
