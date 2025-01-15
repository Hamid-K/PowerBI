using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000126 RID: 294
	internal sealed class DefinitionLoader
	{
		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002B8C0 File Offset: 0x00029AC0
		public DefinitionLoader(RSService service, bool useServiceConnectionForRepublishing)
		{
			RSTrace.CatalogTrace.Assert(service != null, "service");
			this.m_ownsConnection = !useServiceConnectionForRepublishing;
			this.m_service = service;
			this.m_connection = service.Storage.ConnectionManager;
			RSTrace.CatalogTrace.Assert(this.m_connection != null, "m_connection");
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002B934 File Offset: 0x00029B34
		private bool CheckRdce(ExternalItemPath linkPath, string properties, out ItemProperties itemProperties, out string rdce)
		{
			bool flag = false;
			itemProperties = null;
			rdce = null;
			if (Globals.Configuration.IsRdceEnabled)
			{
				if (!ItemPathBase.IsNullOrEmpty(linkPath))
				{
					LinkedReportProperyResolver linkedReportProperyResolver = new LinkedReportProperyResolver(linkPath, this.m_service);
					linkedReportProperyResolver.Resolve();
					rdce = linkedReportProperyResolver.Rdce;
				}
				else
				{
					itemProperties = new ItemProperties(properties);
					rdce = itemProperties.Rdce;
				}
				if (!string.IsNullOrEmpty(rdce))
				{
					string text;
					if (Globals.Configuration.Extensions.ReportDefinitionCustomization != null && !string.IsNullOrEmpty(Globals.Configuration.Extensions.ReportDefinitionCustomization.Name))
					{
						text = Globals.Configuration.Extensions.ReportDefinitionCustomization.Name;
					}
					else
					{
						text = string.Empty;
					}
					if (string.Compare(rdce, text, StringComparison.OrdinalIgnoreCase) != 0)
					{
						throw new RdceMismatchException(rdce, text);
					}
					flag = true;
					if (itemProperties == null)
					{
						itemProperties = new ItemProperties(properties);
					}
				}
			}
			return flag;
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002BA10 File Offset: 0x00029C10
		private ReportSnapshot CheckRdceAndGenerateSnapshotForSubreport(CatalogItemContext reportContext, bool isLinked, string properties, ParameterInfoCollection parentQueryParameters, CatalogItemContext parentContext)
		{
			ReportSnapshot reportSnapshot = null;
			ExternalItemPath externalItemPath = null;
			BaseReportCatalogItem baseReportCatalogItem = null;
			if (isLinked)
			{
				baseReportCatalogItem = (BaseReportCatalogItem)this.m_service.CatalogItemFactory.GetCatalogItem(reportContext);
				if (ItemType.LinkedReport == baseReportCatalogItem.ThisItemType)
				{
					LinkedReportCatalogItem linkedReportCatalogItem = (LinkedReportCatalogItem)baseReportCatalogItem;
					linkedReportCatalogItem.LoadLink();
					externalItemPath = this.m_service.CatalogToExternal(linkedReportCatalogItem.LinkPath);
				}
			}
			ItemProperties itemProperties;
			string text;
			if (this.CheckRdce(externalItemPath, properties, out itemProperties, out text))
			{
				if (baseReportCatalogItem == null)
				{
					baseReportCatalogItem = (BaseReportCatalogItem)this.m_service.CatalogItemFactory.GetCatalogItem(reportContext);
				}
				reportSnapshot = this.GenerateRdceSnapshot(baseReportCatalogItem, externalItemPath, itemProperties, parentQueryParameters, parentContext, text);
			}
			return reportSnapshot;
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002BAA4 File Offset: 0x00029CA4
		private ReportSnapshot CheckRdceAndGenerateSnapshot(BaseReportCatalogItem reportItem, ExternalItemPath linkPath, string properties, ParameterInfoCollection queryParameters)
		{
			ReportSnapshot reportSnapshot = null;
			ItemProperties itemProperties;
			string text;
			if (this.CheckRdce(linkPath, properties, out itemProperties, out text))
			{
				reportSnapshot = this.GenerateRdceSnapshot(reportItem, linkPath, itemProperties, queryParameters, null, text);
			}
			return reportSnapshot;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002BAD0 File Offset: 0x00029CD0
		private ReportSnapshot GenerateRdceSnapshot(BaseReportCatalogItem reportItem, ExternalItemPath linkPath, ItemProperties itemProperties, ParameterInfoCollection queryParameters, string rdceName)
		{
			return this.GenerateRdceSnapshot(reportItem, linkPath, itemProperties, queryParameters, null, rdceName);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0002BAE0 File Offset: 0x00029CE0
		private ReportSnapshot GenerateRdceSnapshot(BaseReportCatalogItem reportItem, ExternalItemPath linkPath, ItemProperties itemProperties, ParameterInfoCollection queryParameters, CatalogItemContext parentContext, string rdceName)
		{
			long num = 0L;
			RunningJobContext jobContext = Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext;
			Timer timer = null;
			if (jobContext != null)
			{
				timer = new Timer();
				timer.StartTimer();
			}
			ReportSnapshot reportSnapshot = null;
			ProfessionalReportCatalogItem profReportItem = null;
			if (reportItem.ThisItemType == ItemType.Report)
			{
				profReportItem = reportItem as ProfessionalReportCatalogItem;
			}
			else if (reportItem.ThisItemType == ItemType.LinkedReport)
			{
				CatalogItemContext catalogItemContext = new CatalogItemContext(this.m_service, linkPath, "Link");
				profReportItem = (ProfessionalReportCatalogItem)this.m_service.CatalogItemFactory.GetCatalogItem(catalogItemContext);
			}
			else
			{
				RSTrace.CatalogTrace.Assert(false, "GenerateRdceSnapshot: unexpected item type {0}", new object[] { reportItem.ThisItemType });
			}
			RSTrace.CatalogTrace.Assert(profReportItem != null, "GenerateRdceSnapshot: null != fullReportItem");
			profReportItem.LoadDefinition(true);
			Dictionary<string, IParameter> dictionary = null;
			if (queryParameters != null)
			{
				dictionary = new Dictionary<string, IParameter>(queryParameters.Count);
				foreach (object obj in queryParameters)
				{
					ParameterInfo parameterInfo = (ParameterInfo)obj;
					dictionary.Add(parameterInfo.Name, new DefinitionLoader.RdceParameter(parameterInfo));
				}
			}
			bool flag = ItemType.LinkedReport == reportItem.ThisItemType;
			bool flag2 = parentContext != null;
			DefinitionLoader.RdceReportContext rdceReportContext = new DefinitionLoader.RdceReportContext(reportItem.ItemContext.ItemName, reportItem.ItemContext.ItemPath.Value, flag, flag ? profReportItem.ItemContext.ItemName : null, flag ? profReportItem.ItemContext.ItemPath.Value : null, flag2, flag2 ? parentContext.ItemName : null, flag2 ? parentContext.ItemPath.Value : null, dictionary);
			RSTrace.CatalogTrace.Assert(this.m_service.UserContext.IsInitialized, "GenerateRdceSnapshot: m_service.UserContext.IsInitialized");
			DefinitionLoader.RdceUserContext rdceUserContext = new DefinitionLoader.RdceUserContext(this.m_service.UserContext);
			RSTrace.CatalogTrace.Assert(!string.IsNullOrEmpty(rdceName), "GenerateRdceSnapshot: !string.IsNullOrEmpty(rdceName)");
			IReportDefinitionCustomizationExtension rdce = ExtensionClassFactory.GetNewInstanceExtensionClass(rdceName, "ReportDefinitionCustomization") as IReportDefinitionCustomizationExtension;
			RSTrace.CatalogTrace.Assert(rdce != null, "GenerateRdceSnapshot: null != rdce");
			if (timer != null)
			{
				num = timer.ElapsedTimeMs();
				timer.StartTimer();
			}
			byte[] reportDefinitionProcessed = null;
			IEnumerable<RdceCustomizableElementId> customizedElementIds = null;
			bool processed = false;
			ExtensionBoundary.RdceBoundary.Invoke(delegate
			{
				processed = rdce.ProcessReportDefinition(profReportItem.Content, rdceReportContext, rdceUserContext, out reportDefinitionProcessed, out customizedElementIds);
			});
			if (processed && customizedElementIds == null)
			{
				customizedElementIds = new RdceCustomizableElementId[0];
			}
			if (timer != null)
			{
				timer.ElapsedTimeMs();
				timer.StartTimer();
			}
			if (processed)
			{
				byte[] array = this.MergeRdl(profReportItem.Content, reportDefinitionProcessed, customizedElementIds);
				Warning[] array2 = null;
				ParameterInfoCollection parameterInfoCollection;
				DataSourceInfoCollection dataSourceInfoCollection;
				DataSetInfoCollection dataSetInfoCollection;
				PageProperties pageProperties;
				byte[] array3;
				this.m_service.ConvertToIntermediate(array, false, itemProperties, reportItem.ItemContext, DateTime.Now, true, true, reportItem.DataSources, reportItem.SharedDataSets, ReportProcessingFlags.NotSet, false, false, out reportSnapshot, out parameterInfoCollection, out array2, out dataSourceInfoCollection, out dataSetInfoCollection, out pageProperties, out array3);
			}
			if (timer != null)
			{
				long num2 = timer.ElapsedTimeMs();
				jobContext.ExecutionInfo.AdditionalInfo.RdcePreparationTime = num.ToString(CultureInfo.InvariantCulture);
				jobContext.ExecutionInfo.AdditionalInfo.RdceInvocationTime = num.ToString(CultureInfo.InvariantCulture);
				jobContext.ExecutionInfo.AdditionalInfo.RdceSnapshotGenerationTime = num2.ToString(CultureInfo.InvariantCulture);
			}
			return reportSnapshot;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0002BE6C File Offset: 0x0002A06C
		private byte[] MergeRdl(byte[] rdlOriginal, byte[] rdlProcessed, IEnumerable<RdceCustomizableElementId> customizedElementIds)
		{
			RSTrace.CatalogTrace.Assert(rdlOriginal != null, "DefinitionLoader.MergeRdl(): null != rdlOriginal");
			RSTrace.CatalogTrace.Assert(customizedElementIds != null, "DefinitionLoader.MergeRdl(): null != customizedElementIds");
			if (rdlProcessed == null)
			{
				throw new RdceInvalidRdlException(null);
			}
			byte[] array = null;
			XmlDocument xmlDocument = new XmlDocument();
			using (MemoryStream memoryStream = new MemoryStream(rdlOriginal))
			{
				xmlDocument.Load(memoryStream);
				XmlNode documentElement = xmlDocument.DocumentElement;
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
				xmlNamespaceManager.AddNamespace("default", documentElement.NamespaceURI);
				XmlNode xmlNode = documentElement.SelectSingleNode(DefinitionLoader.m_xPathReport, xmlNamespaceManager);
				RSTrace.CatalogTrace.Assert(xmlNode != null, "null != nodeOriginalReport");
				XmlDocument xmlDocument2 = new XmlDocument();
				using (MemoryStream memoryStream2 = new MemoryStream(rdlProcessed))
				{
					try
					{
						xmlDocument2.Load(memoryStream2);
					}
					catch (XmlException ex)
					{
						throw new RdceInvalidRdlException(ex);
					}
					XmlNode documentElement2 = xmlDocument2.DocumentElement;
					if (!string.Equals(documentElement.NamespaceURI, documentElement2.NamespaceURI, StringComparison.OrdinalIgnoreCase))
					{
						throw new RdceMismatchRdlVersion(documentElement.NamespaceURI, documentElement2.NamespaceURI);
					}
					string[] array2 = documentElement.NamespaceURI.Split(new char[] { '/' });
					int num = 0;
					bool flag = array2.Length >= 6 && int.TryParse(array2[5], out num) && num >= 2010;
					if (customizedElementIds != null)
					{
						IEnumerator<RdceCustomizableElementId> elementEnumerator = null;
						try
						{
							ExtensionBoundary.RdceBoundary.Invoke(delegate
							{
								elementEnumerator = customizedElementIds.GetEnumerator();
								elementEnumerator.MoveNext();
							});
							if (elementEnumerator == null)
							{
								throw new RdceWrappedException(null, "RDCE customizedElementIds.GetEnumerator() returned null IEnumerator");
							}
							bool keepGoing = true;
							bool flag2 = false;
							ExtensionBoundary.Method <>9__3;
							while (keepGoing)
							{
								RdceCustomizableElementId id = RdceCustomizableElementId.Body;
								ExtensionBoundary.RdceBoundary.Invoke(delegate
								{
									id = elementEnumerator.Current;
								});
								if (!flag || id == RdceCustomizableElementId.DataSets || !flag2)
								{
									string text;
									if (flag && id != RdceCustomizableElementId.DataSets)
									{
										text = DefinitionLoader.m_xPathReportSections;
										flag2 = true;
									}
									else
									{
										text = DefinitionLoader.m_xPathCustomizableNode[(int)id];
									}
									XmlNodeList xmlNodeList = documentElement2.SelectNodes(text, xmlNamespaceManager);
									if (xmlNodeList != null && 1 < xmlNodeList.Count)
									{
										throw new RdceExtraElementException(xmlNodeList[0].Name);
									}
									XmlNode xmlNode2 = null;
									if (xmlNodeList != null && xmlNodeList.Count != 0)
									{
										xmlNode2 = xmlDocument.ImportNode(xmlNodeList[0], true);
									}
									XmlNode xmlNode3 = documentElement.SelectSingleNode(text, xmlNamespaceManager);
									if (xmlNode3 == null)
									{
										if (xmlNode2 != null)
										{
											xmlNode.AppendChild(xmlNode2);
										}
									}
									else if (xmlNode2 == null)
									{
										xmlNode.RemoveChild(xmlNode3);
									}
									else
									{
										xmlNode.ReplaceChild(xmlNode2, xmlNode3);
									}
								}
								ExtensionBoundary rdceBoundary = ExtensionBoundary.RdceBoundary;
								ExtensionBoundary.Method method;
								if ((method = <>9__3) == null)
								{
									method = (<>9__3 = delegate
									{
										keepGoing = elementEnumerator.MoveNext();
									});
								}
								rdceBoundary.Invoke(method);
							}
						}
						finally
						{
							if (elementEnumerator != null)
							{
								ExtensionBoundary.RdceBoundary.Invoke(delegate
								{
									elementEnumerator.Dispose();
								});
							}
						}
					}
				}
				using (MemoryStream memoryStream3 = new MemoryStream())
				{
					xmlDocument.Save(memoryStream3);
					memoryStream3.Seek(0L, SeekOrigin.Begin);
					array = new byte[memoryStream3.Length];
					memoryStream3.Read(array, 0, array.Length);
				}
			}
			return array;
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0002C270 File Offset: 0x0002A470
		public ReportCompiledDefinition GetCompiledDefinition(CatalogItemContext itemContext, SecurityRequirements secRequirements, SubreportRetrieval.ParentReportContext parentContext)
		{
			RSTrace.CatalogTrace.Assert(itemContext != null, "itemContext");
			RSTrace.CatalogTrace.Assert(secRequirements != null, "secRequirements");
			this.m_itemContext = itemContext;
			this.m_requirements = secRequirements;
			bool flag = this.CheckSecurityAndRepublish();
			ItemType itemType;
			ReportSnapshot reportSnapshot;
			Guid guid;
			string text;
			string text2;
			byte[] array;
			Guid guid2;
			int num;
			ReportSnapshot reportSnapshot2;
			string text3;
			string text4;
			Guid guid3;
			if (!new DBInterface(this.m_service.UserContext)
			{
				ConnectionManager = this.m_connection
			}.GetCompiledDefinition(this.ItemContext.CatalogItemPath, out itemType, out reportSnapshot, out guid, out text, out text2, out array, out guid2, out num, out reportSnapshot2, out text3, out text4, out guid3))
			{
				throw new ItemNotFoundException(this.OriginalItemPath.Value);
			}
			RSService.EnsureItemTypeIsReport(itemType, this.OriginalItemPath.Value);
			if (!flag)
			{
				secRequirements.CheckAccess(itemType, array, this.OriginalItemPath);
			}
			bool flag2 = itemType == ItemType.LinkedReport;
			if (flag2 && guid == Guid.Empty)
			{
				throw new InvalidReportLinkException();
			}
			ReportSnapshot reportSnapshot3 = null;
			if (parentContext != null)
			{
				reportSnapshot3 = this.CheckRdceAndGenerateSnapshotForSubreport(itemContext, flag2, text, parentContext.Parameters, parentContext.ItemContext);
			}
			ReportSnapshot reportSnapshot4;
			if (reportSnapshot3 != null)
			{
				RSTrace.CatalogTrace.Assert(Guid.Empty == guid3, "DefinitionLoader.GetCompiledDefinition(): Guid.Empty == executionSnapshotId");
				reportSnapshot4 = reportSnapshot3;
			}
			else
			{
				reportSnapshot4 = (flag2 ? reportSnapshot2 : reportSnapshot);
			}
			return new ReportCompiledDefinition(itemContext, reportSnapshot4, guid2, guid, itemType, guid3, flag2 ? text3 : text, flag2 ? text4 : text2, array, num, reportSnapshot3 != null);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0002C3CC File Offset: 0x0002A5CC
		public ReportExecutionDefinition GetExecutionDefinition(CatalogItemContext itemContext, ClientRequest session, ParameterInfoCollection queryParameters, SecurityRequirements requirements)
		{
			this.m_itemContext = itemContext;
			this.m_requirements = requirements;
			if (session.SessionReport.IsAdhocReport)
			{
				return new ReportExecutionDefinition(itemContext, session.SessionReport.Report.CompiledDefinition, Guid.Empty, Guid.Empty, session.SessionReport.Report.SnapshotData, ItemType.Report, itemContext.ReportDefinitionAsExternalItemPath, null, null, null, ExecutionOptions.Live, session.SessionReport.Report.SnapshotData != null, false, false, false, session.SessionReport.ExecutionDateTime, session.SessionReport.ExpirationDateTime)
				{
					PaginationData = new ReportPaginationData(session.SessionReport.PageCount, session.SessionReport.PaginationMode),
					DataSources = null,
					DataSets = null
				};
			}
			bool flag = this.CheckSecurityAndRepublish();
			DBInterface dbinterface = new DBInterface(this.m_service.UserContext);
			dbinterface.ConnectionManager = this.m_connection;
			string text = queryParameters.ToXml(true);
			bool flag2;
			ItemType itemType;
			ReportSnapshot reportSnapshot;
			ReportSnapshot reportSnapshot2;
			Guid guid;
			string text2;
			string text3;
			string text4;
			byte[] array;
			Guid guid2;
			int num;
			DateTime dateTime;
			bool flag3;
			bool flag4;
			DateTime dateTime2;
			if (!dbinterface.GetReportForExecution(this.ItemContext.CatalogItemPath, text, out flag2, out itemType, out reportSnapshot, out reportSnapshot2, out guid, out text2, out text3, out text4, out array, out guid2, out num, out dateTime, out flag3, out flag4, out dateTime2))
			{
				throw new ItemNotFoundException(this.OriginalItemPath.Value);
			}
			RSService.EnsureItemTypeIsReport(itemType, this.OriginalItemPath.Value);
			if (!flag)
			{
				requirements.CheckAccess(itemType, array, this.OriginalItemPath);
			}
			if (itemType == ItemType.LinkedReport)
			{
				if (guid == Guid.Empty)
				{
					throw new InvalidReportLinkException();
				}
				itemContext.SetReportDefinitionPath(this.m_service.CatalogToExternal(text2));
			}
			if (!flag2 && flag3 && reportSnapshot2 == null)
			{
				throw new ReportNotReadyException();
			}
			int num2 = 0;
			PaginationMode paginationMode = PaginationMode.TotalPages;
			bool flag5 = false;
			if (!itemContext.ItemPath.IsEditSession && flag3 && string.IsNullOrEmpty(itemContext.RSRequestParameters.SnapshotParamValue))
			{
				ReportProcessingFlags reportProcessingFlags;
				num2 = dbinterface.GetSnapshotPromotedInfo(reportSnapshot2, out flag5, out paginationMode, out reportProcessingFlags);
			}
			ReportPaginationData reportPaginationData = new ReportPaginationData(num2, paginationMode);
			BaseReportCatalogItem baseReportCatalogItem = this.m_service.CatalogItemFactory.GetCatalogItem(itemContext, guid2, itemType, array, guid) as BaseReportCatalogItem;
			ReportSnapshot reportSnapshot3 = this.CheckRdceAndGenerateSnapshot(baseReportCatalogItem, this.m_service.CatalogToExternal(text2), text3, queryParameters);
			if (reportSnapshot3 != null)
			{
				RSTrace.CatalogTrace.Assert(reportSnapshot2 == null, "DefinitionLoader.GetExecutionDefinition(): null == snapshotData");
				RSTrace.CatalogTrace.Assert(!flag3, "DefinitionLoader.GetExecutionDefinition(): !hasData");
				RSTrace.CatalogTrace.Assert(!flag2, "DefinitionLoader.GetExecutionDefinition(): !foundInCache");
				RSTrace.CatalogTrace.Assert(!flag4, "DefinitionLoader.GetExecutionDefinition(): !cachingRequested");
			}
			return new ReportExecutionDefinition(itemContext, (reportSnapshot3 == null) ? reportSnapshot : reportSnapshot3, guid2, guid, reportSnapshot2, itemType, itemContext.ReportDefinitionAsExternalItemPath, text3, text4, array, num, flag3, flag2, flag4, reportSnapshot3 != null, dateTime, dateTime2)
			{
				PaginationData = reportPaginationData,
				DataSources = baseReportCatalogItem.DataSources,
				DataSets = baseReportCatalogItem.SharedDataSets
			};
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0002C68C File Offset: 0x0002A88C
		public ReportExecutionDefinition GetHistorySnapshot(CatalogItemContext itemContext, ClientRequest session, SecurityRequirements requirements)
		{
			IDBInterface storage = this.m_service.Storage;
			DateTime dateTime = Globals.ParseSnapshotDateParameter(itemContext.RSRequestParameters.SnapshotParamValue, false);
			ItemType itemType;
			Guid guid;
			int num;
			byte[] array;
			int num2;
			Guid guid2;
			Guid guid3;
			if (!storage.ObjectExists(itemContext.ItemPath, out itemType, out guid, out num, out array, out num2, out guid2, out guid3))
			{
				throw new ItemNotFoundException(itemContext.OriginalItemPath.Value);
			}
			RSService.EnsureItemTypeIsReport(itemType, itemContext.OriginalItemPath.Value);
			requirements.CheckAccess(itemType, array, itemContext.OriginalItemPath);
			ReportSnapshot reportSnapshot;
			string text;
			string text2;
			if (!storage.GetSnapshotFromHistory(itemContext.CatalogItemPath, dateTime, out guid, out itemType, out reportSnapshot, out text, out text2, out array))
			{
				throw new ReportHistoryNotFoundException(itemContext.OriginalItemPath.Value, itemContext.RSRequestParameters.SnapshotParamValue);
			}
			if (itemType == ItemType.LinkedReport)
			{
				if (guid3 == Guid.Empty)
				{
					throw new InvalidReportLinkException();
				}
				CatalogItemPath pathById = storage.GetPathById(guid3);
				itemContext.SetReportDefinitionPath(this.m_service.CatalogToExternal(pathById));
			}
			PaginationMode paginationMode = PaginationMode.TotalPages;
			bool flag = false;
			ReportProcessingFlags reportProcessingFlags;
			ReportPaginationData reportPaginationData = new ReportPaginationData(storage.GetSnapshotPromotedInfo(reportSnapshot, out flag, out paginationMode, out reportProcessingFlags), paginationMode);
			return new ReportExecutionDefinition(itemContext, null, guid, guid3, reportSnapshot, itemType, itemContext.ReportDefinitionAsExternalItemPath, text2, text, array, num2, true, false, false, false, dateTime, DateTime.MaxValue)
			{
				PaginationData = reportPaginationData
			};
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0002C7BC File Offset: 0x0002A9BC
		public ItemParameterDefinition GetParameterDefinition(CatalogItemContext itemContext, string historyId, bool forRendering, SecurityRequirements requirements)
		{
			this.m_itemContext = itemContext;
			this.m_requirements = requirements;
			bool flag = this.CheckSecurityAndRepublish();
			DBInterface storageInternal = this.m_service.ServiceHelper.GetStorageInternal();
			storageInternal.ConnectionManager = this.m_connection;
			DateTime dateTime = Globals.ParseSnapshotDateParameter(historyId, true);
			Guid guid;
			ItemType itemType;
			int num;
			byte[] array;
			string text;
			ReportSnapshot reportSnapshot;
			ReportSnapshot reportSnapshot2;
			Guid guid2;
			DateTime dateTime2;
			if (!storageInternal.GetReportParametersForExecution(this.ItemContext.CatalogItemPath, dateTime, out guid, out itemType, out num, out array, out text, out reportSnapshot, out reportSnapshot2, out guid2, out dateTime2))
			{
				throw new ItemNotFoundException(this.OriginalItemPath.Value);
			}
			RSService.EnsureItemTypeIsReportOrDataSet(itemType, this.OriginalItemPath.Value);
			if (!flag)
			{
				requirements.CheckAccess(itemType, array, this.OriginalItemPath);
			}
			if (itemType == ItemType.LinkedReport && guid2 == Guid.Empty && (!forRendering || dateTime == DateTime.MinValue))
			{
				throw new InvalidReportLinkException();
			}
			if (historyId != null && reportSnapshot2.SnapshotDataID == Guid.Empty)
			{
				throw new ReportHistoryNotFoundException(this.OriginalItemPath.Value, historyId);
			}
			return new ItemParameterDefinition(itemContext, reportSnapshot, guid, guid2, itemType, reportSnapshot2, string.Empty, string.Empty, array, num, dateTime2, text);
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x0002C8CC File Offset: 0x0002AACC
		// (set) Token: 0x06000BD0 RID: 3024 RVA: 0x0002C8D4 File Offset: 0x0002AAD4
		public ReportProcessing.NeedsUpgrade UpgradeCheckCallback
		{
			get
			{
				return this.m_needsUpgradeCallback;
			}
			set
			{
				this.m_needsUpgradeCallback = value;
			}
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0002C8E0 File Offset: 0x0002AAE0
		private bool CheckSecurityAndRepublish()
		{
			RSTrace.CatalogTrace.Assert(this.ItemPath != null && this.ItemPath.Value != null, "ItemPath");
			RSTrace.CatalogTrace.Assert(this.OriginalItemPath != null, "OriginalItemPath");
			if (this.ItemPath.IsEditSession)
			{
				return false;
			}
			if (RepublishingCache.HasPathBeenChecked(this.ItemContext.CatalogItemPath) && !DefinitionLoader.m_forceRepublishing)
			{
				return false;
			}
			ConnectionManager connectionManager;
			bool republishCheckConnection = this.GetRepublishCheckConnection(out connectionManager);
			if (republishCheckConnection)
			{
				RSTrace.CatalogTrace.Assert(connectionManager != this.m_connection, "connection != m_connection");
				connectionManager.ConnectionTransactionType = ConnectionTransactionType.Explicit;
				connectionManager.WillDisconnectStorage();
			}
			else
			{
				RSTrace.CatalogTrace.Assert(connectionManager == this.m_connection, "connection == m_connection");
			}
			bool flag;
			try
			{
				DefinitionDbInterface definitionDbInterface = new DefinitionDbInterface();
				definitionDbInterface.ConnectionManager = connectionManager;
				ReportSnapshot reportSnapshot;
				byte[] array;
				if (!definitionDbInterface.LoadForDefinitionCheck(this.ItemContext.CatalogItemPath, false, out reportSnapshot, out array))
				{
					flag = false;
				}
				else
				{
					RSTrace.CatalogTrace.Assert(reportSnapshot != null, "compiledDefinition");
					RSTrace.CatalogTrace.Assert(reportSnapshot.SnapshotDataID != Guid.Empty, "compiledDefinition.SnapshotDataId");
					this.m_requirements.CheckAccess(ItemType.Report, array, this.ItemPath);
					if (!this.UpgradeCheckCallback(reportSnapshot.ProcessingFlags) && !DefinitionLoader.m_forceRepublishing)
					{
						if (reportSnapshot.ProcessingFlags != ReportProcessingFlags.NotSet)
						{
							RepublishingCache.MarkPathAsChecked(this.ItemContext.CatalogItemPath);
						}
						flag = true;
					}
					else
					{
						this.Republish(definitionDbInterface, reportSnapshot);
						if (republishCheckConnection)
						{
							connectionManager.CommitTransaction();
							RepublishingCache.MarkPathAsChecked(this.ItemContext.CatalogItemPath);
						}
						flag = true;
					}
				}
			}
			finally
			{
				if (republishCheckConnection)
				{
					connectionManager.DisconnectStorage();
				}
			}
			return flag;
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0002CA94 File Offset: 0x0002AC94
		private void Republish(DefinitionDbInterface storage, ReportSnapshot originalCompiledDefinition)
		{
			if (RSTrace.CatalogTrace.TraceInfo)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Attempting republishing of item '{0}'.", new object[] { this.ItemPath.Value });
			}
			byte[] array;
			ReportSnapshot reportSnapshot;
			if (!storage.LoadForRepublishing(this.ItemContext.CatalogItemPath, out array, out reportSnapshot) || !DefinitionLoader.IsSameSnapshot(originalCompiledDefinition, reportSnapshot))
			{
				return;
			}
			CatalogItemContext catalogItemContext = this.AdjustContextForLinkedReports(storage.ConnectionManager);
			try
			{
				ReportSnapshot reportSnapshot2;
				ParameterInfoCollection parameterInfoCollection;
				Warning[] array2;
				DataSourceInfoCollection dataSourceInfoCollection;
				DataSetInfoCollection dataSetInfoCollection;
				PageProperties pageProperties;
				byte[] array3;
				this.m_service.ConvertToIntermediate(array, true, null, catalogItemContext, DateTime.Now, false, originalCompiledDefinition.ProcessingFlags, true, false, out reportSnapshot2, out parameterInfoCollection, out array2, out dataSourceInfoCollection, out dataSetInfoCollection, out pageProperties, out array3);
				Guid guid = storage.UpdateCompiledDefinition(catalogItemContext.CatalogItemPath, originalCompiledDefinition.SnapshotDataID, reportSnapshot2.SnapshotDataID);
				if (guid != Guid.Empty)
				{
					this.UpdateDataSourceIds(storage, guid, dataSourceInfoCollection);
					this.UpdateDataSetIds(storage, guid, dataSetInfoCollection);
				}
			}
			catch (ReportPublishingException ex)
			{
				this.TraceRepublishingFailure(ex);
				new InternalRepublishingException(this.ItemContext.OriginalItemPath.Value, ex, array);
				new ChunkStorage
				{
					ConnectionManager = storage.ConnectionManager
				}.SetSnapshotProcessingFlags(originalCompiledDefinition.SnapshotDataID, true, ex.ReportProcessingFlags);
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0002CBC4 File Offset: 0x0002ADC4
		private CatalogItemContext AdjustContextForLinkedReports(ConnectionManager connectionManager)
		{
			RSTrace.CatalogTrace.Assert(connectionManager != null, "connectionManager");
			DBInterface dbinterface = new DBInterface(this.m_service.UserContext);
			dbinterface.ConnectionManager = connectionManager;
			RSTrace.CatalogTrace.Assert(!this.ItemPath.IsEditSession, "!ItemPath.IsEditSession");
			ItemType itemType;
			Guid guid;
			int num;
			byte[] array;
			int num2;
			Guid guid2;
			Guid guid3;
			if (!dbinterface.CatalogObjectExists(this.ItemContext.CatalogItemPath, out itemType, out guid, out num, out array, out num2, out guid2, out guid3))
			{
				throw new ItemNotFoundException(this.ItemContext.OriginalItemPath.Value);
			}
			CatalogItemContext catalogItemContext;
			if (itemType == ItemType.LinkedReport)
			{
				if (guid3 == Guid.Empty)
				{
					throw new InvalidReportLinkException();
				}
				CatalogItemPath pathById = dbinterface.GetPathById(guid3);
				if (ItemPathBase.IsNullOrEmpty(pathById))
				{
					throw new InvalidReportLinkException();
				}
				catalogItemContext = new CatalogItemContext(this.m_service, pathById, "RepublishingPath");
			}
			else
			{
				catalogItemContext = this.ItemContext;
			}
			return catalogItemContext;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0002CCA0 File Offset: 0x0002AEA0
		private void TraceRepublishingFailure(ReportPublishingException pubException)
		{
			RSTrace.CatalogTrace.Assert(pubException != null);
			if (RSTrace.CatalogTrace.TraceWarning)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "Report republishing failed for item: {0}", this.ItemPath.Value));
				foreach (object obj in pubException.ProcessingMessages)
				{
					ProcessingMessage processingMessage = (ProcessingMessage)obj;
					string text = string.Format(CultureInfo.InvariantCulture, "{0} ({1}.{2}) : {3} [{4}]", new object[]
					{
						(processingMessage.Severity == Severity.Warning) ? "Warning" : "Error",
						processingMessage.ObjectName,
						processingMessage.PropertyName,
						processingMessage.Message,
						processingMessage.Code
					});
					stringBuilder.AppendLine(text);
				}
				RSTrace.CatalogTrace.Trace(TraceLevel.Warning, stringBuilder.ToString());
			}
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0002CDAC File Offset: 0x0002AFAC
		private void UpdateDataSourceIds(DefinitionDbInterface storage, Guid reportId, DataSourceInfoCollection dataSources)
		{
			if (dataSources == null)
			{
				return;
			}
			foreach (object obj in dataSources)
			{
				DataSourceInfo dataSourceInfo = (DataSourceInfo)obj;
				storage.RebindDataSource(reportId, dataSourceInfo.OriginalName, dataSourceInfo.ID);
			}
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0002CE10 File Offset: 0x0002B010
		private void UpdateDataSetIds(DefinitionDbInterface storage, Guid reportId, DataSetInfoCollection dataSets)
		{
			if (dataSets == null)
			{
				return;
			}
			foreach (DataSetInfo dataSetInfo in dataSets)
			{
				storage.RebindDataSet(reportId, dataSetInfo.DataSetName, dataSetInfo.ID);
			}
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0002CE68 File Offset: 0x0002B068
		private bool GetRepublishCheckConnection(out ConnectionManager connection)
		{
			if (!this.m_ownsConnection || this.m_connection.IsBatchScoped)
			{
				connection = this.m_connection;
				return false;
			}
			connection = new ConnectionManager();
			return true;
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0002CE91 File Offset: 0x0002B091
		private ExternalItemPath ItemPath
		{
			get
			{
				return this.m_itemContext.ItemPath;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0002CE9E File Offset: 0x0002B09E
		private ExternalItemPath OriginalItemPath
		{
			get
			{
				return this.m_itemContext.OriginalItemPath;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x0002CEAB File Offset: 0x0002B0AB
		private CatalogItemContext ItemContext
		{
			get
			{
				return this.m_itemContext;
			}
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0002CEB3 File Offset: 0x0002B0B3
		private static bool DefaultNeedsUpgradeCallback(ReportProcessingFlags processingFlags)
		{
			return processingFlags == ReportProcessingFlags.NotSet;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0002CEB9 File Offset: 0x0002B0B9
		private static bool IsSameSnapshot(ReportSnapshot snapA, ReportSnapshot snapB)
		{
			if (snapA == null)
			{
				return snapB == null;
			}
			if (snapB == null)
			{
				return snapA == null;
			}
			return snapA.ProcessingFlags == snapB.ProcessingFlags && snapA.SnapshotDataID == snapB.SnapshotDataID;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0002CEEC File Offset: 0x0002B0EC
		[Conditional("DEBUG")]
		public static void ForceThisThreadRepublish()
		{
			DefinitionLoader.m_forceRepublishing = true;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0002CEF4 File Offset: 0x0002B0F4
		[Conditional("DEBUG")]
		public static void RestoreFromForcedRepublish()
		{
			DefinitionLoader.m_forceRepublishing = false;
		}

		// Token: 0x040004C1 RID: 1217
		private const string m_defaultNamespacePrefix = "default";

		// Token: 0x040004C2 RID: 1218
		private static readonly string m_xPathReport = string.Format(CultureInfo.InvariantCulture, "/{0}:Report", "default");

		// Token: 0x040004C3 RID: 1219
		private static readonly string m_xPathReportSections = string.Format(CultureInfo.InvariantCulture, "{0}/{1}:ReportSections", DefinitionLoader.m_xPathReport, "default");

		// Token: 0x040004C4 RID: 1220
		private static readonly string[] m_xPathCustomizableNode = new string[]
		{
			string.Format(CultureInfo.InvariantCulture, "{0}/{1}:Body", DefinitionLoader.m_xPathReport, "default"),
			string.Format(CultureInfo.InvariantCulture, "{0}/{1}:PageHeader", DefinitionLoader.m_xPathReport, "default"),
			string.Format(CultureInfo.InvariantCulture, "{0}/{1}:PageFooter", DefinitionLoader.m_xPathReport, "default"),
			string.Format(CultureInfo.InvariantCulture, "{0}/{1}:Page", DefinitionLoader.m_xPathReport, "default"),
			string.Format(CultureInfo.InvariantCulture, "{0}/{1}:DataSets", DefinitionLoader.m_xPathReport, "default")
		};

		// Token: 0x040004C5 RID: 1221
		private readonly bool m_ownsConnection;

		// Token: 0x040004C6 RID: 1222
		private readonly ConnectionManager m_connection;

		// Token: 0x040004C7 RID: 1223
		private readonly RSService m_service;

		// Token: 0x040004C8 RID: 1224
		private ReportProcessing.NeedsUpgrade m_needsUpgradeCallback = new ReportProcessing.NeedsUpgrade(DefinitionLoader.DefaultNeedsUpgradeCallback);

		// Token: 0x040004C9 RID: 1225
		private CatalogItemContext m_itemContext;

		// Token: 0x040004CA RID: 1226
		private SecurityRequirements m_requirements;

		// Token: 0x040004CB RID: 1227
		[ThreadStatic]
		private static bool m_forceRepublishing;

		// Token: 0x0200046B RID: 1131
		private class RdceReportContext : IReportContext
		{
			// Token: 0x06002373 RID: 9075 RVA: 0x00084B60 File Offset: 0x00082D60
			internal RdceReportContext(string reportName, string reportPath, bool isLinkedReport, string linkedReportTargetName, string linkedReportTargetPath, bool isSubreport, string parentReportName, string parentReportPath, IDictionary<string, IParameter> queryParameters)
			{
				this.m_reportName = reportName;
				this.m_reportPath = reportPath;
				this.m_isLinkedReport = isLinkedReport;
				this.m_linkedReportTargetName = linkedReportTargetName;
				this.m_linkedReportTargetPath = linkedReportTargetPath;
				this.m_isSubreport = isSubreport;
				this.m_parentReportName = parentReportName;
				this.m_parentReportPath = parentReportPath;
				this.m_queryParameters = queryParameters;
			}

			// Token: 0x17000A6E RID: 2670
			// (get) Token: 0x06002374 RID: 9076 RVA: 0x00084BB8 File Offset: 0x00082DB8
			public string ReportName
			{
				get
				{
					return this.m_reportName;
				}
			}

			// Token: 0x17000A6F RID: 2671
			// (get) Token: 0x06002375 RID: 9077 RVA: 0x00084BC0 File Offset: 0x00082DC0
			public string ReportPath
			{
				get
				{
					return this.m_reportPath;
				}
			}

			// Token: 0x17000A70 RID: 2672
			// (get) Token: 0x06002376 RID: 9078 RVA: 0x00084BC8 File Offset: 0x00082DC8
			public bool IsLinkedReport
			{
				get
				{
					return this.m_isLinkedReport;
				}
			}

			// Token: 0x17000A71 RID: 2673
			// (get) Token: 0x06002377 RID: 9079 RVA: 0x00084BD0 File Offset: 0x00082DD0
			public string LinkedReportTargetName
			{
				get
				{
					return this.m_linkedReportTargetName;
				}
			}

			// Token: 0x17000A72 RID: 2674
			// (get) Token: 0x06002378 RID: 9080 RVA: 0x00084BD8 File Offset: 0x00082DD8
			public string LinkedReportTargetPath
			{
				get
				{
					return this.m_linkedReportTargetPath;
				}
			}

			// Token: 0x17000A73 RID: 2675
			// (get) Token: 0x06002379 RID: 9081 RVA: 0x00084BE0 File Offset: 0x00082DE0
			public bool IsSubreport
			{
				get
				{
					return this.m_isSubreport;
				}
			}

			// Token: 0x17000A74 RID: 2676
			// (get) Token: 0x0600237A RID: 9082 RVA: 0x00084BE8 File Offset: 0x00082DE8
			public string ParentReportName
			{
				get
				{
					return this.m_parentReportName;
				}
			}

			// Token: 0x17000A75 RID: 2677
			// (get) Token: 0x0600237B RID: 9083 RVA: 0x00084BF0 File Offset: 0x00082DF0
			public string ParentReportPath
			{
				get
				{
					return this.m_parentReportPath;
				}
			}

			// Token: 0x17000A76 RID: 2678
			// (get) Token: 0x0600237C RID: 9084 RVA: 0x00084BF8 File Offset: 0x00082DF8
			public IDictionary<string, IParameter> QueryParameters
			{
				get
				{
					return this.m_queryParameters;
				}
			}

			// Token: 0x04000FC6 RID: 4038
			private string m_reportName;

			// Token: 0x04000FC7 RID: 4039
			private string m_reportPath;

			// Token: 0x04000FC8 RID: 4040
			private bool m_isLinkedReport;

			// Token: 0x04000FC9 RID: 4041
			private string m_linkedReportTargetName;

			// Token: 0x04000FCA RID: 4042
			private string m_linkedReportTargetPath;

			// Token: 0x04000FCB RID: 4043
			private bool m_isSubreport;

			// Token: 0x04000FCC RID: 4044
			private string m_parentReportName;

			// Token: 0x04000FCD RID: 4045
			private string m_parentReportPath;

			// Token: 0x04000FCE RID: 4046
			private IDictionary<string, IParameter> m_queryParameters;
		}

		// Token: 0x0200046C RID: 1132
		private class RdceParameter : IParameter
		{
			// Token: 0x17000A77 RID: 2679
			// (get) Token: 0x0600237D RID: 9085 RVA: 0x00084C00 File Offset: 0x00082E00
			public string Name
			{
				get
				{
					return this.m_name;
				}
			}

			// Token: 0x17000A78 RID: 2680
			// (get) Token: 0x0600237E RID: 9086 RVA: 0x00084C08 File Offset: 0x00082E08
			public bool IsMultiValue
			{
				get
				{
					return this.m_isMultiValue;
				}
			}

			// Token: 0x17000A79 RID: 2681
			// (get) Token: 0x0600237F RID: 9087 RVA: 0x00084C10 File Offset: 0x00082E10
			public object[] Values
			{
				get
				{
					return this.m_values;
				}
			}

			// Token: 0x06002380 RID: 9088 RVA: 0x00084C18 File Offset: 0x00082E18
			internal RdceParameter(ParameterInfo p)
			{
				this.m_name = p.Name;
				this.m_isMultiValue = p.MultiValue;
				this.m_values = p.Values;
			}

			// Token: 0x04000FCF RID: 4047
			private string m_name;

			// Token: 0x04000FD0 RID: 4048
			private bool m_isMultiValue;

			// Token: 0x04000FD1 RID: 4049
			private object[] m_values;
		}

		// Token: 0x0200046D RID: 1133
		private class RdceUserContext : IUserContext
		{
			// Token: 0x06002381 RID: 9089 RVA: 0x00084C44 File Offset: 0x00082E44
			internal RdceUserContext(UserContext userContext)
			{
				this.m_userName = userContext.UserName;
				this.m_token = userContext.UserToken;
				this.m_authenticationType = userContext.AuthenticationType;
			}

			// Token: 0x17000A7A RID: 2682
			// (get) Token: 0x06002382 RID: 9090 RVA: 0x00084C70 File Offset: 0x00082E70
			public string UserName
			{
				get
				{
					return this.m_userName;
				}
			}

			// Token: 0x17000A7B RID: 2683
			// (get) Token: 0x06002383 RID: 9091 RVA: 0x00084C78 File Offset: 0x00082E78
			public object Token
			{
				get
				{
					return this.m_token;
				}
			}

			// Token: 0x17000A7C RID: 2684
			// (get) Token: 0x06002384 RID: 9092 RVA: 0x00084C80 File Offset: 0x00082E80
			public AuthenticationType AuthenticationType
			{
				get
				{
					return this.m_authenticationType;
				}
			}

			// Token: 0x04000FD2 RID: 4050
			private string m_userName;

			// Token: 0x04000FD3 RID: 4051
			private object m_token;

			// Token: 0x04000FD4 RID: 4052
			private AuthenticationType m_authenticationType;
		}
	}
}
