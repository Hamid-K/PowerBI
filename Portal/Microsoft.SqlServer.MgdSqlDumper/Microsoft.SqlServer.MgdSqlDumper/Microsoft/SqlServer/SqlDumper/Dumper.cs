using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using <CppImplementationDetails>;

namespace Microsoft.SqlServer.SqlDumper
{
	// Token: 0x02000002 RID: 2
	public sealed class Dumper : IDisposable
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00004A88 File Offset: 0x00003E88
		private void ~Dumper()
		{
			if (!this.m_pszFileName.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszFileName);
				IntPtr intPtr = new IntPtr(0);
				this.m_pszFileName = intPtr;
			}
			if (!this.m_pszExtraFileName.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszExtraFileName);
				IntPtr intPtr2 = new IntPtr(0);
				this.m_pszExtraFileName = intPtr2;
			}
			if (!this.m_pszDirectory.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszDirectory);
				IntPtr intPtr3 = new IntPtr(0);
				this.m_pszDirectory = intPtr3;
			}
			if (!this.m_pszTitleName.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszTitleName);
				IntPtr intPtr4 = new IntPtr(0);
				this.m_pszTitleName = intPtr4;
			}
			if (!this.m_pszImageName.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszImageName);
				IntPtr intPtr5 = new IntPtr(0);
				this.m_pszImageName = intPtr5;
			}
			if (!this.m_pszErrorText.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszErrorText);
				IntPtr intPtr6 = new IntPtr(0);
				this.m_pszErrorText = intPtr6;
			}
			if (!this.m_pszErrorDetail.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszErrorDetail);
				IntPtr intPtr7 = new IntPtr(0);
				this.m_pszErrorDetail = intPtr7;
			}
			if (!this.m_pszDumpIdentity.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszDumpIdentity);
				IntPtr intPtr8 = new IntPtr(0);
				this.m_pszDumpIdentity = intPtr8;
			}
			if (!this.m_pszInstanceName.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszInstanceName);
				IntPtr intPtr9 = new IntPtr(0);
				this.m_pszInstanceName = intPtr9;
			}
			if (!this.m_pszServiceName.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszServiceName);
				IntPtr intPtr10 = new IntPtr(0);
				this.m_pszServiceName = intPtr10;
			}
			if (!this.m_pszlogName.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszlogName);
				IntPtr intPtr11 = new IntPtr(0);
				this.m_pszlogName = intPtr11;
			}
			this.DumperDispose(false);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004CFC File Offset: 0x000040FC
		public unsafe void Dump()
		{
			IDmpDump* pDump = this.m_pDump;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), pDump, (IntPtr)(*(*(long*)pDump + 24L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00004D2C File Offset: 0x0000412C
		public unsafe void SetFlags(DumperFlags flagsTurnOn, DumperFlags flagsTurnOff)
		{
			IDmpDump* pDump = this.m_pDump;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)), pDump, flagsTurnOn, flagsTurnOff, (IntPtr)(*(*(long*)pDump + 32L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004D60 File Offset: 0x00004160
		public unsafe void SetMiniDumpFlags(MiniDumpFlags miniDumpFlagsTurnOn, MiniDumpFlags miniDumpFlagsTurnOff)
		{
			IDmpDump* pDump = this.m_pDump;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)), pDump, miniDumpFlagsTurnOn, miniDumpFlagsTurnOff, (IntPtr)(*(*(long*)pDump + 40L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004D94 File Offset: 0x00004194
		public unsafe void SetThreadId(int threadId)
		{
			IDmpDump* pDump = this.m_pDump;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)), pDump, threadId, (IntPtr)(*(*(long*)pDump + 48L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004DC8 File Offset: 0x000041C8
		public unsafe void SetExtraFile(string fileName)
		{
			if (!this.m_pszExtraFileName.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszExtraFileName);
				IntPtr intPtr = new IntPtr(0);
				this.m_pszExtraFileName = intPtr;
			}
			IntPtr intPtr2 = Marshal.StringToHGlobalUni(fileName);
			this.m_pszExtraFileName = intPtr2;
			IDmpDump* pDump = this.m_pDump;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*), pDump, (ushort*)this.m_pszExtraFileName.ToPointer(), (IntPtr)(*(*(long*)pDump + 104L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004E4C File Offset: 0x0000424C
		public unsafe void SetFileName(string fileName)
		{
			if (!this.m_pszFileName.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszFileName);
				IntPtr intPtr = new IntPtr(0);
				this.m_pszFileName = intPtr;
			}
			IntPtr intPtr2 = Marshal.StringToHGlobalUni(fileName);
			this.m_pszFileName = intPtr2;
			IDmpDump* pDump = this.m_pDump;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*), pDump, (ushort*)this.m_pszFileName.ToPointer(), (IntPtr)(*(*(long*)pDump + 112L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004ED0 File Offset: 0x000042D0
		public unsafe void SetDirectory(string dirPath)
		{
			if (!this.m_pszDirectory.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszDirectory);
				IntPtr intPtr = new IntPtr(0);
				this.m_pszDirectory = intPtr;
			}
			IntPtr intPtr2 = Marshal.StringToHGlobalUni(dirPath);
			IDmpDump* pDump = this.m_pDump;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*), pDump, (ushort*)intPtr2.ToPointer(), (IntPtr)(*(*(long*)pDump + 128L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004F4C File Offset: 0x0000434C
		public unsafe void SetAppImageName(string appImageName)
		{
			if (!this.m_pszImageName.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszImageName);
				IntPtr intPtr = new IntPtr(0);
				this.m_pszImageName = intPtr;
			}
			IntPtr intPtr2 = Marshal.StringToHGlobalUni(appImageName);
			this.m_pszImageName = intPtr2;
			IDmpDump* pDump = this.m_pDump;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*), pDump, (ushort*)this.m_pszImageName.ToPointer(), (IntPtr)(*(*(long*)pDump + 136L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004FD0 File Offset: 0x000043D0
		public unsafe void SetAppVersion(int mostSignificant, int leastSignificant)
		{
			IDmpDump* pDump = this.m_pDump;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)), pDump, mostSignificant, leastSignificant, (IntPtr)(*(*(long*)pDump + 144L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00005008 File Offset: 0x00004408
		public unsafe void SetTitleName(string titleName)
		{
			if (!this.m_pszTitleName.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszTitleName);
				IntPtr intPtr = new IntPtr(0);
				this.m_pszTitleName = intPtr;
			}
			IntPtr intPtr2 = Marshal.StringToHGlobalUni(titleName);
			this.m_pszTitleName = intPtr2;
			IDmpDump* pDump = this.m_pDump;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*), pDump, (ushort*)this.m_pszTitleName.ToPointer(), (IntPtr)(*(*(long*)pDump + 152L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000508C File Offset: 0x0000448C
		public unsafe void SetErrorText(string errorText)
		{
			if (!this.m_pszErrorText.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszErrorText);
				IntPtr intPtr = new IntPtr(0);
				this.m_pszErrorText = intPtr;
			}
			IntPtr intPtr2 = Marshal.StringToHGlobalUni(errorText);
			this.m_pszErrorText = intPtr2;
			IDmpDump* pDump = this.m_pDump;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*), pDump, (ushort*)this.m_pszErrorText.ToPointer(), (IntPtr)(*(*(long*)pDump + 168L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00005110 File Offset: 0x00004510
		public unsafe void SetErrorDetails(string errorDetail)
		{
			if (!this.m_pszErrorDetail.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszErrorDetail);
				IntPtr intPtr = new IntPtr(0);
				this.m_pszErrorDetail = intPtr;
			}
			IntPtr intPtr2 = Marshal.StringToHGlobalUni(errorDetail);
			this.m_pszErrorDetail = intPtr2;
			IDmpDump* pDump = this.m_pDump;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*), pDump, (ushort*)this.m_pszErrorDetail.ToPointer(), (IntPtr)(*(*(long*)pDump + 176L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00005194 File Offset: 0x00004594
		public unsafe void SetDumpIdentity(string dumpIdentity)
		{
			if (!this.m_pszDumpIdentity.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszDumpIdentity);
				IntPtr intPtr = new IntPtr(0);
				this.m_pszDumpIdentity = intPtr;
			}
			IntPtr intPtr2 = Marshal.StringToHGlobalUni(dumpIdentity);
			this.m_pszDumpIdentity = intPtr2;
			IDmpDump* pDump = this.m_pDump;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*), pDump, (ushort*)this.m_pszDumpIdentity.ToPointer(), (IntPtr)(*(*(long*)pDump + 232L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00005218 File Offset: 0x00004618
		public unsafe void SetWaitTime(int waitTime)
		{
			IDmpDump* pDump = this.m_pDump;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)), pDump, waitTime, (IntPtr)(*(*(long*)pDump + 184L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00005250 File Offset: 0x00004650
		public unsafe string GetDumpResultText()
		{
			IDmpDump* pDump = this.m_pDump;
			$ArrayType$$$BY0EAB@G $ArrayType$$$BY0EAB@G;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16*,System.UInt64), pDump, (ushort*)(&$ArrayType$$$BY0EAB@G), 1025UL, (IntPtr)(*(*(long*)pDump + 192L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			return new string((char*)(&$ArrayType$$$BY0EAB@G));
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00005294 File Offset: 0x00004694
		public unsafe void SetBucket(int framesToInclude, int framesToSkip)
		{
			StackFrame[] frames = new StackTrace(true).GetFrames();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (0 < frames.Length)
			{
				while (framesToInclude > 0)
				{
					if (framesToSkip > 1)
					{
						framesToSkip += -1;
					}
					else
					{
						framesToInclude += -1;
						StackFrame stackFrame = frames[num3];
						num += stackFrame.ToString().GetHashCode();
						if (num2 == null)
						{
							num2 = stackFrame.ToString().GetHashCode();
						}
					}
					num3++;
					if (num3 >= frames.Length)
					{
						break;
					}
				}
			}
			IDmpDump* pDump = this.m_pDump;
			int num4 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Void modopt(System.Runtime.CompilerServices.IsConst)*), pDump, -num2, (IntPtr)(*(*(long*)pDump + 208L)));
			if (num4 < 0)
			{
				Marshal.ThrowExceptionForHR(num4);
			}
			IDmpDump* pDump2 = this.m_pDump;
			int num5 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt64), pDump2, (ulong)num, (IntPtr)(*(*(long*)pDump2 + 160L)));
			if (num5 < 0)
			{
				Marshal.ThrowExceptionForHR(num5);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00005354 File Offset: 0x00004754
		public unsafe void SetBucketingParameters(int offset, int signature)
		{
			IDmpDump* pDump = this.m_pDump;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Void modopt(System.Runtime.CompilerServices.IsConst)*), pDump, offset, (IntPtr)(*(*(long*)pDump + 208L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			IDmpDump* pDump2 = this.m_pDump;
			int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt64), pDump2, (ulong)((long)signature), (IntPtr)(*(*(long*)pDump2 + 160L)));
			if (num2 < 0)
			{
				Marshal.ThrowExceptionForHR(num2);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000053B0 File Offset: 0x000047B0
		public unsafe void SetInstanceName(string instanceName)
		{
			if (!this.m_pszInstanceName.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszInstanceName);
				IntPtr intPtr = new IntPtr(0);
				this.m_pszInstanceName = intPtr;
			}
			IntPtr intPtr2 = Marshal.StringToHGlobalUni(instanceName);
			this.m_pszInstanceName = intPtr2;
			IDmpDump* pDump = this.m_pDump;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*), pDump, (ushort*)this.m_pszInstanceName.ToPointer(), (IntPtr)(*(*(long*)pDump + 216L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00005434 File Offset: 0x00004834
		public unsafe void SetServiceName(string serviceName)
		{
			if (!this.m_pszServiceName.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszServiceName);
				IntPtr intPtr = new IntPtr(0);
				this.m_pszServiceName = intPtr;
			}
			IntPtr intPtr2 = Marshal.StringToHGlobalUni(serviceName);
			this.m_pszServiceName = intPtr2;
			IDmpDump* pDump = this.m_pDump;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*), pDump, (ushort*)this.m_pszServiceName.ToPointer(), (IntPtr)(*(*(long*)pDump + 224L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000054B8 File Offset: 0x000048B8
		public void SetException(Exception ex)
		{
			if (ex.StackTrace != null)
			{
				this.SetBucketingParameters(ex.StackTrace.GetHashCode(), ex.StackTrace.GetHashCode());
				this.SetErrorText(ex.Message);
				this.SetErrorDetails(ex.StackTrace);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00005508 File Offset: 0x00004908
		public unsafe void SetLogFile(string logName, int size)
		{
			if (!this.m_pszlogName.Equals(IntPtr.Zero))
			{
				Marshal.FreeHGlobal(this.m_pszlogName);
				IntPtr intPtr = new IntPtr(0);
				this.m_pszlogName = intPtr;
			}
			IntPtr intPtr2 = Marshal.StringToHGlobalUni(logName);
			this.m_pszlogName = intPtr2;
			IDmpDump* pDump = this.m_pDump;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.IsConst)), pDump, (ushort*)this.m_pszlogName.ToPointer(), size, (IntPtr)(*(*(long*)pDump + 240L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00005590 File Offset: 0x00004990
		public unsafe void AddMemoryRaw(IntPtr pData, int size)
		{
			IDmpDump* pDump = this.m_pDump;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Void*,System.UInt64), pDump, (void*)pData, (ulong)((long)size), (IntPtr)(*(*(long*)pDump + 80L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000055CC File Offset: 0x000049CC
		public unsafe void RemoveMemoryRaw(IntPtr pData, int size)
		{
			IDmpDump* pDump = this.m_pDump;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Void*,System.UInt64), pDump, (void*)pData, (ulong)((long)size), (IntPtr)(*(*(long*)pDump + 88L)));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004948 File Offset: 0x00003D48
		internal unsafe Dumper()
		{
			IDmpDump* ptr = null;
			this.m_bDisposed = false;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IDmpDump**), DumpClient.s_pDmpClient, &ptr, (IntPtr)(*(*(long*)DumpClient.s_pDmpClient + 40L)));
			if (num >= 0)
			{
				this.m_pDump = ptr;
			}
			else
			{
				this.m_bDisposed = true;
				Marshal.ThrowExceptionForHR(num);
			}
			IntPtr intPtr = new IntPtr(0);
			this.m_pszFileName = intPtr;
			IntPtr intPtr2 = new IntPtr(0);
			this.m_pszExtraFileName = intPtr2;
			IntPtr intPtr3 = new IntPtr(0);
			this.m_pszDirectory = intPtr3;
			IntPtr intPtr4 = new IntPtr(0);
			this.m_pszImageName = intPtr4;
			IntPtr intPtr5 = new IntPtr(0);
			this.m_pszTitleName = intPtr5;
			IntPtr intPtr6 = new IntPtr(0);
			this.m_pszErrorText = intPtr6;
			IntPtr intPtr7 = new IntPtr(0);
			this.m_pszErrorDetail = intPtr7;
			IntPtr intPtr8 = new IntPtr(0);
			this.m_pszDumpIdentity = intPtr8;
			IntPtr intPtr9 = new IntPtr(0);
			this.m_pszInstanceName = intPtr9;
			IntPtr intPtr10 = new IntPtr(0);
			this.m_pszServiceName = intPtr10;
			IntPtr intPtr11 = new IntPtr(0);
			this.m_pszlogName = intPtr11;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004A4C File Offset: 0x00003E4C
		internal unsafe void DumperDispose([MarshalAs(UnmanagedType.U1)] bool bDummy)
		{
			if (!this.m_bDisposed)
			{
				this.m_bDisposed = true;
				IDmpDump* pDump = this.m_pDump;
				uint num = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), pDump, (IntPtr)(*(*(long*)pDump + 16L)));
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00005608 File Offset: 0x00004A08
		protected void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.~Dumper();
			}
			else
			{
				base.Finalize();
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000056E0 File Offset: 0x00004AE0
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000037 RID: 55
		private unsafe IDmpDump* m_pDump;

		// Token: 0x04000038 RID: 56
		private bool m_bDisposed;

		// Token: 0x04000039 RID: 57
		private IntPtr m_pszFileName;

		// Token: 0x0400003A RID: 58
		private IntPtr m_pszExtraFileName;

		// Token: 0x0400003B RID: 59
		private IntPtr m_pszDirectory;

		// Token: 0x0400003C RID: 60
		private IntPtr m_pszImageName;

		// Token: 0x0400003D RID: 61
		private IntPtr m_pszTitleName;

		// Token: 0x0400003E RID: 62
		private IntPtr m_pszErrorText;

		// Token: 0x0400003F RID: 63
		private IntPtr m_pszErrorDetail;

		// Token: 0x04000040 RID: 64
		private IntPtr m_pszDumpIdentity;

		// Token: 0x04000041 RID: 65
		private IntPtr m_pszInstanceName;

		// Token: 0x04000042 RID: 66
		private IntPtr m_pszServiceName;

		// Token: 0x04000043 RID: 67
		private IntPtr m_pszlogName;
	}
}
