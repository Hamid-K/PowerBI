using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common.QueryCache;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x02000642 RID: 1602
	internal class Translator
	{
		// Token: 0x06004D18 RID: 19736 RVA: 0x00110540 File Offset: 0x0010E740
		internal virtual ShaperFactory<T> TranslateColumnMap<T>(ColumnMap columnMap, MetadataWorkspace workspace, SpanIndex spanIndex, MergeOption mergeOption, bool streaming, bool valueLayer)
		{
			ShaperFactoryQueryCacheKey<T> shaperFactoryQueryCacheKey = new ShaperFactoryQueryCacheKey<T>(ColumnMapKeyBuilder.GetColumnMapKey(columnMap, spanIndex), mergeOption, streaming, valueLayer);
			QueryCacheManager queryCacheManager = workspace.GetQueryCacheManager();
			ShaperFactory<T> shaperFactory;
			if (queryCacheManager.TryCacheLookup<ShaperFactoryQueryCacheKey<T>, ShaperFactory<T>>(shaperFactoryQueryCacheKey, out shaperFactory))
			{
				return shaperFactory;
			}
			Translator.TranslatorVisitor translatorVisitor = new Translator.TranslatorVisitor(workspace, spanIndex, mergeOption, streaming, valueLayer);
			columnMap.Accept<TranslatorResult, TranslatorArg>(translatorVisitor, new TranslatorArg(typeof(IEnumerable<>).MakeGenericType(new Type[] { typeof(T) })));
			CoordinatorFactory<T> coordinatorFactory = (CoordinatorFactory<T>)translatorVisitor.RootCoordinatorScratchpad.Compile();
			Type[] array = null;
			bool[] array2 = null;
			if (!streaming)
			{
				int num = Math.Max(translatorVisitor.ColumnTypes.Any<KeyValuePair<int, Type>>() ? translatorVisitor.ColumnTypes.Keys.Max() : 0, translatorVisitor.NullableColumns.Any<int>() ? translatorVisitor.NullableColumns.Max() : 0);
				array = new Type[num + 1];
				foreach (KeyValuePair<int, Type> keyValuePair in translatorVisitor.ColumnTypes)
				{
					array[keyValuePair.Key] = keyValuePair.Value;
				}
				array2 = new bool[num + 1];
				foreach (int num2 in translatorVisitor.NullableColumns)
				{
					array2[num2] = true;
				}
			}
			shaperFactory = new ShaperFactory<T>(translatorVisitor.StateSlotCount, coordinatorFactory, array, array2, mergeOption);
			QueryCacheEntry queryCacheEntry = new QueryCacheEntry(shaperFactoryQueryCacheKey, shaperFactory);
			if (queryCacheManager.TryLookupAndAdd(queryCacheEntry, out queryCacheEntry))
			{
				shaperFactory = (ShaperFactory<T>)queryCacheEntry.GetTarget();
			}
			return shaperFactory;
		}

		// Token: 0x06004D19 RID: 19737 RVA: 0x001106F4 File Offset: 0x0010E8F4
		internal static ShaperFactory TranslateColumnMap(Translator translator, Type elementType, ColumnMap columnMap, MetadataWorkspace workspace, SpanIndex spanIndex, MergeOption mergeOption, bool streaming, bool valueLayer)
		{
			return (ShaperFactory)Translator.GenericTranslateColumnMap.MakeGenericMethod(new Type[] { elementType }).Invoke(translator, new object[] { columnMap, workspace, spanIndex, mergeOption, streaming, valueLayer });
		}

		// Token: 0x04001B65 RID: 7013
		public static readonly MethodInfo GenericTranslateColumnMap = typeof(Translator).GetDeclaredMethod("TranslateColumnMap", new Type[]
		{
			typeof(ColumnMap),
			typeof(MetadataWorkspace),
			typeof(SpanIndex),
			typeof(MergeOption),
			typeof(bool),
			typeof(bool)
		});

		// Token: 0x02000C68 RID: 3176
		internal class TranslatorVisitor : ColumnMapVisitorWithResults<TranslatorResult, TranslatorArg>
		{
			// Token: 0x06006AEB RID: 27371 RVA: 0x0016CF8C File Offset: 0x0016B18C
			public TranslatorVisitor(MetadataWorkspace workspace, SpanIndex spanIndex, MergeOption mergeOption, bool streaming, bool valueLayer)
			{
				this._workspace = workspace;
				this._spanIndex = spanIndex;
				this._mergeOption = mergeOption;
				this._streaming = streaming;
				this.ColumnTypes = new Dictionary<int, Type>();
				this.NullableColumns = new Set<int>();
				this.IsValueLayer = valueLayer;
			}

			// Token: 0x17001184 RID: 4484
			// (get) Token: 0x06006AEC RID: 27372 RVA: 0x0016CFE5 File Offset: 0x0016B1E5
			// (set) Token: 0x06006AED RID: 27373 RVA: 0x0016CFED File Offset: 0x0016B1ED
			public CoordinatorScratchpad RootCoordinatorScratchpad { get; private set; }

			// Token: 0x17001185 RID: 4485
			// (get) Token: 0x06006AEE RID: 27374 RVA: 0x0016CFF6 File Offset: 0x0016B1F6
			// (set) Token: 0x06006AEF RID: 27375 RVA: 0x0016CFFE File Offset: 0x0016B1FE
			public int StateSlotCount { get; private set; }

			// Token: 0x17001186 RID: 4486
			// (get) Token: 0x06006AF0 RID: 27376 RVA: 0x0016D007 File Offset: 0x0016B207
			// (set) Token: 0x06006AF1 RID: 27377 RVA: 0x0016D00F File Offset: 0x0016B20F
			public Dictionary<int, Type> ColumnTypes { get; private set; }

			// Token: 0x17001187 RID: 4487
			// (get) Token: 0x06006AF2 RID: 27378 RVA: 0x0016D018 File Offset: 0x0016B218
			// (set) Token: 0x06006AF3 RID: 27379 RVA: 0x0016D020 File Offset: 0x0016B220
			public Set<int> NullableColumns { get; private set; }

			// Token: 0x06006AF4 RID: 27380 RVA: 0x0016D02C File Offset: 0x0016B22C
			private static TranslatorResult AcceptWithMappedType(Translator.TranslatorVisitor translatorVisitor, ColumnMap columnMap)
			{
				Type type = translatorVisitor.DetermineClrType(columnMap.Type);
				return columnMap.Accept<TranslatorResult, TranslatorArg>(translatorVisitor, new TranslatorArg(type));
			}

			// Token: 0x06006AF5 RID: 27381 RVA: 0x0016D054 File Offset: 0x0016B254
			internal override TranslatorResult Visit(ComplexTypeColumnMap columnMap, TranslatorArg arg)
			{
				Expression expression = null;
				bool inNullableType = this._inNullableType;
				if (columnMap.NullSentinel != null)
				{
					expression = CodeGenEmitter.Emit_Reader_IsDBNull(columnMap.NullSentinel);
					this._inNullableType = true;
					int columnPos = ((ScalarColumnMap)columnMap.NullSentinel).ColumnPos;
					if (!this._streaming && !this.NullableColumns.Contains(columnPos))
					{
						this.NullableColumns.Add(columnPos);
					}
				}
				Expression expression2;
				if (this.IsValueLayer)
				{
					expression2 = this.BuildExpressionToGetRecordState(columnMap, null, null, expression);
				}
				else
				{
					ComplexType complexType = (ComplexType)columnMap.Type.EdmType;
					ConstructorInfo constructorForType = DelegateFactory.GetConstructorForType(this.DetermineClrType(complexType));
					List<MemberBinding> list = this.CreatePropertyBindings(columnMap, complexType.Properties);
					expression2 = Expression.MemberInit(Expression.New(constructorForType), list);
					if (expression != null)
					{
						expression2 = Expression.Condition(expression, CodeGenEmitter.Emit_NullConstant(expression2.Type), expression2);
					}
				}
				this._inNullableType = inNullableType;
				return new TranslatorResult(expression2, arg.RequestedType);
			}

			// Token: 0x06006AF6 RID: 27382 RVA: 0x0016D134 File Offset: 0x0016B334
			internal override TranslatorResult Visit(EntityColumnMap columnMap, TranslatorArg arg)
			{
				EntityIdentity entityIdentity = columnMap.EntityIdentity;
				Expression expression = null;
				Expression expression2 = this.Emit_EntityKey_ctor(this, entityIdentity, columnMap.Type.EdmType, false, out expression);
				Expression expression4;
				if (this.IsValueLayer)
				{
					Expression expression3 = Expression.Not(CodeGenEmitter.Emit_EntityKey_HasValue(entityIdentity.Keys));
					expression4 = this.BuildExpressionToGetRecordState(columnMap, expression2, expression, expression3);
				}
				else
				{
					EntityType entityType = (EntityType)columnMap.Type.EdmType;
					ClrEntityType clrEntityType = (ClrEntityType)this.LookupObjectMapping(entityType).ClrType;
					Type clrType = clrEntityType.ClrType;
					List<MemberBinding> list = this.CreatePropertyBindings(columnMap, entityType.Properties);
					EntityProxyTypeInfo proxyType = EntityProxyFactory.GetProxyType(clrEntityType, this._workspace);
					Expression expression5 = this.Emit_ConstructEntity(clrEntityType, list, expression2, expression, arg, null);
					Expression expression6;
					if (proxyType == null)
					{
						expression6 = expression5;
					}
					else
					{
						Expression expression7 = this.Emit_ConstructEntity(clrEntityType, list, expression2, expression, arg, proxyType);
						expression6 = Expression.Condition(CodeGenEmitter.Shaper_ProxyCreationEnabled, expression7, expression5);
					}
					if (MergeOption.NoTracking != this._mergeOption)
					{
						Type type = ((proxyType == null) ? clrType : proxyType.ProxyType);
						if (typeof(IEntityWithKey).IsAssignableFrom(type) && this._mergeOption != MergeOption.AppendOnly)
						{
							expression6 = Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_HandleIEntityWithKey.MakeGenericMethod(new Type[] { clrType }), expression6, expression);
						}
						else if (this._mergeOption == MergeOption.AppendOnly)
						{
							LambdaExpression lambdaExpression = this.CreateInlineDelegate(expression6);
							expression6 = Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_HandleEntityAppendOnly.MakeGenericMethod(new Type[] { clrType }), lambdaExpression, expression2, expression);
						}
						else
						{
							expression6 = Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_HandleEntity.MakeGenericMethod(new Type[] { clrType }), expression6, expression2, expression);
						}
					}
					else
					{
						expression6 = Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_HandleEntityNoTracking.MakeGenericMethod(new Type[] { clrType }), new Expression[] { expression6 });
					}
					expression4 = Expression.Condition(CodeGenEmitter.Emit_EntityKey_HasValue(entityIdentity.Keys), expression6, CodeGenEmitter.Emit_WrappedNullConstant());
				}
				int columnPos = ((ScalarColumnMap)entityIdentity.Keys[0]).ColumnPos;
				if (!this._streaming && !this.NullableColumns.Contains(columnPos))
				{
					this.NullableColumns.Add(columnPos);
				}
				return new TranslatorResult(expression4, arg.RequestedType);
			}

			// Token: 0x06006AF7 RID: 27383 RVA: 0x0016D364 File Offset: 0x0016B564
			private Expression Emit_ConstructEntity(EntityType oSpaceType, IEnumerable<MemberBinding> propertyBindings, Expression entityKeyReader, Expression entitySetReader, TranslatorArg arg, EntityProxyTypeInfo proxyTypeInfo)
			{
				bool flag = proxyTypeInfo != null;
				Type clrType = oSpaceType.ClrType;
				Expression expression;
				Type type;
				if (flag)
				{
					expression = Expression.MemberInit(Expression.New(proxyTypeInfo.ProxyType), propertyBindings);
					type = proxyTypeInfo.ProxyType;
				}
				else
				{
					expression = Expression.MemberInit(Expression.New(DelegateFactory.GetConstructorForType(clrType)), propertyBindings);
					type = clrType;
				}
				expression = CodeGenEmitter.Emit_EnsureTypeAndWrap(expression, entityKeyReader, entitySetReader, arg.RequestedType, clrType, type, (this._mergeOption == MergeOption.NoTracking) ? MergeOption.NoTracking : MergeOption.AppendOnly, flag);
				if (flag)
				{
					expression = Expression.Call(Expression.Constant(proxyTypeInfo), CodeGenEmitter.EntityProxyTypeInfo_SetEntityWrapper, new Expression[] { expression });
					if (proxyTypeInfo.InitializeEntityCollections != null)
					{
						expression = Expression.Call(proxyTypeInfo.InitializeEntityCollections, expression);
					}
				}
				return expression;
			}

			// Token: 0x06006AF8 RID: 27384 RVA: 0x0016D410 File Offset: 0x0016B610
			private List<MemberBinding> CreatePropertyBindings(StructuredColumnMap columnMap, ReadOnlyMetadataCollection<EdmProperty> properties)
			{
				List<MemberBinding> list = new List<MemberBinding>(columnMap.Properties.Length);
				ObjectTypeMapping objectTypeMapping = this.LookupObjectMapping(columnMap.Type.EdmType);
				for (int i = 0; i < columnMap.Properties.Length; i++)
				{
					PropertyInfo propertyInfo = DelegateFactory.ValidateSetterProperty(objectTypeMapping.GetPropertyMap(properties[i].Name).ClrProperty.PropertyInfo);
					MethodInfo methodInfo = propertyInfo.Setter();
					Type propertyType = propertyInfo.PropertyType;
					Expression expression = columnMap.Properties[i].Accept<TranslatorResult, TranslatorArg>(this, new TranslatorArg(propertyType)).Expression;
					ScalarColumnMap scalarColumnMap = columnMap.Properties[i] as ScalarColumnMap;
					if (scalarColumnMap != null)
					{
						string text = methodInfo.Name.Substring(4);
						Expression expression2 = CodeGenEmitter.Emit_Shaper_GetPropertyValueWithErrorHandling(propertyType, scalarColumnMap.ColumnPos, text, methodInfo.DeclaringType.Name, scalarColumnMap.Type);
						this._currentCoordinatorScratchpad.AddExpressionWithErrorHandling(expression, expression2);
					}
					list.Add(Expression.Bind(propertyInfo, expression));
				}
				return list;
			}

			// Token: 0x06006AF9 RID: 27385 RVA: 0x0016D50C File Offset: 0x0016B70C
			internal override TranslatorResult Visit(SimplePolymorphicColumnMap columnMap, TranslatorArg arg)
			{
				Expression expression = Translator.TranslatorVisitor.AcceptWithMappedType(this, columnMap.TypeDiscriminator).Expression;
				Expression expression2;
				if (this.IsValueLayer)
				{
					expression2 = CodeGenEmitter.Emit_EnsureType(this.BuildExpressionToGetRecordState(columnMap, null, null, Expression.Constant(true)), arg.RequestedType);
				}
				else
				{
					expression2 = CodeGenEmitter.Emit_WrappedNullConstant();
				}
				foreach (KeyValuePair<object, TypedColumnMap> keyValuePair in columnMap.TypeChoices)
				{
					if (!this.DetermineClrType(keyValuePair.Value.Type).IsAbstract())
					{
						Expression expression3 = Expression.Constant(keyValuePair.Key, expression.Type);
						Expression expression4;
						if (expression.Type == typeof(string))
						{
							expression4 = Expression.Call(Expression.Constant(TrailingSpaceStringComparer.Instance), CodeGenEmitter.IEqualityComparerOfString_Equals, expression3, expression);
						}
						else
						{
							expression4 = CodeGenEmitter.Emit_Equal(expression3, expression);
						}
						bool inNullableType = this._inNullableType;
						this._inNullableType = true;
						expression2 = Expression.Condition(expression4, keyValuePair.Value.Accept<TranslatorResult, TranslatorArg>(this, arg).Expression, expression2);
						this._inNullableType = inNullableType;
					}
				}
				return new TranslatorResult(expression2, arg.RequestedType);
			}

			// Token: 0x06006AFA RID: 27386 RVA: 0x0016D64C File Offset: 0x0016B84C
			internal override TranslatorResult Visit(MultipleDiscriminatorPolymorphicColumnMap columnMap, TranslatorArg arg)
			{
				return new TranslatorResult((Expression)Translator.TranslatorVisitor.Translator_MultipleDiscriminatorPolymorphicColumnMapHelper.MakeGenericMethod(new Type[] { arg.RequestedType }).Invoke(this, new object[] { columnMap }), arg.RequestedType);
			}

			// Token: 0x06006AFB RID: 27387 RVA: 0x0016D688 File Offset: 0x0016B888
			private Expression MultipleDiscriminatorPolymorphicColumnMapHelper<TElement>(MultipleDiscriminatorPolymorphicColumnMap columnMap)
			{
				Expression[] array = new Expression[columnMap.TypeDiscriminators.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = columnMap.TypeDiscriminators[i].Accept<TranslatorResult, TranslatorArg>(this, new TranslatorArg(typeof(object))).Expression;
				}
				Expression expression = Expression.NewArrayInit(typeof(object), array);
				List<Expression> list = new List<Expression>();
				Type typeFromHandle = typeof(KeyValuePair<EntityType, Func<Shaper, TElement>>);
				ConstructorInfo declaredConstructor = typeFromHandle.GetDeclaredConstructor(new Type[]
				{
					typeof(EntityType),
					typeof(Func<Shaper, TElement>)
				});
				foreach (KeyValuePair<EntityType, TypedColumnMap> keyValuePair in columnMap.TypeChoices)
				{
					Expression expression2 = CodeGenEmitter.Emit_EnsureType(Translator.TranslatorVisitor.AcceptWithMappedType(this, keyValuePair.Value).UnwrappedExpression, typeof(TElement));
					LambdaExpression lambdaExpression = this.CreateInlineDelegate(expression2);
					Expression expression3 = Expression.New(declaredConstructor, new Expression[]
					{
						Expression.Constant(keyValuePair.Key),
						lambdaExpression
					});
					list.Add(expression3);
				}
				MethodInfo methodInfo = CodeGenEmitter.Shaper_Discriminate.MakeGenericMethod(new Type[] { typeof(TElement) });
				return Expression.Call(CodeGenEmitter.Shaper_Parameter, methodInfo, expression, Expression.Constant(columnMap.Discriminate), Expression.NewArrayInit(typeFromHandle, list));
			}

			// Token: 0x06006AFC RID: 27388 RVA: 0x0016D800 File Offset: 0x0016BA00
			internal override TranslatorResult Visit(RecordColumnMap columnMap, TranslatorArg arg)
			{
				Expression expression = null;
				bool inNullableType = this._inNullableType;
				if (columnMap.NullSentinel != null)
				{
					expression = CodeGenEmitter.Emit_Reader_IsDBNull(columnMap.NullSentinel);
					this._inNullableType = true;
					int columnPos = ((ScalarColumnMap)columnMap.NullSentinel).ColumnPos;
					if (!this._streaming && !this.NullableColumns.Contains(columnPos))
					{
						this.NullableColumns.Add(columnPos);
					}
				}
				Expression expression2;
				if (this.IsValueLayer)
				{
					expression2 = this.BuildExpressionToGetRecordState(columnMap, null, null, expression);
				}
				else
				{
					InitializerMetadata initializerMetadata;
					Expression expression3;
					if (InitializerMetadata.TryGetInitializerMetadata(columnMap.Type, out initializerMetadata))
					{
						expression2 = this.HandleLinqRecord(columnMap, initializerMetadata);
						expression3 = CodeGenEmitter.Emit_NullConstant(expression2.Type);
					}
					else
					{
						RowType rowType = (RowType)columnMap.Type.EdmType;
						if (this._spanIndex != null && this._spanIndex.HasSpanMap(rowType))
						{
							expression2 = this.HandleSpandexRecord(columnMap, arg, rowType);
							expression3 = CodeGenEmitter.Emit_WrappedNullConstant();
						}
						else
						{
							expression2 = this.HandleRegularRecord(columnMap, arg, rowType);
							expression3 = CodeGenEmitter.Emit_NullConstant(expression2.Type);
						}
					}
					if (expression != null)
					{
						expression2 = Expression.Condition(expression, expression3, expression2);
					}
				}
				this._inNullableType = inNullableType;
				return new TranslatorResult(expression2, arg.RequestedType);
			}

			// Token: 0x06006AFD RID: 27389 RVA: 0x0016D91C File Offset: 0x0016BB1C
			private Expression BuildExpressionToGetRecordState(StructuredColumnMap columnMap, Expression entityKeyReader, Expression entitySetReader, Expression nullCheckExpression)
			{
				RecordStateScratchpad recordStateScratchpad = this._currentCoordinatorScratchpad.CreateRecordStateScratchpad();
				int num = this.AllocateStateSlot();
				recordStateScratchpad.StateSlotNumber = num;
				int num2 = columnMap.Properties.Length;
				int num3 = ((entityKeyReader != null) ? (num2 + 1) : num2);
				recordStateScratchpad.ColumnCount = num2;
				EntityType entityType = null;
				if (TypeHelpers.TryGetEdmType<EntityType>(columnMap.Type, out entityType))
				{
					recordStateScratchpad.DataRecordInfo = new EntityRecordInfo(entityType, EntityKey.EntityNotValidKey, null);
				}
				else
				{
					TypeUsage modelTypeUsage = Helper.GetModelTypeUsage(columnMap.Type);
					recordStateScratchpad.DataRecordInfo = new DataRecordInfo(modelTypeUsage);
				}
				Expression[] array = new Expression[num3];
				string[] array2 = new string[recordStateScratchpad.ColumnCount];
				TypeUsage[] array3 = new TypeUsage[recordStateScratchpad.ColumnCount];
				for (int i = 0; i < num2; i++)
				{
					Expression expression = columnMap.Properties[i].Accept<TranslatorResult, TranslatorArg>(this, new TranslatorArg(typeof(object))).Expression;
					array[i] = Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_SetColumnValue, Expression.Constant(num), Expression.Constant(i), Expression.Coalesce(expression, CodeGenEmitter.DBNull_Value));
					array2[i] = columnMap.Properties[i].Name;
					array3[i] = columnMap.Properties[i].Type;
				}
				if (entityKeyReader != null)
				{
					array[num3 - 1] = Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_SetEntityRecordInfo, Expression.Constant(num), entityKeyReader, entitySetReader);
				}
				recordStateScratchpad.GatherData = CodeGenEmitter.Emit_BitwiseOr(array);
				recordStateScratchpad.PropertyNames = array2;
				recordStateScratchpad.TypeUsages = array3;
				Expression expression2 = Expression.Call(CodeGenEmitter.Emit_Shaper_GetState(num, typeof(RecordState)), CodeGenEmitter.RecordState_GatherData, new Expression[] { CodeGenEmitter.Shaper_Parameter });
				if (nullCheckExpression != null)
				{
					Expression expression3 = Expression.Call(CodeGenEmitter.Emit_Shaper_GetState(num, typeof(RecordState)), CodeGenEmitter.RecordState_SetNullRecord);
					expression2 = Expression.Condition(nullCheckExpression, expression3, expression2);
				}
				return expression2;
			}

			// Token: 0x06006AFE RID: 27390 RVA: 0x0016DAF4 File Offset: 0x0016BCF4
			private Expression HandleLinqRecord(RecordColumnMap columnMap, InitializerMetadata initializerMetadata)
			{
				List<TranslatorResult> list = new List<TranslatorResult>(columnMap.Properties.Length);
				foreach (KeyValuePair<ColumnMap, Type> keyValuePair in columnMap.Properties.Zip(initializerMetadata.GetChildTypes()))
				{
					ColumnMap key = keyValuePair.Key;
					Type type = keyValuePair.Value;
					if (null == type)
					{
						type = this.DetermineClrType(key.Type);
					}
					TranslatorResult translatorResult = key.Accept<TranslatorResult, TranslatorArg>(this, new TranslatorArg(type));
					list.Add(translatorResult);
				}
				return initializerMetadata.Emit(list);
			}

			// Token: 0x06006AFF RID: 27391 RVA: 0x0016DB9C File Offset: 0x0016BD9C
			private Expression HandleRegularRecord(RecordColumnMap columnMap, TranslatorArg arg, RowType spanRowType)
			{
				Expression[] array = new Expression[columnMap.Properties.Length];
				for (int i = 0; i < array.Length; i++)
				{
					Expression unwrappedExpression = Translator.TranslatorVisitor.AcceptWithMappedType(this, columnMap.Properties[i]).UnwrappedExpression;
					array[i] = Expression.Coalesce(CodeGenEmitter.Emit_EnsureType(unwrappedExpression, typeof(object)), CodeGenEmitter.DBNull_Value);
				}
				Expression expression = Expression.NewArrayInit(typeof(object), array);
				TypeUsage typeUsage = columnMap.Type;
				if (this._spanIndex != null)
				{
					typeUsage = this._spanIndex.GetSpannedRowType(spanRowType) ?? typeUsage;
				}
				Expression expression2 = Expression.Constant(typeUsage, typeof(TypeUsage));
				return CodeGenEmitter.Emit_EnsureType(Expression.New(CodeGenEmitter.MaterializedDataRecord_ctor, new Expression[]
				{
					CodeGenEmitter.Shaper_Workspace,
					expression2,
					expression
				}), arg.RequestedType);
			}

			// Token: 0x06006B00 RID: 27392 RVA: 0x0016DC6C File Offset: 0x0016BE6C
			private Expression HandleSpandexRecord(RecordColumnMap columnMap, TranslatorArg arg, RowType spanRowType)
			{
				Dictionary<int, AssociationEndMember> spanMap = this._spanIndex.GetSpanMap(spanRowType);
				Expression expression = columnMap.Properties[0].Accept<TranslatorResult, TranslatorArg>(this, arg).Expression;
				for (int i = 1; i < columnMap.Properties.Length; i++)
				{
					AssociationEndMember associationEndMember = spanMap[i];
					TranslatorResult translatorResult = Translator.TranslatorVisitor.AcceptWithMappedType(this, columnMap.Properties[i]);
					Expression expression2 = translatorResult.Expression;
					CollectionTranslatorResult collectionTranslatorResult = translatorResult as CollectionTranslatorResult;
					if (collectionTranslatorResult != null)
					{
						Expression expressionToGetCoordinator = collectionTranslatorResult.ExpressionToGetCoordinator;
						Type type = expression2.Type.GetGenericArguments()[0];
						MethodInfo methodInfo = CodeGenEmitter.Shaper_HandleFullSpanCollection.MakeGenericMethod(new Type[] { type });
						expression = Expression.Call(CodeGenEmitter.Shaper_Parameter, methodInfo, expression, expressionToGetCoordinator, Expression.Constant(associationEndMember));
					}
					else if (typeof(EntityKey) == expression2.Type)
					{
						MethodInfo shaper_HandleRelationshipSpan = CodeGenEmitter.Shaper_HandleRelationshipSpan;
						expression = Expression.Call(CodeGenEmitter.Shaper_Parameter, shaper_HandleRelationshipSpan, expression, expression2, Expression.Constant(associationEndMember));
					}
					else
					{
						MethodInfo shaper_HandleFullSpanElement = CodeGenEmitter.Shaper_HandleFullSpanElement;
						expression = Expression.Call(CodeGenEmitter.Shaper_Parameter, shaper_HandleFullSpanElement, expression, expression2, Expression.Constant(associationEndMember));
					}
				}
				return expression;
			}

			// Token: 0x06006B01 RID: 27393 RVA: 0x0016DD7A File Offset: 0x0016BF7A
			internal override TranslatorResult Visit(SimpleCollectionColumnMap columnMap, TranslatorArg arg)
			{
				return this.ProcessCollectionColumnMap(columnMap, arg);
			}

			// Token: 0x06006B02 RID: 27394 RVA: 0x0016DD84 File Offset: 0x0016BF84
			internal override TranslatorResult Visit(DiscriminatedCollectionColumnMap columnMap, TranslatorArg arg)
			{
				return this.ProcessCollectionColumnMap(columnMap, arg, columnMap.Discriminator, columnMap.DiscriminatorValue);
			}

			// Token: 0x06006B03 RID: 27395 RVA: 0x0016DD9A File Offset: 0x0016BF9A
			private TranslatorResult ProcessCollectionColumnMap(CollectionColumnMap columnMap, TranslatorArg arg)
			{
				return this.ProcessCollectionColumnMap(columnMap, arg, null, null);
			}

			// Token: 0x06006B04 RID: 27396 RVA: 0x0016DDA8 File Offset: 0x0016BFA8
			private TranslatorResult ProcessCollectionColumnMap(CollectionColumnMap columnMap, TranslatorArg arg, ColumnMap discriminatorColumnMap, object discriminatorValue)
			{
				Type type = this.DetermineElementType(arg.RequestedType, columnMap);
				CoordinatorScratchpad coordinatorScratchpad = new CoordinatorScratchpad(type);
				this.EnterCoordinatorTranslateScope(coordinatorScratchpad);
				ColumnMap columnMap2 = columnMap.Element;
				if (this.IsValueLayer && !(columnMap2 is StructuredColumnMap))
				{
					ColumnMap[] array = new ColumnMap[] { columnMap.Element };
					columnMap2 = new RecordColumnMap(columnMap.Element.Type, columnMap.Element.Name, array, null);
				}
				bool inNullableType = this._inNullableType;
				if (discriminatorColumnMap != null)
				{
					this._inNullableType = true;
				}
				Expression unconvertedExpression = columnMap2.Accept<TranslatorResult, TranslatorArg>(this, new TranslatorArg(type)).UnconvertedExpression;
				Expression[] array2;
				if (columnMap.Keys != null)
				{
					array2 = new Expression[columnMap.Keys.Length];
					for (int i = 0; i < array2.Length; i++)
					{
						Expression expression = Translator.TranslatorVisitor.AcceptWithMappedType(this, columnMap.Keys[i]).Expression;
						array2[i] = expression;
					}
				}
				else
				{
					array2 = new Expression[0];
				}
				Expression expression2 = null;
				if (discriminatorColumnMap != null)
				{
					expression2 = Translator.TranslatorVisitor.AcceptWithMappedType(this, discriminatorColumnMap).Expression;
					this._inNullableType = inNullableType;
				}
				Expression expression3 = this.BuildExpressionToGetCoordinator(type, unconvertedExpression, array2, expression2, discriminatorValue, coordinatorScratchpad);
				MethodInfo genericElementsMethod = Translator.TranslatorVisitor.GetGenericElementsMethod(type);
				Expression expression4;
				if (this.IsValueLayer)
				{
					expression4 = expression3;
				}
				else
				{
					expression4 = Expression.Call(expression3, genericElementsMethod);
					coordinatorScratchpad.Element = CodeGenEmitter.Emit_EnsureType(coordinatorScratchpad.Element, type);
					Type type2 = arg.RequestedType.TryGetElementType(typeof(ICollection<>));
					if (type2 != null)
					{
						Type type3 = EntityUtil.DetermineCollectionType(arg.RequestedType);
						if (type3 == null)
						{
							throw new InvalidOperationException(Strings.ObjectQuery_UnableToMaterializeArbitaryProjectionType(arg.RequestedType));
						}
						Type type4 = typeof(List<>).MakeGenericType(new Type[] { type2 });
						if (type3 != type4)
						{
							coordinatorScratchpad.InitializeCollection = CodeGenEmitter.Emit_EnsureType(DelegateFactory.GetNewExpressionForCollectionType(type3), typeof(ICollection<>).MakeGenericType(new Type[] { type2 }));
						}
						expression4 = CodeGenEmitter.Emit_EnsureType(expression4, arg.RequestedType);
					}
					else if (!arg.RequestedType.IsAssignableFrom(expression4.Type))
					{
						Type type5 = typeof(CompensatingCollection<>).MakeGenericType(new Type[] { type });
						expression4 = CodeGenEmitter.Emit_EnsureType(Expression.New(type5.GetConstructors()[0], new Expression[] { expression4 }), type5);
					}
				}
				this.ExitCoordinatorTranslateScope();
				return new CollectionTranslatorResult(expression4, arg.RequestedType, expression3);
			}

			// Token: 0x06006B05 RID: 27397 RVA: 0x0016E00B File Offset: 0x0016C20B
			public static MethodInfo GetGenericElementsMethod(Type elementType)
			{
				return typeof(Coordinator<>).MakeGenericType(new Type[] { elementType }).GetOnlyDeclaredMethod("GetElements");
			}

			// Token: 0x06006B06 RID: 27398 RVA: 0x0016E030 File Offset: 0x0016C230
			private Type DetermineElementType(Type collectionType, CollectionColumnMap columnMap)
			{
				Type type;
				if (this.IsValueLayer)
				{
					type = typeof(RecordState);
				}
				else
				{
					type = TypeSystem.GetElementType(collectionType);
					if (type == collectionType)
					{
						TypeUsage typeUsage = ((CollectionType)columnMap.Type.EdmType).TypeUsage;
						type = this.DetermineClrType(typeUsage);
					}
				}
				return type;
			}

			// Token: 0x06006B07 RID: 27399 RVA: 0x0016E084 File Offset: 0x0016C284
			private void EnterCoordinatorTranslateScope(CoordinatorScratchpad coordinatorScratchpad)
			{
				if (this.RootCoordinatorScratchpad == null)
				{
					coordinatorScratchpad.Depth = 0;
					this.RootCoordinatorScratchpad = coordinatorScratchpad;
					this._currentCoordinatorScratchpad = coordinatorScratchpad;
					return;
				}
				coordinatorScratchpad.Depth = this._currentCoordinatorScratchpad.Depth + 1;
				this._currentCoordinatorScratchpad.AddNestedCoordinator(coordinatorScratchpad);
				this._currentCoordinatorScratchpad = coordinatorScratchpad;
			}

			// Token: 0x06006B08 RID: 27400 RVA: 0x0016E0D5 File Offset: 0x0016C2D5
			private void ExitCoordinatorTranslateScope()
			{
				this._currentCoordinatorScratchpad = this._currentCoordinatorScratchpad.Parent;
			}

			// Token: 0x06006B09 RID: 27401 RVA: 0x0016E0E8 File Offset: 0x0016C2E8
			private Expression BuildExpressionToGetCoordinator(Type elementType, Expression element, Expression[] keyReaders, Expression discriminator, object discriminatorValue, CoordinatorScratchpad coordinatorScratchpad)
			{
				int num = this.AllocateStateSlot();
				coordinatorScratchpad.StateSlotNumber = num;
				coordinatorScratchpad.Element = element;
				List<Expression> list = new List<Expression>(keyReaders.Length);
				List<Expression> list2 = new List<Expression>(keyReaders.Length);
				foreach (Expression expression in keyReaders)
				{
					int num2 = this.AllocateStateSlot();
					list.Add(CodeGenEmitter.Emit_Shaper_SetState(num2, expression));
					list2.Add(CodeGenEmitter.Emit_Equal(CodeGenEmitter.Emit_Shaper_GetState(num2, expression.Type), expression));
				}
				coordinatorScratchpad.SetKeys = CodeGenEmitter.Emit_BitwiseOr(list);
				coordinatorScratchpad.CheckKeys = CodeGenEmitter.Emit_AndAlso(list2);
				if (discriminator != null)
				{
					coordinatorScratchpad.HasData = CodeGenEmitter.Emit_Equal(Expression.Constant(discriminatorValue, discriminator.Type), discriminator);
				}
				return CodeGenEmitter.Emit_Shaper_GetState(num, typeof(Coordinator<>).MakeGenericType(new Type[] { elementType }));
			}

			// Token: 0x06006B0A RID: 27402 RVA: 0x0016E1C4 File Offset: 0x0016C3C4
			internal override TranslatorResult Visit(RefColumnMap columnMap, TranslatorArg arg)
			{
				EntityIdentity entityIdentity = columnMap.EntityIdentity;
				Expression expression2;
				Expression expression = Expression.Condition(CodeGenEmitter.Emit_EntityKey_HasValue(entityIdentity.Keys), this.Emit_EntityKey_ctor(this, entityIdentity, ((RefType)columnMap.Type.EdmType).ElementType, true, out expression2), Expression.Constant(null, typeof(EntityKey)));
				int columnPos = ((ScalarColumnMap)entityIdentity.Keys[0]).ColumnPos;
				if (!this._streaming && !this.NullableColumns.Contains(columnPos))
				{
					this.NullableColumns.Add(columnPos);
				}
				return new TranslatorResult(expression, arg.RequestedType);
			}

			// Token: 0x06006B0B RID: 27403 RVA: 0x0016E258 File Offset: 0x0016C458
			internal override TranslatorResult Visit(ScalarColumnMap columnMap, TranslatorArg arg)
			{
				Type requestedType = arg.RequestedType;
				TypeUsage type = columnMap.Type;
				int columnPos = columnMap.ColumnPos;
				Type type2 = null;
				PrimitiveTypeKind primitiveTypeKind;
				Expression expression;
				if (Helper.IsSpatialType(type, out primitiveTypeKind))
				{
					expression = CodeGenEmitter.Emit_Conditional_NotDBNull(Helper.IsGeographicType((PrimitiveType)type.EdmType) ? CodeGenEmitter.Emit_EnsureType(CodeGenEmitter.Emit_Shaper_GetGeographyColumnValue(columnPos), requestedType) : CodeGenEmitter.Emit_EnsureType(CodeGenEmitter.Emit_Shaper_GetGeometryColumnValue(columnPos), requestedType), columnPos, requestedType);
					if (!this._streaming && !this.NullableColumns.Contains(columnPos))
					{
						this.NullableColumns.Add(columnPos);
					}
				}
				else if (Helper.IsHierarchyIdType(type))
				{
					expression = CodeGenEmitter.Emit_Conditional_NotDBNull(CodeGenEmitter.Emit_EnsureType(CodeGenEmitter.Emit_Shaper_GetHierarchyIdColumnValue(columnPos), requestedType), columnPos, requestedType);
				}
				else
				{
					bool flag;
					MethodInfo readerMethod = CodeGenEmitter.GetReaderMethod(requestedType, out flag);
					expression = Expression.Call(CodeGenEmitter.Shaper_Reader, readerMethod, new Expression[] { Expression.Constant(columnPos) });
					type2 = TypeSystem.GetNonNullableType(requestedType);
					if (type2.IsEnum() && type2 != requestedType)
					{
						expression = Expression.Convert(expression, type2);
					}
					else if (requestedType == typeof(object) && !this.IsValueLayer && TypeSemantics.IsEnumerationType(type))
					{
						expression = Expression.Condition(CodeGenEmitter.Emit_Reader_IsDBNull(columnPos), expression, Expression.Convert(Expression.Convert(expression, TypeSystem.GetNonNullableType(this.DetermineClrType(type.EdmType))), typeof(object)));
						if (!this._streaming && !this.NullableColumns.Contains(columnPos))
						{
							this.NullableColumns.Add(columnPos);
						}
					}
					expression = CodeGenEmitter.Emit_EnsureType(expression, requestedType);
					if (flag)
					{
						expression = CodeGenEmitter.Emit_Conditional_NotDBNull(expression, columnPos, requestedType);
						if (!this._streaming && !this.NullableColumns.Contains(columnPos))
						{
							this.NullableColumns.Add(columnPos);
						}
					}
				}
				if (!this._streaming)
				{
					Type type3 = type2 ?? requestedType;
					type3 = (type3.IsEnum() ? type3.GetEnumUnderlyingType() : type3);
					Type type4;
					if (this.ColumnTypes.TryGetValue(columnPos, out type4))
					{
						if (type4 == typeof(object) && type3 != typeof(object))
						{
							this.ColumnTypes[columnPos] = type3;
						}
					}
					else
					{
						this.ColumnTypes.Add(columnPos, type3);
						if (this._inNullableType && !this.NullableColumns.Contains(columnPos))
						{
							this.NullableColumns.Add(columnPos);
						}
					}
				}
				Expression expression2 = CodeGenEmitter.Emit_Shaper_GetColumnValueWithErrorHandling(arg.RequestedType, columnPos, type);
				this._currentCoordinatorScratchpad.AddExpressionWithErrorHandling(expression, expression2);
				return new TranslatorResult(expression, requestedType);
			}

			// Token: 0x06006B0C RID: 27404 RVA: 0x0016E4CC File Offset: 0x0016C6CC
			internal override TranslatorResult Visit(VarRefColumnMap columnMap, TranslatorArg arg)
			{
				throw new InvalidOperationException(string.Empty);
			}

			// Token: 0x06006B0D RID: 27405 RVA: 0x0016E4D8 File Offset: 0x0016C6D8
			private int AllocateStateSlot()
			{
				int stateSlotCount = this.StateSlotCount;
				this.StateSlotCount = stateSlotCount + 1;
				return stateSlotCount;
			}

			// Token: 0x06006B0E RID: 27406 RVA: 0x0016E4F6 File Offset: 0x0016C6F6
			private Type DetermineClrType(TypeUsage typeUsage)
			{
				return this.DetermineClrType(typeUsage.EdmType);
			}

			// Token: 0x06006B0F RID: 27407 RVA: 0x0016E504 File Offset: 0x0016C704
			private Type DetermineClrType(EdmType edmType)
			{
				Type type = null;
				edmType = this.ResolveSpanType(edmType);
				BuiltInTypeKind builtInTypeKind = edmType.BuiltInTypeKind;
				if (builtInTypeKind <= BuiltInTypeKind.EntityType)
				{
					if (builtInTypeKind != BuiltInTypeKind.CollectionType)
					{
						if (builtInTypeKind == BuiltInTypeKind.ComplexType || builtInTypeKind == BuiltInTypeKind.EntityType)
						{
							if (this.IsValueLayer)
							{
								type = typeof(RecordState);
							}
							else
							{
								type = this.LookupObjectMapping(edmType).ClrType.ClrType;
							}
						}
					}
					else if (this.IsValueLayer)
					{
						type = typeof(Coordinator<RecordState>);
					}
					else
					{
						EdmType edmType2 = ((CollectionType)edmType).TypeUsage.EdmType;
						type = this.DetermineClrType(edmType2);
						type = typeof(IEnumerable<>).MakeGenericType(new Type[] { type });
					}
				}
				else if (builtInTypeKind <= BuiltInTypeKind.PrimitiveType)
				{
					if (builtInTypeKind != BuiltInTypeKind.EnumType)
					{
						if (builtInTypeKind == BuiltInTypeKind.PrimitiveType)
						{
							type = ((PrimitiveType)edmType).ClrEquivalentType;
							if (type.IsValueType())
							{
								type = typeof(Nullable<>).MakeGenericType(new Type[] { type });
							}
						}
					}
					else if (this.IsValueLayer)
					{
						type = this.DetermineClrType(((EnumType)edmType).UnderlyingType);
					}
					else
					{
						type = this.LookupObjectMapping(edmType).ClrType.ClrType;
						type = typeof(Nullable<>).MakeGenericType(new Type[] { type });
					}
				}
				else if (builtInTypeKind != BuiltInTypeKind.RefType)
				{
					if (builtInTypeKind == BuiltInTypeKind.RowType)
					{
						if (this.IsValueLayer)
						{
							type = typeof(RecordState);
						}
						else
						{
							InitializerMetadata initializerMetadata = ((RowType)edmType).InitializerMetadata;
							if (initializerMetadata != null)
							{
								type = initializerMetadata.ClrType;
							}
							else
							{
								type = typeof(DbDataRecord);
							}
						}
					}
				}
				else
				{
					type = typeof(EntityKey);
				}
				return type;
			}

			// Token: 0x06006B10 RID: 27408 RVA: 0x0016E6AB File Offset: 0x0016C8AB
			private static ConstructorInfo GetConstructor(Type type)
			{
				if (!type.IsAbstract())
				{
					return DelegateFactory.GetConstructorForType(type);
				}
				return null;
			}

			// Token: 0x06006B11 RID: 27409 RVA: 0x0016E6C0 File Offset: 0x0016C8C0
			private ObjectTypeMapping LookupObjectMapping(EdmType edmType)
			{
				EdmType edmType2 = this.ResolveSpanType(edmType);
				if (edmType2 == null)
				{
					edmType2 = edmType;
				}
				ObjectTypeMapping objectMapping;
				if (!this._objectTypeMappings.TryGetValue(edmType2, out objectMapping))
				{
					objectMapping = Util.GetObjectMapping(edmType2, this._workspace);
					this._objectTypeMappings.Add(edmType2, objectMapping);
				}
				return objectMapping;
			}

			// Token: 0x06006B12 RID: 27410 RVA: 0x0016E708 File Offset: 0x0016C908
			private EdmType ResolveSpanType(EdmType edmType)
			{
				EdmType edmType2 = edmType;
				BuiltInTypeKind builtInTypeKind = edmType2.BuiltInTypeKind;
				if (builtInTypeKind != BuiltInTypeKind.CollectionType)
				{
					if (builtInTypeKind == BuiltInTypeKind.RowType)
					{
						RowType rowType = (RowType)edmType2;
						if (this._spanIndex != null && this._spanIndex.HasSpanMap(rowType))
						{
							edmType2 = rowType.Members[0].TypeUsage.EdmType;
						}
					}
				}
				else
				{
					edmType2 = this.ResolveSpanType(((CollectionType)edmType2).TypeUsage.EdmType);
					if (edmType2 != null)
					{
						edmType2 = new CollectionType(edmType2);
					}
				}
				return edmType2;
			}

			// Token: 0x06006B13 RID: 27411 RVA: 0x0016E784 File Offset: 0x0016C984
			private LambdaExpression CreateInlineDelegate(Expression body)
			{
				Type type = body.Type;
				return (LambdaExpression)Translator.TranslatorVisitor.Translator_TypedCreateInlineDelegate.MakeGenericMethod(new Type[] { type }).Invoke(this, new object[] { body });
			}

			// Token: 0x06006B14 RID: 27412 RVA: 0x0016E7C4 File Offset: 0x0016C9C4
			private Expression<Func<Shaper, T>> TypedCreateInlineDelegate<T>(Expression body)
			{
				Expression<Func<Shaper, T>> expression = Expression.Lambda<Func<Shaper, T>>(body, new ParameterExpression[] { CodeGenEmitter.Shaper_Parameter });
				this._currentCoordinatorScratchpad.AddInlineDelegate(expression);
				return expression;
			}

			// Token: 0x06006B15 RID: 27413 RVA: 0x0016E7F4 File Offset: 0x0016C9F4
			private Expression Emit_EntityKey_ctor(Translator.TranslatorVisitor translatorVisitor, EntityIdentity entityIdentity, EdmType type, bool isForColumnValue, out Expression entitySetReader)
			{
				Expression expression = null;
				List<Expression> list = new List<Expression>(entityIdentity.Keys.Length);
				if (this.IsValueLayer)
				{
					for (int i = 0; i < entityIdentity.Keys.Length; i++)
					{
						Expression expression2 = entityIdentity.Keys[i].Accept<TranslatorResult, TranslatorArg>(translatorVisitor, new TranslatorArg(typeof(object))).Expression;
						list.Add(expression2);
					}
				}
				else
				{
					ObjectTypeMapping objectTypeMapping = this.LookupObjectMapping(type);
					for (int j = 0; j < entityIdentity.Keys.Length; j++)
					{
						Type propertyType = DelegateFactory.ValidateSetterProperty(objectTypeMapping.GetPropertyMap(entityIdentity.Keys[j].Name).ClrProperty.PropertyInfo).PropertyType;
						Expression expression3 = entityIdentity.Keys[j].Accept<TranslatorResult, TranslatorArg>(translatorVisitor, new TranslatorArg(propertyType)).Expression;
						list.Add(CodeGenEmitter.Emit_EnsureType(expression3, typeof(object)));
					}
				}
				SimpleEntityIdentity simpleEntityIdentity = entityIdentity as SimpleEntityIdentity;
				if (simpleEntityIdentity != null)
				{
					if (simpleEntityIdentity.EntitySet == null)
					{
						entitySetReader = Expression.Constant(null, typeof(EntitySet));
						return Expression.Constant(null, typeof(EntityKey));
					}
					entitySetReader = Expression.Constant(simpleEntityIdentity.EntitySet, typeof(EntitySet));
				}
				else
				{
					DiscriminatedEntityIdentity discriminatedEntityIdentity = (DiscriminatedEntityIdentity)entityIdentity;
					Expression expression4 = discriminatedEntityIdentity.EntitySetColumnMap.Accept<TranslatorResult, TranslatorArg>(translatorVisitor, new TranslatorArg(typeof(int?))).Expression;
					EntitySet[] entitySetMap = discriminatedEntityIdentity.EntitySetMap;
					entitySetReader = Expression.Constant(null, typeof(EntitySet));
					for (int k = 0; k < entitySetMap.Length; k++)
					{
						entitySetReader = Expression.Condition(Expression.Equal(expression4, Expression.Constant(k, typeof(int?))), Expression.Constant(entitySetMap[k], typeof(EntitySet)), entitySetReader);
					}
					int num = translatorVisitor.AllocateStateSlot();
					expression = CodeGenEmitter.Emit_Shaper_SetStatePassthrough(num, entitySetReader);
					entitySetReader = CodeGenEmitter.Emit_Shaper_GetState(num, typeof(EntitySet));
				}
				Expression expression5;
				if (1 == entityIdentity.Keys.Length)
				{
					expression5 = Expression.New(CodeGenEmitter.EntityKey_ctor_SingleKey, new Expression[]
					{
						entitySetReader,
						list[0]
					});
				}
				else
				{
					expression5 = Expression.New(CodeGenEmitter.EntityKey_ctor_CompositeKey, new Expression[]
					{
						entitySetReader,
						Expression.NewArrayInit(typeof(object), list)
					});
				}
				if (expression != null)
				{
					Expression expression6;
					if (translatorVisitor.IsValueLayer && !isForColumnValue)
					{
						expression6 = Expression.Constant(EntityKey.NoEntitySetKey, typeof(EntityKey));
					}
					else
					{
						expression6 = Expression.Constant(null, typeof(EntityKey));
					}
					expression5 = Expression.Condition(Expression.Equal(expression, Expression.Constant(null, typeof(EntitySet))), expression6, expression5);
				}
				return expression5;
			}

			// Token: 0x04003101 RID: 12545
			private readonly MetadataWorkspace _workspace;

			// Token: 0x04003102 RID: 12546
			private readonly SpanIndex _spanIndex;

			// Token: 0x04003103 RID: 12547
			private readonly MergeOption _mergeOption;

			// Token: 0x04003104 RID: 12548
			private readonly bool _streaming;

			// Token: 0x04003105 RID: 12549
			private readonly bool IsValueLayer;

			// Token: 0x04003106 RID: 12550
			private CoordinatorScratchpad _currentCoordinatorScratchpad;

			// Token: 0x04003107 RID: 12551
			private readonly Dictionary<EdmType, ObjectTypeMapping> _objectTypeMappings = new Dictionary<EdmType, ObjectTypeMapping>();

			// Token: 0x04003108 RID: 12552
			private bool _inNullableType;

			// Token: 0x04003109 RID: 12553
			public static readonly MethodInfo Translator_MultipleDiscriminatorPolymorphicColumnMapHelper = typeof(Translator.TranslatorVisitor).GetOnlyDeclaredMethod("MultipleDiscriminatorPolymorphicColumnMapHelper");

			// Token: 0x0400310A RID: 12554
			public static readonly MethodInfo Translator_TypedCreateInlineDelegate = typeof(Translator.TranslatorVisitor).GetOnlyDeclaredMethod("TypedCreateInlineDelegate");
		}
	}
}
