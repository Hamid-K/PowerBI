using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004A3 RID: 1187
	public class EdmFunction : EdmType
	{
		// Token: 0x06003A30 RID: 14896 RVA: 0x000C05D5 File Offset: 0x000BE7D5
		internal EdmFunction(string name, string namespaceName, DataSpace dataSpace)
			: this(name, namespaceName, dataSpace, new EdmFunctionPayload())
		{
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x000C05E8 File Offset: 0x000BE7E8
		internal EdmFunction(string name, string namespaceName, DataSpace dataSpace, EdmFunctionPayload payload)
			: base(name, namespaceName, dataSpace)
		{
			this._schemaName = payload.Schema;
			IList<FunctionParameter> list = payload.ReturnParameters ?? new FunctionParameter[0];
			foreach (FunctionParameter functionParameter in list)
			{
				if (functionParameter == null)
				{
					throw new ArgumentException(Strings.ADP_CollectionParameterElementIsNull("ReturnParameters"));
				}
				if (functionParameter.Mode != ParameterMode.ReturnValue)
				{
					throw new ArgumentException(Strings.NonReturnParameterInReturnParameterCollection);
				}
			}
			this._returnParameters = new ReadOnlyMetadataCollection<FunctionParameter>(list.Select((FunctionParameter returnParameter) => SafeLink<EdmFunction>.BindChild<FunctionParameter>(this, FunctionParameter.DeclaringFunctionLinker, returnParameter)).ToList<FunctionParameter>());
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
			if (payload.IsFunctionImport != null)
			{
				EdmFunction.SetFunctionAttribute(ref this._functionAttributes, EdmFunction.FunctionAttributes.IsFunctionImport, payload.IsFunctionImport.Value);
			}
			if (payload.ParameterTypeSemantics != null)
			{
				this._parameterTypeSemantics = payload.ParameterTypeSemantics.Value;
			}
			if (payload.StoreFunctionName != null)
			{
				this._storeFunctionNameAttribute = payload.StoreFunctionName;
			}
			if (payload.EntitySets != null)
			{
				if (payload.EntitySets.Count != list.Count)
				{
					throw new ArgumentException(Strings.NumberOfEntitySetsDoesNotMatchNumberOfReturnParameters);
				}
				this._entitySets = new ReadOnlyCollection<EntitySet>(payload.EntitySets);
			}
			else
			{
				if (this._returnParameters.Count > 1)
				{
					throw new ArgumentException(Strings.NullEntitySetsForFunctionReturningMultipleResultSets);
				}
				this._entitySets = new ReadOnlyCollection<EntitySet>(this._returnParameters.Select((FunctionParameter p) => null).ToList<EntitySet>());
			}
			if (payload.CommandText != null)
			{
				this._commandTextAttribute = payload.CommandText;
			}
			if (payload.Parameters != null)
			{
				foreach (FunctionParameter functionParameter2 in payload.Parameters)
				{
					if (functionParameter2 == null)
					{
						throw new ArgumentException(Strings.ADP_CollectionParameterElementIsNull("parameters"));
					}
					if (functionParameter2.Mode == ParameterMode.ReturnValue)
					{
						throw new ArgumentException(Strings.ReturnParameterInInputParameterCollection);
					}
				}
				this._parameters = new SafeLinkCollection<EdmFunction, FunctionParameter>(this, FunctionParameter.DeclaringFunctionLinker, new MetadataCollection<FunctionParameter>(payload.Parameters));
				return;
			}
			this._parameters = new ReadOnlyMetadataCollection<FunctionParameter>(new MetadataCollection<FunctionParameter>());
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06003A32 RID: 14898 RVA: 0x000C094C File Offset: 0x000BEB4C
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EdmFunction;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06003A33 RID: 14899 RVA: 0x000C0950 File Offset: 0x000BEB50
		public override string FullName
		{
			get
			{
				return this.NamespaceName + "." + this.Name;
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06003A34 RID: 14900 RVA: 0x000C0968 File Offset: 0x000BEB68
		public ReadOnlyMetadataCollection<FunctionParameter> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x000C0970 File Offset: 0x000BEB70
		public void AddParameter(FunctionParameter functionParameter)
		{
			Check.NotNull<FunctionParameter>(functionParameter, "functionParameter");
			Util.ThrowIfReadOnly(this);
			if (functionParameter.Mode == ParameterMode.ReturnValue)
			{
				throw new ArgumentException(Strings.ReturnParameterInInputParameterCollection);
			}
			this._parameters.Source.Add(functionParameter);
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06003A36 RID: 14902 RVA: 0x000C09A9 File Offset: 0x000BEBA9
		internal bool HasUserDefinedBody
		{
			get
			{
				return this.IsModelDefinedFunction && !string.IsNullOrEmpty(this.CommandTextAttribute);
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06003A37 RID: 14903 RVA: 0x000C09C3 File Offset: 0x000BEBC3
		[MetadataProperty(BuiltInTypeKind.EntitySet, false)]
		internal EntitySet EntitySet
		{
			get
			{
				if (this._entitySets.Count == 0)
				{
					return null;
				}
				return this._entitySets[0];
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06003A38 RID: 14904 RVA: 0x000C09E0 File Offset: 0x000BEBE0
		[MetadataProperty(BuiltInTypeKind.EntitySet, true)]
		internal ReadOnlyCollection<EntitySet> EntitySets
		{
			get
			{
				return this._entitySets;
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06003A39 RID: 14905 RVA: 0x000C09E8 File Offset: 0x000BEBE8
		[MetadataProperty(BuiltInTypeKind.FunctionParameter, false)]
		public FunctionParameter ReturnParameter
		{
			get
			{
				return this._returnParameters.FirstOrDefault<FunctionParameter>();
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06003A3A RID: 14906 RVA: 0x000C09F5 File Offset: 0x000BEBF5
		[MetadataProperty(BuiltInTypeKind.FunctionParameter, true)]
		public ReadOnlyMetadataCollection<FunctionParameter> ReturnParameters
		{
			get
			{
				return this._returnParameters;
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06003A3B RID: 14907 RVA: 0x000C09FD File Offset: 0x000BEBFD
		// (set) Token: 0x06003A3C RID: 14908 RVA: 0x000C0A05 File Offset: 0x000BEC05
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string StoreFunctionNameAttribute
		{
			get
			{
				return this._storeFunctionNameAttribute;
			}
			set
			{
				Check.NotEmpty(value, "value");
				Util.ThrowIfReadOnly(this);
				this._storeFunctionNameAttribute = value;
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06003A3D RID: 14909 RVA: 0x000C0A20 File Offset: 0x000BEC20
		internal string FunctionName
		{
			get
			{
				return this.StoreFunctionNameAttribute ?? this.Name;
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06003A3E RID: 14910 RVA: 0x000C0A32 File Offset: 0x000BEC32
		[MetadataProperty(typeof(ParameterTypeSemantics), false)]
		public ParameterTypeSemantics ParameterTypeSemanticsAttribute
		{
			get
			{
				return this._parameterTypeSemantics;
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06003A3F RID: 14911 RVA: 0x000C0A3A File Offset: 0x000BEC3A
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public bool AggregateAttribute
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.Aggregate);
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06003A40 RID: 14912 RVA: 0x000C0A43 File Offset: 0x000BEC43
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public virtual bool BuiltInAttribute
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.BuiltIn);
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06003A41 RID: 14913 RVA: 0x000C0A4C File Offset: 0x000BEC4C
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public bool IsFromProviderManifest
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.IsFromProviderManifest);
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06003A42 RID: 14914 RVA: 0x000C0A56 File Offset: 0x000BEC56
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public bool NiladicFunctionAttribute
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.NiladicFunction);
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06003A43 RID: 14915 RVA: 0x000C0A5F File Offset: 0x000BEC5F
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public bool IsComposableAttribute
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.IsComposable);
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06003A44 RID: 14916 RVA: 0x000C0A68 File Offset: 0x000BEC68
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string CommandTextAttribute
		{
			get
			{
				return this._commandTextAttribute;
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06003A45 RID: 14917 RVA: 0x000C0A70 File Offset: 0x000BEC70
		internal bool IsCachedStoreFunction
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.IsCachedStoreFunction);
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06003A46 RID: 14918 RVA: 0x000C0A7A File Offset: 0x000BEC7A
		internal bool IsModelDefinedFunction
		{
			get
			{
				return this.DataSpace == DataSpace.CSpace && !this.IsCachedStoreFunction && !this.IsFromProviderManifest && !this.IsFunctionImport;
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06003A47 RID: 14919 RVA: 0x000C0AA0 File Offset: 0x000BECA0
		internal bool IsFunctionImport
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.IsFunctionImport);
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06003A48 RID: 14920 RVA: 0x000C0AAA File Offset: 0x000BECAA
		// (set) Token: 0x06003A49 RID: 14921 RVA: 0x000C0AB2 File Offset: 0x000BECB2
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Schema
		{
			get
			{
				return this._schemaName;
			}
			set
			{
				Check.NotEmpty(value, "value");
				Util.ThrowIfReadOnly(this);
				this._schemaName = value;
			}
		}

		// Token: 0x06003A4A RID: 14922 RVA: 0x000C0AD0 File Offset: 0x000BECD0
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.Parameters.Source.SetReadOnly();
				foreach (FunctionParameter functionParameter in this.ReturnParameters)
				{
					functionParameter.SetReadOnly();
				}
			}
		}

		// Token: 0x06003A4B RID: 14923 RVA: 0x000C0B40 File Offset: 0x000BED40
		internal override void BuildIdentity(StringBuilder builder)
		{
			if (base.CacheIdentity != null)
			{
				builder.Append(base.CacheIdentity);
				return;
			}
			EdmFunction.BuildIdentity<FunctionParameter>(builder, this.FullName, this.Parameters, (FunctionParameter param) => param.TypeUsage, (FunctionParameter param) => param.Mode);
		}

		// Token: 0x06003A4C RID: 14924 RVA: 0x000C0BB4 File Offset: 0x000BEDB4
		internal static string BuildIdentity(string functionName, IEnumerable<TypeUsage> functionParameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			EdmFunction.BuildIdentity<TypeUsage>(stringBuilder, functionName, functionParameters, (TypeUsage param) => param, (TypeUsage param) => ParameterMode.In);
			return stringBuilder.ToString();
		}

		// Token: 0x06003A4D RID: 14925 RVA: 0x000C0C14 File Offset: 0x000BEE14
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

		// Token: 0x06003A4E RID: 14926 RVA: 0x000C0CB0 File Offset: 0x000BEEB0
		private bool GetFunctionAttribute(EdmFunction.FunctionAttributes attribute)
		{
			return attribute == (attribute & this._functionAttributes);
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x000C0CBD File Offset: 0x000BEEBD
		private static void SetFunctionAttribute(ref EdmFunction.FunctionAttributes field, EdmFunction.FunctionAttributes attribute, bool isSet)
		{
			if (isSet)
			{
				field |= attribute;
				return;
			}
			field ^= field & attribute;
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x000C0CD4 File Offset: 0x000BEED4
		public static EdmFunction Create(string name, string namespaceName, DataSpace dataSpace, EdmFunctionPayload payload, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(namespaceName, "namespaceName");
			EdmFunction edmFunction = new EdmFunction(name, namespaceName, dataSpace, payload);
			if (metadataProperties != null)
			{
				edmFunction.AddMetadataProperties(metadataProperties);
			}
			edmFunction.SetReadOnly();
			return edmFunction;
		}

		// Token: 0x04001406 RID: 5126
		private readonly ReadOnlyMetadataCollection<FunctionParameter> _returnParameters;

		// Token: 0x04001407 RID: 5127
		private readonly ReadOnlyMetadataCollection<FunctionParameter> _parameters;

		// Token: 0x04001408 RID: 5128
		private readonly EdmFunction.FunctionAttributes _functionAttributes = EdmFunction.FunctionAttributes.IsComposable;

		// Token: 0x04001409 RID: 5129
		private string _storeFunctionNameAttribute;

		// Token: 0x0400140A RID: 5130
		private readonly ParameterTypeSemantics _parameterTypeSemantics;

		// Token: 0x0400140B RID: 5131
		private readonly string _commandTextAttribute;

		// Token: 0x0400140C RID: 5132
		private string _schemaName;

		// Token: 0x0400140D RID: 5133
		private readonly ReadOnlyCollection<EntitySet> _entitySets;

		// Token: 0x02000ACB RID: 2763
		[Flags]
		private enum FunctionAttributes : byte
		{
			// Token: 0x04002BA5 RID: 11173
			Aggregate = 1,
			// Token: 0x04002BA6 RID: 11174
			BuiltIn = 2,
			// Token: 0x04002BA7 RID: 11175
			NiladicFunction = 4,
			// Token: 0x04002BA8 RID: 11176
			IsComposable = 8,
			// Token: 0x04002BA9 RID: 11177
			IsFromProviderManifest = 16,
			// Token: 0x04002BAA RID: 11178
			IsCachedStoreFunction = 32,
			// Token: 0x04002BAB RID: 11179
			IsFunctionImport = 64,
			// Token: 0x04002BAC RID: 11180
			Default = 8
		}
	}
}
