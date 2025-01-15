using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Fabric.Data
{
	// Token: 0x020003C2 RID: 962
	[DataContract(Name = "ServiceReplicaSet", Namespace = "http://schemas.microsoft.com/2008/casdata")]
	internal class ServiceReplicaSet
	{
		// Token: 0x060021D4 RID: 8660 RVA: 0x00068564 File Offset: 0x00066764
		internal ServiceReplicaSet(string primary, List<string> secondaries, long version)
		{
			this.m_primary = primary;
			this.m_secondaryReplicas = ((secondaries != null) ? secondaries : ServiceReplicaSet.s_emptySecondaryList);
			this.m_version = version;
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060021D5 RID: 8661 RVA: 0x0006858B File Offset: 0x0006678B
		// (set) Token: 0x060021D6 RID: 8662 RVA: 0x00068593 File Offset: 0x00066793
		[DataMember]
		public string Primary
		{
			get
			{
				return this.m_primary;
			}
			private set
			{
				this.m_primary = value;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x0006859C File Offset: 0x0006679C
		// (set) Token: 0x060021D8 RID: 8664 RVA: 0x000685A4 File Offset: 0x000667A4
		[DataMember]
		public IEnumerable<string> SecondaryReplicas
		{
			get
			{
				return this.m_secondaryReplicas;
			}
			private set
			{
				this.m_secondaryReplicas = value;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x000685AD File Offset: 0x000667AD
		// (set) Token: 0x060021DA RID: 8666 RVA: 0x000685B5 File Offset: 0x000667B5
		[DataMember]
		internal long Version
		{
			get
			{
				return this.m_version;
			}
			private set
			{
				this.m_version = value;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060021DB RID: 8667 RVA: 0x000685BE File Offset: 0x000667BE
		public bool IsUsable
		{
			get
			{
				return !string.IsNullOrEmpty(this.m_primary);
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060021DC RID: 8668 RVA: 0x000685D0 File Offset: 0x000667D0
		public IEnumerable<string> AllReplicas
		{
			get
			{
				if (this.IsUsable)
				{
					yield return this.m_primary;
				}
				foreach (string secondary in this.m_secondaryReplicas)
				{
					yield return secondary;
				}
				yield break;
			}
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x000685F0 File Offset: 0x000667F0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.Append(this.m_primary);
			foreach (string text in this.m_secondaryReplicas)
			{
				stringBuilder.AppendFormat(",{0}", text);
			}
			stringBuilder.AppendFormat(" ({0})", this.m_version);
			return stringBuilder.ToString();
		}

		// Token: 0x04001584 RID: 5508
		private string m_primary;

		// Token: 0x04001585 RID: 5509
		private IEnumerable<string> m_secondaryReplicas;

		// Token: 0x04001586 RID: 5510
		private long m_version;

		// Token: 0x04001587 RID: 5511
		private static readonly List<string> s_emptySecondaryList = new List<string>();
	}
}
