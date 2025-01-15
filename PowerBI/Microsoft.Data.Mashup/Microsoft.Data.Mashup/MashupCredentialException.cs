using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	public class MashupCredentialException : MashupSecurityException
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00009DF0 File Offset: 0x00007FF0
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00009DF8 File Offset: 0x00007FF8
		public DataSourceReference DataSourceReference { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00009E01 File Offset: 0x00008001
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00009E09 File Offset: 0x00008009
		public DataSourceReference DataSourceReferenceOrigin { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00009E12 File Offset: 0x00008012
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00009E1A File Offset: 0x0000801A
		public DataSource DataSource { get; private set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00009E23 File Offset: 0x00008023
		// (set) Token: 0x06000246 RID: 582 RVA: 0x00009E2B File Offset: 0x0000802B
		public DataSource DataSourceOrigin { get; private set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00009E34 File Offset: 0x00008034
		public override string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00009E3C File Offset: 0x0000803C
		public MashupCredentialException(string message, string reason, DataSource dataSource, DataSource dataSourceOrigin)
			: base(message)
		{
			this.reason = reason;
			this.Data[MashupCredentialException.ReasonKey] = reason;
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			this.SetDataSources(dataSource, dataSourceOrigin);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009E74 File Offset: 0x00008074
		public MashupCredentialException(string message, DataSource dataSource, DataSource dataSourceOrigin)
			: this(message, null, dataSource, dataSourceOrigin)
		{
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009E80 File Offset: 0x00008080
		public MashupCredentialException(string message, string reason, DataSource dataSource)
			: this(message, reason, dataSource, null)
		{
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00009E8C File Offset: 0x0000808C
		public MashupCredentialException(string message, DataSource dataSource)
			: this(message, dataSource, null)
		{
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00009E98 File Offset: 0x00008098
		public MashupCredentialException(string message, string reason, DataSourceReference dataSourceReference, DataSourceReference dataSourceReferenceOrigin)
			: base(message)
		{
			this.reason = reason;
			this.Data[MashupCredentialException.ReasonKey] = reason;
			if (dataSourceReference == null)
			{
				throw new ArgumentNullException("dataSourceReference");
			}
			this.DataSourceReference = dataSourceReference;
			this.Data[MashupCredentialException.DataSourceReferenceKey] = dataSourceReference.ToJson();
			this.DataSourceReferenceOrigin = dataSourceReferenceOrigin;
			DataSource dataSource = null;
			if (dataSourceReferenceOrigin != null)
			{
				dataSource = dataSourceReferenceOrigin.DataSource;
				this.Data[MashupCredentialException.DataSourceReferenceOriginKey] = dataSourceReferenceOrigin.ToJson();
			}
			this.SetDataSources(dataSourceReference.DataSource, dataSource);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00009F29 File Offset: 0x00008129
		public MashupCredentialException(string message, DataSourceReference dataSourceReference, DataSourceReference dataSourceReferenceOrigin)
			: this(message, null, dataSourceReference, dataSourceReferenceOrigin)
		{
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009F35 File Offset: 0x00008135
		public MashupCredentialException(string message, string reason, DataSourceReference dataSourceReference)
			: this(message, reason, dataSourceReference, null)
		{
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00009F41 File Offset: 0x00008141
		public MashupCredentialException(string message, DataSourceReference dataSourceReference)
			: this(message, dataSourceReference, null)
		{
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00009F4C File Offset: 0x0000814C
		public override IEnumerable<DataSource> DataSources
		{
			get
			{
				return new DataSource[] { this.DataSource };
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00009F60 File Offset: 0x00008160
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("DataSourceKind", this.DataSource.Kind);
			info.AddValue("DataSourcePath", this.DataSource.Path);
			info.AddValue("DataSourceReference", (this.DataSourceReference == null) ? null : this.DataSourceReference.ToJson());
			info.AddValue("DataSourceOriginKind", (this.DataSourceOrigin == null) ? null : this.DataSourceOrigin.Kind);
			info.AddValue("DataSourceOriginPath", (this.DataSourceOrigin == null) ? null : this.DataSourceOrigin.Path);
			info.AddValue("DataSourceReferenceOrigin", (this.DataSourceReferenceOrigin == null) ? null : this.DataSourceReferenceOrigin.ToJson());
			info.AddValue("Reason", (this.Reason == null) ? null : this.Reason);
			base.GetObjectData(info, context);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000A044 File Offset: 0x00008244
		protected MashupCredentialException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.DataSource = new DataSource(info.GetString("DataSourceKind"), info.GetString("DataSourcePath"));
			string @string = info.GetString("DataSourceReference");
			if (@string != null)
			{
				this.DataSourceReference = new DataSourceReference(@string);
			}
			string string2 = info.GetString("DataSourceOriginKind");
			string string3 = info.GetString("DataSourceOriginPath");
			if (string2 != null && string3 != null)
			{
				this.DataSourceOrigin = new DataSource(string2, string3);
			}
			string string4 = info.GetString("DataSourceReferenceOrigin");
			if (@string != null)
			{
				this.DataSourceReferenceOrigin = new DataSourceReference(string4);
			}
			this.reason = info.GetString("Reason");
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000A0EC File Offset: 0x000082EC
		internal static string CredentialDataKey(string key)
		{
			return MashupException.DataKey(new string[] { "CredentialError", key });
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000A108 File Offset: 0x00008308
		private void SetDataSources(DataSource dataSource, DataSource dataSourceOrigin)
		{
			this.DataSource = dataSource;
			this.Data[MashupCredentialException.DataSourceKindKey] = dataSource.Kind;
			this.Data[MashupCredentialException.DataSourcePathKey] = dataSource.Path;
			this.DataSourceOrigin = dataSourceOrigin;
			if (dataSourceOrigin != null)
			{
				this.Data[MashupCredentialException.DataSourceOriginKindKey] = dataSourceOrigin.Kind;
				this.Data[MashupCredentialException.DataSourceOriginPathKey] = dataSourceOrigin.Path;
			}
			base.InitData();
		}

		// Token: 0x04000118 RID: 280
		private const string ReasonPart = "Reason";

		// Token: 0x04000119 RID: 281
		private const string DataSourceKind = "DataSourceKind";

		// Token: 0x0400011A RID: 282
		private const string DataSourcePath = "DataSourcePath";

		// Token: 0x0400011B RID: 283
		private const string DataSourceReferenceString = "DataSourceReference";

		// Token: 0x0400011C RID: 284
		private const string DataSourceOriginKind = "DataSourceOriginKind";

		// Token: 0x0400011D RID: 285
		private const string DataSourceOriginPath = "DataSourceOriginPath";

		// Token: 0x0400011E RID: 286
		private const string DataSourceReferenceOriginString = "DataSourceReferenceOrigin";

		// Token: 0x0400011F RID: 287
		internal const string CredentialErrorPart = "CredentialError";

		// Token: 0x04000120 RID: 288
		internal static string ReasonKey = MashupCredentialException.CredentialDataKey("Reason");

		// Token: 0x04000121 RID: 289
		internal static string DataSourceKindKey = MashupCredentialException.CredentialDataKey("DataSourceKind");

		// Token: 0x04000122 RID: 290
		internal static string DataSourcePathKey = MashupCredentialException.CredentialDataKey("DataSourcePath");

		// Token: 0x04000123 RID: 291
		internal static string DataSourceReferenceKey = MashupCredentialException.CredentialDataKey("DataSourceReference");

		// Token: 0x04000124 RID: 292
		internal static string DataSourceOriginKindKey = MashupCredentialException.CredentialDataKey("DataSourceOriginKind");

		// Token: 0x04000125 RID: 293
		internal static string DataSourceOriginPathKey = MashupCredentialException.CredentialDataKey("DataSourceOriginPath");

		// Token: 0x04000126 RID: 294
		internal static string DataSourceReferenceOriginKey = MashupCredentialException.CredentialDataKey("DataSourceReferenceOrigin");

		// Token: 0x04000127 RID: 295
		private string reason;
	}
}
