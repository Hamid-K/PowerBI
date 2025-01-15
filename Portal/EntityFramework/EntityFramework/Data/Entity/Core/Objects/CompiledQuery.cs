using System;
using System.Collections;
using System.Data.Entity.Core.Objects.ELinq;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000401 RID: 1025
	public sealed class CompiledQuery
	{
		// Token: 0x06002FA0 RID: 12192 RVA: 0x00096438 File Offset: 0x00094638
		private CompiledQuery(LambdaExpression query)
		{
			Funcletizer funcletizer = Funcletizer.CreateCompiledQueryLockdownFuncletizer();
			Func<bool> func;
			this._query = (LambdaExpression)funcletizer.Funcletize(query, out func);
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x00096470 File Offset: 0x00094670
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult>);
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x00096483 File Offset: 0x00094683
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult>);
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x00096496 File Offset: 0x00094696
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult>);
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000964A9 File Offset: 0x000946A9
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult>);
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000964BC File Offset: 0x000946BC
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult>);
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x000964CF File Offset: 0x000946CF
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult>);
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000964E2 File Offset: 0x000946E2
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult>);
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000964F5 File Offset: 0x000946F5
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>);
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x00096508 File Offset: 0x00094708
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>);
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x0009651B File Offset: 0x0009471B
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>);
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x0009652E File Offset: 0x0009472E
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>);
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x00096541 File Offset: 0x00094741
		public static Func<TArg0, TArg1, TArg2, TArg3, TArg4, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TArg4, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TArg4, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TArg4, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TResult>);
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x00096554 File Offset: 0x00094754
		public static Func<TArg0, TArg1, TArg2, TArg3, TResult> Compile<TArg0, TArg1, TArg2, TArg3, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TArg3, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TArg3, TResult>);
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x00096567 File Offset: 0x00094767
		public static Func<TArg0, TArg1, TArg2, TResult> Compile<TArg0, TArg1, TArg2, TResult>(Expression<Func<TArg0, TArg1, TArg2, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TArg2, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TArg2, TResult>);
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x0009657A File Offset: 0x0009477A
		public static Func<TArg0, TArg1, TResult> Compile<TArg0, TArg1, TResult>(Expression<Func<TArg0, TArg1, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TArg1, TResult>(new CompiledQuery(query).Invoke<TArg0, TArg1, TResult>);
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x0009658D File Offset: 0x0009478D
		public static Func<TArg0, TResult> Compile<TArg0, TResult>(Expression<Func<TArg0, TResult>> query) where TArg0 : ObjectContext
		{
			return new Func<TArg0, TResult>(new CompiledQuery(query).Invoke<TArg0, TResult>);
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x000965A0 File Offset: 0x000947A0
		private TResult Invoke<TArg0, TResult>(TArg0 arg0) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[0]);
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x000965D3 File Offset: 0x000947D3
		private TResult Invoke<TArg0, TArg1, TResult>(TArg0 arg0, TArg1 arg1) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[] { arg1 });
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x00096610 File Offset: 0x00094810
		private TResult Invoke<TArg0, TArg1, TArg2, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[] { arg1, arg2 });
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x00096660 File Offset: 0x00094860
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[] { arg1, arg2, arg3 });
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000966BC File Offset: 0x000948BC
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[] { arg1, arg2, arg3, arg4 });
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x00096720 File Offset: 0x00094920
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[] { arg1, arg2, arg3, arg4, arg5 });
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x00096790 File Offset: 0x00094990
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[] { arg1, arg2, arg3, arg4, arg5, arg6 });
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x00096808 File Offset: 0x00094A08
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 });
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x0009688C File Offset: 0x00094A8C
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 });
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x00096918 File Offset: 0x00094B18
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 });
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x000969B0 File Offset: 0x00094BB0
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 });
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x00096A54 File Offset: 0x00094C54
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10,
				arg11
			});
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x00096B04 File Offset: 0x00094D04
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10,
				arg11, arg12
			});
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x00096BBC File Offset: 0x00094DBC
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10,
				arg11, arg12, arg13
			});
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x00096C80 File Offset: 0x00094E80
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10,
				arg11, arg12, arg13, arg14
			});
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x00096D50 File Offset: 0x00094F50
		private TResult Invoke<TArg0, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15) where TArg0 : ObjectContext
		{
			arg0.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResult), Assembly.GetCallingAssembly());
			return this.ExecuteQuery<TResult>(arg0, new object[]
			{
				arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10,
				arg11, arg12, arg13, arg14, arg15
			});
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x00096E2C File Offset: 0x0009502C
		private TResult ExecuteQuery<TResult>(ObjectContext context, params object[] parameterValues)
		{
			bool flag;
			IEnumerable enumerable = new CompiledELinqQueryState(CompiledQuery.GetElementType(typeof(TResult), out flag), context, this._query, this._cacheToken, parameterValues, null).CreateQuery();
			if (flag)
			{
				return ObjectQueryProvider.ExecuteSingle<TResult>(enumerable.Cast<TResult>(), this._query);
			}
			return (TResult)((object)enumerable);
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x00096E80 File Offset: 0x00095080
		private static Type GetElementType(Type resultType, out bool isSingleton)
		{
			Type elementType = TypeSystem.GetElementType(resultType);
			isSingleton = elementType == resultType || !resultType.IsAssignableFrom(typeof(ObjectQuery<>).MakeGenericType(new Type[] { elementType }));
			if (isSingleton)
			{
				return resultType;
			}
			return elementType;
		}

		// Token: 0x04001010 RID: 4112
		private readonly LambdaExpression _query;

		// Token: 0x04001011 RID: 4113
		private readonly Guid _cacheToken = Guid.NewGuid();
	}
}
