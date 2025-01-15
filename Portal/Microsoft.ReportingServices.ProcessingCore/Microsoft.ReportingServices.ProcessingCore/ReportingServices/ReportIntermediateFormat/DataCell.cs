using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004A9 RID: 1193
	[Serializable]
	internal sealed class DataCell : Cell, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003AA4 RID: 15012 RVA: 0x000FE704 File Offset: 0x000FC904
		internal DataCell()
		{
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x000FE70C File Offset: 0x000FC90C
		internal DataCell(int id, DataRegion dataRegion)
			: base(id, dataRegion)
		{
		}

		// Token: 0x1700194C RID: 6476
		// (get) Token: 0x06003AA6 RID: 15014 RVA: 0x000FE716 File Offset: 0x000FC916
		protected override bool IsDataRegionBodyCell
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700194D RID: 6477
		// (get) Token: 0x06003AA7 RID: 15015 RVA: 0x000FE719 File Offset: 0x000FC919
		// (set) Token: 0x06003AA8 RID: 15016 RVA: 0x000FE721 File Offset: 0x000FC921
		internal DataValueList DataValues
		{
			get
			{
				return this.m_dataValues;
			}
			set
			{
				this.m_dataValues = value;
			}
		}

		// Token: 0x1700194E RID: 6478
		// (get) Token: 0x06003AA9 RID: 15017 RVA: 0x000FE72A File Offset: 0x000FC92A
		public override Microsoft.ReportingServices.ReportProcessing.ObjectType DataScopeObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.DataCell;
			}
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x000FE72E File Offset: 0x000FC92E
		internal override void InternalInitialize(int parentRowID, int parentColumnID, int rowindex, int colIndex, InitializationContext context)
		{
			this.m_dataValues.Initialize(null, rowindex, colIndex, false, context);
		}

		// Token: 0x1700194F RID: 6479
		// (get) Token: 0x06003AAB RID: 15019 RVA: 0x000FE742 File Offset: 0x000FC942
		protected override Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode ExprHostDataRegionMode
		{
			get
			{
				return Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.CustomReportItem;
			}
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x000FE748 File Offset: 0x000FC948
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			DataCell dataCell = (DataCell)base.PublishClone(context);
			if (this.m_dataValues != null)
			{
				dataCell.m_dataValues = new DataValueList(this.m_dataValues.Count);
				foreach (object obj in this.m_dataValues)
				{
					DataValue dataValue = (DataValue)obj;
					dataCell.m_dataValues.Add(dataValue.PublishClone(context));
				}
			}
			return dataCell;
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x000FE7DC File Offset: 0x000FC9DC
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataCell, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Cell, new List<MemberInfo>
			{
				new MemberInfo(MemberName.DataValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataValue)
			});
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x000FE818 File Offset: 0x000FCA18
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(DataCell.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.DataValues)
				{
					writer.Write(this.m_dataValues);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x000FE870 File Offset: 0x000FCA70
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(DataCell.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.DataValues)
				{
					this.m_dataValues = reader.ReadListOfRIFObjects<DataValueList>();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x000FE8C8 File Offset: 0x000FCAC8
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x000FE8D2 File Offset: 0x000FCAD2
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataCell;
		}

		// Token: 0x04001BF0 RID: 7152
		private DataValueList m_dataValues;

		// Token: 0x04001BF1 RID: 7153
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = DataCell.GetDeclaration();
	}
}
