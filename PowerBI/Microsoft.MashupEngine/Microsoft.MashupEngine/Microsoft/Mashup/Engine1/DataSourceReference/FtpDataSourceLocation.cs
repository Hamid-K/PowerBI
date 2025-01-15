using System;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018E3 RID: 6371
	internal sealed class FtpDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A27B RID: 41595 RVA: 0x0021AD48 File Offset: 0x00218F48
		public FtpDataSourceLocation()
		{
			base.Protocol = "ftp";
		}

		// Token: 0x1700298A RID: 10634
		// (get) Token: 0x0600A27C RID: 41596 RVA: 0x0021AD5B File Offset: 0x00218F5B
		public override string ResourceKind
		{
			get
			{
				return "Ftp";
			}
		}

		// Token: 0x1700298B RID: 10635
		// (get) Token: 0x0600A27D RID: 41597 RVA: 0x0021AD62 File Offset: 0x00218F62
		public override string FriendlyName
		{
			get
			{
				return base.GetWebSourceFriendlyName();
			}
		}

		// Token: 0x0600A27E RID: 41598 RVA: 0x0021AD6A File Offset: 0x00218F6A
		public override bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			return base.TryResolveUri(getHostEntry, out resolvedLocation);
		}

		// Token: 0x0600A27F RID: 41599 RVA: 0x0021AD74 File Offset: 0x00218F74
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
			FormulaCreationResult formulaCreationResult = DataSourceLocation.FormatInvocation("Web.Contents", 1, new object[] { base.Address.GetStringOrNull("url") });
			if (formulaCreationResult.Success)
			{
				return DataSourceLocation.CreateContentTypeFormula(base.Address.GetStringOrNull("contentType"), formulaCreationResult);
			}
			return formulaCreationResult;
		}

		// Token: 0x0600A280 RID: 41600 RVA: 0x0021A118 File Offset: 0x00218318
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetUrlResource(out resource);
		}

		// Token: 0x040054C6 RID: 21702
		public static readonly DataSourceLocationFactory Factory = new FtpDataSourceLocation.DslFactory();

		// Token: 0x040054C7 RID: 21703
		private const string Web_Contents_Function = "Web.Contents";

		// Token: 0x020018E4 RID: 6372
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x1700298C RID: 10636
			// (get) Token: 0x0600A282 RID: 41602 RVA: 0x0021AE04 File Offset: 0x00219004
			public override string Protocol
			{
				get
				{
					return "ftp";
				}
			}

			// Token: 0x0600A283 RID: 41603 RVA: 0x0021AE0B File Offset: 0x0021900B
			public override IDataSourceLocation New()
			{
				return new FtpDataSourceLocation();
			}

			// Token: 0x0600A284 RID: 41604 RVA: 0x0021AE12 File Offset: 0x00219012
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return DataSourceLocationFactory.TryCreateUrlLocation("ftp", resourcePath, out location);
			}
		}
	}
}
