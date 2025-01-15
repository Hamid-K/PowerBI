using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Azure.Core.Pipeline
{
	// Token: 0x02000086 RID: 134
	[NullableContext(1)]
	[Nullable(0)]
	internal class GeoRedundantFallbackPolicy : HttpPipelineSynchronousPolicy
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x0000CB78 File Offset: 0x0000AD78
		public GeoRedundantFallbackPolicy([Nullable(new byte[] { 2, 1 })] string[] readFallbackHosts, [Nullable(new byte[] { 2, 1 })] string[] writeFallbackHosts, TimeSpan? primaryCoolDown = null)
		{
			TimeSpan timeSpan = primaryCoolDown ?? TimeSpan.FromMinutes(10.0);
			this._writeFallback = new GeoRedundantFallbackPolicy.Fallback(writeFallbackHosts ?? Array.Empty<string>(), timeSpan);
			this._readFallback = new GeoRedundantFallbackPolicy.Fallback(readFallbackHosts ?? Array.Empty<string>(), timeSpan);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000CBDA File Offset: 0x0000ADDA
		public static void SetHostAffinity(HttpMessage message, bool hostAffinity)
		{
			message.SetProperty(typeof(GeoRedundantFallbackPolicy.HostAffinityKey), hostAffinity);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000CBF4 File Offset: 0x0000ADF4
		private static bool GetHostAffinity(HttpMessage message)
		{
			object obj;
			return message.TryGetProperty(typeof(GeoRedundantFallbackPolicy.HostAffinityKey), out obj) && obj is bool && (bool)obj;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000CC27 File Offset: 0x0000AE27
		private static void SetPrimaryHost(HttpMessage message)
		{
			message.SetProperty(typeof(GeoRedundantFallbackPolicy.PrimaryHostKey), message.Request.Uri.Host);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000CC4C File Offset: 0x0000AE4C
		private static string GetPrimaryHost(HttpMessage message)
		{
			object obj;
			message.TryGetProperty(typeof(GeoRedundantFallbackPolicy.PrimaryHostKey), out obj);
			return (string)obj;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000CC74 File Offset: 0x0000AE74
		public override void OnSendingRequest(HttpMessage message)
		{
			if (message.HasResponse || GeoRedundantFallbackPolicy.GetHostAffinity(message) || message.Request.Uri.Host == null)
			{
				return;
			}
			GeoRedundantFallbackPolicy.Fallback fallback = ((message.Request.Method == RequestMethod.Get || message.Request.Method == RequestMethod.Head) ? this._readFallback : this._writeFallback);
			if (fallback.Hosts.Length == 0)
			{
				return;
			}
			if (message.ProcessingContext.RetryNumber == 0)
			{
				GeoRedundantFallbackPolicy.SetPrimaryHost(message);
				GeoRedundantFallbackPolicy.UpdateHostIfNeeded(message, fallback);
				return;
			}
			fallback.AdvanceIfNeeded(message);
			GeoRedundantFallbackPolicy.UpdateHostIfNeeded(message, fallback);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000CD1C File Offset: 0x0000AF1C
		private static void UpdateHostIfNeeded(HttpMessage message, GeoRedundantFallbackPolicy.Fallback fallback)
		{
			fallback.ResetPrimaryIfNeeded();
			int index = fallback.Index;
			message.Request.Uri.Host = ((index != -1) ? fallback.Hosts[index] : GeoRedundantFallbackPolicy.GetPrimaryHost(message));
		}

		// Token: 0x040001C7 RID: 455
		private readonly GeoRedundantFallbackPolicy.Fallback _writeFallback;

		// Token: 0x040001C8 RID: 456
		private readonly GeoRedundantFallbackPolicy.Fallback _readFallback;

		// Token: 0x0200010E RID: 270
		[NullableContext(0)]
		private class HostAffinityKey
		{
		}

		// Token: 0x0200010F RID: 271
		[NullableContext(0)]
		private class PrimaryHostKey
		{
		}

		// Token: 0x02000110 RID: 272
		[Nullable(0)]
		private class Fallback
		{
			// Token: 0x170001CD RID: 461
			// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0001B8C2 File Offset: 0x00019AC2
			public string[] Hosts { get; }

			// Token: 0x170001CE RID: 462
			// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0001B8CA File Offset: 0x00019ACA
			public int Index
			{
				get
				{
					return Volatile.Read(ref this._index);
				}
			}

			// Token: 0x060007A5 RID: 1957 RVA: 0x0001B8D7 File Offset: 0x00019AD7
			public Fallback(string[] hosts, TimeSpan cooldown)
			{
				this.Hosts = hosts;
				this._index = -1;
				this._ticks = -1L;
				this._cooldown = cooldown;
			}

			// Token: 0x060007A6 RID: 1958 RVA: 0x0001B8FC File Offset: 0x00019AFC
			public void AdvanceIfNeeded(HttpMessage message)
			{
				int index = this.Index;
				long num = Volatile.Read(ref this._ticks);
				if ((index == -1 && message.Request.Uri.Host.Equals(GeoRedundantFallbackPolicy.GetPrimaryHost(message), StringComparison.Ordinal)) || (index != -1 && message.Request.Uri.Host.Equals(this.Hosts[index], StringComparison.Ordinal)))
				{
					int num2 = index + 1;
					if (num2 >= this.Hosts.Length)
					{
						num2 = -1;
					}
					Interlocked.CompareExchange(ref this._index, num2, index);
					if (index == -1)
					{
						Interlocked.CompareExchange(ref this._ticks, Stopwatch.GetTimestamp(), num);
					}
				}
			}

			// Token: 0x060007A7 RID: 1959 RVA: 0x0001B998 File Offset: 0x00019B98
			public void ResetPrimaryIfNeeded()
			{
				long num = Volatile.Read(ref this._ticks);
				int index = this.Index;
				if (num != -1L && Stopwatch.GetTimestamp() - num >= this._cooldown.Ticks)
				{
					Interlocked.CompareExchange(ref this._index, -1, index);
					Interlocked.CompareExchange(ref this._ticks, -1L, num);
				}
			}

			// Token: 0x040003F1 RID: 1009
			private int _index;

			// Token: 0x040003F2 RID: 1010
			private long _ticks;

			// Token: 0x040003F3 RID: 1011
			private readonly TimeSpan _cooldown;
		}
	}
}
