using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019A8 RID: 6568
	public class CultureService : ICultureService
	{
		// Token: 0x0600A680 RID: 42624 RVA: 0x002270C7 File Offset: 0x002252C7
		public CultureService(IEngineHost host, string defaultCulture)
		{
			this.syncRoot = new object();
			this.host = host;
			this.defaultCulture = new CultureService.HostCulture(this.host, defaultCulture);
		}

		// Token: 0x17002A77 RID: 10871
		// (get) Token: 0x0600A681 RID: 42625 RVA: 0x002270F3 File Offset: 0x002252F3
		public ICulture DefaultCulture
		{
			get
			{
				return this.defaultCulture;
			}
		}

		// Token: 0x0600A682 RID: 42626 RVA: 0x002270FC File Offset: 0x002252FC
		public ICulture GetCulture(string name)
		{
			name = name.Replace('\0', '\ufffd');
			object obj = this.syncRoot;
			ICulture culture2;
			lock (obj)
			{
				ICulture culture;
				if (!this.Cache.TryGetValue(name, out culture))
				{
					culture = new CultureService.HostCulture(this.host, name);
					this.Cache.Add(culture.Name, culture);
				}
				culture2 = culture;
			}
			return culture2;
		}

		// Token: 0x17002A78 RID: 10872
		// (get) Token: 0x0600A683 RID: 42627 RVA: 0x00227178 File Offset: 0x00225378
		private Dictionary<string, ICulture> Cache
		{
			get
			{
				if (this.cache == null)
				{
					this.cache = new Dictionary<string, ICulture>();
				}
				return this.cache;
			}
		}

		// Token: 0x0600A684 RID: 42628 RVA: 0x00227194 File Offset: 0x00225394
		private static CultureInfo CreateCultureInfo(IEngineHost host, string name)
		{
			try
			{
				CultureInfo cultureInfo = new CultureInfo(name, false);
				if (cultureInfo.EnglishName.StartsWith("Unknown ", StringComparison.Ordinal))
				{
					throw new ArgumentException("Unable to find the culture info for '" + name + "'.", "name");
				}
				return cultureInfo;
			}
			catch (ArgumentException ex)
			{
				using (IHostTrace hostTrace = TracingService.CreateTrace(host, "Engine/CultureService/CreateCultureInfo", TraceEventType.Information, null))
				{
					hostTrace.Add(ex, true);
				}
			}
			return null;
		}

		// Token: 0x040056A2 RID: 22178
		private readonly object syncRoot;

		// Token: 0x040056A3 RID: 22179
		private readonly CultureService.HostCulture defaultCulture;

		// Token: 0x040056A4 RID: 22180
		private readonly IEngineHost host;

		// Token: 0x040056A5 RID: 22181
		private Dictionary<string, ICulture> cache;

		// Token: 0x020019A9 RID: 6569
		private class HostCulture : ICulture
		{
			// Token: 0x0600A685 RID: 42629 RVA: 0x00227220 File Offset: 0x00225420
			public HostCulture(IEngineHost host, string name)
			{
				name = name.Replace('\0', '\ufffd');
				this.name = name;
				CultureInfo cultureInfo = null;
				try
				{
					cultureInfo = CultureService.CreateCultureInfo(host, name);
				}
				finally
				{
					this.value = cultureInfo;
				}
			}

			// Token: 0x17002A79 RID: 10873
			// (get) Token: 0x0600A686 RID: 42630 RVA: 0x0022726C File Offset: 0x0022546C
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17002A7A RID: 10874
			// (get) Token: 0x0600A687 RID: 42631 RVA: 0x00227274 File Offset: 0x00225474
			public CultureInfo Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x040056A6 RID: 22182
			private readonly string name;

			// Token: 0x040056A7 RID: 22183
			private readonly CultureInfo value;
		}
	}
}
