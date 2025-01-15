using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Xml;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000050 RID: 80
	internal class DataShapeQueryParser : IDisposable
	{
		// Token: 0x06000370 RID: 880 RVA: 0x00009E43 File Offset: 0x00008043
		private DataShapeQueryParser(XmlDictionaryReader reader, TranslationErrorContext errorContext)
		{
			this.m_reader = reader;
			this.m_errorContext = errorContext;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00009E6C File Offset: 0x0000806C
		private DataShapeQuery ReadDataShapeQuery()
		{
			DataShapeQuery dataShapeQuery = new DataShapeQuery();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "DataShapes"))
					{
						if (!(localName == "DataSources"))
						{
							this.SkipUnknownContent();
						}
						else
						{
							dataShapeQuery.DataSources = this.ReadList<DataSource>(new Func<DataSource>(this.ReadDataSource));
						}
					}
					else
					{
						dataShapeQuery.DataShapes = this.ReadList<DataShape>(new Func<DataShape>(this.ReadDataShape));
					}
				}
			}
			return dataShapeQuery;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00009EF4 File Offset: 0x000080F4
		private List<T> ReadList<T>(Func<T> readObject)
		{
			List<T> list = new List<T>();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					list.Add(readObject());
				}
			}
			return list;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00009F28 File Offset: 0x00008128
		private List<T> ReadList<T>(Func<ObjectType, Identifier, string, T> readObject, ObjectType objectType, Identifier objectId, string propertyName)
		{
			List<T> list = new List<T>();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					list.Add(readObject(objectType, objectId, propertyName));
				}
			}
			return list;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00009F60 File Offset: 0x00008160
		private DataSource ReadDataSource()
		{
			DataSource dataSource = new DataSource();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Id"))
					{
						if (!(localName == "DataSourceReference"))
						{
							this.SkipUnknownContent();
						}
						else
						{
							dataSource.DataSourceReference = this.ReadDataSourceReference();
						}
					}
					else
					{
						dataSource.Id = this.ReadIdentifier();
					}
				}
			}
			return dataSource;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00009FD0 File Offset: 0x000081D0
		private DataSourceReference ReadDataSourceReference()
		{
			DataSourceReference dataSourceReference = new DataSourceReference();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "DataSourceName"))
					{
						if (!(localName == "ItemPath"))
						{
							this.SkipUnknownContent();
						}
						else
						{
							dataSourceReference.ItemPath = HttpUtility.UrlDecode(this.ReadSimpleStringElement());
						}
					}
					else
					{
						dataSourceReference.DataSourceName = this.ReadSimpleStringElement();
					}
				}
			}
			return dataSourceReference;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000A048 File Offset: 0x00008248
		private DataShape ReadDataShape()
		{
			DataShape dataShape = new DataShape();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					uint num = global::<PrivateImplementationDetails>.ComputeStringHash(localName);
					if (num <= 1959720596U)
					{
						if (num <= 1177448485U)
						{
							if (num <= 527115941U)
							{
								if (num != 188393196U)
								{
									if (num == 527115941U)
									{
										if (localName == "ExtensionSchema")
										{
											dataShape.ExtensionSchema = this.ReadExtensionSchema();
											continue;
										}
									}
								}
								else if (localName == "Filters")
								{
									dataShape.Filters = this.ReadList<Filter>(new Func<Filter>(this.ReadFilter));
									continue;
								}
							}
							else if (num != 782705398U)
							{
								if (num != 921221376U)
								{
									if (num == 1177448485U)
									{
										if (localName == "IsIndependent")
										{
											dataShape.IsIndependent = this.ReadBoolean().Value;
											continue;
										}
									}
								}
								else if (localName == "Id")
								{
									dataShape.Id = this.ReadIdentifier();
									continue;
								}
							}
							else if (localName == "ContextOnly")
							{
								dataShape.ContextOnly = this.ReadBoolean();
								continue;
							}
						}
						else if (num <= 1867471148U)
						{
							if (num != 1262332209U)
							{
								if (num != 1562193592U)
								{
									if (num == 1867471148U)
									{
										if (localName == "PrimaryHierarchy")
										{
											dataShape.PrimaryHierarchy = this.ReadDataHierarchy();
											continue;
										}
									}
								}
								else if (localName == "DataRows")
								{
									dataShape.DataRows = this.ReadList<DataRow>(new Func<DataRow>(this.ReadDataRow));
									continue;
								}
							}
							else if (localName == "DataSourceVariables")
							{
								dataShape.DataSourceVariables = this.ReadString();
								continue;
							}
						}
						else if (num != 1945641567U)
						{
							if (num != 1947148360U)
							{
								if (num == 1959720596U)
								{
									if (localName == "SecondaryHierarchy")
									{
										dataShape.SecondaryHierarchy = this.ReadDataHierarchy();
										continue;
									}
								}
							}
							else if (localName == "Usage")
							{
								dataShape.Usage = this.ReadEnum<DataShapeUsage>().Value;
								continue;
							}
						}
						else if (localName == "Calculations")
						{
							dataShape.Calculations = this.ReadList<Calculation>(new Func<Calculation>(this.ReadCalculation));
							continue;
						}
					}
					else if (num <= 3069121093U)
					{
						if (num <= 1986825838U)
						{
							if (num != 1963746670U)
							{
								if (num == 1986825838U)
								{
									if (localName == "RequestedPrimaryLeafCount")
									{
										dataShape.RequestedPrimaryLeafCount = this.ReadInt32();
										continue;
									}
								}
							}
							else if (localName == "RestartTokens")
							{
								dataShape.RestartTokens = this.ReadRestartTokens(dataShape.Id);
								continue;
							}
						}
						else if (num != 2610398437U)
						{
							if (num != 2892069418U)
							{
								if (num == 3069121093U)
								{
									if (localName == "DataSourceId")
									{
										dataShape.DataSourceId = this.ReadIdentifier();
										continue;
									}
								}
							}
							else if (localName == "DynamicLimits")
							{
								dataShape.DynamicLimits = this.ReadDynamicLimits();
								continue;
							}
						}
						else if (localName == "Limits")
						{
							dataShape.Limits = this.ReadList<Limit>(new Func<Limit>(this.ReadLimit));
							continue;
						}
					}
					else if (num <= 3956797819U)
					{
						if (num != 3830911799U)
						{
							if (num != 3834414043U)
							{
								if (num == 3956797819U)
								{
									if (localName == "IncludeRestartToken")
									{
										dataShape.IncludeRestartToken = this.ReadBoolean();
										continue;
									}
								}
							}
							else if (localName == "QueryParameters")
							{
								throw new NotSupportedException("DataShapeQueryParser does not support deserializing query parameter declarations.");
							}
						}
						else if (localName == "VisualCalculationMetadata")
						{
							dataShape.VisualCalculationMetadata = this.ReadList<VisualAxis>(new Func<VisualAxis>(this.ReadVisualAxis));
							continue;
						}
					}
					else if (num != 3971247064U)
					{
						if (num != 4167114709U)
						{
							if (num == 4270063599U)
							{
								if (localName == "DataShapes")
								{
									dataShape.DataShapes = this.ReadList<DataShape>(new Func<DataShape>(this.ReadDataShape));
									continue;
								}
							}
						}
						else if (localName == "Messages")
						{
							dataShape.Messages = this.ReadList<Message>(new Func<Message>(this.ReadMessage));
							continue;
						}
					}
					else if (localName == "Transforms")
					{
						dataShape.Transforms = this.ReadList<DataTransform>(new Func<DataTransform>(this.ReadDataTransform));
						continue;
					}
					this.SkipUnknownContent();
				}
			}
			return dataShape;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000A580 File Offset: 0x00008780
		private VisualAxis ReadVisualAxis()
		{
			VisualAxis visualAxis = new VisualAxis();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Name"))
					{
						if (!(localName == "Groups"))
						{
							this.SkipUnknownContent();
						}
						else
						{
							visualAxis.Groups = this.ReadList<VisualAxisGroup>(new Func<VisualAxisGroup>(this.ReadVisualAxisGroup));
						}
					}
					else
					{
						visualAxis.Name = this.ReadString();
					}
				}
			}
			return visualAxis;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000A5FC File Offset: 0x000087FC
		private VisualAxisGroup ReadVisualAxisGroup()
		{
			VisualAxisGroup visualAxisGroup = new VisualAxisGroup();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					if (this.m_reader.LocalName == "Member")
					{
						visualAxisGroup.Member = this.ReadExpression(ObjectType.VisualAxisGroupMember, null, "Expressions");
					}
					else
					{
						this.SkipUnknownContent();
					}
				}
			}
			return visualAxisGroup;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000A658 File Offset: 0x00008858
		private ExtensionSchema ReadExtensionSchema()
		{
			ExtensionSchema extensionSchema = new ExtensionSchema();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Name"))
					{
						if (!(localName == "Entities"))
						{
							this.SkipUnknownContent();
						}
						else
						{
							extensionSchema.Entities = this.ReadList<ExtensionEntity>(new Func<ExtensionEntity>(this.ReadExtensionEntity));
						}
					}
					else
					{
						extensionSchema.Name = this.ReadString();
					}
				}
			}
			return extensionSchema;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000A6D4 File Offset: 0x000088D4
		private ExtensionEntity ReadExtensionEntity()
		{
			ExtensionEntity extensionEntity = new ExtensionEntity();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Name"))
					{
						if (!(localName == "Extends"))
						{
							if (!(localName == "Measures"))
							{
								if (!(localName == "Columns"))
								{
									this.SkipUnknownContent();
								}
								else
								{
									extensionEntity.Columns = this.ReadList<ExtensionColumn>(new Func<ExtensionColumn>(this.ReadExtensionColumn));
								}
							}
							else
							{
								extensionEntity.Measures = this.ReadList<ExtensionMeasure>(new Func<ExtensionMeasure>(this.ReadExtensionMeasure));
							}
						}
						else
						{
							extensionEntity.Extends = this.ReadString();
						}
					}
					else
					{
						extensionEntity.Name = this.ReadString();
					}
				}
			}
			return extensionEntity;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000A79C File Offset: 0x0000899C
		private ExtensionMeasure ReadExtensionMeasure()
		{
			ExtensionMeasure extensionMeasure = new ExtensionMeasure();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Name"))
					{
						if (!(localName == "Expression"))
						{
							if (!(localName == "DataType"))
							{
								this.SkipUnknownContent();
							}
							else
							{
								extensionMeasure.DataType = this.ReadEnum<ConceptualPrimitiveType>();
							}
						}
						else
						{
							extensionMeasure.Expression = this.ReadExpression(ObjectType.ExtensionMeasure, extensionMeasure.Name, "Expression");
						}
					}
					else
					{
						extensionMeasure.Name = this.ReadString();
					}
				}
			}
			return extensionMeasure;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000A83C File Offset: 0x00008A3C
		private ExtensionColumn ReadExtensionColumn()
		{
			ExtensionColumn extensionColumn = new ExtensionColumn();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Name"))
					{
						if (!(localName == "Expression"))
						{
							if (!(localName == "DataType"))
							{
								this.SkipUnknownContent();
							}
							else
							{
								extensionColumn.DataType = this.ReadEnum<ConceptualPrimitiveType>();
							}
						}
						else
						{
							extensionColumn.Expression = this.ReadExpression(ObjectType.ExtensionColumn, extensionColumn.Name, "Expression");
						}
					}
					else
					{
						extensionColumn.Name = this.ReadString();
					}
				}
			}
			return extensionColumn;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000A8DC File Offset: 0x00008ADC
		private DataTransform ReadDataTransform()
		{
			DataTransform dataTransform = new DataTransform();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Id"))
					{
						if (!(localName == "Algorithm"))
						{
							if (!(localName == "Input"))
							{
								if (!(localName == "Output"))
								{
									this.SkipUnknownContent();
								}
								else
								{
									dataTransform.Output = this.ReadDataTransformOutput();
								}
							}
							else
							{
								dataTransform.Input = this.ReadDataTransformInput();
							}
						}
						else
						{
							dataTransform.Algorithm = this.ReadString();
						}
					}
					else
					{
						dataTransform.Id = this.ReadIdentifier();
					}
				}
			}
			return dataTransform;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000A990 File Offset: 0x00008B90
		private DataTransformInput ReadDataTransformInput()
		{
			DataTransformInput dataTransformInput = new DataTransformInput();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Parameters"))
					{
						if (!(localName == "Table"))
						{
							this.SkipUnknownContent();
						}
						else
						{
							dataTransformInput.Table = this.ReadDataTransformTable();
						}
					}
					else
					{
						dataTransformInput.Parameters = this.ReadList<DataTransformParameter>(new Func<DataTransformParameter>(this.ReadDataTransformParameter));
					}
				}
			}
			return dataTransformInput;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000AA0C File Offset: 0x00008C0C
		private DataTransformOutput ReadDataTransformOutput()
		{
			DataTransformOutput dataTransformOutput = new DataTransformOutput();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					if (this.m_reader.LocalName == "Table")
					{
						dataTransformOutput.Table = this.ReadDataTransformTable();
					}
					else
					{
						this.SkipUnknownContent();
					}
				}
			}
			return dataTransformOutput;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000AA60 File Offset: 0x00008C60
		private DataTransformParameter ReadDataTransformParameter()
		{
			DataTransformParameter dataTransformParameter = new DataTransformParameter();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Id"))
					{
						if (!(localName == "Value"))
						{
							this.SkipUnknownContent();
						}
						else
						{
							dataTransformParameter.Value = this.ReadExpression(ObjectType.DataTransformParameter, dataTransformParameter.Id, "Value");
						}
					}
					else
					{
						dataTransformParameter.Id = this.ReadIdentifier();
					}
				}
			}
			return dataTransformParameter;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000AAE0 File Offset: 0x00008CE0
		private DataTransformTable ReadDataTransformTable()
		{
			DataTransformTable dataTransformTable = new DataTransformTable();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Id"))
					{
						if (!(localName == "Columns"))
						{
							this.SkipUnknownContent();
						}
						else
						{
							dataTransformTable.Columns = this.ReadList<DataTransformTableColumn>(new Func<DataTransformTableColumn>(this.ReadDataTransformTableColumn));
						}
					}
					else
					{
						dataTransformTable.Id = this.ReadIdentifier();
					}
				}
			}
			return dataTransformTable;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000AB5C File Offset: 0x00008D5C
		private DataTransformTableColumn ReadDataTransformTableColumn()
		{
			DataTransformTableColumn dataTransformTableColumn = new DataTransformTableColumn();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Id"))
					{
						if (!(localName == "Role"))
						{
							if (!(localName == "Value"))
							{
								this.SkipUnknownContent();
							}
							else
							{
								dataTransformTableColumn.Value = this.ReadExpression(ObjectType.DataTransformTableColumn, dataTransformTableColumn.Id, "Value");
							}
						}
						else
						{
							dataTransformTableColumn.Role = this.ReadString();
						}
					}
					else
					{
						dataTransformTableColumn.Id = this.ReadIdentifier();
					}
				}
			}
			return dataTransformTableColumn;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000ABFC File Offset: 0x00008DFC
		private DataHierarchy ReadDataHierarchy()
		{
			DataHierarchy dataHierarchy = new DataHierarchy();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					if (this.m_reader.LocalName == "DataMembers")
					{
						dataHierarchy.DataMembers = this.ReadList<DataMember>(new Func<DataMember>(this.ReadDataMember));
					}
					else
					{
						this.SkipUnknownContent();
					}
				}
			}
			return dataHierarchy;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000AC5C File Offset: 0x00008E5C
		private DataMember ReadDataMember()
		{
			DataMember dataMember = new DataMember();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					uint num = global::<PrivateImplementationDetails>.ComputeStringHash(localName);
					if (num <= 921221376U)
					{
						if (num != 91525164U)
						{
							if (num != 782705398U)
							{
								if (num == 921221376U)
								{
									if (localName == "Id")
									{
										dataMember.Id = this.ReadIdentifier();
										continue;
									}
								}
							}
							else if (localName == "ContextOnly")
							{
								dataMember.ContextOnly = this.ReadBoolean().Value;
								continue;
							}
						}
						else if (localName == "Group")
						{
							dataMember.Group = this.ReadGroup(dataMember.Id);
							continue;
						}
					}
					else if (num <= 2000325389U)
					{
						if (num != 1945641567U)
						{
							if (num == 2000325389U)
							{
								if (localName == "InstanceFilters")
								{
									dataMember.InstanceFilters = this.ReadList<FilterCondition>(new Func<FilterCondition>(this.ReadFilterCondition));
									continue;
								}
							}
						}
						else if (localName == "Calculations")
						{
							dataMember.Calculations = this.ReadList<Calculation>(new Func<Calculation>(this.ReadCalculation));
							continue;
						}
					}
					else if (num != 3541781168U)
					{
						if (num == 4270063599U)
						{
							if (localName == "DataShapes")
							{
								dataMember.DataShapes = this.ReadList<DataShape>(new Func<DataShape>(this.ReadDataShape));
								continue;
							}
						}
					}
					else if (localName == "DataMembers")
					{
						dataMember.DataMembers = this.ReadList<DataMember>(new Func<DataMember>(this.ReadDataMember));
						continue;
					}
					this.SkipUnknownContent();
				}
			}
			return dataMember;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000AE34 File Offset: 0x00009034
		private Group ReadGroup(Identifier objectId)
		{
			Group group = new Group();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "GroupKeys"))
					{
						if (!(localName == "SortKeys"))
						{
							if (!(localName == "DetailGroupIdentity"))
							{
								if (!(localName == "ScopeIdDefinition"))
								{
									if (!(localName == "StartPosition"))
									{
										this.SkipUnknownContent();
									}
									else
									{
										group.StartPosition = this.ReadScopeId(objectId);
									}
								}
								else
								{
									group.ScopeIdDefinition = this.ReadScopeIdDefinition();
								}
							}
							else
							{
								group.DetailGroupIdentity = this.ReadDetailGroupIdentity();
							}
						}
						else
						{
							group.SortKeys = this.ReadList<Microsoft.DataShaping.InternalContracts.DataShapeQuery.SortKey>(new Func<Microsoft.DataShaping.InternalContracts.DataShapeQuery.SortKey>(this.ReadSortKey));
						}
					}
					else
					{
						group.GroupKeys = this.ReadList<GroupKey>(new Func<GroupKey>(this.ReadGroupKey));
					}
				}
			}
			return group;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000AF18 File Offset: 0x00009118
		private GroupKey ReadGroupKey()
		{
			GroupKey groupKey = new GroupKey();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Id"))
					{
						if (!(localName == "Value"))
						{
							if (!(localName == "ShowItemsWithNoData"))
							{
								this.SkipUnknownContent();
							}
							else
							{
								groupKey.ShowItemsWithNoData = this.ReadBoolean();
							}
						}
						else
						{
							groupKey.Value = this.ReadExpression(ObjectType.GroupKey, groupKey.Id, "Value");
						}
					}
					else
					{
						groupKey.Id = this.ReadIdentifier();
					}
				}
			}
			return groupKey;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000AFB4 File Offset: 0x000091B4
		private Microsoft.DataShaping.InternalContracts.DataShapeQuery.SortKey ReadSortKey()
		{
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.SortKey sortKey = new Microsoft.DataShaping.InternalContracts.DataShapeQuery.SortKey();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Id"))
					{
						if (!(localName == "Value"))
						{
							if (!(localName == "SortDirection"))
							{
								this.SkipUnknownContent();
							}
							else
							{
								sortKey.SortDirection = this.ReadEnum<SortDirection>();
							}
						}
						else
						{
							sortKey.Value = this.ReadExpression(ObjectType.SortKey, sortKey.Id, "Value");
						}
					}
					else
					{
						sortKey.Id = this.ReadIdentifier();
					}
				}
			}
			return sortKey;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000B050 File Offset: 0x00009250
		private DetailGroupIdentity ReadDetailGroupIdentity()
		{
			DetailGroupIdentity detailGroupIdentity = new DetailGroupIdentity();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Id"))
					{
						if (!(localName == "Value"))
						{
							this.SkipUnknownContent();
						}
						else
						{
							detailGroupIdentity.Value = this.ReadExpression(ObjectType.DetailGroupIdentity, detailGroupIdentity.Id, "Value");
						}
					}
					else
					{
						detailGroupIdentity.Id = this.ReadIdentifier();
					}
				}
			}
			return detailGroupIdentity;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000B0D0 File Offset: 0x000092D0
		private ScopeIdDefinition ReadScopeIdDefinition()
		{
			ScopeIdDefinition scopeIdDefinition = new ScopeIdDefinition();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					if (this.m_reader.LocalName == "Values")
					{
						scopeIdDefinition.Values = this.ReadList<ScopeValueDefinition>(new Func<ScopeValueDefinition>(this.ReadScopeValueDefinition));
					}
					else
					{
						this.SkipUnknownContent();
					}
				}
			}
			return scopeIdDefinition;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000B130 File Offset: 0x00009330
		private ScopeValueDefinition ReadScopeValueDefinition()
		{
			ScopeValueDefinition scopeValueDefinition = new ScopeValueDefinition();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Id"))
					{
						if (!(localName == "Value"))
						{
							this.SkipUnknownContent();
						}
						else
						{
							scopeValueDefinition.Value = this.ReadExpression(ObjectType.ScopeValueDefinition, scopeValueDefinition.Id, "Value");
						}
					}
					else
					{
						scopeValueDefinition.Id = this.ReadIdentifier();
					}
				}
			}
			return scopeValueDefinition;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000B1B0 File Offset: 0x000093B0
		private ScopeId ReadScopeId(Identifier objectId)
		{
			ScopeId scopeId = new ScopeId();
			Func<ScopeValue> <>9__0;
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					if (this.m_reader.LocalName == "Values")
					{
						ScopeId scopeId2 = scopeId;
						Func<ScopeValue> func;
						if ((func = <>9__0) == null)
						{
							func = (<>9__0 = () => this.ReadScopeValue(objectId));
						}
						scopeId2.Values = this.ReadList<ScopeValue>(func);
					}
					else
					{
						this.SkipUnknownContent();
					}
				}
			}
			return scopeId;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000B234 File Offset: 0x00009434
		private ScopeValue ReadScopeValue(Identifier objectId)
		{
			return new ScopeValue
			{
				Value = this.ReadVariantValue(ObjectType.ScopeValue, objectId)
			};
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000B24C File Offset: 0x0000944C
		private Candidate<ScalarValue> ReadVariantValue(ObjectType objectType, Identifier objectId)
		{
			Candidate<ScalarValue> candidate = Candidate<ScalarValue>.Invalid;
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					if (this.m_reader.LocalName == "Value")
					{
						candidate = this.ReadVariantValueInternal(objectType, objectId, "Value");
					}
					else
					{
						this.SkipUnknownContent();
					}
				}
			}
			return candidate;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000B2A0 File Offset: 0x000094A0
		private Candidate<ScalarValue> ReadVariantValueInternal(ObjectType objectType, Identifier objectId, string propertyName)
		{
			string attribute = this.m_reader.GetAttribute("type");
			string text = this.ReadSimpleStringElement();
			if (attribute == "null")
			{
				return Candidate<ScalarValue>.Valid(ScalarValue.Null);
			}
			ExpressionNode expressionNode = this.ParseExpressionNode(attribute, text, objectType, objectId, propertyName);
			if (expressionNode == null || expressionNode.Kind != ExpressionNodeKind.Literal || this.m_errorContext.HasError)
			{
				return Candidate<ScalarValue>.Invalid;
			}
			return ((LiteralExpressionNode)expressionNode).Value;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000B31C File Offset: 0x0000951C
		private ExpressionNode ReadExpressionNode(ObjectType objectType, Identifier objectId, string propertyName)
		{
			string text = this.ReadSimpleStringElement();
			return ExpressionParser.ParseExpression(new ExpressionContext(new TranslationErrorContext(), objectType, objectId, propertyName), text);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000B344 File Offset: 0x00009544
		private Filter ReadFilter()
		{
			Filter filter = new Filter();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Target"))
					{
						if (!(localName == "Condition"))
						{
							if (!(localName == "UsageKind"))
							{
								this.SkipUnknownContent();
							}
							else
							{
								filter.UsageKind = this.ReadEnum<FilterUsageKind>().Value;
							}
						}
						else
						{
							filter.Condition = this.ReadFilterCondition();
						}
					}
					else
					{
						filter.Target = this.ReadExpression(ObjectType.Filter, null, "Value");
					}
				}
			}
			return filter;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000B3E0 File Offset: 0x000095E0
		private FilterCondition ReadFilterCondition()
		{
			FilterCondition filterCondition = null;
			Candidate<FilterConditionType> candidate = null;
			Identifier identifier = null;
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					if (string.Equals(this.m_reader.LocalName, "Id", StringComparison.Ordinal))
					{
						identifier = this.ReadIdentifier();
					}
					else if (string.Equals(this.m_reader.LocalName, "Type", StringComparison.Ordinal) && candidate == null)
					{
						candidate = this.ReadEnum<FilterConditionType>();
						if (candidate.IsValid && candidate.Value == FilterConditionType.FilterEmptyGroups)
						{
							filterCondition = new FilterEmptyGroupsCondition();
						}
					}
					else if (candidate != null && candidate.IsValid)
					{
						switch (candidate.Value)
						{
						case FilterConditionType.Apply:
							this.ReadApplyFilterConditionContent(ref filterCondition);
							continue;
						case FilterConditionType.Binary:
							this.ReadBinaryConditionContent(ref filterCondition);
							continue;
						case FilterConditionType.Compound:
							this.ReadCompoundConditionContent(ref filterCondition);
							continue;
						case FilterConditionType.Context:
							this.ReadContextFilterConditionContent(ref filterCondition);
							continue;
						case FilterConditionType.Exists:
							this.ReadExistsFilterConditionContent(ref filterCondition);
							continue;
						case FilterConditionType.In:
							this.ReadInFilterConditionContent(ref filterCondition);
							continue;
						case FilterConditionType.Unary:
							this.ReadUnaryConditionContent(ref filterCondition);
							continue;
						}
						this.SkipUnknownContent();
					}
					else
					{
						this.SkipUnknownContent();
					}
				}
			}
			if (filterCondition != null)
			{
				filterCondition.Id = identifier;
			}
			return filterCondition;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000B520 File Offset: 0x00009720
		private void ReadInFilterConditionContent(ref FilterCondition condition)
		{
			InFilterCondition inFilterCondition = this.CreateIfNull<FilterCondition, InFilterCondition>(ref condition);
			string localName = this.m_reader.LocalName;
			if (localName == "Expressions")
			{
				inFilterCondition.Expressions = this.ReadList<Expression>(new Func<ObjectType, Identifier, string, Expression>(this.ReadExpression), ObjectType.InFilterCondition, null, "Expressions");
				return;
			}
			if (localName == "Values")
			{
				inFilterCondition.Values = this.ReadList<List<Expression>>(new Func<ObjectType, Identifier, string, List<Expression>>(this.ReadExpressionList), ObjectType.InFilterCondition, null, "Values");
				return;
			}
			if (localName == "IdentityComparison")
			{
				inFilterCondition.IdentityComparison = this.ReadBoolean().Value;
				return;
			}
			if (!(localName == "Table"))
			{
				this.SkipUnknownContent();
				return;
			}
			inFilterCondition.Table = this.ReadExpression(ObjectType.InFilterCondition, null, "Table");
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000B5E6 File Offset: 0x000097E6
		private List<Expression> ReadExpressionList(ObjectType objectType, Identifier objectId, string propertyname)
		{
			return this.ReadList<Expression>(new Func<ObjectType, Identifier, string, Expression>(this.ReadExpression), objectType, objectId, propertyname);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000B600 File Offset: 0x00009800
		private void ReadUnaryConditionContent(ref FilterCondition condition)
		{
			UnaryFilterCondition unaryFilterCondition = this.CreateIfNull<FilterCondition, UnaryFilterCondition>(ref condition);
			string localName = this.m_reader.LocalName;
			if (localName == "Expression")
			{
				unaryFilterCondition.Expression = this.ReadExpression(ObjectType.UnaryFilterCondition, null, "Expression");
				return;
			}
			if (!(localName == "Not"))
			{
				this.SkipUnknownContent();
				return;
			}
			unaryFilterCondition.Not = this.ReadBoolean();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000B668 File Offset: 0x00009868
		private void ReadBinaryConditionContent(ref FilterCondition condition)
		{
			BinaryFilterCondition binaryFilterCondition = this.CreateIfNull<FilterCondition, BinaryFilterCondition>(ref condition);
			string localName = this.m_reader.LocalName;
			if (localName == "LeftExpression")
			{
				binaryFilterCondition.LeftExpression = this.ReadExpression(ObjectType.BinaryFilterCondition, null, "LeftExpression");
				return;
			}
			if (localName == "RightExpression")
			{
				binaryFilterCondition.RightExpression = this.ReadExpression(ObjectType.BinaryFilterCondition, null, "RightExpression");
				return;
			}
			if (localName == "Operator")
			{
				binaryFilterCondition.Operator = this.ReadEnum<BinaryFilterOperator>();
				return;
			}
			if (!(localName == "Not"))
			{
				this.SkipUnknownContent();
				return;
			}
			binaryFilterCondition.Not = this.ReadBoolean();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000B708 File Offset: 0x00009908
		private void ReadCompoundConditionContent(ref FilterCondition condition)
		{
			CompoundFilterCondition compoundFilterCondition = this.CreateIfNull<FilterCondition, CompoundFilterCondition>(ref condition);
			string localName = this.m_reader.LocalName;
			if (localName == "Operator")
			{
				compoundFilterCondition.Operator = this.ReadEnum<CompoundFilterOperator>();
				return;
			}
			if (!(localName == "Conditions"))
			{
				this.SkipUnknownContent();
				return;
			}
			compoundFilterCondition.Conditions = this.ReadList<FilterCondition>(new Func<FilterCondition>(this.ReadFilterCondition));
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000B774 File Offset: 0x00009974
		private void ReadContextFilterConditionContent(ref FilterCondition condition)
		{
			ContextFilterCondition contextFilterCondition = this.CreateIfNull<FilterCondition, ContextFilterCondition>(ref condition);
			if (this.m_reader.LocalName == "DataShape")
			{
				contextFilterCondition.DataShape = this.ReadDataShape();
				return;
			}
			this.SkipUnknownContent();
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000B7B4 File Offset: 0x000099B4
		private void ReadApplyFilterConditionContent(ref FilterCondition condition)
		{
			ApplyFilterCondition applyFilterCondition = this.CreateIfNull<FilterCondition, ApplyFilterCondition>(ref condition);
			if (this.m_reader.LocalName == "DataShapeReference")
			{
				applyFilterCondition.DataShapeReference = this.ReadExpression(ObjectType.ApplyFilterCondition, null, "DataShapeReference");
				return;
			}
			this.SkipUnknownContent();
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000B7FC File Offset: 0x000099FC
		private void ReadExistsFilterConditionContent(ref FilterCondition condition)
		{
			ExistsFilterCondition existsFilterCondition = this.CreateIfNull<FilterCondition, ExistsFilterCondition>(ref condition);
			if (this.m_reader.LocalName == "Items")
			{
				existsFilterCondition.Items = this.ReadList<ExistsFilterItem>(new Func<ExistsFilterItem>(this.ReadExistsFilterItem));
				return;
			}
			this.SkipUnknownContent();
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000B848 File Offset: 0x00009A48
		private ExistsFilterItem ReadExistsFilterItem()
		{
			ExistsFilterItem existsFilterItem = new ExistsFilterItem();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Targets"))
					{
						if (!(localName == "Exists"))
						{
							this.SkipUnknownContent();
						}
						else
						{
							existsFilterItem.Exists = this.ReadExpression(ObjectType.ExistsFilterCondition, null, "Exists");
						}
					}
					else
					{
						existsFilterItem.Targets = this.ReadList<Expression>(new Func<ObjectType, Identifier, string, Expression>(this.ReadExpression), ObjectType.ExistsFilterCondition, null, "Targets");
					}
				}
			}
			return existsFilterItem;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000B8D4 File Offset: 0x00009AD4
		private DynamicLimits ReadDynamicLimits()
		{
			DynamicLimits dynamicLimits = new DynamicLimits();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "TargetIntersectionCount"))
					{
						if (!(localName == "IntersectionLimit"))
						{
							if (!(localName == "Primary"))
							{
								if (!(localName == "Secondary"))
								{
									if (!(localName == "Blocks"))
									{
										this.SkipUnknownContent();
									}
									else
									{
										dynamicLimits.Blocks = this.ReadList<DynamicLimitBlock>(new Func<DynamicLimitBlock>(this.ReadDynamicLimitBlock));
									}
								}
								else
								{
									dynamicLimits.Secondary = this.ReadDynamicLimitRecommendation();
								}
							}
							else
							{
								dynamicLimits.Primary = this.ReadDynamicLimitRecommendation();
							}
						}
						else
						{
							dynamicLimits.IntersectionLimit = this.ReadExpression(ObjectType.DynamicLimits, null, "IntersectionLimit");
						}
					}
					else
					{
						dynamicLimits.TargetIntersectionCount = this.ReadInt32();
					}
				}
			}
			return dynamicLimits;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000B9B4 File Offset: 0x00009BB4
		private DynamicLimitBlock ReadDynamicLimitBlock()
		{
			DynamicLimitBlock dynamicLimitBlock = null;
			Candidate<ObjectType> candidate = null;
			DynamicLimitRecommendation dynamicLimitRecommendation = null;
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					if (string.Equals(this.m_reader.LocalName, "Count", StringComparison.Ordinal))
					{
						dynamicLimitRecommendation = this.ReadDynamicLimitRecommendation();
					}
					else if (string.Equals(this.m_reader.LocalName, "Type", StringComparison.Ordinal) && candidate == null)
					{
						candidate = this.ReadEnum<ObjectType>();
					}
					else if (candidate != null && candidate.IsValid)
					{
						ObjectType value = candidate.Value;
						if (value != ObjectType.DynamicLimitEvenDistributionBlock)
						{
							if (value != ObjectType.DynamicLimitPrimarySecondaryBlock)
							{
								this.SkipUnknownContent();
							}
							else
							{
								this.ReadDynamicLimitPrimarySecondaryBlockContent(ref dynamicLimitBlock);
							}
						}
						else
						{
							this.ReadDynamicLimitEvenDistributionBlockContent(ref dynamicLimitBlock);
						}
					}
					else
					{
						this.SkipUnknownContent();
					}
				}
			}
			if (dynamicLimitBlock != null && dynamicLimitRecommendation != null)
			{
				dynamicLimitBlock.Count = dynamicLimitRecommendation;
			}
			return dynamicLimitBlock;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000BA84 File Offset: 0x00009C84
		private void ReadDynamicLimitEvenDistributionBlockContent(ref DynamicLimitBlock block)
		{
			DynamicLimitEvenDistributionBlock dynamicLimitEvenDistributionBlock = this.CreateIfNull<DynamicLimitBlock, DynamicLimitEvenDistributionBlock>(ref block);
			if (this.m_reader.LocalName == "Limits")
			{
				dynamicLimitEvenDistributionBlock.Limits = this.ReadList<DynamicLimit>(new Func<DynamicLimit>(this.ReadDynamicLimit));
				return;
			}
			this.SkipUnknownContent();
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000BAD0 File Offset: 0x00009CD0
		private void ReadDynamicLimitPrimarySecondaryBlockContent(ref DynamicLimitBlock block)
		{
			DynamicLimitPrimarySecondaryBlock dynamicLimitPrimarySecondaryBlock = this.CreateIfNull<DynamicLimitBlock, DynamicLimitPrimarySecondaryBlock>(ref block);
			string localName = this.m_reader.LocalName;
			if (localName == "Primary")
			{
				dynamicLimitPrimarySecondaryBlock.Primary = this.ReadDynamicLimit();
				return;
			}
			if (!(localName == "Secondary"))
			{
				this.SkipUnknownContent();
				return;
			}
			dynamicLimitPrimarySecondaryBlock.Secondary = this.ReadDynamicLimit();
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000BB30 File Offset: 0x00009D30
		private DynamicLimit ReadDynamicLimit()
		{
			DynamicLimit dynamicLimit = new DynamicLimit();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "LimitRef"))
					{
						if (localName == "Count")
						{
							dynamicLimit.Count = this.ReadDynamicLimitRecommendation();
						}
					}
					else
					{
						dynamicLimit.LimitRef = this.ReadExpression(ObjectType.DynamicLimitPrimarySecondaryBlock, null, "LimitRef");
					}
				}
			}
			return dynamicLimit;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000BBA0 File Offset: 0x00009DA0
		private DynamicLimitRecommendation ReadDynamicLimitRecommendation()
		{
			DynamicLimitRecommendation dynamicLimitRecommendation = new DynamicLimitRecommendation();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Min"))
					{
						if (!(localName == "Max"))
						{
							if (!(localName == "IsMandatoryConstraint"))
							{
								this.SkipUnknownContent();
							}
							else
							{
								dynamicLimitRecommendation.IsMandatoryConstraint = this.ReadBoolean().Value;
							}
						}
						else
						{
							dynamicLimitRecommendation.Max = this.ReadInt32();
						}
					}
					else
					{
						dynamicLimitRecommendation.Min = this.ReadInt32();
					}
				}
			}
			return dynamicLimitRecommendation;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000BC30 File Offset: 0x00009E30
		private Limit ReadLimit()
		{
			Limit limit = new Limit();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Id"))
					{
						if (!(localName == "Operator"))
						{
							if (!(localName == "Targets"))
							{
								if (!(localName == "Within"))
								{
									if (!(localName == "TelemetryId"))
									{
										this.SkipUnknownContent();
									}
									else
									{
										limit.TelemetryId = new int?(this.ReadInt32().Value);
									}
								}
								else
								{
									limit.Within = this.ReadExpression(ObjectType.Limit, limit.Id, "Within");
								}
							}
							else
							{
								limit.Targets = this.ReadList<Expression>(new Func<ObjectType, Identifier, string, Expression>(this.ReadExpression), ObjectType.Limit, limit.Id, "Targets");
							}
						}
						else
						{
							limit.Operator = this.ReadLimitOperator();
						}
					}
					else
					{
						limit.Id = this.ReadIdentifier();
					}
				}
			}
			return limit;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000BD2C File Offset: 0x00009F2C
		private LimitOperator ReadLimitOperator()
		{
			LimitOperator limitOperator = null;
			Candidate<LimitOperatorType> candidate = null;
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					if (this.m_reader.LocalName == "Type" && candidate == null)
					{
						candidate = this.ReadEnum<LimitOperatorType>();
						if (candidate.IsValid)
						{
							LimitOperatorType value = candidate.Value;
							if (value != LimitOperatorType.First)
							{
								if (value == LimitOperatorType.Last)
								{
									limitOperator = new LastLimitOperator
									{
										Count = 1
									};
								}
							}
							else
							{
								limitOperator = new FirstLimitOperator
								{
									Count = 1
								};
							}
						}
					}
					else if (candidate != null && candidate.IsValid)
					{
						switch (candidate.Value)
						{
						case LimitOperatorType.Top:
							this.ReadTopLimitOperatorContent(ref limitOperator);
							break;
						case LimitOperatorType.Sample:
							this.ReadSampleLimitOperatorContent(ref limitOperator);
							break;
						case LimitOperatorType.Bottom:
							this.ReadBottomLimitOperatorContent(ref limitOperator);
							break;
						case LimitOperatorType.BinnedLineSample:
							this.ReadBinnedLineSampleLimitOperatorContent(ref limitOperator);
							break;
						case LimitOperatorType.OverlappingPointsSample:
							this.ReadOverlappingPointsSampleLimitOperatorContent(ref limitOperator);
							break;
						case LimitOperatorType.TopNPerLevel:
							this.ReadTopNPerLevelLimitOperatorContent(ref limitOperator);
							break;
						case LimitOperatorType.Window:
							this.ReadWindowLimitOperatorContent(ref limitOperator);
							break;
						default:
							this.SkipUnknownContent();
							break;
						}
					}
					else
					{
						this.SkipUnknownContent();
					}
				}
			}
			return limitOperator;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000BE64 File Offset: 0x0000A064
		private void ReadTopLimitOperatorContent(ref LimitOperator limitOperator)
		{
			TopLimitOperator topLimitOperator = this.CreateIfNull<LimitOperator, TopLimitOperator>(ref limitOperator);
			string localName = this.m_reader.LocalName;
			if (localName == "Count")
			{
				topLimitOperator.Count = this.ReadInt32();
				return;
			}
			if (localName == "Skip")
			{
				topLimitOperator.Skip = this.ReadLong();
				return;
			}
			if (!(localName == "IsStrict"))
			{
				this.SkipUnknownContent();
				return;
			}
			topLimitOperator.IsStrict = this.ReadBoolean();
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		private void ReadSampleLimitOperatorContent(ref LimitOperator limitOperator)
		{
			SampleLimitOperator sampleLimitOperator = this.CreateIfNull<LimitOperator, SampleLimitOperator>(ref limitOperator);
			string localName = this.m_reader.LocalName;
			if (localName == "Count")
			{
				sampleLimitOperator.Count = this.ReadInt32();
				return;
			}
			if (!(localName == "PreserveKeyPoints"))
			{
				this.SkipUnknownContent();
				return;
			}
			sampleLimitOperator.PreserveKeyPoints = this.ReadBoolean();
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000BF3C File Offset: 0x0000A13C
		private void ReadBottomLimitOperatorContent(ref LimitOperator limitOperator)
		{
			BottomLimitOperator bottomLimitOperator = this.CreateIfNull<LimitOperator, BottomLimitOperator>(ref limitOperator);
			if (this.m_reader.LocalName == "Count")
			{
				bottomLimitOperator.Count = this.ReadInt32();
				return;
			}
			this.SkipUnknownContent();
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000BF7C File Offset: 0x0000A17C
		private void ReadBinnedLineSampleLimitOperatorContent(ref LimitOperator limitOperator)
		{
			BinnedLineSampleLimitOperator binnedLineSampleLimitOperator = this.CreateIfNull<LimitOperator, BinnedLineSampleLimitOperator>(ref limitOperator);
			string localName = this.m_reader.LocalName;
			if (localName == "Count")
			{
				binnedLineSampleLimitOperator.Count = this.ReadInt32();
				return;
			}
			if (localName == "MinPointsPerSeries")
			{
				binnedLineSampleLimitOperator.MinPointsPerSeries = this.ReadInt32();
				return;
			}
			if (localName == "MaxPointsPerSeries")
			{
				binnedLineSampleLimitOperator.MaxPointsPerSeries = this.ReadInt32();
				return;
			}
			if (localName == "MaxDynamicSeriesCount")
			{
				binnedLineSampleLimitOperator.MaxDynamicSeriesCount = this.ReadInt32();
				return;
			}
			if (localName == "Measures")
			{
				binnedLineSampleLimitOperator.Measures = this.ReadList<Expression>(new Func<ObjectType, Identifier, string, Expression>(this.ReadExpression), ObjectType.BinnedLineSampleLimitOperator, null, "Measures");
				return;
			}
			if (!(localName == "PrimaryScalarKey"))
			{
				this.SkipUnknownContent();
				return;
			}
			binnedLineSampleLimitOperator.PrimaryScalarKey = this.ReadExpression(ObjectType.BinnedLineSampleLimitOperator, null, "PrimaryScalarKey");
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000C05C File Offset: 0x0000A25C
		private void ReadOverlappingPointsSampleLimitOperatorContent(ref LimitOperator limitOperator)
		{
			OverlappingPointsSampleLimitOperator overlappingPointsSampleLimitOperator = this.CreateIfNull<LimitOperator, OverlappingPointsSampleLimitOperator>(ref limitOperator);
			string localName = this.m_reader.LocalName;
			if (localName == "Count")
			{
				overlappingPointsSampleLimitOperator.Count = this.ReadInt32();
				return;
			}
			if (localName == "X")
			{
				overlappingPointsSampleLimitOperator.X = this.ReadPlotAxis();
				return;
			}
			if (!(localName == "Y"))
			{
				this.SkipUnknownContent();
				return;
			}
			overlappingPointsSampleLimitOperator.Y = this.ReadPlotAxis();
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000C0D4 File Offset: 0x0000A2D4
		private void ReadTopNPerLevelLimitOperatorContent(ref LimitOperator limitOperator)
		{
			TopNPerLevelLimitOperator topNPerLevelLimitOperator = this.CreateIfNull<LimitOperator, TopNPerLevelLimitOperator>(ref limitOperator);
			string localName = this.m_reader.LocalName;
			if (localName == "Count")
			{
				topNPerLevelLimitOperator.Count = this.ReadInt32();
				return;
			}
			if (localName == "Levels")
			{
				topNPerLevelLimitOperator.Levels = this.ReadTopNPerLevelLevels();
				return;
			}
			if (!(localName == "WindowExpansionInstance"))
			{
				this.SkipUnknownContent();
				return;
			}
			topNPerLevelLimitOperator.WindowExpansionInstance = this.ReadTopNPerLevelWindowExpansionInstance();
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000C14C File Offset: 0x0000A34C
		private void ReadWindowLimitOperatorContent(ref LimitOperator limitOperator)
		{
			WindowLimitOperator windowLimitOperator = this.CreateIfNull<LimitOperator, WindowLimitOperator>(ref limitOperator);
			string localName = this.m_reader.LocalName;
			if (localName == "Count")
			{
				windowLimitOperator.Count = this.ReadInt32();
				return;
			}
			if (localName == "RestartTokens")
			{
				windowLimitOperator.RestartTokens = this.ReadRestartTokens(null);
				return;
			}
			if (!(localName == "RestartMatchingBehavior"))
			{
				this.SkipUnknownContent();
				return;
			}
			windowLimitOperator.RestartMatchingBehavior = new RestartMatchingBehavior?(this.ReadEnum<RestartMatchingBehavior>().Value);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000C1D0 File Offset: 0x0000A3D0
		private List<List<Expression>> ReadTopNPerLevelLevels()
		{
			List<List<Expression>> list = new List<List<Expression>>();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					list.Add(this.ReadList<Expression>(new Func<ObjectType, Identifier, string, Expression>(this.ReadExpression), ObjectType.TopNPerLevelLimitOperator, null, "Levels"));
				}
			}
			return list;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000C216 File Offset: 0x0000A416
		private LimitWindowExpansionInstance ReadTopNPerLevelWindowExpansionInstance()
		{
			return new LimitWindowExpansionInstance
			{
				Values = this.ReadList<Expression>(new Func<ObjectType, Identifier, string, Expression>(this.ReadExpression), ObjectType.TopNPerLevelLimitOperator, null, "WindowValues"),
				WindowValues = this.ReadList<LimitWindowExpansionValue>(new Func<LimitWindowExpansionValue>(this.ReadTopNPerLevelWindowExpansionValues))
			};
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000C255 File Offset: 0x0000A455
		private LimitWindowExpansionValue ReadTopNPerLevelWindowExpansionValues()
		{
			return new LimitWindowExpansionValue
			{
				Values = this.ReadList<Expression>(new Func<ObjectType, Identifier, string, Expression>(this.ReadExpression), ObjectType.TopNPerLevelLimitOperator, null, "WindowExpansionInstance"),
				WindowKind = this.ReadEnum<WindowKind>().Value
			};
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000C290 File Offset: 0x0000A490
		private LimitPlotAxis ReadPlotAxis()
		{
			LimitPlotAxis limitPlotAxis = new LimitPlotAxis();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Key"))
					{
						if (!(localName == "Transform"))
						{
							this.SkipUnknownContent();
						}
						else
						{
							limitPlotAxis.Transform = this.ReadEnum<DataReductionPlotAxisTransform>().Value;
						}
					}
					else
					{
						limitPlotAxis.Key = this.ReadExpression(ObjectType.LimitPlotAxis, null, "Key");
					}
				}
			}
			return limitPlotAxis;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000C310 File Offset: 0x0000A510
		private TSpecific CreateIfNull<TGeneral, TSpecific>(ref TGeneral general) where TSpecific : TGeneral, new()
		{
			TSpecific tspecific = (TSpecific)((object)general);
			if (tspecific == null)
			{
				tspecific = new TSpecific();
				general = (TGeneral)((object)tspecific);
			}
			return tspecific;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000C350 File Offset: 0x0000A550
		private DataRow ReadDataRow()
		{
			DataRow dataRow = new DataRow();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					if (this.m_reader.LocalName == "DataIntersections")
					{
						dataRow.Intersections = this.ReadList<DataIntersection>(new Func<DataIntersection>(this.ReadDataIntersection));
					}
					else
					{
						this.SkipUnknownContent();
					}
				}
			}
			return dataRow;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000C3B0 File Offset: 0x0000A5B0
		private DataIntersection ReadDataIntersection()
		{
			DataIntersection dataIntersection = new DataIntersection();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Id"))
					{
						if (!(localName == "Calculations"))
						{
							if (!(localName == "DataShapes"))
							{
								this.SkipUnknownContent();
							}
							else
							{
								dataIntersection.DataShapes = this.ReadList<DataShape>(new Func<DataShape>(this.ReadDataShape));
							}
						}
						else
						{
							dataIntersection.Calculations = this.ReadList<Calculation>(new Func<Calculation>(this.ReadCalculation));
						}
					}
					else
					{
						dataIntersection.Id = this.ReadIdentifier();
					}
				}
			}
			return dataIntersection;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000C45C File Offset: 0x0000A65C
		private Calculation ReadCalculation()
		{
			Calculation calculation = new Calculation();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Id"))
					{
						if (!(localName == "Value"))
						{
							if (!(localName == "SuppressJoinPredicate"))
							{
								if (!(localName == "NativeReferenceName"))
								{
									if (!(localName == "ContextOnly"))
									{
										this.SkipUnknownContent();
									}
									else
									{
										calculation.IsContextOnly = this.ReadBoolean().Value;
									}
								}
								else
								{
									calculation.NativeReferenceName = this.ReadString();
								}
							}
							else
							{
								calculation.SuppressJoinPredicate = this.ReadBoolean();
							}
						}
						else
						{
							calculation.Value = this.ReadExpression(ObjectType.Calculation, calculation.Id, "Value");
						}
					}
					else
					{
						calculation.Id = this.ReadIdentifier();
					}
				}
			}
			return calculation;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000C538 File Offset: 0x0000A738
		private Message ReadMessage()
		{
			Message message = new Message();
			while (this.Advance())
			{
				if (this.IsStartElement)
				{
					string localName = this.m_reader.LocalName;
					if (!(localName == "Code"))
					{
						if (!(localName == "Severity"))
						{
							if (!(localName == "Text"))
							{
								if (!(localName == "ObjectType"))
								{
									if (!(localName == "ObjectName"))
									{
										if (!(localName == "PropertyName"))
										{
											this.SkipUnknownContent();
										}
										else
										{
											message.PropertyName = this.ReadString();
										}
									}
									else
									{
										message.ObjectName = this.ReadString();
									}
								}
								else
								{
									message.ObjectType = this.ReadString();
								}
							}
							else
							{
								message.Text = this.ReadString();
							}
						}
						else
						{
							message.Severity = this.ReadString();
						}
					}
					else
					{
						message.Code = this.ReadString();
					}
				}
			}
			return message;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000C620 File Offset: 0x0000A820
		private Identifier ReadIdentifier()
		{
			string text = this.ReadSimpleStringElement();
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return new Identifier(text);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000C644 File Offset: 0x0000A844
		private string ReadName()
		{
			string text = this.ReadSimpleStringElement();
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return text;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000C664 File Offset: 0x0000A864
		private Expression ReadExpression(ObjectType objectType, Identifier objectId, string propertyName)
		{
			string attribute = this.m_reader.GetAttribute("type");
			string text = this.ReadSimpleStringElement();
			if (attribute == "null")
			{
				return new Expression(this.ExpressionNodeNull);
			}
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return new Expression(this.ParseExpressionNode(attribute, text, objectType, objectId, propertyName));
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		private ExpressionNode ParseExpressionNode(string valueType, string content, ObjectType objectType, Identifier objectId, string propertyName)
		{
			if (valueType == "null")
			{
				return this.ExpressionNodeNull;
			}
			return ExpressionParser.ParseExpression(new ExpressionContext(this.m_errorContext, objectType, objectId, propertyName), content);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000C6EC File Offset: 0x0000A8EC
		private Candidate<bool> ReadBoolean()
		{
			bool flag;
			if (bool.TryParse(this.ReadSimpleStringElement(), out flag))
			{
				return flag;
			}
			return Candidate<bool>.Invalid;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000C714 File Offset: 0x0000A914
		private Candidate<int> ReadInt32()
		{
			int num;
			if (int.TryParse(this.ReadSimpleStringElement(), NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num))
			{
				return num;
			}
			return Candidate<int>.Invalid;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000C744 File Offset: 0x0000A944
		private long? ReadLong()
		{
			long num;
			if (long.TryParse(this.ReadSimpleStringElement(), NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num))
			{
				return new long?(num);
			}
			return null;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000C776 File Offset: 0x0000A976
		private string ReadString()
		{
			return this.ReadSimpleStringElement();
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000C780 File Offset: 0x0000A980
		private Candidate<T> ReadEnum<T>() where T : struct
		{
			T t;
			if (!Enum.TryParse<T>(this.ReadSimpleStringElement(), false, out t))
			{
				return Candidate<T>.Invalid;
			}
			return Candidate<T>.Valid(t);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000C7A9 File Offset: 0x0000A9A9
		private string ReadSimpleStringElement()
		{
			string text = this.m_reader.ReadString();
			if (!this.IsEndElement)
			{
				this.SkipUnknownContent();
			}
			return text;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000C7C4 File Offset: 0x0000A9C4
		private void SkipUnknownContent()
		{
			int num = 0;
			while (this.m_reader.Read() && (!this.IsEndElement || num > 0))
			{
				if (this.IsStartElement)
				{
					num++;
				}
				else if (this.IsEndElement)
				{
					num--;
				}
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000C808 File Offset: 0x0000AA08
		private bool IsStartElement
		{
			get
			{
				return this.m_reader.NodeType == XmlNodeType.Element;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000C818 File Offset: 0x0000AA18
		private bool IsEndElement
		{
			get
			{
				return this.m_reader.NodeType == XmlNodeType.EndElement;
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000C829 File Offset: 0x0000AA29
		private bool Advance()
		{
			return this.m_reader.Read() && !this.IsEndElement;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000C843 File Offset: 0x0000AA43
		[Conditional("DEBUG")]
		private void AssertReadMethodEnterState()
		{
			Microsoft.DataShaping.Contract.RetailAssert(this.IsStartElement, "Reader should be positioned at a StartElement");
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000C855 File Offset: 0x0000AA55
		[Conditional("DEBUG")]
		private void AssertReadMethodExitState()
		{
			Microsoft.DataShaping.Contract.RetailAssert(this.IsEndElement, "Reader should be positioned at an EndElement");
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000C867 File Offset: 0x0000AA67
		public void Dispose()
		{
			if (this.m_reader != null)
			{
				((IDisposable)this.m_reader).Dispose();
				this.m_reader = null;
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000C884 File Offset: 0x0000AA84
		public static DataShapeQuery ParseJson(Stream input)
		{
			DataShapeQuery dataShapeQuery;
			using (XmlDictionaryReader xmlDictionaryReader = JsonReaderWriterFactory.CreateJsonReader(input, DataShapeQueryParser.CreateReaderQuotas()))
			{
				dataShapeQuery = DataShapeQueryParser.Parse(xmlDictionaryReader);
			}
			return dataShapeQuery;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000C8C4 File Offset: 0x0000AAC4
		public static DataShapeQuery ParseXml(Stream input)
		{
			DataShapeQuery dataShapeQuery;
			using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(input, DataShapeQueryParser.CreateReaderQuotas()))
			{
				dataShapeQuery = DataShapeQueryParser.Parse(xmlDictionaryReader);
			}
			return dataShapeQuery;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000C904 File Offset: 0x0000AB04
		private static DataShapeQuery Parse(XmlDictionaryReader reader)
		{
			TranslationErrorContext translationErrorContext = new TranslationErrorContext();
			DataShapeQuery dataShapeQuery = null;
			try
			{
				reader.MoveToStartElement();
				using (DataShapeQueryParser dataShapeQueryParser = new DataShapeQueryParser(reader, translationErrorContext))
				{
					dataShapeQuery = dataShapeQueryParser.ReadDataShapeQuery();
				}
			}
			catch (Exception ex)
			{
				if (ErrorUtils.IsStoppingException(ex))
				{
					throw;
				}
				throw DataShapeQueryTranslationException.Create(DsqtStrings.InvalidDataShapeQuery(ex.Message));
			}
			if (translationErrorContext.HasError)
			{
				throw DataShapeQueryTranslationException.Create(translationErrorContext);
			}
			return dataShapeQuery;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000C984 File Offset: 0x0000AB84
		private static XmlDictionaryReaderQuotas CreateReaderQuotas()
		{
			return new XmlDictionaryReaderQuotas
			{
				MaxDepth = 40
			};
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000C994 File Offset: 0x0000AB94
		private List<RestartToken> ReadRestartTokens(Identifier objectId)
		{
			Func<Candidate<ScalarValue>> <>9__1;
			return this.ReadList<RestartToken>(delegate
			{
				DataShapeQueryParser <>4__this = this;
				Func<Candidate<ScalarValue>> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = () => this.ReadVariantValueInternal(ObjectType.RestartToken, objectId, "Value"));
				}
				return new RestartToken(<>4__this.ReadList<Candidate<ScalarValue>>(func));
			});
		}

		// Token: 0x0400018D RID: 397
		private const string TypeAttributeName = "type";

		// Token: 0x0400018E RID: 398
		private const string ItemAttributeName = "item";

		// Token: 0x0400018F RID: 399
		private const string TypeAttributeValueNull = "null";

		// Token: 0x04000190 RID: 400
		private readonly ExpressionNode ExpressionNodeNull = new LiteralExpressionNode(ScalarValue.Null);

		// Token: 0x04000191 RID: 401
		private const string TypeAttributeValueString = "string";

		// Token: 0x04000192 RID: 402
		private const string TypeAttributeValueNumber = "number";

		// Token: 0x04000193 RID: 403
		private const string TypeAttributeValueBoolean = "boolean";

		// Token: 0x04000194 RID: 404
		private XmlDictionaryReader m_reader;

		// Token: 0x04000195 RID: 405
		private TranslationErrorContext m_errorContext;
	}
}
