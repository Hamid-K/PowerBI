using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000038 RID: 56
	public static class DataConvert
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x00006138 File Offset: 0x00004338
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public unsafe static IDataConvert GetInstance()
		{
			if (DataConvert.instance == null)
			{
				IDataConvert dataConvert = (IDataConvert)new DataConvert.DMTSComDataConvert();
				IDCInfo idcinfo = dataConvert as IDCInfo;
				if (idcinfo != null)
				{
					DCINFO dcinfo;
					dcinfo.InfoType = DCINFOTYPE.VERSION;
					Variant.SetValue(&dcinfo.Data, 2);
					idcinfo.SetInfo(1U, &dcinfo);
				}
				DataConvert.instance = new DataConvert.ExtendedDataConvert(dataConvert);
			}
			return DataConvert.instance;
		}

		// Token: 0x0400007D RID: 125
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private static IDataConvert instance;

		// Token: 0x020000E8 RID: 232
		internal class ExtendedDataConvert : IDataConvert
		{
			// Token: 0x060004AB RID: 1195 RVA: 0x0000E33B File Offset: 0x0000C53B
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			internal ExtendedDataConvert(IDataConvert convert)
			{
				this.dataConvert = convert;
			}

			// Token: 0x060004AC RID: 1196 RVA: 0x0000E34C File Offset: 0x0000C54C
			public unsafe void DataConvert(DBTYPE srcType, DBTYPE dstType, DBLENGTH srcLength, out DBLENGTH dstLength, void* src, void* dst, DBLENGTH maxLength, DBSTATUS srcStatus, out DBSTATUS status, byte precision, byte scale, DBDATACONVERT flags)
			{
				if (Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.DataConvert.ExtendedDataConvert.IsExtendedType(srcType) || srcType == DBTYPE.DBDATE)
				{
					object variantObject = OleDbConvert.GetVariantObject(OleDbConvert.GetObject(srcType, srcLength, src, srcStatus, precision, scale, flags));
					if (variantObject == null)
					{
						status = DBSTATUS.S_ISNULL;
						dstLength = new DBLENGTH
						{
							Value = 0UL
						};
						return;
					}
					VARIANT variant;
					Marshal.GetNativeVariantForObject(variantObject, new IntPtr((void*)(&variant)));
					this.dataConvert.DataConvert(DBTYPE.VARIANT, dstType, new DBLENGTH
					{
						Value = (ulong)sizeof(VARIANT)
					}, out dstLength, (void*)(&variant), dst, maxLength, srcStatus, out status, precision, scale, flags);
					return;
				}
				else
				{
					if (srcType == DBTYPE.I8 && dstType == DBTYPE.VARIANT)
					{
						Variant.Init((VARIANT*)dst);
						((VARIANT*)dst)->Type = VARTYPE.I8;
						((VARIANT*)dst)->Value64 = (ulong)(*(long*)src);
						dstLength = DbLength.Variant;
						status = DBSTATUS.S_OK;
						return;
					}
					this.dataConvert.DataConvert(srcType, dstType, srcLength, out dstLength, src, dst, maxLength, srcStatus, out status, precision, scale, flags);
					return;
				}
			}

			// Token: 0x060004AD RID: 1197 RVA: 0x0000E43D File Offset: 0x0000C63D
			public int CanConvert(DBTYPE srcType, DBTYPE dstType)
			{
				if (srcType - DBTYPE.DBTIME2 > 1 && srcType != DBTYPE.DBDURATION)
				{
					return this.dataConvert.CanConvert(srcType, dstType);
				}
				if (dstType == srcType || dstType == DBTYPE.VARIANT || dstType == DBTYPE.WSTR)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x060004AE RID: 1198 RVA: 0x0000E474 File Offset: 0x0000C674
			public unsafe void GetConversionSize(DBTYPE srcType, DBTYPE dstType, DBLENGTH* srcLength, DBLENGTH* dstLength, void* src)
			{
				if (srcType - DBTYPE.DBTIME2 <= 1 || srcType == DBTYPE.DBDURATION)
				{
					throw new NotImplementedException();
				}
				if (dstType - DBTYPE.DBTIME2 <= 1 || dstType == DBTYPE.DBDURATION)
				{
					throw new NotImplementedException();
				}
				this.dataConvert.GetConversionSize(srcType, dstType, srcLength, dstLength, src);
			}

			// Token: 0x060004AF RID: 1199 RVA: 0x0000E4C3 File Offset: 0x0000C6C3
			private static bool IsExtendedType(DBTYPE type)
			{
				return type == DBTYPE.DBDURATION || type == DBTYPE.DBTIME2 || type == DBTYPE.DBTIMESTAMPOFFSET;
			}

			// Token: 0x040003FD RID: 1021
			[global::System.Runtime.CompilerServices.Nullable(1)]
			private IDataConvert dataConvert;
		}

		// Token: 0x020000E9 RID: 233
		[Guid("c8b522d1-5cf3-11ce-ade5-00aa0044773d")]
		[ComImport]
		internal class DMTSComDataConvert
		{
			// Token: 0x060004B0 RID: 1200
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			public extern DMTSComDataConvert();
		}
	}
}
