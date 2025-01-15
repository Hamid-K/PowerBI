using System;
using Owin;

namespace Microsoft.Owin.Security.DataProtection
{
	// Token: 0x02000025 RID: 37
	public static class AppBuilderExtensions
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00003344 File Offset: 0x00001544
		public static void SetDataProtectionProvider(this IAppBuilder app, IDataProtectionProvider dataProtectionProvider)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			if (dataProtectionProvider == null)
			{
				app.Properties.Remove("security.DataProtectionProvider");
				return;
			}
			app.Properties["security.DataProtectionProvider"] = new Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>>(delegate(string[] purposes)
			{
				IDataProtector dataProtection = dataProtectionProvider.Create(purposes);
				return new Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>(new Func<byte[], byte[]>(dataProtection.Protect), new Func<byte[], byte[]>(dataProtection.Unprotect));
			});
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000033A4 File Offset: 0x000015A4
		public static IDataProtectionProvider GetDataProtectionProvider(this IAppBuilder app)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			object value;
			if (app.Properties.TryGetValue("security.DataProtectionProvider", out value))
			{
				Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>> del = value as Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>>;
				if (del != null)
				{
					return new AppBuilderExtensions.CallDataProtectionProvider(del);
				}
			}
			return null;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000033E8 File Offset: 0x000015E8
		public static IDataProtector CreateDataProtector(this IAppBuilder app, params string[] purposes)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			IDataProtectionProvider dataProtectionProvider = app.GetDataProtectionProvider();
			if (dataProtectionProvider == null)
			{
				dataProtectionProvider = AppBuilderExtensions.FallbackDataProtectionProvider(app);
			}
			return dataProtectionProvider.Create(purposes);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000341B File Offset: 0x0000161B
		private static IDataProtectionProvider FallbackDataProtectionProvider(IAppBuilder app)
		{
			return new DpapiDataProtectionProvider(AppBuilderExtensions.GetAppName(app));
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003428 File Offset: 0x00001628
		private static string GetAppName(IAppBuilder app)
		{
			object value;
			if (app.Properties.TryGetValue("host.AppName", out value))
			{
				string appName = value as string;
				if (!string.IsNullOrEmpty(appName))
				{
					return appName;
				}
			}
			throw new NotSupportedException(Resources.Exception_DefaultDpapiRequiresAppNameKey);
		}

		// Token: 0x02000043 RID: 67
		private class CallDataProtectionProvider : IDataProtectionProvider
		{
			// Token: 0x060000F6 RID: 246 RVA: 0x00004633 File Offset: 0x00002833
			public CallDataProtectionProvider(Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>> create)
			{
				this._create = create;
			}

			// Token: 0x060000F7 RID: 247 RVA: 0x00004644 File Offset: 0x00002844
			public IDataProtector Create(params string[] purposes)
			{
				Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>> protection = this._create(purposes);
				return new AppBuilderExtensions.CallDataProtectionProvider.CallDataProtection(protection.Item1, protection.Item2);
			}

			// Token: 0x04000092 RID: 146
			private readonly Func<string[], Tuple<Func<byte[], byte[]>, Func<byte[], byte[]>>> _create;

			// Token: 0x02000047 RID: 71
			private class CallDataProtection : IDataProtector
			{
				// Token: 0x060000FC RID: 252 RVA: 0x000048B2 File Offset: 0x00002AB2
				public CallDataProtection(Func<byte[], byte[]> protect, Func<byte[], byte[]> unprotect)
				{
					this._protect = protect;
					this._unprotect = unprotect;
				}

				// Token: 0x060000FD RID: 253 RVA: 0x000048C8 File Offset: 0x00002AC8
				public byte[] Protect(byte[] userData)
				{
					return this._protect(userData);
				}

				// Token: 0x060000FE RID: 254 RVA: 0x000048D6 File Offset: 0x00002AD6
				public byte[] Unprotect(byte[] protectedData)
				{
					return this._unprotect(protectedData);
				}

				// Token: 0x040000A1 RID: 161
				private readonly Func<byte[], byte[]> _protect;

				// Token: 0x040000A2 RID: 162
				private readonly Func<byte[], byte[]> _unprotect;
			}
		}
	}
}
