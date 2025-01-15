using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Salesforce;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x02001901 RID: 6401
	internal sealed class SalesforceDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A328 RID: 41768 RVA: 0x0021CA7C File Offset: 0x0021AC7C
		public SalesforceDataSourceLocation()
		{
			base.Protocol = "salesforce-com";
		}

		// Token: 0x170029B8 RID: 10680
		// (get) Token: 0x0600A329 RID: 41769 RVA: 0x00013AF7 File Offset: 0x00011CF7
		public override string ResourceKind
		{
			get
			{
				return "Salesforce";
			}
		}

		// Token: 0x170029B9 RID: 10681
		// (get) Token: 0x0600A32A RID: 41770 RVA: 0x0021CA8F File Offset: 0x0021AC8F
		public override string FriendlyName
		{
			get
			{
				return this.LoginServer ?? base.Protocol;
			}
		}

		// Token: 0x170029BA RID: 10682
		// (get) Token: 0x0600A32B RID: 41771 RVA: 0x0021CAA1 File Offset: 0x0021ACA1
		// (set) Token: 0x0600A32C RID: 41772 RVA: 0x0021CAB3 File Offset: 0x0021ACB3
		public string LoginServer
		{
			get
			{
				return base.Address.GetStringOrNull("loginServer");
			}
			set
			{
				base.Address["loginServer"] = value;
			}
		}

		// Token: 0x170029BB RID: 10683
		// (get) Token: 0x0600A32D RID: 41773 RVA: 0x0021CAC6 File Offset: 0x0021ACC6
		// (set) Token: 0x0600A32E RID: 41774 RVA: 0x0021CAD8 File Offset: 0x0021ACD8
		public string Class
		{
			get
			{
				return base.Address.GetStringOrNull("class");
			}
			set
			{
				base.Address["class"] = value;
			}
		}

		// Token: 0x170029BC RID: 10684
		// (get) Token: 0x0600A32F RID: 41775 RVA: 0x0021A176 File Offset: 0x00218376
		// (set) Token: 0x0600A330 RID: 41776 RVA: 0x0021A188 File Offset: 0x00218388
		public string ItemName
		{
			get
			{
				return base.Address.GetStringOrNull("itemName");
			}
			set
			{
				base.Address["itemName"] = value;
			}
		}

		// Token: 0x0600A331 RID: 41777 RVA: 0x0021CAEC File Offset: 0x0021ACEC
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			if (this.LoginServer == null)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
			}
			string text = null;
			RecordValue recordValue = null;
			try
			{
				string @class = this.Class;
				if (!(@class == "object"))
				{
					if (!(@class == "report-detail"))
					{
						return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
					}
					text = "Salesforce.Reports";
					recordValue = SalesforceModule.SalesforceReportsOptionRecord.FromJson(optionsJson);
				}
				else
				{
					text = "Salesforce.Data";
					recordValue = SalesforceModule.SalesforceDataOptionRecord.FromJson(optionsJson);
				}
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			if (this.ItemName == null)
			{
				return DataSourceLocation.FormatInvocation(text, 1, new object[]
				{
					TextValue.New(this.LoginServer),
					recordValue
				});
			}
			ExpressionBuilder instance = ExpressionBuilder.Instance;
			return new FormulaCreationResult(instance.Navigate(instance.Invoke(text, 1, new object[] { this.LoginServer, recordValue }), "Name", this.ItemName, "Data"));
		}

		// Token: 0x0600A332 RID: 41778 RVA: 0x0021CBEC File Offset: 0x0021ADEC
		public override bool TryGetResource(out IResource resource)
		{
			if (this.LoginServer == null)
			{
				resource = null;
				return false;
			}
			resource = Resource.New(this.ResourceKind, this.LoginServer);
			return true;
		}

		// Token: 0x040054EF RID: 21743
		public static readonly DataSourceLocationFactory Factory = new SalesforceDataSourceLocation.DslFactory();

		// Token: 0x040054F0 RID: 21744
		public const string ObjectClass = "object";

		// Token: 0x040054F1 RID: 21745
		public const string ReportDetailClass = "report-detail";

		// Token: 0x040054F2 RID: 21746
		private const string Salesforce_Data_Function = "Salesforce.Data";

		// Token: 0x040054F3 RID: 21747
		private const string Salesforce_Reports_Function = "Salesforce.Reports";

		// Token: 0x02001902 RID: 6402
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x170029BD RID: 10685
			// (get) Token: 0x0600A334 RID: 41780 RVA: 0x0021CC1B File Offset: 0x0021AE1B
			public override string Protocol
			{
				get
				{
					return "salesforce-com";
				}
			}

			// Token: 0x0600A335 RID: 41781 RVA: 0x0021CC22 File Offset: 0x0021AE22
			public override IDataSourceLocation New()
			{
				return new SalesforceDataSourceLocation();
			}

			// Token: 0x0600A336 RID: 41782 RVA: 0x0021CC2C File Offset: 0x0021AE2C
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				location = new SalesforceDataSourceLocation
				{
					LoginServer = resourcePath
				};
				return true;
			}
		}
	}
}
