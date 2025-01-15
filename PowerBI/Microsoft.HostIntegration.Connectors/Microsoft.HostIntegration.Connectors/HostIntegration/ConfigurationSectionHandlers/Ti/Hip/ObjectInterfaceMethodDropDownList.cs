using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000570 RID: 1392
	public class ObjectInterfaceMethodDropDownList : StringConverter
	{
		// Token: 0x06002F3F RID: 12095 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x000A218C File Offset: 0x000A038C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			ConfigurationElementCollection hipObjects = ((ResolutionEntry)context.Instance).GetHipObjects();
			string assemblyPath = ((ResolutionEntry)context.Instance).ResolutionEntryService.AssemblyPath;
			ArrayList arrayList = new ArrayList();
			foreach (object obj in hipObjects)
			{
				HipObject hipObject = (HipObject)obj;
				try
				{
					arrayList = this.GetHipInterfaceMethods(assemblyPath, hipObject.ImplementingAssembly, hipObject.MetaDataInterface);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Method Property Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			return new TypeConverter.StandardValuesCollection(arrayList);
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x000A2244 File Offset: 0x000A0444
		public ArrayList GetHipInterfaceMethods(string assemblyPath, string ImplementingAssembly, string interfaceFullName)
		{
			string text = assemblyPath + "\\" + ImplementingAssembly;
			ArrayList arrayList = new ArrayList();
			FileInfo fileInfo = new FileInfo(text);
			if (!File.Exists(text))
			{
				throw new Exception("File does not exists: " + text);
			}
			string text2 = fileInfo.Extension.ToLowerInvariant();
			if (text2 != null && text2 == ".dll")
			{
				try
				{
					Assembly assembly = Assembly.LoadFrom(fileInfo.FullName);
					foreach (Type type in assembly.GetTypes())
					{
						string fullName = type.FullName;
						assembly.GetType(type.FullName);
						if (type.IsPublic && !type.IsAbstract && type.IsClass)
						{
							Type @interface = type.GetInterface(interfaceFullName, true);
							if (@interface != null)
							{
								foreach (MethodInfo methodInfo in @interface.GetMethods())
								{
									arrayList.Add(methodInfo.Name);
								}
							}
						}
					}
					return arrayList;
				}
				catch (Exception ex)
				{
					throw new Exception("Invalid HIP Meta Data Assembly. " + ex.Message);
				}
			}
			throw new Exception("The selected file is not a *.dll file.");
		}
	}
}
