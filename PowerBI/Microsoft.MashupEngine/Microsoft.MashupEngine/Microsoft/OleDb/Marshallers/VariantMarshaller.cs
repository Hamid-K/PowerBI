using System;

namespace Microsoft.OleDb.Marshallers
{
	// Token: 0x02001FD5 RID: 8149
	public static class VariantMarshaller
	{
		// Token: 0x1700303B RID: 12347
		// (get) Token: 0x0600C714 RID: 50964 RVA: 0x0027A6AB File Offset: 0x002788AB
		public static IVariantMarshaller Instance
		{
			get
			{
				if (VariantMarshaller.instance == null)
				{
					VariantMarshaller.instance = new Win32VariantMarshaller();
				}
				return VariantMarshaller.instance;
			}
		}

		// Token: 0x0600C715 RID: 50965 RVA: 0x0027A6C3 File Offset: 0x002788C3
		public static IVariantMarshaller SetVariantMarshaller(IVariantMarshaller newVariantMarshaller)
		{
			IVariantMarshaller variantMarshaller = VariantMarshaller.instance;
			VariantMarshaller.instance = newVariantMarshaller;
			return variantMarshaller;
		}

		// Token: 0x0400658C RID: 25996
		private static IVariantMarshaller instance;
	}
}
