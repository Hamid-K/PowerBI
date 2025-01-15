using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000187 RID: 391
	internal abstract class ModelActionBase : ProgressivePackageActionBase
	{
		// Token: 0x06000E4B RID: 3659 RVA: 0x000346FD File Offset: 0x000328FD
		protected ModelActionBase(string itemPath, CatalogItemContext itemContext, Stream outputStream, IList<string> responseFlags, RSService service)
			: this(itemPath, itemContext, outputStream, responseFlags, service, false)
		{
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00034710 File Offset: 0x00032910
		protected ModelActionBase(string itemPath, CatalogItemContext itemContext, Stream outputStream, IList<string> responseFlags, RSService service, bool messageWriterLazyInitialization)
			: base(outputStream, responseFlags, service, messageWriterLazyInitialization)
		{
			RSTrace.CatalogTrace.Assert(itemPath != null, "ModelActionBase.ctor: itemPath != null");
			RSTrace.CatalogTrace.Assert(itemContext != null, "ModelActionBase.ctor: itemContext != null");
			this.m_itemPath = itemPath;
			this.m_itemContext = itemContext;
			this.m_dataSourceResolver = ModelActionBase.m_modelDataSourceResolverFactory.CreateDataSourceResolver(this.m_itemPath, this.m_itemContext, service);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0003477C File Offset: 0x0003297C
		protected override void ExecuteAction()
		{
			if (!this.m_itemContext.SetPath(this.m_itemPath))
			{
				throw new InvalidItemPathException(this.m_itemPath);
			}
			using (this.m_provider.EnterStorageContext(null))
			{
				this.InternalExecute();
			}
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x000347E0 File Offset: 0x000329E0
		protected virtual void WriteModelToOutput(string modelContent)
		{
			using (Stream stream = base.MessageWriter.CreateWritableStream("modelDefinition"))
			{
				using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8))
				{
					streamWriter.Write(modelContent);
				}
			}
		}

		// Token: 0x06000E4F RID: 3663
		protected abstract void InternalExecute();

		// Token: 0x040005E3 RID: 1507
		protected static readonly IModelDataSourceResolverFactory m_modelDataSourceResolverFactory = ModelDataSourceResolverFactory.Instance;

		// Token: 0x040005E4 RID: 1508
		protected readonly string m_itemPath;

		// Token: 0x040005E5 RID: 1509
		protected readonly CatalogItemContext m_itemContext;

		// Token: 0x040005E6 RID: 1510
		protected IModelDataSourceResolver m_dataSourceResolver;
	}
}
