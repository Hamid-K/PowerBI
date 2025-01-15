using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace Dapper
{
	// Token: 0x02000009 RID: 9
	public class DynamicParameters : SqlMapper.IDynamicParameters, SqlMapper.IParameterLookup, SqlMapper.IParameterCallbacks
	{
		// Token: 0x17000012 RID: 18
		object SqlMapper.IParameterLookup.this[string name]
		{
			get
			{
				DynamicParameters.ParamInfo param;
				if (!this.parameters.TryGetValue(name, out param))
				{
					return null;
				}
				return param.Value;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002B21 File Offset: 0x00000D21
		public DynamicParameters()
		{
			this.RemoveUnused = true;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002B3B File Offset: 0x00000D3B
		public DynamicParameters(object template)
		{
			this.RemoveUnused = true;
			this.AddDynamicParams(template);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002B5C File Offset: 0x00000D5C
		public void AddDynamicParams(object param)
		{
			if (param != null)
			{
				DynamicParameters subDynamic = param as DynamicParameters;
				if (subDynamic == null)
				{
					IEnumerable<KeyValuePair<string, object>> dictionary = param as IEnumerable<KeyValuePair<string, object>>;
					if (dictionary == null)
					{
						this.templates = this.templates ?? new List<object>();
						this.templates.Add(param);
						return;
					}
					using (IEnumerator<KeyValuePair<string, object>> enumerator = dictionary.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, object> kvp = enumerator.Current;
							this.Add(kvp.Key, kvp.Value, null, null, null);
						}
						return;
					}
				}
				if (subDynamic.parameters != null)
				{
					foreach (KeyValuePair<string, DynamicParameters.ParamInfo> kvp2 in subDynamic.parameters)
					{
						this.parameters.Add(kvp2.Key, kvp2.Value);
					}
				}
				if (subDynamic.templates != null)
				{
					this.templates = this.templates ?? new List<object>();
					foreach (object t in subDynamic.templates)
					{
						this.templates.Add(t);
					}
				}
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002CDC File Offset: 0x00000EDC
		public void Add(string name, object value, DbType? dbType, ParameterDirection? direction, int? size)
		{
			this.parameters[DynamicParameters.Clean(name)] = new DynamicParameters.ParamInfo
			{
				Name = name,
				Value = value,
				ParameterDirection = (direction ?? ParameterDirection.Input),
				DbType = dbType,
				Size = size
			};
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002D38 File Offset: 0x00000F38
		public void Add(string name, object value = null, DbType? dbType = null, ParameterDirection? direction = null, int? size = null, byte? precision = null, byte? scale = null)
		{
			this.parameters[DynamicParameters.Clean(name)] = new DynamicParameters.ParamInfo
			{
				Name = name,
				Value = value,
				ParameterDirection = (direction ?? ParameterDirection.Input),
				DbType = dbType,
				Size = size,
				Precision = precision,
				Scale = scale
			};
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002DA4 File Offset: 0x00000FA4
		private static string Clean(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				char c = name[0];
				if (c == ':' || c == '?' || c == '@')
				{
					return name.Substring(1);
				}
			}
			return name;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002DD9 File Offset: 0x00000FD9
		void SqlMapper.IDynamicParameters.AddParameters(IDbCommand command, SqlMapper.Identity identity)
		{
			this.AddParameters(command, identity);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002DE3 File Offset: 0x00000FE3
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002DEB File Offset: 0x00000FEB
		public bool RemoveUnused { get; set; }

		// Token: 0x0600003B RID: 59 RVA: 0x00002DF4 File Offset: 0x00000FF4
		protected void AddParameters(IDbCommand command, SqlMapper.Identity identity)
		{
			IList<SqlMapper.LiteralToken> literals = SqlMapper.GetLiteralTokens(identity.sql);
			if (this.templates != null)
			{
				foreach (object template in this.templates)
				{
					SqlMapper.Identity newIdent = identity.ForDynamicParameters(template.GetType());
					Dictionary<SqlMapper.Identity, Action<IDbCommand, object>> dictionary = DynamicParameters.paramReaderCache;
					Action<IDbCommand, object> appender;
					lock (dictionary)
					{
						if (!DynamicParameters.paramReaderCache.TryGetValue(newIdent, out appender))
						{
							appender = SqlMapper.CreateParamInfoGenerator(newIdent, true, this.RemoveUnused, literals);
							DynamicParameters.paramReaderCache[newIdent] = appender;
						}
					}
					appender(command, template);
				}
				foreach (object obj in command.Parameters)
				{
					IDbDataParameter param = (IDbDataParameter)obj;
					if (!this.parameters.ContainsKey(param.ParameterName))
					{
						this.parameters.Add(param.ParameterName, new DynamicParameters.ParamInfo
						{
							AttachedParam = param,
							CameFromTemplate = true,
							DbType = new DbType?(param.DbType),
							Name = param.ParameterName,
							ParameterDirection = param.Direction,
							Size = new int?(param.Size),
							Value = param.Value
						});
					}
				}
				List<Action> tmp = this.outputCallbacks;
				if (tmp != null)
				{
					foreach (Action generator in tmp)
					{
						generator();
					}
				}
			}
			foreach (DynamicParameters.ParamInfo param2 in this.parameters.Values)
			{
				if (!param2.CameFromTemplate)
				{
					DbType? dbType = param2.DbType;
					object val = param2.Value;
					string name = DynamicParameters.Clean(param2.Name);
					bool isCustomQueryParameter = val is SqlMapper.ICustomQueryParameter;
					SqlMapper.ITypeHandler handler = null;
					if (dbType == null && val != null && !isCustomQueryParameter)
					{
						dbType = new DbType?(SqlMapper.LookupDbType(val.GetType(), name, true, out handler));
					}
					if (isCustomQueryParameter)
					{
						((SqlMapper.ICustomQueryParameter)val).AddParameter(command, name);
					}
					else
					{
						DbType? dbType2 = dbType;
						DbType dbType3 = (DbType)(-1);
						if ((dbType2.GetValueOrDefault() == dbType3) & (dbType2 != null))
						{
							SqlMapper.PackListParameters(command, name, val);
						}
						else
						{
							bool add = !command.Parameters.Contains(name);
							IDbDataParameter p;
							if (add)
							{
								p = command.CreateParameter();
								p.ParameterName = name;
							}
							else
							{
								p = (IDbDataParameter)command.Parameters[name];
							}
							p.Direction = param2.ParameterDirection;
							if (handler == null)
							{
								p.Value = SqlMapper.SanitizeParameterValue(val);
								if (dbType != null)
								{
									DbType dbType4 = p.DbType;
									dbType2 = dbType;
									if (!((dbType4 == dbType2.GetValueOrDefault()) & (dbType2 != null)))
									{
										p.DbType = dbType.Value;
									}
								}
								string s = val as string;
								if (s != null && s.Length <= 4000)
								{
									p.Size = 4000;
								}
								if (param2.Size != null)
								{
									p.Size = param2.Size.Value;
								}
								if (param2.Precision != null)
								{
									p.Precision = param2.Precision.Value;
								}
								if (param2.Scale != null)
								{
									p.Scale = param2.Scale.Value;
								}
							}
							else
							{
								if (dbType != null)
								{
									p.DbType = dbType.Value;
								}
								if (param2.Size != null)
								{
									p.Size = param2.Size.Value;
								}
								if (param2.Precision != null)
								{
									p.Precision = param2.Precision.Value;
								}
								if (param2.Scale != null)
								{
									p.Scale = param2.Scale.Value;
								}
								handler.SetValue(p, val ?? DBNull.Value);
							}
							if (add)
							{
								command.Parameters.Add(p);
							}
							param2.AttachedParam = p;
						}
					}
				}
			}
			if (literals.Count != 0)
			{
				SqlMapper.ReplaceLiterals(this, command, literals);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003324 File Offset: 0x00001524
		public IEnumerable<string> ParameterNames
		{
			get
			{
				return this.parameters.Select((KeyValuePair<string, DynamicParameters.ParamInfo> p) => p.Key);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003350 File Offset: 0x00001550
		public T Get<T>(string name)
		{
			DynamicParameters.ParamInfo paramInfo = this.parameters[DynamicParameters.Clean(name)];
			IDbDataParameter attachedParam = paramInfo.AttachedParam;
			object val = ((attachedParam == null) ? paramInfo.Value : attachedParam.Value);
			if (val != DBNull.Value)
			{
				return (T)((object)val);
			}
			if (default(T) != null)
			{
				throw new ApplicationException("Attempting to cast a DBNull to a non nullable type! Note that out/return parameters will not have updated values until the data stream completes (after the 'foreach' for Query(..., buffered: false), or after the GridReader has been disposed for QueryMultiple)");
			}
			return default(T);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000033BC File Offset: 0x000015BC
		public DynamicParameters Output<T>(T target, Expression<Func<T, object>> expression, DbType? dbType = null, int? size = null)
		{
			string failMessage = "Expression must be a property/field chain off of a(n) {0} instance";
			failMessage = string.Format(failMessage, typeof(T).Name);
			Action @throw = delegate
			{
				throw new InvalidOperationException(failMessage);
			};
			MemberExpression lastMemberAccess = expression.Body as MemberExpression;
			if (lastMemberAccess == null || (!(lastMemberAccess.Member is PropertyInfo) && !(lastMemberAccess.Member is FieldInfo)))
			{
				if (expression.Body.NodeType == ExpressionType.Convert && expression.Body.Type == typeof(object) && ((UnaryExpression)expression.Body).Operand is MemberExpression)
				{
					lastMemberAccess = (MemberExpression)((UnaryExpression)expression.Body).Operand;
				}
				else
				{
					@throw();
				}
			}
			MemberExpression diving = lastMemberAccess;
			List<string> names = new List<string>();
			List<MemberExpression> chain = new List<MemberExpression>();
			do
			{
				names.Insert(0, (diving != null) ? diving.Member.Name : null);
				chain.Insert(0, diving);
				ParameterExpression constant = ((diving != null) ? diving.Expression : null) as ParameterExpression;
				diving = ((diving != null) ? diving.Expression : null) as MemberExpression;
				if (constant != null && constant.Type == typeof(T))
				{
					break;
				}
				if (diving == null || (!(diving.Member is PropertyInfo) && !(diving.Member is FieldInfo)))
				{
					@throw();
				}
			}
			while (diving != null);
			string dynamicParamName = string.Concat(names.ToArray());
			string lookup = string.Join("|", names.ToArray());
			Hashtable cache = DynamicParameters.CachedOutputSetters<T>.Cache;
			Action<object, DynamicParameters> setter = (Action<object, DynamicParameters>)cache[lookup];
			if (setter == null)
			{
				DynamicMethod dm = new DynamicMethod("ExpressionParam" + Guid.NewGuid().ToString(), null, new Type[]
				{
					typeof(object),
					base.GetType()
				}, true);
				ILGenerator il = dm.GetILGenerator();
				il.Emit(OpCodes.Ldarg_0);
				il.Emit(OpCodes.Castclass, typeof(T));
				for (int i = 0; i < chain.Count - 1; i++)
				{
					MemberInfo member = chain[0].Member;
					if (member is PropertyInfo)
					{
						MethodInfo get = ((PropertyInfo)member).GetGetMethod(true);
						il.Emit(OpCodes.Callvirt, get);
					}
					else
					{
						il.Emit(OpCodes.Ldfld, (FieldInfo)member);
					}
				}
				MethodInfo paramGetter = base.GetType().GetMethod("Get", new Type[] { typeof(string) }).MakeGenericMethod(new Type[] { lastMemberAccess.Type });
				il.Emit(OpCodes.Ldarg_1);
				il.Emit(OpCodes.Ldstr, dynamicParamName);
				il.Emit(OpCodes.Callvirt, paramGetter);
				MemberInfo lastMember = lastMemberAccess.Member;
				if (lastMember is PropertyInfo)
				{
					MethodInfo set = ((PropertyInfo)lastMember).GetSetMethod(true);
					il.Emit(OpCodes.Callvirt, set);
				}
				else
				{
					il.Emit(OpCodes.Stfld, (FieldInfo)lastMember);
				}
				il.Emit(OpCodes.Ret);
				setter = (Action<object, DynamicParameters>)dm.CreateDelegate(typeof(Action<object, DynamicParameters>));
				Hashtable hashtable = cache;
				lock (hashtable)
				{
					cache[lookup] = setter;
				}
			}
			List<Action> list;
			if ((list = this.outputCallbacks) == null)
			{
				list = (this.outputCallbacks = new List<Action>());
			}
			list.Add(delegate
			{
				MemberExpression lastMemberAccess2 = lastMemberAccess;
				Type targetMemberType = ((lastMemberAccess2 != null) ? lastMemberAccess2.Type : null);
				int sizeToSet = ((size == null && targetMemberType == typeof(string)) ? 4000 : (size ?? 0));
				DynamicParameters.ParamInfo parameter;
				if (this.parameters.TryGetValue(dynamicParamName, out parameter))
				{
					parameter.ParameterDirection = (parameter.AttachedParam.Direction = ParameterDirection.InputOutput);
					if (parameter.AttachedParam.Size == 0)
					{
						parameter.Size = new int?(parameter.AttachedParam.Size = sizeToSet);
					}
				}
				else
				{
					SqlMapper.ITypeHandler handler;
					dbType = ((dbType == null) ? new DbType?(SqlMapper.LookupDbType(targetMemberType, (targetMemberType != null) ? targetMemberType.Name : null, true, out handler)) : dbType);
					this.Add(dynamicParamName, expression.Compile()(target), null, new ParameterDirection?(ParameterDirection.InputOutput), new int?(sizeToSet));
				}
				parameter = this.parameters[dynamicParamName];
				parameter.OutputCallback = setter;
				parameter.OutputTarget = target;
			});
			return this;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000037E4 File Offset: 0x000019E4
		void SqlMapper.IParameterCallbacks.OnCompleted()
		{
			foreach (DynamicParameters.ParamInfo param in this.parameters.Select(delegate(KeyValuePair<string, DynamicParameters.ParamInfo> p)
			{
				KeyValuePair<string, DynamicParameters.ParamInfo> keyValuePair = p;
				return keyValuePair.Value;
			}))
			{
				Action<object, DynamicParameters> outputCallback = param.OutputCallback;
				if (outputCallback != null)
				{
					outputCallback(param.OutputTarget, this);
				}
			}
		}

		// Token: 0x04000023 RID: 35
		internal const DbType EnumerableMultiParameter = (DbType)(-1);

		// Token: 0x04000024 RID: 36
		private static readonly Dictionary<SqlMapper.Identity, Action<IDbCommand, object>> paramReaderCache = new Dictionary<SqlMapper.Identity, Action<IDbCommand, object>>();

		// Token: 0x04000025 RID: 37
		private readonly Dictionary<string, DynamicParameters.ParamInfo> parameters = new Dictionary<string, DynamicParameters.ParamInfo>();

		// Token: 0x04000026 RID: 38
		private List<object> templates;

		// Token: 0x04000028 RID: 40
		private List<Action> outputCallbacks;

		// Token: 0x0200001D RID: 29
		internal static class CachedOutputSetters<T>
		{
			// Token: 0x0400005A RID: 90
			public static readonly Hashtable Cache = new Hashtable();
		}

		// Token: 0x0200001E RID: 30
		private sealed class ParamInfo
		{
			// Token: 0x17000026 RID: 38
			// (get) Token: 0x06000176 RID: 374 RVA: 0x00009C9D File Offset: 0x00007E9D
			// (set) Token: 0x06000177 RID: 375 RVA: 0x00009CA5 File Offset: 0x00007EA5
			public string Name { get; set; }

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x06000178 RID: 376 RVA: 0x00009CAE File Offset: 0x00007EAE
			// (set) Token: 0x06000179 RID: 377 RVA: 0x00009CB6 File Offset: 0x00007EB6
			public object Value { get; set; }

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x0600017A RID: 378 RVA: 0x00009CBF File Offset: 0x00007EBF
			// (set) Token: 0x0600017B RID: 379 RVA: 0x00009CC7 File Offset: 0x00007EC7
			public ParameterDirection ParameterDirection { get; set; }

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x0600017C RID: 380 RVA: 0x00009CD0 File Offset: 0x00007ED0
			// (set) Token: 0x0600017D RID: 381 RVA: 0x00009CD8 File Offset: 0x00007ED8
			public DbType? DbType { get; set; }

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x0600017E RID: 382 RVA: 0x00009CE1 File Offset: 0x00007EE1
			// (set) Token: 0x0600017F RID: 383 RVA: 0x00009CE9 File Offset: 0x00007EE9
			public int? Size { get; set; }

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x06000180 RID: 384 RVA: 0x00009CF2 File Offset: 0x00007EF2
			// (set) Token: 0x06000181 RID: 385 RVA: 0x00009CFA File Offset: 0x00007EFA
			public IDbDataParameter AttachedParam { get; set; }

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x06000182 RID: 386 RVA: 0x00009D03 File Offset: 0x00007F03
			// (set) Token: 0x06000183 RID: 387 RVA: 0x00009D0B File Offset: 0x00007F0B
			internal Action<object, DynamicParameters> OutputCallback { get; set; }

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x06000184 RID: 388 RVA: 0x00009D14 File Offset: 0x00007F14
			// (set) Token: 0x06000185 RID: 389 RVA: 0x00009D1C File Offset: 0x00007F1C
			internal object OutputTarget { get; set; }

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x06000186 RID: 390 RVA: 0x00009D25 File Offset: 0x00007F25
			// (set) Token: 0x06000187 RID: 391 RVA: 0x00009D2D File Offset: 0x00007F2D
			internal bool CameFromTemplate { get; set; }

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000188 RID: 392 RVA: 0x00009D36 File Offset: 0x00007F36
			// (set) Token: 0x06000189 RID: 393 RVA: 0x00009D3E File Offset: 0x00007F3E
			public byte? Precision { get; set; }

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x0600018A RID: 394 RVA: 0x00009D47 File Offset: 0x00007F47
			// (set) Token: 0x0600018B RID: 395 RVA: 0x00009D4F File Offset: 0x00007F4F
			public byte? Scale { get; set; }
		}
	}
}
