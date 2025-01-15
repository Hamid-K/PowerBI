using System;
using System.Diagnostics;
using Owin;

namespace Microsoft.Owin.Logging
{
	// Token: 0x02000029 RID: 41
	public static class AppBuilderLoggerExtensions
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x00004B3C File Offset: 0x00002D3C
		public static void SetLoggerFactory(this IAppBuilder app, ILoggerFactory loggerFactory)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			app.Properties["server.LoggerFactory"] = new Func<string, Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>>((string name) => new Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>(loggerFactory.Create(name).WriteCore));
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00004B80 File Offset: 0x00002D80
		public static ILoggerFactory GetLoggerFactory(this IAppBuilder app)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			object value;
			if (app.Properties.TryGetValue("server.LoggerFactory", out value))
			{
				Func<string, Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>> factory = value as Func<string, Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>>;
				if (factory != null)
				{
					return new AppBuilderLoggerExtensions.WrapLoggerFactory(factory);
				}
			}
			return null;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00004BC1 File Offset: 0x00002DC1
		public static ILogger CreateLogger(this IAppBuilder app, string name)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			return (app.GetLoggerFactory() ?? LoggerFactory.Default).Create(name);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00004BE6 File Offset: 0x00002DE6
		public static ILogger CreateLogger(this IAppBuilder app, Type component)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return app.CreateLogger(component.FullName);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00004C08 File Offset: 0x00002E08
		public static ILogger CreateLogger<TType>(this IAppBuilder app)
		{
			return app.CreateLogger(typeof(TType));
		}

		// Token: 0x0200005F RID: 95
		private class WrapLoggerFactory : ILoggerFactory
		{
			// Token: 0x060002D4 RID: 724 RVA: 0x000080F2 File Offset: 0x000062F2
			public WrapLoggerFactory(Func<string, Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>> create)
			{
				if (create == null)
				{
					throw new ArgumentNullException("create");
				}
				this._create = create;
			}

			// Token: 0x060002D5 RID: 725 RVA: 0x0000810F File Offset: 0x0000630F
			public ILogger Create(string name)
			{
				return new AppBuilderLoggerExtensions.WrappingLogger(this._create(name));
			}

			// Token: 0x040000DE RID: 222
			private readonly Func<string, Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>> _create;
		}

		// Token: 0x02000060 RID: 96
		private class WrappingLogger : ILogger
		{
			// Token: 0x060002D6 RID: 726 RVA: 0x00008122 File Offset: 0x00006322
			public WrappingLogger(Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool> write)
			{
				if (write == null)
				{
					throw new ArgumentNullException("write");
				}
				this._write = write;
			}

			// Token: 0x060002D7 RID: 727 RVA: 0x0000813F File Offset: 0x0000633F
			public bool WriteCore(TraceEventType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> message)
			{
				return this._write(eventType, eventId, state, exception, message);
			}

			// Token: 0x040000DF RID: 223
			private readonly Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool> _write;
		}
	}
}
