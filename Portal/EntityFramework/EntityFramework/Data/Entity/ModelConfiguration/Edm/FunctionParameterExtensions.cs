using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x0200016B RID: 363
	internal static class FunctionParameterExtensions
	{
		// Token: 0x0600168F RID: 5775 RVA: 0x0003B8F3 File Offset: 0x00039AF3
		public static object GetConfiguration(this FunctionParameter functionParameter)
		{
			return functionParameter.Annotations.GetConfiguration();
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x0003B900 File Offset: 0x00039B00
		public static void SetConfiguration(this FunctionParameter functionParameter, object configuration)
		{
			functionParameter.GetMetadataProperties().SetConfiguration(configuration);
		}
	}
}
