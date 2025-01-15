using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.GoogleAnalytics;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018E5 RID: 6373
	internal sealed class GoogleAnalyticsDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A286 RID: 41606 RVA: 0x0021AE20 File Offset: 0x00219020
		public GoogleAnalyticsDataSourceLocation()
		{
			base.Protocol = "google-analytics";
		}

		// Token: 0x1700298D RID: 10637
		// (get) Token: 0x0600A287 RID: 41607 RVA: 0x00105113 File Offset: 0x00103313
		public override string ResourceKind
		{
			get
			{
				return "GoogleAnalytics";
			}
		}

		// Token: 0x1700298E RID: 10638
		// (get) Token: 0x0600A288 RID: 41608 RVA: 0x0021AE33 File Offset: 0x00219033
		public override string FriendlyName
		{
			get
			{
				return "Google Analytics";
			}
		}

		// Token: 0x1700298F RID: 10639
		// (get) Token: 0x0600A289 RID: 41609 RVA: 0x0021AE3A File Offset: 0x0021903A
		public override IEnumerable<string> DisplayAddressFields
		{
			get
			{
				return new string[] { "account", "property", "view" };
			}
		}

		// Token: 0x0600A28A RID: 41610 RVA: 0x0021AE5C File Offset: 0x0021905C
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			try
			{
				GoogleAnalyticsModule.OptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			ExpressionBuilder instance = ExpressionBuilder.Instance;
			IExpression expression = instance.Invoke("GoogleAnalytics.Accounts", 0, Array.Empty<object>());
			string stringOrNull = base.Address.GetStringOrNull("account");
			if (stringOrNull != null)
			{
				expression = instance.Navigate(expression, "Name", stringOrNull, "Data");
				string stringOrNull2 = base.Address.GetStringOrNull("property");
				if (stringOrNull2 != null)
				{
					expression = instance.Navigate(expression, "Name", stringOrNull2, "Data");
					string stringOrNull3 = base.Address.GetStringOrNull("view");
					if (stringOrNull3 != null)
					{
						expression = instance.Navigate(expression, "Name", stringOrNull3, "Data");
					}
				}
			}
			return new FormulaCreationResult(expression);
		}

		// Token: 0x0600A28B RID: 41611 RVA: 0x00218D88 File Offset: 0x00216F88
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetSingletonResource(out resource);
		}

		// Token: 0x040054C8 RID: 21704
		public static readonly DataSourceLocationFactory Factory = new GoogleAnalyticsDataSourceLocation.DslFactory();

		// Token: 0x040054C9 RID: 21705
		private const string GoogleAnalytics_Accounts_Function = "GoogleAnalytics.Accounts";

		// Token: 0x040054CA RID: 21706
		public const string Account = "account";

		// Token: 0x040054CB RID: 21707
		public const string Property = "property";

		// Token: 0x040054CC RID: 21708
		public const string View = "view";

		// Token: 0x020018E6 RID: 6374
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x17002990 RID: 10640
			// (get) Token: 0x0600A28D RID: 41613 RVA: 0x0021AF44 File Offset: 0x00219144
			public override string Protocol
			{
				get
				{
					return "google-analytics";
				}
			}

			// Token: 0x0600A28E RID: 41614 RVA: 0x0021AF4B File Offset: 0x0021914B
			public override IDataSourceLocation New()
			{
				return new GoogleAnalyticsDataSourceLocation();
			}

			// Token: 0x0600A28F RID: 41615 RVA: 0x00218DAB File Offset: 0x00216FAB
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				location = this.New();
				return true;
			}
		}
	}
}
