using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.BusinessIntelligence;

namespace Microsoft.BIServer.Owin.Common.Services
{
	// Token: 0x02000014 RID: 20
	[ExcludeFromCodeCoverage]
	public sealed class ExposureControl : IExposureControl
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002C98 File Offset: 0x00000E98
		public bool PreviewEnabled
		{
			get
			{
				return ExposureControlProvider.Instance.ExposureControl.PreviewEnabled;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002CA9 File Offset: 0x00000EA9
		public bool DogfoodEnabled
		{
			get
			{
				return ExposureControlProvider.Instance.ExposureControl.DogfoodEnabled;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002CBA File Offset: 0x00000EBA
		public bool DevelopmentEnabled
		{
			get
			{
				return ExposureControlProvider.Instance.ExposureControl.DevelopmentEnabled;
			}
		}
	}
}
