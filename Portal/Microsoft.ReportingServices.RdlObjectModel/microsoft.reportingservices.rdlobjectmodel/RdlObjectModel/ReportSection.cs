using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001EE RID: 494
	public class ReportSection : ReportObject
	{
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x000267DB File Offset: 0x000249DB
		// (set) Token: 0x06001064 RID: 4196 RVA: 0x000267EE File Offset: 0x000249EE
		public Body Body
		{
			get
			{
				return (Body)base.PropertyStore.GetObject(0);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Body");
				}
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x0002680B File Offset: 0x00024A0B
		// (set) Token: 0x06001066 RID: 4198 RVA: 0x00026819 File Offset: 0x00024A19
		public ReportSize Width
		{
			get
			{
				return base.PropertyStore.GetSize(1);
			}
			set
			{
				base.PropertyStore.SetSize(1, value);
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00026828 File Offset: 0x00024A28
		// (set) Token: 0x06001068 RID: 4200 RVA: 0x0002683B File Offset: 0x00024A3B
		public Page Page
		{
			get
			{
				return (Page)base.PropertyStore.GetObject(2);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Page");
				}
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x00026858 File Offset: 0x00024A58
		// (set) Token: 0x0600106A RID: 4202 RVA: 0x0002686B File Offset: 0x00024A6B
		[DefaultValue("")]
		public string DataElementName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x0002687A File Offset: 0x00024A7A
		// (set) Token: 0x0600106C RID: 4204 RVA: 0x00026888 File Offset: 0x00024A88
		[DefaultValue(DataElementOutputTypes.Auto)]
		[ValidEnumValues("ReportItemDataElementOutputTypes")]
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				return (DataElementOutputTypes)base.PropertyStore.GetInteger(4);
			}
			set
			{
				base.PropertyStore.SetInteger(4, (int)value);
			}
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00026897 File Offset: 0x00024A97
		public ReportSection()
		{
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0002689F File Offset: 0x00024A9F
		internal ReportSection(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x000268A8 File Offset: 0x00024AA8
		public override void Initialize()
		{
			base.Initialize();
			this.Width = Constants.DefaultZeroSize;
			this.Body = new Body();
			this.Page = new Page();
		}

		// Token: 0x020003F9 RID: 1017
		internal class Definition : DefinitionStore<ReportSection, ReportSection.Definition.Properties>
		{
			// Token: 0x060018BB RID: 6331 RVA: 0x0003BB97 File Offset: 0x00039D97
			private Definition()
			{
			}

			// Token: 0x0200050B RID: 1291
			internal enum Properties
			{
				// Token: 0x040010D9 RID: 4313
				Body,
				// Token: 0x040010DA RID: 4314
				Width,
				// Token: 0x040010DB RID: 4315
				Page,
				// Token: 0x040010DC RID: 4316
				DataElementName,
				// Token: 0x040010DD RID: 4317
				DataElementOutput
			}
		}
	}
}
