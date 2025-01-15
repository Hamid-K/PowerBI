using System;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Hdfs;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018E7 RID: 6375
	internal sealed class HdfsDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A291 RID: 41617 RVA: 0x0021AF52 File Offset: 0x00219152
		public HdfsDataSourceLocation()
		{
			base.Protocol = "webhdfs";
		}

		// Token: 0x17002991 RID: 10641
		// (get) Token: 0x0600A292 RID: 41618 RVA: 0x0010086A File Offset: 0x000FEA6A
		public override string ResourceKind
		{
			get
			{
				return "Hdfs";
			}
		}

		// Token: 0x17002992 RID: 10642
		// (get) Token: 0x0600A293 RID: 41619 RVA: 0x0021AD62 File Offset: 0x00218F62
		public override string FriendlyName
		{
			get
			{
				return base.GetWebSourceFriendlyName();
			}
		}

		// Token: 0x0600A294 RID: 41620 RVA: 0x0021AD6A File Offset: 0x00218F6A
		public override bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			return base.TryResolveUri(getHostEntry, out resolvedLocation);
		}

		// Token: 0x0600A295 RID: 41621 RVA: 0x0021AF68 File Offset: 0x00219168
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
			FormulaCreationResult formulaCreationResult = DataSourceLocation.FormatInvocation(DataSourceLocation.GetHierarchicalNavigation(recordValue).GetValueOrDefault() ? "Hdfs.Files" : "Hdfs.Contents", 1, new object[] { TextValue.NewOrNull(base.Address.GetStringOrNull("url")) });
			if (formulaCreationResult.Success)
			{
				return DataSourceLocation.CreateContentTypeFormula(base.Address.GetStringOrNull("contentType"), formulaCreationResult);
			}
			return formulaCreationResult;
		}

		// Token: 0x0600A296 RID: 41622 RVA: 0x0021B00C File Offset: 0x0021920C
		public override bool TryGetResource(out IResource resource)
		{
			string stringOrNull = base.Address.GetStringOrNull("url");
			if (string.IsNullOrEmpty(stringOrNull))
			{
				resource = null;
				return false;
			}
			resource = Resource.New(this.ResourceKind, HdfsModule.GetHttpUrl(TextValue.New(stringOrNull)));
			return true;
		}

		// Token: 0x040054CD RID: 21709
		public static readonly DataSourceLocationFactory Factory = new HdfsDataSourceLocation.DslFactory();

		// Token: 0x020018E8 RID: 6376
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x17002993 RID: 10643
			// (get) Token: 0x0600A298 RID: 41624 RVA: 0x0021B05C File Offset: 0x0021925C
			public override string Protocol
			{
				get
				{
					return "webhdfs";
				}
			}

			// Token: 0x0600A299 RID: 41625 RVA: 0x0021B063 File Offset: 0x00219263
			public override IDataSourceLocation New()
			{
				return new HdfsDataSourceLocation();
			}

			// Token: 0x0600A29A RID: 41626 RVA: 0x0021B06A File Offset: 0x0021926A
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return DataSourceLocationFactory.TryCreateUrlLocation("webhdfs", resourcePath, out location);
			}
		}
	}
}
