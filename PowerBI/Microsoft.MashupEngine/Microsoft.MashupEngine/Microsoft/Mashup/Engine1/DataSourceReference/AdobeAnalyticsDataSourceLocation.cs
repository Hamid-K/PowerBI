using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.AdobeAnalytics;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018C6 RID: 6342
	internal sealed class AdobeAnalyticsDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A1B2 RID: 41394 RVA: 0x00218CCF File Offset: 0x00216ECF
		public AdobeAnalyticsDataSourceLocation()
		{
			base.Protocol = "adobe-analytics";
		}

		// Token: 0x17002953 RID: 10579
		// (get) Token: 0x0600A1B3 RID: 41395 RVA: 0x00165DEE File Offset: 0x00163FEE
		public override string ResourceKind
		{
			get
			{
				return "AdobeAnalytics";
			}
		}

		// Token: 0x17002954 RID: 10580
		// (get) Token: 0x0600A1B4 RID: 41396 RVA: 0x00218CE2 File Offset: 0x00216EE2
		public override string FriendlyName
		{
			get
			{
				return "Adobe Analytics";
			}
		}

		// Token: 0x17002955 RID: 10581
		// (get) Token: 0x0600A1B5 RID: 41397 RVA: 0x00218CE9 File Offset: 0x00216EE9
		public override IEnumerable<string> DisplayAddressFields
		{
			get
			{
				return new string[] { "cube" };
			}
		}

		// Token: 0x0600A1B6 RID: 41398 RVA: 0x00218CFC File Offset: 0x00216EFC
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			RecordValue recordValue;
			try
			{
				recordValue = AdobeAnalyticsModule.OptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			ExpressionBuilder instance = ExpressionBuilder.Instance;
			IExpression expression = instance.Invoke("AdobeAnalytics.Cubes", 0, new object[] { recordValue });
			string stringOrNull = base.Address.GetStringOrNull("cube");
			if (stringOrNull != null)
			{
				expression = instance.Navigate(expression, "Name", stringOrNull, "Data");
			}
			return new FormulaCreationResult(expression);
		}

		// Token: 0x0600A1B7 RID: 41399 RVA: 0x00218D88 File Offset: 0x00216F88
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetSingletonResource(out resource);
		}

		// Token: 0x04005498 RID: 21656
		public static readonly DataSourceLocationFactory Factory = new AdobeAnalyticsDataSourceLocation.DslFactory();

		// Token: 0x04005499 RID: 21657
		private const string AdobeAnalytics_Cube_Function = "AdobeAnalytics.Cubes";

		// Token: 0x0400549A RID: 21658
		public const string Cube = "cube";

		// Token: 0x020018C7 RID: 6343
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x17002956 RID: 10582
			// (get) Token: 0x0600A1B9 RID: 41401 RVA: 0x00218D9D File Offset: 0x00216F9D
			public override string Protocol
			{
				get
				{
					return "adobe-analytics";
				}
			}

			// Token: 0x0600A1BA RID: 41402 RVA: 0x00218DA4 File Offset: 0x00216FA4
			public override IDataSourceLocation New()
			{
				return new AdobeAnalyticsDataSourceLocation();
			}

			// Token: 0x0600A1BB RID: 41403 RVA: 0x00218DAB File Offset: 0x00216FAB
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				location = this.New();
				return true;
			}
		}
	}
}
