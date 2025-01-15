using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Cloud.Platform.Eventing.Etw
{
	// Token: 0x020003DB RID: 987
	internal static class NativeMethods
	{
		// Token: 0x06001E5D RID: 7773
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		internal static extern uint RegisterTraceGuids([In] NativeMethods.EtwProc cbFunc, [In] IntPtr context, [In] ref Guid controlGuid, [In] uint guidCount, [In] IntPtr guidRegs, [In] string mofImagePath, [In] string mofResourceName, out long regHandle);

		// Token: 0x06001E5E RID: 7774
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		internal static extern uint UnregisterTraceGuids([In] long regHandle);

		// Token: 0x06001E5F RID: 7775
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		internal static extern long GetTraceLoggerHandle([In] IntPtr Buffer);

		// Token: 0x06001E60 RID: 7776
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", SetLastError = true)]
		internal static extern uint TraceEvent([In] long traceHandle, [In] ref NativeMethods.EVENT_TRACE_HEADER_CUSTOM header);

		// Token: 0x06001E61 RID: 7777
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		internal static extern uint StartTrace(out long SessionHandle, [In] string SessionName, ref NativeMethods.EVENT_TRACE_PROPERTIES_CUSTOM Properties);

		// Token: 0x06001E62 RID: 7778
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		internal static extern uint EnableTrace([In] uint enable, [In] uint enableFlag, [In] uint enableLevel, ref Guid providerGuid, [In] long sessionHandle);

		// Token: 0x06001E63 RID: 7779
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		internal static extern uint ControlTrace([In] long sessionHandle, [In] string sessionName, ref NativeMethods.EVENT_TRACE_PROPERTIES_CUSTOM Properties, [In] uint controlCode);

		// Token: 0x06001E64 RID: 7780
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern long OpenTrace(ref NativeMethods.EVENT_TRACE_LOGFILE Logfile);

		// Token: 0x06001E65 RID: 7781
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		internal static extern uint CloseTrace(long hTrace);

		// Token: 0x06001E66 RID: 7782
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		internal static extern uint ProcessTrace([In] long[] TraceHandles, [In] uint HandleCount, IntPtr StartTime, IntPtr EndTime);

		// Token: 0x06001E67 RID: 7783
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		internal static extern uint EnumerateTraceGuids([In] IntPtr[] GuidPropertiesArray, [In] uint PropertyArrayCount, out uint GuidCount);

		// Token: 0x06001E68 RID: 7784 RVA: 0x000729B4 File Offset: 0x00070BB4
		static NativeMethods()
		{
			Marshal.SizeOf(typeof(IntPtr));
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x000729C8 File Offset: 0x00070BC8
		internal static void Verify(uint rc, string message)
		{
			if (rc != 0U)
			{
				throw new EtwException((int)rc, message);
			}
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x000729D5 File Offset: 0x00070BD5
		internal static void Verify(long h, string message)
		{
			if (h == -1L)
			{
				throw new EtwException(message);
			}
		}

		// Token: 0x04000A7A RID: 2682
		internal const long INVALID_TRACE_HANDLE = -1L;

		// Token: 0x04000A7B RID: 2683
		internal const long SHORT_INVALID_TRACE_HANDLE = 4294967295L;

		// Token: 0x04000A7C RID: 2684
		internal const int TRACE_VERSION_CURRENT = 1;

		// Token: 0x04000A7D RID: 2685
		internal const int ERROR_SUCCESS = 0;

		// Token: 0x04000A7E RID: 2686
		internal const int ERROR_NOT_ENOUGH_MEMORY = 8;

		// Token: 0x04000A7F RID: 2687
		internal const int ERROR_INVALID_PARAMETER = 87;

		// Token: 0x04000A80 RID: 2688
		internal const int ERROR_CTX_CLOSE_PENDING = 7007;

		// Token: 0x04000A81 RID: 2689
		internal const int ERROR_BUSY = 170;

		// Token: 0x04000A82 RID: 2690
		internal const int ERROR_CANCELLED = 1223;

		// Token: 0x04000A83 RID: 2691
		internal const int ERROR_MORE_DATA = 234;

		// Token: 0x04000A84 RID: 2692
		internal const uint WNODE_FLAG_TRACED_GUID = 131072U;

		// Token: 0x04000A85 RID: 2693
		internal const uint WNODE_FLAG_LOG_WNODE = 262144U;

		// Token: 0x04000A86 RID: 2694
		internal const uint WNODE_FLAG_USE_GUID_PTR = 524288U;

		// Token: 0x04000A87 RID: 2695
		internal const uint WNODE_FLAG_USE_MOF_PTR = 1048576U;

		// Token: 0x04000A88 RID: 2696
		internal const uint EVENT_TRACE_FILE_MODE_NONE = 0U;

		// Token: 0x04000A89 RID: 2697
		internal const uint EVENT_TRACE_FILE_MODE_SEQUENTIAL = 1U;

		// Token: 0x04000A8A RID: 2698
		internal const uint EVENT_TRACE_FILE_MODE_CIRCULAR = 2U;

		// Token: 0x04000A8B RID: 2699
		internal const uint EVENT_TRACE_FILE_MODE_APPEND = 4U;

		// Token: 0x04000A8C RID: 2700
		internal const uint EVENT_TRACE_FILE_MODE_MASK = 15U;

		// Token: 0x04000A8D RID: 2701
		internal const uint EVENT_TRACE_REAL_TIME_MODE = 256U;

		// Token: 0x04000A8E RID: 2702
		internal const uint EVENT_TRACE_DELAY_OPEN_FILE_MODE = 512U;

		// Token: 0x04000A8F RID: 2703
		internal const uint EVENT_TRACE_BUFFERING_MODE = 1024U;

		// Token: 0x04000A90 RID: 2704
		internal const uint EVENT_TRACE_PRIVATE_LOGGER_MODE = 2048U;

		// Token: 0x04000A91 RID: 2705
		internal const uint EVENT_TRACE_ADD_HEADER_MODE = 4096U;

		// Token: 0x04000A92 RID: 2706
		internal const uint EVENT_TRACE_USE_GLOBAL_SEQUENCE = 16384U;

		// Token: 0x04000A93 RID: 2707
		internal const uint EVENT_TRACE_USE_LOCAL_SEQUENCE = 32768U;

		// Token: 0x04000A94 RID: 2708
		internal const uint EVENT_TRACE_RELOG_MODE = 65536U;

		// Token: 0x04000A95 RID: 2709
		internal const uint EVENT_TRACE_USE_PAGED_MEMORY = 16777216U;

		// Token: 0x04000A96 RID: 2710
		internal const uint EVENT_TRACE_FILE_MODE_NEWFILE = 8U;

		// Token: 0x04000A97 RID: 2711
		internal const uint EVENT_TRACE_FILE_MODE_PREALLOCATE = 32U;

		// Token: 0x04000A98 RID: 2712
		internal const uint EVENT_TRACE_NONSTOPPABLE_MODE = 64U;

		// Token: 0x04000A99 RID: 2713
		internal const uint EVENT_TRACE_SECURE_MODE = 128U;

		// Token: 0x04000A9A RID: 2714
		internal const uint EVENT_TRACE_USE_KBYTES_FOR_SIZE = 8192U;

		// Token: 0x04000A9B RID: 2715
		internal const uint EVENT_TRACE_PRIVATE_IN_PROC = 131072U;

		// Token: 0x04000A9C RID: 2716
		internal const uint EVENT_TRACE_MODE_RESERVED = 1048576U;

		// Token: 0x04000A9D RID: 2717
		internal const uint EVENT_TRACE_SESSION_FLAGS_MASK = 17039080U;

		// Token: 0x04000A9E RID: 2718
		internal const byte EVENT_TRACE_TYPE_INFO = 0;

		// Token: 0x04000A9F RID: 2719
		internal const byte EVENT_TRACE_TYPE_START = 1;

		// Token: 0x04000AA0 RID: 2720
		internal const byte EVENT_TRACE_TYPE_END = 2;

		// Token: 0x04000AA1 RID: 2721
		internal const byte EVENT_TRACE_TYPE_STOP = 2;

		// Token: 0x04000AA2 RID: 2722
		internal const byte EVENT_TRACE_TYPE_DC_START = 3;

		// Token: 0x04000AA3 RID: 2723
		internal const byte EVENT_TRACE_TYPE_DC_END = 4;

		// Token: 0x04000AA4 RID: 2724
		internal const byte EVENT_TRACE_TYPE_EXTENSION = 5;

		// Token: 0x04000AA5 RID: 2725
		internal const byte EVENT_TRACE_TYPE_REPLY = 6;

		// Token: 0x04000AA6 RID: 2726
		internal const byte EVENT_TRACE_TYPE_DEQUEUE = 7;

		// Token: 0x04000AA7 RID: 2727
		internal const byte EVENT_TRACE_TYPE_RESUME = 7;

		// Token: 0x04000AA8 RID: 2728
		internal const byte EVENT_TRACE_TYPE_CHECKPOINT = 8;

		// Token: 0x04000AA9 RID: 2729
		internal const byte EVENT_TRACE_TYPE_SUSPEND = 8;

		// Token: 0x04000AAA RID: 2730
		internal const byte EVENT_TRACE_TYPE_WINEVT_SEND = 9;

		// Token: 0x04000AAB RID: 2731
		internal const byte EVENT_TRACE_TYPE_WINEVT_RECEIVE = 240;

		// Token: 0x04000AAC RID: 2732
		internal const uint EVENT_TRACE_CONTROL_QUERY = 0U;

		// Token: 0x04000AAD RID: 2733
		internal const uint EVENT_TRACE_CONTROL_STOP = 1U;

		// Token: 0x04000AAE RID: 2734
		internal const uint EVENT_TRACE_CONTROL_UPDATE = 2U;

		// Token: 0x04000AAF RID: 2735
		internal const uint EVENT_TRACE_CONTROL_FLUSH = 3U;

		// Token: 0x04000AB0 RID: 2736
		internal const uint TRACE_MESSAGE = 268435456U;

		// Token: 0x020007DA RID: 2010
		internal struct EVENT_TRACE_HEADER_CLASS
		{
			// Token: 0x0400172E RID: 5934
			internal byte Type;

			// Token: 0x0400172F RID: 5935
			internal byte Level;

			// Token: 0x04001730 RID: 5936
			internal ushort Version;
		}

		// Token: 0x020007DB RID: 2011
		internal struct EVENT_TRACE_HEADER
		{
			// Token: 0x04001731 RID: 5937
			internal ushort Size;

			// Token: 0x04001732 RID: 5938
			internal ushort FieldTypeFlags;

			// Token: 0x04001733 RID: 5939
			internal NativeMethods.EVENT_TRACE_HEADER_CLASS Class;

			// Token: 0x04001734 RID: 5940
			internal uint ThreadId;

			// Token: 0x04001735 RID: 5941
			internal uint ProcessId;

			// Token: 0x04001736 RID: 5942
			internal long TimeStamp;

			// Token: 0x04001737 RID: 5943
			internal Guid Guid;

			// Token: 0x04001738 RID: 5944
			internal uint ClientContext;

			// Token: 0x04001739 RID: 5945
			internal uint Flags;
		}

		// Token: 0x020007DC RID: 2012
		internal struct WNODE_HEADER
		{
			// Token: 0x0400173A RID: 5946
			internal uint BufferSize;

			// Token: 0x0400173B RID: 5947
			internal uint ProviderId;

			// Token: 0x0400173C RID: 5948
			internal ulong HistoricalContext;

			// Token: 0x0400173D RID: 5949
			internal ulong TimeStamp;

			// Token: 0x0400173E RID: 5950
			internal Guid Guid;

			// Token: 0x0400173F RID: 5951
			internal uint ClientContext;

			// Token: 0x04001740 RID: 5952
			internal uint Flags;
		}

		// Token: 0x020007DD RID: 2013
		internal struct EVENT_TRACE_PROPERTIES
		{
			// Token: 0x04001741 RID: 5953
			internal NativeMethods.WNODE_HEADER Wnode;

			// Token: 0x04001742 RID: 5954
			internal uint BufferSize;

			// Token: 0x04001743 RID: 5955
			internal uint MinimumBuffers;

			// Token: 0x04001744 RID: 5956
			internal uint MaximumBuffers;

			// Token: 0x04001745 RID: 5957
			internal uint MaximumFileSize;

			// Token: 0x04001746 RID: 5958
			internal uint LogFileMode;

			// Token: 0x04001747 RID: 5959
			internal uint FlushTimer;

			// Token: 0x04001748 RID: 5960
			internal uint EnableFlags;

			// Token: 0x04001749 RID: 5961
			internal uint AgeLimit;

			// Token: 0x0400174A RID: 5962
			internal uint NumberOfBuffers;

			// Token: 0x0400174B RID: 5963
			internal uint FreeBuffers;

			// Token: 0x0400174C RID: 5964
			internal uint EventsLost;

			// Token: 0x0400174D RID: 5965
			internal uint BuffersWritten;

			// Token: 0x0400174E RID: 5966
			internal uint LogBuffersLost;

			// Token: 0x0400174F RID: 5967
			internal uint RealTimeBuffersLost;

			// Token: 0x04001750 RID: 5968
			internal UIntPtr LoggerThreadId;

			// Token: 0x04001751 RID: 5969
			internal uint LogFileNameOffset;

			// Token: 0x04001752 RID: 5970
			internal uint LoggerNameOffset;
		}

		// Token: 0x020007DE RID: 2014
		internal struct EVENT_TRACE
		{
			// Token: 0x04001753 RID: 5971
			internal NativeMethods.EVENT_TRACE_HEADER Header;

			// Token: 0x04001754 RID: 5972
			internal uint InstanceId;

			// Token: 0x04001755 RID: 5973
			internal uint ParentInstanceId;

			// Token: 0x04001756 RID: 5974
			internal Guid ParentGuid;

			// Token: 0x04001757 RID: 5975
			internal IntPtr MofData;

			// Token: 0x04001758 RID: 5976
			internal uint MofLength;

			// Token: 0x04001759 RID: 5977
			internal uint ClientContext;
		}

		// Token: 0x020007DF RID: 2015
		internal struct FILETIME
		{
			// Token: 0x0400175A RID: 5978
			internal uint dwLowDateTime;

			// Token: 0x0400175B RID: 5979
			internal uint dwHighDateTime;
		}

		// Token: 0x020007E0 RID: 2016
		internal struct SYSTEMTIME
		{
			// Token: 0x0400175C RID: 5980
			internal ushort wYear;

			// Token: 0x0400175D RID: 5981
			internal ushort wMonth;

			// Token: 0x0400175E RID: 5982
			internal ushort wDayOfWeek;

			// Token: 0x0400175F RID: 5983
			internal ushort wDay;

			// Token: 0x04001760 RID: 5984
			internal ushort wHour;

			// Token: 0x04001761 RID: 5985
			internal ushort wMinute;

			// Token: 0x04001762 RID: 5986
			internal ushort wSecond;

			// Token: 0x04001763 RID: 5987
			internal ushort wMilliseconds;
		}

		// Token: 0x020007E1 RID: 2017
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct TIME_ZONE_INFORMATION
		{
			// Token: 0x04001764 RID: 5988
			private int Bias;

			// Token: 0x04001765 RID: 5989
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			private string StandardName;

			// Token: 0x04001766 RID: 5990
			private NativeMethods.SYSTEMTIME StandardDate;

			// Token: 0x04001767 RID: 5991
			private int StandardBias;

			// Token: 0x04001768 RID: 5992
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			private string DaylightName;

			// Token: 0x04001769 RID: 5993
			private NativeMethods.SYSTEMTIME DaylightDate;

			// Token: 0x0400176A RID: 5994
			private int DaylightBias;
		}

		// Token: 0x020007E2 RID: 2018
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct TRACE_LOGFILE_HEADER
		{
			// Token: 0x0400176B RID: 5995
			internal uint BufferSize;

			// Token: 0x0400176C RID: 5996
			internal uint Version;

			// Token: 0x0400176D RID: 5997
			internal uint ProviderVersion;

			// Token: 0x0400176E RID: 5998
			internal uint NumberOfProcessors;

			// Token: 0x0400176F RID: 5999
			internal long EndTime;

			// Token: 0x04001770 RID: 6000
			internal uint TimerResolution;

			// Token: 0x04001771 RID: 6001
			internal uint MaximumFileSize;

			// Token: 0x04001772 RID: 6002
			internal uint LogFileMode;

			// Token: 0x04001773 RID: 6003
			internal uint BuffersWritten;

			// Token: 0x04001774 RID: 6004
			internal uint StartBuffers;

			// Token: 0x04001775 RID: 6005
			internal uint PointerSize;

			// Token: 0x04001776 RID: 6006
			internal uint EventsLost;

			// Token: 0x04001777 RID: 6007
			internal uint CpuSpeedInMHz;

			// Token: 0x04001778 RID: 6008
			internal IntPtr LoggerName;

			// Token: 0x04001779 RID: 6009
			internal IntPtr LogFileName;

			// Token: 0x0400177A RID: 6010
			private NativeMethods.TIME_ZONE_INFORMATION TimeZone;

			// Token: 0x0400177B RID: 6011
			internal long BootTime;

			// Token: 0x0400177C RID: 6012
			internal long PerfFreq;

			// Token: 0x0400177D RID: 6013
			internal long StartTime;

			// Token: 0x0400177E RID: 6014
			internal uint ReservedFlags;

			// Token: 0x0400177F RID: 6015
			internal uint BuffersLost;
		}

		// Token: 0x020007E3 RID: 2019
		// (Invoke) Token: 0x06003216 RID: 12822
		internal delegate uint BufferCallbackProc(ref NativeMethods.EVENT_TRACE_LOGFILE LogFile);

		// Token: 0x020007E4 RID: 2020
		// (Invoke) Token: 0x0600321A RID: 12826
		internal delegate uint EventCallbackProc(ref NativeMethods.EVENT_TRACE Event);

		// Token: 0x020007E5 RID: 2021
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct EVENT_TRACE_LOGFILE
		{
			// Token: 0x04001780 RID: 6016
			internal string LogFileName;

			// Token: 0x04001781 RID: 6017
			internal string LoggerName;

			// Token: 0x04001782 RID: 6018
			internal long CurrentTime;

			// Token: 0x04001783 RID: 6019
			internal uint BuffersRead;

			// Token: 0x04001784 RID: 6020
			internal uint LogFileMode;

			// Token: 0x04001785 RID: 6021
			internal NativeMethods.EVENT_TRACE CurrentEvent;

			// Token: 0x04001786 RID: 6022
			internal NativeMethods.TRACE_LOGFILE_HEADER LogfileHeader;

			// Token: 0x04001787 RID: 6023
			internal NativeMethods.BufferCallbackProc BufferCallback;

			// Token: 0x04001788 RID: 6024
			internal uint BufferSize;

			// Token: 0x04001789 RID: 6025
			internal uint Filled;

			// Token: 0x0400178A RID: 6026
			internal uint EventsLost;

			// Token: 0x0400178B RID: 6027
			internal NativeMethods.EventCallbackProc EventCallback;

			// Token: 0x0400178C RID: 6028
			internal uint IsKernelTrace;

			// Token: 0x0400178D RID: 6029
			internal IntPtr Context;
		}

		// Token: 0x020007E6 RID: 2022
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct MOF_FIELD
		{
			// Token: 0x0400178E RID: 6030
			internal long DataPtr;

			// Token: 0x0400178F RID: 6031
			internal int Length;

			// Token: 0x04001790 RID: 6032
			internal int DataType;
		}

		// Token: 0x020007E7 RID: 2023
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct TRACE_GUID_PROPERTIES
		{
			// Token: 0x04001791 RID: 6033
			internal Guid Guid;

			// Token: 0x04001792 RID: 6034
			internal uint GuidType;

			// Token: 0x04001793 RID: 6035
			internal uint LoggerId;

			// Token: 0x04001794 RID: 6036
			internal uint EnableLevel;

			// Token: 0x04001795 RID: 6037
			internal uint EnableFlags;

			// Token: 0x04001796 RID: 6038
			internal byte IsEnable;
		}

		// Token: 0x020007E8 RID: 2024
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct TRACE_GUID_REGISTRATION
		{
			// Token: 0x04001797 RID: 6039
			internal IntPtr Guid;

			// Token: 0x04001798 RID: 6040
			internal IntPtr Handle;
		}

		// Token: 0x020007E9 RID: 2025
		internal enum WMIDPREQUESTCODE
		{
			// Token: 0x0400179A RID: 6042
			WMI_GET_ALL_DATA,
			// Token: 0x0400179B RID: 6043
			WMI_GET_SINGLE_INSTANCE,
			// Token: 0x0400179C RID: 6044
			WMI_SET_SINGLE_INSTANCE,
			// Token: 0x0400179D RID: 6045
			WMI_SET_SINGLE_ITEM,
			// Token: 0x0400179E RID: 6046
			WMI_ENABLE_EVENTS,
			// Token: 0x0400179F RID: 6047
			WMI_DISABLE_EVENTS,
			// Token: 0x040017A0 RID: 6048
			WMI_ENABLE_COLLECTION,
			// Token: 0x040017A1 RID: 6049
			WMI_DISABLE_COLLECTION,
			// Token: 0x040017A2 RID: 6050
			WMI_REGINFO,
			// Token: 0x040017A3 RID: 6051
			WMI_EXECUTE_METHOD
		}

		// Token: 0x020007EA RID: 2026
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct WMI_TRACE_MESSAGE_PACKET
		{
			// Token: 0x040017A4 RID: 6052
			internal ushort MessageNumber;

			// Token: 0x040017A5 RID: 6053
			internal ushort OptionFlags;
		}

		// Token: 0x020007EB RID: 2027
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct WMI_MESSAGE_TRACE_HEADER
		{
			// Token: 0x040017A6 RID: 6054
			internal uint HeaderMarker;

			// Token: 0x040017A7 RID: 6055
			internal NativeMethods.WMI_TRACE_MESSAGE_PACKET Packet;
		}

		// Token: 0x020007EC RID: 2028
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct EVENT_TRACE_PROPERTIES_CUSTOM
		{
			// Token: 0x0600321D RID: 12829 RVA: 0x000A9344 File Offset: 0x000A7544
			internal EVENT_TRACE_PROPERTIES_CUSTOM(int dummy)
			{
				this.m_props = default(NativeMethods.EVENT_TRACE_PROPERTIES);
				this.m_props.Wnode.BufferSize = (uint)Marshal.SizeOf(typeof(NativeMethods.EVENT_TRACE_PROPERTIES_CUSTOM));
				this.m_props.Wnode.Flags = 131072U;
				this.m_props.LoggerNameOffset = (uint)Marshal.SizeOf(typeof(NativeMethods.EVENT_TRACE_PROPERTIES));
				this.m_props.LogFileNameOffset = this.m_props.LoggerNameOffset + (uint)(256 * Marshal.SizeOf(typeof(ushort)));
				this.m_name = "";
				this.m_path = "";
			}

			// Token: 0x040017A8 RID: 6056
			internal NativeMethods.EVENT_TRACE_PROPERTIES m_props;

			// Token: 0x040017A9 RID: 6057
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string m_name;

			// Token: 0x040017AA RID: 6058
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string m_path;

			// Token: 0x040017AB RID: 6059
			private const int MAX_STRING = 256;
		}

		// Token: 0x020007ED RID: 2029
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct EVENT_TRACE_HEADER_CUSTOM
		{
			// Token: 0x040017AC RID: 6060
			internal NativeMethods.EVENT_TRACE_HEADER m_header;

			// Token: 0x040017AD RID: 6061
			internal NativeMethods.MOF_FIELD m_mof1;

			// Token: 0x040017AE RID: 6062
			internal NativeMethods.MOF_FIELD m_mof2;

			// Token: 0x040017AF RID: 6063
			internal NativeMethods.MOF_FIELD m_mof3;

			// Token: 0x040017B0 RID: 6064
			internal NativeMethods.MOF_FIELD m_mof4;

			// Token: 0x040017B1 RID: 6065
			internal NativeMethods.MOF_FIELD m_mof5;

			// Token: 0x040017B2 RID: 6066
			internal NativeMethods.MOF_FIELD m_mof6;

			// Token: 0x040017B3 RID: 6067
			internal NativeMethods.MOF_FIELD m_mof7;

			// Token: 0x040017B4 RID: 6068
			internal NativeMethods.MOF_FIELD m_mof8;

			// Token: 0x040017B5 RID: 6069
			internal NativeMethods.MOF_FIELD m_mof9;

			// Token: 0x040017B6 RID: 6070
			internal NativeMethods.MOF_FIELD m_mof10;

			// Token: 0x040017B7 RID: 6071
			internal NativeMethods.MOF_FIELD m_mof11;

			// Token: 0x040017B8 RID: 6072
			internal NativeMethods.MOF_FIELD m_mof12;

			// Token: 0x040017B9 RID: 6073
			internal NativeMethods.MOF_FIELD m_mof13;

			// Token: 0x040017BA RID: 6074
			internal NativeMethods.MOF_FIELD m_mof14;

			// Token: 0x040017BB RID: 6075
			internal NativeMethods.MOF_FIELD m_mof15;

			// Token: 0x040017BC RID: 6076
			internal NativeMethods.MOF_FIELD m_mof16;
		}

		// Token: 0x020007EE RID: 2030
		// (Invoke) Token: 0x0600321F RID: 12831
		internal delegate uint EtwProc([In] NativeMethods.WMIDPREQUESTCODE requestCode, [In] IntPtr requestContext, ref uint bufferSize, IntPtr buffer);
	}
}
