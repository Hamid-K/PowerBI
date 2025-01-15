using System;
using System.Collections;
using Microsoft.HostIntegration.Tracing.Common;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x0200050B RID: 1291
	public class SystemIPrimitiveConverter : BasePrimitiveConverter
	{
		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06002B95 RID: 11157 RVA: 0x000961D7 File Offset: 0x000943D7
		// (set) Token: 0x06002B96 RID: 11158 RVA: 0x000961DF File Offset: 0x000943DF
		public override PrimitiveConverterTracePoint TracePoint
		{
			get
			{
				return this.tracePoint;
			}
			set
			{
				this.tracePoint = value;
				this.tracePoint[PrimitiveConverterPropertyIdentifiers.PrimitiveConverterType] = PrimitiveConverterTypes.SystemI;
			}
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x000961FA File Offset: 0x000943FA
		public SystemIPrimitiveConverter()
		{
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x0009620D File Offset: 0x0009440D
		public SystemIPrimitiveConverter(int CodePage)
			: base(CodePage)
		{
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x00096221 File Offset: 0x00094421
		public SystemIPrimitiveConverter(Hashtable behaviors)
			: base(behaviors)
		{
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x00096235 File Offset: 0x00094435
		public override void SetTracingAndLogging(object Logging)
		{
			base.SetTracingAndLogging(Logging);
			this.HisLogging = (CommonHISEventLogging)Logging;
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x0009624C File Offset: 0x0009444C
		public unsafe override void PackFloat(float FloatValue, ref byte Buffer, ref int pulLen, int ulMaxLen, CEDAR_TYPE_ENCODING encodeType)
		{
			if (encodeType.nCvtType != 3)
			{
				base.PackFloat(FloatValue, ref Buffer, ref pulLen, ulMaxLen, encodeType);
				return;
			}
			fixed (byte* ptr = &Buffer)
			{
				byte* ptr2 = ptr;
				byte* ptr3 = (byte*)(&FloatValue);
				*ptr2 = ptr3[3];
				ptr2[1] = ptr3[2];
				ptr2[2] = ptr3[1];
				ptr2[3] = *ptr3;
			}
			pulLen += 4;
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x000962A0 File Offset: 0x000944A0
		public unsafe override void UnpackFloat(ref byte Buffer, ref float ReturnedFloat, ref int pulLen, int ulResultLen, CEDAR_TYPE_ENCODING encodeType)
		{
			if (encodeType.nCvtType != 3)
			{
				base.UnpackFloat(ref Buffer, ref ReturnedFloat, ref pulLen, ulResultLen, encodeType);
				return;
			}
			fixed (float* ptr = &ReturnedFloat)
			{
				float* ptr2 = ptr;
				fixed (byte* ptr3 = &Buffer)
				{
					byte* ptr4 = ptr3;
					byte* ptr5 = (byte*)ptr2;
					byte* ptr6 = ptr4;
					ptr5[3] = *ptr6;
					ptr5[2] = ptr6[1];
					ptr5[1] = ptr6[2];
					*ptr5 = ptr6[3];
				}
			}
			pulLen -= 4;
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x00096300 File Offset: 0x00094500
		public unsafe override void PackDouble(double DoubleValue, ref byte Buffer, ref int pulLen, int ulMaxLen, CEDAR_TYPE_ENCODING encodeType)
		{
			if (encodeType.nCvtType != 4)
			{
				base.PackDouble(DoubleValue, ref Buffer, ref pulLen, ulMaxLen, encodeType);
				return;
			}
			fixed (byte* ptr = &Buffer)
			{
				byte* ptr2 = ptr;
				byte* ptr3 = (byte*)(&DoubleValue);
				*ptr2 = ptr3[7];
				ptr2[1] = ptr3[6];
				ptr2[2] = ptr3[5];
				ptr2[3] = ptr3[4];
				ptr2[4] = ptr3[3];
				ptr2[5] = ptr3[2];
				ptr2[6] = ptr3[1];
				ptr2[7] = *ptr3;
			}
			pulLen += 8;
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x00096374 File Offset: 0x00094574
		public unsafe override void UnpackDouble(ref byte Buffer, ref double ReturnedDouble, ref int pulLen, int ulResultLen, CEDAR_TYPE_ENCODING encodeType)
		{
			if (encodeType.nCvtType != 4)
			{
				base.UnpackDouble(ref Buffer, ref ReturnedDouble, ref pulLen, ulResultLen, encodeType);
				return;
			}
			fixed (double* ptr = &ReturnedDouble)
			{
				double* ptr2 = ptr;
				fixed (byte* ptr3 = &Buffer)
				{
					byte* ptr4 = ptr3;
					byte* ptr5 = (byte*)ptr2;
					byte* ptr6 = ptr4;
					ptr5[7] = *ptr6;
					ptr5[6] = ptr6[1];
					ptr5[5] = ptr6[2];
					ptr5[4] = ptr6[3];
					ptr5[3] = ptr6[4];
					ptr5[2] = ptr6[5];
					ptr5[1] = ptr6[6];
					*ptr5 = ptr6[7];
				}
			}
			pulLen -= 8;
		}

		// Token: 0x04001B79 RID: 7033
		private CommonHISEventLogging HisLogging;

		// Token: 0x04001B7A RID: 7034
		private static PrimitiveConverterDummyTracePoint dummyTracePoint = new PrimitiveConverterDummyTracePoint();

		// Token: 0x04001B7B RID: 7035
		private PrimitiveConverterTracePoint tracePoint = SystemIPrimitiveConverter.dummyTracePoint;
	}
}
