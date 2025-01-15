using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000412 RID: 1042
	internal sealed class SubtotalPositionStringConverter : ExclusiveStringListConverter
	{
		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002115 RID: 8469 RVA: 0x000803A5 File Offset: 0x0007E5A5
		internal override string[] Values
		{
			get
			{
				return SubtotalPositionStringConverter.StringValuesArray;
			}
		}

		// Token: 0x04000E7F RID: 3711
		internal static readonly string[] StringValuesArray = new string[] { "Before", "After" };
	}
}
