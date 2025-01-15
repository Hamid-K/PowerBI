using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B3C RID: 2876
	public class Rf2hJmsFolder : Rf2hFolderWithFields
	{
		// Token: 0x170015EE RID: 5614
		// (get) Token: 0x06005AE0 RID: 23264 RVA: 0x001768CC File Offset: 0x00174ACC
		// (set) Token: 0x06005AE1 RID: 23265 RVA: 0x001768D5 File Offset: 0x00174AD5
		public string Destination
		{
			get
			{
				return this.GetProperty<string>(JmsField.Dst);
			}
			set
			{
				this.SetProperty<string>(JmsField.Dst, value);
			}
		}

		// Token: 0x170015EF RID: 5615
		// (get) Token: 0x06005AE2 RID: 23266 RVA: 0x001768DF File Offset: 0x00174ADF
		// (set) Token: 0x06005AE3 RID: 23267 RVA: 0x001768E8 File Offset: 0x00174AE8
		public long? Expiration
		{
			get
			{
				return this.GetProperty<long?>(JmsField.Exp);
			}
			set
			{
				this.SetProperty<long?>(JmsField.Exp, value);
			}
		}

		// Token: 0x170015F0 RID: 5616
		// (get) Token: 0x06005AE4 RID: 23268 RVA: 0x001768F2 File Offset: 0x00174AF2
		// (set) Token: 0x06005AE5 RID: 23269 RVA: 0x001768FB File Offset: 0x00174AFB
		public string CorrelationId
		{
			get
			{
				return this.GetProperty<string>(JmsField.Cid);
			}
			set
			{
				this.SetProperty<string>(JmsField.Cid, value);
			}
		}

		// Token: 0x170015F1 RID: 5617
		// (get) Token: 0x06005AE6 RID: 23270 RVA: 0x00176905 File Offset: 0x00174B05
		// (set) Token: 0x06005AE7 RID: 23271 RVA: 0x0017690E File Offset: 0x00174B0E
		public int? Delivery
		{
			get
			{
				return this.GetProperty<int?>(JmsField.Dlv);
			}
			set
			{
				this.SetProperty<int?>(JmsField.Dlv, value);
			}
		}

		// Token: 0x170015F2 RID: 5618
		// (get) Token: 0x06005AE8 RID: 23272 RVA: 0x00176918 File Offset: 0x00174B18
		// (set) Token: 0x06005AE9 RID: 23273 RVA: 0x00176921 File Offset: 0x00174B21
		public int? Priority
		{
			get
			{
				return this.GetProperty<int?>(JmsField.Pri);
			}
			set
			{
				this.SetProperty<int?>(JmsField.Pri, value);
			}
		}

		// Token: 0x170015F3 RID: 5619
		// (get) Token: 0x06005AEA RID: 23274 RVA: 0x0017692B File Offset: 0x00174B2B
		// (set) Token: 0x06005AEB RID: 23275 RVA: 0x00176934 File Offset: 0x00174B34
		public string ReplyTo
		{
			get
			{
				return this.GetProperty<string>(JmsField.Rto);
			}
			set
			{
				this.SetProperty<string>(JmsField.Rto, value);
			}
		}

		// Token: 0x170015F4 RID: 5620
		// (get) Token: 0x06005AEC RID: 23276 RVA: 0x0017693E File Offset: 0x00174B3E
		// (set) Token: 0x06005AED RID: 23277 RVA: 0x00176947 File Offset: 0x00174B47
		public long? TimeStamp
		{
			get
			{
				return this.GetProperty<long?>(JmsField.Tms);
			}
			set
			{
				this.SetProperty<long?>(JmsField.Tms, value);
			}
		}

		// Token: 0x170015F5 RID: 5621
		// (get) Token: 0x06005AEE RID: 23278 RVA: 0x00176951 File Offset: 0x00174B51
		// (set) Token: 0x06005AEF RID: 23279 RVA: 0x0017695A File Offset: 0x00174B5A
		public string GroupId
		{
			get
			{
				return this.GetProperty<string>(JmsField.Gid);
			}
			set
			{
				this.SetProperty<string>(JmsField.Gid, value);
			}
		}

		// Token: 0x170015F6 RID: 5622
		// (get) Token: 0x06005AF0 RID: 23280 RVA: 0x00176964 File Offset: 0x00174B64
		// (set) Token: 0x06005AF1 RID: 23281 RVA: 0x0017696D File Offset: 0x00174B6D
		public int? GroupSeq
		{
			get
			{
				return this.GetProperty<int?>(JmsField.Seq);
			}
			set
			{
				this.SetProperty<int?>(JmsField.Seq, value);
			}
		}

		// Token: 0x06005AF3 RID: 23283 RVA: 0x001769C4 File Offset: 0x00174BC4
		public Rf2hJmsFolder()
			: this(null)
		{
		}

		// Token: 0x06005AF4 RID: 23284 RVA: 0x001769CD File Offset: 0x00174BCD
		public Rf2hJmsFolder(string completeString)
			: base(Rf2hFolderType.Jms, Rf2hJmsFolder.tagToFieldTypeAndIndex, completeString)
		{
		}

		// Token: 0x06005AF5 RID: 23285 RVA: 0x001769DC File Offset: 0x00174BDC
		private T GetProperty<T>(JmsField propertyIndex)
		{
			return base.GetProperty<T>((int)propertyIndex);
		}

		// Token: 0x06005AF6 RID: 23286 RVA: 0x001769E5 File Offset: 0x00174BE5
		private void SetProperty<T>(JmsField propertyIndex, T value)
		{
			base.SetProperty<T>((int)propertyIndex, value);
		}

		// Token: 0x040047A8 RID: 18344
		private static FolderFieldType[] fieldTypes = new FolderFieldType[]
		{
			FolderFieldType.String,
			FolderFieldType.I8,
			FolderFieldType.String,
			FolderFieldType.I4,
			FolderFieldType.I4,
			FolderFieldType.String,
			FolderFieldType.I8,
			FolderFieldType.String,
			FolderFieldType.I4
		};

		// Token: 0x040047A9 RID: 18345
		private static string[] fieldTags = Enum.GetNames(typeof(JmsField));

		// Token: 0x040047AA RID: 18346
		private static Dictionary<string, FieldTypeAndIndex> tagToFieldTypeAndIndex = Rf2hFolderWithFields.GenerateFieldInfo(Rf2hJmsFolder.fieldTypes, Rf2hJmsFolder.fieldTags);
	}
}
