using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B47 RID: 2887
	public class Rf2hUsrFolder : Rf2hFolderWithFieldsAndProperties
	{
		// Token: 0x17001609 RID: 5641
		// (get) Token: 0x06005B3D RID: 23357 RVA: 0x00177956 File Offset: 0x00175B56
		public string ContentType
		{
			get
			{
				return "text/xml; charset=utf-8";
			}
		}

		// Token: 0x1700160A RID: 5642
		// (get) Token: 0x06005B3E RID: 23358 RVA: 0x0017795D File Offset: 0x00175B5D
		// (set) Token: 0x06005B3F RID: 23359 RVA: 0x00177966 File Offset: 0x00175B66
		public string EndPointURL
		{
			get
			{
				return this.GetProperty<string>(UsrField.EndPointURL);
			}
			set
			{
				this.SetProperty<string>(UsrField.EndPointURL, value);
			}
		}

		// Token: 0x1700160B RID: 5643
		// (get) Token: 0x06005B40 RID: 23360 RVA: 0x00177970 File Offset: 0x00175B70
		// (set) Token: 0x06005B41 RID: 23361 RVA: 0x00177979 File Offset: 0x00175B79
		public string TargetService
		{
			get
			{
				return this.GetProperty<string>(UsrField.TargetService);
			}
			set
			{
				this.SetProperty<string>(UsrField.TargetService, value);
			}
		}

		// Token: 0x1700160C RID: 5644
		// (get) Token: 0x06005B42 RID: 23362 RVA: 0x00177983 File Offset: 0x00175B83
		// (set) Token: 0x06005B43 RID: 23363 RVA: 0x0017798C File Offset: 0x00175B8C
		public string SoapAction
		{
			get
			{
				return this.GetProperty<string>(UsrField.SoapAction);
			}
			set
			{
				this.SetProperty<string>(UsrField.SoapAction, value);
			}
		}

		// Token: 0x1700160D RID: 5645
		// (get) Token: 0x06005B44 RID: 23364 RVA: 0x00177996 File Offset: 0x00175B96
		// (set) Token: 0x06005B45 RID: 23365 RVA: 0x0017799F File Offset: 0x00175B9F
		public string TransportVersion
		{
			get
			{
				return this.GetProperty<string>(UsrField.TransportVersion);
			}
			set
			{
				this.SetProperty<string>(UsrField.TransportVersion, value);
			}
		}

		// Token: 0x06005B46 RID: 23366 RVA: 0x001779AC File Offset: 0x00175BAC
		static Rf2hUsrFolder()
		{
			string[] array = new string[Rf2hUsrFolder.fieldTags.Length];
			for (int i = 0; i < Rf2hUsrFolder.fieldTags.Length; i++)
			{
				array[i] = Rf2hUsrFolder.fieldTags[i].Substring(0, 1).ToLowerInvariant() + Rf2hUsrFolder.fieldTags[i].Substring(1);
			}
			Rf2hUsrFolder.tagToFieldTypeAndIndex = Rf2hFolderWithFields.GenerateFieldInfo(Rf2hUsrFolder.fieldTypes, array);
		}

		// Token: 0x06005B47 RID: 23367 RVA: 0x00177A3A File Offset: 0x00175C3A
		public Rf2hUsrFolder()
			: this(null)
		{
		}

		// Token: 0x06005B48 RID: 23368 RVA: 0x00177A44 File Offset: 0x00175C44
		public Rf2hUsrFolder(string completeString)
			: base(Rf2hFolderType.Usr, Rf2hFolderType.Usr.ToString().ToLowerInvariant(), Rf2hUsrFolder.tagToFieldTypeAndIndex, completeString)
		{
		}

		// Token: 0x06005B49 RID: 23369 RVA: 0x001769DC File Offset: 0x00174BDC
		private T GetProperty<T>(UsrField propertyIndex)
		{
			return base.GetProperty<T>((int)propertyIndex);
		}

		// Token: 0x06005B4A RID: 23370 RVA: 0x001769E5 File Offset: 0x00174BE5
		private void SetProperty<T>(UsrField propertyIndex, T value)
		{
			base.SetProperty<T>((int)propertyIndex, value);
		}

		// Token: 0x040047E3 RID: 18403
		private static FolderFieldType[] fieldTypes = new FolderFieldType[]
		{
			FolderFieldType.String,
			FolderFieldType.String,
			FolderFieldType.String,
			FolderFieldType.String,
			FolderFieldType.String
		};

		// Token: 0x040047E4 RID: 18404
		private static string[] fieldTags = Enum.GetNames(typeof(UsrField));

		// Token: 0x040047E5 RID: 18405
		private static Dictionary<string, FieldTypeAndIndex> tagToFieldTypeAndIndex;
	}
}
