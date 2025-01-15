using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Core;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000835 RID: 2101
	internal sealed class BoundFunctionValue : QueryResultFunctionValue
	{
		// Token: 0x06003C7E RID: 15486 RVA: 0x000C43C4 File Offset: 0x000C25C4
		public BoundFunctionValue(ODataEnvironment environment, FunctionTypeValue type, Dictionary<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue> functions, ODataEntry entry, Microsoft.OData.Edm.IEdmEntitySetBase entryEntitySet)
			: base(environment, functions.First<KeyValuePair<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue>>().Key.Name, environment.Host, type)
		{
			this.functions = functions;
			this.entry = entry;
			this.entryEntitySet = entryEntitySet;
		}

		// Token: 0x06003C7F RID: 15487 RVA: 0x000C4410 File Offset: 0x000C2610
		protected override Value InvokeN(Value[] args)
		{
			ODataFunctionExtensions.ValidateArgs<Microsoft.OData.Edm.IEdmFunction>(args);
			Value[] array = args.Where((Value e) => !e.IsNull).ToArray<Value>();
			int num = array.Length;
			Microsoft.OData.Edm.IEdmFunction edmFunction = this.functions.FindMatchOverload(array, this.entryEntitySet.EntityType().FullTypeName());
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
				if (entitySet != null)
				{
					return new QueryTableValue(new OptimizableQuery(new ODataQuery(this.entry.ReadLink, odataEnvironment, edmFunction, entitySet, edmEntityType, array3, this.Type.AsFunctionType.ReturnType.AsTableType.ItemType.AsRecordType)));
				}
			}
			return ODataResponse.GetResponseValue(ODataQueryBuilderWrapper.GetBoundFunctionUri(this.entry.ReadLink, edmFunction, array3).GetUri(), odataEnvironment);
		}

		// Token: 0x06003C80 RID: 15488 RVA: 0x000C4558 File Offset: 0x000C2758
		private Microsoft.OData.Edm.IEdmEntitySetBase GetEntitySet(ODataEnvironment environment, Microsoft.OData.Edm.IEdmFunction function, Microsoft.OData.Edm.IEdmEntityType type)
		{
			Microsoft.OData.Edm.IEdmEntitySetBase edmEntitySetBase = null;
			Microsoft.OData.Edm.Expressions.IEdmPathExpression entitySetPath = function.EntitySetPath;
			if (entitySetPath != null)
			{
				edmEntitySetBase = this.entryEntitySet;
				string[] array = entitySetPath.Path.ToArray<string>();
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

		// Token: 0x04001F9E RID: 8094
		private readonly Dictionary<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue> functions;

		// Token: 0x04001F9F RID: 8095
		private readonly ODataEntry entry;

		// Token: 0x04001FA0 RID: 8096
		private readonly Microsoft.OData.Edm.IEdmEntitySetBase entryEntitySet;
	}
}
