using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Mashup.Common;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A12 RID: 6674
	internal class MemoryMappedFile : IDisposable
	{
		// Token: 0x17002AF2 RID: 10994
		// (get) Token: 0x0600A8F0 RID: 43248 RVA: 0x0022F5E4 File Offset: 0x0022D7E4
		public static int PageGranularity
		{
			get
			{
				if (MemoryMappedFile.pageGranularity == 0)
				{
					object obj = MemoryMappedFile.staticSyncRoot;
					lock (obj)
					{
						MemoryMappedFile.pageGranularity = MemoryMappedFile.GetAllocationGranularity();
					}
				}
				return MemoryMappedFile.pageGranularity;
			}
		}

		// Token: 0x0600A8F1 RID: 43249 RVA: 0x0022F634 File Offset: 0x0022D834
		public static long PageAlign(long size)
		{
			if (size == 0L)
			{
				return (long)MemoryMappedFile.PageGranularity;
			}
			return PageHelpers.Align(size, (long)MemoryMappedFile.PageGranularity);
		}

		// Token: 0x0600A8F2 RID: 43250 RVA: 0x0022F64C File Offset: 0x0022D84C
		public static MemoryMappedFile Create(ulong length)
		{
			SafeFileHandle safeFileHandle = new SafeFileHandle(MemoryMappedFile.Handle.INVALID_HANDLE_VALUE, true);
			SafeHandle safeHandle = MemoryMappedFile.CreateFileMapping(safeFileHandle, length);
			if (safeHandle.IsInvalid)
			{
				throw new InvalidOperationException("CreateFileMapping failed.", new Win32Exception());
			}
			return new MemoryMappedFile(safeFileHandle, safeHandle, null);
		}

		// Token: 0x0600A8F3 RID: 43251 RVA: 0x0022F68C File Offset: 0x0022D88C
		public static MemoryMappedFile Open(FileStream fileStream, ulong length)
		{
			SafeFileHandle safeFileHandle = fileStream.SafeFileHandle;
			SafeHandle safeHandle = MemoryMappedFile.CreateFileMapping(safeFileHandle, length);
			if (safeHandle.IsInvalid)
			{
				throw new InvalidOperationException("CreateFileMapping failed.", new Win32Exception());
			}
			return new MemoryMappedFile(safeFileHandle, safeHandle, fileStream);
		}

		// Token: 0x0600A8F4 RID: 43252 RVA: 0x0022F6C6 File Offset: 0x0022D8C6
		protected MemoryMappedFile(SafeFileHandle fileHandle, SafeHandle fileMappingHandle, FileStream fileStream = null)
		{
			this.fileHandle = fileHandle;
			this.fileMappingHandle = fileMappingHandle;
			this.fileStream = fileStream;
		}

		// Token: 0x17002AF3 RID: 10995
		// (get) Token: 0x0600A8F5 RID: 43253 RVA: 0x0022F6E3 File Offset: 0x0022D8E3
		protected SafeHandle FileMappingHandle
		{
			get
			{
				return this.fileMappingHandle;
			}
		}

		// Token: 0x0600A8F6 RID: 43254 RVA: 0x0022F6EB File Offset: 0x0022D8EB
		public MemoryMappedView MapView(ulong offset, uint length)
		{
			return new MemoryMappedView(this.fileMappingHandle, offset, length);
		}

		// Token: 0x0600A8F7 RID: 43255 RVA: 0x0022F6FC File Offset: 0x0022D8FC
		public void Dispose()
		{
			if (this.fileMappingHandle != null)
			{
				this.fileMappingHandle.Dispose();
				this.fileMappingHandle = null;
			}
			if (this.fileHandle != null)
			{
				this.fileHandle.Dispose();
				this.fileHandle = null;
			}
			if (this.fileStream != null)
			{
				this.fileStream.Dispose();
				this.fileStream = null;
			}
		}

		// Token: 0x0600A8F8 RID: 43256 RVA: 0x0022F757 File Offset: 0x0022D957
		protected static SafeHandle CreateFileMapping(SafeFileHandle fileHandle, ulong length)
		{
			return MemoryMappedFile.CreateFileMapping(fileHandle, length, null);
		}

		// Token: 0x0600A8F9 RID: 43257 RVA: 0x0022F764 File Offset: 0x0022D964
		protected static SafeHandle CreateFileMapping(SafeFileHandle fileHandle, ulong length, string name)
		{
			uint num = (uint)(length >> 32);
			uint num2 = (uint)length;
			return MemoryMappedFile.CreateFileMapping(fileHandle, IntPtr.Zero, MemoryMappedFile.FileMappingProtection.PAGE_READWRITE, num, num2, name);
		}

		// Token: 0x0600A8FA RID: 43258 RVA: 0x0022F789 File Offset: 0x0022D989
		protected static SafeHandle OpenFileMapping(string name)
		{
			return MemoryMappedFile.OpenFileMapping(FileMappingAccess.FILE_MAP_ALL_ACCESS, false, name);
		}

		// Token: 0x0600A8FB RID: 43259 RVA: 0x0022F794 File Offset: 0x0022D994
		private static int GetAllocationGranularity()
		{
			MemoryMappedFile.SYSTEM_INFO system_INFO;
			MemoryMappedFile.GetSystemInfo(out system_INFO);
			return (int)system_INFO.AllocationGranularity;
		}

		// Token: 0x0600A8FC RID: 43260
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern SafeFileHandle CreateFileMapping(SafeFileHandle hFile, IntPtr lpFileMappingAttributes, MemoryMappedFile.FileMappingProtection flProtect, uint dwMaximumSizeHigh, uint dwMaximumSizeLow, [MarshalAs(UnmanagedType.LPWStr)] string lpName);

		// Token: 0x0600A8FD RID: 43261
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern SafeFileHandle OpenFileMapping(FileMappingAccess dwDesiredAccess, bool bInheritHandle, [MarshalAs(UnmanagedType.LPWStr)] string lpName);

		// Token: 0x0600A8FE RID: 43262
		[DllImport("kernel32.dll")]
		private static extern void GetSystemInfo(out MemoryMappedFile.SYSTEM_INFO Info);

		// Token: 0x040057DE RID: 22494
		private static readonly object staticSyncRoot = new object();

		// Token: 0x040057DF RID: 22495
		private static int pageGranularity;

		// Token: 0x040057E0 RID: 22496
		private SafeFileHandle fileHandle;

		// Token: 0x040057E1 RID: 22497
		private SafeHandle fileMappingHandle;

		// Token: 0x040057E2 RID: 22498
		private FileStream fileStream;

		// Token: 0x02001A13 RID: 6675
		protected static class Handle
		{
			// Token: 0x040057E3 RID: 22499
			public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
		}

		// Token: 0x02001A14 RID: 6676
		[Flags]
		private enum FileMappingProtection : uint
		{
			// Token: 0x040057E5 RID: 22501
			PAGE_READONLY = 2U,
			// Token: 0x040057E6 RID: 22502
			PAGE_READWRITE = 4U
		}

		// Token: 0x02001A15 RID: 6677
		[StructLayout(LayoutKind.Explicit)]
		private struct SYSTEM_INFO_UNION
		{
			// Token: 0x040057E7 RID: 22503
			[FieldOffset(0)]
			public uint OemId;

			// Token: 0x040057E8 RID: 22504
			[FieldOffset(0)]
			public ushort ProcessorArchitecture;

			// Token: 0x040057E9 RID: 22505
			[FieldOffset(2)]
			public ushort Reserved;
		}

		// Token: 0x02001A16 RID: 6678
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct SYSTEM_INFO
		{
			// Token: 0x040057EA RID: 22506
			public MemoryMappedFile.SYSTEM_INFO_UNION CpuInfo;

			// Token: 0x040057EB RID: 22507
			public uint PageSize;

			// Token: 0x040057EC RID: 22508
			public IntPtr MinimumApplicationAddress;

			// Token: 0x040057ED RID: 22509
			public IntPtr MaximumApplicationAddress;

			// Token: 0x040057EE RID: 22510
			public IntPtr ActiveProcessorMask;

			// Token: 0x040057EF RID: 22511
			public uint NumberOfProcessors;

			// Token: 0x040057F0 RID: 22512
			public uint ProcessorType;

			// Token: 0x040057F1 RID: 22513
			public uint AllocationGranularity;

			// Token: 0x040057F2 RID: 22514
			public ushort ProcessorLevel;

			// Token: 0x040057F3 RID: 22515
			public ushort ProcessorRevision;
		}
	}
}
