using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.PlanCompiler;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200038E RID: 910
	internal class Command
	{
		// Token: 0x06002C1D RID: 11293 RVA: 0x0008F000 File Offset: 0x0008D200
		internal Command(MetadataWorkspace metadataWorkspace)
		{
			this.m_parameterMap = new Dictionary<string, ParameterVar>();
			this.m_vars = new List<Var>();
			this.m_tables = new List<Table>();
			this.m_metadataWorkspace = metadataWorkspace;
			if (!Command.TryGetPrimitiveType(PrimitiveTypeKind.Boolean, out this.m_boolType))
			{
				throw new ProviderIncompatibleException(Strings.Cqt_General_NoProviderBooleanType);
			}
			if (!Command.TryGetPrimitiveType(PrimitiveTypeKind.Int32, out this.m_intType))
			{
				throw new ProviderIncompatibleException(Strings.Cqt_General_NoProviderIntegerType);
			}
			if (!Command.TryGetPrimitiveType(PrimitiveTypeKind.String, out this.m_stringType))
			{
				throw new ProviderIncompatibleException(Strings.Cqt_General_NoProviderStringType);
			}
			this.m_trueOp = new ConstantPredicateOp(this.m_boolType, true);
			this.m_falseOp = new ConstantPredicateOp(this.m_boolType, false);
			this.m_nodeInfoVisitor = new NodeInfoVisitor(this);
			this.m_keyPullupVisitor = new KeyPullup(this);
			this.m_freeVarVecEnumerators = new Stack<VarVec.VarVecEnumerator>();
			this.m_freeVarVecs = new Stack<VarVec>();
			this.m_referencedRelProperties = new HashSet<RelProperty>();
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x0008F0F0 File Offset: 0x0008D2F0
		internal Command()
		{
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x0008F103 File Offset: 0x0008D303
		internal virtual MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this.m_metadataWorkspace;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06002C20 RID: 11296 RVA: 0x0008F10B File Offset: 0x0008D30B
		// (set) Token: 0x06002C21 RID: 11297 RVA: 0x0008F113 File Offset: 0x0008D313
		internal virtual Node Root { get; set; }

		// Token: 0x06002C22 RID: 11298 RVA: 0x0008F11C File Offset: 0x0008D31C
		internal virtual void DisableVarVecEnumCaching()
		{
			this.m_disableVarVecEnumCaching = true;
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06002C23 RID: 11299 RVA: 0x0008F128 File Offset: 0x0008D328
		internal virtual int NextBranchDiscriminatorValue
		{
			get
			{
				int nextBranchDiscriminatorValue = this.m_nextBranchDiscriminatorValue;
				this.m_nextBranchDiscriminatorValue = nextBranchDiscriminatorValue + 1;
				return nextBranchDiscriminatorValue;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06002C24 RID: 11300 RVA: 0x0008F146 File Offset: 0x0008D346
		internal virtual int NextNodeId
		{
			get
			{
				return this.m_nextNodeId;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06002C25 RID: 11301 RVA: 0x0008F14E File Offset: 0x0008D34E
		internal virtual TypeUsage BooleanType
		{
			get
			{
				return this.m_boolType;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06002C26 RID: 11302 RVA: 0x0008F156 File Offset: 0x0008D356
		internal virtual TypeUsage IntegerType
		{
			get
			{
				return this.m_intType;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06002C27 RID: 11303 RVA: 0x0008F15E File Offset: 0x0008D35E
		internal virtual TypeUsage StringType
		{
			get
			{
				return this.m_stringType;
			}
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x0008F166 File Offset: 0x0008D366
		private static bool TryGetPrimitiveType(PrimitiveTypeKind modelType, out TypeUsage type)
		{
			type = null;
			if (modelType == PrimitiveTypeKind.String)
			{
				type = TypeUsage.CreateStringTypeUsage(MetadataWorkspace.GetModelPrimitiveType(modelType), false, false);
			}
			else
			{
				type = MetadataWorkspace.GetCanonicalModelTypeUsage(modelType);
			}
			return type != null;
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x0008F190 File Offset: 0x0008D390
		internal virtual VarVec CreateVarVec()
		{
			VarVec varVec;
			if (this.m_freeVarVecs.Count == 0)
			{
				varVec = new VarVec(this);
			}
			else
			{
				varVec = this.m_freeVarVecs.Pop();
				varVec.Clear();
			}
			return varVec;
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x0008F1C6 File Offset: 0x0008D3C6
		internal virtual VarVec CreateVarVec(Var v)
		{
			VarVec varVec = this.CreateVarVec();
			varVec.Set(v);
			return varVec;
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x0008F1D5 File Offset: 0x0008D3D5
		internal virtual VarVec CreateVarVec(IEnumerable<Var> v)
		{
			VarVec varVec = this.CreateVarVec();
			varVec.InitFrom(v);
			return varVec;
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x0008F1E4 File Offset: 0x0008D3E4
		internal virtual VarVec CreateVarVec(VarVec v)
		{
			VarVec varVec = this.CreateVarVec();
			varVec.InitFrom(v);
			return varVec;
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x0008F1F3 File Offset: 0x0008D3F3
		internal virtual void ReleaseVarVec(VarVec vec)
		{
			this.m_freeVarVecs.Push(vec);
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x0008F204 File Offset: 0x0008D404
		internal virtual VarVec.VarVecEnumerator GetVarVecEnumerator(VarVec vec)
		{
			VarVec.VarVecEnumerator varVecEnumerator;
			if (this.m_disableVarVecEnumCaching || this.m_freeVarVecEnumerators.Count == 0)
			{
				varVecEnumerator = new VarVec.VarVecEnumerator(vec);
			}
			else
			{
				varVecEnumerator = this.m_freeVarVecEnumerators.Pop();
				varVecEnumerator.Init(vec);
			}
			return varVecEnumerator;
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x0008F243 File Offset: 0x0008D443
		internal virtual void ReleaseVarVecEnumerator(VarVec.VarVecEnumerator enumerator)
		{
			if (!this.m_disableVarVecEnumCaching)
			{
				this.m_freeVarVecEnumerators.Push(enumerator);
			}
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x0008F259 File Offset: 0x0008D459
		internal static VarList CreateVarList()
		{
			return new VarList();
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x0008F260 File Offset: 0x0008D460
		internal static VarList CreateVarList(IEnumerable<Var> vars)
		{
			return new VarList(vars);
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x0008F268 File Offset: 0x0008D468
		private int NewTableId()
		{
			return this.m_tables.Count;
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x0008F275 File Offset: 0x0008D475
		internal static TableMD CreateTableDefinition(TypeUsage elementType)
		{
			return new TableMD(elementType, null);
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x0008F27E File Offset: 0x0008D47E
		internal static TableMD CreateTableDefinition(EntitySetBase extent)
		{
			return new TableMD(TypeUsage.Create(extent.ElementType), extent);
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x0008F291 File Offset: 0x0008D491
		internal virtual TableMD CreateFlatTableDefinition(RowType type)
		{
			return this.CreateFlatTableDefinition(type.Properties, new List<EdmMember>(), null);
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x0008F2A5 File Offset: 0x0008D4A5
		internal virtual TableMD CreateFlatTableDefinition(IEnumerable<EdmProperty> properties, IEnumerable<EdmMember> keyMembers, EntitySetBase entitySet)
		{
			return new TableMD(properties, keyMembers, entitySet);
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x0008F2B0 File Offset: 0x0008D4B0
		internal virtual Table CreateTableInstance(TableMD tableMetadata)
		{
			Table table = new Table(this, tableMetadata, this.NewTableId());
			this.m_tables.Add(table);
			return table;
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06002C38 RID: 11320 RVA: 0x0008F2D8 File Offset: 0x0008D4D8
		internal virtual IEnumerable<Var> Vars
		{
			get
			{
				return this.m_vars.Where((Var v) => v.VarType != VarType.NotValid);
			}
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x0008F304 File Offset: 0x0008D504
		internal virtual Var GetVar(int id)
		{
			return this.m_vars[id];
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x0008F312 File Offset: 0x0008D512
		internal virtual ParameterVar GetParameter(string paramName)
		{
			return this.m_parameterMap[paramName];
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x0008F320 File Offset: 0x0008D520
		private int NewVarId()
		{
			return this.m_vars.Count;
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x0008F330 File Offset: 0x0008D530
		internal virtual ParameterVar CreateParameterVar(string parameterName, TypeUsage parameterType)
		{
			if (this.m_parameterMap.ContainsKey(parameterName))
			{
				throw new ArgumentException(Strings.DuplicateParameterName(parameterName));
			}
			ParameterVar parameterVar = new ParameterVar(this.NewVarId(), parameterType, parameterName);
			this.m_vars.Add(parameterVar);
			this.m_parameterMap[parameterName] = parameterVar;
			return parameterVar;
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x0008F380 File Offset: 0x0008D580
		private ParameterVar ReplaceParameterVar(ParameterVar oldVar, Func<TypeUsage, TypeUsage> generateReplacementType)
		{
			ParameterVar parameterVar = new ParameterVar(this.NewVarId(), generateReplacementType(oldVar.Type), oldVar.ParameterName);
			this.m_parameterMap[oldVar.ParameterName] = parameterVar;
			this.m_vars.Add(parameterVar);
			return parameterVar;
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x0008F3CA File Offset: 0x0008D5CA
		internal virtual ParameterVar ReplaceEnumParameterVar(ParameterVar oldVar)
		{
			return this.ReplaceParameterVar(oldVar, (TypeUsage t) => TypeHelpers.CreateEnumUnderlyingTypeUsage(t));
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x0008F3F2 File Offset: 0x0008D5F2
		internal virtual ParameterVar ReplaceStrongSpatialParameterVar(ParameterVar oldVar)
		{
			return this.ReplaceParameterVar(oldVar, (TypeUsage t) => TypeHelpers.CreateSpatialUnionTypeUsage(t));
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x0008F41C File Offset: 0x0008D61C
		internal virtual ColumnVar CreateColumnVar(Table table, ColumnMD columnMD)
		{
			ColumnVar columnVar = new ColumnVar(this.NewVarId(), table, columnMD);
			table.Columns.Add(columnVar);
			this.m_vars.Add(columnVar);
			return columnVar;
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x0008F450 File Offset: 0x0008D650
		internal virtual ComputedVar CreateComputedVar(TypeUsage type)
		{
			ComputedVar computedVar = new ComputedVar(this.NewVarId(), type);
			this.m_vars.Add(computedVar);
			return computedVar;
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x0008F478 File Offset: 0x0008D678
		internal virtual SetOpVar CreateSetOpVar(TypeUsage type)
		{
			SetOpVar setOpVar = new SetOpVar(this.NewVarId(), type);
			this.m_vars.Add(setOpVar);
			return setOpVar;
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x0008F49F File Offset: 0x0008D69F
		internal virtual Node CreateNode(Op op)
		{
			return this.CreateNode(op, new List<Node>());
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x0008F4B0 File Offset: 0x0008D6B0
		internal virtual Node CreateNode(Op op, Node arg1)
		{
			return this.CreateNode(op, new List<Node> { arg1 });
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x0008F4D4 File Offset: 0x0008D6D4
		internal virtual Node CreateNode(Op op, Node arg1, Node arg2)
		{
			return this.CreateNode(op, new List<Node> { arg1, arg2 });
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x0008F500 File Offset: 0x0008D700
		internal virtual Node CreateNode(Op op, Node arg1, Node arg2, Node arg3)
		{
			return this.CreateNode(op, new List<Node> { arg1, arg2, arg3 });
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x0008F534 File Offset: 0x0008D734
		internal virtual Node CreateNode(Op op, IList<Node> args)
		{
			int nextNodeId = this.m_nextNodeId;
			this.m_nextNodeId = nextNodeId + 1;
			return new Node(nextNodeId, op, new List<Node>(args));
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x0008F560 File Offset: 0x0008D760
		internal virtual Node CreateNode(Op op, List<Node> args)
		{
			int nextNodeId = this.m_nextNodeId;
			this.m_nextNodeId = nextNodeId + 1;
			return new Node(nextNodeId, op, args);
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x0008F585 File Offset: 0x0008D785
		internal virtual ConstantBaseOp CreateConstantOp(TypeUsage type, object value)
		{
			if (value == null)
			{
				return new NullOp(type);
			}
			if (TypeSemantics.IsBooleanType(type))
			{
				return new InternalConstantOp(type, value);
			}
			return new ConstantOp(type, value);
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x0008F5A8 File Offset: 0x0008D7A8
		internal virtual InternalConstantOp CreateInternalConstantOp(TypeUsage type, object value)
		{
			return new InternalConstantOp(type, value);
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x0008F5B1 File Offset: 0x0008D7B1
		internal virtual NullSentinelOp CreateNullSentinelOp()
		{
			return new NullSentinelOp(this.IntegerType, 1);
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x0008F5C4 File Offset: 0x0008D7C4
		internal virtual NullOp CreateNullOp(TypeUsage type)
		{
			return new NullOp(type);
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x0008F5CC File Offset: 0x0008D7CC
		internal virtual ConstantPredicateOp CreateConstantPredicateOp(bool value)
		{
			if (!value)
			{
				return this.m_falseOp;
			}
			return this.m_trueOp;
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x0008F5DE File Offset: 0x0008D7DE
		internal virtual ConstantPredicateOp CreateTrueOp()
		{
			return this.m_trueOp;
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x0008F5E6 File Offset: 0x0008D7E6
		internal virtual ConstantPredicateOp CreateFalseOp()
		{
			return this.m_falseOp;
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x0008F5EE File Offset: 0x0008D7EE
		internal virtual FunctionOp CreateFunctionOp(EdmFunction function)
		{
			return new FunctionOp(function);
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x0008F5F6 File Offset: 0x0008D7F6
		internal virtual TreatOp CreateTreatOp(TypeUsage type)
		{
			return new TreatOp(type, false);
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x0008F5FF File Offset: 0x0008D7FF
		internal virtual TreatOp CreateFakeTreatOp(TypeUsage type)
		{
			return new TreatOp(type, true);
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x0008F608 File Offset: 0x0008D808
		internal virtual IsOfOp CreateIsOfOp(TypeUsage isOfType)
		{
			return new IsOfOp(isOfType, false, this.m_boolType);
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x0008F617 File Offset: 0x0008D817
		internal virtual IsOfOp CreateIsOfOnlyOp(TypeUsage isOfType)
		{
			return new IsOfOp(isOfType, true, this.m_boolType);
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x0008F626 File Offset: 0x0008D826
		internal virtual CastOp CreateCastOp(TypeUsage type)
		{
			return new CastOp(type);
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x0008F62E File Offset: 0x0008D82E
		internal virtual SoftCastOp CreateSoftCastOp(TypeUsage type)
		{
			return new SoftCastOp(type);
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x0008F636 File Offset: 0x0008D836
		internal virtual ComparisonOp CreateComparisonOp(OpType opType, bool useDatabaseNullSemantics = false)
		{
			return new ComparisonOp(opType, this.BooleanType)
			{
				UseDatabaseNullSemantics = useDatabaseNullSemantics
			};
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x0008F64B File Offset: 0x0008D84B
		internal virtual LikeOp CreateLikeOp()
		{
			return new LikeOp(this.BooleanType);
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x0008F658 File Offset: 0x0008D858
		internal virtual ConditionalOp CreateConditionalOp(OpType opType)
		{
			return new ConditionalOp(opType, this.BooleanType);
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x0008F666 File Offset: 0x0008D866
		internal virtual CaseOp CreateCaseOp(TypeUsage type)
		{
			return new CaseOp(type);
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x0008F66E File Offset: 0x0008D86E
		internal virtual AggregateOp CreateAggregateOp(EdmFunction aggFunc, bool distinctAgg)
		{
			return new AggregateOp(aggFunc, distinctAgg);
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x0008F677 File Offset: 0x0008D877
		internal virtual NewInstanceOp CreateNewInstanceOp(TypeUsage type)
		{
			return new NewInstanceOp(type);
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x0008F67F File Offset: 0x0008D87F
		internal virtual NewEntityOp CreateScopedNewEntityOp(TypeUsage type, List<RelProperty> relProperties, EntitySet entitySet)
		{
			return new NewEntityOp(type, relProperties, true, entitySet);
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x0008F68A File Offset: 0x0008D88A
		internal virtual NewEntityOp CreateNewEntityOp(TypeUsage type, List<RelProperty> relProperties)
		{
			return new NewEntityOp(type, relProperties, false, null);
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x0008F695 File Offset: 0x0008D895
		internal virtual DiscriminatedNewEntityOp CreateDiscriminatedNewEntityOp(TypeUsage type, ExplicitDiscriminatorMap discriminatorMap, EntitySet entitySet, List<RelProperty> relProperties)
		{
			return new DiscriminatedNewEntityOp(type, discriminatorMap, entitySet, relProperties);
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x0008F6A1 File Offset: 0x0008D8A1
		internal virtual NewMultisetOp CreateNewMultisetOp(TypeUsage type)
		{
			return new NewMultisetOp(type);
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x0008F6A9 File Offset: 0x0008D8A9
		internal virtual NewRecordOp CreateNewRecordOp(TypeUsage type)
		{
			return new NewRecordOp(type);
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x0008F6B1 File Offset: 0x0008D8B1
		internal virtual NewRecordOp CreateNewRecordOp(RowType type)
		{
			return new NewRecordOp(TypeUsage.Create(type));
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x0008F6BE File Offset: 0x0008D8BE
		internal virtual NewRecordOp CreateNewRecordOp(TypeUsage type, List<EdmProperty> fields)
		{
			return new NewRecordOp(type, fields);
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x0008F6C7 File Offset: 0x0008D8C7
		internal virtual VarRefOp CreateVarRefOp(Var v)
		{
			return new VarRefOp(v);
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x0008F6CF File Offset: 0x0008D8CF
		internal virtual ArithmeticOp CreateArithmeticOp(OpType opType, TypeUsage type)
		{
			return new ArithmeticOp(opType, type);
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x0008F6D8 File Offset: 0x0008D8D8
		internal PropertyOp CreatePropertyOp(EdmMember prop)
		{
			NavigationProperty navigationProperty = prop as NavigationProperty;
			if (navigationProperty != null)
			{
				RelProperty relProperty = new RelProperty(navigationProperty.RelationshipType, navigationProperty.FromEndMember, navigationProperty.ToEndMember);
				this.AddRelPropertyReference(relProperty);
				RelProperty relProperty2 = new RelProperty(navigationProperty.RelationshipType, navigationProperty.ToEndMember, navigationProperty.FromEndMember);
				this.AddRelPropertyReference(relProperty2);
			}
			return new PropertyOp(Helper.GetModelTypeUsage(prop), prop);
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x0008F739 File Offset: 0x0008D939
		internal RelPropertyOp CreateRelPropertyOp(RelProperty prop)
		{
			this.AddRelPropertyReference(prop);
			return new RelPropertyOp(prop.ToEnd.TypeUsage, prop);
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x0008F753 File Offset: 0x0008D953
		internal virtual RefOp CreateRefOp(EntitySet entitySet, TypeUsage type)
		{
			return new RefOp(entitySet, type);
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x0008F75C File Offset: 0x0008D95C
		internal ExistsOp CreateExistsOp()
		{
			return new ExistsOp(this.BooleanType);
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x0008F769 File Offset: 0x0008D969
		internal virtual ElementOp CreateElementOp(TypeUsage type)
		{
			return new ElementOp(type);
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x0008F771 File Offset: 0x0008D971
		internal virtual GetEntityRefOp CreateGetEntityRefOp(TypeUsage type)
		{
			return new GetEntityRefOp(type);
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x0008F779 File Offset: 0x0008D979
		internal virtual GetRefKeyOp CreateGetRefKeyOp(TypeUsage type)
		{
			return new GetRefKeyOp(type);
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x0008F781 File Offset: 0x0008D981
		internal virtual CollectOp CreateCollectOp(TypeUsage type)
		{
			return new CollectOp(type);
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x0008F789 File Offset: 0x0008D989
		internal virtual DerefOp CreateDerefOp(TypeUsage type)
		{
			return new DerefOp(type);
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x0008F791 File Offset: 0x0008D991
		internal NavigateOp CreateNavigateOp(TypeUsage type, RelProperty relProperty)
		{
			this.AddRelPropertyReference(relProperty);
			return new NavigateOp(type, relProperty);
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x0008F7A1 File Offset: 0x0008D9A1
		internal virtual VarDefListOp CreateVarDefListOp()
		{
			return VarDefListOp.Instance;
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x0008F7A8 File Offset: 0x0008D9A8
		internal virtual VarDefOp CreateVarDefOp(Var v)
		{
			return new VarDefOp(v);
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x0008F7B0 File Offset: 0x0008D9B0
		internal Node CreateVarDefNode(Node definingExpr, out Var computedVar)
		{
			ScalarOp scalarOp = definingExpr.Op as ScalarOp;
			computedVar = this.CreateComputedVar(scalarOp.Type);
			VarDefOp varDefOp = this.CreateVarDefOp(computedVar);
			return this.CreateNode(varDefOp, definingExpr);
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x0008F7E8 File Offset: 0x0008D9E8
		internal Node CreateVarDefListNode(Node definingExpr, out Var computedVar)
		{
			Node node = this.CreateVarDefNode(definingExpr, out computedVar);
			VarDefListOp varDefListOp = this.CreateVarDefListOp();
			return this.CreateNode(varDefListOp, node);
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x0008F810 File Offset: 0x0008DA10
		internal ScanTableOp CreateScanTableOp(TableMD tableMetadata)
		{
			Table table = this.CreateTableInstance(tableMetadata);
			return this.CreateScanTableOp(table);
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x0008F82C File Offset: 0x0008DA2C
		internal virtual ScanTableOp CreateScanTableOp(Table table)
		{
			return new ScanTableOp(table);
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x0008F834 File Offset: 0x0008DA34
		internal virtual ScanViewOp CreateScanViewOp(Table table)
		{
			return new ScanViewOp(table);
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x0008F83C File Offset: 0x0008DA3C
		internal virtual ScanViewOp CreateScanViewOp(TableMD tableMetadata)
		{
			Table table = this.CreateTableInstance(tableMetadata);
			return this.CreateScanViewOp(table);
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x0008F858 File Offset: 0x0008DA58
		internal virtual UnnestOp CreateUnnestOp(Var v)
		{
			Table table = this.CreateTableInstance(Command.CreateTableDefinition(TypeHelpers.GetEdmType<CollectionType>(v.Type).TypeUsage));
			return this.CreateUnnestOp(v, table);
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x0008F889 File Offset: 0x0008DA89
		internal virtual UnnestOp CreateUnnestOp(Var v, Table t)
		{
			return new UnnestOp(v, t);
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x0008F892 File Offset: 0x0008DA92
		internal virtual FilterOp CreateFilterOp()
		{
			return FilterOp.Instance;
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x0008F899 File Offset: 0x0008DA99
		internal virtual ProjectOp CreateProjectOp(VarVec vars)
		{
			return new ProjectOp(vars);
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x0008F8A1 File Offset: 0x0008DAA1
		internal virtual ProjectOp CreateProjectOp(Var v)
		{
			VarVec varVec = this.CreateVarVec();
			varVec.Set(v);
			return new ProjectOp(varVec);
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x0008F8B5 File Offset: 0x0008DAB5
		internal virtual InnerJoinOp CreateInnerJoinOp()
		{
			return InnerJoinOp.Instance;
		}

		// Token: 0x06002C7E RID: 11390 RVA: 0x0008F8BC File Offset: 0x0008DABC
		internal virtual LeftOuterJoinOp CreateLeftOuterJoinOp()
		{
			return LeftOuterJoinOp.Instance;
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x0008F8C3 File Offset: 0x0008DAC3
		internal virtual FullOuterJoinOp CreateFullOuterJoinOp()
		{
			return FullOuterJoinOp.Instance;
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x0008F8CA File Offset: 0x0008DACA
		internal virtual CrossJoinOp CreateCrossJoinOp()
		{
			return CrossJoinOp.Instance;
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x0008F8D1 File Offset: 0x0008DAD1
		internal virtual CrossApplyOp CreateCrossApplyOp()
		{
			return CrossApplyOp.Instance;
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x0008F8D8 File Offset: 0x0008DAD8
		internal virtual OuterApplyOp CreateOuterApplyOp()
		{
			return OuterApplyOp.Instance;
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x0008F8DF File Offset: 0x0008DADF
		internal static SortKey CreateSortKey(Var v, bool asc, string collation)
		{
			return new SortKey(v, asc, collation);
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x0008F8E9 File Offset: 0x0008DAE9
		internal static SortKey CreateSortKey(Var v, bool asc)
		{
			return new SortKey(v, asc, "");
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x0008F8F7 File Offset: 0x0008DAF7
		internal static SortKey CreateSortKey(Var v)
		{
			return new SortKey(v, true, "");
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x0008F905 File Offset: 0x0008DB05
		internal virtual SortOp CreateSortOp(List<SortKey> sortKeys)
		{
			return new SortOp(sortKeys);
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x0008F90D File Offset: 0x0008DB0D
		internal virtual ConstrainedSortOp CreateConstrainedSortOp(List<SortKey> sortKeys)
		{
			return new ConstrainedSortOp(sortKeys, false);
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x0008F916 File Offset: 0x0008DB16
		internal virtual ConstrainedSortOp CreateConstrainedSortOp(List<SortKey> sortKeys, bool withTies)
		{
			return new ConstrainedSortOp(sortKeys, withTies);
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x0008F91F File Offset: 0x0008DB1F
		internal virtual GroupByOp CreateGroupByOp(VarVec gbyKeys, VarVec outputs)
		{
			return new GroupByOp(gbyKeys, outputs);
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x0008F928 File Offset: 0x0008DB28
		internal virtual GroupByIntoOp CreateGroupByIntoOp(VarVec gbyKeys, VarVec inputs, VarVec outputs)
		{
			return new GroupByIntoOp(gbyKeys, inputs, outputs);
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x0008F932 File Offset: 0x0008DB32
		internal virtual DistinctOp CreateDistinctOp(VarVec keyVars)
		{
			return new DistinctOp(keyVars);
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x0008F93A File Offset: 0x0008DB3A
		internal virtual DistinctOp CreateDistinctOp(Var keyVar)
		{
			return new DistinctOp(this.CreateVarVec(keyVar));
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x0008F948 File Offset: 0x0008DB48
		internal virtual UnionAllOp CreateUnionAllOp(VarMap leftMap, VarMap rightMap)
		{
			return this.CreateUnionAllOp(leftMap, rightMap, null);
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x0008F954 File Offset: 0x0008DB54
		internal virtual UnionAllOp CreateUnionAllOp(VarMap leftMap, VarMap rightMap, Var branchDiscriminator)
		{
			VarVec varVec = this.CreateVarVec();
			foreach (Var var in leftMap.Keys)
			{
				varVec.Set(var);
			}
			return new UnionAllOp(varVec, leftMap, rightMap, branchDiscriminator);
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x0008F9B8 File Offset: 0x0008DBB8
		internal virtual IntersectOp CreateIntersectOp(VarMap leftMap, VarMap rightMap)
		{
			VarVec varVec = this.CreateVarVec();
			foreach (Var var in leftMap.Keys)
			{
				varVec.Set(var);
			}
			return new IntersectOp(varVec, leftMap, rightMap);
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x0008FA1C File Offset: 0x0008DC1C
		internal virtual ExceptOp CreateExceptOp(VarMap leftMap, VarMap rightMap)
		{
			VarVec varVec = this.CreateVarVec();
			foreach (Var var in leftMap.Keys)
			{
				varVec.Set(var);
			}
			return new ExceptOp(varVec, leftMap, rightMap);
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x0008FA80 File Offset: 0x0008DC80
		internal virtual SingleRowOp CreateSingleRowOp()
		{
			return SingleRowOp.Instance;
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x0008FA87 File Offset: 0x0008DC87
		internal virtual SingleRowTableOp CreateSingleRowTableOp()
		{
			return SingleRowTableOp.Instance;
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x0008FA8E File Offset: 0x0008DC8E
		internal virtual PhysicalProjectOp CreatePhysicalProjectOp(VarList outputVars, SimpleCollectionColumnMap columnMap)
		{
			return new PhysicalProjectOp(outputVars, columnMap);
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x0008FA98 File Offset: 0x0008DC98
		internal virtual PhysicalProjectOp CreatePhysicalProjectOp(Var outputVar)
		{
			VarList varList = Command.CreateVarList();
			varList.Add(outputVar);
			VarRefColumnMap varRefColumnMap = new VarRefColumnMap(outputVar);
			SimpleCollectionColumnMap simpleCollectionColumnMap = new SimpleCollectionColumnMap(TypeUtils.CreateCollectionType(varRefColumnMap.Type), null, varRefColumnMap, new SimpleColumnMap[0], new SimpleColumnMap[0]);
			return this.CreatePhysicalProjectOp(varList, simpleCollectionColumnMap);
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x0008FAE0 File Offset: 0x0008DCE0
		internal static CollectionInfo CreateCollectionInfo(Var collectionVar, ColumnMap columnMap, VarList flattenedElementVars, VarVec keys, List<SortKey> sortKeys, object discriminatorValue)
		{
			return new CollectionInfo(collectionVar, columnMap, flattenedElementVars, keys, sortKeys, discriminatorValue);
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x0008FAEF File Offset: 0x0008DCEF
		internal virtual SingleStreamNestOp CreateSingleStreamNestOp(VarVec keys, List<SortKey> prefixSortKeys, List<SortKey> postfixSortKeys, VarVec outputVars, List<CollectionInfo> collectionInfoList, Var discriminatorVar)
		{
			return new SingleStreamNestOp(keys, prefixSortKeys, postfixSortKeys, outputVars, collectionInfoList, discriminatorVar);
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x0008FAFF File Offset: 0x0008DCFF
		internal virtual MultiStreamNestOp CreateMultiStreamNestOp(List<SortKey> prefixSortKeys, VarVec outputVars, List<CollectionInfo> collectionInfoList)
		{
			return new MultiStreamNestOp(prefixSortKeys, outputVars, collectionInfoList);
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x0008FB09 File Offset: 0x0008DD09
		internal virtual NodeInfo GetNodeInfo(Node n)
		{
			return n.GetNodeInfo(this);
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x0008FB12 File Offset: 0x0008DD12
		internal virtual ExtendedNodeInfo GetExtendedNodeInfo(Node n)
		{
			return n.GetExtendedNodeInfo(this);
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x0008FB1B File Offset: 0x0008DD1B
		internal virtual void RecomputeNodeInfo(Node n)
		{
			this.m_nodeInfoVisitor.RecomputeNodeInfo(n);
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x0008FB29 File Offset: 0x0008DD29
		internal virtual KeyVec PullupKeys(Node n)
		{
			return this.m_keyPullupVisitor.GetKeys(n);
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x0008FB37 File Offset: 0x0008DD37
		internal static bool EqualTypes(TypeUsage x, TypeUsage y)
		{
			return TypeUsageEqualityComparer.Instance.Equals(x, y);
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x0008FB45 File Offset: 0x0008DD45
		internal static bool EqualTypes(EdmType x, EdmType y)
		{
			return TypeUsageEqualityComparer.Equals(x, y);
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x0008FB50 File Offset: 0x0008DD50
		internal virtual void BuildUnionAllLadder(IList<Node> inputNodes, IList<Var> inputVars, out Node resultNode, out IList<Var> resultVars)
		{
			if (inputNodes.Count == 0)
			{
				resultNode = null;
				resultVars = null;
				return;
			}
			int num = inputVars.Count / inputNodes.Count;
			if (inputNodes.Count == 1)
			{
				resultNode = inputNodes[0];
				resultVars = inputVars;
				return;
			}
			List<Var> list = new List<Var>();
			Node node = inputNodes[0];
			for (int i = 0; i < num; i++)
			{
				list.Add(inputVars[i]);
			}
			for (int j = 1; j < inputNodes.Count; j++)
			{
				VarMap varMap = new VarMap();
				VarMap varMap2 = new VarMap();
				List<Var> list2 = new List<Var>();
				for (int k = 0; k < num; k++)
				{
					SetOpVar setOpVar = this.CreateSetOpVar(list[k].Type);
					list2.Add(setOpVar);
					varMap.Add(setOpVar, list[k]);
					varMap2.Add(setOpVar, inputVars[j * num + k]);
				}
				Op op = this.CreateUnionAllOp(varMap, varMap2);
				node = this.CreateNode(op, node, inputNodes[j]);
				list = list2;
			}
			resultNode = node;
			resultVars = list;
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x0008FC68 File Offset: 0x0008DE68
		internal virtual void BuildUnionAllLadder(IList<Node> inputNodes, IList<Var> inputVars, out Node resultNode, out Var resultVar)
		{
			IList<Var> list;
			this.BuildUnionAllLadder(inputNodes, inputVars, out resultNode, out list);
			if (list != null && list.Count > 0)
			{
				resultVar = list[0];
				return;
			}
			resultVar = null;
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x0008FC9C File Offset: 0x0008DE9C
		internal virtual Node BuildProject(Node inputNode, IEnumerable<Var> inputVars, IEnumerable<Node> computedExpressions)
		{
			VarDefListOp varDefListOp = this.CreateVarDefListOp();
			Node node = this.CreateNode(varDefListOp);
			VarVec varVec = this.CreateVarVec(inputVars);
			foreach (Node node2 in computedExpressions)
			{
				Var var = this.CreateComputedVar(node2.Op.Type);
				varVec.Set(var);
				VarDefOp varDefOp = this.CreateVarDefOp(var);
				Node node3 = this.CreateNode(varDefOp, node2);
				node.Children.Add(node3);
			}
			return this.CreateNode(this.CreateProjectOp(varVec), inputNode, node);
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x0008FD44 File Offset: 0x0008DF44
		internal virtual Node BuildProject(Node input, Node computedExpression, out Var projectVar)
		{
			Node node = this.BuildProject(input, new Var[0], new Node[] { computedExpression });
			projectVar = ((ProjectOp)node.Op).Outputs.First;
			return node;
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x0008FD84 File Offset: 0x0008DF84
		internal virtual void BuildOfTypeTree(Node inputNode, Var inputVar, TypeUsage desiredType, bool includeSubtypes, out Node resultNode, out Var resultVar)
		{
			Op op = (includeSubtypes ? this.CreateIsOfOp(desiredType) : this.CreateIsOfOnlyOp(desiredType));
			Node node = this.CreateNode(op, this.CreateNode(this.CreateVarRefOp(inputVar)));
			Node node2 = this.CreateNode(this.CreateFilterOp(), inputNode, node);
			resultNode = this.BuildFakeTreatProject(node2, inputVar, desiredType, out resultVar);
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x0008FDD8 File Offset: 0x0008DFD8
		internal virtual Node BuildFakeTreatProject(Node inputNode, Var inputVar, TypeUsage desiredType, out Var resultVar)
		{
			Node node = this.CreateNode(this.CreateFakeTreatOp(desiredType), this.CreateNode(this.CreateVarRefOp(inputVar)));
			return this.BuildProject(inputNode, node, out resultVar);
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x0008FE0C File Offset: 0x0008E00C
		internal Node BuildComparison(OpType opType, Node arg0, Node arg1, bool useDatabaseNullSemantics = false)
		{
			if (!Command.EqualTypes(arg0.Op.Type, arg1.Op.Type))
			{
				TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(arg0.Op.Type, arg1.Op.Type);
				if (!Command.EqualTypes(commonTypeUsage, arg0.Op.Type))
				{
					arg0 = this.CreateNode(this.CreateSoftCastOp(commonTypeUsage), arg0);
				}
				if (!Command.EqualTypes(commonTypeUsage, arg1.Op.Type))
				{
					arg1 = this.CreateNode(this.CreateSoftCastOp(commonTypeUsage), arg1);
				}
			}
			return this.CreateNode(this.CreateComparisonOp(opType, useDatabaseNullSemantics), arg0, arg1);
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x0008FEAC File Offset: 0x0008E0AC
		internal virtual Node BuildCollect(Node relOpNode, Var relOpVar)
		{
			Node node = this.CreateNode(this.CreatePhysicalProjectOp(relOpVar), relOpNode);
			TypeUsage typeUsage = TypeHelpers.CreateCollectionTypeUsage(relOpVar.Type);
			return this.CreateNode(this.CreateCollectOp(typeUsage), node);
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x0008FEE2 File Offset: 0x0008E0E2
		private void AddRelPropertyReference(RelProperty relProperty)
		{
			if (relProperty.ToEnd.RelationshipMultiplicity != RelationshipMultiplicity.Many && !this.m_referencedRelProperties.Contains(relProperty))
			{
				this.m_referencedRelProperties.Add(relProperty);
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06002CA7 RID: 11431 RVA: 0x0008FF0D File Offset: 0x0008E10D
		internal virtual HashSet<RelProperty> ReferencedRelProperties
		{
			get
			{
				return this.m_referencedRelProperties;
			}
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x0008FF15 File Offset: 0x0008E115
		internal virtual bool IsRelPropertyReferenced(RelProperty relProperty)
		{
			return this.m_referencedRelProperties.Contains(relProperty);
		}

		// Token: 0x04000EF0 RID: 3824
		private readonly Dictionary<string, ParameterVar> m_parameterMap;

		// Token: 0x04000EF1 RID: 3825
		private readonly List<Var> m_vars;

		// Token: 0x04000EF2 RID: 3826
		private readonly List<Table> m_tables;

		// Token: 0x04000EF3 RID: 3827
		private readonly MetadataWorkspace m_metadataWorkspace;

		// Token: 0x04000EF4 RID: 3828
		private readonly TypeUsage m_boolType;

		// Token: 0x04000EF5 RID: 3829
		private readonly TypeUsage m_intType;

		// Token: 0x04000EF6 RID: 3830
		private readonly TypeUsage m_stringType;

		// Token: 0x04000EF7 RID: 3831
		private readonly ConstantPredicateOp m_trueOp;

		// Token: 0x04000EF8 RID: 3832
		private readonly ConstantPredicateOp m_falseOp;

		// Token: 0x04000EF9 RID: 3833
		private readonly NodeInfoVisitor m_nodeInfoVisitor;

		// Token: 0x04000EFA RID: 3834
		private readonly KeyPullup m_keyPullupVisitor;

		// Token: 0x04000EFB RID: 3835
		private int m_nextNodeId;

		// Token: 0x04000EFC RID: 3836
		private int m_nextBranchDiscriminatorValue = 1000;

		// Token: 0x04000EFD RID: 3837
		private bool m_disableVarVecEnumCaching;

		// Token: 0x04000EFE RID: 3838
		private readonly Stack<VarVec.VarVecEnumerator> m_freeVarVecEnumerators;

		// Token: 0x04000EFF RID: 3839
		private readonly Stack<VarVec> m_freeVarVecs;

		// Token: 0x04000F00 RID: 3840
		private readonly HashSet<RelProperty> m_referencedRelProperties;
	}
}
