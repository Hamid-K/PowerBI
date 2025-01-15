using System;
using System.Linq;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7
{
	// Token: 0x0200077D RID: 1917
	internal sealed class UnboundFunctionValue : QueryResultFunctionValue
	{
		// Token: 0x06003871 RID: 14449 RVA: 0x000B5763 File Offset: 0x000B3963
		public UnboundFunctionValue(ODataEnvironment environment, Uri uri, Microsoft.OData.Edm.IEdmFunctionImport functionImport, FunctionTypeValue type, bool isUriResourcePath = true)
			: base(environment, functionImport.Name, environment.Host, type)
		{
			this.uri = uri;
			this.functionImport = functionImport;
			this.isUriResourcePath = isUriResourcePath;
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x000B5798 File Offset: 0x000B3998
		protected override Value InvokeN(Value[] args)
		{
			ODataFunctionExtensions.ValidateArgs<Microsoft.OData.Edm.IEdmFunctionImport>(args);
			int num = args.Length;
			Microsoft.OData.Edm.IEdmOperationParameter[] array = this.functionImport.Function.Parameters.ToArray<Microsoft.OData.Edm.IEdmOperationParameter>();
			OperationSegmentParameter[] array2 = new OperationSegmentParameter[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = new OperationSegmentParameter(array[i].Name, ODataExpression.BuildConstantNode(args[i]));
			}
			ODataEnvironment odataEnvironment = (ODataEnvironment)base.Environment;
			Uri uri = this.uri;
			Microsoft.OData.Edm.IEdmFunctionImport edmFunctionImport = null;
			if (this.isUriResourcePath)
			{
				string text = uri.Segments[uri.Segments.Length - 1];
				Microsoft.OData.Edm.IEdmFunctionImport edmFunctionImport3;
				if (!text.Equals(this.functionImport.Name, StringComparison.Ordinal))
				{
					Microsoft.OData.Edm.IEdmFunctionImport edmFunctionImport2 = new EdmFunctionImport(this.functionImport.Container, text, this.functionImport.Function);
					edmFunctionImport3 = edmFunctionImport2;
				}
				else
				{
					edmFunctionImport3 = this.functionImport;
				}
				edmFunctionImport = edmFunctionImport3;
			}
			if (this.Type.AsFunctionType.ReturnType.IsTable)
			{
				Microsoft.OData.Edm.IEdmEntitySetBase edmEntitySetBase;
				this.functionImport.TryGetStaticEntitySet(odataEnvironment.EdmModel, out edmEntitySetBase);
				Microsoft.OData.Edm.IEdmEntityType edmEntityType = this.functionImport.Function.ReturnType.AsCollection().ElementType().AsEntity()
					.EntityDefinition();
				ODataPath odataPath;
				if (odataEnvironment.TryConvertToOpaquePath(uri, out odataPath))
				{
					if (edmFunctionImport != null)
					{
						odataPath = new ODataPath(odataPath.Take(odataPath.Count - 1).Concat(new ODataPathSegment[] { ODataQueryBuilderWrapper.CreateOperationImportSegment(edmFunctionImport, edmEntitySetBase, array2) }));
					}
					return new QueryTableValue(new OptimizableQuery(new ODataQuery(odataEnvironment, odataPath, edmEntitySetBase, edmEntityType, this.Type.AsFunctionType.ReturnType.AsTableType.ItemType.AsRecordType, odataEnvironment.Annotations.GetElementCapability(this.functionImport), this.functionImport.Function.IsComposable)));
				}
			}
			if (edmFunctionImport != null)
			{
				uri = ODataQueryBuilderWrapper.GetUnboundFunctionUri(odataEnvironment.ServiceUri, edmFunctionImport, array2).GetUri();
			}
			return ODataResponse.GetResponseValue(uri, odataEnvironment, this.Type.AsFunctionType.ReturnType);
		}

		// Token: 0x04001D2F RID: 7471
		private readonly Uri uri;

		// Token: 0x04001D30 RID: 7472
		private readonly Microsoft.OData.Edm.IEdmFunctionImport functionImport;

		// Token: 0x04001D31 RID: 7473
		private readonly bool isUriResourcePath;
	}
}
