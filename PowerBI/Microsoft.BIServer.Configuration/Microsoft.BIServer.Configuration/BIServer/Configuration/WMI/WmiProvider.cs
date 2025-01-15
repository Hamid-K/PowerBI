using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.Win32;

namespace Microsoft.BIServer.Configuration.WMI
{
	// Token: 0x02000033 RID: 51
	public sealed class WmiProvider
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001AF RID: 431 RVA: 0x000069DA File Offset: 0x00004BDA
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x000069E2 File Offset: 0x00004BE2
		public string MofFile
		{
			get
			{
				return this._mofFile;
			}
			set
			{
				this.VerifyFullPath(value);
				this._mofFile = value;
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000069F4 File Offset: 0x00004BF4
		public WmiProvider(string mofFile)
		{
			if (string.IsNullOrEmpty(mofFile))
			{
				throw new ArgumentNullException("mofFile");
			}
			Logger.Info("Attempting to create WMI provider, MOF file '{0}'", new object[] { mofFile });
			this.VerifyFullPath(mofFile);
			this._mofFile = mofFile;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00006A48 File Offset: 0x00004C48
		[CLSCompliant(false)]
		public SecurityDescriptorControlFlags GetFlagsFromDescriptor(string descriptor, ref IntPtr sdCur)
		{
			int num = 0;
			NativeMethods.ConvertStringSDtoSD(descriptor, out sdCur, out num);
			ushort num2;
			uint num3;
			if (!NativeMethods.GetSecurityDescriptorControl(sdCur, out num2, out num3))
			{
				throw new Win32Exception("Could not get security description control");
			}
			return (SecurityDescriptorControlFlags)num2;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006A7C File Offset: 0x00004C7C
		public void SetSecurityDescriptor(string namespacePath, string securityDescriptor)
		{
			Logger.Trace("Attempting to set security descriptor {0}, namespace path {1}", new object[] { securityDescriptor, namespacePath });
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			int num = 0;
			try
			{
				IntPtr zero = IntPtr.Zero;
				string text = string.Empty;
				text = this.GetSecurityDescriptior(namespacePath);
				SecurityDescriptorControlFlags flagsFromDescriptor = this.GetFlagsFromDescriptor(text, ref zero);
				if (SecurityDescriptorControlFlags.SE_DACL_PROTECTED == (flagsFromDescriptor & SecurityDescriptorControlFlags.SE_DACL_PROTECTED))
				{
					Logger.Info("No need to protect the WMI Namespace: '{0}'", new object[] { namespacePath });
				}
				else if (!NativeMethods.SetSecurityDescriptorControl(zero, SecurityDescriptorControlFlags.SE_DACL_PROTECTED, SecurityDescriptorControlFlags.SE_DACL_PROTECTED))
				{
					throw new WmiException(string.Format("Failed on SetSecurityDescriptorControl API with Error {0}", Marshal.GetLastWin32Error()));
				}
				string text2 = securityDescriptor.Substring(0, securityDescriptor.IndexOf("(", StringComparison.OrdinalIgnoreCase));
				if (string.IsNullOrEmpty(text2))
				{
					text2 = text.Substring(0, text.IndexOf("(", StringComparison.OrdinalIgnoreCase));
					securityDescriptor = string.Format("{0}{1}", text2, securityDescriptor);
				}
				NativeMethods.ConvertStringSDtoSD(securityDescriptor, out intPtr2, out num);
				byte[] array = new byte[num];
				Marshal.Copy(intPtr2, array, 0, num);
				ManagementClass managementClass = new ManagementClass(namespacePath + ":__SystemSecurity");
				ManagementBaseObject methodParameters = managementClass.GetMethodParameters("SetSD");
				methodParameters["SD"] = array;
				ManagementBaseObject managementBaseObject = managementClass.InvokeMethod("SetSD", methodParameters, null);
				if ((uint)managementBaseObject["ReturnValue"] != 0U)
				{
					throw new WmiException(managementBaseObject["ReturnValue"].ToString());
				}
			}
			finally
			{
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
					intPtr2 = IntPtr.Zero;
				}
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
					intPtr = IntPtr.Zero;
				}
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006C38 File Offset: 0x00004E38
		public void SetSecurityDescriptor(string namespacePath, string sddlString, bool overwriteSecurityDescriptor)
		{
			Logger.Trace("Attempting to set security descriptor {0}, namespace path {1}", new object[] { sddlString, namespacePath });
			if (string.IsNullOrEmpty(sddlString) && overwriteSecurityDescriptor)
			{
				throw new WmiException("sddl must be provided when overwriting security descriptors");
			}
			if (string.IsNullOrEmpty(sddlString) && !overwriteSecurityDescriptor)
			{
				Logger.Info("SDDL is NULL/empty and overwriteSecurityDescriptor is false, so nothing to do, return.", Array.Empty<object>());
				return;
			}
			Logger.Info("Setting WMI Namespace '{0}' with SDDL {1}: {2}", new object[]
			{
				namespacePath,
				overwriteSecurityDescriptor ? "(overwritting)" : string.Empty,
				sddlString
			});
			IntPtr zero = IntPtr.Zero;
			string text = string.Empty;
			text = this.GetSecurityDescriptior(namespacePath);
			if (overwriteSecurityDescriptor)
			{
				SecurityDescriptorControlFlags flagsFromDescriptor = this.GetFlagsFromDescriptor(text, ref zero);
				if (SecurityDescriptorControlFlags.SE_DACL_PROTECTED == (flagsFromDescriptor & SecurityDescriptorControlFlags.SE_DACL_PROTECTED))
				{
					Logger.Info("No need to protect the WMI Namespace: '{0}'", new object[] { namespacePath });
				}
				else
				{
					try
					{
						NativeMethods.SetSecurityDescriptorControl(zero, SecurityDescriptorControlFlags.SE_DACL_PROTECTED, SecurityDescriptorControlFlags.SE_DACL_PROTECTED);
					}
					catch (Exception ex)
					{
						Logger.Info("Failed to turn off the DACL inheritance on namespace: '{0}'", new object[] { namespacePath });
						throw ex;
					}
				}
				text = sddlString;
			}
			else
			{
				Logger.Info("Security on WMI namespace '{0}' before appending: {1}", new object[] { namespacePath, text });
				text = this.AppendSecurityDescriptor(sddlString, text);
			}
			this.SetSecurityDescriptor(namespacePath, text);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00006D64 File Offset: 0x00004F64
		public string GetSecurityDescriptior(string namespacePath)
		{
			Logger.Trace("Attempting to get security descriptor for namespace path {0}", new object[] { namespacePath });
			string text = string.Empty;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			int num = 0;
			string text2;
			try
			{
				ManagementBaseObject managementBaseObject = new ManagementClass(namespacePath + ":__SystemSecurity").InvokeMethod("GetSD", null, null);
				if ((uint)managementBaseObject["ReturnValue"] != 0U)
				{
					throw new WmiException("sddl must be provided when overwriting security descriptors");
				}
				NativeMethods.ConvertSDtoStringSD((byte[])managementBaseObject["SD"], out intPtr, out num);
				text = Marshal.PtrToStringAuto(intPtr);
				Logger.Trace("Returning security descriptor {0}", new object[] { text });
				text2 = text;
			}
			finally
			{
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
					intPtr2 = IntPtr.Zero;
				}
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
					intPtr = IntPtr.Zero;
				}
			}
			return text2;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00006E50 File Offset: 0x00005050
		public static bool IsInstanceNameValidForNamespaceName(string instanceName)
		{
			if (string.IsNullOrEmpty(instanceName))
			{
				throw new ArgumentNullException("instanceName");
			}
			return !instanceName.StartsWith("_", StringComparison.OrdinalIgnoreCase) && 0 > instanceName.IndexOf("#", StringComparison.OrdinalIgnoreCase) && 0 > instanceName.IndexOf("$", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00006EA4 File Offset: 0x000050A4
		public void InstallMof()
		{
			Logger.Info("Attempting to install MOF file", Array.Empty<object>());
			string text = null;
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\WBEM");
			if (registryKey == null)
			{
				throw new WmiException("no registry WmiWbemKey set");
			}
			using (registryKey)
			{
				text = (string)registryKey.GetValue("Installation Directory");
				registryKey.Close();
			}
			text = Environment.ExpandEnvironmentVariables(text);
			text = Path.Combine(text, "mofcomp.exe");
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("\"{0}\"", this._mofFile);
			stringBuilder.ToString();
			ProcessStartInfo processStartInfo = new ProcessStartInfo(text, stringBuilder.ToString());
			processStartInfo.UseShellExecute = false;
			processStartInfo.RedirectStandardError = true;
			processStartInfo.RedirectStandardOutput = true;
			Logger.Info(string.Format("Running: {0} {1}", text, stringBuilder.ToString()), Array.Empty<object>());
			using (Process process = Process.Start(processStartInfo))
			{
				process.OutputDataReceived += this.OutputHandler;
				process.ErrorDataReceived += this.OutputHandler;
				process.BeginOutputReadLine();
				process.BeginErrorReadLine();
				process.WaitForExit();
				int exitCode = process.ExitCode;
				if (exitCode != 0)
				{
					Logger.Info("Compile operation for mof file {0} failed. Exit code {1}", new object[] { this._mofFile, exitCode });
					switch (exitCode)
					{
					case 1:
						throw new WmiException("bad MOF compiler");
					case 2:
						throw new WmiException("bad MOF compile command");
					case 3:
						throw new WmiException("bad MOF file");
					default:
						throw new Win32Exception(exitCode);
					}
				}
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007050 File Offset: 0x00005250
		public void UninstallMof(string namespaceName)
		{
			Logger.Info("Attempting to uninstall MOF file for namespace {0}", new object[] { namespaceName });
			if (!string.IsNullOrEmpty(namespaceName))
			{
				this.RemoveWmiNamespaceWithParents(namespaceName);
			}
			this.RemoveAutoRecoverMofSetting(this._mofFile);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007081 File Offset: 0x00005281
		private void VerifyFullPath(string filePath)
		{
			if (!Path.IsPathRooted(filePath))
			{
				throw new ArgumentException("[{0}] is not a full path");
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007098 File Offset: 0x00005298
		private bool IsNamespaceEmpty(string wmiPath)
		{
			bool flag;
			try
			{
				flag = new ManagementClass(new ManagementScope(wmiPath), new ManagementPath("__namespace"), null).GetInstances().Count == 0;
			}
			catch (ManagementException ex)
			{
				Logger.Info(string.Format("Management exception: {0} encountered.", ex.ErrorCode.ToString()), Array.Empty<object>());
				throw ex;
			}
			return flag;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007108 File Offset: 0x00005308
		private void RemoveWmiNamespaceWithParents(string wmiPath)
		{
			int num = wmiPath.LastIndexOf("\\", StringComparison.OrdinalIgnoreCase);
			if (num > 0)
			{
				string text = wmiPath.Substring(0, num);
				string text2 = wmiPath.Substring(num + 1, wmiPath.Length - num - 1);
				this.RemoveWmiNamespace(text, text2);
				if (this.IsNamespaceEmpty(text))
				{
					Logger.Info("Namespace {0} is empty and is being removed.", new object[] { text });
					this.RemoveWmiNamespaceWithParents(text);
				}
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00007170 File Offset: 0x00005370
		private void RemoveWmiNamespace(string wmiPath, string namespaceName)
		{
			string text = string.Format("__Namespace=\"{0}\"", namespaceName);
			ManagementObject managementObject = null;
			try
			{
				managementObject = new ManagementObject(wmiPath, text, new ObjectGetOptions());
				string className = managementObject.ClassPath.ClassName;
			}
			catch (ManagementException ex)
			{
				Logger.Info("Management exception: {0} encountered.", new object[] { ex.ErrorCode.ToString() });
				if (ex.ErrorCode == ManagementStatus.NotFound)
				{
					Logger.Info("Ignoring this exception since there was an attempt to remove an non-existent wmi namespace", Array.Empty<object>());
					return;
				}
				throw ex;
			}
			if (managementObject != null)
			{
				try
				{
					managementObject.Delete();
					Logger.Info(string.Format("Namespace {0} was removed.", text), Array.Empty<object>());
				}
				catch (ManagementException ex2)
				{
					Logger.Info(string.Format("Management exception: {0} encountered.", ex2.ErrorCode.ToString()), Array.Empty<object>());
					throw ex2;
				}
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00007258 File Offset: 0x00005458
		private static ConnectionOptions WmiConnectionOptions
		{
			get
			{
				if (WmiProvider.ConnOptions == null)
				{
					WmiProvider.ConnOptions = new ConnectionOptions();
					WmiProvider.ConnOptions.Authentication = AuthenticationLevel.PacketPrivacy;
					WmiProvider.ConnOptions.Timeout = WmiProvider.WmiTimeout;
				}
				return WmiProvider.ConnOptions;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000728C File Offset: 0x0000548C
		internal static bool IsWellKnownLocalServer(string server)
		{
			return string.Compare(server, "localhost", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(server, "(local)", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(server, ".", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(server, Environment.MachineName, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(server, "127.0.0.1", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(server, "::1", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(server, "[::1]", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000730C File Offset: 0x0000550C
		internal static void TestMachineConnection(string machineName)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "\\\\{0}\\root", machineName);
			try
			{
				new ManagementScope(text, WmiProvider.WmiConnectionOptions).Connect();
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode == -2147023174)
				{
					throw new WmiException(string.Format("Unable to connect to machine name {0}", machineName), ex);
				}
				throw new WmiException("WMI COM Exceptoin", ex);
			}
			catch (Exception ex2)
			{
				throw new WmiException("Exception", ex2);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007394 File Offset: 0x00005594
		internal static IList<string> GetInstanceNames(string machineName)
		{
			List<string> list = new List<string>();
			try
			{
				ManagementScope managementScope = new ManagementScope(string.Format(CultureInfo.InvariantCulture, WmiProvider.ReportServerPattern, machineName), WmiProvider.WmiConnectionOptions);
				managementScope.Connect();
				ManagementPath managementPath = new ManagementPath("__NAMESPACE");
				ManagementClass managementClass = new ManagementClass(managementScope, managementPath, WmiProvider.WmiGetOptions);
				managementClass.Get();
				foreach (ManagementBaseObject managementBaseObject in managementClass.GetInstances())
				{
					string text = WmiProvider.UnEscapeInstanceName((string)((ManagementObject)managementBaseObject)["Name"]);
					list.Add(text);
				}
			}
			catch (Exception ex)
			{
				throw new WmiException("Unable To Connect to machine name " + machineName, ex);
			}
			return list;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007460 File Offset: 0x00005660
		internal static string EscapeInstanceName(string checkName, string machineName)
		{
			try
			{
				ManagementScope managementScope = new ManagementScope(string.Format(CultureInfo.InvariantCulture, WmiProvider.ReportServerPattern, machineName), WmiProvider.WmiConnectionOptions);
				managementScope.Connect();
				ManagementPath managementPath = new ManagementPath("__NAMESPACE");
				ManagementClass managementClass = new ManagementClass(managementScope, managementPath, WmiProvider.WmiGetOptions);
				managementClass.Get();
				foreach (ManagementBaseObject managementBaseObject in managementClass.GetInstances())
				{
					string text = (string)((ManagementObject)managementBaseObject)["Name"];
					string text2 = WmiProvider.UnEscapeInstanceName(text);
					if (text2.Equals(checkName, StringComparison.OrdinalIgnoreCase))
					{
						if (text2.Equals(text, StringComparison.OrdinalIgnoreCase))
						{
							return checkName;
						}
						break;
					}
				}
			}
			catch
			{
			}
			string text3 = new Regex("_").Replace(checkName, "_5f");
			text3 = new Regex("\\$").Replace(text3, "_24");
			text3 = new Regex("\\#").Replace(text3, "_23");
			return "RS_" + text3;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007580 File Offset: 0x00005780
		internal static string UnEscapeInstanceName(string escapedInstanceName)
		{
			string text = escapedInstanceName;
			if (escapedInstanceName.StartsWith("RS_", StringComparison.OrdinalIgnoreCase))
			{
				text = escapedInstanceName.Remove(0, 3);
				text = new Regex("_24").Replace(text, "$");
				text = new Regex("_23").Replace(text, "#");
				text = new Regex("_5f").Replace(text, "_");
			}
			return text;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000075E9 File Offset: 0x000057E9
		public static string EscapeInstanceName(string instanceName)
		{
			return WmiProvider.EscapeInstanceName(instanceName, Environment.MachineName);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000075F6 File Offset: 0x000057F6
		public static string RootWMINamespace(string instanceName)
		{
			return "\\\\.\\root\\Microsoft\\SqlServer\\ReportServer\\" + WmiProvider.EscapeInstanceName(instanceName);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00007608 File Offset: 0x00005808
		internal static List<RSWmiInstance> InstalledInstances(string machineName)
		{
			if (machineName == null || machineName.Length == 0)
			{
				machineName = ".";
			}
			WmiProvider.TestMachineConnection(machineName);
			IList<string> instanceNames = WmiProvider.GetInstanceNames(machineName);
			List<RSWmiInstance> list = new List<RSWmiInstance>();
			if (instanceNames.Count > 0)
			{
				ManagementScope managementScope = null;
				ConnectionOptions connectionOptions = new ConnectionOptions();
				connectionOptions.Authentication = AuthenticationLevel.PacketPrivacy;
				connectionOptions.Timeout = WmiProvider.WmiTimeout;
				for (int i = 0; i < instanceNames.Count; i++)
				{
					string text = WmiProvider.EscapeInstanceName(instanceNames[i], machineName);
					string text2 = string.Format(CultureInfo.InvariantCulture, WmiProvider.ReportServiceInstancePattern, machineName, text, "v" + 13.ToString(CultureInfo.InvariantCulture));
					try
					{
						managementScope = new ManagementScope(text2, connectionOptions);
						managementScope.Connect();
						break;
					}
					catch (Exception ex)
					{
						if (i == instanceNames.Count - 1)
						{
							throw new WmiException("No Report Server available", ex);
						}
					}
				}
				ObjectGetOptions objectGetOptions = new ObjectGetOptions();
				objectGetOptions.Timeout = WmiProvider.WmiTimeout;
				ManagementPath managementPath = new ManagementPath("MSReportServer_Instance");
				ManagementClass managementClass = new ManagementClass(managementScope, managementPath, objectGetOptions);
				try
				{
					managementClass.Get();
				}
				catch (Exception ex2)
				{
					throw new WmiException("Exception getting server class", ex2);
				}
				foreach (ManagementBaseObject managementBaseObject in managementClass.GetInstances())
				{
					RSWmiInstance rswmiInstance = new RSInstance((ManagementObject)managementBaseObject, machineName);
					list.Add(rswmiInstance);
				}
			}
			return list;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00007790 File Offset: 0x00005990
		public static WmiProvider.ProcessorArchitecture NativeProcessorArchitecture
		{
			get
			{
				NativeMethods.SYSTEM_INFO system_INFO = default(NativeMethods.SYSTEM_INFO);
				NativeMethods.GetNativeSystemInfo(ref system_INFO);
				ushort wProcessorArchitecture = system_INFO.uProcessorInfo.wProcessorArchitecture;
				if (wProcessorArchitecture == 0)
				{
					return WmiProvider.ProcessorArchitecture.x86;
				}
				if (wProcessorArchitecture == 9)
				{
					return WmiProvider.ProcessorArchitecture.x64;
				}
				return WmiProvider.ProcessorArchitecture.Unknown;
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000077C5 File Offset: 0x000059C5
		private void RemoveAutoRecoverMofSetting(string mofFileWithPath)
		{
			if (string.IsNullOrEmpty(mofFileWithPath))
			{
				Logger.Info("No Mof file specified Autorecover MOFs registry is not updated.", Array.Empty<object>());
				return;
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000077DF File Offset: 0x000059DF
		private void OutputHandler(object sender, DataReceivedEventArgs outLine)
		{
			if (outLine.Data != null)
			{
				Logger.Info(outLine.Data, Array.Empty<object>());
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000077FC File Offset: 0x000059FC
		private string AppendSecurityDescriptor(string newSDDL, string oldSDDL)
		{
			uint num = 0U;
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr zero3 = IntPtr.Zero;
			IntPtr zero4 = IntPtr.Zero;
			IntPtr zero5 = IntPtr.Zero;
			string text = null;
			IntPtr zero6 = IntPtr.Zero;
			bool flag = false;
			bool flag2 = false;
			string text2 = oldSDDL.Substring(0, oldSDDL.IndexOf("(", StringComparison.OrdinalIgnoreCase));
			int num2 = newSDDL.IndexOf("(", StringComparison.OrdinalIgnoreCase);
			if (string.IsNullOrEmpty(newSDDL.Substring(0, num2)))
			{
				newSDDL = string.Format(CultureInfo.InvariantCulture, "{0}{1}", text2, newSDDL);
			}
			this.GetFlagsFromDescriptor(oldSDDL, ref zero2);
			if (!NativeMethods.GetSecurityDescriptorDacl(zero2, out flag, out zero4, out flag2) || !flag)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			this.GetFlagsFromDescriptor(newSDDL, ref zero);
			if (!NativeMethods.GetSecurityDescriptorDacl(zero, out flag, out zero3, out flag2) || !flag)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			try
			{
				NativeMethods.GetExplicitEntriesFromAcl(zero3, out num, out zero6);
				if (num > 0U)
				{
					uint num3 = NativeMethods.SetEntriesInAcl(num, zero6, zero4, out zero5);
					if (num3 != 0U)
					{
						throw new Win32Exception((int)num3);
					}
					NativeMethods.SECURITY_DESCRIPTOR security_DESCRIPTOR = (NativeMethods.SECURITY_DESCRIPTOR)Marshal.PtrToStructure(zero2, typeof(NativeMethods.SECURITY_DESCRIPTOR));
					intPtr = Marshal.AllocHGlobal(Marshal.SizeOf<NativeMethods.SECURITY_DESCRIPTOR>(security_DESCRIPTOR));
					Marshal.StructureToPtr<NativeMethods.SECURITY_DESCRIPTOR>(security_DESCRIPTOR, intPtr, true);
					if (!NativeMethods.InitializeSecurityDescriptor(intPtr, 1U))
					{
						throw new Win32Exception(Marshal.GetLastWin32Error());
					}
					if (!NativeMethods.SetSecurityDescriptorDacl(intPtr, true, zero5, false))
					{
						throw new Win32Exception(Marshal.GetLastWin32Error());
					}
					text = NativeMethods.ConvertSDtoStringSD(intPtr);
					num2 = text.IndexOf("(", StringComparison.OrdinalIgnoreCase);
					text = string.Format(CultureInfo.InvariantCulture, "{0}{1}", text2, text.Substring(num2));
				}
				else
				{
					text = oldSDDL;
				}
			}
			finally
			{
				Marshal.FreeHGlobal(zero6);
				Marshal.FreeHGlobal(zero5);
				Marshal.FreeHGlobal(intPtr);
			}
			return text;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000079C8 File Offset: 0x00005BC8
		public static ObjectGetOptions WmiGetOptions
		{
			get
			{
				if (WmiProvider.GetOptions == null)
				{
					WmiProvider.GetOptions = new ObjectGetOptions();
					WmiProvider.GetOptions.Timeout = WmiProvider.WmiTimeout;
				}
				return WmiProvider.GetOptions;
			}
		}

		// Token: 0x04000184 RID: 388
		private const string RegistryWmiRepositoryKey = "SOFTWARE\\Microsoft\\WBEM\\CIMOM";

		// Token: 0x04000185 RID: 389
		private const string RegistryWmiWbemKey = "SOFTWARE\\Microsoft\\WBEM";

		// Token: 0x04000186 RID: 390
		private const string AutoRecoverSetting = "Autorecover MOFs";

		// Token: 0x04000187 RID: 391
		private const string NamespaceObjPath = "__Namespace=\"{0}\"";

		// Token: 0x04000188 RID: 392
		private const string RsRootNamespace = "\\\\.\\root\\Microsoft\\SqlServer\\ReportServer\\";

		// Token: 0x04000189 RID: 393
		internal const string MachinePattern = "\\\\{0}\\root";

		// Token: 0x0400018A RID: 394
		internal static readonly string ReportServerPattern = "\\\\{0}\\root\\Microsoft\\SqlServer\\ReportServer";

		// Token: 0x0400018B RID: 395
		internal static readonly string ReportServiceInstancePattern = WmiProvider.ReportServerPattern + "\\{1}\\{2}";

		// Token: 0x0400018C RID: 396
		internal static readonly string ReportAdminPattern = WmiProvider.ReportServiceInstancePattern + "\\Admin";

		// Token: 0x0400018D RID: 397
		internal static readonly string ReportServiceInstanceV9Pattern = WmiProvider.ReportServerPattern + "\\{2}";

		// Token: 0x0400018E RID: 398
		internal static readonly string ReportAdminV9Pattern = WmiProvider.ReportServiceInstanceV9Pattern + "\\Admin";

		// Token: 0x0400018F RID: 399
		internal const string ReportServerManagementClass = "MSReportServer_ConfigurationSetting";

		// Token: 0x04000190 RID: 400
		internal const string ReportServerInstanceClass = "MSReportServer_Instance";

		// Token: 0x04000191 RID: 401
		private const string PropertyName_Key_InstanceName = "InstanceName";

		// Token: 0x04000192 RID: 402
		private static TimeSpan WmiTimeout = new TimeSpan(0, 0, 10);

		// Token: 0x04000193 RID: 403
		private static ConnectionOptions ConnOptions;

		// Token: 0x04000194 RID: 404
		private static ObjectGetOptions GetOptions;

		// Token: 0x04000195 RID: 405
		private string _mofFile = string.Empty;

		// Token: 0x04000196 RID: 406
		private const string ServerLevelRegKeyName = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Server\\ServerLevels";

		// Token: 0x02000057 RID: 87
		public enum ProcessorArchitecture
		{
			// Token: 0x04000206 RID: 518
			Unknown,
			// Token: 0x04000207 RID: 519
			x86,
			// Token: 0x04000208 RID: 520
			IA64,
			// Token: 0x04000209 RID: 521
			x64
		}

		// Token: 0x02000058 RID: 88
		public enum RegistryView
		{
			// Token: 0x0400020B RID: 523
			ProcessDefault,
			// Token: 0x0400020C RID: 524
			Wow6432,
			// Token: 0x0400020D RID: 525
			Wow6464
		}
	}
}
