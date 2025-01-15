using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x0200070C RID: 1804
	[DataContract]
	[KnownType(typeof(SnaLinkRemoteEnvironment))]
	[KnownType(typeof(SnaUserDataRemoteEnvironment))]
	[KnownType(typeof(ElmLinkRemoteEnvironment))]
	[KnownType(typeof(ElmUserDataRemoteEnvironment))]
	[KnownType(typeof(TrmLinkRemoteEnvironment))]
	[KnownType(typeof(TrmUserDataRemoteEnvironment))]
	[KnownType(typeof(ImsConnectRemoteEnvironment))]
	[KnownType(typeof(TcpRemoteEnvironment))]
	[KnownType(typeof(HttpLinkRemoteEnvironment))]
	[KnownType(typeof(HttpUserDataRemoteEnvironment))]
	[KnownType(typeof(DpcRemoteEnvironment))]
	[Serializable]
	public class BaseRemoteEnvironment
	{
		// Token: 0x0600392E RID: 14638 RVA: 0x00002061 File Offset: 0x00000261
		protected BaseRemoteEnvironment()
		{
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x000BF521 File Offset: 0x000BD721
		protected BaseRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault)
		{
			this.RemoteEnvironmentClassId = reClassId;
			this.RemoteEnvironmentClass = reClass;
			this.Name = name;
			this.CodePage = codePage;
			this.TimeOut = timeOut;
			this.IsDefault = isDefault;
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06003930 RID: 14640 RVA: 0x000BF556 File Offset: 0x000BD756
		// (set) Token: 0x06003931 RID: 14641 RVA: 0x000BF55E File Offset: 0x000BD75E
		public string RemoteEnvironmentClassId { get; set; }

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06003932 RID: 14642 RVA: 0x000BF567 File Offset: 0x000BD767
		// (set) Token: 0x06003933 RID: 14643 RVA: 0x000BF56F File Offset: 0x000BD76F
		[DataMember]
		public RemoteEnvironmentClass RemoteEnvironmentClass { get; set; }

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06003934 RID: 14644 RVA: 0x000BF578 File Offset: 0x000BD778
		// (set) Token: 0x06003935 RID: 14645 RVA: 0x000BF580 File Offset: 0x000BD780
		[DataMember]
		[Description("Name of the Remote Environment")]
		[Category("General")]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("Name");
				}
				this.name = value;
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06003936 RID: 14646 RVA: 0x000BF59C File Offset: 0x000BD79C
		// (set) Token: 0x06003937 RID: 14647 RVA: 0x000BF5A4 File Offset: 0x000BD7A4
		[DataMember]
		[Description("Host codepage")]
		[Category("General")]
		public int CodePage
		{
			get
			{
				return this.codePage;
			}
			set
			{
				if (value < 0 || value > 65535)
				{
					throw new ArgumentOutOfRangeException("CodePage");
				}
				this.codePage = value;
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06003938 RID: 14648 RVA: 0x000BF5C4 File Offset: 0x000BD7C4
		// (set) Token: 0x06003939 RID: 14649 RVA: 0x000BF5CC File Offset: 0x000BD7CC
		[DataMember]
		[Description("Timeout value in integer")]
		[Category("General")]
		public int TimeOut
		{
			get
			{
				return this.timeOut;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("TimeOut");
				}
				this.timeOut = value;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x0600393A RID: 14650 RVA: 0x000BF5E4 File Offset: 0x000BD7E4
		// (set) Token: 0x0600393B RID: 14651 RVA: 0x000BF5EC File Offset: 0x000BD7EC
		[Category("General")]
		[Browsable(false)]
		public bool IsDefault { get; set; }

		// Token: 0x04002143 RID: 8515
		private string name;

		// Token: 0x04002144 RID: 8516
		private int codePage;

		// Token: 0x04002145 RID: 8517
		private int timeOut;
	}
}
