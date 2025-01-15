using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportPublishing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000771 RID: 1905
	internal sealed class ReportPublishing
	{
		// Token: 0x0600696C RID: 26988 RVA: 0x00199ED0 File Offset: 0x001980D0
		internal Report CreateIntermediateFormat(ICatalogItemContext reportContext, byte[] definition, ReportProcessing.CreateReportChunk createChunkCallback, ReportProcessing.CheckSharedDataSource checkDataSourceCallback, ReportProcessing.ResolveTemporaryDataSource resolveTemporaryDataSourceCallback, DataSourceInfoCollection originalDataSources, PublishingErrorContext errorContext, AppDomain compilationTempAppDomain, bool generateExpressionHostWithRefusedPermissions, IDataProtection dataProtection, out string description, out string language, out ParameterInfoCollection parameters, out DataSourceInfoCollection dataSources, out UserLocationFlags userReferenceLocation, out ArrayList dataSetsName, out bool hasExternalImages, out bool hasHyperlinks)
		{
			Report report;
			try
			{
				this.m_report = null;
				this.m_errorContext = errorContext;
				if (definition == null)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsNotAReportDefinition, Severity.Error, ObjectType.Report, null, null, Array.Empty<string>());
					throw new ReportProcessingException(this.m_errorContext.Messages);
				}
				this.Phase1(reportContext, definition, createChunkCallback, checkDataSourceCallback, resolveTemporaryDataSourceCallback, originalDataSources, dataProtection, out description, out language, out dataSources, out userReferenceLocation, out hasExternalImages, out hasHyperlinks);
				this.Phase2();
				this.Phase3(reportContext, out parameters, compilationTempAppDomain, generateExpressionHostWithRefusedPermissions);
				this.Phase4();
				if (this.m_errorContext.HasError)
				{
					throw new ReportProcessingException(this.m_errorContext.Messages);
				}
				ReportPublishing.CalculateChildrenPostions(this.m_report);
				ReportPublishing.CalculateChildrenDependencies(this.m_report);
				dataSetsName = null;
				for (int i = 0; i < this.m_dataSets.Count; i++)
				{
					if (!this.m_dataSets[i].UsedOnlyInParameters)
					{
						if (dataSetsName == null)
						{
							dataSetsName = new ArrayList();
						}
						dataSetsName.Add(this.m_dataSets[i].Name);
					}
				}
				report = this.m_report;
			}
			finally
			{
				this.m_report = null;
				this.m_errorContext = null;
			}
			return report;
		}

		// Token: 0x0600696D RID: 26989 RVA: 0x0019A010 File Offset: 0x00198210
		private int GenerateID()
		{
			int num = this.m_idCounter + 1;
			this.m_idCounter = num;
			return num;
		}

		// Token: 0x0600696E RID: 26990 RVA: 0x0019A030 File Offset: 0x00198230
		private void Phase1(ICatalogItemContext reportContext, byte[] definition, ReportProcessing.CreateReportChunk createChunkCallback, ReportProcessing.CheckSharedDataSource checkDataSourceCallback, ReportProcessing.ResolveTemporaryDataSource resolveTemporaryDataSourceCallback, DataSourceInfoCollection originalDataSources, IDataProtection dataProtection, out string description, out string language, out DataSourceInfoCollection dataSources, out UserLocationFlags userReferenceLocation, out bool hasExternalImages, out bool hasHyperlinks)
		{
			try
			{
				XmlTextReader xmlTextReader = XmlUtil.SafeCreateXmlTextReader(definition);
				xmlTextReader = RDLUpgrader.UpgradeTo2005(xmlTextReader) as XmlTextReader;
				this.m_reader = ReportPublishing.RmlValidatingReader.CreateReader(xmlTextReader, this.m_errorContext, "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
				this.m_reportItemNames = new CLSUniqueNameValidator(ProcessingErrorCode.rsInvalidNameNotCLSCompliant, ProcessingErrorCode.rsDuplicateReportItemName);
				this.m_scopeNames = new ScopeNameValidator();
				this.m_imageStreamNames = new ImageStreamNames();
				this.m_reportContext = reportContext;
				this.m_createChunkCallback = createChunkCallback;
				this.m_checkDataSourceCallback = checkDataSourceCallback;
				this.m_dataSources = new DataSourceInfoCollection();
				this.m_subReports = new SubReportList();
				while (this.m_reader.Read())
				{
					if (XmlNodeType.Element == this.m_reader.NodeType && "Report" == this.m_reader.LocalName)
					{
						this.m_reportCT = new ReportCompileTime(new VBExpressionParser(this.m_errorContext), this.m_errorContext);
						this.m_report = this.ReadReport(resolveTemporaryDataSourceCallback, originalDataSources, dataProtection);
					}
				}
				if (this.m_report == null)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsNotACurrentReportDefinition, Severity.Error, ObjectType.Report, null, "Namespace", new string[] { this.m_targetRDLNamespace });
					throw new ReportProcessingException(this.m_errorContext.Messages);
				}
			}
			catch (XmlException ex)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidReportDefinition, Severity.Error, ObjectType.Report, null, null, new string[] { ex.Message });
				throw new ReportProcessingException(this.m_errorContext.Messages);
			}
			finally
			{
				if (this.m_reader != null)
				{
					this.m_reader.Close();
					this.m_reader = null;
				}
				this.m_reportItemNames = null;
				this.m_scopeNames = null;
				this.m_imageStreamNames = null;
				this.m_reportContext = null;
				this.m_createChunkCallback = null;
				this.m_checkDataSourceCallback = null;
				description = this.m_description;
				language = null;
				if (this.m_reportLanguage != null)
				{
					language = this.m_reportLanguage.Name;
				}
				dataSources = this.m_dataSources;
				userReferenceLocation = this.m_userReferenceLocation;
				hasExternalImages = this.m_hasExternalImages;
				hasHyperlinks = this.m_hasHyperlinks;
				this.m_description = null;
				this.m_dataSources = null;
			}
		}

		// Token: 0x0600696F RID: 26991 RVA: 0x0019A268 File Offset: 0x00198468
		private Report ReadReport(ReportProcessing.ResolveTemporaryDataSource resolveTemporaryDataSourceCallback, DataSourceInfoCollection originalDataSources, IDataProtection dataProtection)
		{
			Report report = new Report(this.GenerateID(), this.GenerateID());
			ReportPublishing.PublishingContextStruct publishingContextStruct = new ReportPublishing.PublishingContextStruct(LocationFlags.None, report.ObjectType, null);
			ExpressionInfo expressionInfo = null;
			this.m_reportItemCollectionList.Add(report.ReportItems);
			this.m_aggregateHolderList.Add(report);
			this.m_runningValueHolderList.Add(report.ReportItems);
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Report" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (localName != null)
					{
						switch (localName.Length)
						{
						case 4:
						{
							char c = localName[0];
							if (c != 'B')
							{
								if (c == 'C')
								{
									if (localName == "Code")
									{
										report.Code = this.m_reader.ReadString();
										this.m_reportCT.Builder.SetCustomCode();
									}
								}
							}
							else if (localName == "Body")
							{
								this.ReadBody(report, publishingContextStruct);
							}
							break;
						}
						case 5:
							if (localName == "Width")
							{
								report.Width = this.ReadSize();
							}
							break;
						case 6:
						{
							char c = localName[0];
							if (c != 'A')
							{
								if (c == 'C')
								{
									if (localName == "Custom")
									{
										report.Custom = this.m_reader.ReadCustomXml();
									}
								}
							}
							else if (localName == "Author")
							{
								report.Author = this.m_reader.ReadString();
							}
							break;
						}
						case 7:
							if (localName == "Classes")
							{
								report.CodeClasses = this.ReadClasses(publishingContextStruct);
							}
							break;
						case 8:
						{
							char c = localName[0];
							if (c != 'D')
							{
								if (c == 'L')
								{
									if (localName == "Language")
									{
										expressionInfo = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.ReportLanguage, ExpressionParser.ConstantType.String, publishingContextStruct);
										report.Language = expressionInfo;
									}
								}
							}
							else if (localName == "DataSets")
							{
								this.ReadDataSets(publishingContextStruct);
							}
							break;
						}
						case 9:
						{
							char c = localName[0];
							if (c != 'P')
							{
								if (c == 'T')
								{
									if (localName == "TopMargin")
									{
										report.TopMargin = this.ReadSize();
									}
								}
							}
							else if (localName == "PageWidth")
							{
								report.PageWidth = this.ReadSize();
							}
							break;
						}
						case 10:
						{
							char c = localName[6];
							if (c <= 'h')
							{
								if (c != 'a')
								{
									if (c == 'h')
									{
										if (localName == "DataSchema")
										{
											report.DataSchema = this.m_reader.ReadString();
										}
									}
								}
								else if (localName == "PageHeader")
								{
									report.PageHeader = this.ReadPageSection(true, report, publishingContextStruct);
								}
							}
							else if (c != 'i')
							{
								if (c != 'o')
								{
									if (c == 'r')
									{
										if (localName == "LeftMargin")
										{
											report.LeftMargin = this.ReadSize();
										}
									}
								}
								else if (localName == "PageFooter")
								{
									report.PageFooter = this.ReadPageSection(false, report, publishingContextStruct);
								}
							}
							else if (localName == "PageHeight")
							{
								report.PageHeight = this.ReadSize();
							}
							break;
						}
						case 11:
						{
							char c = localName[1];
							if (c <= 'e')
							{
								if (c != 'a')
								{
									if (c == 'e')
									{
										if (localName == "Description")
										{
											this.m_description = this.m_reader.ReadString();
										}
									}
								}
								else if (localName == "DataSources")
								{
									report.DataSources = this.ReadDataSources(publishingContextStruct, resolveTemporaryDataSourceCallback, originalDataSources, dataProtection);
								}
							}
							else if (c != 'i')
							{
								if (c != 'o')
								{
									if (c == 'u')
									{
										if (localName == "AutoRefresh")
										{
											report.AutoRefresh = this.m_reader.ReadInteger();
										}
									}
								}
								else if (localName == "CodeModules")
								{
									report.CodeModules = this.ReadCodeModules(publishingContextStruct);
								}
							}
							else if (localName == "RightMargin")
							{
								report.RightMargin = this.ReadSize();
							}
							break;
						}
						case 12:
							if (localName == "BottomMargin")
							{
								report.BottomMargin = this.ReadSize();
							}
							break;
						case 13:
							if (localName == "DataTransform")
							{
								report.DataTransform = this.m_reader.ReadString();
							}
							break;
						case 14:
							if (localName == "EmbeddedImages")
							{
								report.EmbeddedImages = this.ReadEmbeddedImages(publishingContextStruct);
							}
							break;
						case 15:
							if (localName == "DataElementName")
							{
								report.DataElementName = this.m_reader.ReadString();
							}
							break;
						case 16:
						{
							char c = localName[0];
							if (c <= 'D')
							{
								if (c != 'C')
								{
									if (c == 'D')
									{
										if (localName == "DataElementStyle")
										{
											report.DataElementStyleAttribute = this.ReadDataElementStyle();
										}
									}
								}
								else if (localName == "CustomProperties")
								{
									report.CustomProperties = this.ReadCustomProperties(publishingContextStruct);
								}
							}
							else if (c != 'I')
							{
								if (c == 'R')
								{
									if (localName == "ReportParameters")
									{
										report.Parameters = this.ReadReportParameters(publishingContextStruct);
									}
								}
							}
							else if (localName == "InteractiveWidth")
							{
								report.InteractiveWidth = this.ReadSize();
							}
							break;
						}
						case 17:
							if (localName == "InteractiveHeight")
							{
								report.InteractiveHeight = this.ReadSize();
							}
							break;
						}
					}
				}
			}
			while (!flag);
			if (expressionInfo == null)
			{
				this.m_reportLanguage = Localization.DefaultReportServerSpecificCulture;
			}
			else if (ExpressionInfo.Types.Constant == expressionInfo.Type)
			{
				PublishingValidator.ValidateSpecificLanguage(expressionInfo, ObjectType.Report, null, "Language", this.m_errorContext, out this.m_reportLanguage);
			}
			if (this.m_interactive)
			{
				report.ShowHideType = Report.ShowHideTypes.Interactive;
			}
			else if (this.m_static)
			{
				report.ShowHideType = Report.ShowHideTypes.Static;
			}
			else
			{
				report.ShowHideType = Report.ShowHideTypes.None;
			}
			report.ImageStreamNames = this.m_imageStreamNames;
			report.SubReports = this.m_subReports;
			report.BodyID = this.GenerateID();
			report.LastID = this.m_idCounter;
			return report;
		}

		// Token: 0x06006970 RID: 26992 RVA: 0x0019A9FC File Offset: 0x00198BFC
		private EmbeddedImageHashtable ReadEmbeddedImages(ReportPublishing.PublishingContextStruct context)
		{
			EmbeddedImageHashtable embeddedImageHashtable = new EmbeddedImageHashtable();
			CLSUniqueNameValidator clsuniqueNameValidator = new CLSUniqueNameValidator(ProcessingErrorCode.rsInvalidNameNotCLSCompliant, ProcessingErrorCode.rsDuplicateEmbeddedImageName);
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("EmbeddedImages" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "EmbeddedImage")
				{
					this.ReadEmbeddedImage(embeddedImageHashtable, clsuniqueNameValidator, context);
				}
			}
			while (!flag);
			return embeddedImageHashtable;
		}

		// Token: 0x06006971 RID: 26993 RVA: 0x0019AA80 File Offset: 0x00198C80
		private void ReadEmbeddedImage(EmbeddedImageHashtable embeddedImages, CLSUniqueNameValidator embeddedImageNames, ReportPublishing.PublishingContextStruct context)
		{
			string attribute = this.m_reader.GetAttribute("Name");
			context.ObjectType = ObjectType.EmbeddedImage;
			context.ObjectName = attribute;
			embeddedImageNames.Validate(context.ObjectType, context.ObjectName, this.m_errorContext);
			bool flag = false;
			byte[] array = null;
			string text = null;
			for (;;)
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						goto IL_0115;
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "MIMEType"))
					{
						if (localName == "ImageData")
						{
							string text2 = this.m_reader.ReadString();
							try
							{
								array = Convert.FromBase64String(text2);
								goto IL_012E;
							}
							catch
							{
								this.m_errorContext.Register(ProcessingErrorCode.rsInvalidEmbeddedImage, Severity.Error, context.ObjectType, context.ObjectName, "ImageData", Array.Empty<string>());
								goto IL_012E;
							}
							goto IL_0115;
						}
					}
					else
					{
						text = this.m_reader.ReadString();
						if (!PublishingValidator.ValidateMimeType(text, context.ObjectType, context.ObjectName, this.m_reader.LocalName, this.m_errorContext))
						{
							text = null;
						}
					}
				}
				IL_012E:
				if (flag)
				{
					break;
				}
				continue;
				IL_0115:
				if ("EmbeddedImage" == this.m_reader.LocalName)
				{
					flag = true;
					goto IL_012E;
				}
				goto IL_012E;
			}
			string text3 = Guid.NewGuid().ToString();
			embeddedImages.Add(attribute, new ImageInfo(text3, text));
			if (array != null && text != null && this.m_createChunkCallback != null)
			{
				using (Stream stream = this.m_createChunkCallback(text3, ReportProcessing.ReportChunkTypes.Image, text))
				{
					stream.Write(array, 0, array.Length);
				}
			}
		}

		// Token: 0x06006972 RID: 26994 RVA: 0x0019AC3C File Offset: 0x00198E3C
		private DataSourceList ReadDataSources(ReportPublishing.PublishingContextStruct context, ReportProcessing.ResolveTemporaryDataSource resolveTemporaryDataSourceCallback, DataSourceInfoCollection originalDataSources, IDataProtection dataProtection)
		{
			DataSourceList dataSourceList = new DataSourceList();
			DataSourceNameValidator dataSourceNameValidator = new DataSourceNameValidator();
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("DataSources" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "DataSource")
				{
					dataSourceList.Add(this.ReadDataSource(dataSourceNameValidator, context, resolveTemporaryDataSourceCallback, originalDataSources, dataProtection));
				}
			}
			while (!flag);
			return dataSourceList;
		}

		// Token: 0x06006973 RID: 26995 RVA: 0x0019ACC4 File Offset: 0x00198EC4
		private DataSource ReadDataSource(DataSourceNameValidator dataSourceNames, ReportPublishing.PublishingContextStruct context, ReportProcessing.ResolveTemporaryDataSource resolveTemporaryDataSourceCallback, DataSourceInfoCollection originalDataSources, IDataProtection dataProtection)
		{
			DataSource dataSource = new DataSource();
			dataSource.Name = this.m_reader.GetAttribute("Name");
			context.ObjectType = ObjectType.DataSource;
			context.ObjectName = dataSource.Name;
			bool flag = false;
			if (dataSourceNames.Validate(context.ObjectType, context.ObjectName, this.m_errorContext))
			{
				flag = true;
			}
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			StringList stringList = null;
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag5 = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("DataSource" == this.m_reader.LocalName)
							{
								flag5 = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (!(localName == "Transaction"))
						{
							if (!(localName == "ConnectionProperties"))
							{
								if (localName == "DataSourceReference")
								{
									flag3 = true;
									dataSource.DataSourceReference = this.m_reader.ReadString();
								}
							}
							else
							{
								flag2 = true;
								this.ReadConnectionProperties(dataSource, context, ref flag4, ref stringList);
							}
						}
						else
						{
							dataSource.Transaction = this.m_reader.ReadBoolean();
						}
					}
				}
				while (!flag5);
			}
			if ((!flag3 && !flag2) || (flag3 && flag2))
			{
				flag = false;
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidDataSource, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
			}
			if (flag && !this.m_dataSourceNames.ContainsKey(dataSource.Name))
			{
				this.m_dataSourceNames.Add(dataSource.Name, null);
			}
			DataSourceInfo dataSourceInfo = null;
			if (flag2)
			{
				dataSource.IsComplex = flag4;
				dataSource.ParameterNames = stringList;
				bool flag6 = false;
				if (dataSource.ConnectStringExpression.Type != ExpressionInfo.Types.Constant)
				{
					flag6 = true;
				}
				dataSourceInfo = new DataSourceInfo(dataSource.Name, dataSource.Type, flag6 ? null : dataSource.ConnectStringExpression.OriginalText, flag6, dataSource.IntegratedSecurity, dataSource.Prompt, dataProtection);
			}
			else if (flag3)
			{
				string text;
				if (this.m_reportContext != null)
				{
					text = this.m_reportContext.MapUserProvidedPath(dataSource.DataSourceReference);
				}
				else
				{
					text = dataSource.DataSourceReference;
				}
				if (this.m_checkDataSourceCallback == null)
				{
					dataSourceInfo = new DataSourceInfo(dataSource.Name, text, Guid.Empty);
				}
				else
				{
					Guid empty = Guid.Empty;
					DataSourceInfo dataSourceInfo2 = this.m_checkDataSourceCallback(text, out empty);
					if (dataSourceInfo2 == null)
					{
						dataSourceInfo = new DataSourceInfo(dataSource.Name);
						this.m_errorContext.Register(ProcessingErrorCode.rsDataSourceReferenceNotPublished, Severity.Warning, context.ObjectType, context.ObjectName, "Report", new string[] { dataSource.Name });
					}
					else
					{
						dataSourceInfo = new DataSourceInfo(dataSource.Name, text, empty, dataSourceInfo2);
					}
				}
			}
			if (dataSourceInfo != null)
			{
				if (resolveTemporaryDataSourceCallback != null)
				{
					resolveTemporaryDataSourceCallback(dataSourceInfo, originalDataSources);
				}
				dataSource.ID = dataSourceInfo.ID;
				this.m_dataSources.Add(dataSourceInfo);
			}
			return dataSource;
		}

		// Token: 0x06006974 RID: 26996 RVA: 0x0019AF9C File Offset: 0x0019919C
		private StringList ReadCodeModules(ReportPublishing.PublishingContextStruct context)
		{
			StringList stringList = new StringList();
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("CodeModules" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "CodeModule")
				{
					stringList.Add(this.m_reader.ReadString());
				}
			}
			while (!flag);
			return stringList;
		}

		// Token: 0x06006975 RID: 26997 RVA: 0x0019B01C File Offset: 0x0019921C
		private CodeClassList ReadClasses(ReportPublishing.PublishingContextStruct context)
		{
			CodeClassList codeClassList = new CodeClassList();
			CLSUniqueNameValidator clsuniqueNameValidator = new CLSUniqueNameValidator(ProcessingErrorCode.rsInvalidNameNotCLSCompliant, ProcessingErrorCode.rsDuplicateClassInstanceName);
			context.ObjectType = ObjectType.CodeClass;
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Classes" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "Class")
				{
					this.ReadClass(codeClassList, clsuniqueNameValidator, context);
				}
			}
			while (!flag);
			this.m_reportCT.Builder.SetCustomCode();
			return codeClassList;
		}

		// Token: 0x06006976 RID: 26998 RVA: 0x0019B0BC File Offset: 0x001992BC
		private void ReadClass(CodeClassList codeClasses, CLSUniqueNameValidator instanceNameValidator, ReportPublishing.PublishingContextStruct context)
		{
			bool flag = false;
			CodeClass codeClass = default(CodeClass);
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Class" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "ClassName"))
					{
						if (localName == "InstanceName")
						{
							codeClass.InstanceName = this.m_reader.ReadString();
							if (!instanceNameValidator.Validate(context.ObjectType, codeClass.InstanceName, this.m_errorContext))
							{
								codeClass.InstanceName = null;
							}
						}
					}
					else
					{
						codeClass.ClassName = this.m_reader.ReadString();
					}
				}
			}
			while (!flag);
			codeClasses.Add(codeClass);
		}

		// Token: 0x06006977 RID: 26999 RVA: 0x0019B198 File Offset: 0x00199398
		private void ReadConnectionProperties(DataSource dataSource, ReportPublishing.PublishingContextStruct context, ref bool hasComplexParams, ref StringList parametersInQuery)
		{
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("ConnectionProperties" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "DataProvider"))
					{
						if (!(localName == "ConnectString"))
						{
							if (!(localName == "IntegratedSecurity"))
							{
								if (localName == "Prompt")
								{
									dataSource.Prompt = this.m_reader.ReadString();
								}
							}
							else
							{
								dataSource.IntegratedSecurity = this.m_reader.ReadBoolean();
							}
						}
						else
						{
							Global.Tracer.Assert(ObjectType.DataSource == context.ObjectType);
							dataSource.ConnectStringExpression = this.ReadQueryOrParameterExpression(context, ref hasComplexParams, ref parametersInQuery);
						}
					}
					else
					{
						dataSource.Type = this.m_reader.ReadString();
					}
				}
			}
			while (!flag);
		}

		// Token: 0x06006978 RID: 27000 RVA: 0x0019B294 File Offset: 0x00199494
		private void ReadDataSets(ReportPublishing.PublishingContextStruct context)
		{
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("DataSets" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "DataSet")
				{
					this.m_dataSets.Add(this.ReadDataSet(context));
				}
			}
			while (!flag);
		}

		// Token: 0x06006979 RID: 27001 RVA: 0x0019B310 File Offset: 0x00199510
		private DataSet ReadDataSet(ReportPublishing.PublishingContextStruct context)
		{
			DataSet dataSet = new DataSet(this.GenerateID());
			YukonDataSetInfo yukonDataSetInfo = null;
			dataSet.Name = this.m_reader.GetAttribute("Name");
			context.Location |= LocationFlags.InDataSet;
			context.ObjectType = dataSet.ObjectType;
			context.ObjectName = dataSet.Name;
			if (this.m_scopeNames.Validate(false, context.ObjectName, context.ObjectType, context.ObjectName, this.m_errorContext))
			{
				this.m_reportScopes.Add(dataSet.Name, dataSet);
			}
			this.m_aggregateHolderList.Add(dataSet);
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("DataSet" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (localName != null)
					{
						switch (localName.Length)
						{
						case 5:
							if (localName == "Query")
							{
								dataSet.Query = this.ReadQuery(context, out yukonDataSetInfo);
							}
							break;
						case 6:
							if (localName == "Fields")
							{
								int num;
								dataSet.Fields = this.ReadFields(context, out num);
								dataSet.NonCalculatedFieldCount = num;
							}
							break;
						case 7:
							if (localName == "Filters")
							{
								dataSet.Filters = this.ReadFilters(ExpressionParser.ExpressionType.DataSetFilters, context);
							}
							break;
						case 9:
							if (localName == "Collation")
							{
								dataSet.Collation = this.m_reader.ReadString();
								uint num2;
								if (DataSetValidator.ValidateCollation(dataSet.Collation, out num2))
								{
									dataSet.LCID = num2;
								}
							}
							break;
						case 15:
							if (localName == "CaseSensitivity")
							{
								dataSet.CaseSensitivity = this.ReadSensitivity();
							}
							break;
						case 16:
							if (localName == "WidthSensitivity")
							{
								dataSet.WidthSensitivity = this.ReadSensitivity();
							}
							break;
						case 17:
							if (localName == "AccentSensitivity")
							{
								dataSet.AccentSensitivity = this.ReadSensitivity();
							}
							break;
						case 19:
							if (localName == "KanatypeSensitivity")
							{
								dataSet.KanatypeSensitivity = this.ReadSensitivity();
							}
							break;
						}
					}
				}
			}
			while (!flag);
			if (yukonDataSetInfo != null && !this.m_dataSetQueryInfo.ContainsKey(context.ObjectName))
			{
				this.m_dataSetQueryInfo.Add(context.ObjectName, yukonDataSetInfo);
				int num3 = ((dataSet.Fields != null) ? dataSet.Fields.Count : 0);
				while (num3 > 0 && dataSet.Fields[num3 - 1].IsCalculatedField)
				{
					num3--;
				}
				yukonDataSetInfo.CalculatedFieldIndex = num3;
			}
			return dataSet;
		}

		// Token: 0x0600697A RID: 27002 RVA: 0x0019B628 File Offset: 0x00199828
		private ReportQuery ReadQuery(ReportPublishing.PublishingContextStruct context, out YukonDataSetInfo queryDataSetInfo)
		{
			ReportQuery reportQuery = new ReportQuery();
			bool flag = false;
			bool flag2 = false;
			StringList stringList = null;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Query" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "DataSourceName"))
					{
						if (!(localName == "CommandType"))
						{
							if (!(localName == "CommandText"))
							{
								if (!(localName == "QueryParameters"))
								{
									if (localName == "Timeout")
									{
										reportQuery.TimeOut = this.m_reader.ReadInteger();
									}
								}
								else
								{
									reportQuery.Parameters = this.ReadQueryParameters(context, ref flag2, ref stringList);
								}
							}
							else
							{
								Global.Tracer.Assert(ObjectType.DataSet == context.ObjectType);
								context.ObjectType = ObjectType.Query;
								reportQuery.CommandText = this.ReadQueryOrParameterExpression(context, ref flag2, ref stringList);
								context.ObjectType = ObjectType.DataSet;
							}
						}
						else
						{
							reportQuery.CommandType = this.ReadCommandType();
						}
					}
					else
					{
						reportQuery.DataSourceName = this.m_reader.ReadString();
					}
				}
			}
			while (!flag);
			queryDataSetInfo = new YukonDataSetInfo(this.m_dataSets.Count, flag2, stringList);
			return reportQuery;
		}

		// Token: 0x0600697B RID: 27003 RVA: 0x0019B780 File Offset: 0x00199980
		private ExpressionInfo ReadQueryOrParameterExpression(ReportPublishing.PublishingContextStruct context, ref bool isComplex, ref StringList parametersInQuery)
		{
			Global.Tracer.Assert(ObjectType.QueryParameter == context.ObjectType || ObjectType.Query == context.ObjectType || ObjectType.DataSource == context.ObjectType);
			ExpressionParser.DetectionFlags detectionFlags = (ExpressionParser.DetectionFlags)0;
			if (this.m_parametersNotUsedInQuery || !isComplex)
			{
				detectionFlags |= ExpressionParser.DetectionFlags.ParameterReference;
			}
			this.m_reportLocationFlags = UserLocationFlags.ReportQueries;
			bool flag;
			string text;
			ExpressionInfo expressionInfo = this.ReadExpression(this.m_reader.LocalName, context.ObjectName, ExpressionParser.ExpressionType.QueryParameter, ExpressionParser.ConstantType.String, context, detectionFlags, out flag, out text);
			if ((this.m_parametersNotUsedInQuery || !isComplex) && flag)
			{
				if (text == null)
				{
					this.m_parametersNotUsedInQuery = false;
					isComplex = true;
				}
				else
				{
					if (!this.m_usedInQueryInfos.Contains(text))
					{
						this.m_usedInQueryInfos.Add(text, true);
					}
					if (!isComplex)
					{
						if (parametersInQuery == null)
						{
							parametersInQuery = new StringList();
						}
						parametersInQuery.Add(text);
					}
				}
			}
			this.m_reportLocationFlags = UserLocationFlags.ReportBody;
			return expressionInfo;
		}

		// Token: 0x0600697C RID: 27004 RVA: 0x0019B854 File Offset: 0x00199A54
		private ParameterValueList ReadQueryParameters(ReportPublishing.PublishingContextStruct context, ref bool hasComplexParams, ref StringList parametersInQuery)
		{
			ParameterValueList parameterValueList = new ParameterValueList();
			bool flag = false;
			string objectName = context.ObjectName;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("QueryParameters" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "QueryParameter")
				{
					parameterValueList.Add(this.ReadQueryParameter(context, ref hasComplexParams, ref parametersInQuery));
				}
			}
			while (!flag);
			context.ObjectName = objectName;
			return parameterValueList;
		}

		// Token: 0x0600697D RID: 27005 RVA: 0x0019B8E4 File Offset: 0x00199AE4
		private ParameterValue ReadQueryParameter(ReportPublishing.PublishingContextStruct context, ref bool isComplex, ref StringList parametersInQuery)
		{
			Global.Tracer.Assert(ObjectType.DataSet == context.ObjectType);
			ParameterValue parameterValue = new ParameterValue();
			parameterValue.Name = this.m_reader.GetAttribute("Name");
			context.ObjectType = ObjectType.QueryParameter;
			context.ObjectName = parameterValue.Name;
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("QueryParameter" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "Value")
				{
					parameterValue.Value = this.ReadQueryOrParameterExpression(context, ref isComplex, ref parametersInQuery);
				}
			}
			while (!flag);
			return parameterValue;
		}

		// Token: 0x0600697E RID: 27006 RVA: 0x0019B9A4 File Offset: 0x00199BA4
		private DataFieldList ReadFields(ReportPublishing.PublishingContextStruct context, out int calculatedFieldStartIndex)
		{
			DataFieldList dataFieldList = new DataFieldList();
			CLSUniqueNameValidator clsuniqueNameValidator = new CLSUniqueNameValidator(ProcessingErrorCode.rsInvalidFieldNameNotCLSCompliant, ProcessingErrorCode.rsDuplicateFieldName);
			bool flag = false;
			calculatedFieldStartIndex = -1;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Fields" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "Field")
				{
					Field field = this.ReadField(clsuniqueNameValidator, context);
					if (field.IsCalculatedField)
					{
						if (calculatedFieldStartIndex < 0)
						{
							calculatedFieldStartIndex = dataFieldList.Count;
						}
						dataFieldList.Add(field);
					}
					else if (calculatedFieldStartIndex < 0)
					{
						dataFieldList.Add(field);
					}
					else
					{
						dataFieldList.Insert(calculatedFieldStartIndex, field);
						calculatedFieldStartIndex++;
					}
				}
			}
			while (!flag);
			if (0 > calculatedFieldStartIndex)
			{
				calculatedFieldStartIndex = dataFieldList.Count;
			}
			return dataFieldList;
		}

		// Token: 0x0600697F RID: 27007 RVA: 0x0019BA7C File Offset: 0x00199C7C
		private Field ReadField(CLSUniqueNameValidator names, ReportPublishing.PublishingContextStruct context)
		{
			Global.Tracer.Assert(ObjectType.DataSet == context.ObjectType);
			string objectName = context.ObjectName;
			Field field = new Field();
			context.ObjectType = ObjectType.Field;
			string text = null;
			field.Name = this.m_reader.GetAttribute("Name");
			Global.Tracer.Assert(field.Name != null, "Name is a mandatory attribute of field elements");
			context.ObjectName = field.Name;
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("Field" == this.m_reader.LocalName)
							{
								flag = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (!(localName == "DataField"))
						{
							if (localName == "Value")
							{
								text = this.m_reader.ReadString();
								if (text != null)
								{
									context.ObjectName = text;
									field.Value = this.ReadExpression(true, text, this.m_reader.LocalName, objectName, ExpressionParser.ExpressionType.FieldValue, ExpressionParser.ConstantType.String, context);
								}
							}
						}
						else
						{
							field.DataField = this.m_reader.ReadString();
							names.Validate(field.Name, field.DataField, objectName, this.m_errorContext);
						}
					}
				}
				while (!flag);
			}
			if (field.DataField != null == (text != null))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidField, Severity.Error, context.ObjectType, field.Name, null, new string[] { objectName });
			}
			return field;
		}

		// Token: 0x06006980 RID: 27008 RVA: 0x0019BC18 File Offset: 0x00199E18
		private FilterList ReadFilters(ExpressionParser.ExpressionType expressionType, ReportPublishing.PublishingContextStruct context)
		{
			FilterList filterList = new FilterList();
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Filters" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "Filter")
				{
					filterList.Add(this.ReadFilter(expressionType, context));
				}
			}
			while (!flag);
			return filterList;
		}

		// Token: 0x06006981 RID: 27009 RVA: 0x0019BC98 File Offset: 0x00199E98
		private Filter ReadFilter(ExpressionParser.ExpressionType expressionType, ReportPublishing.PublishingContextStruct context)
		{
			this.m_hasFilters = true;
			Filter filter = new Filter();
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Filter" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "FilterExpression"))
					{
						if (!(localName == "Operator"))
						{
							if (localName == "FilterValues")
							{
								filter.Values = this.ReadFilterValues(expressionType, context);
							}
						}
						else
						{
							filter.Operator = this.ReadOperator();
						}
					}
					else
					{
						filter.Expression = this.ReadExpression(this.m_reader.LocalName, expressionType, ExpressionParser.ConstantType.String, context);
					}
				}
			}
			while (!flag);
			int num = ((filter.Values == null) ? 0 : filter.Values.Count);
			switch (filter.Operator)
			{
			case Filter.Operators.Equal:
			case Filter.Operators.Like:
			case Filter.Operators.GreaterThan:
			case Filter.Operators.GreaterThanOrEqual:
			case Filter.Operators.LessThan:
			case Filter.Operators.LessThanOrEqual:
			case Filter.Operators.TopN:
			case Filter.Operators.BottomN:
			case Filter.Operators.TopPercent:
			case Filter.Operators.BottomPercent:
			case Filter.Operators.NotEqual:
				if (1 != num)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidNumberOfFilterValues, Severity.Error, context.ObjectType, context.ObjectName, "FilterValues", new string[]
					{
						filter.Operator.ToString(),
						Convert.ToString(1, CultureInfo.InvariantCulture)
					});
				}
				break;
			case Filter.Operators.Between:
				if (2 != num)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidNumberOfFilterValues, Severity.Error, context.ObjectType, context.ObjectName, "FilterValues", new string[]
					{
						filter.Operator.ToString(),
						Convert.ToString(2, CultureInfo.InvariantCulture)
					});
				}
				break;
			}
			if (ExpressionParser.ExpressionType.GroupingFilters == expressionType && filter.Expression.HasRecursiveAggregates())
			{
				this.m_hasSpecialRecursiveAggregates = true;
			}
			return filter;
		}

		// Token: 0x06006982 RID: 27010 RVA: 0x0019BE94 File Offset: 0x0019A094
		private ExpressionInfoList ReadFilterValues(ExpressionParser.ExpressionType expressionType, ReportPublishing.PublishingContextStruct context)
		{
			ExpressionInfoList expressionInfoList = new ExpressionInfoList();
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("FilterValues" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "FilterValue")
				{
					ExpressionInfo expressionInfo = this.ReadExpression(this.m_reader.LocalName, expressionType, ExpressionParser.ConstantType.String, context);
					expressionInfoList.Add(expressionInfo);
					if (ExpressionParser.ExpressionType.GroupingFilters == expressionType && expressionInfo.HasRecursiveAggregates())
					{
						this.m_hasSpecialRecursiveAggregates = true;
					}
				}
			}
			while (!flag);
			return expressionInfoList;
		}

		// Token: 0x06006983 RID: 27011 RVA: 0x0019BF38 File Offset: 0x0019A138
		private void ReadBody(Report report, ReportPublishing.PublishingContextStruct context)
		{
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("Body" == this.m_reader.LocalName)
							{
								flag = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (!(localName == "ReportItems"))
						{
							if (!(localName == "Height"))
							{
								if (!(localName == "Columns"))
								{
									if (!(localName == "ColumnSpacing"))
									{
										if (localName == "Style")
										{
											ReportPublishing.StyleInformation styleInformation = this.ReadStyle(context);
											styleInformation.Filter(ReportPublishing.StyleOwnerType.Body, false);
											report.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
										}
									}
									else
									{
										report.ColumnSpacing = this.ReadSize();
									}
								}
								else
								{
									int num = this.m_reader.ReadInteger();
									if (PublishingValidator.ValidateColumns(num, context.ObjectType, context.ObjectName, "Columns", this.m_errorContext))
									{
										report.Columns = num;
									}
								}
							}
							else
							{
								report.Height = this.ReadSize();
							}
						}
						else
						{
							this.ReadReportItems(null, report, report.ReportItems, context, null);
						}
					}
				}
				while (!flag);
			}
		}

		// Token: 0x06006984 RID: 27012 RVA: 0x0019C0A0 File Offset: 0x0019A2A0
		private ParameterDefList ReadReportParameters(ReportPublishing.PublishingContextStruct context)
		{
			ParameterDefList parameterDefList = new ParameterDefList();
			CLSUniqueNameValidator clsuniqueNameValidator = new CLSUniqueNameValidator(ProcessingErrorCode.rsInvalidNameNotCLSCompliant, ProcessingErrorCode.rsDuplicateReportParameterName);
			bool flag = false;
			int num = 0;
			Hashtable hashtable = new Hashtable();
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("ReportParameters" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "ReportParameter")
				{
					parameterDefList.Add(this.ReadReportParameter(clsuniqueNameValidator, hashtable, context, num));
					num++;
				}
			}
			while (!flag);
			return parameterDefList;
		}

		// Token: 0x06006985 RID: 27013 RVA: 0x0019C140 File Offset: 0x0019A340
		private ParameterDef ReadReportParameter(CLSUniqueNameValidator reportParameterNames, Hashtable parameterNames, ReportPublishing.PublishingContextStruct context, int count)
		{
			ParameterDef parameterDef = new ParameterDef();
			parameterDef.Name = this.m_reader.GetAttribute("Name");
			context.ObjectType = ObjectType.ReportParameter;
			context.ObjectName = parameterDef.Name;
			reportParameterNames.Validate(context.ObjectType, context.ObjectName, this.m_errorContext);
			string text = null;
			string text2 = null;
			bool flag = false;
			string text3 = null;
			bool flag2 = false;
			string text4 = null;
			List<string> list = null;
			string text5 = null;
			string text6 = null;
			bool flag3 = false;
			bool flag4 = false;
			DataSetReference dataSetReference = null;
			DataSetReference dataSetReference2 = null;
			bool flag5 = false;
			string text7 = null;
			bool flag6 = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("ReportParameter" == this.m_reader.LocalName)
						{
							flag6 = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (localName != null)
					{
						int length = localName.Length;
						switch (length)
						{
						case 6:
						{
							char c = localName[0];
							if (c != 'H')
							{
								if (c == 'P')
								{
									if (localName == "Prompt")
									{
										flag2 = true;
										text4 = this.m_reader.ReadString();
									}
								}
							}
							else if (localName == "Hidden")
							{
								flag5 = this.m_reader.ReadBoolean();
							}
							break;
						}
						case 7:
						case 9:
							break;
						case 8:
						{
							char c = localName[0];
							if (c != 'D')
							{
								if (c == 'N')
								{
									if (localName == "Nullable")
									{
										text2 = this.m_reader.ReadString();
									}
								}
							}
							else if (localName == "DataType")
							{
								text = this.m_reader.ReadString();
							}
							break;
						}
						case 10:
						{
							char c = localName[0];
							if (c != 'A')
							{
								if (c == 'M')
								{
									if (localName == "MultiValue")
									{
										text5 = this.m_reader.ReadString();
									}
								}
							}
							else if (localName == "AllowBlank")
							{
								text3 = this.m_reader.ReadString();
							}
							break;
						}
						case 11:
						{
							char c = localName[0];
							if (c != 'U')
							{
								if (c == 'V')
								{
									if (localName == "ValidValues")
									{
										flag4 = this.ReadValidValues(context, parameterDef, parameterNames, ref flag3, out dataSetReference);
									}
								}
							}
							else if (localName == "UsedInQuery")
							{
								text6 = this.m_reader.ReadString();
							}
							break;
						}
						case 12:
							if (localName == "DefaultValue")
							{
								flag = true;
								list = this.ReadDefaultValue(context, parameterDef, parameterNames, ref flag3, out dataSetReference2);
							}
							break;
						default:
							if (length == 17)
							{
								if (localName == "UseAllValidValues")
								{
									text7 = this.m_reader.ReadString();
								}
							}
							break;
						}
					}
				}
			}
			while (!flag6);
			parameterDef.Parse(parameterDef.Name, list, text, text2, text4, null, text3, text5, text6, flag5, this.m_errorContext, CultureInfo.InvariantCulture, text7);
			if (parameterDef.Nullable && !flag)
			{
				parameterDef.DefaultValues = new object[1];
				parameterDef.DefaultValues[0] = null;
			}
			if (parameterDef.DataType == DataType.Boolean)
			{
				dataSetReference = null;
			}
			if (!flag2 && !flag && !flag5 && (!parameterDef.Nullable || (parameterDef.ValidValuesValueExpressions != null && !flag4)))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsMissingParameterDefault, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
			}
			if (parameterDef.Nullable && parameterDef.MultiValue)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidMultiValueParameter, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
			}
			if (!parameterDef.MultiValue && list != null && list.Count > 1)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidDefaultValue, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
			}
			if (dataSetReference2 != null || dataSetReference != null)
			{
				this.m_dynamicParameters.Add(new DynamicParameter(dataSetReference, dataSetReference2, count, flag3));
			}
			if (!parameterNames.ContainsKey(parameterDef.Name))
			{
				parameterNames.Add(parameterDef.Name, count);
			}
			return parameterDef;
		}

		// Token: 0x06006986 RID: 27014 RVA: 0x0019C5B0 File Offset: 0x0019A7B0
		private List<string> ReadDefaultValue(ReportPublishing.PublishingContextStruct context, ParameterDef parameter, Hashtable parameterNames, ref bool isComplex, out DataSetReference defaultDataSet)
		{
			bool flag = false;
			bool flag2 = false;
			List<string> list = null;
			defaultDataSet = null;
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag3 = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("DefaultValue" == this.m_reader.LocalName)
							{
								flag3 = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (!(localName == "DataSetReference"))
						{
							if (localName == "Values")
							{
								flag2 = true;
								list = this.ReadValues(context, parameter, parameterNames, out isComplex);
							}
						}
						else
						{
							flag = true;
							defaultDataSet = this.ReadDataSetReference();
						}
					}
				}
				while (!flag3);
			}
			if ((!flag && !flag2) || (flag && flag2))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidDefaultValue, Severity.Error, context.ObjectType, context.ObjectName, "DefaultValue", Array.Empty<string>());
			}
			return list;
		}

		// Token: 0x06006987 RID: 27015 RVA: 0x0019C69C File Offset: 0x0019A89C
		private List<string> ReadValues(ReportPublishing.PublishingContextStruct context, ParameterDef parameter, Hashtable parameterNames, out bool isComplex)
		{
			List<string> list = null;
			ExpressionInfoList expressionInfoList = new ExpressionInfoList();
			bool flag = false;
			Hashtable hashtable = null;
			bool flag2 = false;
			isComplex = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Values" == this.m_reader.LocalName)
						{
							flag2 = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "Value")
				{
					ExpressionInfo expressionInfo = this.ReadParameterExpression(this.m_reader.LocalName, context, parameter, parameterNames, ref hashtable, ref flag, ref isComplex);
					expressionInfoList.Add(expressionInfo);
				}
			}
			while (!flag2);
			if (isComplex && parameterNames.Count > 0)
			{
				hashtable = (Hashtable)parameterNames.Clone();
			}
			if (flag)
			{
				parameter.DefaultExpressions = expressionInfoList;
			}
			else
			{
				list = new List<string>(expressionInfoList.Count);
				for (int i = 0; i < expressionInfoList.Count; i++)
				{
					list.Add(expressionInfoList[i].Value);
				}
			}
			parameter.Dependencies = hashtable;
			return list;
		}

		// Token: 0x06006988 RID: 27016 RVA: 0x0019C7A4 File Offset: 0x0019A9A4
		private bool ReadValidValues(ReportPublishing.PublishingContextStruct context, ParameterDef parameter, Hashtable parameterNames, ref bool isComplex, out DataSetReference validValueDataSet)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			validValueDataSet = null;
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag4 = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("ValidValues" == this.m_reader.LocalName)
							{
								flag4 = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (!(localName == "DataSetReference"))
						{
							if (localName == "ParameterValues")
							{
								flag2 = true;
								this.ReadParameterValues(context, parameter, parameterNames, ref isComplex, ref flag3);
							}
						}
						else
						{
							flag = true;
							validValueDataSet = this.ReadDataSetReference();
						}
					}
				}
				while (!flag4);
			}
			if ((!flag && !flag2) || (flag && flag2))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidValidValues, Severity.Error, context.ObjectType, context.ObjectName, "ValidValues", Array.Empty<string>());
			}
			return flag3;
		}

		// Token: 0x06006989 RID: 27017 RVA: 0x0019C894 File Offset: 0x0019AA94
		private DataSetReference ReadDataSetReference()
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("DataSetReference" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "DataSetName"))
					{
						if (!(localName == "ValueField"))
						{
							if (localName == "LabelField")
							{
								text3 = this.m_reader.ReadString();
							}
						}
						else
						{
							text2 = this.m_reader.ReadString();
						}
					}
					else
					{
						text = this.m_reader.ReadString();
					}
				}
			}
			while (!flag);
			return new DataSetReference(text, text2, text3);
		}

		// Token: 0x0600698A RID: 27018 RVA: 0x0019C95C File Offset: 0x0019AB5C
		private void ReadParameterValues(ReportPublishing.PublishingContextStruct context, ParameterDef parameter, Hashtable parameterNames, ref bool isComplex, ref bool containsExplicitNull)
		{
			ExpressionInfoList expressionInfoList = new ExpressionInfoList();
			ExpressionInfoList expressionInfoList2 = new ExpressionInfoList();
			Hashtable hashtable = null;
			bool flag = isComplex;
			bool flag2 = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("ParameterValues" == this.m_reader.LocalName)
						{
							flag2 = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "ParameterValue")
				{
					ExpressionInfo expressionInfo = null;
					ExpressionInfo expressionInfo2 = null;
					if (!this.m_reader.IsEmptyElement)
					{
						bool flag3 = false;
						do
						{
							this.m_reader.Read();
							XmlNodeType nodeType2 = this.m_reader.NodeType;
							if (nodeType2 != XmlNodeType.Element)
							{
								if (nodeType2 == XmlNodeType.EndElement)
								{
									if ("ParameterValue" == this.m_reader.LocalName)
									{
										flag3 = true;
									}
								}
							}
							else
							{
								string localName = this.m_reader.LocalName;
								if (!(localName == "Value"))
								{
									if (localName == "Label")
									{
										expressionInfo2 = this.ReadParameterExpression(this.m_reader.LocalName, context, parameter, parameterNames, ref hashtable, ref flag, ref isComplex);
									}
								}
								else
								{
									expressionInfo = this.ReadParameterExpression(this.m_reader.LocalName, context, parameter, parameterNames, ref hashtable, ref flag, ref isComplex);
								}
							}
						}
						while (!flag3);
					}
					containsExplicitNull |= expressionInfo == null;
					expressionInfoList.Add(expressionInfo);
					expressionInfoList2.Add(expressionInfo2);
				}
			}
			while (!flag2);
			if (isComplex && parameterNames.Count > 0)
			{
				hashtable = (Hashtable)parameterNames.Clone();
			}
			parameter.ValidValuesValueExpressions = expressionInfoList;
			parameter.ValidValuesLabelExpressions = expressionInfoList2;
			parameter.Dependencies = hashtable;
		}

		// Token: 0x0600698B RID: 27019 RVA: 0x0019CB00 File Offset: 0x0019AD00
		private ExpressionInfo ReadParameterExpression(string propertyName, ReportPublishing.PublishingContextStruct context, ParameterDef parameter, Hashtable parameterNames, ref Hashtable dependencies, ref bool dynamic, ref bool isComplex)
		{
			string text = null;
			bool flag = false;
			bool flag2;
			ExpressionInfo expressionInfo;
			if (isComplex)
			{
				dynamic = true;
				expressionInfo = this.ReadExpression(propertyName, null, ExpressionParser.ExpressionType.ReportParameter, ExpressionParser.ConstantType.String, context, out flag2);
			}
			else
			{
				ExpressionParser.DetectionFlags detectionFlags = ExpressionParser.DetectionFlags.ParameterReference;
				detectionFlags |= ExpressionParser.DetectionFlags.UserReference;
				bool flag3;
				expressionInfo = this.ReadExpression(propertyName, null, ExpressionParser.ExpressionType.ReportParameter, ExpressionParser.ConstantType.String, context, detectionFlags, out flag3, out text, out flag2);
				if (flag3)
				{
					dynamic = true;
					if (text == null)
					{
						isComplex = true;
					}
					else if (!parameterNames.ContainsKey(text))
					{
						flag = true;
					}
					else
					{
						if (dependencies == null)
						{
							dependencies = new Hashtable();
						}
						dependencies.Add(text, parameterNames[text]);
					}
				}
			}
			if (flag2)
			{
				if (parameter.Name != null && !this.m_reportParamUserProfile.Contains(parameter.Name))
				{
					this.m_reportParamUserProfile.Add(parameter.Name, true);
				}
				this.m_userReferenceLocation |= UserLocationFlags.ReportBody;
			}
			if (flag)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidReportParameterDependency, Severity.Error, ObjectType.ReportParameter, parameter.Name, "ValidValues", new string[] { text });
			}
			return expressionInfo;
		}

		// Token: 0x0600698C RID: 27020 RVA: 0x0019CBF8 File Offset: 0x0019ADF8
		private ParameterValueList ReadParameters(ReportPublishing.PublishingContextStruct context, bool doClsValidation)
		{
			bool flag;
			return this.ReadParameters(context, false, doClsValidation, out flag);
		}

		// Token: 0x0600698D RID: 27021 RVA: 0x0019CC10 File Offset: 0x0019AE10
		private ParameterValueList ReadParameters(ReportPublishing.PublishingContextStruct context, bool omitAllowed, bool doClsValidation, out bool computed)
		{
			computed = false;
			ParameterValueList parameterValueList = new ParameterValueList();
			ParameterNameValidator parameterNameValidator = new ParameterNameValidator();
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Parameters" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "Parameter")
				{
					bool flag2;
					parameterValueList.Add(this.ReadParameter(parameterNameValidator, context, omitAllowed, doClsValidation, out flag2));
					computed = computed || flag2;
				}
			}
			while (!flag);
			return parameterValueList;
		}

		// Token: 0x0600698E RID: 27022 RVA: 0x0019CCA4 File Offset: 0x0019AEA4
		private ParameterValue ReadParameter(ParameterNameValidator parameterNames, ReportPublishing.PublishingContextStruct context, bool omitAllowed, bool doClsValidation, out bool computed)
		{
			computed = false;
			bool flag = false;
			bool flag2 = false;
			ParameterValue parameterValue = new ParameterValue();
			parameterValue.Name = this.m_reader.GetAttribute("Name");
			if (doClsValidation)
			{
				parameterNames.Validate(parameterValue.Name, context.ObjectType, context.ObjectName, this.m_errorContext);
			}
			parameterValue.Value = null;
			parameterValue.Omit = null;
			bool flag3 = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Parameter" == this.m_reader.LocalName)
						{
							flag3 = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Value"))
					{
						if (localName == "Omit")
						{
							if (omitAllowed)
							{
								parameterValue.Omit = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.Boolean, context, out flag2);
							}
						}
					}
					else
					{
						parameterValue.Value = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag);
					}
				}
			}
			while (!flag3);
			computed = flag || flag2;
			return parameterValue;
		}

		// Token: 0x0600698F RID: 27023 RVA: 0x0019CDC4 File Offset: 0x0019AFC4
		private PageSection ReadPageSection(bool isHeader, Report report, ReportPublishing.PublishingContextStruct context)
		{
			PageSection pageSection = new PageSection(isHeader, this.GenerateID(), this.GenerateID(), report);
			context.Location |= LocationFlags.InPageSection;
			context.ObjectType = pageSection.ObjectType;
			context.ObjectName = null;
			this.m_reportItemCollectionList.Add(pageSection.ReportItems);
			this.m_runningValueHolderList.Add(pageSection.ReportItems);
			this.m_reportLocationFlags = UserLocationFlags.ReportPageSection;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if (isHeader)
						{
							if ("PageHeader" == this.m_reader.LocalName)
							{
								flag3 = true;
							}
						}
						else if ("PageFooter" == this.m_reader.LocalName)
						{
							flag3 = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Height"))
					{
						if (!(localName == "PrintOnFirstPage"))
						{
							if (!(localName == "PrintOnLastPage"))
							{
								if (!(localName == "ReportItems"))
								{
									if (localName == "Style")
									{
										ReportPublishing.StyleInformation styleInformation = this.ReadStyle(context, out flag2);
										styleInformation.Filter(ReportPublishing.StyleOwnerType.PageSection, false);
										pageSection.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
									}
								}
								else
								{
									this.ReadReportItems(null, pageSection, pageSection.ReportItems, context, null, out flag);
								}
							}
							else
							{
								pageSection.PrintOnLastPage = this.m_reader.ReadBoolean();
							}
						}
						else
						{
							pageSection.PrintOnFirstPage = this.m_reader.ReadBoolean();
						}
					}
					else
					{
						pageSection.Height = this.ReadSize();
					}
				}
			}
			while (!flag3);
			pageSection.PostProcessEvaluate = (flag || flag2) | this.m_pageSectionDrillthroughs;
			this.m_pageSectionDrillthroughs = false;
			this.m_reportLocationFlags = UserLocationFlags.ReportBody;
			return pageSection;
		}

		// Token: 0x06006990 RID: 27024 RVA: 0x0019CFB4 File Offset: 0x0019B1B4
		private void ReadReportItems(string propertyName, ReportItem parent, ReportItemCollection parentCollection, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget, out bool computed)
		{
			computed = false;
			int num = 0;
			bool flag = parent is Matrix;
			bool flag2 = parent is Table;
			bool flag3 = parent is CustomReportItem;
			bool flag4 = false;
			do
			{
				ReportItem reportItem = null;
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("ReportItems" == this.m_reader.LocalName || (flag3 && "AltReportItem" == this.m_reader.LocalName))
						{
							flag4 = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (localName != null)
					{
						switch (localName.Length)
						{
						case 4:
						{
							char c = localName[2];
							if (c != 'n')
							{
								if (c == 's')
								{
									if (localName == "List")
									{
										num++;
										reportItem = this.ReadList(parent, context);
									}
								}
							}
							else if (localName == "Line")
							{
								num++;
								reportItem = this.ReadLine(parent, context);
							}
							break;
						}
						case 5:
						{
							char c = localName[0];
							if (c != 'C')
							{
								if (c != 'I')
								{
									if (c == 'T')
									{
										if (localName == "Table")
										{
											num++;
											reportItem = this.ReadTable(parent, context);
										}
									}
								}
								else if (localName == "Image")
								{
									num++;
									reportItem = this.ReadImage(parent, context);
								}
							}
							else if (localName == "Chart")
							{
								num++;
								reportItem = this.ReadChart(parent, context);
							}
							break;
						}
						case 6:
							if (localName == "Matrix")
							{
								num++;
								reportItem = this.ReadMatrix(parent, context);
							}
							break;
						case 7:
							if (localName == "Textbox")
							{
								num++;
								reportItem = this.ReadTextbox(parent, context, textBoxesWithDefaultSortTarget);
							}
							break;
						case 8:
						{
							char c = localName[0];
							if (c != 'C')
							{
								if (c == 'O')
								{
									if (localName == "OWCChart")
									{
										num++;
										reportItem = this.ReadOWCChart(parent, context);
									}
								}
							}
							else if (localName == "Checkbox")
							{
								num++;
								reportItem = this.ReadCheckbox(parent, context);
							}
							break;
						}
						case 9:
						{
							char c = localName[0];
							if (c != 'R')
							{
								if (c == 'S')
								{
									if (localName == "Subreport")
									{
										num++;
										reportItem = this.ReadSubreport(parent, context);
									}
								}
							}
							else if (localName == "Rectangle")
							{
								num++;
								reportItem = this.ReadRectangle(parent, context, textBoxesWithDefaultSortTarget);
							}
							break;
						}
						case 14:
							if (localName == "ActiveXControl")
							{
								num++;
								reportItem = this.ReadActiveXControl(parent, context);
							}
							break;
						case 16:
							if (localName == "CustomReportItem")
							{
								num++;
								if (!flag3)
								{
									reportItem = this.ReadCustomReportItem(parent, context, textBoxesWithDefaultSortTarget);
								}
								else
								{
									this.m_errorContext.Register(ProcessingErrorCode.rsInvalidAltReportItem, Severity.Error, context.ObjectType, context.ObjectName, propertyName, Array.Empty<string>());
								}
							}
							break;
						}
					}
					if (flag && num > 1)
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsMultiReportItemsInMatrixSection, Severity.Error, context.ObjectType, context.ObjectName, propertyName, Array.Empty<string>());
					}
					if (flag && (LocationFlags.InMatrixSubtotal & context.Location) != (LocationFlags)0 && reportItem != null && !(reportItem is TextBox))
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsInvalidMatrixSubtotalReportItem, Severity.Error, context.ObjectType, context.ObjectName, "Subtotal", Array.Empty<string>());
					}
					if (flag2 && num > 1)
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsMultiReportItemsInTableCell, Severity.Error, context.ObjectType, context.ObjectName, propertyName, Array.Empty<string>());
					}
					if (flag3 && num > 1)
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsMultiReportItemsInCustomReportItem, Severity.Error, context.ObjectType, context.ObjectName, propertyName, Array.Empty<string>());
					}
					if (reportItem != null)
					{
						computed |= reportItem.Computed;
						parentCollection.AddReportItem(reportItem);
						if (flag || flag2)
						{
							reportItem.IsFullSize = true;
						}
						if (flag3 && (parent.Parent is Matrix || parent.Parent is Table))
						{
							reportItem.IsFullSize = true;
						}
					}
				}
			}
			while (!flag4);
		}

		// Token: 0x06006991 RID: 27025 RVA: 0x0019D494 File Offset: 0x0019B694
		private void ReadReportItems(string propertyName, ReportItem parent, ReportItemCollection parentCollection, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget)
		{
			bool flag;
			this.ReadReportItems(propertyName, parent, parentCollection, context, textBoxesWithDefaultSortTarget, out flag);
		}

		// Token: 0x06006992 RID: 27026 RVA: 0x0019D4B0 File Offset: 0x0019B6B0
		private CustomReportItem ReadCustomReportItem(ReportItem parent, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget)
		{
			CustomReportItem customReportItem = new CustomReportItem(this.GenerateID(), this.GenerateID(), parent);
			customReportItem.Name = this.m_reader.GetAttribute("Name");
			context.ObjectType = customReportItem.ObjectType;
			context.ObjectName = customReportItem.Name;
			this.m_dataRegionCount++;
			this.m_reportItemNames.Validate(context.ObjectType, context.ObjectName, this.m_errorContext);
			bool flag = true;
			if ((context.Location & LocationFlags.InPageSection) != (LocationFlags)0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsCRIInPageSection, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
				flag = false;
			}
			this.m_reportItemCollectionList.Add(customReportItem.AltReportItem);
			this.m_aggregateHolderList.Add(customReportItem);
			this.m_runningValueHolderList.Add(customReportItem);
			this.m_runningValueHolderList.Add(customReportItem.AltReportItem);
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			ExpressionInfo expressionInfo = null;
			ExpressionInfo expressionInfo2 = null;
			TextBoxList textBoxList = new TextBoxList();
			if (!this.m_reader.IsEmptyElement)
			{
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("CustomReportItem" == this.m_reader.LocalName)
							{
								flag8 = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (localName != null)
						{
							switch (localName.Length)
							{
							case 3:
								if (localName == "Top")
								{
									customReportItem.Top = this.ReadSize();
								}
								break;
							case 4:
							{
								char c = localName[0];
								if (c != 'L')
								{
									if (c == 'T')
									{
										if (localName == "Type")
										{
											customReportItem.Type = this.m_reader.ReadString();
										}
									}
								}
								else if (localName == "Left")
								{
									customReportItem.Left = this.ReadSize();
								}
								break;
							}
							case 5:
							{
								char c = localName[0];
								if (c != 'L')
								{
									if (c != 'S')
									{
										if (c == 'W')
										{
											if (localName == "Width")
											{
												customReportItem.Width = this.ReadSize();
											}
										}
									}
									else if (localName == "Style")
									{
										ReportPublishing.StyleInformation styleInformation = this.ReadStyle(context, out flag2);
										customReportItem.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
									}
								}
								else if (localName == "Label")
								{
									expressionInfo = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag6);
									customReportItem.Label = expressionInfo;
								}
								break;
							}
							case 6:
							{
								char c = localName[0];
								if (c != 'H')
								{
									if (c == 'Z')
									{
										if (localName == "ZIndex")
										{
											customReportItem.ZIndex = this.m_reader.ReadInteger();
										}
									}
								}
								else if (localName == "Height")
								{
									customReportItem.Height = this.ReadSize();
								}
								break;
							}
							case 8:
								if (localName == "Bookmark")
								{
									expressionInfo2 = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag7);
									customReportItem.Bookmark = expressionInfo2;
								}
								break;
							case 10:
							{
								char c = localName[0];
								if (c != 'C')
								{
									if (c != 'R')
									{
										if (c == 'V')
										{
											if (localName == "Visibility")
											{
												customReportItem.Visibility = this.ReadVisibility(context, out flag3);
											}
										}
									}
									else if (localName == "RepeatWith")
									{
										customReportItem.RepeatedSibling = true;
										customReportItem.RepeatWith = this.m_reader.ReadString();
									}
								}
								else if (localName == "CustomData")
								{
									this.ReadCustomData(customReportItem, context);
								}
								break;
							}
							case 13:
								if (localName == "AltReportItem")
								{
									this.ReadReportItems("AltReportItem", customReportItem, customReportItem.AltReportItem, context, textBoxList, out flag4);
									Global.Tracer.Assert(1 <= customReportItem.AltReportItem.Count);
								}
								break;
							case 15:
								if (localName == "DataElementName")
								{
									customReportItem.DataElementName = this.m_reader.ReadString();
								}
								break;
							case 16:
								if (localName == "CustomProperties")
								{
									customReportItem.CustomProperties = this.ReadCustomProperties(context, out flag5);
								}
								break;
							case 17:
								if (localName == "DataElementOutput")
								{
									customReportItem.DataElementOutputRDL = this.ReadDataElementOutputRDL();
								}
								break;
							}
						}
					}
				}
				while (!flag8);
			}
			customReportItem.Computed = true;
			if (!flag6 && expressionInfo != null && expressionInfo.Value != null)
			{
				this.m_hasLabels = true;
			}
			if (!flag7 && expressionInfo2 != null && expressionInfo2.Value != null)
			{
				this.m_hasBookmarks = true;
			}
			Global.Tracer.Assert(customReportItem.AltReportItem != null);
			if (customReportItem.AltReportItem.Count == 0)
			{
				Rectangle rectangle = new Rectangle(this.GenerateID(), this.GenerateID(), parent);
				rectangle.Name = string.Concat(new string[]
				{
					customReportItem.Name,
					"_",
					customReportItem.ID.ToString(),
					"_",
					rectangle.ID.ToString()
				});
				this.m_reportItemNames.Validate(rectangle.ObjectType, rectangle.Name, this.m_errorContext);
				rectangle.Computed = false;
				Visibility visibility = new Visibility();
				ExpressionParser.ExpressionContext expressionContext = context.CreateExpressionContext(ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.Boolean, "Hidden", null);
				bool flag9;
				visibility.Hidden = this.m_reportCT.ParseExpression("true", expressionContext, out flag9);
				Global.Tracer.Assert(!flag9);
				rectangle.Visibility = visibility;
				this.m_reportItemCollectionList.Add(rectangle.ReportItems);
				if (parent is Matrix || parent is Table)
				{
					rectangle.IsFullSize = true;
				}
				customReportItem.AltReportItem.AddReportItem(rectangle);
			}
			if (customReportItem.DataSetName != null)
			{
				this.SetSortTargetForTextBoxes(textBoxList, customReportItem);
			}
			else if (textBoxesWithDefaultSortTarget != null)
			{
				textBoxesWithDefaultSortTarget.AddRange(textBoxList);
			}
			if (!flag)
			{
				return null;
			}
			return customReportItem;
		}

		// Token: 0x06006993 RID: 27027 RVA: 0x0019DBA0 File Offset: 0x0019BDA0
		private void ReadCustomData(CustomReportItem crItem, ReportPublishing.PublishingContextStruct context)
		{
			LocationFlags location = context.Location;
			context.Location = context.Location | LocationFlags.InDataSet | LocationFlags.InDataRegion;
			if (this.m_scopeNames.Validate(false, context.ObjectName, context.ObjectType, context.ObjectName, this.m_errorContext))
			{
				this.m_reportScopes.Add(crItem.Name, crItem);
			}
			if ((context.Location & LocationFlags.InPageSection) != (LocationFlags)0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsDataRegionInPageSection, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
			}
			if ((context.Location & LocationFlags.InDetail) != (LocationFlags)0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsDataRegionInTableDetailRow, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
			}
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("CustomData" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "DataSetName"))
					{
						if (!(localName == "DataColumnGroupings"))
						{
							if (!(localName == "DataRowGroupings"))
							{
								if (!(localName == "DataRows"))
								{
									if (localName == "Filters")
									{
										crItem.Filters = this.ReadFilters(ExpressionParser.ExpressionType.DataRegionFilters, context);
									}
								}
								else
								{
									crItem.DataRowCells = this.ReadCustomDataRows(crItem, context);
								}
							}
							else
							{
								crItem.Rows = this.ReadCustomDataColumnOrRowGroupings(false, crItem, context);
							}
						}
						else
						{
							crItem.Columns = this.ReadCustomDataColumnOrRowGroupings(true, crItem, context);
						}
					}
					else
					{
						crItem.DataSetName = this.m_reader.ReadString();
					}
				}
			}
			while (!flag);
		}

		// Token: 0x06006994 RID: 27028 RVA: 0x0019DD50 File Offset: 0x0019BF50
		private CustomReportItemHeadingList ReadCustomDataColumnOrRowGroupings(bool isColumn, CustomReportItem crItem, ReportPublishing.PublishingContextStruct context)
		{
			CustomReportItemHeadingList customReportItemHeadingList = new CustomReportItemHeadingList();
			int num = -1;
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ((isColumn && "DataColumnGroupings" == this.m_reader.LocalName) || (!isColumn && "DataRowGroupings" == this.m_reader.LocalName))
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "DataGroupings")
				{
					num = this.ReadCustomDataGroupings(isColumn, crItem, customReportItemHeadingList, context);
				}
			}
			while (!flag);
			if (isColumn)
			{
				crItem.ExpectedColumns = num;
			}
			else
			{
				crItem.ExpectedRows = num;
			}
			return customReportItemHeadingList;
		}

		// Token: 0x06006995 RID: 27029 RVA: 0x0019DDFC File Offset: 0x0019BFFC
		private int ReadCustomDataGroupings(bool isColumn, CustomReportItem crItem, CustomReportItemHeadingList crGroupingList, ReportPublishing.PublishingContextStruct context)
		{
			bool flag = false;
			int num = 0;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("DataGroupings" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "DataGrouping")
				{
					int num2;
					crGroupingList.Add(this.ReadCustomDataGrouping(isColumn, crItem, context, out num2));
					num += num2;
				}
			}
			while (!flag);
			return num;
		}

		// Token: 0x06006996 RID: 27030 RVA: 0x0019DE80 File Offset: 0x0019C080
		private CustomReportItemHeading ReadCustomDataGrouping(bool isColumn, CustomReportItem crItem, ReportPublishing.PublishingContextStruct context, out int groupingLeafs)
		{
			CustomReportItemHeading customReportItemHeading = new CustomReportItemHeading(this.GenerateID(), crItem);
			this.m_runningValueHolderList.Add(customReportItemHeading);
			customReportItemHeading.IsColumn = isColumn;
			groupingLeafs = 1;
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("DataGrouping" == this.m_reader.LocalName)
							{
								flag = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (!(localName == "Static"))
						{
							if (!(localName == "Grouping"))
							{
								if (!(localName == "Sorting"))
								{
									if (!(localName == "Subtotal"))
									{
										if (!(localName == "CustomProperties"))
										{
											if (localName == "DataGroupings")
											{
												customReportItemHeading.InnerHeadings = new CustomReportItemHeadingList();
												groupingLeafs = this.ReadCustomDataGroupings(isColumn, crItem, customReportItemHeading.InnerHeadings, context);
												customReportItemHeading.HeadingSpan = groupingLeafs;
											}
										}
										else
										{
											customReportItemHeading.CustomProperties = this.ReadCustomProperties(context);
										}
									}
									else
									{
										customReportItemHeading.Subtotal = this.m_reader.ReadBoolean();
									}
								}
								else
								{
									customReportItemHeading.Sorting = this.ReadSorting(context);
								}
							}
							else
							{
								customReportItemHeading.Grouping = this.ReadGrouping(context);
							}
						}
						else
						{
							customReportItemHeading.Static = this.m_reader.ReadBoolean();
						}
					}
				}
				while (!flag);
			}
			if (this.CanMergeGroupingAndSorting(customReportItemHeading.Grouping, customReportItemHeading.Sorting))
			{
				customReportItemHeading.Grouping.GroupAndSort = true;
				customReportItemHeading.Grouping.SortDirections = customReportItemHeading.Sorting.SortDirections;
				customReportItemHeading.Sorting = null;
			}
			if (customReportItemHeading.Sorting != null)
			{
				this.m_hasSorting = true;
			}
			return customReportItemHeading;
		}

		// Token: 0x06006997 RID: 27031 RVA: 0x0019E038 File Offset: 0x0019C238
		private DataCellsList ReadCustomDataRows(CustomReportItem crItem, ReportPublishing.PublishingContextStruct context)
		{
			DataCellsList dataCellsList = new DataCellsList();
			bool flag = false;
			int num = 0;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("DataRows" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "DataRow")
				{
					dataCellsList.Add(this.ReadCustomDataRow(crItem, num++, context));
				}
			}
			while (!flag);
			return dataCellsList;
		}

		// Token: 0x06006998 RID: 27032 RVA: 0x0019E0BC File Offset: 0x0019C2BC
		private DataCellList ReadCustomDataRow(CustomReportItem crItem, int rowIndex, ReportPublishing.PublishingContextStruct context)
		{
			DataCellList dataCellList = new DataCellList();
			bool flag = false;
			int num = 0;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("DataRow" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "DataCell")
				{
					dataCellList.Add(this.ReadCustomDataCell(crItem, rowIndex, num++, context));
				}
			}
			while (!flag);
			return dataCellList;
		}

		// Token: 0x06006999 RID: 27033 RVA: 0x0019E144 File Offset: 0x0019C344
		private DataValueCRIList ReadCustomDataCell(CustomReportItem crItem, int rowIndex, int columnIndex, ReportPublishing.PublishingContextStruct context)
		{
			DataValueCRIList dataValueCRIList = new DataValueCRIList();
			dataValueCRIList.RDLRowIndex = rowIndex;
			dataValueCRIList.RDLColumnIndex = columnIndex;
			int num = 0;
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("DataCell" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "DataValue")
				{
					dataValueCRIList.Add(this.ReadDataValue(false, ++num, context));
				}
			}
			while (!flag);
			return dataValueCRIList;
		}

		// Token: 0x0600699A RID: 27034 RVA: 0x0019E1D8 File Offset: 0x0019C3D8
		private Line ReadLine(ReportItem parent, ReportPublishing.PublishingContextStruct context)
		{
			Line line = new Line(this.GenerateID(), parent);
			line.Name = this.m_reader.GetAttribute("Name");
			context.ObjectType = line.ObjectType;
			context.ObjectName = line.Name;
			this.m_reportItemNames.Validate(context.ObjectType, context.ObjectName, this.m_errorContext);
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			ExpressionInfo expressionInfo = null;
			ExpressionInfo expressionInfo2 = null;
			if (!this.m_reader.IsEmptyElement)
			{
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("Line" == this.m_reader.LocalName)
							{
								flag6 = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (localName != null)
						{
							switch (localName.Length)
							{
							case 3:
								if (localName == "Top")
								{
									line.Top = this.ReadSize();
								}
								break;
							case 4:
								if (localName == "Left")
								{
									line.Left = this.ReadSize();
								}
								break;
							case 5:
							{
								char c = localName[0];
								if (c != 'L')
								{
									if (c != 'S')
									{
										if (c == 'W')
										{
											if (localName == "Width")
											{
												line.Width = this.ReadSize();
											}
										}
									}
									else if (localName == "Style")
									{
										ReportPublishing.StyleInformation styleInformation = this.ReadStyle(context, out flag);
										styleInformation.Filter(ReportPublishing.StyleOwnerType.Line, false);
										line.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
									}
								}
								else if (localName == "Label")
								{
									expressionInfo = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag3);
									line.Label = expressionInfo;
								}
								break;
							}
							case 6:
							{
								char c = localName[0];
								if (c != 'C')
								{
									if (c != 'H')
									{
										if (c == 'Z')
										{
											if (localName == "ZIndex")
											{
												line.ZIndex = this.m_reader.ReadInteger();
											}
										}
									}
									else if (localName == "Height")
									{
										line.Height = this.ReadSize();
									}
								}
								else if (localName == "Custom")
								{
									line.Custom = this.m_reader.ReadCustomXml();
								}
								break;
							}
							case 8:
								if (localName == "Bookmark")
								{
									expressionInfo2 = this.ReadBookmarkExpression(context, out flag4);
									line.Bookmark = expressionInfo2;
								}
								break;
							case 10:
							{
								char c = localName[0];
								if (c != 'R')
								{
									if (c == 'V')
									{
										if (localName == "Visibility")
										{
											line.Visibility = this.ReadVisibility(context, out flag2);
										}
									}
								}
								else if (localName == "RepeatWith")
								{
									line.RepeatedSibling = true;
									line.RepeatWith = this.m_reader.ReadString();
								}
								break;
							}
							case 15:
								if (localName == "DataElementName")
								{
									line.DataElementName = this.m_reader.ReadString();
								}
								break;
							case 16:
								if (localName == "CustomProperties")
								{
									line.CustomProperties = this.ReadCustomProperties(context, out flag5);
								}
								break;
							case 17:
								if (localName == "DataElementOutput")
								{
									line.DataElementOutputRDL = this.ReadDataElementOutputRDL();
								}
								break;
							}
						}
					}
				}
				while (!flag6);
			}
			line.Computed = flag || flag2 || flag3 || flag4 || flag5;
			if (!flag3 && expressionInfo != null && expressionInfo.Value != null)
			{
				this.m_hasLabels = true;
			}
			if (!flag4 && expressionInfo2 != null && expressionInfo2.Value != null)
			{
				this.m_hasBookmarks = true;
			}
			return line;
		}

		// Token: 0x0600699B RID: 27035 RVA: 0x0019E64C File Offset: 0x0019C84C
		private Rectangle ReadRectangle(ReportItem parent, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget)
		{
			Rectangle rectangle = new Rectangle(this.GenerateID(), this.GenerateID(), parent);
			rectangle.Name = this.m_reader.GetAttribute("Name");
			context.ObjectType = rectangle.ObjectType;
			context.ObjectName = rectangle.Name;
			this.m_reportItemNames.Validate(context.ObjectType, context.ObjectName, this.m_errorContext);
			this.m_reportItemCollectionList.Add(rectangle.ReportItems);
			this.m_runningValueHolderList.Add(rectangle.ReportItems);
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			string text = null;
			ExpressionInfo expressionInfo = null;
			ExpressionInfo expressionInfo2 = null;
			if (!this.m_reader.IsEmptyElement)
			{
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("Rectangle" == this.m_reader.LocalName)
							{
								flag8 = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (localName != null)
						{
							switch (localName.Length)
							{
							case 3:
								if (localName == "Top")
								{
									rectangle.Top = this.ReadSize();
								}
								break;
							case 4:
								if (localName == "Left")
								{
									rectangle.Left = this.ReadSize();
								}
								break;
							case 5:
							{
								char c = localName[0];
								if (c != 'L')
								{
									if (c != 'S')
									{
										if (c == 'W')
										{
											if (localName == "Width")
											{
												rectangle.Width = this.ReadSize();
											}
										}
									}
									else if (localName == "Style")
									{
										ReportPublishing.StyleInformation styleInformation = this.ReadStyle(context, out flag);
										styleInformation.Filter(ReportPublishing.StyleOwnerType.Rectangle, false);
										rectangle.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
									}
								}
								else if (localName == "Label")
								{
									expressionInfo = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag3);
									rectangle.Label = expressionInfo;
								}
								break;
							}
							case 6:
							{
								char c = localName[0];
								if (c != 'C')
								{
									if (c != 'H')
									{
										if (c == 'Z')
										{
											if (localName == "ZIndex")
											{
												rectangle.ZIndex = this.m_reader.ReadInteger();
											}
										}
									}
									else if (localName == "Height")
									{
										rectangle.Height = this.ReadSize();
									}
								}
								else if (localName == "Custom")
								{
									rectangle.Custom = this.m_reader.ReadCustomXml();
								}
								break;
							}
							case 7:
								if (localName == "ToolTip")
								{
									rectangle.ToolTip = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag5);
								}
								break;
							case 8:
								if (localName == "Bookmark")
								{
									expressionInfo2 = this.ReadBookmarkExpression(context, out flag4);
									rectangle.Bookmark = expressionInfo2;
								}
								break;
							case 10:
							{
								char c = localName[0];
								if (c != 'R')
								{
									if (c == 'V')
									{
										if (localName == "Visibility")
										{
											rectangle.Visibility = this.ReadVisibility(context, out flag2);
										}
									}
								}
								else if (localName == "RepeatWith")
								{
									rectangle.RepeatedSibling = true;
									rectangle.RepeatWith = this.m_reader.ReadString();
								}
								break;
							}
							case 11:
							{
								char c = localName[0];
								if (c != 'L')
								{
									if (c == 'R')
									{
										if (localName == "ReportItems")
										{
											this.ReadReportItems(null, rectangle, rectangle.ReportItems, context, textBoxesWithDefaultSortTarget, out flag6);
										}
									}
								}
								else if (localName == "LinkToChild")
								{
									text = this.m_reader.ReadString();
								}
								break;
							}
							case 14:
								if (localName == "PageBreakAtEnd")
								{
									rectangle.PageBreakAtEnd = this.m_reader.ReadBoolean();
								}
								break;
							case 15:
								if (localName == "DataElementName")
								{
									rectangle.DataElementName = this.m_reader.ReadString();
								}
								break;
							case 16:
							{
								char c = localName[0];
								if (c != 'C')
								{
									if (c == 'P')
									{
										if (localName == "PageBreakAtStart")
										{
											rectangle.PageBreakAtStart = this.m_reader.ReadBoolean();
										}
									}
								}
								else if (localName == "CustomProperties")
								{
									rectangle.CustomProperties = this.ReadCustomProperties(context, out flag7);
								}
								break;
							}
							case 17:
								if (localName == "DataElementOutput")
								{
									rectangle.DataElementOutputRDL = this.ReadDataElementOutputRDL();
								}
								break;
							}
						}
					}
				}
				while (!flag8);
			}
			rectangle.Computed = (flag || flag2 || flag3 || flag4 || flag6 || flag5 || flag7) | rectangle.PageBreakAtStart | rectangle.PageBreakAtEnd;
			if (!flag3 && expressionInfo != null && expressionInfo.Value != null)
			{
				this.m_hasLabels = true;
			}
			if (!flag4 && expressionInfo2 != null && expressionInfo2.Value != null)
			{
				this.m_hasBookmarks = true;
			}
			if (expressionInfo != null && text != null)
			{
				rectangle.ReportItems.LinkToChild = text;
			}
			return rectangle;
		}

		// Token: 0x0600699C RID: 27036 RVA: 0x0019EC48 File Offset: 0x0019CE48
		private CheckBox ReadCheckbox(ReportItem parent, ReportPublishing.PublishingContextStruct context)
		{
			CheckBox checkBox = new CheckBox(this.GenerateID(), parent);
			checkBox.Name = this.m_reader.GetAttribute("Name");
			context.ObjectType = checkBox.ObjectType;
			context.ObjectName = checkBox.Name;
			this.m_reportItemNames.Validate(context.ObjectType, context.ObjectName, this.m_errorContext);
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			ExpressionInfo expressionInfo = null;
			ExpressionInfo expressionInfo2 = null;
			if (!this.m_reader.IsEmptyElement)
			{
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("Checkbox" == this.m_reader.LocalName)
							{
								flag8 = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (localName != null)
						{
							switch (localName.Length)
							{
							case 3:
								if (localName == "Top")
								{
									checkBox.Top = this.ReadSize();
								}
								break;
							case 4:
								if (localName == "Left")
								{
									checkBox.Left = this.ReadSize();
								}
								break;
							case 5:
							{
								char c = localName[0];
								if (c != 'L')
								{
									if (c != 'S')
									{
										if (c == 'V')
										{
											if (localName == "Value")
											{
												checkBox.Value = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.Boolean, context, out flag6);
											}
										}
									}
									else if (localName == "Style")
									{
										ReportPublishing.StyleInformation styleInformation = this.ReadStyle(context, out flag);
										styleInformation.Filter(ReportPublishing.StyleOwnerType.Checkbox, false);
										checkBox.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
									}
								}
								else if (localName == "Label")
								{
									expressionInfo = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag3);
									checkBox.Label = expressionInfo;
								}
								break;
							}
							case 6:
							{
								char c = localName[0];
								if (c != 'C')
								{
									if (c == 'Z')
									{
										if (localName == "ZIndex")
										{
											checkBox.ZIndex = this.m_reader.ReadInteger();
										}
									}
								}
								else if (localName == "Custom")
								{
									checkBox.Custom = this.m_reader.ReadCustomXml();
								}
								break;
							}
							case 7:
								if (localName == "ToolTip")
								{
									checkBox.ToolTip = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag5);
								}
								break;
							case 8:
								if (localName == "Bookmark")
								{
									expressionInfo2 = this.ReadBookmarkExpression(context, out flag4);
									checkBox.Bookmark = expressionInfo2;
								}
								break;
							case 10:
							{
								char c = localName[0];
								if (c != 'R')
								{
									if (c == 'V')
									{
										if (localName == "Visibility")
										{
											checkBox.Visibility = this.ReadVisibility(context, out flag2);
										}
									}
								}
								else if (localName == "RepeatWith")
								{
									checkBox.RepeatedSibling = true;
									checkBox.RepeatWith = this.m_reader.ReadString();
								}
								break;
							}
							case 14:
								if (localName == "HideDuplicates")
								{
									string text = this.m_reader.ReadString();
									if ((context.Location & LocationFlags.InPageSection) != (LocationFlags)0 || text == null || text.Length <= 0)
									{
										checkBox.HideDuplicates = null;
									}
									else
									{
										checkBox.HideDuplicates = text;
									}
								}
								break;
							case 15:
								if (localName == "DataElementName")
								{
									checkBox.DataElementName = this.m_reader.ReadString();
								}
								break;
							case 16:
								if (localName == "CustomProperties")
								{
									checkBox.CustomProperties = this.ReadCustomProperties(context, out flag7);
								}
								break;
							case 17:
								if (localName == "DataElementOutput")
								{
									checkBox.DataElementOutputRDL = this.ReadDataElementOutputRDL();
								}
								break;
							}
						}
					}
				}
				while (!flag8);
			}
			checkBox.Computed = (flag || flag2 || flag3 || flag5 || flag4 || flag6 || flag7) | (checkBox.HideDuplicates != null);
			if (!flag3 && expressionInfo != null && expressionInfo.Value != null)
			{
				this.m_hasLabels = true;
			}
			if (!flag4 && expressionInfo2 != null && expressionInfo2.Value != null)
			{
				this.m_hasBookmarks = true;
			}
			return checkBox;
		}

		// Token: 0x0600699D RID: 27037 RVA: 0x0019F140 File Offset: 0x0019D340
		private TextBox ReadTextbox(ReportItem parent, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget)
		{
			TextBox textBox = new TextBox(this.GenerateID(), parent);
			textBox.Name = this.m_reader.GetAttribute("Name");
			context.ObjectType = textBox.ObjectType;
			context.ObjectName = textBox.Name;
			this.m_reportItemNames.Validate(context.ObjectType, context.ObjectName, this.m_errorContext);
			Global.Tracer.Assert(!this.m_reportCT.ValueReferenced);
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			ExpressionInfo expressionInfo = null;
			ExpressionInfo expressionInfo2 = null;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Textbox" == this.m_reader.LocalName)
						{
							flag10 = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (localName != null)
					{
						switch (localName.Length)
						{
						case 3:
							if (localName == "Top")
							{
								textBox.Top = this.ReadSize();
							}
							break;
						case 4:
							if (localName == "Left")
							{
								textBox.Left = this.ReadSize();
							}
							break;
						case 5:
						{
							char c = localName[0];
							if (c != 'L')
							{
								switch (c)
								{
								case 'S':
									if (localName == "Style")
									{
										ReportPublishing.StyleInformation styleInformation = this.ReadStyle(context, out flag);
										styleInformation.Filter(ReportPublishing.StyleOwnerType.Textbox, false);
										textBox.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
									}
									break;
								case 'V':
									if (localName == "Value")
									{
										ExpressionInfo expressionInfo3 = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag7);
										if (expressionInfo3 != null)
										{
											textBox.Value = expressionInfo3;
											if (expressionInfo3.Type != ExpressionInfo.Types.Constant)
											{
												textBox.Formula = textBox.Value.OriginalText;
											}
										}
									}
									break;
								case 'W':
									if (localName == "Width")
									{
										textBox.Width = this.ReadSize();
									}
									break;
								}
							}
							else if (localName == "Label")
							{
								expressionInfo = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag4);
								textBox.Label = expressionInfo;
							}
							break;
						}
						case 6:
						{
							char c = localName[0];
							if (c <= 'C')
							{
								if (c != 'A')
								{
									if (c == 'C')
									{
										if (localName == "Custom")
										{
											textBox.Custom = this.m_reader.ReadCustomXml();
										}
									}
								}
								else if (localName == "Action")
								{
									int num = -1;
									bool flag11 = false;
									ActionItem actionItem = this.ReadActionItem(context, out flag2, ref num, ref flag11);
									textBox.Action = new Microsoft.ReportingServices.ReportProcessing.Action(actionItem, flag2);
								}
							}
							else if (c != 'H')
							{
								if (c == 'Z')
								{
									if (localName == "ZIndex")
									{
										textBox.ZIndex = this.m_reader.ReadInteger();
									}
								}
							}
							else if (localName == "Height")
							{
								textBox.Height = this.ReadSize();
							}
							break;
						}
						case 7:
						{
							char c = localName[0];
							if (c != 'C')
							{
								if (c == 'T')
								{
									if (localName == "ToolTip")
									{
										textBox.ToolTip = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag6);
									}
								}
							}
							else if (localName == "CanGrow")
							{
								textBox.CanGrow = this.m_reader.ReadBoolean();
							}
							break;
						}
						case 8:
						{
							char c = localName[0];
							if (c != 'B')
							{
								if (c == 'U')
								{
									if (localName == "UserSort")
									{
										this.ReadUserSort(context, textBox, textBoxesWithDefaultSortTarget);
										this.m_hasUserSort = true;
									}
								}
							}
							else if (localName == "Bookmark")
							{
								expressionInfo2 = this.ReadBookmarkExpression(context, out flag5);
								textBox.Bookmark = expressionInfo2;
							}
							break;
						}
						case 9:
							if (localName == "CanShrink")
							{
								textBox.CanShrink = this.m_reader.ReadBoolean();
							}
							break;
						case 10:
						{
							char c = localName[0];
							if (c != 'A')
							{
								if (c != 'R')
								{
									if (c == 'V')
									{
										if (localName == "Visibility")
										{
											textBox.Visibility = this.ReadVisibility(context, out flag3);
										}
									}
								}
								else if (localName == "RepeatWith")
								{
									textBox.RepeatedSibling = true;
									textBox.RepeatWith = this.m_reader.ReadString();
								}
							}
							else if (localName == "ActionInfo")
							{
								textBox.Action = this.ReadAction(context, ReportPublishing.StyleOwnerType.Textbox, out flag2);
							}
							break;
						}
						case 11:
							if (localName == "ToggleImage")
							{
								textBox.InitialToggleState = this.ReadToggleImage(context, out flag8);
							}
							break;
						case 14:
							if (localName == "HideDuplicates")
							{
								string text = this.m_reader.ReadString();
								if ((context.Location & LocationFlags.InPageSection) != (LocationFlags)0 || text == null || text.Length <= 0)
								{
									textBox.HideDuplicates = null;
								}
								else
								{
									textBox.HideDuplicates = text;
								}
							}
							break;
						case 15:
							if (localName == "DataElementName")
							{
								textBox.DataElementName = this.m_reader.ReadString();
							}
							break;
						case 16:
						{
							char c = localName[0];
							if (c != 'C')
							{
								if (c == 'D')
								{
									if (localName == "DataElementStyle")
									{
										ReportItem.DataElementStylesRDL dataElementStylesRDL = this.ReadDataElementStyleRDL();
										if (ReportItem.DataElementStylesRDL.Auto != dataElementStylesRDL)
										{
											textBox.OverrideReportDataElementStyle = true;
											Global.Tracer.Assert(dataElementStylesRDL == ReportItem.DataElementStylesRDL.AttributeNormal || ReportItem.DataElementStylesRDL.ElementNormal == dataElementStylesRDL);
											textBox.DataElementStyleAttribute = dataElementStylesRDL == ReportItem.DataElementStylesRDL.AttributeNormal;
										}
									}
								}
							}
							else if (localName == "CustomProperties")
							{
								textBox.CustomProperties = this.ReadCustomProperties(context, out flag9);
							}
							break;
						}
						case 17:
							if (localName == "DataElementOutput")
							{
								textBox.DataElementOutputRDL = this.ReadDataElementOutputRDL();
							}
							break;
						}
					}
				}
			}
			while (!flag10);
			textBox.Computed = (flag || flag2 || flag3 || flag9 || flag4 || flag5 || flag6 || flag7 || flag8) | (textBox.UserSort != null) | (textBox.HideDuplicates != null);
			textBox.ValueReferenced = this.m_reportCT.ValueReferenced;
			this.m_reportCT.ResetValueReferencedFlag();
			if (!flag4 && expressionInfo != null && expressionInfo.Value != null)
			{
				this.m_hasLabels = true;
			}
			if (!flag5 && expressionInfo2 != null && expressionInfo2.Value != null)
			{
				this.m_hasBookmarks = true;
			}
			return textBox;
		}

		// Token: 0x0600699E RID: 27038 RVA: 0x0019F8F4 File Offset: 0x0019DAF4
		private void ReadUserSort(ReportPublishing.PublishingContextStruct context, TextBox textbox, TextBoxList textBoxesWithDefaultSortTarget)
		{
			bool flag = (context.Location & LocationFlags.InPageSection) > (LocationFlags)0;
			bool flag2 = false;
			EndUserSort endUserSort = new EndUserSort();
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("UserSort" == this.m_reader.LocalName)
						{
							flag2 = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "SortExpression"))
					{
						if (!(localName == "SortExpressionScope"))
						{
							if (localName == "SortTarget")
							{
								this.m_hasUserSortPeerScopes = true;
								endUserSort.SortTargetString = this.m_reader.ReadString();
							}
						}
						else
						{
							endUserSort.SortExpressionScopeString = this.m_reader.ReadString();
						}
					}
					else
					{
						bool flag3;
						endUserSort.SortExpression = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.SortExpression, ExpressionParser.ConstantType.String, context, out flag3);
					}
				}
			}
			while (!flag2);
			if (flag)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidTextboxInPageSection, Severity.Error, textbox.ObjectType, textbox.Name, "UserSort", Array.Empty<string>());
				return;
			}
			textbox.UserSort = endUserSort;
			if (endUserSort.SortTargetString == null)
			{
				if (textBoxesWithDefaultSortTarget != null)
				{
					textBoxesWithDefaultSortTarget.Add(textbox);
					return;
				}
			}
			else
			{
				this.m_textBoxesWithUserSortTarget.Add(textbox);
			}
		}

		// Token: 0x0600699F RID: 27039 RVA: 0x0019FA38 File Offset: 0x0019DC38
		private void SetSortTargetForTextBoxes(TextBoxList textBoxes, ISortFilterScope target)
		{
			if (textBoxes != null)
			{
				for (int i = 0; i < textBoxes.Count; i++)
				{
					textBoxes[i].UserSort.SetSortTarget(target);
				}
			}
		}

		// Token: 0x060069A0 RID: 27040 RVA: 0x0019FA6C File Offset: 0x0019DC6C
		private ExpressionInfo ReadToggleImage(ReportPublishing.PublishingContextStruct context, out bool computed)
		{
			computed = false;
			this.m_static = true;
			ExpressionInfo expressionInfo = null;
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("ToggleImage" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "InitialState")
				{
					expressionInfo = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.Boolean, context, out computed);
				}
			}
			while (!flag);
			return expressionInfo;
		}

		// Token: 0x060069A1 RID: 27041 RVA: 0x0019FAF8 File Offset: 0x0019DCF8
		private Image ReadImage(ReportItem parent, ReportPublishing.PublishingContextStruct context)
		{
			Image image = new Image(this.GenerateID(), parent);
			image.Name = this.m_reader.GetAttribute("Name");
			context.ObjectType = image.ObjectType;
			context.ObjectName = image.Name;
			this.m_reportItemNames.Validate(context.ObjectType, context.ObjectName, this.m_errorContext);
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			ExpressionInfo expressionInfo = null;
			ExpressionInfo expressionInfo2 = null;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Image" == this.m_reader.LocalName)
						{
							flag10 = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (localName != null)
					{
						switch (localName.Length)
						{
						case 3:
							if (localName == "Top")
							{
								image.Top = this.ReadSize();
							}
							break;
						case 4:
							if (localName == "Left")
							{
								image.Left = this.ReadSize();
							}
							break;
						case 5:
						{
							char c = localName[0];
							if (c != 'L')
							{
								switch (c)
								{
								case 'S':
									if (localName == "Style")
									{
										ReportPublishing.StyleInformation styleInformation = this.ReadStyle(context, out flag);
										styleInformation.Filter(ReportPublishing.StyleOwnerType.Image, false);
										image.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
									}
									break;
								case 'V':
									if (localName == "Value")
									{
										image.Value = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag7);
									}
									break;
								case 'W':
									if (localName == "Width")
									{
										image.Width = this.ReadSize();
									}
									break;
								}
							}
							else if (localName == "Label")
							{
								expressionInfo = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag4);
								image.Label = expressionInfo;
							}
							break;
						}
						case 6:
						{
							char c = localName[1];
							if (c <= 'e')
							{
								if (c != 'I')
								{
									if (c != 'c')
									{
										if (c == 'e')
										{
											if (localName == "Height")
											{
												image.Height = this.ReadSize();
											}
										}
									}
									else if (localName == "Action")
									{
										int num = -1;
										bool flag11 = false;
										ActionItem actionItem = this.ReadActionItem(context, out flag2, ref num, ref flag11);
										image.Action = new Microsoft.ReportingServices.ReportProcessing.Action(actionItem, flag2);
									}
								}
								else if (localName == "ZIndex")
								{
									image.ZIndex = this.m_reader.ReadInteger();
								}
							}
							else if (c != 'i')
							{
								if (c != 'o')
								{
									if (c == 'u')
									{
										if (localName == "Custom")
										{
											image.Custom = this.m_reader.ReadCustomXml();
										}
									}
								}
								else if (localName == "Source")
								{
									image.Source = this.ReadSource();
								}
							}
							else if (localName == "Sizing")
							{
								image.Sizing = this.ReadSizing();
							}
							break;
						}
						case 7:
							if (localName == "ToolTip")
							{
								image.ToolTip = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag6);
							}
							break;
						case 8:
						{
							char c = localName[0];
							if (c != 'B')
							{
								if (c == 'M')
								{
									if (localName == "MIMEType")
									{
										image.MIMEType = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag8);
									}
								}
							}
							else if (localName == "Bookmark")
							{
								expressionInfo2 = this.ReadBookmarkExpression(context, out flag5);
								image.Bookmark = expressionInfo2;
							}
							break;
						}
						case 10:
						{
							char c = localName[0];
							if (c != 'A')
							{
								if (c != 'R')
								{
									if (c == 'V')
									{
										if (localName == "Visibility")
										{
											image.Visibility = this.ReadVisibility(context, out flag3);
										}
									}
								}
								else if (localName == "RepeatWith")
								{
									image.RepeatedSibling = true;
									image.RepeatWith = this.m_reader.ReadString();
								}
							}
							else if (localName == "ActionInfo")
							{
								image.Action = this.ReadAction(context, ReportPublishing.StyleOwnerType.Image, out flag2);
							}
							break;
						}
						case 15:
							if (localName == "DataElementName")
							{
								image.DataElementName = this.m_reader.ReadString();
							}
							break;
						case 16:
							if (localName == "CustomProperties")
							{
								image.CustomProperties = this.ReadCustomProperties(context, out flag9);
							}
							break;
						case 17:
							if (localName == "DataElementOutput")
							{
								image.DataElementOutputRDL = this.ReadDataElementOutputRDL();
							}
							break;
						}
					}
				}
			}
			while (!flag10);
			if (Image.SourceType.Database == image.Source)
			{
				Global.Tracer.Assert(image.Value != null);
				if (ExpressionInfo.Types.Constant == image.Value.Type)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsBinaryConstant, Severity.Error, context.ObjectType, context.ObjectName, "Value", Array.Empty<string>());
				}
				if (!PublishingValidator.ValidateMimeType(image.MIMEType, context.ObjectType, context.ObjectName, "MIMEType", this.m_errorContext))
				{
					image.MIMEType = null;
				}
			}
			else
			{
				if (image.Source == Image.SourceType.External && ExpressionInfo.Types.Constant == image.Value.Type && image.Value.Value != null && image.Value.Value.Trim().Length == 0)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidEmptyImageReference, Severity.Error, context.ObjectType, context.ObjectName, "Value", Array.Empty<string>());
				}
				image.MIMEType = null;
				if (image.Source == Image.SourceType.External && !flag7)
				{
					this.m_imageStreamNames[image.Value.Value] = new ImageInfo(image.Name, null);
				}
			}
			image.Computed = flag || flag2 || flag3 || flag9 || flag4 || flag5 || flag6 || flag7 || flag8;
			this.m_hasImageStreams = true;
			if (!flag4 && expressionInfo != null && expressionInfo.Value != null)
			{
				this.m_hasLabels = true;
			}
			if (!flag5 && expressionInfo2 != null && expressionInfo2.Value != null)
			{
				this.m_hasBookmarks = true;
			}
			if (image.Source == Image.SourceType.External)
			{
				this.m_hasExternalImages = true;
			}
			return image;
		}

		// Token: 0x060069A2 RID: 27042 RVA: 0x001A026C File Offset: 0x0019E46C
		private SubReport ReadSubreport(ReportItem parent, ReportPublishing.PublishingContextStruct context)
		{
			SubReport subReport = new SubReport(this.GenerateID(), parent);
			subReport.Name = this.m_reader.GetAttribute("Name");
			context.ObjectType = subReport.ObjectType;
			context.ObjectName = subReport.Name;
			this.m_reportItemNames.Validate(context.ObjectType, context.ObjectName, this.m_errorContext);
			bool flag = true;
			if ((context.Location & LocationFlags.InPageSection) != (LocationFlags)0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsDataRegionInPageSection, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
				flag = false;
			}
			bool flag2 = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Subreport" == this.m_reader.LocalName)
						{
							flag2 = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (localName != null)
					{
						switch (localName.Length)
						{
						case 3:
							if (localName == "Top")
							{
								subReport.Top = this.ReadSize();
							}
							break;
						case 4:
							if (localName == "Left")
							{
								subReport.Left = this.ReadSize();
							}
							break;
						case 5:
						{
							char c = localName[0];
							if (c != 'L')
							{
								if (c != 'S')
								{
									if (c == 'W')
									{
										if (localName == "Width")
										{
											subReport.Width = this.ReadSize();
										}
									}
								}
								else if (localName == "Style")
								{
									ReportPublishing.StyleInformation styleInformation = this.ReadStyle(context);
									styleInformation.Filter(ReportPublishing.StyleOwnerType.SubReport, false);
									subReport.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
								}
							}
							else if (localName == "Label")
							{
								subReport.Label = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
							}
							break;
						}
						case 6:
						{
							char c = localName[0];
							if (c <= 'H')
							{
								if (c != 'C')
								{
									if (c == 'H')
									{
										if (localName == "Height")
										{
											subReport.Height = this.ReadSize();
										}
									}
								}
								else if (localName == "Custom")
								{
									subReport.Custom = this.m_reader.ReadCustomXml();
								}
							}
							else if (c != 'N')
							{
								if (c == 'Z')
								{
									if (localName == "ZIndex")
									{
										subReport.ZIndex = this.m_reader.ReadInteger();
									}
								}
							}
							else if (localName == "NoRows")
							{
								subReport.NoRows = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
							}
							break;
						}
						case 7:
							if (localName == "ToolTip")
							{
								subReport.ToolTip = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
							}
							break;
						case 8:
							if (localName == "Bookmark")
							{
								subReport.Bookmark = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
							}
							break;
						case 10:
						{
							char c = localName[3];
							if (c <= 'e')
							{
								if (c != 'a')
								{
									if (c == 'e')
									{
										if (localName == "RepeatWith")
										{
											subReport.RepeatedSibling = true;
											subReport.RepeatWith = this.m_reader.ReadString();
										}
									}
								}
								else if (localName == "Parameters")
								{
									subReport.Parameters = this.ReadParameters(context, true);
								}
							}
							else if (c != 'i')
							{
								if (c == 'o')
								{
									if (localName == "ReportName")
									{
										subReport.ReportPath = PublishingValidator.ValidateReportName(this.m_reportContext, this.m_reader.ReadString(), context.ObjectType, context.ObjectName, "ReportName", this.m_errorContext);
									}
								}
							}
							else if (localName == "Visibility")
							{
								subReport.Visibility = this.ReadVisibility(context);
							}
							break;
						}
						case 15:
							if (localName == "DataElementName")
							{
								subReport.DataElementName = this.m_reader.ReadString();
							}
							break;
						case 16:
							if (localName == "CustomProperties")
							{
								subReport.CustomProperties = this.ReadCustomProperties(context);
							}
							break;
						case 17:
						{
							char c = localName[0];
							if (c != 'D')
							{
								if (c == 'M')
								{
									if (localName == "MergeTransactions")
									{
										subReport.MergeTransactions = this.m_reader.ReadBoolean();
										if (subReport.MergeTransactions)
										{
											this.m_subReportMergeTransactions = true;
										}
									}
								}
							}
							else if (localName == "DataElementOutput")
							{
								subReport.DataElementOutputRDL = this.ReadDataElementOutputRDL();
							}
							break;
						}
						}
					}
				}
			}
			while (!flag2);
			subReport.Computed = true;
			if (flag)
			{
				this.m_subReports.Add(subReport);
				this.m_parametersNotUsedInQuery = false;
				return subReport;
			}
			return null;
		}

		// Token: 0x060069A3 RID: 27043 RVA: 0x001A0840 File Offset: 0x0019EA40
		private ActiveXControl ReadActiveXControl(ReportItem parent, ReportPublishing.PublishingContextStruct context)
		{
			ActiveXControl activeXControl = new ActiveXControl(this.GenerateID(), parent);
			activeXControl.Name = this.m_reader.GetAttribute("Name");
			context.ObjectType = activeXControl.ObjectType;
			context.ObjectName = activeXControl.Name;
			this.m_reportItemNames.Validate(context.ObjectType, context.ObjectName, this.m_errorContext);
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			ExpressionInfo expressionInfo = null;
			ExpressionInfo expressionInfo2 = null;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("ActiveXControl" == this.m_reader.LocalName)
						{
							flag8 = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (localName != null)
					{
						switch (localName.Length)
						{
						case 3:
							if (localName == "Top")
							{
								activeXControl.Top = this.ReadSize();
							}
							break;
						case 4:
							if (localName == "Left")
							{
								activeXControl.Left = this.ReadSize();
							}
							break;
						case 5:
						{
							char c = localName[0];
							if (c != 'L')
							{
								if (c != 'S')
								{
									if (c == 'W')
									{
										if (localName == "Width")
										{
											activeXControl.Width = this.ReadSize();
										}
									}
								}
								else if (localName == "Style")
								{
									ReportPublishing.StyleInformation styleInformation = this.ReadStyle(context, out flag);
									styleInformation.Filter(ReportPublishing.StyleOwnerType.ActiveXControl, false);
									activeXControl.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
								}
							}
							else if (localName == "Label")
							{
								expressionInfo = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag3);
								activeXControl.Label = expressionInfo;
							}
							break;
						}
						case 6:
						{
							char c = localName[0];
							if (c != 'C')
							{
								if (c != 'H')
								{
									if (c == 'Z')
									{
										if (localName == "ZIndex")
										{
											activeXControl.ZIndex = this.m_reader.ReadInteger();
										}
									}
								}
								else if (localName == "Height")
								{
									activeXControl.Height = this.ReadSize();
								}
							}
							else if (localName == "Custom")
							{
								activeXControl.Custom = this.m_reader.ReadCustomXml();
							}
							break;
						}
						case 7:
						{
							char c = localName[0];
							if (c != 'C')
							{
								if (c == 'T')
								{
									if (localName == "ToolTip")
									{
										activeXControl.ToolTip = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag5);
									}
								}
							}
							else if (localName == "ClassID")
							{
								activeXControl.ClassID = this.m_reader.ReadString();
							}
							break;
						}
						case 8:
						{
							char c = localName[0];
							if (c != 'B')
							{
								if (c == 'C')
								{
									if (localName == "CodeBase")
									{
										activeXControl.CodeBase = this.m_reader.ReadString();
									}
								}
							}
							else if (localName == "Bookmark")
							{
								expressionInfo2 = this.ReadBookmarkExpression(context, out flag4);
								activeXControl.Bookmark = expressionInfo2;
							}
							break;
						}
						case 10:
						{
							char c = localName[0];
							if (c != 'P')
							{
								if (c != 'R')
								{
									if (c == 'V')
									{
										if (localName == "Visibility")
										{
											activeXControl.Visibility = this.ReadVisibility(context, out flag2);
										}
									}
								}
								else if (localName == "RepeatWith")
								{
									activeXControl.RepeatedSibling = true;
									activeXControl.RepeatWith = this.m_reader.ReadString();
								}
							}
							else if (localName == "Parameters")
							{
								activeXControl.Parameters = this.ReadParameters(context, false, true, out flag6);
							}
							break;
						}
						case 15:
							if (localName == "DataElementName")
							{
								activeXControl.DataElementName = this.m_reader.ReadString();
							}
							break;
						case 16:
							if (localName == "CustomProperties")
							{
								activeXControl.CustomProperties = this.ReadCustomProperties(context, out flag7);
							}
							break;
						case 17:
							if (localName == "DataElementOutput")
							{
								activeXControl.DataElementOutputRDL = this.ReadDataElementOutputRDL();
							}
							break;
						}
					}
				}
			}
			while (!flag8);
			activeXControl.Computed = flag || flag2 || flag3 || flag7 || flag4 || flag5 || flag6;
			if (!flag3 && expressionInfo != null && expressionInfo.Value != null)
			{
				this.m_hasLabels = true;
			}
			if (!flag4 && expressionInfo2 != null && expressionInfo2.Value != null)
			{
				this.m_hasBookmarks = true;
			}
			return activeXControl;
		}

		// Token: 0x060069A4 RID: 27044 RVA: 0x001A0DBC File Offset: 0x0019EFBC
		private ExpressionInfo ReadBookmarkExpression(ReportPublishing.PublishingContextStruct context, out bool computedBookmark)
		{
			ExpressionInfo expressionInfo = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out computedBookmark);
			if ((context.Location & LocationFlags.InPageSection) > (LocationFlags)0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsBookmarkInPageSection, Severity.Warning, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
			}
			return expressionInfo;
		}

		// Token: 0x060069A5 RID: 27045 RVA: 0x001A0E14 File Offset: 0x0019F014
		private List ReadList(ReportItem parent, ReportPublishing.PublishingContextStruct context)
		{
			List list = new List(this.GenerateID(), this.GenerateID(), this.GenerateID(), parent);
			list.Name = this.m_reader.GetAttribute("Name");
			bool flag = (context.Location & LocationFlags.InDataRegion) == (LocationFlags)0;
			context.Location = context.Location | LocationFlags.InDataSet | LocationFlags.InDataRegion;
			context.ObjectType = list.ObjectType;
			context.ObjectName = list.Name;
			TextBoxList textBoxList = new TextBoxList();
			this.m_dataRegionCount++;
			this.m_reportItemNames.Validate(context.ObjectType, context.ObjectName, this.m_errorContext);
			if (this.m_scopeNames.Validate(false, context.ObjectName, context.ObjectType, context.ObjectName, this.m_errorContext))
			{
				this.m_reportScopes.Add(list.Name, list);
			}
			bool flag2 = true;
			if ((context.Location & LocationFlags.InPageSection) != (LocationFlags)0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsDataRegionInPageSection, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
				flag2 = false;
			}
			if ((context.Location & LocationFlags.InDetail) != (LocationFlags)0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsDataRegionInTableDetailRow, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
				flag2 = false;
			}
			this.m_reportItemCollectionList.Add(list.ReportItems);
			this.m_aggregateHolderList.Add(list);
			this.m_runningValueHolderList.Add(list.ReportItems);
			ReportPublishing.StyleInformation styleInformation = null;
			int numberOfAggregates = this.m_reportCT.NumberOfAggregates;
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag3 = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("List" == this.m_reader.LocalName)
							{
								flag3 = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (localName != null)
						{
							switch (localName.Length)
							{
							case 3:
								if (localName == "Top")
								{
									list.Top = this.ReadSize();
								}
								break;
							case 4:
								if (localName == "Left")
								{
									list.Left = this.ReadSize();
								}
								break;
							case 5:
							{
								char c = localName[0];
								if (c != 'L')
								{
									if (c != 'S')
									{
										if (c == 'W')
										{
											if (localName == "Width")
											{
												list.Width = this.ReadSize();
											}
										}
									}
									else if (localName == "Style")
									{
										styleInformation = this.ReadStyle(context);
									}
								}
								else if (localName == "Label")
								{
									list.Label = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
								}
								break;
							}
							case 6:
							{
								char c = localName[0];
								if (c <= 'H')
								{
									if (c != 'C')
									{
										if (c == 'H')
										{
											if (localName == "Height")
											{
												list.Height = this.ReadSize();
											}
										}
									}
									else if (localName == "Custom")
									{
										list.Custom = this.m_reader.ReadCustomXml();
									}
								}
								else if (c != 'N')
								{
									if (c == 'Z')
									{
										if (localName == "ZIndex")
										{
											list.ZIndex = this.m_reader.ReadInteger();
										}
									}
								}
								else if (localName == "NoRows")
								{
									list.NoRows = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
								}
								break;
							}
							case 7:
							{
								char c = localName[0];
								if (c != 'F')
								{
									if (c != 'S')
									{
										if (c == 'T')
										{
											if (localName == "ToolTip")
											{
												list.ToolTip = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
											}
										}
									}
									else if (localName == "Sorting")
									{
										list.Sorting = this.ReadSorting(context);
									}
								}
								else if (localName == "Filters")
								{
									list.Filters = this.ReadFilters(ExpressionParser.ExpressionType.DataRegionFilters, context);
								}
								break;
							}
							case 8:
							{
								char c = localName[0];
								if (c != 'B')
								{
									if (c != 'F')
									{
										if (c == 'G')
										{
											if (localName == "Grouping")
											{
												list.Grouping = this.ReadGrouping(context);
											}
										}
									}
									else if (localName == "FillPage")
									{
										list.FillPage = this.m_reader.ReadBoolean();
									}
								}
								else if (localName == "Bookmark")
								{
									list.Bookmark = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
								}
								break;
							}
							case 10:
							{
								char c = localName[0];
								if (c != 'R')
								{
									if (c == 'V')
									{
										if (localName == "Visibility")
										{
											list.Visibility = this.ReadVisibility(context);
										}
									}
								}
								else if (localName == "RepeatWith")
								{
									list.RepeatedSibling = true;
									list.RepeatWith = this.m_reader.ReadString();
								}
								break;
							}
							case 11:
							{
								char c = localName[0];
								if (c != 'D')
								{
									if (c == 'R')
									{
										if (localName == "ReportItems")
										{
											this.ReadReportItems(null, list, list.ReportItems, context, textBoxList);
										}
									}
								}
								else if (localName == "DataSetName")
								{
									string text = this.m_reader.ReadString();
									if (flag)
									{
										list.DataSetName = text;
									}
								}
								break;
							}
							case 12:
								if (localName == "KeepTogether")
								{
									list.KeepTogether = this.m_reader.ReadBoolean();
								}
								break;
							case 14:
								if (localName == "PageBreakAtEnd")
								{
									list.PageBreakAtEnd = this.m_reader.ReadBoolean();
								}
								break;
							case 15:
								if (localName == "DataElementName")
								{
									list.DataElementName = this.m_reader.ReadString();
								}
								break;
							case 16:
							{
								char c = localName[0];
								if (c != 'C')
								{
									if (c != 'D')
									{
										if (c == 'P')
										{
											if (localName == "PageBreakAtStart")
											{
												list.PageBreakAtStart = this.m_reader.ReadBoolean();
											}
										}
									}
									else if (localName == "DataInstanceName")
									{
										list.DataInstanceName = this.m_reader.ReadString();
									}
								}
								else if (localName == "CustomProperties")
								{
									list.CustomProperties = this.ReadCustomProperties(context);
								}
								break;
							}
							case 17:
								if (localName == "DataElementOutput")
								{
									list.DataElementOutputRDL = this.ReadDataElementOutputRDL();
								}
								break;
							case 25:
								if (localName == "DataInstanceElementOutput")
								{
									list.DataInstanceElementOutput = this.ReadDataElementOutput();
								}
								break;
							}
						}
					}
				}
				while (!flag3);
			}
			if (list.Grouping == null)
			{
				if (this.m_reportCT.NumberOfAggregates > numberOfAggregates)
				{
					this.m_aggregateInDetailSections = true;
				}
				this.SetSortTargetForTextBoxes(textBoxList, list);
			}
			else
			{
				this.SetSortTargetForTextBoxes(textBoxList, list.Grouping);
			}
			if (this.CanMergeGroupingAndSorting(list.Grouping, list.Sorting))
			{
				list.Grouping.GroupAndSort = true;
				list.Grouping.SortDirections = list.Sorting.SortDirections;
				list.Sorting = null;
			}
			if (list.Sorting != null)
			{
				this.m_hasSorting = true;
			}
			if (styleInformation != null)
			{
				styleInformation.Filter(ReportPublishing.StyleOwnerType.List, list.NoRows != null);
				list.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
			}
			list.Computed = true;
			if (!flag2)
			{
				return null;
			}
			return list;
		}

		// Token: 0x060069A6 RID: 27046 RVA: 0x001A170C File Offset: 0x0019F90C
		private Matrix ReadMatrix(ReportItem parent, ReportPublishing.PublishingContextStruct context)
		{
			Matrix matrix = new Matrix(this.GenerateID(), this.GenerateID(), this.GenerateID(), parent);
			matrix.Name = this.m_reader.GetAttribute("Name");
			bool flag = (context.Location & LocationFlags.InDataRegion) == (LocationFlags)0;
			context.Location = context.Location | LocationFlags.InDataSet | LocationFlags.InDataRegion;
			context.ObjectType = matrix.ObjectType;
			context.ObjectName = matrix.Name;
			this.m_dataRegionCount++;
			this.m_reportItemNames.Validate(context.ObjectType, context.ObjectName, this.m_errorContext);
			if (this.m_scopeNames.Validate(false, context.ObjectName, context.ObjectType, context.ObjectName, this.m_errorContext))
			{
				this.m_reportScopes.Add(matrix.Name, matrix);
			}
			bool flag2 = true;
			if ((context.Location & LocationFlags.InPageSection) != (LocationFlags)0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsDataRegionInPageSection, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
				flag2 = false;
			}
			if ((context.Location & LocationFlags.InDetail) != (LocationFlags)0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsDataRegionInTableDetailRow, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
				flag2 = false;
			}
			this.m_reportItemCollectionList.Add(matrix.CornerReportItems);
			this.m_reportItemCollectionList.Add(matrix.CellReportItems);
			this.m_aggregateHolderList.Add(matrix);
			this.m_runningValueHolderList.Add(matrix);
			this.m_runningValueHolderList.Add(matrix.CornerReportItems);
			this.m_runningValueHolderList.Add(matrix.CellReportItems);
			ReportPublishing.StyleInformation styleInformation = null;
			bool flag3 = false;
			TextBoxList textBoxList = new TextBoxList();
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Matrix" == this.m_reader.LocalName)
						{
							flag3 = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (localName != null)
					{
						switch (localName.Length)
						{
						case 3:
							if (localName == "Top")
							{
								matrix.Top = this.ReadSize();
							}
							break;
						case 4:
							if (localName == "Left")
							{
								matrix.Left = this.ReadSize();
							}
							break;
						case 5:
						{
							char c = localName[0];
							if (c != 'L')
							{
								if (c != 'S')
								{
									if (c == 'W')
									{
										if (localName == "Width")
										{
											matrix.Width = this.ReadSize();
										}
									}
								}
								else if (localName == "Style")
								{
									styleInformation = this.ReadStyle(context);
								}
							}
							else if (localName == "Label")
							{
								matrix.Label = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
							}
							break;
						}
						case 6:
						{
							char c = localName[2];
							if (c <= 'i')
							{
								if (c != 'R')
								{
									if (c == 'i')
									{
										if (localName == "Height")
										{
											matrix.Height = this.ReadSize();
										}
									}
								}
								else if (localName == "NoRows")
								{
									matrix.NoRows = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
								}
							}
							else if (c != 'n')
							{
								if (c != 'r')
								{
									if (c == 's')
									{
										if (localName == "Custom")
										{
											matrix.Custom = this.m_reader.ReadCustomXml();
										}
									}
								}
								else if (localName == "Corner")
								{
									this.ReadCorner(matrix, context, textBoxList);
								}
							}
							else if (localName == "ZIndex")
							{
								matrix.ZIndex = this.m_reader.ReadInteger();
							}
							break;
						}
						case 7:
						{
							char c = localName[0];
							if (c != 'F')
							{
								if (c == 'T')
								{
									if (localName == "ToolTip")
									{
										matrix.ToolTip = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
									}
								}
							}
							else if (localName == "Filters")
							{
								matrix.Filters = this.ReadFilters(ExpressionParser.ExpressionType.DataRegionFilters, context);
							}
							break;
						}
						case 8:
							if (localName == "Bookmark")
							{
								matrix.Bookmark = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
							}
							break;
						case 10:
						{
							char c = localName[0];
							if (c != 'M')
							{
								if (c != 'R')
								{
									if (c == 'V')
									{
										if (localName == "Visibility")
										{
											matrix.Visibility = this.ReadVisibility(context);
										}
									}
								}
								else if (localName == "RepeatWith")
								{
									matrix.RepeatedSibling = true;
									matrix.RepeatWith = this.m_reader.ReadString();
								}
							}
							else if (localName == "MatrixRows")
							{
								matrix.MatrixRows = this.ReadMatrixRows(matrix, context, textBoxList);
							}
							break;
						}
						case 11:
							if (localName == "DataSetName")
							{
								string text = this.m_reader.ReadString();
								if (flag)
								{
									matrix.DataSetName = text;
								}
							}
							break;
						case 12:
						{
							char c = localName[0];
							if (c != 'K')
							{
								if (c == 'R')
								{
									if (localName == "RowGroupings")
									{
										this.ReadRowGroupings(matrix, context, textBoxList);
									}
								}
							}
							else if (localName == "KeepTogether")
							{
								matrix.KeepTogether = this.m_reader.ReadBoolean();
							}
							break;
						}
						case 13:
							if (localName == "MatrixColumns")
							{
								matrix.MatrixColumns = this.ReadMatrixColumns();
							}
							break;
						case 14:
							if (localName == "PageBreakAtEnd")
							{
								matrix.PageBreakAtEnd = this.m_reader.ReadBoolean();
							}
							break;
						case 15:
						{
							char c = localName[0];
							if (c != 'C')
							{
								if (c != 'D')
								{
									if (c == 'L')
									{
										if (localName == "LayoutDirection")
										{
											matrix.LayoutDirection = this.ReadLayoutDirection();
										}
									}
								}
								else if (localName == "DataElementName")
								{
									matrix.DataElementName = this.m_reader.ReadString();
								}
							}
							else if (localName == "ColumnGroupings")
							{
								this.ReadColumnGroupings(matrix, context, textBoxList);
							}
							break;
						}
						case 16:
						{
							char c = localName[0];
							if (c != 'C')
							{
								if (c == 'P')
								{
									if (localName == "PageBreakAtStart")
									{
										matrix.PageBreakAtStart = this.m_reader.ReadBoolean();
									}
								}
							}
							else if (localName == "CustomProperties")
							{
								matrix.CustomProperties = this.ReadCustomProperties(context);
							}
							break;
						}
						case 17:
							if (localName == "DataElementOutput")
							{
								matrix.DataElementOutputRDL = this.ReadDataElementOutputRDL();
							}
							break;
						case 19:
							if (localName == "CellDataElementName")
							{
								matrix.CellDataElementName = this.m_reader.ReadString();
							}
							break;
						case 21:
							if (localName == "CellDataElementOutput")
							{
								matrix.CellDataElementOutput = this.ReadDataElementOutput();
							}
							break;
						case 22:
							if (localName == "GroupsBeforeRowHeaders")
							{
								matrix.GroupsBeforeRowHeaders = this.m_reader.ReadInteger();
							}
							break;
						}
					}
				}
			}
			while (!flag3);
			matrix.CalculatePropagatedFlags();
			if (!flag && (matrix.RowGroupingFixedHeader || matrix.ColumnGroupingFixedHeader))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsFixedHeadersInInnerDataRegion, Severity.Error, context.ObjectType, context.ObjectName, "FixedHeader", Array.Empty<string>());
			}
			if (styleInformation != null)
			{
				styleInformation.Filter(ReportPublishing.StyleOwnerType.Matrix, matrix.NoRows != null);
				matrix.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
			}
			this.SetSortTargetForTextBoxes(textBoxList, matrix);
			matrix.Computed = true;
			if (!flag2)
			{
				return null;
			}
			return matrix;
		}

		// Token: 0x060069A7 RID: 27047 RVA: 0x001A2038 File Offset: 0x001A0238
		private void ReadCorner(Matrix matrix, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget)
		{
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Corner" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "ReportItems")
				{
					this.ReadReportItems("Corner", matrix, matrix.CornerReportItems, context, textBoxesWithDefaultSortTarget);
				}
			}
			while (!flag);
		}

		// Token: 0x060069A8 RID: 27048 RVA: 0x001A20B4 File Offset: 0x001A02B4
		private void ReadColumnGroupings(Matrix matrix, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget)
		{
			MatrixHeading matrixHeading = null;
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("ColumnGroupings" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "ColumnGrouping")
				{
					TextBoxList textBoxList = new TextBoxList();
					bool flag2 = true;
					MatrixHeading matrixHeading2 = this.ReadColumnOrRowGrouping(true, matrix, context, textBoxList);
					if (matrixHeading != null)
					{
						matrixHeading.SubHeading = matrixHeading2;
						if (matrixHeading.Grouping != null)
						{
							this.SetSortTargetForTextBoxes(textBoxList, matrixHeading.Grouping);
							flag2 = false;
						}
					}
					else
					{
						matrix.Columns = matrixHeading2;
					}
					if (flag2)
					{
						textBoxesWithDefaultSortTarget.AddRange(textBoxList);
					}
					matrixHeading = matrixHeading2;
					int columnCount = matrix.ColumnCount;
					matrix.ColumnCount = columnCount + 1;
				}
			}
			while (!flag);
		}

		// Token: 0x060069A9 RID: 27049 RVA: 0x001A2188 File Offset: 0x001A0388
		private MatrixHeading ReadColumnOrRowGrouping(bool isColumn, Matrix matrix, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget)
		{
			MatrixHeading matrixHeading = new MatrixHeading(this.GenerateID(), this.GenerateID(), matrix);
			this.m_reportItemCollectionList.Add(matrixHeading.ReportItems);
			this.m_runningValueHolderList.Add(matrixHeading.ReportItems);
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ((isColumn && "ColumnGrouping" == this.m_reader.LocalName) || (!isColumn && "RowGrouping" == this.m_reader.LocalName))
						{
							flag3 = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (localName != null)
					{
						switch (localName.Length)
						{
						case 5:
							if (localName == "Width")
							{
								Global.Tracer.Assert(!isColumn);
								matrixHeading.Size = this.ReadSize();
							}
							break;
						case 6:
							if (localName == "Height")
							{
								Global.Tracer.Assert(isColumn);
								matrixHeading.Size = this.ReadSize();
							}
							break;
						case 10:
							if (localName == "StaticRows")
							{
								flag2 = true;
								Global.Tracer.Assert(!isColumn);
								this.ReadStaticColumnsOrRows(isColumn, matrix, matrixHeading, context, textBoxesWithDefaultSortTarget);
							}
							break;
						case 11:
						{
							char c = localName[0];
							if (c != 'D')
							{
								if (c == 'F')
								{
									if (localName == "FixedHeader")
									{
										if (isColumn)
										{
											matrix.ColumnGroupingFixedHeader = this.m_reader.ReadBoolean();
										}
										else
										{
											matrix.RowGroupingFixedHeader = this.m_reader.ReadBoolean();
										}
									}
								}
							}
							else if (localName == "DynamicRows")
							{
								flag = true;
								Global.Tracer.Assert(!isColumn);
								this.ReadDynamicColumnsOrRows(isColumn, matrix, matrixHeading, context, textBoxesWithDefaultSortTarget);
							}
							break;
						}
						case 13:
							if (localName == "StaticColumns")
							{
								flag2 = true;
								Global.Tracer.Assert(isColumn);
								this.ReadStaticColumnsOrRows(isColumn, matrix, matrixHeading, context, textBoxesWithDefaultSortTarget);
							}
							break;
						case 14:
							if (localName == "DynamicColumns")
							{
								flag = true;
								Global.Tracer.Assert(isColumn);
								this.ReadDynamicColumnsOrRows(isColumn, matrix, matrixHeading, context, textBoxesWithDefaultSortTarget);
							}
							break;
						}
					}
				}
			}
			while (!flag3);
			if (flag == flag2)
			{
				if (isColumn)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidColumnGrouping, Severity.Error, context.ObjectType, context.ObjectName, "ColumnGrouping", Array.Empty<string>());
				}
				else
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidRowGrouping, Severity.Error, context.ObjectType, context.ObjectName, "RowGrouping", Array.Empty<string>());
				}
			}
			if (isColumn && matrixHeading.Grouping != null && (matrixHeading.Grouping.PageBreakAtStart || matrixHeading.Grouping.PageBreakAtEnd))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsPageBreakOnMatrixColumnGroup, Severity.Warning, context.ObjectType, context.ObjectName, "ColumnGrouping", new string[] { matrixHeading.Grouping.Name });
			}
			return matrixHeading;
		}

		// Token: 0x060069AA RID: 27050 RVA: 0x001A24D8 File Offset: 0x001A06D8
		private void ReadRowGroupings(Matrix matrix, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget)
		{
			MatrixHeading matrixHeading = null;
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("RowGroupings" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "RowGrouping")
				{
					TextBoxList textBoxList = new TextBoxList();
					bool flag2 = true;
					MatrixHeading matrixHeading2 = this.ReadColumnOrRowGrouping(false, matrix, context, textBoxList);
					if (matrixHeading != null)
					{
						matrixHeading.SubHeading = matrixHeading2;
						if (matrixHeading.Grouping != null)
						{
							this.SetSortTargetForTextBoxes(textBoxList, matrixHeading.Grouping);
							flag2 = false;
						}
					}
					else
					{
						matrix.Rows = matrixHeading2;
					}
					if (flag2)
					{
						textBoxesWithDefaultSortTarget.AddRange(textBoxList);
					}
					matrixHeading = matrixHeading2;
					int rowCount = matrix.RowCount;
					matrix.RowCount = rowCount + 1;
				}
			}
			while (!flag);
		}

		// Token: 0x060069AB RID: 27051 RVA: 0x001A25AC File Offset: 0x001A07AC
		private void ReadDynamicColumnsOrRows(bool isColumns, Matrix matrix, MatrixHeading heading, ReportPublishing.PublishingContextStruct context, TextBoxList subtotalTextBoxesWithDefaultSortTarget)
		{
			bool flag = false;
			TextBoxList textBoxList = new TextBoxList();
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("DynamicColumns" == this.m_reader.LocalName || "DynamicRows" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Grouping"))
					{
						if (!(localName == "Sorting"))
						{
							if (!(localName == "Subtotal"))
							{
								if (!(localName == "ReportItems"))
								{
									if (localName == "Visibility")
									{
										heading.Visibility = this.ReadVisibility(context);
									}
								}
								else
								{
									this.ReadReportItems(isColumns ? "DynamicColumns" : "DynamicRows", matrix, heading.ReportItems, context, textBoxList);
								}
							}
							else
							{
								heading.Subtotal = this.ReadSubtotal(matrix, context, subtotalTextBoxesWithDefaultSortTarget);
							}
						}
						else
						{
							heading.Sorting = this.ReadSorting(context);
						}
					}
					else
					{
						heading.Grouping = this.ReadGrouping(context);
					}
				}
			}
			while (!flag);
			if (this.CanMergeGroupingAndSorting(heading.Grouping, heading.Sorting))
			{
				heading.Grouping.GroupAndSort = true;
				heading.Grouping.SortDirections = heading.Sorting.SortDirections;
				heading.Sorting = null;
			}
			if (heading.Sorting != null)
			{
				this.m_hasSorting = true;
			}
			if (heading.Subtotal == null && heading.Visibility != null)
			{
				heading.Subtotal = new Subtotal(this.GenerateID(), this.GenerateID(), true);
				this.m_reportItemCollectionList.Add(heading.Subtotal.ReportItems);
				this.m_runningValueHolderList.Add(heading.Subtotal.ReportItems);
			}
			Global.Tracer.Assert(heading.Grouping != null);
			this.SetSortTargetForTextBoxes(textBoxList, heading.Grouping);
		}

		// Token: 0x060069AC RID: 27052 RVA: 0x001A279C File Offset: 0x001A099C
		private void ReadStaticColumnsOrRows(bool isColumn, Matrix matrix, MatrixHeading heading, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget)
		{
			if (isColumn)
			{
				if (matrix.StaticColumns == null)
				{
					matrix.StaticColumns = heading;
				}
				else
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsMultiStaticColumnsOrRows, Severity.Error, context.ObjectType, context.ObjectName, "StaticColumns", Array.Empty<string>());
				}
			}
			else if (matrix.StaticRows == null)
			{
				matrix.StaticRows = heading;
			}
			else
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsMultiStaticColumnsOrRows, Severity.Error, context.ObjectType, context.ObjectName, "StaticRows", Array.Empty<string>());
			}
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("StaticColumns" == this.m_reader.LocalName || "StaticRows" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "StaticColumn"))
					{
						if (localName == "StaticRow")
						{
							Global.Tracer.Assert(!isColumn);
							this.ReadStaticRow(matrix, heading, context, textBoxesWithDefaultSortTarget);
							int num = heading.NumberOfStatics;
							heading.NumberOfStatics = num + 1;
						}
					}
					else
					{
						Global.Tracer.Assert(isColumn);
						this.ReadStaticColumn(matrix, heading, context, textBoxesWithDefaultSortTarget);
						int num = heading.NumberOfStatics;
						heading.NumberOfStatics = num + 1;
					}
				}
			}
			while (!flag);
			heading.IDs = new IntList();
			for (int i = 0; i < heading.ReportItems.Count; i++)
			{
				heading.IDs.Add(this.GenerateID());
			}
		}

		// Token: 0x060069AD RID: 27053 RVA: 0x001A2944 File Offset: 0x001A0B44
		private void ReadStaticColumn(Matrix matrix, MatrixHeading heading, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget)
		{
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("StaticColumn" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "ReportItems")
				{
					this.ReadReportItems("StaticColumn", matrix, heading.ReportItems, context, textBoxesWithDefaultSortTarget);
				}
			}
			while (!flag);
		}

		// Token: 0x060069AE RID: 27054 RVA: 0x001A29C0 File Offset: 0x001A0BC0
		private void ReadStaticRow(Matrix matrix, MatrixHeading heading, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget)
		{
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("StaticRow" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "ReportItems")
				{
					this.ReadReportItems("StaticRow", matrix, heading.ReportItems, context, textBoxesWithDefaultSortTarget);
				}
			}
			while (!flag);
		}

		// Token: 0x060069AF RID: 27055 RVA: 0x001A2A3C File Offset: 0x001A0C3C
		private Subtotal ReadSubtotal(Matrix matrix, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget)
		{
			bool flag = false;
			Subtotal subtotal = new Subtotal(this.GenerateID(), this.GenerateID(), false);
			this.m_reportItemCollectionList.Add(subtotal.ReportItems);
			this.m_runningValueHolderList.Add(subtotal.ReportItems);
			context.Location |= LocationFlags.InMatrixSubtotal;
			bool flag2 = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Subtotal" == this.m_reader.LocalName)
						{
							flag2 = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "ReportItems"))
					{
						if (!(localName == "Style"))
						{
							if (!(localName == "Position"))
							{
								if (!(localName == "DataElementName"))
								{
									if (localName == "DataElementOutput")
									{
										subtotal.DataElementOutput = this.ReadDataElementOutput();
									}
								}
								else
								{
									subtotal.DataElementName = this.m_reader.ReadString();
								}
							}
							else
							{
								subtotal.Position = this.ReadPosition();
							}
						}
						else
						{
							ReportPublishing.StyleInformation styleInformation = this.ReadStyle(context, out flag);
							styleInformation.Filter(ReportPublishing.StyleOwnerType.Subtotal, false);
							subtotal.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
							subtotal.Computed = flag;
						}
					}
					else
					{
						this.ReadReportItems("Subtotal", matrix, subtotal.ReportItems, context, textBoxesWithDefaultSortTarget);
					}
				}
			}
			while (!flag2);
			return subtotal;
		}

		// Token: 0x060069B0 RID: 27056 RVA: 0x001A2BD4 File Offset: 0x001A0DD4
		private MatrixRowList ReadMatrixRows(Matrix matrix, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget)
		{
			MatrixRowList matrixRowList = new MatrixRowList();
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("MatrixRows" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "MatrixRow")
				{
					matrixRowList.Add(this.ReadMatrixRow(matrix, context, textBoxesWithDefaultSortTarget));
				}
			}
			while (!flag);
			matrix.CellIDs = new IntList();
			for (int i = 0; i < matrix.CellReportItems.Count; i++)
			{
				matrix.CellIDs.Add(this.GenerateID());
			}
			return matrixRowList;
		}

		// Token: 0x060069B1 RID: 27057 RVA: 0x001A2C8C File Offset: 0x001A0E8C
		private MatrixRow ReadMatrixRow(Matrix matrix, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget)
		{
			MatrixRow matrixRow = new MatrixRow();
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("MatrixRow" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Height"))
					{
						if (localName == "MatrixCells")
						{
							matrixRow.NumberOfMatrixCells = this.ReadMatrixCells(matrix, context, textBoxesWithDefaultSortTarget);
						}
					}
					else
					{
						matrixRow.Height = this.ReadSize();
					}
				}
			}
			while (!flag);
			return matrixRow;
		}

		// Token: 0x060069B2 RID: 27058 RVA: 0x001A2D2C File Offset: 0x001A0F2C
		private int ReadMatrixCells(Matrix matrix, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget)
		{
			int num = 0;
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("MatrixCells" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "MatrixCell")
				{
					this.ReadMatrixCell(matrix, context, textBoxesWithDefaultSortTarget);
					num++;
				}
			}
			while (!flag);
			return num;
		}

		// Token: 0x060069B3 RID: 27059 RVA: 0x001A2DA4 File Offset: 0x001A0FA4
		private void ReadMatrixCell(Matrix matrix, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget)
		{
			context.Location |= LocationFlags.InMatrixCell;
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("MatrixCell" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "ReportItems")
				{
					this.ReadReportItems("MatrixCell", matrix, matrix.CellReportItems, context, textBoxesWithDefaultSortTarget);
				}
			}
			while (!flag);
		}

		// Token: 0x060069B4 RID: 27060 RVA: 0x001A2E30 File Offset: 0x001A1030
		private MatrixColumnList ReadMatrixColumns()
		{
			MatrixColumnList matrixColumnList = new MatrixColumnList();
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("MatrixColumns" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "MatrixColumn")
				{
					matrixColumnList.Add(this.ReadMatrixColumn());
				}
			}
			while (!flag);
			return matrixColumnList;
		}

		// Token: 0x060069B5 RID: 27061 RVA: 0x001A2EAC File Offset: 0x001A10AC
		private MatrixColumn ReadMatrixColumn()
		{
			MatrixColumn matrixColumn = new MatrixColumn();
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("MatrixColumn" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "Width")
				{
					matrixColumn.Width = this.ReadSize();
				}
			}
			while (!flag);
			return matrixColumn;
		}

		// Token: 0x060069B6 RID: 27062 RVA: 0x001A2F28 File Offset: 0x001A1128
		private Chart ReadChart(ReportItem parent, ReportPublishing.PublishingContextStruct context)
		{
			Chart chart = new Chart(this.GenerateID(), parent);
			chart.Name = this.m_reader.GetAttribute("Name");
			bool flag = (context.Location & LocationFlags.InDataRegion) == (LocationFlags)0;
			context.Location = context.Location | LocationFlags.InDataSet | LocationFlags.InDataRegion;
			context.ObjectType = chart.ObjectType;
			context.ObjectName = chart.Name;
			this.m_dataRegionCount++;
			this.m_reportItemNames.Validate(context.ObjectType, context.ObjectName, this.m_errorContext);
			if (this.m_scopeNames.Validate(false, context.ObjectName, context.ObjectType, context.ObjectName, this.m_errorContext))
			{
				this.m_reportScopes.Add(chart.Name, chart);
			}
			bool flag2 = true;
			if ((context.Location & LocationFlags.InPageSection) != (LocationFlags)0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsDataRegionInPageSection, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
				flag2 = false;
			}
			this.m_aggregateHolderList.Add(chart);
			this.m_runningValueHolderList.Add(chart);
			ReportPublishing.StyleInformation styleInformation = null;
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag3 = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("Chart" == this.m_reader.LocalName)
							{
								flag3 = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (localName != null)
						{
							switch (localName.Length)
							{
							case 3:
								if (localName == "Top")
								{
									chart.Top = this.ReadSize();
								}
								break;
							case 4:
							{
								char c = localName[0];
								if (c != 'L')
								{
									if (c == 'T')
									{
										if (localName == "Type")
										{
											chart.Type = this.ReadChartType();
										}
									}
								}
								else if (localName == "Left")
								{
									chart.Left = this.ReadSize();
								}
								break;
							}
							case 5:
							{
								char c = localName[0];
								if (c != 'L')
								{
									switch (c)
									{
									case 'S':
										if (localName == "Style")
										{
											styleInformation = this.ReadStyle(context);
										}
										break;
									case 'T':
										if (localName == "Title")
										{
											chart.Title = this.ReadChartTitle(context);
										}
										break;
									case 'W':
										if (localName == "Width")
										{
											chart.Width = this.ReadSize();
										}
										break;
									}
								}
								else if (localName == "Label")
								{
									chart.Label = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
								}
								break;
							}
							case 6:
							{
								char c = localName[0];
								if (c <= 'H')
								{
									if (c != 'C')
									{
										if (c == 'H')
										{
											if (localName == "Height")
											{
												chart.Height = this.ReadSize();
											}
										}
									}
									else if (localName == "Custom")
									{
										chart.Custom = this.m_reader.ReadCustomXml();
									}
								}
								else if (c != 'L')
								{
									if (c != 'N')
									{
										if (c == 'Z')
										{
											if (localName == "ZIndex")
											{
												chart.ZIndex = this.m_reader.ReadInteger();
											}
										}
									}
									else if (localName == "NoRows")
									{
										chart.NoRows = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
									}
								}
								else if (localName == "Legend")
								{
									chart.Legend = this.ReadLegend(context);
								}
								break;
							}
							case 7:
							{
								char c = localName[0];
								if (c != 'F')
								{
									switch (c)
									{
									case 'P':
										if (localName == "Palette")
										{
											chart.Palette = this.ReadChartPalette();
										}
										break;
									case 'S':
										if (localName == "Subtype")
										{
											chart.SubType = this.ReadChartSubType();
										}
										break;
									case 'T':
										if (localName == "ToolTip")
										{
											chart.ToolTip = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
										}
										break;
									}
								}
								else if (localName == "Filters")
								{
									chart.Filters = this.ReadFilters(ExpressionParser.ExpressionType.DataRegionFilters, context);
								}
								break;
							}
							case 8:
							{
								char c = localName[0];
								if (c != 'B')
								{
									if (c == 'P')
									{
										if (localName == "PlotArea")
										{
											chart.PlotArea = this.ReadPlotArea(chart, context);
										}
									}
								}
								else if (localName == "Bookmark")
								{
									chart.Bookmark = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
								}
								break;
							}
							case 9:
							{
								char c = localName[0];
								if (c != 'C')
								{
									if (c == 'V')
									{
										if (localName == "ValueAxis")
										{
											chart.ValueAxis = this.ReadCategoryOrValueAxis(chart, context);
										}
									}
								}
								else if (localName == "ChartData")
								{
									this.ReadChartData(chart, context);
								}
								break;
							}
							case 10:
							{
								char c = localName[0];
								if (c <= 'P')
								{
									if (c != 'M')
									{
										if (c == 'P')
										{
											if (localName == "PointWidth")
											{
												chart.PointWidth = this.m_reader.ReadInteger();
											}
										}
									}
									else if (localName == "MultiChart")
									{
										chart.MultiChart = this.ReadMultiChart(chart, context);
									}
								}
								else if (c != 'R')
								{
									if (c == 'V')
									{
										if (localName == "Visibility")
										{
											chart.Visibility = this.ReadVisibility(context);
										}
									}
								}
								else if (localName == "RepeatWith")
								{
									chart.RepeatedSibling = true;
									chart.RepeatWith = this.m_reader.ReadString();
								}
								break;
							}
							case 11:
								if (localName == "DataSetName")
								{
									string text = this.m_reader.ReadString();
									if (flag)
									{
										chart.DataSetName = text;
									}
								}
								break;
							case 12:
							{
								char c = localName[0];
								if (c != 'C')
								{
									if (c == 'K')
									{
										if (localName == "KeepTogether")
										{
											chart.KeepTogether = this.m_reader.ReadBoolean();
										}
									}
								}
								else if (localName == "CategoryAxis")
								{
									chart.CategoryAxis = this.ReadCategoryOrValueAxis(chart, context);
								}
								break;
							}
							case 14:
								if (localName == "PageBreakAtEnd")
								{
									chart.PageBreakAtEnd = this.m_reader.ReadBoolean();
								}
								break;
							case 15:
							{
								char c = localName[0];
								if (c != 'D')
								{
									if (c == 'S')
									{
										if (localName == "SeriesGroupings")
										{
											this.ReadSeriesGroupings(chart, context);
										}
									}
								}
								else if (localName == "DataElementName")
								{
									chart.DataElementName = this.m_reader.ReadString();
								}
								break;
							}
							case 16:
							{
								char c = localName[0];
								if (c != 'C')
								{
									if (c != 'P')
									{
										if (c == 'T')
										{
											if (localName == "ThreeDProperties")
											{
												chart.ThreeDProperties = this.ReadThreeDProperties(chart, context);
											}
										}
									}
									else if (localName == "PageBreakAtStart")
									{
										chart.PageBreakAtStart = this.m_reader.ReadBoolean();
									}
								}
								else if (localName == "CustomProperties")
								{
									chart.CustomProperties = this.ReadCustomProperties(context);
								}
								break;
							}
							case 17:
							{
								char c = localName[0];
								if (c != 'C')
								{
									if (c == 'D')
									{
										if (localName == "DataElementOutput")
										{
											chart.DataElementOutputRDL = this.ReadDataElementOutputRDL();
										}
									}
								}
								else if (localName == "CategoryGroupings")
								{
									this.ReadCategoryGroupings(chart, context);
								}
								break;
							}
							case 18:
								if (localName == "ChartElementOutput")
								{
									chart.CellDataElementOutput = this.ReadDataElementOutput();
								}
								break;
							}
						}
					}
				}
				while (!flag3);
			}
			if (!chart.IsValidChartSubType())
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidChartSubType, Severity.Error, context.ObjectType, context.ObjectName, null, new string[]
				{
					Enum.GetName(typeof(Chart.ChartTypes), chart.Type),
					Enum.GetName(typeof(Chart.ChartSubTypes), chart.SubType)
				});
			}
			if (Chart.ChartTypes.Pie == chart.Type || Chart.ChartTypes.Doughnut == chart.Type)
			{
				chart.CategoryAxis = null;
				chart.ValueAxis = null;
				if (chart.Rows != null)
				{
					if (chart.StaticRows != null && chart.StaticColumns != null)
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsInvalidChartGroupings, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
					}
					else
					{
						ChartHeading chartHeading = chart.Columns;
						while (chartHeading != null && chartHeading.SubHeading != null)
						{
							chartHeading = chartHeading.SubHeading;
						}
						if (chartHeading == null)
						{
							chart.Columns = chart.Rows;
						}
						else
						{
							chartHeading.SubHeading = chart.Rows;
						}
						if (chart.StaticRows != null)
						{
							chart.StaticColumns = chart.StaticRows;
							chart.StaticRows = null;
						}
						Global.Tracer.Assert(chart.NumberOfSeriesDataPoints != null);
						int num = 0;
						for (int i = 0; i < chart.NumberOfSeriesDataPoints.Count; i++)
						{
							num += chart.NumberOfSeriesDataPoints[i];
						}
						chart.NumberOfSeriesDataPoints = new IntList(1);
						chart.NumberOfSeriesDataPoints.Add(num);
						chart.ColumnCount += chart.RowCount;
						chart.RowCount = 0;
						chart.Rows = null;
					}
				}
			}
			if (styleInformation != null)
			{
				styleInformation.Filter(ReportPublishing.StyleOwnerType.Chart, chart.NoRows != null);
				chart.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
			}
			if (chart.Columns == null)
			{
				if ((chart.Type == Chart.ChartTypes.Bubble || chart.Type == Chart.ChartTypes.Scatter) && !chart.HasDataValueAggregates)
				{
					this.ChartAddRowNumberCategory(chart, context);
				}
				else
				{
					this.ChartFakeStaticCategory(chart);
				}
			}
			if (chart.Rows == null)
			{
				this.ChartFakeStaticSeries(chart);
			}
			chart.Computed = true;
			if (flag2)
			{
				this.m_hasImageStreams = true;
				return chart;
			}
			return null;
		}

		// Token: 0x060069B7 RID: 27063 RVA: 0x001A3B10 File Offset: 0x001A1D10
		private void ReadCategoryGroupings(Chart chart, ReportPublishing.PublishingContextStruct context)
		{
			ChartHeading chartHeading = null;
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("CategoryGroupings" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "CategoryGrouping")
				{
					ChartHeading chartHeading2 = this.ReadCategoryOrSeriesGrouping(true, chart, context);
					if (chartHeading != null)
					{
						chartHeading.SubHeading = chartHeading2;
					}
					else
					{
						chart.Columns = chartHeading2;
					}
					chartHeading = chartHeading2;
					int columnCount = chart.ColumnCount;
					chart.ColumnCount = columnCount + 1;
				}
			}
			while (!flag);
		}

		// Token: 0x060069B8 RID: 27064 RVA: 0x001A3BB0 File Offset: 0x001A1DB0
		private void ChartAddRowNumberCategory(Chart chart, ReportPublishing.PublishingContextStruct context)
		{
			Global.Tracer.Assert(chart != null);
			Global.Tracer.Assert(chart.ColumnCount == 0);
			int columnCount = chart.ColumnCount;
			chart.ColumnCount = columnCount + 1;
			chart.Columns = new ChartHeading(this.GenerateID(), chart);
			this.m_hasGrouping = true;
			Grouping grouping = new Grouping(ConstructionPhase.Publishing);
			grouping.Name = "0_" + chart.Name + "_AutoGenerated_RowNumber_Category";
			if (this.m_scopeNames.Validate(true, grouping.Name, context.ObjectType, context.ObjectName, this.m_errorContext, false))
			{
				this.m_reportScopes.Add(grouping.Name, grouping);
			}
			this.m_aggregateHolderList.Add(grouping);
			chart.Columns.Grouping = grouping;
			this.m_runningValueHolderList.Add(chart.Columns);
			ExpressionParser.ExpressionContext expressionContext = context.CreateExpressionContext(ExpressionParser.ExpressionType.GroupExpression, ExpressionParser.ConstantType.String, "CategoryGrouping", null);
			bool flag;
			grouping.GroupExpressions.Add(this.m_reportCT.ParseExpression("=RowNumber(\"" + chart.Name + "\")", expressionContext, out flag));
			Global.Tracer.Assert(!flag);
		}

		// Token: 0x060069B9 RID: 27065 RVA: 0x001A3CDC File Offset: 0x001A1EDC
		private void ChartFakeStaticSeries(Chart chart)
		{
			Global.Tracer.Assert(chart != null);
			Global.Tracer.Assert(chart.RowCount == 0);
			int num = chart.RowCount;
			chart.RowCount = num + 1;
			chart.Rows = new ChartHeading(this.GenerateID(), chart);
			ChartHeading rows = chart.Rows;
			num = rows.NumberOfStatics;
			rows.NumberOfStatics = num + 1;
			chart.StaticRows = chart.Rows;
		}

		// Token: 0x060069BA RID: 27066 RVA: 0x001A3D50 File Offset: 0x001A1F50
		private void ChartFakeStaticCategory(Chart chart)
		{
			Global.Tracer.Assert(chart != null);
			Global.Tracer.Assert(chart.ColumnCount == 0);
			int num = chart.ColumnCount;
			chart.ColumnCount = num + 1;
			chart.Columns = new ChartHeading(this.GenerateID(), chart);
			ChartHeading columns = chart.Columns;
			num = columns.NumberOfStatics;
			columns.NumberOfStatics = num + 1;
			chart.StaticColumns = chart.Columns;
		}

		// Token: 0x060069BB RID: 27067 RVA: 0x001A3DC4 File Offset: 0x001A1FC4
		private ChartHeading ReadCategoryOrSeriesGrouping(bool isCategory, Chart chart, ReportPublishing.PublishingContextStruct context)
		{
			ChartHeading chartHeading = new ChartHeading(this.GenerateID(), chart);
			this.m_runningValueHolderList.Add(chartHeading);
			bool flag = false;
			bool flag2 = false;
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag3 = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ((isCategory && "CategoryGrouping" == this.m_reader.LocalName) || (!isCategory && "SeriesGrouping" == this.m_reader.LocalName))
							{
								flag3 = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (!(localName == "DynamicCategories"))
						{
							if (!(localName == "DynamicSeries"))
							{
								if (!(localName == "StaticCategories"))
								{
									if (localName == "StaticSeries")
									{
										flag2 = true;
										Global.Tracer.Assert(!isCategory);
										this.ReadStaticCategoriesOrSeries(isCategory, chart, chartHeading, context);
									}
								}
								else
								{
									flag2 = true;
									Global.Tracer.Assert(isCategory);
									this.ReadStaticCategoriesOrSeries(isCategory, chart, chartHeading, context);
								}
							}
							else
							{
								flag = true;
								Global.Tracer.Assert(!isCategory);
								this.ReadDynamicCategoriesOrSeries(isCategory, chart, chartHeading, context);
							}
						}
						else
						{
							flag = true;
							Global.Tracer.Assert(isCategory);
							this.ReadDynamicCategoriesOrSeries(isCategory, chart, chartHeading, context);
						}
					}
				}
				while (!flag3);
			}
			if (flag == flag2)
			{
				if (isCategory)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidCategoryGrouping, Severity.Error, context.ObjectType, context.ObjectName, "CategoryGrouping", Array.Empty<string>());
				}
				else
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidSeriesGrouping, Severity.Error, context.ObjectType, context.ObjectName, "SeriesGrouping", Array.Empty<string>());
				}
			}
			if (chartHeading.Grouping != null && (chartHeading.Grouping.PageBreakAtStart || chartHeading.Grouping.PageBreakAtEnd))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsPageBreakOnChartGroup, Severity.Warning, context.ObjectType, context.ObjectName, isCategory ? "CategoryGroupings" : "SeriesGroupings", new string[] { chartHeading.Grouping.Name });
			}
			return chartHeading;
		}

		// Token: 0x060069BC RID: 27068 RVA: 0x001A3FE0 File Offset: 0x001A21E0
		private void ReadSeriesGroupings(Chart chart, ReportPublishing.PublishingContextStruct context)
		{
			ChartHeading chartHeading = null;
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("SeriesGroupings" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "SeriesGrouping")
				{
					ChartHeading chartHeading2 = this.ReadCategoryOrSeriesGrouping(false, chart, context);
					if (chartHeading != null)
					{
						chartHeading.SubHeading = chartHeading2;
					}
					else
					{
						chart.Rows = chartHeading2;
					}
					chartHeading = chartHeading2;
					int rowCount = chart.RowCount;
					chart.RowCount = rowCount + 1;
				}
			}
			while (!flag);
		}

		// Token: 0x060069BD RID: 27069 RVA: 0x001A4080 File Offset: 0x001A2280
		private void ReadDynamicCategoriesOrSeries(bool isCategory, Chart chart, ChartHeading heading, ReportPublishing.PublishingContextStruct context)
		{
			bool flag = false;
			ExpressionInfo expressionInfo = null;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ((isCategory && "DynamicCategories" == this.m_reader.LocalName) || (!isCategory && "DynamicSeries" == this.m_reader.LocalName))
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Grouping"))
					{
						if (!(localName == "Sorting"))
						{
							if (localName == "Label")
							{
								expressionInfo = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
							}
						}
						else
						{
							heading.Sorting = this.ReadSorting(context);
						}
					}
					else
					{
						heading.Grouping = this.ReadGrouping(context);
					}
				}
			}
			while (!flag);
			if (this.CanMergeGroupingAndSorting(heading.Grouping, heading.Sorting))
			{
				heading.Grouping.GroupAndSort = true;
				heading.Grouping.SortDirections = heading.Sorting.SortDirections;
				heading.Sorting = null;
			}
			if (heading.Sorting != null)
			{
				this.m_hasSorting = true;
			}
			if (expressionInfo != null && (ExpressionInfo.Types.Constant != expressionInfo.Type || expressionInfo.Value.Length != 0))
			{
				if (heading.Labels == null)
				{
					heading.Labels = new ExpressionInfoList();
				}
				heading.Labels.Add(expressionInfo);
				return;
			}
			if (heading.Labels == null)
			{
				heading.Labels = new ExpressionInfoList();
			}
			Global.Tracer.Assert(heading.Grouping.GroupExpressions != null && heading.Grouping.GroupExpressions[0] != null);
			heading.Labels.Add(heading.Grouping.GroupExpressions[0]);
		}

		// Token: 0x060069BE RID: 27070 RVA: 0x001A4244 File Offset: 0x001A2444
		private void ReadStaticCategoriesOrSeries(bool isColumn, Chart chart, ChartHeading heading, ReportPublishing.PublishingContextStruct context)
		{
			if (isColumn)
			{
				if (chart.StaticColumns == null)
				{
					chart.StaticColumns = heading;
				}
				else
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsMultiStaticCategoriesOrSeries, Severity.Error, context.ObjectType, context.ObjectName, "StaticCategories", Array.Empty<string>());
				}
			}
			else if (chart.StaticRows == null)
			{
				chart.StaticRows = heading;
			}
			else
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsMultiStaticCategoriesOrSeries, Severity.Error, context.ObjectType, context.ObjectName, "StaticSeries", Array.Empty<string>());
			}
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("StaticCategories" == this.m_reader.LocalName || "StaticSeries" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "StaticMember")
				{
					this.ReadStaticMember(chart, heading, context);
					int numberOfStatics = heading.NumberOfStatics;
					heading.NumberOfStatics = numberOfStatics + 1;
				}
			}
			while (!flag);
		}

		// Token: 0x060069BF RID: 27071 RVA: 0x001A435C File Offset: 0x001A255C
		private void ReadStaticMember(Chart chart, ChartHeading heading, ReportPublishing.PublishingContextStruct context)
		{
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("StaticMember" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "Label")
				{
					ExpressionInfo expressionInfo = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
					if (expressionInfo != null)
					{
						if (heading.Labels == null)
						{
							heading.Labels = new ExpressionInfoList();
						}
						if (ExpressionInfo.Types.Constant == expressionInfo.Type && expressionInfo.Value.Length == 0)
						{
							expressionInfo.Value = null;
						}
						heading.Labels.Add(expressionInfo);
					}
				}
			}
			while (!flag);
		}

		// Token: 0x060069C0 RID: 27072 RVA: 0x001A4420 File Offset: 0x001A2620
		private ChartTitle ReadChartTitle(ReportPublishing.PublishingContextStruct context)
		{
			ChartTitle chartTitle = new ChartTitle();
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("Title" == this.m_reader.LocalName)
							{
								flag = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (!(localName == "Caption"))
						{
							if (!(localName == "Style"))
							{
								if (localName == "Position")
								{
									chartTitle.Position = this.ReadChartTitlePosition();
								}
							}
							else
							{
								ReportPublishing.StyleInformation styleInformation = this.ReadStyle(context);
								styleInformation.Filter(ReportPublishing.StyleOwnerType.Textbox, false);
								chartTitle.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
							}
						}
						else
						{
							chartTitle.Caption = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
						}
					}
				}
				while (!flag);
			}
			return chartTitle;
		}

		// Token: 0x060069C1 RID: 27073 RVA: 0x001A4534 File Offset: 0x001A2734
		private Axis ReadCategoryOrValueAxis(Chart chart, ReportPublishing.PublishingContextStruct context)
		{
			Axis axis = null;
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("CategoryAxis" == this.m_reader.LocalName || "ValueAxis" == this.m_reader.LocalName)
							{
								flag = true;
							}
						}
					}
					else if (this.m_reader.LocalName == "Axis")
					{
						axis = this.ReadAxis(chart, context);
					}
				}
				while (!flag);
			}
			return axis;
		}

		// Token: 0x060069C2 RID: 27074 RVA: 0x001A45CC File Offset: 0x001A27CC
		private Axis ReadAxis(Chart chart, ReportPublishing.PublishingContextStruct context)
		{
			Axis axis = new Axis();
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("Axis" == this.m_reader.LocalName)
							{
								flag = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (localName != null)
						{
							switch (localName.Length)
							{
							case 3:
							{
								char c = localName[1];
								if (c != 'a')
								{
									if (c == 'i')
									{
										if (localName == "Min")
										{
											axis.AutoScaleMin = false;
											axis.Min = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
										}
									}
								}
								else if (localName == "Max")
								{
									axis.AutoScaleMax = false;
									axis.Max = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
								}
								break;
							}
							case 5:
							{
								char c = localName[0];
								if (c != 'S')
								{
									if (c == 'T')
									{
										if (localName == "Title")
										{
											axis.Title = this.ReadChartTitle(context);
										}
									}
								}
								else if (localName == "Style")
								{
									ReportPublishing.StyleInformation styleInformation = this.ReadStyle(context);
									styleInformation.Filter(ReportPublishing.StyleOwnerType.Textbox, false);
									axis.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
								}
								break;
							}
							case 6:
							{
								char c = localName[0];
								if (c != 'M')
								{
									if (c == 'S')
									{
										if (localName == "Scalar")
										{
											axis.Scalar = this.m_reader.ReadBoolean();
										}
									}
								}
								else if (localName == "Margin")
								{
									axis.Margin = this.m_reader.ReadBoolean();
								}
								break;
							}
							case 7:
							{
								char c = localName[0];
								if (c != 'C')
								{
									if (c != 'R')
									{
										if (c == 'V')
										{
											if (localName == "Visible")
											{
												axis.Visible = this.m_reader.ReadBoolean();
											}
										}
									}
									else if (localName == "Reverse")
									{
										axis.Reverse = this.m_reader.ReadBoolean();
									}
								}
								else if (localName == "CrossAt")
								{
									axis.AutoCrossAt = false;
									axis.CrossAt = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
								}
								break;
							}
							case 8:
								if (localName == "LogScale")
								{
									axis.LogScale = this.m_reader.ReadBoolean();
								}
								break;
							case 10:
								if (localName == "Interlaced")
								{
									axis.Interlaced = this.m_reader.ReadBoolean();
								}
								break;
							case 13:
							{
								char c = localName[1];
								if (c != 'a')
								{
									if (c == 'i')
									{
										if (localName == "MinorInterval")
										{
											axis.MinorInterval = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
										}
									}
								}
								else if (localName == "MajorInterval")
								{
									axis.MajorInterval = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
								}
								break;
							}
							case 14:
							{
								char c = localName[1];
								if (c != 'a')
								{
									if (c == 'i')
									{
										if (!(localName == "MinorTickMarks"))
										{
											if (localName == "MinorGridLines")
											{
												axis.MinorGridLines = this.ReadGridLines(context);
											}
										}
										else
										{
											axis.MinorTickMarks = this.ReadAxisTickMarks();
										}
									}
								}
								else if (!(localName == "MajorTickMarks"))
								{
									if (localName == "MajorGridLines")
									{
										axis.MajorGridLines = this.ReadGridLines(context);
									}
								}
								else
								{
									axis.MajorTickMarks = this.ReadAxisTickMarks();
								}
								break;
							}
							}
						}
					}
				}
				while (!flag);
			}
			return axis;
		}

		// Token: 0x060069C3 RID: 27075 RVA: 0x001A4A94 File Offset: 0x001A2C94
		private Legend ReadLegend(ReportPublishing.PublishingContextStruct context)
		{
			Legend legend = new Legend();
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("Legend" == this.m_reader.LocalName)
							{
								flag = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (!(localName == "Visible"))
						{
							if (!(localName == "Style"))
							{
								if (!(localName == "Position"))
								{
									if (!(localName == "Layout"))
									{
										if (localName == "InsidePlotArea")
										{
											legend.InsidePlotArea = this.m_reader.ReadBoolean();
										}
									}
									else
									{
										legend.Layout = this.ReadLegendLayout();
									}
								}
								else
								{
									legend.Position = this.ReadLegendPosition();
								}
							}
							else
							{
								ReportPublishing.StyleInformation styleInformation = this.ReadStyle(context);
								styleInformation.Filter(ReportPublishing.StyleOwnerType.Chart, false);
								legend.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
							}
						}
						else
						{
							legend.Visible = this.m_reader.ReadBoolean();
						}
					}
				}
				while (!flag);
			}
			return legend;
		}

		// Token: 0x060069C4 RID: 27076 RVA: 0x001A4BE0 File Offset: 0x001A2DE0
		private GridLines ReadGridLines(ReportPublishing.PublishingContextStruct context)
		{
			GridLines gridLines = new GridLines();
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("MajorGridLines" == this.m_reader.LocalName || "MinorGridLines" == this.m_reader.LocalName)
							{
								flag = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (!(localName == "Style"))
						{
							if (localName == "ShowGridLines")
							{
								gridLines.ShowGridLines = this.m_reader.ReadBoolean();
							}
						}
						else
						{
							ReportPublishing.StyleInformation styleInformation = this.ReadStyle(context);
							styleInformation.Filter(ReportPublishing.StyleOwnerType.Chart, false);
							gridLines.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
						}
					}
				}
				while (!flag);
			}
			return gridLines;
		}

		// Token: 0x060069C5 RID: 27077 RVA: 0x001A4CE4 File Offset: 0x001A2EE4
		private int ReadChartData(Chart chart, ReportPublishing.PublishingContextStruct context)
		{
			if (!this.m_reader.IsEmptyElement)
			{
				chart.NumberOfSeriesDataPoints = new IntList();
				chart.SeriesPlotType = new BoolList();
				int num = 0;
				bool flag = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("ChartData" == this.m_reader.LocalName)
							{
								flag = true;
							}
						}
					}
					else if (this.m_reader.LocalName == "ChartSeries")
					{
						this.ReadChartSeries(chart, context);
						num++;
					}
				}
				while (!flag);
				return num;
			}
			return 0;
		}

		// Token: 0x060069C6 RID: 27078 RVA: 0x001A4D84 File Offset: 0x001A2F84
		private void ReadChartSeries(Chart chart, ReportPublishing.PublishingContextStruct context)
		{
			bool flag = false;
			bool flag2 = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("ChartSeries" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "DataPoints"))
					{
						if (localName == "PlotType")
						{
							if (this.ReadPlotType())
							{
								chart.SeriesPlotType.Add(true);
								chart.HasSeriesPlotTypeLine = true;
							}
							else
							{
								chart.SeriesPlotType.Add(false);
							}
							flag2 = true;
						}
					}
					else
					{
						chart.NumberOfSeriesDataPoints.Add(this.ReadChartDataPoints(chart, context));
					}
				}
			}
			while (!flag);
			if (!flag2)
			{
				chart.SeriesPlotType.Add(false);
			}
		}

		// Token: 0x060069C7 RID: 27079 RVA: 0x001A4E70 File Offset: 0x001A3070
		private int ReadChartDataPoints(Chart chart, ReportPublishing.PublishingContextStruct context)
		{
			int num = 0;
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("DataPoints" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "DataPoint")
				{
					chart.ChartDataPoints.Add(this.ReadChartDataPoint(chart, context));
					num++;
				}
			}
			while (!flag);
			return num;
		}

		// Token: 0x060069C8 RID: 27080 RVA: 0x001A4EF4 File Offset: 0x001A30F4
		private ChartDataPoint ReadChartDataPoint(Chart chart, ReportPublishing.PublishingContextStruct context)
		{
			context.Location |= LocationFlags.InMatrixCell;
			ChartDataPoint chartDataPoint = new ChartDataPoint();
			bool flag = false;
			bool flag2 = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("DataPoint" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (localName != null)
					{
						int length = localName.Length;
						switch (length)
						{
						case 5:
							if (localName == "Style")
							{
								ReportPublishing.StyleInformation styleInformation = this.ReadStyle(context);
								styleInformation.Filter(ReportPublishing.StyleOwnerType.Chart, false);
								chartDataPoint.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
							}
							break;
						case 6:
						{
							char c = localName[0];
							if (c != 'A')
							{
								if (c == 'M')
								{
									if (localName == "Marker")
									{
										this.ReadDataPointMarker(chartDataPoint, context);
									}
								}
							}
							else if (localName == "Action")
							{
								int num = -1;
								bool flag3 = false;
								ActionItem actionItem = this.ReadActionItem(context, out flag2, ref num, ref flag3);
								chartDataPoint.Action = new Microsoft.ReportingServices.ReportProcessing.Action(actionItem, flag2);
							}
							break;
						}
						case 7:
						case 8:
							break;
						case 9:
							if (localName == "DataLabel")
							{
								chartDataPoint.DataLabel = this.ReadChartDataLabel(context);
							}
							break;
						case 10:
						{
							char c = localName[0];
							if (c != 'A')
							{
								if (c == 'D')
								{
									if (localName == "DataValues")
									{
										bool flag4;
										this.ReadChartDataValues(chartDataPoint, context, out flag4);
										if (flag4)
										{
											chart.HasDataValueAggregates = true;
										}
									}
								}
							}
							else if (localName == "ActionInfo")
							{
								chartDataPoint.Action = this.ReadAction(context, ReportPublishing.StyleOwnerType.Chart, out flag2);
							}
							break;
						}
						default:
							if (length != 15)
							{
								if (length == 17)
								{
									if (localName == "DataElementOutput")
									{
										chartDataPoint.DataElementOutput = this.ReadDataElementOutput();
									}
								}
							}
							else if (localName == "DataElementName")
							{
								chartDataPoint.DataElementName = this.m_reader.ReadString();
							}
							break;
						}
					}
				}
			}
			while (!flag);
			Global.Tracer.Assert(chart.ChartDataPoints != null);
			return chartDataPoint;
		}

		// Token: 0x060069C9 RID: 27081 RVA: 0x001A5190 File Offset: 0x001A3390
		private void ReadDataPointMarker(ChartDataPoint dataPoint, ReportPublishing.PublishingContextStruct context)
		{
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("Marker" == this.m_reader.LocalName)
							{
								flag = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (!(localName == "Type"))
						{
							if (!(localName == "Size"))
							{
								if (localName == "Style")
								{
									ReportPublishing.StyleInformation styleInformation = this.ReadStyle(context);
									styleInformation.Filter(ReportPublishing.StyleOwnerType.Chart, false);
									dataPoint.MarkerStyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
								}
							}
							else
							{
								dataPoint.MarkerSize = this.ReadSize();
							}
						}
						else
						{
							dataPoint.MarkerType = this.ReadMarkerType();
						}
					}
				}
				while (!flag);
			}
		}

		// Token: 0x060069CA RID: 27082 RVA: 0x001A5288 File Offset: 0x001A3488
		private void ReadChartDataValues(ChartDataPoint dataPoint, ReportPublishing.PublishingContextStruct context, out bool hasAggregates)
		{
			hasAggregates = false;
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("DataValues" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "DataValue")
				{
					dataPoint.DataValues.Add(this.ReadChartDataValue(context, ref hasAggregates));
				}
			}
			while (!flag);
		}

		// Token: 0x060069CB RID: 27083 RVA: 0x001A5308 File Offset: 0x001A3508
		private ExpressionInfo ReadChartDataValue(ReportPublishing.PublishingContextStruct context, ref bool hasAggregates)
		{
			ExpressionInfo expressionInfo = null;
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("DataValue" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "Value")
				{
					expressionInfo = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
					if (!hasAggregates && (expressionInfo.Aggregates != null || expressionInfo.RunningValues != null))
					{
						hasAggregates = true;
					}
				}
			}
			while (!flag);
			return expressionInfo;
		}

		// Token: 0x060069CC RID: 27084 RVA: 0x001A53A4 File Offset: 0x001A35A4
		private ChartDataLabel ReadChartDataLabel(ReportPublishing.PublishingContextStruct context)
		{
			ChartDataLabel chartDataLabel = new ChartDataLabel();
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("DataLabel" == this.m_reader.LocalName)
							{
								flag = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (!(localName == "Visible"))
						{
							if (!(localName == "Style"))
							{
								if (!(localName == "Value"))
								{
									if (!(localName == "Position"))
									{
										if (localName == "Rotation")
										{
											chartDataLabel.Rotation = this.m_reader.ReadInteger();
										}
									}
									else
									{
										chartDataLabel.Position = this.ReadDataLabelPosition();
									}
								}
								else
								{
									chartDataLabel.Value = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
								}
							}
							else
							{
								ReportPublishing.StyleInformation styleInformation = this.ReadStyle(context);
								styleInformation.Filter(ReportPublishing.StyleOwnerType.Textbox, false);
								chartDataLabel.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
							}
						}
						else
						{
							chartDataLabel.Visible = this.m_reader.ReadBoolean();
						}
					}
				}
				while (!flag);
			}
			return chartDataLabel;
		}

		// Token: 0x060069CD RID: 27085 RVA: 0x001A5504 File Offset: 0x001A3704
		private MultiChart ReadMultiChart(Chart chart, ReportPublishing.PublishingContextStruct context)
		{
			MultiChart multiChart = new MultiChart();
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("MultiChart" == this.m_reader.LocalName)
							{
								flag = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (!(localName == "Grouping"))
						{
							if (!(localName == "Layout"))
							{
								if (!(localName == "MaxCount"))
								{
									if (localName == "SyncScale")
									{
										multiChart.SyncScale = this.m_reader.ReadBoolean();
									}
								}
								else
								{
									multiChart.MaxCount = this.m_reader.ReadInteger();
								}
							}
							else
							{
								multiChart.Layout = this.ReadLayout();
							}
						}
						else
						{
							multiChart.Grouping = this.ReadGrouping(context);
						}
					}
				}
				while (!flag);
			}
			return multiChart;
		}

		// Token: 0x060069CE RID: 27086 RVA: 0x001A55F8 File Offset: 0x001A37F8
		private PlotArea ReadPlotArea(Chart chart, ReportPublishing.PublishingContextStruct context)
		{
			PlotArea plotArea = new PlotArea();
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("PlotArea" == this.m_reader.LocalName)
							{
								flag = true;
							}
						}
					}
					else if (this.m_reader.LocalName == "Style")
					{
						ReportPublishing.StyleInformation styleInformation = this.ReadStyle(context);
						styleInformation.Filter(ReportPublishing.StyleOwnerType.Chart, false);
						plotArea.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
					}
				}
				while (!flag);
			}
			return plotArea;
		}

		// Token: 0x060069CF RID: 27087 RVA: 0x001A56B8 File Offset: 0x001A38B8
		private ThreeDProperties ReadThreeDProperties(Chart chart, ReportPublishing.PublishingContextStruct context)
		{
			ThreeDProperties threeDProperties = new ThreeDProperties();
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("ThreeDProperties" == this.m_reader.LocalName)
							{
								flag = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (localName != null)
						{
							switch (localName.Length)
							{
							case 7:
							{
								char c = localName[0];
								if (c != 'E')
								{
									if (c == 'S')
									{
										if (localName == "Shading")
										{
											threeDProperties.Shading = this.ReadShading();
										}
									}
								}
								else if (localName == "Enabled")
								{
									threeDProperties.Enabled = this.m_reader.ReadBoolean();
								}
								break;
							}
							case 8:
							{
								char c = localName[0];
								if (c != 'G')
								{
									if (c == 'R')
									{
										if (localName == "Rotation")
										{
											threeDProperties.Rotation = this.m_reader.ReadInteger();
										}
									}
								}
								else if (localName == "GapDepth")
								{
									threeDProperties.GapDepth = this.m_reader.ReadInteger();
								}
								break;
							}
							case 9:
								if (localName == "Clustered")
								{
									threeDProperties.Clustered = this.m_reader.ReadBoolean();
								}
								break;
							case 10:
								if (localName == "DepthRatio")
								{
									threeDProperties.DepthRatio = this.m_reader.ReadInteger();
								}
								break;
							case 11:
							{
								char c = localName[0];
								if (c != 'H')
								{
									if (c != 'I')
									{
										if (c == 'P')
										{
											if (localName == "Perspective")
											{
												threeDProperties.Perspective = this.m_reader.ReadInteger();
											}
										}
									}
									else if (localName == "Inclination")
									{
										threeDProperties.Inclination = this.m_reader.ReadInteger();
									}
								}
								else if (localName == "HeightRatio")
								{
									threeDProperties.HeightRatio = this.m_reader.ReadInteger();
								}
								break;
							}
							case 12:
								if (localName == "DrawingStyle")
								{
									threeDProperties.DrawingStyleCube = this.ReadDrawingStyle();
								}
								break;
							case 13:
								if (localName == "WallThickness")
								{
									threeDProperties.WallThickness = this.m_reader.ReadInteger();
								}
								break;
							case 14:
								if (localName == "ProjectionMode")
								{
									threeDProperties.PerspectiveProjectionMode = this.ReadProjectionMode();
								}
								break;
							}
						}
					}
				}
				while (!flag);
			}
			return threeDProperties;
		}

		// Token: 0x060069D0 RID: 27088 RVA: 0x001A59B0 File Offset: 0x001A3BB0
		private Table ReadTable(ReportItem parent, ReportPublishing.PublishingContextStruct context)
		{
			Table table = new Table(this.GenerateID(), parent);
			table.Name = this.m_reader.GetAttribute("Name");
			bool flag = (context.Location & LocationFlags.InDataRegion) == (LocationFlags)0;
			context.Location = context.Location | LocationFlags.InDataSet | LocationFlags.InDataRegion;
			context.ObjectType = table.ObjectType;
			context.ObjectName = table.Name;
			this.m_dataRegionCount++;
			this.m_reportItemNames.Validate(context.ObjectType, context.ObjectName, this.m_errorContext);
			if (this.m_scopeNames.Validate(false, context.ObjectName, context.ObjectType, context.ObjectName, this.m_errorContext))
			{
				this.m_reportScopes.Add(table.Name, table);
			}
			bool flag2 = true;
			if ((context.Location & LocationFlags.InPageSection) != (LocationFlags)0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsDataRegionInPageSection, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
				flag2 = false;
			}
			if ((context.Location & LocationFlags.InDetail) != (LocationFlags)0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsDataRegionInTableDetailRow, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
				flag2 = false;
			}
			this.m_aggregateHolderList.Add(table);
			this.m_runningValueHolderList.Add(table);
			ReportPublishing.StyleInformation styleInformation = null;
			bool flag3 = false;
			TextBoxList textBoxList = new TextBoxList();
			TextBoxList textBoxList2 = new TextBoxList();
			TableGroup tableGroup = null;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Table" == this.m_reader.LocalName)
						{
							flag3 = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (localName != null)
					{
						switch (localName.Length)
						{
						case 3:
							if (localName == "Top")
							{
								table.Top = this.ReadSize();
							}
							break;
						case 4:
							if (localName == "Left")
							{
								table.Left = this.ReadSize();
							}
							break;
						case 5:
						{
							char c = localName[0];
							if (c != 'L')
							{
								if (c == 'S')
								{
									if (localName == "Style")
									{
										styleInformation = this.ReadStyle(context);
									}
								}
							}
							else if (localName == "Label")
							{
								table.Label = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
							}
							break;
						}
						case 6:
						{
							char c = localName[0];
							if (c <= 'F')
							{
								if (c != 'C')
								{
									if (c == 'F')
									{
										if (localName == "Footer")
										{
											TableRowList tableRowList;
											bool flag4;
											this.ReadHeaderOrFooter(table, context, textBoxList, false, out tableRowList, out flag4);
											table.FooterRows = tableRowList;
											table.FooterRepeatOnNewPage = flag4;
										}
									}
								}
								else if (localName == "Custom")
								{
									table.Custom = this.m_reader.ReadCustomXml();
								}
							}
							else if (c != 'H')
							{
								if (c != 'N')
								{
									if (c == 'Z')
									{
										if (localName == "ZIndex")
										{
											table.ZIndex = this.m_reader.ReadInteger();
										}
									}
								}
								else if (localName == "NoRows")
								{
									table.NoRows = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
								}
							}
							else if (localName == "Header")
							{
								TableRowList tableRowList2;
								bool flag5;
								this.ReadHeaderOrFooter(table, context, textBoxList, true, out tableRowList2, out flag5);
								table.HeaderRows = tableRowList2;
								table.HeaderRepeatOnNewPage = flag5;
							}
							break;
						}
						case 7:
						{
							char c = localName[0];
							if (c != 'D')
							{
								if (c != 'F')
								{
									if (c == 'T')
									{
										if (localName == "ToolTip")
										{
											table.ToolTip = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
										}
									}
								}
								else if (localName == "Filters")
								{
									table.Filters = this.ReadFilters(ExpressionParser.ExpressionType.DataRegionFilters, context);
								}
							}
							else if (localName == "Details")
							{
								TableDetail tableDetail;
								TableGroup tableGroup2;
								this.ReadDetails(table, context, textBoxList2, out tableDetail, out tableGroup2);
								if (tableGroup2 != null)
								{
									table.DetailGroup = tableGroup2;
								}
								else
								{
									table.TableDetail = tableDetail;
								}
							}
							break;
						}
						case 8:
						{
							char c = localName[0];
							if (c != 'B')
							{
								if (c == 'F')
								{
									if (localName == "FillPage")
									{
										table.FillPage = this.m_reader.ReadBoolean();
									}
								}
							}
							else if (localName == "Bookmark")
							{
								table.Bookmark = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
							}
							break;
						}
						case 10:
						{
							char c = localName[0];
							if (c != 'R')
							{
								if (c == 'V')
								{
									if (localName == "Visibility")
									{
										table.Visibility = this.ReadVisibility(context);
									}
								}
							}
							else if (localName == "RepeatWith")
							{
								table.RepeatedSibling = true;
								table.RepeatWith = this.m_reader.ReadString();
							}
							break;
						}
						case 11:
						{
							char c = localName[0];
							if (c != 'D')
							{
								if (c == 'T')
								{
									if (localName == "TableGroups")
									{
										tableGroup = this.ReadTableGroups(table, context);
									}
								}
							}
							else if (localName == "DataSetName")
							{
								string text = this.m_reader.ReadString();
								if (flag)
								{
									table.DataSetName = text;
								}
							}
							break;
						}
						case 12:
						{
							char c = localName[0];
							if (c != 'K')
							{
								if (c == 'T')
								{
									if (localName == "TableColumns")
									{
										table.TableColumns = this.ReadTableColumns(context, table);
									}
								}
							}
							else if (localName == "KeepTogether")
							{
								table.KeepTogether = this.m_reader.ReadBoolean();
							}
							break;
						}
						case 14:
							if (localName == "PageBreakAtEnd")
							{
								table.PageBreakAtEnd = this.m_reader.ReadBoolean();
							}
							break;
						case 15:
							if (localName == "DataElementName")
							{
								table.DataElementName = this.m_reader.ReadString();
							}
							break;
						case 16:
						{
							char c = localName[0];
							if (c != 'C')
							{
								if (c == 'P')
								{
									if (localName == "PageBreakAtStart")
									{
										table.PageBreakAtStart = this.m_reader.ReadBoolean();
									}
								}
							}
							else if (localName == "CustomProperties")
							{
								table.CustomProperties = this.ReadCustomProperties(context);
							}
							break;
						}
						case 17:
							if (localName == "DataElementOutput")
							{
								table.DataElementOutputRDL = this.ReadDataElementOutputRDL();
							}
							break;
						case 21:
							if (localName == "DetailDataElementName")
							{
								table.DetailDataElementName = this.m_reader.ReadString();
							}
							break;
						case 23:
							if (localName == "DetailDataElementOutput")
							{
								table.DetailDataElementOutput = this.ReadDataElementOutput();
							}
							break;
						case 24:
							if (localName == "DetailDataCollectionName")
							{
								table.DetailDataCollectionName = this.m_reader.ReadString();
							}
							break;
						}
					}
				}
			}
			while (!flag3);
			if (!flag && (table.FixedHeader || table.HasFixedColumnHeaders))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsFixedHeadersInInnerDataRegion, Severity.Error, context.ObjectType, context.ObjectName, "FixedHeader", Array.Empty<string>());
			}
			table.CalculatePropagatedFlags();
			if (styleInformation != null)
			{
				styleInformation.Filter(ReportPublishing.StyleOwnerType.Table, table.NoRows != null);
				table.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
			}
			this.SetSortTargetForTextBoxes(textBoxList, table);
			ISortFilterScope sortFilterScope;
			if (table.DetailGroup != null)
			{
				sortFilterScope = table.DetailGroup.Grouping;
			}
			else if (tableGroup != null)
			{
				sortFilterScope = tableGroup.Grouping;
			}
			else
			{
				sortFilterScope = table;
			}
			this.SetSortTargetForTextBoxes(textBoxList2, sortFilterScope);
			table.Computed = true;
			if (!flag2)
			{
				return null;
			}
			return table;
		}

		// Token: 0x060069D1 RID: 27089 RVA: 0x001A62DC File Offset: 0x001A44DC
		private void ReadHeaderOrFooter(Table parent, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget, bool allowFixedHeaders, out TableRowList tableRows, out bool repeatOnNewPage)
		{
			tableRows = null;
			repeatOnNewPage = false;
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Header" == this.m_reader.LocalName || "Footer" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "FixedHeader"))
					{
						if (!(localName == "TableRows"))
						{
							if (localName == "RepeatOnNewPage")
							{
								repeatOnNewPage = this.m_reader.ReadBoolean();
							}
						}
						else
						{
							tableRows = this.ReadTableRowList(parent, context, textBoxesWithDefaultSortTarget);
						}
					}
					else
					{
						bool flag2 = this.m_reader.ReadBoolean();
						if (allowFixedHeaders)
						{
							parent.FixedHeader = flag2;
						}
						else
						{
							this.m_errorContext.Register(ProcessingErrorCode.rsCantMakeTableGroupHeadersFixed, Severity.Error, context.ObjectType, context.ObjectName, "FixedHeader", Array.Empty<string>());
						}
					}
				}
			}
			while (!flag);
		}

		// Token: 0x060069D2 RID: 27090 RVA: 0x001A63F0 File Offset: 0x001A45F0
		private TableRowList ReadTableRowList(Table parent, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget)
		{
			TableRowList tableRowList = new TableRowList();
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("TableRows" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "TableRow")
				{
					tableRowList.Add(this.ReadTableRow(parent, context, textBoxesWithDefaultSortTarget));
				}
			}
			while (!flag);
			return tableRowList;
		}

		// Token: 0x060069D3 RID: 27091 RVA: 0x001A6470 File Offset: 0x001A4670
		private TableRow ReadTableRow(Table parent, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget)
		{
			TableRow tableRow = new TableRow(this.GenerateID(), this.GenerateID());
			this.m_reportItemCollectionList.Add(tableRow.ReportItems);
			this.m_runningValueHolderList.Add(tableRow.ReportItems);
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("TableRow" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "TableCells"))
					{
						if (!(localName == "Height"))
						{
							if (localName == "Visibility")
							{
								tableRow.Visibility = this.ReadVisibility(context);
							}
						}
						else
						{
							tableRow.Height = this.m_reader.ReadString();
						}
					}
					else
					{
						this.ReadTableCells(parent, tableRow.ReportItems, tableRow.ColSpans, context, textBoxesWithDefaultSortTarget);
					}
				}
			}
			while (!flag);
			tableRow.IDs = new IntList();
			for (int i = 0; i < tableRow.ReportItems.Count; i++)
			{
				tableRow.IDs.Add(this.GenerateID());
			}
			return tableRow;
		}

		// Token: 0x060069D4 RID: 27092 RVA: 0x001A65A8 File Offset: 0x001A47A8
		private void ReadDetails(Table parent, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget, out TableDetail tableDetail, out TableGroup detailGroup)
		{
			context.Location |= LocationFlags.InDetail;
			tableDetail = null;
			detailGroup = null;
			int numberOfAggregates = this.m_reportCT.NumberOfAggregates;
			bool flag = false;
			Grouping grouping = null;
			TableRowList tableRowList = null;
			Sorting sorting = null;
			Visibility visibility = null;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Details" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "TableRows"))
					{
						if (!(localName == "Grouping"))
						{
							if (!(localName == "Sorting"))
							{
								if (localName == "Visibility")
								{
									visibility = this.ReadVisibility(context);
								}
							}
							else
							{
								sorting = this.ReadSorting(context);
							}
						}
						else
						{
							grouping = this.ReadGrouping(context);
						}
					}
					else
					{
						tableRowList = this.ReadTableRowList(parent, context, textBoxesWithDefaultSortTarget);
					}
				}
			}
			while (!flag);
			if (grouping != null)
			{
				detailGroup = new TableGroup(this.GenerateID(), parent);
				this.m_runningValueHolderList.Add(detailGroup);
				detailGroup.Grouping = grouping;
				detailGroup.HeaderRows = tableRowList;
				detailGroup.Visibility = visibility;
				if (sorting != null)
				{
					if (this.CanMergeGroupingAndSorting(grouping, sorting))
					{
						detailGroup.Grouping.GroupAndSort = true;
						detailGroup.Grouping.SortDirections = sorting.SortDirections;
						detailGroup.Sorting = null;
						sorting = null;
					}
					else
					{
						detailGroup.Sorting = sorting;
					}
				}
			}
			else
			{
				tableDetail = new TableDetail(this.GenerateID());
				this.m_runningValueHolderList.Add(tableDetail);
				tableDetail.DetailRows = tableRowList;
				tableDetail.Sorting = sorting;
				tableDetail.Visibility = visibility;
			}
			if (sorting != null)
			{
				this.m_hasSorting = true;
			}
			if (this.m_reportCT.NumberOfAggregates > numberOfAggregates)
			{
				this.m_aggregateInDetailSections = true;
			}
		}

		// Token: 0x060069D5 RID: 27093 RVA: 0x001A678C File Offset: 0x001A498C
		private void ReadTableCells(Table parent, ReportItemCollection reportItems, IntList colSpans, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget)
		{
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("TableCells" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "TableCell")
				{
					this.ReadTableCell(parent, reportItems, colSpans, context, textBoxesWithDefaultSortTarget);
				}
			}
			while (!flag);
		}

		// Token: 0x060069D6 RID: 27094 RVA: 0x001A6800 File Offset: 0x001A4A00
		private void ReadTableCell(Table parent, ReportItemCollection reportItems, IntList colSpans, ReportPublishing.PublishingContextStruct context, TextBoxList textBoxesWithDefaultSortTarget)
		{
			int num = 1;
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("TableCell" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "ReportItems"))
					{
						if (localName == "ColSpan")
						{
							num = this.m_reader.ReadInteger();
						}
					}
					else
					{
						this.ReadReportItems("TableCell", parent, reportItems, context, textBoxesWithDefaultSortTarget);
					}
				}
			}
			while (!flag);
			colSpans.Add(num);
		}

		// Token: 0x060069D7 RID: 27095 RVA: 0x001A68AC File Offset: 0x001A4AAC
		private TableColumnList ReadTableColumns(ReportPublishing.PublishingContextStruct context, Table table)
		{
			TableColumnList tableColumnList = new TableColumnList();
			bool flag = false;
			bool flag2 = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("TableColumns" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "TableColumn")
				{
					TableColumn tableColumn = this.ReadTableColumn(context);
					tableColumnList.Add(tableColumn);
					if (tableColumn.FixedHeader)
					{
						flag2 = true;
					}
				}
			}
			while (!flag);
			if (flag2)
			{
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				for (int i = 0; i < tableColumnList.Count; i++)
				{
					if (tableColumnList[i].FixedHeader)
					{
						if (flag4)
						{
							flag5 = true;
							break;
						}
						flag3 = true;
					}
					else if (flag3)
					{
						flag4 = true;
					}
				}
				if (!flag5 && !tableColumnList[0].FixedHeader && !tableColumnList[tableColumnList.Count - 1].FixedHeader)
				{
					flag5 = true;
				}
				if (!flag5 && !flag4 && tableColumnList[0].FixedHeader && tableColumnList[tableColumnList.Count - 1].FixedHeader)
				{
					flag5 = true;
				}
				if (flag5)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidFixedTableColumnHeaderSpacing, Severity.Error, context.ObjectType, context.ObjectName, "FixedHeader", Array.Empty<string>());
				}
				table.HasFixedColumnHeaders = true;
			}
			return tableColumnList;
		}

		// Token: 0x060069D8 RID: 27096 RVA: 0x001A6A10 File Offset: 0x001A4C10
		private TableColumn ReadTableColumn(ReportPublishing.PublishingContextStruct context)
		{
			TableColumn tableColumn = new TableColumn();
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("TableColumn" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "FixedHeader"))
					{
						if (!(localName == "Width"))
						{
							if (localName == "Visibility")
							{
								tableColumn.Visibility = this.ReadVisibility(context);
							}
						}
						else
						{
							tableColumn.Width = this.ReadSize();
						}
					}
					else
					{
						tableColumn.FixedHeader = this.m_reader.ReadBoolean();
					}
				}
			}
			while (!flag);
			return tableColumn;
		}

		// Token: 0x060069D9 RID: 27097 RVA: 0x001A6AD0 File Offset: 0x001A4CD0
		private TableGroup ReadTableGroups(Table table, ReportPublishing.PublishingContextStruct context)
		{
			TableGroup tableGroup = null;
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("TableGroups" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "TableGroup")
				{
					TableGroup tableGroup2 = this.ReadTableGroup(table, context);
					if (tableGroup != null)
					{
						tableGroup.SubGroup = tableGroup2;
					}
					else
					{
						table.TableGroups = tableGroup2;
					}
					tableGroup = tableGroup2;
				}
			}
			while (!flag);
			return tableGroup;
		}

		// Token: 0x060069DA RID: 27098 RVA: 0x001A6B58 File Offset: 0x001A4D58
		private TableGroup ReadTableGroup(Table table, ReportPublishing.PublishingContextStruct context)
		{
			TableGroup tableGroup = new TableGroup(this.GenerateID(), table);
			this.m_runningValueHolderList.Add(tableGroup);
			bool flag = false;
			TextBoxList textBoxList = new TextBoxList();
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("TableGroup" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Grouping"))
					{
						if (!(localName == "Sorting"))
						{
							if (!(localName == "Header"))
							{
								if (!(localName == "Footer"))
								{
									if (localName == "Visibility")
									{
										tableGroup.Visibility = this.ReadVisibility(context);
									}
								}
								else
								{
									TableRowList tableRowList;
									bool flag2;
									this.ReadHeaderOrFooter(table, context, textBoxList, false, out tableRowList, out flag2);
									tableGroup.FooterRows = tableRowList;
									tableGroup.FooterRepeatOnNewPage = flag2;
								}
							}
							else
							{
								TableRowList tableRowList2;
								bool flag3;
								this.ReadHeaderOrFooter(table, context, textBoxList, false, out tableRowList2, out flag3);
								tableGroup.HeaderRows = tableRowList2;
								tableGroup.HeaderRepeatOnNewPage = flag3;
							}
						}
						else
						{
							tableGroup.Sorting = this.ReadSorting(context);
						}
					}
					else
					{
						tableGroup.Grouping = this.ReadGrouping(context);
					}
				}
			}
			while (!flag);
			if (this.CanMergeGroupingAndSorting(tableGroup.Grouping, tableGroup.Sorting))
			{
				tableGroup.Grouping.GroupAndSort = true;
				tableGroup.Grouping.SortDirections = tableGroup.Sorting.SortDirections;
				tableGroup.Sorting = null;
			}
			if (tableGroup.Sorting != null)
			{
				this.m_hasSorting = true;
			}
			Global.Tracer.Assert(tableGroup.Grouping != null);
			this.SetSortTargetForTextBoxes(textBoxList, tableGroup.Grouping);
			return tableGroup;
		}

		// Token: 0x060069DB RID: 27099 RVA: 0x001A6D04 File Offset: 0x001A4F04
		private OWCChart ReadOWCChart(ReportItem parent, ReportPublishing.PublishingContextStruct context)
		{
			OWCChart owcchart = new OWCChart(this.GenerateID(), parent);
			owcchart.Name = this.m_reader.GetAttribute("Name");
			bool flag = (context.Location & LocationFlags.InDataRegion) == (LocationFlags)0;
			context.Location = context.Location | LocationFlags.InDataSet | LocationFlags.InDataRegion;
			context.ObjectType = owcchart.ObjectType;
			context.ObjectName = owcchart.Name;
			this.m_dataRegionCount++;
			this.m_reportItemNames.Validate(context.ObjectType, context.ObjectName, this.m_errorContext);
			if (this.m_scopeNames.Validate(false, context.ObjectName, context.ObjectType, context.ObjectName, this.m_errorContext))
			{
				this.m_reportScopes.Add(owcchart.Name, owcchart);
			}
			bool flag2 = true;
			if ((context.Location & LocationFlags.InPageSection) != (LocationFlags)0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsDataRegionInPageSection, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
				flag2 = false;
			}
			if ((context.Location & LocationFlags.InDetail) != (LocationFlags)0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsDataRegionInTableDetailRow, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
				flag2 = false;
			}
			this.m_aggregateHolderList.Add(owcchart);
			this.m_runningValueHolderList.Add(owcchart);
			ReportPublishing.StyleInformation styleInformation = null;
			bool flag3 = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("OWCChart" == this.m_reader.LocalName)
						{
							flag3 = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (localName != null)
					{
						switch (localName.Length)
						{
						case 3:
							if (localName == "Top")
							{
								owcchart.Top = this.ReadSize();
							}
							break;
						case 4:
							if (localName == "Left")
							{
								owcchart.Left = this.ReadSize();
							}
							break;
						case 5:
						{
							char c = localName[0];
							if (c != 'L')
							{
								if (c != 'S')
								{
									if (c == 'W')
									{
										if (localName == "Width")
										{
											owcchart.Width = this.ReadSize();
										}
									}
								}
								else if (localName == "Style")
								{
									styleInformation = this.ReadStyle(context);
								}
							}
							else if (localName == "Label")
							{
								owcchart.Label = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
							}
							break;
						}
						case 6:
						{
							char c = localName[0];
							if (c <= 'H')
							{
								if (c != 'C')
								{
									if (c == 'H')
									{
										if (localName == "Height")
										{
											owcchart.Height = this.ReadSize();
										}
									}
								}
								else if (localName == "Custom")
								{
									owcchart.Custom = this.m_reader.ReadCustomXml();
								}
							}
							else if (c != 'N')
							{
								if (c == 'Z')
								{
									if (localName == "ZIndex")
									{
										owcchart.ZIndex = this.m_reader.ReadInteger();
									}
								}
							}
							else if (localName == "NoRows")
							{
								owcchart.NoRows = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
							}
							break;
						}
						case 7:
						{
							char c = localName[0];
							if (c != 'F')
							{
								if (c == 'T')
								{
									if (localName == "ToolTip")
									{
										owcchart.ToolTip = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
									}
								}
							}
							else if (localName == "Filters")
							{
								owcchart.Filters = this.ReadFilters(ExpressionParser.ExpressionType.DataRegionFilters, context);
							}
							break;
						}
						case 8:
							if (localName == "Bookmark")
							{
								owcchart.Bookmark = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
							}
							break;
						case 10:
						{
							char c = localName[0];
							if (c != 'O')
							{
								if (c != 'R')
								{
									if (c == 'V')
									{
										if (localName == "Visibility")
										{
											owcchart.Visibility = this.ReadVisibility(context);
										}
									}
								}
								else if (localName == "RepeatWith")
								{
									owcchart.RepeatedSibling = true;
									owcchart.RepeatWith = this.m_reader.ReadString();
								}
							}
							else if (localName == "OWCColumns")
							{
								owcchart.ChartData = this.ReadChartColumns(context);
							}
							break;
						}
						case 11:
							if (localName == "DataSetName")
							{
								string text = this.m_reader.ReadString();
								if (flag)
								{
									owcchart.DataSetName = text;
								}
							}
							break;
						case 12:
							if (localName == "KeepTogether")
							{
								owcchart.KeepTogether = this.m_reader.ReadBoolean();
							}
							break;
						case 13:
							if (localName == "OWCDefinition")
							{
								owcchart.ChartDefinition = this.m_reader.ReadString();
							}
							break;
						case 14:
							if (localName == "PageBreakAtEnd")
							{
								owcchart.PageBreakAtEnd = this.m_reader.ReadBoolean();
							}
							break;
						case 15:
							if (localName == "DataElementName")
							{
								owcchart.DataElementName = this.m_reader.ReadString();
							}
							break;
						case 16:
						{
							char c = localName[0];
							if (c != 'C')
							{
								if (c == 'P')
								{
									if (localName == "PageBreakAtStart")
									{
										owcchart.PageBreakAtStart = this.m_reader.ReadBoolean();
									}
								}
							}
							else if (localName == "CustomProperties")
							{
								owcchart.CustomProperties = this.ReadCustomProperties(context);
							}
							break;
						}
						case 17:
							if (localName == "DataElementOutput")
							{
								owcchart.DataElementOutputRDL = this.ReadDataElementOutputRDL();
							}
							break;
						}
					}
				}
			}
			while (!flag3);
			if (styleInformation != null)
			{
				styleInformation.Filter(ReportPublishing.StyleOwnerType.OWCChart, owcchart.NoRows != null);
				owcchart.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
			}
			owcchart.Computed = true;
			if (flag2)
			{
				this.m_hasImageStreams = true;
				return owcchart;
			}
			return null;
		}

		// Token: 0x060069DC RID: 27100 RVA: 0x001A741C File Offset: 0x001A561C
		private ChartColumnList ReadChartColumns(ReportPublishing.PublishingContextStruct context)
		{
			ChartColumnList chartColumnList = new ChartColumnList();
			CLSUniqueNameValidator clsuniqueNameValidator = new CLSUniqueNameValidator(ProcessingErrorCode.rsInvalidChartColumnNameNotCLSCompliant, ProcessingErrorCode.rsDuplicateItemName);
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("OWCColumns" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "OWCColumn")
				{
					chartColumnList.Add(this.ReadChartColumn(clsuniqueNameValidator, context));
				}
			}
			while (!flag);
			return chartColumnList;
		}

		// Token: 0x060069DD RID: 27101 RVA: 0x001A74A8 File Offset: 0x001A56A8
		private ChartColumn ReadChartColumn(CLSUniqueNameValidator chartColumnNames, ReportPublishing.PublishingContextStruct context)
		{
			ChartColumn chartColumn = new ChartColumn();
			chartColumn.Name = this.m_reader.GetAttribute("Name");
			chartColumnNames.Validate(chartColumn.Name, context.ObjectType, context.ObjectName, this.m_errorContext);
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("OWCColumn" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "Value")
				{
					chartColumn.Value = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
				}
			}
			while (!flag);
			return chartColumn;
		}

		// Token: 0x060069DE RID: 27102 RVA: 0x001A7568 File Offset: 0x001A5768
		private Sorting ReadSorting(ReportPublishing.PublishingContextStruct context)
		{
			Sorting sorting = new Sorting(ConstructionPhase.Publishing);
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Sorting" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "SortBy")
				{
					this.ReadSortBy(sorting, context);
				}
			}
			while (!flag);
			return sorting;
		}

		// Token: 0x060069DF RID: 27103 RVA: 0x001A75E0 File Offset: 0x001A57E0
		private void ReadSortBy(Sorting sorting, ReportPublishing.PublishingContextStruct context)
		{
			ExpressionInfo expressionInfo = null;
			bool flag = true;
			bool flag2 = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("SortBy" == this.m_reader.LocalName)
						{
							flag2 = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "SortExpression"))
					{
						if (localName == "Direction")
						{
							flag = this.ReadDirection();
						}
					}
					else
					{
						expressionInfo = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.SortExpression, ExpressionParser.ConstantType.String, context);
					}
				}
			}
			while (!flag2);
			sorting.SortExpressions.Add(expressionInfo);
			sorting.SortDirections.Add(flag);
			if (expressionInfo.HasRecursiveAggregates())
			{
				this.m_hasSpecialRecursiveAggregates = true;
			}
		}

		// Token: 0x060069E0 RID: 27104 RVA: 0x001A76B0 File Offset: 0x001A58B0
		private bool CanMergeGroupingAndSorting(Grouping grouping, Sorting sorting)
		{
			if (grouping != null && grouping.Parent == null && sorting != null && grouping.GroupExpressions != null && sorting.SortExpressions != null && grouping.GroupExpressions.Count == sorting.SortExpressions.Count)
			{
				for (int i = 0; i < grouping.GroupExpressions.Count; i++)
				{
					if (grouping.GroupExpressions[i].OriginalText != sorting.SortExpressions[i].OriginalText)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060069E1 RID: 27105 RVA: 0x001A7738 File Offset: 0x001A5938
		private Grouping ReadGrouping(ReportPublishing.PublishingContextStruct context)
		{
			this.m_hasGrouping = true;
			Grouping grouping = new Grouping(ConstructionPhase.Publishing);
			grouping.Name = this.m_reader.GetAttribute("Name");
			if (this.m_scopeNames.Validate(true, grouping.Name, context.ObjectType, context.ObjectName, this.m_errorContext))
			{
				this.m_reportScopes.Add(grouping.Name, grouping);
			}
			this.m_aggregateHolderList.Add(grouping);
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Grouping" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (localName != null)
					{
						switch (localName.Length)
						{
						case 5:
							if (localName == "Label")
							{
								grouping.GroupLabel = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context);
							}
							break;
						case 6:
						{
							char c = localName[0];
							if (c != 'C')
							{
								if (c == 'P')
								{
									if (localName == "Parent")
									{
										grouping.Parent = new ExpressionInfoList();
										grouping.Parent.Add(this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.GroupExpression, ExpressionParser.ConstantType.String, context));
									}
								}
							}
							else if (localName == "Custom")
							{
								grouping.Custom = this.m_reader.ReadCustomXml();
							}
							break;
						}
						case 7:
							if (localName == "Filters")
							{
								grouping.Filters = this.ReadFilters(ExpressionParser.ExpressionType.GroupingFilters, context);
								this.m_hasGroupFilters = true;
							}
							break;
						case 14:
							if (localName == "PageBreakAtEnd")
							{
								grouping.PageBreakAtEnd = this.m_reader.ReadBoolean();
							}
							break;
						case 15:
							if (localName == "DataElementName")
							{
								grouping.DataElementName = this.m_reader.ReadString();
							}
							break;
						case 16:
						{
							char c = localName[0];
							if (c != 'C')
							{
								if (c != 'G')
								{
									if (c == 'P')
									{
										if (localName == "PageBreakAtStart")
										{
											grouping.PageBreakAtStart = this.m_reader.ReadBoolean();
										}
									}
								}
								else if (localName == "GroupExpressions")
								{
									this.ReadGroupExpressions(grouping, context);
								}
							}
							else if (localName == "CustomProperties")
							{
								grouping.CustomProperties = this.ReadCustomProperties(context);
							}
							break;
						}
						case 17:
							if (localName == "DataElementOutput")
							{
								grouping.DataElementOutput = this.ReadDataElementOutput();
							}
							break;
						case 18:
							if (localName == "DataCollectionName")
							{
								grouping.DataCollectionName = this.m_reader.ReadString();
							}
							break;
						}
					}
				}
			}
			while (!flag);
			if (grouping.Parent != null && 1 != grouping.GroupExpressions.Count)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidGroupingParent, Severity.Error, context.ObjectType, context.ObjectName, "Parent", Array.Empty<string>());
			}
			return grouping;
		}

		// Token: 0x060069E2 RID: 27106 RVA: 0x001A7AB8 File Offset: 0x001A5CB8
		private void ReadGroupExpressions(Grouping grouping, ReportPublishing.PublishingContextStruct context)
		{
			bool flag = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("GroupExpressions" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "GroupExpression")
				{
					grouping.GroupExpressions.Add(this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.GroupExpression, ExpressionParser.ConstantType.String, context));
				}
			}
			while (!flag);
		}

		// Token: 0x060069E3 RID: 27107 RVA: 0x001A7B40 File Offset: 0x001A5D40
		private Microsoft.ReportingServices.ReportProcessing.Action ReadAction(ReportPublishing.PublishingContextStruct context, ReportPublishing.StyleOwnerType styleOwnerType, out bool computed)
		{
			Microsoft.ReportingServices.ReportProcessing.Action action = new Microsoft.ReportingServices.ReportProcessing.Action();
			bool flag = false;
			bool flag2 = false;
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag3 = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("ActionInfo" == this.m_reader.LocalName)
							{
								flag3 = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (!(localName == "Style"))
						{
							if (localName == "Actions")
							{
								this.ReadActionItemList(action, context);
								if (action.ComputedActionItemsCount > 0)
								{
									flag2 = true;
								}
							}
						}
						else
						{
							ReportPublishing.StyleInformation styleInformation = this.ReadStyle(context, out flag);
							styleInformation.Filter(styleOwnerType, false);
							action.StyleClass = PublishingValidator.ValidateAndCreateStyle(styleInformation.Names, styleInformation.Values, context.ObjectType, context.ObjectName, this.m_errorContext);
						}
					}
				}
				while (!flag3);
			}
			computed = flag || flag2;
			return action;
		}

		// Token: 0x060069E4 RID: 27108 RVA: 0x001A7C40 File Offset: 0x001A5E40
		private void ReadActionItemList(Microsoft.ReportingServices.ReportProcessing.Action actionInfo, ReportPublishing.PublishingContextStruct context)
		{
			int num = -1;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Actions" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "Action")
				{
					actionInfo.ActionItems.Add(this.ReadActionItem(context, out flag2, ref num, ref flag3));
				}
			}
			while (!flag);
			num++;
			actionInfo.ComputedActionItemsCount = num;
			if (flag3 && actionInfo.ActionItems.Count > 1)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidActionLabel, Severity.Error, context.ObjectType, context.ObjectName, "Actions", Array.Empty<string>());
			}
		}

		// Token: 0x060069E5 RID: 27109 RVA: 0x001A7D0C File Offset: 0x001A5F0C
		private ActionItem ReadActionItem(ReportPublishing.PublishingContextStruct context, out bool computed, ref int computedIndex, ref bool missingLabel)
		{
			ActionItem actionItem = new ActionItem();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag9 = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("Action" == this.m_reader.LocalName)
							{
								flag9 = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (!(localName == "Hyperlink"))
						{
							if (!(localName == "Drillthrough"))
							{
								if (!(localName == "BookmarkLink"))
								{
									if (localName == "Label")
									{
										flag4 = true;
										actionItem.Label = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag8);
									}
								}
								else
								{
									flag3 = true;
									actionItem.BookmarkLink = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag7);
								}
							}
							else
							{
								flag2 = true;
								this.ReadDrillthrough(context, actionItem, out flag6);
							}
						}
						else
						{
							this.m_hasHyperlinks = true;
							flag = true;
							actionItem.HyperLinkURL = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag5);
						}
					}
				}
				while (!flag9);
			}
			int num = 0;
			if (flag)
			{
				num++;
			}
			if (flag2)
			{
				num++;
				if ((context.Location & LocationFlags.InPageSection) > (LocationFlags)0)
				{
					this.m_pageSectionDrillthroughs = true;
				}
			}
			if (flag3)
			{
				num++;
			}
			if (1 != num)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidAction, Severity.Error, context.ObjectType, context.ObjectName, "Action", Array.Empty<string>());
			}
			if (!flag4)
			{
				missingLabel = true;
			}
			computed = flag5 || flag6 || flag7 || flag8;
			if (computed)
			{
				computedIndex++;
				actionItem.ComputedIndex = computedIndex;
			}
			return actionItem;
		}

		// Token: 0x060069E6 RID: 27110 RVA: 0x001A7EDC File Offset: 0x001A60DC
		private void ReadDrillthrough(ReportPublishing.PublishingContextStruct context, ActionItem actionItem, out bool computed)
		{
			computed = false;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("Drillthrough" == this.m_reader.LocalName)
						{
							flag4 = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "ReportName"))
					{
						if (!(localName == "Parameters"))
						{
							if (localName == "BookmarkLink")
							{
								actionItem.DrillthroughBookmarkLink = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag2);
							}
						}
						else
						{
							actionItem.DrillthroughParameters = this.ReadParameters(context, true, false, out flag);
						}
					}
					else
					{
						actionItem.DrillthroughReportName = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag3);
						if (ExpressionInfo.Types.Constant == actionItem.DrillthroughReportName.Type)
						{
							actionItem.DrillthroughReportName.Value = PublishingValidator.ValidateReportName(this.m_reportContext, actionItem.DrillthroughReportName.Value, context.ObjectType, context.ObjectName, "DrillthroughReportName", this.m_errorContext);
						}
					}
				}
			}
			while (!flag4);
			computed = flag || flag2 || flag3;
		}

		// Token: 0x060069E7 RID: 27111 RVA: 0x001A8020 File Offset: 0x001A6220
		private Visibility ReadVisibility(ReportPublishing.PublishingContextStruct context, out bool computed)
		{
			this.m_static = true;
			Visibility visibility = new Visibility();
			bool flag = false;
			bool flag2 = false;
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag3 = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("Visibility" == this.m_reader.LocalName)
							{
								flag3 = true;
							}
						}
					}
					else
					{
						string localName = this.m_reader.LocalName;
						if (!(localName == "Hidden"))
						{
							if (localName == "ToggleItem")
							{
								flag2 = true;
								if ((context.Location & LocationFlags.InPageSection) != (LocationFlags)0)
								{
									this.m_errorContext.Register(ProcessingErrorCode.rsToggleInPageSection, Severity.Error, context.ObjectType, context.ObjectName, "ToggleItem", Array.Empty<string>());
								}
								this.m_interactive = true;
								visibility.Toggle = this.m_reader.ReadString();
							}
						}
						else
						{
							visibility.Hidden = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.Boolean, context, out flag);
						}
					}
				}
				while (!flag3);
			}
			computed = flag || flag2;
			return visibility;
		}

		// Token: 0x060069E8 RID: 27112 RVA: 0x001A8140 File Offset: 0x001A6340
		private Visibility ReadVisibility(ReportPublishing.PublishingContextStruct context)
		{
			bool flag;
			return this.ReadVisibility(context, out flag);
		}

		// Token: 0x060069E9 RID: 27113 RVA: 0x001A8158 File Offset: 0x001A6358
		private DataValueList ReadCustomProperties(ReportPublishing.PublishingContextStruct context)
		{
			bool flag;
			return this.ReadCustomProperties(context, out flag);
		}

		// Token: 0x060069EA RID: 27114 RVA: 0x001A8170 File Offset: 0x001A6370
		private DataValueList ReadCustomProperties(ReportPublishing.PublishingContextStruct context, out bool computed)
		{
			bool flag = false;
			computed = false;
			int num = 0;
			DataValueList dataValueList = new DataValueList();
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("CustomProperties" == this.m_reader.LocalName)
						{
							flag = true;
						}
					}
				}
				else if (this.m_reader.LocalName == "CustomProperty")
				{
					dataValueList.Add(this.ReadDataValue(true, ++num, ref computed, context));
				}
			}
			while (!flag);
			return dataValueList;
		}

		// Token: 0x060069EB RID: 27115 RVA: 0x001A81F8 File Offset: 0x001A63F8
		private DataValue ReadDataValue(bool isCustomProperty, int index, ReportPublishing.PublishingContextStruct context)
		{
			bool flag = false;
			return this.ReadDataValue(isCustomProperty, index, ref flag, context);
		}

		// Token: 0x060069EC RID: 27116 RVA: 0x001A8214 File Offset: 0x001A6414
		private DataValue ReadDataValue(bool isCustomProperty, int index, ref bool isComputed, ReportPublishing.PublishingContextStruct context)
		{
			DataValue dataValue = new DataValue();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ((isCustomProperty && "CustomProperty" == this.m_reader.LocalName) || (!isCustomProperty && "DataValue" == this.m_reader.LocalName))
						{
							flag3 = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Name"))
					{
						if (localName == "Value")
						{
							dataValue.Value = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag2);
						}
					}
					else
					{
						dataValue.Name = this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag);
					}
				}
			}
			while (!flag3);
			Global.Tracer.Assert(dataValue.Value != null);
			if (dataValue.Name == null && isCustomProperty)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsMissingCustomPropertyName, Severity.Error, context.ObjectType, context.ObjectName, "Name", new string[] { index.ToString(CultureInfo.CurrentCulture) });
			}
			isComputed |= flag2 || flag;
			return dataValue;
		}

		// Token: 0x060069ED RID: 27117 RVA: 0x001A8360 File Offset: 0x001A6560
		private bool CheckUserProfileDependency()
		{
			bool flag = false;
			if (this.m_reportLocationFlags == UserLocationFlags.ReportBody)
			{
				if ((this.m_userReferenceLocation & UserLocationFlags.ReportBody) == (UserLocationFlags)0)
				{
					flag = true;
				}
			}
			else if (this.m_reportLocationFlags == UserLocationFlags.ReportPageSection)
			{
				if ((this.m_userReferenceLocation & UserLocationFlags.ReportPageSection) == (UserLocationFlags)0)
				{
					flag = true;
				}
			}
			else if (this.m_reportLocationFlags == UserLocationFlags.ReportQueries && (this.m_userReferenceLocation & UserLocationFlags.ReportQueries) == (UserLocationFlags)0)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x060069EE RID: 27118 RVA: 0x001A83B4 File Offset: 0x001A65B4
		private void SetUserProfileDependency()
		{
			if (this.m_reportLocationFlags == UserLocationFlags.ReportBody)
			{
				this.m_userReferenceLocation |= UserLocationFlags.ReportBody;
				return;
			}
			if (this.m_reportLocationFlags == UserLocationFlags.ReportPageSection)
			{
				this.m_userReferenceLocation |= UserLocationFlags.ReportPageSection;
				return;
			}
			if (this.m_reportLocationFlags == UserLocationFlags.ReportQueries)
			{
				this.m_userReferenceLocation |= UserLocationFlags.ReportQueries;
			}
		}

		// Token: 0x060069EF RID: 27119 RVA: 0x001A8408 File Offset: 0x001A6608
		private ExpressionInfo ReadExpression(string expression, string propertyName, string dataSetName, ExpressionParser.ExpressionType expressionType, ExpressionParser.ConstantType constantType, ReportPublishing.PublishingContextStruct context)
		{
			return this.ReadExpression(false, expression, propertyName, dataSetName, expressionType, constantType, context);
		}

		// Token: 0x060069F0 RID: 27120 RVA: 0x001A841C File Offset: 0x001A661C
		private ExpressionInfo ReadExpression(bool parseExtended, string expression, string propertyName, string dataSetName, ExpressionParser.ExpressionType expressionType, ExpressionParser.ConstantType constantType, ReportPublishing.PublishingContextStruct context)
		{
			ExpressionParser.ExpressionContext expressionContext = context.CreateExpressionContext(expressionType, constantType, propertyName, dataSetName, parseExtended);
			if (!this.CheckUserProfileDependency())
			{
				return this.m_reportCT.ParseExpression(expression, expressionContext);
			}
			bool flag;
			ExpressionInfo expressionInfo = this.m_reportCT.ParseExpression(expression, expressionContext, out flag);
			if (flag)
			{
				this.SetUserProfileDependency();
			}
			return expressionInfo;
		}

		// Token: 0x060069F1 RID: 27121 RVA: 0x001A8468 File Offset: 0x001A6668
		private ExpressionInfo ReadExpression(string propertyName, string dataSetName, ExpressionParser.ExpressionType expressionType, ExpressionParser.ConstantType constantType, ReportPublishing.PublishingContextStruct context, out bool userCollectionReferenced)
		{
			ExpressionParser.ExpressionContext expressionContext = context.CreateExpressionContext(expressionType, constantType, propertyName, dataSetName);
			return this.m_reportCT.ParseExpression(this.m_reader.ReadString(), expressionContext, out userCollectionReferenced);
		}

		// Token: 0x060069F2 RID: 27122 RVA: 0x001A849B File Offset: 0x001A669B
		private ExpressionInfo ReadExpression(string propertyName, string dataSetName, ExpressionParser.ExpressionType expressionType, ExpressionParser.ConstantType constantType, ReportPublishing.PublishingContextStruct context)
		{
			return this.ReadExpression(this.m_reader.ReadString(), propertyName, dataSetName, expressionType, constantType, context);
		}

		// Token: 0x060069F3 RID: 27123 RVA: 0x001A84B5 File Offset: 0x001A66B5
		private ExpressionInfo ReadExpression(string propertyName, ExpressionParser.ExpressionType expressionType, ExpressionParser.ConstantType constantType, ReportPublishing.PublishingContextStruct context)
		{
			return this.ReadExpression(propertyName, null, expressionType, constantType, context);
		}

		// Token: 0x060069F4 RID: 27124 RVA: 0x001A84C4 File Offset: 0x001A66C4
		private ExpressionInfo ReadExpression(string propertyName, ExpressionParser.ExpressionType expressionType, ExpressionParser.ConstantType constantType, ReportPublishing.PublishingContextStruct context, out bool computed)
		{
			ExpressionInfo expressionInfo = this.ReadExpression(propertyName, expressionType, constantType, context);
			if (ExpressionInfo.Types.Constant == expressionInfo.Type)
			{
				computed = false;
			}
			else
			{
				computed = true;
			}
			return expressionInfo;
		}

		// Token: 0x060069F5 RID: 27125 RVA: 0x001A84F4 File Offset: 0x001A66F4
		private ExpressionInfo ReadExpression(string propertyName, string dataSetName, ExpressionParser.ExpressionType expressionType, ExpressionParser.ConstantType constantType, ReportPublishing.PublishingContextStruct context, ExpressionParser.DetectionFlags flag, out bool reportParameterReferenced, out string reportParameterName)
		{
			ExpressionParser.ExpressionContext expressionContext = context.CreateExpressionContext(expressionType, constantType, propertyName, dataSetName);
			if (this.CheckUserProfileDependency())
			{
				flag |= ExpressionParser.DetectionFlags.UserReference;
			}
			bool flag2;
			ExpressionInfo expressionInfo = this.m_reportCT.ParseExpression(this.m_reader.ReadString(), expressionContext, flag, out reportParameterReferenced, out reportParameterName, out flag2);
			if (flag2)
			{
				this.SetUserProfileDependency();
			}
			return expressionInfo;
		}

		// Token: 0x060069F6 RID: 27126 RVA: 0x001A8544 File Offset: 0x001A6744
		private ExpressionInfo ReadExpression(string propertyName, string dataSetName, ExpressionParser.ExpressionType expressionType, ExpressionParser.ConstantType constantType, ReportPublishing.PublishingContextStruct context, ExpressionParser.DetectionFlags flag, out bool reportParameterReferenced, out string reportParameterName, out bool userCollectionReferenced)
		{
			ExpressionParser.ExpressionContext expressionContext = context.CreateExpressionContext(expressionType, constantType, propertyName, dataSetName);
			return this.m_reportCT.ParseExpression(this.m_reader.ReadString(), expressionContext, flag, out reportParameterReferenced, out reportParameterName, out userCollectionReferenced);
		}

		// Token: 0x060069F7 RID: 27127 RVA: 0x001A8580 File Offset: 0x001A6780
		private DataSet.Sensitivity ReadSensitivity()
		{
			string text = this.m_reader.ReadString();
			return (DataSet.Sensitivity)Enum.Parse(typeof(DataSet.Sensitivity), text, false);
		}

		// Token: 0x060069F8 RID: 27128 RVA: 0x001A85B0 File Offset: 0x001A67B0
		private CommandType ReadCommandType()
		{
			string text = this.m_reader.ReadString();
			return (CommandType)Enum.Parse(typeof(CommandType), text, false);
		}

		// Token: 0x060069F9 RID: 27129 RVA: 0x001A85E0 File Offset: 0x001A67E0
		private Filter.Operators ReadOperator()
		{
			string text = this.m_reader.ReadString();
			return (Filter.Operators)Enum.Parse(typeof(Filter.Operators), text, false);
		}

		// Token: 0x060069FA RID: 27130 RVA: 0x001A8610 File Offset: 0x001A6810
		private bool ReadDirection()
		{
			string text = this.m_reader.ReadString();
			return ReportProcessing.CompareWithInvariantCulture(text, "Ascending", false) == 0;
		}

		// Token: 0x060069FB RID: 27131 RVA: 0x001A8638 File Offset: 0x001A6838
		private bool ReadLayoutDirection()
		{
			string text = this.m_reader.ReadString();
			return ReportProcessing.CompareWithInvariantCulture(text, "RTL", false) == 0;
		}

		// Token: 0x060069FC RID: 27132 RVA: 0x001A8660 File Offset: 0x001A6860
		private bool ReadProjectionMode()
		{
			string text = this.m_reader.ReadString();
			return ReportProcessing.CompareWithInvariantCulture(text, "Perspective", false) == 0;
		}

		// Token: 0x060069FD RID: 27133 RVA: 0x001A8688 File Offset: 0x001A6888
		private bool ReadDrawingStyle()
		{
			string text = this.m_reader.ReadString();
			return ReportProcessing.CompareWithInvariantCulture(text, "Cube", false) == 0;
		}

		// Token: 0x060069FE RID: 27134 RVA: 0x001A86B0 File Offset: 0x001A68B0
		private Image.SourceType ReadSource()
		{
			string text = this.m_reader.ReadString();
			return (Image.SourceType)Enum.Parse(typeof(Image.SourceType), text, false);
		}

		// Token: 0x060069FF RID: 27135 RVA: 0x001A86E0 File Offset: 0x001A68E0
		private Image.Sizings ReadSizing()
		{
			string text = this.m_reader.ReadString();
			return (Image.Sizings)Enum.Parse(typeof(Image.Sizings), text, false);
		}

		// Token: 0x06006A00 RID: 27136 RVA: 0x001A8710 File Offset: 0x001A6910
		private bool ReadDataElementStyle()
		{
			string text = this.m_reader.ReadString();
			return ReportProcessing.CompareWithInvariantCulture(text, "AttributeNormal", false) == 0;
		}

		// Token: 0x06006A01 RID: 27137 RVA: 0x001A8738 File Offset: 0x001A6938
		private ReportItem.DataElementStylesRDL ReadDataElementStyleRDL()
		{
			string text = this.m_reader.ReadString();
			return (ReportItem.DataElementStylesRDL)Enum.Parse(typeof(ReportItem.DataElementStylesRDL), text, false);
		}

		// Token: 0x06006A02 RID: 27138 RVA: 0x001A8768 File Offset: 0x001A6968
		private ReportItem.DataElementOutputTypesRDL ReadDataElementOutputRDL()
		{
			string text = this.m_reader.ReadString();
			return (ReportItem.DataElementOutputTypesRDL)Enum.Parse(typeof(ReportItem.DataElementOutputTypesRDL), text, false);
		}

		// Token: 0x06006A03 RID: 27139 RVA: 0x001A8798 File Offset: 0x001A6998
		private DataElementOutputTypes ReadDataElementOutput()
		{
			string text = this.m_reader.ReadString();
			return (DataElementOutputTypes)Enum.Parse(typeof(DataElementOutputTypes), text, false);
		}

		// Token: 0x06006A04 RID: 27140 RVA: 0x001A87C8 File Offset: 0x001A69C8
		private Subtotal.PositionType ReadPosition()
		{
			string text = this.m_reader.ReadString();
			return (Subtotal.PositionType)Enum.Parse(typeof(Subtotal.PositionType), text, false);
		}

		// Token: 0x06006A05 RID: 27141 RVA: 0x001A87F8 File Offset: 0x001A69F8
		private ChartDataLabel.Positions ReadDataLabelPosition()
		{
			string text = this.m_reader.ReadString();
			return (ChartDataLabel.Positions)Enum.Parse(typeof(ChartDataLabel.Positions), text, false);
		}

		// Token: 0x06006A06 RID: 27142 RVA: 0x001A8828 File Offset: 0x001A6A28
		private Chart.ChartTypes ReadChartType()
		{
			string text = this.m_reader.ReadString();
			return (Chart.ChartTypes)Enum.Parse(typeof(Chart.ChartTypes), text, false);
		}

		// Token: 0x06006A07 RID: 27143 RVA: 0x001A8858 File Offset: 0x001A6A58
		private Chart.ChartSubTypes ReadChartSubType()
		{
			string text = this.m_reader.ReadString();
			return (Chart.ChartSubTypes)Enum.Parse(typeof(Chart.ChartSubTypes), text, false);
		}

		// Token: 0x06006A08 RID: 27144 RVA: 0x001A8888 File Offset: 0x001A6A88
		private Chart.ChartPalette ReadChartPalette()
		{
			string text = this.m_reader.ReadString();
			return (Chart.ChartPalette)Enum.Parse(typeof(Chart.ChartPalette), text, false);
		}

		// Token: 0x06006A09 RID: 27145 RVA: 0x001A88B8 File Offset: 0x001A6AB8
		private ChartTitle.Positions ReadChartTitlePosition()
		{
			string text = this.m_reader.ReadString();
			return (ChartTitle.Positions)Enum.Parse(typeof(ChartTitle.Positions), text, false);
		}

		// Token: 0x06006A0A RID: 27146 RVA: 0x001A88E8 File Offset: 0x001A6AE8
		private Legend.Positions ReadLegendPosition()
		{
			string text = this.m_reader.ReadString();
			return (Legend.Positions)Enum.Parse(typeof(Legend.Positions), text, false);
		}

		// Token: 0x06006A0B RID: 27147 RVA: 0x001A8918 File Offset: 0x001A6B18
		private Legend.LegendLayout ReadLegendLayout()
		{
			string text = this.m_reader.ReadString();
			return (Legend.LegendLayout)Enum.Parse(typeof(Legend.LegendLayout), text, false);
		}

		// Token: 0x06006A0C RID: 27148 RVA: 0x001A8948 File Offset: 0x001A6B48
		private MultiChart.Layouts ReadLayout()
		{
			string text = this.m_reader.ReadString();
			return (MultiChart.Layouts)Enum.Parse(typeof(MultiChart.Layouts), text, false);
		}

		// Token: 0x06006A0D RID: 27149 RVA: 0x001A8978 File Offset: 0x001A6B78
		private Axis.TickMarks ReadAxisTickMarks()
		{
			string text = this.m_reader.ReadString();
			return (Axis.TickMarks)Enum.Parse(typeof(Axis.TickMarks), text, false);
		}

		// Token: 0x06006A0E RID: 27150 RVA: 0x001A89A8 File Offset: 0x001A6BA8
		private ThreeDProperties.ShadingTypes ReadShading()
		{
			string text = this.m_reader.ReadString();
			return (ThreeDProperties.ShadingTypes)Enum.Parse(typeof(ThreeDProperties.ShadingTypes), text, false);
		}

		// Token: 0x06006A0F RID: 27151 RVA: 0x001A89D8 File Offset: 0x001A6BD8
		private ChartDataPoint.MarkerTypes ReadMarkerType()
		{
			string text = this.m_reader.ReadString();
			return (ChartDataPoint.MarkerTypes)Enum.Parse(typeof(ChartDataPoint.MarkerTypes), text, false);
		}

		// Token: 0x06006A10 RID: 27152 RVA: 0x001A8A08 File Offset: 0x001A6C08
		private bool ReadPlotType()
		{
			string text = this.m_reader.ReadString();
			return ReportProcessing.CompareWithInvariantCulture(text, "Line", false) == 0;
		}

		// Token: 0x06006A11 RID: 27153 RVA: 0x001A8A30 File Offset: 0x001A6C30
		private ReportPublishing.StyleInformation ReadStyle(ReportPublishing.PublishingContextStruct context, out bool computed)
		{
			computed = false;
			ReportPublishing.StyleInformation styleInformation = new ReportPublishing.StyleInformation();
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if ("Style" == this.m_reader.LocalName)
							{
								flag = true;
							}
						}
					}
					else
					{
						bool flag2 = false;
						string localName = this.m_reader.LocalName;
						if (localName != null)
						{
							switch (localName.Length)
							{
							case 5:
								if (!(localName == "Color"))
								{
									goto IL_04E4;
								}
								break;
							case 6:
								if (!(localName == "Format"))
								{
									goto IL_04E4;
								}
								break;
							case 7:
							case 16:
							case 17:
							case 18:
							case 19:
							case 20:
							case 21:
							case 23:
							case 24:
							case 25:
								goto IL_04E4;
							case 8:
							{
								char c = localName[0];
								if (c != 'C')
								{
									if (c != 'F')
									{
										if (c != 'L')
										{
											goto IL_04E4;
										}
										if (!(localName == "Language"))
										{
											goto IL_04E4;
										}
									}
									else if (!(localName == "FontSize"))
									{
										goto IL_04E4;
									}
								}
								else if (!(localName == "Calendar"))
								{
									goto IL_04E4;
								}
								break;
							}
							case 9:
							{
								char c = localName[0];
								if (c != 'D')
								{
									if (c != 'F')
									{
										if (c != 'T')
										{
											goto IL_04E4;
										}
										if (!(localName == "TextAlign"))
										{
											goto IL_04E4;
										}
									}
									else if (!(localName == "FontStyle"))
									{
										goto IL_04E4;
									}
								}
								else if (!(localName == "Direction"))
								{
									goto IL_04E4;
								}
								break;
							}
							case 10:
							{
								char c = localName[4];
								if (c <= 'H')
								{
									if (c != 'F')
									{
										if (c != 'H')
										{
											goto IL_04E4;
										}
										if (!(localName == "LineHeight"))
										{
											goto IL_04E4;
										}
									}
									else if (!(localName == "FontFamily"))
									{
										goto IL_04E4;
									}
								}
								else if (c != 'W')
								{
									if (c != 'i')
									{
										goto IL_04E4;
									}
									if (!(localName == "PaddingTop"))
									{
										goto IL_04E4;
									}
								}
								else if (!(localName == "FontWeight"))
								{
									goto IL_04E4;
								}
								break;
							}
							case 11:
							{
								char c = localName[7];
								if (c > 'M')
								{
									if (c != 'i')
									{
										if (c != 'o')
										{
											if (c != 't')
											{
												goto IL_04E4;
											}
											if (!(localName == "BorderStyle"))
											{
												goto IL_04E4;
											}
										}
										else if (!(localName == "BorderColor"))
										{
											goto IL_04E4;
										}
									}
									else if (!(localName == "BorderWidth"))
									{
										goto IL_04E4;
									}
									this.ReadBorderAttribute(this.m_reader.LocalName, styleInformation, context, out flag2);
									goto IL_04E4;
								}
								if (c != 'B')
								{
									if (c != 'L')
									{
										if (c != 'M')
										{
											goto IL_04E4;
										}
										if (!(localName == "WritingMode"))
										{
											goto IL_04E4;
										}
									}
									else if (!(localName == "PaddingLeft"))
									{
										goto IL_04E4;
									}
								}
								else if (!(localName == "UnicodeBiDi"))
								{
									goto IL_04E4;
								}
								break;
							}
							case 12:
								if (!(localName == "PaddingRight"))
								{
									goto IL_04E4;
								}
								break;
							case 13:
							{
								char c = localName[0];
								if (c != 'P')
								{
									if (c != 'V')
									{
										goto IL_04E4;
									}
									if (!(localName == "VerticalAlign"))
									{
										goto IL_04E4;
									}
								}
								else if (!(localName == "PaddingBottom"))
								{
									goto IL_04E4;
								}
								break;
							}
							case 14:
							{
								char c = localName[0];
								if (c != 'N')
								{
									if (c != 'T')
									{
										goto IL_04E4;
									}
									if (!(localName == "TextDecoration"))
									{
										goto IL_04E4;
									}
								}
								else
								{
									if (!(localName == "NumeralVariant"))
									{
										goto IL_04E4;
									}
									styleInformation.AddAttribute(this.m_reader.LocalName, this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.Integer, context, out flag2));
									goto IL_04E4;
								}
								break;
							}
							case 15:
							{
								char c = localName[10];
								if (c != 'C')
								{
									if (c != 'I')
									{
										if (c != 'g')
										{
											goto IL_04E4;
										}
										if (!(localName == "NumeralLanguage"))
										{
											goto IL_04E4;
										}
									}
									else
									{
										if (!(localName == "BackgroundImage"))
										{
											goto IL_04E4;
										}
										this.ReadBackgroundImage(styleInformation, context, out flag2);
										goto IL_04E4;
									}
								}
								else if (!(localName == "BackgroundColor"))
								{
									goto IL_04E4;
								}
								break;
							}
							case 22:
								if (!(localName == "BackgroundGradientType"))
								{
									goto IL_04E4;
								}
								break;
							case 26:
								if (!(localName == "BackgroundGradientEndColor"))
								{
									goto IL_04E4;
								}
								break;
							default:
								goto IL_04E4;
							}
							styleInformation.AddAttribute(this.m_reader.LocalName, this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag2));
						}
						IL_04E4:
						computed = computed || flag2;
					}
				}
				while (!flag);
			}
			return styleInformation;
		}

		// Token: 0x06006A12 RID: 27154 RVA: 0x001A8F4C File Offset: 0x001A714C
		private ReportPublishing.StyleInformation ReadStyle(ReportPublishing.PublishingContextStruct context)
		{
			bool flag;
			return this.ReadStyle(context, out flag);
		}

		// Token: 0x06006A13 RID: 27155 RVA: 0x001A8F64 File Offset: 0x001A7164
		private void ReadBorderAttribute(string borderAttribute, ReportPublishing.StyleInformation styleInfo, ReportPublishing.PublishingContextStruct context, out bool computed)
		{
			computed = false;
			if (!this.m_reader.IsEmptyElement)
			{
				bool flag = false;
				do
				{
					this.m_reader.Read();
					XmlNodeType nodeType = this.m_reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if (borderAttribute == this.m_reader.LocalName)
							{
								flag = true;
							}
						}
					}
					else
					{
						bool flag2 = false;
						string localName = this.m_reader.LocalName;
						if (localName == "Default" || localName == "Left" || localName == "Right" || localName == "Top" || localName == "Bottom")
						{
							string text;
							if ("Default" == this.m_reader.LocalName)
							{
								text = string.Empty;
							}
							else
							{
								text = this.m_reader.LocalName;
							}
							styleInfo.AddAttribute(borderAttribute + text, this.ReadExpression(borderAttribute, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag2));
						}
						computed = computed || flag2;
					}
				}
				while (!flag);
			}
		}

		// Token: 0x06006A14 RID: 27156 RVA: 0x001A9070 File Offset: 0x001A7270
		private void ReadBackgroundImage(ReportPublishing.StyleInformation styleInfo, ReportPublishing.PublishingContextStruct context, out bool computed)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			do
			{
				this.m_reader.Read();
				XmlNodeType nodeType = this.m_reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ("BackgroundImage" == this.m_reader.LocalName)
						{
							flag4 = true;
						}
					}
				}
				else
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Source"))
					{
						if (!(localName == "Value"))
						{
							if (!(localName == "MIMEType"))
							{
								if (localName == "BackgroundRepeat")
								{
									styleInfo.AddAttribute(this.m_reader.LocalName, this.ReadExpression(this.m_reader.LocalName, ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag3));
								}
							}
							else
							{
								styleInfo.AddAttribute("BackgroundImageMIMEType", this.ReadExpression("BackgroundImageMIMEType", ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag2));
							}
						}
						else
						{
							styleInfo.AddAttribute("BackgroundImageValue", this.ReadExpression("BackgroundImageValue", ExpressionParser.ExpressionType.General, ExpressionParser.ConstantType.String, context, out flag));
						}
					}
					else
					{
						ExpressionInfo expressionInfo = new ExpressionInfo();
						expressionInfo.Type = ExpressionInfo.Types.Constant;
						expressionInfo.IntValue = (int)this.ReadSource();
						styleInfo.AddAttribute("BackgroundImageSource", expressionInfo);
						if (expressionInfo.IntValue == 0)
						{
							this.m_hasExternalImages = true;
						}
					}
				}
			}
			while (!flag4);
			computed = flag || flag2 || flag3;
			this.m_hasImageStreams = true;
		}

		// Token: 0x06006A15 RID: 27157 RVA: 0x001A91C9 File Offset: 0x001A73C9
		private string ReadSize()
		{
			return this.m_reader.ReadString();
		}

		// Token: 0x06006A16 RID: 27158 RVA: 0x001A91D8 File Offset: 0x001A73D8
		private void Phase2()
		{
			if (1 < this.m_dataSets.Count)
			{
				this.m_reportCT.ConvertFields2ComplexExpr();
			}
			else
			{
				this.m_report.OneDataSetName = ((this.m_dataSets.Count == 1) ? this.m_dataSets[0].Name : null);
			}
			if (0 < this.m_textBoxesWithUserSortTarget.Count)
			{
				for (int i = 0; i < this.m_textBoxesWithUserSortTarget.Count; i++)
				{
					EndUserSort userSort = this.m_textBoxesWithUserSortTarget[i].UserSort;
					ISortFilterScope sortFilterScope = this.m_reportScopes[userSort.SortTargetString] as ISortFilterScope;
					if (sortFilterScope != null)
					{
						userSort.SetSortTarget(sortFilterScope);
					}
				}
			}
			this.m_report.MergeOnePass = !this.m_hasGrouping && !this.m_hasSorting && !this.m_aggregateInDetailSections && !this.m_reportCT.BodyRefersToReportItems && !this.m_reportCT.ValueReferencedGlobal && !this.m_subReportMergeTransactions && !this.m_hasUserSort;
			this.m_report.PageMergeOnePass = this.m_report.PageAggregates.Count == 0 && !this.m_reportCT.PageSectionRefersToReportItems;
			this.m_report.SubReportMergeTransactions = this.m_subReportMergeTransactions;
			this.m_report.NeedPostGroupProcessing = this.m_hasSorting | this.m_hasGroupFilters;
			this.m_report.HasSpecialRecursiveAggregates = this.m_hasSpecialRecursiveAggregates;
			this.m_report.HasReportItemReferences = this.m_reportCT.BodyRefersToReportItems;
			this.m_report.HasImageStreams = this.m_hasImageStreams;
			this.m_report.HasBookmarks = this.m_hasBookmarks;
			this.m_report.HasLabels = this.m_hasLabels;
			this.m_report.HasUserSortFilter = this.m_hasUserSort;
		}

		// Token: 0x06006A17 RID: 27159 RVA: 0x001A9398 File Offset: 0x001A7598
		private void Phase3(ICatalogItemContext reportContext, out ParameterInfoCollection parameters, AppDomain compilationTempAppDomain, bool generateExpressionHostWithRefusedPermissions)
		{
			try
			{
				this.m_reportCT.Builder.ReportStart();
				this.m_report.LastAggregateID = this.m_reportCT.LastAggregateID;
				InitializationContext initializationContext = new InitializationContext(reportContext, this.m_hasFilters, this.m_dataSourceNames, this.m_dataSets, this.m_dynamicParameters, this.m_dataSetQueryInfo, this.m_errorContext, this.m_reportCT.Builder, this.m_report, this.m_reportLanguage, this.m_reportScopes, this.m_hasUserSortPeerScopes, this.m_dataRegionCount);
				this.m_report.Initialize(initializationContext);
				bool flag = false;
				parameters = new ParameterInfoCollection();
				ParameterDefList parameters2 = this.m_report.Parameters;
				if (parameters2 != null && parameters2.Count > 0)
				{
					initializationContext.InitializeParameters(this.m_report.Parameters, this.m_dataSets);
					for (int i = 0; i < parameters2.Count; i++)
					{
						ParameterDef parameterDef = parameters2[i];
						if (parameterDef.UsedInQueryAsDefined == ParameterBase.UsedInQueryType.Auto)
						{
							if (this.m_parametersNotUsedInQuery)
							{
								if (this.m_usedInQueryInfos.Contains(parameterDef.Name))
								{
									parameterDef.UsedInQuery = true;
								}
								else
								{
									parameterDef.UsedInQuery = false;
									flag = true;
								}
							}
							else
							{
								parameterDef.UsedInQuery = true;
							}
						}
						else if (parameterDef.UsedInQueryAsDefined == ParameterBase.UsedInQueryType.False)
						{
							flag = true;
							parameterDef.UsedInQuery = false;
						}
						if (parameterDef.UsedInQuery && (this.m_userReferenceLocation & UserLocationFlags.ReportQueries) == (UserLocationFlags)0 && this.m_reportParamUserProfile.Contains(parameterDef.Name))
						{
							this.m_userReferenceLocation |= UserLocationFlags.ReportQueries;
						}
						parameterDef.Initialize(initializationContext);
						ParameterInfo parameterInfo = new ParameterInfo(parameterDef);
						if (parameterDef.Dependencies != null && parameterDef.Dependencies.Count > 0)
						{
							IDictionaryEnumerator enumerator = parameterDef.Dependencies.GetEnumerator();
							ParameterDefList parameterDefList = new ParameterDefList();
							ParameterInfoCollection parameterInfoCollection = new ParameterInfoCollection();
							while (enumerator.MoveNext())
							{
								int num = (int)enumerator.Value;
								parameterDefList.Add(parameters2[num]);
								parameterInfoCollection.Add(parameters[num]);
								if (parameterDef.UsedInQuery)
								{
									parameters[num].UsedInQuery = true;
								}
							}
							parameterDef.DependencyList = parameterDefList;
							parameterInfo.DependencyList = parameterInfoCollection;
						}
						if (parameterDef.ValidValuesDataSource != null)
						{
							parameterInfo.DynamicValidValues = true;
						}
						else if (parameterDef.ValidValuesValueExpressions != null)
						{
							int count = parameterDef.ValidValuesValueExpressions.Count;
							int num2 = 0;
							while (num2 < count && !parameterInfo.DynamicValidValues)
							{
								ExpressionInfo expressionInfo = parameterDef.ValidValuesValueExpressions[num2];
								ExpressionInfo expressionInfo2 = parameterDef.ValidValuesLabelExpressions[num2];
								if ((expressionInfo != null && ExpressionInfo.Types.Constant != expressionInfo.Type) || (expressionInfo2 != null && ExpressionInfo.Types.Constant != expressionInfo2.Type))
								{
									parameterInfo.DynamicValidValues = true;
								}
								num2++;
							}
							if (!parameterInfo.DynamicValidValues)
							{
								parameterInfo.ValidValues = new ValidValueList(count);
								for (int j = 0; j < count; j++)
								{
									ExpressionInfo expressionInfo3 = parameterDef.ValidValuesValueExpressions[j];
									ExpressionInfo expressionInfo4 = parameterDef.ValidValuesLabelExpressions[j];
									parameterInfo.AddValidValue((expressionInfo3 != null) ? expressionInfo3.Value : null, (expressionInfo4 != null) ? expressionInfo4.Value : null, this.m_errorContext, CultureInfo.InvariantCulture);
								}
							}
						}
						parameterInfo.DynamicDefaultValue = parameterDef.DefaultDataSource != null || parameterDef.DefaultExpressions != null;
						parameterInfo.Values = parameterDef.DefaultValues;
						parameters.Add(parameterInfo);
					}
				}
				this.m_parametersNotUsedInQuery = flag;
				this.m_report.ParametersNotUsedInQuery = this.m_parametersNotUsedInQuery;
				this.m_reportCT.Builder.ReportEnd();
				if (!this.m_errorContext.HasError)
				{
					this.m_report.CompiledCode = this.m_reportCT.Compile(this.m_report, compilationTempAppDomain, generateExpressionHostWithRefusedPermissions);
				}
				if (this.m_report.MergeOnePass)
				{
					int num3 = 0;
					for (int k = 0; k < this.m_dataSets.Count; k++)
					{
						if (!this.m_dataSets[k].UsedOnlyInParameters)
						{
							num3++;
							if (1 < num3)
							{
								this.m_report.MergeOnePass = false;
								break;
							}
						}
					}
				}
			}
			finally
			{
				this.m_reportCT = null;
			}
		}

		// Token: 0x06006A18 RID: 27160 RVA: 0x001A97D0 File Offset: 0x001A79D0
		private void Phase4()
		{
			this.PopulateReportItemCollections();
			this.CompactAggregates();
			this.CompactRunningValues();
		}

		// Token: 0x06006A19 RID: 27161 RVA: 0x001A97E4 File Offset: 0x001A79E4
		private void PopulateReportItemCollections()
		{
			try
			{
				Global.Tracer.Assert(this.m_reportItemCollectionList != null);
				for (int i = 0; i < this.m_reportItemCollectionList.Count; i++)
				{
					((ReportItemCollection)this.m_reportItemCollectionList[i]).Populate(this.m_errorContext);
				}
			}
			finally
			{
				this.m_reportItemCollectionList = null;
			}
		}

		// Token: 0x06006A1A RID: 27162 RVA: 0x001A9850 File Offset: 0x001A7A50
		private void CompactAggregates()
		{
			try
			{
				Hashtable hashtable = new Hashtable();
				for (int i = 0; i < this.m_aggregateHolderList.Count; i++)
				{
					IAggregateHolder aggregateHolder = (IAggregateHolder)this.m_aggregateHolderList[i];
					Global.Tracer.Assert(aggregateHolder != null);
					DataAggregateInfoList[] aggregateLists = aggregateHolder.GetAggregateLists();
					Global.Tracer.Assert(aggregateLists != null);
					Global.Tracer.Assert(aggregateLists.Length != 0);
					this.CompactAggregates(aggregateLists, hashtable);
					if (this.CompactAggregates(aggregateHolder.GetPostSortAggregateLists(), hashtable))
					{
						this.m_report.HasPostSortAggregates = true;
					}
					if (aggregateHolder is Grouping && this.CompactAggregates(((Grouping)aggregateHolder).RecursiveAggregates, hashtable))
					{
						this.m_report.NeedPostGroupProcessing = true;
					}
					aggregateHolder.ClearIfEmpty();
				}
			}
			finally
			{
				this.m_aggregateHolderList = null;
			}
		}

		// Token: 0x06006A1B RID: 27163 RVA: 0x001A9930 File Offset: 0x001A7B30
		private bool CompactAggregates(DataAggregateInfoList[] aggregateLists, Hashtable aggregateHashByType)
		{
			bool flag = false;
			if (aggregateLists != null)
			{
				foreach (DataAggregateInfoList dataAggregateInfoList in aggregateLists)
				{
					Global.Tracer.Assert(dataAggregateInfoList != null);
					if (this.CompactAggregates(dataAggregateInfoList, aggregateHashByType))
					{
						flag = true;
					}
					aggregateHashByType.Clear();
				}
			}
			return flag;
		}

		// Token: 0x06006A1C RID: 27164 RVA: 0x001A9978 File Offset: 0x001A7B78
		private bool CompactAggregates(DataAggregateInfoList aggregateList, Hashtable aggregateHashByType)
		{
			bool flag = false;
			for (int i = aggregateList.Count - 1; i >= 0; i--)
			{
				flag = true;
				DataAggregateInfo dataAggregateInfo = aggregateList[i];
				Global.Tracer.Assert(dataAggregateInfo != null);
				if (!dataAggregateInfo.IsCopied)
				{
					Hashtable hashtable = (Hashtable)aggregateHashByType[dataAggregateInfo.AggregateType];
					if (hashtable == null)
					{
						hashtable = new Hashtable();
						aggregateHashByType[dataAggregateInfo.AggregateType] = hashtable;
					}
					DataAggregateInfo dataAggregateInfo2 = (DataAggregateInfo)hashtable[dataAggregateInfo.ExpressionText];
					if (dataAggregateInfo2 == null)
					{
						hashtable[dataAggregateInfo.ExpressionText] = dataAggregateInfo;
					}
					else
					{
						if (dataAggregateInfo2.DuplicateNames == null)
						{
							dataAggregateInfo2.DuplicateNames = new StringList();
						}
						dataAggregateInfo2.DuplicateNames.Add(dataAggregateInfo.Name);
						aggregateList.RemoveAt(i);
					}
				}
			}
			return flag;
		}

		// Token: 0x06006A1D RID: 27165 RVA: 0x001A9A4C File Offset: 0x001A7C4C
		private void CompactRunningValues()
		{
			try
			{
				Hashtable hashtable = new Hashtable();
				for (int i = 0; i < this.m_runningValueHolderList.Count; i++)
				{
					IRunningValueHolder runningValueHolder = (IRunningValueHolder)this.m_runningValueHolderList[i];
					Global.Tracer.Assert(runningValueHolder != null);
					this.CompactRunningValueList(runningValueHolder.GetRunningValueList(), hashtable);
					if (runningValueHolder is OWCChart)
					{
						this.CompactRunningValueList(((OWCChart)runningValueHolder).DetailRunningValues, hashtable);
					}
					else if (runningValueHolder is Chart)
					{
						this.CompactRunningValueList(((Chart)runningValueHolder).CellRunningValues, hashtable);
					}
					else if (runningValueHolder is CustomReportItem)
					{
						this.CompactRunningValueList(((CustomReportItem)runningValueHolder).CellRunningValues, hashtable);
					}
					runningValueHolder.ClearIfEmpty();
				}
			}
			finally
			{
				this.m_runningValueHolderList = null;
			}
		}

		// Token: 0x06006A1E RID: 27166 RVA: 0x001A9B1C File Offset: 0x001A7D1C
		private void CompactRunningValueList(RunningValueInfoList runningValueList, Hashtable runningValueHashByType)
		{
			Global.Tracer.Assert(runningValueList != null);
			Global.Tracer.Assert(runningValueHashByType != null);
			for (int i = runningValueList.Count - 1; i >= 0; i--)
			{
				this.m_report.HasPostSortAggregates = true;
				RunningValueInfo runningValueInfo = runningValueList[i];
				Global.Tracer.Assert(runningValueInfo != null);
				ReportPublishing.AllowNullKeyHashtable allowNullKeyHashtable = (ReportPublishing.AllowNullKeyHashtable)runningValueHashByType[runningValueInfo.AggregateType];
				if (allowNullKeyHashtable == null)
				{
					allowNullKeyHashtable = new ReportPublishing.AllowNullKeyHashtable();
					runningValueHashByType[runningValueInfo.AggregateType] = allowNullKeyHashtable;
				}
				Hashtable hashtable = (Hashtable)allowNullKeyHashtable[runningValueInfo.Scope];
				if (hashtable == null)
				{
					hashtable = new Hashtable();
					allowNullKeyHashtable[runningValueInfo.Scope] = hashtable;
				}
				RunningValueInfo runningValueInfo2 = (RunningValueInfo)hashtable[runningValueInfo.ExpressionText];
				if (runningValueInfo2 == null)
				{
					hashtable[runningValueInfo.ExpressionText] = runningValueInfo;
				}
				else
				{
					if (runningValueInfo2.DuplicateNames == null)
					{
						runningValueInfo2.DuplicateNames = new StringList();
					}
					runningValueInfo2.DuplicateNames.Add(runningValueInfo.Name);
					runningValueList.RemoveAt(i);
				}
			}
			runningValueHashByType.Clear();
		}

		// Token: 0x06006A1F RID: 27167 RVA: 0x001A9C38 File Offset: 0x001A7E38
		internal static void CalculateChildrenDependencies(ReportItem reportItem)
		{
			ReportItemCollection reportItemCollection = null;
			if (!(reportItem is DataRegion) && !(reportItem is Rectangle) && !(reportItem is Report))
			{
				return;
			}
			if (reportItem is Rectangle)
			{
				reportItemCollection = ((Rectangle)reportItem).ReportItems;
			}
			else if (reportItem is List)
			{
				reportItemCollection = ((List)reportItem).ReportItems;
			}
			else if (reportItem is Report)
			{
				reportItemCollection = ((Report)reportItem).ReportItems;
			}
			if (reportItemCollection == null || reportItemCollection.Count < 1)
			{
				return;
			}
			for (int i = 0; i < reportItemCollection.Count; i++)
			{
				ReportItem reportItem2 = reportItemCollection[i];
				double num = reportItem2.TopValue + reportItem2.HeightValue;
				double num2 = -1.0;
				bool flag = ReportPublishing.HasPageBreakAtStart(reportItem2);
				for (int j = i + 1; j < reportItemCollection.Count; j++)
				{
					ReportItem reportItem3 = reportItemCollection[j];
					if (reportItem3.TopValue >= reportItem2.TopValue)
					{
						bool flag2 = false;
						if (flag && reportItem3.TopValue >= reportItem2.TopValue && reportItem3.TopValue <= num)
						{
							flag2 = true;
						}
						if (num2 >= 0.0 && num2 <= reportItem3.TopValue + 0.0009)
						{
							break;
						}
						if (!reportItemCollection.IsReportItemComputed(j))
						{
							flag2 = true;
						}
						bool flag3 = false;
						if (num <= reportItem3.TopValue + 0.0009 || flag2)
						{
							flag3 = true;
							if (!flag2 && (num2 < 0.0 || num2 > reportItem3.TopValue + reportItem3.HeightValue))
							{
								num2 = reportItem3.TopValue + reportItem3.HeightValue;
							}
						}
						else if (!flag2 && i + 1 == j && reportItem3.DistanceBeforeTop == 0)
						{
							flag3 = true;
						}
						if (flag3)
						{
							if (reportItem3.SiblingAboveMe == null)
							{
								reportItem3.SiblingAboveMe = new IntList();
							}
							reportItem3.SiblingAboveMe.Add(i);
						}
					}
				}
				ReportPublishing.CalculateChildrenDependencies(reportItem2);
			}
		}

		// Token: 0x06006A20 RID: 27168 RVA: 0x001A9E34 File Offset: 0x001A8034
		private static bool HasPageBreakAtStart(ReportItem reportItem)
		{
			if (!(reportItem is DataRegion) && !(reportItem is Rectangle))
			{
				return false;
			}
			if (reportItem is List)
			{
				return ((List)reportItem).PropagatedPageBreakAtStart;
			}
			if (reportItem is Table)
			{
				return ((Table)reportItem).PropagatedPageBreakAtStart;
			}
			if (reportItem is Matrix)
			{
				return ((Matrix)reportItem).PropagatedPageBreakAtStart;
			}
			IPageBreakItem pageBreakItem = (IPageBreakItem)reportItem;
			return pageBreakItem != null && !pageBreakItem.IgnorePageBreaks() && pageBreakItem.HasPageBreaks(true);
		}

		// Token: 0x06006A21 RID: 27169 RVA: 0x001A9EAC File Offset: 0x001A80AC
		internal static void CalculateChildrenPostions(ReportItem reportItem)
		{
			ReportItemCollection reportItemCollection = null;
			if (!(reportItem is DataRegion) && !(reportItem is Rectangle) && !(reportItem is Report))
			{
				return;
			}
			if (reportItem is Rectangle)
			{
				reportItemCollection = ((Rectangle)reportItem).ReportItems;
			}
			else if (reportItem is List)
			{
				reportItemCollection = ((List)reportItem).ReportItems;
			}
			else if (reportItem is Report)
			{
				reportItemCollection = ((Report)reportItem).ReportItems;
				if (-1 == reportItem.DistanceFromReportTop)
				{
					reportItem.DistanceFromReportTop = 0;
				}
			}
			if (reportItemCollection == null)
			{
				return;
			}
			double heightValue = reportItem.HeightValue;
			for (int i = 0; i < reportItemCollection.Count; i++)
			{
				ReportItem reportItem2 = reportItemCollection[i];
				reportItem2.DistanceBeforeTop = (int)reportItem2.TopValue;
				double topValue = reportItem2.TopValue;
				double heightValue2 = reportItem2.HeightValue;
				reportItem2.DistanceFromReportTop = reportItem.DistanceFromReportTop + (int)reportItem2.TopValue;
				if (reportItem2 is List)
				{
					((List)reportItem2).IsListMostInner = ReportPublishing.IsListMostInner(((List)reportItem2).ReportItems);
				}
				for (int j = 0; j < i; j++)
				{
					ReportItem reportItem3 = reportItemCollection[j];
					double num = reportItem3.TopValue + reportItem3.HeightValue;
					if (num < reportItem2.TopValue && reportItem2.LeftValue <= reportItem3.LeftValue + reportItem3.WidthValue && reportItem2.LeftValue + reportItem2.WidthValue >= reportItem3.LeftValue)
					{
						reportItem2.DistanceBeforeTop = Math.Min(reportItem2.DistanceBeforeTop, (int)(reportItem2.TopValue - num));
					}
					else if (0.5 > Math.Abs(reportItemCollection[j].TopValue - reportItem2.TopValue))
					{
						reportItem2.DistanceBeforeTop = 0;
					}
				}
				ReportPublishing.CalculateChildrenPostions(reportItem2);
			}
		}

		// Token: 0x06006A22 RID: 27170 RVA: 0x001AA050 File Offset: 0x001A8250
		private static bool IsListMostInner(ReportItemCollection reportItemCollection)
		{
			if (reportItemCollection == null || reportItemCollection.Count < 1)
			{
				return true;
			}
			for (int i = 0; i < reportItemCollection.Count; i++)
			{
				ReportItem reportItem = reportItemCollection[i];
				if (reportItem is DataRegion)
				{
					return false;
				}
				if (reportItem is Rectangle && ((Rectangle)reportItem).ReportItems.ComputedReportItems != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04003588 RID: 13704
		private bool m_static;

		// Token: 0x04003589 RID: 13705
		private bool m_interactive;

		// Token: 0x0400358A RID: 13706
		private int m_idCounter;

		// Token: 0x0400358B RID: 13707
		private ReportPublishing.RmlValidatingReader m_reader;

		// Token: 0x0400358C RID: 13708
		private CLSUniqueNameValidator m_reportItemNames;

		// Token: 0x0400358D RID: 13709
		private ScopeNameValidator m_scopeNames;

		// Token: 0x0400358E RID: 13710
		private ImageStreamNames m_imageStreamNames;

		// Token: 0x0400358F RID: 13711
		private ICatalogItemContext m_reportContext;

		// Token: 0x04003590 RID: 13712
		private ReportProcessing.CreateReportChunk m_createChunkCallback;

		// Token: 0x04003591 RID: 13713
		private ReportProcessing.CheckSharedDataSource m_checkDataSourceCallback;

		// Token: 0x04003592 RID: 13714
		private string m_description;

		// Token: 0x04003593 RID: 13715
		private DataSourceInfoCollection m_dataSources;

		// Token: 0x04003594 RID: 13716
		private SubReportList m_subReports;

		// Token: 0x04003595 RID: 13717
		private UserLocationFlags m_reportLocationFlags = UserLocationFlags.ReportBody;

		// Token: 0x04003596 RID: 13718
		private UserLocationFlags m_userReferenceLocation = UserLocationFlags.None;

		// Token: 0x04003597 RID: 13719
		private bool m_hasExternalImages;

		// Token: 0x04003598 RID: 13720
		private bool m_hasHyperlinks;

		// Token: 0x04003599 RID: 13721
		private bool m_pageSectionDrillthroughs;

		// Token: 0x0400359A RID: 13722
		private bool m_hasGrouping;

		// Token: 0x0400359B RID: 13723
		private bool m_hasSorting;

		// Token: 0x0400359C RID: 13724
		private bool m_hasUserSort;

		// Token: 0x0400359D RID: 13725
		private bool m_hasGroupFilters;

		// Token: 0x0400359E RID: 13726
		private bool m_hasSpecialRecursiveAggregates;

		// Token: 0x0400359F RID: 13727
		private bool m_aggregateInDetailSections;

		// Token: 0x040035A0 RID: 13728
		private bool m_subReportMergeTransactions;

		// Token: 0x040035A1 RID: 13729
		private ReportCompileTime m_reportCT;

		// Token: 0x040035A2 RID: 13730
		private bool m_hasImageStreams;

		// Token: 0x040035A3 RID: 13731
		private bool m_hasLabels;

		// Token: 0x040035A4 RID: 13732
		private bool m_hasBookmarks;

		// Token: 0x040035A5 RID: 13733
		private TextBoxList m_textBoxesWithUserSortTarget = new TextBoxList();

		// Token: 0x040035A6 RID: 13734
		private bool m_hasFilters;

		// Token: 0x040035A7 RID: 13735
		private DataSetList m_dataSets = new DataSetList();

		// Token: 0x040035A8 RID: 13736
		private bool m_parametersNotUsedInQuery = true;

		// Token: 0x040035A9 RID: 13737
		private Hashtable m_usedInQueryInfos = new Hashtable();

		// Token: 0x040035AA RID: 13738
		private Hashtable m_reportParamUserProfile = new Hashtable();

		// Token: 0x040035AB RID: 13739
		private Hashtable m_dataSetQueryInfo = new Hashtable();

		// Token: 0x040035AC RID: 13740
		private ArrayList m_dynamicParameters = new ArrayList();

		// Token: 0x040035AD RID: 13741
		private CultureInfo m_reportLanguage;

		// Token: 0x040035AE RID: 13742
		private bool m_hasUserSortPeerScopes;

		// Token: 0x040035AF RID: 13743
		private Hashtable m_reportScopes = new Hashtable();

		// Token: 0x040035B0 RID: 13744
		private StringDictionary m_dataSourceNames = new StringDictionary();

		// Token: 0x040035B1 RID: 13745
		private int m_dataRegionCount;

		// Token: 0x040035B2 RID: 13746
		private ArrayList m_reportItemCollectionList = new ArrayList();

		// Token: 0x040035B3 RID: 13747
		private ArrayList m_aggregateHolderList = new ArrayList();

		// Token: 0x040035B4 RID: 13748
		private ArrayList m_runningValueHolderList = new ArrayList();

		// Token: 0x040035B5 RID: 13749
		private string m_targetRDLNamespace;

		// Token: 0x040035B6 RID: 13750
		private Report m_report;

		// Token: 0x040035B7 RID: 13751
		private PublishingErrorContext m_errorContext;

		// Token: 0x02000CDB RID: 3291
		private enum StyleOwnerType
		{
			// Token: 0x04004F1F RID: 20255
			Line = 1,
			// Token: 0x04004F20 RID: 20256
			Rectangle,
			// Token: 0x04004F21 RID: 20257
			Checkbox,
			// Token: 0x04004F22 RID: 20258
			Image,
			// Token: 0x04004F23 RID: 20259
			ActiveXControl,
			// Token: 0x04004F24 RID: 20260
			List,
			// Token: 0x04004F25 RID: 20261
			Matrix,
			// Token: 0x04004F26 RID: 20262
			Table,
			// Token: 0x04004F27 RID: 20263
			OWCChart,
			// Token: 0x04004F28 RID: 20264
			Body,
			// Token: 0x04004F29 RID: 20265
			Chart,
			// Token: 0x04004F2A RID: 20266
			Textbox,
			// Token: 0x04004F2B RID: 20267
			SubReport,
			// Token: 0x04004F2C RID: 20268
			Subtotal,
			// Token: 0x04004F2D RID: 20269
			PageSection
		}

		// Token: 0x02000CDC RID: 3292
		private struct PublishingContextStruct
		{
			// Token: 0x06008D1C RID: 36124 RVA: 0x0023E10D File Offset: 0x0023C30D
			internal PublishingContextStruct(LocationFlags location, ObjectType objectType, string objectName)
			{
				this.m_location = location;
				this.m_objectType = objectType;
				this.m_objectName = objectName;
			}

			// Token: 0x17002B51 RID: 11089
			// (get) Token: 0x06008D1D RID: 36125 RVA: 0x0023E124 File Offset: 0x0023C324
			// (set) Token: 0x06008D1E RID: 36126 RVA: 0x0023E12C File Offset: 0x0023C32C
			internal LocationFlags Location
			{
				get
				{
					return this.m_location;
				}
				set
				{
					this.m_location = value;
				}
			}

			// Token: 0x17002B52 RID: 11090
			// (get) Token: 0x06008D1F RID: 36127 RVA: 0x0023E135 File Offset: 0x0023C335
			// (set) Token: 0x06008D20 RID: 36128 RVA: 0x0023E13D File Offset: 0x0023C33D
			internal ObjectType ObjectType
			{
				get
				{
					return this.m_objectType;
				}
				set
				{
					this.m_objectType = value;
				}
			}

			// Token: 0x17002B53 RID: 11091
			// (get) Token: 0x06008D21 RID: 36129 RVA: 0x0023E146 File Offset: 0x0023C346
			// (set) Token: 0x06008D22 RID: 36130 RVA: 0x0023E14E File Offset: 0x0023C34E
			internal string ObjectName
			{
				get
				{
					return this.m_objectName;
				}
				set
				{
					this.m_objectName = value;
				}
			}

			// Token: 0x06008D23 RID: 36131 RVA: 0x0023E157 File Offset: 0x0023C357
			internal ExpressionParser.ExpressionContext CreateExpressionContext(ExpressionParser.ExpressionType expressionType, ExpressionParser.ConstantType constantType, string propertyName, string dataSetName)
			{
				return new ExpressionParser.ExpressionContext(expressionType, constantType, this.m_location, this.m_objectType, this.m_objectName, propertyName, dataSetName, false);
			}

			// Token: 0x06008D24 RID: 36132 RVA: 0x0023E176 File Offset: 0x0023C376
			internal ExpressionParser.ExpressionContext CreateExpressionContext(ExpressionParser.ExpressionType expressionType, ExpressionParser.ConstantType constantType, string propertyName, string dataSetName, bool parseExtended)
			{
				return new ExpressionParser.ExpressionContext(expressionType, constantType, this.m_location, this.m_objectType, this.m_objectName, propertyName, dataSetName, parseExtended);
			}

			// Token: 0x04004F2E RID: 20270
			private LocationFlags m_location;

			// Token: 0x04004F2F RID: 20271
			private ObjectType m_objectType;

			// Token: 0x04004F30 RID: 20272
			private string m_objectName;
		}

		// Token: 0x02000CDD RID: 3293
		private sealed class StyleInformation
		{
			// Token: 0x06008D25 RID: 36133 RVA: 0x0023E198 File Offset: 0x0023C398
			static StyleInformation()
			{
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BorderColor", 0);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BorderColorLeft", 1);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BorderColorRight", 2);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BorderColorTop", 3);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BorderColorBottom", 4);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BorderStyle", 5);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BorderStyleLeft", 6);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BorderStyleRight", 7);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BorderStyleTop", 8);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BorderStyleBottom", 9);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BorderWidth", 10);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BorderWidthLeft", 11);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BorderWidthRight", 12);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BorderWidthTop", 13);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BorderWidthBottom", 14);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BackgroundColor", 15);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BackgroundImageSource", 16);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BackgroundImageValue", 17);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BackgroundImageMIMEType", 18);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BackgroundRepeat", 19);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("FontStyle", 20);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("FontFamily", 21);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("FontSize", 22);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("FontWeight", 23);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("Format", 24);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("TextDecoration", 25);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("TextAlign", 26);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("VerticalAlign", 27);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("Color", 28);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("PaddingLeft", 29);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("PaddingRight", 30);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("PaddingTop", 31);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("PaddingBottom", 32);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("LineHeight", 33);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("Direction", 34);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("Language", 35);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("UnicodeBiDi", 36);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("Calendar", 37);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("NumeralLanguage", 38);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("NumeralVariant", 39);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("WritingMode", 40);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BackgroundGradientType", 41);
				ReportPublishing.StyleInformation.StyleNameIndexes.Add("BackgroundGradientEndColor", 42);
			}

			// Token: 0x17002B54 RID: 11092
			// (get) Token: 0x06008D26 RID: 36134 RVA: 0x0023E571 File Offset: 0x0023C771
			internal StringList Names
			{
				get
				{
					return this.m_names;
				}
			}

			// Token: 0x17002B55 RID: 11093
			// (get) Token: 0x06008D27 RID: 36135 RVA: 0x0023E579 File Offset: 0x0023C779
			internal ExpressionInfoList Values
			{
				get
				{
					return this.m_values;
				}
			}

			// Token: 0x06008D28 RID: 36136 RVA: 0x0023E581 File Offset: 0x0023C781
			internal void AddAttribute(string name, ExpressionInfo expression)
			{
				Global.Tracer.Assert(name != null);
				Global.Tracer.Assert(expression != null);
				this.m_names.Add(name);
				this.m_values.Add(expression);
			}

			// Token: 0x06008D29 RID: 36137 RVA: 0x0023E5BC File Offset: 0x0023C7BC
			internal void Filter(ReportPublishing.StyleOwnerType ownerType, bool hasNoRows)
			{
				Global.Tracer.Assert(this.m_names.Count == this.m_values.Count);
				int num = this.MapStyleOwnerTypeToIndex(ownerType, hasNoRows);
				for (int i = this.m_names.Count - 1; i >= 0; i--)
				{
					if (!this.Allow(this.MapStyleNameToIndex(this.m_names[i]), num))
					{
						this.m_names.RemoveAt(i);
						this.m_values.RemoveAt(i);
					}
				}
			}

			// Token: 0x06008D2A RID: 36138 RVA: 0x0023E63F File Offset: 0x0023C83F
			private int MapStyleOwnerTypeToIndex(ReportPublishing.StyleOwnerType ownerType, bool hasNoRows)
			{
				if (hasNoRows)
				{
					return 0;
				}
				if (ownerType - ReportPublishing.StyleOwnerType.Textbox <= 2)
				{
					return 0;
				}
				if (ownerType == ReportPublishing.StyleOwnerType.PageSection)
				{
					return 2;
				}
				return (int)ownerType;
			}

			// Token: 0x06008D2B RID: 36139 RVA: 0x0023E657 File Offset: 0x0023C857
			private int MapStyleNameToIndex(string name)
			{
				return (int)ReportPublishing.StyleInformation.StyleNameIndexes[name];
			}

			// Token: 0x06008D2C RID: 36140 RVA: 0x0023E669 File Offset: 0x0023C869
			private bool Allow(int styleName, int ownerType)
			{
				return ReportPublishing.StyleInformation.AllowStyleAttributeByType[styleName, ownerType];
			}

			// Token: 0x04004F31 RID: 20273
			private StringList m_names = new StringList();

			// Token: 0x04004F32 RID: 20274
			private ExpressionInfoList m_values = new ExpressionInfoList();

			// Token: 0x04004F33 RID: 20275
			private static Hashtable StyleNameIndexes = new Hashtable();

			// Token: 0x04004F34 RID: 20276
			private static bool[,] AllowStyleAttributeByType = new bool[,]
			{
				{
					true, true, true, true, true, true, true, true, true, true,
					true, true
				},
				{
					true, false, true, true, true, true, true, true, true, true,
					true, true
				},
				{
					true, false, true, true, true, true, true, true, true, true,
					true, true
				},
				{
					true, false, true, true, true, true, true, true, true, true,
					true, true
				},
				{
					true, false, true, true, true, true, true, true, true, true,
					true, true
				},
				{
					true, true, true, true, true, true, true, true, true, true,
					true, true
				},
				{
					true, false, true, true, true, true, true, true, true, true,
					true, true
				},
				{
					true, false, true, true, true, true, true, true, true, true,
					true, true
				},
				{
					true, false, true, true, true, true, true, true, true, true,
					true, true
				},
				{
					true, false, true, true, true, true, true, true, true, true,
					true, true
				},
				{
					true, true, true, true, true, true, true, true, true, true,
					true, true
				},
				{
					true, false, true, true, true, true, true, true, true, true,
					true, true
				},
				{
					true, false, true, true, true, true, true, true, true, true,
					true, true
				},
				{
					true, false, true, true, true, true, true, true, true, true,
					true, true
				},
				{
					true, false, true, true, true, true, true, true, true, true,
					true, true
				},
				{
					true, false, true, false, false, false, true, true, true, false,
					true, true
				},
				{
					true, false, true, false, false, false, true, true, true, false,
					true, true
				},
				{
					true, false, true, false, false, false, true, true, true, false,
					true, true
				},
				{
					true, false, true, false, false, false, true, true, true, false,
					true, true
				},
				{
					true, false, true, false, false, false, true, true, true, false,
					true, true
				},
				{
					true, false, false, false, false, false, false, false, false, false,
					false, true
				},
				{
					true, false, false, false, false, false, false, false, false, false,
					false, true
				},
				{
					true, false, false, false, false, false, false, false, false, false,
					false, true
				},
				{
					true, false, false, false, false, false, false, false, false, false,
					false, true
				},
				{
					true, false, false, false, false, false, false, false, false, false,
					false, true
				},
				{
					true, false, false, false, false, false, false, false, false, false,
					false, true
				},
				{
					true, false, false, false, false, false, false, false, false, false,
					false, false
				},
				{
					true, false, false, false, false, false, false, false, false, false,
					false, false
				},
				{
					true, false, false, false, false, false, false, false, false, false,
					false, true
				},
				{
					true, false, false, false, true, false, false, false, false, false,
					false, false
				},
				{
					true, false, false, false, true, false, false, false, false, false,
					false, false
				},
				{
					true, false, false, false, true, false, false, false, false, false,
					false, false
				},
				{
					true, false, false, false, true, false, false, false, false, false,
					false, false
				},
				{
					true, false, false, false, false, false, false, false, false, false,
					false, false
				},
				{
					true, false, false, false, false, false, false, false, false, false,
					false, true
				},
				{
					true, false, false, false, false, false, false, false, false, false,
					false, true
				},
				{
					true, false, false, false, false, false, false, false, false, false,
					false, false
				},
				{
					true, false, false, false, false, false, false, false, false, false,
					false, true
				},
				{
					true, false, false, false, false, false, false, false, false, false,
					false, true
				},
				{
					true, false, false, false, false, false, false, false, false, false,
					false, true
				},
				{
					true, false, false, false, false, false, false, false, false, false,
					false, true
				},
				{
					false, false, false, false, false, false, false, false, false, false,
					false, true
				},
				{
					false, false, false, false, false, false, false, false, false, false,
					false, true
				}
			};
		}

		// Token: 0x02000CDE RID: 3294
		private sealed class RmlValidatingReader : RDLValidatingReader
		{
			// Token: 0x06008D2E RID: 36142 RVA: 0x0023E698 File Offset: 0x0023C898
			private RmlValidatingReader(XmlTextReader textReader, PublishingErrorContext errorContext, string targetRDLNamespace)
				: base(textReader, targetRDLNamespace)
			{
				base.Schemas.Add(XmlUtil.LoadSchemaFromResourceWithNullResolver(Assembly.GetExecutingAssembly().GetManifestResourceStream("Microsoft.ReportingServices.ReportProcessing.ReportDefinition.xsd")));
				base.ValidationEventHandler += this.ValidationCallBack;
				base.ValidationType = ValidationType.Schema;
				this.m_errorContext = errorContext;
				this.m_targetRDLNamespace = targetRDLNamespace;
			}

			// Token: 0x06008D2F RID: 36143 RVA: 0x0023E6F4 File Offset: 0x0023C8F4
			public override bool Read()
			{
				bool flag;
				try
				{
					if (ReportPublishing.RmlValidatingReader.CustomFlags.AfterCustomElement != this.m_custom)
					{
						base.Read();
						string text;
						if (!base.Validate(out text))
						{
							this.m_errorContext.Register(ProcessingErrorCode.rsInvalidReportDefinition, Severity.Error, ObjectType.Report, null, null, new string[] { text });
							throw new ReportProcessingException(this.m_errorContext.Messages);
						}
					}
					else
					{
						this.m_custom = ReportPublishing.RmlValidatingReader.CustomFlags.None;
					}
					if (ReportPublishing.RmlValidatingReader.CustomFlags.InCustomElement != this.m_custom)
					{
						while (!base.EOF && XmlNodeType.Element == base.NodeType && this.m_targetRDLNamespace != base.NamespaceURI)
						{
							this.Skip();
						}
					}
					flag = !base.EOF;
				}
				catch (ArgumentException ex)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidReportDefinition, Severity.Error, ObjectType.Report, null, null, new string[] { ex.Message });
					throw new ReportProcessingException(this.m_errorContext.Messages);
				}
				return flag;
			}

			// Token: 0x06008D30 RID: 36144 RVA: 0x0023E7DC File Offset: 0x0023C9DC
			public override string ReadString()
			{
				if (base.IsEmptyElement)
				{
					return string.Empty;
				}
				return base.ReadString();
			}

			// Token: 0x06008D31 RID: 36145 RVA: 0x0023E7F2 File Offset: 0x0023C9F2
			internal static ReportPublishing.RmlValidatingReader CreateReader(XmlTextReader upgradedRDLReader, PublishingErrorContext errorContext, string targetRDLNamespace)
			{
				Global.Tracer.Assert(upgradedRDLReader != null);
				upgradedRDLReader.WhitespaceHandling = WhitespaceHandling.None;
				upgradedRDLReader.XmlResolver = null;
				return new ReportPublishing.RmlValidatingReader(upgradedRDLReader, errorContext, targetRDLNamespace);
			}

			// Token: 0x06008D32 RID: 36146 RVA: 0x0023E818 File Offset: 0x0023CA18
			internal bool ReadBoolean()
			{
				if (base.IsEmptyElement)
				{
					Global.Tracer.Assert(false);
					return false;
				}
				return XmlConvert.ToBoolean(base.ReadString());
			}

			// Token: 0x06008D33 RID: 36147 RVA: 0x0023E83A File Offset: 0x0023CA3A
			internal int ReadInteger()
			{
				if (base.IsEmptyElement)
				{
					Global.Tracer.Assert(false);
					return 0;
				}
				return XmlConvert.ToInt32(base.ReadString());
			}

			// Token: 0x06008D34 RID: 36148 RVA: 0x0023E85C File Offset: 0x0023CA5C
			internal string ReadCustomXml()
			{
				Global.Tracer.Assert(this.m_custom == ReportPublishing.RmlValidatingReader.CustomFlags.None);
				if (base.IsEmptyElement)
				{
					return string.Empty;
				}
				this.m_custom = ReportPublishing.RmlValidatingReader.CustomFlags.InCustomElement;
				string text = base.ReadInnerXml();
				this.m_custom = ReportPublishing.RmlValidatingReader.CustomFlags.AfterCustomElement;
				return text;
			}

			// Token: 0x06008D35 RID: 36149 RVA: 0x0023E894 File Offset: 0x0023CA94
			private void ValidationCallBack(object sender, ValidationEventArgs args)
			{
				if (ReportProcessing.CompareWithInvariantCulture(this.m_targetRDLNamespace, base.NamespaceURI, false) == 0)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidReportDefinition, Severity.Error, ObjectType.Report, null, null, new string[] { args.Message });
					throw new ReportProcessingException(this.m_errorContext.Messages);
				}
				XmlNodeType nodeType = base.NodeType;
			}

			// Token: 0x04004F35 RID: 20277
			private const string XsdResourceID = "Microsoft.ReportingServices.ReportProcessing.ReportDefinition.xsd";

			// Token: 0x04004F36 RID: 20278
			private ReportPublishing.RmlValidatingReader.CustomFlags m_custom;

			// Token: 0x04004F37 RID: 20279
			private PublishingErrorContext m_errorContext;

			// Token: 0x04004F38 RID: 20280
			private string m_targetRDLNamespace;

			// Token: 0x02000D48 RID: 3400
			internal enum CustomFlags
			{
				// Token: 0x040050FA RID: 20730
				None,
				// Token: 0x040050FB RID: 20731
				InCustomElement,
				// Token: 0x040050FC RID: 20732
				AfterCustomElement
			}
		}

		// Token: 0x02000CDF RID: 3295
		private sealed class XmlNullResolver : XmlUrlResolver
		{
			// Token: 0x06008D36 RID: 36150 RVA: 0x0023E8F3 File Offset: 0x0023CAF3
			public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
			{
				throw new XmlException("Can't resolve URI reference.", null);
			}
		}

		// Token: 0x02000CE0 RID: 3296
		private sealed class AllowNullKeyHashtable
		{
			// Token: 0x17002B56 RID: 11094
			internal object this[string name]
			{
				get
				{
					if (name == null)
					{
						return this.m_nullValue;
					}
					return this.m_hashtable[name];
				}
				set
				{
					if (name == null)
					{
						this.m_nullValue = value;
						return;
					}
					this.m_hashtable[name] = value;
				}
			}

			// Token: 0x04004F39 RID: 20281
			private Hashtable m_hashtable = new Hashtable();

			// Token: 0x04004F3A RID: 20282
			private object m_nullValue;
		}
	}
}
