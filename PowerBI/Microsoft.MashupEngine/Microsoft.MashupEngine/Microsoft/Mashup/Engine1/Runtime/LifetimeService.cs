using System;
using System.IO;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200155B RID: 5467
	internal static class LifetimeService
	{
		// Token: 0x060087FD RID: 34813 RVA: 0x001CD588 File Offset: 0x001CB788
		public static IDataReaderWithTableSchema RegisterForCleanup(this IEngineHost engineHost, IDataReaderWithTableSchema reader)
		{
			return engineHost.QueryService<ILifetimeService>().RegisterForCleanup(reader);
		}

		// Token: 0x060087FE RID: 34814 RVA: 0x001CD596 File Offset: 0x001CB796
		public static IPageReader RegisterForCleanup(this IEngineHost engineHost, IPageReader reader)
		{
			return engineHost.QueryService<ILifetimeService>().RegisterForCleanup(reader);
		}

		// Token: 0x060087FF RID: 34815 RVA: 0x001CD5A4 File Offset: 0x001CB7A4
		public static IDisposable RegisterForCleanup(this IEngineHost engineHost, IDisposable disposable)
		{
			return engineHost.QueryService<ILifetimeService>().RegisterForCleanup(disposable);
		}

		// Token: 0x06008800 RID: 34816 RVA: 0x001CD5B4 File Offset: 0x001CB7B4
		public static IDataReaderWithTableSchema RegisterForCleanup(this ILifetimeService lifetimeService, IDataReaderWithTableSchema reader)
		{
			if (lifetimeService != null)
			{
				lifetimeService.Register(reader);
				return reader.AfterDispose(delegate
				{
					lifetimeService.Unregister(reader);
				});
			}
			return reader;
		}

		// Token: 0x06008801 RID: 34817 RVA: 0x001CD60C File Offset: 0x001CB80C
		public static IPageReader RegisterForCleanup(this ILifetimeService lifetimeService, IPageReader reader)
		{
			if (lifetimeService != null)
			{
				lifetimeService.Register(reader);
				return reader.AfterDispose(delegate
				{
					lifetimeService.Unregister(reader);
				});
			}
			return reader;
		}

		// Token: 0x06008802 RID: 34818 RVA: 0x001CD664 File Offset: 0x001CB864
		public static IDisposable RegisterForCleanup(this ILifetimeService lifetimeService, IDisposable disposable)
		{
			if (lifetimeService != null)
			{
				lifetimeService.Register(disposable);
				return disposable.AfterDispose(delegate
				{
					lifetimeService.Unregister(disposable);
				});
			}
			return disposable;
		}

		// Token: 0x06008803 RID: 34819 RVA: 0x001CD6BC File Offset: 0x001CB8BC
		public static Stream RegisterForCleanup(this ILifetimeService lifetimeService, Stream stream)
		{
			if (lifetimeService != null)
			{
				lifetimeService.Register(stream);
				return stream.AfterDispose(delegate
				{
					lifetimeService.Unregister(stream);
				});
			}
			return stream;
		}
	}
}
