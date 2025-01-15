using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7
{
	// Token: 0x02000766 RID: 1894
	internal sealed class BoundFunctionValue : QueryResultFunctionValue
	{
		// Token: 0x060037DE RID: 14302 RVA: 0x000B30CC File Offset: 0x000B12CC
		public BoundFunctionValue(ODataEnvironment environment, FunctionTypeValue type, Dictionary<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue> functions, ODataResource resource, Microsoft.OData.Edm.IEdmEntitySetBase resourceEntitySet)
			: base(environment, functions.First<KeyValuePair<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue>>().Key.Name, environment.Host, type)
		{
			this.functions = functions;
			this.resource = resource;
			this.resourceEntitySet = resourceEntitySet;
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x000B3118 File Offset: 0x000B1318
		protected override Value InvokeN(Value[] args)
		{
			ODataFunctionExtensions.ValidateArgs<Microsoft.OData.Edm.IEdmFunction>(args);
			Value[] array = args.Where((Value e) => !e.IsNull).ToArray<Value>();
			int num = array.Length;
			Microsoft.OData.Edm.IEdmFunction edmFunction = this.functions.FindMatchOverload(array, this.resourceEntitySet.EntityType().FullTypeName());
			Microsoft.OData.Edm.IEdmOperationParameter[] array2 = edmFunction.Parameters.ToArray<Microsoft.OData.Edm.IEdmOperationParameter>();
			OperationSegmentParameter[] array3 = new OperationSegmentParameter[num];
			for (int i = 0; i < num; i++)
			{
				array3[i] = new OperationSegmentParameter(array2[i + 1].Name, ODataExpression.BuildConstantNode(array[i]));
			}
			ODataEnvironment odataEnvironment = (ODataEnvironment)base.Environment;
			Microsoft.OData.Edm.IEdmCollectionTypeReference edmCollectionTypeReference = edmFunction.ReturnType.AsCollection();
			if (edmCollectionTypeReference != null)
			{
				Microsoft.OData.Edm.IEdmEntityType edmEntityType = edmCollectionTypeReference.ElementType().AsEntity().EntityDefinition();
				Microsoft.OData.Edm.IEdmEntitySetBase entitySet = this.GetEntitySet(odataEnvironment, edmFunction, edmEntityType);
				ODataPath odataPath;
				if (entitySet != null && odataEnvironment.TryConvertToOpaquePath(this.resource.ReadLink, out odataPath))
				{
					ODataPath odataPath2 = new ODataPath(odataPath.Concat(new ODataPathSegment[]
					{
						new OperationSegment(new Microsoft.OData.Edm.IEdmOperation[] { edmFunction }, array3, entitySet)
					}));
					return new QueryTableValue(new OptimizableQuery(new ODataQuery(odataEnvironment, odataPath2, entitySet, edmEntityType, this.Type.AsFunctionType.ReturnType.AsTableType.ItemType.AsRecordType, odataEnvironment.Annotations.GetElementCapability(edmFunction), edmFunction.IsComposable)));
				}
			}
			return ODataResponse.GetResponseValue(ODataQueryBuilderWrapper.GetBoundFunctionUri(this.resource.ReadLink, edmFunction, array3).GetUri(), odataEnvironment, this.Type.AsFunctionType.ReturnType);
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x000B32BC File Offset: 0x000B14BC
		private Microsoft.OData.Edm.IEdmEntitySetBase GetEntitySet(ODataEnvironment environment, Microsoft.OData.Edm.IEdmFunction function, Microsoft.OData.Edm.IEdmEntityType type)
		{
			Microsoft.OData.Edm.IEdmEntitySetBase edmEntitySetBase = null;
			IEdmPathExpression entitySetPath = function.EntitySetPath;
			if (entitySetPath != null)
			{
				edmEntitySetBase = this.resourceEntitySet;
				string[] array = entitySetPath.PathSegments.ToArray<string>();
				for (int i = 1; i < array.Length; i++)
				{
					Microsoft.OData.Edm.IEdmNavigationProperty edmNavigationProperty = edmEntitySetBase.EntityType().FindProperty(array[i]) as Microsoft.OData.Edm.IEdmNavigationProperty;
					if (edmNavigationProperty == null)
					{
						edmEntitySetBase = null;
						break;
					}
					edmEntitySetBase = (Microsoft.OData.Edm.IEdmEntitySetBase)edmEntitySetBase.FindNavigationTarget(edmNavigationProperty);
				}
			}
			if (edmEntitySetBase == null)
			{
				edmEntitySetBase = (from e in environment.EdmModel.EntityContainer.EntitySets()
					where e.EntityType().Equals(type)
					select e).FirstOrDefault<Microsoft.OData.Edm.IEdmEntitySet>();
			}
			return edmEntitySetBase;
		}

		// Token: 0x04001CE8 RID: 7400
		private readonly Dictionary<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue> functions;

		// Token: 0x04001CE9 RID: 7401
		private readonly ODataResource resource;

		// Token: 0x04001CEA RID: 7402
		private readonly Microsoft.OData.Edm.IEdmEntitySetBase resourceEntitySet;
	}
}
