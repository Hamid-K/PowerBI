using System;
using System.Globalization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Services;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CFA RID: 7418
	public static class MinimalEngineHost
	{
		// Token: 0x04005E42 RID: 24130
		public static readonly IEngineHost Instance = new MinimalEngineHost.EmptyHostEnvironment();

		// Token: 0x04005E43 RID: 24131
		public static readonly ITimeZoneService LocalTimeZoneService = new TimeZoneService(TimeZoneInfo.Local.Id, TimeZoneInfo.Local);

		// Token: 0x02001CFB RID: 7419
		private class EmptyHostEnvironment : IEngineHost
		{
			// Token: 0x0600B928 RID: 47400 RVA: 0x0025872C File Offset: 0x0025692C
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(ICultureService))
				{
					return (T)((object)new MinimalEngineHost.EmptyHostEnvironment.CultureService());
				}
				if (typeof(T) == typeof(ICurrentTimeService))
				{
					return (T)((object)new MinimalEngineHost.EmptyHostEnvironment.CurrentTimeService());
				}
				if (typeof(T) == typeof(ITimeZoneService))
				{
					return (T)((object)MinimalEngineHost.LocalTimeZoneService);
				}
				if (typeof(T) == typeof(IResourcePermissionService))
				{
					return (T)((object)new MinimalEngineHost.EmptyHostEnvironment.ResourcePermissionService());
				}
				return default(T);
			}

			// Token: 0x02001CFC RID: 7420
			private class CultureService : ICultureService
			{
				// Token: 0x17002DCB RID: 11723
				// (get) Token: 0x0600B92B RID: 47403 RVA: 0x002587ED File Offset: 0x002569ED
				public ICulture DefaultCulture
				{
					get
					{
						return this.defaultCulture;
					}
				}

				// Token: 0x0600B92C RID: 47404 RVA: 0x002587F8 File Offset: 0x002569F8
				public ICulture GetCulture(string name)
				{
					CultureInfo cultureInfo = null;
					try
					{
						cultureInfo = CultureExtensions.GetCulture(name);
					}
					catch (ArgumentException)
					{
					}
					return new MinimalEngineHost.EmptyHostEnvironment.CultureService.Culture
					{
						Name = name,
						Value = cultureInfo
					};
				}

				// Token: 0x04005E44 RID: 24132
				private MinimalEngineHost.EmptyHostEnvironment.CultureService.InvariantCulture defaultCulture = new MinimalEngineHost.EmptyHostEnvironment.CultureService.InvariantCulture();

				// Token: 0x02001CFD RID: 7421
				private class InvariantCulture : ICulture
				{
					// Token: 0x17002DCC RID: 11724
					// (get) Token: 0x0600B92D RID: 47405 RVA: 0x00018E5C File Offset: 0x0001705C
					public string Name
					{
						get
						{
							return CultureInfo.InvariantCulture.Name;
						}
					}

					// Token: 0x17002DCD RID: 11725
					// (get) Token: 0x0600B92E RID: 47406 RVA: 0x00018E68 File Offset: 0x00017068
					public CultureInfo Value
					{
						get
						{
							return CultureInfo.InvariantCulture;
						}
					}
				}

				// Token: 0x02001CFE RID: 7422
				private class Culture : ICulture
				{
					// Token: 0x17002DCE RID: 11726
					// (get) Token: 0x0600B930 RID: 47408 RVA: 0x00258838 File Offset: 0x00256A38
					// (set) Token: 0x0600B931 RID: 47409 RVA: 0x00258840 File Offset: 0x00256A40
					public string Name { get; set; }

					// Token: 0x17002DCF RID: 11727
					// (get) Token: 0x0600B932 RID: 47410 RVA: 0x00258849 File Offset: 0x00256A49
					// (set) Token: 0x0600B933 RID: 47411 RVA: 0x00258851 File Offset: 0x00256A51
					public CultureInfo Value { get; set; }
				}
			}

			// Token: 0x02001CFF RID: 7423
			private class CurrentTimeService : ICurrentTimeService
			{
				// Token: 0x0600B935 RID: 47413 RVA: 0x0025885A File Offset: 0x00256A5A
				public CurrentTimeService()
				{
					this.fixedUtcNow = DateTime.UtcNow;
				}

				// Token: 0x17002DD0 RID: 11728
				// (get) Token: 0x0600B936 RID: 47414 RVA: 0x0025886D File Offset: 0x00256A6D
				DateTime ICurrentTimeService.FixedUtcNow
				{
					get
					{
						return this.fixedUtcNow;
					}
				}

				// Token: 0x17002DD1 RID: 11729
				// (get) Token: 0x0600B937 RID: 47415 RVA: 0x00018EAC File Offset: 0x000170AC
				DateTime ICurrentTimeService.UtcNow
				{
					get
					{
						return DateTime.UtcNow;
					}
				}

				// Token: 0x04005E47 RID: 24135
				private readonly DateTime fixedUtcNow;
			}

			// Token: 0x02001D00 RID: 7424
			private class ResourcePermissionService : IResourcePermissionService
			{
				// Token: 0x0600B938 RID: 47416 RVA: 0x0007D355 File Offset: 0x0007B555
				bool IResourcePermissionService.IsResourceAccessPermitted(IResource resource, out ResourceCredentialCollection credentials)
				{
					credentials = null;
					return false;
				}
			}
		}
	}
}
