using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Internal;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ProgressivePackaging;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000184 RID: 388
	internal sealed class GetModelAction : GetModelActionBase
	{
		// Token: 0x06000E32 RID: 3634 RVA: 0x000340F0 File Offset: 0x000322F0
		public GetModelAction(IRenderEditSession session, string itemPath, string dataSourceName, string modelMetadataVersion, Stream inputStream, Stream outputStream, IMetadataStore responseMetadata, IList<string> responseFlags, RSService service, CatalogItemContext itemContext)
			: base(session, itemPath, dataSourceName, modelMetadataVersion, inputStream, outputStream, responseFlags, service, itemContext)
		{
			this.m_responseMetadata = responseMetadata;
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0003411A File Offset: 0x0003231A
		public override void AddMetadata(string name, string value)
		{
			base.MessageWriterIsNull();
			this.m_responseMetadata.AddMetadata(name, value);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0003412F File Offset: 0x0003232F
		protected override bool InitializeAction()
		{
			return this.m_inputStream == null || this.m_inputStream.Length <= 0L || base.TryGetProgressivePackageReader(this.m_inputStream, out this.m_reader);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0003415C File Offset: 0x0003235C
		protected override bool PopulateCacheWithInput(ProgressiveCacheEntry entry)
		{
			if (this.m_reader != null)
			{
				Stream stream = this.m_reader.ConsumeOptionalValue<Stream>("dataSources");
				if (stream != null)
				{
					entry.PopulateCacheEntryWithDataSourceInfo(stream);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00034190 File Offset: 0x00032390
		protected override string ResolveModel(ProgressiveCacheEntry entry, bool isDataSourcePresent)
		{
			DataSourceInfo dataSourceInfo = this.m_dataSourceResolver.RebuildAndResolveDataSource(entry, this.m_dataSourceName, isDataSourcePresent, false, RlsUserInfo.Default);
			ModelRetrieval modelRetrieval = new ModelRetrieval("GetModelAction");
			return this.m_dataSourceResolver.ResolveModel(dataSourceInfo, entry, this.m_modelMetadataVersion, modelRetrieval);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x000341D6 File Offset: 0x000323D6
		protected override void FinalCleanup(ErrorCode status)
		{
			if (this.m_reader != null)
			{
				this.m_reader.Dispose();
				this.m_reader = null;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x000341F2 File Offset: 0x000323F2
		protected override string OperationName
		{
			get
			{
				return "GetModel";
			}
		}

		// Token: 0x040005D8 RID: 1496
		private ProgressivePackageReader m_reader;

		// Token: 0x040005D9 RID: 1497
		private IMetadataStore m_responseMetadata;
	}
}
