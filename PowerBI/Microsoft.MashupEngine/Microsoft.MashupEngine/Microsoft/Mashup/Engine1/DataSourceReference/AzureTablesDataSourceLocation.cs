using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.AzureTables;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018D1 RID: 6353
	internal sealed class AzureTablesDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A1FF RID: 41471 RVA: 0x00219E5D File Offset: 0x0021805D
		public AzureTablesDataSourceLocation()
		{
			base.Protocol = "azure-tables";
		}

		// Token: 0x1700296B RID: 10603
		// (get) Token: 0x0600A200 RID: 41472 RVA: 0x00156A9B File Offset: 0x00154C9B
		public override string ResourceKind
		{
			get
			{
				return "AzureTables";
			}
		}

		// Token: 0x1700296C RID: 10604
		// (get) Token: 0x0600A201 RID: 41473 RVA: 0x00219E70 File Offset: 0x00218070
		public override string FriendlyName
		{
			get
			{
				string stringOrNull = base.Address.GetStringOrNull("account");
				if (stringOrNull == null)
				{
					return base.Protocol;
				}
				string stringOrNull2 = base.Address.GetStringOrNull("name");
				if (string.IsNullOrEmpty(stringOrNull2))
				{
					return this.GetAzureTablesAccountUrl(stringOrNull, base.Address.GetStringOrNull("domain"));
				}
				return this.GetAzureTablesContainerPath(stringOrNull, base.Address.GetStringOrNull("domain"), stringOrNull2);
			}
		}

		// Token: 0x0600A202 RID: 41474 RVA: 0x00219EE4 File Offset: 0x002180E4
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			RecordValue recordValue = null;
			try
			{
				recordValue = AzureTablesModule.AzureTablesOptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			string stringOrNull = base.Address.GetStringOrNull("account");
			if (stringOrNull == null)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
			}
			string stringOrNull2 = base.Address.GetStringOrNull("domain");
			string text = ((stringOrNull2 != null) ? this.GetAzureTablesAccountUrl(stringOrNull, stringOrNull2) : stringOrNull);
			string stringOrNull3 = base.Address.GetStringOrNull("name");
			if (stringOrNull3 == null)
			{
				return DataSourceLocation.FormatInvocation("AzureStorage.Tables", 1, new object[] { text, recordValue });
			}
			ExpressionBuilder instance = ExpressionBuilder.Instance;
			return new FormulaCreationResult(instance.Let(new VariableInitializer[]
			{
				instance.Declare("Source", instance.Invoke("AzureStorage.Tables", 1, new object[] { text, recordValue }), true),
				instance.Declare("Table", instance.Navigate(instance.Identifier("Source"), "Name", stringOrNull3, "Data"), true)
			}));
		}

		// Token: 0x0600A203 RID: 41475 RVA: 0x0021A014 File Offset: 0x00218214
		public string GetAzureTablesContainerPath(string account, string domain, string containerName)
		{
			return DataSourceLocation.GetAzureContainerPath(account, domain ?? "table.core.windows.net", containerName);
		}

		// Token: 0x0600A204 RID: 41476 RVA: 0x0021A027 File Offset: 0x00218227
		public string GetAzureTablesAccountUrl(string account, string domain = null)
		{
			return DataSourceLocation.GetAzureAccountUrl(account, domain ?? "table.core.windows.net", null).Uri.AbsoluteUri;
		}

		// Token: 0x0600A205 RID: 41477 RVA: 0x0021A044 File Offset: 0x00218244
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetAzureResource("table.core.windows.net", out resource);
		}

		// Token: 0x040054AD RID: 21677
		public static readonly DataSourceLocationFactory Factory = new AzureTablesDataSourceLocation.DslFactory();

		// Token: 0x040054AE RID: 21678
		private const string DefaultDomain = "table.core.windows.net";

		// Token: 0x040054AF RID: 21679
		private const string AzureStorage_Tables_Function = "AzureStorage.Tables";

		// Token: 0x020018D2 RID: 6354
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x1700296D RID: 10605
			// (get) Token: 0x0600A207 RID: 41479 RVA: 0x0021A05E File Offset: 0x0021825E
			public override string Protocol
			{
				get
				{
					return "azure-tables";
				}
			}

			// Token: 0x0600A208 RID: 41480 RVA: 0x0021A065 File Offset: 0x00218265
			public override IDataSourceLocation New()
			{
				return new AzureTablesDataSourceLocation();
			}

			// Token: 0x0600A209 RID: 41481 RVA: 0x0021A06C File Offset: 0x0021826C
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return DataSourceLocationFactory.TryCreateAzureLocation("azure-tables", resourcePath, out location);
			}
		}
	}
}
