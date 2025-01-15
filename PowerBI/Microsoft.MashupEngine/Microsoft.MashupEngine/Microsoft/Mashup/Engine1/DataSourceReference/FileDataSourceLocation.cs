using System;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018DF RID: 6367
	internal sealed class FileDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A265 RID: 41573 RVA: 0x0021ABB3 File Offset: 0x00218DB3
		public FileDataSourceLocation()
		{
			base.Protocol = "file";
		}

		// Token: 0x17002984 RID: 10628
		// (get) Token: 0x0600A266 RID: 41574 RVA: 0x0011070B File Offset: 0x0010E90B
		public override string ResourceKind
		{
			get
			{
				return "File";
			}
		}

		// Token: 0x17002985 RID: 10629
		// (get) Token: 0x0600A267 RID: 41575 RVA: 0x0021ABC6 File Offset: 0x00218DC6
		public override string FriendlyName
		{
			get
			{
				return base.Address.GetValueOrDefault("path", base.Protocol) as string;
			}
		}

		// Token: 0x0600A268 RID: 41576 RVA: 0x0021ABE3 File Offset: 0x00218DE3
		public override bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			return base.TryResolvePath(getHostEntry, out resolvedLocation);
		}

		// Token: 0x0600A269 RID: 41577 RVA: 0x0021ABF0 File Offset: 0x00218DF0
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			RecordValue recordValue = null;
			try
			{
				recordValue = FileModule.FileOptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			return DataSourceLocation.FormatInvocation("File.Contents", 1, new object[]
			{
				base.Address.GetStringOrNull("path"),
				recordValue
			});
		}

		// Token: 0x0600A26A RID: 41578 RVA: 0x0021AC58 File Offset: 0x00218E58
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetFileResource(out resource);
		}

		// Token: 0x040054C4 RID: 21700
		public static readonly DataSourceLocationFactory Factory = new FileDataSourceLocation.DslFactory();

		// Token: 0x020018E0 RID: 6368
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x17002986 RID: 10630
			// (get) Token: 0x0600A26C RID: 41580 RVA: 0x0021AC6D File Offset: 0x00218E6D
			public override string Protocol
			{
				get
				{
					return "file";
				}
			}

			// Token: 0x0600A26D RID: 41581 RVA: 0x0021AC74 File Offset: 0x00218E74
			public override IDataSourceLocation New()
			{
				return new FileDataSourceLocation();
			}

			// Token: 0x0600A26E RID: 41582 RVA: 0x0021AC7B File Offset: 0x00218E7B
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return DataSourceLocationFactory.TryCreatePathLocation("file", resourcePath, out location);
			}
		}
	}
}
