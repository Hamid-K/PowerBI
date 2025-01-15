using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020000CE RID: 206
	internal class DynamicToFunctionModificationCommandConverter : DefaultExpressionVisitor
	{
		// Token: 0x06000FEE RID: 4078 RVA: 0x00021364 File Offset: 0x0001F564
		public DynamicToFunctionModificationCommandConverter(EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping, EntityContainerMapping entityContainerMapping)
		{
			this._entityTypeModificationFunctionMapping = entityTypeModificationFunctionMapping;
			this._entityContainerMapping = entityContainerMapping;
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0002137A File Offset: 0x0001F57A
		public DynamicToFunctionModificationCommandConverter(AssociationSetModificationFunctionMapping associationSetModificationFunctionMapping, EntityContainerMapping entityContainerMapping)
		{
			this._associationSetModificationFunctionMapping = associationSetModificationFunctionMapping;
			this._entityContainerMapping = entityContainerMapping;
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00021390 File Offset: 0x0001F590
		public IEnumerable<TCommandTree> Convert<TCommandTree>(IEnumerable<TCommandTree> modificationCommandTrees) where TCommandTree : DbModificationCommandTree
		{
			this._currentFunctionMapping = null;
			this._currentProperty = null;
			this._storeGeneratedKeys = null;
			this._nextStoreGeneratedKey = 0;
			return modificationCommandTrees.Select(delegate(TCommandTree modificationCommandTree)
			{
				if (DynamicToFunctionModificationCommandConverter.<>o__10<TCommandTree>.<>p__0 == null)
				{
					DynamicToFunctionModificationCommandConverter.<>o__10<TCommandTree>.<>p__0 = CallSite<Func<CallSite, DynamicToFunctionModificationCommandConverter, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "ConvertInternal", null, typeof(DynamicToFunctionModificationCommandConverter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				return DynamicToFunctionModificationCommandConverter.<>o__10<TCommandTree>.<>p__0.Target(DynamicToFunctionModificationCommandConverter.<>o__10<TCommandTree>.<>p__0, this, modificationCommandTree);
			}).Cast<TCommandTree>();
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x000213C8 File Offset: 0x0001F5C8
		private DbModificationCommandTree ConvertInternal(DbInsertCommandTree commandTree)
		{
			if (this._currentFunctionMapping == null)
			{
				this._currentFunctionMapping = ((this._entityTypeModificationFunctionMapping != null) ? this._entityTypeModificationFunctionMapping.InsertFunctionMapping : this._associationSetModificationFunctionMapping.InsertFunctionMapping);
				EntityTypeBase elementType = ((DbScanExpression)commandTree.Target.Expression).Target.ElementType;
				this._storeGeneratedKeys = elementType.KeyProperties.Where((EdmProperty p) => p.IsStoreGeneratedIdentity).ToList<EdmProperty>();
			}
			this._nextStoreGeneratedKey = 0;
			return new DbInsertCommandTree(commandTree.MetadataWorkspace, commandTree.DataSpace, commandTree.Target, this.VisitSetClauses(commandTree.SetClauses), (commandTree.Returning != null) ? commandTree.Returning.Accept<DbExpression>(this) : null);
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00021494 File Offset: 0x0001F694
		private DbModificationCommandTree ConvertInternal(DbUpdateCommandTree commandTree)
		{
			this._currentFunctionMapping = this._entityTypeModificationFunctionMapping.UpdateFunctionMapping;
			this._useOriginalValues = true;
			DbExpression dbExpression = commandTree.Predicate.Accept<DbExpression>(this);
			this._useOriginalValues = false;
			return new DbUpdateCommandTree(commandTree.MetadataWorkspace, commandTree.DataSpace, commandTree.Target, dbExpression, this.VisitSetClauses(commandTree.SetClauses), (commandTree.Returning != null) ? commandTree.Returning.Accept<DbExpression>(this) : null);
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00021508 File Offset: 0x0001F708
		private DbModificationCommandTree ConvertInternal(DbDeleteCommandTree commandTree)
		{
			this._currentFunctionMapping = ((this._entityTypeModificationFunctionMapping != null) ? this._entityTypeModificationFunctionMapping.DeleteFunctionMapping : this._associationSetModificationFunctionMapping.DeleteFunctionMapping);
			return new DbDeleteCommandTree(commandTree.MetadataWorkspace, commandTree.DataSpace, commandTree.Target, commandTree.Predicate.Accept<DbExpression>(this));
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0002155E File Offset: 0x0001F75E
		private ReadOnlyCollection<DbModificationClause> VisitSetClauses(IList<DbModificationClause> setClauses)
		{
			return new ReadOnlyCollection<DbModificationClause>((from DbSetClause s in setClauses
				select new DbSetClause(s.Property.Accept<DbExpression>(this), s.Value.Accept<DbExpression>(this))).Cast<DbModificationClause>().ToList<DbModificationClause>());
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00021588 File Offset: 0x0001F788
		public override DbExpression Visit(DbComparisonExpression expression)
		{
			DbComparisonExpression dbComparisonExpression = (DbComparisonExpression)base.Visit(expression);
			DbPropertyExpression dbPropertyExpression = (DbPropertyExpression)dbComparisonExpression.Left;
			if (((EdmProperty)dbPropertyExpression.Property).Nullable)
			{
				DbAndExpression dbAndExpression = dbPropertyExpression.IsNull().And(dbComparisonExpression.Right.IsNull());
				return dbComparisonExpression.Or(dbAndExpression);
			}
			return dbComparisonExpression;
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x000215E0 File Offset: 0x0001F7E0
		public override DbExpression Visit(DbPropertyExpression expression)
		{
			this._currentProperty = (EdmProperty)expression.Property;
			return base.Visit(expression);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x000215FC File Offset: 0x0001F7FC
		public override DbExpression Visit(DbConstantExpression expression)
		{
			if (this._currentProperty != null)
			{
				Tuple<FunctionParameter, bool> parameter = this.GetParameter(this._currentProperty, this._useOriginalValues);
				if (parameter != null)
				{
					return new DbParameterReferenceExpression(parameter.Item1.TypeUsage, parameter.Item1.Name);
				}
			}
			return base.Visit(expression);
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0002164C File Offset: 0x0001F84C
		public override DbExpression Visit(DbAndExpression expression)
		{
			DbExpression dbExpression = this.VisitExpression(expression.Left);
			DbExpression dbExpression2 = this.VisitExpression(expression.Right);
			if (dbExpression != null && dbExpression2 != null)
			{
				return dbExpression.And(dbExpression2);
			}
			return dbExpression ?? dbExpression2;
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00021688 File Offset: 0x0001F888
		public override DbExpression Visit(DbIsNullExpression expression)
		{
			DbPropertyExpression dbPropertyExpression = expression.Argument as DbPropertyExpression;
			if (dbPropertyExpression != null)
			{
				Tuple<FunctionParameter, bool> parameter = this.GetParameter((EdmProperty)dbPropertyExpression.Property, true);
				if (parameter != null)
				{
					if (parameter.Item2)
					{
						return null;
					}
					DbParameterReferenceExpression dbParameterReferenceExpression = new DbParameterReferenceExpression(parameter.Item1.TypeUsage, parameter.Item1.Name);
					DbExpression dbExpression = dbPropertyExpression.Equal(dbParameterReferenceExpression);
					DbAndExpression dbAndExpression = dbPropertyExpression.IsNull().And(dbParameterReferenceExpression.IsNull());
					return dbExpression.Or(dbAndExpression);
				}
			}
			return base.Visit(expression);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00021708 File Offset: 0x0001F908
		public override DbExpression Visit(DbNullExpression expression)
		{
			if (this._currentProperty != null)
			{
				Tuple<FunctionParameter, bool> parameter = this.GetParameter(this._currentProperty, false);
				if (parameter != null)
				{
					return new DbParameterReferenceExpression(parameter.Item1.TypeUsage, parameter.Item1.Name);
				}
			}
			return base.Visit(expression);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00021754 File Offset: 0x0001F954
		public override DbExpression Visit(DbNewInstanceExpression expression)
		{
			return DbExpressionBuilder.NewRow((from <>h__TransparentIdentifier0 in expression.Arguments.Cast<DbPropertyExpression>().Select(delegate(DbPropertyExpression propertyExpression)
				{
					Func<<>f__AnonymousType13<<>f__AnonymousType12<<>f__AnonymousType11<EntitySetMapping, EntityTypeMapping>, MappingFragment>, ScalarPropertyMapping>, bool> <>9__9;
					return new
					{
						propertyExpression = propertyExpression,
						resultBinding = this._currentFunctionMapping.ResultBindings.Single(delegate(ModificationFunctionResultBinding rb)
						{
							var enumerable = from esm in this._entityContainerMapping.EntitySetMappings
								from etm in esm.EntityTypeMappings
								from mf in etm.MappingFragments
								from pm in mf.PropertyMappings.OfType<ScalarPropertyMapping>()
								select new { <>h__TransparentIdentifier1, pm };
							var func;
							if ((func = <>9__9) == null)
							{
								func = (<>9__9 = <>h__TransparentIdentifier2 => <>h__TransparentIdentifier2.pm.Column.EdmEquals(propertyExpression.Property) && <>h__TransparentIdentifier2.pm.Column.DeclaringType.EdmEquals(propertyExpression.Property.DeclaringType));
							}
							return (from <>h__TransparentIdentifier2 in enumerable.Where(func)
								select <>h__TransparentIdentifier2.pm.Property).Contains(rb.Property);
						})
					};
				})
				select new KeyValuePair<string, DbExpression>(<>h__TransparentIdentifier0.resultBinding.ColumnName, <>h__TransparentIdentifier0.propertyExpression)).ToList<KeyValuePair<string, DbExpression>>());
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x000217AC File Offset: 0x0001F9AC
		private Tuple<FunctionParameter, bool> GetParameter(EdmProperty column, bool originalValue = false)
		{
			List<ColumnMappingBuilder> columnMappings = (from esm in this._entityContainerMapping.EntitySetMappings
				from etm in esm.EntityTypeMappings
				from mf in etm.MappingFragments
				from cm in mf.FlattenedProperties
				where cm.ColumnProperty.EdmEquals(column) && cm.ColumnProperty.DeclaringType.EdmEquals(column.DeclaringType)
				select cm).ToList<ColumnMappingBuilder>();
			List<ModificationFunctionParameterBinding> list = this._currentFunctionMapping.ParameterBindings.Where((ModificationFunctionParameterBinding pb) => columnMappings.Any((ColumnMappingBuilder cm) => pb.MemberPath.Members.Reverse<EdmMember>().SequenceEqual(cm.PropertyPath))).ToList<ModificationFunctionParameterBinding>();
			if (!list.Any<ModificationFunctionParameterBinding>())
			{
				List<EdmMember[]> iaColumnMappings = (from asm in this._entityContainerMapping.AssociationSetMappings
					from tm in asm.TypeMappings
					from mf in tm.MappingFragments
					from epm in mf.PropertyMappings.OfType<EndPropertyMapping>()
					from pm in epm.PropertyMappings
					where pm.Column.EdmEquals(column) && pm.Column.DeclaringType.EdmEquals(column.DeclaringType)
					select new EdmMember[] { pm.Property, epm.AssociationEnd }).ToList<EdmMember[]>();
				list = this._currentFunctionMapping.ParameterBindings.Where((ModificationFunctionParameterBinding pb) => iaColumnMappings.Any((EdmMember[] epm) => pb.MemberPath.Members.SequenceEqual(epm))).ToList<ModificationFunctionParameterBinding>();
			}
			if (list.Count == 0 && column.IsPrimaryKeyColumn)
			{
				List<EdmProperty> storeGeneratedKeys = this._storeGeneratedKeys;
				int nextStoreGeneratedKey = this._nextStoreGeneratedKey;
				this._nextStoreGeneratedKey = nextStoreGeneratedKey + 1;
				return Tuple.Create<FunctionParameter, bool>(new FunctionParameter(storeGeneratedKeys[nextStoreGeneratedKey].Name, column.TypeUsage, ParameterMode.In), true);
			}
			if (list.Count == 1)
			{
				return Tuple.Create<FunctionParameter, bool>(list[0].Parameter, list[0].IsCurrent);
			}
			if (list.Count == 0)
			{
				return null;
			}
			ModificationFunctionParameterBinding modificationFunctionParameterBinding;
			if (!originalValue)
			{
				modificationFunctionParameterBinding = list.Single((ModificationFunctionParameterBinding pb) => pb.IsCurrent);
			}
			else
			{
				modificationFunctionParameterBinding = list.Single((ModificationFunctionParameterBinding pb) => !pb.IsCurrent);
			}
			ModificationFunctionParameterBinding modificationFunctionParameterBinding2 = modificationFunctionParameterBinding;
			return Tuple.Create<FunctionParameter, bool>(modificationFunctionParameterBinding2.Parameter, modificationFunctionParameterBinding2.IsCurrent);
		}

		// Token: 0x04000898 RID: 2200
		private readonly EntityTypeModificationFunctionMapping _entityTypeModificationFunctionMapping;

		// Token: 0x04000899 RID: 2201
		private readonly AssociationSetModificationFunctionMapping _associationSetModificationFunctionMapping;

		// Token: 0x0400089A RID: 2202
		private readonly EntityContainerMapping _entityContainerMapping;

		// Token: 0x0400089B RID: 2203
		private ModificationFunctionMapping _currentFunctionMapping;

		// Token: 0x0400089C RID: 2204
		private EdmProperty _currentProperty;

		// Token: 0x0400089D RID: 2205
		private List<EdmProperty> _storeGeneratedKeys;

		// Token: 0x0400089E RID: 2206
		private int _nextStoreGeneratedKey;

		// Token: 0x0400089F RID: 2207
		private bool _useOriginalValues;
	}
}
