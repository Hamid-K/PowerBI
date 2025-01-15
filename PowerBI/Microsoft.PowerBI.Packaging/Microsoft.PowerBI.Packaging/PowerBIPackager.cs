using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Microsoft.PowerBI.Packaging.Storage;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000024 RID: 36
	public static class PowerBIPackager
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00003DF7 File Offset: 0x00001FF7
		public static Uri DataModelPath
		{
			get
			{
				return PowerBIPackager.dataModelPath;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00003DFE File Offset: 0x00001FFE
		public static Uri DataModelSchemaPath
		{
			get
			{
				return PowerBIPackager.dataModelSchemaPath;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00003E05 File Offset: 0x00002005
		public static Uri CustomPropertiesPath
		{
			get
			{
				return PowerBIPackager.customPropertiesPath;
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003E0C File Offset: 0x0000200C
		public static IEnumerable<Uri> GetAllPartPaths()
		{
			yield return PowerBIPackager.connectionsPath;
			yield return PowerBIPackager.customVisualsPath;
			yield return PowerBIPackager.dataModelPath;
			yield return PowerBIPackager.dataModelSchemaPath;
			yield return PowerBIPackager.diagramStatePath;
			yield return PowerBIPackager.diagramLayoutPath;
			yield return PowerBIPackager.mashupPath;
			yield return PowerBIPackager.unappliedChangesPath;
			yield return PowerBIPackager.reportLayoutPath;
			yield return PowerBIPackager.reportLinguisticSchemaPath;
			yield return PowerBIPackager.reportMetadataPath;
			yield return PowerBIPackager.reportSettingsPath;
			yield return SecurityBindingsPackagePart.SecurityBindingsPath;
			yield return PowerBIPackager.staticResourcesPath;
			yield return PowerBIPackager.versionPath;
			yield return PowerBIPackager.customPropertiesPath;
			yield return PowerBIPackager.reportMobileStatePath;
			yield return PowerBIPackager.daxQueryViewPath;
			yield return PowerBIPackager.explorationPath;
			yield break;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003E18 File Offset: 0x00002018
		public static IPowerBIPackage Open(Stream fileStream, bool skipValidation = false)
		{
			bool flag;
			byte[] array;
			Version version;
			return PowerBIPackager.Open(fileStream, out flag, out array, out version, skipValidation);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003E34 File Offset: 0x00002034
		public static IPowerBIPackage Open(Stream storageStream, out bool requiresConversionToReportLayout, out byte[] securityBindings, out Version pbixFileVersion, bool skipValidation = false)
		{
			Package package = Package.Open(storageStream, FileMode.Open, FileAccess.Read);
			if (!skipValidation)
			{
				PowerBIPackager.Validate(package, out pbixFileVersion);
			}
			else
			{
				pbixFileVersion = null;
			}
			PowerBIPackagePart packagePart = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.Connections);
			PowerBIPackagePart packagePart2 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.DataModel);
			PowerBIPackagePart packagePart3 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.DataModelSchema);
			PowerBIPackagePart packagePart4 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.DiagramState);
			PowerBIPackagePart packagePart5 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.DiagramLayout);
			PowerBIPackagePart packagePart6 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.Mashup);
			PowerBIPackagePart packagePart7 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.UnappliedChanges);
			PowerBIPackagePart packagePart8 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.ReportLinguisticSchema);
			PowerBIPackagePart packagePart9 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.ReportMetadata);
			PowerBIPackagePart packagePart10 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.ReportSettings);
			PowerBIPackagePart packagePart11 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.SecuirtyBindings);
			PowerBIPackagePart packagePart12 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.Version);
			PowerBIPackagePart packagePart13 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.CustomProperties);
			PowerBIPackagePart packagePart14 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.ReportMobileState);
			PowerBIPackageCompositePart packageCompositePart = PowerBIPackager.GetPackageCompositePart(package, PowerBIPackager.PowerBIPackageParts.CustomVisuals);
			PowerBIPackageCompositePart packageCompositePart2 = PowerBIPackager.GetPackageCompositePart(package, PowerBIPackager.PowerBIPackageParts.StaticResources);
			PowerBIPackageCompositePart packageCompositePart3 = PowerBIPackager.GetPackageCompositePart(package, PowerBIPackager.PowerBIPackageParts.DaxQueryView);
			PowerBIPackageCompositePart packageCompositePart4 = PowerBIPackager.GetPackageCompositePart(package, PowerBIPackager.PowerBIPackageParts.Exploration);
			IDictionary<Uri, IStreamablePowerBIPackagePartContent> subPartsContents = packageCompositePart.GetSubPartsContents();
			IDictionary<Uri, IStreamablePowerBIPackagePartContent> subPartsContents2 = packageCompositePart2.GetSubPartsContents();
			requiresConversionToReportLayout = package.PartExists(PowerBIPackager.reportDocumentPath);
			securityBindings = ((SecurityBindingsPackagePart)packagePart11).Deserialize(packagePart11.GetContent());
			return new PowerBIPackage(package, packagePart.GetContent(), packagePart6.GetContent(), packagePart7.GetContent(), packagePart2.GetContent(), packagePart3.GetContent(), packagePart4.GetContent(), packagePart5.GetContent(), PowerBIPackager.GetReportDocumentContent(package), packagePart8.GetContent(), packagePart9.GetContent(), packagePart10.GetContent(), packagePart12.GetContent(), packagePart13.GetContent(), packagePart14.GetContent(), subPartsContents, subPartsContents2, packageCompositePart3.GetSubPartsContents(), packageCompositePart4.GetSubPartsContents());
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003FA9 File Offset: 0x000021A9
		public static void Save(IPowerBIPackage powerbiPackage, Stream storageStream, bool forceStaticResourceUrisValid = false)
		{
			PowerBIPackager.Save(powerbiPackage, storageStream, true, forceStaticResourceUrisValid);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003FB4 File Offset: 0x000021B4
		public static void Save(IPowerBIPackage powerbiPackage, Stream storageStream, bool includeData, bool forceStaticResourceUrisValid = false)
		{
			Exception ex = null;
			try
			{
				using (Package package = Package.Open(storageStream, FileMode.OpenOrCreate, FileAccess.ReadWrite))
				{
					try
					{
						bool flag = storageStream.Length == 0L;
						PowerBIPackagePart packagePart = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.Connections);
						PowerBIPackagePart packagePart2 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.DataModel);
						PowerBIPackagePart packagePart3 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.DataModelSchema);
						PowerBIPackagePart packagePart4 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.DiagramState);
						PowerBIPackagePart packagePart5 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.DiagramLayout);
						PowerBIPackagePart packagePart6 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.Mashup);
						PowerBIPackagePart packagePart7 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.UnappliedChanges);
						PowerBIPackagePart packagePart8 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.ReportDocument);
						PowerBIPackagePart packagePart9 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.ReportLinguisticSchema);
						PowerBIPackagePart packagePart10 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.ReportMetadata);
						PowerBIPackagePart packagePart11 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.ReportSettings);
						PowerBIPackagePart packagePart12 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.SecuirtyBindings);
						PowerBIPackagePart packagePart13 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.Version);
						PowerBIPackagePart packagePart14 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.CustomProperties);
						PowerBIPackagePart packagePart15 = PowerBIPackager.GetPackagePart(package, PowerBIPackager.PowerBIPackageParts.ReportMobileState);
						PowerBIPackageCompositePart packageCompositePart = PowerBIPackager.GetPackageCompositePart(package, PowerBIPackager.PowerBIPackageParts.CustomVisuals);
						PowerBIPackageCompositePart packageCompositePart2 = PowerBIPackager.GetPackageCompositePart(package, PowerBIPackager.PowerBIPackageParts.StaticResources);
						PowerBIPackageCompositePart packageCompositePart3 = PowerBIPackager.GetPackageCompositePart(package, PowerBIPackager.PowerBIPackageParts.DaxQueryView);
						PowerBIPackageCompositePart packageCompositePart4 = PowerBIPackager.GetPackageCompositePart(package, PowerBIPackager.PowerBIPackageParts.Exploration);
						packagePart13.SetContent(powerbiPackage.Version);
						if (flag)
						{
							package.Flush();
						}
						if (!flag || powerbiPackage.DataMashup != null || powerbiPackage.UnappliedChanges != null)
						{
							packagePart6.SetContent(powerbiPackage.DataMashup);
							packagePart7.SetContent(powerbiPackage.UnappliedChanges);
						}
						if (powerbiPackage.DataModelSchema != null)
						{
							packagePart3.SetContent(powerbiPackage.DataModelSchema);
						}
						if (powerbiPackage.DataModel != null)
						{
							packagePart2.SetContent(powerbiPackage.DataModel);
						}
						if (!includeData)
						{
							packagePart2.SetContent(null);
						}
						if (powerbiPackage.DiagramLayout != null)
						{
							packagePart5.SetContent(powerbiPackage.DiagramLayout);
						}
						packagePart8.SetContent(powerbiPackage.ReportDocument);
						packagePart11.SetContent(powerbiPackage.ReportSettings);
						packagePart10.SetContent(powerbiPackage.ReportMetadata);
						packagePart9.SetContent(powerbiPackage.LinguisticSchema);
						packagePart4.SetContent(powerbiPackage.DiagramViewState);
						packagePart.SetContent(powerbiPackage.Connections);
						HashSet<Uri> subParts = packageCompositePart.GetSubParts();
						if (powerbiPackage.CustomVisuals != null)
						{
							foreach (KeyValuePair<Uri, IStreamablePowerBIPackagePartContent> keyValuePair in powerbiPackage.CustomVisuals)
							{
								if (subParts != null)
								{
									subParts.Remove(keyValuePair.Key);
								}
								packageCompositePart.SetContent(keyValuePair.Key, keyValuePair.Value);
							}
						}
						PowerBIPackager.RemoveUnusedCustomVisuals(subParts, packageCompositePart);
						HashSet<Uri> subParts2 = packageCompositePart2.GetSubParts();
						if (powerbiPackage.StaticResources != null)
						{
							foreach (KeyValuePair<Uri, IStreamablePowerBIPackagePartContent> keyValuePair2 in powerbiPackage.StaticResources)
							{
								Uri uri2 = keyValuePair2.Key;
								if (forceStaticResourceUrisValid)
								{
									string text = PackUriHelper.CreatePartUri(keyValuePair2.Key).OriginalString;
									text = (text.StartsWith("/", StringComparison.OrdinalIgnoreCase) ? text.Substring(1) : text);
									uri2 = new Uri(text, UriKind.Relative);
								}
								packageCompositePart2.SetContent(uri2, keyValuePair2.Value);
								subParts2.Remove(keyValuePair2.Key);
							}
						}
						if (powerbiPackage.CustomProperties != null)
						{
							packagePart14.SetContent(powerbiPackage.CustomProperties);
						}
						if (powerbiPackage.ReportMobileState != null)
						{
							packagePart15.SetContent(powerbiPackage.ReportMobileState);
						}
						PowerBIPackager.RemoveUnusedParts(package, subParts2.Select((Uri uri) => new Uri(PowerBIPackager.staticResourcesPath.ToString() + "/" + uri.ToString(), UriKind.Relative)));
						PowerBIPackager.SaveCompositePartContent(powerbiPackage.DaxQueryView, packageCompositePart3);
						PowerBIPackager.SaveCompositePartContent(powerbiPackage.Exploration, packageCompositePart4);
						package.Flush();
						IStreamablePowerBIPackagePartContent streamablePowerBIPackagePartContent = ((SecurityBindingsPackagePart)packagePart12).Serialize();
						packagePart12.SetContent(streamablePowerBIPackagePartContent);
						package.Flush();
					}
					catch (Exception ex)
					{
						throw;
					}
				}
				PowerBIPackager.Validate(storageStream);
			}
			catch (Exception)
			{
				if (ex != null)
				{
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				throw;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000043B0 File Offset: 0x000025B0
		private static void SaveCompositePartContent(IDictionary<Uri, IStreamablePowerBIPackagePartContent> compositePartContents, PowerBIPackageCompositePart compositePart)
		{
			HashSet<Uri> subParts = compositePart.GetSubParts();
			if (compositePartContents != null)
			{
				foreach (KeyValuePair<Uri, IStreamablePowerBIPackagePartContent> keyValuePair in compositePartContents)
				{
					subParts.Remove(keyValuePair.Key);
					compositePart.SetContent(keyValuePair.Key, keyValuePair.Value);
				}
			}
			foreach (Uri uri in subParts)
			{
				compositePart.DeletePart(uri);
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000445C File Offset: 0x0000265C
		public static Task SaveAsync(IPowerBIPackage powerbiPackage, Stream storageStream, bool includeData, bool forceStaticResourceUrisValid = false)
		{
			return Task.Run(delegate
			{
				PowerBIPackager.Save(powerbiPackage, storageStream, includeData, forceStaticResourceUrisValid);
			});
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004490 File Offset: 0x00002690
		public static void Validate(Stream storageStream)
		{
			try
			{
				using (Package package = Package.Open(storageStream, FileMode.Open, FileAccess.Read))
				{
					Version version;
					PowerBIPackager.Validate(package, out version);
				}
			}
			catch (Exception ex)
			{
				throw new PackageValidationException(ex);
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000044E0 File Offset: 0x000026E0
		public static bool IsPowerBILiveConnect(string connectionType)
		{
			StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
			return !string.IsNullOrEmpty(connectionType) && (ordinalIgnoreCase.Equals(connectionType, "pbiServiceLive") || ordinalIgnoreCase.Equals(connectionType, "pbiServiceXmlaStyleLive") || ordinalIgnoreCase.Equals(connectionType, "pbiserviceOnPremLive"));
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004528 File Offset: 0x00002728
		public static Version LatestApplicableVersion(bool usingExternalDataSet = false, bool hasUnappliedQueriesAsJson = false, bool hasDaxQueryState = false, bool hasEnhancedReportContent = false)
		{
			Version version = PowerBIPackager.CurrentVersion;
			Action<bool, Version> action = delegate(bool needsVersion, Version requiredVersion)
			{
				if (needsVersion && version < requiredVersion)
				{
					version = requiredVersion;
				}
			};
			action(usingExternalDataSet, PowerBIPackager.ExternalDatasetVersion);
			action(hasUnappliedQueriesAsJson, PowerBIPackager.UnappliedChangesJsonVersion);
			action(hasDaxQueryState, PowerBIPackager.DaxQueryViewVersion);
			action(hasEnhancedReportContent, PowerBIPackager.EnhancedReportDocumentVersion);
			return version;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004585 File Offset: 0x00002785
		public static QueriesSettings GetDefaultQueriesSettings(string version)
		{
			return new QueriesSettings
			{
				TypeDetectionEnabled = true,
				RelationshipImportEnabled = true,
				RelationshipRefreshEnabled = false,
				RunBackgroundAnalysis = true,
				Version = version
			};
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000045B0 File Offset: 0x000027B0
		public static void EnsureCompatiblePackageFormat(IPowerBIPackage powerBIPackage, Version targetVersion)
		{
			Version version;
			PowerBIPackager.ValidateVersion(PowerBIPackagingUtils.GetContentAsString(powerBIPackage.Version, PowerBIPackager.IsVersionOptional, null), out version);
			if (version >= targetVersion)
			{
				return;
			}
			IStreamablePowerBIPackagePartContent streamablePowerBIPackagePartContent = new StreamablePowerBIPackagePartContent(targetVersion.ToString(), "");
			powerBIPackage.Version = streamablePowerBIPackagePartContent;
			if (version < PowerBIPackager.V3ModelsVersion)
			{
				ReportSettingsContainer reportSettingsContainer;
				if (ReportSettingsUtils.TryDeserializeReportSettings(powerBIPackage.ReportSettings, version, out reportSettingsContainer))
				{
					IStreamablePowerBIPackagePartContent streamablePowerBIPackagePartContent2 = ReportSettingsUtils.SerializeReportSettings(reportSettingsContainer);
					powerBIPackage.ReportSettings = streamablePowerBIPackagePartContent2;
				}
				ReportMetadataContainer reportMetadataContainer;
				if (ReportMetadataUtils.TryDeserializeReportMetadata(powerBIPackage.ReportMetadata, version, out reportMetadataContainer))
				{
					IStreamablePowerBIPackagePartContent streamablePowerBIPackagePartContent3 = ReportMetadataUtils.SerializeReportMetadata(reportMetadataContainer);
					powerBIPackage.ReportMetadata = streamablePowerBIPackagePartContent3;
				}
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004644 File Offset: 0x00002844
		private static IEnumerable<PowerBIPackagePart> GetAllParts(Package storagePackage)
		{
			yield return PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.Connections);
			yield return PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.CustomVisuals);
			yield return PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.DataModel);
			yield return PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.DataModelSchema);
			yield return PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.DiagramState);
			yield return PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.DiagramLayout);
			yield return PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.Mashup);
			yield return PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.ReportDocument);
			yield return PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.ReportMetadata);
			yield return PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.ReportSettings);
			yield return PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.ReportLinguisticSchema);
			yield return PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.SecuirtyBindings);
			yield return PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.StaticResources);
			yield return PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.Version);
			yield return PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.CustomProperties);
			yield return PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.ReportMobileState);
			yield return PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.DaxQueryView);
			yield return PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.Exploration);
			yield break;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004654 File Offset: 0x00002854
		private static PowerBIPackagePart GetPackagePart(Package storagePackage, PowerBIPackager.PowerBIPackageParts powerBiPackageParts)
		{
			switch (powerBiPackageParts)
			{
			case PowerBIPackager.PowerBIPackageParts.Connections:
				return new PowerBIPackagePart(storagePackage, PowerBIPackager.connectionsPath, PowerBIPackager.IsConnectionsOptional, CompressionOption.Normal);
			case PowerBIPackager.PowerBIPackageParts.CustomVisuals:
				return new PowerBIPackagePart(storagePackage, PowerBIPackager.customVisualsPath, PowerBIPackager.IsCustomVisualsOptional, CompressionOption.Normal);
			case PowerBIPackager.PowerBIPackageParts.DataModel:
				return new PowerBIPackagePart(storagePackage, PowerBIPackager.dataModelPath, PowerBIPackager.IsDataModelOptional, CompressionOption.NotCompressed);
			case PowerBIPackager.PowerBIPackageParts.DataModelSchema:
				return new PowerBIPackagePart(storagePackage, PowerBIPackager.dataModelSchemaPath, PowerBIPackager.IsDataModelSchemaOptional, CompressionOption.Normal);
			case PowerBIPackager.PowerBIPackageParts.DiagramState:
				return new PowerBIPackagePart(storagePackage, PowerBIPackager.diagramStatePath, PowerBIPackager.IsDiagramStateOptional, CompressionOption.Normal);
			case PowerBIPackager.PowerBIPackageParts.Mashup:
				return new PowerBIPackagePart(storagePackage, PowerBIPackager.mashupPath, PowerBIPackager.IsMashupOptional, CompressionOption.Normal);
			case PowerBIPackager.PowerBIPackageParts.UnappliedChanges:
				return new PowerBIPackagePart(storagePackage, PowerBIPackager.unappliedChangesPath, PowerBIPackager.IsUnappliedChangesOptional, CompressionOption.Normal);
			case PowerBIPackager.PowerBIPackageParts.DiagramLayout:
				return new PowerBIPackagePart(storagePackage, PowerBIPackager.diagramLayoutPath, PowerBIPackager.IsDiagramLayoutOptional, CompressionOption.Normal);
			case PowerBIPackager.PowerBIPackageParts.ReportDocument:
				return new PowerBIPackagePart(storagePackage, PowerBIPackager.reportLayoutPath, PowerBIPackager.IsReportDocumentOptional, CompressionOption.Normal);
			case PowerBIPackager.PowerBIPackageParts.ReportLinguisticSchema:
				return new PowerBIPackagePart(storagePackage, PowerBIPackager.reportLinguisticSchemaPath, PowerBIPackager.IsLinguisticSchemaOptional, CompressionOption.Normal);
			case PowerBIPackager.PowerBIPackageParts.ReportMetadata:
				return new PowerBIPackagePart(storagePackage, PowerBIPackager.reportMetadataPath, PowerBIPackager.IsReportMetadataOptional, CompressionOption.Normal);
			case PowerBIPackager.PowerBIPackageParts.ReportSettings:
				return new PowerBIPackagePart(storagePackage, PowerBIPackager.reportSettingsPath, PowerBIPackager.IsReportSettingsOptional, CompressionOption.Normal);
			case PowerBIPackager.PowerBIPackageParts.SecuirtyBindings:
				return new SecurityBindingsPackagePart(storagePackage);
			case PowerBIPackager.PowerBIPackageParts.StaticResources:
				return new PowerBIPackagePart(storagePackage, PowerBIPackager.staticResourcesPath, PowerBIPackager.IsStaticResourcesOptional, CompressionOption.Normal);
			case PowerBIPackager.PowerBIPackageParts.Version:
				return new PowerBIPackagePart(storagePackage, PowerBIPackager.versionPath, PowerBIPackager.IsVersionOptional, CompressionOption.Normal);
			case PowerBIPackager.PowerBIPackageParts.CustomProperties:
				return new PowerBIPackagePart(storagePackage, PowerBIPackager.customPropertiesPath, PowerBIPackager.IsCustomPropertiesOptional, CompressionOption.Normal);
			case PowerBIPackager.PowerBIPackageParts.ReportMobileState:
				return new PowerBIPackagePart(storagePackage, PowerBIPackager.reportMobileStatePath, PowerBIPackager.IsReportMobileStateOptional, CompressionOption.Normal);
			case PowerBIPackager.PowerBIPackageParts.DaxQueryView:
				return new PowerBIPackagePart(storagePackage, PowerBIPackager.daxQueryViewPath, PowerBIPackager.IsDaxQueryViewOptional, CompressionOption.Normal);
			case PowerBIPackager.PowerBIPackageParts.Exploration:
				return new PowerBIPackagePart(storagePackage, PowerBIPackager.explorationPath, PowerBIPackager.IsExplorationOptional, CompressionOption.Normal);
			default:
				throw new ArgumentOutOfRangeException("powerBiPackageParts");
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004810 File Offset: 0x00002A10
		private static PowerBIPackageCompositePart GetPackageCompositePart(Package storagePackage, PowerBIPackager.PowerBIPackageParts powerBiPackageParts)
		{
			if (powerBiPackageParts <= PowerBIPackager.PowerBIPackageParts.StaticResources)
			{
				if (powerBiPackageParts == PowerBIPackager.PowerBIPackageParts.CustomVisuals)
				{
					return new PowerBIPackageCompositePart(storagePackage, PowerBIPackager.customVisualsPath, PowerBIPackager.IsCustomVisualsOptional, CompressionOption.Normal);
				}
				if (powerBiPackageParts == PowerBIPackager.PowerBIPackageParts.StaticResources)
				{
					return new PowerBIPackageCompositePart(storagePackage, PowerBIPackager.staticResourcesPath, PowerBIPackager.IsStaticResourcesOptional, CompressionOption.Normal);
				}
			}
			else
			{
				if (powerBiPackageParts == PowerBIPackager.PowerBIPackageParts.DaxQueryView)
				{
					return new PowerBIPackageCompositePart(storagePackage, PowerBIPackager.daxQueryViewPath, PowerBIPackager.IsDaxQueryViewOptional, CompressionOption.Normal);
				}
				if (powerBiPackageParts == PowerBIPackager.PowerBIPackageParts.Exploration)
				{
					return new PowerBIPackageCompositePart(storagePackage, PowerBIPackager.explorationPath, PowerBIPackager.IsExplorationOptional, CompressionOption.Normal);
				}
			}
			throw new ArgumentOutOfRangeException("powerBiPackageParts");
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000488C File Offset: 0x00002A8C
		private static void Validate(Package storagePackage, out Version pbixFileVersion)
		{
			if (PowerBIPackager.IsPackageCorruptedByMalwareDetection(storagePackage))
			{
				FileFormatException ex = new FileFormatException();
				ex.Data["KnownCorruption"] = true;
				throw ex;
			}
			PowerBIPackagePart packagePart = PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.Version);
			packagePart.ValidateExistsIfRequired();
			PowerBIPackager.ValidateVersion(PowerBIPackagingUtils.GetContentAsString(packagePart.GetContent(), PowerBIPackager.IsVersionOptional, null), out pbixFileVersion);
			foreach (PowerBIPackagePart powerBIPackagePart in PowerBIPackager.GetAllParts(storagePackage))
			{
				powerBIPackagePart.ValidateExistsIfRequired();
			}
			BasePowerBIPackagePart packagePart2 = PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.DataModel);
			PowerBIPackagePart packagePart3 = PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.DataModelSchema);
			if (packagePart2.PartExists && packagePart3.PartExists)
			{
				throw new FileFormatException("Package cannot contain both a DataModel and DataModelSchema part.");
			}
			BasePowerBIPackagePart packagePart4 = PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.ReportDocument);
			PowerBIPackageCompositePart packageCompositePart = PowerBIPackager.GetPackageCompositePart(storagePackage, PowerBIPackager.PowerBIPackageParts.Exploration);
			if (packagePart4.PartExists && packageCompositePart.GetSubParts().Any<Uri>())
			{
				throw new FileFormatException("Package cannot contain both a Layout and Report/definition parts.");
			}
			PowerBIPackagePart packagePart5 = PowerBIPackager.GetPackagePart(storagePackage, PowerBIPackager.PowerBIPackageParts.CustomProperties);
			if (packagePart5.PartExists && packagePart5.GetContent().GetStream().Length > 2097152L)
			{
				throw new FileFormatException("Custom properties in package cannot be larger than 2 MB");
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000049AC File Offset: 0x00002BAC
		private static bool IsPackageCorruptedByMalwareDetection(Package package)
		{
			return PowerBIPackager.GetAllPartPaths().Any((Uri path) => package.PartExists(new Uri(((path != null) ? path.ToString() : null) + ".txt", UriKind.Relative)));
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000049DC File Offset: 0x00002BDC
		private static void RemoveUnusedCustomVisuals(HashSet<Uri> customVisualsUriToRemove, PowerBIPackageCompositePart customVisualsPart)
		{
			foreach (Uri uri in customVisualsUriToRemove)
			{
				customVisualsPart.DeletePart(uri);
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004A2C File Offset: 0x00002C2C
		private static void RemoveUnusedParts(Package storagePackage, IEnumerable<Uri> otherPartsToRemove)
		{
			if (storagePackage != null)
			{
				if (storagePackage.PartExists(PowerBIPackager.reportDocumentPath))
				{
					storagePackage.DeletePart(PowerBIPackager.reportDocumentPath);
				}
				if (storagePackage.PartExists(PowerBIPackager.reportExplorationPath))
				{
					storagePackage.DeletePart(PowerBIPackager.reportExplorationPath);
				}
				if (storagePackage.PartExists(PowerBIPackager.reportPagesPath))
				{
					storagePackage.DeletePart(PowerBIPackager.reportPagesPath);
				}
				foreach (Uri uri in otherPartsToRemove)
				{
					if (storagePackage.PartExists(uri))
					{
						storagePackage.DeletePart(uri);
					}
				}
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004AC8 File Offset: 0x00002CC8
		private static IStreamablePowerBIPackagePartContent GetReportDocumentContent(Package storagePackage)
		{
			if (storagePackage != null)
			{
				if (storagePackage.PartExists(PowerBIPackager.reportDocumentPath))
				{
					return new StreamablePowerBIPackagePartContent(() => storagePackage.GetPart(PowerBIPackager.reportDocumentPath).GetStream(), "");
				}
				if (storagePackage.PartExists(PowerBIPackager.reportExplorationPath))
				{
					return new StreamablePowerBIPackagePartContent(() => storagePackage.GetPart(PowerBIPackager.reportExplorationPath).GetStream(), "");
				}
				if (storagePackage.PartExists(PowerBIPackager.reportLayoutPath))
				{
					return new StreamablePowerBIPackagePartContent(() => storagePackage.GetPart(PowerBIPackager.reportLayoutPath).GetStream(), "");
				}
			}
			return new StreamablePowerBIPackagePartContent(string.Empty, "");
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004B74 File Offset: 0x00002D74
		private static void ValidateVersion(string versionContents, out Version pbixFileVersion)
		{
			if (PowerBIPackager.ValidVersionsString.Contains(versionContents))
			{
				pbixFileVersion = new Version(versionContents);
				return;
			}
			Version version;
			try
			{
				version = new Version(versionContents);
			}
			catch (FormatException)
			{
				throw PowerBIPackager.CreateInvalidFileVersionException(versionContents);
			}
			catch (ArgumentException)
			{
				throw PowerBIPackager.CreateInvalidFileVersionException(versionContents);
			}
			if (version > PowerBIPackager.MaxSupportedVersion)
			{
				throw new NewerFileVersionException(versionContents);
			}
			throw PowerBIPackager.CreateInvalidFileVersionException(versionContents);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004BE8 File Offset: 0x00002DE8
		private static FileFormatException CreateInvalidFileVersionException(string version)
		{
			return new FileFormatException(string.Format(CultureInfo.InvariantCulture, "'{0}' is not a valid .pbix file version number.", version));
		}

		// Token: 0x04000061 RID: 97
		public const string KnownCorruptionKey = "KnownCorruption";

		// Token: 0x04000062 RID: 98
		private const int c_maxCustomPropertiesSizeInBytes = 2097152;

		// Token: 0x04000063 RID: 99
		private static readonly Uri connectionsPath = new Uri("/Connections", UriKind.Relative);

		// Token: 0x04000064 RID: 100
		private static readonly Uri dataModelSchemaPath = new Uri("/DataModelSchema", UriKind.Relative);

		// Token: 0x04000065 RID: 101
		private static readonly Uri diagramStatePath = new Uri("/DiagramState", UriKind.Relative);

		// Token: 0x04000066 RID: 102
		private static readonly Uri diagramLayoutPath = new Uri("/DiagramLayout", UriKind.Relative);

		// Token: 0x04000067 RID: 103
		private static readonly Uri mashupPath = new Uri("/DataMashup", UriKind.Relative);

		// Token: 0x04000068 RID: 104
		private static readonly Uri reportMetadataPath = new Uri("/Metadata", UriKind.Relative);

		// Token: 0x04000069 RID: 105
		private static readonly Uri reportSettingsPath = new Uri("/Settings", UriKind.Relative);

		// Token: 0x0400006A RID: 106
		private static readonly Uri versionPath = new Uri("/Version", UriKind.Relative);

		// Token: 0x0400006B RID: 107
		private static readonly Uri dataModelPath = new Uri("/DataModel", UriKind.Relative);

		// Token: 0x0400006C RID: 108
		private static readonly Uri customVisualsPath = new Uri("/Report/CustomVisuals", UriKind.Relative);

		// Token: 0x0400006D RID: 109
		private static readonly Uri reportLayoutPath = new Uri("/Report/Layout", UriKind.Relative);

		// Token: 0x0400006E RID: 110
		private static readonly Uri reportLinguisticSchemaPath = new Uri("/Report/LinguisticSchema", UriKind.Relative);

		// Token: 0x0400006F RID: 111
		private static readonly Uri staticResourcesPath = new Uri("/Report/StaticResources", UriKind.Relative);

		// Token: 0x04000070 RID: 112
		private static readonly Uri reportDocumentPath = new Uri("/Report/Document", UriKind.Relative);

		// Token: 0x04000071 RID: 113
		private static readonly Uri reportExplorationPath = new Uri("/Report/Exploration", UriKind.Relative);

		// Token: 0x04000072 RID: 114
		private static readonly Uri reportPagesPath = new Uri("/Report/Pages", UriKind.Relative);

		// Token: 0x04000073 RID: 115
		private static readonly Uri customPropertiesPath = new Uri("/docProps/custom.xml", UriKind.Relative);

		// Token: 0x04000074 RID: 116
		private static readonly Uri reportMobileStatePath = new Uri("/Report/MobileState", UriKind.Relative);

		// Token: 0x04000075 RID: 117
		private static readonly Uri unappliedChangesPath = new Uri("/UnappliedChanges", UriKind.Relative);

		// Token: 0x04000076 RID: 118
		private static readonly Uri daxQueryViewPath = new Uri("/DAXQueries", UriKind.Relative);

		// Token: 0x04000077 RID: 119
		private static readonly Uri explorationPath = new Uri("/Report/definition", UriKind.Relative);

		// Token: 0x04000078 RID: 120
		public static readonly bool IsConnectionsOptional = true;

		// Token: 0x04000079 RID: 121
		public static readonly bool IsCustomVisualsOptional = true;

		// Token: 0x0400007A RID: 122
		public static readonly bool IsDataModelSchemaOptional = true;

		// Token: 0x0400007B RID: 123
		public static readonly bool IsDiagramStateOptional = true;

		// Token: 0x0400007C RID: 124
		public static readonly bool IsDiagramLayoutOptional = true;

		// Token: 0x0400007D RID: 125
		public static readonly bool IsMashupOptional = true;

		// Token: 0x0400007E RID: 126
		public static readonly bool IsLinguisticSchemaOptional = true;

		// Token: 0x0400007F RID: 127
		public static readonly bool IsReportMetadataOptional = true;

		// Token: 0x04000080 RID: 128
		public static readonly bool IsReportSettingsOptional = true;

		// Token: 0x04000081 RID: 129
		public static readonly bool IsStaticResourcesOptional = true;

		// Token: 0x04000082 RID: 130
		public static readonly bool IsVersionOptional = false;

		// Token: 0x04000083 RID: 131
		public static readonly bool IsDataModelOptional = true;

		// Token: 0x04000084 RID: 132
		public static readonly bool IsReportDocumentOptional = true;

		// Token: 0x04000085 RID: 133
		public static readonly bool IsCustomPropertiesOptional = true;

		// Token: 0x04000086 RID: 134
		public static readonly bool IsReportMobileStateOptional = true;

		// Token: 0x04000087 RID: 135
		public static readonly bool IsUnappliedChangesOptional = true;

		// Token: 0x04000088 RID: 136
		public static readonly bool IsDaxQueryViewOptional = true;

		// Token: 0x04000089 RID: 137
		private static readonly bool IsExplorationOptional = true;

		// Token: 0x0400008A RID: 138
		public static readonly Version CurrentVersion = new Version(1, 28);

		// Token: 0x0400008B RID: 139
		public static readonly Version V3ModelsVersion = new Version(1, 19);

		// Token: 0x0400008C RID: 140
		public static readonly Version ExternalDatasetVersion = new Version(1, 29);

		// Token: 0x0400008D RID: 141
		public static readonly Version UnappliedChangesJsonVersion = new Version(1, 30);

		// Token: 0x0400008E RID: 142
		public static readonly Version DaxQueryViewVersion = new Version(1, 31);

		// Token: 0x0400008F RID: 143
		public static readonly Version EnhancedReportDocumentVersion = new Version(1, 32);

		// Token: 0x04000090 RID: 144
		private static readonly List<Version> ValidVersions = new List<Version>
		{
			new Version(0, 1),
			new Version(1, 0),
			new Version(1, 1),
			new Version(1, 2),
			new Version(1, 3),
			new Version(1, 4),
			new Version(1, 5),
			new Version(1, 6),
			new Version(1, 7),
			new Version(1, 8),
			new Version(1, 9),
			new Version(1, 10),
			new Version(1, 11),
			new Version(1, 12),
			new Version(1, 13),
			new Version(1, 14),
			new Version(1, 15),
			new Version(1, 16),
			new Version(1, 17),
			new Version(1, 18),
			new Version(1, 19),
			new Version(1, 20),
			new Version(1, 21),
			new Version(1, 22),
			new Version(1, 23),
			new Version(1, 24),
			new Version(1, 25),
			new Version(1, 26),
			new Version(1, 27),
			PowerBIPackager.CurrentVersion,
			PowerBIPackager.ExternalDatasetVersion,
			PowerBIPackager.UnappliedChangesJsonVersion,
			PowerBIPackager.DaxQueryViewVersion,
			PowerBIPackager.EnhancedReportDocumentVersion
		};

		// Token: 0x04000091 RID: 145
		private static readonly string[] ValidVersionsString = PowerBIPackager.ValidVersions.Select((Version v) => v.ToString()).ToArray<string>();

		// Token: 0x04000092 RID: 146
		public static readonly Version MaxSupportedVersion = PowerBIPackager.ValidVersions.Max<Version>();

		// Token: 0x020000BC RID: 188
		private enum PowerBIPackageParts
		{
			// Token: 0x040002F7 RID: 759
			Connections,
			// Token: 0x040002F8 RID: 760
			CustomVisuals,
			// Token: 0x040002F9 RID: 761
			DataModel,
			// Token: 0x040002FA RID: 762
			DataModelSchema,
			// Token: 0x040002FB RID: 763
			DiagramState,
			// Token: 0x040002FC RID: 764
			Mashup,
			// Token: 0x040002FD RID: 765
			UnappliedChanges,
			// Token: 0x040002FE RID: 766
			DiagramLayout,
			// Token: 0x040002FF RID: 767
			ReportDocument,
			// Token: 0x04000300 RID: 768
			ReportLinguisticSchema,
			// Token: 0x04000301 RID: 769
			ReportMetadata,
			// Token: 0x04000302 RID: 770
			ReportSettings,
			// Token: 0x04000303 RID: 771
			SecuirtyBindings,
			// Token: 0x04000304 RID: 772
			StaticResources,
			// Token: 0x04000305 RID: 773
			Version,
			// Token: 0x04000306 RID: 774
			CustomProperties,
			// Token: 0x04000307 RID: 775
			ReportMobileState,
			// Token: 0x04000308 RID: 776
			DaxQueryView,
			// Token: 0x04000309 RID: 777
			Exploration
		}
	}
}
