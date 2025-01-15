using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.HostIntegration.StrictResources.TIGlobals;
using Microsoft.Win32;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000712 RID: 1810
	[Guid("D4E76CD9-C812-4cdb-94E3-DE4F14D20321")]
	public class RemoteEnvironmentClassFactory : IRemoteEnvironmentClassFactory
	{
		// Token: 0x06003952 RID: 14674 RVA: 0x000BF710 File Offset: 0x000BD910
		static RemoteEnvironmentClassFactory()
		{
			AssemblyName assemblyName = new AssemblyName(Assembly.GetExecutingAssembly().ToString());
			bool flag = assemblyName.Name == "Microsoft.HostIntegration.TI.Globals";
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.ELMTransport";
			}
			RemoteEnvironmentClassFactory.ELMTransportFullName = "Microsoft.HostIntegration.TI.ELMTransport, " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.TRMTransport";
			}
			RemoteEnvironmentClassFactory.TRMTransportFullName = "Microsoft.HostIntegration.TI.TRMTransport, " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.IMSConnectTransport";
			}
			RemoteEnvironmentClassFactory.IMSConnectTransportFullName = "Microsoft.HostIntegration.TI.IMSConnectTransport, " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.DPCTransport";
			}
			RemoteEnvironmentClassFactory.DPCTransportFullName = "Microsoft.HostIntegration.TI.DPCTransport, " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.SNATransport";
			}
			RemoteEnvironmentClassFactory.SNATransportFullName = "Microsoft.HostIntegration.TI.SNATransport, " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.TCPTransport";
			}
			RemoteEnvironmentClassFactory.TCPTransportFullName = "Microsoft.HostIntegration.TI.TCPTransport, " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.HTTPTransport";
			}
			RemoteEnvironmentClassFactory.HTTPTransportFullName = "Microsoft.HostIntegration.TI.HTTPTransport, " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.AggregateConverter";
			}
			RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterTypeName + ", " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.DPCAggregateConverter";
			}
			RemoteEnvironmentClassFactory.SystemiDPCAggregateConverterFullName = "Microsoft.HostIntegration.TI.DPCAggregateConverter, " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.AggregateConverterJson";
			}
			RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName = "Microsoft.HostIntegration.TI.AggregateConverterJson, " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.DPCAggregateConverterJson";
			}
			RemoteEnvironmentClassFactory.SystemiDPCAggregateConverterJsonFullName = "Microsoft.HostIntegration.TI.DPCAggregateConverterJson, " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.GenericLinkStateMachine";
			}
			RemoteEnvironmentClassFactory.GenericLinkStateMachineFullName = "Microsoft.HostIntegration.TI.GenericLinkStateMachine, " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.GenericUserDataStateMachine";
			}
			RemoteEnvironmentClassFactory.GenericUserDataStateMachineFullName = "Microsoft.HostIntegration.TI.GenericUserDataStateMachine, " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.IMSConnectStateMachine";
			}
			RemoteEnvironmentClassFactory.IMSConnectStateMachineFullName = "Microsoft.HostIntegration.TI.IMSConnectStateMachine, " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.DPCStateMachine";
			}
			RemoteEnvironmentClassFactory.DPCStateMachineFullName = "Microsoft.HostIntegration.TI.DPCStateMachine, " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.TCPStateMachine";
			}
			RemoteEnvironmentClassFactory.TCPStateMachineFullName = "Microsoft.HostIntegration.TI.TCPStateMachine, " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.Common.SystemIPrimitiveConverter";
			}
			RemoteEnvironmentClassFactory.SystemiPrimitiveConverterFullName = RemoteEnvironmentClassFactory.SystemiPrimitiveConverterTypeName + ", " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.Common.BasePrimitiveConverter";
			}
			RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterTypeName + ", " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.ELMFlowControl";
			}
			RemoteEnvironmentClassFactory.ELMFlowControlFullName = "Microsoft.HostIntegration.TI.ELMFlowControl, " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.TRMFlowControl";
			}
			RemoteEnvironmentClassFactory.TRMFlowControlFullName = "Microsoft.HostIntegration.TI.TRMFlowControl, " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.DPLFlowControl";
			}
			RemoteEnvironmentClassFactory.DPLFlowControlFullName = "Microsoft.HostIntegration.TI.DPLFlowControl, " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.EndPointFlowControl";
			}
			RemoteEnvironmentClassFactory.EndPointFlowControlFullName = "Microsoft.HostIntegration.TI.EndPointFlowControl, " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.UserDataFlowControl";
			}
			RemoteEnvironmentClassFactory.UserDataFlowControlFullName = "Microsoft.HostIntegration.TI.UserDataFlowControl, " + assemblyName.FullName;
			if (flag)
			{
				assemblyName.Name = "Microsoft.HostIntegration.TI.HTTPFlowControl";
			}
			RemoteEnvironmentClassFactory.HTTPFlowControlFullName = "Microsoft.HostIntegration.TI.HTTPFlowControl, " + assemblyName.FullName;
			RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("Software\\Microsoft\\Cedar\\Vendors");
			if (registryKey != null && registryKey.ValueCount > 0)
			{
				RemoteEnvironmentClassFactory.vendorIDs = new Guid[registryKey.ValueCount - 1];
				RemoteEnvironmentClassFactory.vendorNames = new string[registryKey.ValueCount - 1];
				RemoteEnvironmentClassFactory.vendorRECFactoryNames = new string[registryKey.ValueCount - 1];
				RemoteEnvironmentClassFactory.vendorRECFactoryInstances = new IRemoteEnvironmentClassFactory[registryKey.ValueCount - 1];
				RemoteEnvironmentClassFactory.vendorIndexs = new int[registryKey.ValueCount - 1];
				int num = 0;
				foreach (string text in registryKey.GetValueNames())
				{
					Guid guid = new Guid(text.ToUpperInvariant());
					if (guid != RemoteEnvironmentClassFactory.ourVendorID)
					{
						RemoteEnvironmentClassFactory.vendorIDs[num] = guid;
						RemoteEnvironmentClassFactory.vendorNames[num] = registryKey.GetValue(text) as string;
						RegistryKey registryKey2 = registryKey.OpenSubKey(RemoteEnvironmentClassFactory.vendorNames[num], RegistryKeyPermissionCheck.ReadSubTree);
						RemoteEnvironmentClassFactory.vendorIndexs[num] = (int)registryKey2.GetValue("VendorIndex");
						RemoteEnvironmentClassFactory.vendorRECFactoryNames[num] = registryKey2.GetValue("RECFactory") as string;
						Type type = Type.GetType(RemoteEnvironmentClassFactory.vendorRECFactoryNames[num], false);
						if (type == null)
						{
							throw new Exception(SR.UnableToCreateRECFactory(RemoteEnvironmentClassFactory.vendorRECFactoryNames[num]));
						}
						object obj = Activator.CreateInstance(type);
						RemoteEnvironmentClassFactory.vendorRECFactoryInstances[num] = obj as IRemoteEnvironmentClassFactory;
						num++;
					}
				}
				return;
			}
			RemoteEnvironmentClassFactory.vendorIDs = null;
			RemoteEnvironmentClassFactory.vendorNames = null;
			RemoteEnvironmentClassFactory.vendorRECFactoryNames = null;
			RemoteEnvironmentClassFactory.vendorRECFactoryInstances = null;
		}

		// Token: 0x06003953 RID: 14675 RVA: 0x000BFD9B File Offset: 0x000BDF9B
		public static Guid GetVendorId()
		{
			return RemoteEnvironmentClassFactory.ourVendorID;
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x000BFDA4 File Offset: 0x000BDFA4
		private object[] GetSortedEnumValues(Type enumType, Type enumSortType)
		{
			Array values = Enum.GetValues(enumType);
			string[] names = Enum.GetNames(enumSortType);
			if (values.Length != names.Length)
			{
				throw new Exception("Enum sizes are different.");
			}
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < names.Length; i++)
			{
				for (int j = 0; j < values.Length; j++)
				{
					if (Enum.GetName(enumType, values.GetValue(j)) == names[i])
					{
						arrayList.Add(values.GetValue(j));
						break;
					}
				}
			}
			if (arrayList.Count != values.Length)
			{
				throw new Exception("Failed to get sorted value array of an enum.");
			}
			return arrayList.ToArray();
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06003955 RID: 14677 RVA: 0x000BFE46 File Offset: 0x000BE046
		public RemoteEnvironmentClass[] RemoteEnvironmentClasses
		{
			get
			{
				return this.remoteEnvironmentClasses;
			}
		}

		// Token: 0x06003956 RID: 14678 RVA: 0x000BFE50 File Offset: 0x000BE050
		public RemoteEnvironmentClassFactory()
		{
			ArrayList arrayList = null;
			object[] sortedEnumValues = this.GetSortedEnumValues(typeof(RemoteEnvironmentTypes), typeof(RemoteEnvironmentTypesOrder));
			int num = sortedEnumValues.Length;
			if (RemoteEnvironmentClassFactory.vendorNames != null)
			{
				arrayList = new ArrayList();
				for (int i = 0; i < RemoteEnvironmentClassFactory.vendorNames.Length; i++)
				{
					RemoteEnvironmentClass[] array = RemoteEnvironmentClassFactory.vendorRECFactoryInstances[i].RemoteEnvironmentClasses;
					if (array != null)
					{
						num += array.Length;
						arrayList.Add(array);
					}
				}
			}
			this.remoteEnvironmentClasses = new RemoteEnvironmentClass[num];
			num = 0;
			for (int i = 0; i < sortedEnumValues.Length; i++)
			{
				this.remoteEnvironmentClasses[num] = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass((RemoteEnvironmentTypes)sortedEnumValues[i]);
				num++;
			}
			if (arrayList != null)
			{
				for (int i = 0; i < arrayList.Count; i++)
				{
					RemoteEnvironmentClass[] array2 = (RemoteEnvironmentClass[])arrayList[i];
					for (int j = 0; j < array2.Length; j++)
					{
						this.remoteEnvironmentClasses[num] = array2[j];
						num++;
					}
				}
			}
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x000BFF40 File Offset: 0x000BE140
		public void GetVendorInfoList(out Guid[] VendorIDs, out string[] VendorNames, out RemoteEnvironmentClass[] RemoteEnvironmentClassList, out bool HasManagedREC)
		{
			if (RemoteEnvironmentClassFactory.vendorNames != null)
			{
				VendorNames = new string[RemoteEnvironmentClassFactory.vendorNames.Length + 1];
				VendorNames[0] = RemoteEnvironmentClassFactory.ourVendorName;
				VendorIDs = new Guid[RemoteEnvironmentClassFactory.vendorNames.Length + 1];
				VendorIDs[0] = RemoteEnvironmentClassFactory.ourVendorID;
				for (int i = 0; i < RemoteEnvironmentClassFactory.vendorNames.Length; i++)
				{
					VendorNames[i + 1] = RemoteEnvironmentClassFactory.vendorNames[i];
					VendorIDs[i + 1] = RemoteEnvironmentClassFactory.vendorIDs[i];
				}
			}
			else
			{
				VendorNames = new string[1];
				VendorNames[0] = RemoteEnvironmentClassFactory.ourVendorName;
				VendorIDs = new Guid[1];
				VendorIDs[0] = RemoteEnvironmentClassFactory.ourVendorID;
			}
			HasManagedREC = false;
			if (this.remoteEnvironmentClasses.Length != 0)
			{
				RemoteEnvironmentClassList = new RemoteEnvironmentClass[this.remoteEnvironmentClasses.Length];
				int i = 0;
				foreach (RemoteEnvironmentClass remoteEnvironmentClass in this.remoteEnvironmentClasses)
				{
					RemoteEnvironmentClassList[i] = remoteEnvironmentClass;
					if (remoteEnvironmentClass.IsSupportedByManagedRuntime)
					{
						HasManagedREC = true;
					}
					i++;
				}
				return;
			}
			RemoteEnvironmentClassList = null;
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x000C0036 File Offset: 0x000BE236
		public static string GetTransportTypeName(Transport Transport)
		{
			if (Transport == Transport.LU62)
			{
				return "LU6.2";
			}
			if (Transport == Transport.TCP)
			{
				return "TCP";
			}
			if (Transport == Transport.Http)
			{
				return "HTTP";
			}
			throw new ArgumentException("GetTransportTypeName");
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x000C0068 File Offset: 0x000BE268
		public string GetTransportNameByType(long TransportType)
		{
			Transport transport = (Transport)TransportType;
			if (transport == Transport.LU62)
			{
				return "LU6.2";
			}
			if (transport == Transport.TCP)
			{
				return "TCP";
			}
			if (transport == Transport.Http)
			{
				return "HTTP";
			}
			throw new ArgumentException("GetTransportNameByType");
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x000C00A8 File Offset: 0x000BE2A8
		public static string GetFlowControlFullName(string FlowControlClsid)
		{
			string text = null;
			if (FlowControlClsid != null)
			{
				if (!(FlowControlClsid == "C1085505-1748-4B8F-B76B-D9E837EFEC39"))
				{
					if (!(FlowControlClsid == "68E6A551-D4C4-43D3-A835-E5592884C4CF"))
					{
						if (!(FlowControlClsid == "349C11FB-1F25-49FF-9504-0BA4CB21BBFA"))
						{
							if (!(FlowControlClsid == "E2BECFEF-659D-4648-8100-3060F5A81B05"))
							{
								if (!(FlowControlClsid == "E8FD3C5B-46F3-48FB-9EBF-52BE5CC897C5"))
								{
									if (FlowControlClsid == "739A64BE-857B-4ABF-843A-1F0F87770EB8")
									{
										text = RemoteEnvironmentClassFactory.HTTPFlowControlFullName;
									}
								}
								else
								{
									text = RemoteEnvironmentClassFactory.UserDataFlowControlFullName;
								}
							}
							else
							{
								text = RemoteEnvironmentClassFactory.EndPointFlowControlFullName;
							}
						}
						else
						{
							text = RemoteEnvironmentClassFactory.DPLFlowControlFullName;
						}
					}
					else
					{
						text = RemoteEnvironmentClassFactory.TRMFlowControlFullName;
					}
				}
				else
				{
					text = RemoteEnvironmentClassFactory.ELMFlowControlFullName;
				}
			}
			return text;
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x0600395B RID: 14683 RVA: 0x000C013C File Offset: 0x000BE33C
		private static HostEnvironmentInfo[] MakeHostEnvironmentInfoList
		{
			get
			{
				HostEnvironmentInfo[] array = new HostEnvironmentInfo[5];
				array[0] = default(HostEnvironmentInfo);
				array[0].HostEnvironmentName = RemoteEnvironmentClassFactory.SystemzHostEnvironmentCICS;
				array[0].HostEnvironmentPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
				array[0].VendorName = RemoteEnvironmentClassFactory.ourVendorName;
				array[0].VendorID = RemoteEnvironmentClassFactory.ourVendorID;
				array[0].PrimitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
				array[0].HostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
				array[1] = default(HostEnvironmentInfo);
				array[1].HostEnvironmentName = RemoteEnvironmentClassFactory.SystemzHostEnvironmentIMS;
				array[1].HostEnvironmentPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
				array[1].VendorName = RemoteEnvironmentClassFactory.ourVendorName;
				array[1].VendorID = RemoteEnvironmentClassFactory.ourVendorID;
				array[1].PrimitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
				array[1].HostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
				array[2] = default(HostEnvironmentInfo);
				array[2].HostEnvironmentName = RemoteEnvironmentClassFactory.SystemzPlatformName;
				array[2].HostEnvironmentPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
				array[2].VendorName = RemoteEnvironmentClassFactory.ourVendorName;
				array[2].VendorID = RemoteEnvironmentClassFactory.ourVendorID;
				array[2].PrimitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
				array[2].HostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
				array[3] = default(HostEnvironmentInfo);
				array[3].HostEnvironmentName = RemoteEnvironmentClassFactory.SystemiHostEnvironmentDPC;
				array[3].HostEnvironmentPlatformName = RemoteEnvironmentClassFactory.SystemiPlatformName;
				array[3].VendorName = RemoteEnvironmentClassFactory.ourVendorName;
				array[3].VendorID = RemoteEnvironmentClassFactory.ourVendorID;
				array[3].PrimitiveConverterClassId = RemoteEnvironmentClassFactory.SystemiPrimitiveConverterClassId;
				array[3].HostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemiPrimitiveConverterClassId);
				array[4] = default(HostEnvironmentInfo);
				array[4].HostEnvironmentName = RemoteEnvironmentClassFactory.SystemiPlatformName;
				array[4].HostEnvironmentPlatformName = RemoteEnvironmentClassFactory.SystemiPlatformName;
				array[4].VendorName = RemoteEnvironmentClassFactory.ourVendorName;
				array[4].VendorID = RemoteEnvironmentClassFactory.ourVendorID;
				array[4].PrimitiveConverterClassId = RemoteEnvironmentClassFactory.SystemiPrimitiveConverterClassId;
				array[4].HostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemiPrimitiveConverterClassId);
				return array;
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x0600395C RID: 14684 RVA: 0x000C03A7 File Offset: 0x000BE5A7
		public HostEnvironmentInfo[] HostEnvironmentInfoList
		{
			get
			{
				return RemoteEnvironmentClassFactory.GetHostEnvironmentInfoList;
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x0600395D RID: 14685 RVA: 0x000C03B0 File Offset: 0x000BE5B0
		public static HostEnvironmentInfo[] GetHostEnvironmentInfoList
		{
			get
			{
				ArrayList arrayList = null;
				int num = 0;
				int i;
				if (RemoteEnvironmentClassFactory.vendorNames != null)
				{
					arrayList = new ArrayList();
					for (i = 0; i < RemoteEnvironmentClassFactory.vendorNames.Length; i++)
					{
						HostEnvironmentInfo[] hostEnvironmentInfoList = RemoteEnvironmentClassFactory.vendorRECFactoryInstances[i].HostEnvironmentInfoList;
						if (hostEnvironmentInfoList != null)
						{
							num += hostEnvironmentInfoList.Length;
							arrayList.Add(hostEnvironmentInfoList);
						}
					}
				}
				HostEnvironmentInfo[] makeHostEnvironmentInfoList = RemoteEnvironmentClassFactory.MakeHostEnvironmentInfoList;
				HostEnvironmentInfo[] array = new HostEnvironmentInfo[num + makeHostEnvironmentInfoList.Length];
				for (i = 0; i < makeHostEnvironmentInfoList.Length; i++)
				{
					array[i] = makeHostEnvironmentInfoList[i];
				}
				i = makeHostEnvironmentInfoList.Length;
				for (int j = 0; j < num; j++)
				{
					for (int k = 0; k < ((HostEnvironmentInfo[])arrayList[j]).Length; k++)
					{
						array[i] = ((HostEnvironmentInfo[])arrayList[j])[k];
					}
					i++;
				}
				return array;
			}
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x000C0484 File Offset: 0x000BE684
		public static HostLanguage[] GetHostPlatformLanguages(string PrimitiveConverterClassId)
		{
			HostLanguage[] array;
			if (PrimitiveConverterClassId == RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId)
			{
				array = new HostLanguage[]
				{
					new HostLanguage()
				};
				array[0].Description = RemoteEnvironmentClassFactory.SystemzPlatformName;
				HostLanguage hostLanguage = array[0];
				hostLanguage.Description += " ";
				HostLanguage hostLanguage2 = array[0];
				hostLanguage2.Description += RemoteEnvironmentClassFactory.SystemzProgrammingLanguageCOBOL;
				array[0].DevImport = RemoteEnvironmentClassFactory.SystemzCOBOLImporter;
				array[0].ExporterGuid = RemoteEnvironmentClassFactory.SystemzCOBOLExporter;
				array[0].LanguageExtension = RemoteEnvironmentClassFactory.SystemzCOBOLDevLangExtension;
				array[0].DisplayName = RemoteEnvironmentClassFactory.SystemzProgrammingLanguageCOBOL;
				array[0].FileExt = RemoteEnvironmentClassFactory.SystemzCOBOLFileExtenstions;
				array[0].ImporterAssembly = "Microsoft.HostIntegration.TIDesigner.COBOLImporter.dll";
				array[0].ImporterClass = "Microsoft.HostIntegration.TIDesigner.ISourceImpMSCOBOL";
				array[0].Name = array[0].Description;
				array[0].GUID = "{4192D190-A6FA-42d8-A7C9-F28F1E8D7C61}";
			}
			else
			{
				if (!(PrimitiveConverterClassId == RemoteEnvironmentClassFactory.SystemiPrimitiveConverterClassId))
				{
					throw new ArgumentException("GetHostPlatformLanguages");
				}
				array = new HostLanguage[2];
				array[0] = new HostLanguage();
				array[0].Description = RemoteEnvironmentClassFactory.SystemiPlatformName;
				HostLanguage hostLanguage3 = array[0];
				hostLanguage3.Description += " ";
				HostLanguage hostLanguage4 = array[0];
				hostLanguage4.Description += RemoteEnvironmentClassFactory.SystemiProgrammingLanguageRPG;
				array[0].DevImport = RemoteEnvironmentClassFactory.SystemiRPGImporter;
				array[0].ExporterGuid = RemoteEnvironmentClassFactory.SystemiRPGExporter;
				array[0].LanguageExtension = RemoteEnvironmentClassFactory.SystemiRPGDevLangExtension;
				array[0].DisplayName = RemoteEnvironmentClassFactory.SystemiProgrammingLanguageRPG;
				array[0].FileExt = RemoteEnvironmentClassFactory.SystemiRPGFileExtenstions;
				array[0].ImporterAssembly = "Microsoft.HostIntegration.TIDesigner.RPGImporter.dll";
				array[0].ImporterClass = "Microsoft.HostIntegration.TIDesigner.ISourceImpMSRPG";
				array[0].Name = array[0].Description;
				array[0].GUID = "{9627653F-6720-43E0-A06C-9F60ECCDF018}";
				array[1] = new HostLanguage();
				array[1].Description = RemoteEnvironmentClassFactory.SystemiPlatformName;
				HostLanguage hostLanguage5 = array[1];
				hostLanguage5.Description += " ";
				HostLanguage hostLanguage6 = array[1];
				hostLanguage6.Description += RemoteEnvironmentClassFactory.SystemiProgrammingLanguageCOBOL;
				array[1].DevImport = RemoteEnvironmentClassFactory.SystemiCOBOLImporter;
				array[1].ExporterGuid = RemoteEnvironmentClassFactory.SystemzCOBOLExporter;
				array[1].LanguageExtension = RemoteEnvironmentClassFactory.SystemiCOBOLDevLangExtension;
				array[1].DisplayName = RemoteEnvironmentClassFactory.SystemiProgrammingLanguageCOBOL;
				array[1].FileExt = RemoteEnvironmentClassFactory.SystemiCOBOLFileExtenstions;
				array[1].ImporterAssembly = "Microsoft.HostIntegration.TIDesigner.AS400COBOLImporter.dll";
				array[1].ImporterClass = "Microsoft.HostIntegration.TIDesigner.ISourceImpMSAS400COBOL";
				array[1].Name = array[1].Description;
				array[1].GUID = "{9627653F-6720-43E0-C06C-9F60ECCDF018}";
			}
			return array;
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x000C070C File Offset: 0x000BE90C
		public static string GetPrimitiveConverterFullNameFromClassId(string PrimitiveConverterClassId)
		{
			string text = PrimitiveConverterClassId.ToUpperInvariant();
			string text2;
			if (text == RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId)
			{
				text2 = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName;
			}
			else
			{
				if (!(text == RemoteEnvironmentClassFactory.SystemiPrimitiveConverterClassId))
				{
					throw new ArgumentException("GetPrimitiveConverterFullNameFromClassId");
				}
				text2 = RemoteEnvironmentClassFactory.SystemiPrimitiveConverterFullName;
			}
			return text2;
		}

		// Token: 0x06003960 RID: 14688 RVA: 0x000C0758 File Offset: 0x000BE958
		public static string GetPrimitiveConverterTypeNameFromClassId(string PrimitiveConverterClassId)
		{
			string text = PrimitiveConverterClassId.ToUpperInvariant();
			string text2;
			if (text == RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId)
			{
				text2 = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterTypeName;
			}
			else
			{
				if (!(text == RemoteEnvironmentClassFactory.SystemiPrimitiveConverterClassId))
				{
					throw new ArgumentException("GetPrimitiveConverterTypeNameFromClassId");
				}
				text2 = RemoteEnvironmentClassFactory.SystemiPrimitiveConverterTypeName;
			}
			return text2;
		}

		// Token: 0x06003961 RID: 14689 RVA: 0x000C07A4 File Offset: 0x000BE9A4
		public static string GetTransportFullNameFromOldClassId(string TransportClassId)
		{
			string text = TransportClassId.ToUpperInvariant();
			string text2;
			if (text == "A0C6421E-42D2-4B97-93F9-15CE7D9A3292")
			{
				text2 = RemoteEnvironmentClassFactory.TCPTransportFullName;
			}
			else if (text == "29965231-AF84-42AF-BF80-C566B53253F5")
			{
				text2 = RemoteEnvironmentClassFactory.SNATransportFullName;
			}
			else
			{
				if (!(text == "1B15335A-161A-45AA-883F-EF2F1D6753E8"))
				{
					throw new ArgumentException("GetPrimitiveConverterFullNameFromClassId");
				}
				text2 = RemoteEnvironmentClassFactory.HTTPTransportFullName;
			}
			return text2;
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x000C0804 File Offset: 0x000BEA04
		public static string GetTransportFullNameFromType(Transport TransportType)
		{
			string text = null;
			if (TransportType == Transport.TCP)
			{
				text = RemoteEnvironmentClassFactory.TCPTransportFullName;
			}
			else if (TransportType == Transport.LU62)
			{
				text = RemoteEnvironmentClassFactory.SNATransportFullName;
			}
			else if (TransportType == Transport.Http)
			{
				text = RemoteEnvironmentClassFactory.HTTPTransportFullName;
			}
			return text;
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x000C083C File Offset: 0x000BEA3C
		public static string GetRemoteEnvironmentClassIdPrimitiveConverter(string PrimitiveConverterClassId)
		{
			string text = PrimitiveConverterClassId.ToUpperInvariant();
			string text2;
			if (text == RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId)
			{
				text2 = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.ElmLink).RemoteEnvironmentClassID;
			}
			else
			{
				if (!(text == RemoteEnvironmentClassFactory.SystemiPrimitiveConverterClassId))
				{
					throw new ArgumentException("GetRemoteEnvironmentClassIdPrimitiveConverter");
				}
				text2 = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.DistributedProgramCall).RemoteEnvironmentClassID;
			}
			return text2;
		}

		// Token: 0x06003964 RID: 14692 RVA: 0x000C089C File Offset: 0x000BEA9C
		public static BaseRemoteEnvironment GetRemoteEnvironmentFromPrimitiveAndTransport(string PrimitiveConverterClassId, Transport enumTransport)
		{
			string text = PrimitiveConverterClassId.ToUpperInvariant();
			BaseRemoteEnvironment baseRemoteEnvironment;
			if (enumTransport != Transport.LU62)
			{
				if (enumTransport != Transport.TCP)
				{
					if (enumTransport != Transport.Http)
					{
						throw new ArgumentException("GetRemoteEnvironmentFromPrimitiveAndTransport");
					}
					HttpBaseRemoteEnvironment httpBaseRemoteEnvironment = new HttpBaseRemoteEnvironment();
					if (text == RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId)
					{
						RemoteEnvironmentClass remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.HttpUserData);
						httpBaseRemoteEnvironment.RemoteEnvironmentClass = remoteEnvironmentClass;
						httpBaseRemoteEnvironment.TimeOut = 0;
						httpBaseRemoteEnvironment.CodePage = CultureInfo.CurrentCulture.TextInfo.EBCDICCodePage;
					}
					else
					{
						if (!(text == RemoteEnvironmentClassFactory.SystemiPrimitiveConverterClassId))
						{
							throw new ArgumentException("GetRemoteEnvironmentFromPrimitiveAndTransport");
						}
						RemoteEnvironmentClass remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.HttpUserData);
						remoteEnvironmentClass.PrimitiveConverterClassId = RemoteEnvironmentClassFactory.SystemiPrimitiveConverterClassId;
						remoteEnvironmentClass.PrimitiveConverterName = RemoteEnvironmentClassFactory.SystemiPrimitiveConverterName;
						remoteEnvironmentClass.PrimitiveConverterFullName = RemoteEnvironmentClassFactory.SystemiPrimitiveConverterFullName;
						httpBaseRemoteEnvironment.RemoteEnvironmentClass = remoteEnvironmentClass;
						httpBaseRemoteEnvironment.TimeOut = 0;
						httpBaseRemoteEnvironment.CodePage = CultureInfo.CurrentCulture.TextInfo.EBCDICCodePage;
					}
					baseRemoteEnvironment = httpBaseRemoteEnvironment;
				}
				else
				{
					TcpRemoteEnvironment tcpRemoteEnvironment = new TcpRemoteEnvironment();
					if (text == RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId)
					{
						RemoteEnvironmentClass remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.SystemzSocketsUserData);
						tcpRemoteEnvironment.RemoteEnvironmentClass = remoteEnvironmentClass;
						tcpRemoteEnvironment.TimeOut = 0;
						tcpRemoteEnvironment.CodePage = CultureInfo.CurrentCulture.TextInfo.EBCDICCodePage;
					}
					else
					{
						if (!(text == RemoteEnvironmentClassFactory.SystemiPrimitiveConverterClassId))
						{
							throw new ArgumentException("GetRemoteEnvironmentFromPrimitiveAndTransport");
						}
						RemoteEnvironmentClass remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.SystemiSocketsUserData);
						remoteEnvironmentClass.PrimitiveConverterClassId = RemoteEnvironmentClassFactory.SystemiPrimitiveConverterClassId;
						remoteEnvironmentClass.PrimitiveConverterName = RemoteEnvironmentClassFactory.SystemiPrimitiveConverterName;
						remoteEnvironmentClass.PrimitiveConverterFullName = RemoteEnvironmentClassFactory.SystemiPrimitiveConverterFullName;
						tcpRemoteEnvironment.RemoteEnvironmentClass = remoteEnvironmentClass;
						tcpRemoteEnvironment.TimeOut = 0;
						tcpRemoteEnvironment.CodePage = CultureInfo.CurrentCulture.TextInfo.EBCDICCodePage;
					}
					baseRemoteEnvironment = tcpRemoteEnvironment;
				}
			}
			else
			{
				SnaBaseRemoteEnvironment snaBaseRemoteEnvironment = new SnaBaseRemoteEnvironment();
				if (text == RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId)
				{
					RemoteEnvironmentClass remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.SnaUserData);
					snaBaseRemoteEnvironment.RemoteEnvironmentClass = remoteEnvironmentClass;
					snaBaseRemoteEnvironment.TimeOut = 0;
					snaBaseRemoteEnvironment.CodePage = CultureInfo.CurrentCulture.TextInfo.EBCDICCodePage;
				}
				else
				{
					if (!(text == RemoteEnvironmentClassFactory.SystemiPrimitiveConverterClassId))
					{
						throw new ArgumentException("GetRemoteEnvironmentFromPrimitiveAndTransport");
					}
					RemoteEnvironmentClass remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.SnaUserData);
					remoteEnvironmentClass.PrimitiveConverterClassId = RemoteEnvironmentClassFactory.SystemiPrimitiveConverterClassId;
					remoteEnvironmentClass.PrimitiveConverterName = RemoteEnvironmentClassFactory.SystemiPrimitiveConverterName;
					remoteEnvironmentClass.PrimitiveConverterFullName = RemoteEnvironmentClassFactory.SystemiPrimitiveConverterFullName;
					snaBaseRemoteEnvironment.RemoteEnvironmentClass = remoteEnvironmentClass;
					snaBaseRemoteEnvironment.TimeOut = 0;
					snaBaseRemoteEnvironment.CodePage = CultureInfo.CurrentCulture.TextInfo.EBCDICCodePage;
				}
				baseRemoteEnvironment = snaBaseRemoteEnvironment;
			}
			return baseRemoteEnvironment;
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x000C0AFF File Offset: 0x000BECFF
		public static PrimitiveConverterImplementor[] AvailablePrimitiveConverters()
		{
			return new PrimitiveConverterImplementor[]
			{
				new PrimitiveConverterImplementor(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId, RemoteEnvironmentClassFactory.SystemzPlatformName, RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName),
				new PrimitiveConverterImplementor(RemoteEnvironmentClassFactory.SystemiPrimitiveConverterClassId, RemoteEnvironmentClassFactory.SystemiPlatformName, RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName)
			};
		}

		// Token: 0x06003966 RID: 14694 RVA: 0x000C0B38 File Offset: 0x000BED38
		public RemoteEnvironmentClass GetManagedRemoteEnvironmentClass(string RemoteEnvironmentClassID, out bool WasClassIdConverted)
		{
			string text;
			if (RemoteEnvironmentClassID[0] != '{')
			{
				text = "{" + RemoteEnvironmentClassID.ToUpperInvariant() + "}";
			}
			else
			{
				text = RemoteEnvironmentClassID.ToUpperInvariant();
			}
			WasClassIdConverted = false;
			RemoteEnvironmentClass remoteEnvironmentClass = this.GetRemoteEnvironmentClass(text);
			if (!remoteEnvironmentClass.IsSupportedByManagedRuntime)
			{
				WasClassIdConverted = true;
				RemoteEnvironmentTypes remoteEnvironmentType = remoteEnvironmentClass.remoteEnvironmentType;
				if (remoteEnvironmentType <= RemoteEnvironmentTypes.LegacyCicsLink)
				{
					if (remoteEnvironmentType != RemoteEnvironmentTypes.LegacyImsLu62)
					{
						if (remoteEnvironmentType != RemoteEnvironmentTypes.LegacyCicsLu62)
						{
							if (remoteEnvironmentType == RemoteEnvironmentTypes.LegacyCicsLink)
							{
								remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.SnaLink);
							}
						}
						else
						{
							remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.SnaUserData);
						}
					}
					else
					{
						remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.ImsLu62);
					}
				}
				else if (remoteEnvironmentType <= RemoteEnvironmentTypes.LegacyImsConnect)
				{
					if (remoteEnvironmentType != RemoteEnvironmentTypes.LegacyTcp)
					{
						if (remoteEnvironmentType == RemoteEnvironmentTypes.LegacyImsConnect)
						{
							remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.ImsConnect);
						}
					}
					else
					{
						remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.TrmLink);
					}
				}
				else if (remoteEnvironmentType != RemoteEnvironmentTypes.ImsImplicit)
				{
					if (remoteEnvironmentType == RemoteEnvironmentTypes.ImsExplicit)
					{
						remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.ImsConnect);
					}
				}
				else
				{
					remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.ImsConnect);
				}
			}
			return remoteEnvironmentClass;
		}

		// Token: 0x06003967 RID: 14695 RVA: 0x000C0C24 File Offset: 0x000BEE24
		public RemoteEnvironmentClass GetRemoteEnvironmentClass(string RemoteEnvironmentClassID)
		{
			RemoteEnvironmentClass remoteEnvironmentClass = null;
			string text;
			if (RemoteEnvironmentClassID[0] != '{')
			{
				text = "{" + RemoteEnvironmentClassID.ToUpperInvariant() + "}";
			}
			else
			{
				text = RemoteEnvironmentClassID.ToUpperInvariant();
			}
			if (this.remoteEnvironmentClasses.Length < 1)
			{
				throw new Exception(SR.UnknownRemoteEnvironmentClassId("GetRemoteEnvironmentClass"));
			}
			foreach (RemoteEnvironmentClass remoteEnvironmentClass2 in this.remoteEnvironmentClasses)
			{
				if (remoteEnvironmentClass2.RemoteEnvironmentClassID.ToUpperInvariant() == text)
				{
					remoteEnvironmentClass = remoteEnvironmentClass2;
					break;
				}
			}
			return remoteEnvironmentClass;
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x000C0CAC File Offset: 0x000BEEAC
		public IRemoteEnvironmentClass GetRemoteEnvironmentClassByID(string RemoteEnvironmentClassID)
		{
			return this.GetRemoteEnvironmentClass(RemoteEnvironmentClassID);
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x000C0CB8 File Offset: 0x000BEEB8
		public IRemoteEnvironmentClass GetRemoteEnvironmentClassByType(long RemoteEnvironmentType)
		{
			IRemoteEnvironmentClass remoteEnvironmentClass = null;
			try
			{
				for (int i = 0; i < this.remoteEnvironmentClasses.Length; i++)
				{
					if ((long)this.remoteEnvironmentClasses[i].RemoteEnvironmentType == RemoteEnvironmentType)
					{
						remoteEnvironmentClass = this.remoteEnvironmentClasses[i];
					}
				}
			}
			catch (ArgumentException)
			{
			}
			return remoteEnvironmentClass;
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x000C0D0C File Offset: 0x000BEF0C
		public static bool MakeRemoteEnvironmentClassFromREType(RemoteEnvironmentTypes REType, out RemoteEnvironmentClass REClass)
		{
			bool flag = true;
			REClass = null;
			try
			{
				REClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(REType);
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x000C0D40 File Offset: 0x000BEF40
		public static RemoteEnvironmentClass MakeRemoteEnvironmentClass(RemoteEnvironmentTypes RemoteEnvironmentType)
		{
			string text = RemoteEnvironmentClassFactory.ourVendorName;
			Guid guid = RemoteEnvironmentClassFactory.ourVendorID;
			if (RemoteEnvironmentType <= RemoteEnvironmentTypes.ImsLu62)
			{
				if (RemoteEnvironmentType <= RemoteEnvironmentTypes.LegacyImsConnect)
				{
					if (RemoteEnvironmentType <= RemoteEnvironmentTypes.LegacyCicsLu62)
					{
						if (RemoteEnvironmentType == RemoteEnvironmentTypes.LegacyImsLu62)
						{
							RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
							remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName;
							remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName;
							remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName;
							remoteEnvironmentClass.transportFullName = RemoteEnvironmentClassFactory.SNATransportFullName;
							remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterName;
							remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterName;
							remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
							remoteEnvironmentClass.devDefinesGuid = new Guid("{DA59D503-58EA-11D0-8C31-00C04FC2E0BB}");
							remoteEnvironmentClass.name = "Legacy IMSLU62 User Data";
							remoteEnvironmentClass.transportName = "SNA Transport";
							remoteEnvironmentClass.programmingModelName = "Legacy IMSLU62 User Data";
							remoteEnvironmentClass.StateMachineFullName = "Unknown";
							remoteEnvironmentClass.StateMachineName = "Unknown";
							remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemzHostEnvironmentIMS;
							remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
							remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemz;
							remoteEnvironmentClass.vendorName = text;
							remoteEnvironmentClass.vendorID = guid;
							remoteEnvironmentClass.programmingModel = ProgrammingModel.UserData;
							remoteEnvironmentClass.hostEnvironment = HostEnvironment.IMS;
							remoteEnvironmentClass.transport = Transport.LU62;
							remoteEnvironmentClass.remoteEnvironmentClassID = "{6F5169CA-6835-11D0-8C39-00C04FC2F9BC}";
							remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.LegacyImsLu62;
							remoteEnvironmentClass.remoteEnvironmentVersion = "REC3";
							remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
							remoteEnvironmentClass.propertyPages = new RECPropertyPage[4];
							remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
							remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
							remoteEnvironmentClass.propertyPages[0].Order = 2;
							remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
							remoteEnvironmentClass.propertyPages[1].Name = "General Page";
							remoteEnvironmentClass.propertyPages[1].Order = 0;
							remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
							remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
							remoteEnvironmentClass.propertyPages[2].Order = 3;
							remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{85D02C42-44A8-11D0-BF45-00AA00BDD5D7}");
							remoteEnvironmentClass.propertyPages[3].Name = "Binding Page";
							remoteEnvironmentClass.propertyPages[3].Order = 1;
							remoteEnvironmentClass.isSupportedByManagedRuntime = false;
							remoteEnvironmentClass.isForNew = false;
							remoteEnvironmentClass.persistentConnectionsSupported = false;
							remoteEnvironmentClass.transactionsAreSupported = true;
							return remoteEnvironmentClass;
						}
						if (RemoteEnvironmentType == RemoteEnvironmentTypes.LegacyCicsLu62)
						{
							RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
							remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName;
							remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName;
							remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName;
							remoteEnvironmentClass.transportFullName = RemoteEnvironmentClassFactory.SNATransportFullName;
							remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterName;
							remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterName;
							remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
							remoteEnvironmentClass.devDefinesGuid = new Guid("{DA59D500-58EA-11D0-8C31-00C04FC2E0BB}");
							remoteEnvironmentClass.name = "Legacy CICS User Data";
							remoteEnvironmentClass.transportName = "SNA Transport";
							remoteEnvironmentClass.programmingModelName = "Legacy CICS User Data";
							remoteEnvironmentClass.StateMachineFullName = "Unknown";
							remoteEnvironmentClass.StateMachineName = "Unknown";
							remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemzHostEnvironmentCICS;
							remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
							remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemz;
							remoteEnvironmentClass.vendorName = text;
							remoteEnvironmentClass.vendorID = guid;
							remoteEnvironmentClass.programmingModel = ProgrammingModel.UserData;
							remoteEnvironmentClass.hostEnvironment = HostEnvironment.CICS;
							remoteEnvironmentClass.transport = Transport.LU62;
							remoteEnvironmentClass.remoteEnvironmentClassID = "{6F5169CB-6835-11D0-8C39-00C04FC2F9BC}";
							remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.LegacyCicsLu62;
							remoteEnvironmentClass.remoteEnvironmentVersion = "REC3";
							remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
							remoteEnvironmentClass.propertyPages = new RECPropertyPage[4];
							remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
							remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
							remoteEnvironmentClass.propertyPages[0].Order = 2;
							remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
							remoteEnvironmentClass.propertyPages[1].Name = "General Page";
							remoteEnvironmentClass.propertyPages[1].Order = 0;
							remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
							remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
							remoteEnvironmentClass.propertyPages[2].Order = 3;
							remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{85D02C42-44A8-11D0-BF45-00AA00BDD5D7}");
							remoteEnvironmentClass.propertyPages[3].Name = "Binding Page";
							remoteEnvironmentClass.propertyPages[3].Order = 1;
							remoteEnvironmentClass.isSupportedByManagedRuntime = false;
							remoteEnvironmentClass.isForNew = false;
							remoteEnvironmentClass.persistentConnectionsSupported = false;
							remoteEnvironmentClass.transactionsAreSupported = true;
							return remoteEnvironmentClass;
						}
					}
					else
					{
						if (RemoteEnvironmentType == RemoteEnvironmentTypes.LegacyCicsLink)
						{
							RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
							remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName;
							remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName;
							remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName;
							remoteEnvironmentClass.transportFullName = RemoteEnvironmentClassFactory.SNATransportFullName;
							remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterName;
							remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterName;
							remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
							remoteEnvironmentClass.devDefinesGuid = new Guid("{DA59D502-58EA-11D0-8C31-00C04FC2E0BB}");
							remoteEnvironmentClass.name = "Legacy CICS Link";
							remoteEnvironmentClass.transportName = "SNA Transport";
							remoteEnvironmentClass.programmingModelName = "Legacy CICS Link";
							remoteEnvironmentClass.StateMachineFullName = "Unknown";
							remoteEnvironmentClass.StateMachineName = "Unknown";
							remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemzHostEnvironmentCICS;
							remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
							remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemz;
							remoteEnvironmentClass.vendorName = text;
							remoteEnvironmentClass.vendorID = guid;
							remoteEnvironmentClass.programmingModel = ProgrammingModel.Link;
							remoteEnvironmentClass.hostEnvironment = HostEnvironment.CICS;
							remoteEnvironmentClass.transport = Transport.LU62;
							remoteEnvironmentClass.remoteEnvironmentClassID = "{6F5169CC-6835-11D0-8C39-00C04FC2F9BC}";
							remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.SnaLink;
							remoteEnvironmentClass.remoteEnvironmentVersion = "REC3";
							remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
							remoteEnvironmentClass.propertyPages = new RECPropertyPage[5];
							remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
							remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
							remoteEnvironmentClass.propertyPages[0].Order = 2;
							remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
							remoteEnvironmentClass.propertyPages[1].Name = "General Page";
							remoteEnvironmentClass.propertyPages[1].Order = 0;
							remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
							remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
							remoteEnvironmentClass.propertyPages[2].Order = 3;
							remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{85D02C42-44A8-11D0-BF45-00AA00BDD5D7}");
							remoteEnvironmentClass.propertyPages[3].Name = "Binding Page";
							remoteEnvironmentClass.propertyPages[3].Order = 1;
							remoteEnvironmentClass.propertyPages[4] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[4].Identifier = new Guid("{B51534D0-7405-11D0-A4BB-00C04FC2ED84}");
							remoteEnvironmentClass.propertyPages[4].Name = "Link Page";
							remoteEnvironmentClass.propertyPages[4].Order = 4;
							remoteEnvironmentClass.isSupportedByManagedRuntime = false;
							remoteEnvironmentClass.isForNew = false;
							remoteEnvironmentClass.persistentConnectionsSupported = false;
							remoteEnvironmentClass.transactionsAreSupported = true;
							return remoteEnvironmentClass;
						}
						if (RemoteEnvironmentType == RemoteEnvironmentTypes.LegacyTcp)
						{
							RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
							remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName;
							remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName;
							remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName;
							remoteEnvironmentClass.transportFullName = RemoteEnvironmentClassFactory.IMSConnectTransportFullName;
							remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterName;
							remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterName;
							remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
							remoteEnvironmentClass.devDefinesGuid = new Guid("{9076ECA7-4DAB-11D2-A06B-00C04FC2E0BB}");
							remoteEnvironmentClass.name = "Legacy TCP";
							remoteEnvironmentClass.transportName = "TCP Transport";
							remoteEnvironmentClass.programmingModelName = "Legacy TCP";
							remoteEnvironmentClass.StateMachineFullName = RemoteEnvironmentClassFactory.TCPStateMachineFullName;
							remoteEnvironmentClass.StateMachineName = RemoteEnvironmentClassFactory.TCPStateMachineName;
							remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemzHostEnvironmentCICS;
							remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
							remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemz;
							remoteEnvironmentClass.vendorName = text;
							remoteEnvironmentClass.vendorID = guid;
							remoteEnvironmentClass.programmingModel = ProgrammingModel.UserData;
							remoteEnvironmentClass.hostEnvironment = HostEnvironment.CICS;
							remoteEnvironmentClass.transport = Transport.TCP;
							remoteEnvironmentClass.remoteEnvironmentClassID = "{6F5169CD-6835-11D0-8C39-00C04FC2F9BC}";
							remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.LegacyTcp;
							remoteEnvironmentClass.remoteEnvironmentVersion = "REC3";
							remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
							remoteEnvironmentClass.propertyPages = new RECPropertyPage[4];
							remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
							remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
							remoteEnvironmentClass.propertyPages[0].Order = 2;
							remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
							remoteEnvironmentClass.propertyPages[1].Name = "General Page";
							remoteEnvironmentClass.propertyPages[1].Order = 0;
							remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
							remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
							remoteEnvironmentClass.propertyPages[2].Order = 3;
							remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{ED56A89A-5973-11D2-9647-00C04FB904E0}");
							remoteEnvironmentClass.propertyPages[3].Name = "TCP/IP Page";
							remoteEnvironmentClass.propertyPages[3].Order = 1;
							remoteEnvironmentClass.isSupportedByManagedRuntime = false;
							remoteEnvironmentClass.isForNew = false;
							remoteEnvironmentClass.persistentConnectionsSupported = false;
							remoteEnvironmentClass.transactionsAreSupported = false;
							return remoteEnvironmentClass;
						}
						if (RemoteEnvironmentType == RemoteEnvironmentTypes.LegacyImsConnect)
						{
							RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
							remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName;
							remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName;
							remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName;
							remoteEnvironmentClass.transportFullName = RemoteEnvironmentClassFactory.IMSConnectTransportFullName;
							remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterName;
							remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterName;
							remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
							remoteEnvironmentClass.devDefinesGuid = new Guid("{9076ECA7-4DAB-11D2-A06B-00C04FC2E0BB}");
							remoteEnvironmentClass.name = "Legacy IMS Connect";
							remoteEnvironmentClass.transportName = "IMS Connect Transport";
							remoteEnvironmentClass.programmingModelName = "OTMA";
							remoteEnvironmentClass.StateMachineFullName = "Unknown";
							remoteEnvironmentClass.StateMachineName = "Unknown";
							remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemzHostEnvironmentIMS;
							remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
							remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemz;
							remoteEnvironmentClass.vendorName = text;
							remoteEnvironmentClass.vendorID = guid;
							remoteEnvironmentClass.programmingModel = ProgrammingModel.IMSConnect;
							remoteEnvironmentClass.hostEnvironment = HostEnvironment.IMS;
							remoteEnvironmentClass.transport = Transport.TCP;
							remoteEnvironmentClass.remoteEnvironmentClassID = "{6F5169CE-6835-11D0-8C39-00C04FC2F9BC}";
							remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.LegacyImsConnect;
							remoteEnvironmentClass.remoteEnvironmentVersion = "REC3";
							remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
							remoteEnvironmentClass.propertyPages = new RECPropertyPage[4];
							remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
							remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
							remoteEnvironmentClass.propertyPages[0].Order = 2;
							remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
							remoteEnvironmentClass.propertyPages[1].Name = "General Page";
							remoteEnvironmentClass.propertyPages[1].Order = 0;
							remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
							remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
							remoteEnvironmentClass.propertyPages[2].Order = 3;
							remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
							remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{ED56A89A-5973-11D2-9647-00C04FB904E0}");
							remoteEnvironmentClass.propertyPages[3].Name = "TCP/IP Page";
							remoteEnvironmentClass.propertyPages[3].Order = 1;
							remoteEnvironmentClass.isSupportedByManagedRuntime = false;
							remoteEnvironmentClass.isForNew = false;
							remoteEnvironmentClass.persistentConnectionsSupported = false;
							remoteEnvironmentClass.transactionsAreSupported = false;
							return remoteEnvironmentClass;
						}
					}
				}
				else if (RemoteEnvironmentType <= RemoteEnvironmentTypes.SnaLink)
				{
					if (RemoteEnvironmentType == RemoteEnvironmentTypes.SnaUserData)
					{
						RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
						remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName;
						remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName;
						remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName;
						remoteEnvironmentClass.transportFullName = RemoteEnvironmentClassFactory.SNATransportFullName;
						remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterName;
						remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterName;
						remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
						remoteEnvironmentClass.name = "CICS UserData";
						remoteEnvironmentClass.devDefinesGuid = new Guid("{DA59D500-58EA-11D0-8C31-00C04FC2E0BB}");
						remoteEnvironmentClass.transportName = "SNA Transport";
						remoteEnvironmentClass.programmingModelName = "CICS LU6.2";
						remoteEnvironmentClass.StateMachineFullName = RemoteEnvironmentClassFactory.GenericUserDataStateMachineFullName;
						remoteEnvironmentClass.StateMachineName = RemoteEnvironmentClassFactory.GenericUserDataStateMachineName;
						remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemzHostEnvironmentCICS;
						remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
						remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemz;
						remoteEnvironmentClass.vendorName = text;
						remoteEnvironmentClass.vendorID = guid;
						remoteEnvironmentClass.programmingModel = ProgrammingModel.UserData;
						remoteEnvironmentClass.hostEnvironment = HostEnvironment.CICS;
						remoteEnvironmentClass.transport = Transport.LU62;
						remoteEnvironmentClass.remoteEnvironmentClassID = "{55185CC7-42BA-4926-9F8B-513139CA7A61}";
						remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.SnaUserData;
						remoteEnvironmentClass.remoteEnvironmentVersion = "REC4";
						remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
						remoteEnvironmentClass.propertyPages = new RECPropertyPage[4];
						remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
						remoteEnvironmentClass.propertyPages[0].Order = 2;
						remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[1].Name = "General Page";
						remoteEnvironmentClass.propertyPages[1].Order = 0;
						remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
						remoteEnvironmentClass.propertyPages[2].Order = 3;
						remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{85D02C42-44A8-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[3].Name = "Binding Page";
						remoteEnvironmentClass.propertyPages[3].Order = 1;
						remoteEnvironmentClass.isSupportedByManagedRuntime = true;
						remoteEnvironmentClass.isForNew = true;
						remoteEnvironmentClass.persistentConnectionsSupported = true;
						remoteEnvironmentClass.transactionsAreSupported = true;
						return remoteEnvironmentClass;
					}
					if (RemoteEnvironmentType == RemoteEnvironmentTypes.SnaLink)
					{
						RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
						remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName;
						remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName;
						remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName;
						remoteEnvironmentClass.transportFullName = RemoteEnvironmentClassFactory.SNATransportFullName;
						remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterName;
						remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterName;
						remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
						remoteEnvironmentClass.name = "CICS Link";
						remoteEnvironmentClass.devDefinesGuid = new Guid("{DA59D502-58EA-11D0-8C31-00C04FC2E0BB}");
						remoteEnvironmentClass.transportName = "SNA Transport";
						remoteEnvironmentClass.programmingModelName = "CICS LU6.2 Link";
						remoteEnvironmentClass.StateMachineFullName = RemoteEnvironmentClassFactory.GenericLinkStateMachineFullName;
						remoteEnvironmentClass.StateMachineName = RemoteEnvironmentClassFactory.GenericLinkStateMachineName;
						remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemzHostEnvironmentCICS;
						remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
						remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemz;
						remoteEnvironmentClass.vendorName = text;
						remoteEnvironmentClass.vendorID = guid;
						remoteEnvironmentClass.programmingModel = ProgrammingModel.Link;
						remoteEnvironmentClass.hostEnvironment = HostEnvironment.CICS;
						remoteEnvironmentClass.transport = Transport.LU62;
						remoteEnvironmentClass.remoteEnvironmentClassID = "{56690CE3-D72E-4643-98C2-A85E2978CD64}";
						remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.SnaLink;
						remoteEnvironmentClass.remoteEnvironmentVersion = "REC4";
						remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
						remoteEnvironmentClass.propertyPages = new RECPropertyPage[5];
						remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
						remoteEnvironmentClass.propertyPages[0].Order = 2;
						remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[1].Name = "General Page";
						remoteEnvironmentClass.propertyPages[1].Order = 0;
						remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
						remoteEnvironmentClass.propertyPages[2].Order = 3;
						remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{85D02C42-44A8-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[3].Name = "Binding Page";
						remoteEnvironmentClass.propertyPages[3].Order = 1;
						remoteEnvironmentClass.propertyPages[4] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[4].Identifier = new Guid("{B51534D0-7405-11D0-A4BB-00C04FC2ED84}");
						remoteEnvironmentClass.propertyPages[4].Name = "Link Page";
						remoteEnvironmentClass.propertyPages[4].Order = 4;
						remoteEnvironmentClass.isSupportedByManagedRuntime = true;
						remoteEnvironmentClass.isForNew = true;
						remoteEnvironmentClass.persistentConnectionsSupported = true;
						remoteEnvironmentClass.transactionsAreSupported = true;
						return remoteEnvironmentClass;
					}
				}
				else
				{
					if (RemoteEnvironmentType == RemoteEnvironmentTypes.HttpUserData)
					{
						RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
						remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName;
						remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName;
						remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName;
						remoteEnvironmentClass.transportFullName = RemoteEnvironmentClassFactory.HTTPTransportFullName;
						remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterName;
						remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterName;
						remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
						remoteEnvironmentClass.devDefinesGuid = new Guid("{9076ECA4-4DAB-11D2-A06B-00C04FC2E0BB}");
						remoteEnvironmentClass.name = "HTTP UserData";
						remoteEnvironmentClass.transportName = "HTTP Transport";
						remoteEnvironmentClass.programmingModelName = "CICS HTTP UserData";
						remoteEnvironmentClass.StateMachineFullName = RemoteEnvironmentClassFactory.GenericUserDataStateMachineFullName;
						remoteEnvironmentClass.StateMachineName = RemoteEnvironmentClassFactory.GenericUserDataStateMachineName;
						remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemzHostEnvironmentCICS;
						remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
						remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemz;
						remoteEnvironmentClass.vendorName = text;
						remoteEnvironmentClass.vendorID = guid;
						remoteEnvironmentClass.programmingModel = ProgrammingModel.UserData;
						remoteEnvironmentClass.hostEnvironment = HostEnvironment.CICS;
						remoteEnvironmentClass.transport = Transport.Http;
						remoteEnvironmentClass.remoteEnvironmentClassID = "{B3AA6C63-C1BF-49AE-A154-A39CF8F75E79}";
						remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.HttpUserData;
						remoteEnvironmentClass.remoteEnvironmentVersion = "REC5";
						remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
						remoteEnvironmentClass.propertyPages = new RECPropertyPage[4];
						remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
						remoteEnvironmentClass.propertyPages[0].Order = 2;
						remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[1].Name = "General Page";
						remoteEnvironmentClass.propertyPages[1].Order = 0;
						remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
						remoteEnvironmentClass.propertyPages[2].Order = 3;
						remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{2D4F2A7B-AE31-4c55-B483-F6D1FB5F3BA3}");
						remoteEnvironmentClass.propertyPages[3].Name = "HTTP Page";
						remoteEnvironmentClass.propertyPages[3].Order = 1;
						remoteEnvironmentClass.isSupportedByManagedRuntime = true;
						remoteEnvironmentClass.isForNew = true;
						remoteEnvironmentClass.persistentConnectionsSupported = true;
						remoteEnvironmentClass.transactionsAreSupported = false;
						return remoteEnvironmentClass;
					}
					if (RemoteEnvironmentType == RemoteEnvironmentTypes.HttpLink)
					{
						RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
						remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName;
						remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName;
						remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName;
						remoteEnvironmentClass.transportFullName = RemoteEnvironmentClassFactory.HTTPTransportFullName;
						remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterName;
						remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterName;
						remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
						remoteEnvironmentClass.devDefinesGuid = new Guid("{9076ECA4-4DAB-11D2-A06B-00C04FC2E0BB}");
						remoteEnvironmentClass.name = "HTTP Link";
						remoteEnvironmentClass.transportName = "HTTP Transport";
						remoteEnvironmentClass.programmingModelName = "CICS HTTP Link";
						remoteEnvironmentClass.StateMachineFullName = RemoteEnvironmentClassFactory.GenericLinkStateMachineFullName;
						remoteEnvironmentClass.StateMachineName = RemoteEnvironmentClassFactory.GenericLinkStateMachineName;
						remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemzHostEnvironmentCICS;
						remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
						remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemz;
						remoteEnvironmentClass.vendorName = text;
						remoteEnvironmentClass.vendorID = guid;
						remoteEnvironmentClass.programmingModel = ProgrammingModel.Link;
						remoteEnvironmentClass.hostEnvironment = HostEnvironment.CICS;
						remoteEnvironmentClass.transport = Transport.Http;
						remoteEnvironmentClass.remoteEnvironmentClassID = "{B06A9229-1F92-44FD-A0CB-66403A5C82BD}";
						remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.HttpLink;
						remoteEnvironmentClass.remoteEnvironmentVersion = "REC5";
						remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
						remoteEnvironmentClass.propertyPages = new RECPropertyPage[4];
						remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
						remoteEnvironmentClass.propertyPages[0].Order = 2;
						remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[1].Name = "General Page";
						remoteEnvironmentClass.propertyPages[1].Order = 0;
						remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
						remoteEnvironmentClass.propertyPages[2].Order = 3;
						remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{3BAFE741-54BD-4EEE-A2A2-9732B7687D60}");
						remoteEnvironmentClass.propertyPages[3].Name = "HTTP Page";
						remoteEnvironmentClass.propertyPages[3].Order = 1;
						remoteEnvironmentClass.isSupportedByManagedRuntime = true;
						remoteEnvironmentClass.isForNew = true;
						remoteEnvironmentClass.persistentConnectionsSupported = true;
						remoteEnvironmentClass.transactionsAreSupported = false;
						return remoteEnvironmentClass;
					}
					if (RemoteEnvironmentType == RemoteEnvironmentTypes.ImsLu62)
					{
						RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
						remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName;
						remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName;
						remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName;
						remoteEnvironmentClass.transportFullName = RemoteEnvironmentClassFactory.SNATransportFullName;
						remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterName;
						remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterName;
						remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
						remoteEnvironmentClass.name = "IMS LU6.2";
						remoteEnvironmentClass.devDefinesGuid = new Guid("{DA59D503-58EA-11D0-8C31-00C04FC2E0BB}");
						remoteEnvironmentClass.transportName = "SNA Transport";
						remoteEnvironmentClass.programmingModelName = "IMS LU6.2";
						remoteEnvironmentClass.StateMachineFullName = RemoteEnvironmentClassFactory.GenericUserDataStateMachineFullName;
						remoteEnvironmentClass.StateMachineName = RemoteEnvironmentClassFactory.GenericUserDataStateMachineName;
						remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemzHostEnvironmentIMS;
						remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
						remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemz;
						remoteEnvironmentClass.vendorName = text;
						remoteEnvironmentClass.vendorID = guid;
						remoteEnvironmentClass.programmingModel = ProgrammingModel.UserData;
						remoteEnvironmentClass.hostEnvironment = HostEnvironment.IMS;
						remoteEnvironmentClass.transport = Transport.LU62;
						remoteEnvironmentClass.remoteEnvironmentClassID = "{5093418E-2FC6-4363-8EDF-665217E346D9}";
						remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.ImsLu62;
						remoteEnvironmentClass.remoteEnvironmentVersion = "REC4";
						remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
						remoteEnvironmentClass.propertyPages = new RECPropertyPage[4];
						remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
						remoteEnvironmentClass.propertyPages[0].Order = 2;
						remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[1].Name = "General Page";
						remoteEnvironmentClass.propertyPages[1].Order = 0;
						remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
						remoteEnvironmentClass.propertyPages[2].Order = 3;
						remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{85D02C42-44A8-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[3].Name = "Binding Page";
						remoteEnvironmentClass.propertyPages[3].Order = 1;
						remoteEnvironmentClass.isSupportedByManagedRuntime = true;
						remoteEnvironmentClass.isForNew = true;
						remoteEnvironmentClass.persistentConnectionsSupported = true;
						remoteEnvironmentClass.transactionsAreSupported = true;
						return remoteEnvironmentClass;
					}
				}
			}
			else if (RemoteEnvironmentType <= RemoteEnvironmentTypes.TrmUserData)
			{
				if (RemoteEnvironmentType <= RemoteEnvironmentTypes.SystemzSocketsUserData)
				{
					if (RemoteEnvironmentType == RemoteEnvironmentTypes.ImsConnect)
					{
						RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
						remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName;
						remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName;
						remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName;
						remoteEnvironmentClass.transportFullName = RemoteEnvironmentClassFactory.IMSConnectTransportFullName;
						remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterName;
						remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterName;
						remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
						remoteEnvironmentClass.devDefinesGuid = new Guid("{9076ECA7-4DAB-11D2-A06B-00C04FC2E0BB}");
						remoteEnvironmentClass.name = "IMS Connect";
						remoteEnvironmentClass.transportName = "IMS Connect Transport";
						remoteEnvironmentClass.programmingModelName = "IMS Connect";
						remoteEnvironmentClass.StateMachineFullName = RemoteEnvironmentClassFactory.IMSConnectStateMachineFullName;
						remoteEnvironmentClass.StateMachineName = RemoteEnvironmentClassFactory.IMSConnectStateMachineName;
						remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemzHostEnvironmentIMS;
						remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
						remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemz;
						remoteEnvironmentClass.vendorName = text;
						remoteEnvironmentClass.vendorID = guid;
						remoteEnvironmentClass.programmingModel = ProgrammingModel.IMSConnect;
						remoteEnvironmentClass.hostEnvironment = HostEnvironment.IMS;
						remoteEnvironmentClass.transport = Transport.TCP;
						remoteEnvironmentClass.remoteEnvironmentClassID = "{0A327B80-AFB4-422F-95FC-94623DA12769}";
						remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.ImsConnect;
						remoteEnvironmentClass.remoteEnvironmentVersion = "REC4";
						remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
						remoteEnvironmentClass.propertyPages = new RECPropertyPage[4];
						remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
						remoteEnvironmentClass.propertyPages[0].Order = 2;
						remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[1].Name = "General Page";
						remoteEnvironmentClass.propertyPages[1].Order = 0;
						remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
						remoteEnvironmentClass.propertyPages[2].Order = 3;
						remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{ED56A89A-5973-11D2-9647-00C04FB904E0}");
						remoteEnvironmentClass.propertyPages[3].Name = "TCP/IP Page";
						remoteEnvironmentClass.propertyPages[3].Order = 1;
						remoteEnvironmentClass.isSupportedByManagedRuntime = true;
						remoteEnvironmentClass.isForNew = true;
						remoteEnvironmentClass.persistentConnectionsSupported = true;
						remoteEnvironmentClass.transactionsAreSupported = false;
						return remoteEnvironmentClass;
					}
					if (RemoteEnvironmentType == RemoteEnvironmentTypes.SystemzSocketsUserData)
					{
						RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
						remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName;
						remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName;
						remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName;
						remoteEnvironmentClass.transportFullName = RemoteEnvironmentClassFactory.TCPTransportFullName;
						remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterName;
						remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterName;
						remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
						remoteEnvironmentClass.devDefinesGuid = new Guid("{9076ECA4-4DAB-11D2-A06B-00C04FC2E0BB}");
						remoteEnvironmentClass.name = "Systemz TCP Sockets User Data";
						remoteEnvironmentClass.transportName = "TCP Sockets Transport";
						remoteEnvironmentClass.programmingModelName = "System z Sockets";
						remoteEnvironmentClass.StateMachineFullName = RemoteEnvironmentClassFactory.GenericUserDataStateMachineFullName;
						remoteEnvironmentClass.StateMachineName = RemoteEnvironmentClassFactory.GenericUserDataStateMachineName;
						remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemzPlatformName;
						remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
						remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemz;
						remoteEnvironmentClass.vendorName = text;
						remoteEnvironmentClass.vendorID = guid;
						remoteEnvironmentClass.programmingModel = ProgrammingModel.UserData;
						remoteEnvironmentClass.hostEnvironment = HostEnvironment.Systemz;
						remoteEnvironmentClass.transport = Transport.TCP;
						remoteEnvironmentClass.remoteEnvironmentClassID = "{D75FD330-2A06-4AC9-A9C5-A7F97DF41B96}";
						remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.SystemzSocketsUserData;
						remoteEnvironmentClass.remoteEnvironmentVersion = "REC5";
						remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
						remoteEnvironmentClass.propertyPages = new RECPropertyPage[4];
						remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
						remoteEnvironmentClass.propertyPages[0].Order = 2;
						remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[1].Name = "General Page";
						remoteEnvironmentClass.propertyPages[1].Order = 0;
						remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
						remoteEnvironmentClass.propertyPages[2].Order = 3;
						remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{ED56A89A-5973-11D2-9647-00C04FB904E0}");
						remoteEnvironmentClass.propertyPages[3].Name = "TCP/IP Page";
						remoteEnvironmentClass.propertyPages[3].Order = 1;
						remoteEnvironmentClass.isSupportedByManagedRuntime = true;
						remoteEnvironmentClass.isForNew = true;
						remoteEnvironmentClass.persistentConnectionsSupported = true;
						remoteEnvironmentClass.transactionsAreSupported = false;
						return remoteEnvironmentClass;
					}
				}
				else
				{
					if (RemoteEnvironmentType == RemoteEnvironmentTypes.SystemzSocketsLink)
					{
						RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
						remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName;
						remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName;
						remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName;
						remoteEnvironmentClass.transportFullName = RemoteEnvironmentClassFactory.TCPTransportFullName;
						remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterName;
						remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterName;
						remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
						remoteEnvironmentClass.devDefinesGuid = new Guid("{9076ECA4-4DAB-11D2-A06B-00C04FC2E0BB}");
						remoteEnvironmentClass.name = "Systemz TCP Sockets Link";
						remoteEnvironmentClass.transportName = "TCP Sockets Transport";
						remoteEnvironmentClass.programmingModelName = "System z Sockets with Link model restrictions";
						remoteEnvironmentClass.StateMachineFullName = RemoteEnvironmentClassFactory.GenericLinkStateMachineFullName;
						remoteEnvironmentClass.StateMachineName = RemoteEnvironmentClassFactory.GenericLinkStateMachineName;
						remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemzPlatformName;
						remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
						remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemz;
						remoteEnvironmentClass.vendorName = text;
						remoteEnvironmentClass.vendorID = guid;
						remoteEnvironmentClass.programmingModel = ProgrammingModel.Link;
						remoteEnvironmentClass.hostEnvironment = HostEnvironment.Systemz;
						remoteEnvironmentClass.transport = Transport.TCP;
						remoteEnvironmentClass.remoteEnvironmentClassID = "{964D48F9-E4E4-434F-9167-55BA39385BFF}";
						remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.SystemzSocketsLink;
						remoteEnvironmentClass.remoteEnvironmentVersion = "REC5";
						remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
						remoteEnvironmentClass.propertyPages = new RECPropertyPage[4];
						remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
						remoteEnvironmentClass.propertyPages[0].Order = 2;
						remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[1].Name = "General Page";
						remoteEnvironmentClass.propertyPages[1].Order = 0;
						remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
						remoteEnvironmentClass.propertyPages[2].Order = 3;
						remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{ED56A89A-5973-11D2-9647-00C04FB904E0}");
						remoteEnvironmentClass.propertyPages[3].Name = "TCP/IP Page";
						remoteEnvironmentClass.propertyPages[3].Order = 1;
						remoteEnvironmentClass.isSupportedByManagedRuntime = true;
						remoteEnvironmentClass.isForNew = true;
						remoteEnvironmentClass.persistentConnectionsSupported = true;
						remoteEnvironmentClass.transactionsAreSupported = false;
						return remoteEnvironmentClass;
					}
					if (RemoteEnvironmentType == RemoteEnvironmentTypes.SystemiSocketsUserData)
					{
						RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
						remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName;
						remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName;
						remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemiPrimitiveConverterFullName;
						remoteEnvironmentClass.transportFullName = RemoteEnvironmentClassFactory.TCPTransportFullName;
						remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterName;
						remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemiPrimitiveConverterName;
						remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemiPrimitiveConverterClassId;
						remoteEnvironmentClass.devDefinesGuid = new Guid("{9076ECA7-4DAB-11D2-A06B-00C04FC2E0BB}");
						remoteEnvironmentClass.name = "Systemi TCP Sockets User Data";
						remoteEnvironmentClass.transportName = "TCP Sockets Transport";
						remoteEnvironmentClass.programmingModelName = "System i Sockets";
						remoteEnvironmentClass.StateMachineFullName = RemoteEnvironmentClassFactory.GenericUserDataStateMachineFullName;
						remoteEnvironmentClass.StateMachineName = RemoteEnvironmentClassFactory.GenericUserDataStateMachineName;
						remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemiPlatformName;
						remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemiPlatformName;
						remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemi;
						remoteEnvironmentClass.vendorName = text;
						remoteEnvironmentClass.vendorID = guid;
						remoteEnvironmentClass.programmingModel = ProgrammingModel.UserData;
						remoteEnvironmentClass.hostEnvironment = HostEnvironment.Systemi;
						remoteEnvironmentClass.transport = Transport.TCP;
						remoteEnvironmentClass.remoteEnvironmentClassID = "{50906E71-C59C-47EC-9D8C-D0C8C155933B}";
						remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.SystemiSocketsUserData;
						remoteEnvironmentClass.remoteEnvironmentVersion = "REC5";
						remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemiPrimitiveConverterClassId);
						remoteEnvironmentClass.propertyPages = new RECPropertyPage[4];
						remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
						remoteEnvironmentClass.propertyPages[0].Order = 2;
						remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[1].Name = "General Page";
						remoteEnvironmentClass.propertyPages[1].Order = 0;
						remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
						remoteEnvironmentClass.propertyPages[2].Order = 3;
						remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{ED56A89A-5973-11D2-9647-00C04FB904E0}");
						remoteEnvironmentClass.propertyPages[3].Name = "TCP/IP Page";
						remoteEnvironmentClass.propertyPages[3].Order = 1;
						remoteEnvironmentClass.isSupportedByManagedRuntime = true;
						remoteEnvironmentClass.isForNew = true;
						remoteEnvironmentClass.persistentConnectionsSupported = true;
						remoteEnvironmentClass.transactionsAreSupported = false;
						return remoteEnvironmentClass;
					}
					if (RemoteEnvironmentType == RemoteEnvironmentTypes.TrmUserData)
					{
						RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
						remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName;
						remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName;
						remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName;
						remoteEnvironmentClass.transportFullName = RemoteEnvironmentClassFactory.TRMTransportFullName;
						remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterName;
						remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterName;
						remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
						remoteEnvironmentClass.devDefinesGuid = new Guid("{9076ECA4-4DAB-11D2-A06B-00C04FC2E0BB}");
						remoteEnvironmentClass.name = "TRM User Data";
						remoteEnvironmentClass.transportName = "TRM Transport";
						remoteEnvironmentClass.programmingModelName = "CICS TRM User Data";
						remoteEnvironmentClass.StateMachineFullName = RemoteEnvironmentClassFactory.GenericUserDataStateMachineFullName;
						remoteEnvironmentClass.StateMachineName = RemoteEnvironmentClassFactory.GenericUserDataStateMachineName;
						remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemzHostEnvironmentCICS;
						remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
						remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemz;
						remoteEnvironmentClass.vendorName = text;
						remoteEnvironmentClass.vendorID = guid;
						remoteEnvironmentClass.programmingModel = ProgrammingModel.TRMUserData;
						remoteEnvironmentClass.hostEnvironment = HostEnvironment.CICS;
						remoteEnvironmentClass.transport = Transport.TCP;
						remoteEnvironmentClass.remoteEnvironmentClassID = "{7F4BA82E-54CC-451B-99B5-EAAD9BD0365F}";
						remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.TrmUserData;
						remoteEnvironmentClass.remoteEnvironmentVersion = "REC4";
						remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
						remoteEnvironmentClass.propertyPages = new RECPropertyPage[4];
						remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
						remoteEnvironmentClass.propertyPages[0].Order = 2;
						remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[1].Name = "General Page";
						remoteEnvironmentClass.propertyPages[1].Order = 0;
						remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
						remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
						remoteEnvironmentClass.propertyPages[2].Order = 3;
						remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
						remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{ED56A89A-5973-11D2-9647-00C04FB904E0}");
						remoteEnvironmentClass.propertyPages[3].Name = "TCP/IP Page";
						remoteEnvironmentClass.propertyPages[3].Order = 1;
						remoteEnvironmentClass.isSupportedByManagedRuntime = true;
						remoteEnvironmentClass.isForNew = true;
						remoteEnvironmentClass.persistentConnectionsSupported = true;
						remoteEnvironmentClass.transactionsAreSupported = false;
						return remoteEnvironmentClass;
					}
				}
			}
			else if (RemoteEnvironmentType <= RemoteEnvironmentTypes.ElmLink)
			{
				if (RemoteEnvironmentType == RemoteEnvironmentTypes.TrmLink)
				{
					RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
					remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName;
					remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName;
					remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName;
					remoteEnvironmentClass.transportFullName = RemoteEnvironmentClassFactory.TRMTransportFullName;
					remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterName;
					remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterName;
					remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
					remoteEnvironmentClass.devDefinesGuid = new Guid("{9076ECA4-4DAB-11D2-A06B-00C04FC2E0BB}");
					remoteEnvironmentClass.name = "TRM Link";
					remoteEnvironmentClass.transportName = "TRM Transport";
					remoteEnvironmentClass.programmingModelName = "CICS TRM Link";
					remoteEnvironmentClass.StateMachineFullName = RemoteEnvironmentClassFactory.GenericLinkStateMachineFullName;
					remoteEnvironmentClass.StateMachineName = RemoteEnvironmentClassFactory.GenericLinkStateMachineName;
					remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemzHostEnvironmentCICS;
					remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
					remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemz;
					remoteEnvironmentClass.vendorName = text;
					remoteEnvironmentClass.vendorID = guid;
					remoteEnvironmentClass.programmingModel = ProgrammingModel.TRMLink;
					remoteEnvironmentClass.hostEnvironment = HostEnvironment.CICS;
					remoteEnvironmentClass.transport = Transport.TCP;
					remoteEnvironmentClass.remoteEnvironmentClassID = "{C348E268-D254-44B3-90A2-F2AD2139BEA9}";
					remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.TrmLink;
					remoteEnvironmentClass.remoteEnvironmentVersion = "REC4";
					remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
					remoteEnvironmentClass.propertyPages = new RECPropertyPage[4];
					remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
					remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
					remoteEnvironmentClass.propertyPages[0].Order = 2;
					remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
					remoteEnvironmentClass.propertyPages[1].Name = "General Page";
					remoteEnvironmentClass.propertyPages[1].Order = 0;
					remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
					remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
					remoteEnvironmentClass.propertyPages[2].Order = 3;
					remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{ED56A89A-5973-11D2-9647-00C04FB904E0}");
					remoteEnvironmentClass.propertyPages[3].Name = "TCP/IP Page";
					remoteEnvironmentClass.propertyPages[3].Order = 1;
					remoteEnvironmentClass.isSupportedByManagedRuntime = true;
					remoteEnvironmentClass.isForNew = true;
					remoteEnvironmentClass.persistentConnectionsSupported = true;
					remoteEnvironmentClass.transactionsAreSupported = false;
					return remoteEnvironmentClass;
				}
				if (RemoteEnvironmentType == RemoteEnvironmentTypes.ElmUserData)
				{
					RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
					remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName;
					remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName;
					remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName;
					remoteEnvironmentClass.transportFullName = RemoteEnvironmentClassFactory.ELMTransportFullName;
					remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterName;
					remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterName;
					remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
					remoteEnvironmentClass.devDefinesGuid = new Guid("{9076ECA4-4DAB-11D2-A06B-00C04FC2E0BB}");
					remoteEnvironmentClass.name = "ELM User Data";
					remoteEnvironmentClass.transportName = "ELM Transport";
					remoteEnvironmentClass.programmingModelName = "CICS ELM User Data";
					remoteEnvironmentClass.StateMachineFullName = RemoteEnvironmentClassFactory.GenericUserDataStateMachineFullName;
					remoteEnvironmentClass.StateMachineName = RemoteEnvironmentClassFactory.GenericUserDataStateMachineName;
					remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemzHostEnvironmentCICS;
					remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
					remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemz;
					remoteEnvironmentClass.vendorName = text;
					remoteEnvironmentClass.vendorID = guid;
					remoteEnvironmentClass.programmingModel = ProgrammingModel.ELMUserData;
					remoteEnvironmentClass.hostEnvironment = HostEnvironment.CICS;
					remoteEnvironmentClass.transport = Transport.TCP;
					remoteEnvironmentClass.remoteEnvironmentClassID = "{2AD1C7FD-3FFE-4967-A9CC-050AD6A336F0}";
					remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.ElmUserData;
					remoteEnvironmentClass.remoteEnvironmentVersion = "REC4";
					remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
					remoteEnvironmentClass.propertyPages = new RECPropertyPage[4];
					remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
					remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
					remoteEnvironmentClass.propertyPages[0].Order = 2;
					remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
					remoteEnvironmentClass.propertyPages[1].Name = "General Page";
					remoteEnvironmentClass.propertyPages[1].Order = 0;
					remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
					remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
					remoteEnvironmentClass.propertyPages[2].Order = 3;
					remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{ED56A89A-5973-11D2-9647-00C04FB904E0}");
					remoteEnvironmentClass.propertyPages[3].Name = "TCP/IP Page";
					remoteEnvironmentClass.propertyPages[3].Order = 1;
					remoteEnvironmentClass.isSupportedByManagedRuntime = true;
					remoteEnvironmentClass.isForNew = true;
					remoteEnvironmentClass.persistentConnectionsSupported = true;
					remoteEnvironmentClass.transactionsAreSupported = false;
					return remoteEnvironmentClass;
				}
				if (RemoteEnvironmentType == RemoteEnvironmentTypes.ElmLink)
				{
					RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
					remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName;
					remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName;
					remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName;
					remoteEnvironmentClass.transportFullName = RemoteEnvironmentClassFactory.ELMTransportFullName;
					remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterName;
					remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterName;
					remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
					remoteEnvironmentClass.devDefinesGuid = new Guid("{9076ECA4-4DAB-11D2-A06B-00C04FC2E0BB}");
					remoteEnvironmentClass.name = "ELM Link";
					remoteEnvironmentClass.transportName = "ELM Transport";
					remoteEnvironmentClass.programmingModelName = "CICS ELM Link";
					remoteEnvironmentClass.StateMachineFullName = RemoteEnvironmentClassFactory.GenericLinkStateMachineFullName;
					remoteEnvironmentClass.StateMachineName = RemoteEnvironmentClassFactory.GenericLinkStateMachineName;
					remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemzHostEnvironmentCICS;
					remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
					remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemz;
					remoteEnvironmentClass.vendorName = text;
					remoteEnvironmentClass.vendorID = guid;
					remoteEnvironmentClass.programmingModel = ProgrammingModel.ELMLink;
					remoteEnvironmentClass.hostEnvironment = HostEnvironment.CICS;
					remoteEnvironmentClass.transport = Transport.TCP;
					remoteEnvironmentClass.remoteEnvironmentClassID = "{20CFA202-8BF2-4C1A-8126-AB759E913072}";
					remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.ElmLink;
					remoteEnvironmentClass.remoteEnvironmentVersion = "REC4";
					remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
					remoteEnvironmentClass.propertyPages = new RECPropertyPage[4];
					remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
					remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
					remoteEnvironmentClass.propertyPages[0].Order = 2;
					remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
					remoteEnvironmentClass.propertyPages[1].Name = "General Page";
					remoteEnvironmentClass.propertyPages[1].Order = 0;
					remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
					remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
					remoteEnvironmentClass.propertyPages[2].Order = 3;
					remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{ED56A89A-5973-11D2-9647-00C04FB904E0}");
					remoteEnvironmentClass.propertyPages[3].Name = "TCP/IP Page";
					remoteEnvironmentClass.propertyPages[3].Order = 1;
					remoteEnvironmentClass.isSupportedByManagedRuntime = true;
					remoteEnvironmentClass.isForNew = true;
					remoteEnvironmentClass.persistentConnectionsSupported = true;
					remoteEnvironmentClass.transactionsAreSupported = false;
					return remoteEnvironmentClass;
				}
			}
			else
			{
				if (RemoteEnvironmentType == RemoteEnvironmentTypes.ImsImplicit)
				{
					RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
					remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName;
					remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName;
					remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName;
					remoteEnvironmentClass.transportFullName = "TranTCP.dll";
					remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterName;
					remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterName;
					remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
					remoteEnvironmentClass.devDefinesGuid = new Guid("{9076ECA4-4DAB-11D2-A06B-00C04FC2E0BB}");
					remoteEnvironmentClass.name = "IMS Implicit";
					remoteEnvironmentClass.transportName = "TCP Transport";
					remoteEnvironmentClass.programmingModelName = "IMS Implicit";
					remoteEnvironmentClass.StateMachineFullName = "Unknown";
					remoteEnvironmentClass.StateMachineName = "Unknown";
					remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemzHostEnvironmentIMS;
					remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
					remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemz;
					remoteEnvironmentClass.vendorName = text;
					remoteEnvironmentClass.vendorID = guid;
					remoteEnvironmentClass.programmingModel = ProgrammingModel.IMSImplicit;
					remoteEnvironmentClass.hostEnvironment = HostEnvironment.IMS;
					remoteEnvironmentClass.transport = Transport.TCP;
					remoteEnvironmentClass.remoteEnvironmentClassID = "{27577120-375C-4AD0-B348-DE6AF154B69F}";
					remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.ImsImplicit;
					remoteEnvironmentClass.remoteEnvironmentVersion = "REC4";
					remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
					remoteEnvironmentClass.propertyPages = new RECPropertyPage[4];
					remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
					remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
					remoteEnvironmentClass.propertyPages[0].Order = 2;
					remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
					remoteEnvironmentClass.propertyPages[1].Name = "General Page";
					remoteEnvironmentClass.propertyPages[1].Order = 0;
					remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
					remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
					remoteEnvironmentClass.propertyPages[2].Order = 3;
					remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{ED56A89A-5973-11D2-9647-00C04FB904E0}");
					remoteEnvironmentClass.propertyPages[3].Name = "TCP/IP Page";
					remoteEnvironmentClass.propertyPages[3].Order = 1;
					remoteEnvironmentClass.isSupportedByManagedRuntime = false;
					remoteEnvironmentClass.isForNew = false;
					remoteEnvironmentClass.persistentConnectionsSupported = false;
					remoteEnvironmentClass.transactionsAreSupported = false;
					return remoteEnvironmentClass;
				}
				if (RemoteEnvironmentType == RemoteEnvironmentTypes.ImsExplicit)
				{
					RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
					remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterFullName;
					remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterJsonFullName;
					remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterFullName;
					remoteEnvironmentClass.transportFullName = "TranTCP.dll";
					remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemGenericAggregateConverterName;
					remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterName;
					remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId;
					remoteEnvironmentClass.devDefinesGuid = new Guid("{9076ECA4-4DAB-11D2-A06B-00C04FC2E0BB}");
					remoteEnvironmentClass.name = "IMS Explicit";
					remoteEnvironmentClass.transportName = "TCP Transport";
					remoteEnvironmentClass.programmingModelName = "IMS Explicit";
					remoteEnvironmentClass.StateMachineFullName = "Unknown";
					remoteEnvironmentClass.StateMachineName = "Unknown";
					remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemzHostEnvironmentIMS;
					remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemzPlatformName;
					remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemz;
					remoteEnvironmentClass.vendorName = text;
					remoteEnvironmentClass.vendorID = guid;
					remoteEnvironmentClass.programmingModel = ProgrammingModel.IMSExplicit;
					remoteEnvironmentClass.hostEnvironment = HostEnvironment.IMS;
					remoteEnvironmentClass.transport = Transport.TCP;
					remoteEnvironmentClass.remoteEnvironmentClassID = "{F8FED25A-2FBC-41E2-803F-D35CE683964B}";
					remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.ImsExplicit;
					remoteEnvironmentClass.remoteEnvironmentVersion = "REC4";
					remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemzPrimitiveConverterClassId);
					remoteEnvironmentClass.propertyPages = new RECPropertyPage[4];
					remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
					remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
					remoteEnvironmentClass.propertyPages[0].Order = 2;
					remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
					remoteEnvironmentClass.propertyPages[1].Name = "General Page";
					remoteEnvironmentClass.propertyPages[1].Order = 0;
					remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
					remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
					remoteEnvironmentClass.propertyPages[2].Order = 3;
					remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{ED56A89A-5973-11D2-9647-00C04FB904E0}");
					remoteEnvironmentClass.propertyPages[3].Name = "TCP/IP Page";
					remoteEnvironmentClass.propertyPages[3].Order = 1;
					remoteEnvironmentClass.isSupportedByManagedRuntime = false;
					remoteEnvironmentClass.isForNew = false;
					remoteEnvironmentClass.persistentConnectionsSupported = false;
					remoteEnvironmentClass.transactionsAreSupported = false;
					return remoteEnvironmentClass;
				}
				if (RemoteEnvironmentType == RemoteEnvironmentTypes.DistributedProgramCall)
				{
					RemoteEnvironmentClass remoteEnvironmentClass = new RemoteEnvironmentClass();
					remoteEnvironmentClass.aggregateConverterFullName = RemoteEnvironmentClassFactory.SystemiDPCAggregateConverterFullName;
					remoteEnvironmentClass.aggregateConverterJsonFullName = RemoteEnvironmentClassFactory.SystemiDPCAggregateConverterJsonFullName;
					remoteEnvironmentClass.primitiveConverterFullName = RemoteEnvironmentClassFactory.SystemiPrimitiveConverterFullName;
					remoteEnvironmentClass.transportFullName = RemoteEnvironmentClassFactory.DPCTransportFullName;
					remoteEnvironmentClass.aggregateConverterName = RemoteEnvironmentClassFactory.SystemiDPCAggregateConverterName;
					remoteEnvironmentClass.primitiveConverterName = RemoteEnvironmentClassFactory.SystemiPrimitiveConverterName;
					remoteEnvironmentClass.primitiveConverterClassId = RemoteEnvironmentClassFactory.SystemiPrimitiveConverterClassId;
					remoteEnvironmentClass.devDefinesGuid = new Guid("{9076ECA7-4DAB-11D2-A06B-00C04FC2E0BB}");
					remoteEnvironmentClass.name = "Distributed Program Call";
					remoteEnvironmentClass.transportName = "DPC TCP Transport";
					remoteEnvironmentClass.programmingModelName = "DPC";
					remoteEnvironmentClass.StateMachineFullName = RemoteEnvironmentClassFactory.DPCStateMachineFullName;
					remoteEnvironmentClass.StateMachineName = RemoteEnvironmentClassFactory.DPCStateMachineName;
					remoteEnvironmentClass.hostEnvironmentName = RemoteEnvironmentClassFactory.SystemiHostEnvironmentDPC;
					remoteEnvironmentClass.hostPlatformName = RemoteEnvironmentClassFactory.SystemiPlatformName;
					remoteEnvironmentClass.hostType = HostPlatformTypes.PlatformSystemi;
					remoteEnvironmentClass.vendorName = text;
					remoteEnvironmentClass.vendorID = guid;
					remoteEnvironmentClass.programmingModel = ProgrammingModel.DistributedProgramCall;
					remoteEnvironmentClass.hostEnvironment = HostEnvironment.SystemiDistributedProgramCall;
					remoteEnvironmentClass.transport = Transport.TCP;
					remoteEnvironmentClass.remoteEnvironmentClassID = "{BFAD4741-B1CF-4456-89EA-92F851E42A2E}";
					remoteEnvironmentClass.remoteEnvironmentType = RemoteEnvironmentTypes.DistributedProgramCall;
					remoteEnvironmentClass.remoteEnvironmentVersion = "REC5";
					remoteEnvironmentClass.hostLanguages = RemoteEnvironmentClassFactory.GetHostPlatformLanguages(RemoteEnvironmentClassFactory.SystemiPrimitiveConverterClassId);
					remoteEnvironmentClass.propertyPages = new RECPropertyPage[4];
					remoteEnvironmentClass.propertyPages[0] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[0].Identifier = new Guid("{19422082-47B9-11D0-BF45-00AA00BDD5D7}");
					remoteEnvironmentClass.propertyPages[0].Name = "Locale Page";
					remoteEnvironmentClass.propertyPages[0].Order = 2;
					remoteEnvironmentClass.propertyPages[1] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[1].Identifier = new Guid("{47B63F17-4884-11D0-BF45-00AA00BDD5D7}");
					remoteEnvironmentClass.propertyPages[1].Name = "General Page";
					remoteEnvironmentClass.propertyPages[1].Order = 0;
					remoteEnvironmentClass.propertyPages[2] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[2].Identifier = new Guid("{47B63F1A-4884-11D0-BF45-00AA00BDD5D7}");
					remoteEnvironmentClass.propertyPages[2].Name = "Security Page";
					remoteEnvironmentClass.propertyPages[2].Order = 3;
					remoteEnvironmentClass.propertyPages[3] = new RECPropertyPage();
					remoteEnvironmentClass.propertyPages[3].Identifier = new Guid("{ED56A89A-5973-11D2-9647-00C04FB904E0}");
					remoteEnvironmentClass.propertyPages[3].Name = "TCP/IP Page";
					remoteEnvironmentClass.propertyPages[3].Order = 1;
					remoteEnvironmentClass.isSupportedByManagedRuntime = true;
					remoteEnvironmentClass.isForNew = true;
					remoteEnvironmentClass.persistentConnectionsSupported = true;
					remoteEnvironmentClass.transactionsAreSupported = false;
					return remoteEnvironmentClass;
				}
			}
			throw new ArgumentException("MakeRemoteEnvironmentClass");
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x000C3FD9 File Offset: 0x000C21D9
		public static BaseRemoteEnvironment MakeRemoteEnvironment(DynamicRemoteEnvironmentTypes DynamicRemoteEnvironmentType)
		{
			return RemoteEnvironmentClassFactory.MakeRemoteEnvironment(DynamicRemoteEnvironmentType, Enum.GetName(typeof(DynamicRemoteEnvironmentTypes), DynamicRemoteEnvironmentType), null);
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x000C3FF7 File Offset: 0x000C21F7
		public static BaseRemoteEnvironment MakeRemoteEnvironment(DynamicRemoteEnvironmentTypes DynamicRemoteEnvironmentType, string RemoteEnvironmentName)
		{
			return RemoteEnvironmentClassFactory.MakeRemoteEnvironment(DynamicRemoteEnvironmentType, RemoteEnvironmentName, null);
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x000C4001 File Offset: 0x000C2201
		public static void REClassIdFromRETypeName(string RETypeName, out string REClassId, out DynamicRemoteEnvironmentTypes REType)
		{
			if (Enum.TryParse<DynamicRemoteEnvironmentTypes>(RETypeName, out REType))
			{
				RemoteEnvironmentClassFactory.REClassIdFromREType(REType, out REClassId);
				return;
			}
			REClassId = null;
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x000C4018 File Offset: 0x000C2218
		public static void REClassIdFromREType(DynamicRemoteEnvironmentTypes REType, out string REClassId)
		{
			REClassId = null;
			if (REType <= DynamicRemoteEnvironmentTypes.SystemzSocketsUserData)
			{
				if (REType <= DynamicRemoteEnvironmentTypes.HttpUserData)
				{
					if (REType == DynamicRemoteEnvironmentTypes.SnaUserData)
					{
						REClassId = "{55185CC7-42BA-4926-9F8B-513139CA7A61}";
						return;
					}
					if (REType == DynamicRemoteEnvironmentTypes.SnaLink)
					{
						REClassId = "{56690CE3-D72E-4643-98C2-A85E2978CD64}";
						return;
					}
					if (REType != DynamicRemoteEnvironmentTypes.HttpUserData)
					{
						return;
					}
					REClassId = "{B3AA6C63-C1BF-49AE-A154-A39CF8F75E79}";
					return;
				}
				else if (REType <= DynamicRemoteEnvironmentTypes.ImsLu62)
				{
					if (REType == DynamicRemoteEnvironmentTypes.HttpLink)
					{
						REClassId = "{B06A9229-1F92-44FD-A0CB-66403A5C82BD}";
						return;
					}
					if (REType != DynamicRemoteEnvironmentTypes.ImsLu62)
					{
						return;
					}
					REClassId = "{5093418E-2FC6-4363-8EDF-665217E346D9}";
					return;
				}
				else
				{
					if (REType == DynamicRemoteEnvironmentTypes.ImsConnect)
					{
						REClassId = "{0A327B80-AFB4-422F-95FC-94623DA12769}";
						return;
					}
					if (REType != DynamicRemoteEnvironmentTypes.SystemzSocketsUserData)
					{
						return;
					}
					REClassId = "{D75FD330-2A06-4AC9-A9C5-A7F97DF41B96}";
					return;
				}
			}
			else if (REType <= DynamicRemoteEnvironmentTypes.TrmUserData)
			{
				if (REType == DynamicRemoteEnvironmentTypes.SystemzSocketsLink)
				{
					REClassId = "{964D48F9-E4E4-434F-9167-55BA39385BFF}";
					return;
				}
				if (REType == DynamicRemoteEnvironmentTypes.SystemiSocketsUserData)
				{
					REClassId = "{50906E71-C59C-47EC-9D8C-D0C8C155933B}";
					return;
				}
				if (REType != DynamicRemoteEnvironmentTypes.TrmUserData)
				{
					return;
				}
				REClassId = "{7F4BA82E-54CC-451B-99B5-EAAD9BD0365F}";
				return;
			}
			else if (REType <= DynamicRemoteEnvironmentTypes.ElmUserData)
			{
				if (REType == DynamicRemoteEnvironmentTypes.TrmLink)
				{
					REClassId = "{C348E268-D254-44B3-90A2-F2AD2139BEA9}";
					return;
				}
				if (REType != DynamicRemoteEnvironmentTypes.ElmUserData)
				{
					return;
				}
				REClassId = "{2AD1C7FD-3FFE-4967-A9CC-050AD6A336F0}";
				return;
			}
			else
			{
				if (REType == DynamicRemoteEnvironmentTypes.ElmLink)
				{
					REClassId = "{20CFA202-8BF2-4C1A-8126-AB759E913072}";
					return;
				}
				if (REType != DynamicRemoteEnvironmentTypes.DistributedProgramCall)
				{
					return;
				}
				REClassId = "{BFAD4741-B1CF-4456-89EA-92F851E42A2E}";
				return;
			}
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x000C4150 File Offset: 0x000C2350
		public static DynamicRemoteEnvironmentTypes DynamicRETypeFromREClassId(string REClassId)
		{
			string text;
			if (REClassId[0] != '{')
			{
				text = "{" + REClassId.ToUpperInvariant() + "}";
			}
			else
			{
				text = REClassId.ToUpperInvariant();
			}
			if (text != null)
			{
				uint num = <bba5a8c5-e874-495c-b8ae-10208dde68db><PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2513043238U)
				{
					if (num <= 688618992U)
					{
						if (num != 308212143U)
						{
							if (num != 651282189U)
							{
								if (num == 688618992U)
								{
									if (text == "{20CFA202-8BF2-4C1A-8126-AB759E913072}")
									{
										return DynamicRemoteEnvironmentTypes.ElmLink;
									}
								}
							}
							else if (text == "{56690CE3-D72E-4643-98C2-A85E2978CD64}")
							{
								return DynamicRemoteEnvironmentTypes.SnaLink;
							}
						}
						else if (text == "{B3AA6C63-C1BF-49AE-A154-A39CF8F75E79}")
						{
							return DynamicRemoteEnvironmentTypes.HttpUserData;
						}
					}
					else if (num <= 1870857634U)
					{
						if (num != 1329120709U)
						{
							if (num == 1870857634U)
							{
								if (text == "{BFAD4741-B1CF-4456-89EA-92F851E42A2E}")
								{
									return DynamicRemoteEnvironmentTypes.DistributedProgramCall;
								}
							}
						}
						else if (text == "{50906E71-C59C-47EC-9D8C-D0C8C155933B}")
						{
							return DynamicRemoteEnvironmentTypes.SystemiSocketsUserData;
						}
					}
					else if (num != 2136304754U)
					{
						if (num == 2513043238U)
						{
							if (text == "{964D48F9-E4E4-434F-9167-55BA39385BFF}")
							{
								return DynamicRemoteEnvironmentTypes.SystemzSocketsLink;
							}
						}
					}
					else if (text == "{5093418E-2FC6-4363-8EDF-665217E346D9}")
					{
						return DynamicRemoteEnvironmentTypes.ImsLu62;
					}
				}
				else if (num <= 3357042628U)
				{
					if (num != 2626132833U)
					{
						if (num != 2955836348U)
						{
							if (num == 3357042628U)
							{
								if (text == "{B06A9229-1F92-44FD-A0CB-66403A5C82BD}")
								{
									return DynamicRemoteEnvironmentTypes.HttpLink;
								}
							}
						}
						else if (text == "{2AD1C7FD-3FFE-4967-A9CC-050AD6A336F0}")
						{
							return DynamicRemoteEnvironmentTypes.ElmUserData;
						}
					}
					else if (text == "{C348E268-D254-44B3-90A2-F2AD2139BEA9}")
					{
						return DynamicRemoteEnvironmentTypes.TrmLink;
					}
				}
				else if (num <= 3831479908U)
				{
					if (num != 3404714738U)
					{
						if (num == 3831479908U)
						{
							if (text == "{55185CC7-42BA-4926-9F8B-513139CA7A61}")
							{
								return DynamicRemoteEnvironmentTypes.SnaUserData;
							}
						}
					}
					else if (text == "{7F4BA82E-54CC-451B-99B5-EAAD9BD0365F}")
					{
						return DynamicRemoteEnvironmentTypes.TrmUserData;
					}
				}
				else if (num != 4169518892U)
				{
					if (num == 4230191345U)
					{
						if (text == "{D75FD330-2A06-4AC9-A9C5-A7F97DF41B96}")
						{
							return DynamicRemoteEnvironmentTypes.SystemzSocketsUserData;
						}
					}
				}
				else if (text == "{0A327B80-AFB4-422F-95FC-94623DA12769}")
				{
					return DynamicRemoteEnvironmentTypes.ImsConnect;
				}
			}
			throw new ArgumentException("DynamicRETypeFromREClassId");
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x000C43D8 File Offset: 0x000C25D8
		public static BaseRemoteEnvironment MakeRemoteEnvironment(DynamicRemoteEnvironmentTypes DynamicRemoteEnvironmentType, string RemoteEnvironmentName, string connStr)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();
			dbConnectionStringBuilder.ConnectionString = connStr;
			if (DynamicRemoteEnvironmentType <= DynamicRemoteEnvironmentTypes.SystemzSocketsUserData)
			{
				if (DynamicRemoteEnvironmentType <= DynamicRemoteEnvironmentTypes.HttpUserData)
				{
					if (DynamicRemoteEnvironmentType == DynamicRemoteEnvironmentTypes.SnaUserData)
					{
						SnaUserDataRemoteEnvironment snaUserDataRemoteEnvironment = new SnaUserDataRemoteEnvironment();
						snaUserDataRemoteEnvironment.Name = RemoteEnvironmentName;
						RemoteEnvironmentClass remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.SnaUserData);
						snaUserDataRemoteEnvironment.RemoteEnvironmentClass = remoteEnvironmentClass;
						snaUserDataRemoteEnvironment.TimeOut = 0;
						snaUserDataRemoteEnvironment.CodePage = CultureInfo.CurrentCulture.TextInfo.EBCDICCodePage;
						snaUserDataRemoteEnvironment.ModeName = "PA62TKNU";
						RemoteEnvironmentHelper.FromConnectionProperties(snaUserDataRemoteEnvironment, dbConnectionStringBuilder);
						return snaUserDataRemoteEnvironment;
					}
					if (DynamicRemoteEnvironmentType == DynamicRemoteEnvironmentTypes.SnaLink)
					{
						SnaLinkRemoteEnvironment snaLinkRemoteEnvironment = new SnaLinkRemoteEnvironment();
						snaLinkRemoteEnvironment.Name = RemoteEnvironmentName;
						RemoteEnvironmentClass remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.SnaLink);
						snaLinkRemoteEnvironment.RemoteEnvironmentClass = remoteEnvironmentClass;
						snaLinkRemoteEnvironment.TimeOut = 0;
						snaLinkRemoteEnvironment.CodePage = CultureInfo.CurrentCulture.TextInfo.EBCDICCodePage;
						snaLinkRemoteEnvironment.MirrorTransactionId = "CSMI";
						snaLinkRemoteEnvironment.ModeName = "PA62TKNU";
						RemoteEnvironmentHelper.FromConnectionProperties(snaLinkRemoteEnvironment, dbConnectionStringBuilder);
						return snaLinkRemoteEnvironment;
					}
					if (DynamicRemoteEnvironmentType == DynamicRemoteEnvironmentTypes.HttpUserData)
					{
						HttpUserDataRemoteEnvironment httpUserDataRemoteEnvironment = new HttpUserDataRemoteEnvironment();
						httpUserDataRemoteEnvironment.Name = RemoteEnvironmentName;
						RemoteEnvironmentClass remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.HttpUserData);
						httpUserDataRemoteEnvironment.RemoteEnvironmentClass = remoteEnvironmentClass;
						httpUserDataRemoteEnvironment.TimeOut = 0;
						httpUserDataRemoteEnvironment.CodePage = CultureInfo.CurrentCulture.TextInfo.EBCDICCodePage;
						RemoteEnvironmentHelper.FromConnectionProperties(httpUserDataRemoteEnvironment, dbConnectionStringBuilder);
						return httpUserDataRemoteEnvironment;
					}
				}
				else if (DynamicRemoteEnvironmentType <= DynamicRemoteEnvironmentTypes.ImsLu62)
				{
					if (DynamicRemoteEnvironmentType == DynamicRemoteEnvironmentTypes.HttpLink)
					{
						HttpLinkRemoteEnvironment httpLinkRemoteEnvironment = new HttpLinkRemoteEnvironment();
						httpLinkRemoteEnvironment.Name = RemoteEnvironmentName;
						RemoteEnvironmentClass remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.HttpLink);
						httpLinkRemoteEnvironment.RemoteEnvironmentClass = remoteEnvironmentClass;
						httpLinkRemoteEnvironment.TimeOut = 0;
						httpLinkRemoteEnvironment.CodePage = CultureInfo.CurrentCulture.TextInfo.EBCDICCodePage;
						RemoteEnvironmentHelper.FromConnectionProperties(httpLinkRemoteEnvironment, dbConnectionStringBuilder);
						return httpLinkRemoteEnvironment;
					}
					if (DynamicRemoteEnvironmentType == DynamicRemoteEnvironmentTypes.ImsLu62)
					{
						SnaUserDataRemoteEnvironment snaUserDataRemoteEnvironment2 = new SnaUserDataRemoteEnvironment();
						snaUserDataRemoteEnvironment2.Name = RemoteEnvironmentName;
						RemoteEnvironmentClass remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.ImsLu62);
						snaUserDataRemoteEnvironment2.RemoteEnvironmentClass = remoteEnvironmentClass;
						snaUserDataRemoteEnvironment2.TimeOut = 0;
						snaUserDataRemoteEnvironment2.CodePage = CultureInfo.CurrentCulture.TextInfo.EBCDICCodePage;
						snaUserDataRemoteEnvironment2.ModeName = "PA62TKNU";
						RemoteEnvironmentHelper.FromConnectionProperties(snaUserDataRemoteEnvironment2, dbConnectionStringBuilder);
						return snaUserDataRemoteEnvironment2;
					}
				}
				else
				{
					if (DynamicRemoteEnvironmentType == DynamicRemoteEnvironmentTypes.ImsConnect)
					{
						ImsConnectRemoteEnvironment imsConnectRemoteEnvironment = new ImsConnectRemoteEnvironment();
						imsConnectRemoteEnvironment.Name = RemoteEnvironmentName;
						RemoteEnvironmentClass remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.ImsConnect);
						imsConnectRemoteEnvironment.RemoteEnvironmentClass = remoteEnvironmentClass;
						imsConnectRemoteEnvironment.TimeOut = 0;
						imsConnectRemoteEnvironment.CodePage = CultureInfo.CurrentCulture.TextInfo.EBCDICCodePage;
						RemoteEnvironmentHelper.FromConnectionProperties(imsConnectRemoteEnvironment, dbConnectionStringBuilder);
						return imsConnectRemoteEnvironment;
					}
					if (DynamicRemoteEnvironmentType == DynamicRemoteEnvironmentTypes.SystemzSocketsUserData)
					{
						TcpRemoteEnvironment tcpRemoteEnvironment = new TcpRemoteEnvironment();
						tcpRemoteEnvironment.Name = RemoteEnvironmentName;
						RemoteEnvironmentClass remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.SystemzSocketsUserData);
						tcpRemoteEnvironment.RemoteEnvironmentClass = remoteEnvironmentClass;
						tcpRemoteEnvironment.TimeOut = 0;
						tcpRemoteEnvironment.CodePage = CultureInfo.CurrentCulture.TextInfo.EBCDICCodePage;
						RemoteEnvironmentHelper.FromConnectionProperties(tcpRemoteEnvironment, dbConnectionStringBuilder);
						return tcpRemoteEnvironment;
					}
				}
			}
			else if (DynamicRemoteEnvironmentType <= DynamicRemoteEnvironmentTypes.TrmUserData)
			{
				if (DynamicRemoteEnvironmentType == DynamicRemoteEnvironmentTypes.SystemzSocketsLink)
				{
					TcpRemoteEnvironment tcpRemoteEnvironment2 = new TcpRemoteEnvironment();
					tcpRemoteEnvironment2.Name = RemoteEnvironmentName;
					RemoteEnvironmentClass remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.SystemzSocketsLink);
					tcpRemoteEnvironment2.RemoteEnvironmentClass = remoteEnvironmentClass;
					tcpRemoteEnvironment2.TimeOut = 0;
					tcpRemoteEnvironment2.CodePage = CultureInfo.CurrentCulture.TextInfo.EBCDICCodePage;
					RemoteEnvironmentHelper.FromConnectionProperties(tcpRemoteEnvironment2, dbConnectionStringBuilder);
					return tcpRemoteEnvironment2;
				}
				if (DynamicRemoteEnvironmentType == DynamicRemoteEnvironmentTypes.SystemiSocketsUserData)
				{
					TcpRemoteEnvironment tcpRemoteEnvironment3 = new TcpRemoteEnvironment();
					tcpRemoteEnvironment3.Name = RemoteEnvironmentName;
					RemoteEnvironmentClass remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.SystemiSocketsUserData);
					tcpRemoteEnvironment3.RemoteEnvironmentClass = remoteEnvironmentClass;
					tcpRemoteEnvironment3.TimeOut = 0;
					tcpRemoteEnvironment3.CodePage = CultureInfo.CurrentCulture.TextInfo.EBCDICCodePage;
					RemoteEnvironmentHelper.FromConnectionProperties(tcpRemoteEnvironment3, dbConnectionStringBuilder);
					return tcpRemoteEnvironment3;
				}
				if (DynamicRemoteEnvironmentType == DynamicRemoteEnvironmentTypes.TrmUserData)
				{
					TrmUserDataRemoteEnvironment trmUserDataRemoteEnvironment = new TrmUserDataRemoteEnvironment();
					trmUserDataRemoteEnvironment.Name = RemoteEnvironmentName;
					RemoteEnvironmentClass remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.TrmUserData);
					trmUserDataRemoteEnvironment.RemoteEnvironmentClass = remoteEnvironmentClass;
					trmUserDataRemoteEnvironment.TimeOut = 0;
					trmUserDataRemoteEnvironment.CodePage = CultureInfo.CurrentCulture.TextInfo.EBCDICCodePage;
					trmUserDataRemoteEnvironment.ConcurrentServerTransactionId = "MSCS";
					RemoteEnvironmentHelper.FromConnectionProperties(trmUserDataRemoteEnvironment, dbConnectionStringBuilder);
					return trmUserDataRemoteEnvironment;
				}
			}
			else if (DynamicRemoteEnvironmentType <= DynamicRemoteEnvironmentTypes.ElmUserData)
			{
				if (DynamicRemoteEnvironmentType == DynamicRemoteEnvironmentTypes.TrmLink)
				{
					TrmLinkRemoteEnvironment trmLinkRemoteEnvironment = new TrmLinkRemoteEnvironment();
					trmLinkRemoteEnvironment.Name = RemoteEnvironmentName;
					RemoteEnvironmentClass remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.TrmLink);
					trmLinkRemoteEnvironment.RemoteEnvironmentClass = remoteEnvironmentClass;
					trmLinkRemoteEnvironment.TimeOut = 0;
					trmLinkRemoteEnvironment.CodePage = CultureInfo.CurrentCulture.TextInfo.EBCDICCodePage;
					trmLinkRemoteEnvironment.ConcurrentServerTransactionId = "MSCS";
					RemoteEnvironmentHelper.FromConnectionProperties(trmLinkRemoteEnvironment, dbConnectionStringBuilder);
					return trmLinkRemoteEnvironment;
				}
				if (DynamicRemoteEnvironmentType == DynamicRemoteEnvironmentTypes.ElmUserData)
				{
					ElmUserDataRemoteEnvironment elmUserDataRemoteEnvironment = new ElmUserDataRemoteEnvironment();
					elmUserDataRemoteEnvironment.Name = RemoteEnvironmentName;
					RemoteEnvironmentClass remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.ElmUserData);
					elmUserDataRemoteEnvironment.RemoteEnvironmentClass = remoteEnvironmentClass;
					elmUserDataRemoteEnvironment.TimeOut = 0;
					elmUserDataRemoteEnvironment.CodePage = CultureInfo.CurrentCulture.TextInfo.EBCDICCodePage;
					RemoteEnvironmentHelper.FromConnectionProperties(elmUserDataRemoteEnvironment, dbConnectionStringBuilder);
					return elmUserDataRemoteEnvironment;
				}
			}
			else
			{
				if (DynamicRemoteEnvironmentType == DynamicRemoteEnvironmentTypes.ElmLink)
				{
					ElmLinkRemoteEnvironment elmLinkRemoteEnvironment = new ElmLinkRemoteEnvironment();
					elmLinkRemoteEnvironment.Name = RemoteEnvironmentName;
					RemoteEnvironmentClass remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.ElmLink);
					elmLinkRemoteEnvironment.RemoteEnvironmentClass = remoteEnvironmentClass;
					elmLinkRemoteEnvironment.TimeOut = 0;
					elmLinkRemoteEnvironment.CodePage = CultureInfo.CurrentCulture.TextInfo.EBCDICCodePage;
					RemoteEnvironmentHelper.FromConnectionProperties(elmLinkRemoteEnvironment, dbConnectionStringBuilder);
					return elmLinkRemoteEnvironment;
				}
				if (DynamicRemoteEnvironmentType == DynamicRemoteEnvironmentTypes.DistributedProgramCall)
				{
					DpcRemoteEnvironment dpcRemoteEnvironment = new DpcRemoteEnvironment();
					dpcRemoteEnvironment.Name = RemoteEnvironmentName;
					RemoteEnvironmentClass remoteEnvironmentClass = RemoteEnvironmentClassFactory.MakeRemoteEnvironmentClass(RemoteEnvironmentTypes.DistributedProgramCall);
					dpcRemoteEnvironment.RemoteEnvironmentClass = remoteEnvironmentClass;
					dpcRemoteEnvironment.TimeOut = 0;
					dpcRemoteEnvironment.CodePage = CultureInfo.CurrentCulture.TextInfo.EBCDICCodePage;
					RemoteEnvironmentHelper.FromConnectionProperties(dpcRemoteEnvironment, dbConnectionStringBuilder);
					return dpcRemoteEnvironment;
				}
			}
			throw new ArgumentException("MakeRemoteEnvironment");
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x000C48B4 File Offset: 0x000C2AB4
		public static Hashtable ParseConnectionString(string connStr)
		{
			Hashtable hashtable = new Hashtable();
			if (!string.IsNullOrEmpty(connStr))
			{
				char[] array = new char[] { ';' };
				char[] array2 = new char[] { '=' };
				string[] array3 = connStr.Split(array, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < array3.Length; i++)
				{
					string[] array4 = array3[i].Split(array2, StringSplitOptions.RemoveEmptyEntries);
					if (array4.Length == 2)
					{
						hashtable[array4[0].Trim()] = array4[1].Trim();
					}
				}
			}
			return hashtable;
		}

		// Token: 0x0400214F RID: 8527
		private static string[] vendorNames;

		// Token: 0x04002150 RID: 8528
		private static Guid[] vendorIDs;

		// Token: 0x04002151 RID: 8529
		private static string[] vendorRECFactoryNames;

		// Token: 0x04002152 RID: 8530
		private static int[] vendorIndexs;

		// Token: 0x04002153 RID: 8531
		private static IRemoteEnvironmentClassFactory[] vendorRECFactoryInstances;

		// Token: 0x04002154 RID: 8532
		private static string ourVendorName = "Microsoft";

		// Token: 0x04002155 RID: 8533
		private static Guid ourVendorID = new Guid("A779FD35-11C3-42BA-AB21-EE3EA335267C");

		// Token: 0x04002156 RID: 8534
		private static string SystemzCOBOLImporter = "Microsoft.HostIntegration.TIDesigner.COBOLImporter.dll:Microsoft.HostIntegration.TIDesigner.ISourceImpMSCOBOL";

		// Token: 0x04002157 RID: 8535
		private static string SystemzCOBOLExporter = "{62AAEFC4-5860-11D0-A874-00C04FC29A5D}";

		// Token: 0x04002158 RID: 8536
		private static string SystemiRPGExporter = "{DF9BA3C7-989F-48c9-AC74-50E815EFCF35}";

		// Token: 0x04002159 RID: 8537
		private static string SystemiRPGImporter = "Microsoft.HostIntegration.TIDesigner.RPGImporter.dll:Microsoft.HostIntegration.TIDesigner.ISourceImpMSRPG";

		// Token: 0x0400215A RID: 8538
		private static string SystemiCOBOLImporter = "Microsoft.HostIntegration.TIDesigner.AS400COBOLImporter.dll:Microsoft.HostIntegration.TIDesigner.ISourceImpMSAS400COBOL";

		// Token: 0x0400215B RID: 8539
		private static string ELMTransportFullName;

		// Token: 0x0400215C RID: 8540
		private static string TRMTransportFullName;

		// Token: 0x0400215D RID: 8541
		private static string IMSConnectTransportFullName;

		// Token: 0x0400215E RID: 8542
		private static string DPCTransportFullName;

		// Token: 0x0400215F RID: 8543
		private static string SNATransportFullName;

		// Token: 0x04002160 RID: 8544
		private static string TCPTransportFullName;

		// Token: 0x04002161 RID: 8545
		private static string HTTPTransportFullName;

		// Token: 0x04002162 RID: 8546
		private static string GenericLinkStateMachineFullName;

		// Token: 0x04002163 RID: 8547
		private static string GenericLinkStateMachineName = "GenericLinkStateMachine";

		// Token: 0x04002164 RID: 8548
		private static string GenericUserDataStateMachineFullName;

		// Token: 0x04002165 RID: 8549
		private static string GenericUserDataStateMachineName = "GenericUserDataStateMachine";

		// Token: 0x04002166 RID: 8550
		private static string IMSConnectStateMachineFullName;

		// Token: 0x04002167 RID: 8551
		private static string IMSConnectStateMachineName = "IMSConnectDataStateMachine";

		// Token: 0x04002168 RID: 8552
		private static string DPCStateMachineFullName;

		// Token: 0x04002169 RID: 8553
		private static string DPCStateMachineName = "Distributed Program Call StateMachine";

		// Token: 0x0400216A RID: 8554
		private static string TCPStateMachineFullName;

		// Token: 0x0400216B RID: 8555
		private static string TCPStateMachineName = "TCPStateMachine";

		// Token: 0x0400216C RID: 8556
		public static string SystemzPlatformName = "System z";

		// Token: 0x0400216D RID: 8557
		public static string SystemiPlatformName = "System i";

		// Token: 0x0400216E RID: 8558
		public static string SystemzProgrammingLanguageCOBOL = "COBOL";

		// Token: 0x0400216F RID: 8559
		public static string SystemzProgrammingLanguagePLI = "PLI";

		// Token: 0x04002170 RID: 8560
		public static string SystemiProgrammingLanguageCOBOL = "COBOL";

		// Token: 0x04002171 RID: 8561
		public static string SystemiProgrammingLanguageRPG = "RPG";

		// Token: 0x04002172 RID: 8562
		public static string SystemzCOBOLFileExtenstions = "*.COB;*.CBL";

		// Token: 0x04002173 RID: 8563
		public static string SystemzPLIFileExtenstions = "*.PLI";

		// Token: 0x04002174 RID: 8564
		public static string SystemiCOBOLFileExtenstions = "*.COB;*.CBL";

		// Token: 0x04002175 RID: 8565
		public static string SystemiRPGFileExtenstions = "*.MBR;*.RPG";

		// Token: 0x04002176 RID: 8566
		public static string SystemzCOBOLDevLangExtension = "IBMCOBOL.XML";

		// Token: 0x04002177 RID: 8567
		public static string SystemzPLIDevLangExtension = "IBMPLI.XML";

		// Token: 0x04002178 RID: 8568
		public static string SystemiCOBOLDevLangExtension = "IBMCOBOL.XML";

		// Token: 0x04002179 RID: 8569
		public static string SystemiRPGDevLangExtension = "IBMRPG400.XML";

		// Token: 0x0400217A RID: 8570
		public static string SystemzHostEnvironmentCICS = "CICS";

		// Token: 0x0400217B RID: 8571
		public static string SystemzHostEnvironmentIMS = "IMS";

		// Token: 0x0400217C RID: 8572
		public static string SystemiHostEnvironmentDPC = "System i Distributed Program Call";

		// Token: 0x0400217D RID: 8573
		public static string SystemGenericAggregateConverterTypeName = "Microsoft.HostIntegration.TI.AggregateConverter";

		// Token: 0x0400217E RID: 8574
		public static string SystemGenericAggregateConverterFullName;

		// Token: 0x0400217F RID: 8575
		public static string SystemGenericAggregateConverterName = "Aggregate data conversion non-specific";

		// Token: 0x04002180 RID: 8576
		public static string SystemiDPCAggregateConverterName = "Aggregate data conversion for Systemi Distributed Program Call";

		// Token: 0x04002181 RID: 8577
		public static string SystemiDPCAggregateConverterFullName;

		// Token: 0x04002182 RID: 8578
		public static string SystemGenericAggregateConverterJsonFullName;

		// Token: 0x04002183 RID: 8579
		public static string SystemiDPCAggregateConverterJsonFullName;

		// Token: 0x04002184 RID: 8580
		public static string SystemzPrimitiveConverterName = "Data conversion for IBM System z hardware";

		// Token: 0x04002185 RID: 8581
		public static string SystemzPrimitiveConverterTypeName = "Microsoft.HostIntegration.Common.BasePrimitiveConverter";

		// Token: 0x04002186 RID: 8582
		public static string SystemzPrimitiveConverterFullName;

		// Token: 0x04002187 RID: 8583
		public static string SystemiPrimitiveConverterName = "Data conversion for IBM System i hardware";

		// Token: 0x04002188 RID: 8584
		public static string SystemiPrimitiveConverterTypeName = "Microsoft.HostIntegration.Common.SystemIPrimitiveConverter";

		// Token: 0x04002189 RID: 8585
		public static string SystemiPrimitiveConverterFullName;

		// Token: 0x0400218A RID: 8586
		public static string SystemzPrimitiveConverterClassId = "4C3B40BD-91F2-406D-A574-E125FEB400E8";

		// Token: 0x0400218B RID: 8587
		public static string SystemiPrimitiveConverterClassId = "5F00230A-697A-441B-92B7-E57E9B0B305F";

		// Token: 0x0400218C RID: 8588
		public const string ELMFlowControlClassId = "C1085505-1748-4B8F-B76B-D9E837EFEC39";

		// Token: 0x0400218D RID: 8589
		public static string ELMFlowControlFullName;

		// Token: 0x0400218E RID: 8590
		public const string TRMFlowControlClassId = "68E6A551-D4C4-43D3-A835-E5592884C4CF";

		// Token: 0x0400218F RID: 8591
		public static string TRMFlowControlFullName;

		// Token: 0x04002190 RID: 8592
		public const string DPLFlowControlClassId = "349C11FB-1F25-49FF-9504-0BA4CB21BBFA";

		// Token: 0x04002191 RID: 8593
		public static string DPLFlowControlFullName;

		// Token: 0x04002192 RID: 8594
		public const string EndPointFlowControlClassId = "E2BECFEF-659D-4648-8100-3060F5A81B05";

		// Token: 0x04002193 RID: 8595
		public static string EndPointFlowControlFullName;

		// Token: 0x04002194 RID: 8596
		public const string HTTPFlowControlClassId = "739A64BE-857B-4ABF-843A-1F0F87770EB8";

		// Token: 0x04002195 RID: 8597
		public static string HTTPFlowControlFullName;

		// Token: 0x04002196 RID: 8598
		public const string UserDataFlowControlClassId = "E8FD3C5B-46F3-48FB-9EBF-52BE5CC897C5";

		// Token: 0x04002197 RID: 8599
		public static string UserDataFlowControlFullName;

		// Token: 0x04002198 RID: 8600
		public const string DynamicRETypeClassIdELMLink = "{20CFA202-8BF2-4C1A-8126-AB759E913072}";

		// Token: 0x04002199 RID: 8601
		public const string DynamicRETypeClassIdELMUserData = "{2AD1C7FD-3FFE-4967-A9CC-050AD6A336F0}";

		// Token: 0x0400219A RID: 8602
		public const string DynamicRETypeClassIdTRMLink = "{C348E268-D254-44B3-90A2-F2AD2139BEA9}";

		// Token: 0x0400219B RID: 8603
		public const string DynamicRETypeClassIdTRMUserData = "{7F4BA82E-54CC-451B-99B5-EAAD9BD0365F}";

		// Token: 0x0400219C RID: 8604
		public const string DynamicRETypeClassIdHttpLink = "{B06A9229-1F92-44FD-A0CB-66403A5C82BD}";

		// Token: 0x0400219D RID: 8605
		public const string DynamicRETypeClassIdHttpUserData = "{B3AA6C63-C1BF-49AE-A154-A39CF8F75E79}";

		// Token: 0x0400219E RID: 8606
		public const string DynamicRETypeClassIdIMSConnect = "{0A327B80-AFB4-422F-95FC-94623DA12769}";

		// Token: 0x0400219F RID: 8607
		public const string DynamicRETypeClassIdSNALink = "{56690CE3-D72E-4643-98C2-A85E2978CD64}";

		// Token: 0x040021A0 RID: 8608
		public const string DynamicRETypeClassIdSNAUserData = "{55185CC7-42BA-4926-9F8B-513139CA7A61}";

		// Token: 0x040021A1 RID: 8609
		public const string DynamicRETypeClassIdIMSLU62 = "{5093418E-2FC6-4363-8EDF-665217E346D9}";

		// Token: 0x040021A2 RID: 8610
		public const string DynamicRETypeClassIdDistributedProgramCall = "{BFAD4741-B1CF-4456-89EA-92F851E42A2E}";

		// Token: 0x040021A3 RID: 8611
		public const string DynamicRETypeClassIdSystemzSocketsLink = "{964D48F9-E4E4-434F-9167-55BA39385BFF}";

		// Token: 0x040021A4 RID: 8612
		public const string DynamicRETypeClassIdSystemzSocketsUserData = "{D75FD330-2A06-4AC9-A9C5-A7F97DF41B96}";

		// Token: 0x040021A5 RID: 8613
		public const string DynamicRETypeClassIdSystemiSocketsUserData = "{50906E71-C59C-47EC-9D8C-D0C8C155933B}";

		// Token: 0x040021A6 RID: 8614
		public const string TIGlobalsAssemblyName = "Microsoft.HostIntegration.TI.Globals";

		// Token: 0x040021A7 RID: 8615
		private RemoteEnvironmentClass[] remoteEnvironmentClasses;
	}
}
