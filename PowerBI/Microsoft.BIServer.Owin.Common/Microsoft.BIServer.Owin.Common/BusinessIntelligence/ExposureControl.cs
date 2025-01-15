using System;
using System.Text;

namespace Microsoft.BusinessIntelligence
{
	// Token: 0x02000003 RID: 3
	internal sealed class ExposureControl
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal ExposureControl(ExposureLevel exposureLevel)
		{
			this.exposureLevel = exposureLevel;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000205F File Offset: 0x0000025F
		internal bool PreviewEnabled
		{
			get
			{
				return ExposureLevel.Preview == this.exposureLevel || ExposureLevel.Dogfood == this.exposureLevel || ExposureLevel.Development == this.exposureLevel;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000207E File Offset: 0x0000027E
		internal bool DogfoodEnabled
		{
			get
			{
				return ExposureLevel.Dogfood == this.exposureLevel || ExposureLevel.Development == this.exposureLevel;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002094 File Offset: 0x00000294
		internal bool DevelopmentEnabled
		{
			get
			{
				return ExposureLevel.Development == this.exposureLevel;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020A0 File Offset: 0x000002A0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PreviewEnabled = " + this.PreviewEnabled.ToString() + ",");
			stringBuilder.Append("DogfoodEnabled = " + this.DogfoodEnabled.ToString() + ",");
			stringBuilder.Append("DevelopmentEnabled = " + this.DevelopmentEnabled.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x04000028 RID: 40
		private readonly ExposureLevel exposureLevel;
	}
}
