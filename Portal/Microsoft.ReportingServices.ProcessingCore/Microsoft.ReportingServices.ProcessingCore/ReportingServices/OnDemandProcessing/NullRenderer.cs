using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000829 RID: 2089
	internal sealed class NullRenderer
	{
		// Token: 0x0600754B RID: 30027 RVA: 0x001E5CD2 File Offset: 0x001E3ED2
		internal NullRenderer()
		{
		}

		// Token: 0x0600754C RID: 30028 RVA: 0x001E5CDC File Offset: 0x001E3EDC
		internal void Process(Microsoft.ReportingServices.OnDemandReportRendering.Report report, OnDemandProcessingContext odpContext, bool generateDocumentMap, bool createSnapshot)
		{
			this.m_odpContext = odpContext;
			this.m_report = report;
			this.m_generateDocMap = generateDocumentMap && this.m_report.HasDocumentMap;
			this.m_createSnapshot = createSnapshot;
			if (this.m_generateDocMap)
			{
				odpContext.HasRenderFormatDependencyInDocumentMap = false;
			}
			if (this.m_generateDocMap || this.m_createSnapshot)
			{
				foreach (Microsoft.ReportingServices.OnDemandReportRendering.ReportSection reportSection in report.ReportSections)
				{
					this.Visit(reportSection);
				}
			}
			if (this.m_generateDocMap && this.m_docMapWriter != null)
			{
				this.m_docMapWriter.WriteEndContainer();
				this.m_docMapWriter.Close();
				this.m_docMapWriter = null;
				if (odpContext.HasRenderFormatDependencyInDocumentMap)
				{
					odpContext.OdpMetadata.ReportSnapshot.SetRenderFormatDependencyInDocumentMap(odpContext);
				}
			}
		}

		// Token: 0x0600754D RID: 30029 RVA: 0x001E5DBC File Offset: 0x001E3FBC
		private void Visit(Microsoft.ReportingServices.OnDemandReportRendering.ReportSection section)
		{
			this.Visit(section.Body.ReportItemCollection);
			this.VisitStyle(section.Body.Style);
			this.VisitStyle(section.Page.Style);
		}

		// Token: 0x0600754E RID: 30030 RVA: 0x001E5DF4 File Offset: 0x001E3FF4
		private void Visit(Microsoft.ReportingServices.OnDemandReportRendering.ReportItemCollection itemCollection)
		{
			for (int i = 0; i < itemCollection.Count; i++)
			{
				this.Visit(itemCollection[i]);
			}
		}

		// Token: 0x0600754F RID: 30031 RVA: 0x001E5E20 File Offset: 0x001E4020
		private void Visit(Microsoft.ReportingServices.OnDemandReportRendering.ReportItem item)
		{
			if (item == null || item.Instance == null)
			{
				return;
			}
			bool generateDocMap = this.m_generateDocMap;
			if (!this.ProcessVisibilityAndContinue(item.Visibility, item.Instance.Visibility, null))
			{
				return;
			}
			if (item is Microsoft.ReportingServices.OnDemandReportRendering.Line || item is Microsoft.ReportingServices.OnDemandReportRendering.Chart || item is Microsoft.ReportingServices.OnDemandReportRendering.GaugePanel || item is Microsoft.ReportingServices.OnDemandReportRendering.Map)
			{
				this.GenerateSimpleReportItemDocumentMap(item);
			}
			else if (item is Microsoft.ReportingServices.OnDemandReportRendering.TextBox)
			{
				this.GenerateSimpleReportItemDocumentMap(item);
				this.VisitStyle(item.Style);
			}
			else if (item is Microsoft.ReportingServices.OnDemandReportRendering.Image)
			{
				this.GenerateSimpleReportItemDocumentMap(item);
				Microsoft.ReportingServices.OnDemandReportRendering.Image image = item as Microsoft.ReportingServices.OnDemandReportRendering.Image;
				Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType source = image.Source;
				if (this.m_createSnapshot && (source == Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType.External || source == Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType.Database))
				{
					Microsoft.ReportingServices.OnDemandReportRendering.ImageInstance imageInstance = image.Instance as Microsoft.ReportingServices.OnDemandReportRendering.ImageInstance;
					if (imageInstance != null)
					{
						byte[] imageData = imageInstance.ImageData;
					}
				}
			}
			else if (item is Microsoft.ReportingServices.OnDemandReportRendering.Rectangle)
			{
				this.VisitRectangle(item as Microsoft.ReportingServices.OnDemandReportRendering.Rectangle);
				this.VisitStyle(item.Style);
			}
			else if (!(item is Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem))
			{
				bool flag = false;
				if (this.m_generateDocMap)
				{
					string documentMapLabel = item.Instance.DocumentMapLabel;
					if (documentMapLabel != null)
					{
						flag = true;
						this.WriteDocumentMapBeginContainer(documentMapLabel, item.Instance.UniqueName);
					}
				}
				if (item is Microsoft.ReportingServices.OnDemandReportRendering.Tablix)
				{
					this.VisitTablix(item as Microsoft.ReportingServices.OnDemandReportRendering.Tablix);
					this.VisitStyle(item.Style);
				}
				else if (item is Microsoft.ReportingServices.OnDemandReportRendering.SubReport)
				{
					this.VisitSubReport(item as Microsoft.ReportingServices.OnDemandReportRendering.SubReport);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
				if (flag)
				{
					this.WriteDocumentMapEndContainer();
				}
			}
			this.m_generateDocMap = generateDocMap;
		}

		// Token: 0x06007550 RID: 30032 RVA: 0x001E5FAC File Offset: 0x001E41AC
		private void GenerateSimpleReportItemDocumentMap(Microsoft.ReportingServices.OnDemandReportRendering.ReportItem item)
		{
			if (this.m_generateDocMap)
			{
				string documentMapLabel = item.Instance.DocumentMapLabel;
				if (documentMapLabel != null)
				{
					this.WriteDocumentMapNode(documentMapLabel, item.Instance.UniqueName);
				}
			}
		}

		// Token: 0x06007551 RID: 30033 RVA: 0x001E5FE4 File Offset: 0x001E41E4
		private void VisitRectangle(Microsoft.ReportingServices.OnDemandReportRendering.Rectangle rectangleDef)
		{
			bool flag = false;
			if (this.m_generateDocMap)
			{
				string documentMapLabel = rectangleDef.Instance.DocumentMapLabel;
				if (documentMapLabel != null)
				{
					flag = true;
					int linkToChild = rectangleDef.LinkToChild;
					string text;
					if (linkToChild >= 0)
					{
						text = rectangleDef.ReportItemCollection[linkToChild].Instance.UniqueName;
					}
					else
					{
						text = rectangleDef.Instance.UniqueName;
					}
					this.WriteDocumentMapBeginContainer(documentMapLabel, text);
				}
			}
			this.Visit(rectangleDef.ReportItemCollection);
			if (flag)
			{
				this.WriteDocumentMapEndContainer();
			}
		}

		// Token: 0x06007552 RID: 30034 RVA: 0x001E605C File Offset: 0x001E425C
		private void VisitSubReport(Microsoft.ReportingServices.OnDemandReportRendering.SubReport subreportDef)
		{
			if (subreportDef.Report != null && subreportDef.Instance != null && !subreportDef.ProcessedWithError)
			{
				Microsoft.ReportingServices.OnDemandReportRendering.Report report = subreportDef.Report;
				if (report.HasDocumentMap || this.m_createSnapshot)
				{
					foreach (Microsoft.ReportingServices.OnDemandReportRendering.ReportSection reportSection in report.ReportSections)
					{
						this.Visit(reportSection.Body.ReportItemCollection);
						this.VisitStyle(reportSection.Body.Style);
					}
				}
			}
		}

		// Token: 0x06007553 RID: 30035 RVA: 0x001E60F4 File Offset: 0x001E42F4
		private void VisitTablix(Microsoft.ReportingServices.OnDemandReportRendering.Tablix tablixDef)
		{
			if (tablixDef.Corner != null)
			{
				TablixCornerRowCollection rowCollection = tablixDef.Corner.RowCollection;
				for (int i = 0; i < rowCollection.Count; i++)
				{
					TablixCornerRow tablixCornerRow = rowCollection[i];
					if (tablixCornerRow != null)
					{
						for (int j = 0; j < tablixCornerRow.Count; j++)
						{
							Microsoft.ReportingServices.OnDemandReportRendering.TablixCornerCell tablixCornerCell = tablixCornerRow[j];
							if (tablixCornerCell != null)
							{
								this.Visit(tablixCornerCell.CellContents.ReportItem);
							}
						}
					}
				}
			}
			this.VisitTablixMemberCollection(tablixDef.ColumnHierarchy.MemberCollection, -1, true);
			this.VisitTablixMemberCollection(tablixDef.RowHierarchy.MemberCollection, -1, true);
		}

		// Token: 0x06007554 RID: 30036 RVA: 0x001E6188 File Offset: 0x001E4388
		private void VisitTablixMemberCollection(TablixMemberCollection memberCollection, int rowMemberIndex, bool isTopLevel)
		{
			if (memberCollection == null)
			{
				return;
			}
			for (int i = 0; i < memberCollection.Count; i++)
			{
				Microsoft.ReportingServices.OnDemandReportRendering.TablixMember tablixMember = memberCollection[i];
				if (tablixMember.IsStatic)
				{
					this.VisitTablixMember(tablixMember, rowMemberIndex, null);
				}
				else
				{
					TablixDynamicMemberInstance tablixDynamicMemberInstance = (TablixDynamicMemberInstance)tablixMember.Instance;
					Stack<int> stack = new Stack<int>();
					if (isTopLevel)
					{
						tablixDynamicMemberInstance.ResetContext();
					}
					while (tablixDynamicMemberInstance.MoveNext())
					{
						this.VisitTablixMember(tablixMember, rowMemberIndex, stack);
					}
					for (int j = 0; j < stack.Count; j++)
					{
						this.WriteDocumentMapEndContainer();
					}
				}
			}
		}

		// Token: 0x06007555 RID: 30037 RVA: 0x001E6210 File Offset: 0x001E4410
		private void VisitTablixMember(Microsoft.ReportingServices.OnDemandReportRendering.TablixMember memberDef, int rowMemberIndex, Stack<int> openRecursiveLevels)
		{
			if (memberDef.Instance == null)
			{
				return;
			}
			bool generateDocMap = this.m_generateDocMap;
			if (!this.ProcessVisibilityAndContinue(memberDef.Visibility, memberDef.Instance.Visibility, memberDef))
			{
				return;
			}
			if (!memberDef.IsStatic && rowMemberIndex == -1 && memberDef.Group != null && this.m_generateDocMap)
			{
				GroupInstance instance = memberDef.Group.Instance;
				string documentMapLabel = instance.DocumentMapLabel;
				int recursiveLevel = instance.RecursiveLevel;
				if (documentMapLabel != null)
				{
					while (openRecursiveLevels.Count > 0 && openRecursiveLevels.Peek() >= recursiveLevel)
					{
						this.WriteDocumentMapEndContainer();
						openRecursiveLevels.Pop();
					}
					this.WriteDocumentMapBeginContainer(documentMapLabel, memberDef.Group.Instance.UniqueName);
					openRecursiveLevels.Push(recursiveLevel);
				}
			}
			if (rowMemberIndex == -1 && memberDef.TablixHeader != null && memberDef.TablixHeader.CellContents != null)
			{
				this.Visit(memberDef.TablixHeader.CellContents.ReportItem);
			}
			if (memberDef.Children == null)
			{
				if (memberDef.IsColumn)
				{
					if (rowMemberIndex != -1)
					{
						Microsoft.ReportingServices.OnDemandReportRendering.TablixCell tablixCell = memberDef.OwnerTablix.Body.RowCollection[rowMemberIndex][memberDef.MemberCellIndex];
						if (tablixCell != null && tablixCell.CellContents != null)
						{
							this.Visit(tablixCell.CellContents.ReportItem);
						}
					}
				}
				else
				{
					this.VisitTablixMemberCollection(memberDef.OwnerTablix.ColumnHierarchy.MemberCollection, memberDef.MemberCellIndex, true);
				}
			}
			else
			{
				this.VisitTablixMemberCollection(memberDef.Children, rowMemberIndex, false);
			}
			this.m_generateDocMap = generateDocMap;
		}

		// Token: 0x06007556 RID: 30038 RVA: 0x001E6378 File Offset: 0x001E4578
		private void VisitStyle(Microsoft.ReportingServices.OnDemandReportRendering.Style style)
		{
			if (style == null || !this.m_createSnapshot)
			{
				return;
			}
			BackgroundImage backgroundImage = style.BackgroundImage;
			if (backgroundImage == null || backgroundImage.Source == Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType.Embedded || backgroundImage.Instance == null)
			{
				return;
			}
			byte[] imageData = backgroundImage.Instance.ImageData;
		}

		// Token: 0x06007557 RID: 30039 RVA: 0x001E63BC File Offset: 0x001E45BC
		private bool ProcessVisibilityAndContinue(Microsoft.ReportingServices.OnDemandReportRendering.Visibility aVisibility, VisibilityInstance aVisibilityInstance, Microsoft.ReportingServices.OnDemandReportRendering.TablixMember memberDef)
		{
			if (aVisibility == null)
			{
				return true;
			}
			if (aVisibilityInstance != null && this.m_createSnapshot)
			{
				bool startHidden = aVisibilityInstance.StartHidden;
			}
			SharedHiddenState hiddenState = aVisibility.HiddenState;
			if (hiddenState != SharedHiddenState.Always)
			{
				if (hiddenState == SharedHiddenState.Sometimes)
				{
					if (aVisibilityInstance.CurrentlyHidden && aVisibility.ToggleItem == null)
					{
						if (this.m_createSnapshot)
						{
							this.m_generateDocMap = false;
							return true;
						}
						return false;
					}
				}
				else if (memberDef != null && memberDef.IsTotal)
				{
					if (this.m_createSnapshot)
					{
						this.m_generateDocMap = false;
						return true;
					}
					return false;
				}
				return true;
			}
			if (this.m_createSnapshot)
			{
				this.m_generateDocMap = false;
				return true;
			}
			return false;
		}

		// Token: 0x06007558 RID: 30040 RVA: 0x001E6444 File Offset: 0x001E4644
		private void InitWriter()
		{
			this.m_docMapStream = this.m_odpContext.ChunkFactory.CreateChunk("DocumentMap", ReportProcessing.ReportChunkTypes.Interactivity, null);
			this.m_docMapWriter = new DocumentMapWriter(this.m_docMapStream, this.m_odpContext);
			this.m_docMapWriter.WriteBeginContainer(this.m_report.Name, this.m_report.Instance.UniqueName);
		}

		// Token: 0x06007559 RID: 30041 RVA: 0x001E64AB File Offset: 0x001E46AB
		private void WriteDocumentMapNode(string aLabel, string aId)
		{
			if (this.m_docMapWriter == null)
			{
				this.InitWriter();
			}
			this.m_docMapWriter.WriteNode(aLabel, aId);
		}

		// Token: 0x0600755A RID: 30042 RVA: 0x001E64C8 File Offset: 0x001E46C8
		private void WriteDocumentMapBeginContainer(string aLabel, string aId)
		{
			if (this.m_docMapWriter == null)
			{
				this.InitWriter();
			}
			this.m_docMapWriter.WriteBeginContainer(aLabel, aId);
		}

		// Token: 0x0600755B RID: 30043 RVA: 0x001E64E5 File Offset: 0x001E46E5
		private void WriteDocumentMapEndContainer()
		{
			if (this.m_docMapWriter == null)
			{
				this.InitWriter();
			}
			this.m_docMapWriter.WriteEndContainer();
		}

		// Token: 0x17002794 RID: 10132
		// (get) Token: 0x0600755C RID: 30044 RVA: 0x001E6500 File Offset: 0x001E4700
		internal Stream DocumentMapStream
		{
			get
			{
				return this.m_docMapStream;
			}
		}

		// Token: 0x04003B88 RID: 15240
		private OnDemandProcessingContext m_odpContext;

		// Token: 0x04003B89 RID: 15241
		private DocumentMapWriter m_docMapWriter;

		// Token: 0x04003B8A RID: 15242
		private Stream m_docMapStream;

		// Token: 0x04003B8B RID: 15243
		private bool m_generateDocMap;

		// Token: 0x04003B8C RID: 15244
		private bool m_createSnapshot;

		// Token: 0x04003B8D RID: 15245
		private Microsoft.ReportingServices.OnDemandReportRendering.Report m_report;
	}
}
