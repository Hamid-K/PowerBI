using System;
using System.Linq;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x0200089E RID: 2206
	internal sealed class UnboundFunctionValue : QueryResultFunctionValue
	{
		// Token: 0x06003F37 RID: 16183 RVA: 0x000CFBD5 File Offset: 0x000CDDD5
		public UnboundFunctionValue(ODataEnvironment environment, FunctionTypeValue type, Tuple<string, Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue> functionImport)
			: base(environment, functionImport.Item2.Name, environment.Host, type)
		{
			this.functionImport = functionImport;
		}

		// Token: 0x06003F38 RID: 16184 RVA: 0x000CFBFC File Offset: 0x000CDDFC
		protected override Value InvokeN(Value[] args)
		{
			ODataFunctionExtensions.ValidateArgs<Microsoft.OData.Edm.IEdmFunctionImport>(args);
			int num = args.Length;
			Microsoft.OData.Edm.IEdmFunctionImport item = this.functionImport.Item2;
			Microsoft.OData.Edm.IEdmOperationParameter[] array = item.Function.Parameters.ToArray<Microsoft.OData.Edm.IEdmOperationParameter>();
			OperationSegmentParameter[] array2 = new OperationSegmentParameter[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = new OperationSegmentParameter(array[i].Name, ODataExpression.BuildConstantNode(args[i]));
			}
			ODataEnvironment odataEnvironment = (ODataEnvironment)base.Environment;
			if (this.Type.AsFunctionType.ReturnType.IsTable)
			{
				Microsoft.OData.Edm.IEdmEntitySet edmEntitySet;
				item.TryGetStaticEntitySet(out edmEntitySet);
				Microsoft.OData.Edm.IEdmEntityType edmEntityType = item.Function.ReturnType.AsCollection().ElementType().AsEntity()
					.EntityDefinition();
				return new QueryTableValue(new OptimizableQuery(new ODataQuery(odataEnvironment, item, edmEntitySet, edmEntityType, array2, this.Type.AsFunctionType.ReturnType.AsTableType.ItemType.AsRecordType)));
			}
			return ODataResponse.GetResponseValue(ODataQueryBuilderWrapper.GetUnboundFunctionUri(odataEnvironment.ServiceUri, item, array2).GetUri(), odataEnvironment);
		}

		// Token: 0x04002139 RID: 8505
		private readonly Tuple<string, Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue> functionImport;
	}
}
