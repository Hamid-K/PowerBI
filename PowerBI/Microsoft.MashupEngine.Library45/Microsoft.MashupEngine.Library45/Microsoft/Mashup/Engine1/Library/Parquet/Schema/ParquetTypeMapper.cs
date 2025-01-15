using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using ParquetSharp;
using ParquetSharp.Schema;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Schema
{
	// Token: 0x02001F89 RID: 8073
	internal abstract class ParquetTypeMapper
	{
		// Token: 0x06010EE7 RID: 69351
		public abstract bool TryMap(Node node, TypeValue typeValue, SchemaConfig config, out ParquetTypeMap typeMap);

		// Token: 0x06010EE8 RID: 69352 RVA: 0x003A5982 File Offset: 0x003A3B82
		public static implicit operator ParquetTypeMapper(ParquetTypeMap typeMap)
		{
			return ParquetTypeMapper.MapTo((Node node, TypeValue typeValue) => typeMap);
		}

		// Token: 0x06010EE9 RID: 69353 RVA: 0x003A59A0 File Offset: 0x003A3BA0
		public static ParquetTypeMapper MapTo(Func<Node, TypeValue, ParquetTypeMap> map)
		{
			return ParquetTypeMapper.MapTo((Node node, TypeValue typeValue, SchemaConfig config) => map(node, typeValue));
		}

		// Token: 0x06010EEA RID: 69354 RVA: 0x003A59BE File Offset: 0x003A3BBE
		public static ParquetTypeMapper MapTo(Func<Node, TypeValue, SchemaConfig, ParquetTypeMap> map)
		{
			return new ParquetTypeMapper.SimpleParquetTypeMapper(map);
		}

		// Token: 0x06010EEB RID: 69355 RVA: 0x003A59C6 File Offset: 0x003A3BC6
		public static ParquetTypeMapper MapTo<TNode>(Func<TNode, TypeValue, SchemaConfig, ParquetTypeMap> map) where TNode : Node
		{
			return ParquetTypeMapper.MapIf<TNode>(map, null, null, null, null);
		}

		// Token: 0x06010EEC RID: 69356 RVA: 0x003A59D2 File Offset: 0x003A3BD2
		public static ParquetTypeMapper MapTo<TNode>(Func<TNode, TypeValue, ParquetTypeMap> map) where TNode : Node
		{
			return ParquetTypeMapper.MapIf<TNode>(map, null, null, null, null);
		}

		// Token: 0x06010EED RID: 69357 RVA: 0x003A59E0 File Offset: 0x003A3BE0
		public static SingleTypeParquetTypeMapper MapIf<TNode>(ParquetTypeMap typeMap, Func<TNode, bool> matchesNode = null, Func<TypeValue, bool> matchesType = null) where TNode : Node
		{
			return new SingleTypeParquetTypeMapper(typeMap.TypeKinds, typeMap.PhysicalType, typeMap.LogicalTypeType, ParquetTypeMapper.MapIf<TNode>((TNode node, TypeValue typeValue) => typeMap, matchesNode, matchesType, null, null));
		}

		// Token: 0x06010EEE RID: 69358 RVA: 0x003A5A38 File Offset: 0x003A3C38
		public static ParquetTypeMapper MapIf<TNode>(Func<TNode, TypeValue, SchemaConfig, ParquetTypeMap> map, Func<TNode, bool> matchesNode = null, Func<TypeValue, bool> matchesType = null, Func<TNode, SchemaConfig, bool> matchesNodeWithConfig = null, Func<TypeValue, SchemaConfig, bool> matchesTypeWithConfig = null) where TNode : Node
		{
			return ParquetTypeMapper.If<TNode>(ParquetTypeMapper.MapTo((Node node, TypeValue typeValue, SchemaConfig config) => map(node as TNode, typeValue, config)), matchesNode, matchesType, null, null, matchesNodeWithConfig, matchesTypeWithConfig);
		}

		// Token: 0x06010EEF RID: 69359 RVA: 0x003A5A84 File Offset: 0x003A3C84
		public static ParquetTypeMapper MapIf<TNode>(Func<TNode, TypeValue, ParquetTypeMap> map, Func<TNode, bool> matchesNode = null, Func<TypeValue, bool> matchesType = null, Func<TNode, SchemaConfig, bool> matchesNodeWithConfig = null, Func<TypeValue, SchemaConfig, bool> matchesTypeWithConfig = null) where TNode : Node
		{
			return ParquetTypeMapper.If<TNode>(ParquetTypeMapper.MapTo((Node node, TypeValue typeValue) => map(node as TNode, typeValue)), matchesNode, matchesType, null, null, matchesNodeWithConfig, matchesTypeWithConfig);
		}

		// Token: 0x06010EF0 RID: 69360 RVA: 0x003A5AD0 File Offset: 0x003A3CD0
		public static ParquetTypeMapper If<TNode>(ParquetTypeMapper inner, Func<TNode, bool> matchesNode = null, Func<TypeValue, bool> matchesType = null, bool? nodePresence = null, bool? typePresence = null, Func<TNode, SchemaConfig, bool> matchesNodeWithConfig = null, Func<TypeValue, SchemaConfig, bool> matchesTypeWithConfig = null) where TNode : Node
		{
			if (matchesNode != null)
			{
				if (matchesNodeWithConfig == null)
				{
					matchesNodeWithConfig = (TNode node, SchemaConfig config) => matchesNode(node);
				}
				else
				{
					matchesNodeWithConfig = (TNode node, SchemaConfig config) => matchesNode(node) && matchesNodeWithConfig(node, config);
				}
			}
			if (matchesType != null)
			{
				if (matchesTypeWithConfig == null)
				{
					matchesTypeWithConfig = (TypeValue typeValue, SchemaConfig config) => matchesType(typeValue);
				}
				else
				{
					matchesTypeWithConfig = (TypeValue typeValue, SchemaConfig config) => matchesType(typeValue) && matchesTypeWithConfig(typeValue, config);
				}
			}
			return ParquetTypeMapper.If<TNode>(inner, delegate(TNode node, TypeValue typeValue, SchemaConfig config)
			{
				bool flag = true;
				bool flag2 = flag;
				bool flag5;
				if (nodePresence != null)
				{
					bool flag3 = node != null;
					bool? flag4 = nodePresence;
					flag5 = (flag3 == flag4.GetValueOrDefault()) & (flag4 != null);
				}
				else
				{
					flag5 = true;
				}
				flag = flag2 && flag5;
				if (node != null && matchesNode != null)
				{
					flag &= matchesNodeWithConfig(node, config);
				}
				bool flag6 = flag;
				bool flag8;
				if (typePresence != null)
				{
					bool flag7 = typeValue != null;
					bool? flag4 = typePresence;
					flag8 = (flag7 == flag4.GetValueOrDefault()) & (flag4 != null);
				}
				else
				{
					flag8 = true;
				}
				flag = flag6 && flag8;
				if (typeValue != null && matchesType != null)
				{
					flag &= matchesTypeWithConfig(typeValue, config);
				}
				return flag;
			});
		}

		// Token: 0x06010EF1 RID: 69361 RVA: 0x003A5B8E File Offset: 0x003A3D8E
		public static ParquetTypeMapper If(ParquetTypeMapper inner, Func<Node, bool> matchesNode = null, Func<TypeValue, bool> matchesType = null, bool? nodePresence = null, bool? typePresence = null)
		{
			return ParquetTypeMapper.If<Node>(inner, matchesNode, matchesType, nodePresence, typePresence, null, null);
		}

		// Token: 0x06010EF2 RID: 69362 RVA: 0x003A5BA0 File Offset: 0x003A3DA0
		public static ParquetTypeMapper If<TNode>(ParquetTypeMapper inner, Func<TNode, TypeValue, SchemaConfig, bool> matches) where TNode : Node
		{
			return ParquetTypeMapper.If(inner, delegate(Node node, TypeValue typeValue, SchemaConfig config)
			{
				TNode tnode = node as TNode;
				return (node == null || tnode != null) && matches(tnode, typeValue, config);
			});
		}

		// Token: 0x06010EF3 RID: 69363 RVA: 0x003A5BCC File Offset: 0x003A3DCC
		public static ParquetTypeMapper If<TMap>(ParquetTypeMapper inner, Func<TMap, bool> validate) where TMap : ParquetTypeMap
		{
			return ParquetTypeMapper.If(inner, delegate(ParquetTypeMap typeMap)
			{
				TMap tmap = typeMap as TMap;
				return tmap != null && validate(tmap);
			});
		}

		// Token: 0x06010EF4 RID: 69364 RVA: 0x003A5BF8 File Offset: 0x003A3DF8
		public static ParquetTypeMapper If(ParquetTypeMapper inner, Func<Node, TypeValue, bool> matches)
		{
			return ParquetTypeMapper.If(inner, (Node node, TypeValue typeValue, SchemaConfig config) => matches(node, typeValue));
		}

		// Token: 0x06010EF5 RID: 69365 RVA: 0x003A5C24 File Offset: 0x003A3E24
		public static ParquetTypeMapper If(ParquetTypeMapper inner, Func<Node, TypeValue, SchemaConfig, bool> matches)
		{
			return new ParquetTypeMapper.CheckedParquetTypeMapper(inner, matches);
		}

		// Token: 0x06010EF6 RID: 69366 RVA: 0x003A5C2D File Offset: 0x003A3E2D
		public static ParquetTypeMapper If(ParquetTypeMapper inner, Func<ParquetTypeMap, bool> validate)
		{
			return new ParquetTypeMapper.ValidatingParquetTypeMapper(inner, validate);
		}

		// Token: 0x06010EF7 RID: 69367 RVA: 0x003A5C36 File Offset: 0x003A3E36
		public static ParquetTypeMapper Cased<TKey>(Func<Node, TypeValue, SchemaConfig, TKey> getKey, IReadOnlyDictionary<TKey, ParquetTypeMapper> cases)
		{
			return new ParquetTypeMapper.CasedParquetTypeMapper<TKey>(getKey, cases);
		}

		// Token: 0x06010EF8 RID: 69368 RVA: 0x003A5C3F File Offset: 0x003A3E3F
		public static ParquetTypeMapper Matching(params ParquetTypeMapper[] typeMappers)
		{
			return ParquetTypeMapper.Matching(typeMappers);
		}

		// Token: 0x06010EF9 RID: 69369 RVA: 0x003A5C47 File Offset: 0x003A3E47
		public static ParquetTypeMapper Matching(IEnumerable<ParquetTypeMapper> typeMappers)
		{
			return new ParquetTypeMapper.MatchingParquetTypeMapper(typeMappers);
		}

		// Token: 0x06010EFA RID: 69370 RVA: 0x003A5C4F File Offset: 0x003A3E4F
		public static ParquetTypeMapper Try(ParquetTypeMapper typeMapper)
		{
			return new ParquetTypeMapper.TryParquetTypeMapper(typeMapper);
		}

		// Token: 0x06010EFB RID: 69371 RVA: 0x003A5C57 File Offset: 0x003A3E57
		public static ParquetTypeMapper Fail(Func<Node, TypeValue, ValueException> error)
		{
			return ParquetTypeMapper.MapTo(delegate(Node node, TypeValue typeValue)
			{
				throw error(node, typeValue);
			});
		}

		// Token: 0x06010EFC RID: 69372 RVA: 0x003A5C78 File Offset: 0x003A3E78
		public static ParquetTypeMapper CheckType(ParquetTypeMapper mapper, params ValueKind[] actualKinds)
		{
			HashSet<ValueKind> actualKindsSet = new HashSet<ValueKind>(actualKinds);
			return ParquetTypeMapper.If(mapper, delegate(Node node, TypeValue typeValue)
			{
				ParquetTypeMapper.CheckType(node, typeValue, actualKindsSet);
				return true;
			});
		}

		// Token: 0x06010EFD RID: 69373 RVA: 0x003A5CAC File Offset: 0x003A3EAC
		protected static void CheckType(Node node, TypeValue typeValue, ICollection<ValueKind> actualKinds)
		{
			if (typeValue == null || typeValue.TypeKind == ValueKind.Any || actualKinds.Contains(typeValue.TypeKind))
			{
				return;
			}
			Value value;
			if (actualKinds.Count != 1)
			{
				value = ListValue.New(actualKinds.Select((ValueKind kind) => TextValue.New(TypeValue.GetTypeKind(TypeServices.GetTypeValueForKind(kind)))));
			}
			else
			{
				value = TextValue.New(TypeValue.GetTypeKind(TypeServices.GetTypeValueForKind(actualKinds.First<ValueKind>())));
			}
			Value value2 = value;
			if (node != null)
			{
				throw ParquetTypeErrors.IncompatibleTypeError(typeValue, "Kind", TextValue.New(TypeValue.GetTypeKind(typeValue)), value2);
			}
			throw ParquetTypeErrors.UnmappedTypeError(typeValue, new NamedValue[]
			{
				new NamedValue("NativeTypeName", TextValue.NewOrNull(typeValue.Facets.NativeTypeName)),
				new NamedValue("ExpectedKind", TextValue.New(TypeValue.GetTypeKind(typeValue))),
				new NamedValue("ActualKind", value2)
			});
		}

		// Token: 0x02001F8A RID: 8074
		private sealed class SimpleParquetTypeMapper : ParquetTypeMapper
		{
			// Token: 0x06010EFF RID: 69375 RVA: 0x003A5D9E File Offset: 0x003A3F9E
			public SimpleParquetTypeMapper(Func<Node, TypeValue, SchemaConfig, ParquetTypeMap> map)
			{
				this.map = map;
			}

			// Token: 0x06010F00 RID: 69376 RVA: 0x003A5DB0 File Offset: 0x003A3FB0
			public override bool TryMap(Node node, TypeValue typeValue, SchemaConfig config, out ParquetTypeMap typeMap)
			{
				typeMap = this.map(node, typeValue, config);
				ParquetTypeMapper.CheckType(node, typeValue, typeMap.TypeKinds);
				if (node != null)
				{
					PhysicalType physicalTypeOrSentinel = ParquetTypeMap.GetPhysicalTypeOrSentinel(node);
					if (physicalTypeOrSentinel != typeMap.PhysicalType)
					{
						throw ParquetTypeErrors.IncompatibleTypeError(typeValue, "PhysicalType", TextValue.New(ParquetTypeMap.GetPhysicalTypeName(typeMap.PhysicalType)), TextValue.New(ParquetTypeMap.GetPhysicalTypeName(physicalTypeOrSentinel)));
					}
				}
				ParquetPrimitiveTypeMap parquetPrimitiveTypeMap = typeMap as ParquetPrimitiveTypeMap;
				if (typeValue != null && parquetPrimitiveTypeMap != null && parquetPrimitiveTypeMap.TypeLength != null && node != null)
				{
					PrimitiveNode primitiveNode = (PrimitiveNode)node;
					if (primitiveNode.TypeLength != parquetPrimitiveTypeMap.TypeLength.Value)
					{
						throw ParquetTypeErrors.IncompatibleTypeError(typeValue, "MaxLength", NumberValue.New(parquetPrimitiveTypeMap.TypeLength.Value), NumberValue.New(primitiveNode.TypeLength));
					}
				}
				return true;
			}

			// Token: 0x04006600 RID: 26112
			private readonly Func<Node, TypeValue, SchemaConfig, ParquetTypeMap> map;
		}

		// Token: 0x02001F8B RID: 8075
		private sealed class CheckedParquetTypeMapper : ParquetTypeMapper
		{
			// Token: 0x06010F01 RID: 69377 RVA: 0x003A5E85 File Offset: 0x003A4085
			public CheckedParquetTypeMapper(ParquetTypeMapper inner, Func<Node, TypeValue, SchemaConfig, bool> matches)
			{
				this.inner = inner;
				this.matches = matches;
			}

			// Token: 0x06010F02 RID: 69378 RVA: 0x003A5E9B File Offset: 0x003A409B
			public override bool TryMap(Node node, TypeValue typeValue, SchemaConfig config, out ParquetTypeMap typeMap)
			{
				if (!this.matches(node, typeValue, config))
				{
					typeMap = null;
					return false;
				}
				return this.inner.TryMap(node, typeValue, config, out typeMap);
			}

			// Token: 0x04006601 RID: 26113
			private readonly ParquetTypeMapper inner;

			// Token: 0x04006602 RID: 26114
			private readonly Func<Node, TypeValue, SchemaConfig, bool> matches;
		}

		// Token: 0x02001F8C RID: 8076
		private sealed class ValidatingParquetTypeMapper : ParquetTypeMapper
		{
			// Token: 0x06010F03 RID: 69379 RVA: 0x003A5EC3 File Offset: 0x003A40C3
			public ValidatingParquetTypeMapper(ParquetTypeMapper inner, Func<ParquetTypeMap, bool> validate)
			{
				this.inner = inner;
				this.validate = validate;
			}

			// Token: 0x06010F04 RID: 69380 RVA: 0x003A5ED9 File Offset: 0x003A40D9
			public override bool TryMap(Node node, TypeValue typeValue, SchemaConfig config, out ParquetTypeMap typeMap)
			{
				if (this.inner.TryMap(node, typeValue, config, out typeMap) && this.validate(typeMap))
				{
					return true;
				}
				typeMap = null;
				return false;
			}

			// Token: 0x04006603 RID: 26115
			private readonly ParquetTypeMapper inner;

			// Token: 0x04006604 RID: 26116
			private readonly Func<ParquetTypeMap, bool> validate;
		}

		// Token: 0x02001F8D RID: 8077
		private sealed class CasedParquetTypeMapper<TKey> : ParquetTypeMapper
		{
			// Token: 0x06010F05 RID: 69381 RVA: 0x003A5F04 File Offset: 0x003A4104
			public CasedParquetTypeMapper(Func<Node, TypeValue, SchemaConfig, TKey> getKey, IReadOnlyDictionary<TKey, ParquetTypeMapper> cases)
			{
				this.getKey = getKey;
				this.cases = cases;
			}

			// Token: 0x06010F06 RID: 69382 RVA: 0x003A5F1C File Offset: 0x003A411C
			public override bool TryMap(Node node, TypeValue typeValue, SchemaConfig config, out ParquetTypeMap typeMap)
			{
				TKey tkey = this.getKey(node, typeValue, config);
				ParquetTypeMapper parquetTypeMapper;
				if (!this.cases.TryGetValue(tkey, out parquetTypeMapper))
				{
					typeMap = null;
					return false;
				}
				return parquetTypeMapper.TryMap(node, typeValue, config, out typeMap);
			}

			// Token: 0x04006605 RID: 26117
			private readonly Func<Node, TypeValue, SchemaConfig, TKey> getKey;

			// Token: 0x04006606 RID: 26118
			private readonly IReadOnlyDictionary<TKey, ParquetTypeMapper> cases;
		}

		// Token: 0x02001F8E RID: 8078
		private sealed class MatchingParquetTypeMapper : ParquetTypeMapper
		{
			// Token: 0x06010F07 RID: 69383 RVA: 0x003A5F59 File Offset: 0x003A4159
			public MatchingParquetTypeMapper(IEnumerable<ParquetTypeMapper> typeMappers)
			{
				this.typeMappers = typeMappers;
			}

			// Token: 0x06010F08 RID: 69384 RVA: 0x003A5F68 File Offset: 0x003A4168
			public MatchingParquetTypeMapper(params ParquetTypeMapper[] typeMappers)
				: this(typeMappers)
			{
			}

			// Token: 0x06010F09 RID: 69385 RVA: 0x003A5F74 File Offset: 0x003A4174
			public override bool TryMap(Node node, TypeValue typeValue, SchemaConfig config, out ParquetTypeMap typeMap)
			{
				using (IEnumerator<ParquetTypeMapper> enumerator = this.typeMappers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.TryMap(node, typeValue, config, out typeMap))
						{
							return true;
						}
					}
				}
				typeMap = null;
				return false;
			}

			// Token: 0x04006607 RID: 26119
			private readonly IEnumerable<ParquetTypeMapper> typeMappers;
		}

		// Token: 0x02001F8F RID: 8079
		private sealed class TryParquetTypeMapper : ParquetTypeMapper
		{
			// Token: 0x06010F0A RID: 69386 RVA: 0x003A5FD0 File Offset: 0x003A41D0
			public TryParquetTypeMapper(ParquetTypeMapper inner)
			{
				this.inner = inner;
			}

			// Token: 0x06010F0B RID: 69387 RVA: 0x003A5FE0 File Offset: 0x003A41E0
			public override bool TryMap(Node node, TypeValue typeValue, SchemaConfig config, out ParquetTypeMap typeMap)
			{
				bool flag;
				try
				{
					flag = this.inner.TryMap(node, typeValue, config, out typeMap);
				}
				catch (ValueException)
				{
					typeMap = null;
					flag = false;
				}
				return flag;
			}

			// Token: 0x04006608 RID: 26120
			private readonly ParquetTypeMapper inner;
		}
	}
}
