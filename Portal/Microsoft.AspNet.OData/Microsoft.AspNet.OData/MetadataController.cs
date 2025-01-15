using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Csdl;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000052 RID: 82
	public class MetadataController : ODataController
	{
		// Token: 0x06000239 RID: 569 RVA: 0x0000A6AE File Offset: 0x000088AE
		public IEdmModel GetMetadata()
		{
			return this.GetModel();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000A6B8 File Offset: 0x000088B8
		public ODataServiceDocument GetServiceDocument()
		{
			IEdmModel model = this.GetModel();
			ODataServiceDocument odataServiceDocument = new ODataServiceDocument();
			IEdmEntityContainer entityContainer = model.EntityContainer;
			odataServiceDocument.EntitySets = from e in entityContainer.EntitySets()
				select MetadataController.GetODataEntitySetInfo(model.GetNavigationSourceUrl(e).ToString(), e.Name);
			IEnumerable<IEdmSingleton> enumerable = entityContainer.Elements.OfType<IEdmSingleton>();
			odataServiceDocument.Singletons = enumerable.Select((IEdmSingleton e) => MetadataController.GetODataSingletonInfo(model.GetNavigationSourceUrl(e).ToString(), e.Name));
			IEnumerable<IEdmFunctionImport> enumerable2 = from f in entityContainer.Elements.OfType<IEdmFunctionImport>()
				where !f.Function.Parameters.Any<IEdmOperationParameter>() && f.IncludeInServiceDocument
				select f;
			odataServiceDocument.FunctionImports = from f in enumerable2.Distinct(new FunctionImportComparer())
				select MetadataController.GetODataFunctionImportInfo(f.Name);
			return odataServiceDocument;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000A78E File Offset: 0x0000898E
		private static ODataEntitySetInfo GetODataEntitySetInfo(string url, string name)
		{
			return new ODataEntitySetInfo
			{
				Name = name,
				Url = new Uri(url, UriKind.Relative)
			};
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000A7A9 File Offset: 0x000089A9
		private static ODataSingletonInfo GetODataSingletonInfo(string url, string name)
		{
			return new ODataSingletonInfo
			{
				Name = name,
				Url = new Uri(url, UriKind.Relative)
			};
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000A7C4 File Offset: 0x000089C4
		private static ODataFunctionImportInfo GetODataFunctionImportInfo(string name)
		{
			return new ODataFunctionImportInfo
			{
				Name = name,
				Url = new Uri(name, UriKind.Relative)
			};
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000A7E0 File Offset: 0x000089E0
		private IEdmModel GetModel()
		{
			IEdmModel model = base.Request.GetModel();
			if (model == null)
			{
				throw Error.InvalidOperation(SRResources.RequestMustHaveModel, new object[0]);
			}
			model.SetEdmxVersion(MetadataController._defaultEdmxVersion);
			return model;
		}

		// Token: 0x040000B6 RID: 182
		private static readonly Version _defaultEdmxVersion = new Version(4, 0);
	}
}
