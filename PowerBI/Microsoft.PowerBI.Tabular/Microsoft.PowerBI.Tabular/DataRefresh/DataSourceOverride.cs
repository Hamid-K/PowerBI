using System;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular.DataRefresh
{
	// Token: 0x02000211 RID: 529
	public abstract class DataSourceOverride : IObjectOverride
	{
		// Token: 0x06001DCF RID: 7631 RVA: 0x000C98D0 File Offset: 0x000C7AD0
		internal DataSourceOverride()
		{
		}

		// Token: 0x06001DD0 RID: 7632
		internal abstract MetadataObject GetOriginalObject();

		// Token: 0x06001DD1 RID: 7633
		internal abstract ObjectPath GetOriginalObjectPath();

		// Token: 0x06001DD2 RID: 7634
		internal abstract ReplacementPropertiesCollection GetReplacementProperties();

		// Token: 0x06001DD3 RID: 7635
		internal abstract void EnsureAllReferencesResolved(Model model);

		// Token: 0x06001DD4 RID: 7636
		internal abstract bool ReadPropertyFromJson(JsonTextReader jsonReader);

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001DD5 RID: 7637 RVA: 0x000C98D8 File Offset: 0x000C7AD8
		ObjectType IObjectOverride.ObjectType
		{
			get
			{
				return ObjectType.DataSource;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x000C98DB File Offset: 0x000C7ADB
		MetadataObject IObjectOverride.OriginalObject
		{
			get
			{
				return this.GetOriginalObject();
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x000C98E3 File Offset: 0x000C7AE3
		ObjectPath IObjectOverride.OriginalObjectPath
		{
			get
			{
				return this.GetOriginalObjectPath();
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x000C98EB File Offset: 0x000C7AEB
		ReplacementPropertiesCollection IObjectOverride.ReplacementProperties
		{
			get
			{
				return this.GetReplacementProperties();
			}
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x000C98F3 File Offset: 0x000C7AF3
		void IObjectOverride.EnsureAllReferencesResolved(Model model)
		{
			this.EnsureAllReferencesResolved(model);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x000C98FC File Offset: 0x000C7AFC
		bool IObjectOverride.ReadPropertyFromJson(JsonTextReader jsonReader)
		{
			return this.ReadPropertyFromJson(jsonReader);
		}

		// Token: 0x02000440 RID: 1088
		internal static class OverrideName
		{
			// Token: 0x04001423 RID: 5155
			public const string ConnectionString = "ConnectionString";

			// Token: 0x04001424 RID: 5156
			public const string ImpersonationMode = "ImpersonationMode";

			// Token: 0x04001425 RID: 5157
			public const string Account = "Account";

			// Token: 0x04001426 RID: 5158
			public const string Password = "Password";

			// Token: 0x04001427 RID: 5159
			public const string MaxConnections = "MaxConnections";

			// Token: 0x04001428 RID: 5160
			public const string Isolation = "Isolation";

			// Token: 0x04001429 RID: 5161
			public const string Timeout = "Timeout";

			// Token: 0x0400142A RID: 5162
			public const string Provider = "Provider";

			// Token: 0x0400142B RID: 5163
			public const string ConnectionDetails = "ConnectionDetails";

			// Token: 0x0400142C RID: 5164
			public const string Options = "Options";

			// Token: 0x0400142D RID: 5165
			public const string Credential = "Credential";

			// Token: 0x0400142E RID: 5166
			public const string ContextExpression = "ContextExpression";
		}
	}
}
