using System;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.SharePoint;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x02001909 RID: 6409
	internal sealed class SharePointListDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A378 RID: 41848 RVA: 0x0021D80A File Offset: 0x0021BA0A
		public SharePointListDataSourceLocation()
		{
			base.Protocol = "sharepoint-list";
		}

		// Token: 0x170029D3 RID: 10707
		// (get) Token: 0x0600A379 RID: 41849 RVA: 0x00060B78 File Offset: 0x0005ED78
		public override string ResourceKind
		{
			get
			{
				return "SharePoint";
			}
		}

		// Token: 0x170029D4 RID: 10708
		// (get) Token: 0x0600A37A RID: 41850 RVA: 0x0021AD62 File Offset: 0x00218F62
		public override string FriendlyName
		{
			get
			{
				return base.GetWebSourceFriendlyName();
			}
		}

		// Token: 0x0600A37B RID: 41851 RVA: 0x0021AD6A File Offset: 0x00218F6A
		public override bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			return base.TryResolveUri(getHostEntry, out resolvedLocation);
		}

		// Token: 0x0600A37C RID: 41852 RVA: 0x0021D820 File Offset: 0x0021BA20
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			RecordValue recordValue = null;
			try
			{
				recordValue = SharePointModule.TablesOptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			return DataSourceLocation.FormatInvocation("SharePoint.Tables", 1, new object[]
			{
				TextValue.New(base.Address.GetStringOrNull("url")),
				recordValue
			});
		}

		// Token: 0x0600A37D RID: 41853 RVA: 0x0021A118 File Offset: 0x00218318
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetUrlResource(out resource);
		}

		// Token: 0x04005503 RID: 21763
		public static readonly DataSourceLocationFactory Factory = new SharePointListDataSourceLocation.DslFactory();

		// Token: 0x0200190A RID: 6410
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x170029D5 RID: 10709
			// (get) Token: 0x0600A37F RID: 41855 RVA: 0x0021D89C File Offset: 0x0021BA9C
			public override string Protocol
			{
				get
				{
					return "sharepoint-list";
				}
			}

			// Token: 0x0600A380 RID: 41856 RVA: 0x0021D8A3 File Offset: 0x0021BAA3
			public override IDataSourceLocation New()
			{
				return new SharePointListDataSourceLocation();
			}

			// Token: 0x0600A381 RID: 41857 RVA: 0x0021D8AA File Offset: 0x0021BAAA
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return DataSourceLocationFactory.TryCreateUrlLocation("sharepoint-list", resourcePath, out location);
			}
		}
	}
}
