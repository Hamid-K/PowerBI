using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000C3 RID: 195
	internal sealed class DefaultValidationScope : IValidationScope
	{
		// Token: 0x06000B3D RID: 2877 RVA: 0x0002514F File Offset: 0x0002334F
		private DefaultValidationScope()
		{
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00025157 File Offset: 0x00023357
		public string ObjectType
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0002515E File Offset: 0x0002335E
		public string ObjectID
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00025165 File Offset: 0x00023365
		public string ObjectName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x0400049C RID: 1180
		public static readonly IValidationScope Empty = new DefaultValidationScope();
	}
}
