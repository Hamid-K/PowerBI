using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip;
using Microsoft.HostIntegration.TI.Linq;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000751 RID: 1873
	public static class HipImport
	{
		// Token: 0x06003B5D RID: 15197 RVA: 0x000C7594 File Offset: 0x000C5794
		public static List<string> GetExportedConfigurationComputers(string tiExportedFile)
		{
			List<string> list = new List<string>();
			try
			{
				XmlReader xmlReader = XmlReader.Create(tiExportedFile, new XmlReaderSettings
				{
					DtdProcessing = DtdProcessing.Prohibit,
					XmlResolver = null
				});
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.XmlResolver = null;
				xmlDocument.Load(xmlReader);
				xmlReader.Close();
				foreach (object obj in xmlDocument.SelectSingleNode("/hipConfig/computerSpecific/computers").ChildNodes)
				{
					foreach (object obj2 in ((XmlNode)obj).ChildNodes)
					{
						XmlNode xmlNode = (XmlNode)obj2;
						string name = xmlNode.Name;
						if (name != null && name == "Name")
						{
							list.Add(xmlNode.InnerText);
						}
					}
				}
			}
			catch (Exception)
			{
				throw new Exception("File was not a valid TI Manager Exported HIP Configuration XML File.");
			}
			return list;
		}

		// Token: 0x06003B5E RID: 15198 RVA: 0x000C76B8 File Offset: 0x000C58B8
		public static ImportInformation Import(string tiExportedFile, string computerName)
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			ImportInformation importInformation = new ImportInformation();
			importInformation.Warnings = list;
			importInformation.Errors = list2;
			importInformation.HipConfigurationSection = null;
			try
			{
				HipImport.ImportHipDataBase(tiExportedFile, importInformation);
				ImportedHIPDatabase importedDatabase = importInformation.ImportedDatabase;
				Computer computer = null;
				foreach (Computer computer2 in importedDatabase.Computers)
				{
					if (string.Compare(computer2.Name, computerName, StringComparison.OrdinalIgnoreCase) == 0)
					{
						computer = computer2;
						break;
					}
				}
				if (computer == null)
				{
					throw new ApplicationException("Computer " + computerName + " is not in the exported information");
				}
				importInformation.ReplacementComputer = computer;
				importInformation.HipConfigurationSection = new HipConfigurationSectionHandler();
				importInformation.HipConfigurationSection.Cache = null;
				ReadOrder readOrder = new ReadOrder();
				readOrder.AppConfig = HipConfigurationPriority.First;
				readOrder.Cache = HipConfigurationPriority.Unused;
				importInformation.HipConfigurationSection.ReadOrder = readOrder;
				HipImport.CheckHostEnvironments(importInformation);
				HipImport.CheckObjects(importInformation);
				HipImport.CheckSecurityPolicies(importInformation);
				HipImport.CheckImplementingAssemblyDirectories(importInformation);
				HipImport.CheckLesAndLeeps(importInformation);
				HipImport.CheckDeterminants(importInformation);
				Dictionary<int, DeterminantInformation> dictionary = new Dictionary<int, DeterminantInformation>();
				foreach (Determinant determinant in importInformation.UsedDeterminants.Values)
				{
					int type = determinant.Type;
					if (!dictionary.ContainsKey(type))
					{
						dictionary.Add(type, new DeterminantInformation());
					}
					DeterminantInformation determinantInformation = dictionary[type];
					determinantInformation.Determinants.Add(determinant);
					LEEndpoint leendpoint = determinant.LEEndpoint;
					if (!determinantInformation.IdToEndpoints.ContainsKey(leendpoint.Identity))
					{
						determinantInformation.IdToEndpoints.Add(leendpoint.Identity, leendpoint);
					}
					foreach (HEPermission hepermission in determinant.View.HEPermissions)
					{
						LinqHostEnvironment hostEnvironment = hepermission.HostEnvironment;
						if (!determinantInformation.IdToHes.ContainsKey(hostEnvironment.Identity) && importInformation.ValidHostEnvironments.ContainsKey(hostEnvironment.Identity))
						{
							determinantInformation.IdToHes.Add(hostEnvironment.Identity, hostEnvironment);
						}
					}
				}
				importInformation.DeterminantTypeToDeterminantInformations = dictionary;
				HipImport.FilterHostEnvironments(importInformation);
				HipImport.FilterObjects(importInformation);
				HipImport.FilterSecurityPolicies(importInformation);
				HipImport.AddHostEnvironments(importInformation);
				HipImport.AddObjects(importInformation);
				HipImport.AddSecurityPolicies(importInformation);
				HipImport.AddServices(importInformation);
			}
			catch (ApplicationException ex)
			{
				list2.Add(ex.Message);
				importInformation.HipConfigurationSection = null;
			}
			return importInformation;
		}

		// Token: 0x06003B5F RID: 15199 RVA: 0x000C799C File Offset: 0x000C5B9C
		private static void ImportHipDataBase(string tiExportedFile, ImportInformation importInformation)
		{
			List<string> warnings = importInformation.Warnings;
			List<string> errors = importInformation.Errors;
			ImportedHIPDatabase importedHIPDatabase = new ImportedHIPDatabase();
			using (FileStream fileStream = new FileStream(tiExportedFile, FileMode.Open))
			{
				XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas
				{
					MaxArrayLength = 1000000
				});
				DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(LocalEnvironment));
				DataContractSerializer dataContractSerializer2 = new DataContractSerializer(typeof(LEEndpoint));
				DataContractSerializer dataContractSerializer3 = new DataContractSerializer(typeof(LinqHostEnvironment));
				DataContractSerializer dataContractSerializer4 = new DataContractSerializer(typeof(SecurityPolicy));
				DataContractSerializer dataContractSerializer5 = new DataContractSerializer(typeof(AffiliatedApplication));
				DataContractSerializer dataContractSerializer6 = new DataContractSerializer(typeof(Microsoft.HostIntegration.TI.Linq.Object));
				DataContractSerializer dataContractSerializer7 = new DataContractSerializer(typeof(TIMFile));
				DataContractSerializer dataContractSerializer8 = new DataContractSerializer(typeof(Method));
				DataContractSerializer dataContractSerializer9 = new DataContractSerializer(typeof(View));
				DataContractSerializer dataContractSerializer10 = new DataContractSerializer(typeof(Determinant));
				DataContractSerializer dataContractSerializer11 = new DataContractSerializer(typeof(HEPermission));
				DataContractSerializer dataContractSerializer12 = new DataContractSerializer(typeof(Computer));
				DataContractSerializer dataContractSerializer13 = new DataContractSerializer(typeof(Application));
				DataContractSerializer dataContractSerializer14 = new DataContractSerializer(typeof(Listener));
				bool flag = xmlDictionaryReader.Read();
				while (flag)
				{
					if (xmlDictionaryReader.NodeType == XmlNodeType.Element)
					{
						if (dataContractSerializer.IsStartObject(xmlDictionaryReader))
						{
							importedHIPDatabase.LocalEnvironments.Add((LocalEnvironment)dataContractSerializer.ReadObject(xmlDictionaryReader));
							continue;
						}
						if (dataContractSerializer2.IsStartObject(xmlDictionaryReader))
						{
							importedHIPDatabase.LocalEnvironmentEndPoints.Add((LEEndpoint)dataContractSerializer2.ReadObject(xmlDictionaryReader));
							continue;
						}
						if (dataContractSerializer3.IsStartObject(xmlDictionaryReader))
						{
							importedHIPDatabase.HostEnvironments.Add((LinqHostEnvironment)dataContractSerializer3.ReadObject(xmlDictionaryReader));
							continue;
						}
						if (dataContractSerializer4.IsStartObject(xmlDictionaryReader))
						{
							importedHIPDatabase.SecurityPolicies.Add((SecurityPolicy)dataContractSerializer4.ReadObject(xmlDictionaryReader));
							continue;
						}
						if (dataContractSerializer5.IsStartObject(xmlDictionaryReader))
						{
							importedHIPDatabase.AffiliatedApplications.Add((AffiliatedApplication)dataContractSerializer5.ReadObject(xmlDictionaryReader));
							continue;
						}
						if (dataContractSerializer6.IsStartObject(xmlDictionaryReader))
						{
							importedHIPDatabase.Objects.Add((Microsoft.HostIntegration.TI.Linq.Object)dataContractSerializer6.ReadObject(xmlDictionaryReader));
							continue;
						}
						if (dataContractSerializer7.IsStartObject(xmlDictionaryReader))
						{
							importedHIPDatabase.TIMFiles.Add((TIMFile)dataContractSerializer7.ReadObject(xmlDictionaryReader));
							continue;
						}
						if (dataContractSerializer8.IsStartObject(xmlDictionaryReader))
						{
							importedHIPDatabase.Methods.Add((Method)dataContractSerializer8.ReadObject(xmlDictionaryReader));
							continue;
						}
						if (dataContractSerializer9.IsStartObject(xmlDictionaryReader))
						{
							importedHIPDatabase.Views.Add((View)dataContractSerializer9.ReadObject(xmlDictionaryReader));
							continue;
						}
						if (dataContractSerializer10.IsStartObject(xmlDictionaryReader))
						{
							importedHIPDatabase.Determinants.Add((Determinant)dataContractSerializer10.ReadObject(xmlDictionaryReader));
							continue;
						}
						if (dataContractSerializer11.IsStartObject(xmlDictionaryReader))
						{
							importedHIPDatabase.HostEnvironmentPermissions.Add((HEPermission)dataContractSerializer11.ReadObject(xmlDictionaryReader));
							continue;
						}
						if (dataContractSerializer12.IsStartObject(xmlDictionaryReader))
						{
							importedHIPDatabase.Computers.Add((Computer)dataContractSerializer12.ReadObject(xmlDictionaryReader));
							continue;
						}
						if (dataContractSerializer13.IsStartObject(xmlDictionaryReader))
						{
							importedHIPDatabase.Applications.Add((Application)dataContractSerializer13.ReadObject(xmlDictionaryReader));
							continue;
						}
						if (dataContractSerializer14.IsStartObject(xmlDictionaryReader))
						{
							importedHIPDatabase.Listeners.Add((Listener)dataContractSerializer14.ReadObject(xmlDictionaryReader));
							continue;
						}
					}
					flag = xmlDictionaryReader.Read();
				}
			}
			bool flag2 = false;
			foreach (LEEndpoint leendpoint in importedHIPDatabase.LocalEnvironmentEndPoints)
			{
				flag2 = false;
				foreach (LocalEnvironment localEnvironment in importedHIPDatabase.LocalEnvironments)
				{
					if (localEnvironment.Identity == leendpoint.LEID)
					{
						leendpoint.LocalEnvironment = localEnvironment;
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					throw new ApplicationException("LE Endpoint with Identity: " + leendpoint.Identity.ToString() + " has non-existent LE Identity: " + leendpoint.LEID.ToString());
				}
			}
			foreach (Microsoft.HostIntegration.TI.Linq.Object @object in importedHIPDatabase.Objects)
			{
				foreach (Method method in importedHIPDatabase.Methods)
				{
					if (method.ObjectID == @object.Identity)
					{
						method.Object = @object;
					}
				}
				flag2 = false;
				foreach (TIMFile timfile in importedHIPDatabase.TIMFiles)
				{
					if (timfile.Identity == @object.FileID)
					{
						@object.TIMFile = timfile;
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					throw new ApplicationException("Object with Identity: " + @object.Identity.ToString() + " has non-existent TIM file Identity: " + @object.FileID.ToString());
				}
			}
			foreach (Method method2 in importedHIPDatabase.Methods)
			{
				if (method2.Object == null)
				{
					throw new ApplicationException("Method with Identity: " + method2.Identity.ToString() + " has non-existent Object Identity: " + method2.ObjectID.ToString());
				}
			}
			foreach (View view in importedHIPDatabase.Views)
			{
				flag2 = false;
				foreach (Microsoft.HostIntegration.TI.Linq.Object object2 in importedHIPDatabase.Objects)
				{
					if (view.ObjectID == object2.Identity)
					{
						view.Object = object2;
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					throw new ApplicationException("View with Identity: " + view.Identity.ToString() + " has non-existent Object Identity: " + view.ObjectID.ToString());
				}
				flag2 = false;
				foreach (LocalEnvironment localEnvironment2 in importedHIPDatabase.LocalEnvironments)
				{
					if (view.LEID == localEnvironment2.Identity)
					{
						view.LocalEnvironment = localEnvironment2;
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					throw new ApplicationException("View with Identity: " + view.Identity.ToString() + " has non-existent LE Identity: " + view.LEID.ToString());
				}
				flag2 = false;
				foreach (SecurityPolicy securityPolicy in importedHIPDatabase.SecurityPolicies)
				{
					if (view.SecurityPolicyID == securityPolicy.Identity)
					{
						view.SecurityPolicy = securityPolicy;
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					throw new ApplicationException("View with Identity: " + view.Identity.ToString() + " has non-existent Security Policy Identity: " + view.SecurityPolicyID.ToString());
				}
			}
			foreach (HEPermission hepermission in importedHIPDatabase.HostEnvironmentPermissions)
			{
				flag2 = false;
				foreach (LinqHostEnvironment linqHostEnvironment in importedHIPDatabase.HostEnvironments)
				{
					if (hepermission.HEID == linqHostEnvironment.Identity)
					{
						hepermission.HostEnvironment = linqHostEnvironment;
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					throw new ApplicationException("HE Permission with Identity: " + hepermission.Identity.ToString() + " has non-existent HE Identity: " + hepermission.HEID.ToString());
				}
				flag2 = false;
				foreach (View view2 in importedHIPDatabase.Views)
				{
					if (hepermission.ViewID == view2.Identity)
					{
						hepermission.View = view2;
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					throw new ApplicationException("HE Permission with Identity: " + hepermission.Identity.ToString() + " has non-existent View Identity: " + hepermission.ViewID.ToString());
				}
			}
			foreach (Determinant determinant in importedHIPDatabase.Determinants)
			{
				flag2 = false;
				foreach (Method method3 in importedHIPDatabase.Methods)
				{
					if (determinant.MethodID == method3.Identity)
					{
						determinant.Method = method3;
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					throw new ApplicationException("Deteminant with Identity: " + determinant.Identity.ToString() + " has non-existent Method Identity: " + determinant.MethodID.ToString());
				}
				flag2 = false;
				foreach (View view3 in importedHIPDatabase.Views)
				{
					if (determinant.ViewID == view3.Identity)
					{
						determinant.View = view3;
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					throw new ApplicationException("Deteminant with Identity: " + determinant.Identity.ToString() + " has non-existent View Identity: " + determinant.ViewID.ToString());
				}
				flag2 = false;
				foreach (LEEndpoint leendpoint2 in importedHIPDatabase.LocalEnvironmentEndPoints)
				{
					if (determinant.EndPointID == leendpoint2.Identity)
					{
						determinant.LEEndpoint = leendpoint2;
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					throw new ApplicationException("Deteminant with Identity: " + determinant.Identity.ToString() + " has non-existent LE Endpoint Identity: " + determinant.EndPointID.ToString());
				}
			}
			foreach (AffiliatedApplication affiliatedApplication in importedHIPDatabase.AffiliatedApplications)
			{
				flag2 = false;
				foreach (SecurityPolicy securityPolicy2 in importedHIPDatabase.SecurityPolicies)
				{
					if (securityPolicy2.Identity == affiliatedApplication.PolicyID)
					{
						affiliatedApplication.SecurityPolicy = securityPolicy2;
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					throw new ApplicationException("Affiliate Application with Identity: " + affiliatedApplication.Identity.ToString() + " has non-existent Security Policy Identity: " + affiliatedApplication.PolicyID.ToString());
				}
			}
			foreach (Application application in importedHIPDatabase.Applications)
			{
				flag2 = false;
				foreach (Computer computer in importedHIPDatabase.Computers)
				{
					if (computer.Identity == application.ComputerID)
					{
						application.Computer = computer;
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					throw new ApplicationException("Application with Identity: " + application.Identity.ToString() + " has non-existent Computer Identity: " + application.ComputerID.ToString());
				}
			}
			foreach (Listener listener in importedHIPDatabase.Listeners)
			{
				flag2 = false;
				foreach (Application application2 in importedHIPDatabase.Applications)
				{
					if (application2.Identity == listener.ApplicationID)
					{
						listener.Application = application2;
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					throw new ApplicationException("Listener with Identity: " + listener.Identity.ToString() + " has non-existent Application Identity: " + listener.ApplicationID.ToString());
				}
			}
			foreach (Listener listener2 in importedHIPDatabase.Listeners)
			{
				flag2 = false;
				foreach (LocalEnvironment localEnvironment3 in importedHIPDatabase.LocalEnvironments)
				{
					if (localEnvironment3.Identity == listener2.LEID)
					{
						listener2.LocalEnvironment = localEnvironment3;
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					throw new ApplicationException("Listener with Identity: " + listener2.Identity.ToString() + " has non-existent Local Environment Identity: " + listener2.LEID.ToString());
				}
			}
			foreach (Microsoft.HostIntegration.TI.Linq.Object object3 in importedHIPDatabase.Objects)
			{
				if (!object3.ClassName.StartsWith("I", StringComparison.InvariantCultureIgnoreCase))
				{
					object3.ClassName = "I" + object3.ClassName;
				}
			}
			foreach (Microsoft.HostIntegration.TI.Linq.Object object4 in importedHIPDatabase.Objects)
			{
				if (string.IsNullOrEmpty(object4.ImplementingAssembly))
				{
					warnings.Add("Implementing Assembly is not available for " + object4.Name);
				}
				else
				{
					FileInfo fileInfo = new FileInfo(object4.ImplementingAssembly);
					object4.ImplementingAssembly = fileInfo.Name;
				}
			}
			foreach (TIMFile timfile2 in importedHIPDatabase.TIMFiles)
			{
				if (string.IsNullOrEmpty(timfile2.Name))
				{
					warnings.Add("TIM File is not available");
				}
				else
				{
					FileInfo fileInfo2 = new FileInfo(timfile2.Name);
					timfile2.Name = fileInfo2.Name;
				}
			}
			importInformation.ImportedDatabase = importedHIPDatabase;
		}

		// Token: 0x06003B60 RID: 15200 RVA: 0x000C8B50 File Offset: 0x000C6D50
		private static void CheckHostEnvironments(ImportInformation importInformation)
		{
			ImportedHIPDatabase importedDatabase = importInformation.ImportedDatabase;
			List<string> warnings = importInformation.Warnings;
			List<string> errors = importInformation.Errors;
			HipConfigurationSectionHandler hipConfigurationSection = importInformation.HipConfigurationSection;
			Dictionary<int, LinqHostEnvironment> dictionary = new Dictionary<int, LinqHostEnvironment>();
			foreach (LinqHostEnvironment linqHostEnvironment in importedDatabase.HostEnvironments)
			{
				if (linqHostEnvironment.HEType == 1)
				{
					if (linqHostEnvironment.ConvertCoClass != "DEFAULT390CONVERTPRIMEX" && linqHostEnvironment.ConvertCoClass != "ConvertPrimAS400Obj")
					{
						errors.Add("Import only supports Default OS/390 and AS/400 conversions");
						continue;
					}
				}
				else
				{
					if (linqHostEnvironment.HEType != 2)
					{
						errors.Add("Import only supports TCP (1) and SNA (2) Host Environments");
						continue;
					}
					if (linqHostEnvironment.ConvertCoClass != "DEFAULT390CONVERTPRIMEX" && linqHostEnvironment.ConvertCoClass != "ConvertPrimAS400Obj")
					{
						errors.Add("Import only supports Default OS/390 and AS/400 conversions");
						continue;
					}
				}
				dictionary.Add(linqHostEnvironment.Identity, linqHostEnvironment);
			}
			if (dictionary.Count == 0)
			{
				throw new ApplicationException("There are no valid Host Environments");
			}
			if (dictionary.Count == 1)
			{
				importInformation.ValidHostEnvironments = dictionary;
				return;
			}
			List<string> list = new List<string>();
			List<int> list2 = new List<int>();
			List<int> list3 = new List<int>(dictionary.Keys);
			for (int i = 0; i < list3.Count - 1; i++)
			{
				int identity = dictionary[list3[i]].Identity;
				if (!list2.Contains(identity))
				{
					string name = dictionary[list3[i]].Name;
					for (int j = i + 1; j < list3.Count; j++)
					{
						int identity2 = dictionary[list3[j]].Identity;
						if (!list2.Contains(identity2))
						{
							string name2 = dictionary[list3[j]].Name;
							if (string.CompareOrdinal(name, name2) == 0)
							{
								if (!list.Contains(name))
								{
									errors.Add("There are multiple Host Environments with the name: " + name);
									list.Add(name);
								}
								list2.Add(identity2);
							}
						}
					}
				}
			}
			importInformation.ValidHostEnvironments = new Dictionary<int, LinqHostEnvironment>();
			foreach (LinqHostEnvironment linqHostEnvironment2 in dictionary.Values)
			{
				if (!list2.Contains(linqHostEnvironment2.Identity))
				{
					importInformation.ValidHostEnvironments.Add(linqHostEnvironment2.Identity, linqHostEnvironment2);
				}
			}
			if (importInformation.ValidHostEnvironments.Count == 0)
			{
				throw new ApplicationException("There are no valid Host Environments");
			}
		}

		// Token: 0x06003B61 RID: 15201 RVA: 0x000C8DF8 File Offset: 0x000C6FF8
		private static void CheckObjects(ImportInformation importInformation)
		{
			ImportedHIPDatabase importedDatabase = importInformation.ImportedDatabase;
			List<string> warnings = importInformation.Warnings;
			List<string> errors = importInformation.Errors;
			HipConfigurationSectionHandler hipConfigurationSection = importInformation.HipConfigurationSection;
			Dictionary<int, Microsoft.HostIntegration.TI.Linq.Object> dictionary = new Dictionary<int, Microsoft.HostIntegration.TI.Linq.Object>();
			foreach (Microsoft.HostIntegration.TI.Linq.Object @object in importedDatabase.Objects)
			{
				if (@object.ServerType == 2)
				{
					dictionary.Add(@object.Identity, @object);
				}
				else if (@object.ServerType == 1)
				{
					warnings.Add("Import only supports .Net Server Objects");
				}
				else
				{
					errors.Add("Object type is not COM (1) or .Net(2)");
				}
			}
			if (dictionary.Count == 0)
			{
				throw new ApplicationException("There are no valid Objects");
			}
			if (dictionary.Count == 1)
			{
				importInformation.ValidObjects = dictionary;
				return;
			}
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			List<int> list3 = new List<int>();
			List<int> list4 = new List<int>(dictionary.Keys);
			for (int i = 0; i < list4.Count - 1; i++)
			{
				int identity = dictionary[list4[i]].Identity;
				if (!list3.Contains(identity))
				{
					string name = dictionary[list4[i]].Name;
					string text = dictionary[list4[i]].PrimaryName + "." + dictionary[list4[i]].ClassName;
					for (int j = i + 1; j < list4.Count; j++)
					{
						int identity2 = dictionary[list4[j]].Identity;
						if (!list3.Contains(identity2))
						{
							string name2 = dictionary[list4[j]].Name;
							string text2 = dictionary[list4[j]].PrimaryName + "." + dictionary[list4[j]].ClassName;
							if (string.CompareOrdinal(name, name2) == 0)
							{
								if (!list.Contains(name))
								{
									errors.Add("There are multiple Objects with the name: " + name);
									list.Add(name);
								}
								list3.Add(identity2);
							}
							if (string.CompareOrdinal(text, text2) == 0)
							{
								if (!list2.Contains(text))
								{
									errors.Add("There are multiple Objects with the Interface: " + text);
									list2.Add(text);
								}
								if (!list3.Contains(identity2))
								{
									list3.Add(identity2);
								}
							}
						}
					}
				}
			}
			importInformation.ValidObjects = new Dictionary<int, Microsoft.HostIntegration.TI.Linq.Object>();
			foreach (Microsoft.HostIntegration.TI.Linq.Object object2 in dictionary.Values)
			{
				if (!list3.Contains(object2.Identity))
				{
					importInformation.ValidObjects.Add(object2.Identity, object2);
				}
			}
			if (importInformation.ValidObjects.Count == 0)
			{
				throw new ApplicationException("There are no valid Objects");
			}
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x000C9104 File Offset: 0x000C7304
		private static void CheckSecurityPolicies(ImportInformation importInformation)
		{
			ImportedHIPDatabase importedDatabase = importInformation.ImportedDatabase;
			List<string> warnings = importInformation.Warnings;
			List<string> errors = importInformation.Errors;
			HipConfigurationSectionHandler hipConfigurationSection = importInformation.HipConfigurationSection;
			Dictionary<int, SecurityPolicy> dictionary = new Dictionary<int, SecurityPolicy>();
			foreach (SecurityPolicy securityPolicy in importedDatabase.SecurityPolicies)
			{
				if (securityPolicy.Source == 2)
				{
					dictionary.Add(securityPolicy.Identity, securityPolicy);
				}
				else if (securityPolicy.Source == 1)
				{
					new EssoSecurityPolicy();
					if (securityPolicy.AffiliatedApplications.Count != 1)
					{
						errors.Add("Invalid Security Policy configuration, more than one Affiliate Application");
					}
					else
					{
						dictionary.Add(securityPolicy.Identity, securityPolicy);
					}
				}
				else
				{
					errors.Add("Security Policy Source is not Host (1) or Service (2)");
				}
			}
			if (dictionary.Count == 0)
			{
				throw new ApplicationException("There are no valid Security Policies");
			}
			if (dictionary.Count == 1)
			{
				importInformation.ValidSecurityPolicies = dictionary;
				return;
			}
			List<string> list = new List<string>();
			List<int> list2 = new List<int>();
			List<int> list3 = new List<int>(dictionary.Keys);
			for (int i = 0; i < list3.Count - 1; i++)
			{
				int identity = dictionary[list3[i]].Identity;
				if (!list2.Contains(identity))
				{
					string name = dictionary[list3[i]].Name;
					for (int j = i + 1; j < list3.Count; j++)
					{
						int identity2 = dictionary[list3[j]].Identity;
						if (!list2.Contains(identity2))
						{
							string name2 = dictionary[list3[j]].Name;
							if (string.CompareOrdinal(name, name2) == 0)
							{
								if (!list.Contains(name))
								{
									errors.Add("There are multiple Security Policies with the name: " + name);
									list.Add(name);
								}
								list2.Add(identity2);
							}
						}
					}
				}
			}
			importInformation.ValidSecurityPolicies = new Dictionary<int, SecurityPolicy>();
			foreach (SecurityPolicy securityPolicy2 in dictionary.Values)
			{
				if (!list2.Contains(securityPolicy2.Identity))
				{
					importInformation.ValidSecurityPolicies.Add(securityPolicy2.Identity, securityPolicy2);
				}
			}
			if (importInformation.ValidSecurityPolicies.Count == 0)
			{
				throw new ApplicationException("There are no valid Security Policies");
			}
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x000C9374 File Offset: 0x000C7574
		private static void CheckImplementingAssemblyDirectories(ImportInformation importInformation)
		{
			ImportedHIPDatabase importedDatabase = importInformation.ImportedDatabase;
			List<string> warnings = importInformation.Warnings;
			List<string> errors = importInformation.Errors;
			Computer replacementComputer = importInformation.ReplacementComputer;
			string text = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			foreach (Application application in importedDatabase.Applications)
			{
				if (application.ComputerID == replacementComputer.Identity)
				{
					flag3 = true;
					string text2 = application.DotNetPath;
					if (string.IsNullOrWhiteSpace(text2))
					{
						warnings.Add("Application: " + application.Name + " has no directory path for Implementing Assemblies");
					}
					else
					{
						if (!text2.EndsWith("\\"))
						{
							text2 += "\\";
						}
						if (string.CompareOrdinal(text, text2) != 0)
						{
							bool exists = new DirectoryInfo(text2).Exists;
							if (!exists)
							{
								warnings.Add("Application: " + application.Name + " has non-existent directory path for Implementing Assemblies: " + text2);
							}
							if (text == null)
							{
								text = text2;
								flag = exists;
							}
							else if (!flag && exists)
							{
								text = text2;
								flag = exists;
								flag2 = true;
							}
						}
					}
				}
			}
			if (!flag3)
			{
				throw new ApplicationException("Computer " + replacementComputer.Name + " has no applications");
			}
			if (flag2)
			{
				if (flag)
				{
					warnings.Add("There are Applications with different directory paths for Implementing Assemblies. Using: " + text);
				}
				else
				{
					warnings.Add("There are Applications with different directory paths for Implementing Assemblies, none of which exist. Using: " + text);
				}
			}
			else if (text == null)
			{
				errors.Add("All Applications have empty paths for Implementing Assemblies");
			}
			else if (!flag)
			{
				warnings.Add("The directories for Implementing Assemblies do not exist. Using: " + text);
			}
			importInformation.UsedDirectory = text;
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x000C9524 File Offset: 0x000C7724
		private static void CheckLesAndLeeps(ImportInformation importInformation)
		{
			ImportedHIPDatabase importedDatabase = importInformation.ImportedDatabase;
			List<string> warnings = importInformation.Warnings;
			List<string> errors = importInformation.Errors;
			Computer replacementComputer = importInformation.ReplacementComputer;
			Dictionary<int, LocalEnvironment> dictionary = new Dictionary<int, LocalEnvironment>();
			foreach (Application application in replacementComputer.Applications)
			{
				foreach (Listener listener in application.Listeners)
				{
					LocalEnvironment localEnvironment = listener.LocalEnvironment;
					dictionary.Add(localEnvironment.Identity, localEnvironment);
				}
			}
			importInformation.ValidLocalEnvironments = dictionary;
			Dictionary<int, LEEndpoint> dictionary2 = new Dictionary<int, LEEndpoint>();
			foreach (LocalEnvironment localEnvironment2 in dictionary.Values)
			{
				foreach (LEEndpoint leendpoint in localEnvironment2.LEEndpoints)
				{
					dictionary2.Add(leendpoint.Identity, leendpoint);
				}
			}
			importInformation.ValidEndpoints = dictionary2;
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x000C966C File Offset: 0x000C786C
		private static void CheckDeterminants(ImportInformation importInformation)
		{
			ImportedHIPDatabase importedDatabase = importInformation.ImportedDatabase;
			List<string> warnings = importInformation.Warnings;
			List<string> errors = importInformation.Errors;
			Dictionary<int, LEEndpoint> validEndpoints = importInformation.ValidEndpoints;
			Dictionary<int, Determinant> dictionary = new Dictionary<int, Determinant>();
			foreach (Determinant determinant in importedDatabase.Determinants)
			{
				int endPointID = determinant.EndPointID;
				if (validEndpoints.ContainsKey(endPointID))
				{
					DeterminantType type = (DeterminantType)determinant.Type;
					if (type <= DeterminantType.Data)
					{
						if (type != DeterminantType.Endpoint)
						{
							if (type != DeterminantType.Trm)
							{
								if (type == DeterminantType.Data)
								{
									if (determinant.LEEndpoint.LocalEnvironment.LEType != 2)
									{
										warnings.Add("Determinant with Identity: " + determinant.Identity.ToString() + " is TCP User Data, which is not supported");
										continue;
									}
									determinant.Type = 4096;
									goto IL_0353;
								}
							}
							else
							{
								if (determinant.LEEndpoint.LocalEnvironment.LEType != 1)
								{
									errors.Add("Determinant with Identity: " + determinant.Identity.ToString() + " is TRM, but is associated with an SNA Endpoint");
									continue;
								}
								string text = determinant.TRMHandlerProgID;
								if (text != null)
								{
									if (text == "TRMHandler.TRMHandlerMSLink.1")
									{
										int num = 8192;
										determinant.Type = num;
										goto IL_0353;
									}
									if (text == "TRMHandler.TRMHandlerIBMCCS.1" || text == "TRMHandler.TRMHandlerMSCCS.1")
									{
										warnings.Add("Determinant with Identity: " + determinant.Identity.ToString() + " is TRM User Data which is not supported");
										continue;
									}
								}
								errors.Add("Determinant with Identity: " + determinant.Identity.ToString() + " is ELM, but has an incorrect ELM Handler");
								continue;
							}
						}
						else
						{
							if (determinant.LEEndpoint.LocalEnvironment.LEType != 2)
							{
								warnings.Add("Determinant with Identity: " + determinant.Identity.ToString() + " is TCP Endpoint, which is not supported");
								continue;
							}
							determinant.Type = 1024;
							goto IL_0353;
						}
					}
					else if (type != DeterminantType.EnvelopeSna)
					{
						if (type != DeterminantType.Elm)
						{
							if (type != DeterminantType.Invalid)
							{
							}
						}
						else
						{
							if (determinant.LEEndpoint.LocalEnvironment.LEType != 1)
							{
								errors.Add("Determinant with Identity: " + determinant.Identity.ToString() + " is ELM, but is associated with an SNA Endpoint");
								continue;
							}
							string text = determinant.ELMHandlerProgID;
							if (text != null)
							{
								int num;
								if (!(text == "ELMHandler.ELMHandlerMSLink.1"))
								{
									if (!(text == "ELMHandler.ELMHandlerIBMCCS.1"))
									{
										if (!(text == "ELMHandler.ELMHandlerMSCCS.1"))
										{
											goto IL_017D;
										}
										num = 256;
									}
									else
									{
										num = 257;
									}
								}
								else
								{
									num = 128;
								}
								determinant.Type = num;
								goto IL_0353;
							}
							IL_017D:
							errors.Add("Determinant with Identity: " + determinant.Identity.ToString() + " is ELM, but has an incorrect ELM Handler");
							continue;
						}
					}
					else
					{
						if (determinant.LEEndpoint.LocalEnvironment.LEType != 2)
						{
							errors.Add("Determinant with Identity: " + determinant.Identity.ToString() + " is SNA Link, but is associaed with an TCP Endpoint");
							continue;
						}
						determinant.Type = 2048;
						goto IL_0353;
					}
					errors.Add("Determinant with Identity: " + determinant.Identity.ToString() + " has an invalid Determinant Type");
					continue;
					IL_0353:
					dictionary.Add(determinant.Identity, determinant);
				}
			}
			Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
			List<int> list = new List<int>();
			Dictionary<int, Determinant> dictionary3 = new Dictionary<int, Determinant>();
			foreach (Determinant determinant2 in dictionary.Values)
			{
				int endPointID2 = determinant2.EndPointID;
				int type2 = determinant2.Type;
				if (!dictionary2.ContainsKey(endPointID2))
				{
					dictionary2.Add(endPointID2, type2);
				}
				if (dictionary2[endPointID2] != type2)
				{
					if (!list.Contains(endPointID2))
					{
						errors.Add("Endpoint with Identity: " + endPointID2.ToString() + " is associated with multiple determinant types, this is not supported. Determinants of varying types will not be imported");
						list.Add(endPointID2);
					}
				}
				else
				{
					dictionary3.Add(determinant2.Identity, determinant2);
				}
			}
			Dictionary<int, LocalEnvironment> validLocalEnvironments = importInformation.ValidLocalEnvironments;
			Dictionary<int, View> dictionary4 = new Dictionary<int, View>();
			foreach (View view in importedDatabase.Views)
			{
				LocalEnvironment localEnvironment = view.LocalEnvironment;
				if (validLocalEnvironments.ContainsKey(localEnvironment.Identity))
				{
					SecurityPolicy securityPolicy = view.SecurityPolicy;
					if (!importInformation.ValidSecurityPolicies.ContainsKey(securityPolicy.Identity))
					{
						errors.Add("View: " + view.Name + " is associated with an invalid Security Policy");
					}
					else
					{
						bool flag = true;
						foreach (HEPermission hepermission in view.HEPermissions)
						{
							LinqHostEnvironment hostEnvironment = hepermission.HostEnvironment;
							if (importInformation.ValidHostEnvironments.ContainsKey(hostEnvironment.Identity))
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							errors.Add("View: " + view.Name + " has no valid Host Environments");
						}
						else
						{
							dictionary4.Add(view.Identity, view);
						}
					}
				}
			}
			Dictionary<int, Determinant> dictionary5 = new Dictionary<int, Determinant>();
			foreach (Determinant determinant3 in dictionary3.Values)
			{
				int identity = determinant3.Method.Object.Identity;
				if (!importInformation.ValidObjects.ContainsKey(identity))
				{
					warnings.Add("Determinant with Identity: " + determinant3.Identity.ToString() + " is associated with an invalid Object");
				}
				else
				{
					int identity2 = determinant3.View.Identity;
					if (!dictionary4.ContainsKey(identity2))
					{
						warnings.Add("Determinant with Identity: " + determinant3.Identity.ToString() + " is associated with an invalid View");
					}
					else
					{
						dictionary5.Add(determinant3.Identity, determinant3);
					}
				}
			}
			importInformation.UsedDeterminants = dictionary5;
		}

		// Token: 0x06003B66 RID: 15206 RVA: 0x000C9D3C File Offset: 0x000C7F3C
		private static void FilterHostEnvironments(ImportInformation importInformation)
		{
			Dictionary<int, LinqHostEnvironment> dictionary = new Dictionary<int, LinqHostEnvironment>();
			foreach (DeterminantInformation determinantInformation in importInformation.DeterminantTypeToDeterminantInformations.Values)
			{
				foreach (LinqHostEnvironment linqHostEnvironment in determinantInformation.IdToHes.Values)
				{
					if (!dictionary.ContainsKey(linqHostEnvironment.Identity))
					{
						dictionary.Add(linqHostEnvironment.Identity, linqHostEnvironment);
					}
				}
			}
			importInformation.UsedHostEnvironments = dictionary;
		}

		// Token: 0x06003B67 RID: 15207 RVA: 0x000C9DF4 File Offset: 0x000C7FF4
		private static void FilterObjects(ImportInformation importInformation)
		{
			Dictionary<int, Microsoft.HostIntegration.TI.Linq.Object> dictionary = new Dictionary<int, Microsoft.HostIntegration.TI.Linq.Object>();
			foreach (DeterminantInformation determinantInformation in importInformation.DeterminantTypeToDeterminantInformations.Values)
			{
				foreach (Determinant determinant in determinantInformation.Determinants)
				{
					Microsoft.HostIntegration.TI.Linq.Object @object = determinant.Method.Object;
					if (!dictionary.ContainsKey(@object.Identity))
					{
						dictionary.Add(@object.Identity, @object);
					}
				}
			}
			importInformation.UsedObjects = dictionary;
		}

		// Token: 0x06003B68 RID: 15208 RVA: 0x000C9EB0 File Offset: 0x000C80B0
		private static void FilterSecurityPolicies(ImportInformation importInformation)
		{
			Dictionary<int, SecurityPolicy> dictionary = new Dictionary<int, SecurityPolicy>();
			foreach (DeterminantInformation determinantInformation in importInformation.DeterminantTypeToDeterminantInformations.Values)
			{
				foreach (Determinant determinant in determinantInformation.Determinants)
				{
					SecurityPolicy securityPolicy = determinant.View.SecurityPolicy;
					if (!dictionary.ContainsKey(securityPolicy.Identity))
					{
						dictionary.Add(securityPolicy.Identity, securityPolicy);
					}
				}
			}
			importInformation.UsedSecurityPolicies = dictionary;
		}

		// Token: 0x06003B69 RID: 15209 RVA: 0x000C9F6C File Offset: 0x000C816C
		private static void AddHostEnvironments(ImportInformation importInformation)
		{
			List<string> warnings = importInformation.Warnings;
			List<string> errors = importInformation.Errors;
			HipConfigurationSectionHandler hipConfigurationSection = importInformation.HipConfigurationSection;
			foreach (LinqHostEnvironment linqHostEnvironment in importInformation.UsedHostEnvironments.Values)
			{
				if (linqHostEnvironment.HEType == 1)
				{
					int num = 1;
					List<string> allHostEndpoints = HipImport.GetAllHostEndpoints(linqHostEnvironment.HostName);
					using (List<string>.Enumerator enumerator2 = allHostEndpoints.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							string text = enumerator2.Current;
							TcpHostEnvironment tcpHostEnvironment = new TcpHostEnvironment();
							tcpHostEnvironment.CodePage = linqHostEnvironment.CodePage;
							if (linqHostEnvironment.ConvertCoClass == "DEFAULT390CONVERTPRIMEX")
							{
								tcpHostEnvironment.DataConversion = PrimitiveConverterTypes.OS390;
							}
							else
							{
								tcpHostEnvironment.DataConversion = PrimitiveConverterTypes.AS400;
							}
							tcpHostEnvironment.IpAddress = text;
							tcpHostEnvironment.Name = linqHostEnvironment.Name;
							if (allHostEndpoints.Count != 1)
							{
								TcpHostEnvironment tcpHostEnvironment2 = tcpHostEnvironment;
								tcpHostEnvironment2.Name = tcpHostEnvironment2.Name + "-" + num.ToString(CultureInfo.InvariantCulture);
							}
							tcpHostEnvironment.Timeout = linqHostEnvironment.RcvTimeOut;
							hipConfigurationSection.TcpHostEnvironments.AddTcpHostEnvironment(tcpHostEnvironment);
							num++;
						}
						continue;
					}
				}
				int num2 = 1;
				List<string> allHostEndpoints2 = HipImport.GetAllHostEndpoints(linqHostEnvironment.HostName);
				foreach (string text2 in allHostEndpoints2)
				{
					SnaHostEnvironment snaHostEnvironment = new SnaHostEnvironment();
					snaHostEnvironment.CodePage = linqHostEnvironment.CodePage;
					if (linqHostEnvironment.ConvertCoClass == "DEFAULT390CONVERTPRIMEX")
					{
						snaHostEnvironment.DataConversion = PrimitiveConverterTypes.OS390;
					}
					else
					{
						snaHostEnvironment.DataConversion = PrimitiveConverterTypes.AS400;
					}
					snaHostEnvironment.RemoteLuName = text2;
					snaHostEnvironment.Name = linqHostEnvironment.Name;
					if (allHostEndpoints2.Count != 1)
					{
						SnaHostEnvironment snaHostEnvironment2 = snaHostEnvironment;
						snaHostEnvironment2.Name = snaHostEnvironment2.Name + "-" + num2.ToString(CultureInfo.InvariantCulture);
					}
					snaHostEnvironment.Timeout = linqHostEnvironment.RcvTimeOut;
					hipConfigurationSection.SnaHostEnvironments.AddSnaHostEnvironment(snaHostEnvironment);
					num2++;
				}
			}
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x000CA1DC File Offset: 0x000C83DC
		private static List<string> GetAllHostEndpoints(string hostName)
		{
			List<string> list = new List<string>();
			foreach (string text in hostName.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
			{
				bool flag = false;
				foreach (string text2 in list)
				{
					if (string.Compare(text, text2, StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x06003B6B RID: 15211 RVA: 0x000CA270 File Offset: 0x000C8470
		private static void AddObjects(ImportInformation importInformation)
		{
			List<string> warnings = importInformation.Warnings;
			List<string> errors = importInformation.Errors;
			HipConfigurationSectionHandler hipConfigurationSection = importInformation.HipConfigurationSection;
			foreach (Microsoft.HostIntegration.TI.Linq.Object @object in importInformation.UsedObjects.Values)
			{
				HipObject hipObject = new HipObject();
				hipObject.MetaDataInterface = @object.PrimaryName + "." + @object.ClassName;
				string name = @object.TIMFile.Name;
				hipObject.MetaDataAssembly = (string.IsNullOrWhiteSpace(name) ? (hipObject.MetaDataInterface + "_MetaData_Assembly") : name);
				if (string.IsNullOrWhiteSpace(name))
				{
					warnings.Add("Object: " + hipObject.MetaDataInterface + " has no name associated with its meta data assembly: its configuration is incomplete");
				}
				hipObject.ImplementingAssembly = (string.IsNullOrWhiteSpace(@object.ImplementingAssembly) ? (hipObject.MetaDataInterface + "_Implementing_Assembly") : @object.ImplementingAssembly);
				hipObject.ImplementingClass = (string.IsNullOrWhiteSpace(@object.ImplementingNameSpaceDotClass) ? (hipObject.MetaDataInterface + "_Implementing_Class") : @object.ImplementingNameSpaceDotClass);
				if (string.IsNullOrWhiteSpace(@object.ImplementingAssembly) || string.IsNullOrWhiteSpace(@object.ImplementingNameSpaceDotClass))
				{
					warnings.Add("Object: " + hipObject.MetaDataInterface + " has a deferred implementation: its configuration is incomplete");
				}
				hipConfigurationSection.HipObjects.AddHipObject(hipObject);
			}
		}

		// Token: 0x06003B6C RID: 15212 RVA: 0x000CA400 File Offset: 0x000C8600
		private static void AddSecurityPolicies(ImportInformation importInformation)
		{
			List<string> warnings = importInformation.Warnings;
			List<string> errors = importInformation.Errors;
			HipConfigurationSectionHandler hipConfigurationSection = importInformation.HipConfigurationSection;
			foreach (SecurityPolicy securityPolicy in importInformation.UsedSecurityPolicies.Values)
			{
				if (securityPolicy.Source != 2)
				{
					EssoSecurityPolicy essoSecurityPolicy = new EssoSecurityPolicy();
					essoSecurityPolicy.AffiliateApplication = securityPolicy.AffiliatedApplications[0].Name;
					if (securityPolicy.UseDefaultCredentials == 1)
					{
						essoSecurityPolicy.DefaultCredentialsGroup = securityPolicy.DefaulUserApp;
					}
					essoSecurityPolicy.Name = securityPolicy.Name;
					hipConfigurationSection.EssoSecurityPolicies.AddEssoSecurityPolicy(essoSecurityPolicy);
				}
			}
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x000CA4BC File Offset: 0x000C86BC
		private static void FixDuplicateInterfaceMethodName(ResolutionEntryCollection resolutionEntryCollection, ref ResolutionEntry resolutionEntry)
		{
			using (IEnumerator enumerator = resolutionEntryCollection.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((ResolutionEntry)enumerator.Current).InterfaceMethod == resolutionEntry.InterfaceMethod)
					{
						(new char[1])[0] = '0';
						int num = -1;
						for (int i = resolutionEntry.Method.Length; i > 0; i--)
						{
							char[] array = resolutionEntry.Method.Substring(i - 1, 1).ToCharArray();
							if (array[0] < '0' || array[0] > '9')
							{
								break;
							}
							num = i;
						}
						if (num > -1)
						{
							int num2 = (int)Convert.ToInt16(resolutionEntry.Method.Substring(num - 1));
							num2++;
							resolutionEntry.Method = resolutionEntry.Method.Substring(0, num - 1) + num2.ToString();
						}
						else
						{
							ResolutionEntry resolutionEntry2 = resolutionEntry;
							resolutionEntry2.Method += "1";
						}
					}
				}
			}
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x000CA5D0 File Offset: 0x000C87D0
		private static void AddServices(ImportInformation importInformation)
		{
			List<string> warnings = importInformation.Warnings;
			List<string> errors = importInformation.Errors;
			HipConfigurationSectionHandler hipConfigurationSection = importInformation.HipConfigurationSection;
			Dictionary<int, DeterminantInformation> determinantTypeToDeterminantInformations = importInformation.DeterminantTypeToDeterminantInformations;
			foreach (int num in importInformation.DeterminantTypeToDeterminantInformations.Keys)
			{
				DeterminantInformation determinantInformation = importInformation.DeterminantTypeToDeterminantInformations[num];
				Service service = new Service();
				service.AssemblyPath = importInformation.UsedDirectory;
				service.ElmLink = null;
				service.ElmUserData = null;
				service.SnaEndpoint = null;
				service.SnaLink = null;
				service.SnaUserData = null;
				service.TrmLink = null;
				service.Http = null;
				string text = "";
				string text2 = "";
				bool flag = true;
				foreach (LEEndpoint leendpoint in determinantInformation.IdToEndpoints.Values)
				{
					string text3 = (string.IsNullOrWhiteSpace(leendpoint.StringInfo) ? leendpoint.Number.ToString(CultureInfo.InvariantCulture) : leendpoint.StringInfo);
					if (flag)
					{
						text = text3;
					}
					else
					{
						text = text + ";" + text3;
					}
					flag = false;
				}
				flag = true;
				foreach (LinqHostEnvironment linqHostEnvironment in determinantInformation.IdToHes.Values)
				{
					List<string> allHostEndpoints = HipImport.GetAllHostEndpoints(linqHostEnvironment.HostName);
					if (allHostEndpoints.Count == 1)
					{
						if (flag)
						{
							text2 = linqHostEnvironment.Name;
						}
						else
						{
							text2 = text2 + ";" + linqHostEnvironment.Name;
						}
						flag = false;
					}
					else
					{
						for (int i = 1; i <= allHostEndpoints.Count; i++)
						{
							if (flag)
							{
								text2 = linqHostEnvironment.Name + "-" + i.ToString(CultureInfo.InvariantCulture);
							}
							else
							{
								text2 = string.Concat(new string[]
								{
									text2,
									";",
									linqHostEnvironment.Name,
									"-",
									i.ToString(CultureInfo.InvariantCulture)
								});
							}
							flag = false;
						}
					}
				}
				DeterminantType determinantType = (DeterminantType)num;
				if (determinantType <= DeterminantType.NewElmUserDataIBM)
				{
					if (determinantType != DeterminantType.NewElmLink)
					{
						if (determinantType != DeterminantType.NewElmUserDataMS)
						{
							if (determinantType != DeterminantType.NewElmUserDataIBM)
							{
								goto IL_0AD9;
							}
							service.ServiceName = "HipUserdataIBM";
							ElmUserData elmUserData = new ElmUserData();
							elmUserData.IsTypeDefined = true;
							elmUserData.Hosts = text2;
							elmUserData.Ports = text;
							elmUserData.RequestHeaderFormatEnum = TcpRequestHeaderFormat.IbmSuppliedExitRoutine;
							ResolutionEntryCollection resolutionEntryCollection = new ResolutionEntryCollection();
							foreach (Determinant determinant in determinantInformation.Determinants)
							{
								ResolutionEntry resolutionEntry = new ResolutionEntry();
								resolutionEntry.Data = determinant.ResolutionString;
								resolutionEntry.Position = determinant.DeterminantPos.Value;
								Microsoft.HostIntegration.TI.Linq.Object @object = determinant.Method.Object;
								resolutionEntry.InterfaceName = @object.PrimaryName + "." + @object.ClassName;
								resolutionEntry.Method = determinant.Method.Name;
								HipImport.FixDuplicateInterfaceMethodName(resolutionEntryCollection, ref resolutionEntry);
								if (determinant.View.SecurityPolicy.Source != 2)
								{
									resolutionEntry.EssoSecurityPolicyName = determinant.View.SecurityPolicy.Name;
								}
								else
								{
									resolutionEntry.EssoSecurityPolicyName = null;
								}
								resolutionEntryCollection.AddResolutionEntry(resolutionEntry);
							}
							elmUserData.ResolutionEntries = resolutionEntryCollection;
							service.ElmUserData = elmUserData;
						}
						else
						{
							service.ServiceName = "HipUserdataMS";
							ElmUserData elmUserData2 = new ElmUserData();
							elmUserData2.IsTypeDefined = true;
							elmUserData2.Hosts = text2;
							elmUserData2.Ports = text;
							elmUserData2.RequestHeaderFormatEnum = TcpRequestHeaderFormat.Microsoft;
							ResolutionEntryCollection resolutionEntryCollection = new ResolutionEntryCollection();
							foreach (Determinant determinant2 in determinantInformation.Determinants)
							{
								ResolutionEntry resolutionEntry = new ResolutionEntry();
								resolutionEntry.Data = determinant2.ResolutionString;
								resolutionEntry.Position = determinant2.DeterminantPos.Value;
								Microsoft.HostIntegration.TI.Linq.Object object2 = determinant2.Method.Object;
								resolutionEntry.InterfaceName = object2.PrimaryName + "." + object2.ClassName;
								resolutionEntry.Method = determinant2.Method.Name;
								HipImport.FixDuplicateInterfaceMethodName(resolutionEntryCollection, ref resolutionEntry);
								if (determinant2.View.SecurityPolicy.Source != 2)
								{
									resolutionEntry.EssoSecurityPolicyName = determinant2.View.SecurityPolicy.Name;
								}
								else
								{
									resolutionEntry.EssoSecurityPolicyName = null;
								}
								resolutionEntryCollection.AddResolutionEntry(resolutionEntry);
							}
							elmUserData2.ResolutionEntries = resolutionEntryCollection;
							service.ElmUserData = elmUserData2;
						}
					}
					else
					{
						service.ServiceName = "HipElmLink";
						ElmLink elmLink = new ElmLink();
						elmLink.IsTypeDefined = true;
						elmLink.Hosts = text2;
						elmLink.Ports = text;
						elmLink.RequestHeaderFormatEnum = TcpRequestHeaderFormat.Microsoft;
						ResolutionEntryCollection resolutionEntryCollection = new ResolutionEntryCollection();
						foreach (Determinant determinant3 in determinantInformation.Determinants)
						{
							ResolutionEntry resolutionEntry = new ResolutionEntry();
							resolutionEntry.LinkToProgram = determinant3.ResolutionString;
							Microsoft.HostIntegration.TI.Linq.Object object3 = determinant3.Method.Object;
							resolutionEntry.InterfaceName = object3.PrimaryName + "." + object3.ClassName;
							resolutionEntry.Method = determinant3.Method.Name;
							HipImport.FixDuplicateInterfaceMethodName(resolutionEntryCollection, ref resolutionEntry);
							if (determinant3.View.SecurityPolicy.Source != 2)
							{
								resolutionEntry.EssoSecurityPolicyName = determinant3.View.SecurityPolicy.Name;
							}
							else
							{
								resolutionEntry.EssoSecurityPolicyName = null;
							}
							resolutionEntryCollection.AddResolutionEntry(resolutionEntry);
						}
						elmLink.ResolutionEntries = resolutionEntryCollection;
						service.ElmLink = elmLink;
					}
				}
				else if (determinantType <= DeterminantType.NewSnaLink)
				{
					if (determinantType != DeterminantType.NewSnaEndpoint)
					{
						if (determinantType != DeterminantType.NewSnaLink)
						{
							goto IL_0AD9;
						}
						service.ServiceName = "HipSnaLink";
						SnaLink snaLink = new SnaLink();
						snaLink.IsTypeDefined = true;
						snaLink.Hosts = text2;
						snaLink.LocalLuName = determinantInformation.Determinants[0].LEEndpoint.LocalEnvironment.LUName;
						ResolutionEntryCollection resolutionEntryCollection = new ResolutionEntryCollection();
						foreach (Determinant determinant4 in determinantInformation.Determinants)
						{
							ResolutionEntry resolutionEntry = new ResolutionEntry();
							resolutionEntry.LinkToProgram = determinant4.ResolutionString;
							Microsoft.HostIntegration.TI.Linq.Object object4 = determinant4.Method.Object;
							resolutionEntry.InterfaceName = object4.PrimaryName + "." + object4.ClassName;
							resolutionEntry.Method = determinant4.Method.Name;
							HipImport.FixDuplicateInterfaceMethodName(resolutionEntryCollection, ref resolutionEntry);
							if (determinant4.View.SecurityPolicy.Source != 2)
							{
								resolutionEntry.EssoSecurityPolicyName = determinant4.View.SecurityPolicy.Name;
							}
							else
							{
								resolutionEntry.EssoSecurityPolicyName = null;
							}
							resolutionEntryCollection.AddResolutionEntry(resolutionEntry);
						}
						snaLink.ResolutionEntries = resolutionEntryCollection;
						service.SnaLink = snaLink;
					}
					else
					{
						service.ServiceName = "HipSnaEndpoint";
						SnaEndpoint snaEndpoint = new SnaEndpoint();
						snaEndpoint.IsTypeDefined = true;
						snaEndpoint.Hosts = text2;
						snaEndpoint.LocalLuName = determinantInformation.Determinants[0].LEEndpoint.LocalEnvironment.LUName;
						ResolutionEntryCollection resolutionEntryCollection = new ResolutionEntryCollection();
						foreach (Determinant determinant5 in determinantInformation.Determinants)
						{
							ResolutionEntry resolutionEntry = new ResolutionEntry();
							resolutionEntry.Endpoint = determinant5.ResolutionString;
							Microsoft.HostIntegration.TI.Linq.Object object5 = determinant5.Method.Object;
							resolutionEntry.InterfaceName = object5.PrimaryName + "." + object5.ClassName;
							resolutionEntry.Method = determinant5.Method.Name;
							HipImport.FixDuplicateInterfaceMethodName(resolutionEntryCollection, ref resolutionEntry);
							if (determinant5.View.SecurityPolicy.Source != 2)
							{
								resolutionEntry.EssoSecurityPolicyName = determinant5.View.SecurityPolicy.Name;
							}
							else
							{
								resolutionEntry.EssoSecurityPolicyName = null;
							}
							resolutionEntryCollection.AddResolutionEntry(resolutionEntry);
						}
						snaEndpoint.ResolutionEntries = resolutionEntryCollection;
						service.SnaEndpoint = snaEndpoint;
					}
				}
				else if (determinantType != DeterminantType.NewSnaUserData)
				{
					if (determinantType != DeterminantType.NewTrmLink)
					{
						goto IL_0AD9;
					}
					service.ServiceName = "HipTrmLink";
					TrmLink trmLink = new TrmLink();
					trmLink.IsTypeDefined = true;
					trmLink.Hosts = text2;
					trmLink.Ports = text;
					trmLink.RequestHeaderFormatEnum = TcpRequestHeaderFormat.Microsoft;
					ResolutionEntryCollection resolutionEntryCollection = new ResolutionEntryCollection();
					foreach (Determinant determinant6 in determinantInformation.Determinants)
					{
						ResolutionEntry resolutionEntry = new ResolutionEntry();
						resolutionEntry.LinkToProgram = determinant6.ResolutionString;
						Microsoft.HostIntegration.TI.Linq.Object object6 = determinant6.Method.Object;
						resolutionEntry.InterfaceName = object6.PrimaryName + "." + object6.ClassName;
						resolutionEntry.Method = determinant6.Method.Name;
						HipImport.FixDuplicateInterfaceMethodName(resolutionEntryCollection, ref resolutionEntry);
						if (determinant6.View.SecurityPolicy.Source != 2)
						{
							resolutionEntry.EssoSecurityPolicyName = determinant6.View.SecurityPolicy.Name;
						}
						else
						{
							resolutionEntry.EssoSecurityPolicyName = null;
						}
						resolutionEntryCollection.AddResolutionEntry(resolutionEntry);
					}
					trmLink.ResolutionEntries = resolutionEntryCollection;
					service.TrmLink = trmLink;
				}
				else
				{
					service.ServiceName = "HipSnaUserdata";
					SnaUserData snaUserData = new SnaUserData();
					snaUserData.IsTypeDefined = true;
					snaUserData.Hosts = text2;
					snaUserData.LocalLuName = determinantInformation.Determinants[0].LEEndpoint.LocalEnvironment.LUName;
					ResolutionEntryCollection resolutionEntryCollection = new ResolutionEntryCollection();
					foreach (Determinant determinant7 in determinantInformation.Determinants)
					{
						ResolutionEntry resolutionEntry = new ResolutionEntry();
						resolutionEntry.Data = determinant7.ResolutionString;
						resolutionEntry.Position = determinant7.DeterminantPos.Value;
						Microsoft.HostIntegration.TI.Linq.Object object7 = determinant7.Method.Object;
						resolutionEntry.InterfaceName = object7.PrimaryName + "." + object7.ClassName;
						resolutionEntry.Method = determinant7.Method.Name;
						HipImport.FixDuplicateInterfaceMethodName(resolutionEntryCollection, ref resolutionEntry);
						if (determinant7.View.SecurityPolicy.Source != 2)
						{
							resolutionEntry.EssoSecurityPolicyName = determinant7.View.SecurityPolicy.Name;
						}
						else
						{
							resolutionEntry.EssoSecurityPolicyName = null;
						}
						resolutionEntryCollection.AddResolutionEntry(resolutionEntry);
					}
					snaUserData.ResolutionEntries = resolutionEntryCollection;
					service.SnaUserData = snaUserData;
				}
				hipConfigurationSection.Services.AddService(service);
				continue;
				IL_0AD9:
				throw new ApplicationException("Invalid Determinant Type when creating services");
			}
		}
	}
}
