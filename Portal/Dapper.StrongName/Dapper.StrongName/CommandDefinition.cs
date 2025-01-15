using System;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace Dapper
{
	// Token: 0x02000003 RID: 3
	public struct CommandDefinition
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal static CommandDefinition ForCallback(object parameters)
		{
			if (parameters is DynamicParameters)
			{
				return new CommandDefinition(parameters);
			}
			return default(CommandDefinition);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002075 File Offset: 0x00000275
		internal void OnCompleted()
		{
			SqlMapper.IParameterCallbacks parameterCallbacks = this.Parameters as SqlMapper.IParameterCallbacks;
			if (parameterCallbacks == null)
			{
				return;
			}
			parameterCallbacks.OnCompleted();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000208C File Offset: 0x0000028C
		public string CommandText { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002094 File Offset: 0x00000294
		public object Parameters { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000209C File Offset: 0x0000029C
		public IDbTransaction Transaction { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020A4 File Offset: 0x000002A4
		public int? CommandTimeout { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020AC File Offset: 0x000002AC
		public CommandType? CommandType { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020B4 File Offset: 0x000002B4
		public bool Buffered
		{
			get
			{
				return (this.Flags & CommandFlags.Buffered) > CommandFlags.None;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020C1 File Offset: 0x000002C1
		internal bool AddToCache
		{
			get
			{
				return (this.Flags & CommandFlags.NoCache) == CommandFlags.None;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020CE File Offset: 0x000002CE
		public CommandFlags Flags { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020D6 File Offset: 0x000002D6
		public bool Pipelined
		{
			get
			{
				return (this.Flags & CommandFlags.Pipelined) > CommandFlags.None;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020E3 File Offset: 0x000002E3
		public CommandDefinition(string commandText, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CommandText = commandText;
			this.Parameters = parameters;
			this.Transaction = transaction;
			this.CommandTimeout = commandTimeout;
			this.CommandType = commandType;
			this.Flags = flags;
			this.CancellationToken = cancellationToken;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000211A File Offset: 0x0000031A
		private CommandDefinition(object parameters)
		{
			this = default(CommandDefinition);
			this.Parameters = parameters;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000212A File Offset: 0x0000032A
		public CancellationToken CancellationToken { get; }

		// Token: 0x0600000F RID: 15 RVA: 0x00002134 File Offset: 0x00000334
		internal IDbCommand SetupCommand(IDbConnection cnn, Action<IDbCommand, object> paramReader)
		{
			IDbCommand cmd = cnn.CreateCommand();
			Action<IDbCommand> init = CommandDefinition.GetInit(cmd.GetType());
			if (init != null)
			{
				init(cmd);
			}
			if (this.Transaction != null)
			{
				cmd.Transaction = this.Transaction;
			}
			cmd.CommandText = this.CommandText;
			if (this.CommandTimeout != null)
			{
				cmd.CommandTimeout = this.CommandTimeout.Value;
			}
			else if (SqlMapper.Settings.CommandTimeout != null)
			{
				cmd.CommandTimeout = SqlMapper.Settings.CommandTimeout.Value;
			}
			if (this.CommandType != null)
			{
				cmd.CommandType = this.CommandType.Value;
			}
			if (paramReader != null)
			{
				paramReader(cmd, this.Parameters);
			}
			return cmd;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021FC File Offset: 0x000003FC
		private static Action<IDbCommand> GetInit(Type commandType)
		{
			if (commandType == null)
			{
				return null;
			}
			Action<IDbCommand> action;
			if (SqlMapper.Link<Type, Action<IDbCommand>>.TryGet(CommandDefinition.commandInitCache, commandType, out action))
			{
				return action;
			}
			MethodInfo bindByName = CommandDefinition.GetBasicPropertySetter(commandType, "BindByName", typeof(bool));
			MethodInfo initialLongFetchSize = CommandDefinition.GetBasicPropertySetter(commandType, "InitialLONGFetchSize", typeof(int));
			action = null;
			if (bindByName != null || initialLongFetchSize != null)
			{
				DynamicMethod method = new DynamicMethod(commandType.Name + "_init", null, new Type[] { typeof(IDbCommand) });
				ILGenerator il = method.GetILGenerator();
				if (bindByName != null)
				{
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Castclass, commandType);
					il.Emit(OpCodes.Ldc_I4_1);
					il.EmitCall(OpCodes.Callvirt, bindByName, null);
				}
				if (initialLongFetchSize != null)
				{
					il.Emit(OpCodes.Ldarg_0);
					il.Emit(OpCodes.Castclass, commandType);
					il.Emit(OpCodes.Ldc_I4_M1);
					il.EmitCall(OpCodes.Callvirt, initialLongFetchSize, null);
				}
				il.Emit(OpCodes.Ret);
				action = (Action<IDbCommand>)method.CreateDelegate(typeof(Action<IDbCommand>));
			}
			SqlMapper.Link<Type, Action<IDbCommand>>.TryAdd(ref CommandDefinition.commandInitCache, commandType, ref action);
			return action;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002344 File Offset: 0x00000544
		private static MethodInfo GetBasicPropertySetter(Type declaringType, string name, Type expectedType)
		{
			PropertyInfo prop = declaringType.GetProperty(name, BindingFlags.Instance | BindingFlags.Public);
			if (prop != null && prop.CanWrite && prop.PropertyType == expectedType && prop.GetIndexParameters().Length == 0)
			{
				return prop.GetSetMethod();
			}
			return null;
		}

		// Token: 0x04000011 RID: 17
		private static SqlMapper.Link<Type, Action<IDbCommand>> commandInitCache;
	}
}
