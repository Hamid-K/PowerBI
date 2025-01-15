using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000321 RID: 801
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class TextRunInstance : ReportElementInstance
	{
		// Token: 0x06001DC4 RID: 7620 RVA: 0x000750DE File Offset: 0x000732DE
		internal TextRunInstance(TextRun textRunDef)
			: base(textRunDef)
		{
		}

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x06001DC5 RID: 7621
		public abstract string UniqueName { get; }

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x06001DC6 RID: 7622
		public abstract string Value { get; }

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x06001DC7 RID: 7623
		public abstract object OriginalValue { get; }

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x000750E7 File Offset: 0x000732E7
		public virtual string ToolTip
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x06001DC9 RID: 7625 RVA: 0x000750EA File Offset: 0x000732EA
		public TextRun Definition
		{
			get
			{
				return (TextRun)this.m_reportElementDef;
			}
		}

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x06001DCA RID: 7626
		public abstract MarkupType MarkupType { get; }

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x06001DCB RID: 7627
		public abstract TypeCode TypeCode { get; }

		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x06001DCC RID: 7628
		public abstract bool IsCompiled { get; }

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x06001DCD RID: 7629
		public abstract bool ProcessedWithError { get; }

		// Token: 0x06001DCE RID: 7630 RVA: 0x000750F7 File Offset: 0x000732F7
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_uniqueName = null;
		}

		// Token: 0x04000F68 RID: 3944
		protected string m_uniqueName;
	}
}
