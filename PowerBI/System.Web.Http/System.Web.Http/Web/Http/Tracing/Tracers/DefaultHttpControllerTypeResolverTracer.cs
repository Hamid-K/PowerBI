using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dispatcher;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000123 RID: 291
	internal class DefaultHttpControllerTypeResolverTracer : DefaultHttpControllerTypeResolver, IDecorator<DefaultHttpControllerTypeResolver>
	{
		// Token: 0x060007BB RID: 1979 RVA: 0x000137F4 File Offset: 0x000119F4
		public DefaultHttpControllerTypeResolverTracer(DefaultHttpControllerTypeResolver innerResolver, ITraceWriter traceWriter)
		{
			this._innerResolver = innerResolver;
			this._traceWriter = traceWriter;
			this._innerTypeName = this._innerResolver.GetType().Name;
			this._innerResolver.SetGetTypesFunc(new Func<Assembly, Type[]>(this.GetTypesAndTrace));
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00013842 File Offset: 0x00011A42
		public DefaultHttpControllerTypeResolver Inner
		{
			get
			{
				return this._innerResolver;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0001384A File Offset: 0x00011A4A
		protected internal override Predicate<Type> IsControllerTypePredicate
		{
			get
			{
				return this._innerResolver.IsControllerTypePredicate;
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00013858 File Offset: 0x00011A58
		public override ICollection<Type> GetControllerTypes(IAssembliesResolver assembliesResolver)
		{
			ICollection<Type> result = null;
			this._traceWriter.TraceBeginEnd(null, TraceCategories.ControllersCategory, TraceLevel.Debug, this._innerTypeName, "GetControllerTypes", null, delegate
			{
				result = this._innerResolver.GetControllerTypes(assembliesResolver);
			}, null, null);
			return result;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x000138B4 File Offset: 0x00011AB4
		private Type[] GetTypesAndTrace(Assembly assembly)
		{
			Type[] types;
			try
			{
				types = DefaultHttpControllerTypeResolver.GetTypes(assembly);
			}
			catch (Exception ex)
			{
				this._traceWriter.Warn(null, TraceCategories.ControllersCategory, ex, SRResources.TraceHttpControllerTypeResolverError, new object[] { assembly.FullName });
				throw;
			}
			return types;
		}

		// Token: 0x0400020E RID: 526
		private readonly DefaultHttpControllerTypeResolver _innerResolver;

		// Token: 0x0400020F RID: 527
		private readonly ITraceWriter _traceWriter;

		// Token: 0x04000210 RID: 528
		private readonly string _innerTypeName;
	}
}
