using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200003D RID: 61
	[Serializable]
	public class MashupPrivacyTraitException : MashupPrivacyException
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x0000B74C File Offset: 0x0000994C
		public MashupPrivacyTraitException(string message, DataSource dataSource, DataSourceSetting originalSetting, string jsonTraits, Exception innerException)
			: base(message, innerException)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSources");
			}
			this.dataSource = dataSource;
			this.originalSetting = originalSetting;
			this.jsonTraits = jsonTraits;
			base.InitData();
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000B784 File Offset: 0x00009984
		private MashupPrivacyTraitException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			KeyValuePair<DataSource, DataSourceSetting> keyValuePair = DataSourceSettings.ToDictionary(info.GetString("DataSourceSetting")).Single<KeyValuePair<DataSource, DataSourceSetting>>();
			this.dataSource = keyValuePair.Key;
			this.originalSetting = keyValuePair.Value;
			this.jsonTraits = info.GetString("Trait");
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000B7DA File Offset: 0x000099DA
		public override string Reason
		{
			get
			{
				return "PrivacyTraitSetting";
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000B7E1 File Offset: 0x000099E1
		public DataSource DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000B7E9 File Offset: 0x000099E9
		public DataSourceSetting OriginalDataSourceSetting
		{
			get
			{
				return this.originalSetting;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000B7F1 File Offset: 0x000099F1
		public string Traits
		{
			get
			{
				return this.jsonTraits;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000B7F9 File Offset: 0x000099F9
		public override IEnumerable<DataSource> DataSources
		{
			get
			{
				return new ReadOnlyCollection<DataSource>(new DataSource[] { this.dataSource });
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000B80F File Offset: 0x00009A0F
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("DataSourceSetting", DataSourceSettings.Create(this.dataSource, this.originalSetting));
			info.AddValue("Trait", this.jsonTraits);
			base.GetObjectData(info, context);
		}

		// Token: 0x04000182 RID: 386
		private const string DataSourceSettingKey = "DataSourceSetting";

		// Token: 0x04000183 RID: 387
		private const string TraitsKey = "Trait";

		// Token: 0x04000184 RID: 388
		private DataSource dataSource;

		// Token: 0x04000185 RID: 389
		private DataSourceSetting originalSetting;

		// Token: 0x04000186 RID: 390
		private string jsonTraits;
	}
}
