using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000351 RID: 849
	internal class NominalTypeEliminator : BasicOpVisitorOfNode
	{
		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x060028E3 RID: 10467 RVA: 0x000806E1 File Offset: 0x0007E8E1
		private Command m_command
		{
			get
			{
				return this.m_compilerState.Command;
			}
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000806F0 File Offset: 0x0007E8F0
		private NominalTypeEliminator(PlanCompiler compilerState, StructuredTypeInfo typeInfo, Dictionary<Var, PropertyRefList> varPropertyMap, Dictionary<Node, PropertyRefList> nodePropertyMap, Dictionary<EdmFunction, EdmProperty[]> tvfResultKeys)
		{
			this.m_compilerState = compilerState;
			this.m_typeInfo = typeInfo;
			this.m_varPropertyMap = varPropertyMap;
			this.m_nodePropertyMap = nodePropertyMap;
			this.m_varInfoMap = new VarInfoMap();
			this.m_tvfResultKeys = tvfResultKeys;
			this.m_typeToNewTypeMap = new Dictionary<TypeUsage, TypeUsage>(TypeUsageEqualityComparer.Instance);
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x00080744 File Offset: 0x0007E944
		internal static void Process(PlanCompiler compilerState, StructuredTypeInfo structuredTypeInfo, Dictionary<EdmFunction, EdmProperty[]> tvfResultKeys)
		{
			Dictionary<Var, PropertyRefList> dictionary;
			Dictionary<Node, PropertyRefList> dictionary2;
			PropertyPushdownHelper.Process(compilerState.Command, out dictionary, out dictionary2);
			new NominalTypeEliminator(compilerState, structuredTypeInfo, dictionary, dictionary2, tvfResultKeys).Process();
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x00080770 File Offset: 0x0007E970
		private void Process()
		{
			foreach (ParameterVar parameterVar in (from v in this.m_command.Vars.OfType<ParameterVar>()
				where TypeSemantics.IsEnumerationType(v.Type) || TypeSemantics.IsStrongSpatialType(v.Type)
				select v).ToArray<ParameterVar>())
			{
				ParameterVar parameterVar2 = (TypeSemantics.IsEnumerationType(parameterVar.Type) ? this.m_command.ReplaceEnumParameterVar(parameterVar) : this.m_command.ReplaceStrongSpatialParameterVar(parameterVar));
				this.m_varInfoMap.CreatePrimitiveTypeVarInfo(parameterVar, parameterVar2);
			}
			Node root = this.m_command.Root;
			PlanCompiler.Assert(root.Op.OpType == OpType.PhysicalProject, "root node is not PhysicalProjectOp?");
			root.Op.Accept<Node>(this, root);
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x060028E7 RID: 10471 RVA: 0x00080834 File Offset: 0x0007EA34
		private TypeUsage DefaultTypeIdType
		{
			get
			{
				return this.m_command.StringType;
			}
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x00080844 File Offset: 0x0007EA44
		private TypeUsage GetNewType(TypeUsage type)
		{
			TypeUsage typeUsage;
			if (this.m_typeToNewTypeMap.TryGetValue(type, out typeUsage))
			{
				return typeUsage;
			}
			CollectionType collectionType;
			if (TypeHelpers.TryGetEdmType<CollectionType>(type, out collectionType))
			{
				typeUsage = TypeUtils.CreateCollectionType(this.GetNewType(collectionType.TypeUsage));
			}
			else if (TypeUtils.IsStructuredType(type))
			{
				typeUsage = this.m_typeInfo.GetTypeInfo(type).FlattenedTypeUsage;
			}
			else if (TypeSemantics.IsEnumerationType(type))
			{
				typeUsage = TypeHelpers.CreateEnumUnderlyingTypeUsage(type);
			}
			else if (TypeSemantics.IsStrongSpatialType(type))
			{
				typeUsage = TypeHelpers.CreateSpatialUnionTypeUsage(type);
			}
			else
			{
				typeUsage = type;
			}
			this.m_typeToNewTypeMap[type] = typeUsage;
			return typeUsage;
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000808D0 File Offset: 0x0007EAD0
		private Node BuildAccessor(Node input, EdmProperty property)
		{
			Op op = input.Op;
			NewRecordOp newRecordOp = op as NewRecordOp;
			if (newRecordOp != null)
			{
				int num;
				if (newRecordOp.GetFieldPosition(property, out num))
				{
					return this.Copy(input.Children[num]);
				}
				return null;
			}
			else
			{
				if (op.OpType == OpType.Null)
				{
					return null;
				}
				PropertyOp propertyOp = this.m_command.CreatePropertyOp(property);
				return this.m_command.CreateNode(propertyOp, this.Copy(input));
			}
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x0008093C File Offset: 0x0007EB3C
		private Node BuildAccessorWithNulls(Node input, EdmProperty property)
		{
			Node node = this.BuildAccessor(input, property);
			if (node == null)
			{
				node = this.CreateNullConstantNode(Helper.GetModelTypeUsage(property));
			}
			return node;
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x00080964 File Offset: 0x0007EB64
		private Node BuildTypeIdAccessor(Node input, TypeInfo typeInfo)
		{
			Node node;
			if (typeInfo.HasTypeIdProperty)
			{
				node = this.BuildAccessorWithNulls(input, typeInfo.TypeIdProperty);
			}
			else
			{
				node = this.CreateTypeIdConstant(typeInfo);
			}
			return node;
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x00080994 File Offset: 0x0007EB94
		private Node BuildSoftCast(Node node, TypeUsage targetType)
		{
			PlanCompiler.Assert(node.Op.IsScalarOp, "Attempting SoftCast around non-ScalarOp?");
			if (Command.EqualTypes(node.Op.Type, targetType))
			{
				return node;
			}
			while (node.Op.OpType == OpType.SoftCast)
			{
				node = node.Child0;
			}
			return this.m_command.CreateNode(this.m_command.CreateSoftCastOp(targetType), node);
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x000809FA File Offset: 0x0007EBFA
		private Node Copy(Node n)
		{
			return OpCopier.Copy(this.m_command, n);
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x00080A08 File Offset: 0x0007EC08
		private Node CreateNullConstantNode(TypeUsage type)
		{
			return this.m_command.CreateNode(this.m_command.CreateNullOp(type));
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x00080A24 File Offset: 0x0007EC24
		private Node CreateNullSentinelConstant()
		{
			NullSentinelOp nullSentinelOp = this.m_command.CreateNullSentinelOp();
			return this.m_command.CreateNode(nullSentinelOp);
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x00080A4C File Offset: 0x0007EC4C
		private Node CreateTypeIdConstant(TypeInfo typeInfo)
		{
			object typeId = typeInfo.TypeId;
			TypeUsage typeUsage;
			if (typeInfo.RootType.DiscriminatorMap != null)
			{
				typeUsage = Helper.GetModelTypeUsage(typeInfo.RootType.DiscriminatorMap.DiscriminatorProperty);
			}
			else
			{
				typeUsage = this.DefaultTypeIdType;
			}
			InternalConstantOp internalConstantOp = this.m_command.CreateInternalConstantOp(typeUsage, typeId);
			return this.m_command.CreateNode(internalConstantOp);
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x00080AA8 File Offset: 0x0007ECA8
		private Node CreateTypeIdConstantForPrefixMatch(TypeInfo typeInfo)
		{
			object typeId = typeInfo.TypeId;
			string text = ((typeId != null) ? typeId.ToString() : null) + "%";
			InternalConstantOp internalConstantOp = this.m_command.CreateInternalConstantOp(this.DefaultTypeIdType, text);
			return this.m_command.CreateNode(internalConstantOp);
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x00080AF1 File Offset: 0x0007ECF1
		private IEnumerable<PropertyRef> GetPropertyRefsForComparisonAndIsNull(TypeInfo typeInfo, NominalTypeEliminator.OperationKind opKind)
		{
			PlanCompiler.Assert(opKind == NominalTypeEliminator.OperationKind.IsNull || opKind == NominalTypeEliminator.OperationKind.Equality, "Unexpected opKind: " + opKind.ToString() + "; Can only handle IsNull and Equality");
			TypeUsage type = typeInfo.Type;
			RowType rowType = null;
			if (TypeHelpers.TryGetEdmType<RowType>(type, out rowType))
			{
				if (opKind == NominalTypeEliminator.OperationKind.IsNull && typeInfo.HasNullSentinelProperty)
				{
					yield return NullSentinelPropertyRef.Instance;
				}
				else
				{
					foreach (EdmProperty i in rowType.Properties)
					{
						if (!TypeUtils.IsStructuredType(Helper.GetModelTypeUsage(i)))
						{
							yield return new SimplePropertyRef(i);
						}
						else
						{
							TypeInfo typeInfo2 = this.m_typeInfo.GetTypeInfo(Helper.GetModelTypeUsage(i));
							foreach (PropertyRef propertyRef in this.GetPropertyRefs(typeInfo2, opKind))
							{
								PropertyRef propertyRef2 = propertyRef.CreateNestedPropertyRef(i);
								yield return propertyRef2;
							}
							IEnumerator<PropertyRef> enumerator2 = null;
						}
						i = null;
					}
					ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator = default(ReadOnlyMetadataCollection<EdmProperty>.Enumerator);
				}
				yield break;
			}
			EntityType entityType = null;
			if (TypeHelpers.TryGetEdmType<EntityType>(type, out entityType))
			{
				if (opKind == NominalTypeEliminator.OperationKind.Equality || (opKind == NominalTypeEliminator.OperationKind.IsNull && !typeInfo.HasTypeIdProperty))
				{
					foreach (PropertyRef propertyRef3 in typeInfo.GetIdentityPropertyRefs())
					{
						yield return propertyRef3;
					}
					IEnumerator<PropertyRef> enumerator2 = null;
				}
				else
				{
					yield return TypeIdPropertyRef.Instance;
				}
				yield break;
			}
			ComplexType complexType = null;
			if (TypeHelpers.TryGetEdmType<ComplexType>(type, out complexType))
			{
				PlanCompiler.Assert(opKind == NominalTypeEliminator.OperationKind.IsNull, "complex types not equality-comparable");
				PlanCompiler.Assert(typeInfo.HasNullSentinelProperty, "complex type with no null sentinel property: can't handle isNull");
				yield return NullSentinelPropertyRef.Instance;
				yield break;
			}
			RefType refType = null;
			if (TypeHelpers.TryGetEdmType<RefType>(type, out refType))
			{
				foreach (PropertyRef propertyRef4 in typeInfo.GetAllPropertyRefs())
				{
					yield return propertyRef4;
				}
				IEnumerator<PropertyRef> enumerator2 = null;
				yield break;
			}
			PlanCompiler.Assert(false, "Unknown type");
			yield break;
			yield break;
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x00080B0F File Offset: 0x0007ED0F
		private IEnumerable<PropertyRef> GetPropertyRefs(TypeInfo typeInfo, NominalTypeEliminator.OperationKind opKind)
		{
			PlanCompiler.Assert(opKind != NominalTypeEliminator.OperationKind.All, "unexpected attempt to GetPropertyRefs(...,OperationKind.All)");
			if (opKind == NominalTypeEliminator.OperationKind.GetKeys)
			{
				return typeInfo.GetKeyPropertyRefs();
			}
			if (opKind == NominalTypeEliminator.OperationKind.GetIdentity)
			{
				return typeInfo.GetIdentityPropertyRefs();
			}
			return this.GetPropertyRefsForComparisonAndIsNull(typeInfo, opKind);
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x00080B40 File Offset: 0x0007ED40
		private IEnumerable<EdmProperty> GetProperties(TypeInfo typeInfo, NominalTypeEliminator.OperationKind opKind)
		{
			if (opKind == NominalTypeEliminator.OperationKind.All)
			{
				foreach (EdmProperty edmProperty in typeInfo.GetAllProperties())
				{
					yield return edmProperty;
				}
				IEnumerator<EdmProperty> enumerator = null;
			}
			else
			{
				foreach (PropertyRef propertyRef in this.GetPropertyRefs(typeInfo, opKind))
				{
					yield return typeInfo.GetNewProperty(propertyRef);
				}
				IEnumerator<PropertyRef> enumerator2 = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x00080B60 File Offset: 0x0007ED60
		private void GetPropertyValues(TypeInfo typeInfo, NominalTypeEliminator.OperationKind opKind, Node input, bool ignoreMissingProperties, out List<EdmProperty> properties, out List<Node> values)
		{
			values = new List<Node>();
			properties = new List<EdmProperty>();
			foreach (EdmProperty edmProperty in this.GetProperties(typeInfo, opKind))
			{
				KeyValuePair<EdmProperty, Node> propertyValue = this.GetPropertyValue(input, edmProperty, ignoreMissingProperties);
				if (propertyValue.Value != null)
				{
					properties.Add(propertyValue.Key);
					values.Add(propertyValue.Value);
				}
			}
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x00080BEC File Offset: 0x0007EDEC
		private KeyValuePair<EdmProperty, Node> GetPropertyValue(Node input, EdmProperty property, bool ignoreMissingProperties)
		{
			Node node;
			if (!ignoreMissingProperties)
			{
				node = this.BuildAccessorWithNulls(input, property);
			}
			else
			{
				node = this.BuildAccessor(input, property);
			}
			return new KeyValuePair<EdmProperty, Node>(property, node);
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x00080C1C File Offset: 0x0007EE1C
		private List<global::System.Data.Entity.Core.Query.InternalTrees.SortKey> HandleSortKeys(List<global::System.Data.Entity.Core.Query.InternalTrees.SortKey> keys)
		{
			List<global::System.Data.Entity.Core.Query.InternalTrees.SortKey> list = new List<global::System.Data.Entity.Core.Query.InternalTrees.SortKey>();
			bool flag = false;
			foreach (global::System.Data.Entity.Core.Query.InternalTrees.SortKey sortKey in keys)
			{
				VarInfo varInfo;
				if (!this.m_varInfoMap.TryGetVarInfo(sortKey.Var, out varInfo))
				{
					list.Add(sortKey);
				}
				else
				{
					StructuredVarInfo structuredVarInfo = varInfo as StructuredVarInfo;
					if (structuredVarInfo != null && structuredVarInfo.NewVarsIncludeNullSentinelVar)
					{
						this.m_compilerState.HasSortingOnNullSentinels = true;
					}
					foreach (Var var in varInfo.NewVars)
					{
						global::System.Data.Entity.Core.Query.InternalTrees.SortKey sortKey2 = Command.CreateSortKey(var, sortKey.AscendingSort, sortKey.Collation);
						list.Add(sortKey2);
					}
					flag = true;
				}
			}
			if (!flag)
			{
				return keys;
			}
			return list;
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x00080D10 File Offset: 0x0007EF10
		private Node CreateTVFProjection(Node unnestNode, List<Var> unnestOpTableColumns, TypeInfo unnestOpTableTypeInfo, out List<Var> newVars)
		{
			RowType rowType = unnestOpTableTypeInfo.Type.EdmType as RowType;
			bool flag = rowType != null;
			string text = "Unexpected TVF return type (must be row): ";
			TypeUsage type = unnestOpTableTypeInfo.Type;
			PlanCompiler.Assert(flag, text + ((type != null) ? type.ToString() : null));
			List<Var> list = new List<Var>();
			List<Node> list2 = new List<Node>();
			PropertyRef[] array = unnestOpTableTypeInfo.PropertyRefList.ToArray<PropertyRef>();
			Dictionary<EdmProperty, PropertyRef> dictionary = new Dictionary<EdmProperty, PropertyRef>();
			foreach (PropertyRef propertyRef in array)
			{
				dictionary.Add(unnestOpTableTypeInfo.GetNewProperty(propertyRef), propertyRef);
			}
			foreach (EdmProperty edmProperty in unnestOpTableTypeInfo.FlattenedType.Properties)
			{
				PropertyRef propertyRef2 = dictionary[edmProperty];
				Var var = null;
				SimplePropertyRef simplePropertyRef = propertyRef2 as SimplePropertyRef;
				if (simplePropertyRef != null)
				{
					int num = rowType.Members.IndexOf(simplePropertyRef.Property);
					PlanCompiler.Assert(num >= 0, "Can't find a column in the TVF result type");
					list2.Add(this.m_command.CreateVarDefNode(this.m_command.CreateNode(this.m_command.CreateVarRefOp(unnestOpTableColumns[num])), out var));
				}
				else if (propertyRef2 is NullSentinelPropertyRef)
				{
					list2.Add(this.m_command.CreateVarDefNode(this.CreateNullSentinelConstant(), out var));
				}
				PlanCompiler.Assert(var != null, "TVFs returning a collection of rows with non-primitive properties are not supported");
				list.Add(var);
			}
			newVars = list;
			return this.m_command.CreateNode(this.m_command.CreateProjectOp(this.m_command.CreateVarVec(list)), unnestNode, this.m_command.CreateNode(this.m_command.CreateVarDefListOp(), list2));
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x00080ED4 File Offset: 0x0007F0D4
		public override Node Visit(VarDefListOp op, Node n)
		{
			this.VisitChildren(n);
			List<Node> list = new List<Node>();
			foreach (Node node in n.Children)
			{
				PlanCompiler.Assert(node.Op is VarDefOp, "VarDefOp expected");
				VarDefOp varDefOp = (VarDefOp)node.Op;
				if (TypeUtils.IsStructuredType(varDefOp.Var.Type) || TypeUtils.IsCollectionType(varDefOp.Var.Type))
				{
					List<Node> list2;
					TypeUsage typeUsage;
					this.FlattenComputedVar((ComputedVar)varDefOp.Var, node, out list2, out typeUsage);
					using (List<Node>.Enumerator enumerator2 = list2.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Node node2 = enumerator2.Current;
							list.Add(node2);
						}
						continue;
					}
				}
				if (TypeSemantics.IsEnumerationType(varDefOp.Var.Type) || TypeSemantics.IsStrongSpatialType(varDefOp.Var.Type))
				{
					list.Add(this.FlattenEnumOrStrongSpatialVar(varDefOp, node.Child0));
				}
				else
				{
					list.Add(node);
				}
			}
			return this.m_command.CreateNode(n.Op, list);
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x00081024 File Offset: 0x0007F224
		private void FlattenComputedVar(ComputedVar v, Node node, out List<Node> newNodes, out TypeUsage newType)
		{
			newNodes = new List<Node>();
			Node child = node.Child0;
			newType = null;
			if (TypeUtils.IsCollectionType(v.Type))
			{
				PlanCompiler.Assert(child.Op.OpType != OpType.Function, "Flattening of TVF output is not allowed.");
				newType = this.GetNewType(v.Type);
				Var var;
				Node node2 = this.m_command.CreateVarDefNode(child, out var);
				newNodes.Add(node2);
				this.m_varInfoMap.CreateCollectionVarInfo(v, var);
				return;
			}
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(v.Type);
			PropertyRefList propertyRefList = this.m_varPropertyMap[v];
			List<Var> list = new List<Var>();
			List<EdmProperty> list2 = new List<EdmProperty>();
			newNodes = new List<Node>();
			bool flag = false;
			foreach (PropertyRef propertyRef in typeInfo.PropertyRefList)
			{
				if (propertyRefList.Contains(propertyRef))
				{
					EdmProperty newProperty = typeInfo.GetNewProperty(propertyRef);
					Node node3;
					if (propertyRefList.AllProperties)
					{
						node3 = this.BuildAccessorWithNulls(child, newProperty);
					}
					else
					{
						node3 = this.BuildAccessor(child, newProperty);
						if (node3 == null)
						{
							continue;
						}
					}
					list2.Add(newProperty);
					Var var2;
					Node node4 = this.m_command.CreateVarDefNode(node3, out var2);
					newNodes.Add(node4);
					list.Add(var2);
					if (!flag && NominalTypeEliminator.IsNullSentinelPropertyRef(propertyRef))
					{
						flag = true;
					}
				}
			}
			this.m_varInfoMap.CreateStructuredVarInfo(v, typeInfo.FlattenedType, list, list2, flag);
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x000811AC File Offset: 0x0007F3AC
		private static bool IsNullSentinelPropertyRef(PropertyRef propertyRef)
		{
			if (propertyRef is NullSentinelPropertyRef)
			{
				return true;
			}
			NestedPropertyRef nestedPropertyRef = propertyRef as NestedPropertyRef;
			return nestedPropertyRef != null && nestedPropertyRef.OuterProperty is NullSentinelPropertyRef;
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x000811E0 File Offset: 0x0007F3E0
		private Node FlattenEnumOrStrongSpatialVar(VarDefOp varDefOp, Node node)
		{
			Var var;
			Node node2 = this.m_command.CreateVarDefNode(node, out var);
			this.m_varInfoMap.CreatePrimitiveTypeVarInfo(varDefOp.Var, var);
			return node2;
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x00081210 File Offset: 0x0007F410
		public override Node Visit(PhysicalProjectOp op, Node n)
		{
			this.VisitChildren(n);
			VarList varList = this.FlattenVarList(op.Outputs);
			SimpleCollectionColumnMap simpleCollectionColumnMap = this.ExpandColumnMap(op.ColumnMap);
			PhysicalProjectOp physicalProjectOp = this.m_command.CreatePhysicalProjectOp(varList, simpleCollectionColumnMap);
			n.Op = physicalProjectOp;
			return n;
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x00081254 File Offset: 0x0007F454
		private SimpleCollectionColumnMap ExpandColumnMap(SimpleCollectionColumnMap columnMap)
		{
			VarRefColumnMap varRefColumnMap = columnMap.Element as VarRefColumnMap;
			PlanCompiler.Assert(varRefColumnMap != null, "Encountered a SimpleCollectionColumnMap element that is not VarRefColumnMap when expanding a column map in NominalTypeEliminator.");
			VarInfo varInfo;
			if (!this.m_varInfoMap.TryGetVarInfo(varRefColumnMap.Var, out varInfo))
			{
				return columnMap;
			}
			if (TypeUtils.IsStructuredType(varRefColumnMap.Var.Type))
			{
				TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(varRefColumnMap.Var.Type);
				PlanCompiler.Assert(typeInfo.RootType.FlattenedType.Properties.Count == varInfo.NewVars.Count, string.Concat(new string[]
				{
					"Var count mismatch; Expected ",
					typeInfo.RootType.FlattenedType.Properties.Count.ToString(),
					"; got ",
					varInfo.NewVars.Count.ToString(),
					" instead."
				}));
			}
			ColumnMap columnMap2 = new ColumnMapProcessor(varRefColumnMap, varInfo, this.m_typeInfo).ExpandColumnMap();
			return new SimpleCollectionColumnMap(TypeUtils.CreateCollectionType(columnMap2.Type), columnMap2.Name, columnMap2, columnMap.Keys, columnMap.ForeignKeys);
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x00081376 File Offset: 0x0007F576
		private IEnumerable<Var> FlattenVars(IEnumerable<Var> vars)
		{
			foreach (Var var in vars)
			{
				VarInfo varInfo;
				if (!this.m_varInfoMap.TryGetVarInfo(var, out varInfo))
				{
					yield return var;
				}
				else
				{
					foreach (Var var2 in varInfo.NewVars)
					{
						yield return var2;
					}
					List<Var>.Enumerator enumerator2 = default(List<Var>.Enumerator);
				}
			}
			IEnumerator<Var> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x0008138D File Offset: 0x0007F58D
		private VarVec FlattenVarSet(VarVec varSet)
		{
			return this.m_command.CreateVarVec(this.FlattenVars(varSet));
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x000813A1 File Offset: 0x0007F5A1
		private VarList FlattenVarList(VarList varList)
		{
			return Command.CreateVarList(this.FlattenVars(varList));
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x000813B0 File Offset: 0x0007F5B0
		public override Node Visit(DistinctOp op, Node n)
		{
			this.VisitChildren(n);
			VarVec varVec = this.FlattenVarSet(op.Keys);
			n.Op = this.m_command.CreateDistinctOp(varVec);
			return n;
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x000813E4 File Offset: 0x0007F5E4
		public override Node Visit(GroupByOp op, Node n)
		{
			this.VisitChildren(n);
			VarVec varVec = this.FlattenVarSet(op.Keys);
			VarVec varVec2 = this.FlattenVarSet(op.Outputs);
			if (varVec != op.Keys || varVec2 != op.Outputs)
			{
				n.Op = this.m_command.CreateGroupByOp(varVec, varVec2);
			}
			return n;
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x00081438 File Offset: 0x0007F638
		public override Node Visit(GroupByIntoOp op, Node n)
		{
			this.VisitChildren(n);
			VarVec varVec = this.FlattenVarSet(op.Keys);
			VarVec varVec2 = this.FlattenVarSet(op.Inputs);
			VarVec varVec3 = this.FlattenVarSet(op.Outputs);
			if (varVec != op.Keys || varVec2 != op.Inputs || varVec3 != op.Outputs)
			{
				n.Op = this.m_command.CreateGroupByIntoOp(varVec, varVec2, varVec3);
			}
			return n;
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x000814A4 File Offset: 0x0007F6A4
		public override Node Visit(ProjectOp op, Node n)
		{
			this.VisitChildren(n);
			VarVec varVec = this.FlattenVarSet(op.Outputs);
			if (op.Outputs != varVec)
			{
				if (varVec.IsEmpty)
				{
					return n.Child0;
				}
				n.Op = this.m_command.CreateProjectOp(varVec);
			}
			return n;
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x000814F0 File Offset: 0x0007F6F0
		public override Node Visit(ScanTableOp op, Node n)
		{
			Var var = op.Table.Columns[0];
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(var.Type);
			RowType flattenedType = typeInfo.FlattenedType;
			List<EdmProperty> list = new List<EdmProperty>();
			List<EdmMember> list2 = new List<EdmMember>();
			HashSet<string> hashSet = new HashSet<string>();
			foreach (object obj in TypeHelpers.GetAllStructuralMembers(var.Type.EdmType))
			{
				EdmProperty edmProperty = (EdmProperty)obj;
				hashSet.Add(edmProperty.Name);
			}
			foreach (EdmProperty edmProperty2 in flattenedType.Properties)
			{
				if (hashSet.Contains(edmProperty2.Name))
				{
					list.Add(edmProperty2);
				}
			}
			foreach (PropertyRef propertyRef in typeInfo.GetKeyPropertyRefs())
			{
				EdmProperty newProperty = typeInfo.GetNewProperty(propertyRef);
				list2.Add(newProperty);
			}
			TableMD tableMD = this.m_command.CreateFlatTableDefinition(list, list2, op.Table.TableMetadata.Extent);
			Table table = this.m_command.CreateTableInstance(tableMD);
			this.m_varInfoMap.CreateStructuredVarInfo(var, flattenedType, table.Columns, list);
			n.Op = this.m_command.CreateScanTableOp(table);
			return n;
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x000816A0 File Offset: 0x0007F8A0
		internal static Var GetSingletonVar(Node n)
		{
			switch (n.Op.OpType)
			{
			case OpType.ScanTable:
			{
				ScanTableOp scanTableOp = (ScanTableOp)n.Op;
				if (scanTableOp.Table.Columns.Count != 1)
				{
					return null;
				}
				return scanTableOp.Table.Columns[0];
			}
			case OpType.Filter:
			case OpType.Sort:
			case OpType.ConstrainedSort:
			case OpType.SingleRow:
				return NominalTypeEliminator.GetSingletonVar(n.Child0);
			case OpType.Project:
			{
				ProjectOp projectOp = (ProjectOp)n.Op;
				if (projectOp.Outputs.Count != 1)
				{
					return null;
				}
				return projectOp.Outputs.First;
			}
			case OpType.Unnest:
			{
				UnnestOp unnestOp = (UnnestOp)n.Op;
				if (unnestOp.Table.Columns.Count != 1)
				{
					return null;
				}
				return unnestOp.Table.Columns[0];
			}
			case OpType.UnionAll:
			case OpType.Intersect:
			case OpType.Except:
			{
				SetOp setOp = (SetOp)n.Op;
				if (setOp.Outputs.Count != 1)
				{
					return null;
				}
				return setOp.Outputs.First;
			}
			case OpType.Distinct:
			{
				DistinctOp distinctOp = (DistinctOp)n.Op;
				if (distinctOp.Keys.Count != 1)
				{
					return null;
				}
				return distinctOp.Keys.First;
			}
			}
			return null;
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x00081808 File Offset: 0x0007FA08
		public override Node Visit(ScanViewOp op, Node n)
		{
			Var singletonVar = NominalTypeEliminator.GetSingletonVar(n.Child0);
			PlanCompiler.Assert(singletonVar != null, "cannot identify Var for the input node to the ScanViewOp");
			PlanCompiler.Assert(op.Table.Columns.Count == 1, "table for scanViewOp has more than on column?");
			Var var = op.Table.Columns[0];
			Node node = base.VisitNode(n.Child0);
			VarInfo varInfo;
			if (!this.m_varInfoMap.TryGetVarInfo(singletonVar, out varInfo))
			{
				PlanCompiler.Assert(false, "didn't find inputVar for scanViewOp?");
			}
			StructuredVarInfo structuredVarInfo = (StructuredVarInfo)varInfo;
			this.m_typeInfo.GetTypeInfo(var.Type);
			this.m_varInfoMap.CreateStructuredVarInfo(var, structuredVarInfo.NewType, structuredVarInfo.NewVars, structuredVarInfo.Fields);
			return node;
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x000818C0 File Offset: 0x0007FAC0
		public override Node Visit(SortOp op, Node n)
		{
			this.VisitChildren(n);
			List<global::System.Data.Entity.Core.Query.InternalTrees.SortKey> list = this.HandleSortKeys(op.Keys);
			if (list != op.Keys)
			{
				n.Op = this.m_command.CreateSortOp(list);
			}
			return n;
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x00081900 File Offset: 0x0007FB00
		public override Node Visit(UnnestOp op, Node n)
		{
			this.VisitChildren(n);
			Var var = null;
			EdmFunction edmFunction = null;
			if (n.HasChild0)
			{
				Node child = n.Child0;
				VarDefOp varDefOp = child.Op as VarDefOp;
				if (varDefOp != null && TypeUtils.IsCollectionType(varDefOp.Var.Type))
				{
					ComputedVar computedVar = (ComputedVar)varDefOp.Var;
					if (child.HasChild0 && child.Child0.Op.OpType == OpType.Function)
					{
						var = computedVar;
						edmFunction = ((FunctionOp)child.Child0.Op).Function;
					}
					else
					{
						List<Node> list = new List<Node>();
						TypeUsage typeUsage;
						this.FlattenComputedVar(computedVar, child, out list, out typeUsage);
						PlanCompiler.Assert(list.Count == 1, "Flattening unnest var produced more than one Var.");
						n.Child0 = list[0];
					}
				}
			}
			if (edmFunction != null)
			{
				PlanCompiler.Assert(var != null, "newUnnestVar must be initialized in the TVF case.");
			}
			else
			{
				VarInfo varInfo;
				if (!this.m_varInfoMap.TryGetVarInfo(op.Var, out varInfo) || varInfo.Kind != VarInfoKind.CollectionVarInfo)
				{
					throw new InvalidOperationException(Strings.ADP_InternalProviderError(1006));
				}
				var = ((CollectionVarInfo)varInfo).NewVar;
			}
			Var var2 = op.Table.Columns[0];
			if (!TypeUtils.IsStructuredType(var2.Type))
			{
				PlanCompiler.Assert(edmFunction == null, "TVFs returning a collection of values of a non-structured type are not supported");
				if (TypeSemantics.IsEnumerationType(var2.Type) || TypeSemantics.IsStrongSpatialType(var2.Type))
				{
					UnnestOp unnestOp = this.m_command.CreateUnnestOp(var);
					this.m_varInfoMap.CreatePrimitiveTypeVarInfo(var2, unnestOp.Table.Columns[0]);
					n.Op = unnestOp;
				}
				else
				{
					n.Op = this.m_command.CreateUnnestOp(var, op.Table);
				}
			}
			else
			{
				TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(var2.Type);
				TableMD tableMD;
				if (edmFunction != null)
				{
					RowType tvfReturnType = TypeHelpers.GetTvfReturnType(edmFunction);
					PlanCompiler.Assert(Command.EqualTypes(tvfReturnType, var2.Type.EdmType), "Unexpected TVF return type (row type is expected).");
					tableMD = this.m_command.CreateFlatTableDefinition(tvfReturnType.Properties, this.GetTvfResultKeys(edmFunction), null);
				}
				else
				{
					tableMD = this.m_command.CreateFlatTableDefinition(typeInfo.FlattenedType);
				}
				Table table = this.m_command.CreateTableInstance(tableMD);
				n.Op = this.m_command.CreateUnnestOp(var, table);
				List<Var> columns;
				if (edmFunction != null)
				{
					n = this.CreateTVFProjection(n, table.Columns, typeInfo, out columns);
				}
				else
				{
					columns = table.Columns;
				}
				this.m_varInfoMap.CreateStructuredVarInfo(var2, typeInfo.FlattenedType, columns, typeInfo.FlattenedType.Properties.ToList<EdmProperty>());
			}
			return n;
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x00081B9C File Offset: 0x0007FD9C
		private IEnumerable<EdmProperty> GetTvfResultKeys(EdmFunction tvf)
		{
			EdmProperty[] array;
			if (this.m_tvfResultKeys.TryGetValue(tvf, out array))
			{
				return array;
			}
			return Enumerable.Empty<EdmProperty>();
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x00081BC0 File Offset: 0x0007FDC0
		protected override Node VisitSetOp(SetOp op, Node n)
		{
			this.VisitChildren(n);
			for (int i = 0; i < op.VarMap.Length; i++)
			{
				List<ComputedVar> list;
				op.VarMap[i] = this.FlattenVarMap(op.VarMap[i], out list);
				if (list != null)
				{
					n.Children[i] = this.FixupSetOpChild(n.Children[i], op.VarMap[i], list);
				}
			}
			op.Outputs.Clear();
			foreach (Var var in op.VarMap[0].Keys)
			{
				op.Outputs.Set(var);
			}
			return n;
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x00081C88 File Offset: 0x0007FE88
		private Node FixupSetOpChild(Node setOpChild, VarMap varMap, List<ComputedVar> newComputedVars)
		{
			PlanCompiler.Assert(setOpChild != null, "null setOpChild?");
			PlanCompiler.Assert(varMap != null, "null varMap?");
			PlanCompiler.Assert(newComputedVars != null, "null newComputedVars?");
			VarVec varVec = this.m_command.CreateVarVec();
			foreach (KeyValuePair<Var, Var> keyValuePair in varMap)
			{
				varVec.Set(keyValuePair.Value);
			}
			List<Node> list = new List<Node>();
			foreach (Var var in newComputedVars)
			{
				VarDefOp varDefOp = this.m_command.CreateVarDefOp(var);
				Node node = this.m_command.CreateNode(varDefOp, this.CreateNullConstantNode(var.Type));
				list.Add(node);
			}
			Node node2 = this.m_command.CreateNode(this.m_command.CreateVarDefListOp(), list);
			ProjectOp projectOp = this.m_command.CreateProjectOp(varVec);
			return this.m_command.CreateNode(projectOp, setOpChild, node2);
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x00081DB8 File Offset: 0x0007FFB8
		private VarMap FlattenVarMap(VarMap varMap, out List<ComputedVar> newComputedVars)
		{
			newComputedVars = null;
			VarMap varMap2 = new VarMap();
			foreach (KeyValuePair<Var, Var> keyValuePair in varMap)
			{
				VarInfo varInfo;
				if (!this.m_varInfoMap.TryGetVarInfo(keyValuePair.Value, out varInfo))
				{
					varMap2.Add(keyValuePair.Key, keyValuePair.Value);
				}
				else
				{
					VarInfo varInfo2;
					if (!this.m_varInfoMap.TryGetVarInfo(keyValuePair.Key, out varInfo2))
					{
						varInfo2 = this.FlattenSetOpVar((SetOpVar)keyValuePair.Key);
					}
					if (varInfo2.Kind == VarInfoKind.CollectionVarInfo)
					{
						varMap2.Add(((CollectionVarInfo)varInfo2).NewVar, ((CollectionVarInfo)varInfo).NewVar);
					}
					else if (varInfo2.Kind == VarInfoKind.PrimitiveTypeVarInfo)
					{
						varMap2.Add(((PrimitiveTypeVarInfo)varInfo2).NewVar, ((PrimitiveTypeVarInfo)varInfo).NewVar);
					}
					else
					{
						StructuredVarInfo structuredVarInfo = (StructuredVarInfo)varInfo2;
						StructuredVarInfo structuredVarInfo2 = (StructuredVarInfo)varInfo;
						foreach (EdmProperty edmProperty in structuredVarInfo.Fields)
						{
							Var var;
							PlanCompiler.Assert(structuredVarInfo.TryGetVar(edmProperty, out var), "Could not find VarInfo for prop " + edmProperty.Name);
							Var var2;
							if (!structuredVarInfo2.TryGetVar(edmProperty, out var2))
							{
								var2 = this.m_command.CreateComputedVar(var.Type);
								if (newComputedVars == null)
								{
									newComputedVars = new List<ComputedVar>();
								}
								newComputedVars.Add((ComputedVar)var2);
							}
							varMap2.Add(var, var2);
						}
					}
				}
			}
			return varMap2;
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x00081F8C File Offset: 0x0008018C
		private VarInfo FlattenSetOpVar(SetOpVar v)
		{
			if (TypeUtils.IsCollectionType(v.Type))
			{
				TypeUsage newType = this.GetNewType(v.Type);
				Var var = this.m_command.CreateSetOpVar(newType);
				return this.m_varInfoMap.CreateCollectionVarInfo(v, var);
			}
			if (TypeSemantics.IsEnumerationType(v.Type) || TypeSemantics.IsStrongSpatialType(v.Type))
			{
				TypeUsage newType2 = this.GetNewType(v.Type);
				Var var2 = this.m_command.CreateSetOpVar(newType2);
				return this.m_varInfoMap.CreatePrimitiveTypeVarInfo(v, var2);
			}
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(v.Type);
			PropertyRefList propertyRefList = this.m_varPropertyMap[v];
			List<Var> list = new List<Var>();
			List<EdmProperty> list2 = new List<EdmProperty>();
			bool flag = false;
			foreach (PropertyRef propertyRef in typeInfo.PropertyRefList)
			{
				if (propertyRefList.Contains(propertyRef))
				{
					EdmProperty newProperty = typeInfo.GetNewProperty(propertyRef);
					list2.Add(newProperty);
					SetOpVar setOpVar = this.m_command.CreateSetOpVar(Helper.GetModelTypeUsage(newProperty));
					list.Add(setOpVar);
					if (!flag && NominalTypeEliminator.IsNullSentinelPropertyRef(propertyRef))
					{
						flag = true;
					}
				}
			}
			return this.m_varInfoMap.CreateStructuredVarInfo(v, typeInfo.FlattenedType, list, list2, flag);
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x000820E4 File Offset: 0x000802E4
		public override Node Visit(SoftCastOp op, Node n)
		{
			TypeUsage type = n.Child0.Op.Type;
			TypeUsage type2 = op.Type;
			this.VisitChildren(n);
			TypeUsage newType = this.GetNewType(type2);
			if (TypeSemantics.IsRowType(type2))
			{
				PlanCompiler.Assert(n.Child0.Op.OpType == OpType.NewRecord, "Expected a record constructor here. Found " + n.Child0.Op.OpType.ToString() + " instead");
				TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(type);
				TypeInfo typeInfo2 = this.m_typeInfo.GetTypeInfo(op.Type);
				NewRecordOp newRecordOp = this.m_command.CreateNewRecordOp(newType);
				List<Node> list = new List<Node>();
				IEnumerator<EdmProperty> enumerator = newRecordOp.Properties.GetEnumerator();
				int num = newRecordOp.Properties.Count;
				enumerator.MoveNext();
				IEnumerator<Node> enumerator2 = n.Child0.Children.GetEnumerator();
				int i = n.Child0.Children.Count;
				enumerator2.MoveNext();
				while (i < num)
				{
					PlanCompiler.Assert(typeInfo2.HasNullSentinelProperty && !typeInfo.HasNullSentinelProperty, "NullSentinelProperty mismatch on input?");
					list.Add(this.CreateNullSentinelConstant());
					enumerator.MoveNext();
					num--;
				}
				while (i > num)
				{
					PlanCompiler.Assert(!typeInfo2.HasNullSentinelProperty && typeInfo.HasNullSentinelProperty, "NullSentinelProperty mismatch on output?");
					enumerator2.MoveNext();
					i--;
				}
				do
				{
					EdmProperty edmProperty = enumerator.Current;
					Node node = this.BuildSoftCast(enumerator2.Current, Helper.GetModelTypeUsage(edmProperty));
					list.Add(node);
					enumerator.MoveNext();
				}
				while (enumerator2.MoveNext());
				return this.m_command.CreateNode(newRecordOp, list);
			}
			if (TypeSemantics.IsCollectionType(type2))
			{
				return this.BuildSoftCast(n.Child0, newType);
			}
			if (TypeSemantics.IsPrimitiveType(type2))
			{
				return n;
			}
			PlanCompiler.Assert(TypeSemantics.IsNominalType(type2) || TypeSemantics.IsReferenceType(type2), "Gasp! Not a nominal type or even a reference type");
			PlanCompiler.Assert(Command.EqualTypes(newType, n.Child0.Op.Type), "Types are not equal");
			return n.Child0;
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x00082318 File Offset: 0x00080518
		public override Node Visit(CastOp op, Node n)
		{
			this.VisitChildren(n);
			if (TypeSemantics.IsEnumerationType(op.Type))
			{
				PlanCompiler.Assert(TypeSemantics.IsPrimitiveType(n.Child0.Op.Type), "Primitive type expected.");
				PrimitiveType underlyingEdmTypeForEnumType = Helper.GetUnderlyingEdmTypeForEnumType(op.Type.EdmType);
				return this.RewriteAsCastToUnderlyingType(underlyingEdmTypeForEnumType, op, n);
			}
			if (TypeSemantics.IsSpatialType(op.Type))
			{
				PlanCompiler.Assert(TypeSemantics.IsPrimitiveType(n.Child0.Op.Type, PrimitiveTypeKind.Geography) || TypeSemantics.IsPrimitiveType(n.Child0.Op.Type, PrimitiveTypeKind.Geometry), "Union spatial type expected.");
				PrimitiveType spatialNormalizedPrimitiveType = Helper.GetSpatialNormalizedPrimitiveType(op.Type.EdmType);
				return this.RewriteAsCastToUnderlyingType(spatialNormalizedPrimitiveType, op, n);
			}
			return n;
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x000823DC File Offset: 0x000805DC
		private Node RewriteAsCastToUnderlyingType(PrimitiveType underlyingType, CastOp op, Node n)
		{
			if (underlyingType.PrimitiveTypeKind == ((PrimitiveType)n.Child0.Op.Type.EdmType).PrimitiveTypeKind)
			{
				return n.Child0;
			}
			return this.m_command.CreateNode(this.m_command.CreateCastOp(TypeUsage.Create(underlyingType, op.Type.Facets)), n.Child0);
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x00082444 File Offset: 0x00080644
		public override Node Visit(ConstantOp op, Node n)
		{
			PlanCompiler.Assert(n.Children.Count == 0, "Constant operations don't have children.");
			PlanCompiler.Assert(op.Value != null, "Value must not be null");
			if (TypeSemantics.IsEnumerationType(op.Type))
			{
				object obj = (op.Value.GetType().IsEnum() ? Convert.ChangeType(op.Value, op.Value.GetType().GetEnumUnderlyingType(), CultureInfo.InvariantCulture) : op.Value);
				return this.m_command.CreateNode(this.m_command.CreateConstantOp(TypeHelpers.CreateEnumUnderlyingTypeUsage(op.Type), obj));
			}
			if (TypeSemantics.IsStrongSpatialType(op.Type))
			{
				op.Type = TypeHelpers.CreateSpatialUnionTypeUsage(op.Type);
			}
			return n;
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x00082508 File Offset: 0x00080708
		public override Node Visit(CaseOp op, Node n)
		{
			bool flag2;
			bool flag = PlanCompilerUtil.IsRowTypeCaseOpWithNullability(op, n, out flag2);
			this.VisitChildren(n);
			Node node;
			if (flag && this.TryRewriteCaseOp(n, flag2, out node))
			{
				return node;
			}
			if (TypeUtils.IsCollectionType(op.Type) || TypeSemantics.IsEnumerationType(op.Type) || TypeSemantics.IsStrongSpatialType(op.Type))
			{
				TypeUsage newType = this.GetNewType(op.Type);
				n.Op = this.m_command.CreateCaseOp(newType);
				return n;
			}
			if (TypeUtils.IsStructuredType(op.Type))
			{
				PropertyRefList propertyRefList = this.m_nodePropertyMap[n];
				return this.FlattenCaseOp(n, this.m_typeInfo.GetTypeInfo(op.Type), propertyRefList);
			}
			return n;
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x000825B4 File Offset: 0x000807B4
		private bool TryRewriteCaseOp(Node n, bool thenClauseIsNull, out Node rewrittenNode)
		{
			rewrittenNode = n;
			if (!this.m_typeInfo.GetTypeInfo(n.Op.Type).HasNullSentinelProperty)
			{
				return false;
			}
			Node node = (thenClauseIsNull ? n.Child2 : n.Child1);
			if (node.Op.OpType != OpType.NewRecord)
			{
				return false;
			}
			Node child = node.Child0;
			TypeUsage integerType = this.m_command.IntegerType;
			PlanCompiler.Assert(child.Op.Type.EdmEquals(integerType), "Column that is expected to be a null sentinel is not of Integer type.");
			CaseOp caseOp = this.m_command.CreateCaseOp(integerType);
			List<Node> list = new List<Node>(3);
			list.Add(n.Child0);
			Node node2 = this.m_command.CreateNode(this.m_command.CreateNullOp(integerType));
			Node node3 = (thenClauseIsNull ? node2 : child);
			Node node4 = (thenClauseIsNull ? child : node2);
			list.Add(node3);
			list.Add(node4);
			node.Child0 = this.m_command.CreateNode(caseOp, list);
			rewrittenNode = node;
			return true;
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x000826B0 File Offset: 0x000808B0
		private Node FlattenCaseOp(Node n, TypeInfo typeInfo, PropertyRefList desiredProperties)
		{
			List<EdmProperty> list = new List<EdmProperty>();
			List<Node> list2 = new List<Node>();
			foreach (PropertyRef propertyRef in typeInfo.PropertyRefList)
			{
				if (desiredProperties.Contains(propertyRef))
				{
					EdmProperty newProperty = typeInfo.GetNewProperty(propertyRef);
					List<Node> list3 = new List<Node>();
					for (int i = 0; i < n.Children.Count - 1; i++)
					{
						Node node = this.Copy(n.Children[i]);
						list3.Add(node);
						i++;
						Node node2 = this.BuildAccessorWithNulls(n.Children[i], newProperty);
						list3.Add(node2);
					}
					Node node3 = this.BuildAccessorWithNulls(n.Children[n.Children.Count - 1], newProperty);
					list3.Add(node3);
					Node node4 = this.m_command.CreateNode(this.m_command.CreateCaseOp(Helper.GetModelTypeUsage(newProperty)), list3);
					list.Add(newProperty);
					list2.Add(node4);
				}
			}
			NewRecordOp newRecordOp = this.m_command.CreateNewRecordOp(typeInfo.FlattenedTypeUsage, list);
			return this.m_command.CreateNode(newRecordOp, list2);
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x00082804 File Offset: 0x00080A04
		public override Node Visit(CollectOp op, Node n)
		{
			this.VisitChildren(n);
			n.Op = this.m_command.CreateCollectOp(this.GetNewType(op.Type));
			return n;
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x0008282C File Offset: 0x00080A2C
		public override Node Visit(ComparisonOp op, Node n)
		{
			TypeUsage type = n.Child0.Op.Type;
			TypeUsage type2 = n.Child1.Op.Type;
			if (!TypeUtils.IsStructuredType(type))
			{
				return this.VisitScalarOpDefault(op, n);
			}
			this.VisitChildren(n);
			PlanCompiler.Assert(!TypeSemantics.IsComplexType(type) && !TypeSemantics.IsComplexType(type2), "complex type?");
			PlanCompiler.Assert(op.OpType == OpType.EQ || op.OpType == OpType.NE, "non-equality comparison of structured types?");
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(type);
			TypeInfo typeInfo2 = this.m_typeInfo.GetTypeInfo(type2);
			List<EdmProperty> list;
			List<Node> list2;
			this.GetPropertyValues(typeInfo, NominalTypeEliminator.OperationKind.Equality, n.Child0, false, out list, out list2);
			List<EdmProperty> list3;
			List<Node> list4;
			this.GetPropertyValues(typeInfo2, NominalTypeEliminator.OperationKind.Equality, n.Child1, false, out list3, out list4);
			PlanCompiler.Assert(list.Count == list3.Count && list2.Count == list4.Count, "different shaped structured types?");
			Node node = null;
			for (int i = 0; i < list2.Count; i++)
			{
				ComparisonOp comparisonOp = this.m_command.CreateComparisonOp(op.OpType, op.UseDatabaseNullSemantics);
				Node node2 = this.m_command.CreateNode(comparisonOp, list2[i], list4[i]);
				if (node == null)
				{
					node = node2;
				}
				else
				{
					node = this.m_command.CreateNode(this.m_command.CreateConditionalOp(OpType.And), node, node2);
				}
			}
			return node;
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x0008299C File Offset: 0x00080B9C
		public override Node Visit(ConditionalOp op, Node n)
		{
			if (op.OpType != OpType.IsNull)
			{
				return this.VisitScalarOpDefault(op, n);
			}
			TypeUsage type = n.Child0.Op.Type;
			if (!TypeUtils.IsStructuredType(type))
			{
				return this.VisitScalarOpDefault(op, n);
			}
			this.VisitChildren(n);
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(type);
			List<EdmProperty> list = null;
			List<Node> list2 = null;
			this.GetPropertyValues(typeInfo, NominalTypeEliminator.OperationKind.IsNull, n.Child0, false, out list, out list2);
			PlanCompiler.Assert(list.Count == list2.Count && list.Count > 0, "No properties returned from GetPropertyValues(IsNull)?");
			Node node = null;
			foreach (Node node2 in list2)
			{
				Node node3 = this.m_command.CreateNode(this.m_command.CreateConditionalOp(OpType.IsNull), node2);
				if (node == null)
				{
					node = node3;
				}
				else
				{
					node = this.m_command.CreateNode(this.m_command.CreateConditionalOp(OpType.And), node, node3);
				}
			}
			return node;
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x00082AB4 File Offset: 0x00080CB4
		public override Node Visit(ConstrainedSortOp op, Node n)
		{
			this.VisitChildren(n);
			List<global::System.Data.Entity.Core.Query.InternalTrees.SortKey> list = this.HandleSortKeys(op.Keys);
			if (list != op.Keys)
			{
				n.Op = this.m_command.CreateConstrainedSortOp(list, op.WithTies);
			}
			return n;
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x00082AF7 File Offset: 0x00080CF7
		public override Node Visit(GetEntityRefOp op, Node n)
		{
			return this.FlattenGetKeyOp(op, n);
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x00082B01 File Offset: 0x00080D01
		public override Node Visit(GetRefKeyOp op, Node n)
		{
			return this.FlattenGetKeyOp(op, n);
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x00082B0C File Offset: 0x00080D0C
		private Node FlattenGetKeyOp(ScalarOp op, Node n)
		{
			PlanCompiler.Assert(op.OpType == OpType.GetEntityRef || op.OpType == OpType.GetRefKey, "Expecting GetEntityRef or GetRefKey ops");
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(n.Child0.Op.Type);
			TypeInfo typeInfo2 = this.m_typeInfo.GetTypeInfo(op.Type);
			this.VisitChildren(n);
			List<Node> list2;
			if (op.OpType == OpType.GetRefKey)
			{
				List<EdmProperty> list;
				this.GetPropertyValues(typeInfo, NominalTypeEliminator.OperationKind.GetKeys, n.Child0, false, out list, out list2);
			}
			else
			{
				PlanCompiler.Assert(op.OpType == OpType.GetEntityRef, "Expected OpType.GetEntityRef: Found " + op.OpType.ToString());
				List<EdmProperty> list;
				this.GetPropertyValues(typeInfo, NominalTypeEliminator.OperationKind.GetIdentity, n.Child0, false, out list, out list2);
			}
			if (typeInfo2.HasNullSentinelProperty && !typeInfo.HasNullSentinelProperty)
			{
				list2.Insert(0, this.CreateNullSentinelConstant());
			}
			List<EdmProperty> list3 = new List<EdmProperty>(typeInfo2.FlattenedType.Properties);
			PlanCompiler.Assert(list2.Count == list3.Count, "fieldTypes.Count mismatch?");
			NewRecordOp newRecordOp = this.m_command.CreateNewRecordOp(typeInfo2.FlattenedTypeUsage, list3);
			return this.m_command.CreateNode(newRecordOp, list2);
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x00082C3C File Offset: 0x00080E3C
		private Node VisitPropertyOp(Op op, Node n, PropertyRef propertyRef, bool throwIfMissing)
		{
			PlanCompiler.Assert(op.OpType == OpType.Property || op.OpType == OpType.RelProperty, "Unexpected optype: " + op.OpType.ToString());
			TypeUsage type = n.Child0.Op.Type;
			TypeUsage type2 = op.Type;
			this.VisitChildren(n);
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(type);
			Node node2;
			if (TypeUtils.IsStructuredType(type2))
			{
				TypeInfo typeInfo2 = this.m_typeInfo.GetTypeInfo(type2);
				List<EdmProperty> list = new List<EdmProperty>();
				List<Node> list2 = new List<Node>();
				PropertyRefList propertyRefList = this.m_nodePropertyMap[n];
				foreach (PropertyRef propertyRef2 in typeInfo2.PropertyRefList)
				{
					if (propertyRefList.Contains(propertyRef2))
					{
						PropertyRef propertyRef3 = propertyRef2.CreateNestedPropertyRef(propertyRef);
						EdmProperty edmProperty;
						if (typeInfo.TryGetNewProperty(propertyRef3, throwIfMissing, out edmProperty))
						{
							EdmProperty newProperty = typeInfo2.GetNewProperty(propertyRef2);
							Node node = this.BuildAccessor(n.Child0, edmProperty);
							if (node != null)
							{
								list.Add(newProperty);
								list2.Add(node);
							}
						}
					}
				}
				Op op2 = this.m_command.CreateNewRecordOp(typeInfo2.FlattenedTypeUsage, list);
				node2 = this.m_command.CreateNode(op2, list2);
			}
			else
			{
				EdmProperty newProperty2 = typeInfo.GetNewProperty(propertyRef);
				node2 = this.BuildAccessorWithNulls(n.Child0, newProperty2);
			}
			return node2;
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x00082DBC File Offset: 0x00080FBC
		public override Node Visit(PropertyOp op, Node n)
		{
			return this.VisitPropertyOp(op, n, new SimplePropertyRef(op.PropertyInfo), true);
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x00082DD2 File Offset: 0x00080FD2
		public override Node Visit(RelPropertyOp op, Node n)
		{
			return this.VisitPropertyOp(op, n, new RelPropertyRef(op.PropertyInfo), false);
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x00082DE8 File Offset: 0x00080FE8
		public override Node Visit(RefOp op, Node n)
		{
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(n.Child0.Op.Type);
			TypeInfo typeInfo2 = this.m_typeInfo.GetTypeInfo(op.Type);
			this.VisitChildren(n);
			List<EdmProperty> list;
			List<Node> list2;
			this.GetPropertyValues(typeInfo, NominalTypeEliminator.OperationKind.All, n.Child0, false, out list, out list2);
			List<EdmProperty> list3 = new List<EdmProperty>(typeInfo2.FlattenedType.Properties);
			if (typeInfo2.HasEntitySetIdProperty)
			{
				PlanCompiler.Assert(list3[0] == typeInfo2.EntitySetIdProperty, "OutputField0 must be the entitySetId property");
				if (typeInfo.HasNullSentinelProperty && !typeInfo2.HasNullSentinelProperty)
				{
					PlanCompiler.Assert(list3.Count == list.Count, "Mismatched field count: Expected " + list.Count.ToString() + "; Got " + list3.Count.ToString());
					NominalTypeEliminator.RemoveNullSentinel(typeInfo, list, list2);
				}
				else
				{
					PlanCompiler.Assert(list3.Count == list.Count + 1, "Mismatched field count: Expected " + (list.Count + 1).ToString() + "; Got " + list3.Count.ToString());
				}
				int entitySetId = this.m_typeInfo.GetEntitySetId(op.EntitySet);
				list2.Insert(0, this.m_command.CreateNode(this.m_command.CreateInternalConstantOp(Helper.GetModelTypeUsage(typeInfo2.EntitySetIdProperty), entitySetId)));
			}
			else
			{
				if (typeInfo.HasNullSentinelProperty && !typeInfo2.HasNullSentinelProperty)
				{
					NominalTypeEliminator.RemoveNullSentinel(typeInfo, list, list2);
				}
				PlanCompiler.Assert(list3.Count == list.Count, "Mismatched field count: Expected " + list.Count.ToString() + "; Got " + list3.Count.ToString());
			}
			NewRecordOp newRecordOp = this.m_command.CreateNewRecordOp(typeInfo2.FlattenedTypeUsage, list3);
			return this.m_command.CreateNode(newRecordOp, list2);
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x00082FD7 File Offset: 0x000811D7
		private static void RemoveNullSentinel(TypeInfo inputTypeInfo, List<EdmProperty> inputFields, List<Node> inputFieldValues)
		{
			PlanCompiler.Assert(inputFields[0] == inputTypeInfo.NullSentinelProperty, "InputField0 must be the null sentinel property");
			inputFields.RemoveAt(0);
			inputFieldValues.RemoveAt(0);
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x00083000 File Offset: 0x00081200
		public override Node Visit(VarRefOp op, Node n)
		{
			VarInfo varInfo;
			if (!this.m_varInfoMap.TryGetVarInfo(op.Var, out varInfo))
			{
				bool flag = !TypeUtils.IsStructuredType(op.Type);
				string text = "No varInfo for a structured type var: Id = ";
				string text2 = op.Var.Id.ToString();
				string text3 = " Type = ";
				TypeUsage type = op.Type;
				PlanCompiler.Assert(flag, text + text2 + text3 + ((type != null) ? type.ToString() : null));
				return n;
			}
			if (varInfo.Kind == VarInfoKind.CollectionVarInfo)
			{
				n.Op = this.m_command.CreateVarRefOp(((CollectionVarInfo)varInfo).NewVar);
				return n;
			}
			if (varInfo.Kind == VarInfoKind.PrimitiveTypeVarInfo)
			{
				n.Op = this.m_command.CreateVarRefOp(((PrimitiveTypeVarInfo)varInfo).NewVar);
				return n;
			}
			StructuredVarInfo structuredVarInfo = (StructuredVarInfo)varInfo;
			NewRecordOp newRecordOp = this.m_command.CreateNewRecordOp(structuredVarInfo.NewTypeUsage, structuredVarInfo.Fields);
			List<Node> list = new List<Node>();
			foreach (Var var in varInfo.NewVars)
			{
				VarRefOp varRefOp = this.m_command.CreateVarRefOp(var);
				list.Add(this.m_command.CreateNode(varRefOp));
			}
			return this.m_command.CreateNode(newRecordOp, list);
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x00083150 File Offset: 0x00081350
		public override Node Visit(NewEntityOp op, Node n)
		{
			return this.FlattenConstructor(op, n);
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x0008315A File Offset: 0x0008135A
		public override Node Visit(NewInstanceOp op, Node n)
		{
			return this.FlattenConstructor(op, n);
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x00083164 File Offset: 0x00081364
		public override Node Visit(DiscriminatedNewEntityOp op, Node n)
		{
			return this.FlattenConstructor(op, n);
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x00083170 File Offset: 0x00081370
		private Node NormalizeTypeDiscriminatorValues(DiscriminatedNewEntityOp op, Node discriminator)
		{
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(op.Type);
			CaseOp caseOp = this.m_command.CreateCaseOp(typeInfo.RootType.TypeIdProperty.TypeUsage);
			List<Node> list = new List<Node>(op.DiscriminatorMap.TypeMap.Count * 2 - 1);
			for (int i = 0; i < op.DiscriminatorMap.TypeMap.Count; i++)
			{
				object key = op.DiscriminatorMap.TypeMap[i].Key;
				EntityType value = op.DiscriminatorMap.TypeMap[i].Value;
				TypeInfo typeInfo2 = this.m_typeInfo.GetTypeInfo(TypeUsage.Create(value));
				Node node = this.CreateTypeIdConstant(typeInfo2);
				if (i == op.DiscriminatorMap.TypeMap.Count - 1)
				{
					list.Add(node);
				}
				else
				{
					ConstantBaseOp constantBaseOp = this.m_command.CreateConstantOp(Helper.GetModelTypeUsage(op.DiscriminatorMap.DiscriminatorProperty.TypeUsage), key);
					Node node2 = this.m_command.CreateNode(constantBaseOp);
					ComparisonOp comparisonOp = this.m_command.CreateComparisonOp(OpType.EQ, false);
					Node node3 = this.m_command.CreateNode(comparisonOp, discriminator, node2);
					list.Add(node3);
					list.Add(node);
				}
			}
			discriminator = this.m_command.CreateNode(caseOp, list);
			return discriminator;
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x000832D0 File Offset: 0x000814D0
		public override Node Visit(NewRecordOp op, Node n)
		{
			return this.FlattenConstructor(op, n);
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x000832DC File Offset: 0x000814DC
		private Node GetEntitySetIdExpr(EdmProperty entitySetIdProperty, NewEntityBaseOp op)
		{
			EntitySet entitySet = op.EntitySet;
			Node node;
			if (entitySet != null)
			{
				int entitySetId = this.m_typeInfo.GetEntitySetId(entitySet);
				InternalConstantOp internalConstantOp = this.m_command.CreateInternalConstantOp(Helper.GetModelTypeUsage(entitySetIdProperty), entitySetId);
				node = this.m_command.CreateNode(internalConstantOp);
			}
			else
			{
				node = this.CreateNullConstantNode(Helper.GetModelTypeUsage(entitySetIdProperty));
			}
			return node;
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x00083338 File Offset: 0x00081538
		private Node FlattenConstructor(ScalarOp op, Node n)
		{
			PlanCompiler.Assert(op.OpType == OpType.NewInstance || op.OpType == OpType.NewRecord || op.OpType == OpType.DiscriminatedNewEntity || op.OpType == OpType.NewEntity, "unexpected op: " + op.OpType.ToString() + "?");
			this.VisitChildren(n);
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(op.Type);
			RowType flattenedType = typeInfo.FlattenedType;
			NewEntityBaseOp newEntityBaseOp = op as NewEntityBaseOp;
			DiscriminatedNewEntityOp discriminatedNewEntityOp = null;
			IEnumerable enumerable;
			if (op.OpType == OpType.NewRecord)
			{
				enumerable = ((NewRecordOp)op).Properties;
			}
			else if (op.OpType == OpType.DiscriminatedNewEntity)
			{
				discriminatedNewEntityOp = (DiscriminatedNewEntityOp)op;
				enumerable = discriminatedNewEntityOp.DiscriminatorMap.Properties;
			}
			else
			{
				enumerable = TypeHelpers.GetAllStructuralMembers(op.Type);
			}
			List<EdmProperty> list = new List<EdmProperty>();
			List<Node> list2 = new List<Node>();
			if (typeInfo.HasTypeIdProperty)
			{
				list.Add(typeInfo.TypeIdProperty);
				if (discriminatedNewEntityOp == null)
				{
					list2.Add(this.CreateTypeIdConstant(typeInfo));
				}
				else
				{
					Node node = n.Children[0];
					if (typeInfo.RootType.DiscriminatorMap == null)
					{
						node = this.NormalizeTypeDiscriminatorValues(discriminatedNewEntityOp, node);
					}
					list2.Add(node);
				}
			}
			if (typeInfo.HasEntitySetIdProperty)
			{
				list.Add(typeInfo.EntitySetIdProperty);
				PlanCompiler.Assert(newEntityBaseOp != null, "unexpected optype:" + op.OpType.ToString());
				Node entitySetIdExpr = this.GetEntitySetIdExpr(typeInfo.EntitySetIdProperty, newEntityBaseOp);
				list2.Add(entitySetIdExpr);
			}
			if (typeInfo.HasNullSentinelProperty)
			{
				list.Add(typeInfo.NullSentinelProperty);
				list2.Add(this.CreateNullSentinelConstant());
			}
			int num = ((discriminatedNewEntityOp == null) ? 0 : 1);
			foreach (object obj in enumerable)
			{
				EdmMember edmMember = (EdmMember)obj;
				Node node2 = n.Children[num];
				if (TypeUtils.IsStructuredType(Helper.GetModelTypeUsage(edmMember)))
				{
					RowType flattenedType2 = this.m_typeInfo.GetTypeInfo(Helper.GetModelTypeUsage(edmMember)).FlattenedType;
					int num2 = typeInfo.RootType.GetNestedStructureOffset(new SimplePropertyRef(edmMember));
					using (ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator2 = flattenedType2.Properties.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							EdmProperty edmProperty = enumerator2.Current;
							Node node3 = this.BuildAccessor(node2, edmProperty);
							if (node3 != null)
							{
								list.Add(flattenedType.Properties[num2]);
								list2.Add(node3);
							}
							num2++;
						}
						goto IL_029B;
					}
					goto IL_0276;
				}
				goto IL_0276;
				IL_029B:
				num++;
				continue;
				IL_0276:
				PropertyRef propertyRef = new SimplePropertyRef(edmMember);
				EdmProperty newProperty = typeInfo.GetNewProperty(propertyRef);
				list.Add(newProperty);
				list2.Add(node2);
				goto IL_029B;
			}
			if (newEntityBaseOp != null)
			{
				foreach (RelProperty relProperty in newEntityBaseOp.RelationshipProperties)
				{
					Node node4 = n.Children[num];
					RowType flattenedType3 = this.m_typeInfo.GetTypeInfo(relProperty.ToEnd.TypeUsage).FlattenedType;
					int num3 = typeInfo.RootType.GetNestedStructureOffset(new RelPropertyRef(relProperty));
					foreach (EdmProperty edmProperty2 in flattenedType3.Properties)
					{
						Node node5 = this.BuildAccessor(node4, edmProperty2);
						if (node5 != null)
						{
							list.Add(flattenedType.Properties[num3]);
							list2.Add(node5);
						}
						num3++;
					}
					num++;
				}
			}
			NewRecordOp newRecordOp = this.m_command.CreateNewRecordOp(typeInfo.FlattenedTypeUsage, list);
			return this.m_command.CreateNode(newRecordOp, list2);
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x00083748 File Offset: 0x00081948
		public override Node Visit(NullOp op, Node n)
		{
			if (!TypeUtils.IsStructuredType(op.Type))
			{
				if (TypeSemantics.IsEnumerationType(op.Type))
				{
					op.Type = TypeHelpers.CreateEnumUnderlyingTypeUsage(op.Type);
				}
				else if (TypeSemantics.IsStrongSpatialType(op.Type))
				{
					op.Type = TypeHelpers.CreateSpatialUnionTypeUsage(op.Type);
				}
				return n;
			}
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(op.Type);
			List<EdmProperty> list = new List<EdmProperty>();
			List<Node> list2 = new List<Node>();
			if (typeInfo.HasTypeIdProperty)
			{
				list.Add(typeInfo.TypeIdProperty);
				TypeUsage modelTypeUsage = Helper.GetModelTypeUsage(typeInfo.TypeIdProperty);
				list2.Add(this.CreateNullConstantNode(modelTypeUsage));
			}
			NewRecordOp newRecordOp = new NewRecordOp(typeInfo.FlattenedTypeUsage, list);
			return this.m_command.CreateNode(newRecordOp, list2);
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x0008380C File Offset: 0x00081A0C
		public override Node Visit(IsOfOp op, Node n)
		{
			this.VisitChildren(n);
			if (!TypeUtils.IsStructuredType(op.IsOfType))
			{
				return n;
			}
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(op.IsOfType);
			return this.CreateTypeComparisonOp(n.Child0, typeInfo, op.IsOfOnly);
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x00083854 File Offset: 0x00081A54
		public override Node Visit(TreatOp op, Node n)
		{
			this.VisitChildren(n);
			ScalarOp scalarOp = (ScalarOp)n.Child0.Op;
			if (op.IsFakeTreat || TypeSemantics.IsStructurallyEqual(scalarOp.Type, op.Type) || TypeSemantics.IsSubTypeOf(scalarOp.Type, op.Type))
			{
				return n.Child0;
			}
			if (!TypeUtils.IsStructuredType(op.Type))
			{
				return n;
			}
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(op.Type);
			Node node = this.CreateTypeComparisonOp(n.Child0, typeInfo, false);
			CaseOp caseOp = this.m_command.CreateCaseOp(typeInfo.FlattenedTypeUsage);
			Node node2 = this.m_command.CreateNode(caseOp, node, n.Child0, this.CreateNullConstantNode(caseOp.Type));
			PropertyRefList propertyRefList = this.m_nodePropertyMap[n];
			return this.FlattenCaseOp(node2, typeInfo, propertyRefList);
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x0008392C File Offset: 0x00081B2C
		private Node CreateTypeComparisonOp(Node input, TypeInfo typeInfo, bool isExact)
		{
			Node node = this.BuildTypeIdAccessor(input, typeInfo);
			Node node2;
			if (isExact)
			{
				node2 = this.CreateTypeEqualsOp(typeInfo, node);
			}
			else if (typeInfo.RootType.DiscriminatorMap != null)
			{
				node2 = this.CreateDisjunctiveTypeComparisonOp(typeInfo, node);
			}
			else
			{
				Node node3 = this.CreateTypeIdConstantForPrefixMatch(typeInfo);
				LikeOp likeOp = this.m_command.CreateLikeOp();
				node2 = this.m_command.CreateNode(likeOp, node, node3, this.CreateNullConstantNode(this.DefaultTypeIdType));
			}
			return node2;
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x0008399C File Offset: 0x00081B9C
		private Node CreateDisjunctiveTypeComparisonOp(TypeInfo typeInfo, Node typeIdProperty)
		{
			PlanCompiler.Assert(typeInfo.RootType.DiscriminatorMap != null, "should be used only for DiscriminatorMap type checks");
			IEnumerable<TypeInfo> enumerable = from t in typeInfo.GetTypeHierarchy()
				where !t.Type.EdmType.Abstract
				select t;
			Node node = null;
			foreach (TypeInfo typeInfo2 in enumerable)
			{
				Node node2 = this.CreateTypeEqualsOp(typeInfo2, typeIdProperty);
				if (node == null)
				{
					node = node2;
				}
				else
				{
					node = this.m_command.CreateNode(this.m_command.CreateConditionalOp(OpType.Or), node, node2);
				}
			}
			if (node == null)
			{
				node = this.m_command.CreateNode(this.m_command.CreateFalseOp());
			}
			return node;
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x00083A68 File Offset: 0x00081C68
		private Node CreateTypeEqualsOp(TypeInfo typeInfo, Node typeIdProperty)
		{
			Node node = this.CreateTypeIdConstant(typeInfo);
			ComparisonOp comparisonOp = this.m_command.CreateComparisonOp(OpType.EQ, false);
			return this.m_command.CreateNode(comparisonOp, typeIdProperty, node);
		}

		// Token: 0x04000E30 RID: 3632
		private readonly Dictionary<Var, PropertyRefList> m_varPropertyMap;

		// Token: 0x04000E31 RID: 3633
		private readonly Dictionary<Node, PropertyRefList> m_nodePropertyMap;

		// Token: 0x04000E32 RID: 3634
		private readonly VarInfoMap m_varInfoMap;

		// Token: 0x04000E33 RID: 3635
		private readonly PlanCompiler m_compilerState;

		// Token: 0x04000E34 RID: 3636
		private readonly StructuredTypeInfo m_typeInfo;

		// Token: 0x04000E35 RID: 3637
		private readonly Dictionary<EdmFunction, EdmProperty[]> m_tvfResultKeys;

		// Token: 0x04000E36 RID: 3638
		private readonly Dictionary<TypeUsage, TypeUsage> m_typeToNewTypeMap;

		// Token: 0x04000E37 RID: 3639
		private const string PrefixMatchCharacter = "%";

		// Token: 0x020009EB RID: 2539
		internal enum OperationKind
		{
			// Token: 0x0400288A RID: 10378
			Equality,
			// Token: 0x0400288B RID: 10379
			IsNull,
			// Token: 0x0400288C RID: 10380
			GetIdentity,
			// Token: 0x0400288D RID: 10381
			GetKeys,
			// Token: 0x0400288E RID: 10382
			All
		}
	}
}
