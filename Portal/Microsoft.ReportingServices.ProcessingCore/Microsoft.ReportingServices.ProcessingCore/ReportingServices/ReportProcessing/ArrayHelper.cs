using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000660 RID: 1632
	internal class ArrayHelper
	{
		// Token: 0x06005AA5 RID: 23205 RVA: 0x001751FC File Offset: 0x001733FC
		internal static bool Equals(Array o1, Array o2)
		{
			if (o1.Length == o2.Length)
			{
				for (int i = 0; i < o1.Length; i++)
				{
					if (!o1.GetValue(i).Equals(o2.GetValue(i)))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}
	}
}
