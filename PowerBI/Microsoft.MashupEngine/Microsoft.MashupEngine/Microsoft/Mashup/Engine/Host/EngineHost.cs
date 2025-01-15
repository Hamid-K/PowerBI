using System;
using System.Globalization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Host
{
	// Token: 0x02000211 RID: 529
	public static class EngineHost
	{
		// Token: 0x0400065E RID: 1630
		public static readonly IEngineHost Empty = new EngineHost.EmptyHostEnvironment();

		// Token: 0x02000212 RID: 530
		private class EmptyHostEnvironment : IEngineHost
		{
			// Token: 0x06000ABF RID: 2751 RVA: 0x00018D50 File Offset: 0x00016F50
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(ICultureService))
				{
					return (T)((object)new EngineHost.EmptyHostEnvironment.CultureService());
				}
				if (typeof(T) == typeof(ICurrentTimeService))
				{
					return (T)((object)new EngineHost.EmptyHostEnvironment.CurrentTimeService());
				}
				if (typeof(T) == typeof(ITimeZoneService))
				{
					return (T)((object)new EngineHost.EmptyHostEnvironment.LocalTimeZoneService());
				}
				if (typeof(T) == typeof(ICacheSets))
				{
					return (T)((object)EngineHost.EmptyHostEnvironment.CacheSets.Empty);
				}
				return default(T);
			}

			// Token: 0x02000213 RID: 531
			private class CultureService : ICultureService
			{
				// Token: 0x17000319 RID: 793
				// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00018E11 File Offset: 0x00017011
				public ICulture DefaultCulture
				{
					get
					{
						return this.defaultCulture;
					}
				}

				// Token: 0x06000AC3 RID: 2755 RVA: 0x00018E1C File Offset: 0x0001701C
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
					return new EngineHost.EmptyHostEnvironment.CultureService.Culture
					{
						Name = name,
						Value = cultureInfo
					};
				}

				// Token: 0x0400065F RID: 1631
				private EngineHost.EmptyHostEnvironment.CultureService.InvariantCulture defaultCulture = new EngineHost.EmptyHostEnvironment.CultureService.InvariantCulture();

				// Token: 0x02000214 RID: 532
				private class InvariantCulture : ICulture
				{
					// Token: 0x1700031A RID: 794
					// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00018E5C File Offset: 0x0001705C
					public string Name
					{
						get
						{
							return CultureInfo.InvariantCulture.Name;
						}
					}

					// Token: 0x1700031B RID: 795
					// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x00018E68 File Offset: 0x00017068
					public CultureInfo Value
					{
						get
						{
							return CultureInfo.InvariantCulture;
						}
					}
				}

				// Token: 0x02000215 RID: 533
				private class Culture : ICulture
				{
					// Token: 0x1700031C RID: 796
					// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x00018E6F File Offset: 0x0001706F
					// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x00018E77 File Offset: 0x00017077
					public string Name { get; set; }

					// Token: 0x1700031D RID: 797
					// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x00018E80 File Offset: 0x00017080
					// (set) Token: 0x06000ACA RID: 2762 RVA: 0x00018E88 File Offset: 0x00017088
					public CultureInfo Value { get; set; }
				}
			}

			// Token: 0x02000216 RID: 534
			private class CurrentTimeService : ICurrentTimeService
			{
				// Token: 0x06000ACC RID: 2764 RVA: 0x00018E91 File Offset: 0x00017091
				public CurrentTimeService()
				{
					this.fixedUtcNow = DateTime.UtcNow;
				}

				// Token: 0x1700031E RID: 798
				// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00018EA4 File Offset: 0x000170A4
				DateTime ICurrentTimeService.FixedUtcNow
				{
					get
					{
						return this.fixedUtcNow;
					}
				}

				// Token: 0x1700031F RID: 799
				// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00018EAC File Offset: 0x000170AC
				DateTime ICurrentTimeService.UtcNow
				{
					get
					{
						return DateTime.UtcNow;
					}
				}

				// Token: 0x04000662 RID: 1634
				private readonly DateTime fixedUtcNow;
			}

			// Token: 0x02000217 RID: 535
			private class LocalTimeZoneService : ITimeZoneService
			{
				// Token: 0x17000320 RID: 800
				// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00018EB3 File Offset: 0x000170B3
				public ITimeZone DefaultTimeZone
				{
					get
					{
						return this.local;
					}
				}

				// Token: 0x06000AD0 RID: 2768 RVA: 0x00018EBC File Offset: 0x000170BC
				public bool TryGetTimeZone(string name, out ITimeZone timeZone)
				{
					bool flag;
					try
					{
						timeZone = new EngineHost.EmptyHostEnvironment.LocalTimeZoneService.TimeZone(name, TimeZoneInfo.FindSystemTimeZoneById(name));
						flag = true;
					}
					catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
					{
						timeZone = null;
						flag = false;
					}
					return flag;
				}

				// Token: 0x04000663 RID: 1635
				private readonly EngineHost.EmptyHostEnvironment.LocalTimeZoneService.TimeZone local = new EngineHost.EmptyHostEnvironment.LocalTimeZoneService.TimeZone(TimeZoneInfo.Local.Id, TimeZoneInfo.Local);

				// Token: 0x02000218 RID: 536
				private sealed class TimeZone : ITimeZone
				{
					// Token: 0x06000AD2 RID: 2770 RVA: 0x00018F2E File Offset: 0x0001712E
					public TimeZone(string name, TimeZoneInfo value)
					{
						this.name = name;
						this.value = value;
					}

					// Token: 0x17000321 RID: 801
					// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x00018F44 File Offset: 0x00017144
					public string Name
					{
						get
						{
							return this.name;
						}
					}

					// Token: 0x17000322 RID: 802
					// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x00018F4C File Offset: 0x0001714C
					public TimeZoneInfo Value
					{
						get
						{
							return this.value;
						}
					}

					// Token: 0x04000664 RID: 1636
					private readonly string name;

					// Token: 0x04000665 RID: 1637
					private readonly TimeZoneInfo value;
				}
			}

			// Token: 0x02000219 RID: 537
			private class CacheSets : ICacheSets, IDisposable
			{
				// Token: 0x06000AD5 RID: 2773 RVA: 0x000020FD File Offset: 0x000002FD
				private CacheSets()
				{
				}

				// Token: 0x17000323 RID: 803
				// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x00018F54 File Offset: 0x00017154
				public ICacheSet Metadata
				{
					get
					{
						return EngineHost.EmptyHostEnvironment.CacheSets.CacheSet.Empty;
					}
				}

				// Token: 0x17000324 RID: 804
				// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00018F54 File Offset: 0x00017154
				public ICacheSet Data
				{
					get
					{
						return EngineHost.EmptyHostEnvironment.CacheSets.CacheSet.Empty;
					}
				}

				// Token: 0x06000AD8 RID: 2776 RVA: 0x0000336E File Offset: 0x0000156E
				public void Dispose()
				{
				}

				// Token: 0x04000666 RID: 1638
				public static readonly ICacheSets Empty = new EngineHost.EmptyHostEnvironment.CacheSets();

				// Token: 0x0200021A RID: 538
				private class CacheSet : ICacheSet, IDisposable
				{
					// Token: 0x06000ADA RID: 2778 RVA: 0x000020FD File Offset: 0x000002FD
					private CacheSet()
					{
					}

					// Token: 0x17000325 RID: 805
					// (get) Token: 0x06000ADB RID: 2779 RVA: 0x000020FA File Offset: 0x000002FA
					public IPersistentCache PersistentCache
					{
						get
						{
							return null;
						}
					}

					// Token: 0x17000326 RID: 806
					// (get) Token: 0x06000ADC RID: 2780 RVA: 0x000020FA File Offset: 0x000002FA
					public IObjectCache ObjectCache
					{
						get
						{
							return null;
						}
					}

					// Token: 0x17000327 RID: 807
					// (get) Token: 0x06000ADD RID: 2781 RVA: 0x000020FA File Offset: 0x000002FA
					public IPersistentObjectCache PersistentObjectCache
					{
						get
						{
							return null;
						}
					}

					// Token: 0x06000ADE RID: 2782 RVA: 0x0000336E File Offset: 0x0000156E
					public void Dispose()
					{
					}

					// Token: 0x04000667 RID: 1639
					public static readonly ICacheSet Empty = new EngineHost.EmptyHostEnvironment.CacheSets.CacheSet();
				}
			}
		}
	}
}
