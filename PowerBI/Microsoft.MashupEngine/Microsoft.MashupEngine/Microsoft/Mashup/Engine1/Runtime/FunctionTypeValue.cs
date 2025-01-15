using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001320 RID: 4896
	public abstract class FunctionTypeValue : TypeValue, IFunctionTypeValue, ITypeValue, IValue
	{
		// Token: 0x06008137 RID: 33079 RVA: 0x001B826F File Offset: 0x001B646F
		public static FunctionTypeValue New(TypeValue returnType, RecordValue parameters, int min)
		{
			return new FunctionTypeValue.CustomFunctionTypeValue(returnType, parameters, min);
		}

		// Token: 0x06008138 RID: 33080 RVA: 0x001B8279 File Offset: 0x001B6479
		public static FunctionTypeValue New(RecordValue signature, int min)
		{
			return FunctionTypeValue.New(signature["ReturnType"].AsType, signature["Parameters"].AsRecord, min);
		}

		// Token: 0x170022DB RID: 8923
		// (get) Token: 0x06008139 RID: 33081 RVA: 0x00140DB6 File Offset: 0x0013EFB6
		public override ValueKind TypeKind
		{
			get
			{
				return ValueKind.Function;
			}
		}

		// Token: 0x170022DC RID: 8924
		// (get) Token: 0x0600813A RID: 33082 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsFunctionType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170022DD RID: 8925
		// (get) Token: 0x0600813B RID: 33083 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override FunctionTypeValue AsFunctionType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170022DE RID: 8926
		// (get) Token: 0x0600813C RID: 33084
		public abstract TypeValue ReturnType { get; }

		// Token: 0x170022DF RID: 8927
		// (get) Token: 0x0600813D RID: 33085
		public abstract RecordValue Parameters { get; }

		// Token: 0x170022E0 RID: 8928
		// (get) Token: 0x0600813E RID: 33086 RVA: 0x00189499 File Offset: 0x00187699
		public int ParameterCount
		{
			get
			{
				return this.Parameters.Keys.Length;
			}
		}

		// Token: 0x0600813F RID: 33087 RVA: 0x001B82A1 File Offset: 0x001B64A1
		public string ParameterName(int index)
		{
			return this.Parameters.Keys[index];
		}

		// Token: 0x06008140 RID: 33088 RVA: 0x001B82B4 File Offset: 0x001B64B4
		public TypeValue ParameterType(int index)
		{
			return this.Parameters[index].AsType;
		}

		// Token: 0x170022E1 RID: 8929
		// (get) Token: 0x06008141 RID: 33089
		public abstract int Min { get; }

		// Token: 0x170022E2 RID: 8930
		// (get) Token: 0x06008142 RID: 33090 RVA: 0x001B82C7 File Offset: 0x001B64C7
		public int Max
		{
			get
			{
				return this.ParameterCount;
			}
		}

		// Token: 0x170022E3 RID: 8931
		// (get) Token: 0x06008143 RID: 33091
		public abstract bool Abstract { get; }

		// Token: 0x06008144 RID: 33092 RVA: 0x001B82CF File Offset: 0x001B64CF
		public override Value NewMeta(RecordValue metaValue)
		{
			if (metaValue.IsEmpty)
			{
				return this;
			}
			return new FunctionTypeValue.MetaFacetsFunctionTypeValue(this, metaValue, this.Facets);
		}

		// Token: 0x06008145 RID: 33093 RVA: 0x001B82E8 File Offset: 0x001B64E8
		public override TypeValue NewFacets(TypeFacets facets)
		{
			if (facets.IsEmpty)
			{
				return this;
			}
			return new FunctionTypeValue.MetaFacetsFunctionTypeValue(this, this.MetaValue, facets);
		}

		// Token: 0x06008146 RID: 33094 RVA: 0x001B8301 File Offset: 0x001B6501
		public override bool IsCompatibleWith(TypeValue other)
		{
			return other.TypeKind == ValueKind.Any || other.NonNullable.Equals(TypeValue.Function) || other.Equals(this);
		}

		// Token: 0x170022E4 RID: 8932
		// (get) Token: 0x06008147 RID: 33095 RVA: 0x001B82C7 File Offset: 0x001B64C7
		int IFunctionTypeValue.ParameterCount
		{
			get
			{
				return this.ParameterCount;
			}
		}

		// Token: 0x06008148 RID: 33096 RVA: 0x001B832A File Offset: 0x001B652A
		string IFunctionTypeValue.ParameterName(int index)
		{
			return this.ParameterName(index);
		}

		// Token: 0x06008149 RID: 33097 RVA: 0x001B8333 File Offset: 0x001B6533
		ITypeValue IFunctionTypeValue.ParameterType(int index)
		{
			return this.ParameterType(index);
		}

		// Token: 0x170022E5 RID: 8933
		// (get) Token: 0x0600814A RID: 33098 RVA: 0x001B833C File Offset: 0x001B653C
		ITypeValue IFunctionTypeValue.ReturnType
		{
			get
			{
				return this.ReturnType;
			}
		}

		// Token: 0x04004684 RID: 18052
		public new static readonly FunctionTypeValue Any = new FunctionTypeValue.PrimitiveFunctionTypeValue();

		// Token: 0x02001321 RID: 4897
		private class PrimitiveFunctionTypeValue : FunctionTypeValue
		{
			// Token: 0x170022E6 RID: 8934
			// (get) Token: 0x0600814D RID: 33101 RVA: 0x001B8358 File Offset: 0x001B6558
			public override TypeValue ReturnType
			{
				get
				{
					throw ValueException.NewExpressionError<Message0>(Strings.ValueException_FunctionTypeIsAbstract, null, null);
				}
			}

			// Token: 0x170022E7 RID: 8935
			// (get) Token: 0x0600814E RID: 33102 RVA: 0x001B8358 File Offset: 0x001B6558
			public override RecordValue Parameters
			{
				get
				{
					throw ValueException.NewExpressionError<Message0>(Strings.ValueException_FunctionTypeIsAbstract, null, null);
				}
			}

			// Token: 0x170022E8 RID: 8936
			// (get) Token: 0x0600814F RID: 33103 RVA: 0x001B8358 File Offset: 0x001B6558
			public override int Min
			{
				get
				{
					throw ValueException.NewExpressionError<Message0>(Strings.ValueException_FunctionTypeIsAbstract, null, null);
				}
			}

			// Token: 0x170022E9 RID: 8937
			// (get) Token: 0x06008150 RID: 33104 RVA: 0x00002105 File Offset: 0x00000305
			public override bool IsNullable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170022EA RID: 8938
			// (get) Token: 0x06008151 RID: 33105 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override TypeValue NonNullable
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170022EB RID: 8939
			// (get) Token: 0x06008152 RID: 33106 RVA: 0x001B8366 File Offset: 0x001B6566
			public override TypeValue Nullable
			{
				get
				{
					if (this.nullableType == null)
					{
						this.nullableType = new FunctionTypeValue.NullableFunctionTypeValue(this);
					}
					return this.nullableType;
				}
			}

			// Token: 0x170022EC RID: 8940
			// (get) Token: 0x06008153 RID: 33107 RVA: 0x00002139 File Offset: 0x00000339
			public override bool Abstract
			{
				get
				{
					return true;
				}
			}

			// Token: 0x04004685 RID: 18053
			private TypeValue nullableType;
		}

		// Token: 0x02001322 RID: 4898
		private class CustomFunctionTypeValue : FunctionTypeValue
		{
			// Token: 0x06008155 RID: 33109 RVA: 0x001B838C File Offset: 0x001B658C
			public CustomFunctionTypeValue(TypeValue returnType, RecordValue parameters, int min)
			{
				bool flag = false;
				for (int i = min; i < parameters.Count; i++)
				{
					if (!parameters[i].AsType.IsNullable)
					{
						flag = true;
					}
				}
				if (flag)
				{
					Value[] array = new Value[parameters.Count];
					for (int j = 0; j < min; j++)
					{
						array[j] = parameters[j];
					}
					for (int k = min; k < array.Length; k++)
					{
						TypeValue asType = parameters[k].AsType;
						array[k] = asType.Nullable.NewMeta(asType.MetaValue).AsType;
					}
					parameters = RecordValue.New(parameters.Keys, array);
				}
				this.returnType = returnType;
				this.parameters = parameters;
				this.min = min;
			}

			// Token: 0x170022ED RID: 8941
			// (get) Token: 0x06008156 RID: 33110 RVA: 0x001B844C File Offset: 0x001B664C
			public override TypeValue ReturnType
			{
				get
				{
					return this.returnType;
				}
			}

			// Token: 0x170022EE RID: 8942
			// (get) Token: 0x06008157 RID: 33111 RVA: 0x001B8454 File Offset: 0x001B6654
			public override RecordValue Parameters
			{
				get
				{
					return this.parameters;
				}
			}

			// Token: 0x170022EF RID: 8943
			// (get) Token: 0x06008158 RID: 33112 RVA: 0x001B845C File Offset: 0x001B665C
			public override int Min
			{
				get
				{
					return this.min;
				}
			}

			// Token: 0x170022F0 RID: 8944
			// (get) Token: 0x06008159 RID: 33113 RVA: 0x00002105 File Offset: 0x00000305
			public override bool IsNullable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170022F1 RID: 8945
			// (get) Token: 0x0600815A RID: 33114 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override TypeValue NonNullable
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170022F2 RID: 8946
			// (get) Token: 0x0600815B RID: 33115 RVA: 0x001B8464 File Offset: 0x001B6664
			public override TypeValue Nullable
			{
				get
				{
					if (this.nullableType == null)
					{
						this.nullableType = new FunctionTypeValue.NullableFunctionTypeValue(this);
					}
					return this.nullableType;
				}
			}

			// Token: 0x170022F3 RID: 8947
			// (get) Token: 0x0600815C RID: 33116 RVA: 0x00002105 File Offset: 0x00000305
			public override bool Abstract
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04004686 RID: 18054
			private readonly TypeValue returnType;

			// Token: 0x04004687 RID: 18055
			private readonly RecordValue parameters;

			// Token: 0x04004688 RID: 18056
			private readonly int min;

			// Token: 0x04004689 RID: 18057
			private TypeValue nullableType;
		}

		// Token: 0x02001323 RID: 4899
		private class NullableFunctionTypeValue : FunctionTypeValue
		{
			// Token: 0x0600815D RID: 33117 RVA: 0x001B8480 File Offset: 0x001B6680
			public NullableFunctionTypeValue(FunctionTypeValue type)
			{
				this.type = type;
			}

			// Token: 0x170022F4 RID: 8948
			// (get) Token: 0x0600815E RID: 33118 RVA: 0x001B848F File Offset: 0x001B668F
			public override TypeValue ReturnType
			{
				get
				{
					return this.type.ReturnType;
				}
			}

			// Token: 0x170022F5 RID: 8949
			// (get) Token: 0x0600815F RID: 33119 RVA: 0x001B849C File Offset: 0x001B669C
			public override RecordValue Parameters
			{
				get
				{
					return this.type.Parameters;
				}
			}

			// Token: 0x170022F6 RID: 8950
			// (get) Token: 0x06008160 RID: 33120 RVA: 0x001B84A9 File Offset: 0x001B66A9
			public override int Min
			{
				get
				{
					return this.type.Min;
				}
			}

			// Token: 0x170022F7 RID: 8951
			// (get) Token: 0x06008161 RID: 33121 RVA: 0x00002139 File Offset: 0x00000339
			public override bool IsNullable
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170022F8 RID: 8952
			// (get) Token: 0x06008162 RID: 33122 RVA: 0x001B84B6 File Offset: 0x001B66B6
			public override TypeValue NonNullable
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x170022F9 RID: 8953
			// (get) Token: 0x06008163 RID: 33123 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override TypeValue Nullable
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170022FA RID: 8954
			// (get) Token: 0x06008164 RID: 33124 RVA: 0x001B84BE File Offset: 0x001B66BE
			public override bool Abstract
			{
				get
				{
					return this.type.Abstract;
				}
			}

			// Token: 0x0400468A RID: 18058
			private readonly FunctionTypeValue type;
		}

		// Token: 0x02001324 RID: 4900
		private class MetaFacetsFunctionTypeValue : FunctionTypeValue
		{
			// Token: 0x06008165 RID: 33125 RVA: 0x001B84CB File Offset: 0x001B66CB
			public MetaFacetsFunctionTypeValue(FunctionTypeValue type, RecordValue meta, TypeFacets facets)
			{
				this.type = type;
				this.meta = meta;
				this.facets = facets;
			}

			// Token: 0x06008166 RID: 33126 RVA: 0x001B84E8 File Offset: 0x001B66E8
			private static TypeValue New(FunctionTypeValue type, RecordValue meta, TypeFacets facets)
			{
				if (!meta.IsEmpty || !facets.IsEmpty)
				{
					return new FunctionTypeValue.MetaFacetsFunctionTypeValue(type, meta, facets);
				}
				return type;
			}

			// Token: 0x170022FB RID: 8955
			// (get) Token: 0x06008167 RID: 33127 RVA: 0x001B8504 File Offset: 0x001B6704
			public override RecordValue MetaValue
			{
				get
				{
					return this.meta;
				}
			}

			// Token: 0x170022FC RID: 8956
			// (get) Token: 0x06008168 RID: 33128 RVA: 0x001B850C File Offset: 0x001B670C
			public override TypeFacets Facets
			{
				get
				{
					return this.facets;
				}
			}

			// Token: 0x170022FD RID: 8957
			// (get) Token: 0x06008169 RID: 33129 RVA: 0x001B8514 File Offset: 0x001B6714
			public override ValueKind TypeKind
			{
				get
				{
					return this.type.TypeKind;
				}
			}

			// Token: 0x170022FE RID: 8958
			// (get) Token: 0x0600816A RID: 33130 RVA: 0x001B8521 File Offset: 0x001B6721
			public override bool IsNullable
			{
				get
				{
					return this.type.IsNullable;
				}
			}

			// Token: 0x170022FF RID: 8959
			// (get) Token: 0x0600816B RID: 33131 RVA: 0x001B852E File Offset: 0x001B672E
			public override TypeValue NonNullable
			{
				get
				{
					return this.type.NonNullable;
				}
			}

			// Token: 0x17002300 RID: 8960
			// (get) Token: 0x0600816C RID: 33132 RVA: 0x001B853B File Offset: 0x001B673B
			public override TypeValue Nullable
			{
				get
				{
					return this.type.Nullable;
				}
			}

			// Token: 0x17002301 RID: 8961
			// (get) Token: 0x0600816D RID: 33133 RVA: 0x001B8548 File Offset: 0x001B6748
			public override bool Abstract
			{
				get
				{
					return this.type.Abstract;
				}
			}

			// Token: 0x17002302 RID: 8962
			// (get) Token: 0x0600816E RID: 33134 RVA: 0x001B8555 File Offset: 0x001B6755
			public override object TypeIdentity
			{
				get
				{
					return this.type.TypeIdentity;
				}
			}

			// Token: 0x17002303 RID: 8963
			// (get) Token: 0x0600816F RID: 33135 RVA: 0x001B8562 File Offset: 0x001B6762
			public override TypeValue ReturnType
			{
				get
				{
					return this.type.ReturnType;
				}
			}

			// Token: 0x17002304 RID: 8964
			// (get) Token: 0x06008170 RID: 33136 RVA: 0x001B856F File Offset: 0x001B676F
			public override RecordValue Parameters
			{
				get
				{
					return this.type.Parameters;
				}
			}

			// Token: 0x17002305 RID: 8965
			// (get) Token: 0x06008171 RID: 33137 RVA: 0x001B857C File Offset: 0x001B677C
			public override int Min
			{
				get
				{
					return this.type.Min;
				}
			}

			// Token: 0x06008172 RID: 33138 RVA: 0x001B8589 File Offset: 0x001B6789
			public override Value NewMeta(RecordValue metaValue)
			{
				return FunctionTypeValue.MetaFacetsFunctionTypeValue.New(this.type, metaValue, this.facets);
			}

			// Token: 0x06008173 RID: 33139 RVA: 0x001B859D File Offset: 0x001B679D
			public override TypeValue NewFacets(TypeFacets facets)
			{
				return FunctionTypeValue.MetaFacetsFunctionTypeValue.New(this.type, this.meta, facets);
			}

			// Token: 0x06008174 RID: 33140 RVA: 0x001B85B1 File Offset: 0x001B67B1
			public override bool IsCompatibleWith(TypeValue other)
			{
				return this.type.IsCompatibleWith(other);
			}

			// Token: 0x0400468B RID: 18059
			private readonly FunctionTypeValue type;

			// Token: 0x0400468C RID: 18060
			private readonly RecordValue meta;

			// Token: 0x0400468D RID: 18061
			private readonly TypeFacets facets;
		}
	}
}
