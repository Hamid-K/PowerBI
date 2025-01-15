using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Web;
using System.Web.SessionState;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000AF RID: 175
	internal class SessionStoreProviderData
	{
		// Token: 0x06000414 RID: 1044 RVA: 0x00014266 File Offset: 0x00012466
		public SessionStoreProviderData(SessionStateActions actionFlag, SessionStateStoreData item)
		{
			this.ActionFlag = actionFlag;
			this.Item = item;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00002061 File Offset: 0x00000261
		public SessionStoreProviderData()
		{
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001427C File Offset: 0x0001247C
		public void ReadObject(Stream ins)
		{
			BinaryReader binaryReader = new BinaryReader(ins);
			try
			{
				this._actionFlag = binaryReader.ReadInt32();
				int num = binaryReader.ReadInt32();
				bool flag = binaryReader.ReadBoolean();
				bool flag2 = binaryReader.ReadBoolean();
				SessionStateItemCollection sessionStateItemCollection;
				if (flag)
				{
					sessionStateItemCollection = SessionStateItemCollection.Deserialize(binaryReader);
				}
				else
				{
					sessionStateItemCollection = new SessionStateItemCollection();
				}
				HttpStaticObjectsCollection httpStaticObjectsCollection;
				if (flag2)
				{
					httpStaticObjectsCollection = HttpStaticObjectsCollection.Deserialize(binaryReader);
				}
				else
				{
					httpStaticObjectsCollection = null;
				}
				this._item = new SessionStateStoreData(sessionStateItemCollection, httpStaticObjectsCollection, num);
				if (binaryReader.ReadByte() != 255)
				{
					throw new SerializationException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 6002));
				}
			}
			catch (EndOfStreamException ex)
			{
				throw new SerializationException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 6002), ex);
			}
			catch (HttpException ex2)
			{
				throw new SerializationException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 6002), ex2);
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00014358 File Offset: 0x00012558
		public void WriteObject(Stream ous)
		{
			bool flag = true;
			bool flag2 = true;
			BinaryWriter binaryWriter = new BinaryWriter(ous);
			binaryWriter.Write(this._actionFlag);
			binaryWriter.Write(this._item.Timeout);
			if (this._item.Items == null || this._item.Items.Count == 0)
			{
				flag = false;
			}
			binaryWriter.Write(flag);
			if (this._item.StaticObjects == null || this._item.StaticObjects.NeverAccessed)
			{
				flag2 = false;
			}
			binaryWriter.Write(flag2);
			if (flag)
			{
				((SessionStateItemCollection)this._item.Items).Serialize(binaryWriter);
			}
			if (flag2)
			{
				this._item.StaticObjects.Serialize(binaryWriter);
			}
			binaryWriter.Write(byte.MaxValue);
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00014415 File Offset: 0x00012615
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x0001441D File Offset: 0x0001261D
		public SessionStateActions ActionFlag
		{
			get
			{
				return (SessionStateActions)this._actionFlag;
			}
			set
			{
				this._actionFlag = (int)value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00014426 File Offset: 0x00012626
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x0001442E File Offset: 0x0001262E
		public SessionStateStoreData Item
		{
			get
			{
				return this._item;
			}
			set
			{
				this._item = value;
			}
		}

		// Token: 0x0400032A RID: 810
		private int _actionFlag;

		// Token: 0x0400032B RID: 811
		private SessionStateStoreData _item;
	}
}
