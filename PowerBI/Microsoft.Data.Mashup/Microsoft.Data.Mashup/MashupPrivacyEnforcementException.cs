using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000039 RID: 57
	[Serializable]
	public class MashupPrivacyEnforcementException : MashupPrivacyException
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x0000B65D File Offset: 0x0000985D
		public MashupPrivacyEnforcementException(string message, DataSource[] dataSources, Exception innerException)
			: base(message, innerException)
		{
			if (dataSources == null)
			{
				throw new ArgumentNullException("dataSources");
			}
			this.dataSources = dataSources;
			base.InitData();
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000B682 File Offset: 0x00009882
		private MashupPrivacyEnforcementException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.dataSources = Util.DeserializeDataSourcesArray(info);
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000B698 File Offset: 0x00009898
		public override IEnumerable<DataSource> DataSources
		{
			get
			{
				return new ReadOnlyCollection<DataSource>(this.dataSources);
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000B6A5 File Offset: 0x000098A5
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			Util.Serialize(info, this.dataSources);
			base.GetObjectData(info, context);
		}

		// Token: 0x0400017D RID: 381
		private DataSource[] dataSources;
	}
}
