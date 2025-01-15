using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Permissions;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000073 RID: 115
	public sealed class AssemblyReferencesHelper : MarshalByRefObject
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x00022D98 File Offset: 0x00020F98
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736AD6E5F9586BAC2D531EABC3ACC666C2F8EC879FA94F8F7B0327D2FF2ED523448F83C3D5C5DD2DFC7BC99C5286B2C125117BF5CBE242B9D41750732B2BDFFE649C6EFB8E5526D526FDD130095ECDB7BF210809C6CDAD8824FAA9AC0310AC3CBA2AA0523567B2DFA7FE250B30FACBD62D4EC99B94AC47C7D3B28F1F6E4C8")]
		public void GetAssemblyReferences(string startPoint, ref StringCollection files)
		{
			byte[] publicKey = Assembly.GetExecutingAssembly().GetName().GetPublicKey();
			this.ResolveReferences(Path.GetDirectoryName(startPoint), Path.GetFileName(startPoint), files, new Hashtable(StringComparer.InvariantCultureIgnoreCase), publicKey, true);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00022DD8 File Offset: 0x00020FD8
		private void ResolveReferences(string dir, string fileName, StringCollection files, Hashtable assemblyNames, byte[] publicKeyToSkip, bool isMainAssembly)
		{
			string text = Path.Combine(dir, fileName);
			if (!File.Exists(text))
			{
				return;
			}
			Assembly assembly;
			try
			{
				assembly = Assembly.LoadFrom(text);
			}
			catch (FileLoadException)
			{
				return;
			}
			catch (BadImageFormatException)
			{
				return;
			}
			if (assemblyNames.Contains(fileName))
			{
				return;
			}
			if (this.AreArraysEqual(publicKeyToSkip, assembly.GetName().GetPublicKey()))
			{
				if (isMainAssembly)
				{
					throw new InvalidOperationException(SR.ClrAssembly_MainAssemblyHasAMOsPublicKey);
				}
				return;
			}
			else
			{
				assemblyNames.Add(fileName, assembly);
				files.Add(text);
				foreach (AssemblyName assemblyName in assembly.GetReferencedAssemblies())
				{
					if (string.Compare("mscorlib", assemblyName.Name, true, CultureInfo.InvariantCulture) != 0)
					{
						this.ResolveReferences(dir, assemblyName.Name + ".dll", files, assemblyNames, publicKeyToSkip, false);
					}
				}
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00022EBC File Offset: 0x000210BC
		private bool AreArraysEqual(byte[] a, byte[] b)
		{
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = a.Length - 1; i >= 0; i--)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}
	}
}
