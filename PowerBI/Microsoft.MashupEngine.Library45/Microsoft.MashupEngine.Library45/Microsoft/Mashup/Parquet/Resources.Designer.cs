using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Parquet
{
	// Token: 0x02001F13 RID: 7955
	internal class Resources
	{
		// Token: 0x17002C4D RID: 11341
		// (get) Token: 0x06010BE1 RID: 68577 RVA: 0x0039B337 File Offset: 0x00399537
		public static ResourceManager ResourceManager
		{
			get
			{
				return Resources.ResourceLoader.Resources;
			}
		}

		// Token: 0x06010BE2 RID: 68578 RVA: 0x0039B33E File Offset: 0x0039953E
		public static Message1 CyclicTypeError(object p0)
		{
			return Resources.ResourceLoader.GetMessage("CyclicTypeError", p0);
		}

		// Token: 0x17002C4E RID: 11342
		// (get) Token: 0x06010BE3 RID: 68579 RVA: 0x0039B34B File Offset: 0x0039954B
		public static Message0 DecimalOutOfRange
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("DecimalOutOfRange");
			}
		}

		// Token: 0x17002C4F RID: 11343
		// (get) Token: 0x06010BE4 RID: 68580 RVA: 0x0039B357 File Offset: 0x00399557
		public static Message0 DecimalPrecisionInvalid
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("DecimalPrecisionInvalid");
			}
		}

		// Token: 0x17002C50 RID: 11344
		// (get) Token: 0x06010BE5 RID: 68581 RVA: 0x0039B363 File Offset: 0x00399563
		public static Message0 DecimalScaleInvalid
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("DecimalScaleInvalid");
			}
		}

		// Token: 0x17002C51 RID: 11345
		// (get) Token: 0x06010BE6 RID: 68582 RVA: 0x0039B36F File Offset: 0x0039956F
		public static Message0 ExpectedFixedLengthError
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("ExpectedFixedLengthError");
			}
		}

		// Token: 0x17002C52 RID: 11346
		// (get) Token: 0x06010BE7 RID: 68583 RVA: 0x0039B37B File Offset: 0x0039957B
		public static Message0 IncompatibleType
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("IncompatibleType");
			}
		}

		// Token: 0x17002C53 RID: 11347
		// (get) Token: 0x06010BE8 RID: 68584 RVA: 0x0039B387 File Offset: 0x00399587
		public static Message0 InsufficientType
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("InsufficientType");
			}
		}

		// Token: 0x17002C54 RID: 11348
		// (get) Token: 0x06010BE9 RID: 68585 RVA: 0x0039B393 File Offset: 0x00399593
		public static Message0 IntervalComponentOutOfRangeError
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("IntervalComponentOutOfRangeError");
			}
		}

		// Token: 0x06010BEA RID: 68586 RVA: 0x0039B39F File Offset: 0x0039959F
		public static Message1 LoadError(object p0)
		{
			return Resources.ResourceLoader.GetMessage("LoadError", p0);
		}

		// Token: 0x06010BEB RID: 68587 RVA: 0x0039B3AC File Offset: 0x003995AC
		public static Message1 MaxDepthExceededError(object p0)
		{
			return Resources.ResourceLoader.GetMessage("MaxDepthExceededError", p0);
		}

		// Token: 0x17002C55 RID: 11349
		// (get) Token: 0x06010BEC RID: 68588 RVA: 0x0039B3B9 File Offset: 0x003995B9
		public static Message0 NegativeBinaryLength
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("NegativeBinaryLength");
			}
		}

		// Token: 0x17002C56 RID: 11350
		// (get) Token: 0x06010BED RID: 68589 RVA: 0x0039B3C5 File Offset: 0x003995C5
		public static Message0 NonNullInNullColumn
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("NonNullInNullColumn");
			}
		}

		// Token: 0x17002C57 RID: 11351
		// (get) Token: 0x06010BEE RID: 68590 RVA: 0x0039B3D1 File Offset: 0x003995D1
		public static Message0 NullInNotNullColumn
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("NullInNotNullColumn");
			}
		}

		// Token: 0x17002C58 RID: 11352
		// (get) Token: 0x06010BEF RID: 68591 RVA: 0x0039B3DD File Offset: 0x003995DD
		public static Message0 Parquet_Document
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Parquet_Document");
			}
		}

		// Token: 0x17002C59 RID: 11353
		// (get) Token: 0x06010BF0 RID: 68592 RVA: 0x0039B3E9 File Offset: 0x003995E9
		public static Message0 Parquet_Document_Category
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Parquet_Document_Category");
			}
		}

		// Token: 0x17002C5A RID: 11354
		// (get) Token: 0x06010BF1 RID: 68593 RVA: 0x0039B3F5 File Offset: 0x003995F5
		public static Message0 Parquet_Document_Description
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Parquet_Document_Description");
			}
		}

		// Token: 0x17002C5B RID: 11355
		// (get) Token: 0x06010BF2 RID: 68594 RVA: 0x0039B401 File Offset: 0x00399601
		public static Message0 Parquet_Metadata
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Parquet_Metadata");
			}
		}

		// Token: 0x17002C5C RID: 11356
		// (get) Token: 0x06010BF3 RID: 68595 RVA: 0x0039B40D File Offset: 0x0039960D
		public static Message0 Parquet_Metadata_Category
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Parquet_Metadata_Category");
			}
		}

		// Token: 0x17002C5D RID: 11357
		// (get) Token: 0x06010BF4 RID: 68596 RVA: 0x0039B419 File Offset: 0x00399619
		public static Message0 Parquet_Metadata_Description
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Parquet_Metadata_Description");
			}
		}

		// Token: 0x17002C5E RID: 11358
		// (get) Token: 0x06010BF5 RID: 68597 RVA: 0x0039B425 File Offset: 0x00399625
		public static Message0 Requires64Bit
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("Requires64Bit");
			}
		}

		// Token: 0x17002C5F RID: 11359
		// (get) Token: 0x06010BF6 RID: 68598 RVA: 0x0039B431 File Offset: 0x00399631
		public static Message0 SeekableStreamRequired
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("SeekableStreamRequired");
			}
		}

		// Token: 0x17002C60 RID: 11360
		// (get) Token: 0x06010BF7 RID: 68599 RVA: 0x0039B43D File Offset: 0x0039963D
		public static Message0 UnmappedType
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("UnmappedType");
			}
		}

		// Token: 0x17002C61 RID: 11361
		// (get) Token: 0x06010BF8 RID: 68600 RVA: 0x0039B449 File Offset: 0x00399649
		public static Message0 UnrecognizedLogicalType
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("UnrecognizedLogicalType");
			}
		}

		// Token: 0x06010BF9 RID: 68601 RVA: 0x0039B455 File Offset: 0x00399655
		public static Message2 UnusableFacet(object p0, object p1)
		{
			return Resources.ResourceLoader.GetMessage("UnusableFacet", p0, p1);
		}

		// Token: 0x06010BFA RID: 68602 RVA: 0x0039B463 File Offset: 0x00399663
		public static Message2 UserSchemaError(object p0, object p1)
		{
			return Resources.ResourceLoader.GetMessage("UserSchemaError", p0, p1);
		}

		// Token: 0x17002C62 RID: 11362
		// (get) Token: 0x06010BFB RID: 68603 RVA: 0x0039B471 File Offset: 0x00399671
		public static Message0 WriteDurationError
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("WriteDurationError");
			}
		}

		// Token: 0x17002C63 RID: 11363
		// (get) Token: 0x06010BFC RID: 68604 RVA: 0x0039B47D File Offset: 0x0039967D
		public static Message0 WriteDatetimezoneError
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("WriteDatetimezoneError");
			}
		}

		// Token: 0x06010BFD RID: 68605 RVA: 0x0039B489 File Offset: 0x00399689
		public static Message1 WriteError(object p0)
		{
			return Resources.ResourceLoader.GetMessage("WriteError", p0);
		}

		// Token: 0x17002C64 RID: 11364
		// (get) Token: 0x06010BFE RID: 68606 RVA: 0x0039B496 File Offset: 0x00399696
		public static Message0 WriteInsufficientMemory
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("WriteInsufficientMemory");
			}
		}

		// Token: 0x17002C65 RID: 11365
		// (get) Token: 0x06010BFF RID: 68607 RVA: 0x0039B4A2 File Offset: 0x003996A2
		public static Message0 LegacyColumnNameEncoding
		{
			get
			{
				return Resources.ResourceLoader.GetMessage("LegacyColumnNameEncoding");
			}
		}

		// Token: 0x02001F14 RID: 7956
		private class ResourceLoader
		{
			// Token: 0x06010C01 RID: 68609 RVA: 0x0039B4AE File Offset: 0x003996AE
			internal ResourceLoader()
			{
				this.resources = new ResourceManager("Microsoft.Mashup.Parquet.Resources", base.GetType().Assembly);
			}

			// Token: 0x06010C02 RID: 68610 RVA: 0x0039B4D4 File Offset: 0x003996D4
			private static Resources.ResourceLoader GetLoader()
			{
				if (Microsoft.Mashup.Parquet.Resources.ResourceLoader.instance == null)
				{
					Resources.ResourceLoader resourceLoader = new Resources.ResourceLoader();
					Interlocked.CompareExchange<Resources.ResourceLoader>(ref Microsoft.Mashup.Parquet.Resources.ResourceLoader.instance, resourceLoader, null);
				}
				return Microsoft.Mashup.Parquet.Resources.ResourceLoader.instance;
			}

			// Token: 0x17002C66 RID: 11366
			// (get) Token: 0x06010C03 RID: 68611 RVA: 0x00002188 File Offset: 0x00000388
			public static CultureInfo Culture
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002C67 RID: 11367
			// (get) Token: 0x06010C04 RID: 68612 RVA: 0x0039B500 File Offset: 0x00399700
			public static ResourceManager Resources
			{
				get
				{
					return Microsoft.Mashup.Parquet.Resources.ResourceLoader.GetLoader().resources;
				}
			}

			// Token: 0x06010C05 RID: 68613 RVA: 0x0039B50C File Offset: 0x0039970C
			public static Message0 GetMessage(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Parquet.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message0(null);
				}
				return new Message0(loader.resources.GetString(name, Microsoft.Mashup.Parquet.Resources.ResourceLoader.Culture));
			}

			// Token: 0x06010C06 RID: 68614 RVA: 0x0039B540 File Offset: 0x00399740
			public static Message1 GetMessage(string name, object arg0)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Parquet.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message1(null, null);
				}
				return new Message1(loader.resources.GetString(name, Microsoft.Mashup.Parquet.Resources.ResourceLoader.Culture), arg0);
			}

			// Token: 0x06010C07 RID: 68615 RVA: 0x0039B578 File Offset: 0x00399778
			public static Message2 GetMessage(string name, object arg0, object arg1)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Parquet.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message2(null, null, null);
				}
				return new Message2(loader.resources.GetString(name, Microsoft.Mashup.Parquet.Resources.ResourceLoader.Culture), arg0, arg1);
			}

			// Token: 0x06010C08 RID: 68616 RVA: 0x0039B5B0 File Offset: 0x003997B0
			public static Message3 GetMessage(string name, object arg0, object arg1, object arg2)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Parquet.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message3(null, null, null, null);
				}
				return new Message3(loader.resources.GetString(name, Microsoft.Mashup.Parquet.Resources.ResourceLoader.Culture), arg0, arg1, arg2);
			}

			// Token: 0x06010C09 RID: 68617 RVA: 0x0039B5EC File Offset: 0x003997EC
			public static Message4 GetMessage(string name, params object[] args)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Parquet.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return new Message4(null, null);
				}
				return new Message4(loader.resources.GetString(name, Microsoft.Mashup.Parquet.Resources.ResourceLoader.Culture), args);
			}

			// Token: 0x06010C0A RID: 68618 RVA: 0x0039B624 File Offset: 0x00399824
			public static string GetString(string name, params object[] args)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Parquet.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				string @string = loader.resources.GetString(name, Microsoft.Mashup.Parquet.Resources.ResourceLoader.Culture);
				if (args != null && args.Length != 0)
				{
					return string.Format(CultureInfo.CurrentCulture, @string, args);
				}
				return @string;
			}

			// Token: 0x06010C0B RID: 68619 RVA: 0x0039B664 File Offset: 0x00399864
			public static string GetString(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Parquet.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetString(name, Microsoft.Mashup.Parquet.Resources.ResourceLoader.Culture);
			}

			// Token: 0x06010C0C RID: 68620 RVA: 0x0039B690 File Offset: 0x00399890
			public static object GetObject(string name)
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Parquet.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetObject(name, Microsoft.Mashup.Parquet.Resources.ResourceLoader.Culture);
			}

			// Token: 0x06010C0D RID: 68621 RVA: 0x0039B6BC File Offset: 0x003998BC
			public static T GetObject<T>(string name) where T : class
			{
				Resources.ResourceLoader loader = Microsoft.Mashup.Parquet.Resources.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return default(T);
				}
				return (T)((object)loader.resources.GetObject(name, Microsoft.Mashup.Parquet.Resources.ResourceLoader.Culture));
			}

			// Token: 0x0400645D RID: 25693
			private static Resources.ResourceLoader instance;

			// Token: 0x0400645E RID: 25694
			private ResourceManager resources;
		}
	}
}
