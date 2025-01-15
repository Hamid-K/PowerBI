using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002C6 RID: 710
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ReportProperty
	{
		// Token: 0x06001ADE RID: 6878 RVA: 0x0006BAF8 File Offset: 0x00069CF8
		internal ReportProperty()
		{
			this.m_isExpression = false;
			this.m_expressionString = null;
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0006BB0E File Offset: 0x00069D0E
		internal ReportProperty(bool isExpression, string expressionString)
		{
			this.m_isExpression = isExpression;
			this.m_expressionString = expressionString;
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x0006BB24 File Offset: 0x00069D24
		public bool IsExpression
		{
			get
			{
				return this.m_isExpression;
			}
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x0006BB2C File Offset: 0x00069D2C
		public string ExpressionString
		{
			get
			{
				return this.m_expressionString;
			}
		}

		// Token: 0x04000D5D RID: 3421
		private bool m_isExpression;

		// Token: 0x04000D5E RID: 3422
		private string m_expressionString;
	}
}
