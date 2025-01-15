using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.RdlExpressions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000256 RID: 598
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class ChartMemberInstance : BaseInstance
	{
		// Token: 0x06001742 RID: 5954 RVA: 0x0005E64C File Offset: 0x0005C84C
		internal ChartMemberInstance(Chart owner, ChartMember memberDef)
			: base(memberDef.ReportScope)
		{
			this.m_owner = owner;
			this.m_memberDef = memberDef;
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x0005E668 File Offset: 0x0005C868
		public object LabelObject
		{
			get
			{
				return this.GetLabelObject().Value;
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x0005E675 File Offset: 0x0005C875
		internal TypeCode LabelTypeCode
		{
			get
			{
				return this.GetLabelObject().TypeCode;
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x0005E684 File Offset: 0x0005C884
		public string Label
		{
			get
			{
				if (!this.m_labelEvaluated)
				{
					this.m_labelEvaluated = true;
					if (this.m_owner.IsOldSnapshot)
					{
						object value = this.GetLabelObject().Value;
						if (value != null)
						{
							this.m_label = value.ToString();
						}
					}
					else
					{
						this.m_label = this.m_memberDef.MemberDefinition.GetFormattedLabelValue(this.GetLabelObject(), this.m_owner.RenderingContext.OdpContext);
					}
				}
				return this.m_label;
			}
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x0005E6FC File Offset: 0x0005C8FC
		private VariantResult GetLabelObject()
		{
			if (this.m_labelObject == null)
			{
				if (this.m_owner.IsOldSnapshot)
				{
					this.m_labelObject = new VariantResult?(new VariantResult(false, ((ShimChartMember)this.m_memberDef).LabelInstanceValue));
				}
				else
				{
					this.m_labelObject = new VariantResult?(this.m_memberDef.MemberDefinition.EvaluateLabel(this, this.m_owner.RenderingContext.OdpContext));
				}
			}
			return this.m_labelObject.Value;
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x0005E77D File Offset: 0x0005C97D
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_labelEvaluated = false;
			this.m_labelObject = null;
		}

		// Token: 0x04000B7C RID: 2940
		protected Chart m_owner;

		// Token: 0x04000B7D RID: 2941
		protected ChartMember m_memberDef;

		// Token: 0x04000B7E RID: 2942
		protected bool m_labelEvaluated;

		// Token: 0x04000B7F RID: 2943
		protected string m_label;

		// Token: 0x04000B80 RID: 2944
		protected StyleInstance m_style;

		// Token: 0x04000B81 RID: 2945
		private VariantResult? m_labelObject;
	}
}
