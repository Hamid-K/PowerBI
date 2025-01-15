using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000037 RID: 55
	[Serializable]
	public class MashupPermissionException : MashupSecurityException
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000B3E6 File Offset: 0x000095E6
		// (set) Token: 0x060002CB RID: 715 RVA: 0x0000B3EE File Offset: 0x000095EE
		public DataSource DataSource { get; private set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000B3F7 File Offset: 0x000095F7
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0000B3FF File Offset: 0x000095FF
		public MashupPermission Permission { get; private set; }

		// Token: 0x060002CE RID: 718 RVA: 0x0000B408 File Offset: 0x00009608
		public MashupPermissionException(string message, DataSource dataSource, MashupPermission mashupPermission)
			: base(message)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			if (mashupPermission == null)
			{
				throw new ArgumentNullException("mashupPermission");
			}
			this.DataSource = dataSource;
			this.Permission = mashupPermission;
			this.Data[MashupPermissionException.DataSourceKindKey] = dataSource.Kind;
			this.Data[MashupPermissionException.DataSourcePathKey] = dataSource.Path;
			this.Data[MashupPermissionException.PermissionKindKey] = mashupPermission.Kind;
			this.Data[MashupPermissionException.PermissionValueKey] = mashupPermission.Value;
			this.Data[MashupPermissionException.PermissionPropertiesKey] = mashupPermission.SerializedProperties;
			base.InitData();
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000B4BC File Offset: 0x000096BC
		protected MashupPermissionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.DataSource = new DataSource(info.GetString("DataSourceKind"), info.GetString("DataSourcePath"));
			this.Permission = new MashupPermission(info.GetString("PermissionKind"), info.GetString("PermissionValue"), MashupPermission.DeserializeProperties(info.GetString("PermissionProperties")));
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000B523 File Offset: 0x00009723
		public override string Reason
		{
			get
			{
				return this.Permission.Kind;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000B530 File Offset: 0x00009730
		public override IEnumerable<DataSource> DataSources
		{
			get
			{
				return new DataSource[] { this.DataSource };
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000B544 File Offset: 0x00009744
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("DataSourceKind", this.DataSource.Kind);
			info.AddValue("DataSourcePath", this.DataSource.Path);
			info.AddValue("PermissionKind", this.Permission.Kind);
			info.AddValue("PermissionValue", this.Permission.Value);
			info.AddValue("PermissionProperties", this.Permission.SerializedProperties);
			base.GetObjectData(info, context);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000B5C7 File Offset: 0x000097C7
		internal static string PermissionDataKey(string key)
		{
			return MashupException.DataKey(new string[] { "PermissionError", key });
		}

		// Token: 0x0400016D RID: 365
		private const string DataSourceKind = "DataSourceKind";

		// Token: 0x0400016E RID: 366
		private const string DataSourcePath = "DataSourcePath";

		// Token: 0x0400016F RID: 367
		private const string PermissionKind = "PermissionKind";

		// Token: 0x04000170 RID: 368
		private const string PermissionValue = "PermissionValue";

		// Token: 0x04000171 RID: 369
		private const string PermissionProperties = "PermissionProperties";

		// Token: 0x04000172 RID: 370
		internal const string PermissionErrorPart = "PermissionError";

		// Token: 0x04000173 RID: 371
		internal static string DataSourceKindKey = MashupPermissionException.PermissionDataKey("DataSourceKind");

		// Token: 0x04000174 RID: 372
		internal static string DataSourcePathKey = MashupPermissionException.PermissionDataKey("DataSourcePath");

		// Token: 0x04000175 RID: 373
		internal static string PermissionKindKey = MashupPermissionException.PermissionDataKey("PermissionKind");

		// Token: 0x04000176 RID: 374
		internal static string PermissionValueKey = MashupPermissionException.PermissionDataKey("PermissionValue");

		// Token: 0x04000177 RID: 375
		internal static string PermissionPropertiesKey = MashupPermissionException.PermissionDataKey("PermissionProperties");
	}
}
