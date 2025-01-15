using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000158 RID: 344
	[Serializable]
	internal class IntervalProperties : ConfigurationElement, ISerializable
	{
		// Token: 0x06000AAE RID: 2734 RVA: 0x00015607 File Offset: 0x00013807
		public IntervalProperties()
		{
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x000237C2 File Offset: 0x000219C2
		// (set) Token: 0x06000AB0 RID: 2736 RVA: 0x000237D4 File Offset: 0x000219D4
		[ConfigurationProperty("persistence", IsRequired = false)]
		public TimeSpan PersistenceInterval
		{
			get
			{
				return (TimeSpan)base["persistence"];
			}
			set
			{
				base["persistence"] = value;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x000237E7 File Offset: 0x000219E7
		// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x000237F9 File Offset: 0x000219F9
		[ConfigurationProperty("sync", IsRequired = false)]
		public TimeSpan SyncInterval
		{
			get
			{
				return (TimeSpan)base["sync"];
			}
			set
			{
				base["sync"] = value;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0002380C File Offset: 0x00021A0C
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x0002381E File Offset: 0x00021A1E
		[ConfigurationProperty("reset", IsRequired = false)]
		public TimeSpan ResetInterval
		{
			get
			{
				return (TimeSpan)base["reset"];
			}
			set
			{
				base["reset"] = value;
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00023834 File Offset: 0x00021A34
		protected IntervalProperties(SerializationInfo info, StreamingContext context)
		{
			try
			{
				this.PersistenceInterval = (TimeSpan)info.GetValue("persistence", typeof(TimeSpan));
			}
			catch (SerializationException)
			{
			}
			try
			{
				this.SyncInterval = (TimeSpan)info.GetValue("sync", typeof(TimeSpan));
			}
			catch (SerializationException)
			{
			}
			try
			{
				this.ResetInterval = (TimeSpan)info.GetValue("reset", typeof(TimeSpan));
			}
			catch (SerializationException)
			{
			}
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x000238E0 File Offset: 0x00021AE0
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("persistence", this.PersistenceInterval);
			info.AddValue("sync", this.SyncInterval);
			info.AddValue("reset", this.ResetInterval);
		}

		// Token: 0x04000755 RID: 1877
		internal const string SYNC = "sync";

		// Token: 0x04000756 RID: 1878
		internal const string RESET = "reset";

		// Token: 0x04000757 RID: 1879
		internal const string PERSISTENCE = "persistence";

		// Token: 0x04000758 RID: 1880
		internal static IntervalProperties DefaultIntervals = new IntervalProperties();
	}
}
