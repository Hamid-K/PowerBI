using System;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Nls;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AFC RID: 2812
	internal class SegmentHelpers
	{
		// Token: 0x0600592A RID: 22826 RVA: 0x00002061 File Offset: 0x00000261
		private SegmentHelpers()
		{
		}

		// Token: 0x0600592B RID: 22827 RVA: 0x00170108 File Offset: 0x0016E308
		internal unsafe static int GenerateSegmentHeader(DynamicDataBuffer buffer, int segmentHeaderTypeAs4Bytes, SegmentHeaderType segmentHeaderType, int conversationId, int requestId, SegmentType segmentType, ControlFlag1 controlFlag1, ControlFlag2 controlFlag2)
		{
			fixed (byte* ptr = &buffer.Data[0])
			{
				int* ptr2 = (int*)ptr;
				*(ptr2++) = segmentHeaderTypeAs4Bytes;
				bool flag = false;
				int num;
				switch (segmentHeaderType)
				{
				case SegmentHeaderType.Tsh:
					num = 28;
					break;
				case SegmentHeaderType.Tshm:
					num = 36;
					flag = true;
					break;
				case SegmentHeaderType.Tshc:
					num = 28;
					break;
				default:
					throw new InvalidOperationException("Generate Segment Header called with invalid Segment Header Type");
				}
				*(ptr2++) = 0;
				if (flag)
				{
					ConversionHelpers.MoveIntToAddressBigEndian((byte*)ptr2, conversationId);
					ptr2++;
					ConversionHelpers.MoveIntToAddressBigEndian((byte*)ptr2, requestId);
					ptr2++;
				}
				byte* ptr3 = (byte*)ptr2;
				*(ptr3++) = 2;
				*(ptr3++) = (byte)segmentType;
				*(ptr3++) = (byte)controlFlag1;
				*(ptr3++) = (byte)controlFlag2;
				long* ptr4 = (long*)ptr3;
				*(ptr4++) = 0L;
				ptr2 = (int*)ptr4;
				*(ptr2++) = 546;
				short* ptr5 = (short*)ptr2;
				*(ptr5++) = 1252;
				*ptr5 = 0;
				return num;
			}
		}

		// Token: 0x0600592C RID: 22828 RVA: 0x001701E4 File Offset: 0x0016E3E4
		internal unsafe static SegmentHeaderInformation ExtractSegmentHeader(DynamicDataBuffer buffer)
		{
			SegmentHeaderInformation segmentHeaderInformation = new SegmentHeaderInformation();
			fixed (byte* ptr = &buffer.Data[0])
			{
				int* ptr2 = (int*)ptr;
				int num = *(ptr2++);
				segmentHeaderInformation.SegmentHeaderType = SegmentHeaderType.Tshm;
				bool flag = false;
				if (num <= 541610836)
				{
					if (num == -1010244893)
					{
						goto IL_0081;
					}
					if (num == -725032221)
					{
						goto IL_0079;
					}
					if (num != 541610836)
					{
						goto IL_008D;
					}
				}
				else if (num != 1086907107)
				{
					if (num == 1128813396)
					{
						goto IL_0081;
					}
					if (num != 1296585556)
					{
						goto IL_008D;
					}
					goto IL_0079;
				}
				segmentHeaderInformation.SegmentHeaderType = SegmentHeaderType.Tsh;
				int num2 = 28;
				goto IL_0098;
				IL_0079:
				num2 = 36;
				flag = true;
				goto IL_0098;
				IL_0081:
				segmentHeaderInformation.SegmentHeaderType = SegmentHeaderType.Tshc;
				num2 = 28;
				goto IL_0098;
				IL_008D:
				throw new InvalidOperationException("Segment Header Not Recognized");
				IL_0098:
				segmentHeaderInformation.LengthOfSegmentHeader = num2;
				int num3 = ConversionHelpers.ExtractIntFromAddress(ref ptr2, false);
				segmentHeaderInformation.TotalLengthOfSegment = num3;
				segmentHeaderInformation.LengthOfRestOfSegment = num3 - num2;
				if (flag)
				{
					segmentHeaderInformation.ConversationId = ConversionHelpers.ExtractIntFromAddress(ref ptr2, false);
					segmentHeaderInformation.RequestId = ConversionHelpers.ExtractIntFromAddress(ref ptr2, false);
				}
				else
				{
					segmentHeaderInformation.ConversationId = 0;
					segmentHeaderInformation.RequestId = 0;
				}
				byte* ptr3 = (byte*)ptr2;
				segmentHeaderInformation.LittleEndian = *(ptr3++) == 2;
				byte b = *(ptr3++);
				segmentHeaderInformation.SegmentType = (SegmentType)b;
				byte b2 = *(ptr3++);
				segmentHeaderInformation.ControlFlag1 = (ControlFlag1)b2;
				b2 = *(ptr3++);
				segmentHeaderInformation.ControlFlag2 = (ControlFlag2)b2;
				long* ptr4 = (long*)ptr3;
				ptr4++;
				ptr2 = (int*)ptr4;
				segmentHeaderInformation.NumericEncoding = ConversionHelpers.ExtractIntFromAddress(ref ptr2, segmentHeaderInformation.LittleEndian);
				short* ptr5 = (short*)ptr2;
				segmentHeaderInformation.Ccsid = ConversionHelpers.ExtractShortFromAddress(ref ptr5, segmentHeaderInformation.LittleEndian);
				ptr5++;
				return segmentHeaderInformation;
			}
		}

		// Token: 0x0600592D RID: 22829 RVA: 0x00170364 File Offset: 0x0016E564
		internal unsafe static void FillLength(byte[] buffer, int lengthOfSegment)
		{
			fixed (byte* ptr = &buffer[4])
			{
				ConversionHelpers.MoveIntToAddressBigEndian(ptr, lengthOfSegment);
			}
		}

		// Token: 0x0600592E RID: 22830 RVA: 0x00170384 File Offset: 0x0016E584
		internal unsafe static void GenerateApiHeader(DynamicDataBuffer buffer, int index, int replyLength, int objectHandle)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				ConversionHelpers.MoveIntToAddressBigEndian((byte*)ptr2, replyLength);
				ptr2++;
				*(ptr2++) = 0;
				*(ptr2++) = 0;
				*(ptr2++) = objectHandle;
			}
		}

		// Token: 0x0600592F RID: 22831 RVA: 0x001703C4 File Offset: 0x0016E5C4
		internal unsafe static ApiHeaderInformation ExtractApiHeader(DynamicDataBuffer buffer, int index, bool littleEndian)
		{
			ApiHeaderInformation apiHeaderInformation = new ApiHeaderInformation();
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				apiHeaderInformation.ReplyLength = ConversionHelpers.ExtractIntFromAddress(ref ptr2, false);
				apiHeaderInformation.CompletionCode = (CompletionCode)ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
				apiHeaderInformation.ReasonCode = (ReturnCode)ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
				apiHeaderInformation.ObjectHandle = ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
			}
			return apiHeaderInformation;
		}

		// Token: 0x06005930 RID: 22832 RVA: 0x00170424 File Offset: 0x0016E624
		internal unsafe static void GenerateObjectDescriptor(DynamicDataBuffer buffer, int index, ObjectType objectType, string objectName, string dynamicQueueNamePrefix)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = 538985551;
				*(ptr3++) = 4;
				*(ptr3++) = (int)objectType;
				byte* ptr4 = (byte*)ptr3;
				ConversionHelpers.MoveStringToBufferAscii(buffer.Data, (int)((long)(ptr4 - ptr2) + (long)index), objectName, 48, false);
				ptr4 += 48;
				for (int i = 0; i < 48; i++)
				{
					*(ptr4++) = 0;
				}
				ConversionHelpers.MoveStringToBufferAscii(buffer.Data, (int)((long)(ptr4 - ptr2) + (long)index), dynamicQueueNamePrefix, 48, false);
				ptr4 += 48;
				for (int j = 0; j < 12; j++)
				{
					*(ptr4++) = 0;
				}
				ptr3 = (int*)ptr4;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				for (int k = 0; k < 24; k++)
				{
					*(ptr3++) = 0;
				}
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
				*(ptr3++) = 0;
			}
		}

		// Token: 0x06005931 RID: 22833 RVA: 0x001705D8 File Offset: 0x0016E7D8
		internal unsafe static string ExtractObjectName(DynamicDataBuffer buffer, int index, bool littleEndian, int ccsid)
		{
			HisEncoding encoding = HisEncoding.GetEncoding(ccsid);
			fixed (byte* ptr = &buffer.Data[index])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				ptr3 += 3;
				byte* ptr4 = (byte*)ptr3;
				int num = index + (int)((long)(ptr4 - ptr2));
				return ConversionHelpers.GetStringOrNull(buffer.Data, num, 48, encoding);
			}
		}

		// Token: 0x06005932 RID: 22834 RVA: 0x00170624 File Offset: 0x0016E824
		internal unsafe static void GenerateOpenOptions(DynamicDataBuffer buffer, int index, OpenOption options)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				*ptr2 = (int)options;
			}
		}

		// Token: 0x06005933 RID: 22835 RVA: 0x00170648 File Offset: 0x0016E848
		internal unsafe static void GenerateCloseOptions(DynamicDataBuffer buffer, int index, CloseOption options)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				*ptr2 = (int)options;
			}
		}

		// Token: 0x06005934 RID: 22836 RVA: 0x0017066C File Offset: 0x0016E86C
		internal unsafe static void GenerateFopa(DynamicDataBuffer buffer, int index)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				*(ptr2++) = 1095782214;
				*(ptr2++) = 1;
				*(ptr2++) = 28;
				*(ptr2++) = 0;
				*(ptr2++) = 0;
				*(ptr2++) = 0;
				*(ptr2++) = 0;
			}
		}

		// Token: 0x06005935 RID: 22837 RVA: 0x001706C4 File Offset: 0x0016E8C4
		internal unsafe static void GeneratePutGet(DynamicDataBuffer buffer, int index, int length)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				*ptr2 = length;
			}
		}

		// Token: 0x06005936 RID: 22838 RVA: 0x001706E8 File Offset: 0x0016E8E8
		internal unsafe static int ExtractPutGet(DynamicDataBuffer buffer, int index, bool littleEndian)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				return ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
			}
		}

		// Token: 0x06005937 RID: 22839 RVA: 0x00170710 File Offset: 0x0016E910
		internal unsafe static void GenerateSocketAction(DynamicDataBuffer buffer, int index, int conversationId, SocketActionType socketActionType)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				*(ptr2++) = conversationId;
				*(ptr2++) = 0;
				*(ptr2++) = (int)socketActionType;
				*(ptr2++) = 0;
				*ptr2 = 0;
			}
		}

		// Token: 0x06005938 RID: 22840 RVA: 0x00170750 File Offset: 0x0016E950
		internal unsafe static SocketActionInformation ExtractSocketAction(DynamicDataBuffer buffer, int index, bool littleEndian)
		{
			SocketActionInformation socketActionInformation = new SocketActionInformation();
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				socketActionInformation.ConversationId = ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
				ptr2++;
				socketActionInformation.SocketActionType = (SocketActionType)ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
			}
			return socketActionInformation;
		}

		// Token: 0x06005939 RID: 22841 RVA: 0x00170798 File Offset: 0x0016E998
		internal unsafe static NotificationInformation ExtractNotification(DynamicDataBuffer buffer, int index, bool littleEndian)
		{
			NotificationInformation notificationInformation = new NotificationInformation();
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				if (ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian) != 1)
				{
					throw new InvalidOperationException("Version of Notification is not 1");
				}
				notificationInformation.ObjectHandle = ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
				notificationInformation.Code = (NotificationType)ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
				notificationInformation.ReasonCode = (ReturnCode)ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
			}
			return notificationInformation;
		}

		// Token: 0x0600593A RID: 22842 RVA: 0x00170800 File Offset: 0x0016EA00
		internal static int GenerateHeartbeat(DynamicDataBuffer buffer)
		{
			int num = SegmentHelpers.GenerateSegmentHeader(buffer, 1128813396, SegmentHeaderType.Tshc, 1, 0, SegmentType.Heartbeat, ControlFlag1.ConfirmRequest, ControlFlag2.None);
			buffer.UsedLength = num;
			SegmentHelpers.FillLength(buffer.Data, buffer.UsedLength);
			return num;
		}

		// Token: 0x0600593B RID: 22843 RVA: 0x0017083C File Offset: 0x0016EA3C
		internal unsafe static void GenerateMqStat(DynamicDataBuffer buffer, int index)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				*(ptr2++) = 0;
				*(ptr2++) = 1413567571;
				*(ptr2++) = 1;
				for (int i = 0; i < 54; i++)
				{
					*(ptr2++) = 0;
				}
			}
		}

		// Token: 0x0600593C RID: 22844 RVA: 0x0017088C File Offset: 0x0016EA8C
		internal unsafe static void GenerateXaOpen(DynamicDataBuffer buffer, int index, int resourceManagerId)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				*(ptr2++) = 805371904;
				*(ptr2++) = 0;
				*(ptr2++) = 0;
				*ptr2 = resourceManagerId;
			}
		}

		// Token: 0x0600593D RID: 22845 RVA: 0x001708C8 File Offset: 0x0016EAC8
		internal unsafe static XaReturnCode ExtractXaOpenReturnCode(DynamicDataBuffer buffer, int index, bool littleEndian)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				ptr2++;
				return (XaReturnCode)ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
			}
		}

		// Token: 0x0600593E RID: 22846 RVA: 0x001708F4 File Offset: 0x0016EAF4
		internal unsafe static void GenerateXaClose(DynamicDataBuffer buffer, int index, int resourceManagerId)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				*(ptr2++) = 805371904;
				*(ptr2++) = 0;
				*(ptr2++) = 0;
				*ptr2 = resourceManagerId;
			}
		}

		// Token: 0x0600593F RID: 22847 RVA: 0x00170930 File Offset: 0x0016EB30
		internal unsafe static XaReturnCode ExtractXaCloseReturnCode(DynamicDataBuffer buffer, int index, bool littleEndian)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				ptr2++;
				return (XaReturnCode)ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
			}
		}

		// Token: 0x06005940 RID: 22848 RVA: 0x0017095C File Offset: 0x0016EB5C
		internal unsafe static int GenerateXaInfo(DynamicDataBuffer buffer, int index, string content)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				byte* ptr2 = ptr;
				byte* ptr3 = ptr2;
				*(ptr3++) = (byte)content.Length;
				ConversionHelpers.MoveStringToBufferAscii(buffer.Data, index + 1, content);
				ptr3 += content.Length;
				*(ptr3++) = 0;
				int num = (int)((long)(ptr3 - ptr2)) % 4;
				if (num != 0)
				{
					int num2 = 4 - num;
					for (int i = 0; i < num2; i++)
					{
						*(ptr3++) = 0;
					}
				}
				return (int)((long)(ptr3 - ptr2));
			}
		}

		// Token: 0x06005941 RID: 22849 RVA: 0x001709D8 File Offset: 0x0016EBD8
		internal unsafe static void GenerateXaStart(DynamicDataBuffer buffer, int index, int resourceManagerId, XaFlags flags)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				*(ptr2++) = 2080374784;
				*(ptr2++) = 0;
				*(ptr2++) = (int)flags;
				*ptr2 = resourceManagerId;
			}
		}

		// Token: 0x06005942 RID: 22850 RVA: 0x00170A14 File Offset: 0x0016EC14
		internal unsafe static XaReturnCode ExtractXaStartReturnCode(DynamicDataBuffer buffer, int index, bool littleEndian)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				ptr2++;
				return (XaReturnCode)ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
			}
		}

		// Token: 0x06005943 RID: 22851 RVA: 0x00170A40 File Offset: 0x0016EC40
		internal unsafe static int GenerateXaXid(DynamicDataBuffer buffer, int index, Xid xid)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				byte* ptr2 = ptr;
				byte* ptr3 = ptr2;
				*(ptr3++) = 81;
				*(ptr3++) = 77;
				*(ptr3++) = 72;
				*(ptr3++) = 0;
				*(ptr3++) = (byte)xid.TransactionIdLength;
				*(ptr3++) = (byte)xid.BranchQualifierLength;
				for (int i = 0; i < xid.TransactionIdLength + xid.BranchQualifierLength; i++)
				{
					*(ptr3++) = xid.Data[i];
				}
				return (int)((long)(ptr3 - ptr2));
			}
		}

		// Token: 0x06005944 RID: 22852 RVA: 0x00170AC8 File Offset: 0x0016ECC8
		internal unsafe static Xid ExtractXaXid(DynamicDataBuffer buffer, int index, bool littleEndian)
		{
			int num;
			int num2;
			int num3;
			byte[] array;
			fixed (byte* ptr = &buffer.Data[index])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				num = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				num2 = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				num3 = ConversionHelpers.ExtractIntFromAddress(ref ptr3, littleEndian);
				array = new byte[num2 + num3];
				int num4 = index + (int)((long)((byte*)ptr3 - (byte*)ptr2));
				Array.Copy(buffer.Data, num4, array, 0, num2 + num3);
			}
			return new Xid(num, num2, num3, array);
		}

		// Token: 0x06005945 RID: 22853 RVA: 0x00170B38 File Offset: 0x0016ED38
		internal unsafe static void GenerateXaEnd(DynamicDataBuffer buffer, int index, int resourceManagerId, XaFlags flags)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				*(ptr2++) = 2080374784;
				*(ptr2++) = 0;
				*(ptr2++) = (int)flags;
				*ptr2 = resourceManagerId;
			}
		}

		// Token: 0x06005946 RID: 22854 RVA: 0x00170B74 File Offset: 0x0016ED74
		internal unsafe static XaReturnCode ExtractXaEndReturnCode(DynamicDataBuffer buffer, int index, bool littleEndian)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				ptr2++;
				return (XaReturnCode)ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
			}
		}

		// Token: 0x06005947 RID: 22855 RVA: 0x00170BA0 File Offset: 0x0016EDA0
		internal unsafe static void GenerateXaPrepare(DynamicDataBuffer buffer, int index, int resourceManagerId, XaFlags flags)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				*(ptr2++) = 2080374784;
				*(ptr2++) = 0;
				*(ptr2++) = (int)flags;
				*ptr2 = resourceManagerId;
			}
		}

		// Token: 0x06005948 RID: 22856 RVA: 0x00170BDC File Offset: 0x0016EDDC
		internal unsafe static XaReturnCode ExtractXaPrepareReturnCode(DynamicDataBuffer buffer, int index, bool littleEndian)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				ptr2++;
				return (XaReturnCode)ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
			}
		}

		// Token: 0x06005949 RID: 22857 RVA: 0x00170C08 File Offset: 0x0016EE08
		internal unsafe static void GenerateXaCommit(DynamicDataBuffer buffer, int index, int resourceManagerId, XaFlags flags)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				*(ptr2++) = 2080374784;
				*(ptr2++) = 0;
				*(ptr2++) = (int)flags;
				*ptr2 = resourceManagerId;
			}
		}

		// Token: 0x0600594A RID: 22858 RVA: 0x00170C44 File Offset: 0x0016EE44
		internal unsafe static XaReturnCode ExtractXaCommitReturnCode(DynamicDataBuffer buffer, int index, bool littleEndian)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				ptr2++;
				return (XaReturnCode)ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
			}
		}

		// Token: 0x0600594B RID: 22859 RVA: 0x00170C70 File Offset: 0x0016EE70
		internal unsafe static void GenerateXaRollback(DynamicDataBuffer buffer, int index, int resourceManagerId, XaFlags flags)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				*(ptr2++) = 2080374784;
				*(ptr2++) = 0;
				*(ptr2++) = (int)flags;
				*ptr2 = resourceManagerId;
			}
		}

		// Token: 0x0600594C RID: 22860 RVA: 0x00170CAC File Offset: 0x0016EEAC
		internal unsafe static XaReturnCode ExtractXaRollbackReturnCode(DynamicDataBuffer buffer, int index, bool littleEndian)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				ptr2++;
				return (XaReturnCode)ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
			}
		}

		// Token: 0x0600594D RID: 22861 RVA: 0x00170CD8 File Offset: 0x0016EED8
		internal unsafe static void GenerateXaForget(DynamicDataBuffer buffer, int index, int resourceManagerId, XaFlags flags)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				*(ptr2++) = 2080374784;
				*(ptr2++) = 0;
				*(ptr2++) = (int)flags;
				*ptr2 = resourceManagerId;
			}
		}

		// Token: 0x0600594E RID: 22862 RVA: 0x00170D14 File Offset: 0x0016EF14
		internal unsafe static XaReturnCode ExtractXaForgetReturnCode(DynamicDataBuffer buffer, int index, bool littleEndian)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				ptr2++;
				return (XaReturnCode)ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
			}
		}

		// Token: 0x0600594F RID: 22863 RVA: 0x00170D40 File Offset: 0x0016EF40
		internal unsafe static void GenerateXaRecover(DynamicDataBuffer buffer, int index, int resourceManagerId, XaFlags flags, int maximumNumberOfXids)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				*(ptr2++) = 2080374784;
				*(ptr2++) = 0;
				*(ptr2++) = (int)flags;
				*(ptr2++) = resourceManagerId;
				*(ptr2++) = maximumNumberOfXids;
			}
		}

		// Token: 0x06005950 RID: 22864 RVA: 0x00170D88 File Offset: 0x0016EF88
		internal unsafe static XaReturnCode ExtractXaRecoverReturnCode(DynamicDataBuffer buffer, int index, bool littleEndian)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				ptr2++;
				return (XaReturnCode)ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
			}
		}

		// Token: 0x06005951 RID: 22865 RVA: 0x00170DB4 File Offset: 0x0016EFB4
		internal unsafe static int ExtractXaRecoverXidCount(DynamicDataBuffer buffer, int index, bool littleEndian)
		{
			fixed (byte* ptr = &buffer.Data[index])
			{
				int* ptr2 = (int*)ptr;
				ptr2 += 4;
				return ConversionHelpers.ExtractIntFromAddress(ref ptr2, littleEndian);
			}
		}
	}
}
