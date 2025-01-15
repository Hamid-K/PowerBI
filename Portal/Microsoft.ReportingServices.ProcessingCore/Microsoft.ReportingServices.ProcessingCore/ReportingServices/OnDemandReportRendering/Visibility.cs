using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000378 RID: 888
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class Visibility
	{
		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x06002223 RID: 8739
		public abstract ReportBoolProperty Hidden { get; }

		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x06002224 RID: 8740
		public abstract string ToggleItem { get; }

		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x06002225 RID: 8741
		public abstract SharedHiddenState HiddenState { get; }

		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x06002226 RID: 8742
		public abstract bool RecursiveToggleReceiver { get; }

		// Token: 0x06002227 RID: 8743 RVA: 0x000835F4 File Offset: 0x000817F4
		internal static ReportBoolProperty GetStartHidden(Microsoft.ReportingServices.ReportProcessing.Visibility visibility)
		{
			ReportBoolProperty reportBoolProperty;
			if (visibility == null)
			{
				reportBoolProperty = new ReportBoolProperty();
			}
			else
			{
				reportBoolProperty = new ReportBoolProperty(visibility.Hidden);
			}
			return reportBoolProperty;
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x0008361C File Offset: 0x0008181C
		internal static ReportBoolProperty GetStartHidden(Microsoft.ReportingServices.ReportIntermediateFormat.Visibility visibility)
		{
			ReportBoolProperty reportBoolProperty;
			if (visibility == null)
			{
				reportBoolProperty = new ReportBoolProperty();
			}
			else
			{
				reportBoolProperty = new ReportBoolProperty(visibility.Hidden);
			}
			return reportBoolProperty;
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x00083643 File Offset: 0x00081843
		internal static SharedHiddenState GetHiddenState(Microsoft.ReportingServices.ReportProcessing.Visibility visibility)
		{
			return (SharedHiddenState)Microsoft.ReportingServices.ReportProcessing.Visibility.GetSharedHidden(visibility);
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x0008364B File Offset: 0x0008184B
		internal static SharedHiddenState GetHiddenState(Microsoft.ReportingServices.ReportIntermediateFormat.Visibility visibility)
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Visibility.GetSharedHidden(visibility);
		}

		// Token: 0x040010EB RID: 4331
		protected ReportBoolProperty m_startHidden;
	}
}
