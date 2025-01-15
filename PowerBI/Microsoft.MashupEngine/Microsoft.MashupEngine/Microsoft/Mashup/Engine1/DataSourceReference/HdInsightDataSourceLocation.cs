using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018E9 RID: 6377
	internal sealed class HdInsightDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A29C RID: 41628 RVA: 0x0021B078 File Offset: 0x00219278
		public HdInsightDataSourceLocation()
		{
			base.Protocol = "hdinsight";
		}

		// Token: 0x17002994 RID: 10644
		// (get) Token: 0x0600A29D RID: 41629 RVA: 0x001004E6 File Offset: 0x000FE6E6
		public override string ResourceKind
		{
			get
			{
				return "HDInsight";
			}
		}

		// Token: 0x17002995 RID: 10645
		// (get) Token: 0x0600A29E RID: 41630 RVA: 0x0021AD62 File Offset: 0x00218F62
		public override string FriendlyName
		{
			get
			{
				return base.GetWebSourceFriendlyName();
			}
		}

		// Token: 0x0600A29F RID: 41631 RVA: 0x0021B08C File Offset: 0x0021928C
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			RecordValue recordValue = null;
			try
			{
				recordValue = OptionRecordDefinition.HierarchicalNavigation.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			string stringOrNull = base.Address.GetStringOrNull("url");
			if (stringOrNull == null)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
			}
			bool? hierarchicalNavigation = DataSourceLocation.GetHierarchicalNavigation(recordValue);
			string text;
			string text2;
			bool flag = DataSourceLocation.TrySplitAzure(new Uri(stringOrNull), out text, out text2);
			if (hierarchicalNavigation == null && !flag)
			{
				return DataSourceLocation.FormatInvocation("HdInsight.Containers", 1, new object[] { TextValue.New(stringOrNull) });
			}
			bool? flag2 = hierarchicalNavigation;
			bool flag3 = false;
			if ((flag2.GetValueOrDefault() == flag3) & (flag2 != null))
			{
				return DataSourceLocation.FormatInvocation("HdInsight.Contents", 1, new object[] { TextValue.New(stringOrNull) });
			}
			if (flag)
			{
				return DataSourceLocation.FormatInvocation("HdInsight.Files", 2, new object[]
				{
					TextValue.New(text),
					TextValue.New(text2)
				});
			}
			return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, null);
		}

		// Token: 0x0600A2A0 RID: 41632 RVA: 0x0021A118 File Offset: 0x00218318
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetUrlResource(out resource);
		}

		// Token: 0x040054CE RID: 21710
		public static readonly DataSourceLocationFactory Factory = new HdInsightDataSourceLocation.DslFactory();

		// Token: 0x020018EA RID: 6378
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x17002996 RID: 10646
			// (get) Token: 0x0600A2A2 RID: 41634 RVA: 0x0021B1A4 File Offset: 0x002193A4
			public override string Protocol
			{
				get
				{
					return "hdinsight";
				}
			}

			// Token: 0x0600A2A3 RID: 41635 RVA: 0x0021B1AB File Offset: 0x002193AB
			public override IDataSourceLocation New()
			{
				return new HdInsightDataSourceLocation();
			}

			// Token: 0x0600A2A4 RID: 41636 RVA: 0x0021B1B2 File Offset: 0x002193B2
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return DataSourceLocationFactory.TryCreateUrlLocation("hdinsight", resourcePath, out location);
			}
		}
	}
}
