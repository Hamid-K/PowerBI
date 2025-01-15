using System;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Utilities;
using System.IO;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000277 RID: 631
	public class DatabaseLogger : IDisposable, IDbConfigurationInterceptor, IDbInterceptor
	{
		// Token: 0x06001FD8 RID: 8152 RVA: 0x0005AC6D File Offset: 0x00058E6D
		public DatabaseLogger()
		{
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x0005AC80 File Offset: 0x00058E80
		public DatabaseLogger(string path)
			: this(path, false)
		{
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x0005AC8A File Offset: 0x00058E8A
		public DatabaseLogger(string path, bool append)
		{
			Check.NotEmpty(path, "path");
			this._writer = new StreamWriter(path, append)
			{
				AutoFlush = true
			};
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x0005ACBD File Offset: 0x00058EBD
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x0005ACCC File Offset: 0x00058ECC
		protected virtual void Dispose(bool disposing)
		{
			this.StopLogging();
			if (disposing && this._writer != null)
			{
				this._writer.Dispose();
				this._writer = null;
			}
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x0005ACF1 File Offset: 0x00058EF1
		public void StartLogging()
		{
			this.StartLogging(DbConfiguration.DependencyResolver);
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x0005ACFE File Offset: 0x00058EFE
		public void StopLogging()
		{
			if (this._formatter != null)
			{
				DbInterception.Remove(this._formatter);
				this._formatter = null;
			}
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x0005AD1A File Offset: 0x00058F1A
		void IDbConfigurationInterceptor.Loaded(DbConfigurationLoadedEventArgs loadedEventArgs, DbConfigurationInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConfigurationLoadedEventArgs>(loadedEventArgs, "loadedEventArgs");
			Check.NotNull<DbConfigurationInterceptionContext>(interceptionContext, "interceptionContext");
			this.StartLogging(loadedEventArgs.DependencyResolver);
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x0005AD40 File Offset: 0x00058F40
		private void StartLogging(IDbDependencyResolver resolver)
		{
			if (this._formatter == null)
			{
				this._formatter = resolver.GetService<Func<DbContext, Action<string>, DatabaseLogFormatter>>()(null, (this._writer == null) ? new Action<string>(Console.Write) : new Action<string>(this.WriteThreadSafe));
				DbInterception.Add(this._formatter);
			}
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x0005AD94 File Offset: 0x00058F94
		private void WriteThreadSafe(string value)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this._writer.Write(value);
			}
		}

		// Token: 0x04000B72 RID: 2930
		private TextWriter _writer;

		// Token: 0x04000B73 RID: 2931
		private DatabaseLogFormatter _formatter;

		// Token: 0x04000B74 RID: 2932
		private readonly object _lock = new object();
	}
}
