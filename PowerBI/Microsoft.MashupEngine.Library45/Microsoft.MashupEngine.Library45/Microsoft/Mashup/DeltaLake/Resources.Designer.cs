using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.DeltaLake
{
	// Token: 0x02001F0E RID: 7950
	internal class Resources
	{
		// Token: 0x17002C42 RID: 11330
		// (get) Token: 0x06010BBF RID: 68543 RVA: 0x0039AED3 File Offset: 0x003990D3
		public static ResourceManager ResourceManager
		{
			get
			{
				return Resources.ResourceLoader.Resources;
			}
		}

		// Token: 0x17002C43 RID: 11331
		// (get) Token: 0x06010BC0 RID: 68544 RVA: 0x0039AEDA File Offset: 0x003990DA
		public static Message0 DeltaLake_Table
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("DeltaLake_Table");
			}
		}

		// Token: 0x17002C44 RID: 11332
		// (get) Token: 0x06010BC1 RID: 68545 RVA: 0x0039AEE6 File Offset: 0x003990E6
		public static Message0 DeltaLake_Table_Category
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("DeltaLake_Table_Category");
			}
		}

		// Token: 0x17002C45 RID: 11333
		// (get) Token: 0x06010BC2 RID: 68546 RVA: 0x0039AEF2 File Offset: 0x003990F2
		public static Message0 DeltaLake_Table_Description
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("DeltaLake_Table_Description");
			}
		}

		// Token: 0x17002C46 RID: 11334
		// (get) Token: 0x06010BC3 RID: 68547 RVA: 0x0039AEFE File Offset: 0x003990FE
		public static Message0 DeltaTable_NotFound
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("DeltaTable_NotFound");
			}
		}

		// Token: 0x17002C47 RID: 11335
		// (get) Token: 0x06010BC4 RID: 68548 RVA: 0x0039AF0A File Offset: 0x0039910A
		public static Message0 Requires64Bit
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Requires64Bit");
			}
		}

		// Token: 0x06010BC5 RID: 68549 RVA: 0x0039AF16 File Offset: 0x00399116
		public static Message2 CantConvertType(object p0, object p1)
		{
			return Resources.ResourceLoader.GetMessage("CantConvertType", p0, p1);
		}

		// Token: 0x17002C48 RID: 11336
		// (get) Token: 0x06010BC6 RID: 68550 RVA: 0x0039AF24 File Offset: 0x00399124
		public static Message0 CantInsertPartitionedTable
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("CantInsertPartitionedTable");
			}
		}

		// Token: 0x06010BC7 RID: 68551 RVA: 0x0039AF30 File Offset: 0x00399130
		public static Message1 DeletionVectorTypeNotSupported(object p0)
		{
			return Resources.ResourceLoader.GetMessage("DeletionVectorTypeNotSupported", p0);
		}

		// Token: 0x06010BC8 RID: 68552 RVA: 0x0039AF3D File Offset: 0x0039913D
		public static Message1 DeletionVectorInvalid(object p0)
		{
			return Resources.ResourceLoader.GetMessage("DeletionVectorInvalid", p0);
		}

		// Token: 0x17002C49 RID: 11337
		// (get) Token: 0x06010BC9 RID: 68553 RVA: 0x0039AF4A File Offset: 0x0039914A
		public static Message0 PartitionDataMismatch
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("PartitionDataMismatch");
			}
		}

		// Token: 0x17002C4A RID: 11338
		// (get) Token: 0x06010BCA RID: 68554 RVA: 0x0039AF56 File Offset: 0x00399156
		public static Message0 CreatedByOtherProcess
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("CreatedByOtherProcess");
			}
		}

		// Token: 0x06010BCB RID: 68555 RVA: 0x0039AF62 File Offset: 0x00399162
		public static Message3 CantInsertType(object p0, object p1, object p2)
		{
			return Resources.ResourceLoader.GetMessage("CantInsertType", p0, p1, p2);
		}

		// Token: 0x06010BCC RID: 68556 RVA: 0x0039AF71 File Offset: 0x00399171
		public static Message1 UnsupportedRead(object p0)
		{
			return Resources.ResourceLoader.GetMessage("UnsupportedRead", p0);
		}

		// Token: 0x06010BCD RID: 68557 RVA: 0x0039AF7E File Offset: 0x0039917E
		public static Message1 UnsupportedWrite(object p0)
		{
			return Resources.ResourceLoader.GetMessage("UnsupportedWrite", p0);
		}

		// Token: 0x06010BCE RID: 68558 RVA: 0x0039AF8B File Offset: 0x0039918B
		public static Message1 CaseConflict(object p0)
		{
			return Resources.ResourceLoader.GetMessage("CaseConflict", p0);
		}

		// Token: 0x02001F0F RID: 7951
		private class ResourceLoader
		{
			// Token: 0x06010BD0 RID: 68560 RVA: 0x0039AF98 File Offset: 0x00399198
			internal ResourceLoader()
			{
				this.resources = new ResourceManager("Microsoft.Mashup.DeltaLake.Resources", base.GetType().Assembly);
			}

			// Token: 0x06010BD1 RID: 68561 RVA: 0x0039AFBC File Offset: 0x003991BC
			private static Resources.ResourceLoader GetLoader()
			{
				if (Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.instance == null)
				{
					Resources.ResourceLoader resourceLoader = new Resources.ResourceLoader();
					Interlocked.CompareExchange<Resources.ResourceLoader>(ref Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.instance, resourceLoader, null);
				}
				return Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.instance;
			}

			// Token: 0x17002C4B RID: 11339
			// (get) Token: 0x06010BD2 RID: 68562 RVA: 0x00002188 File Offset: 0x00000388
			public static CultureInfo Culture
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002C4C RID: 11340
			// (get) Token: 0x06010BD3 RID: 68563 RVA: 0x0039AFE8 File Offset: 0x003991E8
			public static ResourceManager Resources
			{
				get
				{
					return Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.GetLoader().resources;
				}
			}

			// Token: 0x06010BD4 RID: 68564 RVA: 0x0039AFF4 File Offset: 0x003991F4
			public static Message0 GetMessage(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message0(null);
				}
				return new Message0(loader.resources.GetString(name, Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.Culture));
			}

			// Token: 0x06010BD5 RID: 68565 RVA: 0x0039B028 File Offset: 0x00399228
			public static Message1 GetMessage(string name, object arg0)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message1(null, null);
				}
				return new Message1(loader.resources.GetString(name, Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.Culture), arg0);
			}

			// Token: 0x06010BD6 RID: 68566 RVA: 0x0039B060 File Offset: 0x00399260
			public static Message2 GetMessage(string name, object arg0, object arg1)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message2(null, null, null);
				}
				return new Message2(loader.resources.GetString(name, Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.Culture), arg0, arg1);
			}

			// Token: 0x06010BD7 RID: 68567 RVA: 0x0039B098 File Offset: 0x00399298
			public static Message3 GetMessage(string name, object arg0, object arg1, object arg2)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message3(null, null, null, null);
				}
				return new Message3(loader.resources.GetString(name, Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.Culture), arg0, arg1, arg2);
			}

			// Token: 0x06010BD8 RID: 68568 RVA: 0x0039B0D4 File Offset: 0x003992D4
			public static Message4 GetMessage(string name, params object[] args)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message4(null, null);
				}
				return new Message4(loader.resources.GetString(name, Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.Culture), args);
			}

			// Token: 0x06010BD9 RID: 68569 RVA: 0x0039B10C File Offset: 0x0039930C
			public static string GetString(string name, params object[] args)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				string @string = loader.resources.GetString(name, Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.Culture);
				if (args != null && args.Length != 0)
				{
					return string.Format(CultureInfo.CurrentCulture, @string, args);
				}
				return @string;
			}

			// Token: 0x06010BDA RID: 68570 RVA: 0x0039B14C File Offset: 0x0039934C
			public static string GetString(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetString(name, Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.Culture);
			}

			// Token: 0x06010BDB RID: 68571 RVA: 0x0039B178 File Offset: 0x00399378
			public static object GetObject(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetObject(name, Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.Culture);
			}

			// Token: 0x06010BDC RID: 68572 RVA: 0x0039B1A4 File Offset: 0x003993A4
			public static T GetObject<T>(string name) where T : class
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return default(T);
				}
				return (T)((object)loader.resources.GetObject(name, Microsoft.Mashup.DeltaLake.Resources.ResourceLoader.Culture));
			}

			// Token: 0x04006458 RID: 25688
			private static Resources.ResourceLoader instance;

			// Token: 0x04006459 RID: 25689
			private ResourceManager resources;
		}
	}
}
