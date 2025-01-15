using System;
using System.Globalization;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001F0 RID: 496
	public sealed class ExtendedFileVersionInfo : IDisposable
	{
		// Token: 0x06000D0A RID: 3338 RVA: 0x0002DC30 File Offset: 0x0002BE30
		[CanBeNull]
		public static ExtendedFileVersionInfo TryCreate(string fileName)
		{
			ExtendedFileVersionInfo extendedFileVersionInfo;
			try
			{
				extendedFileVersionInfo = new ExtendedFileVersionInfo(fileName);
			}
			catch (ExtendedFileVersionInfoException)
			{
				extendedFileVersionInfo = null;
			}
			return extendedFileVersionInfo;
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0002DC5C File Offset: 0x0002BE5C
		private unsafe ExtendedFileVersionInfo([NotNull] string fileName)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(fileName, "filename");
			this.m_filename = fileName;
			int fileVersionInfoSize = ExtendedFileVersionInfo.NativeMethods.GetFileVersionInfoSize(this.m_filename, out this.m_handle);
			if (fileVersionInfoSize == 0)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "GetFileVersionInfoSize({0}) failed: {1}", new object[] { this.m_filename, lastWin32Error });
				throw new ExtendedFileVersionInfoException(this.m_filename, "GetFileVersionInfoSize", lastWin32Error);
			}
			this.m_buffer = Marshal.AllocCoTaskMem(fileVersionInfoSize);
			if (ExtendedFileVersionInfo.NativeMethods.GetFileVersionInfo(this.m_filename, this.m_handle, fileVersionInfoSize, this.m_buffer) == 0)
			{
				int lastWin32Error2 = Marshal.GetLastWin32Error();
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "GetFileVersionInfo({0}) failed: {1}", new object[] { this.m_filename, lastWin32Error2 });
				throw new ExtendedFileVersionInfoException(this.m_filename, "GetFileVersionInfo", lastWin32Error2);
			}
			short* ptr = null;
			uint num = 0U;
			if (ExtendedFileVersionInfo.NativeMethods.VerQueryValue(this.m_buffer, "\\VarFileInfo\\Translation", out ptr, out num) == 0)
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "VerQueryValue({0}, \\VarFileInfo\\Translation) failed", new object[] { this.m_filename });
				throw new ExtendedFileVersionInfoException(this.m_filename, "VerQueryValue on \\VarFileInfo\\Translation");
			}
			this.m_langid = (int)(*ptr);
			this.m_codepage = (int)ptr[1];
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0002DD9C File Offset: 0x0002BF9C
		~ExtendedFileVersionInfo()
		{
			this.Dispose(false);
		}

		// Token: 0x170001D3 RID: 467
		private string this[string name]
		{
			get
			{
				string text;
				if (!this.TryGetVersionString(name, out text))
				{
					throw new ExtendedFileVersionInfoException(this.m_filename, "VerQueryValue on " + this.GetSpvString(name));
				}
				return text;
			}
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0002DE04 File Offset: 0x0002C004
		public static string GetVersionString([NotNull] string fileName, [NotNull] string name)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(fileName, "fileName");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			string text;
			using (ExtendedFileVersionInfo extendedFileVersionInfo = new ExtendedFileVersionInfo(fileName))
			{
				text = extendedFileVersionInfo[name];
			}
			return text;
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0002DE54 File Offset: 0x0002C054
		public static bool TryGetVersionString([NotNull] string fileName, [NotNull] string name, out string result)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(fileName, "fileName");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			bool flag;
			try
			{
				using (ExtendedFileVersionInfo extendedFileVersionInfo = new ExtendedFileVersionInfo(fileName))
				{
					flag = extendedFileVersionInfo.TryGetVersionString(name, out result);
				}
			}
			catch (ExtendedFileVersionInfoException)
			{
				result = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0002DEB8 File Offset: 0x0002C0B8
		public bool TryGetVersionString([NotNull] string name, out string result)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			string spvString = this.GetSpvString(name);
			IntPtr intPtr;
			uint num;
			if (ExtendedFileVersionInfo.NativeMethods.VerQueryValue(this.m_buffer, spvString, out intPtr, out num) == 0)
			{
				result = null;
				return false;
			}
			if (num > 0U)
			{
				num -= 1U;
			}
			result = Marshal.PtrToStringAnsi(intPtr, (int)num);
			return true;
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0002DF01 File Offset: 0x0002C101
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0002DF10 File Offset: 0x0002C110
		private void Dispose(bool disposing)
		{
			if (this.m_buffer.ToPointer() != null)
			{
				Marshal.FreeCoTaskMem(this.m_buffer);
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0002DF2E File Offset: 0x0002C12E
		private string GetSpvString(string name)
		{
			return string.Format(CultureInfo.InvariantCulture, "\\StringFileInfo\\{0:X4}{1:X4}\\{2}", new object[] { this.m_langid, this.m_codepage, name });
		}

		// Token: 0x04000503 RID: 1283
		private string m_filename;

		// Token: 0x04000504 RID: 1284
		private int m_handle;

		// Token: 0x04000505 RID: 1285
		private IntPtr m_buffer;

		// Token: 0x04000506 RID: 1286
		private int m_langid;

		// Token: 0x04000507 RID: 1287
		private int m_codepage;

		// Token: 0x0200069B RID: 1691
		private static class NativeMethods
		{
			// Token: 0x06002DFE RID: 11774
			[DllImport("version.dll", BestFitMapping = false, SetLastError = true)]
			public static extern int GetFileVersionInfoSize(string sFileName, out int handle);

			// Token: 0x06002DFF RID: 11775
			[DllImport("version.dll", BestFitMapping = false, SetLastError = true)]
			public static extern int GetFileVersionInfo(string sFileName, int handle, int size, IntPtr buffer);

			// Token: 0x06002E00 RID: 11776
			[DllImport("version.dll", BestFitMapping = false)]
			public static extern int VerQueryValue(IntPtr pBlock, string pSubBlock, out IntPtr pValue, out uint len);

			// Token: 0x06002E01 RID: 11777
			[DllImport("version.dll", BestFitMapping = false)]
			public unsafe static extern int VerQueryValue(IntPtr pBlock, string pSubBlock, out short* pValue, out uint len);
		}
	}
}
