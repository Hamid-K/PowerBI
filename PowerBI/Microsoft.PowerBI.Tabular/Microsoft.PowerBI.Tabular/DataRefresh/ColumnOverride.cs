using System;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular.DataRefresh
{
	// Token: 0x0200020F RID: 527
	public abstract class ColumnOverride : IObjectOverride
	{
		// Token: 0x06001DB6 RID: 7606 RVA: 0x000C96B6 File Offset: 0x000C78B6
		internal ColumnOverride()
		{
		}

		// Token: 0x06001DB7 RID: 7607
		internal abstract MetadataObject GetOriginalObject();

		// Token: 0x06001DB8 RID: 7608
		internal abstract ObjectPath GetOriginalObjectPath();

		// Token: 0x06001DB9 RID: 7609
		internal abstract ReplacementPropertiesCollection GetReplacementProperties();

		// Token: 0x06001DBA RID: 7610
		internal abstract void EnsureAllReferencesResolved(Model model);

		// Token: 0x06001DBB RID: 7611
		internal abstract bool ReadPropertyFromJson(JsonTextReader jsonReader);

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001DBC RID: 7612 RVA: 0x000C96BE File Offset: 0x000C78BE
		ObjectType IObjectOverride.ObjectType
		{
			get
			{
				return ObjectType.Column;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001DBD RID: 7613 RVA: 0x000C96C1 File Offset: 0x000C78C1
		MetadataObject IObjectOverride.OriginalObject
		{
			get
			{
				return this.GetOriginalObject();
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001DBE RID: 7614 RVA: 0x000C96C9 File Offset: 0x000C78C9
		ObjectPath IObjectOverride.OriginalObjectPath
		{
			get
			{
				return this.GetOriginalObjectPath();
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x000C96D1 File Offset: 0x000C78D1
		ReplacementPropertiesCollection IObjectOverride.ReplacementProperties
		{
			get
			{
				return this.GetReplacementProperties();
			}
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x000C96D9 File Offset: 0x000C78D9
		void IObjectOverride.EnsureAllReferencesResolved(Model model)
		{
			this.EnsureAllReferencesResolved(model);
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x000C96E2 File Offset: 0x000C78E2
		bool IObjectOverride.ReadPropertyFromJson(JsonTextReader jsonReader)
		{
			return this.ReadPropertyFromJson(jsonReader);
		}

		// Token: 0x0200043E RID: 1086
		internal static class OverrideName
		{
			// Token: 0x04001421 RID: 5153
			public const string SourceColumn = "SourceColumn";
		}
	}
}
