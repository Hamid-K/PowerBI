using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000066 RID: 102
	internal sealed class SqlCommandSet
	{
		// Token: 0x0600092A RID: 2346 RVA: 0x0001760E File Offset: 0x0001580E
		internal SqlCommandSet()
		{
			this._batchCommand = new SqlCommand();
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0001763C File Offset: 0x0001583C
		private SqlCommand BatchCommand
		{
			get
			{
				SqlCommand batchCommand = this._batchCommand;
				if (batchCommand == null)
				{
					throw ADP.ObjectDisposed(this);
				}
				return batchCommand;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0001765B File Offset: 0x0001585B
		internal int CommandCount
		{
			get
			{
				return this.CommandList.Count;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x00017668 File Offset: 0x00015868
		private List<SqlCommandSet.LocalCommand> CommandList
		{
			get
			{
				List<SqlCommandSet.LocalCommand> commandList = this._commandList;
				if (commandList == null)
				{
					throw ADP.ObjectDisposed(this);
				}
				return commandList;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x00017687 File Offset: 0x00015887
		internal int CommandTimeout
		{
			set
			{
				this.BatchCommand.CommandTimeout = value;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00017695 File Offset: 0x00015895
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x000176A2 File Offset: 0x000158A2
		internal SqlConnection Connection
		{
			get
			{
				return this.BatchCommand.Connection;
			}
			set
			{
				this.BatchCommand.Connection = value;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x000176B0 File Offset: 0x000158B0
		internal SqlTransaction Transaction
		{
			set
			{
				this.BatchCommand.Transaction = value;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x000176BE File Offset: 0x000158BE
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x000176C8 File Offset: 0x000158C8
		internal void Append(SqlCommand command)
		{
			ADP.CheckArgumentNull(command, "command");
			SqlClientEventSource.Log.TryTraceEvent<int, int, int>("SqlCommandSet.Append | API | Object Id {0}, Command '{1}', Parameter Count {2}", this.ObjectID, command.ObjectID, command.Parameters.Count);
			string commandText = command.CommandText;
			if (string.IsNullOrEmpty(commandText))
			{
				throw ADP.CommandTextRequired("Append");
			}
			CommandType commandType = command.CommandType;
			if (commandType == CommandType.Text || commandType == CommandType.StoredProcedure)
			{
				SqlParameterCollection sqlParameterCollection = null;
				SqlParameterCollection parameters = command.Parameters;
				if (0 < parameters.Count)
				{
					sqlParameterCollection = new SqlParameterCollection();
					for (int i = 0; i < parameters.Count; i++)
					{
						SqlParameter sqlParameter = new SqlParameter();
						parameters[i].CopyTo(sqlParameter);
						sqlParameterCollection.Add(sqlParameter);
						if (!SqlCommandSet.s_sqlIdentifierParser.IsMatch(sqlParameter.ParameterName))
						{
							throw ADP.BadParameterName(sqlParameter.ParameterName);
						}
					}
					foreach (object obj in sqlParameterCollection)
					{
						SqlParameter sqlParameter2 = (SqlParameter)obj;
						object value = sqlParameter2.Value;
						byte[] array = value as byte[];
						if (array != null)
						{
							int offset = sqlParameter2.Offset;
							int size = sqlParameter2.Size;
							int num = array.Length - offset;
							if (size > 0 && size < num)
							{
								num = size;
							}
							byte[] array2 = new byte[Math.Max(num, 0)];
							Buffer.BlockCopy(array, offset, array2, 0, array2.Length);
							sqlParameter2.Offset = 0;
							sqlParameter2.Value = array2;
						}
						else
						{
							char[] array3 = value as char[];
							if (array3 != null)
							{
								int offset2 = sqlParameter2.Offset;
								int size2 = sqlParameter2.Size;
								int num2 = array3.Length - offset2;
								if (size2 != 0 && size2 < num2)
								{
									num2 = size2;
								}
								char[] array4 = new char[Math.Max(num2, 0)];
								Buffer.BlockCopy(array3, offset2, array4, 0, array4.Length * 2);
								sqlParameter2.Offset = 0;
								sqlParameter2.Value = array4;
							}
							else
							{
								ICloneable cloneable = value as ICloneable;
								if (cloneable != null)
								{
									sqlParameter2.Value = cloneable.Clone();
								}
							}
						}
					}
				}
				int num3 = -1;
				if (sqlParameterCollection != null)
				{
					for (int j = 0; j < sqlParameterCollection.Count; j++)
					{
						if (ParameterDirection.ReturnValue == sqlParameterCollection[j].Direction)
						{
							num3 = j;
							break;
						}
					}
				}
				SqlCommandSet.LocalCommand localCommand = new SqlCommandSet.LocalCommand(commandText, sqlParameterCollection, num3, command.CommandType, command.ColumnEncryptionSetting);
				this.CommandList.Add(localCommand);
				return;
			}
			if (commandType == CommandType.TableDirect)
			{
				throw SQL.NotSupportedCommandType(commandType);
			}
			throw ADP.InvalidCommandType(commandType);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00017960 File Offset: 0x00015B60
		internal static void BuildStoredProcedureName(StringBuilder builder, string part)
		{
			if (part != null && 0 < part.Length)
			{
				if ('[' == part[0])
				{
					int num = 0;
					foreach (char c in part)
					{
						if (']' == c)
						{
							num++;
						}
					}
					if (1 == num % 2)
					{
						builder.Append(part);
						return;
					}
				}
				SqlServerEscapeHelper.EscapeIdentifier(builder, part);
			}
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x000179C0 File Offset: 0x00015BC0
		internal void Clear()
		{
			SqlClientEventSource.Log.TryTraceEvent<int>("SqlCommandSet.Clear | API | Object Id {0}", this.ObjectID);
			DbCommand batchCommand = this.BatchCommand;
			if (batchCommand != null)
			{
				batchCommand.Parameters.Clear();
				batchCommand.CommandText = null;
			}
			List<SqlCommandSet.LocalCommand> commandList = this._commandList;
			if (commandList != null)
			{
				commandList.Clear();
			}
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00017A10 File Offset: 0x00015C10
		internal void Dispose()
		{
			SqlClientEventSource.Log.TryTraceEvent<int>("SqlCommandSet.Dispose | API | Object Id {0}", this.ObjectID);
			SqlCommand batchCommand = this._batchCommand;
			this._commandList = null;
			this._batchCommand = null;
			if (batchCommand != null)
			{
				batchCommand.Dispose();
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00017A50 File Offset: 0x00015C50
		internal int ExecuteNonQuery()
		{
			SqlConnection.ExecutePermission.Demand();
			int num;
			using (TryEventScope.Create<int>("SqlCommandSet.ExecuteNonQuery | API | Object Id {0}, Commands executed in Batch RPC mode", this.ObjectID))
			{
				if (this.Connection.IsContextConnection)
				{
					throw SQL.BatchedUpdatesNotAvailableOnContextConnection();
				}
				this.ValidateCommandBehavior("ExecuteNonQuery", CommandBehavior.Default);
				this.BatchCommand.BatchRPCMode = true;
				this.BatchCommand.ClearBatchCommand();
				this.BatchCommand.Parameters.Clear();
				for (int i = 0; i < this._commandList.Count; i++)
				{
					SqlCommandSet.LocalCommand localCommand = this._commandList[i];
					this.BatchCommand.AddBatchCommand(localCommand._commandText, localCommand._parameters, localCommand._cmdType, localCommand._columnEncryptionSetting);
				}
				num = this.BatchCommand.ExecuteBatchRPCCommand();
			}
			return num;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00017B2C File Offset: 0x00015D2C
		internal SqlParameter GetParameter(int commandIndex, int parameterIndex)
		{
			return this.CommandList[commandIndex]._parameters[parameterIndex];
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00017B48 File Offset: 0x00015D48
		internal bool GetBatchedAffected(int commandIdentifier, out int recordsAffected, out Exception error)
		{
			error = this.BatchCommand.GetErrors(commandIdentifier);
			int? recordsAffected2 = this.BatchCommand.GetRecordsAffected(commandIdentifier);
			recordsAffected = recordsAffected2.GetValueOrDefault();
			return recordsAffected2 != null;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00017B80 File Offset: 0x00015D80
		internal int GetParameterCount(int commandIndex)
		{
			return this.CommandList[commandIndex]._parameters.Count;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00017B98 File Offset: 0x00015D98
		private void ValidateCommandBehavior(string method, CommandBehavior behavior)
		{
			if ((behavior & ~(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection)) != CommandBehavior.Default)
			{
				ADP.ValidateCommandBehavior(behavior);
				throw ADP.NotSupportedCommandBehavior(behavior & ~(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection), method);
			}
		}

		// Token: 0x0400017C RID: 380
		private const string SqlIdentifierPattern = "^@[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}\uff3f_@#\\$]*$";

		// Token: 0x0400017D RID: 381
		private static readonly Regex s_sqlIdentifierParser = new Regex("^@[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}\uff3f_@#\\$]*$", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x0400017E RID: 382
		private List<SqlCommandSet.LocalCommand> _commandList = new List<SqlCommandSet.LocalCommand>();

		// Token: 0x0400017F RID: 383
		private SqlCommand _batchCommand;

		// Token: 0x04000180 RID: 384
		private static int s_objectTypeCount;

		// Token: 0x04000181 RID: 385
		internal readonly int _objectID = Interlocked.Increment(ref SqlCommandSet.s_objectTypeCount);

		// Token: 0x020001BD RID: 445
		private sealed class LocalCommand
		{
			// Token: 0x06001D95 RID: 7573 RVA: 0x0007A1A4 File Offset: 0x000783A4
			internal LocalCommand(string commandText, SqlParameterCollection parameters, int returnParameterIndex, CommandType cmdType, SqlCommandColumnEncryptionSetting columnEncryptionSetting)
			{
				this._commandText = commandText;
				this._parameters = parameters;
				this._returnParameterIndex = returnParameterIndex;
				this._cmdType = cmdType;
				this._columnEncryptionSetting = columnEncryptionSetting;
			}

			// Token: 0x04001329 RID: 4905
			internal readonly string _commandText;

			// Token: 0x0400132A RID: 4906
			internal readonly SqlParameterCollection _parameters;

			// Token: 0x0400132B RID: 4907
			internal readonly int _returnParameterIndex;

			// Token: 0x0400132C RID: 4908
			internal readonly CommandType _cmdType;

			// Token: 0x0400132D RID: 4909
			internal readonly SqlCommandColumnEncryptionSetting _columnEncryptionSetting;
		}
	}
}
