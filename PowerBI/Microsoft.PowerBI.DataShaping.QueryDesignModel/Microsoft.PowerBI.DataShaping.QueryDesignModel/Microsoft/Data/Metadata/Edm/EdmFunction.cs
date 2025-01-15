using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000080 RID: 128
	public sealed class EdmFunction : EdmType
	{
		// Token: 0x06000985 RID: 2437 RVA: 0x00016E60 File Offset: 0x00015060
		internal EdmFunction(string name, string namespaceName, DataSpace dataSpace, EdmFunctionPayload payload)
			: base(name, namespaceName, dataSpace)
		{
			this._schemaName = payload.Schema;
			this._fullName = base.NamespaceName + "." + base.Name;
			if (payload.ReturnParameter != null)
			{
				this._returnParameter = SafeLink<EdmFunction>.BindChild<FunctionParameter>(this, FunctionParameter.DeclaringFunctionLinker, payload.ReturnParameter);
			}
			if (payload.IsAggregate != null)
			{
				EdmFunction.SetFunctionAttribute(ref this._functionAttributes, EdmFunction.FunctionAttributes.Aggregate, payload.IsAggregate.Value);
			}
			if (payload.IsBuiltIn != null)
			{
				EdmFunction.SetFunctionAttribute(ref this._functionAttributes, EdmFunction.FunctionAttributes.BuiltIn, payload.IsBuiltIn.Value);
			}
			if (payload.IsNiladic != null)
			{
				EdmFunction.SetFunctionAttribute(ref this._functionAttributes, EdmFunction.FunctionAttributes.NiladicFunction, payload.IsNiladic.Value);
			}
			if (payload.IsComposable != null)
			{
				EdmFunction.SetFunctionAttribute(ref this._functionAttributes, EdmFunction.FunctionAttributes.IsComposable, payload.IsComposable.Value);
			}
			if (payload.IsFromProviderManifest != null)
			{
				EdmFunction.SetFunctionAttribute(ref this._functionAttributes, EdmFunction.FunctionAttributes.IsFromProviderManifest, payload.IsFromProviderManifest.Value);
			}
			if (payload.IsCachedStoreFunction != null)
			{
				EdmFunction.SetFunctionAttribute(ref this._functionAttributes, EdmFunction.FunctionAttributes.IsCachedStoreFunction, payload.IsCachedStoreFunction.Value);
			}
			if (payload.ParameterTypeSemantics != null)
			{
				this._parameterTypeSemantics = payload.ParameterTypeSemantics.Value;
			}
			if (payload.StoreFunctionName != null)
			{
				this._storeFunctionNameAttribute = payload.StoreFunctionName;
			}
			if (payload.EntitySet != null)
			{
				this._entitySet = payload.EntitySet;
			}
			if (payload.CommandText != null)
			{
				this._commandTextAttribute = payload.CommandText;
			}
			if (payload.Parameters != null)
			{
				FunctionParameter[] parameters = payload.Parameters;
				for (int i = 0; i < parameters.Length; i++)
				{
					if (parameters[i] == null)
					{
						throw EntityUtil.CollectionParameterElementIsNull("parameters");
					}
				}
				this._parameters = new SafeLinkCollection<EdmFunction, FunctionParameter>(this, FunctionParameter.DeclaringFunctionLinker, new MetadataCollection<FunctionParameter>(payload.Parameters));
				return;
			}
			this._parameters = new ReadOnlyMetadataCollection<FunctionParameter>(new MetadataCollection<FunctionParameter>());
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x00017096 File Offset: 0x00015296
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EdmFunction;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0001709A File Offset: 0x0001529A
		public override string FullName
		{
			get
			{
				return this._fullName;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x000170A2 File Offset: 0x000152A2
		public ReadOnlyMetadataCollection<FunctionParameter> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x000170AA File Offset: 0x000152AA
		internal bool HasUserDefinedBody
		{
			get
			{
				return base.DataSpace == DataSpace.CSpace && !string.IsNullOrEmpty(this.CommandTextAttribute);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x000170C5 File Offset: 0x000152C5
		[MetadataProperty(BuiltInTypeKind.EntitySet, false)]
		internal EntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x000170CD File Offset: 0x000152CD
		[MetadataProperty(BuiltInTypeKind.FunctionParameter, false)]
		public FunctionParameter ReturnParameter
		{
			get
			{
				return this._returnParameter;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x000170D5 File Offset: 0x000152D5
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		internal string StoreFunctionNameAttribute
		{
			get
			{
				return this._storeFunctionNameAttribute;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x000170DD File Offset: 0x000152DD
		[MetadataProperty(typeof(ParameterTypeSemantics), false)]
		internal ParameterTypeSemantics ParameterTypeSemanticsAttribute
		{
			get
			{
				return this._parameterTypeSemantics;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x000170E5 File Offset: 0x000152E5
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		internal bool AggregateAttribute
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.Aggregate);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x000170EE File Offset: 0x000152EE
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		internal bool BuiltInAttribute
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.BuiltIn);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x000170F7 File Offset: 0x000152F7
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		internal bool IsFromProviderManifest
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.IsFromProviderManifest);
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00017101 File Offset: 0x00015301
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		internal bool NiladicFunctionAttribute
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.NiladicFunction);
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x0001710A File Offset: 0x0001530A
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		internal bool IsComposableAttribute
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.IsComposable);
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x00017113 File Offset: 0x00015313
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string CommandTextAttribute
		{
			get
			{
				return this._commandTextAttribute;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0001711B File Offset: 0x0001531B
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		internal string Schema
		{
			get
			{
				return this._schemaName;
			}
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00017124 File Offset: 0x00015324
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.Parameters.Source.SetReadOnly();
				FunctionParameter returnParameter = this.ReturnParameter;
				if (returnParameter != null)
				{
					returnParameter.SetReadOnly();
				}
			}
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00017160 File Offset: 0x00015360
		internal override void BuildIdentity(StringBuilder builder)
		{
			if (base.CacheIdentity != null)
			{
				builder.Append(base.CacheIdentity);
				return;
			}
			EdmFunction.BuildIdentity<FunctionParameter>(builder, this.FullName, this.Parameters, (FunctionParameter param) => param.TypeUsage, (FunctionParameter param) => param.Mode);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x000171D4 File Offset: 0x000153D4
		internal static string BuildIdentity(string functionName, IEnumerable<TypeUsage> functionParameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			EdmFunction.BuildIdentity<TypeUsage>(stringBuilder, functionName, functionParameters, (TypeUsage param) => param, (TypeUsage param) => ParameterMode.In);
			return stringBuilder.ToString();
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00017234 File Offset: 0x00015434
		internal static void BuildIdentity<TParameterMetadata>(StringBuilder builder, string functionName, IEnumerable<TParameterMetadata> functionParameters, Func<TParameterMetadata, TypeUsage> getParameterTypeUsage, Func<TParameterMetadata, ParameterMode> getParameterMode)
		{
			builder.Append(functionName);
			builder.Append('(');
			bool flag = true;
			foreach (TParameterMetadata tparameterMetadata in functionParameters)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					builder.Append(",");
				}
				builder.Append(Helper.ToString(getParameterMode(tparameterMetadata)));
				builder.Append(' ');
				getParameterTypeUsage(tparameterMetadata).BuildIdentity(builder);
			}
			builder.Append(')');
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x000172D0 File Offset: 0x000154D0
		private bool GetFunctionAttribute(EdmFunction.FunctionAttributes attribute)
		{
			return attribute == (attribute & this._functionAttributes);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x000172DD File Offset: 0x000154DD
		private static void SetFunctionAttribute(ref EdmFunction.FunctionAttributes field, EdmFunction.FunctionAttributes attribute, bool isSet)
		{
			if (isSet)
			{
				field |= attribute;
				return;
			}
			field ^= field & attribute;
		}

		// Token: 0x040007F5 RID: 2037
		private readonly FunctionParameter _returnParameter;

		// Token: 0x040007F6 RID: 2038
		private readonly ReadOnlyMetadataCollection<FunctionParameter> _parameters;

		// Token: 0x040007F7 RID: 2039
		private readonly EdmFunction.FunctionAttributes _functionAttributes = EdmFunction.FunctionAttributes.IsComposable;

		// Token: 0x040007F8 RID: 2040
		private readonly string _storeFunctionNameAttribute;

		// Token: 0x040007F9 RID: 2041
		private readonly ParameterTypeSemantics _parameterTypeSemantics;

		// Token: 0x040007FA RID: 2042
		private readonly string _commandTextAttribute;

		// Token: 0x040007FB RID: 2043
		private readonly string _schemaName;

		// Token: 0x040007FC RID: 2044
		private readonly EntitySet _entitySet;

		// Token: 0x040007FD RID: 2045
		private readonly string _fullName;

		// Token: 0x020002B8 RID: 696
		[Flags]
		private enum FunctionAttributes : byte
		{
			// Token: 0x04000FAE RID: 4014
			None = 0,
			// Token: 0x04000FAF RID: 4015
			Aggregate = 1,
			// Token: 0x04000FB0 RID: 4016
			BuiltIn = 2,
			// Token: 0x04000FB1 RID: 4017
			NiladicFunction = 4,
			// Token: 0x04000FB2 RID: 4018
			IsComposable = 8,
			// Token: 0x04000FB3 RID: 4019
			IsFromProviderManifest = 16,
			// Token: 0x04000FB4 RID: 4020
			IsCachedStoreFunction = 32,
			// Token: 0x04000FB5 RID: 4021
			Default = 8
		}
	}
}
