using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x0200001A RID: 26
	public sealed class PidInfoHelper
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00003514 File Offset: 0x00001714
		public PidInfoHelper(ProductType product)
		{
			this._product = product;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003524 File Offset: 0x00001724
		public PidInfo GetPidInfo(string pid)
		{
			if (string.IsNullOrEmpty(pid))
			{
				return null;
			}
			string text = this.NormalizePid(pid);
			PidInfo pidInfo;
			lock (this)
			{
				if (this.cachedPidString == null || string.Compare(this.cachedPidString, text, StringComparison.OrdinalIgnoreCase) != 0)
				{
					this.cachedPidInfo = this.GeneratePidInfo(text);
					this.cachedPidString = this.NormalizePid(pid);
				}
				pidInfo = this.cachedPidInfo;
			}
			return pidInfo;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000035A4 File Offset: 0x000017A4
		private PidInfo GeneratePidInfo(string pidString)
		{
			if (pidString == "00000-00000-00000-00000-00000")
			{
				return new PidInfo(null, null, (this._product == ProductType.PowerBiReportServer) ? SkuType.PbirsEvaluation : SkuType.SsrsEvaluation);
			}
			if (pidString == "11111-00000-00000-00000-00000")
			{
				return new PidInfo(null, null, (this._product == ProductType.PowerBiReportServer) ? SkuType.PbirsEvaluation : SkuType.SsrsExpress);
			}
			if (pidString == "22222-00000-00000-00000-00000")
			{
				return new PidInfo(null, null, (this._product == ProductType.PowerBiReportServer) ? SkuType.PbirsDeveloper : SkuType.SsrsDeveloper);
			}
			try
			{
				this.ValidateProductKey(pidString, null);
				if (this.m_oDigPID2 != null && this.m_oDigPID2.szPid2 != null && this.m_oDigPID4 != null && this.m_oDigPID4.abCdKey != null && this.m_oDigPID4.szEulaType != null && this.m_oDigPID4.szEditionId != null)
				{
					SkuType skuType = SkuUtils.ParseSqlKeySkuName(this._product, this.m_oDigPID4.szEditionId);
					return (skuType == SkuType.None) ? null : new PidInfo(this.m_oDigPID2.szPid2, this.m_oDigPID4.abCdKey, skuType);
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("Unable to validate pidString " + ex.Message);
			}
			return null;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000036D4 File Offset: 0x000018D4
		private string NormalizePid(string pid)
		{
			pid = pid.ToUpperInvariant();
			if (pid.Contains("-"))
			{
				return pid;
			}
			StringBuilder stringBuilder = new StringBuilder(29);
			while (pid.Length >= 5)
			{
				stringBuilder.Append(pid.Substring(0, 5));
				if (pid.Length > 5)
				{
					stringBuilder.Append('-');
				}
				pid = pid.Substring(5);
			}
			stringBuilder.Append(pid);
			pid = stringBuilder.ToString();
			return pid;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003748 File Offset: 0x00001948
		private bool ValidateProductKey(string sKey, string oemId = null)
		{
			string directoryName = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
			string text = Path.Combine(directoryName, "pidgenx.dll");
			string text2 = Path.Combine(directoryName, (this._product == ProductType.PowerBiReportServer) ? PidInfoHelper.PBIRSPIDMAPS : PidInfoHelper.SSRSPIDMAPS);
			if (!File.Exists(text2))
			{
				throw new FileNotFoundException(text2);
			}
			if (!File.Exists(text))
			{
				throw new FileNotFoundException(text);
			}
			IntPtr intPtr = SkuNativeMethods.LoadLibraryW(text);
			if (IntPtr.Zero == intPtr)
			{
				throw new Exception("Could not find" + text + "; " + Marshal.GetLastWin32Error().ToString());
			}
			IntPtr procAddress = SkuNativeMethods.GetProcAddress(intPtr, "PidGenX");
			if (IntPtr.Zero == procAddress)
			{
				throw new Exception("Could not find function in PidgenX");
			}
			this.m_oDigPID4 = new PidgenXInterop.DIGITALPID4();
			this.m_oDigPID4.dwLength = (uint)Marshal.SizeOf<PidgenXInterop.DIGITALPID4>(this.m_oDigPID4);
			this.m_oDigPID3 = new PidgenXInterop.DIGITALPID3();
			this.m_oDigPID3.dwLength = (uint)Marshal.SizeOf<PidgenXInterop.DIGITALPID3>(this.m_oDigPID3);
			this.m_oDigPID2 = new PidgenXInterop.DIGITALPID2();
			uint num = PidgenXInterop.PidGenX(sKey, text2, "MpcId", oemId, this.m_oDigPID2, this.m_oDigPID3, this.m_oDigPID4);
			if (num == 0U)
			{
				return true;
			}
			this.m_oDigPID4 = null;
			this.m_oDigPID3 = null;
			this.m_oDigPID2 = null;
			if (PidgenXInterop.INVALID_KEY_HR == num)
			{
				throw new Exception("Invalid key");
			}
			if (PidgenXInterop.INVALID_CONFIG_FORMAT_HR == num)
			{
				throw new Exception("Invalid Pigdenx configuration file format");
			}
			throw new Exception("Failed to validate key");
		}

		// Token: 0x0400007A RID: 122
		private static readonly string SSRSPIDMAPS = "PidPrivateConfigObjectMaps.xml";

		// Token: 0x0400007B RID: 123
		private static readonly string PBIRSPIDMAPS = "PbiPidPrivateConfigObjectMaps.xml";

		// Token: 0x0400007C RID: 124
		private PidInfo cachedPidInfo;

		// Token: 0x0400007D RID: 125
		private string cachedPidString;

		// Token: 0x0400007E RID: 126
		private readonly ProductType _product;

		// Token: 0x0400007F RID: 127
		private const int NormalizedPidLength = 29;

		// Token: 0x04000080 RID: 128
		private PidgenXInterop.DIGITALPID4 m_oDigPID4;

		// Token: 0x04000081 RID: 129
		private PidgenXInterop.DIGITALPID3 m_oDigPID3;

		// Token: 0x04000082 RID: 130
		private PidgenXInterop.DIGITALPID2 m_oDigPID2;
	}
}
