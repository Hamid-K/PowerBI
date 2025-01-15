using System;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B04 RID: 2820
	internal class GoogleAnalyticsCubeObject
	{
		// Token: 0x06004E36 RID: 20022 RVA: 0x001039B4 File Offset: 0x00101BB4
		public GoogleAnalyticsCubeObject(string id, string name, string description, GoogleAnalyticsDataType type, GoogleAnalyticsCubeObjectPath path, GoogleAnalyticsCubeObjectStatus status, GoogleAnalyticsCubeObjectKind kind, string baseName)
		{
			this.id = id;
			this.name = name;
			this.description = description;
			this.type = type;
			this.path = path;
			this.status = status;
			this.kind = kind;
			this.baseName = baseName;
		}

		// Token: 0x1700187B RID: 6267
		// (get) Token: 0x06004E37 RID: 20023 RVA: 0x00103A04 File Offset: 0x00101C04
		public GoogleAnalyticsCubeObjectKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x1700187C RID: 6268
		// (get) Token: 0x06004E38 RID: 20024 RVA: 0x00103A0C File Offset: 0x00101C0C
		public string ID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x1700187D RID: 6269
		// (get) Token: 0x06004E39 RID: 20025 RVA: 0x00103A14 File Offset: 0x00101C14
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700187E RID: 6270
		// (get) Token: 0x06004E3A RID: 20026 RVA: 0x00103A1C File Offset: 0x00101C1C
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x1700187F RID: 6271
		// (get) Token: 0x06004E3B RID: 20027 RVA: 0x00103A24 File Offset: 0x00101C24
		public GoogleAnalyticsDataType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001880 RID: 6272
		// (get) Token: 0x06004E3C RID: 20028 RVA: 0x00103A2C File Offset: 0x00101C2C
		public GoogleAnalyticsCubeObjectPath Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x17001881 RID: 6273
		// (get) Token: 0x06004E3D RID: 20029 RVA: 0x00103A34 File Offset: 0x00101C34
		public GoogleAnalyticsCubeObjectStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x17001882 RID: 6274
		// (get) Token: 0x06004E3E RID: 20030 RVA: 0x00103A3C File Offset: 0x00101C3C
		public string BaseName
		{
			get
			{
				return this.baseName;
			}
		}

		// Token: 0x06004E3F RID: 20031 RVA: 0x00103A44 File Offset: 0x00101C44
		public override bool Equals(object obj)
		{
			GoogleAnalyticsCubeObject googleAnalyticsCubeObject = obj as GoogleAnalyticsCubeObject;
			return googleAnalyticsCubeObject != null && googleAnalyticsCubeObject.id == this.id;
		}

		// Token: 0x06004E40 RID: 20032 RVA: 0x00103A6E File Offset: 0x00101C6E
		public override int GetHashCode()
		{
			return this.id.GetHashCode();
		}

		// Token: 0x04002A00 RID: 10752
		private readonly string id;

		// Token: 0x04002A01 RID: 10753
		private readonly string name;

		// Token: 0x04002A02 RID: 10754
		private readonly string description;

		// Token: 0x04002A03 RID: 10755
		private readonly GoogleAnalyticsDataType type;

		// Token: 0x04002A04 RID: 10756
		private readonly GoogleAnalyticsCubeObjectPath path;

		// Token: 0x04002A05 RID: 10757
		private readonly GoogleAnalyticsCubeObjectStatus status;

		// Token: 0x04002A06 RID: 10758
		private readonly GoogleAnalyticsCubeObjectKind kind;

		// Token: 0x04002A07 RID: 10759
		private readonly string baseName;
	}
}
