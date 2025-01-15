using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Editions;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000047 RID: 71
	public sealed class ExtensionClassFactory
	{
		// Token: 0x0600020A RID: 522 RVA: 0x00007B23 File Offset: 0x00005D23
		internal ExtensionClassFactory()
		{
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007B38 File Offset: 0x00005D38
		public void ClearAll()
		{
			Hashtable extensionTables = this.m_extensionTables;
			lock (extensionTables)
			{
				foreach (object obj in this.m_extensionTables.Values)
				{
					Hashtable hashtable = (Hashtable)obj;
					Hashtable hashtable2 = hashtable;
					lock (hashtable2)
					{
						foreach (object obj2 in hashtable.Values)
						{
							((ExtensionWrapper)obj2).MarkAsInvalid();
						}
						hashtable.Clear();
					}
				}
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007C34 File Offset: 0x00005E34
		public static void ClearAllExtensions()
		{
			ExtensionClassFactory.m_classFactory.ClearAll();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007C40 File Offset: 0x00005E40
		public static IExtension GetEventHandlerByEventType(string eventName)
		{
			IExtension extension = null;
			EventExtension eventExtension;
			if (ProcessingContext.Configuration.EventTypes.TryGetValue(eventName, out eventExtension))
			{
				extension = ExtensionClassFactory.GetNewInstanceExtensionClass(eventExtension.Name, "EventProcessing");
			}
			return extension;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00007C78 File Offset: 0x00005E78
		public static object GetNewCustomReportItemProcessingInstanceClass(string reportItemName)
		{
			Extension extensionConfigurationInfo = ExtensionClassFactory.m_classFactory.GetExtensionConfigurationInfo("ReportItems", reportItemName);
			return ExtensionClassFactory.m_classFactory.CreateExtensionObject(extensionConfigurationInfo, false);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007CA2 File Offset: 0x00005EA2
		public static IExtension GetSemanticQueryEngine()
		{
			return ExtensionClassFactory.m_classFactory.CreateAndInitializeExtensionInstance(ProcessingContext.Configuration.Extensions.SemanticQueryEngine);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007CC0 File Offset: 0x00005EC0
		public static IExtension GetNewInstanceExtensionClass(string extensionName, string extensionType)
		{
			Extension extensionConfigurationInfo = ExtensionClassFactory.m_classFactory.GetExtensionConfigurationInfo(extensionType, extensionName);
			return ExtensionClassFactory.m_classFactory.CreateAndInitializeExtensionInstance(extensionConfigurationInfo);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007CE8 File Offset: 0x00005EE8
		public static Type GetExtensionType(string extensionName, string extensionType)
		{
			Extension extensionConfigurationInfo = ExtensionClassFactory.m_classFactory.GetExtensionConfigurationInfo(extensionType, extensionName);
			return ExtensionClassFactory.m_classFactory.GetExtensionType(extensionConfigurationInfo);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007D10 File Offset: 0x00005F10
		private IExtension CreateAndInitializeExtensionInstance(Extension extConfig)
		{
			object obj = null;
			if (extConfig != null)
			{
				obj = this.CreateExtensionObject(extConfig, false);
				if (obj != null)
				{
					ExtensionClassFactory.ExtensionLoadFailReason extensionLoadFailReason = ExtensionClassFactory.ExtensionLoadFailReason.Loading;
					try
					{
						IExtension extension = obj as IExtension;
						if (extension != null)
						{
							try
							{
								extension.SetConfiguration(extConfig.Configuration);
							}
							catch
							{
								extensionLoadFailReason = ExtensionClassFactory.ExtensionLoadFailReason.ConfigurationSetting;
								throw;
							}
							ICatalogConfiguredExtension catalogConfiguredExtension = obj as ICatalogConfiguredExtension;
							if (catalogConfiguredExtension != null)
							{
								IEnumerable<string> enumerable = catalogConfiguredExtension.EnumerateRequiredProperties();
								if (enumerable != null && ProcessingContext.Configuration.OAuthConfiguration != null)
								{
									catalogConfiguredExtension.SetCatalogConfiguration(ProcessingContext.Configuration.OAuthConfiguration.GetProperties(enumerable));
								}
							}
						}
						else
						{
							if (string.Compare(extConfig.Type, "Data", StringComparison.Ordinal) != 0)
							{
								throw new ServerConfigurationErrorException(null, string.Format(CultureInfo.InvariantCulture, "Report server extension {0} does not implement IExtension interface.", extConfig.Name));
							}
							obj = this.WrapDataExtension(obj, extConfig.Name);
						}
					}
					catch (Exception ex)
					{
						this.ProcessExtensionLoadException(ex, extConfig, extensionLoadFailReason);
						obj = null;
					}
				}
			}
			return obj as IExtension;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007E04 File Offset: 0x00006004
		private Type GetExtensionType(Extension extConfig)
		{
			if (extConfig != null)
			{
				return this.CreateExtensionObject(extConfig, true) as Type;
			}
			return null;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007E18 File Offset: 0x00006018
		private IExtension WrapDataExtension(object iExtension, string extensionName)
		{
			IDbConnection dbConnection = iExtension as IDbConnection;
			if (dbConnection == null)
			{
				throw new ServerConfigurationErrorException(null, string.Format(CultureInfo.InvariantCulture, "Report Server Data Extension {0} does not implement IExtension or System.Data.IDbConnection.", extensionName));
			}
			ConnectionWrapper connectionWrapper = new ConnectionWrapper(dbConnection);
			connectionWrapper.WrappedManagedProvider = true;
			if (ExtensionClassFactory.m_Tracer.TraceInfo)
			{
				ExtensionClassFactory.m_Tracer.Trace(TraceLevel.Info, "A wrapper has been created for the connection to the {0} data source.", new object[] { extensionName });
			}
			return connectionWrapper;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00007E7C File Offset: 0x0000607C
		private object CreateExtensionObject(Extension extConfig, bool typeOnly)
		{
			object objExtension = null;
			if (extConfig != null)
			{
				try
				{
					RevertImpersonationContext.Run(delegate
					{
						Assembly assembly = this.LoadAssembly(extConfig.Assembly);
						if (typeOnly)
						{
							objExtension = assembly.GetType(extConfig.Class);
						}
						else
						{
							objExtension = assembly.CreateInstance(extConfig.Class);
						}
						ExtensionClassFactory.EnsureExtensionEnabled(extConfig, assembly.FullName);
					});
					if (objExtension == null)
					{
						throw new ServerConfigurationErrorException(null, "Could not create Extension of type: " + extConfig.Type + "name: " + extConfig.Name);
					}
				}
				catch (Exception ex)
				{
					this.ProcessExtensionLoadException(ex, extConfig, ExtensionClassFactory.ExtensionLoadFailReason.Loading);
					objExtension = null;
				}
			}
			return objExtension;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007F28 File Offset: 0x00006128
		internal static void EnsureExtensionEnabled(Extension extension, string assemblyFullName)
		{
			if (ProcessingContext.Configuration.IsExtensibilityEnabled)
			{
				return;
			}
			if (!KnownExtensions.IsKnownExtension(extension.Type, extension.ClassAndAssembly, assemblyFullName))
			{
				if (extension.Type != "Authentication" && extension.Type != "Security")
				{
					throw new OperationNotSupportedException(RestrictedFeatures.Extensibility.ToString());
				}
				if (!ProcessingContext.Configuration.IsCustomAuthEnabled)
				{
					throw new OperationNotSupportedException(RestrictedFeatures.CustomAuth.ToString());
				}
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007FB4 File Offset: 0x000061B4
		public static void AdjustRenderingExtensionsForSku(ExtensionsConfiguration extensionsConfig, SkuType sku)
		{
			List<Extension> list = new List<Extension>();
			foreach (object obj in extensionsConfig.Renderer)
			{
				Extension extension = (Extension)obj;
				list.Add(extension);
			}
			extensionsConfig.Renderer.Add(ExtensionClassFactory.GetSharedDataSetJsonRenderExtension());
			foreach (Extension extension2 in list)
			{
				ExtensionClassFactory.AdjustRenderingExtensionForSku(extension2, extensionsConfig, sku);
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00008064 File Offset: 0x00006264
		private static RenderingExtension GetSharedDataSetJsonRenderExtension()
		{
			return new RenderingExtension
			{
				Assembly = "Microsoft.ReportingServices.DataRendering",
				Class = "Microsoft.ReportingServices.Rendering.DataRenderer.JsonDataReport",
				LogAllExecutionRequests = true,
				Name = "SHAREDDATASETJSON",
				Visible = false,
				Type = "Render"
			};
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000080B0 File Offset: 0x000062B0
		public static void AdjustDataExtensionsForSku(ExtensionsConfiguration extensionsConfig, SkuType sku)
		{
			List<Extension> list = new List<Extension>();
			foreach (object obj in extensionsConfig.Data)
			{
				Extension extension = (Extension)obj;
				list.Add(extension);
			}
			foreach (Extension extension2 in list)
			{
				ExtensionClassFactory.EnsureDataExtensionEnabled(extension2, extensionsConfig, sku);
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000814C File Offset: 0x0000634C
		private static void AdjustRenderingExtensionForSku(Extension extension, ExtensionsConfiguration extensionsConfig, SkuType sku)
		{
			if (extension is RenderingExtension)
			{
				if (KnownExtensions.IsRestrictedRenderingExtension(sku, extension.ClassAndAssembly))
				{
					ExtensionClassFactory.RemoveExtensionFromConfigInfo(extension, extensionsConfig);
					if (RSTrace.ConfigManagerTracer.TraceInfo)
					{
						RSTrace.ConfigManagerTracer.Trace(TraceLevel.Info, "Extension " + extension.Name + " is not supported on this edition of Reporting Services and has been removed");
					}
				}
				if (KnownExtensions.IsHiddenRenderingExtension(sku, extension.ClassAndAssembly))
				{
					extension.Visible = false;
				}
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000081B8 File Offset: 0x000063B8
		private static void EnsureDataExtensionEnabled(Extension extension, ExtensionsConfiguration extensionsConfig, SkuType sku)
		{
			if (extension.Type == "Data" && KnownExtensions.IsRestrictedDataExtension(sku, extension.ClassAndAssembly))
			{
				ExtensionClassFactory.RemoveExtensionFromConfigInfo(extension, extensionsConfig);
				if (RSTrace.ConfigManagerTracer.TraceInfo)
				{
					RSTrace.ConfigManagerTracer.Trace(TraceLevel.Info, "Extension " + extension.Name + " is not supported on this edition of Reporting Services and has been removed");
				}
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00008218 File Offset: 0x00006418
		private void ProcessExtensionLoadException(Exception e, Extension extConfig, ExtensionClassFactory.ExtensionLoadFailReason reason)
		{
			if (reason == ExtensionClassFactory.ExtensionLoadFailReason.ConfigurationSetting)
			{
				RSEventLog.Current.WriteError(Event.SetExtensionConfigFailed, new object[] { extConfig.Name });
			}
			else
			{
				RSEventLog.Current.WriteError(Event.CouldNotLoadExtension, new object[]
				{
					RSEventLog.Current.SourceName,
					extConfig.Name
				});
			}
			if (ExtensionClassFactory.m_Tracer.TraceInfo && e.GetType().IsAssignableFrom(typeof(NotEnabledException)))
			{
				ExtensionClassFactory.m_Tracer.Trace(TraceLevel.Verbose, "Skipped instantiating {0} report server extension. Extension was not enabled.", new object[] { extConfig.Name });
				return;
			}
			if (ExtensionClassFactory.m_Tracer.TraceError)
			{
				ExtensionClassFactory.m_Tracer.TraceException(TraceLevel.Error, "Exception caught instantiating {0} report server extension: {1}.", new object[]
				{
					extConfig.Name,
					e.ToString()
				});
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000082E8 File Offset: 0x000064E8
		private Assembly LoadAssembly(string name)
		{
			Assembly assembly = null;
			try
			{
				assembly = Assembly.Load(name);
			}
			catch (FileNotFoundException ex)
			{
				if (ProcessingContext.Configuration.CurrentApplication != RunningApplication.ReportServerWebApp)
				{
					throw new ServerConfigurationErrorException(ex, "Unable to load assembly " + name);
				}
				assembly = this.LoadAssemblyFromFile(name);
			}
			return assembly;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008340 File Offset: 0x00006540
		private Assembly LoadAssemblyFromFile(string name)
		{
			string reportServerBinDirectory = this.GetReportServerBinDirectory();
			Assembly assembly;
			try
			{
				AssemblyName assemblyName = new AssemblyName(name);
				assemblyName.CodeBase = string.Format("{0}.dll", Path.Combine(reportServerBinDirectory, assemblyName.Name));
				assembly = Assembly.Load(assemblyName);
			}
			catch (FileNotFoundException ex)
			{
				throw new ServerConfigurationErrorException(ex, "Unable to load assembly " + name);
			}
			return assembly;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000083A4 File Offset: 0x000065A4
		private string GetReportServerBinDirectory()
		{
			string text = ConfigurationManager.AppSettings["ReportServerPath"];
			if (!string.IsNullOrEmpty(text))
			{
				text = Path.Combine(text, "bin");
			}
			else
			{
				text = Directory.GetCurrentDirectory();
				text = text.Replace("Portal", "ReportServer\\Bin");
			}
			return text;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000083F0 File Offset: 0x000065F0
		private Extension GetExtensionConfigurationInfo(string extensionType, string extensionName)
		{
			Extension extension = null;
			if (ProcessingContext.Configuration != null && ProcessingContext.Configuration.Extensions != null && extensionType != null)
			{
				int length = extensionType.Length;
				switch (length)
				{
				case 4:
					if (extensionType == "Data")
					{
						extension = ProcessingContext.Configuration.Extensions.Data[extensionName];
					}
					break;
				case 5:
				case 7:
				case 9:
				case 12:
				case 16:
				case 17:
					break;
				case 6:
					if (extensionType == "Render")
					{
						extension = ProcessingContext.Configuration.Extensions.Renderer[extensionName];
					}
					break;
				case 8:
				{
					char c = extensionType[2];
					if (c != 'c')
					{
						if (c != 'l')
						{
							if (c == 's')
							{
								if (extensionType == "Designer")
								{
									extension = ProcessingContext.Configuration.Extensions.Designer[extensionName];
								}
							}
						}
						else if (extensionType == "Delivery")
						{
							extension = ProcessingContext.Configuration.Extensions.Delivery[extensionName];
						}
					}
					else if (extensionType == "Security")
					{
						extension = ProcessingContext.Configuration.Extensions.Security[extensionName];
					}
					break;
				}
				case 10:
					if (extensionType == "DeliveryUI")
					{
						extension = ProcessingContext.Configuration.Extensions.DeliveryUI[extensionName];
					}
					break;
				case 11:
					if (extensionType == "ReportItems")
					{
						extension = ProcessingContext.Configuration.Extensions.ReportItems[extensionName];
					}
					break;
				case 13:
					if (extensionType == "SemanticQuery")
					{
						extension = ProcessingContext.Configuration.Extensions.SemanticQuery[extensionName];
					}
					break;
				case 14:
					if (extensionType == "Authentication")
					{
						extension = ProcessingContext.Configuration.Extensions.Authentication[extensionName];
					}
					break;
				case 15:
				{
					char c = extensionType[0];
					if (c != 'E')
					{
						if (c == 'M')
						{
							if (extensionType == "ModelGeneration")
							{
								extension = ProcessingContext.Configuration.Extensions.ModelGeneration[extensionName];
							}
						}
					}
					else if (extensionType == "EventProcessing")
					{
						extension = ProcessingContext.Configuration.Extensions.Event[extensionName];
					}
					break;
				}
				case 18:
					if (extensionType == "ReportItemDesigner")
					{
						extension = ProcessingContext.Configuration.Extensions.ReportItemDesigner[extensionName];
					}
					break;
				default:
					if (length == 29)
					{
						if (extensionType == "ReportDefinitionCustomization")
						{
							extension = ProcessingContext.Configuration.Extensions.ReportDefinitionCustomization;
						}
					}
					break;
				}
			}
			return extension;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00008703 File Offset: 0x00006903
		internal static void RemoveExtensionFromConfigInfo(Extension extension)
		{
			ExtensionClassFactory.RemoveExtensionFromConfigInfo(extension, ProcessingContext.Configuration.Extensions);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008718 File Offset: 0x00006918
		private static void RemoveExtensionFromConfigInfo(Extension extension, ExtensionsConfiguration extensionsConfig)
		{
			string type = extension.Type;
			if (type != null)
			{
				int length = type.Length;
				object obj;
				switch (length)
				{
				case 4:
					if (!(type == "Data"))
					{
						return;
					}
					obj = extensionsConfig.Data.SyncRoot;
					lock (obj)
					{
						extensionsConfig.Data.Remove(extension);
						return;
					}
					break;
				case 5:
				case 7:
				case 9:
					return;
				case 6:
					if (!(type == "Render"))
					{
						return;
					}
					goto IL_017C;
				case 8:
				{
					char c = type[2];
					if (c != 'c')
					{
						if (c != 'l')
						{
							if (c != 's')
							{
								return;
							}
							if (!(type == "Designer"))
							{
								return;
							}
							goto IL_0242;
						}
						else if (!(type == "Delivery"))
						{
							return;
						}
					}
					else
					{
						if (!(type == "Security"))
						{
							return;
						}
						goto IL_01AF;
					}
					break;
				}
				case 10:
					if (!(type == "DeliveryUI"))
					{
						return;
					}
					goto IL_0212;
				default:
					if (length != 14)
					{
						if (length != 15)
						{
							return;
						}
						if (!(type == "EventProcessing"))
						{
							return;
						}
						goto IL_0149;
					}
					else
					{
						if (!(type == "Authentication"))
						{
							return;
						}
						goto IL_01E2;
					}
					break;
				}
				obj = extensionsConfig.Delivery.SyncRoot;
				lock (obj)
				{
					extensionsConfig.Delivery.Remove(extension);
					return;
				}
				IL_0149:
				obj = extensionsConfig.Event.SyncRoot;
				lock (obj)
				{
					extensionsConfig.Event.Remove(extension);
					return;
				}
				IL_017C:
				obj = extensionsConfig.Renderer.SyncRoot;
				lock (obj)
				{
					extensionsConfig.Renderer.Remove(extension);
					return;
				}
				IL_01AF:
				obj = extensionsConfig.Security.SyncRoot;
				lock (obj)
				{
					extensionsConfig.Security.Remove(extension);
					return;
				}
				IL_01E2:
				obj = extensionsConfig.Authentication.SyncRoot;
				lock (obj)
				{
					extensionsConfig.Authentication.Remove(extension);
					return;
				}
				IL_0212:
				obj = extensionsConfig.DeliveryUI.SyncRoot;
				lock (obj)
				{
					extensionsConfig.DeliveryUI.Remove(extension);
					return;
				}
				IL_0242:
				obj = extensionsConfig.Designer.SyncRoot;
				lock (obj)
				{
					extensionsConfig.Designer.Remove(extension);
				}
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000089FC File Offset: 0x00006BFC
		public static string GetExtensionLocalizedName(Extension extConfig)
		{
			return ExtensionClassFactory.GetExtensionLocalizedName(extConfig, true);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00008A08 File Offset: 0x00006C08
		public static string GetExtensionLocalizedName(Extension extConfig, bool removeFailuresAndContinue)
		{
			if (extConfig == null)
			{
				return null;
			}
			try
			{
				ReturnValue result = null;
				RevertImpersonationContext.Run(delegate
				{
					LocalizedNameAttribute[] array = (LocalizedNameAttribute[])ExtensionClassFactory.m_classFactory.LoadAssembly(extConfig.Assembly).GetType(extConfig.Class, true).GetCustomAttributes(typeof(LocalizedNameAttribute), true);
					if (array.Length != 0)
					{
						result = new ReturnValue(array[0].Name);
					}
				});
				if (result != null)
				{
					return (string)result.Value;
				}
			}
			catch (Exception ex)
			{
				ExtensionClassFactory.m_classFactory.ProcessExtensionLoadException(ex, extConfig, ExtensionClassFactory.ExtensionLoadFailReason.Loading);
				if (removeFailuresAndContinue)
				{
					return null;
				}
				throw;
			}
			if (RSTrace.ExtensionFactoryTracer.TraceInfo)
			{
				RSTrace.ExtensionFactoryTracer.Trace(TraceLevel.Warning, "The extension {0} does not have a LocalizedNameAttribute.", new object[] { extConfig.Name });
			}
			IExtension newInstanceExtensionClass = ExtensionClassFactory.GetNewInstanceExtensionClass(extConfig.Name, extConfig.Type);
			if (newInstanceExtensionClass != null)
			{
				string text = newInstanceExtensionClass.LocalizedName;
				if (text == null)
				{
					text = extConfig.Name;
				}
				return text;
			}
			return null;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00008B00 File Offset: 0x00006D00
		public static bool IsRegisteredCustomReportItemExtension(string extensionType)
		{
			return ExtensionClassFactory.m_classFactory.GetExtensionConfigurationInfo("ReportItems", extensionType) != null;
		}

		// Token: 0x04000100 RID: 256
		private Hashtable m_extensionTables = new Hashtable();

		// Token: 0x04000101 RID: 257
		private static ExtensionClassFactory m_classFactory = new ExtensionClassFactory();

		// Token: 0x04000102 RID: 258
		internal static RSTrace m_Tracer = RSTrace.ExtensionFactoryTracer;

		// Token: 0x04000103 RID: 259
		public const string InternalSharedDataSetJsonRendererName = "SHAREDDATASETJSON";

		// Token: 0x020000E5 RID: 229
		private enum ExtensionLoadFailReason
		{
			// Token: 0x040004A2 RID: 1186
			ConfigurationSetting,
			// Token: 0x040004A3 RID: 1187
			Loading
		}
	}
}
