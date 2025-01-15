using System;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.SharePoint;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x02001907 RID: 6407
	internal sealed class SharePointFolderDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A36E RID: 41838 RVA: 0x0021D754 File Offset: 0x0021B954
		public SharePointFolderDataSourceLocation()
		{
			base.Protocol = "sharepoint-folder";
		}

		// Token: 0x170029D0 RID: 10704
		// (get) Token: 0x0600A36F RID: 41839 RVA: 0x00060B78 File Offset: 0x0005ED78
		public override string ResourceKind
		{
			get
			{
				return "SharePoint";
			}
		}

		// Token: 0x170029D1 RID: 10705
		// (get) Token: 0x0600A370 RID: 41840 RVA: 0x0021AD62 File Offset: 0x00218F62
		public override string FriendlyName
		{
			get
			{
				return base.GetWebSourceFriendlyName();
			}
		}

		// Token: 0x0600A371 RID: 41841 RVA: 0x0021AD6A File Offset: 0x00218F6A
		public override bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			return base.TryResolveUri(getHostEntry, out resolvedLocation);
		}

		// Token: 0x0600A372 RID: 41842 RVA: 0x0021D768 File Offset: 0x0021B968
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			RecordValue recordValue = null;
			try
			{
				recordValue = SharePointModule.FilesOptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			bool? flag;
			recordValue = DataSourceLocation.GetAndRemoveHierarchicalNavigation(recordValue, out flag);
			return DataSourceLocation.FormatInvocation(flag.GetValueOrDefault() ? "SharePoint.Contents" : "SharePoint.Files", 1, new object[]
			{
				TextValue.New(base.Address.GetStringOrNull("url")),
				recordValue
			});
		}

		// Token: 0x0600A373 RID: 41843 RVA: 0x0021A118 File Offset: 0x00218318
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetUrlResource(out resource);
		}

		// Token: 0x04005502 RID: 21762
		public static readonly DataSourceLocationFactory Factory = new SharePointFolderDataSourceLocation.DslFactory();

		// Token: 0x02001908 RID: 6408
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x170029D2 RID: 10706
			// (get) Token: 0x0600A375 RID: 41845 RVA: 0x0021D7FC File Offset: 0x0021B9FC
			public override string Protocol
			{
				get
				{
					return "sharepoint-folder";
				}
			}

			// Token: 0x0600A376 RID: 41846 RVA: 0x0021D803 File Offset: 0x0021BA03
			public override IDataSourceLocation New()
			{
				return new SharePointFolderDataSourceLocation();
			}
		}
	}
}
