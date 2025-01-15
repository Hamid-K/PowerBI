using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Internal.Materialization;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x02000462 RID: 1122
	internal abstract class InitializerMetadata : IEquatable<InitializerMetadata>
	{
		// Token: 0x0600372F RID: 14127 RVA: 0x000B3150 File Offset: 0x000B1350
		private InitializerMetadata(Type clrType)
		{
			this.ClrType = clrType;
			this.Identity = InitializerMetadata._identifierPrefix + Interlocked.Increment(ref InitializerMetadata.s_identifier).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06003730 RID: 14128
		internal abstract InitializerMetadataKind Kind { get; }

		// Token: 0x06003731 RID: 14129 RVA: 0x000B3191 File Offset: 0x000B1391
		internal static bool TryGetInitializerMetadata(TypeUsage typeUsage, out InitializerMetadata initializerMetadata)
		{
			initializerMetadata = null;
			if (BuiltInTypeKind.RowType == typeUsage.EdmType.BuiltInTypeKind)
			{
				initializerMetadata = ((RowType)typeUsage.EdmType).InitializerMetadata;
			}
			return initializerMetadata != null;
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x000B31BC File Offset: 0x000B13BC
		internal static InitializerMetadata CreateGroupingInitializer(EdmItemCollection itemCollection, Type resultType)
		{
			return itemCollection.GetCanonicalInitializerMetadata(new InitializerMetadata.GroupingInitializerMetadata(resultType));
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x000B31CA File Offset: 0x000B13CA
		internal static InitializerMetadata CreateProjectionInitializer(EdmItemCollection itemCollection, MemberInitExpression initExpression)
		{
			return itemCollection.GetCanonicalInitializerMetadata(new InitializerMetadata.ProjectionInitializerMetadata(initExpression));
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x000B31D8 File Offset: 0x000B13D8
		internal static InitializerMetadata CreateProjectionInitializer(EdmItemCollection itemCollection, NewExpression newExpression)
		{
			return itemCollection.GetCanonicalInitializerMetadata(new InitializerMetadata.ProjectionNewMetadata(newExpression));
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x000B31E6 File Offset: 0x000B13E6
		internal static InitializerMetadata CreateEmptyProjectionInitializer(EdmItemCollection itemCollection, NewExpression newExpression)
		{
			return itemCollection.GetCanonicalInitializerMetadata(new InitializerMetadata.EmptyProjectionNewMetadata(newExpression));
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x000B31F4 File Offset: 0x000B13F4
		internal static InitializerMetadata CreateEntityCollectionInitializer(EdmItemCollection itemCollection, Type type, NavigationProperty navigationProperty)
		{
			return itemCollection.GetCanonicalInitializerMetadata(new InitializerMetadata.EntityCollectionInitializerMetadata(type, navigationProperty));
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x000B3203 File Offset: 0x000B1403
		internal virtual void AppendColumnMapKey(ColumnMapKeyBuilder builder)
		{
			builder.Append("CLR-", this.ClrType);
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x000B3216 File Offset: 0x000B1416
		public override bool Equals(object obj)
		{
			return this.Equals(obj as InitializerMetadata);
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x000B3224 File Offset: 0x000B1424
		public bool Equals(InitializerMetadata other)
		{
			return this == other || (this.Kind == other.Kind && this.ClrType.Equals(other.ClrType) && this.IsStructurallyEquivalent(other));
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x000B3258 File Offset: 0x000B1458
		public override int GetHashCode()
		{
			return this.ClrType.GetHashCode();
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x000B3265 File Offset: 0x000B1465
		protected virtual bool IsStructurallyEquivalent(InitializerMetadata other)
		{
			return true;
		}

		// Token: 0x0600373C RID: 14140
		internal abstract Expression Emit(List<TranslatorResult> propertyTranslatorResults);

		// Token: 0x0600373D RID: 14141
		internal abstract IEnumerable<Type> GetChildTypes();

		// Token: 0x0600373E RID: 14142 RVA: 0x000B3268 File Offset: 0x000B1468
		protected static List<Expression> GetPropertyReaders(List<TranslatorResult> propertyTranslatorResults)
		{
			return propertyTranslatorResults.Select((TranslatorResult s) => s.UnwrappedExpression).ToList<Expression>();
		}

		// Token: 0x0400120D RID: 4621
		internal readonly Type ClrType;

		// Token: 0x0400120E RID: 4622
		private static long s_identifier;

		// Token: 0x0400120F RID: 4623
		internal readonly string Identity;

		// Token: 0x04001210 RID: 4624
		private static readonly string _identifierPrefix = typeof(InitializerMetadata).Name;

		// Token: 0x02000AA3 RID: 2723
		private class Grouping<K, T> : IGrouping<K, T>, IEnumerable<T>, IEnumerable
		{
			// Token: 0x0600626E RID: 25198 RVA: 0x0015600D File Offset: 0x0015420D
			public Grouping(K key, IEnumerable<T> group)
			{
				this._key = key;
				this._group = group;
			}

			// Token: 0x170010BA RID: 4282
			// (get) Token: 0x0600626F RID: 25199 RVA: 0x00156023 File Offset: 0x00154223
			public K Key
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x170010BB RID: 4283
			// (get) Token: 0x06006270 RID: 25200 RVA: 0x0015602B File Offset: 0x0015422B
			public IEnumerable<T> Group
			{
				get
				{
					return this._group;
				}
			}

			// Token: 0x06006271 RID: 25201 RVA: 0x00156033 File Offset: 0x00154233
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				if (this._group == null)
				{
					yield break;
				}
				foreach (T t in this._group)
				{
					yield return t;
				}
				IEnumerator<T> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06006272 RID: 25202 RVA: 0x00156042 File Offset: 0x00154242
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable<T>)this).GetEnumerator();
			}

			// Token: 0x04002B3E RID: 11070
			private readonly K _key;

			// Token: 0x04002B3F RID: 11071
			private readonly IEnumerable<T> _group;
		}

		// Token: 0x02000AA4 RID: 2724
		private class GroupingInitializerMetadata : InitializerMetadata
		{
			// Token: 0x06006273 RID: 25203 RVA: 0x0015604A File Offset: 0x0015424A
			internal GroupingInitializerMetadata(Type type)
				: base(type)
			{
			}

			// Token: 0x170010BC RID: 4284
			// (get) Token: 0x06006274 RID: 25204 RVA: 0x00156053 File Offset: 0x00154253
			internal override InitializerMetadataKind Kind
			{
				get
				{
					return InitializerMetadataKind.Grouping;
				}
			}

			// Token: 0x06006275 RID: 25205 RVA: 0x00156058 File Offset: 0x00154258
			internal override Expression Emit(List<TranslatorResult> propertyTranslatorResults)
			{
				Type type = this.ClrType.GetGenericArguments()[0];
				Type type2 = this.ClrType.GetGenericArguments()[1];
				return Expression.Convert(Expression.New(typeof(InitializerMetadata.Grouping<, >).MakeGenericType(new Type[] { type, type2 }).GetConstructors().Single<ConstructorInfo>(), InitializerMetadata.GetPropertyReaders(propertyTranslatorResults)), this.ClrType);
			}

			// Token: 0x06006276 RID: 25206 RVA: 0x001560BE File Offset: 0x001542BE
			internal override IEnumerable<Type> GetChildTypes()
			{
				Type type = this.ClrType.GetGenericArguments()[0];
				Type groupElementType = this.ClrType.GetGenericArguments()[1];
				yield return type;
				yield return typeof(IEnumerable<>).MakeGenericType(new Type[] { groupElementType });
				yield break;
			}
		}

		// Token: 0x02000AA5 RID: 2725
		private class ProjectionNewMetadata : InitializerMetadata
		{
			// Token: 0x06006277 RID: 25207 RVA: 0x001560CE File Offset: 0x001542CE
			internal ProjectionNewMetadata(NewExpression newExpression)
				: base(newExpression.Type)
			{
				this._newExpression = newExpression;
			}

			// Token: 0x170010BD RID: 4285
			// (get) Token: 0x06006278 RID: 25208 RVA: 0x001560E3 File Offset: 0x001542E3
			internal override InitializerMetadataKind Kind
			{
				get
				{
					return InitializerMetadataKind.ProjectionNew;
				}
			}

			// Token: 0x06006279 RID: 25209 RVA: 0x001560E8 File Offset: 0x001542E8
			protected override bool IsStructurallyEquivalent(InitializerMetadata other)
			{
				InitializerMetadata.ProjectionNewMetadata projectionNewMetadata = (InitializerMetadata.ProjectionNewMetadata)other;
				if (this._newExpression.Members == null && projectionNewMetadata._newExpression.Members == null)
				{
					return true;
				}
				if (this._newExpression.Members == null || projectionNewMetadata._newExpression.Members == null)
				{
					return false;
				}
				if (this._newExpression.Members.Count != projectionNewMetadata._newExpression.Members.Count)
				{
					return false;
				}
				for (int i = 0; i < this._newExpression.Members.Count; i++)
				{
					object obj = this._newExpression.Members[i];
					MemberInfo memberInfo = projectionNewMetadata._newExpression.Members[i];
					if (!obj.Equals(memberInfo))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600627A RID: 25210 RVA: 0x001561A1 File Offset: 0x001543A1
			internal override Expression Emit(List<TranslatorResult> propertyTranslatorResults)
			{
				return Expression.New(this._newExpression.Constructor, InitializerMetadata.GetPropertyReaders(propertyTranslatorResults));
			}

			// Token: 0x0600627B RID: 25211 RVA: 0x001561B9 File Offset: 0x001543B9
			internal override IEnumerable<Type> GetChildTypes()
			{
				return this._newExpression.Arguments.Select((Expression arg) => arg.Type);
			}

			// Token: 0x0600627C RID: 25212 RVA: 0x001561EC File Offset: 0x001543EC
			internal override void AppendColumnMapKey(ColumnMapKeyBuilder builder)
			{
				base.AppendColumnMapKey(builder);
				builder.Append(this._newExpression.Constructor.ToString());
				IEnumerable<MemberInfo> members = this._newExpression.Members;
				foreach (MemberInfo memberInfo in (members ?? Enumerable.Empty<MemberInfo>()))
				{
					builder.Append("DT", memberInfo.DeclaringType);
					builder.Append("." + memberInfo.Name);
				}
			}

			// Token: 0x04002B40 RID: 11072
			private readonly NewExpression _newExpression;
		}

		// Token: 0x02000AA6 RID: 2726
		private class EmptyProjectionNewMetadata : InitializerMetadata.ProjectionNewMetadata
		{
			// Token: 0x0600627D RID: 25213 RVA: 0x00156288 File Offset: 0x00154488
			internal EmptyProjectionNewMetadata(NewExpression newExpression)
				: base(newExpression)
			{
			}

			// Token: 0x0600627E RID: 25214 RVA: 0x00156291 File Offset: 0x00154491
			internal override Expression Emit(List<TranslatorResult> propertyReaders)
			{
				return base.Emit(new List<TranslatorResult>());
			}

			// Token: 0x0600627F RID: 25215 RVA: 0x0015629E File Offset: 0x0015449E
			internal override IEnumerable<Type> GetChildTypes()
			{
				yield return null;
				yield break;
			}
		}

		// Token: 0x02000AA7 RID: 2727
		private class ProjectionInitializerMetadata : InitializerMetadata
		{
			// Token: 0x06006280 RID: 25216 RVA: 0x001562A7 File Offset: 0x001544A7
			internal ProjectionInitializerMetadata(MemberInitExpression initExpression)
				: base(initExpression.Type)
			{
				this._initExpression = initExpression;
			}

			// Token: 0x170010BE RID: 4286
			// (get) Token: 0x06006281 RID: 25217 RVA: 0x001562BC File Offset: 0x001544BC
			internal override InitializerMetadataKind Kind
			{
				get
				{
					return InitializerMetadataKind.ProjectionInitializer;
				}
			}

			// Token: 0x06006282 RID: 25218 RVA: 0x001562C0 File Offset: 0x001544C0
			protected override bool IsStructurallyEquivalent(InitializerMetadata other)
			{
				InitializerMetadata.ProjectionInitializerMetadata projectionInitializerMetadata = (InitializerMetadata.ProjectionInitializerMetadata)other;
				if (this._initExpression.Bindings.Count != projectionInitializerMetadata._initExpression.Bindings.Count)
				{
					return false;
				}
				for (int i = 0; i < this._initExpression.Bindings.Count; i++)
				{
					MemberBinding memberBinding = this._initExpression.Bindings[i];
					MemberBinding memberBinding2 = projectionInitializerMetadata._initExpression.Bindings[i];
					if (!memberBinding.Member.Equals(memberBinding2.Member))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06006283 RID: 25219 RVA: 0x0015634C File Offset: 0x0015454C
			internal override Expression Emit(List<TranslatorResult> propertyReaders)
			{
				MemberBinding[] array = new MemberBinding[this._initExpression.Bindings.Count];
				MemberBinding[] array2 = new MemberBinding[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					MemberBinding memberBinding = this._initExpression.Bindings[i];
					Expression unwrappedExpression = propertyReaders[i].UnwrappedExpression;
					MemberBinding memberBinding2 = Expression.Bind(memberBinding.Member, unwrappedExpression);
					MemberBinding memberBinding3 = Expression.Bind(memberBinding.Member, Expression.Constant(TypeSystem.GetDefaultValue(unwrappedExpression.Type), unwrappedExpression.Type));
					array[i] = memberBinding2;
					array2[i] = memberBinding3;
				}
				return Expression.MemberInit(this._initExpression.NewExpression, array);
			}

			// Token: 0x06006284 RID: 25220 RVA: 0x001563EF File Offset: 0x001545EF
			internal override IEnumerable<Type> GetChildTypes()
			{
				foreach (MemberBinding memberBinding in this._initExpression.Bindings)
				{
					string text;
					Type type;
					TypeSystem.PropertyOrField(memberBinding.Member, out text, out type);
					yield return type;
				}
				IEnumerator<MemberBinding> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06006285 RID: 25221 RVA: 0x00156400 File Offset: 0x00154600
			internal override void AppendColumnMapKey(ColumnMapKeyBuilder builder)
			{
				base.AppendColumnMapKey(builder);
				foreach (MemberBinding memberBinding in this._initExpression.Bindings)
				{
					builder.Append(",", memberBinding.Member.DeclaringType);
					builder.Append("." + memberBinding.Member.Name);
				}
			}

			// Token: 0x04002B41 RID: 11073
			private readonly MemberInitExpression _initExpression;
		}

		// Token: 0x02000AA8 RID: 2728
		internal class EntityCollectionInitializerMetadata : InitializerMetadata
		{
			// Token: 0x06006286 RID: 25222 RVA: 0x00156484 File Offset: 0x00154684
			internal EntityCollectionInitializerMetadata(Type type, NavigationProperty navigationProperty)
				: base(type)
			{
				this._navigationProperty = navigationProperty;
			}

			// Token: 0x170010BF RID: 4287
			// (get) Token: 0x06006287 RID: 25223 RVA: 0x00156494 File Offset: 0x00154694
			internal override InitializerMetadataKind Kind
			{
				get
				{
					return InitializerMetadataKind.EntityCollection;
				}
			}

			// Token: 0x06006288 RID: 25224 RVA: 0x00156498 File Offset: 0x00154698
			protected override bool IsStructurallyEquivalent(InitializerMetadata other)
			{
				InitializerMetadata.EntityCollectionInitializerMetadata entityCollectionInitializerMetadata = (InitializerMetadata.EntityCollectionInitializerMetadata)other;
				return this._navigationProperty.Equals(entityCollectionInitializerMetadata._navigationProperty);
			}

			// Token: 0x06006289 RID: 25225 RVA: 0x001564C0 File Offset: 0x001546C0
			internal override Expression Emit(List<TranslatorResult> propertyTranslatorResults)
			{
				Type elementType = this.GetElementType();
				MethodInfo methodInfo = InitializerMetadata.EntityCollectionInitializerMetadata.CreateEntityCollectionMethod.MakeGenericMethod(new Type[] { elementType });
				Expression expression = propertyTranslatorResults[0].Expression;
				Expression expressionToGetCoordinator = (propertyTranslatorResults[1] as CollectionTranslatorResult).ExpressionToGetCoordinator;
				return Expression.Call(methodInfo, expression, expressionToGetCoordinator, Expression.Constant(this._navigationProperty.RelationshipType.FullName), Expression.Constant(this._navigationProperty.ToEndMember.Name));
			}

			// Token: 0x0600628A RID: 25226 RVA: 0x00156538 File Offset: 0x00154738
			public static EntityCollection<T> CreateEntityCollection<T>(IEntityWrapper wrappedOwner, Coordinator<T> coordinator, string relationshipName, string targetRoleName) where T : class
			{
				if (wrappedOwner.Entity == null)
				{
					return null;
				}
				EntityCollection<T> result = wrappedOwner.RelationshipManager.GetRelatedCollection<T>(relationshipName, targetRoleName);
				coordinator.RegisterCloseHandler(delegate(Shaper readerState, List<IEntityWrapper> elements)
				{
					result.Load(elements, readerState.MergeOption);
				});
				return result;
			}

			// Token: 0x0600628B RID: 25227 RVA: 0x00156580 File Offset: 0x00154780
			internal override IEnumerable<Type> GetChildTypes()
			{
				Type elementType = this.GetElementType();
				yield return null;
				yield return typeof(IEnumerable<>).MakeGenericType(new Type[] { elementType });
				yield break;
			}

			// Token: 0x0600628C RID: 25228 RVA: 0x00156590 File Offset: 0x00154790
			internal override void AppendColumnMapKey(ColumnMapKeyBuilder builder)
			{
				base.AppendColumnMapKey(builder);
				builder.Append(",NP" + this._navigationProperty.Name);
				builder.Append(",AT", this._navigationProperty.DeclaringType);
			}

			// Token: 0x0600628D RID: 25229 RVA: 0x001565CC File Offset: 0x001547CC
			private Type GetElementType()
			{
				Type type = this.ClrType.TryGetElementType(typeof(ICollection<>));
				if (type == null)
				{
					throw new InvalidOperationException(Strings.ELinq_UnexpectedTypeForNavigationProperty(this._navigationProperty, typeof(EntityCollection<>), typeof(ICollection<>), this.ClrType));
				}
				return type;
			}

			// Token: 0x04002B42 RID: 11074
			private readonly NavigationProperty _navigationProperty;

			// Token: 0x04002B43 RID: 11075
			internal static readonly MethodInfo CreateEntityCollectionMethod = typeof(InitializerMetadata.EntityCollectionInitializerMetadata).GetOnlyDeclaredMethod("CreateEntityCollection");
		}
	}
}
