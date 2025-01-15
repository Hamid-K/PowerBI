using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm.Provider
{
	// Token: 0x0200051A RID: 1306
	internal sealed class EdmProviderManifestFunctionBuilder
	{
		// Token: 0x0600405A RID: 16474 RVA: 0x000D892C File Offset: 0x000D6B2C
		internal EdmProviderManifestFunctionBuilder(ReadOnlyCollection<PrimitiveType> edmPrimitiveTypes)
		{
			TypeUsage[] array = new TypeUsage[edmPrimitiveTypes.Count];
			foreach (PrimitiveType primitiveType in edmPrimitiveTypes)
			{
				array[(int)primitiveType.PrimitiveTypeKind] = TypeUsage.Create(primitiveType);
			}
			this.primitiveTypes = array;
		}

		// Token: 0x0600405B RID: 16475 RVA: 0x000D89A0 File Offset: 0x000D6BA0
		internal ReadOnlyCollection<EdmFunction> ToFunctionCollection()
		{
			return new ReadOnlyCollection<EdmFunction>(this.functions);
		}

		// Token: 0x0600405C RID: 16476 RVA: 0x000D89B0 File Offset: 0x000D6BB0
		internal static void ForAllBasePrimitiveTypes(Action<PrimitiveTypeKind> forEachType)
		{
			for (int i = 0; i < 32; i++)
			{
				PrimitiveTypeKind primitiveTypeKind = (PrimitiveTypeKind)i;
				if (!Helper.IsStrongSpatialTypeKind(primitiveTypeKind))
				{
					forEachType(primitiveTypeKind);
				}
			}
		}

		// Token: 0x0600405D RID: 16477 RVA: 0x000D89DC File Offset: 0x000D6BDC
		internal static void ForTypes(IEnumerable<PrimitiveTypeKind> typeKinds, Action<PrimitiveTypeKind> forEachType)
		{
			foreach (PrimitiveTypeKind primitiveTypeKind in typeKinds)
			{
				forEachType(primitiveTypeKind);
			}
		}

		// Token: 0x0600405E RID: 16478 RVA: 0x000D8A24 File Offset: 0x000D6C24
		internal void AddAggregate(string aggregateFunctionName, PrimitiveTypeKind collectionArgumentElementTypeKind)
		{
			this.AddAggregate(collectionArgumentElementTypeKind, aggregateFunctionName, collectionArgumentElementTypeKind);
		}

		// Token: 0x0600405F RID: 16479 RVA: 0x000D8A30 File Offset: 0x000D6C30
		internal void AddAggregate(PrimitiveTypeKind returnTypeKind, string aggregateFunctionName, PrimitiveTypeKind collectionArgumentElementTypeKind)
		{
			FunctionParameter functionParameter = this.CreateReturnParameter(returnTypeKind);
			FunctionParameter functionParameter2 = this.CreateAggregateParameter(collectionArgumentElementTypeKind);
			EdmFunction edmFunction = new EdmFunction(aggregateFunctionName, "Edm", DataSpace.CSpace, new EdmFunctionPayload
			{
				IsAggregate = new bool?(true),
				IsBuiltIn = new bool?(true),
				ReturnParameters = new FunctionParameter[] { functionParameter },
				Parameters = new FunctionParameter[] { functionParameter2 },
				IsFromProviderManifest = new bool?(true)
			});
			edmFunction.SetReadOnly();
			this.functions.Add(edmFunction);
		}

		// Token: 0x06004060 RID: 16480 RVA: 0x000D8AB7 File Offset: 0x000D6CB7
		internal void AddFunction(PrimitiveTypeKind returnType, string functionName)
		{
			this.AddFunction(returnType, functionName, new KeyValuePair<string, PrimitiveTypeKind>[0]);
		}

		// Token: 0x06004061 RID: 16481 RVA: 0x000D8AC7 File Offset: 0x000D6CC7
		internal void AddFunction(PrimitiveTypeKind returnType, string functionName, PrimitiveTypeKind argumentTypeKind, string argumentName)
		{
			this.AddFunction(returnType, functionName, new KeyValuePair<string, PrimitiveTypeKind>[]
			{
				new KeyValuePair<string, PrimitiveTypeKind>(argumentName, argumentTypeKind)
			});
		}

		// Token: 0x06004062 RID: 16482 RVA: 0x000D8AE6 File Offset: 0x000D6CE6
		internal void AddFunction(PrimitiveTypeKind returnType, string functionName, PrimitiveTypeKind argument1TypeKind, string argument1Name, PrimitiveTypeKind argument2TypeKind, string argument2Name)
		{
			this.AddFunction(returnType, functionName, new KeyValuePair<string, PrimitiveTypeKind>[]
			{
				new KeyValuePair<string, PrimitiveTypeKind>(argument1Name, argument1TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument2Name, argument2TypeKind)
			});
		}

		// Token: 0x06004063 RID: 16483 RVA: 0x000D8B15 File Offset: 0x000D6D15
		internal void AddFunction(PrimitiveTypeKind returnType, string functionName, PrimitiveTypeKind argument1TypeKind, string argument1Name, PrimitiveTypeKind argument2TypeKind, string argument2Name, PrimitiveTypeKind argument3TypeKind, string argument3Name)
		{
			this.AddFunction(returnType, functionName, new KeyValuePair<string, PrimitiveTypeKind>[]
			{
				new KeyValuePair<string, PrimitiveTypeKind>(argument1Name, argument1TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument2Name, argument2TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument3Name, argument3TypeKind)
			});
		}

		// Token: 0x06004064 RID: 16484 RVA: 0x000D8B54 File Offset: 0x000D6D54
		internal void AddFunction(PrimitiveTypeKind returnType, string functionName, PrimitiveTypeKind argument1TypeKind, string argument1Name, PrimitiveTypeKind argument2TypeKind, string argument2Name, PrimitiveTypeKind argument3TypeKind, string argument3Name, PrimitiveTypeKind argument4TypeKind, string argument4Name, PrimitiveTypeKind argument5TypeKind, string argument5Name, PrimitiveTypeKind argument6TypeKind, string argument6Name)
		{
			this.AddFunction(returnType, functionName, new KeyValuePair<string, PrimitiveTypeKind>[]
			{
				new KeyValuePair<string, PrimitiveTypeKind>(argument1Name, argument1TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument2Name, argument2TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument3Name, argument3TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument4Name, argument4TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument5Name, argument5TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument6Name, argument6TypeKind)
			});
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x000D8BD0 File Offset: 0x000D6DD0
		internal void AddFunction(PrimitiveTypeKind returnType, string functionName, PrimitiveTypeKind argument1TypeKind, string argument1Name, PrimitiveTypeKind argument2TypeKind, string argument2Name, PrimitiveTypeKind argument3TypeKind, string argument3Name, PrimitiveTypeKind argument4TypeKind, string argument4Name, PrimitiveTypeKind argument5TypeKind, string argument5Name, PrimitiveTypeKind argument6TypeKind, string argument6Name, PrimitiveTypeKind argument7TypeKind, string argument7Name)
		{
			this.AddFunction(returnType, functionName, new KeyValuePair<string, PrimitiveTypeKind>[]
			{
				new KeyValuePair<string, PrimitiveTypeKind>(argument1Name, argument1TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument2Name, argument2TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument3Name, argument3TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument4Name, argument4TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument5Name, argument5TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument6Name, argument6TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument7Name, argument7TypeKind)
			});
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x000D8C5C File Offset: 0x000D6E5C
		private void AddFunction(PrimitiveTypeKind returnType, string functionName, KeyValuePair<string, PrimitiveTypeKind>[] parameterDefinitions)
		{
			FunctionParameter functionParameter = this.CreateReturnParameter(returnType);
			FunctionParameter[] array = parameterDefinitions.Select((KeyValuePair<string, PrimitiveTypeKind> paramDef) => this.CreateParameter(paramDef.Value, paramDef.Key)).ToArray<FunctionParameter>();
			EdmFunction edmFunction = new EdmFunction(functionName, "Edm", DataSpace.CSpace, new EdmFunctionPayload
			{
				IsBuiltIn = new bool?(true),
				ReturnParameters = new FunctionParameter[] { functionParameter },
				Parameters = array,
				IsFromProviderManifest = new bool?(true)
			});
			edmFunction.SetReadOnly();
			this.functions.Add(edmFunction);
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x000D8CDE File Offset: 0x000D6EDE
		private FunctionParameter CreateParameter(PrimitiveTypeKind primitiveParameterType, string parameterName)
		{
			return new FunctionParameter(parameterName, this.primitiveTypes[(int)primitiveParameterType], ParameterMode.In);
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x000D8CEF File Offset: 0x000D6EEF
		private FunctionParameter CreateAggregateParameter(PrimitiveTypeKind collectionParameterTypeElementTypeKind)
		{
			return new FunctionParameter("collection", TypeUsage.Create(this.primitiveTypes[(int)collectionParameterTypeElementTypeKind].EdmType.GetCollectionType()), ParameterMode.In);
		}

		// Token: 0x06004069 RID: 16489 RVA: 0x000D8D13 File Offset: 0x000D6F13
		private FunctionParameter CreateReturnParameter(PrimitiveTypeKind primitiveReturnType)
		{
			return new FunctionParameter("ReturnType", this.primitiveTypes[(int)primitiveReturnType], ParameterMode.ReturnValue);
		}

		// Token: 0x0400166C RID: 5740
		private readonly List<EdmFunction> functions = new List<EdmFunction>();

		// Token: 0x0400166D RID: 5741
		private readonly TypeUsage[] primitiveTypes;
	}
}
