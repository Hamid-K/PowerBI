using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B3E RID: 2878
	public class Rf2hMcdFolder : Rf2hFolderWithFields
	{
		// Token: 0x170015F7 RID: 5623
		// (get) Token: 0x06005AF7 RID: 23287 RVA: 0x001769EF File Offset: 0x00174BEF
		// (set) Token: 0x06005AF8 RID: 23288 RVA: 0x001769F8 File Offset: 0x00174BF8
		public string Domain
		{
			get
			{
				return this.GetProperty<string>(McdField.Msd);
			}
			set
			{
				this.SetProperty<string>(McdField.Msd, value);
			}
		}

		// Token: 0x170015F8 RID: 5624
		// (get) Token: 0x06005AF9 RID: 23289 RVA: 0x00176A02 File Offset: 0x00174C02
		// (set) Token: 0x06005AFA RID: 23290 RVA: 0x00176A0B File Offset: 0x00174C0B
		public string MessageSet
		{
			get
			{
				return this.GetProperty<string>(McdField.Set);
			}
			set
			{
				this.SetProperty<string>(McdField.Set, value);
			}
		}

		// Token: 0x170015F9 RID: 5625
		// (get) Token: 0x06005AFB RID: 23291 RVA: 0x00176A15 File Offset: 0x00174C15
		// (set) Token: 0x06005AFC RID: 23292 RVA: 0x00176A1E File Offset: 0x00174C1E
		public string Type
		{
			get
			{
				return this.GetProperty<string>(McdField.Type);
			}
			set
			{
				this.SetProperty<string>(McdField.Type, value);
			}
		}

		// Token: 0x170015FA RID: 5626
		// (get) Token: 0x06005AFD RID: 23293 RVA: 0x00176A28 File Offset: 0x00174C28
		// (set) Token: 0x06005AFE RID: 23294 RVA: 0x00176A31 File Offset: 0x00174C31
		public string Format
		{
			get
			{
				return this.GetProperty<string>(McdField.Fmt);
			}
			set
			{
				this.SetProperty<string>(McdField.Fmt, value);
			}
		}

		// Token: 0x06005B00 RID: 23296 RVA: 0x00176A7B File Offset: 0x00174C7B
		public Rf2hMcdFolder()
			: this(null)
		{
		}

		// Token: 0x06005B01 RID: 23297 RVA: 0x00176A84 File Offset: 0x00174C84
		public Rf2hMcdFolder(string completeString)
			: base(Rf2hFolderType.Mcd, Rf2hMcdFolder.tagToFieldTypeAndIndex, completeString)
		{
		}

		// Token: 0x06005B02 RID: 23298 RVA: 0x001769DC File Offset: 0x00174BDC
		private T GetProperty<T>(McdField propertyIndex)
		{
			return base.GetProperty<T>((int)propertyIndex);
		}

		// Token: 0x06005B03 RID: 23299 RVA: 0x001769E5 File Offset: 0x00174BE5
		private void SetProperty<T>(McdField propertyIndex, T value)
		{
			base.SetProperty<T>((int)propertyIndex, value);
		}

		// Token: 0x040047B0 RID: 18352
		private static FolderFieldType[] fieldTypes = new FolderFieldType[]
		{
			FolderFieldType.String,
			FolderFieldType.String,
			FolderFieldType.String,
			FolderFieldType.String
		};

		// Token: 0x040047B1 RID: 18353
		private static string[] fieldTags = Enum.GetNames(typeof(McdField));

		// Token: 0x040047B2 RID: 18354
		private static Dictionary<string, FieldTypeAndIndex> tagToFieldTypeAndIndex = Rf2hFolderWithFields.GenerateFieldInfo(Rf2hMcdFolder.fieldTypes, Rf2hMcdFolder.fieldTags);
	}
}
