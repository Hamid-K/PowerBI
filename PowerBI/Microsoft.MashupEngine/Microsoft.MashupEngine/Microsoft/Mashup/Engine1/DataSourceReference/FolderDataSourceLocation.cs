using System;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018E1 RID: 6369
	internal sealed class FolderDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A270 RID: 41584 RVA: 0x0021AC89 File Offset: 0x00218E89
		public FolderDataSourceLocation()
		{
			base.Protocol = "folder";
		}

		// Token: 0x17002987 RID: 10631
		// (get) Token: 0x0600A271 RID: 41585 RVA: 0x00110B06 File Offset: 0x0010ED06
		public override string ResourceKind
		{
			get
			{
				return "Folder";
			}
		}

		// Token: 0x17002988 RID: 10632
		// (get) Token: 0x0600A272 RID: 41586 RVA: 0x0021ABC6 File Offset: 0x00218DC6
		public override string FriendlyName
		{
			get
			{
				return base.Address.GetValueOrDefault("path", base.Protocol) as string;
			}
		}

		// Token: 0x0600A273 RID: 41587 RVA: 0x0021ABE3 File Offset: 0x00218DE3
		public override bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			return base.TryResolvePath(getHostEntry, out resolvedLocation);
		}

		// Token: 0x0600A274 RID: 41588 RVA: 0x0021AC9C File Offset: 0x00218E9C
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			RecordValue recordValue = null;
			try
			{
				recordValue = FileModule.FolderOptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			bool? flag;
			recordValue = DataSourceLocation.GetAndRemoveHierarchicalNavigation(recordValue, out flag);
			return DataSourceLocation.FormatInvocation(flag.GetValueOrDefault(true) ? "Folder.Files" : "Folder.Contents", 1, new object[]
			{
				base.Address.GetStringOrNull("path"),
				recordValue
			});
		}

		// Token: 0x0600A275 RID: 41589 RVA: 0x0021AC58 File Offset: 0x00218E58
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetFileResource(out resource);
		}

		// Token: 0x040054C5 RID: 21701
		public static readonly DataSourceLocationFactory Factory = new FolderDataSourceLocation.DslFactory();

		// Token: 0x020018E2 RID: 6370
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x17002989 RID: 10633
			// (get) Token: 0x0600A277 RID: 41591 RVA: 0x0021AD2C File Offset: 0x00218F2C
			public override string Protocol
			{
				get
				{
					return "folder";
				}
			}

			// Token: 0x0600A278 RID: 41592 RVA: 0x0021AD33 File Offset: 0x00218F33
			public override IDataSourceLocation New()
			{
				return new FolderDataSourceLocation();
			}

			// Token: 0x0600A279 RID: 41593 RVA: 0x0021AD3A File Offset: 0x00218F3A
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return DataSourceLocationFactory.TryCreatePathLocation("folder", resourcePath, out location);
			}
		}
	}
}
