using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005D6 RID: 1494
	internal sealed class UpdateCompiler
	{
		// Token: 0x060047F3 RID: 18419 RVA: 0x000FFA04 File Offset: 0x000FDC04
		internal UpdateCompiler(UpdateTranslator translator)
		{
			this.m_translator = translator;
		}

		// Token: 0x060047F4 RID: 18420 RVA: 0x000FFA14 File Offset: 0x000FDC14
		internal UpdateCommand BuildDeleteCommand(PropagatorResult oldRow, TableChangeProcessor processor)
		{
			bool flag = true;
			DbExpressionBinding target = UpdateCompiler.GetTarget(processor);
			DbExpression dbExpression = this.BuildPredicate(target, oldRow, null, processor, ref flag);
			DbDeleteCommandTree dbDeleteCommandTree = new DbDeleteCommandTree(this.m_translator.MetadataWorkspace, DataSpace.SSpace, target, dbExpression);
			return new DynamicUpdateCommand(processor, this.m_translator, ModificationOperator.Delete, oldRow, null, dbDeleteCommandTree, null);
		}

		// Token: 0x060047F5 RID: 18421 RVA: 0x000FFA5C File Offset: 0x000FDC5C
		internal UpdateCommand BuildUpdateCommand(PropagatorResult oldRow, PropagatorResult newRow, TableChangeProcessor processor)
		{
			bool flag = false;
			DbExpressionBinding target = UpdateCompiler.GetTarget(processor);
			List<DbModificationClause> list = new List<DbModificationClause>();
			Dictionary<int, string> dictionary;
			DbExpression dbExpression;
			foreach (DbModificationClause dbModificationClause in this.BuildSetClauses(target, newRow, oldRow, processor, false, out dictionary, out dbExpression, ref flag))
			{
				list.Add(dbModificationClause);
			}
			DbExpression dbExpression2 = this.BuildPredicate(target, oldRow, newRow, processor, ref flag);
			if (list.Count == 0)
			{
				if (flag)
				{
					List<IEntityStateEntry> list2 = new List<IEntityStateEntry>();
					list2.AddRange(SourceInterpreter.GetAllStateEntries(oldRow, this.m_translator, processor.Table));
					list2.AddRange(SourceInterpreter.GetAllStateEntries(newRow, this.m_translator, processor.Table));
					if (list2.All((IEntityStateEntry it) => it.State == EntityState.Unchanged))
					{
						flag = false;
					}
				}
				if (!flag)
				{
					return null;
				}
			}
			DbUpdateCommandTree dbUpdateCommandTree = new DbUpdateCommandTree(this.m_translator.MetadataWorkspace, DataSpace.SSpace, target, dbExpression2, new ReadOnlyCollection<DbModificationClause>(list), dbExpression);
			return new DynamicUpdateCommand(processor, this.m_translator, ModificationOperator.Update, oldRow, newRow, dbUpdateCommandTree, dictionary);
		}

		// Token: 0x060047F6 RID: 18422 RVA: 0x000FFB78 File Offset: 0x000FDD78
		internal UpdateCommand BuildInsertCommand(PropagatorResult newRow, TableChangeProcessor processor)
		{
			DbExpressionBinding target = UpdateCompiler.GetTarget(processor);
			bool flag = true;
			List<DbModificationClause> list = new List<DbModificationClause>();
			Dictionary<int, string> dictionary;
			DbExpression dbExpression;
			foreach (DbModificationClause dbModificationClause in this.BuildSetClauses(target, newRow, null, processor, true, out dictionary, out dbExpression, ref flag))
			{
				list.Add(dbModificationClause);
			}
			DbInsertCommandTree dbInsertCommandTree = new DbInsertCommandTree(this.m_translator.MetadataWorkspace, DataSpace.SSpace, target, new ReadOnlyCollection<DbModificationClause>(list), dbExpression);
			return new DynamicUpdateCommand(processor, this.m_translator, ModificationOperator.Insert, null, newRow, dbInsertCommandTree, dictionary);
		}

		// Token: 0x060047F7 RID: 18423 RVA: 0x000FFC18 File Offset: 0x000FDE18
		private IEnumerable<DbModificationClause> BuildSetClauses(DbExpressionBinding target, PropagatorResult row, PropagatorResult originalRow, TableChangeProcessor processor, bool insertMode, out Dictionary<int, string> outputIdentifiers, out DbExpression returning, ref bool rowMustBeTouched)
		{
			Dictionary<EdmProperty, PropagatorResult> dictionary = new Dictionary<EdmProperty, PropagatorResult>();
			List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>();
			outputIdentifiers = new Dictionary<int, string>();
			PropagatorFlags propagatorFlags = (insertMode ? PropagatorFlags.NoFlags : (PropagatorFlags.Preserve | PropagatorFlags.Unknown));
			for (int i = 0; i < processor.Table.ElementType.Properties.Count; i++)
			{
				EdmProperty edmProperty = processor.Table.ElementType.Properties[i];
				PropagatorResult propagatorResult = row.GetMemberValue(i);
				if (-1 != propagatorResult.Identifier)
				{
					propagatorResult = propagatorResult.ReplicateResultWithNewValue(this.m_translator.KeyManager.GetPrincipalValue(propagatorResult));
				}
				bool flag = false;
				bool flag2 = false;
				for (int j = 0; j < processor.KeyOrdinals.Length; j++)
				{
					if (processor.KeyOrdinals[j] == i)
					{
						flag2 = true;
						break;
					}
				}
				PropagatorFlags propagatorFlags2 = PropagatorFlags.NoFlags;
				if (!insertMode && flag2)
				{
					flag = true;
				}
				else
				{
					propagatorFlags2 |= propagatorResult.PropagatorFlags;
				}
				StoreGeneratedPattern storeGeneratedPattern = MetadataHelper.GetStoreGeneratedPattern(edmProperty);
				bool flag3 = storeGeneratedPattern == StoreGeneratedPattern.Computed || (insertMode && storeGeneratedPattern == StoreGeneratedPattern.Identity);
				if (flag3)
				{
					DbPropertyExpression dbPropertyExpression = target.Variable.Property(edmProperty);
					list.Add(new KeyValuePair<string, DbExpression>(edmProperty.Name, dbPropertyExpression));
					int identifier = propagatorResult.Identifier;
					if (-1 != identifier)
					{
						if (this.m_translator.KeyManager.HasPrincipals(identifier))
						{
							throw new InvalidOperationException(Strings.Update_GeneratedDependent(edmProperty.Name));
						}
						outputIdentifiers.Add(identifier, edmProperty.Name);
						if (storeGeneratedPattern != StoreGeneratedPattern.Identity && processor.IsKeyProperty(i))
						{
							throw new NotSupportedException(Strings.Update_NotSupportedComputedKeyColumn("StoreGeneratedPattern", "Computed", "Identity", edmProperty.Name, edmProperty.DeclaringType.FullName));
						}
					}
				}
				if ((propagatorFlags2 & propagatorFlags) != PropagatorFlags.NoFlags)
				{
					flag = true;
				}
				else if (flag3)
				{
					flag = true;
					rowMustBeTouched = true;
				}
				if (!flag && !insertMode && storeGeneratedPattern == StoreGeneratedPattern.Identity)
				{
					PropagatorResult memberValue = originalRow.GetMemberValue(i);
					if (!ByValueEqualityComparer.Default.Equals(memberValue.GetSimpleValue(), propagatorResult.GetSimpleValue()))
					{
						throw new InvalidOperationException(Strings.Update_ModifyingIdentityColumn("Identity", edmProperty.Name, edmProperty.DeclaringType.FullName));
					}
					flag = true;
				}
				if (!flag)
				{
					dictionary.Add(edmProperty, propagatorResult);
				}
			}
			if (0 < list.Count)
			{
				returning = DbExpressionBuilder.NewRow(list);
			}
			else
			{
				returning = null;
			}
			List<DbModificationClause> list2 = new List<DbModificationClause>(dictionary.Count);
			foreach (KeyValuePair<EdmProperty, PropagatorResult> keyValuePair in dictionary)
			{
				list2.Add(new DbSetClause(UpdateCompiler.GeneratePropertyExpression(target, keyValuePair.Key), this.GenerateValueExpression(keyValuePair.Key, keyValuePair.Value)));
			}
			return list2;
		}

		// Token: 0x060047F8 RID: 18424 RVA: 0x000FFEDC File Offset: 0x000FE0DC
		private DbExpression BuildPredicate(DbExpressionBinding target, PropagatorResult referenceRow, PropagatorResult current, TableChangeProcessor processor, ref bool rowMustBeTouched)
		{
			Dictionary<EdmProperty, PropagatorResult> dictionary = new Dictionary<EdmProperty, PropagatorResult>();
			int num = 0;
			foreach (EdmProperty edmProperty in processor.Table.ElementType.Properties)
			{
				PropagatorResult memberValue = referenceRow.GetMemberValue(num);
				PropagatorResult propagatorResult = ((current == null) ? null : current.GetMemberValue(num));
				if (!rowMustBeTouched && (UpdateCompiler.HasFlag(memberValue, PropagatorFlags.ConcurrencyValue) || UpdateCompiler.HasFlag(propagatorResult, PropagatorFlags.ConcurrencyValue)))
				{
					rowMustBeTouched = true;
				}
				if (!dictionary.ContainsKey(edmProperty) && (UpdateCompiler.HasFlag(memberValue, PropagatorFlags.ConcurrencyValue | PropagatorFlags.Key) || UpdateCompiler.HasFlag(propagatorResult, PropagatorFlags.ConcurrencyValue | PropagatorFlags.Key)))
				{
					dictionary.Add(edmProperty, memberValue);
				}
				num++;
			}
			DbExpression dbExpression = null;
			foreach (KeyValuePair<EdmProperty, PropagatorResult> keyValuePair in dictionary)
			{
				DbExpression dbExpression2 = this.GenerateEqualityExpression(target, keyValuePair.Key, keyValuePair.Value);
				if (dbExpression == null)
				{
					dbExpression = dbExpression2;
				}
				else
				{
					dbExpression = dbExpression.And(dbExpression2);
				}
			}
			return dbExpression;
		}

		// Token: 0x060047F9 RID: 18425 RVA: 0x00100004 File Offset: 0x000FE204
		private DbExpression GenerateEqualityExpression(DbExpressionBinding target, EdmProperty property, PropagatorResult value)
		{
			DbExpression dbExpression = UpdateCompiler.GeneratePropertyExpression(target, property);
			DbExpression dbExpression2 = this.GenerateValueExpression(property, value);
			if (dbExpression2.ExpressionKind == DbExpressionKind.Null)
			{
				return dbExpression.IsNull();
			}
			return dbExpression.Equal(dbExpression2);
		}

		// Token: 0x060047FA RID: 18426 RVA: 0x0010003A File Offset: 0x000FE23A
		private static DbExpression GeneratePropertyExpression(DbExpressionBinding target, EdmProperty property)
		{
			return target.Variable.Property(property);
		}

		// Token: 0x060047FB RID: 18427 RVA: 0x00100048 File Offset: 0x000FE248
		private DbExpression GenerateValueExpression(EdmProperty property, PropagatorResult value)
		{
			if (value.IsNull)
			{
				return Helper.GetModelTypeUsage(property).Null();
			}
			object obj = this.m_translator.KeyManager.GetPrincipalValue(value);
			if (Convert.IsDBNull(obj))
			{
				return Helper.GetModelTypeUsage(property).Null();
			}
			TypeUsage modelTypeUsage = Helper.GetModelTypeUsage(property);
			Type type = obj.GetType();
			if (type.IsEnum())
			{
				obj = Convert.ChangeType(obj, type.GetEnumUnderlyingType(), CultureInfo.InvariantCulture);
			}
			Type clrEquivalentType = ((PrimitiveType)modelTypeUsage.EdmType).ClrEquivalentType;
			if (type != clrEquivalentType)
			{
				obj = Convert.ChangeType(obj, clrEquivalentType, CultureInfo.InvariantCulture);
			}
			return modelTypeUsage.Constant(obj);
		}

		// Token: 0x060047FC RID: 18428 RVA: 0x001000E3 File Offset: 0x000FE2E3
		private static bool HasFlag(PropagatorResult input, PropagatorFlags flags)
		{
			return input != null && (flags & input.PropagatorFlags) > PropagatorFlags.NoFlags;
		}

		// Token: 0x060047FD RID: 18429 RVA: 0x001000F5 File Offset: 0x000FE2F5
		private static DbExpressionBinding GetTarget(TableChangeProcessor processor)
		{
			return processor.Table.Scan().BindAs("target");
		}

		// Token: 0x04001999 RID: 6553
		internal readonly UpdateTranslator m_translator;

		// Token: 0x0400199A RID: 6554
		private const string s_targetVarName = "target";
	}
}
