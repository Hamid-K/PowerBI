using System;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x0200190B RID: 6411
	internal sealed class SodaDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A383 RID: 41859 RVA: 0x0021D8B8 File Offset: 0x0021BAB8
		public SodaDataSourceLocation()
		{
			base.Protocol = "soda";
		}

		// Token: 0x170029D6 RID: 10710
		// (get) Token: 0x0600A384 RID: 41860 RVA: 0x000378A3 File Offset: 0x00035AA3
		public override string ResourceKind
		{
			get
			{
				return "Web";
			}
		}

		// Token: 0x170029D7 RID: 10711
		// (get) Token: 0x0600A385 RID: 41861 RVA: 0x0021AD62 File Offset: 0x00218F62
		public override string FriendlyName
		{
			get
			{
				return base.GetWebSourceFriendlyName();
			}
		}

		// Token: 0x0600A386 RID: 41862 RVA: 0x0021AD6A File Offset: 0x00218F6A
		public override bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			return base.TryResolveUri(getHostEntry, out resolvedLocation);
		}

		// Token: 0x0600A387 RID: 41863 RVA: 0x0021D8CC File Offset: 0x0021BACC
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			try
			{
				OptionRecordDefinition.Empty.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			return DataSourceLocation.FormatInvocation("Soda.Feed", 1, new object[] { base.Address.GetStringOrNull("url") });
		}

		// Token: 0x0600A388 RID: 41864 RVA: 0x0021A118 File Offset: 0x00218318
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetUrlResource(out resource);
		}

		// Token: 0x04005504 RID: 21764
		public static readonly DataSourceLocationFactory Factory = new SodaDataSourceLocation.DslFactory();

		// Token: 0x04005505 RID: 21765
		private const string Soda_Feed_Formula = "Soda.Feed";

		// Token: 0x0200190C RID: 6412
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x170029D8 RID: 10712
			// (get) Token: 0x0600A38A RID: 41866 RVA: 0x0021D93C File Offset: 0x0021BB3C
			public override string Protocol
			{
				get
				{
					return "soda";
				}
			}

			// Token: 0x0600A38B RID: 41867 RVA: 0x0021D943 File Offset: 0x0021BB43
			public override IDataSourceLocation New()
			{
				return new SodaDataSourceLocation();
			}
		}
	}
}
