using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.OleDb.Marshallers;

namespace Microsoft.OleDb
{
	// Token: 0x02001E8B RID: 7819
	public static class DataConvert
	{
		// Token: 0x0600C14A RID: 49482 RVA: 0x0026DEC1 File Offset: 0x0026C0C1
		public static IDataConvert GetInstance()
		{
			if (DataConvert.instance == null)
			{
				DataConvert.instance = DataConvert.CreateExtendedDataConvert();
			}
			return DataConvert.instance;
		}

		// Token: 0x0600C14B RID: 49483 RVA: 0x0026DEDC File Offset: 0x0026C0DC
		public unsafe static IDataConvert CreateExtendedDataConvert()
		{
			IDataConvert dataConvert = (IDataConvert)new DataConvert.ComDataConvert();
			IDCInfo idcinfo = dataConvert as IDCInfo;
			if (idcinfo != null)
			{
				DCINFO dcinfo;
				dcinfo.eInfoType = DCINFOTYPE.VERSION;
				Variant.SetValue(&dcinfo.vData, 2);
				idcinfo.SetInfo(1U, &dcinfo);
			}
			return new DataConvert.ExtendedDataConvert(dataConvert);
		}

		// Token: 0x0400618F RID: 24975
		private static IDataConvert instance;

		// Token: 0x02001E8C RID: 7820
		private class ExtendedDataConvert : IDataConvert
		{
			// Token: 0x0600C14C RID: 49484 RVA: 0x0026DF22 File Offset: 0x0026C122
			public ExtendedDataConvert(IDataConvert convert)
			{
				this.dataConvert = convert;
			}

			// Token: 0x0600C14D RID: 49485 RVA: 0x0026DF34 File Offset: 0x0026C134
			public unsafe void DataConvert(DBTYPE wSrcType, DBTYPE wDstType, DBLENGTH cbSrcLength, out DBLENGTH cbDstLength, void* pSrc, void* pDst, DBLENGTH cbDstMaxLength, DBSTATUS dbsSrcStatus, out DBSTATUS dbsStatus, byte bPrecision, byte bScale, DBDATACONVERT dwFlags)
			{
				if (!Microsoft.OleDb.DataConvert.ExtendedDataConvert.IsExtendedType(wSrcType) && wSrcType != DBTYPE.DBDATE)
				{
					this.dataConvert.DataConvert(wSrcType, wDstType, cbSrcLength, out cbDstLength, pSrc, pDst, cbDstMaxLength, dbsSrcStatus, out dbsStatus, bPrecision, bScale, dwFlags);
					return;
				}
				object variantObject = OleDbConvert.GetVariantObject(OleDbConvert.GetObject(this, wSrcType, cbSrcLength, pSrc, dbsSrcStatus, bPrecision, bScale, dwFlags));
				if (variantObject == null)
				{
					dbsStatus = DBSTATUS.S_ISNULL;
					cbDstLength = new DBLENGTH
					{
						value = 0UL
					};
					return;
				}
				VARIANT variant;
				VariantMarshaller.Instance.GetNative(variantObject, (IntPtr)((void*)(&variant)));
				this.dataConvert.DataConvert(DBTYPE.VARIANT, wDstType, new DBLENGTH
				{
					value = (ulong)sizeof(VARIANT)
				}, out cbDstLength, (void*)(&variant), pDst, cbDstMaxLength, dbsSrcStatus, out dbsStatus, bPrecision, bScale, dwFlags);
				VariantMarshaller.Instance.Cleanup((IntPtr)((void*)(&variant)));
			}

			// Token: 0x0600C14E RID: 49486 RVA: 0x0026E00B File Offset: 0x0026C20B
			public int CanConvert(DBTYPE wSrcType, DBTYPE wDstType)
			{
				if (wSrcType - DBTYPE.DBTIME2 > 1 && wSrcType != DBTYPE.DBDURATION)
				{
					return this.dataConvert.CanConvert(wSrcType, wDstType);
				}
				if (wDstType == wSrcType || wDstType == DBTYPE.VARIANT || wDstType == DBTYPE.WSTR)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x0600C14F RID: 49487 RVA: 0x0026E044 File Offset: 0x0026C244
			public unsafe void GetConversionSize(DBTYPE wSrcType, DBTYPE wDstType, DBLENGTH* pcbSrcLength, DBLENGTH* pcbDstLength, void* pSrc)
			{
				if (wSrcType - DBTYPE.DBTIME2 <= 1 || wSrcType == DBTYPE.DBDURATION)
				{
					throw new NotImplementedException();
				}
				if (wDstType - DBTYPE.DBTIME2 <= 1 || wDstType == DBTYPE.DBDURATION)
				{
					throw new NotImplementedException();
				}
				this.dataConvert.GetConversionSize(wSrcType, wDstType, pcbSrcLength, pcbDstLength, pSrc);
			}

			// Token: 0x0600C150 RID: 49488 RVA: 0x0026E093 File Offset: 0x0026C293
			private static bool IsExtendedType(DBTYPE type)
			{
				return type == DBTYPE.DBDURATION || type == DBTYPE.DBTIME2 || type == DBTYPE.DBTIMESTAMPOFFSET;
			}

			// Token: 0x04006190 RID: 24976
			private IDataConvert dataConvert;
		}

		// Token: 0x02001E8D RID: 7821
		[Guid("c8b522d1-5cf3-11ce-ade5-00aa0044773d")]
		[ComImport]
		private class ComDataConvert
		{
			// Token: 0x0600C151 RID: 49489
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			public extern ComDataConvert();
		}
	}
}
