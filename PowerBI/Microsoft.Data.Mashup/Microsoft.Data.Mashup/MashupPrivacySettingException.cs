using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200003C RID: 60
	[Serializable]
	public class MashupPrivacySettingException : MashupPrivacyException
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x0000B6E7 File Offset: 0x000098E7
		public MashupPrivacySettingException(string message, DataSource[] dataSources, Exception innerException)
			: base(message, innerException)
		{
			if (dataSources == null)
			{
				throw new ArgumentNullException("dataSources");
			}
			this.dataSources = dataSources;
			base.InitData();
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000B70C File Offset: 0x0000990C
		private MashupPrivacySettingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.dataSources = Util.DeserializeDataSourcesArray(info);
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000B722 File Offset: 0x00009922
		public override string Reason
		{
			get
			{
				return "PrivacySetting";
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000B729 File Offset: 0x00009929
		public override IEnumerable<DataSource> DataSources
		{
			get
			{
				return new ReadOnlyCollection<DataSource>(this.dataSources);
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000B736 File Offset: 0x00009936
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			Util.Serialize(info, this.dataSources);
			base.GetObjectData(info, context);
		}

		// Token: 0x04000181 RID: 385
		private DataSource[] dataSources;
	}
}
