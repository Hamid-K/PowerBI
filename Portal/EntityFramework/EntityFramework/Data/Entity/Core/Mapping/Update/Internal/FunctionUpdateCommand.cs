using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005C7 RID: 1479
	internal class FunctionUpdateCommand : UpdateCommand
	{
		// Token: 0x0600476A RID: 18282 RVA: 0x000FCD71 File Offset: 0x000FAF71
		internal FunctionUpdateCommand(ModificationFunctionMapping functionMapping, UpdateTranslator translator, ReadOnlyCollection<IEntityStateEntry> stateEntries, ExtractedStateEntry stateEntry)
			: this(translator, stateEntries, stateEntry, translator.GenerateCommandDefinition(functionMapping).CreateCommand())
		{
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x000FCD89 File Offset: 0x000FAF89
		protected FunctionUpdateCommand(UpdateTranslator translator, ReadOnlyCollection<IEntityStateEntry> stateEntries, ExtractedStateEntry stateEntry, DbCommand dbCommand)
			: base(translator, stateEntry.Original, stateEntry.Current)
		{
			this._stateEntries = stateEntries;
			this._dbCommand = new InterceptableDbCommand(dbCommand, translator.InterceptionContext, null);
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x0600476C RID: 18284 RVA: 0x000FCDB9 File Offset: 0x000FAFB9
		// (set) Token: 0x0600476D RID: 18285 RVA: 0x000FCDC1 File Offset: 0x000FAFC1
		protected virtual List<KeyValuePair<string, PropagatorResult>> ResultColumns { get; set; }

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x0600476E RID: 18286 RVA: 0x000FCDCA File Offset: 0x000FAFCA
		internal override IEnumerable<int> InputIdentifiers
		{
			get
			{
				if (this._inputIdentifiers == null)
				{
					yield break;
				}
				foreach (KeyValuePair<int, DbParameter> keyValuePair in this._inputIdentifiers)
				{
					yield return keyValuePair.Key;
				}
				List<KeyValuePair<int, DbParameter>>.Enumerator enumerator = default(List<KeyValuePair<int, DbParameter>>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x0600476F RID: 18287 RVA: 0x000FCDDA File Offset: 0x000FAFDA
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

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06004770 RID: 18288 RVA: 0x000FCDF5 File Offset: 0x000FAFF5
		internal override UpdateCommandKind Kind
		{
			get
			{
				return UpdateCommandKind.Function;
			}
		}

		// Token: 0x06004771 RID: 18289 RVA: 0x000FCDF8 File Offset: 0x000FAFF8
		internal override IList<IEntityStateEntry> GetStateEntries(UpdateTranslator translator)
		{
			return this._stateEntries;
		}

		// Token: 0x06004772 RID: 18290 RVA: 0x000FCE00 File Offset: 0x000FB000
		internal void SetParameterValue(PropagatorResult result, ModificationFunctionParameterBinding parameterBinding, UpdateTranslator translator)
		{
			DbParameter dbParameter = this._dbCommand.Parameters[parameterBinding.Parameter.Name];
			TypeUsage typeUsage = parameterBinding.Parameter.TypeUsage;
			object principalValue = translator.KeyManager.GetPrincipalValue(result);
			translator.SetParameterValue(dbParameter, typeUsage, principalValue);
			int identifier = result.Identifier;
			if (-1 != identifier)
			{
				if (this._inputIdentifiers == null)
				{
					this._inputIdentifiers = new List<KeyValuePair<int, DbParameter>>(2);
				}
				foreach (int num in translator.KeyManager.GetPrincipals(identifier))
				{
					this._inputIdentifiers.Add(new KeyValuePair<int, DbParameter>(num, dbParameter));
				}
			}
		}

		// Token: 0x06004773 RID: 18291 RVA: 0x000FCEC4 File Offset: 0x000FB0C4
		internal void RegisterRowsAffectedParameter(FunctionParameter rowsAffectedParameter)
		{
			if (rowsAffectedParameter != null)
			{
				this._rowsAffectedParameter = this._dbCommand.Parameters[rowsAffectedParameter.Name];
			}
		}

		// Token: 0x06004774 RID: 18292 RVA: 0x000FCEE8 File Offset: 0x000FB0E8
		internal void AddResultColumn(UpdateTranslator translator, string columnName, PropagatorResult result)
		{
			if (this.ResultColumns == null)
			{
				this.ResultColumns = new List<KeyValuePair<string, PropagatorResult>>(2);
			}
			this.ResultColumns.Add(new KeyValuePair<string, PropagatorResult>(columnName, result));
			int identifier = result.Identifier;
			if (-1 != identifier)
			{
				if (translator.KeyManager.HasPrincipals(identifier))
				{
					throw new InvalidOperationException(Strings.Update_GeneratedDependent(columnName));
				}
				this.AddOutputIdentifier(columnName, identifier);
			}
		}

		// Token: 0x06004775 RID: 18293 RVA: 0x000FCF48 File Offset: 0x000FB148
		private void AddOutputIdentifier(string columnName, int identifier)
		{
			if (this._outputIdentifiers == null)
			{
				this._outputIdentifiers = new Dictionary<int, string>(2);
			}
			this._outputIdentifiers[identifier] = columnName;
		}

		// Token: 0x06004776 RID: 18294 RVA: 0x000FCF6C File Offset: 0x000FB16C
		internal virtual void SetInputIdentifiers(Dictionary<int, object> identifierValues)
		{
			if (this._inputIdentifiers != null)
			{
				foreach (KeyValuePair<int, DbParameter> keyValuePair in this._inputIdentifiers)
				{
					object obj;
					if (identifierValues.TryGetValue(keyValuePair.Key, out obj))
					{
						keyValuePair.Value.Value = obj;
					}
				}
			}
		}

		// Token: 0x06004777 RID: 18295 RVA: 0x000FCFE0 File Offset: 0x000FB1E0
		internal override long Execute(Dictionary<int, object> identifierValues, List<KeyValuePair<PropagatorResult, object>> generatedValues)
		{
			EntityConnection connection = base.Translator.Connection;
			this._dbCommand.Transaction = ((connection.CurrentTransaction == null) ? null : connection.CurrentTransaction.StoreTransaction);
			this._dbCommand.Connection = connection.StoreConnection;
			if (base.Translator.CommandTimeout != null)
			{
				this._dbCommand.CommandTimeout = base.Translator.CommandTimeout.Value;
			}
			this.SetInputIdentifiers(identifierValues);
			long num;
			if (this.ResultColumns != null)
			{
				num = 0L;
				IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(base.CurrentValues.StructuralType);
				using (DbDataReader reader = this._dbCommand.ExecuteReader(CommandBehavior.SequentialAccess))
				{
					if (reader.Read())
					{
						num += 1L;
						IEnumerable<KeyValuePair<string, PropagatorResult>> resultColumns = this.ResultColumns;
						Func<KeyValuePair<string, PropagatorResult>, KeyValuePair<int, PropagatorResult>> <>9__0;
						Func<KeyValuePair<string, PropagatorResult>, KeyValuePair<int, PropagatorResult>> func;
						if ((func = <>9__0) == null)
						{
							func = (<>9__0 = (KeyValuePair<string, PropagatorResult> r) => new KeyValuePair<int, PropagatorResult>(this.GetColumnOrdinal(this.Translator, reader, r.Key), r.Value));
						}
						foreach (KeyValuePair<int, PropagatorResult> keyValuePair in from r in resultColumns.Select(func)
							orderby r.Key
							select r)
						{
							int key = keyValuePair.Key;
							if (key == -1)
							{
								break;
							}
							TypeUsage typeUsage = allStructuralMembers[keyValuePair.Value.RecordOrdinal].TypeUsage;
							object obj;
							if (Helper.IsSpatialType(typeUsage) && !reader.IsDBNull(key))
							{
								obj = SpatialHelpers.GetSpatialValue(base.Translator.MetadataWorkspace, reader, typeUsage, key);
							}
							else
							{
								obj = reader.GetValue(key);
							}
							PropagatorResult value = keyValuePair.Value;
							generatedValues.Add(new KeyValuePair<PropagatorResult, object>(value, obj));
							int identifier = value.Identifier;
							if (-1 != identifier)
							{
								identifierValues.Add(identifier, obj);
							}
						}
					}
					CommandHelper.ConsumeReader(reader);
					goto IL_021C;
				}
			}
			num = (long)this._dbCommand.ExecuteNonQuery();
			IL_021C:
			return this.GetRowsAffected(num, base.Translator);
		}

		// Token: 0x06004778 RID: 18296 RVA: 0x000FD24C File Offset: 0x000FB44C
		internal override async Task<long> ExecuteAsync(Dictionary<int, object> identifierValues, List<KeyValuePair<PropagatorResult, object>> generatedValues, CancellationToken cancellationToken)
		{
			FunctionUpdateCommand.<>c__DisplayClass24_0 CS$<>8__locals1 = new FunctionUpdateCommand.<>c__DisplayClass24_0();
			CS$<>8__locals1.<>4__this = this;
			cancellationToken.ThrowIfCancellationRequested();
			EntityConnection connection = base.Translator.Connection;
			this._dbCommand.Transaction = ((connection.CurrentTransaction == null) ? null : connection.CurrentTransaction.StoreTransaction);
			this._dbCommand.Connection = connection.StoreConnection;
			if (base.Translator.CommandTimeout != null)
			{
				this._dbCommand.CommandTimeout = base.Translator.CommandTimeout.Value;
			}
			this.SetInputIdentifiers(identifierValues);
			long rowsAffected;
			if (this.ResultColumns != null)
			{
				rowsAffected = 0L;
				IBaseList<EdmMember> members = TypeHelpers.GetAllStructuralMembers(base.CurrentValues.StructuralType);
				DbDataReader dbDataReader = await this._dbCommand.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken).WithCurrentCulture<DbDataReader>();
				CS$<>8__locals1.reader = dbDataReader;
				try
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = CS$<>8__locals1.reader.ReadAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (cultureAwaiter.GetResult())
					{
						rowsAffected += 1L;
						IEnumerable<KeyValuePair<string, PropagatorResult>> resultColumns = this.ResultColumns;
						Func<KeyValuePair<string, PropagatorResult>, KeyValuePair<int, PropagatorResult>> func;
						if ((func = CS$<>8__locals1.<>9__0) == null)
						{
							FunctionUpdateCommand.<>c__DisplayClass24_0 CS$<>8__locals2 = CS$<>8__locals1;
							Func<KeyValuePair<string, PropagatorResult>, KeyValuePair<int, PropagatorResult>> func2 = (KeyValuePair<string, PropagatorResult> r) => new KeyValuePair<int, PropagatorResult>(CS$<>8__locals1.<>4__this.GetColumnOrdinal(CS$<>8__locals1.<>4__this.Translator, CS$<>8__locals1.reader, r.Key), r.Value);
							CS$<>8__locals2.<>9__0 = func2;
							func = func2;
						}
						foreach (KeyValuePair<int, PropagatorResult> resultColumn in from r in resultColumns.Select(func)
							orderby r.Key
							select r)
						{
							int columnOrdinal = resultColumn.Key;
							TypeUsage columnType = members[resultColumn.Value.RecordOrdinal].TypeUsage;
							bool flag = Helper.IsSpatialType(columnType);
							if (flag)
							{
								cultureAwaiter = CS$<>8__locals1.reader.IsDBNullAsync(columnOrdinal, cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
								if (!cultureAwaiter.IsCompleted)
								{
									await cultureAwaiter;
									cultureAwaiter = cultureAwaiter2;
									cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
								}
								flag = !cultureAwaiter.GetResult();
							}
							object obj = ((!flag) ? (await CS$<>8__locals1.reader.GetFieldValueAsync<object>(columnOrdinal, cancellationToken).WithCurrentCulture<object>()) : (await SpatialHelpers.GetSpatialValueAsync(base.Translator.MetadataWorkspace, CS$<>8__locals1.reader, columnType, columnOrdinal, cancellationToken).WithCurrentCulture<object>()));
							PropagatorResult value = resultColumn.Value;
							generatedValues.Add(new KeyValuePair<PropagatorResult, object>(value, obj));
							int identifier = value.Identifier;
							if (-1 != identifier)
							{
								identifierValues.Add(identifier, obj);
							}
							columnType = null;
							resultColumn = default(KeyValuePair<int, PropagatorResult>);
						}
						IEnumerator<KeyValuePair<int, PropagatorResult>> enumerator = null;
					}
					await CommandHelper.ConsumeReaderAsync(CS$<>8__locals1.reader, cancellationToken).WithCurrentCulture();
				}
				finally
				{
					if (CS$<>8__locals1.reader != null)
					{
						((IDisposable)CS$<>8__locals1.reader).Dispose();
					}
				}
				members = null;
			}
			else
			{
				rowsAffected = await this._dbCommand.ExecuteNonQueryAsync(cancellationToken).WithCurrentCulture<int>();
			}
			return this.GetRowsAffected(rowsAffected, base.Translator);
		}

		// Token: 0x06004779 RID: 18297 RVA: 0x000FD2AC File Offset: 0x000FB4AC
		protected virtual long GetRowsAffected(long rowsAffected, UpdateTranslator translator)
		{
			if (this._rowsAffectedParameter != null)
			{
				if (DBNull.Value.Equals(this._rowsAffectedParameter.Value))
				{
					rowsAffected = 0L;
				}
				else
				{
					try
					{
						rowsAffected = Convert.ToInt64(this._rowsAffectedParameter.Value, CultureInfo.InvariantCulture);
					}
					catch (Exception ex)
					{
						if (ex.RequiresContext())
						{
							throw new UpdateException(Strings.Update_UnableToConvertRowsAffectedParameter(this._rowsAffectedParameter.ParameterName, typeof(long).FullName), ex, this.GetStateEntries(translator).Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
						}
						throw;
					}
				}
			}
			return rowsAffected;
		}

		// Token: 0x0600477A RID: 18298 RVA: 0x000FD34C File Offset: 0x000FB54C
		private int GetColumnOrdinal(UpdateTranslator translator, DbDataReader reader, string columnName)
		{
			int ordinal;
			try
			{
				ordinal = reader.GetOrdinal(columnName);
			}
			catch (IndexOutOfRangeException)
			{
				throw new UpdateException(Strings.Update_MissingResultColumn(columnName), null, this.GetStateEntries(translator).Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
			}
			return ordinal;
		}

		// Token: 0x0600477B RID: 18299 RVA: 0x000FD394 File Offset: 0x000FB594
		private static ModificationOperator GetModificationOperator(EntityState state)
		{
			if (state <= EntityState.Added)
			{
				if (state != EntityState.Unchanged)
				{
					if (state != EntityState.Added)
					{
						return ModificationOperator.Update;
					}
					return ModificationOperator.Insert;
				}
			}
			else if (state == EntityState.Deleted)
			{
				return ModificationOperator.Delete;
			}
			return ModificationOperator.Update;
		}

		// Token: 0x0600477C RID: 18300 RVA: 0x000FD3B4 File Offset: 0x000FB5B4
		internal override int CompareToType(UpdateCommand otherCommand)
		{
			FunctionUpdateCommand functionUpdateCommand = (FunctionUpdateCommand)otherCommand;
			IEntityStateEntry entityStateEntry = this._stateEntries[0];
			IEntityStateEntry entityStateEntry2 = functionUpdateCommand._stateEntries[0];
			int num = (int)(FunctionUpdateCommand.GetModificationOperator(entityStateEntry.State) - FunctionUpdateCommand.GetModificationOperator(entityStateEntry2.State));
			if (num != 0)
			{
				return num;
			}
			num = StringComparer.Ordinal.Compare(entityStateEntry.EntitySet.Name, entityStateEntry2.EntitySet.Name);
			if (num != 0)
			{
				return num;
			}
			num = StringComparer.Ordinal.Compare(entityStateEntry.EntitySet.EntityContainer.Name, entityStateEntry2.EntitySet.EntityContainer.Name);
			if (num != 0)
			{
				return num;
			}
			int num2 = ((this._inputIdentifiers == null) ? 0 : this._inputIdentifiers.Count);
			int num3 = ((functionUpdateCommand._inputIdentifiers == null) ? 0 : functionUpdateCommand._inputIdentifiers.Count);
			num = num2 - num3;
			if (num != 0)
			{
				return num;
			}
			for (int i = 0; i < num2; i++)
			{
				DbParameter value = this._inputIdentifiers[i].Value;
				DbParameter value2 = functionUpdateCommand._inputIdentifiers[i].Value;
				num = ByValueComparer.Default.Compare(value.Value, value2.Value);
				if (num != 0)
				{
					return num;
				}
			}
			for (int j = 0; j < num2; j++)
			{
				int key = this._inputIdentifiers[j].Key;
				int key2 = functionUpdateCommand._inputIdentifiers[j].Key;
				num = key - key2;
				if (num != 0)
				{
					return num;
				}
			}
			return num;
		}

		// Token: 0x0400195F RID: 6495
		private readonly ReadOnlyCollection<IEntityStateEntry> _stateEntries;

		// Token: 0x04001960 RID: 6496
		private readonly DbCommand _dbCommand;

		// Token: 0x04001961 RID: 6497
		private List<KeyValuePair<int, DbParameter>> _inputIdentifiers;

		// Token: 0x04001962 RID: 6498
		private Dictionary<int, string> _outputIdentifiers;

		// Token: 0x04001963 RID: 6499
		private DbParameter _rowsAffectedParameter;
	}
}
