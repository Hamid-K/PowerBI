using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005C3 RID: 1475
	internal class DynamicUpdateCommand : UpdateCommand
	{
		// Token: 0x06004751 RID: 18257 RVA: 0x000FBFA8 File Offset: 0x000FA1A8
		internal DynamicUpdateCommand(TableChangeProcessor processor, UpdateTranslator translator, ModificationOperator modificationOperator, PropagatorResult originalValues, PropagatorResult currentValues, DbModificationCommandTree tree, Dictionary<int, string> outputIdentifiers)
			: base(translator, originalValues, currentValues)
		{
			this._processor = processor;
			this._operator = modificationOperator;
			this._modificationCommandTree = tree;
			this._outputIdentifiers = outputIdentifiers;
			if (ModificationOperator.Insert == modificationOperator || modificationOperator == ModificationOperator.Update)
			{
				this._inputIdentifiers = new List<KeyValuePair<int, DbSetClause>>(2);
				foreach (KeyValuePair<EdmMember, PropagatorResult> keyValuePair in Helper.PairEnumerations<EdmMember, PropagatorResult>(TypeHelpers.GetAllStructuralMembers(base.CurrentValues.StructuralType), base.CurrentValues.GetMemberValues()))
				{
					int identifier = keyValuePair.Value.Identifier;
					DbSetClause dbSetClause;
					if (-1 != identifier && DynamicUpdateCommand.TryGetSetterExpression(tree, keyValuePair.Key, modificationOperator, out dbSetClause))
					{
						foreach (int num in translator.KeyManager.GetPrincipals(identifier))
						{
							this._inputIdentifiers.Add(new KeyValuePair<int, DbSetClause>(num, dbSetClause));
						}
					}
				}
			}
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x000FC0C0 File Offset: 0x000FA2C0
		private static bool TryGetSetterExpression(DbModificationCommandTree tree, EdmMember member, ModificationOperator op, out DbSetClause setter)
		{
			IEnumerable<DbModificationClause> enumerable;
			if (ModificationOperator.Insert == op)
			{
				enumerable = ((DbInsertCommandTree)tree).SetClauses;
			}
			else
			{
				enumerable = ((DbUpdateCommandTree)tree).SetClauses;
			}
			foreach (DbModificationClause dbModificationClause in enumerable)
			{
				DbSetClause dbSetClause = (DbSetClause)dbModificationClause;
				if (((DbPropertyExpression)dbSetClause.Property).Property.EdmEquals(member))
				{
					setter = dbSetClause;
					return true;
				}
			}
			setter = null;
			return false;
		}

		// Token: 0x06004753 RID: 18259 RVA: 0x000FC14C File Offset: 0x000FA34C
		internal override long Execute(Dictionary<int, object> identifierValues, List<KeyValuePair<PropagatorResult, object>> generatedValues)
		{
			long num3;
			using (DbCommand dbCommand = this.CreateCommand(identifierValues))
			{
				EntityConnection connection = base.Translator.Connection;
				dbCommand.Transaction = ((connection.CurrentTransaction == null) ? null : connection.CurrentTransaction.StoreTransaction);
				dbCommand.Connection = connection.StoreConnection;
				if (base.Translator.CommandTimeout != null)
				{
					dbCommand.CommandTimeout = base.Translator.CommandTimeout.Value;
				}
				int num;
				if (this._modificationCommandTree.HasReader)
				{
					num = 0;
					using (DbDataReader dbDataReader = dbCommand.ExecuteReader(CommandBehavior.SequentialAccess))
					{
						if (dbDataReader.Read())
						{
							num++;
							IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(base.CurrentValues.StructuralType);
							for (int i = 0; i < dbDataReader.FieldCount; i++)
							{
								string name = dbDataReader.GetName(i);
								EdmMember edmMember = allStructuralMembers[name];
								object obj;
								if (Helper.IsSpatialType(edmMember.TypeUsage) && !dbDataReader.IsDBNull(i))
								{
									obj = SpatialHelpers.GetSpatialValue(base.Translator.MetadataWorkspace, dbDataReader, edmMember.TypeUsage, i);
								}
								else
								{
									obj = dbDataReader.GetValue(i);
								}
								int num2 = allStructuralMembers.IndexOf(edmMember);
								PropagatorResult memberValue = base.CurrentValues.GetMemberValue(num2);
								generatedValues.Add(new KeyValuePair<PropagatorResult, object>(memberValue, obj));
								int identifier = memberValue.Identifier;
								if (-1 != identifier)
								{
									identifierValues.Add(identifier, obj);
								}
							}
						}
						CommandHelper.ConsumeReader(dbDataReader);
						goto IL_017A;
					}
				}
				num = dbCommand.ExecuteNonQuery();
				IL_017A:
				num3 = (long)num;
			}
			return num3;
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x000FC31C File Offset: 0x000FA51C
		internal override async Task<long> ExecuteAsync(Dictionary<int, object> identifierValues, List<KeyValuePair<PropagatorResult, object>> generatedValues, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long num2;
			using (DbCommand command = this.CreateCommand(identifierValues))
			{
				EntityConnection connection = base.Translator.Connection;
				command.Transaction = ((connection.CurrentTransaction == null) ? null : connection.CurrentTransaction.StoreTransaction);
				command.Connection = connection.StoreConnection;
				if (base.Translator.CommandTimeout != null)
				{
					command.CommandTimeout = base.Translator.CommandTimeout.Value;
				}
				int rowsAffected;
				if (this._modificationCommandTree.HasReader)
				{
					rowsAffected = 0;
					DbDataReader dbDataReader = await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken).WithCurrentCulture<DbDataReader>();
					using (DbDataReader reader = dbDataReader)
					{
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = reader.ReadAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						if (!cultureAwaiter.IsCompleted)
						{
							await cultureAwaiter;
							cultureAwaiter = cultureAwaiter2;
							cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
						}
						if (cultureAwaiter.GetResult())
						{
							rowsAffected++;
							IBaseList<EdmMember> members = TypeHelpers.GetAllStructuralMembers(base.CurrentValues.StructuralType);
							for (int ordinal = 0; ordinal < reader.FieldCount; ordinal++)
							{
								EdmMember member = members[reader.GetName(ordinal)];
								bool flag = Helper.IsSpatialType(member.TypeUsage);
								if (flag)
								{
									cultureAwaiter = reader.IsDBNullAsync(ordinal, cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
									if (!cultureAwaiter.IsCompleted)
									{
										await cultureAwaiter;
										cultureAwaiter = cultureAwaiter2;
										cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
									}
									flag = !cultureAwaiter.GetResult();
								}
								object obj = ((!flag) ? (await reader.GetFieldValueAsync<object>(ordinal, cancellationToken).WithCurrentCulture<object>()) : (await SpatialHelpers.GetSpatialValueAsync(base.Translator.MetadataWorkspace, reader, member.TypeUsage, ordinal, cancellationToken).WithCurrentCulture<object>()));
								int num = members.IndexOf(member);
								PropagatorResult memberValue = base.CurrentValues.GetMemberValue(num);
								generatedValues.Add(new KeyValuePair<PropagatorResult, object>(memberValue, obj));
								int identifier = memberValue.Identifier;
								if (-1 != identifier)
								{
									identifierValues.Add(identifier, obj);
								}
								member = null;
							}
							members = null;
						}
						await CommandHelper.ConsumeReaderAsync(reader, cancellationToken).WithCurrentCulture();
					}
					DbDataReader reader = null;
				}
				else
				{
					rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken).WithCurrentCulture<int>();
				}
				num2 = (long)rowsAffected;
			}
			return num2;
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x000FC37C File Offset: 0x000FA57C
		protected virtual DbCommand CreateCommand(Dictionary<int, object> identifierValues)
		{
			DbModificationCommandTree dbModificationCommandTree = this._modificationCommandTree;
			if (this._inputIdentifiers != null)
			{
				Dictionary<DbSetClause, DbSetClause> dictionary = new Dictionary<DbSetClause, DbSetClause>();
				for (int i = 0; i < this._inputIdentifiers.Count; i++)
				{
					KeyValuePair<int, DbSetClause> keyValuePair = this._inputIdentifiers[i];
					object obj;
					if (identifierValues.TryGetValue(keyValuePair.Key, out obj))
					{
						DbSetClause dbSetClause = new DbSetClause(keyValuePair.Value.Property, DbExpressionBuilder.Constant(obj));
						dictionary[keyValuePair.Value] = dbSetClause;
						this._inputIdentifiers[i] = new KeyValuePair<int, DbSetClause>(keyValuePair.Key, dbSetClause);
					}
				}
				dbModificationCommandTree = DynamicUpdateCommand.RebuildCommandTree(dbModificationCommandTree, dictionary);
			}
			return base.Translator.CreateCommand(dbModificationCommandTree);
		}

		// Token: 0x06004756 RID: 18262 RVA: 0x000FC42C File Offset: 0x000FA62C
		private static DbModificationCommandTree RebuildCommandTree(DbModificationCommandTree originalTree, Dictionary<DbSetClause, DbSetClause> clauseMappings)
		{
			if (clauseMappings.Count == 0)
			{
				return originalTree;
			}
			DbModificationCommandTree dbModificationCommandTree;
			if (originalTree.CommandTreeKind == DbCommandTreeKind.Insert)
			{
				DbInsertCommandTree dbInsertCommandTree = (DbInsertCommandTree)originalTree;
				dbModificationCommandTree = new DbInsertCommandTree(dbInsertCommandTree.MetadataWorkspace, dbInsertCommandTree.DataSpace, dbInsertCommandTree.Target, new ReadOnlyCollection<DbModificationClause>(DynamicUpdateCommand.ReplaceClauses(dbInsertCommandTree.SetClauses, clauseMappings)), dbInsertCommandTree.Returning);
			}
			else
			{
				DbUpdateCommandTree dbUpdateCommandTree = (DbUpdateCommandTree)originalTree;
				dbModificationCommandTree = new DbUpdateCommandTree(dbUpdateCommandTree.MetadataWorkspace, dbUpdateCommandTree.DataSpace, dbUpdateCommandTree.Target, dbUpdateCommandTree.Predicate, new ReadOnlyCollection<DbModificationClause>(DynamicUpdateCommand.ReplaceClauses(dbUpdateCommandTree.SetClauses, clauseMappings)), dbUpdateCommandTree.Returning);
			}
			return dbModificationCommandTree;
		}

		// Token: 0x06004757 RID: 18263 RVA: 0x000FC4C4 File Offset: 0x000FA6C4
		private static List<DbModificationClause> ReplaceClauses(IList<DbModificationClause> originalClauses, Dictionary<DbSetClause, DbSetClause> mappings)
		{
			List<DbModificationClause> list = new List<DbModificationClause>(originalClauses.Count);
			for (int i = 0; i < originalClauses.Count; i++)
			{
				DbSetClause dbSetClause;
				if (mappings.TryGetValue((DbSetClause)originalClauses[i], out dbSetClause))
				{
					list.Add(dbSetClause);
				}
				else
				{
					list.Add(originalClauses[i]);
				}
			}
			return list;
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06004758 RID: 18264 RVA: 0x000FC51B File Offset: 0x000FA71B
		internal ModificationOperator Operator
		{
			get
			{
				return this._operator;
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06004759 RID: 18265 RVA: 0x000FC523 File Offset: 0x000FA723
		internal override EntitySet Table
		{
			get
			{
				return this._processor.Table;
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x0600475A RID: 18266 RVA: 0x000FC530 File Offset: 0x000FA730
		internal override IEnumerable<int> InputIdentifiers
		{
			get
			{
				if (this._inputIdentifiers == null)
				{
					yield break;
				}
				foreach (KeyValuePair<int, DbSetClause> keyValuePair in this._inputIdentifiers)
				{
					yield return keyValuePair.Key;
				}
				List<KeyValuePair<int, DbSetClause>>.Enumerator enumerator = default(List<KeyValuePair<int, DbSetClause>>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x0600475B RID: 18267 RVA: 0x000FC540 File Offset: 0x000FA740
		internal override IEnumerable<int> OutputIdentifiers
		{
			get
			{
				if (this._outputIdentifiers == null)
				{
					return Enumerable.Empty<int>();
				}
				return this._outputIdentifiers.Keys;
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x0600475C RID: 18268 RVA: 0x000FC55B File Offset: 0x000FA75B
		internal override UpdateCommandKind Kind
		{
			get
			{
				return UpdateCommandKind.Dynamic;
			}
		}

		// Token: 0x0600475D RID: 18269 RVA: 0x000FC560 File Offset: 0x000FA760
		internal override IList<IEntityStateEntry> GetStateEntries(UpdateTranslator translator)
		{
			List<IEntityStateEntry> list = new List<IEntityStateEntry>(2);
			if (base.OriginalValues != null)
			{
				foreach (IEntityStateEntry entityStateEntry in SourceInterpreter.GetAllStateEntries(base.OriginalValues, translator, this.Table))
				{
					list.Add(entityStateEntry);
				}
			}
			if (base.CurrentValues != null)
			{
				foreach (IEntityStateEntry entityStateEntry2 in SourceInterpreter.GetAllStateEntries(base.CurrentValues, translator, this.Table))
				{
					list.Add(entityStateEntry2);
				}
			}
			return list;
		}

		// Token: 0x0600475E RID: 18270 RVA: 0x000FC61C File Offset: 0x000FA81C
		internal override int CompareToType(UpdateCommand otherCommand)
		{
			DynamicUpdateCommand dynamicUpdateCommand = (DynamicUpdateCommand)otherCommand;
			int num = (int)(this.Operator - dynamicUpdateCommand.Operator);
			if (num != 0)
			{
				return num;
			}
			num = StringComparer.Ordinal.Compare(this._processor.Table.Name, dynamicUpdateCommand._processor.Table.Name);
			if (num != 0)
			{
				return num;
			}
			num = StringComparer.Ordinal.Compare(this._processor.Table.EntityContainer.Name, dynamicUpdateCommand._processor.Table.EntityContainer.Name);
			if (num != 0)
			{
				return num;
			}
			PropagatorResult propagatorResult = ((this.Operator == ModificationOperator.Delete) ? base.OriginalValues : base.CurrentValues);
			PropagatorResult propagatorResult2 = ((dynamicUpdateCommand.Operator == ModificationOperator.Delete) ? dynamicUpdateCommand.OriginalValues : dynamicUpdateCommand.CurrentValues);
			for (int i = 0; i < this._processor.KeyOrdinals.Length; i++)
			{
				int num2 = this._processor.KeyOrdinals[i];
				object simpleValue = propagatorResult.GetMemberValue(num2).GetSimpleValue();
				object simpleValue2 = propagatorResult2.GetMemberValue(num2).GetSimpleValue();
				num = ByValueComparer.Default.Compare(simpleValue, simpleValue2);
				if (num != 0)
				{
					return num;
				}
			}
			for (int j = 0; j < this._processor.KeyOrdinals.Length; j++)
			{
				int num3 = this._processor.KeyOrdinals[j];
				int identifier = propagatorResult.GetMemberValue(num3).Identifier;
				int identifier2 = propagatorResult2.GetMemberValue(num3).Identifier;
				num = identifier - identifier2;
				if (num != 0)
				{
					return num;
				}
			}
			return num;
		}

		// Token: 0x04001953 RID: 6483
		private readonly ModificationOperator _operator;

		// Token: 0x04001954 RID: 6484
		private readonly TableChangeProcessor _processor;

		// Token: 0x04001955 RID: 6485
		private readonly List<KeyValuePair<int, DbSetClause>> _inputIdentifiers;

		// Token: 0x04001956 RID: 6486
		private readonly Dictionary<int, string> _outputIdentifiers;

		// Token: 0x04001957 RID: 6487
		private readonly DbModificationCommandTree _modificationCommandTree;
	}
}
