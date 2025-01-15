using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004A5 RID: 1189
	[Serializable]
	internal sealed class CustomReportItem : DataRegion, ICreateSubtotals, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003A5D RID: 14941 RVA: 0x000FD620 File Offset: 0x000FB820
		internal CustomReportItem(ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x06003A5E RID: 14942 RVA: 0x000FD630 File Offset: 0x000FB830
		internal CustomReportItem(int id, ReportItem parent)
			: base(id, parent)
		{
		}

		// Token: 0x17001935 RID: 6453
		// (get) Token: 0x06003A5F RID: 14943 RVA: 0x000FD641 File Offset: 0x000FB841
		internal override bool IsDataRegion
		{
			get
			{
				return this.m_isDataRegion;
			}
		}

		// Token: 0x17001936 RID: 6454
		// (get) Token: 0x06003A60 RID: 14944 RVA: 0x000FD649 File Offset: 0x000FB849
		internal override Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.CustomReportItem;
			}
		}

		// Token: 0x17001937 RID: 6455
		// (get) Token: 0x06003A61 RID: 14945 RVA: 0x000FD64D File Offset: 0x000FB84D
		internal override HierarchyNodeList ColumnMembers
		{
			get
			{
				return this.m_dataColumnMembers;
			}
		}

		// Token: 0x17001938 RID: 6456
		// (get) Token: 0x06003A62 RID: 14946 RVA: 0x000FD655 File Offset: 0x000FB855
		internal override HierarchyNodeList RowMembers
		{
			get
			{
				return this.m_dataRowMembers;
			}
		}

		// Token: 0x17001939 RID: 6457
		// (get) Token: 0x06003A63 RID: 14947 RVA: 0x000FD65D File Offset: 0x000FB85D
		internal override RowList Rows
		{
			get
			{
				return this.m_dataRows;
			}
		}

		// Token: 0x1700193A RID: 6458
		// (get) Token: 0x06003A64 RID: 14948 RVA: 0x000FD665 File Offset: 0x000FB865
		internal CustomReportItemExprHost CustomReportItemExprHost
		{
			get
			{
				return this.m_criExprHost;
			}
		}

		// Token: 0x1700193B RID: 6459
		// (get) Token: 0x06003A65 RID: 14949 RVA: 0x000FD66D File Offset: 0x000FB86D
		protected override IndexedExprHost UserSortExpressionsHost
		{
			get
			{
				if (this.m_criExprHost == null)
				{
					return null;
				}
				return this.m_criExprHost.UserSortExpressionsHost;
			}
		}

		// Token: 0x1700193C RID: 6460
		// (get) Token: 0x06003A66 RID: 14950 RVA: 0x000FD684 File Offset: 0x000FB884
		// (set) Token: 0x06003A67 RID: 14951 RVA: 0x000FD68C File Offset: 0x000FB88C
		internal DataMemberList DataColumnMembers
		{
			get
			{
				return this.m_dataColumnMembers;
			}
			set
			{
				this.m_dataColumnMembers = value;
			}
		}

		// Token: 0x1700193D RID: 6461
		// (get) Token: 0x06003A68 RID: 14952 RVA: 0x000FD695 File Offset: 0x000FB895
		// (set) Token: 0x06003A69 RID: 14953 RVA: 0x000FD69D File Offset: 0x000FB89D
		internal DataMemberList DataRowMembers
		{
			get
			{
				return this.m_dataRowMembers;
			}
			set
			{
				this.m_dataRowMembers = value;
			}
		}

		// Token: 0x1700193E RID: 6462
		// (get) Token: 0x06003A6A RID: 14954 RVA: 0x000FD6A6 File Offset: 0x000FB8A6
		// (set) Token: 0x06003A6B RID: 14955 RVA: 0x000FD6AE File Offset: 0x000FB8AE
		internal CustomDataRowList DataRows
		{
			get
			{
				return this.m_dataRows;
			}
			set
			{
				this.m_dataRows = value;
			}
		}

		// Token: 0x1700193F RID: 6463
		// (get) Token: 0x06003A6C RID: 14956 RVA: 0x000FD6B7 File Offset: 0x000FB8B7
		// (set) Token: 0x06003A6D RID: 14957 RVA: 0x000FD6BF File Offset: 0x000FB8BF
		internal string Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x17001940 RID: 6464
		// (get) Token: 0x06003A6E RID: 14958 RVA: 0x000FD6C8 File Offset: 0x000FB8C8
		// (set) Token: 0x06003A6F RID: 14959 RVA: 0x000FD6D0 File Offset: 0x000FB8D0
		internal ReportItem AltReportItem
		{
			get
			{
				return this.m_altReportItem;
			}
			set
			{
				this.m_altReportItem = value;
			}
		}

		// Token: 0x17001941 RID: 6465
		// (get) Token: 0x06003A70 RID: 14960 RVA: 0x000FD6D9 File Offset: 0x000FB8D9
		// (set) Token: 0x06003A71 RID: 14961 RVA: 0x000FD6E1 File Offset: 0x000FB8E1
		internal int AltReportItemIndexInParentCollectionDef
		{
			get
			{
				return this.m_altReportItemIndexInParentCollectionDef;
			}
			set
			{
				this.m_altReportItemIndexInParentCollectionDef = value;
			}
		}

		// Token: 0x17001942 RID: 6466
		// (get) Token: 0x06003A72 RID: 14962 RVA: 0x000FD6EA File Offset: 0x000FB8EA
		// (set) Token: 0x06003A73 RID: 14963 RVA: 0x000FD6F2 File Offset: 0x000FB8F2
		internal ReportItemCollection RenderReportItem
		{
			get
			{
				return this.m_renderReportItem;
			}
			set
			{
				this.m_renderReportItem = value;
			}
		}

		// Token: 0x17001943 RID: 6467
		// (get) Token: 0x06003A74 RID: 14964 RVA: 0x000FD6FB File Offset: 0x000FB8FB
		// (set) Token: 0x06003A75 RID: 14965 RVA: 0x000FD703 File Offset: 0x000FB903
		internal bool ExplicitlyDefinedAltReportItem
		{
			get
			{
				return this.m_explicitAltReportItemDefined;
			}
			set
			{
				this.m_explicitAltReportItemDefined = value;
			}
		}

		// Token: 0x06003A76 RID: 14966 RVA: 0x000FD70C File Offset: 0x000FB90C
		internal void SetAsDataRegion()
		{
			this.m_isDataRegion = true;
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x000FD718 File Offset: 0x000FB918
		internal override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			if (this.IsDataRegion)
			{
				if (!context.RegisterDataRegion(this))
				{
					return false;
				}
				context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataSet | Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion;
			}
			context.ExprHostBuilder.DataRegionStart(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.CustomReportItem, this.m_name);
			base.Initialize(context);
			base.ExprHostID = context.ExprHostBuilder.DataRegionEnd(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.CustomReportItem);
			if (this.IsDataRegion)
			{
				context.UnRegisterDataRegion(this);
			}
			return false;
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x000FD7A4 File Offset: 0x000FB9A4
		protected override bool ValidateInnerStructure(InitializationContext context)
		{
			if (!this.IsDataRegion)
			{
				return false;
			}
			if (this.m_dataRows != null && this.m_dataRows.Count != 0)
			{
				return true;
			}
			if (this.m_rowCount != 0 || this.m_columnCount != 0)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsWrongNumberOfDataRows, Severity.Error, context.ObjectType, context.ObjectName, this.m_rowCount.ToString(CultureInfo.InvariantCulture.NumberFormat), Array.Empty<string>());
				return false;
			}
			return false;
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x000FD820 File Offset: 0x000FBA20
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			CustomReportItem customReportItem = (CustomReportItem)base.PublishClone(context);
			context.CurrentDataRegionClone = customReportItem;
			if (this.m_dataColumnMembers != null)
			{
				customReportItem.m_dataColumnMembers = new DataMemberList(this.m_dataColumnMembers.Count);
				foreach (object obj in this.m_dataColumnMembers)
				{
					DataMember dataMember = (DataMember)obj;
					customReportItem.m_dataColumnMembers.Add(dataMember.PublishClone(context, customReportItem));
				}
			}
			if (this.m_dataRowMembers != null)
			{
				customReportItem.m_dataRowMembers = new DataMemberList(this.m_dataRowMembers.Count);
				foreach (object obj2 in this.m_dataRowMembers)
				{
					DataMember dataMember2 = (DataMember)obj2;
					customReportItem.m_dataRowMembers.Add(dataMember2.PublishClone(context, customReportItem));
				}
			}
			if (this.m_dataRows != null)
			{
				customReportItem.m_dataRows = new CustomDataRowList(this.m_dataRows.Count);
				foreach (object obj3 in this.m_dataRows)
				{
					CustomDataRow customDataRow = (CustomDataRow)obj3;
					customReportItem.m_dataRows.Add((CustomDataRow)customDataRow.PublishClone(context));
				}
			}
			context.CreateSubtotalsDefinitions.Add(customReportItem);
			return customReportItem;
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x000FD9B8 File Offset: 0x000FBBB8
		public void CreateAutomaticSubtotals(AutomaticSubtotalContext context)
		{
			if (this.m_createdSubtotals)
			{
				return;
			}
			if (!this.IsDataRegion)
			{
				return;
			}
			if (this.m_dataRows != null && this.m_rowCount == this.m_dataRows.Count)
			{
				for (int i = 0; i < this.m_dataRows.Count; i++)
				{
					if (this.m_dataRows[i].Cells == null || this.m_dataRows[i].Cells.Count != this.m_columnCount)
					{
						return;
					}
				}
				context.Location = Microsoft.ReportingServices.ReportPublishing.LocationFlags.None;
				context.ObjectType = this.ObjectType;
				context.ObjectName = "CustomReportItem";
				context.CurrentDataRegion = this;
				context.CurrentScope = base.DataSetName;
				context.CurrentDataScope = this;
				context.CellLists = new List<CellList>(this.m_dataRows.Count);
				for (int j = 0; j < this.m_dataRows.Count; j++)
				{
					context.CellLists.Add(new CellList());
				}
				context.Rows = new RowList(this.m_dataRows.Count);
				context.StartIndex = 0;
				this.CreateAutomaticSubtotals(context, this.m_dataColumnMembers, true);
				context.StartIndex = 0;
				this.CreateAutomaticSubtotals(context, this.m_dataRowMembers, false);
				context.CurrentScope = null;
				context.CurrentDataScope = null;
				this.m_createdSubtotals = true;
				return;
			}
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x000FDB18 File Offset: 0x000FBD18
		private int CreateAutomaticSubtotals(AutomaticSubtotalContext context, DataMemberList members, bool isColumn)
		{
			int num = 0;
			for (int i = 0; i < members.Count; i++)
			{
				DataMember dataMember = members[i];
				if (dataMember.Subtotal)
				{
					context.CurrentIndex = context.StartIndex;
					if (isColumn)
					{
						using (List<CellList>.Enumerator enumerator = context.CellLists.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								CellList cellList = enumerator.Current;
								cellList.Clear();
							}
							goto IL_006E;
						}
						goto IL_0062;
					}
					goto IL_0062;
					IL_006E:
					base.BuildAndSetupAxisScopeTreeForAutoSubtotals(ref context, dataMember);
					DataMember dataMember2 = (DataMember)dataMember.PublishClone(context, null, true);
					context.AdjustReferences();
					dataMember2.IsAutoSubtotal = true;
					dataMember2.Subtotal = false;
					members.Insert(i + 1, dataMember2);
					num = context.CurrentIndex - context.StartIndex;
					if (isColumn)
					{
						int num2 = 0;
						while (i < this.m_dataRows.Count)
						{
							this.m_dataRows[num2].Cells.InsertRange(context.CurrentIndex, context.CellLists[num2]);
							num2++;
						}
						this.m_columnCount += num;
					}
					else
					{
						this.m_dataRows.InsertRange(context.CurrentIndex, context.Rows);
						this.m_rowCount += num;
					}
					if (dataMember.SubMembers != null)
					{
						context.CurrentScope = dataMember.Grouping.Name;
						context.CurrentDataScope = dataMember;
						int num3 = this.CreateAutomaticSubtotals(context, dataMember.SubMembers, isColumn);
						if (isColumn)
						{
							dataMember.ColSpan += num3;
						}
						else
						{
							dataMember.RowSpan += num3;
						}
						num += num3;
						goto IL_021F;
					}
					int num4 = context.StartIndex;
					context.StartIndex = num4 + 1;
					goto IL_021F;
					IL_0062:
					context.Rows.Clear();
					goto IL_006E;
				}
				if (dataMember.SubMembers != null)
				{
					if (dataMember.Grouping != null)
					{
						context.CurrentScope = dataMember.Grouping.Name;
						context.CurrentDataScope = dataMember;
					}
					int num5 = this.CreateAutomaticSubtotals(context, dataMember.SubMembers, isColumn);
					if (isColumn)
					{
						dataMember.ColSpan += num5;
					}
					else
					{
						dataMember.RowSpan += num5;
					}
					num += num5;
				}
				else
				{
					int num4 = context.StartIndex;
					context.StartIndex = num4 + 1;
				}
				IL_021F:;
			}
			return num;
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x000FDD68 File Offset: 0x000FBF68
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CustomReportItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegion, new List<MemberInfo>
			{
				new MemberInfo(MemberName.DataColumnMembers, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataMember),
				new MemberInfo(MemberName.DataRowMembers, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataMember),
				new MemberInfo(MemberName.DataRows, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CustomDataRow),
				new MemberInfo(MemberName.Type, Token.String),
				new MemberInfo(MemberName.AltReportItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItem, Token.Reference),
				new MemberInfo(MemberName.RenderReportItemColDef, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItemCollection),
				new MemberInfo(MemberName.AltReportItemIndexInParentCollectionDef, Token.Int32),
				new MemberInfo(MemberName.ExplicitAltReportItem, Token.Boolean),
				new MemberInfo(MemberName.IsDataRegion, Token.Boolean)
			});
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x000FDE4C File Offset: 0x000FC04C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(CustomReportItem.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.AltReportItem)
				{
					if (memberName == MemberName.Type)
					{
						writer.Write(this.m_type);
						continue;
					}
					if (memberName == MemberName.RenderReportItemColDef)
					{
						writer.Write(this.m_renderReportItem);
						continue;
					}
					switch (memberName)
					{
					case MemberName.DataColumnMembers:
						writer.Write(this.m_dataColumnMembers);
						continue;
					case MemberName.DataRowMembers:
						writer.Write(this.m_dataRowMembers);
						continue;
					case MemberName.DataRows:
						writer.Write(this.m_dataRows);
						continue;
					case MemberName.AltReportItem:
						writer.WriteReference(this.m_altReportItem);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.AltReportItemIndexInParentCollectionDef)
					{
						writer.Write(this.m_altReportItemIndexInParentCollectionDef);
						continue;
					}
					if (memberName == MemberName.ExplicitAltReportItem)
					{
						writer.Write(this.m_explicitAltReportItemDefined);
						continue;
					}
					if (memberName == MemberName.IsDataRegion)
					{
						writer.Write(this.m_isDataRegion);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x000FDF84 File Offset: 0x000FC184
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			this.m_isDataRegion = this.m_dataSetName != null;
			reader.RegisterDeclaration(CustomReportItem.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.AltReportItem)
				{
					if (memberName == MemberName.Type)
					{
						this.m_type = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.RenderReportItemColDef)
					{
						this.m_renderReportItem = (ReportItemCollection)reader.ReadRIFObject();
						continue;
					}
					switch (memberName)
					{
					case MemberName.DataColumnMembers:
						this.m_dataColumnMembers = reader.ReadListOfRIFObjects<DataMemberList>();
						continue;
					case MemberName.DataRowMembers:
						this.m_dataRowMembers = reader.ReadListOfRIFObjects<DataMemberList>();
						continue;
					case MemberName.DataRows:
						this.m_dataRows = reader.ReadListOfRIFObjects<CustomDataRowList>();
						continue;
					case MemberName.AltReportItem:
						this.m_altReportItem = reader.ReadReference<ReportItem>(this);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.AltReportItemIndexInParentCollectionDef)
					{
						this.m_altReportItemIndexInParentCollectionDef = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.ExplicitAltReportItem)
					{
						this.m_explicitAltReportItemDefined = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.IsDataRegion)
					{
						this.m_isDataRegion = reader.ReadBoolean();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x000FE0D4 File Offset: 0x000FC2D4
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(CustomReportItem.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.AltReportItem)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_altReportItem = (ReportItem)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x000FE178 File Offset: 0x000FC378
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CustomReportItem;
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x000FE180 File Offset: 0x000FC380
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null, "(reportExprHost != null && reportObjectModel != null)");
				this.m_criExprHost = reportExprHost.CustomReportItemHostsRemotable[base.ExprHostID];
				base.DataRegionSetExprHost(this.m_criExprHost, this.m_criExprHost.SortHost, this.m_criExprHost.FilterHostsRemotable, this.m_criExprHost.UserSortExpressionsHost, this.m_criExprHost.PageBreakExprHost, this.m_criExprHost.JoinConditionExprHostsRemotable, reportObjectModel);
			}
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x000FE20C File Offset: 0x000FC40C
		internal override void DataRegionContentsSetExprHost(ObjectModelImpl reportObjectModel, bool traverseDataRegions)
		{
			if (this.m_dataRows != null && this.m_dataRows.Count > 0)
			{
				IList<DataCellExprHost> list = ((this.m_criExprHost != null) ? this.m_criExprHost.CellHostsRemotable : null);
				if (list != null)
				{
					for (int i = 0; i < this.m_dataRows.Count; i++)
					{
						CustomDataRow customDataRow = this.m_dataRows[i];
						Global.Tracer.Assert(customDataRow != null && customDataRow.Cells != null, "(null != row && null != row.Cells)");
						for (int j = 0; j < customDataRow.DataCells.Count; j++)
						{
							DataCell dataCell = customDataRow.DataCells[j];
							Global.Tracer.Assert(dataCell != null && dataCell.DataValues != null, "(null != cell && null != cell.DataValues)");
							if (dataCell.ExpressionHostID >= 0)
							{
								dataCell.DataValues.SetExprHost(list[dataCell.ExpressionHostID].DataValueHostsRemotable, reportObjectModel);
							}
						}
					}
					return;
				}
			}
			else
			{
				Global.Tracer.Assert(this.m_criExprHost == null || this.m_criExprHost.CellHostsRemotable == null || this.m_criExprHost.CellHostsRemotable.Count == 0);
			}
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x000FE33C File Offset: 0x000FC53C
		internal override object EvaluateNoRowsMessageExpression()
		{
			return this.m_criExprHost.NoRowsExpr;
		}

		// Token: 0x06003A84 RID: 14980 RVA: 0x000FE349 File Offset: 0x000FC549
		protected override ReportHierarchyNode CreateHierarchyNode(int id)
		{
			return new DataMember(id, this);
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x000FE352 File Offset: 0x000FC552
		protected override Row CreateRow(int id, int columnCount)
		{
			return new CustomDataRow(id)
			{
				DataCells = new DataCellList(columnCount)
			};
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x000FE366 File Offset: 0x000FC566
		protected override Cell CreateCell(int id, int rowIndex, int colIndex)
		{
			return new DataCell(id, this);
		}

		// Token: 0x04001BDF RID: 7135
		private DataMemberList m_dataColumnMembers;

		// Token: 0x04001BE0 RID: 7136
		private DataMemberList m_dataRowMembers;

		// Token: 0x04001BE1 RID: 7137
		private CustomDataRowList m_dataRows;

		// Token: 0x04001BE2 RID: 7138
		private bool m_isDataRegion;

		// Token: 0x04001BE3 RID: 7139
		private string m_type;

		// Token: 0x04001BE4 RID: 7140
		private ReportItem m_altReportItem;

		// Token: 0x04001BE5 RID: 7141
		private int m_altReportItemIndexInParentCollectionDef = -1;

		// Token: 0x04001BE6 RID: 7142
		private ReportItemCollection m_renderReportItem;

		// Token: 0x04001BE7 RID: 7143
		private bool m_explicitAltReportItemDefined;

		// Token: 0x04001BE8 RID: 7144
		[NonSerialized]
		private bool m_createdSubtotals;

		// Token: 0x04001BE9 RID: 7145
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = CustomReportItem.GetDeclaration();

		// Token: 0x04001BEA RID: 7146
		[NonSerialized]
		private CustomReportItemExprHost m_criExprHost;
	}
}
